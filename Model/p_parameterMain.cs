using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class p_parameterMain
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
        private string _typename;
        public string TypeName
        {
            get { return _typename; }
            set { _typename = value; }
        }
        /// <summary>
        /// 参数编号
        /// </summary>		
        private string _typeid;
        public string TypeID
        {
            get { return _typeid; }
            set { _typeid = value; }
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
        private DateTime _indate;
        public DateTime inDate
        {
            get { return _indate; }
            set { _indate = value; }
        }

        /// <summary>
        /// transports
        /// </summary>		
        private string _transports;
        public string transports
        {
            get { return _transports; }
            set { _transports = value; }
        }

        /// <summary>
        /// curr
        /// </summary>		
        public string curr
        {
            get;set;
        }
    }
}
