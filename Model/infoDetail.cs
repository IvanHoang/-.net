using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class infoDetail
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id{get;set;}
        /// <summary>
        /// 业务流水号,IYYMMDD001（自动生成）
        /// </summary>		
        public string jobnumber { get; set; }
        /// <summary>
        /// 备案序号
        /// </summary>		
        public int putrecSeqno  { get; set; }
        /// <summary>
        /// 货物型号
        /// </summary>		
        public string SKU { get; set; }
        /// <summary>
        /// 品名
        /// </summary>		
        public string GoodsName { get; set; }
        /// <summary>
        /// HSCode
        /// </summary>		
        public string HSCode { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>		
        public string gdsSpcfModelDesc { get; set; }
        /// <summary>
        /// 申报数量
        /// </summary>		
        public decimal? dcl_QTY { get; set; }
        /// <summary>
        /// 申报单位ddl
        /// </summary>		
        public string dclUnitcd { get; set; }
        /// <summary>
        /// 法定数量
        /// </summary>		
        public decimal? law_QTY { get; set; }
        /// <summary>
        /// 法定单位ddl
        /// </summary>		
        public string lawfUnitcd { get; set; }
        /// <summary>
        /// 立方
        /// </summary>		
        public decimal? Volume { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>		
        public decimal? grossWt { get; set; }
        /// <summary>
        /// 净重
        /// </summary>		
        public decimal? netWt { get; set; }
        /// <summary>
        /// 原产国ddl
        /// </summary>		
        public string Origin { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>		
        public DateTime InDate { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>		
        public string InPep { get; set; }
        /// <summary>
        /// 部门
        /// </summary>		
        public string inDep { get; set; }
        /// <summary>
        /// 批号
        /// </summary>		
        public string batch { get; set; }
        /// <summary>
        /// 库位
        /// </summary>		
        public string Location { get; set; }
        /// <summary>
        /// 单价
        /// </summary>		                       
        public decimal? unitprice { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>		
        public decimal? totalamount { get; set; }
        /// <summary>
        /// 币种
        /// </summary>		
        public string curr { get; set; }
        /// <summary>
        /// 货物状态
        /// </summary>		
        public string goodsStatus { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>		
        public string LPN { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>		
        public DateTime productionDate { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>		
        public DateTime validDate { get; set; }


    }
}
