using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Model;
using NurseProjecDAO;
using NurseProjecDAO.Tools;
using System.Data;

namespace NurseProjectWEB
{
    public partial class Services : System.Web.UI.Page
    {
        private ServiceImpl implService;
        private Service S;
        private short id;
        private string type;
        string script = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OcultarForm", "ocultarForm();", true);
            }
            else
            {
                string userRole = Session["UserRole"].ToString();
                if (userRole != "Administrador")
                {
                    Response.Write("Acceso no autorizado. Debes tener el rol de Administrador para acceder a esta página.");
                }
                if (!IsPostBack)
                {
                    load();
                    LoadType();
                    Select();
                }
            }


        }
        public void Select()
        {
            try
            {
                // Suponiendo que implService es una instancia de ServiceImpl (reemplázalo con tu lógica específica)
                implService = new ServiceImpl();
                DataTable dt = implService.Select(); // Asegúrate de tener un método Select en tu lógica de Service

                DataTable table = new DataTable("Service");
                table.Columns.Add("Nombre", typeof(string));
                table.Columns.Add("Descripción", typeof(string));
                table.Columns.Add("Precio", typeof(decimal));
                table.Columns.Add("Estado", typeof(byte));
                table.Columns.Add("Seleccionar", typeof(string));
                table.Columns.Add("Borrar", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    table.Rows.Add(
                        dr["name"].ToString(),
                        dr["description"].ToString(),
                        Convert.ToDecimal(dr["price"]),
                        Convert.ToByte(dr["state"]),
                        "", ""
                    );
                }

                GridDat.DataSource = dt;
                GridDat.DataBind();

                for (int i = 0; i < GridDat.Rows.Count; i++)
                {
                    string id = dt.Rows[i]["id"].ToString();
                    string up = $"<a class='btn btn-sm btn-warning' href='Services.aspx?id={id}&type=U'> Seleccionar</a>";
                    string del = $"<a class='btn btn-sm btn-danger' href='Services.aspx?id={id}&type=D' onclick='return ConfirmDelete();'> Eliminar</a>";
                    GridDat.Rows[i].Cells[4].Text = up;
                    GridDat.Rows[i].Cells[5].Text = del;
                    GridDat.Rows[i].Attributes["data-id"] = id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = Tools.EliminarEspacios(txtName.Text);
            string desc = Tools.EliminarEspacios(txtDescription.Text);
            double price = double.Parse(txtPrice.Text);

            S = new Service(nombre, desc, price, 1);

            implService = new ServiceImpl();
            int n = implService.Insert(S);

            if (n > 0)
            {
                ShowMessage("El registro se ha realizado con éxito.", "success");
                Clear();
                Select();
            }
            else
            {
                ShowMessage("¡Error! No se pudo realizar el registro.", "danger");
            }
        }

        private void ShowMessage(string message, string cssClass)
        {
            label1.CssClass = $"alert alert-{cssClass}";
            label1.Text = message;
            label1.Style["display"] = "block";
        }

        private void Clear()
        {
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }
        private void LoadType()
        {
            type = Request.QueryString["type"];
            if (type == "U")
            {
                btnAtras.Visible = true;
                btnUpdate.Visible = true;
                btnRegistrar.Visible = false;
                Get();
            }
        }
        private void load()
        {
            try
            {
                type = Request.QueryString["type"];
                if (type == "D")
                {
                    Delete();
                    Select();
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
                    implService = new ServiceImpl();
                    S = implService.Get(id);
                    if (S != null)
                    {
                        int n = implService.Delete(S);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("Services.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                short id = short.Parse(Request.QueryString["id"]);
                implService = new ServiceImpl();


                string nombre = Tools.EliminarEspacios(txtName.Text);
                string desc = Tools.EliminarEspacios(txtDescription.Text);
                double price = double.Parse(txtPrice.Text);

                S = new Service(id, nombre, desc, price, 1);

                implService = new ServiceImpl();
                int n = implService.Insert(S);

                if (n > 0)
                {
                    ShowMessage("La actualizacion se ha realizado con éxito.", "success");
                    Clear();
                    Select();
                }
                else
                {
                    ShowMessage("¡Error! No se pudo realizar la actualizacion.", "danger");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void Get()
        {
            S = null;
            id = short.Parse(Request.QueryString["id"]);
            if (id > 0)
            {
                implService = new ServiceImpl();
                S = implService.Get(id);
                if (S != null && !IsPostBack)
                {
                    txtName.Text = S.Name.ToString();
                    txtDescription.Text = S.Description.ToString();
                    txtPrice.Text = S.Price.ToString();
                }
            }
        }
    }
}