Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Public Class page6
    Inherits System.Web.UI.Page
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("\"))
    Dim path2 As String = path1 & "\Base_de_donne\Scolarite.mdf"
    Dim Connect As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path2 & " ;Integrated Security=True;User Instance=True")
    Dim Command As New SqlCommand
    Dim MonReader As SqlDataReader
    Dim Adaptateur As New SqlDataAdapter(Command)
    Dim MonDataSet As New DataSet
    Dim master1 As New Site1
    Dim ctrName As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        master1.cookies("userInfo2", "Code_Ens", Response, Request, Session)
        If Not Session("Code_Ens") Is Nothing Then
            Try
                Label1.Text = Session("Code_Ens").ToString
            Catch ex As Exception
            End Try
            If (IsPostBack = False) Then
                t = New Table()
                t.ID = "tableName"
                Session("tableName") = t
                ViewState("ctrName") = ctrName
            End If
            CreeTab("Saisie")
            ctrName = ViewState("ctrName")
            t = Session("tableName")
            Connect.Open()
            Command.Connection = Connect
            Command.CommandText = "SELECT * from Utilisateurs"
            MonReader = Command.ExecuteReader()
            If MonReader.Read Then                
                Dim date1 As Date = MonReader("Debut_S1")
                Dim date2 As Date = MonReader("Debut_S2")
                If Date.Compare(DateAndTime.Today, date1) >= 0 Then
                    Semestre.Text = "Semestre 1"
                End If
                If Date.Compare(DateAndTime.Today, date2) >= 0 Then
                    Semestre.Text = "Semestre 2"
                End If
            End If
            Connect.Close()
        Else
            Response.Redirect("Logout.aspx")
        End If
    End Sub

    Public Sub CreeTab(ByVal str3 As String)

        Try
            If (Label1.Text <> "") Then
                Connect.Open()
                t.Rows.Clear()
                Dim ROWC As New TableRow
                Dim ROW(36) As TableRow
                Dim cell(2) As TableCell
                For m = 0 To 2
                    ROW(1) = New TableHeaderRow
                    cell(m) = New TableHeaderCell
                Next
                cell(1).Text = "Module" : cell(0).Text = "Promo" : cell(2).Text = "Groupes"
                ROW(1).Cells.Add(cell(0))
                ROW(1).Cells.Add(cell(1))
                ROW(1).Cells.Add(cell(2))
                Dim str1 As String = ""
                Dim str2 As String = ""
                Command.Connection = Connect
                Command.CommandText = "SELECT Code_Mat,Gr,Sec from ENSEIGNEMENTS Where Code_Ens='" & Label1.Text & "'"
                Dim Adaptateur As New SqlDataAdapter(Command)
                Dim MonData As New DataTable
                Adaptateur.Fill(MonData)
                Dim cpt As Integer = 1 : Dim n As Integer = 0 : Dim cont As Integer = 0
                Dim col1 As New Color
                Dim i As Integer = 2
                Dim links(36) As HyperLink
                Dim order As New DataView(MonData)
                order.Sort = "Code_Mat DESC"
                MonData = order.ToTable()
                If (MonData.Rows().Count > 0) Then
                    For Each Ligne As DataRow In MonData.Rows()
                        n += 1
                        If (Ligne("Code_Mat").ToString <> cell(1).Text) And cpt <> 1 Then
                            ROW(i).Cells.Item(0).RowSpan = cpt - i + 1
                            ROW(i).Cells.Item(1).RowSpan = cpt - i + 1
                            If (cont Mod 2 <> 0) Then
                                col1 = Color.WhiteSmoke
                            Else
                                col1 = Color.White
                            End If
                            ROW(i).Cells.Item(0).BackColor = col1
                            ROW(i).Cells.Item(1).BackColor = col1
                            ROW(i).Cells.Item(2).BackColor = col1
                            For m = i + 1 To cpt
                                ROW(m).Cells.Item(0).Visible = False
                                ROW(m).Cells.Item(1).Visible = False
                                ROW(m).Cells.Item(2).BackColor = col1
                            Next
                            cont += 1
                            i = cpt + 1
                        End If
                        links(n) = New HyperLink
                        links(n).ID = "row" & cpt & "- cell" & 2
                        links(n).Text = Ligne("Gr").ToString
                        links(n).NavigateUrl = "~/" & str3 & ".aspx?Code_Ens=" & Label1.Text & "&Gr=" & links(n).Text
                        cell(1) = New TableCell : cell(0) = New TableCell : cell(2) = New TableCell
                        cell(1).Text = Ligne("Code_Mat").ToString
                        cell(0).Text = Ligne("Sec").ToString
                        cpt += 1 : ROW(cpt) = New TableRow
                        ROW(cpt).Cells.Add(cell(0)) : ROW(cpt).Cells.Add(cell(1)) : cell(2).Controls.Add(links(n))
                        ROW(cpt).Cells.Add(cell(2))
                        Session("Code_Mat") = ROW(cpt).Cells.Item(1).Text
                        links(n).NavigateUrl &= "&Code_Mat=" & ROW(cpt).Cells.Item(1).Text
                    Next
                    If (cont Mod 2 <> 0) Then
                        col1 = Color.WhiteSmoke
                    Else
                        col1 = Color.White
                    End If

                    ROW(i).Cells.Item(0).BackColor = col1
                    ROW(i).Cells.Item(1).BackColor = col1
                    ROW(i).Cells.Item(2).BackColor = col1
                    ROW(i).Cells.Item(0).RowSpan = cpt - i + 1
                    ROW(i).Cells.Item(1).RowSpan = cpt - i + 1
                    For m = i + 1 To cpt
                        ROW(m).Cells.Item(0).Visible = False
                        ROW(m).Cells.Item(1).Visible = False
                        ROW(m).Cells.Item(2).BackColor = col1
                    Next

                    For m = 1 To cpt
                        t.Rows.Add(ROW(m))
                    Next
                    Session("tableName") = t
                    ViewState("ctrName") = ctrName
                    Panel1.Controls.Add(t)
                    Connect.Close()
                Else
                    myModal.Attributes.Add("style", "display:block")
                    myModal.Attributes("class") = "alert alert-danger alert-dismissible"
                    message.Text = "La DE n'a pas envoyer votre enseignement"
                End If
            End If
            
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            Connect.Close()
        End Try
    End Sub
    Protected Sub fermer_click(ByVal sender As Object, ByVal e As EventArgs)
        myModal.Attributes.Add("style", "display:none")
    End Sub
   
End Class