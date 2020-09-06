using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Command;

namespace DAL
{
    public class hr_empinfoDAL
    {

        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public hr_empinfoDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetModelTable(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  id, Name, USname, password, sex, tel, email,case when in_service=0 then '已离职' else '正常' end as in_service, Address, inpep, indate from hr_empinfo ");
            strSql.Append(" where in_service = 1 and  id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        public DataTable Get_list(string userName, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  id, name, USname FROM hr_empinfo where in_service = 1 and USname = '" + userName + "' and password = '" + pwd + "'");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public DataTable Get_list_OMS(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT   name, USname FROM hr_empinfo where in_service = 1 and USname = '" + userName + "'");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM hr_empinfo where in_service=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append("  " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.id, T.Name, T.USname, T.password, T.sex, T.tel, T.email,case when T.in_service=0 then '已离职' else '正常' end as in_service, T.Address, T.inpep, T.indate  from hr_empinfo T WHERE in_service=1  ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append("  " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(hr_empinfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into hr_empinfo(");
            strSql.Append("name,USname,password,sex,tel,email,in_service,Address,inpep,indate)");
            strSql.Append(" values (");
            strSql.Append("@name,@USname,@password,@sex,@tel,@email,@in_service,@Address,@inpep,@indate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@USname", SqlDbType.NVarChar,50),
                    new SqlParameter("@password", SqlDbType.NVarChar,50),
                    new SqlParameter("@sex", SqlDbType.NVarChar,50),
                    new SqlParameter("@tel", SqlDbType.NVarChar,50),
                    new SqlParameter("@email", SqlDbType.NVarChar,50),
                    new SqlParameter("@in_service", SqlDbType.Bit,1),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@inpep", SqlDbType.NVarChar,50),
                    new SqlParameter("@indate", SqlDbType.DateTime)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.USname;
            parameters[2].Value = model.password;
            parameters[3].Value = model.sex;
            parameters[4].Value = model.tel;
            parameters[5].Value = model.email;
            parameters[6].Value = model.in_service;
            parameters[7].Value = model.Address;
            parameters[8].Value = PubConstant.YongHu;
            parameters[9].Value = DateTime.Now;

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(hr_empinfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hr_empinfo set ");
            strSql.Append("name=@name,");
            strSql.Append("USname=@USname,");
            strSql.Append("password=@password,");
            strSql.Append("sex=@sex,");
            strSql.Append("tel=@tel,");
            strSql.Append("email=@email,");
            strSql.Append("Address=@Address");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@USname", SqlDbType.NVarChar,50),
                    new SqlParameter("@password", SqlDbType.NVarChar,50),
                    new SqlParameter("@sex", SqlDbType.NVarChar,50),
                    new SqlParameter("@tel", SqlDbType.NVarChar,50),
                    new SqlParameter("@email", SqlDbType.NVarChar,50),
                    new SqlParameter("@Address", SqlDbType.NVarChar,200),
                    new SqlParameter("@inpep", SqlDbType.NVarChar,50),
                    new SqlParameter("@indate", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.USname;
            parameters[2].Value = model.password;
            parameters[3].Value = model.sex;
            parameters[4].Value = model.tel;
            parameters[5].Value = model.email;
            parameters[6].Value = model.Address;
            parameters[7].Value = PubConstant.YongHu;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool changePWD(string user, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hr_empinfo set ");
            strSql.Append("password=@pwd ");
            strSql.Append(" where USname=@user and in_service=1 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@user", SqlDbType.NVarChar,50),
                    new SqlParameter("@pwd", SqlDbType.NVarChar,50)
                 };
            parameters[0].Value = user;
            parameters[1].Value = pwd;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 离职
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update hr_empinfo set in_service=0 ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hr_empinfoModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,USname,password,sex,tel,email,inDep,post,in_service,Job_no,Address,inpep,indate from hr_empinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            hr_empinfoModel model = new hr_empinfoModel();
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
        public hr_empinfoModel GetModelNew(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,USname,password,sex,tel,email,inDep,inpep,indate from hr_empinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            hr_empinfoModel model = new hr_empinfoModel();
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
        public hr_empinfoModel GetbumenModel(string inDep)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,name,USname,password,sex,tel,email,inDep,post,in_service,Job_no,Address,inpep,indate from hr_empinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = inDep;

            hr_empinfoModel model = new hr_empinfoModel();
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
        public hr_empinfoModel DataRowToModel(DataRow row)
        {
            hr_empinfoModel model = new hr_empinfoModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["USname"] != null)
                {
                    model.USname = row["USname"].ToString();
                }
                if (row["password"] != null)
                {
                    model.password = row["password"].ToString();
                }
                if (row["sex"] != null)
                {
                    model.sex = row["sex"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
               
                //if (row["in_service"] != null && row["in_service"].ToString() != "")
                //{
                //    if ((row["in_service"].ToString() == "1") || (row["in_service"].ToString().ToLower() == "true"))
                //    {
                //        model.in_service = true;
                //    }
                //    else
                //    {
                //        model.in_service = false;
                //    }
                //}
               
                //if (row["Address"] != null)
                //{
                //    model.Address = row["Address"].ToString();
                //}
                if (row["inpep"] != null)
                {
                    model.inpep = row["inpep"].ToString();
                }
                if (row["indate"] != null && row["indate"].ToString() != "")
                {
                    model.indate = DateTime.Parse(row["indate"].ToString());
                }
            }
            return model;
        }
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from hr_empinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool upin(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update [hr_empinfo] set ");
            strSql.Append(" in_service=@in_service ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@in_service", SqlDbType.Bit,1),
                    new SqlParameter("@id", SqlDbType.Int,4),
            };
            parameters[0].Value = false;
            parameters[1].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否存在该用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public int Exists(string UserName, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hr_empinfo ");
            strSql.Append(" where in_service=1 and USname='" + UserName + "' ");
            if (pwd != "" && pwd != null)
            {
                strSql.Append(" and password='" + pwd + "'");
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
     /// <summary>
     /// 
     /// </summary>
     /// <param name="UserName"></param>
     /// <param name="Name"></param>
     /// <param name="id"></param>
     /// <returns></returns>
        public bool ExistsAddUpdate(string UserName, string Name,string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from hr_empinfo");
            strSql.Append(" where in_service=1 and (USname=@UserName or Name=@Name) and id<>@id");
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.NVarChar),
                    	new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@id", SqlDbType.Int)
			};
            parameters[0].Value = UserName;
            parameters[1].Value = Name;
            parameters[2].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        public int baocun(string UserName, string pwd, string newPwd1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  hr_empinfo set password =@newPwd1 ");
            strSql.Append(" where USname=@UserName and password =@Pwd ");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,20),
                    new SqlParameter("@newPwd1", SqlDbType.NVarChar,20),
                    new SqlParameter("@Pwd", SqlDbType.NVarChar,20)
            };
            parameters[0].Value = UserName;
            parameters[1].Value = newPwd1;
            parameters[2].Value = pwd;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return rows;
        }
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
