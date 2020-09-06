using Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

public static class Upload
{
    public static DataTable GetTableByFile(string path, string filename, out string msg, int NotNullCol = 1)
    {
        DataTable dt = new DataTable();
        msg = "";
        try
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + path + filename + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dtName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            if (dtName.Rows.Count > 0)
            {
                string sheetname = dtName.Rows[0]["TABLE_NAME"] != null && dtName.Rows[0]["TABLE_NAME"].ToString() != "" ? dtName.Rows[0]["TABLE_NAME"].ToString() : "";
                if (sheetname != "")
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "] where " + (NotNullCol == 1 ? " F1 is not null or F2 is not null or F3 is not null or F4 is not null or F5 is not null " : " F1 is not null or F2 is not null or F" + NotNullCol + " is not null "), conn);
                    // OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "] ", conn);

                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            dt = null;
            msg = ex.Message;
        }
        return dt;
    }

    public static DataTable _GetTableByFile(string path, string filename, out string msg, int NotNullCol = 1)
    {
        DataTable dt = new DataTable();
        msg = "";
        try
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + path + filename + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable dtName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
            if (dtName.Rows.Count > 0)
            {
                string sheetname = dtName.Rows[0]["TABLE_NAME"] != null && dtName.Rows[0]["TABLE_NAME"].ToString() != "" ? dtName.Rows[0]["TABLE_NAME"].ToString() : "";
                if (sheetname != "")
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "] where " + (NotNullCol == 1 ? " F1 is not null or F2 is not null or F3 is not null " : " F1 is not null or F2 is not null or or F3 is not null or F" + NotNullCol + " is not null "), conn);
                    // OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetname + "] ", conn);

                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            dt = null;
            msg = ex.Message;
        }
        return dt;
    }


    public static IList<infoDetail> GetInfoFromXls(string file)
    {
        var list = new List<infoDetail>();
        var ext = Path.GetExtension(file);
        using (FileStream fs = File.OpenRead(file))
        {
            
            ISheet sheet = null;
            if (ext == ".xls")
            {
                HSSFWorkbook wk = new HSSFWorkbook(fs);   //把xls文件中的数据写入wk中
                sheet = wk.GetSheetAt(0);
            }
            else if (ext == ".xlsx")
            {
                XSSFWorkbook wk = new XSSFWorkbook(fs);
                sheet = wk.GetSheetAt(0);
            }
            if (sheet == null)
            {
                return list;
            }
            if (sheet.LastRowNum < 1)
            {
                return list;
            }

            IRow fisrtRow = sheet.GetRow(0);  //检测第一行标题
            if (fisrtRow == null)
            {
                return list;
            }


            for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
            {
                IRow row = sheet.GetRow(j);  //读取当前行数据
                if (row != null)
                {
                    var item = new infoDetail
                    {
                        jobnumber = row.GetCellStr(0),//不应该上传这个字段 
                        putrecSeqno = row.GetCellNumber(1),
                        SKU = row.GetCellStr(2),
                        GoodsName = row.GetCellStr(3),
                        HSCode = row.GetCellStr(4),
                        gdsSpcfModelDesc = row.GetCellStr(5),
                        dcl_QTY = row.GetCellDecimal(6),
                        dclUnitcd = row.GetCellStr(7),
                        law_QTY = row.GetCellDecimal(8),
                        lawfUnitcd = row.GetCellStr(9),
                        Volume = row.GetCellDecimal(10),
                        grossWt = row.GetCellDecimal(11),
                        netWt = row.GetCellDecimal(12),
                        Origin = row.GetCellStr(13),
                        batch = row.GetCellStr(14),
                        unitprice = row.GetCellDecimal(15),
                        totalamount = row.GetCellDecimal(16),
                        curr = row.GetCellStr(17),
                        goodsStatus = row.GetCellStr(18)
                    };

                    list.Add(item);

                }
            }
            return list;
        }
    }


    static string GetCellStr(this IRow row, int index)
    {
        return row.GetCell(index) == null ? "" : row.GetCell(index).ToString();
    }

    static int GetCellNumber(this IRow row, int index)
    {
        return row.GetCell(index) == null ? 0 : Convert.ToInt32(row.GetCell(index).ToString());
    }

    static decimal GetCellDecimal(this IRow row, int index)
    {
        return row.GetCell(index) == null ? 0 : Convert.ToDecimal(row.GetCell(index).ToString());
    }





}

