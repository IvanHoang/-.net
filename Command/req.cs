using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public class Req
{
    public int outint = 0;
    public decimal outdec = 0M;
    public DateTime outtime = DateTime.Now;

    public Req(HttpContext context)
    {
        if (context.Request["ope"] != null)
        {
            ope = context.Request["ope"].ToString();
        }
        if (context.Request["val"] != null && context.Request["val"].ToString() != "")
        {
            val = context.Request["val"].ToString();
        }
        if (context.Request["val"] != null && int.TryParse(context.Request["val"].ToString(), out outint))
        {
            val_int = Convert.ToInt32(context.Request["val"].ToString());
        }
        if (context.Request["val"] != null && decimal.TryParse(context.Request["val"].ToString(), out outdec))
        {
            val_dec = Convert.ToDecimal(context.Request["val"].ToString());
        }
        if (context.Request["colname"] != null && context.Request["colname"].ToString() != "")
        {
            colname = context.Request["colname"].ToString();
        }
        if (context.Request["pageIndex"] != null && context.Request["pageIndex"].ToString() != "")
        {
            pageIndex = int.Parse(context.Request["pageIndex"].ToString());
        }
        if (context.Request["pageSize"] != null && context.Request["pageSize"].ToString() != "")
        {
            pageSize = int.Parse(context.Request["pageSize"].ToString());
        }
        if (context.Request["id"] != null && int.TryParse(context.Request["id"].ToString(), out outint))
        {
            id = Convert.ToInt32(context.Request["id"].ToString());
        }
        if (context.Request["mid"] != null && int.TryParse(context.Request["mid"].ToString(), out outint))
        {
            mid = Convert.ToInt32(context.Request["mid"].ToString());
        }
        if (context.Request["did"] != null && int.TryParse(context.Request["did"].ToString(), out outint))
        {
            did = Convert.ToInt32(context.Request["did"].ToString());
        }
        if (context.Request["orderby"] != null && context.Request["orderby"].ToString() != "")
        {
            orderby = context.Request["orderby"].ToString();
            if (context.Request["paixu"] != null && context.Request["paixu"].ToString() != "")
            {
                filedOrder = orderby + " " + context.Request["paixu"].ToString();
            }
            else
            {
                filedOrder = orderby + " desc";
            }
        }
        if (context.Request["state"] != null && context.Request["state"].ToString() != "")
        {
            state = context.Request["state"].ToString();
        }
        if (context.Request["jsonStr"] != null && context.Request["jsonStr"].ToString() != "")
        {
            jsonStr = context.Request["jsonStr"].ToString();
        }
        if (context.Request["date1"] != null && DateTime.TryParse(context.Request["date1"].ToString(), out outtime))
        {
            date1 = Convert.ToDateTime(context.Request["date1"].ToString());
        }
        if (context.Request["date2"] != null && DateTime.TryParse(context.Request["date2"].ToString(), out outtime))
        {
            date2 = Convert.ToDateTime(context.Request["date2"].ToString());
        }
        if (context.Request["ids"] != null && context.Request["ids"].ToString() != "")
        {
            ids = context.Request["ids"].ToString();
            if (ids.Length > 0 && ids[0] == ',')
            {
                ids = ids.Substring(1, ids.Length - 1);
            }
            if (ids.Length > 0 && ids[ids.Length - 1] == ',')
            {
                ids = ids.Substring(0, ids.Length - 1);
            }
        }
        if (context.Request["directionTypecd"] != null && context.Request["directionTypecd"].ToString() != "")
        {
            directionTypecd = context.Request["directionTypecd"].ToString();
        }
        if (context.Request["optStatus"] != null && context.Request["optStatus"].ToString() != "")
        {
            optStatus = context.Request["optStatus"].ToString();
        }
        if (context.Request["bondInvtNo"] != null && context.Request["bondInvtNo"].ToString() != "")
        {
            bondInvtNo = context.Request["bondInvtNo"].ToString();
        }
        if (context.Request["invtPreentNo"] != null && context.Request["invtPreentNo"].ToString() != "")
        {
            invtPreentNo = context.Request["invtPreentNo"].ToString();
        }
        if (context.Request["putrecNo"] != null && context.Request["putrecNo"].ToString() != "")
        {
            putrecNo = context.Request["putrecNo"].ToString();
        }
        if (context.Request["etpsInnerInvtNo"] != null && context.Request["etpsInnerInvtNo"].ToString() != "")
        {
            etpsInnerInvtNo = context.Request["etpsInnerInvtNo"].ToString();
        }
        if (context.Request["impexpMarkcd"] != null && context.Request["impexpMarkcd"].ToString() != "")
        {
            impexpMarkcd = context.Request["impexpMarkcd"].ToString();
        }
        if (context.Request["beginDate"] != null && DateTime.TryParse(context.Request["beginDate"].ToString(), out outtime))
        {
            beginDate = Convert.ToDateTime(context.Request["beginDate"].ToString());
            beginDateStr = Convert.ToDateTime(context.Request["beginDate"].ToString()).ToString("yyyy-MM-dd");
        }
        if (context.Request["endDate"] != null && DateTime.TryParse(context.Request["endDate"].ToString(), out outtime))
        {
            endDate = Convert.ToDateTime(context.Request["endDate"].ToString());
            endDateStr = Convert.ToDateTime(context.Request["endDate"].ToString()).ToString("yyyy-MM-dd");
        }
        if (context.Request["page"] != null && int.TryParse(context.Request["page"].ToString(), out outint))
        {
            page = Convert.ToInt32(context.Request["page"].ToString());
        }
        if (context.Request["rows"] != null && int.TryParse(context.Request["rows"].ToString(), out outint))
        {
            rows = Convert.ToInt32(context.Request["rows"].ToString());
        }
        if (context.Request["passportNo"] != null && context.Request["passportNo"].ToString() != "")
        {
            passportNo = context.Request["passportNo"].ToString();
        }
        if (context.Request["sasPassportPreentNo"] != null && context.Request["sasPassportPreentNo"].ToString() != "")
        {
            sasPassportPreentNo = context.Request["sasPassportPreentNo"].ToString();
        }
        if (context.Request["sbDate1"] != null && DateTime.TryParse(context.Request["sbDate1"].ToString(), out outtime))
        {
            sbDate1 = Convert.ToDateTime(context.Request["sbDate1"].ToString());
        }
        if (context.Request["sbDate2"] != null && DateTime.TryParse(context.Request["sbDate2"].ToString(), out outtime))
        {
            sbDate2 = Convert.ToDateTime(context.Request["sbDate2"].ToString());
        }
        if (context.Request["entryGdsSeqno"] != null && int.TryParse(context.Request["entryGdsSeqno"].ToString(), out outint))
        {
            entryGdsSeqno = Convert.ToInt32(context.Request["entryGdsSeqno"].ToString());
        }

    }
    public string val = "";
    public int val_int = 0;
    public decimal val_dec = 0M;
    public string ope = "";
    public string colname = "";
    public int beginIndex = 0;
    public int endIndex = 0;
    public int pageIndex = 0;
    public int pageSize = 10;
    public int id = 0;
    public string ids = "";
    public int mid = 0;
    public int did = 0;
    public string orderby = "";
    public string filedOrder = "";
    public string state = "";
    public string jsonStr = "";
    public DateTime? date1 = null;
    public DateTime? date2 = null;
    public string directionTypecd = "";
    public string optStatus = "";
    public string bondInvtNo = "";
    public string invtPreentNo = "";
    public string putrecNo = "";
    public string etpsInnerInvtNo = "";
    public int entryGdsSeqno = 0;
    public string impexpMarkcd = "";
    public DateTime? beginDate = null;
    public DateTime? endDate = null;
    public string beginDateStr = null;
    public string endDateStr = null;
    public int page = 0;
    public int rows = 10;
    public string passportNo;
    public string sasPassportPreentNo;
    public DateTime? sbDate1;
    public DateTime? sbDate2;

}

