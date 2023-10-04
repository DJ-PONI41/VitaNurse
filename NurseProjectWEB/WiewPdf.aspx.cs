using NurseProjecDAO;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Web.UI;

namespace NurseProjectWEB
{
    public partial class WiewPdf : Page
    {
        NurseImpl implNurse;
        Nurse N;

        private short id;
        private string type;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (short.TryParse(Request.QueryString["id"], out short NurseId))
                {
                    id = NurseId;
                    LoadType();
                   
                }
                else
                {
                    //
                }
            }

           
        }

        void LoadType()
        {
            try
            {
                type = Request.QueryString["type"];

                if (type == "Titulo" )
                {
                    Get();
                }
                else if (type == "CV")
                {
                    Get();
                }
                else
                {

                    Response.Write("Tipo de PDF no válido.");
                }
            }
            catch (Exception ex)
            {

                string mensaje = ex.Message;
            }
        }

        void Get()
        {
            N = null;

            id = short.Parse(Request.QueryString["id"]);
            try
            {
                if (id > 0)
                {
                    implNurse = new NurseImpl();
                    N = implNurse.Get(id);

                    if (N != null)
                    {
                        byte[] pdfData = null;
                        string contentType = "";

                        if (type == "Titulo" && N.LugarTitulacion != null)
                        {
                            pdfData = N.LugarTitulacion;
                            contentType = "application/pdf";
                        }
                        else if (type == "CV" && N.Cvc != null)
                        {
                            pdfData = N.Cvc;
                            contentType = "application/pdf";
                        }

                        if (pdfData != null)
                        {
                            Response.Clear();
                            Response.ContentType = contentType;
                            Response.AddHeader("content-disposition", "inline; filename=nombre_del_archivo.pdf");
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
                else
                {

                    Response.Write("ID de enfermera no válido.");
                }
            }
            catch (Exception ex)
            {
                
                string mensaje = ex.Message;
            }
        }

    }
}
