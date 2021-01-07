<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="note.aspx.vb" Inherits="web1.page5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

.table 
{
  border-collapse: collapse;
  width: 100%;
}
*
{
    box-sizing: border-box;
}
.th
{
    background-color : #8093a1
}
th, td 
{
  text-align: center;
  padding: 8px;
}
td
{
	font-size: 16px;
	
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="d-print-block ">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br /><br />
    <asp:Button ID="Button2" runat="server" Text="ImageEtud" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="ImageEns" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="Button4" runat="server" Text="Button" />
    <br />
    <asp:Image ID="Image1" runat="server" Height="97px" Width="187px" />
    <br /><br />
    <asp:Button ID="Button1" runat="server" Text="insert par default" />
    <br />
    <br />
    <br />
    </div>
    
     <button type="button" onclick="Imprimer()" class="btn btn-secondary"><i class="fas fa-print"></i> Imprimer</button>
     <script type ="text/javascript" >
         function Imprimer() {window.print(); }
     </script>
   </asp:Content>
