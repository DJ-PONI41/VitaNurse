using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPaciente.aspx");
        }
        protected void btn_info_click(object sender, EventArgs e)
        {
            Response.Redirect("Service.aspx");
        }
    }
}