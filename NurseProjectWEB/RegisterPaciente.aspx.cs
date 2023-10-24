using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class RegisterPaciente : System.Web.UI.Page
    {     


        protected void Page_Load(object sender, EventArgs e)
        {
            
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
                else if (string.IsNullOrEmpty(usuario) || !Tools.ValidateUsername(usuario))
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
                
                else
                {
                    Paciente paciente = new Paciente(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, historial);
                    PacienteImpl implPaciente = new PacienteImpl();

                    User user = new User(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, usuario, contraseña, rol);
                    UserImpl implUser = new UserImpl();

                    int result = implUser.Insert2(user, paciente);

                    if (result > 0)
                    {
                        label1.CssClass = "alert alert-success";
                        label1.Text = "El registro se ha realizado con éxito.";
                        label1.Style["display"] = "block";
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        label1.CssClass = "alert alert-danger";
                        label1.Text = "¡Error! No se pudo realizar el registro.";
                        label1.Style["display"] = "block";
                    }
                }

                
            }
            catch (Exception ex)
            {
                label1.CssClass = "alert alert-danger";
                label1.Text = "¡Error! " + ex.Message;
                label1.Style["display"] = "block";
            }
        }


    }
}