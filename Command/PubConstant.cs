using System;
using System.Configuration;
using System.Web;

public class PubConstant
{
    static int testint = 0;


    /// <summary>
    /// 用户ID
    /// </summary>
    public static string YongHu_uid
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_wms_uid"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_wms_uid"].Value)
                : "";
        }
    }
    
     
    /// <summary>
    /// 用户
    /// </summary>
    public static string YongHu
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_yonghu"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_yonghu"].Value)
                : "";
        }
    }
    /// <summary>
    /// 部门
    /// </summary>
    public static string BuMen
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_BuMen"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_BuMen"].Value)
                : "";
        }
    }



    /// <summary>
    /// 公司全称
    /// </summary>
    public static string companyName
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_companyName"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_companyName"].Value)
                : "";
        }
    }
     
    /// <summary>
    /// 前缀
    /// </summary>
    public static string sql_
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_sql"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_sql"].Value)
                : "";
        }
    }
    /// <summary>
    /// 库名
    /// </summary>
    public static string sqlname
    {
        get
        {
            return HttpContext.Current.Request.Cookies["wms20_sqlname"] != null
                ? Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_sqlname"].Value)
                : "";
        }
    }
     
    public static DateTime? ValidityDate
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["wms20_ValidityDate"] != null)
                return Convert.ToDateTime(Descrypt.Decrypt(HttpContext.Current.Request.Cookies["wms20_ValidityDate"].Value));
            else
                return null;
        }
    }
     
 

}