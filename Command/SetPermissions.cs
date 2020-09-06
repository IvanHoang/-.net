using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Command;
using System.Data;
using System.Data.SqlClient;


namespace Command
{
    public class SetPermissions
    {
        

        //public static string Check(string EmpName, string Action)
        //{

        //    DbHelperSQLP db = new DbHelperSQLP();
        //    string SQL = string.Format("SELECT * FROM [jin2_test].[dbo].[PowerEmp] WHERE [EmpName]='{0}' AND [ActionID]=(SELECT id FROM [jin2_param].[dbo].[PowerAction] WHERE [Action]='{1}')", EmpName, Action);
        //    DataTable dt = db.Query(SQL).Tables[0];
        //    if (dt.Rows.Count > 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return "style=\"display:none\"";
        //    }

        //}


        public static string ReturnPower(string action,string Xitong, string Modular, string PageName, string Remark)
        {
            string str = "";
            if (PubConstant.YongHu != "管理员")
            {
                str = GetPower(action, Xitong, Modular, PageName, Remark) ? "" : "style=\"display:none\"";
            }

            return str;
        }


        public static bool GetPower(string action, string Xitong, string Modular, string PageName, string Remark)
        {
            bool flag = false;
            if (PubConstant.YongHu != "管理员")
            {
                flag = GetPower1(action, Xitong, Modular, PageName, Remark);
            }
            else
            {
                flag = true;
            }
            return flag;
        }


        public static bool GetPower1(string action, string Xitong, string Modular, string PageName, string Remark)
        {
            DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
            string sql = "select isnull(pEmp.id,0) from [jin2_param].[dbo].[PowerAction] full join (select * from [PowerEmp] where [PowerEmp].EmpName='" + PubConstant.YongHu + "')as pEmp on pEmp.ActionID=PowerAction.id where PowerAction.Action='" + action + "'";

            DataSet ds = DbHelperSQL.Query(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                    return true;
                else
                    return false;
            }
            else
            {

                Add(action, Xitong, Modular, PageName, Remark);
                return false;
            }
        }


        public static int Add(string action, string Xitong, string Modular, string PageName, string Remark)
        {
            DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [jin2_param].[dbo].[PowerAction](");
            strSql.Append("Xitong,Modular,PageName,Name,Remark,Action,InDate)");
            strSql.Append(" values (");
            strSql.Append("@Xitong,@Modular,@PageName,@Name,@Remark,@Action,getdate())");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Xitong", SqlDbType.NVarChar,50),
					new SqlParameter("@Modular", SqlDbType.NVarChar,50),
					new SqlParameter("@PageName", SqlDbType.NVarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,100),
					new SqlParameter("@Action", SqlDbType.NVarChar,100)};
            parameters[0].Value = Xitong;
            parameters[1].Value = Modular;
            parameters[2].Value = PageName;
            parameters[3].Value = Remark;
            parameters[4].Value = null;
            parameters[5].Value = action;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }



















    }
}
