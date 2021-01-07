Imports System.Web
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class Images
    Implements System.Web.IHttpHandler

    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = Path.Remove(Path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim con As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Try
            Dim Matricule As String = context.Request.QueryString("Matricule").ToString()
            con.Open()
            Dim sTSQL As String = "Select Image from ImageEtud where Matricule=@Matricule"
            Dim objCmd As New SqlCommand
            objCmd.Connection = con
            objCmd.CommandText = sTSQL
            objCmd.Parameters.AddWithValue("@Matricule", Matricule.ToString())
            Dim data As Object = objCmd.ExecuteScalar()
            context.Response.BinaryWrite(CType(data, Byte()))

        Catch ex As Exception
            Try
                con.Close()
                Dim CodeEns As String = context.Request.QueryString("Code_Ens").ToString()
                con.Open()
                Dim sTSQL As String = "Select Image from ImageEns where Code_Ens=@Code_Ens"
                Dim objCmd As New SqlCommand
                objCmd.Connection = con
                objCmd.CommandText = sTSQL
                objCmd.Parameters.AddWithValue("@Code_Ens", CodeEns.ToString())
                Dim data As Object = objCmd.ExecuteScalar()
                context.Response.BinaryWrite(CType(data, Byte()))
            Catch ex2 As Exception
            End Try
        Finally
            con.Close()
        End Try

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class