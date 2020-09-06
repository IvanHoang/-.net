using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class hr_empinfoModel
    {
        #region Model
        private int _id;
        private string _name;
        private string _usname;
        private string _password;
        private string _sex;
        private string _tel;
        private string _email;
        private bool _in_service = true;
        private string _address;
        private string _inpep;
        private DateTime _indate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USname
        {
            set { _usname = value; }
            get { return _usname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
   
        /// <summary>
        /// 
        /// </summary>
        public bool in_service
        {
            set { _in_service = value; }
            get { return _in_service; }
        }
    
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string inpep
        {
            set { _inpep = value; }
            get { return _inpep; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string inDep
        {
            get;set;
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime indate
        {
            set { _indate = value; }
            get { return _indate; }
        }
        #endregion Model
    }
}
