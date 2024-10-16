<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Recuperacao.aspx.cs" Inherits="DentCareConnect.Recuperacao" %>
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
            <h4>Recuperação da sua palavra-passe</h4>
            <br />
            <table style="width:400px; margin:auto">
                <tr>
                    <td class="td-label">
                        Indique o seu email: 
                    </td>
                    <td class="td-textbox">
                        <asp:TextBox ID="tb_email" runat="server" Width="250"></asp:TextBox>         
                    </td>
                </tr>
            </table>
            <div style="margin-top:20px">
                <asp:Button ID="btn_recuperar" CssClass="appointment-btn" runat="server" Text="Recuperar" BorderStyle="None" Width="200" OnClick="btn_recuperar_Click"/>
                <br />
                <br />
                <p><asp:Label ID="lbl_mensagem" runat="server" ForeColor="#CC0000"></asp:Label></p>
            </div>
        </div>
    </div>
</asp:Content>
