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
    public class p_parameterDetailDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public bool Exists(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from p_parameterDetail");
            strSql.Append(" where ");
            //strSql.Append(" TypeName = @TypeName and  ");
            strSql.Append(" TypeID = @TypeID and  ");
            strSql.Append(" Details = @Details and  ");
            strSql.Append(" DetailsCode = @DetailsCode and  ");
            strSql.Append(" otherCode = @otherCode and  ");
            strSql.Append(" Remark = @Remark and  ");
            strSql.Append(" inPep = @inPep and  ");
            strSql.Append(" inDate = @inDate  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(p_parameterDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into p_parameterDetail(");
            strSql.Append("TypeID,Remark,inPep,inDate,Details");
            strSql.Append(") values (");
            strSql.Append("@TypeID,@Remark,@inPep,@inDate,@Details");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@TypeID", SqlDbType.Int) ,
                        //new SqlParameter("@Details", SqlDbType.NVarChar,50) ,
                        //new SqlParameter("@DetailsCode", SqlDbType.NVarChar,50) ,
                        //new SqlParameter("@otherCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@inPep", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@inDate", SqlDbType.DateTime) ,
                        new SqlParameter("@Details",SqlDbType.NVarChar,50)
                        //new SqlParameter("@id", SqlDbType.Int)

            };

            //parameters[0].Value = model.TypeName;
            parameters[0].Value = model.TypeID;
            //parameters[1].Value = model.Details;
            //parameters[2].Value = model.DetailsCode;
            //parameters[3].Value = model.otherCode;
            parameters[1].Value = model.Remark;
            parameters[2].Value = PubConstant.YongHu;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.Details;
            //parameters[4].Value = model.id;

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
        public bool Update(p_parameterDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update p_parameterDetail set ");

            //strSql.Append(" TypeName = @TypeName , ");
            strSql.Append(" TypeID = @TypeID , ");
            strSql.Append(" Details = @Details , ");
            strSql.Append(" DetailsCode = @DetailsCode , ");
            strSql.Append(" otherCode = @otherCode , ");
            strSql.Append(" Remark = @Remark ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4) ,
                        //new SqlParameter("@TypeName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@TypeID", SqlDbType.Int,50) ,
                        new SqlParameter("@Details", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@DetailsCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@otherCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@Remark", SqlDbType.NVarChar,500)

            };

            parameters[0].Value = model.id;
            //parameters[1].Value = model.TypeName;
            parameters[1].Value = model.TypeID;
            parameters[2].Value = model.Details;
            parameters[3].Value = model.DetailsCode;
            parameters[4].Value = model.otherCode;
            parameters[5].Value = model.Remark;
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

        public int NextId()
        {
            string sql = "select ISNULL(MAX(id), 0) + 1 as id from p_parameterDetail";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from p_parameterDetail ");
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
            strSql.Append("delete from p_parameterDetail ");
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
        public p_parameterDetail GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, TypeID, Details, DetailsCode, otherCode, Remark, inPep, inDate  ");
            strSql.Append("  from p_parameterDetail ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            p_parameterDetail model = new p_parameterDetail();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                //model.TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
                //model.TypeID = ds.Tables[0].Rows[0]["TypeID"].ToString();
                if (ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                model.Details = ds.Tables[0].Rows[0]["Details"].ToString();
                model.DetailsCode = ds.Tables[0].Rows[0]["DetailsCode"].ToString();
                model.otherCode = ds.Tables[0].Rows[0]["otherCode"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.inPep = ds.Tables[0].Rows[0]["inPep"].ToString();
                if (ds.Tables[0].Rows[0]["inDate"].ToString() != "")
                {
                    model.inDate = DateTime.Parse(ds.Tables[0].Rows[0]["inDate"].ToString());
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
            strSql.Append(")AS Row, T.*  from p_parameterDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM p_parameterDetail ");
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
            strSql.Append(" FROM p_parameterDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
