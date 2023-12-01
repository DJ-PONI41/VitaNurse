using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Solicitud_detalles : System.Web.UI.Page
    {
        private SolicitudImpl implSolicitud;
        private Solicitud S;
        private PacienteImpl implPaciente;
        private Paciente Pas;
        private NurseImpl implNurse;
        private string type;
        private Nurse N;
        private short id;
        private short id_pas;
        private double lati;
        private double longi;
        private const double EarthRadius = 6371;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                string userRole = Session["UserRole"].ToString();
                if (userRole != "Enfermera")
                {
                    Response.Write("Acceso no autorizado. Debes tener el rol de Enfermera para acceder a esta página.");
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
        }
        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            RechasarSolicitud();
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
                string errorMessage = $"Error al obtener la solicitud con ID {id}: {ex.Message}";
                throw new Exception(errorMessage, ex);
            }
        }

        private void Get()
        {
            S = null;
            Pas = null;
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                implSolicitud = new SolicitudImpl();
                S = implSolicitud.Get(id);
                if (S != null && !IsPostBack)
                {
                    id_pas = (short)S.IdPaciente;
                    implPaciente = new PacienteImpl();
                    Pas = implPaciente.Get(id_pas);
                    txtNamePaciente.Text = Pas.Name;
                    txtMunicipio.Text = Pas.Municipio;
                    lati = Double.Parse(Pas.Latitude);
                    longi = Double.Parse(Pas.Longitude);
                    txtMessage.Text = S.Detalles;
                    txtdireccion.Text = Pas.Addres;
                    txtDateVisita.Text = S.FechaHora != null ? S.FechaHora.ToString("yyyy-MM-dd") : string.Empty;
                    //txtMunicipio.Text = S.Municipio.ToString();
                    //txtNamePaciente.Text = S.Name.ToString();
                }
            }
        }

        private void InsertSolicitud()
        {
            try
            {

                string latitud = txtLat.Text;
                string longitud = txtLong.Text;
                double dLat = Double.Parse(latitud) - lati;
                double dLon = Double.Parse(longitud) - longi;

                double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lati) * Math.Cos(Double.Parse(latitud)) * Math.Pow(Math.Sin(dLon / 2), 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double distance = EarthRadius * c;

                string distancia_kilom = distance.ToString() + " km";


                id = short.Parse(Request.QueryString["id"]);

                S = new Solicitud(id, latitud, longitud, distancia_kilom);
                implSolicitud = new SolicitudImpl();
                int n = implSolicitud.Update(S);

                Response.Redirect("Listado_Solicitudes_nurse.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RechasarSolicitud()
        {
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                try
                {
                    implSolicitud = new SolicitudImpl();
                    S = implSolicitud.Get(id);
                    if (S != null)
                    {
                        int n = implSolicitud.Delete(S);
                    }
                    Response.Redirect("Listado_Solicitudes_nurse.aspx");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}