using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class p_parameterDetail
    {

        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 参数名称
        /// </summary>		
        //private string _typename;
        //public string TypeName
        //{
        //    get { return _typename; }
        //    set { _typename = value; }
        //}
        /// <summary>
        /// 参数编号
        /// </summary>		
        private int _typeid;
        public int TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>		
        private string _details;
        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }
        /// <summary>
        /// 代码
        /// </summary>		
        private string _detailscode;
        public string DetailsCode
        {
            get { return _detailscode; }
            set { _detailscode = value; }
        }
        /// <summary>
        /// 内容对应第三方代码(人工不可填写，只能通过同步数据获得)
        /// </summary>		
        private string _othercode;
        public string otherCode
        {
            get { return _othercode; }
            set { _othercode = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// inPep
        /// </summary>		
        private string _inpep;
        public string inPep
        {
            get { return _inpep; }
            set { _inpep = value; }
        }
        /// <summary>
        /// inDate
        /// </summary>		
        private DateTime _indate = DateTime.Now;
        public DateTime inDate
        {
            get { return _indate; }
            set { _indate = value; }
        }
    }
}
