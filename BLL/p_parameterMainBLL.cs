using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
using DAL;



namespace BLL
{
    public class p_parameterMainBLL
    {
		p_parameterMainDAL dal = new p_parameterMainDAL();
		public p_parameterMainBLL()
		{ }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(p_parameterMain model)
		{
			return dal.Add(model);

		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(p_parameterMain model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{

			return dal.Delete(id);
		}
		/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string idlist)
		{
			return dal.DeleteList(idlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public p_parameterMain GetModel(int id)
		{

			return dal.GetModel(id);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return dal.GetList(Top, strWhere, filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<p_parameterMain> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<p_parameterMain> DataTableToList(DataTable dt)
		{
			List<p_parameterMain> modelList = new List<p_parameterMain>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				p_parameterMain model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new p_parameterMain();
					if (dt.Rows[n]["id"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.TypeName = dt.Rows[n]["TypeName"].ToString();
					model.TypeID = dt.Rows[n]["TypeID"].ToString();
					model.inPep = dt.Rows[n]["inPep"].ToString();
					model.transports = dt.Rows[n]["transports"].ToString();
					model.curr = dt.Rows[n]["curr"].ToString();
					if (dt.Rows[n]["inDate"].ToString() != "")
					{
						model.inDate = DateTime.Parse(dt.Rows[n]["inDate"].ToString());
					}


					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
		#endregion
	}
}
