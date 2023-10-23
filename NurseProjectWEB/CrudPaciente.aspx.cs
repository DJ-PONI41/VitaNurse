using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing;
using NurseProjecDAO.Tools;

namespace NurseProjectWEB
{
    public partial class CrudPaciente : System.Web.UI.Page
    {
        PacienteImpl implPaciente;
        Paciente P;
        User U;
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
            try
            {
                string nombre = Tools.EliminarEspacios(txtNombre.Text);
                string apellidoPaterno = Tools.EliminarEspacios(txtApellidoPaterno.Text);
                string apellidoMaterno = Tools.EliminarEspacios(txtApellidoMaterno.Text);
                string ci = Tools.EliminarEspacios(txtCi.Text);
                string fechaNacimientoStr = txtFechaNacimiento.Text;
                string direccion = Tools.EliminarEspacios(txtDireccion.Text);
                string latitud = txtLat.Text;
                string longitud = txtLong.Text;
                string celular = Tools.EliminarEspacios(txtCelular.Text);
                string municipio = Tools.EliminarEspacios(txtMunicipio.Text);
                string correo = Tools.EliminarEspacios(txtCorreo.Text);
                string usuario = Tools.EliminarEspacios(txtUsuario.Text);
                string contraseña = Tools.EliminarEspacios(txtContrasena.Text);
                string historial = Tools.EliminarEspacios(txtHistorial.Text);
                string rol = "Paciente";

                byte[] ImgOriginal = null;

                if (fileUpload.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png")
                    {
                        label1.CssClass = "alert alert-danger";
                        label1.Text = "La imagen debe ser en formato JPG, JPEG o PNG.";
                        label1.Style["display"] = "block";
                        return;
                    }

                    int img = fileUpload.PostedFile.ContentLength;
                    ImgOriginal = new byte[img];
                    fileUpload.PostedFile.InputStream.Read(ImgOriginal, 0, img);
                }
                else
                {
                    ImgOriginal = null;
                }

                if (string.IsNullOrEmpty(nombre) || !Tools.ValidarTextoConÑ(nombre))
                {
                    label1.Text = "El nombre es inválido o está vacío";
                }
                else if (string.IsNullOrEmpty(apellidoPaterno) || !Tools.ValidarTextoConÑ(apellidoPaterno))
                {
                    label1.Text = "El apellido paterno es inválido o está vacío";
                }
                else if (!Tools.ValidarTextoConÑ(apellidoMaterno))
                {
                    label1.Text = "El apellido materno es inválido";
                }
                else if (string.IsNullOrEmpty(ci) || !Tools.ValidarCi(ci))
                {
                    label1.Text = "El CI es inválido o está vacío";
                }

                DateTime fechaNacimiento;

                if (!DateTime.TryParse(fechaNacimientoStr, out fechaNacimiento))
                {
                    label1.CssClass = "alert alert-danger";
                    label1.Text = "Fecha de nacimiento no válida. Por favor, ingrese una fecha válida.";
                    label1.Style["display"] = "block";
                    return;
                }

                if (fechaNacimiento > DateTime.Now)
                {
                    label1.CssClass = "alert alert-danger";
                    label1.Text = "La fecha de nacimiento no puede ser en el futuro. Por favor, ingrese una fecha válida.";
                    label1.Style["display"] = "block";
                    return;
                }
                else if (string.IsNullOrEmpty(celular) || !Tools.ValidatePhoneNumber(celular))
                {
                    label1.Text = "El número de celular es inválido o está vacío";
                }
                else if (string.IsNullOrEmpty(usuario) || !Tools.ValidarTextoConÑ(usuario))
                {
                    label1.Text = "El usuario es inválido o está vacío";
                }
                else if (string.IsNullOrEmpty(contraseña) || !Tools.ValidarContraseña(contraseña))
                {
                    label1.Text = "La contraseña es inválida o está vacía";
                }

                else if (string.IsNullOrEmpty(direccion) || !Tools.VlAdress(direccion))
                {
                    label1.Text = "La dirección es inválida o está vacía";
                }
                else if (string.IsNullOrEmpty(municipio) || !Tools.ValidarTextoConÑ(municipio))
                {
                    label1.Text = "El municipio es inválido o está vacío";
                }
                else if (string.IsNullOrEmpty(correo) || !Tools.validarCorreo(correo))
                {
                    label1.Text = "El correo electrónico es inválido o está vacío";
                }

                else if (string.IsNullOrEmpty(historial) || !Tools.ValidarTextoConÑ(historial))
                {
                    label1.Text = "El historial es inválido o está vacío";
                }                

                P = new Paciente(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, historial);
                implPaciente = new PacienteImpl();
                U = new User(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, usuario, contraseña, rol);
                int n = implPaciente.InsertP2(U, P);
                if (n > 0)
                {
                    label1.CssClass = "alert alert-success";
                    label1.Text = "El registro se ha realizado con éxito.";
                    label1.Style["display"] = "block";
                    Select();
                    Clear();
                }
                else
                {
                    label1.CssClass = "alert alert-danger";
                    label1.Text = "¡Error! No se pudo realizar el registro.";
                    label1.Style["display"] = "block";
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
                implPaciente = new PacienteImpl();
                DataTable dt = implPaciente.Select();
                DataTable table = new DataTable("Paciente");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Fecha de nacimiento", typeof(string));
                table.Columns.Add("Celular", typeof(string));
                table.Columns.Add("CI", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Direccion", typeof(string));
                table.Columns.Add("Rol", typeof(string));
                table.Columns.Add("Historial Medico", typeof(string));
                table.Columns.Add("Seleccionar", typeof(string));
                table.Columns.Add("Borrar", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaFormateada = fechaNacimiento.ToString("yyyy-MM-dd");
                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                    dr["Apellido Materno"].ToString(), fechaFormateada,
                                    dr["Celular"].ToString(), dr["CI"].ToString(), dr["Correo"].ToString(),
                                    dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                    dr["Historial Medico"].ToString(), "", "");
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();
                    string up = "<a class='btn btn-sm btn-warning' href='CrudPaciente.aspx?id=" + id + "&type=U'> Seleccionar</a>";

                    string del = "<a class='btn btn-sm btn-danger' href='CrudPaciente.aspx?id=" + id + "&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'>Eliminar</i></a>";
                    GridDat.Rows[i].Cells[10].Text = up;
                    GridDat.Rows[i].Cells[11].Text = del;
                    GridDat.Rows[i].Attributes["data-id"] = id;

                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        void Delete()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implPaciente = new PacienteImpl();
                    P = implPaciente.Get(id);
                    if (P != null)
                    {
                        int n = implPaciente.Delete(P);
                    }
                    else
                    {
                        //error
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
                    txtUsuario.Visible = false;
                    txtContrasena.Visible = false;
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
            P = null;

            id = short.Parse(Request.QueryString["id"]);
            try
            {
                if (id > 0)
                {
                    implPaciente = new PacienteImpl();
                    P = implPaciente.Get(id);

                    if (P != null)
                    {
                        if (!IsPostBack)
                        {
                            txtNombre.Text = P.Name.ToString();
                            txtApellidoPaterno.Text = P.LastName.ToString();
                            txtApellidoMaterno.Text = P.SecondLastName.ToString();
                            txtFechaNacimiento.Text = P.Birthdate.ToString("yyyy-MM-dd");
                            txtCelular.Text = P.Phone.ToString();
                            txtCi.Text = P.Ci.ToString();
                            txtCorreo.Text = P.Email.ToString();
                            txtDireccion.Text = P.Addres.ToString();
                            txtLat.Text = P.Latitude.ToString();
                            txtLong.Text = P.Longitude.ToString();
                            txtMunicipio.Text = P.Municipio.ToString();
                            txtHistorial.Text = P.Historial.ToString();
                            if (P.PhotoData != null && P.PhotoData.Length > 0)
                            {
                                string base64Image = Convert.ToBase64String(P.PhotoData);
                                imgPreview.ImageUrl = "data:image/jpeg;base64," + base64Image;
                            }
                            else
                            {
                                imgPreview.ImageUrl = string.Empty;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear()
        {
            txtNombre.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtCi.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtCelular.Text = string.Empty;
            txtMunicipio.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            txtHistorial.Text = string.Empty;
            txtLat.Text = "-17.33059869950836";
            txtLong.Text = "-66.22559118521447";
        }


    }
}