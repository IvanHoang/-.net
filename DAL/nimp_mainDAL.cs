using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
using Command;
using System.Diagnostics;

namespace DAL
{
    public class nimp_mainDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from nimp_main");
            strSql.Append(" where ");
            strSql.Append(" id=@id  ");
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
        public int Add(nimp_main model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into nimp_main(");
            strSql.Append("jobnumber,agentName,bondInvtNo,entryNo,transport,BL_No,customNO,planDate,inpep,indate,inDep,status,inoutdate,c_jobumber,invoiceNo,transportNo,orderno,remark");
            strSql.Append(") values (");
            strSql.Append("@jobnumber,@agentName,@bondInvtNo,@entryNo,@transport,@BL_No,@customNO,@planDate,@inpep,@indate,@inDep,@status,@inoutdate,@c_jobumber,@invoiceNo,@transportNo,@orderno,@remark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@jobnumber", SqlDbType.NVarChar,20) ,
                        new SqlParameter("@agentName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@bondInvtNo", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@entryNo", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@transport",SqlDbType.NVarChar,50),
                        new SqlParameter("@BL_No",SqlDbType.NVarChar,50),
                        new SqlParameter("@customNO",SqlDbType.NVarChar,50),
                        new SqlParameter("@planDate",SqlDbType.DateTime),
                        new SqlParameter("@inpep",SqlDbType.NVarChar,10),
                        new SqlParameter("@indate",SqlDbType.DateTime),
                        new SqlParameter("@inDep",SqlDbType.NVarChar,10),
                        new SqlParameter("@status",SqlDbType.Int,4),
                        new SqlParameter("@inoutdate",SqlDbType.DateTime),
                        new SqlParameter("@c_jobumber",SqlDbType.NVarChar,50),
                        new SqlParameter("@invoiceNo",SqlDbType.NVarChar,50),
                        new SqlParameter("@transportNo",SqlDbType.NVarChar,50),
                        new SqlParameter("@orderno",SqlDbType.NVarChar,50),
                        new SqlParameter("@remark",SqlDbType.NVarChar,200)

            };

            parameters[0].Value = model.jobnumber;
            parameters[1].Value = model.agentName;
            parameters[2].Value = model.bondInvtNo;
            parameters[3].Value = model.entryNo;
            parameters[4].Value = model.transport;
            parameters[5].Value = model.BL_No;
            parameters[6].Value = model.customNO;
            parameters[7].Value = model.planDate;
            parameters[8].Value = PubConstant.YongHu;
            parameters[9].Value = DateTime.Now;
            parameters[10].Value = PubConstant.BuMen;
            parameters[11].Value = model.status;
            parameters[12].Value = model.inoutdate;
            parameters[13].Value = model.c_jobumber;
            parameters[14].Value = model.invoiceNo;
            parameters[15].Value = model.transportNo;
            parameters[16].Value = model.orderno;
            parameters[17].Value = model.remark;

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
            strSql.Append(")AS Row, T.*  from nimp_main T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }




        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *,D.id AS detail_id FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from nimp_main T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT  LEFT join infoDetail D on TT.jobnumber=D.jobnumber");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            

            Debug.WriteLine(strSql);

            return DbHelperSQL.Query(strSql.ToString());
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(nimp_main model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update nimp_main set ");
            strSql.Append(" jobnumber = @jobnumber ,  ");
            strSql.Append(" agentName = @agentName ,  ");
            strSql.Append(" bondInvtNo = @bondInvtNo ,  ");
            strSql.Append(" entryNo = @entryNo , ");
            strSql.Append(" transport = @transport  ,");
            strSql.Append(" BL_No = @BL_No , ");
            strSql.Append(" customNO = @customNO , ");
            strSql.Append(" planDate = @planDate , ");
            strSql.Append(" inpep = @inpep , ");
            strSql.Append(" indate = @indate , ");
            strSql.Append(" inDep = @inDep,  ");
            strSql.Append(" status = @status  ,");
            strSql.Append(" inoutdate = @inoutdate,  ");
            strSql.Append(" c_jobumber = @c_jobumber,  ");
            strSql.Append(" invoiceNo = @invoiceNo,  ");
            strSql.Append(" transportNo = @transportNo , ");
            strSql.Append(" orderno = @orderno , ");
            strSql.Append(" remark = @remark  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {

                        new SqlParameter("@jobnumber", SqlDbType.NVarChar) ,
                        new SqlParameter("@agentName", SqlDbType.NVarChar) ,
                        new SqlParameter("@bondInvtNo", SqlDbType.NVarChar) ,
                        new SqlParameter("@entryNo", SqlDbType.NVarChar) ,
                        new SqlParameter("@transport",SqlDbType.NVarChar),
                        new SqlParameter("@BL_No",SqlDbType.NVarChar),
                        new SqlParameter("@customNO",SqlDbType.NVarChar),
                        new SqlParameter("@planDate",SqlDbType.DateTime),
                        new SqlParameter("@inpep",SqlDbType.NVarChar),
                        new SqlParameter("@indate",SqlDbType.DateTime),
                        new SqlParameter("@inDep",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.Int),
                        new SqlParameter("@inoutdate",SqlDbType.DateTime),
                        new SqlParameter("@c_jobumber",SqlDbType.NVarChar),
                        new SqlParameter("@invoiceNo",SqlDbType.NVarChar),
                        new SqlParameter("@transportNo",SqlDbType.NVarChar),
                        new SqlParameter("@orderno",SqlDbType.NVarChar),
                        new SqlParameter("@remark",SqlDbType.NVarChar),
                         new SqlParameter("@id", SqlDbType.Int)

            };

            parameters[0].Value = model.jobnumber;
            parameters[1].Value = model.agentName;
            parameters[2].Value = model.bondInvtNo;
            parameters[3].Value = model.entryNo;
            parameters[4].Value = model.transport;
            parameters[5].Value = model.BL_No;
            parameters[6].Value = model.customNO;
            parameters[7].Value = model.planDate;
            parameters[8].Value = model.inpep;
            parameters[9].Value = model.indate;
            parameters[10].Value = model.inDep;
            parameters[11].Value = model.status;
            parameters[12].Value = model.inoutdate;
            parameters[13].Value = model.c_jobumber;
            parameters[14].Value = model.invoiceNo;
            parameters[15].Value = model.transportNo;
            parameters[16].Value = model.orderno;
            parameters[17].Value = model.remark;
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
            string sql = "select ISNULL(MAX(id), 0) + 1 as id from nimp_main";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from nimp_main ");
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
            strSql.Append("delete from nimp_main ");
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
        public nimp_main GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, jobnumber, agentName, bondInvtNo, transport,entryNo,BL_No,customNO,inpep, inDep,status,c_jobumber,invoiceNo,transportNo,orderno,remark, planDate,indate,inoutdate");
            strSql.Append("  from nimp_main ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            nimp_main model = new nimp_main();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.jobnumber = ds.Tables[0].Rows[0]["jobnumber"].ToString();
                model.agentName = ds.Tables[0].Rows[0]["agentName"].ToString();
                model.bondInvtNo = ds.Tables[0].Rows[0]["bondInvtNo"].ToString();
                model.transport = ds.Tables[0].Rows[0]["transport"].ToString();
                model.entryNo = ds.Tables[0].Rows[0]["entryNo"].ToString();
                model.BL_No = ds.Tables[0].Rows[0]["BL_No"].ToString();
                model.customNO = ds.Tables[0].Rows[0]["customNO"].ToString();
                model.inpep = ds.Tables[0].Rows[0]["inpep"].ToString();
                model.inDep = ds.Tables[0].Rows[0]["inDep"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                {
                    model.status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString());
                }
                model.c_jobumber = ds.Tables[0].Rows[0]["c_jobumber"].ToString();
                model.invoiceNo = ds.Tables[0].Rows[0]["invoiceNo"].ToString();
                model.transportNo = ds.Tables[0].Rows[0]["transportNo"].ToString();
                model.orderno = ds.Tables[0].Rows[0]["orderno"].ToString();
                model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                if (ds.Tables[0].Rows[0]["planDate"].ToString() != "")
                {
                    model.planDate = DateTime.Parse(ds.Tables[0].Rows[0]["planDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["indate"].ToString() != "")
                {
                    model.indate = DateTime.Parse(ds.Tables[0].Rows[0]["indate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["inoutdate"].ToString() != "")
                {
                    model.inoutdate = DateTime.Parse(ds.Tables[0].Rows[0]["inoutdate"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM nimp_main as T ");
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
            strSql.Append(" FROM nimp_main ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

    }
}
