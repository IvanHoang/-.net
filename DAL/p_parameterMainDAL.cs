using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
using Command;

namespace DAL
{
    public class p_parameterMainDAL
    {
		DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
		public bool Exists(int id)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from p_parameterMain");
			strSql.Append(" where ");
			strSql.Append(" TypeName = @TypeName and  ");
			strSql.Append(" TypeID = @TypeID and  ");
			strSql.Append(" inPep = @inPep and  ");
			strSql.Append(" inDate = @inDate  ");
			strSql.Append(" transports = @transports and  ");
			strSql.Append(" curr = @curr   ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(p_parameterMain model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("insert into p_parameterMain(");
			strSql.Append("TypeName,TypeID,inPep,inDate,transports,curr");
			strSql.Append(") values (");
			strSql.Append("@TypeName,@TypeID,@inPep,@inDate,@transports,@curr");
			strSql.Append(") ");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
						new SqlParameter("@TypeName", SqlDbType.NVarChar,50) ,
						new SqlParameter("@TypeID", SqlDbType.NVarChar,50) ,
						new SqlParameter("@inPep", SqlDbType.NVarChar,50) ,
						new SqlParameter("@inDate", SqlDbType.DateTime),
						new SqlParameter("@transports",SqlDbType.NVarChar,20),
						new SqlParameter("@curr",SqlDbType.NVarChar,10)

			};

			parameters[0].Value = model.TypeName;
			parameters[1].Value = model.TypeID;
			parameters[2].Value = model.inPep;
			parameters[3].Value = model.inDate;
			parameters[4].Value = model.transports;
			parameters[5].Value = model.curr;

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
		public bool Update(p_parameterMain model)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("update p_parameterMain set ");

			strSql.Append(" TypeName = @TypeName , ");
			strSql.Append(" TypeID = @TypeID , ");
			strSql.Append(" inPep = @inPep , ");
			strSql.Append(" inDate = @inDate  ");
			strSql.Append(" transports = @transports  ");
			strSql.Append(" curr = @curr  ");
			strSql.Append(" where id=@id ");

			SqlParameter[] parameters = {
						new SqlParameter("@id", SqlDbType.Int,4) ,
						new SqlParameter("@TypeName", SqlDbType.NVarChar,50) ,
						new SqlParameter("@TypeID", SqlDbType.NVarChar,50) ,
						new SqlParameter("@inPep", SqlDbType.NVarChar,50) ,
						new SqlParameter("@inDate", SqlDbType.DateTime),
						new SqlParameter("@transports",SqlDbType.NVarChar,20),
						new SqlParameter("@curr",SqlDbType.NVarChar,20)

			};

			parameters[0].Value = model.id;
			parameters[1].Value = model.TypeName;
			parameters[2].Value = model.TypeID;
			parameters[3].Value = model.inPep;
			parameters[4].Value = model.inDate;
			parameters[5].Value = model.transports;
			parameters[6].Value = model.curr;
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
			strSql.Append("delete from p_parameterMain ");
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
			strSql.Append("delete from p_parameterMain ");
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
		public p_parameterMain GetModel(int id)
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append("select id, TypeName, TypeID, inPep, inDate, transports, curr  ");
			strSql.Append("  from p_parameterMain ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;


			p_parameterMain model = new p_parameterMain();
			DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

			if (ds.Tables[0].Rows.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["id"].ToString() != "")
				{
					model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				model.TypeName = ds.Tables[0].Rows[0]["TypeName"].ToString();
				model.TypeID = ds.Tables[0].Rows[0]["TypeID"].ToString();
				model.inPep = ds.Tables[0].Rows[0]["inPep"].ToString();
				model.transports= ds.Tables[0].Rows[0]["transports"].ToString();
				model.curr = ds.Tables[0].Rows[0]["curr"].ToString();
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM p_parameterMain ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			return DbHelperSQLParam.Query(strSql.ToString());
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
			strSql.Append(" FROM p_parameterMain ");
			if (strWhere.Trim() != "")
			{
				strSql.Append(" where " + strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

	}
}
