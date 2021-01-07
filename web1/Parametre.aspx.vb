Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class page4
    Inherits System.Web.UI.Page
    Dim path0 As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path0.Remove(path0.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")

     Dim master1 As New Site1
    Dim MonReader As SqlDataReader
    Dim Command As New SqlCommand
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        master1.cookies("userInfo", "Matricule", Response, Request, Session)
        master1.cookies("userInfo2", "Code_Ens", Response, Request, Session)
        If Not Session("Matricule") Is Nothing Then
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = "SELECT * from ETUDIANTS Where Matricule='" & Session("Matricule").ToString() & "'"
            MonReader = Command.ExecuteReader()
            If MonReader.Read() Then

                Label2.Text = MonReader("Matricule").ToString()
                Label3.Text = "ETUDIANTS"
                Label3.Visible = False
            Else
                Response.Redirect("Logout.aspx")
            End If
            Connect.Close()
        ElseIf Not Session("Code_Ens") Is Nothing Then
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = "SELECT * from ENSEIGNANT Where Code_Ens='" & Session("Code_Ens").ToString() & "'"
            MonReader = Command.ExecuteReader()
            If MonReader.Read() Then
                Label2.Text = MonReader("Code_Ens").ToString()
                Label3.Text = "ENSEIGNANT"
                Label3.Visible = False
            End If
            Connect.Close()
        Else
            Response.Redirect("\htmlpages\Website_Bootstrap\Accueil\index.html")
        End If
    End Sub

    Protected Sub Valide_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Valide.Click
        Dim bool As Boolean = True
        If Label3.Text = "ETUDIANTS" Then
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = "update ETUDIANTS set PassWord ='" & cryptage(Box2.Text) & "' Where PassWord ='" & cryptage(Box1.Text) & "' and Matricule = '" & Label2.Text & "' "
        ElseIf Label3.Text = "ENSEIGNANT" Then
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = "update ENSEIGNANT set Passwd ='" & cryptage(Box2.Text) & "' Where Passwd ='" & cryptage(Box1.Text) & "' and  Code_Ens='" & Label2.Text & "'"
        Else
            bool = False
        End If
        If bool = True Then
            Try
                If Command.ExecuteNonQuery() Then

                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-success alert-dismissible"
                    message.Text = "le modification est terminer"
                Else
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                    message.Text = "l'ancient mot de passe est faux"
                End If
            Catch ERR As Exception
                myModal.Attributes.Add("style", "display:block")
                myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                message.Text = "Le mot de passe ne doit pas contenir des caractères spéciaux (',\,=,+,-,; etc...)"
            End Try

        Else
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "la confirmation de Nouveau mot de passe est faux "
        End If
        Connect.Close()
    End Sub

    Protected Sub Changer1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Changer1.Click
        Dim bool As Boolean = True
        Dim postedfile As HttpPostedFile = FileUpload1.PostedFile
        Dim FileName As String = Path.GetFileName(postedfile.FileName)
        Dim FileExtension As String = Path.GetExtension(FileName)
        If FileExtension.ToLower() = ".png" Or FileExtension.ToLower() = ".jpg" Or FileExtension.ToLower() = ".bmp" Or FileExtension.ToLower() = ".gif" Then
            Dim stream As Stream = postedfile.InputStream
            Dim BinaryReader As New BinaryReader(stream)
            Dim bytes() As Byte = BinaryReader.ReadBytes(CInt(stream.Length))
            Dim paraImagedata As SqlParameter = New SqlParameter()
            Dim paraSize As SqlParameter = New SqlParameter()
            If Label3.Text = "ETUDIANTS" Then
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = "Update ImageEtud set [Image]=@Image where Matricule='" & Label2.Text & "'"
            ElseIf Label3.Text = "ENSEIGNANT" Then
                Connect.Open()
                Command.Connection = Connect
                Command.CommandText = "Update ImageEns set [Image]=@Image where Code_Ens='" & Label2.Text & "'"
            Else
                bool = False
            End If
            paraImagedata.ParameterName = "@Image"
            paraImagedata.Value = bytes
            Command.Parameters.Add(paraImagedata)
            Try
                Command.ExecuteNonQuery()
            Catch ex As Exception
            Finally
                Connect.Close()
            End Try
        Else
            myModal.Attributes.Add("style", "display:block")
            myModal.Attributes("class") = "alert alert-danger alert-dismissible"
            message.Text = "Le type de fichier ne correspond pas, veuillez choisir un fichier de type Image"
        End If
    End Sub

    Protected Sub fermer_click(ByVal sender As Object, ByVal e As EventArgs)
        myModal.Attributes.Add("style", "display:none")
    End Sub

    Function cryptage(ByVal chaine As String) As String
        Dim i, j, k As Integer
        Dim result As String = ""
        k = 0
        For Each c As String In chaine
            If (c = " ") Then
                result &= " "
            Else
                i = Asc(c)
                i = ((i + 1) Mod 127)
                result &= Convert.ToChar(i)
            End If
        Next
        Return result
    End Function

    Function decryptage(ByVal chaine As String) As String
        Dim i, j, k As Integer
        Dim result As String = ""
        k = 0
        For Each c As String In chaine
            If (c = " ") Then
                result &= " "
            Else
                i = Asc(c)
                i = i - 1
                If (i < 0) Then
                    i += 127
                End If
                result &= Convert.ToChar(i)
            End If
        Next
        Return result
    End Function
End Class