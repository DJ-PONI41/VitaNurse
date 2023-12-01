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

namespace NurseProjectWEB
{
    public partial class NurseReport : System.Web.UI.Page
    {
        private NurseImpl implNurse;
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
            NurseImpl implNurse = new NurseImpl();
            DataTable dt = implNurse.SelectReport();
           
            foreach (DataRow row in dt.Rows)
            {
                TableRow tableRow = new TableRow();

                // Agrega celdas con los datos de cada columna
                foreach (DataColumn column in dt.Columns)
                {
                    TableCell cell = new TableCell();

                    if (column.ColumnName == "id")
                    {
                        // Si la columna es 'id', usa el valor real pero no lo muestra
                        HiddenField hiddenField = new HiddenField();
                        hiddenField.ID = "hiddenId_" + row["id"];
                        hiddenField.Value = row["id"].ToString();
                        form1.Controls.Add(hiddenField);
                    }
                    else if (column.ColumnName == "Titulo Profesional")
                    {
                        // Si la columna es 'Titulo Profesional', agrega un enlace con el texto 'Ver Titulo'
                        string id = row["id"].ToString();
                        string linkHtml = $"<a href='WiewPdf.aspx?id={id}&type=Titulo' target='_blank'>Ver Titulo</a>";
                        cell.Text = linkHtml;
                    }
                    else if (column.ColumnName == "CV")
                    {
                        // Si la columna es 'CV', agrega un enlace con el texto 'Ver CV'
                        string id = row["id"].ToString();
                        string linkHtml = $"<a href='WiewPdf.aspx?id={id}&type=CV' target='_blank'>Ver CV</a>";
                        cell.Text = linkHtml;
                    }
                    else if (column.DataType == typeof(DateTime))
                    {
                        cell.Text = ((DateTime)row[column.ColumnName]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        cell.Text = row[column.ColumnName].ToString();
                    }

                    tableRow.Cells.Add(cell);
                }

                // Agrega la fila a la tabla
                tableBody.Controls.Add(tableRow);
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
            implNurse = new NurseImpl();
            string nombreABuscar = Cuadro_busqueda.Value.Trim();

            DataTable dt = implNurse.SelectReportNurse(nombreABuscar);

            tableBody.Controls.Clear();

            LlenarTabla2(dt);
        }

        private void Select(DateTime Inicio, DateTime Final)
        {
            try
            {
                implNurse = new NurseImpl();
                DataTable dt = implNurse.SelectReportNurseDate( Inicio,  Final);
                DataTable table = new DataTable("Nurse");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Cantidad Pacientes Atendidos", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    

                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                   dr["Apellido Materno"].ToString(), dr["Correo"].ToString(),
                                   dr["Cantidad Pacientes Atendidos"].ToString());
                }

                GridDat.DataSource = table;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["Id"].ToString();                   
                    GridDat.Rows[i].Attributes["data-id"] = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        void exportReportbyPDF()
        {
            DataTable dt = implNurse.SelectReport2();

            Document doc = new Document(PageSize.LETTER);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            int numColumns = 11; // Define el número de columnas que deseas en tu informe

            PdfPTable pdfTable = new PdfPTable(numColumns);
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

            foreach (DataRow row in dt.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    if (item is DateTime)
                    {
                        // Si el valor es una fecha formatearlo como fecha
                        PdfPCell cell = new PdfPCell(new Phrase(((DateTime)item).ToString("dd/MM/yyyy")));
                        cell.BackgroundColor = BaseColor.WHITE;
                        cell.Padding = 5;
                        pdfTable.AddCell(cell);
                    }
                    else
                    {
                        // Si no es una fecha simplemente agregarlo como texto
                        PdfPCell cell = new PdfPCell(new Phrase(item.ToString()));
                        cell.BackgroundColor = BaseColor.WHITE;
                        cell.Padding = 5;
                        pdfTable.AddCell(cell);
                    }
                }
            }

            doc.Add(pdfTable);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ReporteEnfermeras.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();

        }

        protected void btnFecha_Click(object sender, EventArgs e)
        {
            DateTime Inicio, Fin;
            if (!DateTime.TryParse(txtInicio.Text, out Inicio) || Inicio > DateTime.Now)
            {
               
            }
            else if (!DateTime.TryParse(txtFin.Text, out Fin) || Fin > DateTime.Now)
            {
               
            }
            else
            {
                Select(Inicio, Fin);
            }

        }
    }
}