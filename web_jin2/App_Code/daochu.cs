using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// daochu 的摘要说明
/// </summary>
public class daochu
{
	public daochu()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}


    public static void DataTableToExcel(DataTable dt, string connString)
    {
        //先算出欄位及列數
        int rows = dt.Rows.Count;
        int cols = dt.Columns.Count;
        //int num = 0;
        //用來建立命令 
        StringBuilder sb = new StringBuilder();

        sb.Append("CREATE TABLE ");
        sb.Append(dt.TableName + " ( ");
        //用來做開TABLE的欄名資訊
        for (int i = 0; i < cols; i++)
        {
            if (i < cols - 1)
            {
                switch (dt.Columns[i].ColumnName)
                {
                    case "过磅重量":
                        sb.Append(string.Format("{0} float,", dt.Columns[i].ColumnName));
                        break;
                    case "标重重量":
                        sb.Append(string.Format("{0} float,", dt.Columns[i].ColumnName));
                        break;
                    case "误差重量":
                        sb.Append(string.Format("{0} float,", dt.Columns[i].ColumnName));
                        break;
                    case "入库重量":
                        sb.Append(string.Format("{0} float,", dt.Columns[i].ColumnName));
                        break;
                    case "调整入库重量":
                        sb.Append(string.Format("{0} float,", dt.Columns[i].ColumnName));
                        break;
                    default:
                        sb.Append(string.Format("{0} varchar,", dt.Columns[i].ColumnName));
                        break;
                }
                
            }
            else
            {
                sb.Append(string.Format("{0} varchar)", dt.Columns[i].ColumnName));
            }
        }
        //把要開啟的臨時Excel建立起來
        using (OleDbConnection objConn = new OleDbConnection(connString))
        {
            OleDbCommand objCmd = new OleDbCommand();
            objCmd.Connection = objConn;

            objCmd.CommandText = sb.ToString();


            objConn.Open();
            //先執行CreateTable的任務
            objCmd.ExecuteNonQuery();


            //開始處理資料內容的新增
            #region 開始處理資料內容的新增
            //把之前 CreateTable 清空
            sb.Remove(0, sb.Length);
            sb.Append("INSERT INTO ");
            sb.Append(dt.TableName + " ( ");
            //這邊開始組該Excel欄位順序
            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1)
                    sb.Append("[" + dt.Columns[i].ColumnName + "]" + ",");
                else
                    sb.Append(dt.Columns[i].ColumnName + ") values (");
            }
            //這邊組 DataTable裡面的值要給到Excel欄位的
            for (int i = 0; i < cols; i++)
            {
                if (i < cols - 1)
                    sb.Append("[" + "@" + dt.Columns[i].ColumnName + "]" + ",");
                else
                    sb.Append("[" + "@" + dt.Columns[i].ColumnName + "]" + ")");
            }
            #endregion


            //建立插入動作的Command
            objCmd.CommandText = sb.ToString();
            OleDbParameterCollection param = objCmd.Parameters;

            for (int i = 0; i < cols; i++)
            {
                param.Add(new OleDbParameter("@" + dt.Columns[i].ColumnName, OleDbType.VarChar));
            }

            //使用參數化的方式來給予值
            foreach (DataRow row in dt.Rows)
            {
                bool nullflag = true;
                for (int i = 0; i < param.Count; i++)
                {
                    param[i].Value = row[i];
                    if (param[i].Value != null || !nullflag)
                    {
                        nullflag = false;
                    }
                }
                if (nullflag) continue;
                //執行這一筆的給值
                objCmd.ExecuteNonQuery();
            }


        }//end using
    }
}