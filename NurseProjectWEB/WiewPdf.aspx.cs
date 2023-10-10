using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.IO;
using System.Web.UI;

namespace NurseProjectWEB
{
    public partial class WiewPdf : Page
    {
        private NurseImpl implNurse;
        private Nurse N;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (short.TryParse(Request.QueryString["id"], out short nurseId))
                {
                    LoadPdfData(nurseId, Request.QueryString["type"]);
                }
                else
                {
                    Response.Write("Enfermera no encontrada");
                }
            }
        }

        private void LoadPdfData(short id, string type)
        {
            try
            {
                implNurse = new NurseImpl();
                N = implNurse.Get(id);

                if (N != null)
                {
                    byte[] pdfData = null;
                    string contentType = "";
                    string fileName = "";

                    if (type == "Titulo" && N.LugarTitulacion != null)
                    {
                        pdfData = N.LugarTitulacion;
                        contentType = "application/pdf";
                        fileName = "nombre_del_archivo_titulo.pdf";
                    }
                    else if (type == "CV" && N.Cvc != null)
                    {
                        pdfData = N.Cvc;
                        contentType = "application/pdf";
                        fileName = "nombre_del_archivo_cv.pdf";
                    }

                    if (pdfData != null)
                    {
                        Response.Clear();
                        Response.ContentType = contentType;
                        Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
                        Response.BinaryWrite(pdfData);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("El PDF solicitado no se encontró.");
                    }
                }
                else
                {
                    Response.Write("No se encontró el registro del enfermero.");
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }
    }
}
