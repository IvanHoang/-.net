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
    public class goodsSetDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from goodsSet");
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
        public int Add(goodsSet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into goodsSet(");
            strSql.Append("AgentName,SKU,GoodsName,gdsSpcfModelDesc,dclUnitcd,lawfUnitcd,Volume,netWt,Origin,InDate,InPep,department,HSCode");
            strSql.Append(")values(");
            strSql.Append("@AgentName,@SKU,@GoodsName,@gdsSpcfModelDesc,@dclUnitcd,@lawfUnitcd,@Volume,@netWt,@Origin,@InDate,@InPep,@department,@HSCode");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters ={
                new SqlParameter("@AgentName",SqlDbType.NVarChar,50),
                new SqlParameter("@SKU",SqlDbType.NVarChar,50),
                new SqlParameter("@GoodsName",SqlDbType.NVarChar,100),
                new SqlParameter("@gdsSpcfModelDesc",SqlDbType.NVarChar,100),
                new SqlParameter("@dclUnitcd",SqlDbType.NVarChar,50),
                new SqlParameter("@lawfUnitcd",SqlDbType.NVarChar,50),
                new SqlParameter("@Volume",SqlDbType.Decimal,13),
                new SqlParameter("@netWt",SqlDbType.Decimal,13),
                new SqlParameter("@Origin",SqlDbType.NVarChar,50),
                new SqlParameter("@InDate",SqlDbType.DateTime),
                new SqlParameter("@InPep",SqlDbType.NVarChar,50),
                new SqlParameter("@department",SqlDbType.NVarChar,50),
                new SqlParameter("@HSCode",SqlDbType.NVarChar,50)
            };

            parameters[0].Value = model.AgentName;
            parameters[1].Value = model.SKU;
            parameters[2].Value = model.GoodsName;
            parameters[3].Value = model.gdsSpcfModelDesc;
            parameters[4].Value = model.dclUnitcd;
            parameters[5].Value = model.lawfUnitcd;
            parameters[6].Value = model.Volume;
            parameters[7].Value = model.netWt;
            parameters[8].Value = model.Origin;
            parameters[9].Value = DateTime.Now;
            parameters[10].Value = PubConstant.YongHu;
            parameters[11].Value = PubConstant.BuMen;
            parameters[12].Value = model.HSCode;
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
        public bool Update(goodsSet model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update goodsSet set");
            strSql.Append("AgentName=@AgentName ,");
            strSql.Append("SKU=@SKU ,");
            strSql.Append("GoodsName=@GoodsName ,");
            strSql.Append("gdsSpcfModelDesc=@gdsSpcfModelDesc ,");
            strSql.Append("dclUnitcd=@dclUnitcd ,");
            strSql.Append("lawfUnitcd=@lawfUnitcd ,");
            strSql.Append("Volume=@Volume ,");
            strSql.Append("netWt=@netWt ,");
            strSql.Append("Origin=@Origin,");
            strSql.Append("department=@department ,");
            strSql.Append("HSCode=@HSCode ");
            strSql.Append("where id=@id");

            SqlParameter[] parameters =
            {
                new SqlParameter("@id",SqlDbType.Int,4),
                new SqlParameter("@AgentName",SqlDbType.NVarChar,50),
                new SqlParameter("@SKU",SqlDbType.NVarChar,50),
                new SqlParameter("@GoodsName",SqlDbType.NVarChar,100),
                new SqlParameter("@gdsSpcfModelDesc",SqlDbType.NVarChar,100),
                new SqlParameter("@dclUnitcd",SqlDbType.NVarChar,50),
                new SqlParameter("@lawfUnitcd",SqlDbType.NVarChar,50),
                new SqlParameter("@Volume",SqlDbType.Decimal,13),
                new SqlParameter("@netWt",SqlDbType.Decimal,13),
                new SqlParameter("@Origin",SqlDbType.NVarChar,50),
                //new SqlParameter("@department",SqlDbType.NVarChar,50),
                new SqlParameter("@HSCode",SqlDbType.NVarChar,50)
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.AgentName;
            parameters[2].Value = model.SKU;
            parameters[3].Value = model.GoodsName;
            parameters[4].Value = model.gdsSpcfModelDesc;
            parameters[5].Value = model.dclUnitcd;
            parameters[6].Value = model.lawfUnitcd;
            parameters[7].Value = model.Volume;
            parameters[8].Value = model.netWt;
            parameters[9].Value = model.Origin;
            //parameters[10].Value = model.department;
            parameters[10].Value = model.HSCode;
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
            string sql = "select ISNULL(MAX(id), 0) + 1 as id from goodsSet";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from goodsSet ");
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
            strSql.Append("delete from goodsSet ");
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
        public goodsSet GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,AgentName,SKU,GoodsName,gdsSpcfModelDesc,dclUnitcd,lawfUnitcd,Volume,netWt,Origin,InDate,InPep,department,HSCode");
            strSql.Append("from goodsSet");
            strSql.Append("where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            goodsSet model = new goodsSet();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.AgentName = ds.Tables[0].Rows[0]["AgentName"].ToString();
                model.SKU = ds.Tables[0].Rows[0]["SKU"].ToString();
                model.GoodsName = ds.Tables[0].Rows[0]["GoodsName"].ToString();
                model.gdsSpcfModelDesc = ds.Tables[0].Rows[0]["gdsSpcfModelDesc"].ToString();
                model.dclUnitcd = ds.Tables[0].Rows[0]["dclUnitcd"].ToString();
                model.lawfUnitcd = ds.Tables[0].Rows[0]["lawfUnitcd"].ToString();
                if (ds.Tables[0].Rows[0]["Volume"].ToString() != "")
                {
                    model.Volume = decimal.Parse(ds.Tables[0].Rows[0]["Volume"].ToString());
                }
                if (ds.Tables[0].Rows[0]["netWt"].ToString() != "")
                {
                    model.netWt = decimal.Parse(ds.Tables[0].Rows[0]["netWt"].ToString());
                }
                model.Origin = ds.Tables[0].Rows[0]["Origin"].ToString();
                model.InPep = ds.Tables[0].Rows[0]["InPep"].ToString();
                model.department = ds.Tables[0].Rows[0]["department"].ToString();
                model.HSCode = ds.Tables[0].Rows[0]["HSCode"].ToString();
                if (ds.Tables[0].Rows[0]["InDate"].ToString() != "")
                {
                    model.InDate = DateTime.Parse(ds.Tables[0].Rows[0]["inDate"].ToString());
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
            strSql.Append(")AS Row, T.*  from goodsSet T ");
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
            strSql.Append(" FROM goodsSet ");
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
            strSql.Append(" FROM goodsSet ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
