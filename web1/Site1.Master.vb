Imports System.Data
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Imports System.DateTime
Public Class Site1
    Inherits System.Web.UI.MasterPage
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")

    Dim Command As New SqlCommand
    Dim MonReader As SqlDataReader
    Dim Adaptateur As New SqlDataAdapter(Command)
    Dim MonDataSet As New DataSet
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1))
        Response.Cache.SetNoStore()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cookies("userInfo", "Matricule", Response, Request, Session)
            cookies("userInfo2", "Code_Ens", Response, Request, Session)
            If Not Session("Matricule") Is Nothing Then
                Connect.Open()
                ETUDInfo.ConnectionString = Connect.ConnectionString
                Command.Connection = Connect
                Command.CommandText = "SELECT * from ETUDIANTS Where Matricule='" & Session("Matricule").ToString() & "'"
                MonReader = Command.ExecuteReader()
                If MonReader.Read() Then
                    Label1.Text = MonReader("NomEtud").ToString()
                    Label4.Text = MonReader("Prenoms").ToString()
                    Label2.Text = MonReader("Matricule").ToString()
                    Label3.Text = "ETUDIANTS"
                    Panel1.EnableViewState = False
                    Panel1.ViewStateMode = UI.ViewStateMode.Disabled
                    Panel1.Visible = False
                    Connect.Close()
                    Connect.Open()
                    Command.Connection = Connect
                    Command.CommandText = "SELECT * from INSCRITS Where Matricule='" & Session("Matricule").ToString() & "'"
                    img.ImageUrl = "images.ashx?Matricule=" & Label2.Text
                    MonReader = Command.ExecuteReader()
                    If MonReader.Read() Then
                        Label6.Text = MonReader("Promo").ToString() & "  Section: "
                        Label8.Text = MonReader("Sect").ToString()
                        Label7.Text = " Groupe : "
                        Label9.Text = MonReader("Gr").ToString()
                        Label6.Visible = True
                        Label8.Visible = True
                        Label7.Visible = True
                        Label9.Visible = True
                        Connect.Close()
                        Connect.Open()
                        Command.Connection = Connect
                        Command.CommandText = "SELECT DISTINCT INSCRITMODULE.MoyMod, INSCRITMODULE.Code_Mat, INSCRITMODULE.Matricule FROM INSCRITMODULE  INNER JOIN INSCRITS ON INSCRITMODULE.Matricule = INSCRITS.Matricule WHERE (INSCRITMODULE.Matricule = '" & Session("Matricule").ToString() & " ')"
                        Adaptateur.UpdateCommand = Command
                        Dim dtbl2 As New DataTable
                        Adaptateur.Fill(dtbl2)
                        Dim tauxdemoy As Integer = 0
                        For Each Ligne As DataRow In dtbl2.Rows
                            If Ligne("MoyMod").ToString <> Nothing Then
                                If Ligne("MoyMod").ToString >= 10 Then
                                    tauxdemoy += 1
                                End If
                            End If
                        Next
                        Label10.Text = "Taux de réussite: "
                        If dtbl2.Rows.Count > 0 Then
                            tauxdemoy = (tauxdemoy * 100) / dtbl2.Rows.Count
                        End If
                        Label11.Text = tauxdemoy & "%"
                        progressbar.Attributes.Add("style", "width:" & Label11.Text)
                    Else
                        Response.Redirect("Logout.aspx")
                    End If
                Else
                    Response.Redirect("Logout.aspx")
                End If
                Connect.Close()
            ElseIf Not Session("Code_Ens") Is Nothing Then
                Connect.Open()
                Panel2.EnableViewState = False
                Panel2.ViewStateMode = UI.ViewStateMode.Disabled
                Panel2.Visible = False
                Command.Connection = Connect
                Command.CommandText = "SELECT * from ENSEIGNANT Where Code_Ens='" & Session("Code_Ens").ToString() & "'"
                MonReader = Command.ExecuteReader()
                If MonReader.Read() Then
                    Dim nom, prenom, str As String
                    str = MonReader("NomEns").ToString()
                    nom = str.Remove(str.LastIndexOf(" "))
                    prenom = MonReader("NomPren").ToString().Replace(nom, "")
                    Label1.Text = nom
                    Label4.Text = prenom
                    Label2.Text = MonReader("Code_Ens").ToString()
                    Label3.Text = "ENSEIGNANT"
                    Label3.Visible = False
                    img.ImageUrl = "images.ashx?Code_Ens=" & Label2.Text
                    Connect.Close()
                    Connect.Open()
                    Command.Connection = Connect
                    Command.CommandText = "SELECT Code_Mat, Gr FROM  ENSEIGNEMENTS where Code_Ens='" & Session("Code_Ens").ToString() & "'"
                    Adaptateur.UpdateCommand = Command
                    Dim dtbl As New DataTable
                    Adaptateur.Fill(dtbl)
                    Dim date1 As Date
                    Dim i, j As Integer
                    i = dtbl.Rows.Count
                    Label6.Text = "Modules: "
                    Label8.Text = ""
                    Dim save As String = ""
                    For Each Ligne As DataRow In dtbl.Rows

                        If save <> Ligne("Code_Mat").ToString Then
                            Label8.Text &= Ligne("Code_Mat").ToString & " , "
                            save = Ligne("Code_Mat").ToString

                        End If
                    Next
                    Label6.Visible = True
                    Label8.Visible = True
                    dtbl.Clear()
                    Command.CommandText = "SELECT Gr from Enregistrer Where Code_Ens='" & Session("Code_Ens").ToString() & "' and Gr<>' '"
                    Adaptateur.UpdateCommand = Command
                    Adaptateur.Fill(dtbl)
                    j = dtbl.Rows.Count
                    j = (j * 100) / i
                    Label10.Text = "Taux de remplissage: "
                    Label11.Text = j & "%"
                    Command.CommandText = "SELECT Dernier_Delai FROM Utilisateurs "
                    MonReader = Command.ExecuteReader()
                    If MonReader.Read() Then
                        date1 = MonReader("Dernier_Delai")
                        Connect.Close() : Connect.Open()
                        If j < 100 Then
                            Dim date2 As TimeSpan
                            date2 = date1.Date - Date.Now.Date
                            If (date2.Days > 0) Then
                                Label12.Text = "Attention! il  reste que " & date2.Days.ToString & " jours pour envoyer les informations"
                            ElseIf (date2.Days = 0) Then
                                Label12.Text = "Attention! il reste moins de 24 h pour envoyer les informations"
                            Else
                                Label12.Text = "la date d'envoyer les informations est expirée "
                            End If
                        Else
                            notification.Attributes("class") = "alert alert-success alert-dismissible"
                            Label12.Text = "Vous avez envoyer les informations à la DE "
                        End If
                        progressbar.Attributes.Add("style", "width:" & Label11.Text)
                    Else
                        Label12.Text = "La DE n'a pas decidé le dernier delai pour envoyer les informations"
                    End If
                Else

                    Response.Redirect("Logout.aspx")
                End If
                Connect.Close()
            Else
                Response.Redirect("\htmlpages\Website_Bootstrap\Accueil\index.html")
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try


    End Sub

    Protected Sub Acceuil_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Acceuil.Click
        If Session("Matricule") <> "" Then
            Response.Redirect("Etudiant.aspx")
        ElseIf (Session("Code_Ens") <> "") Then
            Response.Redirect("Enseignant.aspx")
        End If
    End Sub

    Protected Sub Parametres_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Parametres.Click
        If Session("Matricule") <> "" Then
            Response.Redirect("Parametre.aspx")
        ElseIf (Session("Code_Ens") <> "") Then
            Response.Redirect("Parametre.aspx")
        End If
    End Sub

    Protected Sub Deconnecter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Deconnecter.Click
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Logout.aspx")
    End Sub

    Sub cookies(ByVal str1 As String, ByVal str2 As String, ByVal Response As HttpResponse, ByVal Request As HttpRequest, ByVal Session As HttpSessionState)
        Response.Cookies(str1)("lastVisit") = DateTime.Now.ToString()
        Response.Cookies(str1).Expires = Now.AddDays(2)
        Dim acookie As New HttpCookie(str1)
        acookie.Values("lastVisit") = Now.ToString()
        acookie.Expires = Now.AddDays(1)
        Response.Cookies.Add(acookie)
        If Not Request.Cookies(str1) Is Nothing Then
            If Request.Cookies(str1)(str2) = "" Then
                Response.Cookies(str1)(str2) = Session(str2)
                acookie.Values(str2) = Session(str2)
            Else
                Response.Cookies(str1)(str2) = Request.Cookies(str1)(str2)
                acookie.Values(str2) = Request.Cookies(str1)(str2)
                Session(str2) = Request.Cookies(str1)(str2)
            End If
        End If
    End Sub

    Protected Sub Parametres2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Parametres2.Click
        Response.Redirect("Parametre.aspx")
    End Sub
End Class