<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DentCareConnect.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .td-label{
            text-align:right; 
            padding-right:10px
        }
        .td-textbox{
            padding-bottom:10px; 
            text-align:left
        }
    </style>
    <div class="container">
        <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">
            <h4>Entrar na sua conta</h4>
            <br />
            <table style="width:300px; margin:auto">
                <tr>
                    <td class="td-label">
                        Username: 
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_username" runat="server" Width="200"></asp:TextBox>         
                    </td>
                </tr>
                <tr>
                    <td class="td-label">
                        Password:
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_password" runat="server" Width="200" TextMode="Password"></asp:TextBox>        
                    </td>
                </tr>
            </table>
            <div style="margin-top:20px">
                <asp:Button ID="btn_login" CssClass="appointment-btn" runat="server" Text="Entrar" BorderStyle="None" Width="200" OnClick="btn_login_Click"/>
                <br />                
                <asp:LinkButton ID="lbtn_recuperar" runat="server" Font-Size="X-Small" OnClick="lbtn_recuperar_Click">Recuperar Password</asp:LinkButton> | 
                <asp:LinkButton ID="lbtn_registar" runat="server" Font-Size="X-Small" OnClick="lbtn_registar_Click">Registar-se</asp:LinkButton>   
                <br />
                <br />
                <p><asp:Label ID="lbl_mensagem" runat="server" ForeColor="#CC0000"></asp:Label></p>
            </div>
        </div>
    </div>
</asp:Content>
