using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Formulario_visita : System.Web.UI.Page
    {
        private SolicitudImpl implSolicitud;
        private Solicitud S;
        private NurseImpl implNurse;
        private string type;
        private Nurse N;
        private short id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string userRole = Session["UserRole"].ToString();
                if (userRole != "Paciente")
                {
                    Response.Write("Acceso no autorizado. Debes tener el rol de Paciente para acceder a esta página.");
                }
            }

            if (!IsPostBack)
            {
                load();
            }
        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            InsertSolicitud();
            Response.Redirect("Agendar_visita.aspx");
        }
        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "U")
                {
                    Get();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    txtNameNurse.Text = N.Name.ToString();
                    txtEspecialidad.Text = N.Especialidad.ToString();
                }
            }
        }

        private void InsertSolicitud()
        {
            try
            {

                string detalles = txtMessage.Text;
                string latitud = txtLat.Text;
                string longitud = txtLong.Text;
                id = short.Parse(Request.QueryString["id"]);
                DateTime Fecha_solicitud;
                DateTime.TryParse(txtDateVisita.Text, out Fecha_solicitud);
                int id_pas = Convert.ToInt32(Session["UserID"]);

                S = new Solicitud(latitud, longitud, Fecha_solicitud, detalles, id_pas, id);
                implSolicitud = new SolicitudImpl();
                int n = implSolicitud.Insert(S);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}