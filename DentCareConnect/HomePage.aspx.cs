using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                a_medicos.HRef = $"Medicos.aspx?user={Request.QueryString["user"]}";
                a_about.HRef = $"About.aspx?user={Request.QueryString["user"]}";
            }
            else
            {
                a_medicos.HRef = "Medicos.aspx";
                a_about.HRef = "About.aspx";
            }

            lbl_mensagem.Visible = false;
        }

        protected void btn_submeter_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"INSERT INTO Newsletter VALUES ('{tb_email.Text}')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            lbl_mensagem.Text = "Obrigado pela sua subscrição!";
            lbl_mensagem.Visible = true;
        }
    }
}