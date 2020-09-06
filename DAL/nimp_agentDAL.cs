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
    public class nimp_agentDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from nimp_agent");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)            
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public DataTable GetTableSql(string sql)
        {
            return DbHelperSQL.GetTableSql(sql);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(nimp_agent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nimp_agent(");
            strSql.Append("AgentName,agentAllName,contactPep,contactTel,ypep,ytel,cpep,ctel,address,tel,userName,pwd,fax,bank,bank_ads,bank_tel,bank_account,invoiceNo,in_pep,in_date");
            strSql.Append(")values(");
            strSql.Append("@AgentName,@agentAllName,@contactPep,@contactTel,@ypep,@ytel,@cpep,@ctel,@address,@tel,@userName,@pwd,@fax,@bank,@bank_ads,@bank_tel,@bank_account,@invoiceNo,@in_pep,@in_date");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters ={
                new SqlParameter("@AgentName",SqlDbType.NVarChar,20),
                new SqlParameter("@agentAllName",SqlDbType.NVarChar,50),
                new SqlParameter("@contactPep",SqlDbType.NVarChar,10),
                new SqlParameter("@contactTel",SqlDbType.NVarChar,100),
                new SqlParameter("@ypep",SqlDbType.NVarChar,50),
                new SqlParameter("@ytel",SqlDbType.NVarChar,100),
                new SqlParameter("@cpep",SqlDbType.NVarChar,50),
                new SqlParameter("@ctel",SqlDbType.NVarChar,100),
                new SqlParameter("@address",SqlDbType.NVarChar,100),
                new SqlParameter("@tel",SqlDbType.NVarChar,100),
                new SqlParameter("@userName",SqlDbType.NVarChar,50),
                new SqlParameter("@pwd",SqlDbType.NVarChar,20),
                new SqlParameter("@fax",SqlDbType.NVarChar,100) ,
                new SqlParameter("@bank",SqlDbType.NVarChar,20),
                new SqlParameter("@bank_ads",SqlDbType.NVarChar,200),
                new SqlParameter("@bank_tel",SqlDbType.NVarChar,40),
                new SqlParameter("@bank_account",SqlDbType.NVarChar,40),
                new SqlParameter("@invoiceNo",SqlDbType.NVarChar,50),
                new SqlParameter("in_pep",SqlDbType.NVarChar,10),
                new SqlParameter("@in_date",SqlDbType.DateTime),

            };

            parameters[0].Value = model.AgentName;
            parameters[1].Value = model.agentAllName;
            parameters[2].Value = model.contactPep;
            parameters[3].Value = model.contactTel;
            parameters[4].Value = model.ypep;
            parameters[5].Value = model.ytel;
            parameters[6].Value = model.cpep;
            parameters[7].Value = model.ctel;
            parameters[8].Value = model.address;
            parameters[9].Value = model.tel;
            parameters[10].Value = model.userName;
            parameters[11].Value = model.pwd;
            parameters[12].Value = model.fax;
            parameters[13].Value = model.bank;
            parameters[14].Value = model.bank_ads;
            parameters[15].Value = model.bank_tel;
            parameters[16].Value = model.bank_account;
            parameters[17].Value = model.invoiceNo;
            parameters[18].Value = PubConstant.YongHu;
            parameters[19].Value = DateTime.Now;

            //DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

        public DataTable RunSql(string sql)
        {
            return DbHelperSQL.Query(sql).Tables[0];
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(nimp_agent model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update nimp_agent set ");
            strSql.Append("AgentName=@AgentName,");
            strSql.Append("agentAllName=@agentAllName,");
            strSql.Append("contactPep=@contactPep,");
            strSql.Append("contactTel=@contactTel,");
            strSql.Append("ypep=@ypep,");
            strSql.Append("ytel=@ytel,");
            strSql.Append("cpep=@cpep,");
            strSql.Append("ctel=@ctel,");
            strSql.Append("tel=@tel,");
            strSql.Append("userName=@userName,");
            strSql.Append("pwd=@pwd,");
            strSql.Append("fax=@fax,");
            strSql.Append("bank=@bank,");
            strSql.Append("bank_ads=@bank_ads,");
            strSql.Append("bank_tel=@bank_tel,");
            strSql.Append("bank_account=@bank_account,");
            strSql.Append("invoiceNo=@invoiceNo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters ={
                new SqlParameter("@AgentName",SqlDbType.NVarChar,20),
                new SqlParameter("@agentAllName",SqlDbType.NVarChar,50),
                new SqlParameter("@contactPep",SqlDbType.NVarChar,10),
                new SqlParameter("@contactTel",SqlDbType.NVarChar,100),
                new SqlParameter("@ypep",SqlDbType.NVarChar,50),
                new SqlParameter("@ytel",SqlDbType.NVarChar,100),
                new SqlParameter("@cpep",SqlDbType.NVarChar,50),
                new SqlParameter("@ctel",SqlDbType.NVarChar,100),
                new SqlParameter("@address",SqlDbType.NVarChar,100),
                new SqlParameter("@tel",SqlDbType.NVarChar,100),
                new SqlParameter("@userName",SqlDbType.NVarChar,50),
                new SqlParameter("@pwd",SqlDbType.NVarChar,20),
                new SqlParameter("@fax",SqlDbType.NVarChar,100) ,
                new SqlParameter("@bank",SqlDbType.NVarChar,20),
                new SqlParameter("@bank_ads",SqlDbType.NVarChar,200),
                new SqlParameter("@bank_tel",SqlDbType.NVarChar,40),
                new SqlParameter("@bank_account",SqlDbType.NVarChar,40),
                new SqlParameter("@invoiceNo",SqlDbType.NVarChar,50),
                //new SqlParameter("in_pep",SqlDbType.NVarChar,10),
                //new SqlParameter("@in_date",SqlDbType.DateTime),
                new SqlParameter("@id", SqlDbType.Int,4) ,

            };

            parameters[0].Value = model.AgentName;
            parameters[1].Value = model.agentAllName;
            parameters[2].Value = model.contactPep;
            parameters[3].Value = model.contactTel;
            parameters[4].Value = model.ypep;
            parameters[5].Value = model.ytel;
            parameters[6].Value = model.cpep;
            parameters[7].Value = model.ctel;
            parameters[8].Value = model.address;
            parameters[9].Value = model.tel;
            parameters[10].Value = model.userName;
            parameters[11].Value = model.pwd;
            parameters[12].Value = model.fax;
            parameters[13].Value = model.bank;
            parameters[14].Value = model.bank_ads;
            parameters[15].Value = model.bank_tel;
            parameters[16].Value = model.bank_account;
            parameters[17].Value = model.invoiceNo;
            //parameters[18].Value = PubConstant.YongHu;
            //parameters[19].Value = DateTime.Now;
            parameters[18].Value = model.id;
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
		/// 获取下一个商品的id
		/// </summary>
		/// <returns></returns>
        public int NextId()
        {
            string sql = "select ISNULL(MAX(id), 0) + 1 as id from nimp_agent";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int ExecuteSql(string strSql)
        {

            int rows = DbHelperSQL.ExecuteSql(strSql);
            return rows;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from nimp_agent ");
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

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from nimp_agent ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public nimp_agent GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,AgentName,agentAllName,contactPep,contactTel,ypep,ytel,cpep,ctel,address,tel,userName,pwd,fax,bank,bank_ads,bank_tel,bank_account,invoiceNo,in_pep,in_date");
            strSql.Append("from nimp_agent ");
            strSql.Append("where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            nimp_agent model = new nimp_agent();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.AgentName = ds.Tables[0].Rows[0]["AgentName"].ToString();
                model.agentAllName = ds.Tables[0].Rows[0]["agentAllName"].ToString();
                model.contactPep = ds.Tables[0].Rows[0]["contactPep"].ToString();
                model.contactTel = ds.Tables[0].Rows[0]["contactTel"].ToString();
                model.ypep = ds.Tables[0].Rows[0]["ypep"].ToString();
                model.ytel = ds.Tables[0].Rows[0]["ytel"].ToString();
                model.cpep = ds.Tables[0].Rows[0]["cpep"].ToString();
                model.ctel = ds.Tables[0].Rows[0]["ctel"].ToString();
                model.address = ds.Tables[0].Rows[0]["address"].ToString();
                model.tel = ds.Tables[0].Rows[0]["tel"].ToString();
                model.userName = ds.Tables[0].Rows[0]["userName"].ToString();
                model.pwd = ds.Tables[0].Rows[0]["pwd"].ToString();
                model.fax = ds.Tables[0].Rows[0]["fax"].ToString();
                model.bank = ds.Tables[0].Rows[0]["bank"].ToString();
                model.bank_ads = ds.Tables[0].Rows[0]["bank_ads"].ToString();
                model.bank_tel = ds.Tables[0].Rows[0]["bank_tel"].ToString();
                model.bank_account = ds.Tables[0].Rows[0]["bank_account"].ToString();
                model.invoiceNo = ds.Tables[0].Rows[0]["invoiceNo"].ToString();
                model.in_pep = ds.Tables[0].Rows[0]["in_pep"].ToString();
                if (ds.Tables[0].Rows[0]["in_date"].ToString() != "")
                {
                    model.in_date = DateTime.Parse(ds.Tables[0].Rows[0]["in_date"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
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
            strSql.Append(")AS Row, T.*  from nimp_agent T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        // <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM nimp_agent ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM nimp_agent ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
