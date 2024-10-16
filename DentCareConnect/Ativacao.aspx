<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ativacao.aspx.cs" Inherits="DentCareConnect.Ativacao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ativação conta</title>
    <style>
     .appointment-btn {
        background: #a5c422;
        border-radius: 3px;
        color: #ffffff;
        font-weight: 600;
        padding-top: 12px;
        padding-bottom: 12px;
     }
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-12 col-sm-12" style="margin-left:15px">
            A sua conta encontra-se ativa!
            <br />
            <br />
            <asp:Button ID="btn_regressar" runat="server" CssClass="appointment-btn" Text="Regressar à página inicial" OnClick="btn_regressar_Click" />
        </div>
    </form>
</body>
</html>
