using NurseProjecDAO.Implementacion;
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
    public partial class Agendar_visita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Crear una instancia de la implementación de Nurse (NurseImpl)
                NurseImpl implNurse = new NurseImpl();

                // Obtener datos de la base de datos
                DataTable dt = implNurse.Select();

                // Recorrer las filas del DataTable y construir las tarjetas HTML dinámicamente
                foreach (DataRow row in dt.Rows)
                {
                    var cardArticle = new HtmlGenericControl("article");
                    cardArticle.Attributes["class"] = "card__article";

                    var cardImg = new HtmlImage();
                    cardImg.Src = "assets/img/landscape-2.png";
                    cardImg.Alt = "image";
                    cardImg.Attributes["class"] = "card__img";

                    var cardDataDiv = new HtmlGenericControl("div");
                    cardDataDiv.Attributes["class"] = "card__data";

                    // Obtener la especialidad y el nombre desde las columnas del DataTable
                    var especialidad = row["Especialidad"].ToString();
                    var nombre = row["Nombre"].ToString();
                    var id = row["id"].ToString();

                    var descriptionSpan = new HtmlGenericControl("span");
                    descriptionSpan.Attributes["class"] = "card__description";
                    descriptionSpan.InnerText = "Especialidad: " + especialidad;

                    var titleH2 = new HtmlGenericControl("h2");
                    titleH2.Attributes["class"] = "card__title";
                    titleH2.InnerText = "Nombre: " + nombre;

                    var buttonA = new HtmlAnchor();
                    buttonA.HRef = $"Formulario_visita.aspx?id={id}&type=U";
                    buttonA.InnerText = "Agendar";
                    buttonA.Attributes["class"] = "card__button";

                    cardDataDiv.Controls.Add(descriptionSpan);
                    cardDataDiv.Controls.Add(titleH2);
                    cardDataDiv.Controls.Add(buttonA);

                    cardArticle.Controls.Add(cardImg);
                    cardArticle.Controls.Add(cardDataDiv);

                    flashCardContainer.Controls.Add(cardArticle);
                }
            }

        }
    }
}