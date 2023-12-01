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
using System.Web.UI;

namespace NurseProjectWEB
{
    public partial class RegisterNurse : System.Web.UI.Page
    {       
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
            
                string nombre = Tools.EliminarEspacios(txtNombre.Text);
                string apellidoPaterno = Tools.EliminarEspacios(txtApellidoPaterno.Text);
                string apellidoMaterno = Tools.EliminarEspacios(txtApellidoMaterno.Text);
                string celular = Tools.EliminarEspacios(txtCelular.Text);
                string ci = Tools.EliminarEspacios(txtCi.Text);
                string correo = Tools.EliminarEspacios(txtCorreo.Text);
                string direccion = Tools.EliminarEspacios(txtDireccion.Text);
                string latitud = txtLat.Text;
                string longitud = txtLong.Text;
                string municipio = Tools.EliminarEspacios(txtMunicipio.Text);
                string especialidad = Tools.EliminarEspacios(txtEspecialidad.Text);

                if (string.IsNullOrEmpty(nombre) || !Tools.ValidarTextoConÑ(nombre))
                    ShowMessage("El nombre no es válido.", "danger");
                else if (string.IsNullOrEmpty(apellidoPaterno) || !Tools.ValidarTextoConÑ(apellidoPaterno))
                    ShowMessage("El apellido paterno no es válido.", "danger");
                else if (!Tools.ValidarTextoConÑ(apellidoMaterno))
                    ShowMessage("El apellido materno no es válido.", "danger");
                else if (string.IsNullOrEmpty(celular) || !Tools.ValidatePhoneNumber(celular))
                    ShowMessage("El número de celular no es válido.", "danger");
                else if (string.IsNullOrEmpty(correo) || !Tools.validarCorreo(correo))
                    ShowMessage("El correo electrónico no es válido.", "danger");
                else if (string.IsNullOrEmpty(direccion) || !Tools.VlAdress(direccion))
                    ShowMessage("La dirección no es válida.", "danger");
                else if (string.IsNullOrEmpty(municipio) || !Tools.ValidarTextoConÑ(municipio))
                    ShowMessage("El municipio no es válido.", "danger");
                else if (string.IsNullOrEmpty(especialidad) || !Tools.ValidarTextoConÑ(especialidad))
                    ShowMessage("La especialidad no es válida.", "danger");
                else
                {
                    DateTime fechaNacimiento, añoTitulacion;

                    if (!DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento) || fechaNacimiento > DateTime.Now)
                        ShowMessage("La fecha de nacimiento no es válida.", "danger");
                    else if (!DateTime.TryParse(txtTitulacion.Text, out añoTitulacion) || añoTitulacion > DateTime.Now)
                        ShowMessage("El año de titulación no es válido.", "danger");
                    else
                    {
                        // Validación de archivos
                        if (fileUpload.HasFile)
                        {
                            string ext = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                            if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                            {
                                ShowMessage("La foto debe ser una imagen en formato JPG, JPEG o PNG.", "danger");
                                return;
                            }
                        }
                        else
                        {
                            ShowMessage("Debes seleccionar una foto.", "danger");
                            return;
                        }

                        if (fileTitulo.HasFile)
                        {
                            string ext = System.IO.Path.GetExtension(fileTitulo.FileName).ToLower();
                            if (ext != ".pdf")
                            {
                                ShowMessage("El archivo de título debe ser un PDF.", "danger");
                                return;
                            }
                        }
                        else
                        {
                            ShowMessage("Debes adjuntar el archivo de título.", "danger");
                            return;
                        }

                        if (fileCvc.HasFile)
                        {
                            string ext = System.IO.Path.GetExtension(fileCvc.FileName).ToLower();
                            if (ext != ".pdf")
                            {
                                ShowMessage("El archivo de CV debe ser un PDF.", "danger");
                                return;
                            }
                        }
                        else
                        {
                            ShowMessage("Debes adjuntar el archivo de CV.", "danger");
                            return;
                        }

                        string user = GenerateUser(nombre, apellidoPaterno, apellidoMaterno, "Enfermera");
                        string password = ContraseñaRandom();

                        byte[] imgData = ReadFileData(fileUpload.PostedFile);
                        byte[] tituloPdfData = ReadFileData(fileTitulo.PostedFile);
                        byte[] cvcPdfData = ReadFileData(fileCvc.PostedFile);

                        Nurse N = new Nurse(nombre, apellidoPaterno, apellidoMaterno, imgData, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, especialidad, añoTitulacion, tituloPdfData, cvcPdfData);
                        User U =  new User(nombre, apellidoPaterno, apellidoMaterno, imgData, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, user, password, "Enfermera");

                        implUser = new UserImpl();
                        int u = implUser.Insert2(U, N);

                        if (u > 0)
                        {
                            ShowMessage("El registro se ha realizado con éxito.", "success");
                             EnviarCorreo(user, password, correo);
                            Response.Redirect("Home.aspx");
                        }
                        else
                        {
                            ShowMessage("¡Error! No se pudo realizar el registro.", "danger");
                        }
                    }
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
            string userChars = $"{apellidoPaterno[0]}";

            if (!string.IsNullOrEmpty(apellidoMaterno))
            {
                userChars += apellidoMaterno[0];
            }
            else
            {
                var random = new Random();
                userChars += (char)('A' + random.Next(26));
                userChars += (char)('A' + random.Next(26)); 
            }

            string nombres = nombre.Replace(" ", "");
            userChars += nombres.Substring(0, Math.Min(6, nombres.Length));
            userChars += rol[0];

            return userChars;
        }



        private string ContraseñaRandom()
        {
            const string caracteresMayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string caracteresMinusculas = "abcdefghijklmnopqrstuvwxyz";
            const string caracteresDigitos = "0123456789";
            const string caracteresEspeciales = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

            const string todosLosCaracteres = caracteresMayusculas + caracteresMinusculas + caracteresDigitos + caracteresEspeciales;

            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var data = new byte[4];
                rng.GetBytes(data);
                var seed = BitConverter.ToInt32(data, 0);
                var random = new Random(seed);

                var passwordChars = new char[16]; 

                
                passwordChars[0] = caracteresMayusculas[random.Next(caracteresMayusculas.Length)];
                passwordChars[1] = caracteresMinusculas[random.Next(caracteresMinusculas.Length)];
                passwordChars[2] = caracteresDigitos[random.Next(caracteresDigitos.Length)];
                passwordChars[3] = caracteresEspeciales[random.Next(caracteresEspeciales.Length)];

                
                for (int i = 4; i < passwordChars.Length; i++)
                {
                    passwordChars[i] = todosLosCaracteres[random.Next(todosLosCaracteres.Length)];
                }

                
                for (int i = 0; i < passwordChars.Length - 1; i++)
                {
                    int j = random.Next(i, passwordChars.Length);
                    (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
                }

                return new string(passwordChars);
            }
        }





        private void EnviarCorreo(string user, string password, string email)
        {
            try
            {
                string remitente = "pruebasprubea@gmail.com";
                string contraseñaRemitente = "gnwnnxeytwqgafwc";
                string servidorSmtp = "smtp.gmail.com";
                int puertoSmtp = 587;

                using (MailMessage mensaje = new MailMessage(remitente, email))
                {
                    mensaje.Subject = "Credenciales de acceso";
                    mensaje.Body = $"Usuario: {user}\nContraseña: {password}";

                    using (SmtpClient clienteSmtp = new SmtpClient(servidorSmtp, puertoSmtp))
                    {
                        clienteSmtp.EnableSsl = true;
                        clienteSmtp.Credentials = new NetworkCredential(remitente, contraseñaRemitente);

                        clienteSmtp.Send(mensaje);
                        ScriptManager.RegisterStartupScript(this, GetType(), "CorreoEnviado", "alert('Se ha enviado un correo con éxito.');", true);
                    }
                }
            }
            catch (SmtpException ex)
            {
                ShowMessage("Error al enviar el correo: " + ex.Message, "danger");
            }
            catch (Exception ex)
            {
                ShowMessage("Ocurrió un error inesperado: " + ex.Message, "danger");
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