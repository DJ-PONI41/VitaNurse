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
            
            Select();
            load();
            LoadType();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidoPaterno = txtApellidoPaterno.Text;
            string apellidoMaterno = txtApellidoMaterno.Text;
            string ci = txtCi.Text;
            DateTime fechaNacimiento = Convert.ToDateTime(TxtFechaNacimiento.Text);
            string direccion = txtDireccion.Text;
            string latitud = txtLat.Text;
            string longitud = txtLong.Text;
            string celular = txtCelular.Text;
            string municipio = txtMunicipio.Text;
            string correo = txtCorreo.Text;
            string usuario = txtUsuario.Text;
            string contraseña = txtContrasena.Text;
            string historial = txtHistorial.Text;
            string rol = cbnRol.SelectedValue;


            try
            {

                P = new Paciente(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, historial);
                implPaciente = new PacienteImpl();
                int n = implPaciente.Insert(P);

                U = new User(nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, celular, ci, correo, direccion, latitud, longitud, municipio, usuario, contraseña, rol);
                implUser = new UserImpl();
                int u = implUser.Insert(U);

                if (n > 0 && u > 0)
                {
                    label.Text = "El registro fue exitoso";
                }
                else
                {
                    label.Text = "La inserción no tuvo éxito";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {

        }


        void Select()
        {
            try
            {
                implPaciente = new PacienteImpl();
                DataTable dt = implPaciente.Select();
                DataTable table = new DataTable("Paciente");
                table.Columns.Add("", typeof(string));
                table.Columns.Add("", typeof(string));
                table.Columns.Add("", typeof(string));
                table.Columns.Add("", typeof(string));
                table.Columns.Add("Seleccionar", typeof(string));
                table.Columns.Add("Borrar", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    table.Rows.Add(dr[""].ToString(), dr[""].ToString(), dr[""].ToString(), dr[""].ToString(), "", "");
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();
                    string up = "<a class='btn btn-sm btn-warning' href='webAdmProviders.aspx?id=" + id + "&type=U'> Seleccionar</a>";

                    string del = "<a class='btn btn-sm btn-danger' href='webAdmProviders.aspx?id=" + id + "&type=D' onclick='return ConfirmDelete();'> <i class='fas fa-trash' style='background:#FF0000;'></i></a>";
                    GridDat.Rows[i].Cells[4].Text = up;
                    GridDat.Rows[i].Cells[5].Text = del;
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