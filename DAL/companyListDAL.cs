using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
using Command;

namespace DAL
{
    /// <summary>
    /// 数据访问类:companyList
    /// </summary>
    public partial class companyListDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public companyListDAL()
        { }

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string jin2Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from companyList");
            strSql.Append(" where jin2Code=@jin2Code ");
            SqlParameter[] parameters = {
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50)			};
            parameters[0].Value = jin2Code;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(companyListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into companyList(");
            strSql.Append("jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate)");
            strSql.Append(" values (");
            strSql.Append("@jin2Code,@companyName,@putrecNo,@bizopEtpsno,@bizopEtpsNm,@rcvgdEtpsNo,@rcvgdEtpsNm,@dclEtpsno,@dclEtpsNm,@rltEntryBizopEtpsSccd,@rltEntryBizopEtpsno,@rltEntryBizopEtpsNm,@createPep,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50),
					new SqlParameter("@companyName", SqlDbType.NVarChar,50),
					new SqlParameter("@putrecNo", SqlDbType.NVarChar,80),
					new SqlParameter("@bizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@bizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@rcvgdEtpsNo", SqlDbType.NVarChar,10),
					new SqlParameter("@rcvgdEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@dclEtpsno", SqlDbType.NChar,10),
					new SqlParameter("@dclEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@rltEntryBizopEtpsSccd", SqlDbType.NVarChar,18),
					new SqlParameter("@rltEntryBizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@rltEntryBizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@createPep", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.jin2Code;
            parameters[1].Value = model.companyName;
            parameters[2].Value = model.putrecNo;
            parameters[3].Value = model.bizopEtpsno;
            parameters[4].Value = model.bizopEtpsNm;
            parameters[5].Value = model.rcvgdEtpsNo;
            parameters[6].Value = model.rcvgdEtpsNm;
            parameters[7].Value = model.dclEtpsno;
            parameters[8].Value = model.dclEtpsNm;
            parameters[9].Value = model.rltEntryBizopEtpsSccd;
            parameters[10].Value = model.rltEntryBizopEtpsno;
            parameters[11].Value = model.rltEntryBizopEtpsNm;
            parameters[12].Value = PubConstant.YongHu;
            parameters[13].Value = model.createDate;

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
        public bool Update(companyListModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update companyList set ");
            strSql.Append("companyName=@companyName,");
            strSql.Append("putrecNo=@putrecNo,");
            strSql.Append("bizopEtpsno=@bizopEtpsno,");
            strSql.Append("bizopEtpsNm=@bizopEtpsNm,");
            strSql.Append("rcvgdEtpsNo=@rcvgdEtpsNo,");
            strSql.Append("rcvgdEtpsNm=@rcvgdEtpsNm,");
            strSql.Append("dclEtpsno=@dclEtpsno,");
            strSql.Append("dclEtpsNm=@dclEtpsNm,");
            strSql.Append("rltEntryBizopEtpsSccd=@rltEntryBizopEtpsSccd,");
            strSql.Append("rltEntryBizopEtpsno=@rltEntryBizopEtpsno,");
            strSql.Append("rltEntryBizopEtpsNm=@rltEntryBizopEtpsNm,");
            strSql.Append("createPep=@createPep,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@companyName", SqlDbType.NVarChar,50),
					new SqlParameter("@putrecNo", SqlDbType.NVarChar,80),
					new SqlParameter("@bizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@bizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@rcvgdEtpsNo", SqlDbType.NVarChar,10),
					new SqlParameter("@rcvgdEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@dclEtpsno", SqlDbType.NChar,10),
					new SqlParameter("@dclEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@rltEntryBizopEtpsSccd", SqlDbType.NVarChar,18),
					new SqlParameter("@rltEntryBizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@rltEntryBizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@createPep", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.companyName;
            parameters[1].Value = model.putrecNo;
            parameters[2].Value = model.bizopEtpsno;
            parameters[3].Value = model.bizopEtpsNm;
            parameters[4].Value = model.rcvgdEtpsNo;
            parameters[5].Value = model.rcvgdEtpsNm;
            parameters[6].Value = model.dclEtpsno;
            parameters[7].Value = model.dclEtpsNm;
            parameters[8].Value = model.rltEntryBizopEtpsSccd;
            parameters[9].Value = model.rltEntryBizopEtpsno;
            parameters[10].Value = model.rltEntryBizopEtpsNm;
            parameters[11].Value = PubConstant.YongHu;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.id;
            parameters[14].Value = model.jin2Code;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from companyList ");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string jin2Code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from companyList ");
            strSql.Append(" where jin2Code=@jin2Code ");
            SqlParameter[] parameters = {
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50)			};
            parameters[0].Value = jin2Code;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from companyList ");
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
        public companyListModel GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate from companyList ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            companyListModel model = new companyListModel();
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
        public companyListModel GetModel(string jin2Code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate from companyList ");
            strSql.Append(" where jin2Code=@jin2Code");
            SqlParameter[] parameters = {
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = jin2Code;

            companyListModel model = new companyListModel();
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
        public DataTable GetModelTable(string jin2Code)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate from companyList ");
            strSql.Append(" where jin2Code=@jin2Code");
            SqlParameter[] parameters = {
					new SqlParameter("@jin2Code", SqlDbType.NVarChar,50)
			};
            parameters[0].Value = jin2Code;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds.Tables[0];
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public companyListModel DataRowToModel(DataRow row)
        {
            companyListModel model = new companyListModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["jin2Code"] != null)
                {
                    model.jin2Code = row["jin2Code"].ToString();
                }
                if (row["companyName"] != null)
                {
                    model.companyName = row["companyName"].ToString();
                }
                if (row["putrecNo"] != null)
                {
                    model.putrecNo = row["putrecNo"].ToString();
                }
                if (row["bizopEtpsno"] != null)
                {
                    model.bizopEtpsno = row["bizopEtpsno"].ToString();
                }
                if (row["bizopEtpsNm"] != null)
                {
                    model.bizopEtpsNm = row["bizopEtpsNm"].ToString();
                }
                if (row["rcvgdEtpsNo"] != null)
                {
                    model.rcvgdEtpsNo = row["rcvgdEtpsNo"].ToString();
                }
                if (row["rcvgdEtpsNm"] != null)
                {
                    model.rcvgdEtpsNm = row["rcvgdEtpsNm"].ToString();
                }
                if (row["dclEtpsno"] != null)
                {
                    model.dclEtpsno = row["dclEtpsno"].ToString();
                }
                if (row["dclEtpsNm"] != null)
                {
                    model.dclEtpsNm = row["dclEtpsNm"].ToString();
                }
                if (row["rltEntryBizopEtpsSccd"] != null)
                {
                    model.rltEntryBizopEtpsSccd = row["rltEntryBizopEtpsSccd"].ToString();
                }
                if (row["rltEntryBizopEtpsno"] != null)
                {
                    model.rltEntryBizopEtpsno = row["rltEntryBizopEtpsno"].ToString();
                }
                if (row["rltEntryBizopEtpsNm"] != null)
                {
                    model.rltEntryBizopEtpsNm = row["rltEntryBizopEtpsNm"].ToString();
                }
                if (row["createPep"] != null)
                {
                    model.createPep = row["createPep"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate ");
            strSql.Append(" FROM companyList ");
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
            strSql.Append(" id,jin2Code,companyName,putrecNo,bizopEtpsno,bizopEtpsNm,rcvgdEtpsNo,rcvgdEtpsNm,dclEtpsno,dclEtpsNm,rltEntryBizopEtpsSccd,rltEntryBizopEtpsno,rltEntryBizopEtpsNm,createPep,createDate ");
            strSql.Append(" FROM companyList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM companyList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            strSql.Append(")AS Row, T.*  from companyList T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "companyList";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
