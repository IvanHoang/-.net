using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// NPOI_ExcelToDataTable 的摘要说明
/// </summary>
public class NPOI_ExcelToDataTable
{
    public NPOI_ExcelToDataTable()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }/// <summary>
    /// Excel文件导成Datatable
    /// </summary>
    /// <param name="strFilePath">Excel文件目录地址</param>
    /// <param name="strTableName">Datatable表名</param>
    /// <param name="iSheetIndex">Excel sheet index</param>
    /// <returns></returns>
    public static DataTable XlSToDataTable(string strFilePath, string strTableName, int iSheetIndex)
    {

        string strExtName = Path.GetExtension(strFilePath);

        DataTable dt = new DataTable();
        if (!string.IsNullOrEmpty(strTableName))
        {
            dt.TableName = strTableName;
        }

        if (strExtName.Equals(".xls") || strExtName.Equals(".xlsx"))
        {
            using (FileStream file = new FileStream(strFilePath, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(iSheetIndex);

                //列头
                foreach (ICell item in sheet.GetRow(sheet.FirstRowNum).Cells)
                {
                    dt.Columns.Add(item.ToString(), typeof(string));
                }

                //写入内容
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                while (rows.MoveNext())
                {
                    IRow row = (HSSFRow)rows.Current;
                    if (row.RowNum == sheet.FirstRowNum)
                    {
                        continue;
                    }

                    DataRow dr = dt.NewRow();
                    foreach (ICell item in row.Cells)
                    {
                        switch (item.CellType)
                        {
                            case CellType.Boolean:
                                dr[item.ColumnIndex] = item.BooleanCellValue;
                                break;
                            case CellType.Error:
                                dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                break;
                            case CellType.Formula:
                                switch (item.CachedFormulaResultType)
                                {
                                    case CellType.Boolean:
                                        dr[item.ColumnIndex] = item.BooleanCellValue;
                                        break;
                                    case CellType.Error:
                                        dr[item.ColumnIndex] = ErrorEval.GetText(item.ErrorCellValue);
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(item))
                                        {
                                            dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = item.NumericCellValue;
                                        }
                                        break;
                                    case CellType.String:
                                        string str = item.StringCellValue;
                                        if (!string.IsNullOrEmpty(str))
                                        {
                                            dr[item.ColumnIndex] = str.ToString();
                                        }
                                        else
                                        {
                                            dr[item.ColumnIndex] = null;
                                        }
                                        break;
                                    case CellType.Unknown:
                                    case CellType.Blank:
                                    default:
                                        dr[item.ColumnIndex] = string.Empty;
                                        break;
                                }
                                break;
                            case CellType.Numeric:
                                if (DateUtil.IsCellDateFormatted(item))
                                {
                                    dr[item.ColumnIndex] = item.DateCellValue.ToString("yyyy-MM-dd hh:MM:ss");
                                }
                                else
                                {
                                    dr[item.ColumnIndex] = item.NumericCellValue;
                                }
                                break;
                            case CellType.String:
                                string strValue = item.StringCellValue;
                                if (string.IsNullOrEmpty(strValue))
                                {
                                    dr[item.ColumnIndex] = strValue.ToString();
                                }
                                else
                                {
                                    dr[item.ColumnIndex] = null;
                                }
                                break;
                            case CellType.Unknown:
                            case CellType.Blank:
                            default:
                                dr[item.ColumnIndex] = string.Empty;
                                break;
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
        }

        return dt;
    }

    public static bool CreateExcel(DataTable dt, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (dt != null)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "微软雅黑"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 20;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "楷体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 12;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "楷体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 12;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "楷体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 10;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "楷体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 10;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表格
            string sheetname = dt.TableName != null && dt.TableName != "" ? dt.TableName : "Sheet1";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 200);
            }
            //第一行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;
            //第二行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex + i));
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex++;

            #endregion

            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }

    public static bool CreateExcel_HZQD(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null && ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表头
            DataTable dt = ds.Tables[0];
            string sheetname = "核注清单";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 200);
            }
            //第一行
            row = sheet1.CreateRow(rIndex);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("核注清单#");//设置单元格内容
            rIndex++;
            //第二行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    case "起运运抵国":
                        colname = "起运/运抵国(地区)";
                        break;
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;
            //第三行 内容
            row = sheet1.CreateRow(rIndex);//创建行
            //填充数据
            IRow drow = sheet1.CreateRow(2);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell celll = drow.CreateCell((j), CellType.String);
                celll.SetCellValue(dt.Rows[0][j].ToString());
            }
            rIndex++;

            #endregion
            #region 表体
            dt = ds.Tables[1];
            //第四行 
            row = sheet1.CreateRow(rIndex);//创建行
            cell.CellStyle = style_body; //把样式赋给单元格
            cell = row.CreateCell(0);//第一格
            cell.SetCellValue("核注清单#表体");//设置单元格内容
            rIndex++;
            //第五行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    case "原产国":
                        colname = "原产国(地区)";
                        break;
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;

            //第六行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex + i));
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex++;
            //最后一行
            row = sheet1.CreateRow(rIndex + dt.Rows.Count - 1);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("end#");//设置单元格内容
            #endregion




            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }
    public static bool CreateExcel_HZQD_DF(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null && ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            ISheet sheet2 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表头
            DataTable dt = ds.Tables[0];
            string sheetname = "表头";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 200);
            }

            //第一行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;

                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;
            //第二行 内容
            row = sheet1.CreateRow(rIndex);//创建行
            //填充数据
            IRow drow = sheet1.CreateRow(1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell celll = drow.CreateCell((j), CellType.String);
                celll.SetCellValue(dt.Rows[0][j].ToString());
            }


            #endregion
            #region 表体
            rIndex = 0;
            string sheetname2 = "表体";
            sheet2 = hssfworkbook.CreateSheet(sheetname2);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet2.SetColumnWidth(i, 20 * 256 + 200);
            }


            dt = ds.Tables[1];
            //第1行 
            row = sheet2.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;

            //第2行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet2.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet2.CreateRow((rIndex + i));
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex++;

            #endregion




            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }

    public static bool CreateExcel_HFD(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null && ds.Tables.Count == 3 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表头
            DataTable dt = ds.Tables[0];
            string sheetname = "核放单";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 200);
            }
            //第一行
            row = sheet1.CreateRow(rIndex);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("核放单#");//设置单元格内容
            rIndex++;
            //第二行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;

                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;
            //第三行 内容
            row = sheet1.CreateRow(rIndex);//创建行
            //填充数据
            IRow drow = sheet1.CreateRow(2);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell celll = drow.CreateCell((j), CellType.String);
                celll.SetCellValue(dt.Rows[0][j].ToString());
            }
            rIndex++;

            #endregion

            row = sheet1.CreateRow(rIndex);//创建空行
            rIndex++;

            #region 表体
            dt = ds.Tables[1];
            //第四行 
            row = sheet1.CreateRow(rIndex);//创建行           
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("核放单#表体");//设置单元格内容
            rIndex++;
            //第五行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;

                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格
                }

            }
            rIndex++;


            //第六行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex));
                rIndex++;

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }


            #endregion

            row = sheet1.CreateRow(rIndex);//创建空行
            rIndex++;


            #region 关联单证表体
            dt = ds.Tables[2];
            //第八行 
            row = sheet1.CreateRow(rIndex);//创建行           
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("核放单#关联单证表体");//设置单元格内容
            rIndex++;
            //第九行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;

                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;

            //第十行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex));

                rIndex++;

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex++;
            //最后一行
            row = sheet1.CreateRow(rIndex - 1);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("end#");//设置单元格内容
            #endregion



            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }



    public static bool CreateExcel_CRKD(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null && ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表头
            DataTable dt = ds.Tables[0];
            string sheetname = "出入库单";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 400);
            }
            //第一行
            row = sheet1.CreateRow(rIndex);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("出入库单#");//设置单元格内容
            rIndex++;
            //第二行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;

                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格
                }

            }
            rIndex++;
            //第三行 内容
            row = sheet1.CreateRow(rIndex);//创建行
            //填充数据
            IRow drow = sheet1.CreateRow(2);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell celll = drow.CreateCell((j), CellType.String);
                celll.SetCellValue(dt.Rows[0][j].ToString());
            }
            rIndex++;

            #endregion

            row = sheet1.CreateRow(rIndex);//创建空行
            rIndex++;


            #region 表体
            dt = ds.Tables[1];
            //第四行 
            row = sheet1.CreateRow(rIndex);//创建行          
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("出入库单#表体");//设置单元格内容
            rIndex++;
            //第五行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    case "原产国地区":
                        colname = "原产国(地区)";
                        break;
                    case "最终目的国地区":
                        colname = "最终目的国（地区）";
                        break;
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                }

            }
            rIndex++;

            //第六行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex));
                rIndex++;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex++;
            //最后一行
            row = sheet1.CreateRow(rIndex - 1);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("end#");//设置单元格内容
            #endregion




            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }


    public static bool CreateExcel_YWSBB(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null && ds.Tables.Count == 3 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号
            #region 表头
            DataTable dt = ds.Tables[0];
            string sheetname = "业务申报表";
            sheet1 = hssfworkbook.CreateSheet(sheetname);

            for (int i = 0; i < dt.Columns.Count - 1; i++)
            {
                sheet1.SetColumnWidth(i, 20 * 256 + 200);
            }
            //第一行
            row = sheet1.CreateRow(rIndex);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("业务申报表#");//设置单元格内容
            rIndex++;
            //第二行 列名
            row = sheet1.CreateRow(rIndex);
            //写入列名
            int a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;
            //第三行 内容
            row = sheet1.CreateRow(rIndex);//创建行
            //填充数据
            IRow drow = sheet1.CreateRow(2);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell celll = drow.CreateCell((j), CellType.String);
                celll.SetCellValue(dt.Rows[0][j].ToString());
            }
            rIndex++;

            #endregion
            #region 表体
            dt = ds.Tables[1];
            //第四行 
            row = sheet1.CreateRow(rIndex);//创建行
            cell.CellStyle = style_body; //把样式赋给单元格
            cell = row.CreateCell(0);//第一格
            cell.SetCellValue("业务申报表#表体");//设置单元格内容
            rIndex++;
            //第五行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    case "原产国":
                        colname = "原产国(地区）";
                        break;
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;

            //第六行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex + i));
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex += dt.Rows.Count;
            //

            #endregion
            #region 单耗表
            dt = ds.Tables[2];
            //第七行 
            row = sheet1.CreateRow(rIndex);//创建行
            cell.CellStyle = style_body; //把样式赋给单元格
            cell = row.CreateCell(0);//第一格
            cell.SetCellValue("业务申报表#单耗表");//设置单元格内容
            rIndex++;
            //第八行
            row = sheet1.CreateRow(rIndex);
            //写入列名
            a = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                a++;
                //写入标题名称
                string colname = dt.Columns[i].ColumnName;
                switch (colname)
                {
                    default:
                        break;
                }
                if (colname != "" && colname != null)
                {
                    cell = row.CreateCell(a);//创建单元格
                    cell.SetCellValue(colname);  //在第一行，第一列添加一个值
                    cell.CellStyle = style_body; //把样式赋给单元格

                    //sheet1.AutoSizeColumn(i);  //会按照值的长短 自动调节列的大小 
                }

            }
            rIndex++;

            //第九行 内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row = sheet1.CreateRow(rIndex + i);//创建行
                //填充数据
                IRow drow1 = sheet1.CreateRow((rIndex + i));
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell celll = drow1.CreateCell((j), CellType.String);
                    celll.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            rIndex += dt.Rows.Count;
            //
            //最后一行
            row = sheet1.CreateRow(rIndex + dt.Rows.Count - 1);//创建行
            cell = row.CreateCell(0);//第一格
            cell.CellStyle = style_body; //把样式赋给单元格
            cell.SetCellValue("end#");//设置单元格内容
            #endregion




            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    }


    public static bool CreateExcel_KCCX(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号

            DataTable dt = ds.Tables[0];
            string sheetname = "库存查询";
            sheet1 = hssfworkbook.CreateSheet(sheetname);


            for (int i = 0; i < dt.Rows.Count+1; i++)
            {
                row = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    cell = row.CreateCell(j);

                    if (i == 0)
                    {
                        cell.SetCellValue(dt.Columns[j].ColumnName);
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i-1][j].ToString());
                    }                   
                }
            }
              
            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;
    
    }

    public static bool CreateExcel_HZQD_BD(DataSet ds, string filePath, string fileName)
    {
        int outint = 0;
        decimal outdec = 0M;
        if (ds != null)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();  //命名空间：using NPOI.HSSF.UserModel;
            ISheet sheet1 = null;
            IRow row = null;
            ICell cell = null;
            //sheet1 = hssfworkbook.CreateSheet("Sheet1");　　//命名空间：using NPOI.SS.UserModel;
            #region 创建样式
            ICellStyle style_biaoti = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font.FontName = "宋体"; //和excel里面的字体对应
            font.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font.IsItalic = false; //斜体
            font.FontHeightInPoints = 14;//字体大小
            font.Boldweight = short.MaxValue;//字体加粗
            style_biaoti.SetFont(font); //将字体样式赋给样式对象
            style_biaoti.Alignment = HorizontalAlignment.Center;
            //style_biaoti.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
            //style_biaoti.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
            style_biaoti.WrapText = true;

            ICellStyle style_body = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body.FontName = "宋体"; //和excel里面的字体对应

            font_body.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body.IsItalic = false; //斜体
            font_body.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body.SetFont(font_body); //将字体样式赋给样式对象
            style_body.Alignment = HorizontalAlignment.Center;
            style_body.VerticalAlignment = VerticalAlignment.Center;
            style_body.WrapText = true;
            style_body.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;


            ICellStyle style_body_red = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_body_red = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_body_red.FontName = "宋体"; //和excel里面的字体对应
            font_body_red.Color = new HSSFColor.Red().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_body_red.IsItalic = false; //斜体
            font_body_red.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_body_red.SetFont(font_body_red); //将字体样式赋给样式对象
            style_body_red.Alignment = HorizontalAlignment.Center;
            style_body_red.VerticalAlignment = VerticalAlignment.Center;
            style_body_red.WrapText = true;
            style_body_red.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style_body_red.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            ICellStyle style_head = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_head = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_head.FontName = "宋体"; //和excel里面的字体对应
            font_head.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_head.IsItalic = false; //斜体
            font_head.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_head.SetFont(font_head); //将字体样式赋给样式对象
            style_head.Alignment = HorizontalAlignment.Left;
            style_head.WrapText = true;



            ICellStyle style_smtit = hssfworkbook.CreateCellStyle();//创建样式对象
            style_smtit.SetFont(font_body); //将字体样式赋给样式对象
            style_smtit.Alignment = HorizontalAlignment.Left;
            style_smtit.WrapText = true;

            ICellStyle style_feet = hssfworkbook.CreateCellStyle();//创建样式对象
            IFont font_feet = hssfworkbook.CreateFont(); //创建一个字体样式对象
            font_feet.FontName = "宋体"; //和excel里面的字体对应
            font_feet.Color = new HSSFColor.Black().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            font_feet.IsItalic = false; //斜体
            font_feet.FontHeightInPoints = 14;//字体大小
            //font_body.Boldweight = short.MaxValue;//字体加粗
            style_feet.SetFont(font_feet); //将字体样式赋给样式对象
            style_feet.WrapText = true;
            #endregion

            int rIndex = 0; //行号

            DataTable dt = ds.Tables[0];
            string sheetname = "核注清单表体";
            sheet1 = hssfworkbook.CreateSheet(sheetname);


            for (int i = 0; i < dt.Rows.Count + 1; i++)
            {
                row = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    cell = row.CreateCell(j);

                    if (i == 0)
                    {
                        cell.SetCellValue(dt.Columns[j].ColumnName);
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i - 1][j].ToString());
                    }
                }
            }

            if (!Directory.Exists(filePath)) //如果文件夹不存在则创建
            {
                Directory.CreateDirectory(filePath);
            }
            using (FileStream file = new FileStream(filePath + fileName, FileMode.Create))
            {
                hssfworkbook.Write(file);  //创建test.xls文件。
                file.Close();
                return true;
            }
        }
        return false;

    }



}