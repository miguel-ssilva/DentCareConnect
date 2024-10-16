<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Registar.aspx.cs" Inherits="DentCareConnect.Registar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .td-label{
            text-align:right; 
            padding-right:10px
        }
        .td-textbox{
            padding-bottom:3px; 
            text-align:left
        }
    </style>
    <div class="container">
        <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">
            <h4>Registo de novo utilizador</h4>
            <br />
            <table style="width:700px; margin:auto">
                <tr>
                    <td class="td-label">
                        Nome: 
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_nome" runat="server" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_nome"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Morada:
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_morada" runat="server" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_morada"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Código-Postal:
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_cp" runat="server" Width="300"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="ex.: 1234-123" ForeColor="Red" ControlToValidate="tb_cp" ValidationExpression="^\d{4}(-\d{3})?$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Cidade:
                    </td>
                    <td class="td-textbox">
                        <asp:DropDownList ID="ddl_cidade" runat="server" Width="300">
                            <asp:ListItem></asp:ListItem>
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
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddl_cidade"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Género:
                    </td>
                    <td class="td-textbox">
                        <asp:DropDownList ID="ddl_genero" runat="server" Width="300">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>Masculino</asp:ListItem>
                            <asp:ListItem>Feminino</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddl_genero"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Data de Nascimento:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_datanascimento" runat="server" TextMode="Date" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_datanascimento"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        NIF:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_nif" runat="server" Width="300" MaxLength="9"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_nif"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Telemóvel:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_telemovel" runat="server" TextMode="Phone" Width="300" MaxLength="9"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="* inválido" ForeColor="Red" ControlToValidate="tb_telemovel" ValidationExpression="(9[1236]\d) ?(\d{3}) ?(\d{3})"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Email:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_email" runat="server" TextMode="Email" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_email"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="* inválido" ForeColor="Red" ControlToValidate="tb_email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Username:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_username" runat="server" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_username"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Password:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_password" runat="server" TextMode="Password" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_password"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Repetir Password:
                    </td>
                    <td class="td-textbox">                        
                        <asp:TextBox ID="tb_password2" runat="server" TextMode="Password" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="tb_password2"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <div style="margin-top:20px">
                <asp:Button ID="btn_registar" CssClass="appointment-btn" runat="server" Text="Registar" BorderStyle="None" Width="200" OnClick="btn_registar_Click"/>
                <br />
                <br />
                <p><asp:Label ID="lbl_mensagem" runat="server" ForeColor="#CC0000"></asp:Label></p>
            </div>
        </div>
    </div>
</asp:Content>
