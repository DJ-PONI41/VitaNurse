using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
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

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtApellidoMaterno.Text;
            string ci = txtCi.Text;
            DateTime fechaNamiento = TxtFechaNacimiento.SelectedDate;
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
            
            
            try
            {
                P = new Paciente(nombre, apellidoPaterno, apellidoMaterno,fechaNamiento,celular,ci,correo,direccion,latitud,longitud,municipio,historial);
                implPaciente = new PacienteImpl();
                int n = implPaciente.Insert(P);

                U =  new User(nombre, apellidoPaterno, apellidoMaterno, fechaNamiento, celular, ci, correo, direccion, latitud, longitud, municipio, usuario,contraseña,rol);
                implUser = new UserImpl();
                int u = implUser.Insert(U);
                if (n > 0)
                {
                    
                    label1.Text = "El registro fue exitoso";
                }
                else
                {
                    label1.Text = "La inserción no tuvo éxito";
                }
            }
            catch (Exception ex )
            {

                throw ex;
            }
        }
    }
}