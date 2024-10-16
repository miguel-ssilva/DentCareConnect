using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    public partial class template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();                

                string user = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

                li_login.Visible = false;
                li_registar.Visible = false;
                lbtn_cliente.Visible = true;
                lbtn_cliente.Text = user;

                // Vai buscar o Nome do user à base de dados
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT Nome FROM Pacientes WHERE Username = '{user}'";
                con.Open();
                string nome = (string)cmd.ExecuteScalar();
                con.Close();
                                
                lbl_saudacao.Visible = true;
                lbl_saudacao.Text = $"Olá, {nome}";
                lbtn_sair.Visible = true;
                welcome.Visible = false;
                saudacoes.Visible = true;

                a_logo.HRef = $"HomePage.aspx{RetornaUser()}";
            }
            else
            {
                li_login.Visible = true;
                li_registar.Visible = true;
                lbtn_cliente.Visible = false;
                lbtn_cliente.Text = "";
                lbl_saudacao.Visible = false;
                lbl_saudacao.Text = "";
                lbtn_sair.Visible = false;
                welcome.Visible = true;
                saudacoes.Visible = false;

                a_logo.HRef = $"HomePage.aspx";
            }
        }
        public string RetornaUser()
        {
            // Se existir "user" no URL guarda o username e mantem até terminar sessão
            string user = "";            
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                user = Request.QueryString["user"];                
                return $"?user={user}";
            }
            else
            {
                return "";
            }
        }

        protected void lbtn_cliente_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AreaCliente.aspx{RetornaUser()}");
        }

        protected void lbtn_sair_Click(object sender, EventArgs e)
        {
            Response.Redirect($"HomePage.aspx");
        }

        protected void lbtn_home_Click(object sender, EventArgs e)
        {
            Response.Redirect($"HomePage.aspx{RetornaUser()}");
        }

        protected void lbtn_about_Click(object sender, EventArgs e)
        {
            Response.Redirect($"About.aspx{RetornaUser()}");
        }

        protected void lbtn_medicos_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Medicos.aspx{RetornaUser()}");
        }

        protected void lbtn_contactos_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Contactos.aspx{RetornaUser()}");
        }

        protected void btn_marcar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"MarcarConsulta.aspx{RetornaUser()}");
        }
    }
}