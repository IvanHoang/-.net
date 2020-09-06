using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using Model;
using System.Security.Cryptography;
namespace BLL
{
    public class hr_empinfoBLL
    {
        hr_empinfoDAL dal = new hr_empinfoDAL();
        public hr_empinfoBLL()
        {
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetModelTable(int id)
        {
            return dal.GetModelTable(id);
        }
        public DataTable Get_list(string userName, string pwd)
        {
            return dal.Get_list(userName, MD5Encrypt64(pwd));
        }
        public DataTable Get_list_OMS(string userName)
        {
            return dal.Get_list_OMS(userName);
        }
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(hr_empinfoModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(hr_empinfoModel model)
        {
            return dal.Update(model);
        }
        public bool changePWD(string user,string pwd)
        {
            return dal.changePWD(user, MD5Encrypt64(pwd));
        }
        /// <summary>
        /// 离职
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }
        public hr_empinfoModel GetModel(int id)
        {

            return dal.GetModel(id);
        }
        public hr_empinfoModel GetModelNew(int id)
        {

            return dal.GetModelNew(id);
        }
        
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        public bool upin(int id)
        {
            return dal.upin(id);
        }
        public int Exists(string UserName, string pwd)
        {
            return dal.Exists(UserName, MD5Encrypt64(pwd));
        }

        public bool ExistsAddUpdate(string UserName, string Name, string id)
        {
            return dal.ExistsAddUpdate(UserName, Name, id);
        }

        public int baocun(string UserName, string pwd, string newPwd1)
        {
            return dal.baocun(UserName,pwd,newPwd1);
        }

        public  string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}
