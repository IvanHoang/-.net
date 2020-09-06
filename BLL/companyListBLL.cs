using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 数据访问类:companyList
    /// </summary>
    public partial class companyListBLL
    {
        companyListDAL dal = new companyListDAL();
        public companyListBLL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string jin2Code)
        {
            return dal.Exists(jin2Code);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(companyListModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(companyListModel model)
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string jin2Code)
        {

            return dal.Delete(jin2Code);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public companyListModel GetModel(string jin2Code)
        {

            return dal.GetModel(jin2Code);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public companyListModel GetModel(int id)
        {

            return dal.GetModel(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetModelTable(string jin2Code)
        {

            return dal.GetModelTable(jin2Code);
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
        public List<companyListModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<companyListModel> DataTableToList(DataTable dt)
        {
            List<companyListModel> modelList = new List<companyListModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                companyListModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
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

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
