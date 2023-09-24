using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
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
        PacienteImpl implPaciente;
        Paciente P;

        UserImpl implUser;
        User U;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {

            //obtener datos de la imagene
            int img = fileUpload.PostedFile.ContentLength;
            byte[] ImgOriginal = new byte[img];
            fileUpload.PostedFile.InputStream.Read(ImgOriginal, 0, img);

            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtApellidoMaterno.Text;

            string ci = txtCi.Text;
            //DateTime fechaNamiento = TxtFechaNacimiento.SelectedDate;
            DateTime fechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string direccion = txtDireccion.Text;
            string latitud = txtLat.Text;
            string longitud = txtLong.Text;
            string celular = txtCelular.Text;
            string municipio = txtMunicipio.Text;
            string correo = txtCorreo.Text;
            string usuario = txtUsuario.Text;
            string contraseña = txtContrasena.Text;
            //string repetirContrasena = txtRepetirContrasena.Text;
            string historial = txtHistorial.Text;
            string rol = "Paciente";

            if (DateTime.TryParseExact(txtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
            {
                try
                {
                    P = new Paciente(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, historial);
                    implPaciente = new PacienteImpl();
                    //int n = implPaciente.Insert(P);

                    U = new User(nombre, apellidoPaterno, apellidoMaterno, ImgOriginal, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, usuario, contraseña, rol);
                    implUser = new UserImpl();
                    int u = implUser.Insert2(U, P);
                    if (u > 0)
                    {

                        label1.CssClass = "alert alert-success";
                        label1.Text = "El registro se ha realizado con éxito.";
                        label1.Style["display"] = "block";
                        Response.Redirect("Login.aspx");
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
        }


    }
}