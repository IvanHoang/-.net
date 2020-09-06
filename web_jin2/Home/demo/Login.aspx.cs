using System;
using System.Web.UI;
using BLL;
using Model;


public partial class home_Login : Page
{
    protected string un = "zhaolong";
    CompanyListParamBLL cpbll = new CompanyListParamBLL();
    hr_empinfoBLL hebll = new hr_empinfoBLL();
    protected string allname = "";
    protected string BuMen = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Response.Cookies["wms20_yonghu"].Value = Descrypt.Encrypt("");
          
            CompanyListParamModel cp = cpbll.GetModel(un);
            //hr_empinfoModel he = hebll.GetModel(1);
            if (cp != null)
            {
                allname = cp.companyAllName;
                //BuMen = he.inDep;
             
                Response.Cookies["wms20_sqlname"].Value = Descrypt.Encrypt(un);
                Response.Cookies["wms20_sqlname"].Expires = DateTime.Now.AddDays(1);
                Response.Cookies["wms20_companyName"].Value = Descrypt.Encrypt(allname);
                Response.Cookies["wms20_companyName"].Expires = DateTime.Now.AddDays(1);
                //Response.Cookies["wms20_BuMen"].Value = Descrypt.Encrypt(BuMen);
                //Response.Cookies["wms20_BuMen"].Expires = DateTime.Now.AddDays(1);


            }
        }
    }
}