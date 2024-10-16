using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    public partial class Recuperacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_recuperar_Click(object sender, EventArgs e)
        {
            lbl_mensagem.Text = "";

            //gerar pass
            int tamanho = 10;
            string carateres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random random = new Random();
            while (0 < tamanho--)
            {
                res.Append(carateres[random.Next(carateres.Length)]);
            }
            string pass = res.ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "recuperar_password";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@password", EncryptDecrypt.EncryptString(pass));
            cmd.Parameters.AddWithValue("@email", tb_email.Text);

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(valor);

            con.Open();
            cmd.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            con.Close();

            if (respostaSP == 2)
            {
                lbl_mensagem.Text = "A conta não está ativa!";
            }
            else if (respostaSP == 0)
            {
                lbl_mensagem.Text = "O email inserido não existe!";
            }
            else if (respostaSP == 1)
            {
                lbl_mensagem.Text = "Palavra-passe recuperada e enviada para o email";

                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                // Vai buscar o email de envio ao webconfig
                email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                email.To.Add(new MailAddress(tb_email.Text));
                email.Subject = "Recuperação palavra-passe";

                email.IsBodyHtml = true;
                email.Body = $"A sua palavra-passe foi recuperada.\nA sua nova palavra-passe é: {pass}";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, password);
                servidor.EnableSsl = true;
                servidor.Send(email);
            }
        }
    }
}