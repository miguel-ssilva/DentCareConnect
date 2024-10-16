using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.Drawing;
using System.Net.Mail;
using System.Net;

namespace DentCareConnect
{
    public partial class MarcarConsulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            // Verifica se é a primeira vez que a página está a ser carregada
            if (!IsPostBack)
            {                
                CarregaEspecialidades();

                if (Request.QueryString.AllKeys.Contains("Medico"))
                {
                    ddl_especialidades.SelectedValue = Request.QueryString["Especialidade"];

                    CarregaMedicos();
                    CarregaTratamento();
                    CarregaClinicas();

                    ddl_medicos.SelectedValue = Request.QueryString["Medico"];

                    ViewState["SelectedValue"] = ddl_medicos.SelectedValue;
                    ddl_horario_consulta.Items.Clear();
                    calendar_consulta.SelectedDates.Clear();
                    lbl_mensagem.Visible = false;
                    ddl_horario_consulta.Enabled = false;

                    ddl_medicos.Enabled = true;
                    ddl_tratamentos.Enabled = true;
                    ddl_clinicas.Enabled = false;                    
                }
            }            
        }

        protected void CarregaMedicos()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = "";

            if (ddl_clinicas.SelectedIndex == 0 || ddl_clinicas.SelectedIndex == -1)
            {
                // Preenche a DropDownList com os dados da Base de dados (tabela Medicos)
                query = $"SELECT Nome FROM Medicos WHERE Ativo = 'true' and EspecialidadeID = (SELECT EspecialidadeID FROM Especialidades WHERE Especialidade = '{ddl_especialidades.SelectedValue}')";
            }
            else
            {
                // Preenche a DropDownList com os dados da Base de dados (tabela Medicos)
                query = $"SELECT Nome FROM Medicos WHERE Ativo = 'true' and EspecialidadeID = (SELECT EspecialidadeID FROM Especialidades WHERE Especialidade = '{ddl_especialidades.SelectedValue}') AND ClinicaID = (SELECT ClinicaID FROM Clinicas WHERE Clinica = '{ddl_clinicas.SelectedValue}')";
            }                

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            ddl_medicos.DataSource = dt;
            ddl_medicos.DataTextField = "Nome";
            ddl_medicos.DataValueField = "Nome";
            ddl_medicos.DataBind();
            con.Close();
        }
        protected void CarregaEspecialidades()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Medicos)
            string query = "SELECT Especialidade FROM Especialidades";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            ddl_especialidades.DataSource = dt;
            ddl_especialidades.DataTextField = "Especialidade";
            ddl_especialidades.DataValueField = "Especialidade";
            ddl_especialidades.DataBind();
            con.Close();
        }
        protected void CarregaClinicas()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Medicos)
            string query = "SELECT Clinica FROM Clinicas";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            ddl_clinicas.DataSource = dt;
            ddl_clinicas.DataTextField = "Clinica";
            ddl_clinicas.DataValueField = "Clinica";
            ddl_clinicas.DataBind();
            con.Close();            
        }
        protected void CarregaTratamento()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Medicos)
            string query = $"SELECT Tratamento FROM Tratamentos WHERE EspecialidadeID = (SELECT EspecialidadeID FROM Especialidades WHERE Especialidade = '{ddl_especialidades.SelectedValue}')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            ddl_tratamentos.DataSource = dt;
            ddl_tratamentos.DataTextField = "Tratamento";
            ddl_tratamentos.DataValueField = "Tratamento";
            ddl_tratamentos.DataBind();
            con.Close();
        }

        protected List<string> BuscarMedico()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"SELECT DiaSemana FROM HorariosTrabalho h INNER JOIN DiaSemana d ON h.DiaSemanaID = d.DiaSemanaID WHERE h.MedicoID = (SELECT MedicoID FROM Medicos WHERE Nome = '{ViewState["SelectedValue"]}')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> diasTrabalhoMedico = new List<string>();

            while (reader.Read())
            {
                diasTrabalhoMedico.Add(reader[0].ToString());
            }
            con.Close();

            return diasTrabalhoMedico;
        }        

        protected void calendar_consulta_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.ForeColor = System.Drawing.Color.Green;
            e.Cell.Font.Bold = true;

            if (e.Day.Date < DateTime.Now)
            {
                e.Day.IsSelectable = false;                
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }

            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
            }
            // Método para buscar os dias de trabalho do médico
            List<string> diasTrabalhoMedico = BuscarMedico();            

            if (!diasTrabalhoMedico.Contains(e.Day.Date.DayOfWeek.ToString()))
            {
                // Desabilita a seleção para os dias em que o médico não trabalha
                e.Day.IsSelectable = false;
                // Altera a cor para indicar visualmente que o dia está inativo
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }

            
        }
          //###################################//
         //########## DropDownLists ##########//
        //###################################//
        protected void ddl_medicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedValue"] = ddl_medicos.SelectedValue;   
            ddl_horario_consulta.Items.Clear();
            calendar_consulta.SelectedDates.Clear();
            lbl_mensagem.Visible = false;
            ddl_horario_consulta.Enabled = false;
        }

        protected void ddl_medicos_DataBound(object sender, EventArgs e)
        {
            ddl_medicos.Items.Insert(0, new ListItem("Selecione um médico", ""));
        }
        protected void ddl_especialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_medicos.Enabled = true;
            ddl_tratamentos.Enabled = true;
            CarregaMedicos();
            CarregaTratamento();
            CarregaClinicas();
            lbl_mensagem.Visible = false;
            ddl_horario_consulta.Enabled = false;
            ddl_clinicas.Enabled = true;
        }
        protected void ddl_especialidades_DataBound(object sender, EventArgs e)
        {
            ddl_especialidades.Items.Insert(0, new ListItem("Selecione uma especialidade", ""));
        }
        protected void ddl_tratamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_mensagem.Visible = false;
        }
        protected void ddl_tratamentos_DataBound(object sender, EventArgs e)
        {
            ddl_tratamentos.Items.Insert(0, new ListItem("Selecione um tratamento", ""));
        }
        protected void ddl_horario_consulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_mensagem.Visible = false;
        }
        protected void ddl_horario_consulta_DataBound(object sender, EventArgs e)
        {
            ddl_horario_consulta.Items.Insert(0, new ListItem(" ", ""));
        }        
        
        protected void ddl_clinicas_DataBound(object sender, EventArgs e)
        {
            ddl_clinicas.Items.Insert(0, new ListItem("Filtrar por Clinica...", ""));
        }
        protected void ddl_clinicas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregaMedicos();
        }
        protected void calendar_consulta_SelectionChanged(object sender, EventArgs e)
        {
            ddl_horario_consulta.Enabled = true;
            CarregarHorariosDisponiveis(calendar_consulta.SelectedDate);
        }
        private void CarregarHorariosDisponiveis(DateTime dataSelecionada)
        {
            // Método para buscar os horários disponíveis

            List<string> horariosDisponiveis = ObterHorariosDisponiveis(dataSelecionada); 

            ddl_horario_consulta.Items.Clear(); // Limpa os itens anteriores da DropDownList

            // Preenche a DropDownList com os horários disponíveis
            foreach (string horario in horariosDisponiveis)
            {
                ddl_horario_consulta.Items.Add(new ListItem(horario, horario));
            }
        }

        private List<string> ObterHorariosDisponiveis(DateTime dataSelecionada)
        {
            string diaSemana = dataSelecionada.DayOfWeek.ToString();

            TimeSpan horaEntrada = ObterHoraEntrada(diaSemana);
            TimeSpan horaSaida = ObterHoraSaida(diaSemana);

            List<string> horariosDisponiveis = new List<string>();

            if (horaEntrada != TimeSpan.MinValue && horaSaida != TimeSpan.MinValue)
            {
                TimeSpan horarioAtual = horaEntrada;

                // Calcula os horários disponíveis de acordo com os intervalos de uma hora
                while (horarioAtual.Add(new TimeSpan(1, 0, 0)) <= horaSaida)
                {
                    string horarioFormatado = horarioAtual.ToString(@"hh\:mm"); // Formata a hora
                    horariosDisponiveis.Add(horarioFormatado);

                    horarioAtual = horarioAtual.Add(new TimeSpan(1, 0, 0));
                }
            }
            // Método para buscar os horários marcados
            List<string> horariosMarcados = ObterHorariosMarcados(dataSelecionada); 

            // Remover os horários já marcados da lista de horários disponíveis
            horariosDisponiveis.RemoveAll(h => horariosMarcados.Contains(h));

            return horariosDisponiveis;
        }
        private TimeSpan ObterHoraEntrada(string diaSemana)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"SELECT HoraEntrada FROM HorariosTrabalho h INNER JOIN DiaSemana d ON h.DiaSemanaID = d.DiaSemanaID WHERE d.DiaSemana = '{diaSemana}' and h.MedicoID = (SELECT MedicoID FROM Medicos WHERE Nome = '{ViewState["SelectedValue"]}')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            TimeSpan horaEntrada = (TimeSpan)cmd.ExecuteScalar();
            con.Close();
             
            return horaEntrada;
        }
        private TimeSpan ObterHoraSaida(string diaSemana)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"SELECT HoraSaida FROM HorariosTrabalho h INNER JOIN DiaSemana d ON h.DiaSemanaID = d.DiaSemanaID WHERE d.DiaSemana = '{diaSemana}' and h.MedicoID = (SELECT MedicoID FROM Medicos WHERE Nome = '{ViewState["SelectedValue"]}')";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            TimeSpan horaSaida = (TimeSpan)cmd.ExecuteScalar();
            con.Close();

            return horaSaida;
        }
        private List<string> ObterHorariosMarcados(DateTime dataSelecionada)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);
            
            string dataSelecionadaQuery = dataSelecionada.Year.ToString() + "-" + dataSelecionada.Month.ToString() + "-" + dataSelecionada.Day.ToString();

            string query = $"SELECT HoraConsulta FROM Consultas WHERE MedicoID = (SELECT MedicoID FROM Medicos WHERE Nome = '{ViewState["SelectedValue"]}') AND DataConsulta = '{dataSelecionadaQuery}' AND EstadoID != 3";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<string> horariosMarcados = new List<string>();

            while (reader.Read())
            {
                TimeSpan horarioAdd = (TimeSpan)reader[0];
                horariosMarcados.Add(horarioAdd.ToString(@"hh\:mm"));
            }
            con.Close();            

            return horariosMarcados;
        }

        protected void btn_marcar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                if (ddl_medicos.SelectedIndex == 0 || ddl_especialidades.SelectedIndex == 0)
                {
                    lbl_mensagem.Visible = true;
                    lbl_mensagem.Text = "Tem de seleccionar uma especialidade e um médico";
                }
                else if (ddl_tratamentos.SelectedIndex == 0)
                {
                    lbl_mensagem.Visible = true;
                    lbl_mensagem.Text = "Tem de seleccionar um tratamento";
                }
                else if (ddl_horario_consulta.Enabled == false)
                {
                    lbl_mensagem.Visible = true;
                    lbl_mensagem.Text = "Tem de seleccionar data e hora da sua consulta";
                }
                else
                {
                    lbl_mensagem.Visible = true;

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "inserir_consulta";

                    cmd.Connection = con;

                    string paciente = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

                    // Inserir nova consulta na base de dados
                    cmd.Parameters.AddWithValue("@medico", ViewState["SelectedValue"]);
                    cmd.Parameters.AddWithValue("@paciente", paciente);                    
                    cmd.Parameters.AddWithValue("@data", calendar_consulta.SelectedDate);
                    cmd.Parameters.AddWithValue("@hora", ddl_horario_consulta.SelectedValue);
                    cmd.Parameters.AddWithValue("@tratamento", ddl_tratamentos.SelectedValue);
                    cmd.Parameters.AddWithValue("@diaSemana", calendar_consulta.SelectedDate.DayOfWeek.ToString());
                                        
                    con.Open();
                    cmd.ExecuteNonQuery();                    
                    con.Close();                    

                    // Enviar email de confirmação da marcação de consulta

                    lbl_mensagem.Text = "Consulta marcada com sucesso!";

                    SmtpClient servidor = new SmtpClient();
                    MailMessage email = new MailMessage();

                    // Vai buscar o email de envio ao webconfig
                    email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);

                    // Buscar email do paciente à BD
                    string query = $"SELECT Email FROM Pacientes WHERE Username = '{paciente}'";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    string email_destino = cmd.ExecuteScalar().ToString();
                    con.Close();

                    query = $"SELECT Clinica FROM Clinicas WHERE ClinicaID = (SELECT ClinicaID FROM Medicos WHERE Nome = '{ViewState["SelectedValue"]}')";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    string clinica = cmd.ExecuteScalar().ToString();
                    con.Close();

                    DateTime data_marcada = calendar_consulta.SelectedDate;

                    email.To.Add(new MailAddress(email_destino));
                    email.Subject = "Confirmação de consulta";

                    email.IsBodyHtml = true;
                    email.Body = $"Confirmamos o agendamento da sua consulta para o dia {data_marcada.Date.ToString("dd/MM/yyyy")} às {ddl_horario_consulta.SelectedValue} na {clinica}. <br/>Tratamento: {ddl_tratamentos.SelectedValue} <br/>Qualquer dúvida contacte-nos através do nosso número ou email e aceda à sua área cliente onde pode ver e gerir as suas consultas.";

                    servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                    servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                    string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                    string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                    servidor.Credentials = new NetworkCredential(utilizador, password);
                    servidor.EnableSsl = true;
                    servidor.Send(email);

                    ddl_horario_consulta.Items.Clear();
                    calendar_consulta.SelectedDates.Clear();
                    ddl_horario_consulta.Enabled = false;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        
    }
}