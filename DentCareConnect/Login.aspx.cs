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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "autenticar_utilizador";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@username", tb_username.Text);
            cmd.Parameters.AddWithValue("@password", EncryptDecrypt.EncryptString(tb_password.Text));

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(valor);

            con.Open();
            cmd.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            con.Close();

            if (respostaSP == 1)
            {
                Response.Redirect($"HomePage.aspx?user={EncryptDecrypt.EncryptString(tb_username.Text)}");
            }
            else if (respostaSP == 0)
            {
                lbl_mensagem.Text = "Utilizador e palavra-passe não existem!";
            }
            else if (respostaSP == 2)
            {
                lbl_mensagem.Text = "Utilizador inativo!";
            }
        }

        protected void lbtn_registar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registar.aspx");
        }

        protected void lbtn_recuperar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Recuperacao.aspx");
        }
    }
}