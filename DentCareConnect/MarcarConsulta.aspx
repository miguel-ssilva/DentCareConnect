<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="MarcarConsulta.aspx.cs" Inherits="DentCareConnect.MarcarConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <center><h2>Marcação de consultas</h2></center>
        <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">
            Especialidade: <asp:DropDownList ID="ddl_especialidades" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_especialidades_SelectedIndexChanged" OnDataBound="ddl_especialidades_DataBound" Width="235"></asp:DropDownList>
            <br /><br />
            Clinicas: <asp:DropDownList ID="ddl_clinicas" runat="server" Width="270" Enabled="False" OnDataBound="ddl_clinicas_DataBound" AutoPostBack="True" OnSelectedIndexChanged="ddl_clinicas_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />
            Médico: <asp:DropDownList ID="ddl_medicos" runat="server" Width="280" AutoPostBack="true" OnSelectedIndexChanged="ddl_medicos_SelectedIndexChanged" OnDataBound="ddl_medicos_DataBound" Enabled="False"></asp:DropDownList>
            <br /><br />
            Tratamento: <asp:DropDownList ID="ddl_tratamentos" runat="server" Enabled="False" Width="250" OnDataBound="ddl_tratamentos_DataBound" OnSelectedIndexChanged="ddl_tratamentos_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />
            <center>
                <asp:Calendar ID="calendar_consulta" runat="server" OnDayRender="calendar_consulta_DayRender" Width="300" OnSelectionChanged="calendar_consulta_SelectionChanged"></asp:Calendar>
            </center>
            <br />
            Horas disponíveis: <asp:DropDownList ID="ddl_horario_consulta" runat="server" Width="70" Enabled="False" OnDataBound="ddl_horario_consulta_DataBound" OnSelectedIndexChanged="ddl_horario_consulta_SelectedIndexChanged"></asp:DropDownList>
            <br /><br />
            <asp:Label ID="lbl_mensagem" runat="server" Visible="False" ForeColor="Red"></asp:Label>
            <br /><br />
            <asp:Button ID="btn_marcar" runat="server" Text="Marcar consulta" CssClass="appointment-btn" BorderStyle="None" Width="200"  OnClick="btn_marcar_Click"/>
        </div>
    </div>
</asp:Content>
