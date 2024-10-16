<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="DentCareConnect.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
            <section id="home" class="slider" data-stellar-background-ratio="0.5">
                <div class="container">
                    <div class="row">
                         <div class="owl-carousel owl-theme">
                              <div class="item item-first">
                                   <div class="caption">
                                        <div class="col-md-offset-1 col-md-10">
                                             <h3>Conheça a equipa mais experiente na nossa área</h3>
                                             <h1>Os melhores profissionais</h1>
                                             <a id="a_medicos" runat="server" href="Medicos.aspx" class="section-btn btn btn-default smoothScroll">Nossos Médicos</a>
                                        </div>
                                   </div>
                              </div>
                              <div class="item item-second">
                                   <div class="caption">
                                        <div class="col-md-offset-1 col-md-10">
                                             <h3>Conheça a nossa história com mais de 25 anos</h3>
                                             <h1>Venha conhecer-nos</h1>
                                             <a id="a_about" runat="server" href="About.aspx" class="section-btn btn btn-default btn-gray smoothScroll">Sobre Nós</a>
                                        </div>
                                   </div>
                              </div>

                              <div class="item item-third">
                                   <div class="caption">
                                        <div class="col-md-offset-1 col-md-10">
                                             <h3>Subscreva a nossa Newsletter</h3>
                                             <h1>Noticias para sorrir</h1>
                                             <a href="#newsletter" class="section-btn btn btn-default btn-blue smoothScroll">Newsletter</a>
                                        </div>
                                   </div>
                              </div>
                         </div>
                    </div>
                </div>
            </section> 
        <section id="newsletter">
            <div class="container">
                <div class="col-md-12 col-sm-12" style="text-align:center; margin-top:20px">

                    <div class="col-md-6 col-sm-6">
                         <img src="images/news-image1.jpg" class="img-responsive" alt="">
                    </div>

                    <div class="col-md-6 col-sm-6">
                         <!-- CONTACT FORM HERE -->
                         <div id="appointment-form" role="form" method="post" action="#">

                              <!-- SECTION TITLE -->
                              <div class="section-title wow fadeInUp" data-wow-delay="0.4s" style="margin-top:20px">
                                   <h2>Newsletter</h2>
                              </div>

                              <div>
                                   <div class="col-md-12 col-sm-12">
                                       <asp:Label ID="lbl_email" runat="server" Text="Email" Font-Bold="True" Font-Size="Large"></asp:Label>
                                       <br />
                                       <asp:TextBox ID="tb_email" runat="server" Width="300" Font-Size="Large"></asp:TextBox>  
                                   </div>   
                                    
                                   <div class="col-md-12 col-sm-12" style="margin-top:50px">                                  
                                       <asp:Button ID="btn_submeter" runat="server" Text="Subscrever Newsletter" CssClass="appointment-btn" BorderStyle="None" Font-Size="X-Large" Width="300" OnClick="btn_submeter_Click"/>
                                       <br />
                                       <asp:Label ID="lbl_mensagem" runat="server" Text="" Visible="False" ForeColor="Red" Font-Size="X-Small"></asp:Label>
                                   </div>
                              </div>
                        </div>
                    </div>
               </div>
          </div>
        </section>
</asp:Content>
