using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Data;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace NurseProjectWEB
{
    public partial class PatienteReport : System.Web.UI.Page
    {
        PacienteImpl implPaciente;      
       
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
                LlenarTabla();
            }
        }

        private void LlenarTabla()
        {
            PacienteImpl implPaciente = new PacienteImpl(); 
            DataTable datosDesdeBD = implPaciente.SelectReport();

            int contador = 1;

            foreach (DataRow fila in datosDesdeBD.Rows)
            {
                TableRow tr = new TableRow();

                TableCell tdContador = new TableCell();
                tdContador.Text = contador.ToString();
                tr.Cells.Add(tdContador);

                foreach (DataColumn columna in datosDesdeBD.Columns)
                {
                    if (columna.ColumnName != "id")
                    {
                        TableCell td = new TableCell();
                        td.Text = fila[columna.ColumnName].ToString();
                        tr.Cells.Add(td);
                    }
                }

                contador++;
                tableBody.Controls.Add(tr);
            }
        }

        private void LlenarTabla2(DataTable datosDesdeBD)
        {
            int contador = 1;

            foreach (DataRow fila in datosDesdeBD.Rows)
            {
                TableRow tr = new TableRow();

                TableCell tdContador = new TableCell();
                tdContador.Text = contador.ToString();
                tr.Cells.Add(tdContador);

                foreach (DataColumn columna in datosDesdeBD.Columns)
                {
                    if (columna.ColumnName != "id")
                    {
                        TableCell td = new TableCell();
                        td.Text = fila[columna.ColumnName].ToString();
                        tr.Cells.Add(td);
                    }
                }

                contador++;

                tableBody.Controls.Add(tr);
            }
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            exportReportbyPDF();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            implPaciente = new PacienteImpl();
            string nombreABuscar = Cuadro_busqueda.Value.Trim();

            DataTable dt = implPaciente.SelectReportPaciente(nombreABuscar);

            tableBody.Controls.Clear();

            LlenarTabla2(dt);
        }
     
        void exportReportbyPDF()
        {
            implPaciente = new PacienteImpl();
            DataTable dt = implPaciente.SelectReport2();

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
                headerCell.BackgroundColor = BaseColor.WHITE;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(headerCell);
            }


            Paragraph emptySpaceBeforeTitle = new Paragraph(" ");
            emptySpaceBeforeTitle.SpacingBefore = 20f;
            doc.Add(emptySpaceBeforeTitle);

            Paragraph titleParagraph = new Paragraph("Informe de Pacientes", customTitleFont);
            titleParagraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(titleParagraph);

            Paragraph dateParagraph = new Paragraph("Fecha: " + DateTime.Now.ToString(), customTitleFont);
            dateParagraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(dateParagraph);


            Paragraph emptySpaceAfterTitle = new Paragraph(" ");
            emptySpaceAfterTitle.SpacingAfter = 20f;
            doc.Add(emptySpaceAfterTitle);


            foreach (DataRow row in dt.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                    cell.BackgroundColor = BaseColor.WHITE;
                    cell.Padding = 5;
                    pdfTable.AddCell(cell);
                }
            }

            doc.Add(pdfTable);
            doc.Close();


            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReportePacientes.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

    }
}