<%@ WebHandler Language="C#" Class="p_parameterHandler" %>

using System;
using System.Web;
using System.Data;
using BLL;
using Newtonsoft.Json;
using Model;

public class p_parameterHandler : IHttpHandler
{
    p_parameterDetailBLL bll = new p_parameterDetailBLL();
    p_parameterMainBLL bm = new p_parameterMainBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string result = "";
        DataTable dt = new DataTable();
        string data = "";
        int count = 0;
        string obj = "";
        string msg = "";
        string error = "";
        string strWhere = " 1 = 1 ";
        int outint;
        #region 查询参数
        Req req = new Req(context);
        p_parameterDetail main = new p_parameterDetail();
        if (context.Request["TypeID"] != null && context.Request["TypeID"] != "")
        {
            strWhere += " and TypeID like '%" + context.Request["TypeID"].Trim().ToString() + "%' ";
        }


        req.beginIndex = (req.pageIndex - 1) * req.pageSize + 1;
        req.endIndex = req.pageIndex * req.pageSize;
        #endregion
        switch (req.ope)
        {
            case "getMainList":
                dt = bm.GetList("").Tables[0];
                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;
            case "getList":
                count = bll.GetModelList(strWhere).Count;
                dt = bll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];
                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;
            case "save":
                // string paramsJson = context.Request["obj"].ToString();
                obj = context.Request["obj"].ToString();
                // p_parameterDetail parameterDetail = JsonConvert.DeserializeObject<p_parameterDetail>(paramsJson);
                main = JsonConvert.DeserializeObject<p_parameterDetail>(obj);
                //parameterDetail.inPep = PubConstant.YongHu;
                if (main.id > 0)
                {
                    var detail = bll.GetModel(main.Details, "id!=" + main.id);
                    if (detail != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"参数名称不能重复\"}";
                    }
                    count = bll.Update(main) ? 1 : 0;
                }
                else
                {
                    var detail = bll.GetModel(main.Details, "");
                    if (detail != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"参数名称不能重复\"}";
                    }
                    count=bll.Add(main);

                }
                //data = JsonConvert.SerializeObject(main);
                result = "{\"count\": " + count + ",\"msg\":\"" + error + "\", \"data\": " + data + "}";
                break;
            case "del":
                count = 0;
                int id = 0;
                if (context.Request["id"] != null)
                {
                    id = Convert.ToInt32(context.Request["id"]);
                    count = bll.Delete(id) ? 1 : 0;
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