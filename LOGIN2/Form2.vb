Public Class ConfirForm
    Public conf As Boolean = False
    Public Sub rogner(ByVal Butt As Button)
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
    End Sub
    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub ConfirForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        rogner(btnoui)
        rogner(btnnon)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Me.Close()

    End Sub

    Private Sub btnoui_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnoui.Click
        conf = True
        Me.Close()

    End Sub

    Private Sub btnnon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnon.Click
        conf = False
        Me.Close()
    End Sub
End Class