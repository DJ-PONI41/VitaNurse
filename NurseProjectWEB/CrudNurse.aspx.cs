using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace NurseProjectWEB
{
    public partial class CrudNurse : System.Web.UI.Page
    {




        NurseImpl implNurse;
        Nurse N;

        UserImpl implUser;
        User U;

        private short id;
        private string type;
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
                if (userRole != "Administrador")
                {
                    // El usuario no tiene el rol adecuado, mostrar un mensaje de error o redirigir a una página de acceso no autorizado
                    Response.Write("Acceso no autorizado. Debes tener el rol de Administrador para acceder a esta página.");
                    // También puedes redirigir a una página de acceso no autorizado en lugar de mostrar un mensaje aquí.
                }
            }



            if (!IsPostBack)
            {

                load();
                LoadType();
                Select();

            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            InsertUserNurse();
            Select();
        }


        void InsertUserNurse()
        {
            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtApellidoMaterno.Text;
            DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
            string celular = txtCelular.Text;
            string ci = txtCi.Text;
            string correo = txtCorreo.Text;
            string direccion = txtDireccion.Text;
            string latitud = txtLat.Text;
            string longitud = txtLong.Text;
            string municipio = txtMunicipio.Text;
            string especialidad = txtEspecialidad.Text;
            DateTime añotitulacion = DateTime.Parse(txtTitulacion.Text);
            string rol = "Enfermera";

            //Datos img
            int img = fileUpload.PostedFile.ContentLength;
            byte[] ImgOriginal = new byte[img];
            fileUpload.PostedFile.InputStream.Read(ImgOriginal, 0, img);

            //datos pdf
            int titulo = fileTitulo.PostedFile.ContentLength;
            byte[] PdfOriginal = new byte[titulo];
            fileUpload.PostedFile.InputStream.Read(PdfOriginal, 0, titulo);

            int Cvc = fileCvc.PostedFile.ContentLength;
            byte[] CvcOriginal = new byte[Cvc];
            fileUpload.PostedFile.InputStream.Read(CvcOriginal, 0, Cvc);



            try
            {
                string inicialApellidoPaternoMinuscula = apellidoPaterno.Substring(0, 1).ToLower();
                string inicialApellidoMaternoMinuscula = string.Empty;
                if (!string.IsNullOrEmpty(apellidoMaterno))
                {
                    inicialApellidoMaternoMinuscula = apellidoMaterno.Substring(0, 1).ToLower();
                }
                string nombreCompletoSinEspacios = Regex.Replace(nombre, @"\s", "").ToLower();
                string user;
                if (string.IsNullOrEmpty(apellidoMaterno))
                {
                    user = GenerateRandomUser(nombreCompletoSinEspacios, 6) + rol.Substring(0, 1);
                }
                else
                {
                    user = inicialApellidoPaternoMinuscula + inicialApellidoMaternoMinuscula + nombreCompletoSinEspacios.Substring(0, Math.Min(6, nombreCompletoSinEspacios.Length)) + rol.Substring(0, 1);
                }
                string password = ContraseñaRandom();


                N = new Nurse(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, especialidad, añotitulacion, PdfOriginal, CvcOriginal);
                implNurse = new NurseImpl();
                int n = implNurse.InsertN2(U, N);

                U = new User(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, user, password, rol);
                implUser = new UserImpl();
                //int u = implUser.Insert2(U, N);

                if (n > 0)
                {

                    label1.CssClass = "alert alert-success";
                    label1.Text = "El registro se ha realizado con éxito.";
                    label1.Style["display"] = "block";
                    Task.Run(() => EnviarCorreo(user, password, correo));
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    label1.CssClass = "alert alert-danger";
                    label1.Text = "¡Error! No se pudo realizar el registro.";
                    label1.Style["display"] = "block";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        void load()
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
        void Select()
        {
            try
            {
                implNurse = new NurseImpl();
                DataTable dt = implNurse.Select();
                DataTable table = new DataTable("Nurse");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Fecha de nacimiento", typeof(string));
                table.Columns.Add("Celular", typeof(string));
                table.Columns.Add("CI", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Direccion", typeof(string));
                table.Columns.Add("Rol", typeof(string));

                table.Columns.Add("Especialidad", typeof(string));
                table.Columns.Add("Año de Titulacion", typeof(string));
                table.Columns.Add("Seleccionar", typeof(string));
                table.Columns.Add("Borrar", typeof(string));


                foreach (DataRow dr in dt.Rows)
                {
                    //
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaFormateada = fechaNacimiento.ToString("yyyy-MM-dd");

                    DateTime fechaTitulacion = (DateTime)dr["Año de Titulacion"];
                    string fechaFormateadaTitulacion = fechaTitulacion.ToString("yyyy-MM-dd");



                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                    dr["Apellido Materno"].ToString(), fechaFormateada,
                                    dr["Celular"].ToString(), dr["CI"].ToString(), dr["Correo"].ToString(),
                                    dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                    dr["Especialidad"].ToString(), fechaFormateadaTitulacion, "", "");
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();
                    string up = "<a class='btn btn-sm btn-warning' href='CrudNurse.aspx?id=" + id + "&type=U'> Seleccionar</a>";

                    string del = "<a class='btn btn-sm btn-danger' href='CrudNurse.aspx?id=" + id + "&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'>Eliminar</i></a>";
                    GridDat.Rows[i].Cells[11].Text = up;
                    GridDat.Rows[i].Cells[12].Text = del;
                    GridDat.Rows[i].Attributes["data-id"] = id;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        void Delete()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implNurse = new NurseImpl();

                    // Obtener el objeto Customer antes de llamar al método Delete
                    N = implNurse.Get(id);

                    if (N != null)
                    {
                        int n = implNurse.Delete(N);
                        // Realizar cualquier acción adicional después de la eliminación
                    }
                    else
                    {
                        // El objeto Customer no existe en la base de datos, manejar el caso de error
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        void LoadType()
        {
            try
            {
                type = Request.QueryString["type"];

                if (type == "U")
                {
                    //txtUsuario.Visible = false;
                    //txtContrasena.Visible = false;
                    Get();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void Get()
        {
            N = null;

            id = short.Parse(Request.QueryString["id"]);
            try
            {
                if (id > 0)
                {
                    implNurse = new NurseImpl();
                    N = implNurse.Get(id);

                    if (N != null)
                    {
                        if (!IsPostBack)
                        {
                            txtNombre.Text = N.Name.ToString();
                            txtApellidoPaterno.Text = N.LastName.ToString();
                            txtApellidoMaterno.Text = N.SecondLastName.ToString();
                            txtFechaNacimiento.Text = N.Birthdate.ToString("dd/MM/yyyy");
                            txtCelular.Text = N.Phone.ToString();
                            txtCi.Text = N.Ci.ToString();
                            txtCorreo.Text = N.Email.ToString();
                            txtDireccion.Text = N.Addres.ToString();
                            txtLat.Text = N.Latitude.ToString();
                            txtLong.Text = N.Longitude.ToString();
                            txtMunicipio.Text = N.Municipio.ToString();
                            txtEspecialidad.Text = N.Especialidad.ToString();
                            txtTitulacion.Text = N.AñoTitulacion.ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GenerateRandomUser(string nombre, int maxLength)
        {
            //var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var userChars = new char[Math.Min(maxLength, nombre.Length)];

            for (int i = 0; i < userChars.Length; i++)
            {
                userChars[i] = nombre[random.Next(nombre.Length)];
            }

            return new string(userChars);
        }

        string ContraseñaRandom()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var passwordChars = new char[8];
            var random = new Random();

            for (int i = 0; i < passwordChars.Length; i++)
            {
                passwordChars[i] = characters[random.Next(characters.Length)];
            }

            return new String(passwordChars);
        }

        private void EnviarCorreo(string user, string password, string email)
        {
            try
            {
                string remitente = "pruebasprubea@gmail.com";
                string contraseñaRemitente = "gnwnnxeytwqgafwc";

                MailMessage mensaje = new MailMessage(remitente, email);
                mensaje.Subject = "Credenciales de acceso";
                mensaje.Body = $"Usuario: {user}\nContraseña: {password}";

                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.EnableSsl = true;
                clienteSmtp.Credentials = new NetworkCredential(remitente, contraseñaRemitente);

                clienteSmtp.Send(mensaje);
                label1.Text = "Se ha enviado un correo con su Usuario y Contraseña.";
            }
            catch (Exception ex)
            {
                label1.Text = ex.Message;
            }
        }
    }
}