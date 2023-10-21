using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO.Tools;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Web;
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
                string nombre = Tools.EliminarEspacios(txtNombre.Text);
                string apellidoPaterno = Tools.EliminarEspacios(txtApellidoPaterno.Text);
                string apellidoMaterno = Tools.EliminarEspacios(txtApellidoMaterno.Text);
                DateTime fechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                string celular = Tools.EliminarEspacios(txtCelular.Text);
                string ci = Tools.EliminarEspacios(txtCi.Text);
                string correo = Tools.EliminarEspacios(txtCorreo.Text);
                string direccion = Tools.EliminarEspacios(txtDireccion.Text);
                string latitud = txtLat.Text;
                string longitud = txtLong.Text;
                string municipio = Tools.EliminarEspacios(txtMunicipio.Text);
                string especialidad = Tools.EliminarEspacios(txtEspecialidad.Text);
                DateTime añoTitulacion = DateTime.Parse(txtTitulacion.Text);
                string rol = "Enfermera";

                if (string.IsNullOrEmpty(nombre))
                {
                    label1.Text = "El nombre está vacío";
                    return;
                }

                if (!Tools.ValidarTextoConÑ(nombre))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }


                if (string.IsNullOrEmpty(apellidoPaterno))
                {
                    label1.Text = "El nombre está vacío";
                    return;
                }

                if (!Tools.ValidarTextoConÑ(apellidoPaterno))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }


                if (!Tools.ValidarTextoConÑ(apellidoMaterno))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }

                if (string.IsNullOrEmpty(celular))
                {
                    label1.Text = "El nombre está vacío";
                }

                if (!Tools.ValidatePhoneNumber(celular))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }

                if (string.IsNullOrEmpty(correo))
                {
                    label1.Text = "El nombre está vacío";
                }

                if (!Tools.validarCorreo(correo))
                {
                    label1.Text = "El correo electrónico no es válido.";
                    return;
                }

                if (string.IsNullOrEmpty(direccion))
                {
                    label1.Text = "El nombre está vacío";
                }

                if (!Tools.VlAdress(direccion))
                {
                    label1.Text = "El correo electrónico no es válido.";
                    return;
                }

                if (string.IsNullOrEmpty(municipio))
                {
                    label1.Text = "El nombre está vacío";
                    return;
                }

                if (!Tools.ValidarTextoConÑ(municipio))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }

                if (string.IsNullOrEmpty(especialidad))
                {
                    label1.Text = "El nombre está vacío";
                    return;
                }

                if (!Tools.ValidarTextoConÑ(especialidad))
                {
                    label1.Text = "El nombre contiene caracteres inválidos";
                    return;
                }


                // Datos img
                byte[] imgData = ReadFileData(fileUpload.PostedFile);

                // Datos PDF - Título
                byte[] tituloPdfData = ReadFileData(fileTitulo.PostedFile);

                // Datos PDF - CVC
                byte[] cvcPdfData = ReadFileData(fileCvc.PostedFile);

                string user = GenerateUser(nombre, apellidoPaterno, apellidoMaterno, rol);
                string password = ContraseñaRandom(apellidoMaterno);

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

        private string ContraseñaRandom(string apellidoMaterno)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var passwordChars = new List<char>(8); 

            var random = new Random();

            for (int i = 0; i < 8; i++)
            {
                
                passwordChars.Add(characters[random.Next(characters.Length)]);
            }

           
            if (string.IsNullOrEmpty(apellidoMaterno))
            {
                for (int i = 0; i < 2; i++)
                {
                    passwordChars.Add(characters[random.Next(characters.Length)]);
                }
            }

            return new string(passwordChars.ToArray());
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