<%@ WebHandler Language="C#" Class="loginHandler" %>

using System;
using System.Web;
using BLL;
using System.Data;
using Newtonsoft.Json;
using Model;
public class loginHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {

        hr_empinfoBLL bll = new hr_empinfoBLL();

        CompanyListParamBLL cpbll = new CompanyListParamBLL();
        context.Response.ContentType = "text/plain";
        string ope = "";
        string result = "";
        int count;
        string error = "";
        DataTable dt = new DataTable();
        Req req = new Req(context);
        if (context.Request["ope"] != null)
        {
            ope = context.Request["ope"].ToString();
        }

        switch (req.ope)
        {

            case "CheckIn":
                //8888// z3muat26YK0Bg0c1m9FE0g==
                //5tgb// B1OQ5mFeAQdxNZyc1vq9IQ==
                //hIBhVg9U0moEY5Hq6TQZvA==
                string UserName = context.Request["UserName"];
                string Password = context.Request["Password"];
                CompanyListParamModel cp = cpbll.GetModel(PubConstant.sqlname);
                if (context.Request.Url.ToString().IndexOf("localhost") < 0 && HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() != "114.84.50.84" && cp.ValidityDate < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")))
                {
                    count = 0;
                    error = "用户名或者密码不正确!";
                    result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                    break;
                }
                dt = bll.Get_list(UserName, Password);
                if (dt == null || dt.Rows.Count == 0)
                {
                    count = 0;
                    error = "用户名或者密码不正确!";
                    result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                }
                else
                {


                    string employeeName = dt.Rows[0]["Name"].ToString();
                    string sysUser = dt.Rows[0]["USname"].ToString();
                    //string BuMen = dt.Rows[0]["inDep"].ToString();
                    string uid = dt.Rows[0]["id"].ToString();
                    context.Response.Cookies["wms20_wms_uid"].Value = Descrypt.Encrypt(uid);
                    context.Response.Cookies["wms20_wms_uid"].Expires = DateTime.Now.AddDays(1);
                    context.Response.Cookies["wms20_yonghu"].Value = Descrypt.Encrypt(employeeName);
                    context.Response.Cookies["wms20_yonghu"].Expires = DateTime.Now.AddDays(1);
                    //context.Response.Cookies["wms20_BuMen"].Value = Descrypt.Encrypt(BuMen);
                    //context.Response.Cookies["wms20_BuMen"].Expires = DateTime.Now.AddDays(1);

                    count = 1;
                    result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                }
                break;
        }
        context.Response.Write(result);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}