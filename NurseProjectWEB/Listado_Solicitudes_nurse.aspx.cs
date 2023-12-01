using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NurseProjectWEB
{
    public partial class Listado_Solicitudes_nurse : System.Web.UI.Page
    {
        private SolicitudImpl implSolicitud;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                implSolicitud = new SolicitudImpl();
                int idNurse = Convert.ToInt32(Session["UserID"]);
                DataTable dt = implSolicitud.Select_esp(idNurse); 

                GenerarCuerpoTablaDinamica(dt);
            }
        }

        private void GenerarCuerpoTablaDinamica(DataTable dt)
        {
            HtmlGenericControl tbody = new HtmlGenericControl("tbody");

            int contador = 1;

            foreach (DataRow row in dt.Rows)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");

                foreach (DataColumn col in dt.Columns)
                {
                    HtmlGenericControl td = new HtmlGenericControl("td");

                    if (col.ColumnName == "ID")
                    {
                        td.InnerText = contador.ToString();
                    }
                    else
                    {
                        if (col.ColumnName == "Estado")
                        {
                            int estado = Convert.ToInt32(row[col.ColumnName]);

                            if (estado == 0)
                            {
                                td.InnerHtml = "<p class=\"status cancelled\">Rechazado</p>";
                            }
                            else if (estado == 1)
                            {
                                td.InnerHtml = "<p class=\"status delivered\">Aceptado</p>";
                            }
                            else if (estado == 2)
                            {
                                td.InnerHtml = "<p class=\"status pending\">Pendiente</p>";
                            }
                        }
                        else
                        {
                            td.InnerText = row[col.ColumnName].ToString();
                        }
                    }

                    tr.Controls.Add(td);
                }
                HtmlGenericControl tdMasInformacion = new HtmlGenericControl("td");
                HtmlAnchor linkMasInformacion = new HtmlAnchor();
                linkMasInformacion.InnerHtml = "Ver Detalles";
                linkMasInformacion.Attributes["class"] = "btnMasInformacion";
                linkMasInformacion.HRef = $"Solicitud_detalles.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);


                tbody.Controls.Add(tr);

                contador++;
            }

            tableBody.Controls.Add(tbody);
        }


        private void BtnMasInformacion_Click(object sender, EventArgs e)
        {
            Button btnMasInformacion = (Button)sender;
            string idSolicitud = btnMasInformacion.CommandArgument;

            Response.Redirect($"Solicitud_detalles.aspx?id={idSolicitud}");
        }

    }
}