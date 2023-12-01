using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class Crud_Listado_nuevo_Paciente : System.Web.UI.Page
    {
        private PacienteImpl implPaciente;
        private string type;
        private short id;
        private Paciente P;
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
                PacienteImpl implPaciente = new PacienteImpl();
                DataTable dt = implPaciente.Select();
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

                    if (col.ColumnName == "id")
                    {
                        td.InnerText = contador.ToString();
                    }
                    else
                    {
                        if (col.ColumnName == "Año de Titulacion" || col.ColumnName == "Fecha de nacimiento")
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
                linkMasInformacion.InnerHtml = "Editar";
                linkMasInformacion.Attributes["class"] = "btnMasInformacion";
                linkMasInformacion.HRef = $"CrudPaciente.aspx?id={row["ID"]}&type=U";
                tdMasInformacion.Controls.Add(linkMasInformacion);
                tr.Controls.Add(tdMasInformacion);

                HtmlGenericControl tdRechazar = new HtmlGenericControl("td");
                HtmlAnchor LinkRechazar = new HtmlAnchor();
                LinkRechazar.InnerHtml = "Eliminar";
                LinkRechazar.Attributes["class"] = "btnMasInformacion";
                LinkRechazar.HRef = $"Crud_Listado_nuevo_Paciente.aspx?id={row["ID"]}&type=D";
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
        
        protected void Registrar_nuevo_paciente_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrudPaciente.aspx");
        }
    }
}