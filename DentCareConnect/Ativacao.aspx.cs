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
    public partial class Ativacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vai buscar o user ao url
            string username = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            // Ativação do respetivo user na base de dados
            cmd.CommandText = "ativar_utilizador";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@username", username);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btn_regressar_Click(object sender, EventArgs e)
        {
            Response.Redirect($"HomePage.aspx?user={Request.QueryString["user"]}");
        }
    }
}