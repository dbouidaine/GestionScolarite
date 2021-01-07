Imports System.Data.SqlClient
Imports System.DateTime
Imports System.IO
Imports System.Web.UI.DataVisualization.Charting

Public Class page3
    Inherits System.Web.UI.Page
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = Path.Remove(Path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")

    Dim Command As New SqlCommand
    Dim TAB(5) As LinkButton
    Dim MonReader As SqlDataReader
    Dim Adaptateur As New SqlDataAdapter(Command)
    Dim MonDataSet As New DataSet
    Dim master1 As New Site1
    Dim ctrName As Integer = 0


    Structure Donnees
        Dim matricule As String
        Dim jour As Date
        Dim just, typ_abs As String
    End Structure
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                Session("Gr") = Request.QueryString("Gr").ToString
                Session("Code_Mat") = Request.QueryString("Code_Mat").ToString
            Catch ex As Exception
                Response.Redirect("Enseignant.aspx")
            End Try
            If Not Session("Gr") Is Nothing Or Not Session("Code_Mat") Is Nothing Then

                CodeEns.Text = Session("Code_Ens").ToString
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = "SELECT * from ENSEIGNANT Where Code_Ens='" & CodeEns.Text & "'"
                MonReader = Command.ExecuteReader()
                If MonReader.Read() Then
                    Gr.Text = Session("Gr")
                    CodeMat.Text = Session("Code_Mat")                  
                End If
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = "SELECT Promo from INSCRITMODULE Where Code_Mat='" & CodeMat.Text & "'"
                MonReader = Command.ExecuteReader()
                If MonReader.Read Then
                    Promo.Text = MonReader("Promo").ToString
                End If
                SqlDataSource1.ConnectionString = Connect.ConnectionString
                SqlDataSource2.ConnectionString = Connect.ConnectionString
                SqlDataSource3.ConnectionString = Connect.ConnectionString
                SqlDataSource4.ConnectionString = Connect.ConnectionString
                SqlDataSource5.ConnectionString = Connect.ConnectionString
                Connect.Close()
                Notes.ToolTip = "afficher la table des notes des etudiants pour modifier et envoyer"
                Abscences.ToolTip = "pour gérer les asbsences des etudiants"
                Statistiques.ToolTip = "pour afficher les statistique de groupe"
                Groupes.ToolTip = "pour affiche la liste des etudiants de groupe"
                Annuler.ToolTip = "annuler les modification"
                If (IsPostBack = False) Then
                    Groupes.CssClass = Groupes.CssClass & " active"
                    GetChartData()
                    Groupes.Text &= " : " & Gr.Text                   
                    mainview.ActiveViewIndex = 0

                Else
                    If click.Text = "Notes" Then
                        mainview.ActiveViewIndex = 1
                        SqlDataSource1.SelectCommand = "SELECT ETUDIANTS.Matricule, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.CcNote, INSCRITMODULE.CiNote, INSCRITMODULE.TpNote, INSCRITMODULE.CfNote, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "')  ORDER BY ETUDIANTS.NomEtud"
                    ElseIf click.Text = "Absences" Then
                        mainview.ActiveViewIndex = 2
                        SqlDataSource2.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule), ETUDIANTS.NomEtud, ETUDIANTS.Prenoms FROM (ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule) WHERE (INSCRITS.Promo = '" & Promo.Text & "') AND (INSCRITS.Gr = '" & Gr.Text & "') ORDER BY ETUDIANTS.NomEtud"
                    ElseIf click.Text = "Statistique" Then
                        GetChartData()
                        State.Visible = True
                        State.ViewStateMode = UI.ViewStateMode.Enabled
                    End If
                End If
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = "SELECT * from Utilisateurs"
                MonReader = Command.ExecuteReader()
                If MonReader.Read Then
                    dernier_delai.Text = MonReader("Dernier_Delai").ToString
                    Dim date1 As Date = MonReader("Debut_S1")
                    Dim date2 As Date = MonReader("Debut_S2")
                    If Date.Compare(DateAndTime.Today, date1) >= 0 Then
                        Semestre.Text = "1"
                    End If
                    If Date.Compare(DateAndTime.Today, date2) >= 0 Then
                        Semestre.Text = "2"
                    End If
                    If DateAndTime.Today > MonReader("Dernier_Delai") Then
                        Modifier.Enabled = False
                        Envoyer.Enabled = False
                    End If
                End If
                Connect.Close()
            Else
                Response.Redirect("Enseignant.aspx")
            End If
        Catch ex As Exception
            Response.Redirect("Enseignant.aspx")
        Finally
            Connect.Close()
        End Try

    End Sub

    '---------button click--------
    Protected Sub Envoyer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Envoyer.Click
        Try
            Dim rech1 As String = "SELECT DISTINCT(ETUDIANTS.Matricule) ,INSCRITMODULE.CfNote, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "')  ORDER BY ETUDIANTS.Matricule"
            Dim Enregistre As String = "oui"
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = rech1
            Adaptateur.UpdateCommand = Command
            Dim dtbl As New DataTable
            Adaptateur.Fill(dtbl)
            For Each Ligne As DataRow In dtbl.Rows
                If Ligne("CfNote").ToString = Nothing Or Ligne("MoyMod").ToString = Nothing Then
                    Enregistre = "non"
                End If
            Next
            If Enregistre = "oui" Then
                Dim str1 As String = "INSERT INTO Enregistrer ([Code_Ens],[Code_Mat],[Gr],[Enregistre]) VALUES ('" & CodeEns.Text & "','" & CodeMat.Text & "','" & Gr.Text & "','" & Enregistre & "')"
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = str1
                Command.ExecuteNonQuery()
                Modifier.Visible = False
                Envoyer.Enabled = False
                Annuler.Visible = False
                Annuler.ViewStateMode = UI.ViewStateMode.Disabled
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-success alert-dismissible"
                message.Text = "les notes ont été envoyés avec succès"

            Else
                'Response.Write("<script language=""javascript"">alert('les notes ne sont pas envoyer, il faut les remlpir tous!');</script>")
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                message.Text = "les notes ne sont pas envoyer, il faut les remlpir tous!"
                'MsgBox("les notes ne sont pas envoyer, il faut les remlpir tous ")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    
    Protected Sub Notes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Notes.Click
        Try
            ActiveBar.Text = Notes.Text
            If Groupes.CssClass.Contains("active") Then
                Groupes.CssClass = Groupes.CssClass.Remove(Groupes.CssClass.LastIndexOf("a"))
            End If
            If Notes.CssClass.Contains("active") Then
                Notes.CssClass = Notes.CssClass.Remove(Notes.CssClass.LastIndexOf("a"))
            End If
            If Abscences.CssClass.Contains("active") Then
                Abscences.CssClass = Abscences.CssClass.Remove(Abscences.CssClass.LastIndexOf("a"))
            End If
            If Statistiques.CssClass.Contains("active") Then
                Statistiques.CssClass = Statistiques.CssClass.Remove(Statistiques.CssClass.LastIndexOf("a"))
            End If
            Notes.CssClass = Notes.CssClass & " active"
            Connect.Open()
            Command.Connection = Connect
            Dim str1 As String = "SELECT Enregistre FROM Enregistrer WHERE Code_Ens='" & CodeEns.Text & "' AND Code_Mat='" & CodeMat.Text & "' AND Gr='" & Gr.Text & "'"
            Command.CommandText = str1
            MonReader = Command.ExecuteReader()
            If (MonReader.Read()) Then
                Modifier.Visible = False
                Envoyer.Enabled = False
                Envoyer.ToolTip = "envoyer les notes à la DE"

            Else
                Modifier.Visible = True
                Modifier.Text = "Modifier"
                Modifier.ToolTip = "Modifier les notes des etudiants"
            End If
            If Date.Today.ToString > dernier_delai.Text Then
                Modifier.Visible = False
                Envoyer.Enabled = False
            End If
            mainview.ActiveViewIndex = 1
            GridView1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT DISTINCT(ETUDIANTS.Matricule), ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.CcNote, INSCRITMODULE.CiNote, INSCRITMODULE.TpNote, INSCRITMODULE.CfNote, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "') ORDER BY ETUDIANTS.NomEtud"
            click.Text = "Notes"
            Envoyer.Visible = True
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Groupes_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Groupes.Click
        Try
            ActiveBar.Text = Groupes.Text
            If Groupes.CssClass.Contains("active") Then
                Groupes.CssClass = Groupes.CssClass.Remove(Groupes.CssClass.LastIndexOf("a"))
            End If
            If Notes.CssClass.Contains("active") Then
                Notes.CssClass = Notes.CssClass.Remove(Notes.CssClass.LastIndexOf("a"))
            End If
            If Abscences.CssClass.Contains("active") Then
                Abscences.CssClass = Abscences.CssClass.Remove(Abscences.CssClass.LastIndexOf("a"))
            End If
            If Statistiques.CssClass.Contains("active") Then
                Statistiques.CssClass = Statistiques.CssClass.Remove(Statistiques.CssClass.LastIndexOf("a"))
            End If
            Groupes.CssClass = Groupes.CssClass & " active"
            Annuler.Visible = False
            Annuler.ViewStateMode = UI.ViewStateMode.Disabled
            Groupe1.Visible = True
            SqlDataSource1.SelectCommand = "SELECT DISTINCT(ETUDIANTS.Matricule), ETUDIANTS.NomEtud, ETUDIANTS.Prenoms FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "')  ORDER BY ETUDIANTS.NomEtud"
            mainview.ActiveViewIndex = 0
            Envoyer.Visible = False
            Modifier.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Abscences_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Abscences.Click
        Try
            ActiveBar.Text = Abscences.Text
            If Groupes.CssClass.Contains("active") Then
                Groupes.CssClass = Groupes.CssClass.Remove(Groupes.CssClass.LastIndexOf("a"))
            End If
            If Notes.CssClass.Contains("active") Then
                Notes.CssClass = Notes.CssClass.Remove(Notes.CssClass.LastIndexOf("a"))
            End If
            If Abscences.CssClass.Contains("active") Then
                Abscences.CssClass = Abscences.CssClass.Remove(Abscences.CssClass.LastIndexOf("a"))
            End If
            If Statistiques.CssClass.Contains("active") Then
                Statistiques.CssClass = Statistiques.CssClass.Remove(Statistiques.CssClass.LastIndexOf("a"))
            End If

            Abscences.CssClass = Abscences.CssClass & " active"
            Annuler.Visible = False
            Annuler.ViewStateMode = UI.ViewStateMode.Disabled
            mainview.ActiveViewIndex = 2
            Grid.Visible = True
            SqlDataSource2.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule), ETUDIANTS.NomEtud, ETUDIANTS.Prenoms FROM (ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule) WHERE (INSCRITS.Promo = '" & Promo.Text & "') AND (INSCRITS.Gr = '" & Gr.Text & "') ORDER BY ETUDIANTS.NomEtud"
            click.Text = "Absences"
            Envoyer.Visible = False
            Modifier.Visible = True
            If Date.Today.ToString > dernier_delai.Text Then
                Modifier.Enabled = False
            End If
            Modifier.Text = "Inserer"
            Modifier.ToolTip = "inserer les absences des etudiants"
            GridView1.Columns(0).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Modifier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Modifier.Click
        Dim cond As Boolean = True
        Dim modifsucc As Boolean = True
        Dim cond1 As Boolean = False
        Try
            If Modifier.Text = "Modifier" Then
                For Each item As GridViewRow In GridView1.Rows
                    If item.RowType = DataControlRowType.DataRow Then
                        edition("Label1", "TextBox1", item)
                        edition("Label2", "TextBox2", item)
                        edition("Label3", "TextBox3", item)
                        edition("Label4", "TextBox4", item)
                        edition("Label5", "TextBox5", item)
                    End If
                Next
                Modifier.ToolTip = "sauvegarder les modification "
                Envoyer.Visible = False
                Envoyer.ViewStateMode = UI.ViewStateMode.Disabled
                Modifier.Text = "Confirmer"
                Annuler.Visible = True
                Annuler.ViewStateMode = UI.ViewStateMode.Enabled
            ElseIf Modifier.Text = "Confirmer" Then
                cond = True
                Modifier.ToolTip = "Modifier les notes des etudiants"
                For Each item As GridViewRow In GridView1.Rows
                    If item.RowType = DataControlRowType.DataRow Then

                            Mise_A_Jour("Label1", "TextBox1", "CcNote", item, cond)
                            If cond = False Then
                                modifsucc = False
                                cond = True
                            End If
                            Mise_A_Jour("Label2", "TextBox2", "TpNote", item, cond)
                            If cond = False Then
                                modifsucc = False
                                cond = True
                            End If
                            Mise_A_Jour("Label3", "TextBox3", "CiNote", item, cond)
                            If cond = False Then
                                modifsucc = False
                                cond = True
                            End If
                            Mise_A_Jour("Label4", "TextBox4", "CfNote", item, cond)
                            If cond = False Then
                                modifsucc = False
                                cond = True
                            End If
                            Mise_A_Jour("Label5", "TextBox5", "MoyMod", item, cond)
                            If cond = False Then
                                modifsucc = False
                                cond = True
                            End If
                        End If
                Next
                If modifsucc = True Then
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-success alert-dismissible"
                    message.Text = "la modification terminé avec succès!"
                End If
                Envoyer.Visible = True
                Envoyer.ViewStateMode = UI.ViewStateMode.Enabled
                Annuler.Visible = False
                Annuler.ViewStateMode = UI.ViewStateMode.Disabled
                Modifier.Text = "Modifier"
            ElseIf Modifier.Text = "Inserer" Then
                Dim i As Integer = 0
                Dim d As New Donnees
                Dim b As Boolean = True
                Dim box As CheckBox
                For i = 0 To Grid.Rows.Count - 1
                    box = DirectCast(Grid.Rows(i).FindControl("CheckBox"), CheckBox)
                    b = box.Checked
                    If (b) Then
                        cond1 = True
                        GetData(d, i, cond)
                        If cond = True Then
                            Absence(d)
                        Else
                            Exit For
                        End If
                    End If
                Next
                If cond1 = False Then
                    '    Response.Write("<script language=""javascript"">alert('Il faut selectionner au moins un étudaint');</script>")
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                    message.Text = "Il faut selectionner au moins un étudaint"
                End If
                'MsgBox("Il faut selectionner au moins un étudaint")
                If cond = True And cond1 = True Then
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-success alert-dismissible"
                    message.Text = "l'insertion terminé avec succès!"
                End If
                RadioButtonList.ClearSelection()
                DateBox.Text = ""
                Grid.DataBind()
                End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Annuler_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Annuler.Click
        Try
            For Each item As GridViewRow In GridView1.Rows
                If item.RowType = DataControlRowType.DataRow Then
                    Annulation("Label1", "TextBox1", item)
                    Annulation("Label2", "TextBox2", item)
                    Annulation("Label3", "TextBox3", item)
                    Annulation("Label4", "TextBox4", item)
                    Annulation("Label5", "TextBox5", item)
                End If
            Next
            Modifier.ToolTip = "Modifier les notes des etudiants"
            Envoyer.Visible = True
            Envoyer.ViewStateMode = UI.ViewStateMode.Enabled
            Annuler.Visible = False
            Annuler.ViewStateMode = UI.ViewStateMode.Disabled
            Modifier.Text = "Modifier"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Statistiques_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Statistiques.Click
        Try
            ActiveBar.Text = Statistiques.Text
            If Groupes.CssClass.Contains("active") Then
                Groupes.CssClass = Groupes.CssClass.Remove(Groupes.CssClass.LastIndexOf("a"))
            End If
            If Notes.CssClass.Contains("active") Then
                Notes.CssClass = Notes.CssClass.Remove(Notes.CssClass.LastIndexOf("a"))
            End If
            If Abscences.CssClass.Contains("active") Then
                Abscences.CssClass = Abscences.CssClass.Remove(Abscences.CssClass.LastIndexOf("a"))
            End If
            If Statistiques.CssClass.Contains("active") Then
                Statistiques.CssClass = Statistiques.CssClass.Remove(Statistiques.CssClass.LastIndexOf("a"))
            End If
            Statistiques.CssClass = Statistiques.CssClass & " active"
            Annuler.Visible = False
            Annuler.ViewStateMode = UI.ViewStateMode.Disabled
            Envoyer.Visible = False
            Modifier.Visible = False
            click2.Text = ""
            Dim Messagemoy, NomMajor, PrenomMajor, MoyMajor As String
            Messagemoy = "" : NomMajor = "" : PrenomMajor = "" : MoyMajor = ""
            ModifeStat(NomMajor, PrenomMajor, MoyMajor, Messagemoy)
            nbAbs()
            MajClass0.Text = NomMajor
            MajClass1.Text = PrenomMajor
            MajClass2.Text = MoyMajor
            MoyClass.Text = Messagemoy
            If click.Text <> "Statistique" Then
                GetChartData()
            End If
            click.Text = "Statistique"
        Catch ex As Exception
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub sup16_Click(ByVal sender As Object, ByVal e As EventArgs) Handles sup16.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = sup16.Text
            SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule),NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "' and INSCRITMODULE.MoyMod>=16 )ORDER BY INSCRITMODULE.MoyMod"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try
    End Sub

    Protected Sub sup14_Click(ByVal sender As Object, ByVal e As EventArgs) Handles sup14.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = sup14.Text
            SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule),NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "' and INSCRITMODULE.MoyMod>=14 and INSCRITMODULE.MoyMod<16 )ORDER BY INSCRITMODULE.MoyMod"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try
    End Sub

    Protected Sub sup12_Click(ByVal sender As Object, ByVal e As EventArgs) Handles sup12.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = sup12.Text
            SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule),NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "' and INSCRITMODULE.MoyMod>=12 and INSCRITMODULE.MoyMod<14 )ORDER BY INSCRITMODULE.MoyMod"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try
    End Sub

    Protected Sub Moyenne_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Moyenne.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = Moyenne.Text
            Try
                State.ViewStateMode = UI.ViewStateMode.Enabled
                State.Visible = True
                click2.Visible = True
                click2.Text = Moyenne.Text
                SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule),NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "' and INSCRITMODULE.MoyMod>=10 and INSCRITMODULE.MoyMod<12 )ORDER BY INSCRITMODULE.MoyMod"
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                Connect.Close()
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Echoue_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Echoue.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = Echoue.Text
            SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule), NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "' and  INSCRITMODULE.MoyMod<10 and INSCRITMODULE.MoyMod>='" & NE.Text & "'  )ORDER BY INSCRITMODULE.MoyMod"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub NeLocal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles NeLocal.Click
        Try
            State.ViewStateMode = UI.ViewStateMode.Enabled
            State.Visible = True
            click2.Visible = True
            click2.Text = NeLocal.Text
            SqlDataSource4.SelectCommand = "SELECT DISTINCT(INSCRITS.Matricule), NbAbsences.NbAbs, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, INSCRITMODULE.MoyMod FROM ETUDIANTS INNER JOIN NbAbsences On ETUDIANTS.Matricule = NbAbsences.Matricule INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "') and  (INSCRITMODULE.MoyMod<" & NE.Text & "  )ORDER BY INSCRITMODULE.MoyMod"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try
    End Sub

    '------------------procedure-------

    Private Sub edition(ByVal str1 As String, ByVal str2 As String, ByVal item As GridViewRow)
        Dim label As New Label
        Dim box As New TextBox
        label = CType(item.FindControl(str1), Label)
        box = CType(item.FindControl(str2), TextBox)
        label.Visible = False
        box.Text = label.Text
        box.Visible = True
    End Sub

    Private Sub Annulation(ByVal str1 As String, ByVal str2 As String, ByVal item As GridViewRow)
        Try
            Dim label As New Label
            Dim box As New TextBox
            label = CType(item.FindControl(str1), Label)
            box = CType(item.FindControl(str2), TextBox)
            label.Visible = True
            box.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Private Sub Mise_A_Jour(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal item As GridViewRow, ByRef cond As Boolean)
        Try
            Dim matricule As Label = CType(item.FindControl("label0"), Label)
            Dim label As New Label
            Dim box As New TextBox
            Dim R As Double
            label = CType(item.FindControl(str1), Label) 'get the original value
            box = CType(item.FindControl(str2), TextBox) ' get the data to update
            Try
                R = CDbl(box.Text)
                If R > 20 Or R < 0 Then
                    'Response.Write("<script language=""javascript"">alert('Il faut entrer un note entre 0 et 20!!');</script>")
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                    message.Text = "Il faut entrer un note entre 0 et 20!!"
                    cond = False
                    '    MsgBox("Il faut entrer un note entre 0 et 20!!")
                Else
                    If label.Text <> box.Text Then
                        Try
                            Connect.Open()
                            Command.Connection = Connect
                            box.Text = box.Text.Replace(",", ".")
                            Command.CommandText = "UPDATE INSCRITMODULE SET [" & str3 & "] = '" & box.Text & "' WHERE Matricule ='" & matricule.Text & "' and Code_Mat='" & CodeMat.Text & "'"
                            Command.ExecuteNonQuery()
                            label.Text = box.Text

                        Catch ex As Exception
                            cond = False
                            ' Response.Write("<script language=""javascript"">alert('Il Faut entrer un type numerique');</script>")
                            myModal.Attributes.Add("style", "display:block")
                            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                            message.Text = "Il Faut entrer un type numerique"

                            '  MsgBox("Il Faut entrer un type numerique")
                        Finally
                            Connect.Close()
                        End Try
                    End If
                End If
            Catch ex As Exception
                If box.Text <> "" Then
                    '                    Response.Write("<script language=""javascript"">alert('Il Faut entrer un type numerique');</script>")
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                    cond = False
                    message.Text = "Il Faut entrer un type numerique"

                    'MsgBox("Il Faut entrer un type numerique")
                End If
            Finally
                label.Visible = True
                box.Visible = False
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Protected Sub Absence(ByVal d As Donnees)
        Dim rech, maj, ins As String
        Try
            Connect.Open()
            Command.CommandText = "select AnScol from INSCRITS"
            Adaptateur.UpdateCommand = Command
            Dim dtbl As New DataTable
            Adaptateur.Fill(dtbl)
            rech = "SELECT * From ABSENCES  WHERE Matricule = '" & d.matricule & "' AND Sem = 'S" & Semestre.Text & "' AND Jour = @Jour AND Matiere = '" & CodeMat.Text & "' AND TypAbs ='" & d.typ_abs & "' "
            maj = "UPDATE ABSENCES SET justifie = '" & d.just & "' WHERE Jour = @Jour1 AND Matricule = '" & d.matricule & "' AND Sem = 'S" & Semestre.Text & "' AND Matiere = '" & CodeMat.Text & "' AND TypAbs = '" & d.typ_abs & "' "
            ins = "INSERT INTO ABSENCES ([Anscol],[Sem],[Matricule],[Matiere],[Jour],[justifie],[TypAbs]) VALUES ('" & dtbl.Rows(0).Item(0) & "','S" & Semestre.Text & "','" & d.matricule & "','" & CodeMat.Text & "',@Jour2,'" & d.just & "','" & d.typ_abs & "')"
            Command.CommandText = rech
            Command.Parameters.AddWithValue("@Jour", d.jour)
            MonReader = Command.ExecuteReader()
            If (MonReader.Read()) Then
                Command.Parameters.AddWithValue("@Jour1", d.jour)
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = maj
                Try
                    Command.ExecuteNonQuery()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                Connect.Close()
            Else
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = ins
                Command.Parameters.AddWithValue("@Jour2", d.jour)
                Try
                    Command.ExecuteReader()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Command.Parameters.Clear()
            Connect.Close()
        End Try

    End Sub

    Sub GetData(ByRef D As Donnees, ByVal i As Integer, ByRef cond As Boolean)
        D.matricule = Grid.Rows(i).Cells(0).Text
        D.typ_abs = DropDownList.SelectedValue()
        D.just = RadioButtonList.SelectedValue()
        If D.just = "" Then
            'Response.Write("<script language=""javascript"">alert('Il faut remplir tous les champs');</script>")
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "Il faut remplir tous les champs"

            ' MsgBox("Il faut remplir tous les champs")
            cond = False
        Else
            Try
                D.jour = CType(DateBox.Text, Date)
            Catch ex As Exception
                cond = False
                '    Response.Write("<script language=""javascript"">alert('Il faut remplir tous les champs');</script>")
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                message.Text = "Il faut remplir tous les champs"

                ' MsgBox("Il faut remplir tous les champs")
            End Try
        End If
    End Sub

    Private Sub ModifeStat(ByRef NomMajor As String, ByRef PrenomMajor As String, ByRef MoyMajor As String, ByRef MessageMoy As String)
        Try
            Connect.Open()
            Command.Connection = Connect
            Dim cpt1, cpt2, cpt3, cpt4, cpt5, cpt6 As Integer
            Dim max, moy, NeLocal0 As Double
            Dim rech1, rech2, rech3 As String
            Dim txt As New TextBox
            rech2 = "SELECT DISTINCT INSCRITMODULE.MoyMod,ETUDIANTS.NomEtud,ETUDIANTS.Prenoms FROM ETUDIANTS INNER JOIN INSCRITS ON ETUDIANTS.Matricule = INSCRITS.Matricule INNER JOIN INSCRITMODULE ON ETUDIANTS.Matricule = INSCRITMODULE.Matricule WHERE (INSCRITS.Promo ='" & Promo.Text & "') AND (INSCRITS.Gr ='" & Gr.Text & "') AND (INSCRITMODULE.Code_Mat = '" & CodeMat.Text & "')ORDER BY ETUDIANTS.NomEtud"
            rech3 = ""
            Command.Connection = Connect
            Command.CommandText = rech2
            Adaptateur.UpdateCommand = Command
            cpt1 = 0 : cpt2 = 0 : cpt3 = 0 : moy = 0 : max = 0
            cpt4 = 0 : cpt5 = 0 : cpt6 = 0
            Dim dtbl As New DataTable
            Adaptateur.Fill(dtbl)
            For Each Ligne As DataRow In dtbl.Rows
                moy += Ligne("MoyMod").ToString
                If Ligne("MoyMod").ToString > max Then
                    max = Ligne("MoyMod").ToString
                    NomMajor = Ligne("NomEtud").ToString
                    PrenomMajor = Ligne("Prenoms").ToString
                End If
            Next
            moy = moy / dtbl.Rows.Count
            NeLocal0 = Math.Round((moy * 60) / 100, 2)
            MoyMajor = max
            MessageMoy = moy
            For Each Ligne As DataRow In dtbl.Rows
                If Ligne("MoyMod").ToString >= 16 Then
                    cpt1 += 1
                ElseIf Ligne("MoyMod").ToString < 16 And Ligne("MoyMod").ToString >= 14 Then
                    cpt2 += 1
                ElseIf Ligne("MoyMod").ToString < 14 And Ligne("MoyMod").ToString >= 12 Then
                    cpt3 += 1
                ElseIf Ligne("MoyMod").ToString < 12 And Ligne("MoyMod").ToString >= 10 Then
                    cpt4 += 1
                ElseIf Ligne("MoyMod").ToString < 10 And Ligne("MoyMod").ToString >= NeLocal0 Then
                    cpt5 += 1
                ElseIf Ligne("MoyMod").ToString < NeLocal0 Then
                    cpt6 += 1
                End If
            Next
            Echoue.ToolTip = "affiche les etudiants de moyenne superieur ou égale à " & NE.Text & " inferieur à 10"
            NeLocal.ToolTip = "affiche les etudiants de moyenne inferieur à " & NE.Text
            txt.Text = NeLocal0
            NE.Text = NeLocal0
            NE.Text = NE.Text.Replace(",", ".")
            Connect.Close()
            Connect.Open()
            Command.Connection = Connect
            rech1 = "SELECT * from Statistique where Code_Mat='" & CodeMat.Text & "' and Gr='" & Gr.Text & "'"
            rech2 = "UPDATE Statistique SET [NbAuSuMoy] =" & cpt4 & " ,[NbAuSoMoy] =" & cpt5 & " ,[NeLocal]=" & NE.Text & " ,[NbNeLocal]=" & cpt6 & " ,[Sup16] =" & cpt1 & " ,[Sup14] =" & cpt2 & "  ,[Sup12]=" & cpt3 & "  WHERE  Code_Mat='" & CodeMat.Text & "' and Gr='" & Gr.Text & "'"
            rech3 = "INSERT INTO Statistique ([Anscol],[Sem],[Gr],[Code_Mat],[Promo],[NbAuSuMoy],[Sup16],[Sup14],[Sup12],[NbAuSoMoy],[NbNeLocal],[NeLocal]) VALUES ('" & Date.Now.Year.ToString & "','S" & Semestre.Text & "','" & Gr.Text & "','" & CodeMat.Text & "','" & Promo.Text & "','" & cpt4 & "','" & cpt1 & "','" & cpt2 & "','" & cpt3 & "','" & cpt5 & "','" & cpt6 & "','" & NE.Text & "')"
            Chart1.Visible = True : Chart1.ViewStateMode = UI.ViewStateMode.Enabled
            Command.CommandText = rech1
            MonReader = Command.ExecuteReader()
            If (MonReader.Read()) Then
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = rech2
                Command.ExecuteNonQuery()
            Else

                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = rech3
                Command.ExecuteNonQuery()
            End If
        Catch ex As Exception
            ' Response.Write("<script language=""javascript"">alert('les statistiques des moyennes ne peuvent pas etre afficher car les notes nont encore remplis');</script>")
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "les statistiques des moyennes ne peuvent pas etre afficher car les notes n'ont encore remplis"

            sup12.Visible = False : sup14.Visible = False : sup16.Visible = False
            Moyenne.Visible = False : Echoue.Visible = False : NeLocal.Visible = False
            Chart1.Visible = False : Chart1.ViewStateMode = UI.ViewStateMode.Disabled
        Finally
            Connect.Close()
        End Try

    End Sub

    Private Sub GetChartData()
        Try
            Dim series As DataVisualization.Charting.Series = Chart1.Series("Series1")
            mainview.ActiveViewIndex = 3
            Connect.Open()
            Command.Connection = Connect
            Dim rech1 As String
            rech1 = "SELECT * from Statistique where Code_Mat='" & CodeMat.Text & "' and Gr='" & Gr.Text & "'"
            Command.CommandText = rech1
            MonReader = Command.ExecuteReader()
            If (MonReader.Read()) Then
                Dim n1, n4, n5, n6, n2, n3, moy As Double                
                n1 = MonReader("NbAuSuMoy")
                n2 = MonReader("NbAuSoMoy")
                n3 = MonReader("NbNeLocal")
                n4 = MonReader("Sup16")
                n5 = MonReader("Sup14")
                n6 = MonReader("Sup12")                
                moy = n1 + n2 + n3 + n5 + n4 + n6
                n1 = n1 * 100 / moy
                n2 = n2 * 100 / moy
                n3 = n3 * 100 / moy
                n4 = n4 * 100 / moy
                n5 = n5 * 100 / moy
                n6 = n6 * 100 / moy
                If n4 <> 0 Then
                    series.Points.AddXY(Math.Round(n4, 2) & "% >=16", MonReader("Sup16").ToString)
                    sup16.Visible = True
                    sup16.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("Sup16").ToString)
                    sup16.Visible = False
                    sup16.Enabled = False
                End If
                If n5 <> 0 Then
                    series.Points.AddXY(Math.Round(n5, 2) & "% >=14", MonReader("Sup14").ToString)
                    sup14.Visible = True
                    sup14.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("Sup14").ToString)
                    sup14.Visible = False
                    sup14.Enabled = False
                End If
                If n6 <> 0 Then
                    series.Points.AddXY(Math.Round(n6, 2) & "% >=12", MonReader("Sup12").ToString)
                    sup12.Visible = True
                    sup12.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("Sup12").ToString)
                    sup12.Visible = False
                    sup12.Enabled = False
                End If
                If n1 <> 0 Then
                    series.Points.AddXY(Math.Round(n1, 2) & "% >=10", MonReader("NbAuSuMoy").ToString)
                    Moyenne.Visible = True
                    Moyenne.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("NbAuSuMoy").ToString)
                    Moyenne.Visible = False
                Moyenne.Enabled = False
                End If
                If n2 <> 0 Then
                    series.Points.AddXY(Math.Round(n1, 2) & "% >=" & MonReader("NeLocal").ToString , MonReader("NbAuSoMoy").ToString)
                    Echoue.Visible = True
                    Echoue.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("NbAuSoMoy").ToString)
                    Echoue.Visible = False
                    Echoue.Enabled = False
                End If
                If n3 <> 0 Then
                    series.Points.AddXY(Math.Round(n3, 2) & "% <" & MonReader("NeLocal").ToString, MonReader("NbNeLocal").ToString)
                    NeLocal.Visible = True
                    NeLocal.Enabled = True
                Else
                    series.Points.AddXY("", MonReader("NbNeLocal").ToString)
                    NeLocal.Visible = False
                    NeLocal.Enabled = False
                End If    
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)
        '  MyBase.VerifyRenderingInServerForm(control)
    End Sub

    Private Sub nbAbs()
        Try
            Dim str1, str2 As String
            Dim tab(50), i, anscol As Integer
            str1 = "SELECT DISTINCT(Matricule),AnScol from INSCRITS WHERE (Gr ='" & Gr.Text & "') AND (Promo ='" & Promo.Text & "')"
            str2 = "SELECT DISTINCT(Matricule) from NbAbsences WHERE (Gr ='" & Gr.Text & "') AND (Promo ='" & Promo.Text & "') and (Code_Mat='" & CodeMat.Text & "')"
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = str1
            Adaptateur.UpdateCommand = Command
            i = 0
            Dim dtbl, dtbl2 As New DataTable
            Adaptateur.Fill(dtbl)
            For Each Ligne As DataRow In dtbl.Rows
                Connect.Close()
                Connect.Open()
                Command.Connection = Connect
                anscol = Ligne("AnScol").ToString
                str2 = "SELECT Matricule,AnScol FROM ABSENCES WHERE (Matiere ='" & CodeMat.Text & "') AND (Matricule = '" & Ligne("Matricule").ToString & "') AND (ABSENCES.Justifie = 'Non')"
                Command.CommandText = str2
                Adaptateur.UpdateCommand = Command
                Adaptateur.Fill(dtbl2)
                tab(i) = dtbl2.Rows.Count
                dtbl2.Clear()
                i += 1
            Next
            i = 0
            For Each Ligne As DataRow In dtbl.Rows
                str1 = "SELECT NbAbs from NbAbsences WHERE (Matricule ='" & Ligne("Matricule").ToString & "') and (Code_Mat='" & CodeMat.Text & "')"
                Connect.Close()
                Connect.Open()
                Command.CommandText = str1
                MonReader = Command.ExecuteReader()

                If (MonReader.Read()) Then
                    str1 = "UPDATE NbAbsences SET [NbAbs] ='" & tab(i) & "' WHERE  Matricule='" & Ligne("Matricule").ToString & "' and (Code_Mat='" & CodeMat.Text & "')"
                    Connect.Close()
                    Connect.Open()
                    Command.Connection = Connect
                    Command.CommandText = str1
                    Command.ExecuteNonQuery()
                    i += 1
                Else
                    str1 = "INSERT INTO NbAbsences ([Anscol],[Sem],[Gr],[Code_Mat],[Promo],[Matricule],[NbAbs]) VALUES ('" & anscol & "','S" & Semestre.Text & "','" & Gr.Text & "','" & CodeMat.Text & "','" & Promo.Text & "','" & Ligne("Matricule").ToString & "','" & tab(i) & "')"
                    Connect.Close()
                    Connect.Open()
                    Command.Connection = Connect
                    Command.CommandText = str1
                    Command.ExecuteNonQuery()
                    i += 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try

    End Sub
    Protected Sub fermer_click(ByVal sender As Object, ByVal e As EventArgs)
        myModal.Attributes.Add("style", "display:none")
    End Sub
End Class