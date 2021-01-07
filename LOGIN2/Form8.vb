Imports System.Data.OleDb

Public Class Parametre
    Dim A, B As New Integer
    Dim moov As Boolean
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("L"))
    Dim path2 As String = path1 & "Base_de_donne\IMPORTER BDD.mdb"
    Dim accscone As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path2)
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Close()

    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But.Click
        Label18.Visible = False
        Panel11.Visible = False

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        Panel13.Visible = False
        Panel14.Visible = True
        Panel1.Visible = True
        Panel2.Visible = True

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But2.Click

        Panel3.Visible = False

        Label9.Visible = False
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        Panel14.Visible = False
        Panel13.Visible = True

        Panel7.Visible = True
        Panel9.Visible = True
    End Sub

    Private Sub anc1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim p As New Drawing2D.GraphicsPath
        p.StartFigure()
        p.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        p.AddLine(40, 0, But.Width - 40, 0)
        p.AddArc(New Rectangle(But.Width - 40, 0, 40, 40), -90, 90)
        p.AddLine(But.Width, 40, But.Width, But.Height - 40)
        p.AddArc(New Rectangle(But.Width - 40, But.Height - 40, 40, 40), 0, 90)
        p.AddLine(But.Width - 40, But.Height, 40, But.Height)
        p.AddArc(New Rectangle(0, But.Height - 40, 40, 40), 90, 90)
        p.CloseFigure()
        But.Region = New Region(p)

        Dim s As New Drawing2D.GraphicsPath
        s.StartFigure()
        s.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        s.AddLine(40, 0, But2.Width - 40, 0)
        s.AddArc(New Rectangle(But2.Width - 40, 0, 40, 40), -90, 90)
        s.AddLine(But2.Width, 40, But2.Width, But2.Height - 40)
        s.AddArc(New Rectangle(But2.Width - 40, But2.Height - 40, 40, 40), 0, 90)
        s.AddLine(But2.Width - 40, But2.Height, 40, But2.Height)
        s.AddArc(New Rectangle(0, But2.Height - 40, 40, 40), 90, 90)
        s.CloseFigure()
        But2.Region = New Region(s)


        Dim r As New Drawing2D.GraphicsPath
        r.StartFigure()
        r.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        r.AddLine(40, 0, But4.Width - 40, 0)
        r.AddArc(New Rectangle(But4.Width - 40, 0, 40, 40), -90, 90)
        r.AddLine(But4.Width, 40, But4.Width, But4.Height - 40)
        r.AddArc(New Rectangle(But4.Width - 40, But4.Height - 40, 40, 40), 0, 90)
        r.AddLine(But4.Width - 40, But4.Height, 40, But4.Height)
        r.AddArc(New Rectangle(0, But4.Height - 40, 40, 40), 90, 90)
        r.CloseFigure()
        But4.Region = New Region(r)

        Dim t As New Drawing2D.GraphicsPath
        t.StartFigure()
        t.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        t.AddLine(40, 0, But5.Width - 40, 0)
        t.AddArc(New Rectangle(But5.Width - 40, 0, 40, 40), -90, 90)
        t.AddLine(But5.Width, 40, But5.Width, But5.Height - 40)
        t.AddArc(New Rectangle(But5.Width - 40, But5.Height - 40, 40, 40), 0, 90)
        t.AddLine(But5.Width - 40, But5.Height, 40, But5.Height)
        t.AddArc(New Rectangle(0, But5.Height - 40, 40, 40), 90, 90)
        t.CloseFigure()
        But5.Region = New Region(t)


        Dim q As New Drawing2D.GraphicsPath
        q.StartFigure()
        q.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        q.AddLine(40, 0, Button3.Width - 40, 0)
        q.AddArc(New Rectangle(Button3.Width - 40, 0, 40, 40), -90, 90)
        q.AddLine(Button3.Width, 40, Button3.Width, Button3.Height - 40)
        q.AddArc(New Rectangle(Button3.Width - 40, Button3.Height - 40, 40, 40), 0, 90)
        q.AddLine(Button3.Width - 40, Button3.Height, 40, Button3.Height)
        q.AddArc(New Rectangle(0, Button3.Height - 40, 40, 40), 90, 90)
        q.CloseFigure()
        Button3.Region = New Region(q)

        Dim v As New Drawing2D.GraphicsPath
        v.StartFigure()
        v.AddArc(New Rectangle(0, 0, 40, 40), 180, 90)
        v.AddLine(40, 0, Button4.Width - 40, 0)
        v.AddArc(New Rectangle(Button4.Width - 40, 0, 40, 40), -90, 90)
        v.AddLine(Button4.Width, 40, Button4.Width, Button4.Height - 40)
        v.AddArc(New Rectangle(Button4.Width - 40, Button4.Height - 40, 40, 40), 0, 90)
        v.AddLine(Button4.Width - 40, Button4.Height, 40, Button4.Height)
        v.AddArc(New Rectangle(0, Button4.Height - 40, 40, 40), 90, 90)
        v.CloseFigure()
        Button4.Region = New Region(v)

    End Sub

    Private Sub But4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub



    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label5.Text = ""
        Label6.Text = ""
        Label8.Text = ""
        Label7.Text = ""
    End Sub

    Private Sub TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label5.Text = ""
        Label6.Text = ""
        Label8.Text = ""
        Label7.Text = ""
    End Sub

    Private Sub TextBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Label5.Text = ""
        Label6.Text = ""
        Label8.Text = ""
        Label7.Text = ""
    End Sub

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim adapaccs As New OleDbDataAdapter("select * from Utilisateurs", accscone)
        Dim tabacc As New DataTable
        adapaccs.Fill(tabacc)

        confnvpw.Text = ""
        confpw.Text = ""
        conf2.Text = ""
        ecrnvpw.Text = ""

        If TextBox4.Text <> tabacc.Rows(0).Item(2) Then
            confpw.Text = "Mot de Passe Faux !!!"
        ElseIf TextBox5.Text = tabacc.Rows(0).Item(2) Then
            confnvpw.Text = "Mot de Passe existe Déja !!!"
        ElseIf TextBox5.Text = "" Then
            ecrnvpw.Text = "Nouveau Mot de Passe???"
        Else
            Label18.Text = tabacc.Rows(0).Item(2)
            Label18.Visible = True




            Panel11.Visible = True

        End If
    End Sub

    Private Sub Button4_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        accscone.Open()

        Dim cmdacc As New OleDbCommand
        conf2.Text = ""
        If TextBox6.Text <> TextBox5.Text Or TextBox5.Text = "" Then
            conf2.Text = "Erreur, Non Confirmé !!!"
        Else
            cmdacc = New OleDbCommand("update Utilisateurs set PassUtil='" & TextBox6.Text & "' WHere Utilid=1", accscone)
            cmdacc.ExecuteNonQuery()

            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            Form9.Show()
            Label18.Visible = False


            TextBox4.Visible = True
            Panel11.Visible = False

        End If
        accscone.Close()

    End Sub

    Private Sub But4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But4.Click


        Dim tabutil As New DataTable
        Dim adaputil As New OleDbDataAdapter("select * from Utilisateurs", accscone)
        adaputil.Fill(tabutil)

        Label5.Text = ""
        Label6.Text = ""
        Label8.Text = ""
        Label7.Text = ""
        If TextBox1.Text <> tabutil.Rows(0).Item(1) Then
            Label5.Text = "Nomd d'Utilisateur Faux !!!"
        ElseIf TextBox2.Text = tabutil.Rows(0).Item(1) Then
            Label6.Text = "Nom d'Utilisateur existe Déja !!!"
        ElseIf TextBox2.Text = "" Then
            Label8.Text = "Nouveau Nom d'Utilisateur???"
        Else

            Label9.Text = tabutil.Rows(0).Item(1)
            Label9.Visible = True
            Panel3.Visible = True

        End If
    End Sub

    Private Sub But5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But5.Click
        accscone.Open()

        Dim cmdutil As New OleDbCommand
        Dim strutil As String

        Label7.Text = ""
        If TextBox3.Text <> TextBox2.Text Or TextBox3.Text = "" Then
            Label7.Text = "Erreur, Non Confirmé !!!"
        Else

            strutil = "UPDATE  Utilisateurs set NomUtil='" & TextBox3.Text & "' WHere Utilid=1"
            cmdutil = New OleDbCommand(strutil, accscone)
            cmdutil.ExecuteNonQuery()

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            Form9.Show()
            Label9.Visible = False


            TextBox1.Visible = True
            Panel3.Visible = False

        End If
        accscone.Close()
    End Sub
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        moov = True

        A = Windows.Forms.Cursor.Position.X - Me.Left
        B = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PictureBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseMove
        If moov Then
            Me.Left = Windows.Forms.Cursor.Position.X - A
            Me.Top = Windows.Forms.Cursor.Position.Y - B

        End If
    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        moov = False
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        Close()
    End Sub
End Class