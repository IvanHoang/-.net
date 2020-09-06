using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Model;
using DAL;



namespace BLL
{
    public class nimp_mainBLL
    {
		private readonly nimp_mainDAL dal = new nimp_mainDAL();
		public nimp_mainBLL()
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
		public int Add(nimp_main model)
		{
			return dal.Add(model);

		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(nimp_main model)
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
		public nimp_main GetModel(int id)
		{

			return dal.GetModel(id);
		}

		public nimp_main GetModel(string jobnumber,string agentName,string bondInvtNo,string entryNo,string BL_No,string customNO,string invoiceNo,string orderno,string transportNo)
		{
			string sql = string.Format("select * from nimp_agent where jobnumber= '{0}' and agentName='{1}' and bondInvtNo='{2}' and entryNo='{3}' and BL_No='{4}' and customNO='{5}' and invoiceNo='{6}' and orderno='{7}' and transportNo='{8}'", jobnumber, agentName, bondInvtNo,entryNo, BL_No, customNO, invoiceNo, orderno, transportNo);
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
		public DataSet GetListByPageNew(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPageNew(strWhere, orderby, startIndex, endIndex);
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
		public List<nimp_main> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<nimp_main> DataTableToList(DataTable dt)
		{
			List<nimp_main> modelList = new List<nimp_main>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				nimp_main model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new nimp_main();
					if (dt.Rows[n]["id"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["id"].ToString());
					}
					model.jobnumber = dt.Rows[n]["jobnumber"].ToString();
					model.agentName = dt.Rows[n]["agentName"].ToString();
					model.bondInvtNo = dt.Rows[n]["bondInvtNo"].ToString();
					model.entryNo = dt.Rows[n]["entryNo"].ToString();
					model.transport = dt.Rows[n]["transport"].ToString();
					model.BL_No = dt.Rows[n]["BL_No"].ToString();
					model.customNO = dt.Rows[n]["customNO"].ToString();
					model.inpep = dt.Rows[n]["inpep"].ToString();
					model.inDep = dt.Rows[n]["inDep"].ToString();
					if (dt.Rows[n]["status"].ToString() != "")
					{
						model.id = int.Parse(dt.Rows[n]["status"].ToString());
					}
					model.c_jobumber = dt.Rows[n]["c_jobumber"].ToString();
					model.invoiceNo = dt.Rows[n]["invoiceNo"].ToString();
					model.transportNo = dt.Rows[n]["transportNo"].ToString();
					model.orderno = dt.Rows[n]["orderno"].ToString();
					model.remark = dt.Rows[n]["remark"].ToString();
					if (dt.Rows[n]["planDate"].ToString() != "")
					{
						model.planDate = DateTime.Parse(dt.Rows[n]["planDate"].ToString());
					}
					if (dt.Rows[n]["indate"].ToString() != "")
					{
						model.indate = DateTime.Parse(dt.Rows[n]["indate"].ToString());
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

		// 获取下一个商品的ID
		public int NextId()
		{
			return dal.NextId();
		}
		#endregion
	}
}
