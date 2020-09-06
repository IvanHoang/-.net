<%@ WebHandler Language="C#" Class="goodsSetHandler" %>

using System;
using System.Web;
using System.Data;
using BLL;
using Newtonsoft.Json;
using Model;
using System.IO;

public class goodsSetHandler : IHttpHandler
{
    goodsSetBLL bll = new goodsSetBLL();
    nimp_agentBLL bllnimp = new nimp_agentBLL();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string result = "";
        DataTable dt = new DataTable();
        string data = "";
        int count = 0;
        string msg = "";
        string error = "";
        string strWhere = " 1 = 1 ";
        string obj = "";
        #region 查询参数
        Req req = new Req(context);
        goodsSet main = new goodsSet();
        if (context.Request["AgentNames"] != null && context.Request["AgentNames"] != "" && context.Request["AgentNames"] != "请选择")
        {
            strWhere += " and AgentName like '%" + context.Request["AgentNames"].Trim().ToString() + "%' ";
        }
        if (context.Request["goodsName"] != null && context.Request["goodsName"] != "" && context.Request["goodsName"] != "请选择")
        {
            strWhere += " and goodsName like '%" + context.Request["goodsName"].Trim().ToString() + "%' ";
        }
        if (context.Request["SKUs"] != null && context.Request["SKUs"] != "" && context.Request["SKUs"] != "请选择")
        {
            strWhere += " and SKU like '%" + context.Request["SKUs"].Trim().ToString() + "%' ";
        }


        req.beginIndex = (req.pageIndex - 1) * req.pageSize + 1;
        req.endIndex = req.pageIndex * req.pageSize;
        #endregion
        switch (req.ope)
        {
            case "getList":
                count = bll.GetModelList(strWhere).Count;
                dt = bll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];
                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;
            case "save":
                obj = context.Request["obj"].ToString();
                main = JsonConvert.DeserializeObject<goodsSet>(obj);
                if (main.id > 0)
                {
                    var sku = bll.GetModel(main.SKU, "id!=" + main.id);
                    if (sku != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"货物型号不能重复\"}";
                    }
                    count = bll.Update(main) ? 1 : 0;
                }
                else
                {
                    var sku = bll.GetModel(main.SKU, "");
                    if (sku != null)
                    {
                        result = "{\"count\": 0, \"msg\": \"货物型号不能重复\"}";
                    }
                    count = bll.Add(main);
                }
                //data = JsonConvert.SerializeObject(model);
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
                break;
            case "del":
                if (req.ids != null && req.ids != "")
                {
                    count = bll.DeleteList(req.ids) ? 1 : 0;
                }
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
                break;
            case "upGoods":
                if (context.Request.Files.Count > 0)
                {
                    string filename = context.Request.Files[0].FileName;
                    string name = filename.Substring(0, filename.LastIndexOf('.'));
                    string houzhui = filename.Substring(filename.LastIndexOf('.'), filename.Length - filename.LastIndexOf('.'));
                    string nowPath = System.Web.HttpContext.Current.Server.MapPath(".");
                    string path = nowPath.Substring(0, nowPath.LastIndexOf("\\"));
                    path = path.Substring(0, path.LastIndexOf("\\"));
                    path += "\\Files\\Up\\";
                    string newname = "Googset" + Guid.NewGuid().ToString().Replace("-", "") + houzhui;

                    Directory.CreateDirectory(path);
                    context.Request.Files[0].SaveAs(path + newname);
                    count = File.Exists(path + newname) ? 1 : 0;
                    if (count > 0)
                    {
                        count = 0;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("\t\t\t\t\r\r\b\t******************************************");
                        dt = Upload.GetTableByFile(path, newname, out msg);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var model = new goodsSet();
                            var singleResult = 0;

                            model.InPep = PubConstant.YongHu;
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row["F1"].ToString() == "HSCode")
                                {
                                    continue;
                                }

                                model.InDate = DateTime.Now;
                                model.HSCode = row["F1"].ToString();
                                model.AgentName = row["F2"].ToString();
                                model.SKU = row["F3"].ToString();
                                model.GoodsName = row["F4"].ToString();
                                model.gdsSpcfModelDesc = row["F5"].ToString();
                                model.dclUnitcd = row["F6"].ToString();
                                model.lawfUnitcd = row["F7"].ToString();
                                model.Origin = row["F8"].ToString();
                                model.department = row["F9"].ToString();
                                model.Volume = Convert.ToInt32(row["F10"]);
                                model.netWt = Convert.ToInt32(row["F11"]);


                                singleResult = bll.Add(model);
                                if (singleResult < 0)
                                {
                                    //error = "商品编码已经存在, 不可重复!";
                                    sb.Append(model.HSCode + ",");
                                    //break;
                                }
                                count += singleResult;
                            }
                        }
                        File.AppendAllText("D:\\id.txt", sb.ToString());
                        if (msg != null)
                        {
                            msg = msg.Replace("\n", "");
                        }
                        data = "\"\"";
                        result = "{\"count\": " + count + ",\"code\": " + (count > 0 ? "0" : "1") + ",\"msg\": \"" + error + "\", \"data\": " + data + "}";
                    }
                    else
                    {
                        msg = "上传失败！";
                        result = "{\"count\": " + count + ",\"code\": " + (count > 0 ? "0" : "1") + ",\"msg\": \"" + error + "\"}";
                    }

                }
                else
                {
                    count = 0;
                    error = "请先选择一个文件";
                    result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
                }

                break;


            case "kehu":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var agents = new nimp_agentBLL().GetModelList("");
                    foreach (var x in agents)
                    {
                        _data.Add(new { id = x.id, text = x.AgentName });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
                }
                break;

            case "yuanchanguo":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var goodssets = bll.GetModelList("");
                    foreach (var x in goodssets)
                    {
                        _data.Add(new { id = x.id, text = x.Origin });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";

                }
                break;

            case "fadingdanwei":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var nimpmain = new p_parameterDetailBLL().GetModelList("TypeID=2");
                    foreach (var x in nimpmain)
                    {
                        _data.Add(new { id = x.id, text = x.Details });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";

                }
                break;
            case "shenbaodanwei":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var nimpmain = new p_parameterDetailBLL().GetModelList("TypeID=1");
                    foreach (var x in nimpmain)
                    {
                        _data.Add(new { id = x.id, text = x.Details });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
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