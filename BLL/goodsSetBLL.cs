using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class goodsSetBLL
    {
		private readonly goodsSetDAL dal = new goodsSetDAL();
		public goodsSetBLL()
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
		public int Add(goodsSet model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(goodsSet model)
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
		public goodsSet GetModel(int id)
		{

			return dal.GetModel(id);
		}

		public goodsSet GetModel(string HSCode,string GoodsName,string SKU)
        {
			string sql = string.Format("select * from goodsSet where HSCode= '{0}' and GoodsName= '{1}' and SKU= '{2}'",HSCode,GoodsName,SKU);
			DataTable dt = dal.RunSql(sql);
			int id = 0;
			if (dt.Rows.Count > 0)
			{
				id = Convert.ToInt32(dt.Rows[0]["id"]);
				return dal.GetModel(id);
			}
			return null;
		}
		public goodsSet GetModel(string SKU, string otherWhere)
        {
			string sql = string.Format("select * from goodsSet where  SKU= '{0}'  ", SKU);
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
		public List<goodsSet> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<goodsSet> DataTableToList(DataTable dt)
		{
			List<goodsSet> modelList = new List<goodsSet>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				goodsSet model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new goodsSet();
					if (dt.Rows[n]["id"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.AgentName = dt.Rows[n]["AgentName"].ToString();
					model.SKU = dt.Rows[n]["SKU"].ToString();
					model.GoodsName = dt.Rows[n]["GoodsName"].ToString();
					model.gdsSpcfModelDesc = dt.Rows[n]["gdsSpcfModelDesc"].ToString();
					model.dclUnitcd = dt.Rows[n]["dclUnitcd"].ToString();
					model.lawfUnitcd = dt.Rows[n]["lawfUnitcd"].ToString();
					if (dt.Rows[n]["Volume"].ToString() != "")
					{
						model.Volume = decimal.Parse(dt.Rows[n]["Volume"].ToString());
					}
					if (dt.Rows[n]["netWt"].ToString() != "")
					{
						model.netWt = decimal.Parse(dt.Rows[n]["netWt"].ToString());
					}
					model.Origin = dt.Rows[n]["Origin"].ToString();
					model.InPep = dt.Rows[n]["InPep"].ToString();
					model.department = dt.Rows[n]["department"].ToString();
					model.HSCode = dt.Rows[n]["HSCode"].ToString();
					if (dt.Rows[n]["InDate"].ToString() != "")
					{
						model.InDate = DateTime.Parse(dt.Rows[n]["inDate"].ToString());
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

		// 获取下一个商品的ID
		public int NextId()
        {
			return dal.NextId();
        }


	}
}
