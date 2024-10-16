<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Medicos.aspx.cs" Inherits="DentCareConnect.Medicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .container-medico {
            display: inline-block;
            justify-content: space-around;
            padding: 20px;
        }
        .info-medico {
            min-height: 250px;
            width: 30%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .imagens {
            width: 100%;
            /*height: auto;*/
            /*width: 250px;
            height: 300px;*/
        }
        .medicos {
            width: 300px;
            height: 250px;
            margin: 15px; 
            margin-top: 30px;
            border-radius: 10px;
            border-style:solid;
        }   
        .rating {            
            display: flex;
            flex-direction: row-reverse;
            margin-top: 20px;
        }

        .rating input {
            display: none;
        }

        .rating label {
            cursor: pointer;
            width: 20px;
            height: 20px;
            margin-right: 5px;
            background-image: url('images/estrela-vazia.png');
            background-size: cover;
        }

        .rating input:checked ~ label {
            background-image: url('images/estrela.png');            
        }
        .btn-submeter{
            background: #a5c422;
            border-radius: 3px;
            color: #ffffff;
        }
    </style>
    <div class="container">
        <div style="text-align:left; margin-top:10px">
            <asp:LinkButton ID="lbtn_voltar" runat="server" Font-Underline="True" ForeColor="Blue" OnClick="lbtn_voltar_Click" Visible="False">Voltar</asp:LinkButton>
        </div>
        <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">            
            <div class="container-medico">
                <asp:Panel ID="panel_medicos" runat="server">
                <div>
                    <table style="margin-left:15px">
                        <tr>
                            <td style="width:150px; text-align:left">
                                <b>Filtrar por:</b>
                            </td>
                            <td style="padding-right:15px">
                                <asp:Label ID="lbl_clinica" runat="server" Text="Clinica"></asp:Label>
                            </td>
                            <td style="padding-right:30px">
                                <asp:DropDownList ID="ddl_clinicas" runat="server" AutoPostBack="True" OnDataBound="ddl_clinicas_DataBound" OnSelectedIndexChanged="ddl_clinicas_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="padding-right:15px">
                                <asp:Label ID="lbl_especialidade" runat="server" Text="Especialidade"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_especialidades" runat="server" AutoPostBack="True" OnDataBound="ddl_especialidades_DataBound" OnSelectedIndexChanged="ddl_especialidades_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>                        
                </div>                
                <br />
                <br />
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatColumns="3" RepeatDirection="Horizontal" DataKeyField="MedicoID" Height="430px" Width="340px" OnItemCommand="DataList1_ItemCommand" OnItemDataBound="DataList1_ItemDataBound">
                    <ItemTemplate>                         
                        <table class="medicos">                           
                            <tr>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="imgBtn_imagem" CommandName="imgBtn_imagem" runat="server" CssClass="imagens" ImageUrl='<%#"data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("Foto")) %>'/>
                                </td>
                            </tr> 
                            <tr>
                                <td style="text-align:center">
                                    <h3><b><%#Eval("Nome") %></b></h3>   
                                    <br />
                                </td>                    
                            </tr>
                            <tr>
                                <td style="text-align:center; margin-bottom:5px">
                                    <p style="margin-bottom: 2px; color: #a5c422">Especialidade</p>
                                    <p><%#Eval("Especialidade") %></p>
                                </td>                    
                            </tr>
                            <tr>
                                <td style="text-align:center">
                                    <p style="margin-bottom: 2px; color: #a5c422">Clinica</p>
                                    <p><%#Eval("Clinica") %></p>
                                </td>                    
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DentCareConnectConnectionString %>" SelectCommand="SELECT MedicoID, Nome, Clinica, Seguro, Especialidade, Salario, Ativo, DescricaoCarreira, Foto FROM Medicos m INNER JOIN Clinicas c ON m.ClinicaID = c.ClinicaID INNER JOIN Especialidades e ON m.EspecialidadeID = e.EspecialidadeID INNER JOIN Seguros s ON m.SeguroID = s.SeguroID WHERE Ativo = 'true'"></asp:SqlDataSource>
                </asp:Panel>
                <asp:Panel ID="panel_perfilMedico" runat="server" Visible="False">                    
                    <h2>
                        <asp:Label ID="lbl_nomeMedico" runat="server" Text=""></asp:Label>
                    </h2>
                    <br />
                    <div style="width:350px; height:250px">
                        <asp:Image ID="imageMedico" runat="server" Width="100%" Height="100%"/>
                    </div>
                    <div class="rating">                        
                        <input type="radio" id="star5" name="rating" value="5"/><label for="star5" title="5 estrelas"></label>
                        <input type="radio" id="star4" name="rating" value="4"/><label for="star4" title="4 estrelas"></label>
                        <input type="radio" id="star3" name="rating" value="3"/><label for="star3" title="3 estrelas"></label>
                        <input type="radio" id="star2" name="rating" value="2"/><label for="star2" title="2 estrelas"></label>
                        <input type="radio" id="star1" name="rating" value="1"/><label for="star1" title="1 estrela"></label>
                        <p style="margin-right:10px">Avaliação:</p>                        
                    </div>
                    <div style="text-align:right">
                        <asp:Button ID="btn_submeterAvaliacao" runat="server" CssClass="btn-submeter" BorderStyle="None" Height="25" Text="Submeter avaliação" OnClick="btn_submeterAvaliacao_Click" />
                        <br />
                        <asp:Label ID="lbl_avaliacao" runat="server" Text="" Visible="False" ForeColor="Red" Font-Size="X-Small"></asp:Label>
                    </div>
                    <asp:HiddenField ID="HiddenAvaliacao" runat="server" />
                    <div style="text-align:justify; width:350px; margin-top:20px">
                        <b>Descrição:</b>
                        <br />
                        <asp:Label ID="lbl_descricao" runat="server" Text=""></asp:Label>
                        <br /><br />
                        <b>Clinica:</b> <asp:Label ID="lbl_clinicaMedico" runat="server" Text=""></asp:Label>
                        <br /><br />
                        <b>Especialidade:</b> <asp:Label ID="lbl_especialidadeMedico" runat="server" Text=""></asp:Label>
                        <br /><br />
                        <b>Acordo de Seguro:</b> <asp:Label ID="lbl_seguroMedico" runat="server" Text=""></asp:Label>
                        <br /><br />
                        <h4 style="margin-bottom:0px; padding-bottom:0px">
                            <asp:Label ID="lbl_rating" runat="server" Text="" Visible="false"></asp:Label>
                        </h4>
                        <p style="margin-top:0px">
                            <asp:Label ID="lbl_ratingText" runat="server" Text=" de 5 estrelas"></asp:Label>
                        </p>
                    </div>
                    <div style="text-align:right">
                        <asp:Button ID="btn_marcarConsulta" runat="server" CssClass="appointment-btn" BorderStyle="None" Text="Marcar consulta" OnClick="btn_marcarConsulta_Click" />
                    </div>
                    
                </asp:Panel>
            </div>            
        </div>
    </div>
    <script>
        const stars = document.querySelectorAll('.rating input');

        stars.forEach(star => {
            star.addEventListener('change', () => {
                const valorAvaliacao = star.value;
                console.log(`Avaliação selecionada: ${valorAvaliacao} estrela(s)`);

                // Atualiza as estrelas selecionadas
                for (let i = 1; i <= 5; i++) {
                    const label = document.querySelector(`#star${i} + label`);
                    if (i <= valorAvaliacao) {
                        label.style.backgroundImage = "url('images/estrela.png')";
                    } else {
                        label.style.backgroundImage = "url('images/estrela-vazia.png')";
                    }
                }  

                document.getElementById('<%= HiddenAvaliacao.ClientID %>').value = valorAvaliacao;
            });
        });
    </script>
</asp:Content>
