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
                DataTable table2 = implUser.LoginNurse(txtUsuario.Text, txtPassword.Text);

                // Comprobar si se ha encontrado un usuario en la tabla de usuarios
                if (table.Rows.Count > 0)
                {
                    ProcessUserLogin(table);
                }
                // Comprobar si se ha encontrado un usuario en la tabla de enfermeras
                else if (table2.Rows.Count > 0)
                {
                    ProcessUserLogin(table2);
                }
                else
                {
                    ShowErrorMessage("Contraseña o usuario incorrectos");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessUserLogin(DataTable userData)
        {
            Session["UserID"] = short.Parse(userData.Rows[0][0].ToString());
            Session["UserName"] = userData.Rows[0][1].ToString();
            Session["UserRole"] = userData.Rows[0][2].ToString();

            string userRole = userData.Rows[0][2].ToString();

            switch (userRole)
            {
                case "Administrador":
                    Response.Redirect("AdmHome.aspx");
                    break;

                case "Enfermera":
                    Response.Redirect("Nurse_home.aspx");
                    break;
                case "Paciente":
                    Response.Redirect("Nurse_home.aspx");
                    break;

                default:
                    Response.Redirect("Home.aspx");
                    break;
            }
        }

        private void ShowErrorMessage(string message)
        {
            // Muestra un mensaje de error en la página
            // lblMessage.Text = message;
            // lblMessage.CssClass = "error-message";
        }
    }
}