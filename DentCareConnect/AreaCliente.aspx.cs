using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DentCareConnect
{
    // AreaCliente e Backoffice tudo junto. No futuro migrar Backoffice para outra página
    public partial class AreaCliente : System.Web.UI.Page
    {
        // Inicializar medicoID para aceder depois globalmente no panel editar e panel dos medicos
        public string medicoID = "";
        // Inicializar pacienteID para aceder depois globalmente no panel editar e panel dos pacientes
        public string pacienteID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.QueryString.AllKeys.Contains("user"))
            {
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                string user = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

                if (user == "Admin")
                {
                    panel_admin.Visible = true;
                    panel_paciente.Visible = false;

                    if (panel_medicos.Visible == false && panel_pacientes.Visible == false && panel_gerir_horarios.Visible == false)
                    {
                        panel_welcome.Visible = true;

                        lbl_hora_entrada.Text = string.Empty;
                        lbl_hora_saida.Text = string.Empty;                        
                    }

                    if (rbtn_todos_medicos.Checked == true)
                    {
                        SqlDataSource1.SelectCommand = "SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID";
                    }
                    else if (rbtn_ativoMedico_sim.Checked == true)
                    {
                        SqlDataSource1.SelectCommand = "SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true'";
                    }
                    else if (rbtn_ativoMedico_nao.Checked == true)
                    {
                        SqlDataSource1.SelectCommand = "SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'false'";
                    }

                    if (rbtn_todos_pacientes.Checked == true)
                    {
                        SqlDataSource2.SelectCommand = "SELECT * FROM Pacientes WHERE PacienteID != 12";
                    }
                    else if (rbtn_ativoPaciente_sim.Checked == true)
                    {
                        SqlDataSource2.SelectCommand = "SELECT * FROM Pacientes WHERE PacienteID != 12 and Ativo = 'true'";
                    }
                    else if (rbtn_ativoPaciente_nao.Checked == true)
                    {
                        SqlDataSource2.SelectCommand = "SELECT * FROM Pacientes WHERE PacienteID != 12 and Ativo = 'false'";
                    }

                    Repeater1.DataBind();
                    Repeater2.DataBind();
                }
                else
                {
                    // Consultas do paciente
                    SqlDataSource3.SelectCommand = $"SELECT * FROM Consultas WHERE PacienteID = (SELECT PacienteID FROM Pacientes WHERE Username = '{user}') and DataConsulta < GETDATE() ORDER BY DataConsulta DESC, HoraConsulta DESC";

                    SqlDataSource4.SelectCommand = $"SELECT * FROM Consultas WHERE PacienteID = (SELECT PacienteID FROM Pacientes WHERE Username = '{user}') and DataConsulta >= GETDATE() ORDER BY DataConsulta ASC, HoraConsulta ASC";

                    lbl_mensagem.Visible = false;
                    panel_admin.Visible = false;
                    panel_paciente.Visible = true;
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);
                    string query = $"SELECT * FROM Pacientes WHERE Username = '{user}'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tb_nome.Text = reader["Nome"].ToString();
                        tb_morada.Text = reader["Morada"].ToString();
                        tb_cp.Text = reader["CodigoPostal"].ToString();
                        ddl_cidade.Text = reader["Cidade"].ToString();
                        tb_tlmvl.Text = reader["Telemovel"].ToString();
                        tb_email.Text = reader["Email"].ToString();
                    }

                    con.Close();
                }
            }

        }

        // Area de cliente (Paciente)
        #region
        protected void lbtn_editar_Click(object sender, EventArgs e)
        {
            tb_nome.Enabled = true;
            tb_morada.Enabled = true;
            tb_cp.Enabled = true;
            ddl_cidade.Enabled = true;
            tb_tlmvl.Enabled = true;
            tb_email.Enabled = true;

            lbtn_editar.Visible = false;
            btn_gravar.Visible = true;
            btn_cancelar.Visible = true;
        }

        protected void btn_gravar_Click(object sender, EventArgs e)
        {
            lbtn_editar.Visible = true;
            btn_gravar.Visible = false;
            btn_cancelar.Visible = false;

            tb_nome.Enabled = false;
            tb_morada.Enabled = false;
            tb_cp.Enabled = false;
            ddl_cidade.Enabled = false;
            tb_tlmvl.Enabled = false;
            tb_email.Enabled = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "atualizar_utilizador";

            cmd.Connection = con;

            string user = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

            // Atualizar informação do paciente na BD
            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@nome", tb_nome.Text);
            cmd.Parameters.AddWithValue("@morada", tb_morada.Text);
            cmd.Parameters.AddWithValue("@cp", tb_cp.Text);
            cmd.Parameters.AddWithValue("@cidade", ddl_cidade.SelectedValue);
            cmd.Parameters.AddWithValue("@telemovel", tb_tlmvl.Text);
            cmd.Parameters.AddWithValue("@email", tb_email.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            lbtn_editar.Visible = true;
            btn_gravar.Visible = false;
            btn_cancelar.Visible = false;

            tb_nome.Enabled = false;
            tb_morada.Enabled = false;
            tb_cp.Enabled = false;
            ddl_cidade.Enabled = false;
            tb_tlmvl.Enabled = false;
            tb_email.Enabled = false;
        }
        protected void btn_alterarPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_pass.Text) || string.IsNullOrEmpty(tb_passNova.Text) || string.IsNullOrEmpty(tb_passNova2.Text))
            {
                lbl_mensagem.Visible = true;
                lbl_mensagem.Text = "Deve preencher todos os campos";
            }
            else if (tb_passNova.Text != tb_passNova2.Text)
            {
                lbl_mensagem.Visible = true;
                lbl_mensagem.Text = "Deve repetir corretamente a nova password";
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "alterar_password";

                cmd.Connection = con;

                string user = EncryptDecrypt.DecryptString(Request.QueryString["user"]);

                // Atualizar informação do paciente na BD
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@passAntiga", EncryptDecrypt.EncryptString(tb_pass.Text));
                cmd.Parameters.AddWithValue("@passNova", EncryptDecrypt.EncryptString(tb_passNova.Text));

                // Retorno para validação da password
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
                    lbl_mensagem.Visible = true;
                    lbl_mensagem.Text = "Password alterada com sucesso!";
                }
                else if (respostaSP == 2)
                {
                    lbl_mensagem.Visible = true;
                    lbl_mensagem.Text = "Password antiga inserida está incorreta";
                }
            }
        }
        #endregion
        // BackOffice - toda a área de gestão para o Admin
        #region
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("lbtn_editar_medico"))
            {
                panel_medicos.Visible = false;
                panel_editar_medicos.Visible = true;
                panel_welcome.Visible = false;
                panel_gerir_horarios.Visible = false;

                medicoID = e.CommandArgument.ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                // Preenche a DropDownList com os dados da Base de dados (tabela Clinicas)
                string query = "SELECT Clinica FROM Clinicas";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                ddl_clinica.DataSource = dt;
                ddl_clinica.DataTextField = "Clinica";
                ddl_clinica.DataValueField = "Clinica";
                ddl_clinica.DataBind();
                con.Close();

                // Preenche a DropDownList com os dados da Base de dados (tabela Especialidades)
                query = "SELECT Especialidade FROM Especialidades";

                cmd = new SqlCommand(query, con);
                con.Open();

                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();

                adapter.Fill(dt);

                ddl_especialidade.DataSource = dt;
                ddl_especialidade.DataTextField = "Especialidade";
                ddl_especialidade.DataValueField = "Especialidade";
                ddl_especialidade.DataBind();
                con.Close();

                // Preenche a DropDownList com os dados da Base de dados (tabela Seguros)
                query = "SELECT Seguro FROM Seguros";

                cmd = new SqlCommand(query, con);
                con.Open();

                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();

                adapter.Fill(dt);

                ddl_seguro.DataSource = dt;
                ddl_seguro.DataTextField = "Seguro";
                ddl_seguro.DataValueField = "Seguro";
                ddl_seguro.DataBind();
                con.Close();

                query = $"SELECT * FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE MedicoID = {medicoID}";

                cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lbl_nome_medico.Text = reader["Nome"].ToString();
                    tb_salario.Text = reader["Salario"].ToString();
                    ddl_clinica.Text = reader["Clinica"].ToString();
                    ddl_especialidade.Text = reader["Especialidade"].ToString();
                    ddl_seguro.Text = reader["Seguro"].ToString();
                    tb_descricao.Text = reader["DescricaoCarreira"].ToString();
                    if ((bool)reader["Ativo"] == true)
                    {
                        rbtn_sim.Checked = true;
                    }
                    else
                    {
                        rbtn_nao.Checked = true;
                    }
                }
                con.Close();

                btn_gravar_medico.CommandArgument = medicoID;
            }
            else if (e.CommandName.Equals("lbtn_gerir_horarios"))
            {
                panel_medicos.Visible = false;
                panel_editar_medicos.Visible = false;
                panel_welcome.Visible = false;
                panel_gerir_horarios.Visible = true;

                medicoID = e.CommandArgument.ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);
                                
                string query = "SELECT * FROM DiaSemana";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                adapter.Fill(dt);

                ddl_diaSemana.DataSource = dt;
                ddl_diaSemana.DataTextField = "DiaSemana";
                ddl_diaSemana.DataValueField = "DiaSemanaID";
                ddl_diaSemana.DataBind();
                con.Close();

                query = $"SELECT Nome FROM Medicos WHERE MedicoID = {medicoID}";

                cmd = new SqlCommand(query, con);
                con.Open();
                lbl_name_medico.Text = cmd.ExecuteScalar().ToString();
                con.Close();

                btn_atualizar_horario.CommandArgument = medicoID;
            }
        }
        protected void ddl_diaSemana_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string medico = btn_atualizar_horario.CommandArgument.ToString();

            string query = $"SELECT m.Nome, h.HoraEntrada, h.HoraSaida FROM HorariosTrabalho h INNER JOIN Medicos m ON h.MedicoID = m.MedicoID WHERE h.MedicoID = {medico} AND DiaSemanaID = {ddl_diaSemana.SelectedValue}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {                
                lbl_hora_entrada.Text = reader["HoraEntrada"].ToString();
                lbl_hora_saida.Text = reader["HoraSaida"].ToString();
            }
            con.Close();
        }
        protected void ddl_diaSemana_DataBound(object sender, EventArgs e)
        {
            ddl_diaSemana.Items.Insert(0, new ListItem("Selecione um dia", ""));
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                LinkButton EditarMedico = (LinkButton)e.Item.FindControl("lbtn_editar_medico");
                LinkButton GerirHorario = (LinkButton)e.Item.FindControl("lbtn_gerir_horarios");
                Label Ativo = (Label)e.Item.FindControl("lbl_ativo");

                if ((bool)dr["Ativo"] == true)
                {
                    Ativo.Text = "Sim";
                }
                else
                {
                    Ativo.Text = "Não";
                }

                if (EditarMedico != null)
                {
                    EditarMedico.CommandArgument = dr["MedicoID"].ToString();
                }
                if (GerirHorario != null)
                {
                    GerirHorario.CommandArgument = dr["MedicoID"].ToString();
                }
            }
        }
        protected void btn_gravar_medico_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            bool ativo;
            if (rbtn_sim.Checked == true)
            {
                ativo = true;
            }
            else
            {
                ativo = false;
            }

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "atualizar_medico";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@medicoID", Convert.ToInt32(btn_gravar_medico.CommandArgument));
            cmd.Parameters.AddWithValue("@clinica", ddl_clinica.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@seguro", ddl_seguro.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@especialidade", ddl_especialidade.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@salario", Convert.ToDecimal(tb_salario.Text));
            cmd.Parameters.AddWithValue("@descricao", tb_descricao.Text);
            cmd.Parameters.AddWithValue("@ativo", ativo);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            panel_editar_medicos.Visible = false;
            panel_welcome.Visible = true;

            Repeater1.DataBind();
        }
        protected void btn_atualizar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"UPDATE HorariosTrabalho SET HoraEntrada = '{tb_horaEntrada.Text}:00:00', HoraSaida = '{tb_horaSaida.Text}:00:00' WHERE MedicoID = {btn_atualizar_horario.CommandArgument} AND DiaSemanaID = {ddl_diaSemana.SelectedValue}";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close() ;
        }


        protected void lbtn_medicos_Click(object sender, EventArgs e)
        {
            panel_medicos.Visible = true;
            panel_editar_medicos.Visible = false;
            panel_pacientes.Visible = false;
            panel_editar_pacientes.Visible = false;
            panel_adicionar_medico.Visible = false;
            panel_welcome.Visible = false;
            panel_nova_clinica.Visible = false;
            panel_gerir_horarios.Visible = false;

        }
        protected void lbtn_pacientes_Click(object sender, EventArgs e)
        {
            panel_pacientes.Visible = true;
            panel_medicos.Visible = false;
            panel_editar_medicos.Visible = false;
            panel_editar_pacientes.Visible = false;
            panel_adicionar_medico.Visible = false;
            panel_welcome.Visible = false;
            panel_nova_clinica.Visible = false;
            panel_gerir_horarios.Visible = false;
        }
        protected void lbtn_novaClinica_Click(object sender, EventArgs e)
        {
            panel_nova_clinica.Visible = true;
            panel_adicionar_medico.Visible = false;
            panel_medicos.Visible = false;
            panel_editar_medicos.Visible = false;
            panel_pacientes.Visible = false;
            panel_editar_pacientes.Visible = false;
            panel_welcome.Visible = false;
            panel_gerir_horarios.Visible = false;
        }

        protected void lbtn_adicionarMedico_Click(object sender, EventArgs e)
        {
            panel_adicionar_medico.Visible = true;
            panel_medicos.Visible = false;
            panel_editar_medicos.Visible = false;
            panel_pacientes.Visible = false;
            panel_editar_pacientes.Visible = false;
            panel_welcome.Visible = false;
            panel_nova_clinica.Visible = false;
            panel_gerir_horarios.Visible = false;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Clinicas)
            string query = "SELECT Clinica FROM Clinicas";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            ddl_add_clinica.DataSource = dt;
            ddl_add_clinica.DataTextField = "Clinica";
            ddl_add_clinica.DataValueField = "Clinica";
            ddl_add_clinica.DataBind();
            con.Close();

            // Preenche a DropDownList com os dados da Base de dados (tabela Seguros)
            query = "SELECT Seguro FROM Seguros";

            cmd = new SqlCommand(query, con);
            con.Open();

            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adapter.Fill(dt);

            ddl_add_seguro.DataSource = dt;
            ddl_add_seguro.DataTextField = "Seguro";
            ddl_add_seguro.DataValueField = "Seguro";
            ddl_add_seguro.DataBind();
            con.Close();

            // Preenche a DropDownList com os dados da Base de dados (tabela Especialidades)
            query = "SELECT Especialidade FROM Especialidades";

            cmd = new SqlCommand(query, con);
            con.Open();

            adapter = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adapter.Fill(dt);

            ddl_add_especialidade.DataSource = dt;
            ddl_add_especialidade.DataTextField = "Especialidade";
            ddl_add_especialidade.DataValueField = "Especialidade";
            ddl_add_especialidade.DataBind();
            con.Close();
        }       


        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("lbtn_editar_paciente"))
            {
                panel_pacientes.Visible = false;
                panel_editar_pacientes.Visible = true;
                panel_welcome.Visible = false;

                pacienteID = e.CommandArgument.ToString();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                string query = $"SELECT * FROM Pacientes p INNER JOIN Seguros s ON p.SeguroID = p.SeguroID WHERE PacienteID = {pacienteID}";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lbl_nome_paciente.Text = reader["Nome"].ToString();
                    lbl_morada.Text = reader["Morada"].ToString();
                    lbl_cp.Text = reader["CodigoPostal"].ToString();
                    lbl_cidade.Text = reader["Cidade"].ToString();
                    lbl_dataNascimento.Text = reader["DataNascimento"].ToString().Substring(0, reader["DataNascimento"].ToString().IndexOf(' '));
                    if (reader["Genero"].ToString() == "M")
                    {
                        lbl_genero.Text = "Masculino";
                    }
                    else
                    {
                        lbl_genero.Text = "Feminino";
                    }
                    lbl_tlmvl.Text = reader["Telemovel"].ToString();
                    lbl_nif.Text = reader["NIF"].ToString();
                    lbl_email.Text = reader["Email"].ToString();
                    lbl_username.Text = reader["Username"].ToString();
                    if ((bool)reader["Ativo"] == true)
                    {
                        rbtn_simP.Checked = true;
                    }
                    else
                    {
                        rbtn_naoP.Checked = true;
                    }
                }
                con.Close();

                btn_gravar_paciente.CommandArgument = pacienteID;
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                LinkButton EditarPaciente = (LinkButton)e.Item.FindControl("lbtn_editar_paciente");
                Label Ativo = (Label)e.Item.FindControl("lbl_ativo2");

                if ((bool)dr["Ativo"] == true)
                {
                    Ativo.Text = "Sim";
                }
                else
                {
                    Ativo.Text = "Não";
                }

                if (EditarPaciente != null)
                {
                    EditarPaciente.CommandArgument = dr["PacienteID"].ToString();
                }
            }
        }


        protected void btn_gravar_paciente_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            bool ativo;
            if (rbtn_simP.Checked == true)
            {
                ativo = true;
            }
            else
            {
                ativo = false;
            }

            string query = $"UPDATE Pacientes SET Ativo = '{ativo}' WHERE PacienteID = {btn_gravar_paciente.CommandArgument}";

            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            panel_editar_pacientes.Visible = false;
            panel_welcome.Visible = true;

            Repeater2.DataBind();
        }
        protected void btn_add_medico_Click(object sender, EventArgs e)
        {
            if (tb_add_nome.Text != "" || tb_add_salario.Text != "")
            {
                Stream imgStream = FileUpload1.PostedFile.InputStream;
                int tamanhoFich = FileUpload1.PostedFile.ContentLength;
                string contentType = FileUpload1.PostedFile.ContentType;

                byte[] imgBinaryData = new byte[tamanhoFich];
                imgStream.Read(imgBinaryData, 0, tamanhoFich);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "adicionar_medico";

                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@nome", tb_add_nome.Text);
                cmd.Parameters.AddWithValue("@clinica", ddl_add_clinica.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@seguro", ddl_add_seguro.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@especialidade", ddl_add_especialidade.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@salario", Convert.ToDecimal(tb_add_salario.Text));
                cmd.Parameters.AddWithValue("@descricao", tb_add_descricao.Text);
                cmd.Parameters.AddWithValue("@foto", imgBinaryData);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                panel_adicionar_medico.Visible = false;
                panel_welcome.Visible = true;
            }
        }

        protected void ListView_consultas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            DateTime dataConsulta = (DateTime)dr["DataConsulta"];
            ((Label)e.Item.FindControl("lbl_dataConsulta")).Text = dataConsulta.Date.ToString("dd/MM/yyyy");
            ((Label)e.Item.FindControl("lbl_horaConsulta")).Text = dr["HoraConsulta"].ToString();
        }

        protected void ListView_nextConsultas_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            DateTime dataConsulta = (DateTime)dr["DataConsulta"];
            ((Label)e.Item.FindControl("lbl_dataConsulta")).Text = dataConsulta.Date.ToString("dd/MM/yyyy");
            ((Label)e.Item.FindControl("lbl_horaConsulta")).Text = dr["HoraConsulta"].ToString();
        }

        protected void btn_add_clinica_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            string query = $"INSERT INTO Clinicas VALUES ('{tb_clinica.Text}', '{tb_localidade.Text}')";

            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            panel_nova_clinica.Visible = false;
            panel_welcome.Visible = true;
        }

        #endregion

        

        
    }
}