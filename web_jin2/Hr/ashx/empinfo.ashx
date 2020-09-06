<%@ WebHandler Language="C#" Class="empinfo" %>

using System;
using System.Web;
using Newtonsoft.Json;
using Model;
using System.Data;
using System.IO;
using System.Data.OleDb;
using BLL;
public class empinfo : IHttpHandler {
    hr_empinfoBLL bll = new hr_empinfoBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string result = "";
        DataTable dt = new DataTable();
        string data = "";
        int count = 0;
        string msg = "";
        string error = "";
        string strWhere = " ";
        int outint;
        #region 查询参数
        Req req = new Req(context);

        if (context.Request["Name"] != null && context.Request["Name"] != "")
        {
            strWhere += " and Name = '" + context.Request["Name"].Trim().ToString() + "' ";
        }
       
        req.beginIndex = (req.pageIndex - 1) * req.pageSize + 1;
        req.endIndex = req.pageIndex * req.pageSize;
        #endregion
        switch (req.ope)
        {
            case "getList":
                count = bll.GetRecordCount(strWhere);
                dt = bll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];
                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;
            case "getModel":
                dt = bll.GetModelTable(req.id);
                if (dt.Rows.Count>0)
                {
                  
                    data = JsonConvert.SerializeObject(dt);
                    count = dt.Rows.Count;
                    result = "{\"count\": " + count + ",\"dt\": " + data + "}";

                }
                else
                {
                    count = 0;
                    error = "参数不正确";
                    result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                }
                break;
            case "save":
                hr_empinfoModel main = new hr_empinfoModel();


                if (context.Request["USname"] != null)
                {
                    main.USname = context.Request["USname"].ToString();
                }
                if (context.Request["Name"] != null)
                {
                    main.name = context.Request["Name"].ToString();
                }
                if (context.Request["sex"] != null)
                {
                    main.sex = context.Request["sex"].ToString();
                }
                if (context.Request["password"] != null)
                {
                    main.password =bll.MD5Encrypt64(context.Request["password"].ToString());
                }
                if (context.Request["tel"] != null)
                {
                    main.tel = context.Request["tel"].ToString();
                }
                if (context.Request["email"] != null)
                {
                    main.email = context.Request["email"].ToString();
                }
                if (context.Request["Address"] != null)
                {
                    main.Address = context.Request["Address"].ToString();
                }
                if (req.id > 0)
                {
                    main.id = req.id;
                }

                if (!bll.ExistsAddUpdate(main.USname,main.name, main.id.ToString()))
                {
                    int newid = 0;
                    if (main.id > 0)
                    {
                        count = bll.Update(main) ? 1 : 0;
                        newid = main.id;
                    }
                    else
                    {
                        main.in_service = true;
                        newid = bll.Add(main);
                        count = newid;
                        
                    }
                }
                else
                {
                    count = 0;
                    error = "系统用户名、员工姓名不允许重复";
                }

                result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                break;
            case "del":
                count = 0;
                string codeId = "";
                if (context.Request["codeId"] != null)
                {
                    codeId = context.Request["codeId"].ToString();
                    count = bll.DeleteList(codeId) ? 1 : 0;
                }

                result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                break;
            case "change":

                string OldPWD = "";
                string NewPWQ1 = "";
                string NewPWQ2 = "";
                
                if (context.Request["OldPWD"] != null)
                {
                    OldPWD = context.Request["OldPWD"].ToString();
                }
                if (context.Request["NewPWQ1"] != null)
                {
                    NewPWQ1 = context.Request["NewPWQ1"].ToString();
                }
                if (context.Request["NewPWQ2"] != null)
                {
                    NewPWQ2 = context.Request["NewPWQ2"].ToString();
                }
                DataTable dt1 = bll.Get_list(PubConstant.YongHu, OldPWD);
                if(dt1.Rows.Count>0)
                {

                    count = bll.changePWD(PubConstant.YongHu, NewPWQ1) ? 1 : 0;

                }
                else
                {

                    count = 0;
                    error = "原密码错误";
                }

               

                result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
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