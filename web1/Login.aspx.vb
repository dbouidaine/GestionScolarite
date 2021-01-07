Imports System.Data
Imports System.Data.SqlClient
Imports System

Public Class Login2
    Inherits System.Web.UI.Page
    Dim ParaPage As New page4
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")
    Dim cm As New SqlCommand
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            Session.Abandon()
            Session.Clear()
            Session.RemoveAll()
            Dim mycookies() As String = Request.Cookies.AllKeys
            System.Web.Security.FormsAuthentication.SignOut()
            For Each cookis In mycookies
                Response.Cookies(cookis).Expires = Now.AddDays(-1)
            Next
            Response.ClearContent()
        End If
    End Sub
    Protected Sub Con_Ens_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Con_Ens.Click
        Dim str1 As String = "SELECT * from ENSEIGNANT Where NomUser ='" & Username1.Text & "' and Passwd ='" & ParaPage.cryptage(password1.Text) & "'"
        Dim str2 As String = "~/Enseignant.aspx"
        SendInfo(str1, str2, "Code_Ens", Connect, Response)
    End Sub

    Protected Sub Con_Etud_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Con_Etud.Click
        Dim str1 As String = "SELECT * from ETUDIANTS Where NomUser ='" & Username2.Text & "' and PassWord ='" & ParaPage.cryptage(password2.Text) & "'"
        Dim str2 As String = "~/Etudiant.aspx"
        SendInfo(str1, str2, "Matricule", Connect, Response)
    End Sub
    Public Sub SendInfo(ByVal str1 As String, ByVal str2 As String, ByVal str3 As String, ByVal Connect As SqlConnection, ByVal response As HttpResponse)
        Dim cm As New SqlCommand
        Connect.Open()
        cm.Connection = Connect
        cm.CommandText = str1
        Dim MonReader As SqlDataReader = cm.ExecuteReader()
        If MonReader.Read() Then
            Session(str3) = MonReader(str3).ToString()
            response.Redirect(str2)
            Session.RemoveAll()
        Else
            If str3 = "Matricule" Then
                Literal2.Text = "username or password are false."
            Else
                Literal1.Text = "username or password are false"
            End If
        End If
        Connect.Close()
    End Sub
End Class