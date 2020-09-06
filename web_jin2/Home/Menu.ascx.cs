using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (PubConstant.sqlname == null || PubConstant.sqlname == "")
        //{
        //    Response.Redirect(PubConstant.LogURL != "" ? PubConstant.LogURL : "~/Home/Login.aspx");
        //}
        //if (PubConstant.YongHu == null || PubConstant.YongHu == "")
        //{
        //    Response.Redirect(PubConstant.LogURL != "" ? PubConstant.LogURL : "~/Home/Login.aspx");
        //}
        //if (PubConstant.auto == "auto" && (PubConstant.jin2_username == null || PubConstant.jin2_username == ""))
        //{
        //    Response.Redirect(PubConstant.LogURL != "" ? PubConstant.LogURL : "~/Home/Login.aspx");
        //}
        //if (PubConstant.YongHu.ToLower() != "admin" && (PubConstant.ValidityDate == null || PubConstant.ValidityDate < Convert.ToDateTime(Convert.ToDateTime(PubConstant.ValidityDate).ToString("yyyy-MM-dd"))))
        //{
        //    Response.Redirect(PubConstant.LogURL != "" ? PubConstant.LogURL : "~/Home/Login.aspx");
        //}
    }
}