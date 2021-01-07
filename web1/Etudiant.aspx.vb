Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class page2
    Inherits System.Web.UI.Page
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")
    Dim Command As New SqlCommand
    Dim MonReader As SqlDataReader
    Dim Adaptateur As New SqlDataAdapter(Command)
    Dim MonDataSet As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim master1 As New Site1
            master1.cookies("userInfo", "Matricule", Response, Request, Session)
            If Not Session("Matricule") Is Nothing Then
                Connect.Open()
                Dim com As New SqlCommand
                Command.Connection = Connect
                Try
                    Command.CommandText = "SELECT * from ETUDIANTS Where Matricule='" & Session("Matricule").ToString() & "'"
                    MonReader = Command.ExecuteReader()
                    If MonReader.Read() Then
                        Label2.Text = MonReader("Matricule").ToString()
                    Else
                        Response.Redirect("Logout.aspx")
                    End If
                    Connect.Close()
                    Connect.Open()
                    Command.Connection = Connect
                    Command.CommandText = "SELECT * from Utilisateurs"
                    MonReader = Command.ExecuteReader()
                    If MonReader.Read Then
                        Dim date1 As Date = MonReader("Debut_S1")
                        Dim date2 As Date = MonReader("Debut_S2")
                        Connect.Close()
                        If Date.Compare(DateAndTime.Today, date1) >= 0 Then
                            ConsNoteSem1()
                        Else
                            myModal.Attributes.Add("style", "display:block")
                            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                            message.Text = "Vous ne pouvez rien voir jusqu'a maintenant car l'année scolaire n'a pas encore commencé" '<-------!!!!!!!!!!!!!!!!!-------------
                        End If
                        If Date.Compare(DateAndTime.Today, date2) >= 0 Then
                            ConsNoteSem2()
                        Else
                            myModal.Attributes.Add("style", "display:block")
                            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                            message.Text = "Les notes et les moyennes ne sont pas encore disponible" '<-------!!!!!!!!!!!!!!!!!-------------
                        End If
                    End If
                Catch ex As Exception
                    Response.Redirect("Logout.aspx")
                End Try
            Else
                Response.Redirect("Logout.aspx")
            End If
            Connect.Close()
        Catch ex As Exception
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "Quleque enseignants n'ont pas encore remplie les notes"
        Finally
            Connect.Close()
        End Try
    End Sub

    Sub ConsNoteSem1()
        Connect.Open()
        Try
            Label4.Text = "SEMESTRE 1"
            ConsNoteS1.Visible = True
            Command.CommandText = "SELECT DISTINCT INSCRITMODULE.CcNote, INSCRITMODULE.CiNote, INSCRITMODULE.TpNote, INSCRITMODULE.CfNote, INSCRITMODULE.MoyMod, INSCRITMODULE.Code_Mat FROM INSCRITMODULE  INNER JOIN INSCRITS ON INSCRITMODULE.Matricule = INSCRITS.Matricule WHERE  (INSCRITMODULE.Matricule = '" & Label2.Text & "') AND (INSCRITMODULE.Sem = 'S1') ORDER BY INSCRITMODULE.Code_Mat "
            Adaptateur.UpdateCommand = Command
            Dim dtbl As New DataTable
            Adaptateur.Fill(dtbl)
            If dtbl.Rows.Count > 0 Then
                ConsNoteS1.DataSource = dtbl
                ConsNoteS1.DataBind()
            Else
                Command.CommandText = "SELECT DISTINCT INSCRITMODULE.Code_Mat FROM INSCRITMODULE  INNER JOIN INSCRITS ON  INSCRITMODULE.Matricule = INSCRITS.Matricule WHERE (INSCRITMODULE.Matricule = '" & Label2.Text & "') AND (INSCRITMODULE.Sem = 'S1')  "
                Adaptateur.UpdateCommand = Command
                dtbl.Clear()
                Adaptateur.Fill(dtbl)
                ConsNoteS1.DataSource = dtbl
                ConsNoteS1.DataBind()
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                message.Text = "les notes de n'ont pas envoyer a l'administration"
            End If
        Catch ex As Exception
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "les notes de n'ont pas envoyer a l'administration"
        End Try
        Connect.Close()
    End Sub

    Sub ConsNoteSem2()
        Connect.Open()
        Try
            ConsNoteS2.Visible = True
            Label7.Text = "SEMESTRE 2"
            Command.CommandText = "SELECT DISTINCT INSCRITMODULE.CcNote, INSCRITMODULE.CiNote, INSCRITMODULE.TpNote, INSCRITMODULE.CfNote, INSCRITMODULE.MoyMod, INSCRITMODULE.Code_Mat FROM INSCRITMODULE INNER JOIN INSCRITS ON  INSCRITMODULE.Matricule = INSCRITS.Matricule WHERE (INSCRITMODULE.Matricule = '" & Label2.Text & "') AND (INSCRITMODULE.Sem = 'S2')  "
            Adaptateur.UpdateCommand = Command
            Dim dtbl As New DataTable
            Adaptateur.Fill(dtbl)
            If dtbl.Rows.Count > 0 Then
                ConsNoteS2.DataSource = dtbl
                ConsNoteS2.DataBind()
            Else
                Command.CommandText = "SELECT DISTINCT INSCRITMODULE.Code_Mat FROM INSCRITMODULE  INNER JOIN INSCRITS ON  INSCRITMODULE.Matricule = INSCRITS.Matricule WHERE (INSCRITMODULE.Matricule = '" & Label2.Text & "') AND (INSCRITMODULE.Sem = 'S1')  "
                Adaptateur.UpdateCommand = Command
                dtbl.Clear()
                Adaptateur.Fill(dtbl)
                ConsNoteS2.DataSource = dtbl
                ConsNoteS2.DataBind()
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                message.Text = "les notes n'ont pas envoyer a l'administration"
            End If
        Catch ex As Exception

            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "les notes n'ont pas envoyer a l'administration"
        End Try
        Connect.Close()
    End Sub
    Protected Sub fermer_click(ByVal sender As Object, ByVal e As EventArgs)
        myModal.Attributes.Add("style", "display:none")
    End Sub
    
End Class
