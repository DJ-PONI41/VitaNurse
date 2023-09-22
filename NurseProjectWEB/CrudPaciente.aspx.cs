﻿using NurseProjecDAO.Implementacion;
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

namespace NurseProjectWEB
{
    public partial class CrudPaciente : System.Web.UI.Page
    {
        PacienteImpl implPaciente;
        Paciente P;

        UserImpl implUser;
        User U;

        private short id;
        private string type;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Select();
                load();
                LoadType();

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
                table.Columns.Add("Fecha de nacimiento", typeof(DateTime));
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
                    //
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaSinHora = fechaNacimiento.ToString("yyyy-MM-dd");

                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                    dr["Apellido Materno"].ToString(), fechaSinHora,
                                    dr["Celular"].ToString(),dr["CI"].ToString(), dr["Correo"].ToString(), 
                                    dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                    dr["Historial Medico"].ToString(), "", "");
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();
                    string up = "<a class='btn btn-sm btn-warning' href='webAdmProviders.aspx?id=" + id + "&type=U'> Seleccionar</a>";

                    string del = "<a class='btn btn-sm btn-danger' href='webAdmProviders.aspx?id=" + id + "&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'>Borrar</i></a>";
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

                    // Obtener el objeto Customer antes de llamar al método Delete
                    P = implPaciente.Get(id);

                    if (P != null)
                    {
                        int n = implPaciente.Delete(P);
                        // Realizar cualquier acción adicional después de la eliminación
                    }
                    else
                    {
                        // El objeto Customer no existe en la base de datos, manejar el caso de error
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
                   // Get();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}