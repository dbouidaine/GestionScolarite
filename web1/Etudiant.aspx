<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Etudiant.aspx.vb" Inherits="web1.page2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Acceuil</title>
<link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/fontawesome-free-5.7.2-web/css/all.css" />
  <link rel="stylesheet" href="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/css/bootstrap.css" />
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/jquery-3.3.1.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/popper.min.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/js/bootstrap.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Student_Page/Student.js"></script>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.css" />
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Student_Page/Student.css" />
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/print.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     	<div class="my-col col-md-8 bg-white mr-md-4 d-print-block ">				
            <asp:Label ID="Label2" runat="server" Text="" Visible ="false"  ></asp:Label>
            <h2> <asp:Label ID="Label4" runat="server" Text=""></asp:Label></h2>
      <div id="myModal" runat="server"  class ="modal" >

  <!-- Modal content -->
  <div class="modal-content">
    <a runat="server" id="fermer12" href ="#" onserverclick="fermer_click" style ="width :10px;" ><span class="close" >&times;</span></a>
    <asp:Label ID="message" runat="server" Text=""></asp:Label>
  </div>

</div>
        
 			<br />                              
            <asp:GridView ID="ConsNoteS1" runat="server"  AutoGenerateColumns ="False"  
            Visible ="False" CssClass ="table " Width =" 100%" GridLines="None" data-step="8" data-intro="voir votre notes de premier semestre">
                <Columns><asp:BoundField DataField ="Code_Mat" HeaderText ="Module"/></Columns>
                <Columns><asp:BoundField DataField = "CcNote" HeaderText = "CcNote"/></Columns>
                <Columns><asp:BoundField DataField = "TpNote" HeaderText ="TpNote"/></Columns>
                <Columns><asp:BoundField DataField="CiNote" HeaderText ="CiNote"/></Columns>
                <Columns><asp:BoundField DataField="CfNote" HeaderText ="CfNote"/></Columns>
                <Columns><asp:BoundField DataField = "MoyMod" HeaderText ="Moyenne"/></Columns>
                <HeaderStyle CssClass ="th" /> 
           </asp:GridView>
           <br />
           <h2><asp:Label ID="Label7" runat="server" Text=""></asp:Label></h2>
        	<!--<input class="form-control d-print-none" id="Text1" type="text" placeholder="Entrer quelque chose dans ce field pour la rechercher dans le tableau..." />-->
 			<br />                              
           <asp:GridView ID="ConsNoteS2" runat="server" AutoGenerateColumns ="false" 
               Visible ="false" CssClass ="table " GridLines="None" Width="100%" data-step="9" data-intro="voir votre notes de premier semestre">
                <Columns><asp:BoundField DataField ="Code_Mat" HeaderText ="Module"/></Columns>
                <Columns><asp:BoundField DataField = "CcNote" HeaderText = "CcNote"/></Columns>
                <Columns><asp:BoundField DataField = "TpNote" HeaderText ="TpNote"/></Columns>
                <Columns><asp:BoundField DataField="CiNote" HeaderText ="CiNote"/></Columns>
                <Columns><asp:BoundField DataField="CfNote" HeaderText ="CfNote"/></Columns>
                <Columns><asp:BoundField DataField = "MoyMod" HeaderText ="Moyenne"/></Columns>
                <HeaderStyle CssClass ="th" /> 
           </asp:GridView>
           <br />          
          <!--  <div class="" style="text-align: right;">
			    <button type="button" onclick="Imprimenter()" class="btn btn-secondary"><i class="fas fa-print"></i> Imprimer</button>
			</div>-->
			<br>
        </div>
  
</asp:Content>
