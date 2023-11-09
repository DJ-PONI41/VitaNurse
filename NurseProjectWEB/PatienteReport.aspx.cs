using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using System;
using System.Data;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace NurseProjectWEB
{
    public partial class PatienteReport : System.Web.UI.Page
    {
        PacienteImpl implPaciente;
        Paciente P;
        string names;
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

            Select();
        }


        private void Select()
        {
            try
            {
                implPaciente = new PacienteImpl();
                DataTable dt = implPaciente.SelectReport();
                DataTable table = new DataTable("Paciente");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Fecha de nacimiento", typeof(string));
                table.Columns.Add("Celular", typeof(string));
                table.Columns.Add("CI", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Direccion", typeof(string));
                table.Columns.Add("Rol", typeof(string));
                table.Columns.Add("Historial Medico", typeof(string));


                foreach (DataRow dr in dt.Rows)
                {
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaFormateada = fechaNacimiento.ToString("yyyy-MM-dd");
                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                    dr["Apellido Materno"].ToString(), fechaFormateada,
                                    dr["Celular"].ToString(), dr["CI"].ToString(), dr["Correo"].ToString(),
                                    dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                    dr["Historial Medico"].ToString());
                }

                GridDat.DataSource = table;
                GridDat.DataBind();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void exportReportbyPDF()
        {
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


        private void SelectBuscador(string name)
        {
            try
            {
                implPaciente = new PacienteImpl();
                DataTable dt = implPaciente.SelectReportPaciente(name);
                DataTable table = new DataTable("Paciente");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Apellido Paterno", typeof(string));
                table.Columns.Add("Apellido Materno", typeof(string));
                table.Columns.Add("Fecha de nacimiento", typeof(string));
                table.Columns.Add("Celular", typeof(string));
                table.Columns.Add("CI", typeof(string));
                table.Columns.Add("Correo", typeof(string));
                table.Columns.Add("Direccion", typeof(string));
                table.Columns.Add("Rol", typeof(string));
                table.Columns.Add("Historial Medico", typeof(string));


                foreach (DataRow dr in dt.Rows)
                {
                    DateTime fechaNacimiento = (DateTime)dr["Fecha de nacimiento"];
                    string fechaFormateada = fechaNacimiento.ToString("yyyy-MM-dd");
                    table.Rows.Add(dr["Nombre"].ToString(), dr["Apellido Paterno"].ToString(),
                                    dr["Apellido Materno"].ToString(), fechaFormateada,
                                    dr["Celular"].ToString(), dr["CI"].ToString(), dr["Correo"].ToString(),
                                    dr["Direccion"].ToString(), dr["Rol"].ToString(),
                                    dr["Historial Medico"].ToString());
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


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string name = txtNombre.Text.Trim();

            if (name != "")
            {
                SelectBuscador(name);
            }
            else
            {
                Select();
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            exportReportbyPDF();
        }





    }
}