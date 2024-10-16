<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="AreaCliente.aspx.cs" Inherits="DentCareConnect.AreaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container-cliente {
            display: flex;
            justify-content: space-around;
            padding: 20px;
        }

        .info-cliente,
        .info-consultas {            
            width: 45%;            
        } 

        .info-cliente {
            min-height: 600px;            
            flex-grow: 1;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            margin-right: 10px;
        }

        .info-consultas {
            display: flex;
            flex-direction: column;
            flex-grow: 1;
        }

        .info-consultas div {
            min-height: 300px;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;             
        }
        .btn_NextPrevPage
        {
            color: #a5c422;
        }
    </style>

    <%--AREA CLIENTE--%>
    <asp:Panel ID="panel_paciente" runat="server" Visible="False">
        <div class="container">
            <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">
                <div>
                    <div class="container-cliente">
                        <div class="info-cliente">
                            <h2>Informações do Cliente</h2>
                            <p>Nome: <br /><asp:TextBox ID="tb_nome" runat="server" Enabled="False" Width="300"></asp:TextBox></p>
                            <p>Morada: <br /><asp:TextBox ID="tb_morada" runat="server" Enabled="False" Width="300"></asp:TextBox></p>
                            <p>Código-postal: <br /><asp:TextBox ID="tb_cp" runat="server" Enabled="False" Width="300"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_cp" ValidationExpression="^\d{4}(-\d{3})?$"></asp:RegularExpressionValidator></p>                            
                            <p>Cidade: <br /><asp:DropDownList ID="ddl_cidade" runat="server" Enabled="False" Width="300">
                                    <asp:ListItem>Aveiro</asp:ListItem>
                                    <asp:ListItem>Beja</asp:ListItem>
                                    <asp:ListItem>Braga</asp:ListItem>
                                    <asp:ListItem>Bragança</asp:ListItem>
                                    <asp:ListItem>Castelo Branco</asp:ListItem>
                                    <asp:ListItem>Coimbra</asp:ListItem>
                                    <asp:ListItem>Évora</asp:ListItem>
                                    <asp:ListItem>Faro</asp:ListItem>
                                    <asp:ListItem>Guarda</asp:ListItem>
                                    <asp:ListItem>Leiria</asp:ListItem>
                                    <asp:ListItem>Lisboa</asp:ListItem>
                                    <asp:ListItem>Portalegre</asp:ListItem>
                                    <asp:ListItem>Porto</asp:ListItem>
                                    <asp:ListItem>Santarém</asp:ListItem>
                                    <asp:ListItem>Setúbal</asp:ListItem>
                                    <asp:ListItem>Viana do Castelo</asp:ListItem>
                                    <asp:ListItem>Vila Real</asp:ListItem>
                                    <asp:ListItem>Viseu</asp:ListItem>
                                </asp:DropDownList></p>
                            <p>Telemóvel: <br /><asp:TextBox ID="tb_tlmvl" runat="server" Enabled="False" Width="300"></asp:TextBox></p>
                            <p>E-mail: <br /><asp:TextBox ID="tb_email" runat="server" Enabled="False" Width="300"></asp:TextBox></p>
                            <br />
                            <asp:LinkButton ID="lbtn_editar" runat="server" ForeColor="Blue" OnClick="lbtn_editar_Click" Font-Underline="True">Editar</asp:LinkButton>
                            <asp:Button ID="btn_gravar" runat="server" Text="Gravar" Visible="False" OnClick="btn_gravar_Click" CssClass="appointment-btn" BorderStyle="None" Width="100"/>  <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" Visible="False" OnClick="btn_cancelar_Click" CssClass="appointment-btn" BorderStyle="None" Width="100"/>
                        </div>        
                        <div class="info-consultas">
                            <div style="margin-bottom: 10px">
                                <h2>Histórico de Consultas</h2>
                                <asp:ListView ID="ListView_consultas" runat="server" DataSourceID="SqlDataSource3" OnItemDataBound="ListView_consultas_ItemDataBound">
                                    <ItemTemplate>
                                        <p>Data: <asp:Label ID="lbl_dataConsulta" runat="server" Text=""></asp:Label> - <asp:Label ID="lbl_horaConsulta" runat="server" Text=""></asp:Label></p>
                                    </ItemTemplate>
                                </asp:ListView>
                                  
                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView_consultas" PageSize="5">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="btn_NextPrevPage" NextPageText="  Próximo ->" PreviousPageText="<- Anterior  "/>
                                    </Fields>
                                </asp:DataPager>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DentCareConnectConnectionString %>" SelectCommand="SELECT * FROM Consultas" ></asp:SqlDataSource>                                
                            </div>
                            <div>
                                <h2>Próximas Consultas</h2>
                                <asp:ListView ID="ListView_nextConsultas" runat="server" DataSourceID="SqlDataSource4" OnItemDataBound="ListView_nextConsultas_ItemDataBound">
                                    <ItemTemplate>
                                        <p>Data: <asp:Label ID="lbl_dataConsulta" runat="server" Text=""></asp:Label> - <asp:Label ID="lbl_horaConsulta" runat="server" Text=""></asp:Label></p>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListView_nextConsultas" PageSize="5">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" ButtonCssClass="btn_NextPrevPage" NextPageText="  Próximo ->" PreviousPageText="<- Anterior  "/>
                                    </Fields>
                                </asp:DataPager>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DentCareConnectConnectionString %>" SelectCommand="SELECT * FROM Consultas" ></asp:SqlDataSource> 
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="container-cliente">                            
                            <div class="info-cliente">                                
                                <h2>Alteração de Password</h2>
                                <br />                                
                                <p>Password antiga: <br /><asp:TextBox ID="tb_pass" runat="server" Width="300" TextMode="Password"></asp:TextBox></p>
                                <p>Password nova: <br /><asp:TextBox ID="tb_passNova" runat="server" Width="300" TextMode="Password"></asp:TextBox></p>
                                <p>Repetir Password nova: <br /><asp:TextBox ID="tb_passNova2" runat="server" Width="300" TextMode="Password"></asp:TextBox></p>
                                <br />
                                <asp:Label ID="lbl_mensagem" runat="server" Text="" Visible="False" ForeColor="Red"></asp:Label>
                                <br /><br />                                
                                <asp:Button ID="btn_alterarPass" runat="server" Text="Alterar Password" CssClass="appointment-btn" BorderStyle="None" Width="200" OnClick="btn_alterarPass_Click"/>                             
                            </div>                            
                            <div class="info-cliente">
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--BACKOFFICE--%>
    <asp:Panel ID="panel_admin" runat="server" Visible="False">
        <style>
            nav {
                background-color: #f2f2f2;
                padding: 10px;
                text-align:center;
            }
            nav ul {
                list-style: none;
                padding: 0;
                margin: 0;
            }
            nav ul li {
                display: inline;
                margin-right: 20px;
            }
            section {
                padding: 20px;
            }
            h2{
                text-align:center;
            }
            td{
                width: 200px;

                padding-left: 5px;
                padding-right: 5px;
            }
            table{
                margin-top: 30px;
            }          
        </style>
        <div class="container">
            <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px; top: 0px; left: 0px;">
                <h2>Backoffice - Clínica Dentária</h2>     
                <nav>
                    <ul>
                        <li>
                            <asp:LinkButton ID="lbtn_medicos" runat="server" OnClick="lbtn_medicos_Click">Médicos</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtn_pacientes" runat="server" OnClick="lbtn_pacientes_Click">Pacientes</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtn_adicionarMedico" runat="server" OnClick="lbtn_adicionarMedico_Click">Adicionar Médico</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtn_novaClinica" runat="server" OnClick="lbtn_novaClinica_Click">Nova Clinica</asp:LinkButton>
                        </li>
                    </ul>
                </nav>

                <%--Boas Vindas--%>
                <asp:Panel ID="panel_welcome" runat="server" Visible="false">
                    <div style="padding-left:130px">
                        <div style="background-image: url(images/news-image1.jpg); background-repeat: no-repeat; margin-top:20px; min-height:500px; display: block; margin-left: 25px; margin-right: auto; width:auto">                            
                        </div>
                    </div>
                </asp:Panel>
                
                <%--Secção Médicos--%>
                <center>
                <asp:Panel ID="panel_medicos" runat="server" Visible="False">
                    <br />
                    <p>Todos <asp:RadioButton ID="rbtn_todos_medicos" runat="server" AutoPostBack="True" GroupName="ativo_medicos" Checked="True" /> &ensp; Ativos <asp:RadioButton ID="rbtn_ativoMedico_sim" runat="server" AutoPostBack="True" GroupName="ativo_medicos"/> &ensp; Inativos <asp:RadioButton ID="rbtn_ativoMedico_nao" runat="server" AutoPostBack="True" GroupName="ativo_medicos"/></p>
                    <table border="1">
                        <tr>
                            <td>
                                <b>Nome</b>
                            </td>
                            <td>
                                <b>Especialidade</b>
                            </td>
                            <td>
                                <b>Clinica</b>
                            </td>
                            <td style="width:70px; text-align:center">
                                <b>Ativo</b>
                            </td>
                            <td style="width:50px">

                            </td>
                            <td style="width:120px">

                            </td>
                        </tr>
                        
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("Nome") %>
                                </td>
                                <td>
                                    <%# Eval("Especialidade") %>
                                </td>
                                <td>
                                    <%# Eval("Clinica") %>
                                </td>
                                <td style="width:70px; text-align:center">
                                    <asp:Label ID="lbl_ativo" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="width:50px">
                                    <asp:LinkButton ID="lbtn_editar_medico" runat="server" CommandName="lbtn_editar_medico" ForeColor="Blue">Editar</asp:LinkButton>
                                </td>
                                <td style="width:120px; text-align:center">
                                    <asp:LinkButton ID="lbtn_gerir_horarios" runat="server" CommandName="lbtn_gerir_horarios" ForeColor="Blue">Gerir Horário</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DentCareConnectConnectionString %>" SelectCommand="SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID"></asp:SqlDataSource>
                    </table>
                </asp:Panel>                       
                </center>   
                <center>
                    <asp:Panel ID="panel_editar_medicos" runat="server" Visible="false">
                        <style>
                            .table-editar{
                                padding-bottom:5px;
                            }
                        </style>
                        <h3><asp:Label ID="lbl_nome_medico" runat="server" Text=""></asp:Label></h3>
                        <br />
                        <table>
                            <tr>
                                <td class="table-editar">
                                    Clinica: 
                                </td>
                                <td class="table-editar">
                                    <asp:DropDownList ID="ddl_clinica" runat="server" Width="250"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Especialidade: 
                                </td>
                                <td class="table-editar">
                                    <asp:DropDownList ID="ddl_especialidade" runat="server" Width="250"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Seguro: 
                                </td>
                                <td class="table-editar">
                                    <asp:DropDownList ID="ddl_seguro" runat="server" Width="250"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Salário: 
                                </td>
                                <td class="table-editar">
                                    <asp:TextBox ID="tb_salario" runat="server" Width="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Descrição: 
                                </td>
                                <td class="table-editar">
                                    <asp:TextBox ID="tb_descricao" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Ativo: 
                                </td>
                                <td class="table-editar">
                                    Sim <asp:RadioButton ID="rbtn_sim" runat="server" GroupName="Ativo"/> Não <asp:RadioButton ID="rbtn_nao" runat="server" GroupName="Ativo"/>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btn_gravar_medico" runat="server" Text="Gravar" CssClass="appointment-btn" BorderStyle="None" OnClick="btn_gravar_medico_Click" Width="100"/>
                    </asp:Panel>                    
                </center>
                <center>
                    
                    <asp:Panel ID="panel_gerir_horarios" runat="server" Visible="false">                        
                        <br />
                        <h3><asp:Label ID="lbl_name_medico" runat="server" Text=""></asp:Label></h3>
                        <br />
                        <div class="container-cliente">                        
                        <div>
                            <b>Dia da semana:</b> <asp:DropDownList ID="ddl_diaSemana" runat="server" Width="250" OnSelectedIndexChanged="ddl_diaSemana_SelectedIndexChanged" OnDataBound="ddl_diaSemana_DataBound" AutoPostBack="true"></asp:DropDownList>
                            <br /><br />
                            <b>Hora entrada:</b> <asp:Label ID="lbl_hora_entrada" runat="server" Text=""></asp:Label>
                            <br /><br />
                            <b>Hora saida:</b> <asp:Label ID="lbl_hora_saida" runat="server" Text=""></asp:Label>
                        </div>

                        <div>
                            Hora entrada: <asp:TextBox ID="tb_horaEntrada" runat="server" TextMode="Number"></asp:TextBox>
                            <br /><br />
                            Hora saida: <asp:TextBox ID="tb_horaSaida" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                        </div>
                        
                        <br />
                        <asp:Button ID="btn_atualizar_horario" runat="server" Text="Atualizar" CssClass="appointment-btn" BorderStyle="None" OnClick="btn_atualizar_Click" Width="100"/>
                    </asp:Panel>
                    
                </center>
                
                <%--Secção Pacientes--%>
                <center>
                <asp:Panel ID="panel_pacientes" runat="server" Visible="False">
                    <br />
                    <p>Todos <asp:RadioButton ID="rbtn_todos_pacientes" runat="server" AutoPostBack="True" GroupName="ativo_pacientes" Checked="True" /> &ensp; Ativos <asp:RadioButton ID="rbtn_ativoPaciente_sim" runat="server" AutoPostBack="True" GroupName="ativo_pacientes"/> &ensp; Inativos <asp:RadioButton ID="rbtn_ativoPaciente_nao" runat="server" AutoPostBack="True" GroupName="ativo_pacientes"/></p>
                    <table border="1">
                        <tr>
                            <td>
                                <b>Nome</b>
                            </td>
                            <td>
                                <b>Username</b>
                            </td>
                            <td>
                                <b>Email</b>
                            </td>
                            <td>
                                <b>Cidade</b>
                            </td>
                            <td style="width:70px; text-align:center">
                                <b>Ativo</b>
                            </td>
                            <td style="width:50px">

                            </td>
                        </tr>
                        
                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2" OnItemCommand="Repeater2_ItemCommand" OnItemDataBound="Repeater2_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# Eval("Nome") %>
                                </td>
                                <td>
                                    <%# Eval("Username") %>
                                </td>
                                <td>
                                    <%# Eval("Email") %>
                                </td>
                                <td>
                                    <%# Eval("Cidade") %>
                                </td>
                            <td style="width:70px; text-align:center">
                                <asp:Label ID="lbl_ativo2" runat="server" Text=""></asp:Label>
                            </td>
                                <td style="width:50px">
                                    <asp:LinkButton ID="lbtn_editar_paciente" runat="server" CommandName="lbtn_editar_paciente" ForeColor="Blue">Editar</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                        
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DentCareConnectConnectionString %>" SelectCommand="SELECT * FROM Pacientes WHERE PacienteID != 12"></asp:SqlDataSource>
                    </table>
                </asp:Panel>                     
                </center>
                <center>
                    <asp:Panel ID="panel_editar_pacientes" runat="server" Visible="false">
                        <style>
                            .table-editar{
                                padding-bottom:5px;
                            }
                        </style>
                        <h3><asp:Label ID="lbl_nome_paciente" runat="server" Text=""></asp:Label></h3>
                        <br />
                        <table>
                            <tr>
                                <td class="table-editar">
                                    Morada: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_morada" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Código-Postal: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_cp" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Cidade: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_cidade" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Data de Nascimento: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_dataNascimento" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Género: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_genero" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Telemóvel: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_tlmvl" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    NIF: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_nif" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Email: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Username: 
                                </td>
                                <td class="table-editar">
                                    <asp:Label ID="lbl_username" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="table-editar">
                                    Ativo: 
                                </td>
                                <td class="table-editar">
                                    Sim <asp:RadioButton ID="rbtn_simP" runat="server" GroupName="AtivoP"/> Não <asp:RadioButton ID="rbtn_naoP" runat="server" GroupName="AtivoP"/>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Button ID="btn_gravar_paciente" runat="server" Text="Gravar" CssClass="appointment-btn" BorderStyle="None" OnClick="btn_gravar_paciente_Click" Width="100"/>
                    </asp:Panel>
                </center>

                <%--Secção Adicionar Médicos--%>
                <style>
                    .td-label{
                        width: 160px;
                        text-align:right; 
                        padding-right:10px
                    }
                    .td-textbox{
                        padding-bottom:5px; 
                        text-align:left
                    }
                </style>
                <asp:Panel ID="panel_adicionar_medico" runat="server" Visible="False">

                    <h3>Adicionar novo médico</h3>
                    <br />
                    <table style="width:700px; margin:auto">
                        <tr>
                            <td class="td-label">
                                Nome:
                            </td>
                            <td class="td-textbox">
                                <asp:TextBox ID="tb_add_nome" runat="server" Width="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Clinica:
                            </td>
                            <td class="td-textbox">
                                <asp:DropDownList ID="ddl_add_clinica" runat="server" Width="250"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Seguro:
                            </td>
                            <td class="td-textbox">
                                <asp:DropDownList ID="ddl_add_seguro" runat="server" Width="250"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Especialidade:
                            </td>
                            <td class="td-textbox">
                                <asp:DropDownList ID="ddl_add_especialidade" runat="server" Width="250"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Salário:
                            </td>
                            <td class="td-textbox">
                                <asp:TextBox ID="tb_add_salario" runat="server" Width="250"></asp:TextBox> €
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Descrição:
                            </td>
                            <td class="td-textbox">
                                <asp:TextBox ID="tb_add_descricao" runat="server" Width="250" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Foto:
                            </td>
                            <td class="td-textbox">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="330"/>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="* Formato salário incorreto (ex.: 1000,00)" ValidationExpression="^[-,0-9]+$" ControlToValidate="tb_add_salario" ForeColor="Red"></asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <asp:Button ID="btn_add_medico" runat="server" Text="Adicionar Médico" CssClass="appointment-btn" BorderStyle="None" OnClick="btn_add_medico_Click"/>
                </asp:Panel>

                <%--Secção Nova Clinica--%>
                <style>
                    .td-label{
                        width: 160px;
                        text-align:right; 
                        padding-right:10px
                    }
                    .td-textbox{
                        padding-bottom:5px; 
                        text-align:left
                    }
                </style>
                <asp:Panel ID="panel_nova_clinica" runat="server" Visible="False">

                    <h3>Adicionar nova clinica</h3>
                    <br />
                    <table style="width:700px; margin:auto">
                        <tr>
                            <td class="td-label">
                                Nome da clinica:
                            </td>
                            <td class="td-textbox">
                                <asp:TextBox ID="tb_clinica" runat="server" Width="250"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td-label">
                                Localidade:
                            </td>
                            <td class="td-textbox">
                                <asp:TextBox ID="tb_localidade" runat="server" Width="250"></asp:TextBox>
                            </td>
                        </tr>                        
                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btn_add_clinica" runat="server" Text="Adicionar Clinica" CssClass="appointment-btn" BorderStyle="None" OnClick="btn_add_clinica_Click"/>
                </asp:Panel>
            </div>
        </div>
    </asp:Panel>   
</asp:Content>
