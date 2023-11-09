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
                load();
                LoadType();
                Select();
            }
        }

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "D")
                {
                    Delete();
                    Select();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Select()
        {
            try
            {
                implNurse = new NurseImpl();
                DataTable dt = implNurse.SelectApproval();
                DataTable table = new DataTable("Nurse");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Fecha de nacimiento", typeof(string));
                table.Columns.Add("Titulo Profesional", typeof(string));
                table.Columns.Add("CV", typeof(string));
                table.Columns.Add("Celular", typeof(string));
                table.Columns.Add("CI", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Direccion", typeof(string));
                table.Columns.Add("Rol", typeof(string));
                table.Columns.Add("Especialidad", typeof(string));
                table.Columns.Add("Año de Titulacion", typeof(string));
                table.Columns.Add("Aceptar", typeof(string));
                table.Columns.Add("Borrar", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaFormateada = fechaNacimiento.ToString("yyyy-MM-dd");

                    DateTime fechaTitulacion = (DateTime)dr["Año de Titulacion"];
                    string fechaFormateadaTitulacion = fechaTitulacion.ToString("yyyy-MM-dd");

                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                   dr["Apellido Materno"].ToString(), fechaFormateada, "", "",
                                   dr["Celular"].ToString(), dr["CI"].ToString(), dr["Correo"].ToString(),
                                   dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                   dr["Especialidad"].ToString(), fechaFormateadaTitulacion, "", "");
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();
                    string up = $"<a class='btn btn-sm btn-warning' href='ApprovalNurse.aspx?id={id}&type=U'> Seleccionar</a>";
                    string del = $"<a class='btn btn-sm btn-danger' href='ApprovalNurse.aspx?id={id}&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'>Rechazar</i></a>";
                    string PdfTitulo = $"<a href='WiewPdf.aspx?id={id}&type=Titulo' target='_blank'>Ver Titulo</a>";
                    string PdfCv = $"<a href='WiewPdf.aspx?id={id}&type=CV' target='_blank'>Ver CV</a>";
                    GridDat.Rows[i].Cells[4].Text = PdfTitulo;
                    GridDat.Rows[i].Cells[5].Text = PdfCv;
                    GridDat.Rows[i].Cells[13].Text = up;
                    GridDat.Rows[i].Cells[14].Text = del;
                    GridDat.Rows[i].Attributes["data-id"] = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Delete()
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
                        int n = implNurse.Delete(N);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void LoadType()
        {
            type = Request.QueryString["type"];
            if (type == "U")
            {
                btnVolver.Visible = true;
                btnAceptar.Visible = true;
                
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApprovalNurse.aspx");
        }
    }
}