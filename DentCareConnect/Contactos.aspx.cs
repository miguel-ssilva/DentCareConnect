using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    public partial class Contactos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_submeter_Click(object sender, EventArgs e)
        {
            SmtpClient servidor = new SmtpClient();
            MailMessage email = new MailMessage();

            // Vai buscar o email de envio ao webconfig
            email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
            email.To.Add(new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]));
            email.Subject = "Envio de contacto via site";

            email.IsBodyHtml = true;
            email.Body = $"{message.Value}<br /><br />Nome: {name.Value}<br />Email: {email_origem.Value}<br />Telemóvel: {phone.Value}<br />";

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