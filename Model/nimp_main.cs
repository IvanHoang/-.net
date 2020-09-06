using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public partial class nimp_main
    {
    public nimp_main()
    {}
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; } = 0;
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string jobnumber { get; set; }
        /// <summary>
        /// 客户简称
        /// </summary>
        public string agentName { get; set; }  
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string bondInvtNo { get; set; }
        /// <summary>
        /// 报关单号
        /// </summary>
        public string entryNo { get; set; }
        /// <summary>
        /// 运输工具
        /// </summary>
        public string transport { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BL_No { get; set; }
        /// <summary>
        /// 客户自编号
        /// </summary>
        public string customNO { get; set; }
        /// <summary>
        /// 预计入库日期/预计出库日期
        /// </summary>
        public DateTime planDate { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime indate { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string inpep { get; set; }
        /// <summary>
        /// inDep
        /// </summary>
        public string inDep { get; set; }
        /// <summary>
        /// 入库通知:10;上架确认:11；已入库:12.出库通知:20；提货单:21；出库确认:22；已出库：23
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 出入库时间
        /// </summary>
        public DateTime inoutdate { get; set; }
        /// <summary>
        /// 客服中心流水号,不可修改
        /// </summary>
        public string c_jobumber { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string transportNo { get; set; }
        /// <summary>
        /// 订单号 
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }


    }
}
