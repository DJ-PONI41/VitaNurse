using System;
using System.Data;
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
                string username = txtUsuario.Text;
                string password = txtPassword.Text;

                DataTable table = implUser.Login(username, password);
                DataTable table2 = implUser.LoginNurse(username, password);

                if (table.Rows.Count > 0)
                {
                    ProcessUserLogin(table);
                }
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
                lblMessage.Text = ex.Message;
            }
        }

        private void ProcessUserLogin(DataTable userData)
        {
            Session["UserID"] = short.Parse(userData.Rows[0][0].ToString());
            Session["UserName"] = userData.Rows[0][1].ToString();
            Session["UserRole"] = userData.Rows[0][2].ToString();

            string userRole = userData.Rows[0][2].ToString();
            string redirectPage = "Home.aspx";

            switch (userRole)
            {
                case "Administrador":
                    redirectPage = "AdmHome.aspx";
                    break;
                case "Enfermera":
                    redirectPage = "Nurse_home.aspx";
                    break;
                case "Paciente":
                    redirectPage = "Pasciente_home.aspx";
                    break;
            }

            Response.Redirect(redirectPage);
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "error-message";
        }
    }
}
