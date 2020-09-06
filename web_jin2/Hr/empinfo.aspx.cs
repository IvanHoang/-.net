using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Hr_empinfo : System.Web.UI.Page
{
    protected string btnUpdate = "";//保存

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // btnUpdate = Command.SetPermissions.ReturnPower("ryqx-rysz-bc", "权限管理", "权限", "权限分配", "人员权限人员设置保存");

        }
    }
}