using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class p_parameterDetailBLL
    {
		p_parameterDetailDAL dal = new p_parameterDetailDAL();
		public p_parameterDetailBLL()
		{ }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string id)
		{
			return dal.Exists(id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(p_parameterDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(p_parameterDetail model)
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
		public p_parameterDetail GetModel(int id)
		{

			return dal.GetModel(id);
		}


		public p_parameterDetail GetModel(string Details,string otherWhere)
		{
			string sql = string.Format("select * from p_parameterDetail where Details= '{0}'", Details);
			if (!string.IsNullOrWhiteSpace(otherWhere))
			{
				sql += (" and " + otherWhere);
			}
			DataTable dt = dal.RunSql(sql);
			int id = 0;
			if (dt.Rows.Count > 0)
			{
				id = Convert.ToInt32(dt.Rows[0]["id"]);
				return dal.GetModel(id);
			}
			return null;
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
		public List<p_parameterDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<p_parameterDetail> DataTableToList(DataTable dt)
		{
			List<p_parameterDetail> modelList = new List<p_parameterDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				p_parameterDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new p_parameterDetail();
					if (dt.Rows[n]["id"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["id"].ToString());
					}
					//model.TypeName = dt.Rows[n]["TypeName"].ToString();
					//model.TypeID = dt.Rows[n]["TypeID"].ToString();
					if (dt.Rows[n]["TypeID"].ToString() != "")
					{
						model.TypeID = int.Parse(dt.Rows[n]["TypeID"].ToString());
					}
					model.Details = dt.Rows[n]["Details"].ToString();
					model.DetailsCode = dt.Rows[n]["DetailsCode"].ToString();
					model.otherCode = dt.Rows[n]["otherCode"].ToString();
					model.Remark = dt.Rows[n]["Remark"].ToString();
					model.inPep = dt.Rows[n]["inPep"].ToString();
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

		// 个人扩展
		public int NextId()
        {
			return dal.NextId();
        }
	}
}
