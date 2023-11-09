using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Cierra la sesión del usuario y lo redirige a la página de inicio.
            Session.Clear();
            Session.Abandon();

            HttpCookie httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            httpCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(httpCookie);

            Response.Redirect("Home.aspx");
        }
    }
}