Imports System.Data.OleDb

Public Class Form1
    Dim XM As Integer
    Dim ym As Integer
    Dim mov As Boolean
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("L"))
    Dim path2 As String = path1 & "Base_de_donne\IMPORTER BDD.mdb"

    Dim accscone As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path2)
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim p As New Drawing2D.GraphicsPath
        p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        p.AddLine(40, 0, Butt.Width - 40, 0)
        p.AddArc(New Rectangle(Butt.Width - 40, 0, 40, 40), -90, 90)
        p.AddLine(Butt.Width, 40, Butt.Width, Butt.Height - 40)
        p.AddArc(New Rectangle(Butt.Width - 40, Butt.Height - 40, 40, 40), 0, 90)
        p.AddLine(Butt.Width - 40, Butt.Height, 40, Butt.Height)
        p.AddArc(New Rectangle(0, Butt.Height - 40, 40, 40), 90, 90)
        p.CloseFigure()
        Butt.Region = New Region(p)

        Dim q As New Drawing2D.GraphicsPath
        q.StartFigure()
        q.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        q.AddLine(40, 0, P2.Width - 40, 0)
        q.AddArc(New Rectangle(P2.Width - 40, 0, 40, 40), -90, 90)
        q.AddLine(P2.Width, 40, P2.Width, P2.Height - 40)
        q.AddArc(New Rectangle(P2.Width - 40, P2.Height - 40, 40, 40), 0, 90)
        q.AddLine(P2.Width - 40, P2.Height, 40, P2.Height)
        q.AddArc(New Rectangle(0, P2.Height - 40, 40, 40), 90, 90)
        q.CloseFigure()
        P2.Region = New Region(q)

        Dim z As New Drawing2D.GraphicsPath
        z.StartFigure()
        z.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        z.AddLine(40, 0, P1.Width - 40, 0)
        z.AddArc(New Rectangle(P1.Width - 40, 0, 40, 40), -90, 90)
        z.AddLine(P1.Width, 40, P1.Width, P1.Height - 40)
        z.AddArc(New Rectangle(P1.Width - 40, P1.Height - 40, 40, 40), 0, 90)
        z.AddLine(P1.Width - 40, P1.Height, 40, P1.Height)
        z.AddArc(New Rectangle(0, P1.Height - 40, 40, 40), 90, 90)
        z.CloseFigure()
        P1.Region = New Region(q)
    End Sub

    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        Label1.Text = ""
        Label2.Text = ""
    End Sub

    Private Sub TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        Label2.Text = ""
        Label1.Text = ""
    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Close()

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles P2.Paint

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butt.Click
        Try
            accscone.Open()
            Dim tabutil As New DataTable
            Dim adaputil As New OleDbDataAdapter("select * from Utilisateurs", accscone)
            adaputil.Fill(tabutil)
            Label1.Text = ""
            Label2.Text = ""
            If TextBox1.Text <> tabutil.Rows(0).Item(1) Then
                Label1.Text = "INCORRECT USERNAME !!!"
            ElseIf TextBox2.Text <> tabutil.Rows(0).Item(2) Then
                Label2.Text = "INCORRECT PASSWORD !!!"
            Else
                Page2.Show()
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        If TextBox2.UseSystemPasswordChar = False Then
            TextBox2.UseSystemPasswordChar = True
        Else
            TextBox2.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub Butt_StyleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Butt.StyleChanged

    End Sub



    Private Sub PictureBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseDown

        mov = True
        XM = Windows.Forms.Cursor.Position.X - Me.Left
        ym = Windows.Forms.Cursor.Position.Y - Me.Top

    End Sub

    Private Sub PictureBox2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseMove
        If mov Then
            Me.Top = Windows.Forms.Cursor.Position.Y - ym
            Me.Left = Windows.Forms.Cursor.Position.X - XM
        End If

    End Sub

    Private Sub PictureBox2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseUp
        mov = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Close()
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class
