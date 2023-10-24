﻿using NurseProjecDAO.Implementacion;
using NurseProjecDAO.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NurseProjectWEB
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnActualizarContraseña_Click(object sender, EventArgs e)
        {
            try
            {
                string login = Session["UserName"].ToString();
                short userID = Convert.ToInt16(Session["UserID"]);

                string antiguaContraseña = txtActual.Text;
                string nuevaContraseña = txtNueva.Text;
                string repetirContraseña = txtRepetir.Text;

                UserImpl implUser = new UserImpl();

                DataTable table = implUser.Login(login, antiguaContraseña);

                if (table.Rows.Count > 0)
                {
                    if (Tools.ValidarContraseña(nuevaContraseña))
                    {
                        if (nuevaContraseña == repetirContraseña)
                        {
                            implUser.UpdatePassword(login, antiguaContraseña, nuevaContraseña, userID);
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Las contraseñas no coinciden";
                            lblMessage.CssClass = "error-message";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "La contraseña no cumple con los requisitos mínimos.";
                        lblMessage.CssClass = "error-message";
                    }
                }
                else
                {
                    lblMessage.Text = "La contraseña anterior es incorrecta.";
                    lblMessage.CssClass = "error-message";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdmHome.aspx");
        }
    }
}