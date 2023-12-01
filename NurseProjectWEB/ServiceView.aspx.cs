using NurseProjecDAO.Implementacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class ServiceView : System.Web.UI.Page
    {
        ServiceImpl implService;
        protected void Page_Load(object sender, EventArgs e)
        {
            Select();
        }
        public void Select()
        {
            try
            {
                // Suponiendo que implService es una instancia de ServiceImpl (reemplázalo con tu lógica específica)
                implService = new ServiceImpl();

                DataTable table = new DataTable("Service");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Descripción", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    table.Rows.Add(
                    );
                }

                GridDat.DataSource = table;
                GridDat.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}