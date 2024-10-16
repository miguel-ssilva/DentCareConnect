<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Contactos.aspx.cs" Inherits="DentCareConnect.Contactos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="appointment" data-stellar-background-ratio="3">
          <div class="container">
               <div class="row">

                    <div class="col-md-6 col-sm-6">
                         <img src="images/appointment-image.jpg" class="img-responsive" alt="">
                    </div>

                    <div class="col-md-6 col-sm-6">
                         <!-- CONTACT FORM HERE -->
                         <div id="appointment-form" role="form" method="post" action="#">

                              <!-- SECTION TITLE -->
                              <div class="section-title wow fadeInUp" data-wow-delay="0.4s">
                                   <h2>Contacte-nos</h2>
                              </div>

                              <div class="wow fadeInUp" data-wow-delay="0.8s">
                                   <div class="col-md-6 col-sm-6">
                                        <label for="name">Nome</label>
                                        <input type="text" class="form-control" id="name" runat="server" name="name" placeholder="Nome Completo">
                                   </div>

                                   <div class="col-md-6 col-sm-6">
                                        <label for="email">Email</label>
                                        <input type="email" class="form-control" id="email_origem" runat="server" name="email" placeholder="Email">
                                   </div>
                                   
                                   <div class="col-md-12 col-sm-12">
                                        <label for="telephone">Telemóvel</label>
                                        <input type="tel" class="form-control" id="phone" runat="server" name="phone" placeholder="Número telemóvel">
                                        <label for="Message">Mensagem</label>
                                        <textarea class="form-control" rows="5" id="message" runat="server" name="message" placeholder="Mensagem"></textarea>
                                       <center>
                                       <asp:Button ID="btn_submeter" CssClass="appointment-btn" Width="300" BorderStyle="None" runat="server" Text="Enviar" OnClick="btn_submeter_Click" />   
                                       </center>
                                   </div>
                              </div>
                        </div>
                    </div>

               </div>
          </div>
     </section>
</asp:Content>
