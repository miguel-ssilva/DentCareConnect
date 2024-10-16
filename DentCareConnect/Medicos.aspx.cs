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
    public partial class Medicos : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_avaliacao.Visible = false;

            if (!IsPostBack)
            {
                CarregaClinicas();
                CarregaEspecialidades();
            }            
        }

        public void ChamaQueryFiltro()
        {
            if (ViewState["Clinica"] == null && ViewState["Especialidade"] == null)
            {
                SqlDataSource1.SelectCommand = "SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true'";
            }
            else if (ViewState["Clinica"] != null && ViewState["Especialidade"] != null)
            {
                SqlDataSource1.SelectCommand = $"SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true' and Clinica = '{ViewState["Clinica"].ToString()}' and Especialidade = '{ViewState["Especialidade"].ToString()}'";
            }
            else if (ViewState["Clinica"] != null && ViewState["Especialidade"] == null)
            {
                SqlDataSource1.SelectCommand = $"SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true' and Clinica = '{ViewState["Clinica"]}'";
            }
            else if (ViewState["Clinica"] == null && ViewState["Especialidade"] != null)
            {
                SqlDataSource1.SelectCommand = $"SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true' and Especialidade = '{ViewState["Especialidade"].ToString()}'";
            }
        }
        protected void CarregaClinicas()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Clinicas)
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
        protected void CarregaEspecialidades()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

            // Preenche a DropDownList com os dados da Base de dados (tabela Especialidades)
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

        protected void ddl_clinicas_DataBound(object sender, EventArgs e)
        {
            ddl_clinicas.Items.Insert(0, new ListItem("Selecione uma clinica", ""));
        }

        protected void ddl_especialidades_DataBound(object sender, EventArgs e)
        {
            ddl_especialidades.Items.Insert(0, new ListItem("Selecione uma especialidade", ""));
        }

        protected void ddl_clinicas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_clinicas.SelectedIndex == 0)
            {
                ViewState["Clinica"] = null;
            }
            else
            {
                ViewState["Clinica"] = ddl_clinicas.SelectedValue;
            }
            ChamaQueryFiltro();
        }

        protected void ddl_especialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_especialidades.SelectedIndex == 0)
            {
                ViewState["Especialidade"] = null;
            }
            else
            {
                ViewState["Especialidade"] = ddl_especialidades.SelectedValue;
            }
            ChamaQueryFiltro();
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                ((ImageButton)e.Item.FindControl("imgBtn_imagem")).CommandArgument = dr["MedicoID"].ToString();
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("imgBtn_imagem"))
            {
                panel_medicos.Visible = false;
                panel_perfilMedico.Visible = true;
                lbtn_voltar.Visible = true;
                ViewState["MedicoID"] = ((ImageButton)e.Item.FindControl("imgBtn_imagem")).CommandArgument;

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                string query = $"SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE MedicoID = {ViewState["MedicoID"]}";

                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lbl_nomeMedico.Text = reader["Nome"].ToString();
                    imageMedico.ImageUrl = "data:image;base64," + Convert.ToBase64String((byte[])reader["Foto"]); 
                    lbl_descricao.Text = reader["DescricaoCarreira"].ToString();
                    lbl_clinicaMedico.Text = reader["Clinica"].ToString();
                    lbl_especialidadeMedico.Text = reader["Especialidade"].ToString();
                    lbl_seguroMedico.Text = reader["Seguro"].ToString();
                }

                con.Close();

                query = $"SELECT COUNT(Avaliacao) FROM AvaliacoesMedicos WHERE MedicoID = {ViewState["MedicoID"]}";

                cmd = new SqlCommand(query, con);
                con.Open();
                int numero_avaliacoes = (int)cmd.ExecuteScalar();
                con.Close();

                if (numero_avaliacoes > 0)
                {
                    lbl_rating.Visible = true;
                    lbl_ratingText.Visible = true;

                    query = $"SELECT Avaliacao FROM AvaliacoesMedicos WHERE MedicoID = {ViewState["MedicoID"]}";
                    cmd = new SqlCommand(query, con);
                    con.Open();
                    reader = cmd.ExecuteReader();
                    int soma_avaliacoes = 0;
                    while (reader.Read())
                    {
                        soma_avaliacoes += (int)reader["Avaliacao"];
                    }
                    con.Close();

                    decimal media_avaliacoes = (decimal)soma_avaliacoes / (decimal)numero_avaliacoes;

                    lbl_rating.Text = media_avaliacoes.ToString("0.0").Replace(",",".");

                    if (media_avaliacoes > 2.5m)
                    {
                        lbl_rating.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_rating.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lbl_rating.Visible = false;
                    lbl_ratingText.Visible = false;
                }
            }
        }

        protected void lbtn_voltar_Click(object sender, EventArgs e)
        {
            panel_medicos.Visible = true;
            panel_perfilMedico.Visible = false;
            lbtn_voltar.Visible = false;
            lbl_avaliacao.Visible = false;
        }

        protected void btn_submeterAvaliacao_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {                
                string user = EncryptDecrypt.DecryptString(Request.QueryString["user"]);
                string avaliacao = HiddenAvaliacao.Value;

                if (string.IsNullOrEmpty(avaliacao))
                {
                    lbl_avaliacao.Visible = true;
                    lbl_avaliacao.Text = "Seleccione as estrelas para avaliar";
                }
                else
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DentCareConnectConnectionString"].ConnectionString);

                    string query = $"INSERT INTO AvaliacoesMedicos VALUES ({ViewState["MedicoID"]}, (SELECT PacienteID FROM Pacientes WHERE Username = '{user}'), {avaliacao})";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    lbl_avaliacao.Visible = true;
                    lbl_avaliacao.Text = "Avaliação submetida com sucesso!";
                }                
            }
            else
            {
                lbl_avaliacao.Visible = true;
                lbl_avaliacao.Text = "Tem de fazer Login";
            }
        }

        protected void btn_marcarConsulta_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                Response.Redirect($"MarcarConsulta.aspx?user={Request.QueryString["user"]}&Medico={lbl_nomeMedico.Text}&Especialidade={lbl_especialidadeMedico.Text}");
            }
            else
            {
                Response.Redirect($"MarcarConsulta.aspx?Medico={lbl_nomeMedico.Text}&Especialidade={lbl_especialidadeMedico.Text}");
            }
            
        }
    }
}