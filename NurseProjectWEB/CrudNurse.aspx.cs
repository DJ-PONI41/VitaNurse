using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO;
using System;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Data;
using NurseProjecDAO.Tools;

namespace NurseProjectWEB
{
    public partial class CrudNurse : System.Web.UI.Page
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
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            InsertUserNurse();
        }

        private void InsertUserNurse()
        {
            try
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
                        User U = new User(nombre, apellidoPaterno, apellidoMaterno, imgData, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, user, password, "Enfermera");

                        implNurse = new NurseImpl();
                        int n = implNurse.InsertN2(U, N);

                        if (n > 0)
                        {
                            ShowMessage("El registro se ha realizado con éxito.", "success");
                            EnviarCorreo(user, password, correo);
                            Response.Redirect("Crud_Listado_nueva_enfermera.aspx");
                        }
                        else
                        {
                            ShowMessage("¡Error! No se pudo realizar el registro.", "danger");
                        }
                    }
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
                string remitente = "pruebasprueba@gmail.com";
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
                        ShowMessage("Se ha enviado un correo con su Usuario y Contraseña.", "success");
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

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "D")
                {
                    Delete();
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
                btnAtras.Visible = true;
                imgPreview.Visible = true;
                btnUpdate.Visible = true;
                btnRegistrar.Visible = false;
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

        private void ShowMessage(string message, string cssClass)
        {
            label1.CssClass = $"alert alert-{cssClass}";
            label1.Text = message;
            label1.Style["display"] = "block";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            short id = short.Parse(Request.QueryString["id"]);
            implNurse = new NurseImpl();

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

                    // Verificar si se cargó un nuevo archivo de titulo (PDF)
                    byte[] PdfOriginal = null;
                    if (fileTitulo.HasFile)
                    {
                        string ext = System.IO.Path.GetExtension(fileTitulo.FileName).ToLower();
                        if (ext != ".pdf")
                        {
                            ShowMessage("El archivo de título debe ser un PDF.", "danger");
                            return;
                        }

                        int titulo = fileTitulo.PostedFile.ContentLength;
                        PdfOriginal = new byte[titulo];
                        fileTitulo.PostedFile.InputStream.Read(PdfOriginal, 0, titulo);
                    }
                    else
                    {
                        Nurse TituloExistente = implNurse.Get(id);
                        if (TituloExistente != null)
                        {
                            PdfOriginal = TituloExistente.LugarTitulacion;
                        }
                    }

                    // Verificar si se cargó un nuevo archivo de CV 
                    byte[] CvOriginal = null;
                    if (fileCvc.HasFile)
                    {
                        string ext = System.IO.Path.GetExtension(fileCvc.FileName).ToLower();
                        if (ext != ".pdf")
                        {
                            ShowMessage("El archivo de CV debe ser un PDF.", "danger");
                            return;
                        }

                        int Cvc = fileCvc.PostedFile.ContentLength;
                        CvOriginal = new byte[Cvc];
                        fileCvc.PostedFile.InputStream.Read(CvOriginal, 0, Cvc);
                    }
                    else
                    {
                        Nurse CVExistente = implNurse.Get(id);
                        if (CVExistente != null)
                        {
                            CvOriginal = CVExistente.Cvc;
                        }
                    }

                    // Verificar si se cargó una nueva imagen
                    byte[] ImgOriginal = null;
                    if (fileUpload.HasFile)
                    {
                        int img = fileUpload.PostedFile.ContentLength;
                        ImgOriginal = new byte[img];
                        fileUpload.PostedFile.InputStream.Read(ImgOriginal, 0, img);
                    }
                    else
                    {
                        
                        Nurse ImgExistente = implNurse.Get(id);
                        if (ImgExistente != null)
                        {
                            ImgOriginal = ImgExistente.PhotoData;
                        }
                    }

                    Nurse nurse = new Nurse(id, nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, especialidad, añoTitulacion, PdfOriginal, CvOriginal);
                    int n = implNurse.UpdateN(nurse);

                    if (n > 0)
                    {
                        Response.Redirect("Listado_Crud_Nurse.aspx");
                    }
                    else
                    {
                        ShowMessage("Error al actualizar.", "error");
                    }
                }
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Crud_Listado_nueva_enfermera.aspx");
        }
    }

}