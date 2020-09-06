<%@ WebHandler Language="C#" Class="goodsinfoHandler" %>

using System;
using System.Web;
using System.Data;
using BLL;
using Newtonsoft.Json;
using Model;
using System.IO;

public class goodsinfoHandler : IHttpHandler
{
    //infoDetailBLL bll = new infoDetailBLL();
    nimp_mainBLL bll = new nimp_mainBLL();
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
        string obj = "";
        int outint;
        nimp_main main = new nimp_main();
        #region 查询参数
        Req req = new Req(context);

        if (context.Request["transportNos"] != null && context.Request["transportNos"] != "" && context.Request["transportNos"] != "请选择")
        {
            strWhere += " and T.transportNo like '%" + context.Request["transportNos"].Trim().ToString() + "%' ";
        }
        if (context.Request["jobnumbers"] != null && context.Request["jobnumbers"] != "" && context.Request["jobnumbers"] != "请选择")
        {
            strWhere += " and jobnumber like '%" + context.Request["jobnumbers"].Trim().ToString() + "%' ";
        }
        if (context.Request["bondInvtNos"] != null && context.Request["bondInvtNos"] != "" && context.Request["bondInvtNos"] != "请选择")
        {
            strWhere += " and bondInvtNo like '%" + context.Request["bondInvtNos"].Trim().ToString() + "%' ";
        }
        if (context.Request["agentNames"] != null && context.Request["agentNames"] != "" && context.Request["agentNames"] != "请选择")
        {
            strWhere += " and agentName like '%" + context.Request["agentNames"].Trim().ToString() + "%' ";
        }
        if (context.Request["entryNos"] != null && context.Request["entryNos"] != "" && context.Request["entryNos"] != "请选择")
        {
            strWhere += " and entryNo like '%" + context.Request["entryNos"].Trim().ToString() + "%' ";
        }
        if (context.Request["BL_Nos"] != null && context.Request["BL_Nos"] != "" && context.Request["BL_Nos"] != "请选择")
        {
            strWhere += " and BL_No like '%" + context.Request["BL_Nos"].Trim().ToString() + "%' ";
        }
        if (context.Request["customNOs"] != null && context.Request["customNOs"] != "" && context.Request["customNOs"] != "请选择")
        {
            strWhere += " and customNO like '%" + context.Request["customNOs"].Trim().ToString() + "%' ";
        }
        if (context.Request["invoiceNos"] != null && context.Request["invoiceNos"] != "" && context.Request["invoiceNos"] != "请选择")
        {
            strWhere += " and invoiceNo like '%" + context.Request["invoiceNos"].Trim().ToString() + "%' ";
        }
        if (context.Request["ordernos"] != null && context.Request["ordernos"] != "" && context.Request["ordernos"] != "请选择")
        {
            strWhere += " and orderno like '%" + context.Request["ordernos"].Trim().ToString() + "%' ";
        }


        req.beginIndex = (req.pageIndex - 1) * req.pageSize + 1;
        req.endIndex = req.pageIndex * req.pageSize;
        string ope = context.Request["ope"].ToString();
        #endregion
        switch (req.ope)
        {
            case "getListDetail":
                {
                    var idBll = new BLL.infoDetailBLL();
                    count = idBll.GetModelList(strWhere).Count;
                    //dt = bll.GetListByPageNew(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];

                    dt = idBll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];

                    data = JsonConvert.SerializeObject(dt);
                    result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                    break;
                }
            case "getList":
                count = bll.GetModelList(strWhere).Count;
                //dt = bll.GetListByPageNew(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];

                dt = bll.GetListByPage(strWhere, req.filedOrder, req.beginIndex, req.endIndex).Tables[0];

                data = JsonConvert.SerializeObject(dt);
                result = "{\"count\": " + count + ",\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"total\": " + count + ",\"data\": " + data + "}";
                break;


            case "save_detail":

                if (PubConstant.YongHu_uid == "")
                {
                    result = "{\"count\": 0 , \"msg\": \"您已经退出了\"}";
                    break;
                }
                var uid1 = Convert.ToInt32(PubConstant.YongHu_uid);

                var user1 = new hr_empinfoBLL().GetModelNew(uid1);
                //user.inDep = user.inDep ?? "";
                user1.inpep = user1.inpep ?? "";
                obj = context.Request["obj"].ToString();

                var detail = JsonConvert.DeserializeObject<infoDetail>(obj);

                detail.InDate = DateTime.Now;
                detail.inDep = PubConstant.BuMen;
                detail.InPep = user1.inpep;
                var detailBll = new BLL.infoDetailBLL();
                if (detail.id > 0)
                {
                    count = detailBll.Update(detail) ? 1 : 0;

                }
                else
                {
                    count = detailBll.Add(detail);
                }
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
                break;
            case "save":

                if (PubConstant.YongHu_uid == "")
                {
                    result = "{\"count\": 0 , \"msg\": \"您已经退出了\"}";
                    break;
                }
                var uid = Convert.ToInt32(PubConstant.YongHu_uid);

                var user = new hr_empinfoBLL().GetModelNew(uid);
                //user.inDep = user.inDep ?? "";
                user.inpep = user.inpep ?? "";
                obj = context.Request["obj"].ToString();

                var nimp_main = JsonConvert.DeserializeObject<nimp_main>(obj);

                nimp_main.inDep = PubConstant.BuMen;
                nimp_main.indate = DateTime.Now;
                nimp_main.inpep = user.inpep;

                if (nimp_main.id > 0)
                {
                    count = bll.Update(nimp_main) ? 1 : 0;

                }
                else
                {
                    //创建新的业务流水号
                    nimp_main.jobnumber = CreateNo();
                    count = bll.Add(nimp_main);
                }
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\",\"jobnumber\":\"" + nimp_main.jobnumber + "\"}";
                break;
            case "del":
                if (req.ids != null && req.ids != "")
                {
                    count = bll.DeleteList(req.ids) ? 1 : 0;
                }
                result = "{\"count\": " + count + ", \"msg\": \"" + msg + "\"}";
                break;
            case "detail_delete":
                count = 0;
                int id = 0;
                if (context.Request["id"] != null)
                {
                    id = Convert.ToInt32(context.Request["id"]);
                    count = new infoDetailBLL().Delete(id) ? 1 : 0;
                }
                result = "{\"count\": " + count + ",\"msg\":\"" + error + "\"}";
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
                        //dt = Upload.GetTableByFile(path, newname, out msg);

                        var list = Upload.GetInfoFromXls(path + newname);
                        if (list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                item.InDate = DateTime.Now;
                                item.InPep = PubConstant.YongHu;
                                item.inDep = PubConstant.BuMen;

                                if (new infoDetailBLL().GetModelList("HSCode=" + item.HSCode).Count > 0)
                                {
                                    sb.Append(item.HSCode + ","); //商品编码已经存在, 不可重复!
                                    continue;
                                }
                                var singleResult = new infoDetailBLL().Add(item);
                                if (singleResult > 0)
                                {
                                    count += 1;
                                }

                            }

                            File.AppendAllText("D:\\id.txt", sb.ToString());
                            error = "上传成功" + count + "条";
                            data = "\"\"";
                            result = "{\"count\": " + count + ",\"code\": " + (count > 0 ? "0" : "1") + ",\"msg\": \"" + error + "\", \"data\": " + data + "}";
                        }
                        else
                        {
                            error = "没有数据请检查文件";
                            result = "{\"count\": " + count + ",\"code\": " + (count > 0 ? "0" : "1") + ",\"msg\": \"" + error + "\", \"data\": " + data + "}";
                        }

                    }
                    else
                    {
                        msg = "上传失败！";
                        result = "{\"count\": " + count + ",\"code\": " + (count > 0 ? "0" : "1") + ",\"msg\": \"" + msg + "\"}";
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
                    var goodssets = new goodsSetBLL().GetModelList("");
                    foreach (var x in goodssets)
                    {
                        _data.Add(new { id = x.id, text = x.Origin });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";



                }
                break;
            case "yunshugongju":
                {
                    //var _data = new System.Collections.Generic.List<object>();
                    //var nimpmain = bll.GetModelList("");
                    //foreach (var x in nimpmain)
                    //{
                    //    _data.Add(new { id = x.id, text = x.transport });
                    //}
                    //data = JsonConvert.SerializeObject(_data);
                    //result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";

                    var _data = new System.Collections.Generic.List<object>();
                    var nimpmain = new BLL.p_parameterDetailBLL().GetModelList("TypeID=3");
                    foreach (var x in nimpmain)
                    {
                        _data.Add(new { id = x.id, text = x.Details });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";


                    //var _data = new System.Collections.Generic.List<object>();
                    //var nimpmain = new p_parameterMainBLL().GetModelList("");
                    //foreach (var x in nimpmain)
                    //{
                    //    _data.Add(new { id = x.id, text = x.transports });
                    //}
                    //data = JsonConvert.SerializeObject(_data);
                    //result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";


                }
                break;
            case "huowuxinghao":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var goodsset = new goodsSetBLL().GetModelList("");
                    foreach (var x in goodsset)
                    {
                        _data.Add(new { id = x.id, text = x.SKU });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
                }
                break;
            case "bizhong":
                {

                    //var _data = new System.Collections.Generic.List<object>();
                    //var nimpmain = new BLL.p_parameterMainBLL().GetModelList("");
                    //foreach (var x in nimpmain)
                    //{
                    //    _data.Add(new { id = x.id, text = x.curr });
                    //}
                    //data = JsonConvert.SerializeObject(_data);
                    //result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
                    var _data = new System.Collections.Generic.List<object>();
                    var nimpmain = new BLL.p_parameterDetailBLL().GetModelList("TypeID=5");
                    foreach (var x in nimpmain)
                    {
                        _data.Add(new { id = x.id, text = x.Details });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
                }
                break;
            case "huopingzhuangtai":
                {
                    var _data = new System.Collections.Generic.List<object>();
                    var statu = bll.GetModelList("");
                    foreach (var x in statu)
                    {
                        _data.Add(new { id = x.id, text = x.status });
                    }
                    data = JsonConvert.SerializeObject(_data);
                    result = "{\"code\": " + 0 + ",\"msg\": \"" + msg + "\",\"data\": " + data + "}";
                }
                break;

        }
        context.Response.Write(result);
    }


    public string CreateNo()
    {
        var bll = new nimp_mainBLL();
        var tb = bll.GetList(1, "", "id desc").Tables[0];
        var jobnumber = tb.Rows.Count > 0 ? Convert.ToString(tb.Rows[0]["jobnumber"]) : "";
        if (string.IsNullOrWhiteSpace(jobnumber))
        {
            return DateTime.Now.ToString("yyyyMMdd001"); //初始化数据
        }
        var date = jobnumber.Substring(0, 8);
        var number = Convert.ToInt32(jobnumber.Substring(8));

        var now = DateTime.Now.ToString("yyyyMMdd");
        if (now == date)
        {
            var n = number + 1;
            var nstr = n.ToString();
            if (n < 10)
            {
                nstr = "00" + n;
            }
            else if (n < 100)
            {
                nstr = "0" + n;
            }
            return string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), nstr);
        }
        return DateTime.Now.ToString("yyyyMMdd001"); //新的一天

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}