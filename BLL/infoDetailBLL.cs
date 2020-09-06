using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
using Model;

namespace BLL
{
    public class infoDetailBLL
    {
		infoDetailDAL dal = new infoDetailDAL();
		public infoDetailBLL()
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
		public int Add(infoDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(infoDetail model)
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
		public infoDetail GetModel(int id)
		{

			return dal.GetModel(id);
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
		public List<infoDetail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<infoDetail> DataTableToList(DataTable dt)
		{
			List<infoDetail> modelList = new List<infoDetail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				infoDetail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new infoDetail();
					if (dt.Rows[n]["id"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["id"].ToString());
					}
					if (dt.Rows[n]["putrecSeqno"].ToString() != "")
					{
						model.putrecSeqno = int.Parse(dt.Rows[n]["putrecSeqno"].ToString());
					}
					model.jobnumber = dt.Rows[n]["jobnumber"].ToString();
					model.SKU = dt.Rows[n]["SKU"].ToString();
					model.GoodsName = dt.Rows[n]["GoodsName"].ToString();
					model.HSCode = dt.Rows[n]["HSCode"].ToString();
					model.gdsSpcfModelDesc = dt.Rows[n]["gdsSpcfModelDesc"].ToString();
					if (dt.Rows[n]["dcl_QTY"].ToString() != "")
					{
						model.dcl_QTY = decimal.Parse(dt.Rows[n]["dcl_QTY"].ToString());
					}
					model.dclUnitcd = dt.Rows[n]["dclUnitcd"].ToString();
					if (dt.Rows[n]["law_QTY"].ToString() != "")
					{
						model.law_QTY = decimal.Parse(dt.Rows[n]["law_QTY"].ToString());
					}
					model.lawfUnitcd = dt.Rows[n]["lawfUnitcd"].ToString();
					if (dt.Rows[n]["Volume"].ToString() != "")
					{
						model.Volume = decimal.Parse(dt.Rows[n]["Volume"].ToString());
					}
					if (dt.Rows[n]["grossWt"].ToString() != "")
					{
						model.grossWt = decimal.Parse(dt.Rows[n]["grossWt"].ToString());
					}
					if (dt.Rows[n]["netWt"].ToString() != "")
					{
						model.netWt = decimal.Parse(dt.Rows[n]["netWt"].ToString());
					}
					model.Origin = dt.Rows[n]["Origin"].ToString();
					model.InPep = dt.Rows[n]["InPep"].ToString();

					if (dt.Rows[n]["InDate"].ToString() != "")
					{
						model.InDate = DateTime.Parse(dt.Rows[n]["InDate"].ToString());
					}
					model.inDep = dt.Rows[n]["inDep"].ToString();
					model.batch = dt.Rows[n]["batch"].ToString();
					model.LPN=dt.Rows[n]["LPN"].ToString();
					if (dt.Rows[n]["productionDate"].ToString() != "")
					{
						model.productionDate = DateTime.Parse(dt.Rows[n]["productionDate"].ToString());
					}
					if (dt.Rows[n]["validDate"].ToString() != "")
					{
						model.validDate = DateTime.Parse(dt.Rows[n]["validDate"].ToString());
					}
					model.Location = dt.Rows[n]["Location"].ToString();
					model.curr = dt.Rows[n]["curr"].ToString();
					model.goodsStatus = dt.Rows[n]["goodsStatus"].ToString();
					if (dt.Rows[n]["unitprice"].ToString() != "")
					{
						model.netWt = decimal.Parse(dt.Rows[n]["unitprice"].ToString());
					}
					if (dt.Rows[n]["totalamount"].ToString() != "")
					{
						model.netWt = decimal.Parse(dt.Rows[n]["totalamount"].ToString());
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
