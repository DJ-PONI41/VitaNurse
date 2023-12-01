using iTextSharp.text.pdf;
using iTextSharp.text;
using NurseProjecDAO.Implementacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NurseProjectWEB
{
    public partial class SolicitudReport : System.Web.UI.Page
    {
        SolicitudImpl implSolicitud;

        private int estado_rep;
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
                estado_rep = 0;
                SolicitudImpl implSolicitud = new SolicitudImpl();
                DataTable datosDesdeBD = implSolicitud.SelectReport();

                GenerarCuerpoTablaDinamica(datosDesdeBD);

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

                HtmlGenericControl tdCounter = new HtmlGenericControl("td");
                tdCounter.InnerText = contador.ToString();
                tr.Controls.Add(tdCounter);

                foreach (DataColumn col in dt.Columns)
                {
                    HtmlGenericControl td = new HtmlGenericControl("td");

                    if (col.ColumnName == "Estado")
                    {
                        string estado = (string)row[col.ColumnName];

                        if (estado == "Rechazado")
                        {
                            td.InnerHtml = "<p class=\"status cancelled\">Rechazado</p>";
                        }
                        else if (estado == "Aceptado")
                        {
                            td.InnerHtml = "<p class=\"status delivered\">Aceptado</p>";
                        }
                        else if (estado == "Pendiente")
                        {
                            td.InnerHtml = "<p class=\"status pending\">Pendiente</p>";
                        }
                    }
                    else
                    {
                        td.InnerText = row[col.ColumnName].ToString();
                    }

                    tr.Controls.Add(td);
                }

                tbody.Controls.Add(tr);

                contador++;
            }

            tableBody.Controls.Add(tbody);
        }

        protected void btnExportPDFA_Click(object sender, EventArgs e)
        {
            exportReportbyPDFA();
        }

        protected void btnExportPDFR_Click(object sender, EventArgs e)
        {
            exportReportbyPDFR();
        }

        protected void btnExportPDFP_Click(object sender, EventArgs e)
        {
            exportReportbyPDFP();
        }

        protected void btnRecargar_tabla_Click(object sender, EventArgs e)
        {
            ExportA.Visible = false;
            ExportP.Visible = false;
            ExportR.Visible = false;
            ExportPDF.Visible = true;
            estado_rep = 0;
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable datosDesdeBD = implSolicitud.SelectReport();

            GenerarCuerpoTablaDinamica(datosDesdeBD);
        }
        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            exportReportbyPDF();
        }
        protected void Filt_acept_Click(object sender, EventArgs e)
        {
            ExportA.Visible = true;
            ExportP.Visible = false;
            ExportR.Visible = false;
            ExportPDF.Visible = false;
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable datosDesdeBD = implSolicitud.SelectReportAceptados();

            GenerarCuerpoTablaDinamica(datosDesdeBD);
        }

        protected void Filt_Rech_Click(object sender, EventArgs e)
        {
            ExportA.Visible = false;
            ExportP.Visible = false;
            ExportR.Visible = true;
            ExportPDF.Visible = false;
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable datosDesdeBD = implSolicitud.SelectReportRechazados();

            GenerarCuerpoTablaDinamica(datosDesdeBD);
        }

        protected void Filt_Pend_Click(object sender, EventArgs e)
        {
            ExportA.Visible = false;
            ExportP.Visible = true;
            ExportR.Visible = false;
            ExportPDF.Visible = false;
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable datosDesdeBD = implSolicitud.SelectReportPendientes();

            GenerarCuerpoTablaDinamica(datosDesdeBD);
        }

        void exportReportbyPDF()
        {
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable dt = implSolicitud.SelectReport2();

            Document doc = new Document(PageSize.LETTER);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);

            pdfTable.DefaultCell.Border = PdfPCell.BOX;
            pdfTable.DefaultCell.BorderColor = BaseColor.GRAY;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont customFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
            iTextSharp.text.Font customTitleFont = new iTextSharp.text.Font(customFont, 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

            PdfPCell headerCell;

           
            foreach (DataColumn column in dt.Columns)
            {
                headerCell = new PdfPCell(new Phrase(column.ColumnName, customTitleFont));
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY; 
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(headerCell);
            }

            
            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre del Paciente"].ToString(), BaseColor.WHITE)); 
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre de la Enfermera"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Detalles de Solicitud"].ToString(), BaseColor.WHITE));

               
                BaseColor estadoColor = ObtenerColorPorEstado(row["Estado"].ToString());
                pdfTable.AddCell(CrearCeldaConColor(row["Estado"].ToString(), estadoColor));
            }

            doc.Add(pdfTable);

           
            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Informe de Solicitudes", customTitleFont));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString(), customTitleFont));
            doc.Add(new Paragraph(" "));

            doc.Close();

            
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSolicitudes.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        void exportReportbyPDFA()
        {
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable dt = implSolicitud.SelectReportAceptados();

            Document doc = new Document(PageSize.LETTER);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);

            pdfTable.DefaultCell.Border = PdfPCell.BOX;
            pdfTable.DefaultCell.BorderColor = BaseColor.GRAY;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont customFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
            iTextSharp.text.Font customTitleFont = new iTextSharp.text.Font(customFont, 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

            PdfPCell headerCell;


            foreach (DataColumn column in dt.Columns)
            {
                headerCell = new PdfPCell(new Phrase(column.ColumnName, customTitleFont));
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(headerCell);
            }


            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre del Paciente"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre de la Enfermera"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Detalles de Solicitud"].ToString(), BaseColor.WHITE));


                BaseColor estadoColor = ObtenerColorPorEstado(row["Estado"].ToString());
                pdfTable.AddCell(CrearCeldaConColor(row["Estado"].ToString(), estadoColor));
            }

            doc.Add(pdfTable);


            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Informe de Solicitudes", customTitleFont));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString(), customTitleFont));
            doc.Add(new Paragraph(" "));

            doc.Close();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSolicitudes.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        void exportReportbyPDFP()
        {
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable dt = implSolicitud.SelectReportPendientes();

            Document doc = new Document(PageSize.LETTER);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);

            pdfTable.DefaultCell.Border = PdfPCell.BOX;
            pdfTable.DefaultCell.BorderColor = BaseColor.GRAY;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont customFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
            iTextSharp.text.Font customTitleFont = new iTextSharp.text.Font(customFont, 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

            PdfPCell headerCell;


            foreach (DataColumn column in dt.Columns)
            {
                headerCell = new PdfPCell(new Phrase(column.ColumnName, customTitleFont));
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(headerCell);
            }


            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre del Paciente"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre de la Enfermera"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Detalles de Solicitud"].ToString(), BaseColor.WHITE));


                BaseColor estadoColor = ObtenerColorPorEstado(row["Estado"].ToString());
                pdfTable.AddCell(CrearCeldaConColor(row["Estado"].ToString(), estadoColor));
            }

            doc.Add(pdfTable);


            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Informe de Solicitudes", customTitleFont));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString(), customTitleFont));
            doc.Add(new Paragraph(" "));

            doc.Close();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSolicitudes.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        void exportReportbyPDFR()
        {
            SolicitudImpl implSolicitud = new SolicitudImpl();
            DataTable dt = implSolicitud.SelectReportRechazados();

            Document doc = new Document(PageSize.LETTER);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            PdfPTable pdfTable = new PdfPTable(dt.Columns.Count);

            pdfTable.DefaultCell.Border = PdfPCell.BOX;
            pdfTable.DefaultCell.BorderColor = BaseColor.GRAY;
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont customFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.EMBEDDED);
            iTextSharp.text.Font customTitleFont = new iTextSharp.text.Font(customFont, 12, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);

            PdfPCell headerCell;


            foreach (DataColumn column in dt.Columns)
            {
                headerCell = new PdfPCell(new Phrase(column.ColumnName, customTitleFont));
                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(headerCell);
            }


            foreach (DataRow row in dt.Rows)
            {
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre del Paciente"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Nombre de la Enfermera"].ToString(), BaseColor.WHITE));
                pdfTable.AddCell(CrearCeldaConColor(row["Detalles de Solicitud"].ToString(), BaseColor.WHITE));


                BaseColor estadoColor = ObtenerColorPorEstado(row["Estado"].ToString());
                pdfTable.AddCell(CrearCeldaConColor(row["Estado"].ToString(), estadoColor));
            }

            doc.Add(pdfTable);


            doc.Add(new Paragraph(" "));
            doc.Add(new Paragraph("Informe de Solicitudes", customTitleFont));
            doc.Add(new Paragraph("Fecha: " + DateTime.Now.ToString(), customTitleFont));
            doc.Add(new Paragraph(" "));

            doc.Close();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteSolicitudes.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }


        private PdfPCell CrearCeldaConColor(string texto, BaseColor colorFondo)
        {
            PdfPCell cell = new PdfPCell(new Phrase(texto));
            cell.BackgroundColor = colorFondo;
            cell.Padding = 5;
            return cell;
        }

        
        private BaseColor ObtenerColorPorEstado(string estado)
        {
            switch (estado)
            {
                case "Aceptado":
                    return BaseColor.GREEN; 
                case "Rechazado":
                    return BaseColor.RED; 
                case "Pendiente":
                default:
                    return BaseColor.YELLOW; 
            }
        }

        
    }
}