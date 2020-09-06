using Command;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 数据访问类:CompanyList
    /// </summary>
    public partial class CompanyListParamDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP("param");
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string sqlName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from CompanyList");
            strSql.Append(" where sqlName=@sqlName");
            SqlParameter[] parameters = {
					new SqlParameter("@sqlName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = sqlName;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public string GetappendTypecd()
        {
            string sql = "SELECT top 1 [appendTypecd] FROM BwsBsc";
            DbHelperSQLP DbHelperSQL2 = new DbHelperSQLP();
            return DbHelperSQL2.GetSingle(sql).ToString();
           
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompanyListParamModel GetModel(string sqlName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id, sqlName, qz, companyName, companyAllName from CompanyList ");
            strSql.Append(" where sqlName=@sqlName");
            SqlParameter[] parameters = {
					new SqlParameter("@sqlName", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = sqlName;

            CompanyListParamModel model = new CompanyListParamModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompanyListParamModel DataRowToModel(DataRow row)
        {
            CompanyListParamModel model = new CompanyListParamModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["sqlName"] != null)
                {
                    model.sqlName = row["sqlName"].ToString();
                }
                if (row["qz"] != null)
                {
                    model.qz = row["qz"].ToString();
                }
                if (row["companyName"] != null)
                {
                    model.companyName = row["companyName"].ToString();
                }
                if (row["companyAllName"] != null)
                {
                    model.companyAllName = row["companyAllName"].ToString();
                }
               
                
            }
            return model;
        }

      

        public DataSet Getmodel(string sqlName)
        {
            string sql = string.Format("SELECT top 1 * FROM CompanyList WHERE sqlName='{0}'",sqlName);
            return DbHelperSQL.Query(sql);
        
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
