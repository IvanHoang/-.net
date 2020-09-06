using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 数据访问类:CompanyList
    /// </summary>
    public partial class CompanyListParamBLL
    {
        CompanyListParamDAL dal = new CompanyListParamDAL();
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string sqlName)
        {
            return dal.Exists(sqlName);
        }

        public string GetappendTypecd()
        {
            return dal.GetappendTypecd();
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompanyListParamModel GetModel(string sqlName)
        {
            return dal.GetModel(sqlName);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CompanyListParamModel DataRowToModel(DataRow row)
        {
            CompanyListParamModel model = new CompanyListParamModel();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["sqlName"] != null)
                {
                    model.sqlName = row["sqlName"].ToString();
                }
                if (row["qz"] != null)
                {
                    model.qz = row["qz"].ToString();
                }
                if (row["companyName"] != null)
                {
                    model.companyName = row["companyName"].ToString();
                }
                if (row["companyAllName"] != null)
                {
                    model.companyAllName = row["companyAllName"].ToString();
                }
                if (row["jin2_username"] != null)
                {
                    model.jin2_username = row["jin2_username"].ToString();
                }
                if (row["jin2_password"] != null)
                {
                    model.jin2_password = row["jin2_password"].ToString();
                }
                if (row["auto"] != null)
                {
                    model.auto = row["auto"].ToString();
                }
                if (row["dataType"] != null)
                {
                    model.dataType = row["dataType"].ToString();
                }
                if (row["ValidityDate"] != null)
                {
                    model.ValidityDate = Convert.ToDateTime(row["ValidityDate"].ToString());
                }
                if (row["secdLawfCtrl"] != null)
                {
                    model.secdLawfCtrl = Convert.ToBoolean(row["secdLawfCtrl"].ToString());
                }
                if (row["CheckCtrl"] != null)
                {
                    model.CheckCtrl = int.Parse(row["CheckCtrl"].ToString());
                }
                if (row["oms"] != null)
                {
                    model.oms = row["oms"].ToString();
                }
                if (row["uploadAgent"] != null)
                {
                    model.uploadAgent = int.Parse(row["uploadAgent"].ToString());
                }
                if (row["sendSave"] != null)
                {
                    model.sendSave = int.Parse(row["sendSave"].ToString());
                }
            }
            return model;
        }

        //public int UpdateWarnDate(string sqlName, int hzqd_hk1, int hzqd_hk2, int hfd_gk)
        //{
        //    return dal.UpdateWarnDate(sqlName, hzqd_hk1, hzqd_hk2, hfd_gk);
        //}

        public DataSet Getmodel(string sqlName)
        {
            return dal.Getmodel(sqlName);
        }



        #endregion  BasicMethod
            #region  ExtensionMethod

            #endregion  ExtensionMethod
    }
}
