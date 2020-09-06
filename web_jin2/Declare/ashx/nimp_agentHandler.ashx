<%@ WebHandler Language="C#" Class="nimp_agentHandler" %>

using System;
using System.Web;
using System.Data;
using BLL;
using Newtonsoft.Json;
using Model;

public class nimp_agentHandler : IHttpHandler
{
    nimp_agentBLL bll = new nimp_agentBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string result = "";
        System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder(" 1 = 1");
        DataTable dt = new DataTable();
        string data = "";
        int count = 0;
        string msg = "";
        string error = "";
        string strWhere = " 1 = 1 ";
        int outint;
        string obj = "";
        #region 查询参数
        Req req = new Req(context);
        nimp_agent main = new nimp_agent();
        if (context.Request["AgentNames"] != null && context.Request["AgentNames"] != "" && context.Request["AgentNames"] != "请选择")
        {
            strWhere += " and AgentName like '%" + context.Request["AgentNames"].Trim().ToString() + "%' ";
        }
        if (context.Request["agentAllNames"] != null && context.Request["agentAllNames"] != "" && context.Request["agentAllNames"] != "请选择")
        {
            strWhere += " and agentAllName like '%" + context.Request["agentAllNames"].Trim().ToString() + "%' ";
        }

        req.beginIndex = (req.pageIndex - 1) * req.pageSize + 1;
        req.endIndex = req.pageIndex * req.pageSize;

        string ope = context.Request["ope"].ToString();
        #endregion
        switch (ope)
        {
            case "getList":
                strWhere += " and (isdel=0 or isdel is null)";
                count = bll.GetModelList(strWhere).Count;
                dt = bll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];
                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;
            case "save":
                obj = context.Request["obj"].ToString();
                main = JsonConvert.DeserializeObject<nimp_agent>(obj);
                if (main.id > 0)
                {
                    var agent = bll.GetModel(main.agentAllName, main.AgentName, "id!=" + main.id);
                    if (agent != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"客户名称或客户简称不能重复\"}";
                    }
                    count = bll.Update(main) ? 1 : 0;
                }
                else
                {
                    var agent = bll.GetModel(main.agentAllName, main.AgentName, "");
                    if (agent != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"客户名称或客户简称不能重复\"}";
                    }
                    count = bll.Add(main);
                }
                //data = JsonConvert.SerializeObject(model);
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
                break;
            case "del":
                if (req.ids != null && req.ids != "")
                {
                    count = bll.ExecuteSql("update nimp_agent set isdel=-1 where id in (" + req.ids + ")");
                }
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
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