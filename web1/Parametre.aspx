<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Parametre.aspx.vb" Inherits="web1.page4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
<meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
  <link rel="stylesheet" href="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/css/bootstrap.css" />
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/fontawesome-free-5.7.2-web/css/all.css" />
  <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/js/bootstrap.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/jquery-3.3.1.js"></script>  
  <script type="text/javascript" src="htmlpages\Website_Bootstrap\Pages\Login_Page\Login.js"></script>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.css" />
<style type="text/css" >
.split.right img
{
	width: 75%;
	max-width: 300px;
	border-radius: 50%;
	box-shadow: 2px 2px 50px #C3C3C3;
}

</style>
<title>Les Parametres</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-col col-md-8 bg-white mr-5">
        <h2>Paramètres</h2>
			
			<br />
		<div id="myModal" runat="server"  class ="modal" >

  <!-- Modal content -->
  <div class="modal-content">
    <a runat="server" id="fermer12" href ="#" onserverclick="fermer_click" style ="width :10px;" ><span class="close" >&times;</span></a>
    <asp:Label ID="message" runat="server" Text=""></asp:Label>
  </div>

</div>
        	<!-- Nav tabs -->
			<ul class="nav nav-tabs d-print-none" data-step="6" data-intro="changer le mot de passe ou la photo de profil ">
	        <li class="nav-item">
		        <a class="nav-link active PassSTEPS" id="pass" href="#home" onclick ="call_password()" data-step="7" data-intro="cliquer ici pour changer le mot de passe"> Mot De Passe </a>
				</li>
				<li class="nav-item">
				    <a class="nav-link PhSTEPS" href="#menu1" id ="phot" onclick="call_photo()" data-step="7" data-intro="Ici, vous pouvez voir les ..., allez aux parametres pour changer le photo ou le mot de passe de votre profile ...etc">Photo</a>
				</li>
            </ul>
            <!-- Tab panes -->
			<div class="tab-content" >
			    <div id="home" class=" tab-pane active " data-step="8" data-intro="saisir l'ancien mot de passe , le nouveau mot de passe et confirmer le changement"><br>
				    <h3>Changer Mot De Pass</h3>
					<p>Vous pouvez changer votre mot de passe ici, assurer que votre mot 	de passe est fort pour que vous evitez ...</p>
					<br />
                    <div class="form-group" >               	    
                        <asp:TextBox ID="Box1" MaxLength= "20" runat="server" class="form-control" placeholder="Ancien mot de passe " name="text2"  
                        data-step="9" data-intro="saisir l'ancien mot de passe"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RFV1" runat="server" 
                            ErrorMessage="Ancien mot de passe est vide " ControlToValidate="Box1" 
                            ForeColor="#1EBD02" ValidationGroup="show1" Display="Dynamic"></asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="Box2" runat="server" MaxLength= "20" TextMode="Password"  class="form-control" placeholder="Nouveau mot de passe" 
                        name="text2" data-step="10" data-intro="saisir le nouveau mot de passe"> </asp:TextBox>
                        <p id="text" style="display: none; color: black;margin-left: 50px;">Attention! La touche vers Maj est active.</p>
						
                        <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                            ErrorMessage="Nouveau mot de passe est vide " ControlToValidate="Box2" 
                            ForeColor="#1EBD02" ValidationGroup="show1" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REV1" runat="server" 
                            ErrorMessage="Mot de pass >=7 catectere" ControlToValidate="Box2" 
                            ValidationExpression=".{7}.*" ValidationGroup="show1" Display="Dynamic" 
                            ForeColor="#CA00CA"></asp:RegularExpressionValidator>
                        <br />
                        <asp:TextBox ID="Box3" runat="server" MaxLength= "20" TextMode="Password" class="form-control" placeholder=" confirmation mot de passe" name="text2" 
                        data-step="11" data-intro="confirmer le nouveau mot de passe"> </asp:TextBox>
                        <p id="text2" style="display: none; color:black;margin-left: 50px;">Attention! La touche vers Maj est active.</p>
						
                        <asp:RequiredFieldValidator ID="RFV3" runat="server" 
                            ErrorMessage="confirmation mot de passe est vide" ControlToValidate="Box3" 
                            ForeColor="#1EBD02" ValidationGroup="show1" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ErrorMessage="la confirmation de Nouveau mot de passe est faux" ControlToValidate="Box2" 
                            ControlToCompare="Box3" ForeColor="#CA00CA" ValidationGroup="show1" 
                            Display="Dynamic"></asp:CompareValidator>
                        <br />
                        <asp:Button ID="Valide" runat="server" CssClass="btn btn-success" 
                            Text="Sauvgarder " ValidationGroup="show1" data-step="12" data-intro="Sauvegarder le changement"/> <br />
                        <br />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>		
                    </div> 
                </div>                 
                <div id="menu1" class=" tab-pane active " style ="display:none " ><br />
			        <h3>Changer Photo</h3>
				    <p>Vous pouvez changer votre photo ou bien laisser l'image par  default</p>
				    <br />
				    <form action ="">
      				    <div class="custom-file">	
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="PhSTEPS"  onchange="loadFile(event)" data-step="13" data-intro="choisir une photo de profil"/>
                            <br />
                           
                              	<br />
	    					<asp:Button ID="Changer1" runat="server" Text="changer" CssClass="btn btn-success PhSTEPS"  data-step="14" data-intro="Ici, vous pouvez voir les ..., allez aux parametres pour changer le photo ou le mot de passe de votre profile ...etc"/>
                            <br /><br /><img id="output" alt="" src="" height="240px" width="240px" />
                            	
                        </div>
                    </form> 
                </div> 
            </div> 
        <asp:Label ID="Label2" runat="server" Visible="false"  ></asp:Label>
       	<asp:Label Visible ="false"  ID="Label3" runat="server" Text="Label"></asp:Label>
    </div> 
    
<script type="text/javascript" >
    var loadFile = function (event) {
        var output = document.getElementById('output');
        output.src = URL.createObjectURL(event.target.files[0]);
    };
    function call_photo() {
        document.getElementById('menu1').style.display = "block";
        document.getElementById('home').style.display = "none";
        $("#pass").removeClass("active");
        $("#phot").addClass("active");
        
    }
    function call_password() {
        document.getElementById('home').style.display = "block";
        document.getElementById('menu1').style.display = "none";
        $("#phot").removeClass("active");
        $("#pass").addClass("active");
    }
    var input = document.getElementById("ContentPlaceHolder1_Box2");
    var text = document.getElementById("text");
    input.addEventListener("keyup", function (event) {
        if (event.getModifierState("CapsLock")) {
            text.style.display = "block";
        }
        else {
            text.style.display = "none"
        }
    });

    var input2 = document.getElementById("ContentPlaceHolder1_Box3");
    var text2 = document.getElementById("text2");
    input2.addEventListener("keyup", function (event2) {
        if (event2.getModifierState("CapsLock")) {
            text2.style.display = "block";
        }
        else {
            text2.style.display = "none"
        }
    });
</script>    
</asp:Content>
