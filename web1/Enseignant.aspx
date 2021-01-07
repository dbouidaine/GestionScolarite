<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Enseignant.aspx.vb" Inherits="web1.page6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/fontawesome-free-5.7.2-web/css/all.css"/>
  <link rel="stylesheet" href="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/css/bootstrap.css"/>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/jquery-3.3.1.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/js/bootstrap.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/popper.min.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Teacher_Page/Home/Home.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.js"></script>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Teacher_Page/Home/Home.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/print.css"/>
<title>Acceuil</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="my-col col-md-8 bg-white mr-md-4  d-print-table">
  <div id="myModal" runat="server"  class ="modal" >

  <!-- Modal content -->
  <div class="modal-content">
    <a runat="server" id="fermer12" href ="#" onserverclick="fermer_click" style ="width :10px;" ><span class="close" >&times;</span></a>
    <asp:Label ID="message" runat="server" Text=""></asp:Label>
  </div>

</div>
        <asp:Label ID="Label1" runat="server"  Visible = "false" Text = ""></asp:Label>
        <br />
        <h2> &nbsp;&nbsp;&nbsp; <asp:Label ID="Semestre" runat="server" Text=""></asp:Label></h2>
        <br />
        <br />
        <div style=" overflow :auto;">
            <asp:Panel  ID="Panel1" runat="server"  data-step="7" data-intro="Ici, vous pouvez voir les ..., allez aux parametres pour changer le photo ou le mot de passe de votre profile ...etc" >    <asp:Table ID="t" runat="server" >
                  </asp:Table></asp:Panel>
        </div>
        <br />
       
</div> 
</asp:Content>
