using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NurseProjecDAO.Implementacion;
namespace NurseProjectWEB
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                UserImpl implUser = new UserImpl();
                DataTable table = implUser.Login(txtUsuario.Text, txtPassword.Text);

                // DataTable primeraVez = implUser.loginPrimeraVez(txtNameUser.Text, txtPassword.Password);

                if (table.Rows.Count > 0)
                {
                    Session["UserID"] = short.Parse(table.Rows[0][0].ToString());
                    Session["UserName"] = table.Rows[0][1].ToString();
                    Session["UserRole"] = table.Rows[0][2].ToString();

                    switch (table.Rows[0][2].ToString()) // Nos devuelve el rol
                    {
                        case "Administrador":
                            // Redireccionar al administrador a la página Default.aspx
                            Response.Redirect("Home.aspx");
                            break;

                        case "Enfermera":
                            Response.Redirect("Nurse_home.aspx");
                            break;
                        case "Paciente":
                            Response.Redirect("Nurse_home.aspx");
                            break;

                        default:
                            // En caso de que el rol no coincida con ninguno de los casos anteriores, redireccionar a una página de error o a la página principal
                            // Response.Redirect("Error.aspx");
                            break;
                    }

                }
                else
                {
                    //lblMessage.Text = "Contraseña o usuario incorrectos";
                    //lblMessage.CssClass = "error-message";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}