using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Nurse_home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                // El usuario no ha iniciado sesión, redirigir a la página de inicio de sesión
                Response.Redirect("Login.aspx");
            }
            else
            {
                // El usuario ha iniciado sesión, verificar el rol
                string userRole = Session["UserRole"].ToString();

                // Verificar si el usuario tiene el rol adecuado para acceder a esta ventana
                if (userRole != "Enfermera")
                {
                    // El usuario no tiene el rol adecuado, mostrar un mensaje de error o redirigir a una página de acceso no autorizado
                    Response.Write("Acceso no autorizado. Debes tener el rol de Enfermera para acceder a esta página.");
                    // También puedes redirigir a una página de acceso no autorizado en lugar de mostrar un mensaje aquí.
                }
            }

        }
    }
}