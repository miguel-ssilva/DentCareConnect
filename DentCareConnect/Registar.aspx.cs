using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    public partial class Registar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {            
            if (tb_password.Text == tb_password2.Text)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "inserir_utilizador";

                cmd.Connection = con;

                // Inserir novo utilizador na base de dados
                cmd.Parameters.AddWithValue("@nome", tb_nome.Text);
                cmd.Parameters.AddWithValue("@datanascimento", tb_datanascimento.Text);
                if (ddl_genero.SelectedValue == "Masculino")
                {
                    cmd.Parameters.AddWithValue("@genero", "M");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@genero", "F");
                }
                cmd.Parameters.AddWithValue("@morada", tb_morada.Text);
                cmd.Parameters.AddWithValue("@cp", tb_cp.Text);
                cmd.Parameters.AddWithValue("@cidade", ddl_cidade.SelectedValue);
                cmd.Parameters.AddWithValue("@telemovel", tb_telemovel.Text);
                cmd.Parameters.AddWithValue("@email", tb_email.Text);
                cmd.Parameters.AddWithValue("@nif", tb_nif.Text);
                cmd.Parameters.AddWithValue("@username", tb_username.Text);
                cmd.Parameters.AddWithValue("@password", EncryptDecrypt.EncryptString(tb_password.Text));

                // Retorno para validação da existência ou não do user inserido
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
                    lbl_mensagem.Text = "Verifique o seu email para ativar a sua conta de registo!";

                    SmtpClient servidor = new SmtpClient();
                    MailMessage email = new MailMessage();

                    // Vai buscar o email de envio ao webconfig
                    email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                    email.To.Add(new MailAddress(tb_email.Text));
                    email.Subject = "Ativação de conta";

                    email.IsBodyHtml = true;
                    email.Body = $"Bem Vindo às Clinicas DentCare Connect!<br/>Obrigado <b>{tb_nome.Text}</b> pelo registo na nossa aplicação.<br/>Para ativar a sua conta clique <a href='https://localhost:44365/Ativacao.aspx?user={EncryptDecrypt.EncryptString(tb_username.Text)}'>aqui</a>";

                    servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                    servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                    string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                    string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                    servidor.Credentials = new NetworkCredential(utilizador, password);
                    servidor.EnableSsl = true;
                    servidor.Send(email);
                }
                else if (respostaSP == 2)
                {
                    lbl_mensagem.Text = "Username já existe!";
                }
                else
                {
                    lbl_mensagem.Text = "Email já existe!";
                }
            }
            else
            {
                lbl_mensagem.Text = "A repetição da password está errada";               
            }
            
        }
    }
}