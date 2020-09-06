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
    public class nimp_agentBLL
    {
        private readonly nimp_agentDAL dal = new nimp_agentDAL();
        public nimp_agentBLL()
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
        public int Add(nimp_agent model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(nimp_agent model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int ExecuteSql(string sql)
        {

            return dal.ExecuteSql(sql);
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
        public nimp_agent GetModel(int id)
        {

            return dal.GetModel(id);
        }

        public nimp_agent GetModel(string agentAllName, string AgentName, string otherWhere)
        {
            string sql = string.Format("select * from nimp_agent where ( agentAllName= '{0}' or AgentName= '{1}') ", agentAllName, AgentName);
            if (!string.IsNullOrWhiteSpace(otherWhere))
            {
                sql += (" and "+ otherWhere);
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
        public List<nimp_agent> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<nimp_agent> DataTableToList(DataTable dt)
        {
            List<nimp_agent> modelList = new List<nimp_agent>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                nimp_agent model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new nimp_agent();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.AgentName = dt.Rows[n]["AgentName"].ToString();
                    model.agentAllName = dt.Rows[n]["agentAllName"].ToString();
                    model.contactPep = dt.Rows[n]["contactPep"].ToString();
                    model.contactTel = dt.Rows[n]["contactTel"].ToString();
                    model.ypep = dt.Rows[n]["ypep"].ToString();
                    model.ytel = dt.Rows[n]["ytel"].ToString();
                    model.cpep = dt.Rows[n]["cpep"].ToString();
                    model.ctel = dt.Rows[n]["ctel"].ToString();
                    model.address = dt.Rows[n]["address"].ToString();
                    model.tel = dt.Rows[n]["tel"].ToString();
                    model.userName = dt.Rows[n]["userName"].ToString();
                    model.pwd = dt.Rows[n]["pwd"].ToString();
                    model.fax = dt.Rows[n]["fax"].ToString();
                    model.bank = dt.Rows[n]["bank"].ToString();
                    model.bank_ads = dt.Rows[n]["bank_ads"].ToString();
                    model.bank_tel = dt.Rows[n]["bank_tel"].ToString();
                    model.bank_account = dt.Rows[n]["bank_account"].ToString();
                    model.invoiceNo = dt.Rows[n]["invoiceNo"].ToString();
                    model.in_pep = dt.Rows[n]["in_pep"].ToString();
                    if (dt.Rows[n]["in_date"].ToString() != "")
                    {
                        model.in_date = DateTime.Parse(dt.Rows[n]["in_date"].ToString());
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
