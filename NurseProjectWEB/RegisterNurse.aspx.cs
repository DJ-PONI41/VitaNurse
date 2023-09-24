using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace NurseProjectWEB
{
    public partial class RegisterNurse : System.Web.UI.Page
    {
        NurseImpl implNurse;
        Nurse N;

        UserImpl implUser;
        User U;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            InsertUserNurse();
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
                //int n = implNurse.Insert2(N,U);

                U = new User(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, user, password, rol);
                implUser = new UserImpl();
                int u = implUser.Insert2(U, N);

                if (u > 0)
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