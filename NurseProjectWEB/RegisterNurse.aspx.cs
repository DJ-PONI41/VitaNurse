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
        private NurseImpl implNurse;
        private UserImpl implUser;
    

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            InsertUserNurse();
        }

        void InsertUserNurse()
        {        
            try
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
                DateTime añoTitulacion = DateTime.Parse(txtTitulacion.Text);
                string rol = "Enfermera";

                // Datos img
                byte[] imgData = ReadFileData(fileUpload.PostedFile);

                // Datos PDF - Título
                byte[] tituloPdfData = ReadFileData(fileTitulo.PostedFile);

                // Datos PDF - CVC
                byte[] cvcPdfData = ReadFileData(fileCvc.PostedFile);

                string user = GenerateUser(nombre, apellidoPaterno, apellidoMaterno, rol);
                string password = ContraseñaRandom();

                Nurse N = new Nurse(nombre, apellidoPaterno, apellidoMaterno, imgData, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, especialidad, añoTitulacion, tituloPdfData, cvcPdfData);
                User U = new User(nombre, apellidoPaterno, apellidoMaterno, imgData, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, user, password, rol);
                
                implUser = new UserImpl();
                int u = implUser.Insert2(U, N);
               
                if (u > 0)
                {
                    ShowMessage("El registro se ha realizado con éxito.", "success");
                    Task.Run(() => EnviarCorreo(user, password, correo));
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ShowMessage("¡Error! No se pudo realizar el registro.", "danger");
                }

            }
            catch (Exception ex)
            {

                ShowMessage("¡Error! " + ex.Message, "danger");
            }
        }

        private byte[] ReadFileData(HttpPostedFile file)
        {
            int fileLength = file.ContentLength;
            byte[] fileData = new byte[fileLength];
            file.InputStream.Read(fileData, 0, fileLength);
            return fileData;
        }

        private string GenerateUser(string nombre, string apellidoPaterno, string apellidoMaterno, string rol)
        {
            string nombres = nombre.Replace(" ", "").ToLower();
            string userChars = $"{apellidoPaterno[0]}";

            if (!string.IsNullOrEmpty(apellidoMaterno))
            {
                userChars += $"{apellidoMaterno[0]}";
            }

            userChars += nombres.Substring(0, Math.Min(6, nombres.Length));
            userChars += rol[0];

            return userChars;
        }

        private string ContraseñaRandom()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var passwordChars = new char[8];
            var random = new Random();

            for (int i = 0; i < passwordChars.Length; i++)
            {
                passwordChars[i] = characters[random.Next(characters.Length)];
            }

            return new string(passwordChars);
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
                ShowMessage("Se ha enviado un correo con su Usuario y Contraseña.", "success");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "danger");
            }
        }

        private void ShowMessage(string message, string cssClass)
        {
            label1.CssClass = $"alert alert-{cssClass}";
            label1.Text = message;
            label1.Style["display"] = "block";
        }

    }
}