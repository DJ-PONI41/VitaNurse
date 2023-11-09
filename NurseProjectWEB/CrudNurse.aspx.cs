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
                Select();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            InsertUserNurse();
            Select();
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
                            Task.Run(() => EnviarCorreo(user, password, correo));
                            Select();
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
            var upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var lowerChars = "abcdefghijklmnopqrstuvwxyz";
            var digitChars = "0123456789";
            var specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

            var allChars = upperChars + lowerChars + digitChars + specialChars;
            var passwordChars = new char[8];

            var random = new Random();

            passwordChars[0] = upperChars[random.Next(upperChars.Length)];
            passwordChars[1] = lowerChars[random.Next(lowerChars.Length)];
            passwordChars[2] = digitChars[random.Next(digitChars.Length)];
            passwordChars[3] = specialChars[random.Next(specialChars.Length)];


            for (int i = 4; i < 12; i++)
            {
                passwordChars[i] = allChars[random.Next(allChars.Length)];
            }

            for (int i = 0; i < passwordChars.Length - 1; i++)
            {
                int j = random.Next(i, passwordChars.Length);
                (passwordChars[i], passwordChars[j]) = (passwordChars[j], passwordChars[i]);
            }

            return new string(passwordChars);
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
                DataTable dt = implNurse.Select();
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
                table.Columns.Add("Seleccionar", typeof(string));
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
                    string up = $"<a class='btn btn-sm btn-warning' href='CrudNurse.aspx?id={id}&type=U'> Seleccionar</a>";
                    string del = $"<a class='btn btn-sm btn-danger' href='CrudNurse.aspx?id={id}&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'>Eliminar</i></a>";
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
                btnAtras.Visible = true;
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
                        Select();
                        Response.Redirect("CrudNurse.aspx");
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
            Response.Redirect("CrudNurse.aspx");
        }
    }

}