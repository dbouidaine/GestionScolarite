Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class page5
    Inherits System.Web.UI.Page

    Dim path0 As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path0.Remove(path0.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Cn As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")

    Dim command As New SqlCommand
    Dim adaptateur As New SqlDataAdapter(command)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Dim postedfile As HttpPostedFile = FileUpload1.PostedFile
        Dim FileName As String = Path.GetFileName(postedfile.FileName)
        Dim FileExtension As String = Path.GetExtension(FileName)
        Dim FileSize As Integer = postedfile.ContentLength
        MsgBox(Path.GetDirectoryName(FileName))
        Image1.ImageUrl = Path.GetFullPath(FileName)
        If FileExtension.ToLower() = ".png" Or FileExtension.ToLower() = ".jpg" Or FileExtension.ToLower() = ".bmp" Or FileExtension.ToLower() = ".gif" Then
            Dim stream As Stream = postedfile.InputStream
            Dim BinaryReader As New BinaryReader(stream)
            Dim bytes() As Byte = BinaryReader.ReadBytes(CInt(stream.Length))
            cn.Open()
            Dim cm As New SqlCommand
            cm.Connection = cn
            cm.CommandText = " SELECT distinct(Matricule) FROM ETUDIANTS"
            Dim adaptateur As New SqlDataAdapter(cm)
            Dim tb As New DataTable
            adaptateur.Fill(tb)
            Dim str As String = ""
            Dim paraImagedata As SqlParameter = New SqlParameter()
            Dim paraSize As SqlParameter = New SqlParameter()
            paraSize.ParameterName = "@Size"
            paraSize.Value = FileSize
            cm.Parameters.Add(paraSize)
            paraImagedata.ParameterName = "@Image"
            paraImagedata.Value = bytes
            cm.Parameters.Add(paraImagedata)
            For Each Ligne As DataRow In tb.Rows()
                cn.Close()
                cn.Open()
                cm.Connection = cn
                cm.CommandText = "Update ImageEtud set [Size]=@Size,[Image]=@Image where Matricule='" & Ligne("Matricule").ToString & "'"
                cm.ExecuteNonQuery()
            Next
            cn.Close()
            cn.Open()
            cm.Connection = cn
            cm.CommandText = " SELECT distinct(Code_Ens) FROM ENSEIGNANT"
            adaptateur.Fill(tb)
            For Each Ligne As DataRow In tb.Rows()
                cn.Close()
                cn.Open()
                cm.Connection = cn
                cm.CommandText = "Update ImageEns set [Size]=@Size,[Image]=@Image where Code_Ens='" & Ligne("Code_Ens").ToString & "'"
                cm.ExecuteNonQuery()
            Next
            cn.Close()
        Else
            MsgBox("cant uplaod")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        cn.Open()
        Dim cm As New SqlCommand
        cm.Connection = cn
        cm.CommandText = " SELECT distinct(Matricule) FROM ETUDIANTS"
        Dim adaptateur As New SqlDataAdapter(cm)
        Dim tb As New DataTable
        adaptateur.Fill(tb)
        Dim str As String = ""
        For Each Ligne As DataRow In tb.Rows()
            cn.Close()
            cn.Open()
            cm.Connection = cn
            cm.CommandText = "INSERT INTO ImageEtud ([Matricule]) VALUES ('" & Ligne("Matricule").ToString & "')"
            Try
                cm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cn.Close()
            End Try
        Next
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        cn.Open()
        Dim cm As New SqlCommand
        cm.Connection = cn
        cm.CommandText = " SELECT distinct(Code_Ens) FROM ENSEIGNANT"
        Dim adaptateur As New SqlDataAdapter(cm)
        Dim tb As New DataTable
        adaptateur.Fill(tb)
        Dim str As String = ""
        For Each Ligne As DataRow In tb.Rows()
            cn.Close()
            cn.Open()
            cm.Connection = cn
            cm.CommandText = "INSERT INTO ImageEns ([Code_Ens]) VALUES ('" & Ligne("Code_Ens").ToString & "')"
            Try
                cm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cn.Close()
            End Try
        Next
    End Sub
End Class