using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class ApprovalNurse : System.Web.UI.Page
    {
        private NurseImpl implNurse;

        private Nurse N;

        private short id;
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string userRole = Session["UserRole"].ToString();
                if (userRole != "Administrador")
                {
                    Response.Write("Acceso no autorizado. Debes tener el rol de Administrador para acceder a esta página.");
                }
            }

            if (!IsPostBack)
            {
                LoadType();
            }
        }

        private void LoadType()
        {
            type = Request.QueryString["type"];
            if (type == "U")
            {
                //btnVolver.Visible = true;
                btnAceptar.Visible = true;
                btnUpdate.Visible = true;
                Get();
            }
        }

        private void Get()
        {
            N = null;
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                implNurse = new NurseImpl();
                N = implNurse.Get(id);
                if (N != null && !IsPostBack)
                {
                    txtNombre.Text = N.Name.ToString();
                    txtApellidoPaterno.Text = N.LastName.ToString();
                    txtApellidoMaterno.Text = N.SecondLastName.ToString();
                    txtFechaNacimiento.Text = N.Birthdate.ToString("yyyy-MM-dd");
                    txtCelular.Text = N.Phone.ToString();
                    txtCi.Text = N.Ci.ToString();
                    txtCorreo.Text = N.Email.ToString();
                    txtDireccion.Text = N.Addres.ToString();
                    txtLat.Text = N.Latitude.ToString();
                    txtLong.Text = N.Longitude.ToString();
                    txtMunicipio.Text = N.Municipio.ToString();
                    txtEspecialidad.Text = N.Especialidad.ToString();
                    txtTitulacion.Text = N.AñoTitulacion.ToString("yyyy-MM-dd");

                    if (N.PhotoData != null && N.PhotoData.Length > 0)
                    {
                        string base64Image = Convert.ToBase64String(N.PhotoData);
                        imgPreview.ImageUrl = "data:image/jpeg;base64," + base64Image;
                    }
                    else
                    {
                        imgPreview.ImageUrl = string.Empty;
                    }
                }
            }
        }


        private void EnviarCorreo(string email, string subject, string body)
        {
            try
            {
                string remitente = "pruebasprubea@gmail.com";
                string contraseñaRemitente = "gnwnnxeytwqgafwc";

                MailMessage mensaje = new MailMessage(remitente, email);
                mensaje.Subject = subject;
                mensaje.Body = body;

                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.EnableSsl = true;
                clienteSmtp.Credentials = new NetworkCredential(remitente, contraseñaRemitente);

                clienteSmtp.Send(mensaje);
                
                ScriptManager.RegisterStartupScript(this, GetType(), "CorreoEnviado", "alert('Se ha enviado un correo con éxito.');", true);
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error en la página web
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCorreo", $"alert('Error al enviar el correo: {ex.Message}');", true);
            }
        }
               

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implNurse = new NurseImpl();
                    N = implNurse.Get(id);
                    if (N != null)
                    {
                        int n = implNurse.Update(N);

                        if (n > 0)
                        {
                            EnviarCorreo(N.Email, "Cuenta activada", "Su cuenta ha sido activada correctamente.");
                            Response.Redirect("Listado_Crud_Nurse.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Listado_Crud_Nurse.aspx");
        }
    }
}