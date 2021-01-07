<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Saisie.aspx.vb" Inherits="web1.page3" EnableEventValidation="false" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Saisie</title>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/fontawesome-free-5.7.2-web/css/all.css"/>
  <link rel="stylesheet" href="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/css/bootstrap.css"/>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/jquery-3.3.1.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/popper.min.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/bootstrap-4.3.1-dist/js/bootstrap.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Teacher_Page/Home2/Home2.js"></script>
  <script type="text/javascript" src="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.js"></script>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/chart.js"></script>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/Master.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Teacher_Page/Home/Home.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Teacher_Page/Home2/Home2.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/Pages/Master_Page/print.css"/>
  <link rel="stylesheet" type="text/css" href="htmlpages/Website_Bootstrap/modale.css"/>
  <script type="text/javascript"src="htmlpages/Website_Bootstrap/modale.js"></script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <div class="my-col col-md-8 bg-white mr-md-4">
    <br />
    <div id="myModal" runat="server"  class ="modal" >

  <!-- Modal content -->
  <div class="modal-content">
    <a runat="server" id="fermer12" href ="#" onserverclick="fermer_click" style ="width :10px;" ><span class="close" >&times;</span></a>
    <asp:Label ID="message" runat="server" Text=""></asp:Label>
  </div>

</div>
<div class="d-print-none">
<asp:Label ID="ActiveBar" runat="server" Text="Groupes" CssClass="d-none " ></asp:Label>
        <asp:Label ID="dernier_delai" runat="server" Text="" Visible ="false" Enabled ="false"  ></asp:Label>
            <h2 > Semestre <asp:Label ID="Semestre" runat="server" Text=""></asp:Label></h2>
            <br />
     </div>
            <h2>
                Module :<asp:Label ID="CodeMat" runat="server"  ></asp:Label>
                <asp:Label ID="Gr" runat="server" Text="" Visible ="false" ></asp:Label>
            </h2>
            <ul class="nav nav-tabs"  data-step="8" data-intro="Vous pouvez saisir les notes, les absences et voir les statistiques du groupe sélectionné" >   
			    <li class="nav-item"  data-step="9" data-intro="En cliquant, vous trouverez la liste des étudiant de ce groupe" >   
			        <asp:LinkButton ID="Groupes" runat="server" CssClass="nav-link d-print-block">Groupe</asp:LinkButton>
                </li>
				<li class="nav-item"  data-step="10" data-intro="En cliquant ici, vous pouvez saisir les notes" >   
			        <asp:LinkButton ID="Notes" runat="server" CssClass="nav-link  d-print-none">Notes</asp:LinkButton>
                </li>
				<li class="nav-item"  data-step="11" data-intro="En cliquant ici, vous pouvez saisir les absences" >   
			        <asp:LinkButton CssClass="nav-link  d-print-none" ID="Abscences" runat="server" >Abscences</asp:LinkButton>
            	</li>
				<li class="nav-item"  data-step="12" data-intro="En cliquant ici, vous pouvez voir les statistiques" >   
			        <asp:LinkButton ID="Statistiques" CssClass="nav-link  d-print-none" runat="server">Statistiques</asp:LinkButton>
				</li>
            </ul>   
        <div class="tab-content">
            <asp:MultiView ID="mainview" runat ="server" EnableTheming="True" >
                <asp:View ID="view1" runat ="server">
              <br />
                    <div style="overflow :auto;  width :100%;" id="Groupe"  data-step="13" data-intro="La liste des étudiant de groupe" >   
                        <asp:GridView ID="Groupe1" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="Matricule" DataSourceID="SqlDataSource3" 
                            GridLines="None" >
                            <Columns>
                                <asp:BoundField DataField="Matricule" HeaderText="Matricule" ReadOnly="True" 
                                SortExpression="Matricule" />
                                <asp:BoundField DataField="NomEtud" HeaderText="NomEtud" 
                                SortExpression="NomEtud" />
                                <asp:BoundField DataField="Prenoms" HeaderText="Prenoms" 
                                SortExpression="Prenoms" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                            ConnectionString=" " 
                            SelectCommand="SELECT DISTINCT(ETUDIANTS.Matricule), ETUDIANTS.NomEtud, ETUDIANTS.Prenoms FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule WHERE (INSCRITS.Promo = @Promo) AND (INSCRITS.Gr = @Gr) ORDER BY ETUDIANTS.NomEtud
                            ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Promo" Name="Promo" PropertyName="Text" />
                                <asp:ControlParameter ControlID="Gr" Name="Gr" PropertyName="Text" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </asp:View>
                <asp:View ID="view2" runat ="server">
                    <br />
                    <h3>Notes</h3>
                    -Rechercher les etudiants
                    <input class="form-control d-print-none" id="myInput" type="text" placeholder="rechercher un étudiantS" />   
                    <br />
                    <div  style=" overflow :auto; "   id="home"  data-step="15" data-intro="le tableau dans lequel vous pouvez saisir les notes " >
                        <asp:GridView ID="GridView1" runat="server" Width="100%" 
                            AutoGenerateColumns="False"  DataSourceID="SqlDataSource1" 
                            DataKeyNames ="Matricule" BorderStyle="None" 
                            BorderWidth="0px" GridLines="None" HorizontalAlign="Left" ShowHeaderWhenEmpty="True" 
                         >
                            <Columns>
                                <asp:commandfield showeditbutton="true" Visible="false"
                                    edittext="Modifer"
                                    updatetext="Confirmer"
                                    canceltext="Annuler"
                                    headertext="Modifer"
                                />
                                <asp:TemplateField HeaderText="Matricule">
                                    <ItemTemplate>
                                        <asp:Label ID="Label0" Visible="true" runat="server" Enabled="true" Text='<%#Eval("Matricule") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NomEtud" HeaderText="Nom" 
                                    SortExpression="NomEtud" ReadOnly ="true"  ></asp:BoundField>
                                <asp:BoundField DataField="Prenoms" HeaderText="Prenoms" 
                                    SortExpression="Prenoms" ReadOnly ="true"  ></asp:BoundField>
                                <asp:TemplateField HeaderText="CcNote">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" Visible="true" runat="server" Enabled="true" Text='<%#Eval("CcNote") %>'></asp:Label>
                                        <asp:TextBox ID="TextBox1" Visible="false" runat="server" Text=''  BorderColor="#007BFF" BorderWidth="1px"  Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="TpNote">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" Visible="true" runat="server" Enabled="true" Text='<%#Eval("TpNote") %>'></asp:Label>
                                        <asp:TextBox ID="TextBox2" Visible="false" runat="server" Text=''  BorderColor="#007BFF" BorderWidth="1px"  Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CiNote">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" Visible="true" runat="server" Enabled="true" Text='<%#Eval("CiNote") %>'></asp:Label>
                                        <asp:TextBox ID="TextBox3" Visible="false" runat="server" Text=''   Width="60px"  BorderColor="#007BFF" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="CfNote">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" Visible="true" runat="server" Enabled="true" Text='<%#Eval("CfNote") %>'></asp:Label>
                                        <asp:TextBox ID="TextBox4" Visible="false" runat="server" Text='' Width="60px"  BorderColor="#007BFF" BorderWidth="1px" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Moyenne">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" Visible="true" runat="server" Enabled="true" Text='<%#Eval("MoyMod") %>'></asp:Label>
                                        <asp:TextBox ID="TextBox5" Visible="false" runat="server" Text='' Width="60px" BorderColor="#007BFF" BorderWidth="1px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                          </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString=" " 
                            SelectCommand =""
                            UpdateCommand ="UPDATE INSCRITMODULE SET [CcNote] =@CcNote ,[TpNote] =@TpNote ,[CiNote] =@CiNote ,[CfNote] =@CfNote,[MoyMod] =@MoyMod WHERE Matricule =' @Matricule ' and Code_Mat='@Code_Mat'"
                        >
                        </asp:SqlDataSource>
                    </div>
                </asp:View>
                <asp:View ID="view3" runat ="server">
                    <br />
						      <h3>Abscences</h3><br />
                 	
                        <div>Type D&#39;Absence :
                        <asp:DropDownList ID="DropDownList" CssClass="custom-select" runat="server" data-step="16" data-intro="choisir le type d'absences" >
                            <asp:ListItem>TD</asp:ListItem>
                            <asp:ListItem>TP</asp:ListItem>
                            <asp:ListItem>INTERRO</asp:ListItem>
                            <asp:ListItem>CI</asp:ListItem>
                            <asp:ListItem>CF</asp:ListItem>
                        </asp:DropDownList>
                        <br />                       
                        La Date :
                        <asp:TextBox ID="DateBox" runat="server"  CssClass =" form-control " TextMode="Date" data-step="17" data-intro="choisir la date de l'absence" ></asp:TextBox>
                        <br />
                         Justifie :<asp:RadioButtonList ID="RadioButtonList" runat="server"  Width="8px" 
                            RepeatDirection="Horizontal" data-step="18" data-intro="est ce que l'absence est justifié ou pas?" >
                            <asp:ListItem>Non</asp:ListItem>
                            <asp:ListItem>Oui</asp:ListItem>
                        </asp:RadioButtonList>
                        
                        </div>
                        -Rechercher les etudiants             
                    <input class="form-control d-print-none" id="myInput2" type="text" placeholder="Entrer quelque chose dans ce field pour la rechercher dans le tableau..." title="pour chercher les etudiants dans le tableau" data-step="19" data-intro="rechercher un étudiant dans ce groupe" />
		 					 
                        <div style=" overflow :auto; " id="menu1"  data-step="20" data-intro="la liste des étudiants pour saisir les abcenses" ><br />
                        <asp:GridView ID="Grid" runat="server" AutoGenerateColumns="False" Width="100%" 
                            DataSourceID="SqlDataSource2" ShowHeaderWhenEmpty="True" Visible ="False" GridLines="None" >
                             <Columns>
            <asp:BoundField DataField="Matricule" HeaderText="Matricule" 
                SortExpression="Matricule" />
            <asp:BoundField DataField="NomEtud" HeaderText="NomEtud" 
                SortExpression="NomEtud"  />
            <asp:BoundField DataField="Prenoms" HeaderText="Prenoms" 
                SortExpression="Prenoms"  />
            <asp:TemplateField>
            <ItemTemplate>
            <asp:CheckBox ID="CheckBox" runat="server" data-step="21" data-intro="sélectionner l'étudiant qui a été absent" />
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString=" " 
                            SelectCommand =""
                        >
                        </asp:SqlDataSource>
                        
                    </div>
                </asp:View>
                <asp:View ID="view4" runat ="server" >
                     <br />
                     <h3>Statistiques</h3><br />
                   <div  data-step="22" data-intro="le premier étudiant et la meilleur moyenne dans le groupe" >
                   <center ><h4>Premier de la classe</h4></center>
                    <table class="table table-bordered ">
                        <tr>
                            <td style =" width :75%">
                                <asp:Label ID="MajClass0" runat="server" Text=""></asp:Label>
                                &nbsp;<asp:Label ID="MajClass1" runat="server" Text=""></asp:Label>
                            </td>         
                            <td  width :25%">
                                <asp:Label ID="MajClass2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr> 
                    </table>
                   </div>
                    <br />  
                         <center >       
                     <div data-step="23" data-intro="voir la moyenne de la classe" >
                     <center ><h4>Moyenne de Classe</h4></center>
                    <table class="table table-bordered ">
                        <tr>
                            <td style =" width :75%">
                               Moyenne de Classe
                            </td>
                            <td style =" width :25%">
                                <asp:Label ID="MoyClass" runat="server" Height="28px"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                     </div>
                    <br />
                        </center>             
                    <br />
                    <div id="menu2" ><br />
                        <asp:Panel ID="Panel1" runat="server">
                           <center > 
                            <div class ="ContentPanel flex-fill">
                                <asp:Chart ID="Chart1" runat="server" Palette="None" data-step="25" 
                                     PaletteCustomColors="40, 167, 69; 0, 123, 255; 23, 162, 184; 108, 117, 125; 255, 193, 7; 220, 53, 69" 
                                     data-intro="voir les statistiques des moyennes dans un diagramme circulaire" >
                                    <series>
                                        <asp:Series ChartArea="ChartArea1" ChartType="Doughnut" Name="Series1" 
                                            Font="Times New Roman, 9.75pt, style=Bold" LabelBackColor="Black" 
                                            LabelBorderColor="Black" LabelForeColor="White">
                                        </asp:Series>
                                    </series>
                                    <chartareas>
                                        <asp:ChartArea AlignmentOrientation="All" BackColor="White" 
                                            BorderColor="Transparent" BorderDashStyle="Dash" BorderWidth="10" 
                                            Name="ChartArea1" ShadowColor="Transparent">
                                            <Area3DStyle Inclination="50" IsRightAngleAxes="False" 
                                                LightStyle="Realistic" PointDepth="150" Rotation="10" WallWidth="15" />
                                        </asp:ChartArea>
                                    </chartareas>
                                    <Titles>
                                        <asp:Title Name="LES  STATISTIQUE DES MOYENNES" 
                                            Text="LES  STATISTIQUE DES MOYENNES" Font="Calibri, 10.2pt, style=Bold">
                                        </asp:Title>
                                    </Titles>
                                    <BorderSkin BackColor="Transparent" BorderColor="Transparent" />
                                </asp:Chart>
                                
                                <asp:Chart ID="Chart2" runat="server"  
                                    EnableViewState="True" Height="296px" Width="415px" 
                                    DataSourceID="SqlDataSource5" Palette="None"
                                    data-step="24" data-intro="voir les statistiques des absences dans un diagramme des batons" >
                                    <series>
                                        <asp:Series ChartArea="ChartArea1" Name="Series1" 
                                            Font="Times New Roman, 9.75pt, style=Bold" LabelBackColor="Transparent" 
                                            LabelBorderColor="Transparent" LabelForeColor="White" XValueMember="NomEtud" 
                                            YValueMembers="NbAbs" BackGradientStyle="Center" 
                                            BackHatchStyle="DashedVertical" BorderDashStyle="NotSet" Color="Black" 
                                            CustomProperties="DrawSideBySide=False" LabelBorderDashStyle="NotSet" 
                                            Palette="Grayscale" ShadowColor="" ChartType="Bar" YValuesPerPoint="4">
                                        </asp:Series>
                                    </series>
                                    <chartareas>
                                        <asp:ChartArea AlignmentOrientation="All" BackColor="White" 
                                            BorderColor="Transparent" BorderDashStyle="Dash" BorderWidth="10" 
                                            Name="ChartArea1" ShadowColor="Transparent">
                                            <Area3DStyle Inclination="50" IsRightAngleAxes="False" 
                                                LightStyle="Realistic" PointDepth="150" Rotation="10" WallWidth="15" />
                                        </asp:ChartArea>
                                    </chartareas>
                                    <Titles>
                                        <asp:Title Name="LES STATISTIQUE DES ABSENCES" 
                                            Text="LES STATISTIQUE DES ABSENCES" 
                                            Font="Calibri, 10.2pt, style=Bold">
                                        </asp:Title>
                                    </Titles>
                                </asp:Chart>
                                <div data-step="26" data-intro="clé de lecture de diagramme circulaire"  ID="buttons">
                                    <br />
                                    <asp:Button ID="sup16" runat="server" CssClass="btn btn-success" 
                                        Text="Excellent" 
                                        ToolTip="affiche les etudiants de moyenne superieur ou égale à 16" 
                                        Visible="False" data-step="27" data-intro="affiche les etudiants de moyenne superieur ou égale à 16" />
                                    <asp:Button ID="sup14" runat="server" CssClass="btn btn-primary" 
                                        Text="Tres Bien" 
                                        ToolTip="affiche les etudiants de moyenne superieur ou égale à 14 inferieur à 16" 
                                        Visible="False" data-step="28" data-intro="affiche les etudiants de moyenne superieur ou égale à 14 inferieur à 16" />
                                    <asp:Button ID="sup12" runat="server" CssClass="btn btn-info" Text="Assez Bien" 
                                        ToolTip="affiche les etudiants de moyenne superieur ou égale à 12 inferieur à 14" 
                                        Visible="False" data-step="29" data-intro="affiche les etudiants de moyenne superieur ou égale à 12 inferieur à 14" />
                                    <asp:Button ID="Moyenne" runat="server" CssClass="btn btn-secondary" 
                                        text="Moyen" 
                                        ToolTip="affiche les etudiants de moyenne superieur ou égale à 10 inferieur à 12" 
                                        Visible="False" data-step="30" data-intro="affiche les etudiants de moyenne superieur ou égale à 10 inferieur à 12" />
                                    <asp:Button ID="Echoue" runat="server" CssClass="btn btn-warning" Text="Faible" 
                                        Visible="False" data-step="31" data-intro="affiche les etudiants de moyenne inférieur à 10" />
                                    <asp:Button ID="NeLocal" runat="server" CssClass="btn btn-danger" 
                                        Text="Note Elim" Visible="False" data-step="32" data-intro="affiche les etudiants de moyenne inférieur à la note éliminatoir"  />
                                </div>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                                    ConnectionString=" " 
                                    SelectCommand="SELECT NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms FROM NbAbsences INNER JOIN ETUDIANTS ON NbAbsences.Matricule = ETUDIANTS.Matricule WHERE (NbAbsences.Gr = @Gr) AND (NbAbsences.Promo = @Promo) AND (NbAbsences.Code_Mat = @Code_Mat) AND (NbAbsences.NbAbs>0)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="Gr" Name="Gr" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="Promo" Name="Promo" PropertyName="Text" />
                                        <asp:ControlParameter ControlID="CodeMat" Name="Code_Mat" PropertyName="Text" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            </center>
                            <div style ="overflow :auto ;" >
                                <center><h2>&nbsp;</h2>
                                    <h2>
                                        <asp:Label ID="click2" runat="server" Text="" Visible="false "></asp:Label>
                                    </h2>
                                </center> 
                                <asp:GridView ID="State" runat="server" DataSourceID="SqlDataSource4" 
                                    AutoGenerateColumns="False" BorderStyle="None" ViewStateMode="Disabled" 
                                    Visible="False" GridLines="None" data-step="33" data-intro="la liste des étudiants correspondante à l'appréciation choisis" >
                                    <Columns>
                                        <asp:BoundField DataField="Matricule" HeaderText="Matricule" 
                                            SortExpression="Matricule" ReadOnly="true"  />
                                        <asp:BoundField DataField="NomEtud" HeaderText="Nom" 
                                            SortExpression="NomEtud" ReadOnly ="true"  ></asp:BoundField>
                                        <asp:BoundField DataField="Prenoms" HeaderText="Prenoms" 
                                            SortExpression="Prenoms" ReadOnly ="true"  ></asp:BoundField>  
                                        <asp:BoundField DataField="MoyMod" HeaderText="Moyenne" 
                                            SortExpression="MoyMod" ReadOnly="true"  />
                                        <asp:BoundField DataField="NbAbs" HeaderText="Absence" 
                                            SortExpression="NbAbs" ReadOnly ="true"  ></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                                    ConnectionString=" " 
                                    SelectCommand="">
                                </asp:SqlDataSource>
                            </div>
                        </asp:Panel>
                        
                    </div>
                </asp:View>
            </asp:MultiView> 
        </div> 
        <br />
        <div class="mr-0" style="text-align: right;">
         <asp:Button ID="Annuler" runat="server" Text="Annuler" ViewStateMode="Disabled" Visible="False" CssClass="btn btn-secondary d-print-none" data-step="35" data-intro="Ici, vous pouvez voir les ..., allez aux parametres pour changer le photo ou le mot de passe de votre profile ...etc" />
       <asp:Button ID="Modifier" runat="server" Text="Modifier" Visible ="False" CssClass="btn btn-secondary d-print-none" data-step="34" data-intro="cliquer pour faire la saisie"  />
      <button type="button" onclick="Imprimer()" class="btn btn-secondary d-print-none" data-step="36" data-intro="cliquer pour imprimer " ></i> Imprimer</button>
		<asp:Button ID="Envoyer" runat="server" Text="Envoyer" Visible ="false"  CssClass="btn btn-success d-print-none" data-step="37" data-intro="cliquer pour envoyer les notes à l'administration" />
       <br />
        </div>
        <br />
       
    </div>    
   
   <asp:Label ID="Promo" runat="server" Text="" Visible ="false" ></asp:Label>
        <asp:Label ID="click" runat="server" Text="" Visible ="false" ></asp:Label>
         <asp:Label ID="CodeEns" runat="server" Visible="false"  ></asp:Label>
        <asp:Label ID="NE" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="commands" runat="server" Text="" Visible="false"></asp:Label>  
  <script type ="text/javascript" >
  

    //------------------------Pour Faire le guide----------------------
    function BarSelector() {

        var AB = document.getElementById("ContentPlaceHolder1_ActiveBar").innerHTML;
        switch (AB)
		{
			case 'Notes':
				StepsChangerN();
				break;
			case 'Abscences':
				StepsChangerA();
				break;
			case 'Statistiques':
				StepsChangerS();
				break;
			default:
				StepsChangerG();
				break;
		}
	}
	NodeList.prototype.forEach = NodeList.prototype.forEach || Array.prototype.forEach;
	function StepsChangerN()
	{
		document.querySelectorAll('[class*=ASTEPS],[class*=SSTEPS],[class*=GSTEPS]').forEach((function(x){ x.setAttribute("data-step","");}))
	}
	function StepsChangerA()
	{
		document.querySelectorAll('[class*=NSTEPS],[class*=SSTEPS],[class*=GSTEPS]').forEach((function(x){ x.setAttribute("data-step","");}))
	}
	function StepsChangerS()
	{
		document.querySelectorAll('[class*=NSTEPS],[class*=ASTEPS],[class*=GSTEPS]').forEach((function(x){ x.setAttribute("data-step","");}))
	}
	function StepsChangerG()
	{
		document.querySelectorAll('[class*=NSTEPS],[class*=ASTEPS],[class*=SSTEPS]').forEach((function(x){ x.setAttribute("data-step","");}))
	}	
	//-----------------------------------------------------------------
  </script>
</asp:Content>
