using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Listado_Crud_Nurse : System.Web.UI.Page
    {
        private NurseImpl implNurse;
        private string type;
        private short id;
        private Nurse N;
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
                NurseImpl implNurse = new NurseImpl();
                DataTable dt = implNurse.SelectApproval();
                GenerarCuerpoTablaDinamica(dt);

                //LlenarTabla();

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

                    if (col.ColumnName == "id")
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
                        else if (col.ColumnName == "Titulo Profesional" || col.ColumnName == "CV")
                        {
                            string id = row["id"].ToString();
                            string tipo = (col.ColumnName == "Titulo Profesional") ? "Titulo" : "CV";
                            string linkHtml = $"<a href='WiewPdf.aspx?id={id}&type={tipo}' target='_blank'>Ver {tipo}</a>";
                            td.InnerHtml = linkHtml;
                        }
                        else if (col.ColumnName == "Año de Titulacion" || col.ColumnName == "Fecha de nacimiento")
                        {
                            td.InnerText = Convert.ToDateTime(row[col.ColumnName]).ToString("dd/MM/yyyy");
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
                linkMasInformacion.InnerHtml = "Aceptar";
                linkMasInformacion.Attributes["class"] = "btnMasInformacion";
                linkMasInformacion.HRef = $"ApprovalNurse.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Rechazar";
                LinkRechazar.Attributes["class"] = "btnMasInformacion";
                LinkRechazar.HRef = $"Listado_Crud_Nurse.aspx?id={row["ID"]}&type=D";
                tdRechazar.Controls.Add(LinkRechazar);
                tr.Controls.Add(tdRechazar);

                tbody.Controls.Add(tr);

                contador++;
            }

            tableBody.Controls.Add(tbody);
        }

        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "D")
                {
                    Delete();
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
    }
}