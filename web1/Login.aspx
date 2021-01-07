<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="web1.Login2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>	
   <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
	<link rel="stylesheet" type="text/css" href="htmlpages\Website_Bootstrap\Pages\Login_Page\Login.css" />
	<script type="text/javascript" src="htmlpages\Website_Bootstrap\Pages\Login_Page\Login.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <a  id="Accueil" href="\htmlpages\Website_Bootstrap\Accueil\index.html"  
        style="position: fixed; top: 0px; right: 0px; text-decoration: none; color: #FF4F53;"> Accueil </a>
        <div class="container ">
			<div  id="choose" class="col-12">
				<div class="col-12" style="border-bottom: 7px solid #e6e6e6;">
					<img alt="" src="htmlpages/Website_Bootstrap/Logos/FULL.png" class="logo"/></div>
				<div class="choose">	
					<div class="choose_teacher col-6">
						<img  onclick="call_enseignant()" src="htmlpages/Website_Bootstrap/Logos/avatar1.png" alt="Connexion Enseignant" class="avatar" /><br />
						<input onclick="call_enseignant()" id="Button1"  style="color: #FF4F53" type="button" value="Enseignant" />
                        </div>
					<div class="choose_student col-6">
						<img onclick="call_etudiant()" src="htmlpages/Website_Bootstrap/Logos/avatar21.png" alt="Connexion Etudiant" class="avatar" /><br />
						<input onclick="call_etudiant()"  style="color: #FF4F53" id="Button2" type="button" name="" value="Etudiant" />
					</div>
				</div>
			</div>

            <form action ="">
               <div id="Enseignant_login" class="col-12" style="display: none;" >
				    <div class="col-12" style="border-bottom: 7px solid #e6e6e6;">
					    <img alt="" src="htmlpages/Website_Bootstrap/Logos/FULL.png" class="logo col-9" />
					    <div class="col-3">
                            <input id="Button3" type="button" name="Retour" value="Retour" onclick="back()" 
                                style="color: #FF4F53" /></div>
	    			</div>
		    	    <div id="login" class="login col-12" style="color : #ff0000;">
				        <div class="col-12"><img alt = "" src="htmlpages/Website_Bootstrap/Logos/avatar1.png" class="avatar_login" /></div>
				        <asp:TextBox ID="Username1" runat="server" placeholder="Nom d'utilisateur" ></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RFV1" runat="server" 
                            ErrorMessage="Nom d'utilisateur est vide" ControlToValidate="Username1" ValidationGroup="Enseignant_login"></asp:RequiredFieldValidator>
                        <br /> 
                        <asp:TextBox ID="password1" runat="server" placeholder="Mot de passe" TextMode ="Password" ></asp:TextBox>       
                        <br />
                        <p id="text" style="display: none; color: white;margin-left: 50px;">Attention! La touche vers Maj est active.</p>
						<br />
                        <asp:RequiredFieldValidator ID="RFV2" runat="server" 
                            ErrorMessage="Mot de passe est vide" ControlToValidate="password1" ValidationGroup="Enseignant_login"></asp:RequiredFieldValidator>
                        <br />                
                        <asp:Button ID="Con_Ens" runat="server" text="Connection"  
                             ValidationGroup="Enseignant_login" ForeColor="#FF4F53" /><br />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        
				    </div>
			    </div>
                <div id="Etudiant_login" class="col-12" style="display: none;">
				    <div class="col-12" style="border-bottom: 7px solid #e6e6e6;">
					    <img alt="" src="htmlpages/Website_Bootstrap/Logos/FULL.png" class="logo col-9" />
					    <div class="col-3">
                            <input id="Button4" type="button" name="Retour" value="Retour"  style="color: #FF4F53" onclick="back()" />
                        </div>
				    </div>
				    <div id="login2" class="login col-12" style="color : #ff0000;" >
					    <div class="col-12" ><img  alt="" src="htmlpages/Website_Bootstrap/Logos/avatar21.png" class="avatar_login" /></div>
					    <asp:TextBox ID="Username2" runat="server" placeholder="Nom d'utilisateur" ></asp:TextBox>
                        <br /> 
                        <asp:RequiredFieldValidator ID="RFV3" runat="server" 
                            ErrorMessage="Nom d'utilisateur est vide" ControlToValidate="Username2" 
                            ValidationGroup="Etudiant_login">
                        </asp:RequiredFieldValidator>
                        <asp:TextBox ID="password2" runat="server" placeholder="Mot de passe" TextMode="Password"></asp:TextBox>
                        <br />                  
                        <p id="text2" style="display: none; color: white;margin-left: 50px;">Attention! La touche vers Maj est active.</p>
						<br />
                        <asp:RequiredFieldValidator ID="RFV4" runat="server" 
                            ErrorMessage="Mot de passe est vide" ControlToValidate="password2" ValidationGroup="Etudiant_login">
                        </asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="Con_Etud" runat="server"  text="Connection" 
                            ValidationGroup ="Etudiant_login" ForeColor="#FF4F53"  /><br />
                        <asp:Literal ID="Literal2" runat="server"  ></asp:Literal>
                        
                    </div>
			    </div>
            </form>
        </div>
    </form>
<script type="text/javascript">
    function call_enseignant() {
        document.getElementById('Enseignant_login').style.display = "block";
        document.getElementById('choose').style.display = "none";
    }
    function call_etudiant() {
        document.getElementById('Etudiant_login').style.display = "block";
        document.getElementById('choose').style.display = "none";
    }
    function back() {

        document.getElementById('choose').style.display = 'block';
        document.getElementById('Enseignant_login').style.display = "none";
        document.getElementById('Etudiant_login').style.display = "none";
    }
    var input = document.getElementById("password1");
    var text = document.getElementById("text");
    input.addEventListener("keyup", function (event) {
        if (event.getModifierState("CapsLock")) {
            text.style.display = "block";
        }
        else {
            text.style.display = "none"
        }
    });

    var input2 = document.getElementById("password2");
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
    
</body>
</html>
