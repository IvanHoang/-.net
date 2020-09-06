using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Command;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// 数据访问类:CompanyIfoDAL
	/// </summary>
	public partial class CompanyIfoDAL
	{
         DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
		public CompanyIfoDAL()
		{}
		#region  BasicMethod


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CompanyIfoModel  model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into CompanyIfo(");
			strSql.Append("customNo,bizopEtpsNm,bizopEtpsno,bizopEtpsSccd,dclPlcCuscd,createPep,createDate)");
			strSql.Append(" values (");
			strSql.Append("@customNo,@bizopEtpsNm,@bizopEtpsno,@bizopEtpsSccd,@dclPlcCuscd,@createPep,@createDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@customNo", SqlDbType.NVarChar,50),
					new SqlParameter("@bizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@bizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@bizopEtpsSccd", SqlDbType.NVarChar,18),
					new SqlParameter("@dclPlcCuscd", SqlDbType.NVarChar,4),
					new SqlParameter("@createPep", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.DateTime)};
			parameters[0].Value = model.customNo;
			parameters[1].Value = model.bizopEtpsNm;
			parameters[2].Value = model.bizopEtpsno;
			parameters[3].Value = model.bizopEtpsSccd;
			parameters[4].Value = model.dclPlcCuscd;
            parameters[5].Value = PubConstant.YongHu;
			parameters[6].Value = model.createDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(CompanyIfoModel  model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update CompanyIfo set ");
			strSql.Append("customNo=@customNo,");
			strSql.Append("bizopEtpsNm=@bizopEtpsNm,");
			strSql.Append("bizopEtpsno=@bizopEtpsno,");
			strSql.Append("bizopEtpsSccd=@bizopEtpsSccd,");
			strSql.Append("dclPlcCuscd=@dclPlcCuscd,");
			strSql.Append("createPep=@createPep,");
			strSql.Append("createDate=@createDate");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@customNo", SqlDbType.NVarChar,50),
					new SqlParameter("@bizopEtpsNm", SqlDbType.NVarChar,768),
					new SqlParameter("@bizopEtpsno", SqlDbType.NVarChar,10),
					new SqlParameter("@bizopEtpsSccd", SqlDbType.NVarChar,18),
					new SqlParameter("@dclPlcCuscd", SqlDbType.NVarChar,4),
					new SqlParameter("@createPep", SqlDbType.NVarChar,50),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.customNo;
			parameters[1].Value = model.bizopEtpsNm;
			parameters[2].Value = model.bizopEtpsno;
			parameters[3].Value = model.bizopEtpsSccd;
			parameters[4].Value = model.dclPlcCuscd;
			parameters[5].Value =PubConstant.YongHu;
			parameters[6].Value = model.createDate;
			parameters[7].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CompanyIfo ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from CompanyIfo ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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

        public DataTable GetModelTable()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,customNo,bizopEtpsNm,bizopEtpsno,bizopEtpsSccd,dclPlcCuscd,createPep,createDate from CompanyIfo ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0];
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CompanyIfoModel  DataRowToModel(DataRow row)
		{
			CompanyIfoModel  model=new CompanyIfoModel ();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["customNo"]!=null)
				{
					model.customNo=row["customNo"].ToString();
				}
				if(row["bizopEtpsNm"]!=null)
				{
					model.bizopEtpsNm=row["bizopEtpsNm"].ToString();
				}
				if(row["bizopEtpsno"]!=null)
				{
					model.bizopEtpsno=row["bizopEtpsno"].ToString();
				}
				if(row["bizopEtpsSccd"]!=null)
				{
					model.bizopEtpsSccd=row["bizopEtpsSccd"].ToString();
				}
				if(row["dclPlcCuscd"]!=null)
				{
					model.dclPlcCuscd=row["dclPlcCuscd"].ToString();
				}
				if(row["createPep"]!=null)
				{
					model.createPep=row["createPep"].ToString();
				}
				if(row["createDate"]!=null && row["createDate"].ToString()!="")
				{
					model.createDate=DateTime.Parse(row["createDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,customNo,bizopEtpsNm,bizopEtpsno,bizopEtpsSccd,dclPlcCuscd,createPep,createDate ");
			strSql.Append(" FROM CompanyIfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,customNo,bizopEtpsNm,bizopEtpsno,bizopEtpsSccd,dclPlcCuscd,createPep,createDate ");
			strSql.Append(" FROM CompanyIfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM CompanyIfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from CompanyIfo T ");
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
			parameters[0].Value = "CompanyIfo";
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


