Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Public Class Page2

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


    Dim XM As Integer
    Dim ym As Integer
    Dim mov As Boolean
    Dim A As Integer
    Dim B As Integer
    Dim vidI As Boolean = False
    Dim vidIM As Boolean = False
    Dim vidUE As Boolean = False
    Dim vidA As Boolean = False
    Dim vidENSM As Boolean = False
    Dim moov As Boolean
    Dim vidabs As String = "nonfait"
    Dim videns As String = "nonfait"
    Dim videtud As String = "nonfait"
    Dim vidensnm As String = "nonfait"
    Dim vidinscr As String = "nonfait"
    Dim vidinscrmod As String = "nonfait"
    Dim vidinscriue As String = "nonfait"
    Dim path0 As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path0.Remove(path0.LastIndexOf("L"))
    Dim path2 As String = path1 & "Base_de_donne\EXPORT BDD.mdb"
    Dim path3 As String = path1 & "Base_de_donne\Scolarite.mdf"
    Dim impocon As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path3 & ";Integrated Security=True;Connect Timeout=30;User Instance=True")
    Dim accscon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path2)
    Dim path4 As String = path1 & "Base_de_donne\IMPORTER BDD.mdb"
    Dim accscone As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path4)
    Dim cmdfil, cmdinit As New SqlCommand
    Dim cmdexport, cmdaccinit As New OleDbCommand

    Private Sub Page2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

    End Sub

    Private Sub Page2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rogner(Button5)
        rogner(Button6)
        rogner(Button7)
        rogner(Button8)
        rogner(Button9)
        rogner(Button10)
        rogner(Button11)
        rogner(Button12)
        rogner(Button14)
        rogner(Button15)
        rogner(Button16)
        'rogner(Button17)
        rogner(Button18)
        rogner(Button19)
        rogner(Button20)
        rogner(Button21)
        rogner(Button22)
        rogner(Button23)
        rogner(Button24)
        rogner(Button25)
        rogner(Button26)
        rogner(Button27)
        rogner(Button28)
        rogner(Button29)
        rogner(Button30)
        rogner(Button31)
        rogner(Button32)
        rogner(Button33)
        rogner(Button34)
        rogner(Button35)
        rogner(Button38)
        rogner(Button39)
        rogner(Button40)
        rogner(Button41)
        rogner(Button42)
        rogner(Button43)
        rogner(Button44)
        rogner(Button45)
        rogner(Button46)
        rogner(Button47)
        rogner(Button48)
        rogner(Button49)
        rogner(valdbsem)
        rogner(valdbsem2)
        rogner(valsais)
        rogner(annudbsem)
        rogner(annudbsem2)
        rogner(annusais)


        PictureBox1.Visible = False
        Panel1.Width = 45
        PictureBox2.Location = New Point(6, 3)


    End Sub
    Public Sub Vider(ByVal str As String)
        Try
            ProgressBar.Visible = True
            ProgressBar.Value = 0
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From " & str, accscon)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From " & str, impocon)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            adapsqlfile.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                ProgressBar.Maximum = l + 1
                For k = 0 To l
                    ProgressBar.Value += 1
                    varcmd = "DELETE From " & str & " WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('" & str & "',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try

    End Sub
    Public Sub ImgEns()
        impocon.Open()
        Dim filename As String = Path.Combine(path1 & "\web1\htmlpages\Website_Bootstrap\Logos\man.png")
        Dim img = Image.FromFile(filename)
        Dim ms As New MemoryStream
        img.Save(ms, img.RawFormat)
        Dim data = ms.ToArray()
        Dim cm As New SqlCommand
        cm.Connection = impocon
        cm.CommandText = " SELECT distinct(Code_Ens) FROM ENSEIGNANT"
        Dim adaptateur As New SqlDataAdapter(cm)
        Dim tb As New DataTable
        adaptateur.Fill(tb)
        Dim str As String = ""
        For Each Ligne As DataRow In tb.Rows()

            impocon.Close()
            impocon.Open()
            cm.Connection = impocon
            cm.CommandText = " IF NOT EXISTS ( SELECT distinct(Code_Ens) FROM ENSEIGNANT where Code_Ens = '" & Ligne("Code_Ens").ToString & "' ) BEGIN INSERT INTO ImageEns ([Code_Ens],[Image]) VALUES ('" & Ligne("Code_Ens").ToString & "',@img) End"
            cm.Parameters.AddWithValue("@img", data)
            Try
                cm.ExecuteNonQuery()
                cm.Parameters.Clear()
                impocon.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                impocon.Close()
            End Try
        Next
    End Sub

    Public Sub ImgEtud()
        impocon.Open()
        Dim filename As String = Path.Combine(path1 & "\web1\htmlpages\Website_Bootstrap\Logos\man.png")
        Dim img = Image.FromFile(filename)
        Dim ms As New MemoryStream
        img.Save(ms, img.RawFormat)
        Dim data = ms.ToArray()
        Dim cm As New SqlCommand
        cm.Connection = impocon
        cm.CommandText = " SELECT distinct(Matricule) FROM ETUDIANTS"
        Dim adaptateur As New SqlDataAdapter(cm)
        Dim tb As New DataTable
        adaptateur.Fill(tb)
        Dim str As String = ""
        For Each Ligne As DataRow In tb.Rows()

            impocon.Close()
            impocon.Open()
            cm.Connection = impocon
            cm.CommandText = "IF NOT EXISTS ( SELECT distinct(Matricule) FROM ImageEtud where Matricule = '" & Ligne("Matricule").ToString & "' )  BEGIN INSERT INTO ImageEtud ([Matricule],[Image]) VALUES ('" & Ligne("Matricule").ToString & "',@img) END"
            cm.Parameters.AddWithValue("@img", data)
            Try
                cm.ExecuteNonQuery()
                cm.Parameters.Clear()
                impocon.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                impocon.Close()
            End Try
        Next
    End Sub

    Function Generer_User(ByVal nom As String) As String
        Dim result As String = ""
        Dim i As Integer = 0
        result &= nom(nom.Length - 1) & "_"
        For i = 0 To nom.Length - 2
            If nom(i) <> " " Then
                result &= nom(i)
            End If
        Next
        Return result
    End Function

    Function Generer_Password(ByVal pd As String) As String
        Dim result As String = ""
        Dim i As Integer = 0
        For i = 0 To pd.Length - 1
            If pd(i) <> " " Then
                result &= pd(i)
            End If
        Next
        Return cryptage(result)
    End Function

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

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()

    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click
        Panel11.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        dataview1.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
        Panel5.Visible = False
        Panel4.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False

        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        Else
            PictureBox1.Visible = False

            Panel1.Width = 45
            PictureBox2.Location = New Point(6, 3)
        End If

    End Sub

    Private Sub Panel3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel3.Click
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IPanel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (Panel4.Visible = True) Then
            Panel4.Visible = False
        Else
            Panel4.Visible = True
        End If
        Panel11.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False

        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        dataview1.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (Panel5.Visible = True) Then
            Panel5.Visible = False
        Else
            Panel5.Visible = True
        End If
        Panel11.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        dataview1.Visible = False
        Panel4.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (Panel6.Visible = True) Then
            Panel6.Visible = False
        Else
            Panel6.Visible = True
        End If
        Panel11.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        dataview1.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel7.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If (Panel7.Visible = True) Then
            Panel7.Visible = False
        Else
            Panel7.Visible = True
        End If
        Panel11.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        Codens.Visible = False
        codetud.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        dataview1.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
    End Sub

    Private Sub Panel2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel2.MouseDown
        mov = True
        XM = Windows.Forms.Cursor.Position.X - Me.Left
        ym = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel2.MouseMove
        If mov Then
            Me.Top = Windows.Forms.Cursor.Position.Y - ym
            Me.Left = Windows.Forms.Cursor.Position.X - XM
        End If
    End Sub

    Private Sub Panel2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel2.MouseUp
        mov = False
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub PictureBox8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox8.Click
        Close()

    End Sub

    Private Sub PictureBox9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox9.Click
        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If (UE_Panel.Visible = True) Then
            UE_Panel.Visible = False
        Else
            UE_Panel.Visible = True
        End If
        Panel8.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        IPanel.Visible = False
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If (IM_Panel.Visible = True) Then
            IM_Panel.Visible = False
        Else
            IM_Panel.Visible = True
        End If
        Panel8.Visible = False
        A_Panel.Visible = False
        IPanel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If (IPanel.Visible = True) Then
            IPanel.Visible = False
        Else
            IPanel.Visible = True
        End If
        Panel8.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Panel8.Visible = False

        A_Panel.Visible = False
        IPanel.Visible = False
        IM_Panel.Visible = False
        UE_Panel.Visible = False
        If (ENS_Panel.Visible = True) Then
            ENS_Panel.Visible = False
        Else
            ENS_Panel.Visible = True
        End If

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If (A_Panel.Visible = True) Then
            A_Panel.Visible = False
        Else
            A_Panel.Visible = True
        End If
        Panel8.Visible = False
        ENS_Panel.Visible = False
        IPanel.Visible = False
        IM_Panel.Visible = False
        UE_Panel.Visible = False
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()

    End Sub

    

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try
            Codens.Visible = False
            Button13.Visible = False
            dataview1.Visible = False
            ImgEns()
            dataview1.Visible = False
            Dim varcmd, strinit As String
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            impocon.Open()
            accscon.Open()
            Dim trouv As Boolean = False
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNANT", accscone)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNANT", impocon)
            adapfil.Fill(tabfil)
            Dim i, j, t As New Integer
            t = tabfil.Rows.Count
            '********************************** verification existance ***********************
            For Each ligne As DataRow In tabfil.Rows
                If ligne("NomUser").ToString = Nothing Or ligne("Passwd").ToString = Nothing Then
                    MsgBox("Il faut d'abord générer les noms d'utilisateurs et les Mot de passe des Enseignants")
                    trouv = True
                    Exit For
                End If

            Next
            '**********************************************************************************
            If (trouv = False) Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                ProgressBar.Maximum = t
                For i = 0 To t - 1
                    ProgressBar.Value += 1
                    varcmd = "IF NOT EXISTS ( SELECT distinct(Code_Ens) FROM ENSEIGNANT where Code_Ens = '" & tabfil.Rows(i).Item(0) & "' ) BEGIN INSERT INTO ENSEIGNANT ([Code_Ens],[NomPren],[NomEns],[NomUser],[Passwd]) VALUES ('" & tabfil.Rows(i).Item(0) & "','" & tabfil.Rows(i).Item(1) & "','" & tabfil.Rows(i).Item(2) & "','" & tabfil.Rows(i).Item(3) & "' ,'" & tabfil.Rows(i).Item(4) & "') END"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                MsgBox("Operation Terminé avec Succés...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        dataview1.Visible = False
        Codens.Visible = False
        Dim i, j As New Integer
        Dim tabver As New DataTable
        Dim comver As New SqlCommand
        Dim adapver As New SqlDataAdapter("SELECT Code_Ens,Code_Mat,Sec,Gr,AnScol,Sem,Saisie FROM ENSEIGNEMENTS", impocon)
        adapver.Fill(tabver)
        i = tabver.Rows.Count
        If i <> 0 Then
            For j = 0 To i - 1
                If tabver.Rows(j).Item(6) = "OUI" Then
                    tabver.Rows(j).Delete()
                End If
            Next
            dataview1.DataSource = tabver
            dataview1.Visible = True
            Button13.Visible = True
        Else
            MsgBox("LES ENSEIGNANTS SONT TOUS REMPLIR...")
        End If

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Try
            codetud.Visible = False
            Button13.Visible = False
            dataview1.Visible = False
            ImgEtud()
            dataview1.Visible = False
            Dim tabfil, tabsqlfil, tabfile, tabuser, tabprenom As New DataTable
            Dim varcmd, strinit, nomsel As String
            Dim trouv As Boolean = False
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From ETUDIANTS", accscone)
            Dim adapuser As New OleDbDataAdapter("SELECT NomEtud From ETUDIANTS", accscone)
            Dim adaprenom As New OleDbDataAdapter("SELECT Prenoms From ETUDIANTS", accscone)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From ETUDIANTS", impocon)
            Dim i, j, t, k As New Integer
            adapfile.Fill(tabfile)
            adapuser.Fill(tabuser)
            adaprenom.Fill(tabprenom)

            '********************************** verification existance ***********************
            For Each ligne As DataRow In tabfile.Rows
                If ligne("NomUser").ToString = Nothing Or ligne("PassWord").ToString = Nothing Then
                    MsgBox("Il faut d'abord générer les noms d'utilisateurs et les Mot de passe des Etudiants")
                    trouv = True
                    Exit For
                End If

            Next
            '**********************************************************************************
            k = tabuser.Rows.Count
            t = tabfile.Rows.Count
            If trouv = False Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                ProgressBar.Maximum = t
                For i = 0 To t - 1
                    ProgressBar.Value += 1

                    nomsel = tabprenom.Rows(i).Item(0)
                    nomsel.ElementAt(0)
                    varcmd = "IF NOT EXISTS ( SELECT distinct(Matricule) FROM ETUDIANTS where Matricule = '" & tabfile.Rows(i).Item(0) & "' ) BEGIN INSERT INTO ETUDIANTS ([Matricule],[NomEtud],[Prenoms],[NomUser],[PassWord])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & tabfile.Rows(i).Item(3) & "' ,'" & tabfile.Rows(i).Item(4) & "' ) END"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                MsgBox("Operation Terminé avec Succés...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim tabcopy As New DataTable
        codetud.Visible = False

        Dim j, k, i As New Integer
        Dim strno As String
        strno = "Non"
        dataview1.Visible = False
        Dim adapcopyabs As New OleDbDataAdapter("SELECT  ABSENCES.Anscol, ABSENCES.Sem, ABSENCES.Matiere, ETUDIANTS.Matricule, ETUDIANTS.NomEtud, ETUDIANTS.Prenoms, ABSENCES.Jour, ABSENCES.justifie, ABSENCES.TypAbs FROM (ABSENCES INNER JOIN ETUDIANTS ON ABSENCES.Matricule = ETUDIANTS.Matricule)", accscon)
        adapcopyabs.Fill(tabcopy)
        j = tabcopy.Rows.Count
        If j <> 0 Then
            For i = 0 To j - 1
                If tabcopy.Rows(i).Item(5) = "OUI" Then
                    tabcopy.Rows(i).Delete()
                End If
            Next
            dataview1.DataSource = tabcopy
            dataview1.Visible = True
            Button13.Visible = True
        Else
            MsgBox("NO ABSENCES")

        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Try
            

            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String

            impocon.Open()
            accscon.Open()
            accscone.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfil As New OleDbDataAdapter("SELECT * From ABSENCES", accscone)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From ABSENCES", impocon)
            adapfil.Fill(tabfil)
            Dim i, j, t As New Integer
            If vidabs = "fait" Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                t = tabfil.Rows.Count
                For i = 0 To t - 1
                    varcmd = "INSERT INTO ABSENCES ([Anscol],[Sem],[Matricule],[Matiere],[Jour],[Justifie],[TypAbs])VALUES('" & tabfil.Rows(i).Item(0) & "','" & tabfil.Rows(i).Item(1) & "','" & tabfil.Rows(i).Item(2) & "','" & tabfil.Rows(i).Item(3) & "' ,'" & tabfil.Rows(i).Item(4) & "','" & tabfil.Rows(i).Item(5) & "','" & tabfil.Rows(i).Item(6) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('ABSENCES',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                vidabs = "nonfait"
            Else
                MsgBox("IL FAUT DABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try

    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Try
            ProgressBar.Visible = True
            ProgressBar.Value = 0
            accscon.Open()
            impocon.Open()
            Dim tabacc, tabsql As New DataTable
            Dim strexpor As String
            Dim i, j, k As New Integer
            Dim adapacc As New OleDbDataAdapter("select * from ABSENCES", accscon)
            Dim adapsql As New SqlDataAdapter("SELECT * FROM ABSENCES", impocon)
            Dim cmdacc2 As New OleDbCommand
            adapacc.Fill(tabacc)
            adapsql.Fill(tabsql)
            i = tabacc.Rows.Count
            k = tabsql.Rows.Count
            If i <> 0 Then
                Dim cmdacc As New OleDbCommand("DELETE from ABSENCES", accscon)
                cmdacc.ExecuteNonQuery()
            End If
            ProgressBar.Maximum = k

            For j = 0 To k - 1
                ProgressBar.Value += 1

                strexpor = "insert into ABSENCES ([AnScol],[Sem],[Matricule],[Matiere],[Jour],[Justifie],[TypAbs])values('" & tabsql.Rows(j).Item(1) & "','" & tabsql.Rows(j).Item(2) & "','" & tabsql.Rows(j).Item(3) & "','" & tabsql.Rows(j).Item(4) & "','" & tabsql.Rows(j).Item(5) & "','" & tabsql.Rows(j).Item(6) & "','" & tabsql.Rows(j).Item(7) & "')"
                cmdacc2 = New OleDbCommand(strexpor, accscon)
                cmdacc2.ExecuteNonQuery()

            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        vidENSM = True
        Panel8.Visible = True
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Try    
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            accscone.Open()
            If vidensnm = "fait" Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                impocon.Open()
                accscon.Open()
                Dim cmdajoucol As New OleDbCommand
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNEMENTS", accscone)
                Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNEMENTS", impocon)
                adapfil.Fill(tabfil)
                Dim i, j, t As New Integer
                t = tabfil.Rows.Count
                ProgressBar.Maximum = t


                For i = 0 To t - 1
                    ProgressBar.Value += 1

                    varcmd = "INSERT INTO ENSEIGNEMENTS ([Code_Ens],[Code_Mat],[Sec],[Gr],[AnScol],[Sem],[Saisie])VALUES('" & tabfil.Rows(i).Item(0) & "','" & tabfil.Rows(i).Item(1) & "','" & tabfil.Rows(i).Item(2) & "','" & tabfil.Rows(i).Item(3) & "' ,'" & tabfil.Rows(i).Item(4) & "','" & tabfil.Rows(i).Item(5) & "','" & tabfil.Rows(i).Item(6) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('ENSEIGNEMENTS',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                impocon.Close()
                accscon.Close()
                vidensnm = "nonfait"
            Else
                MsgBox("IL FAUT DABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        vidIM = True
        Panel8.Visible = True

    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Try
            
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            If vidinscrmod = "fait" Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                impocon.Open()
                accscone.Open()
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITMODULE", accscone)
                Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITMODULE", impocon)
                adapfile.Fill(tabfile)
                Dim i, j, t As New Integer
                t = tabfile.Rows.Count
                ProgressBar.Maximum = t
                For i = 0 To t - 1
                    ProgressBar.Value += 1
                    varcmd = "INSERT INTO INSCRITMODULE([AnScol],[Sem],[Promo],[Matricule],[Code_Mat])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & tabfile.Rows(i).Item(3) & "' ,'" & tabfile.Rows(i).Item(4) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('INSCRITMODULE',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()

                impocon.Close()
                accscon.Close()
                vidinscrmod = "nonfait"

                MsgBox("Operation Terminé avec Succés...")
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Try
            ProgressBar.Visible = True
            ProgressBar.Value = 0
            Dim tabaccfil, tabfile, tabsqlfil As New DataTable
            Dim varcmd, strinit, vardelet As String
            impocon.Open()
            accscon.Open()
            Dim i, l, t As Integer
            Dim adapfil As New OleDbDataAdapter("SELECT  *  From  INSCRITMODULE", accscon)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From INSCRITMODULE", impocon)
            Dim cmdexpo As New OleDbCommand
            adapsqlfil.Fill(tabsqlfil)
            adapfil.Fill(tabfile)
            l = tabsqlfil.Rows.Count
            t = tabfile.Rows.Count
            If t <> 0 Then
                strinit = "ALTER TABLE INSCRITMODULE DROP COLUMN ID"
                cmdexpo = New OleDbCommand(strinit, accscon)
                cmdexpo.ExecuteNonQuery()
                cmdexpo = New OleDbCommand("ALTER TABLE INSCRITMODULE ADD ID COUNTER", accscon)
                cmdexpo.ExecuteNonQuery()
                cmdexpo = New OleDbCommand("ALTER TABLE INSCRITMODULE ALTER COLUMN ID COUNTER(1,1)", accscon)
                cmdexpo.ExecuteNonQuery()
                vardelet = "Delete from INSCRITMODULE "
                cmdexport = New OleDbCommand(vardelet, accscon)
                cmdexport.ExecuteNonQuery()
            End If
            Dim tab(10) As Double
            ProgressBar.Maximum = l
            For i = 0 To l - 1
                ProgressBar.Value += 1
                If tabsqlfil.Rows(i).Item(6).ToString = Nothing Then
                    tab(6) = 0
                Else
                    tab(6) = tabsqlfil.Rows(i).Item(6)
                End If


                If tabsqlfil.Rows(i).Item(7).ToString = Nothing Then
                    tab(7) = 0
                Else
                    tab(7) = tabsqlfil.Rows(i).Item(7)
                End If

                If tabsqlfil.Rows(i).Item(8).ToString = Nothing Then
                    tab(8) = 0
                Else
                    tab(8) = tabsqlfil.Rows(i).Item(8)
                End If
                If tabsqlfil.Rows(i).Item(9).ToString = Nothing Then
                    tab(9) = 0
                Else
                    tab(9) = tabsqlfil.Rows(i).Item(9)
                End If
                If tabsqlfil.Rows(i).Item(10).ToString = Nothing Then
                    tab(10) = 0
                Else
                    tab(10) = tabsqlfil.Rows(i).Item(10)
                End If
                varcmd = "INSERT INTO INSCRITMODULE ([AnScol],[Sem],[Promo],[Matricule],[Code_Mat],[CcNote],[TpNote],[CiNote],[CfNote],[MoyMod])VALUES('" & tabsqlfil.Rows(i).Item(1) & "','" & tabsqlfil.Rows(i).Item(2) & "','" & tabsqlfil.Rows(i).Item(3) & "','" & tabsqlfil.Rows(i).Item(4) & "','" & tabsqlfil.Rows(i).Item(5) & "','" & tab(6) & "','" & tab(7) & "','" & tab(8) & "','" & tab(9) & "' ,'" & tab(10) & "')"
                cmdexpo = New OleDbCommand(varcmd, accscon)
                cmdexpo.ExecuteNonQuery()
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        vidI = True
        Panel8.Visible = True

    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            If vidinscr = "fait" Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                impocon.Open()
                accscon.Open()
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITS", accscone)
                Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITS", impocon)
                adapfile.Fill(tabfile)
                Dim i, j, t As New Integer
                t = tabfile.Rows.Count
                ProgressBar.Maximum = t

                For i = 0 To t - 1
                    ProgressBar.Value += 1

                    varcmd = "INSERT INTO INSCRITS ([AnScol],[Sem],[Promo],[Sect],[Gr],[Matricule],[Ne],[MoySem],[MoyRach],[Moyenne],[Rang])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & tabfile.Rows(i).Item(3) & "' ,'" & tabfile.Rows(i).Item(4) & "','" & tabfile.Rows(i).Item(5) & "','" & tabfile.Rows(i).Item(6) & "','" & tabfile.Rows(i).Item(7) & "','" & tabfile.Rows(i).Item(8) & "','" & tabfile.Rows(i).Item(9) & "','" & tabfile.Rows(i).Item(10) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('INSCRITS',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                'Me.Close()
                vidinscr = "Nonfait"
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

   

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        'Try
        ProgressBar.Visible = True
        ProgressBar.Value = 0
        impocon.Open()
        accscon.Open()
        MsgBox("CLIQUER SUR OK POUR CONFIRMER L'EXPORTATION DU BDD")
        Dim strpath As String
        Dim tabexel As New DataTable
        Dim i, j, k As New Integer
        Dim cmdsql As New OleDbCommand
        Dim cmdacc As New OleDbCommand("DELETE from INSCRITS", accscon)
        cmdacc.ExecuteNonQuery()
        Dim adapexel As New SqlDataAdapter("select * from [INSCRITS]", impocon)
        adapexel.Fill(tabexel)
        i = tabexel.Rows.Count
        ProgressBar.Maximum = i

        For j = 0 To i - 1
            ProgressBar.Value += 1

            tabexel.Rows(j).Item(7).ToString()
            tabexel.Rows(j).Item(7) = 0
            tabexel.Rows(j).Item(8).ToString()
            tabexel.Rows(j).Item(8) = 0
            tabexel.Rows(j).Item(9).ToString()
            tabexel.Rows(j).Item(9) = 0
            tabexel.Rows(j).Item(10).ToString()
            tabexel.Rows(j).Item(10) = 0
            tabexel.Rows(j).Item(11).ToString()
            tabexel.Rows(j).Item(11) = 0

            strpath = "INSERT INTO INSCRITS ([AnScol],[Sem],[Promo],[Sect],[Gr],[Matricule],[Ne],[MoySem],[MoyRach],[Moyenne],[Rang])VALUES('" & tabexel.Rows(j).Item(1) & "','" & tabexel.Rows(j).Item(2) & "','" & tabexel.Rows(j).Item(3) & "','" & tabexel.Rows(j).Item(4) & "','" & tabexel.Rows(j).Item(5) & "','" & tabexel.Rows(j).Item(6) & "','" & tabexel.Rows(j).Item(7) & "','" & tabexel.Rows(j).Item(8) & "','" & tabexel.Rows(j).Item(9) & "','" & tabexel.Rows(j).Item(10) & "','" & tabexel.Rows(j).Item(11) & "')"
            cmdsql = New OleDbCommand(strpath, accscon)
            cmdsql.ExecuteNonQuery()
        Next
        MsgBox("Operation Terminé avec Succés...")
        'Catch ex As Exception
        'MsgBox(ex.Message)
        'Finally
        ProgressBar.Visible = False
        accscone.Close()
        impocon.Close()
        accscon.Close()
        ' End Try
    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        vidUE = True
        Panel8.Visible = True

    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            If vidinscriue = "fait" Then
                ProgressBar.Visible = True
                ProgressBar.Value = 0
                impocon.Open()
                accscon.Open()
                Dim k, l, m As New Integer
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfil As New OleDbDataAdapter("SELECT * From INSCRITSUE", accscone)
                Dim adapsqlfil As New SqlDataAdapter("SELECT * From INSCRITSUE", impocon)
                adapfil.Fill(tabfil)
                Dim i, j, t As New Integer
                t = tabfil.Rows.Count
                ProgressBar.Maximum = t

                For i = 0 To t - 1
                    ProgressBar.Value += 1

                    varcmd = "INSERT INTO INSCRITSUE ([AnScol],[Promo],[Matricule],[MoySem1],[MoyRach1],[Ne1],[MoySem2],[MoyRach2],[Ne2],[MoyAnu],[MoyRan],[Rang],[Decision])VALUES('" & tabfil.Rows(i).Item(0) & "','" & tabfil.Rows(i).Item(1) & "','" & tabfil.Rows(i).Item(2) & "','" & tabfil.Rows(i).Item(3) & "' ,'" & tabfil.Rows(i).Item(4) & "','" & tabfil.Rows(i).Item(5) & "','" & tabfil.Rows(i).Item(6) & "','" & tabfil.Rows(i).Item(7) & "','" & tabfil.Rows(i).Item(8) & "','" & tabfil.Rows(i).Item(9) & "','" & tabfil.Rows(i).Item(10) & "','" & tabfil.Rows(i).Item(11) & "','" & tabfil.Rows(i).Item(12) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('INSCRITSUE',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                vidinscriue = "nonfait"
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub
    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        Try
            ProgressBar.Visible = True
            ProgressBar.Value = 0
            impocon.Open()
            accscon.Open()
            MsgBox("CLIQUER SUR OK POUR CONFIRMER L'EXPORTATION DU BDD")
            Dim strpath As String
            Dim tabexel As New DataTable
            Dim i, j, k As New Integer
            Dim cmdsql As New OleDbCommand
            Dim cmdacc As New OleDbCommand("DELETE from INSCRITSUE", accscon)
            cmdacc.ExecuteNonQuery()
            Dim adapexel As New SqlDataAdapter("select * from [INSCRITSUE]", impocon)
            adapexel.Fill(tabexel)
            i = tabexel.Rows.Count
            ProgressBar.Maximum = i
            For j = 0 To i - 1
                ProgressBar.Value += 1
                tabexel.Rows(j).Item(3).ToString()
                tabexel.Rows(j).Item(3) = 0
                tabexel.Rows(j).Item(4).ToString()
                tabexel.Rows(j).Item(4) = 0
                tabexel.Rows(j).Item(5).ToString()
                tabexel.Rows(j).Item(5) = 0
                tabexel.Rows(j).Item(6).ToString()
                tabexel.Rows(j).Item(6) = 0
                tabexel.Rows(j).Item(7).ToString()
                tabexel.Rows(j).Item(7) = 0
                tabexel.Rows(j).Item(8).ToString()
                tabexel.Rows(j).Item(8) = 0
                tabexel.Rows(j).Item(9).ToString()
                tabexel.Rows(j).Item(9) = 0
                tabexel.Rows(j).Item(10).ToString()
                tabexel.Rows(j).Item(10) = 0
                tabexel.Rows(j).Item(11).ToString()
                tabexel.Rows(j).Item(11) = 0
                strpath = "INSERT INTO INSCRITSUE ([AnScol],[Promo],[Matricule],[MoySem1],[MoyRach1],[Ne1],[MoySem2],[MoyRach2],[Ne2],[MoyAnu],[MoyRan],[Rang],[Decision])VALUES('" & tabexel.Rows(j).Item(0) & "','" & tabexel.Rows(j).Item(1) & "','" & tabexel.Rows(j).Item(2) & "','" & tabexel.Rows(j).Item(3) & "','" & tabexel.Rows(j).Item(4) & "','" & tabexel.Rows(j).Item(5) & "','" & tabexel.Rows(j).Item(6) & "','" & tabexel.Rows(j).Item(7) & "','" & tabexel.Rows(j).Item(8) & "','" & tabexel.Rows(j).Item(9) & "','" & tabexel.Rows(j).Item(10) & "','" & tabexel.Rows(j).Item(11) & "','" & tabexel.Rows(j).Item(12) & "')"
                cmdsql = New OleDbCommand(strpath, accscon)
                cmdsql.ExecuteNonQuery()
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            ProgressBar.Visible = False
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        Parametre.Show()
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateAlerte.ValueChanged

    End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click
        If DBS1Panel.Visible = True Then
            DBS1Panel.Visible = False
        Else
            DBS1Panel.Visible = True
        End If
        DBS2Panel.Visible = False
        DDSPanel.Visible = False


    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        If AlerPanel8.Visible = True Then
            AlerPanel8.Visible = False
        Else
            AlerPanel8.Visible = True
        End If
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click

        If DBS2Panel.Visible = True Then
            DBS2Panel.Visible = False
        Else
            DBS2Panel.Visible = True
        End If



        DBS1Panel.Visible = False
        DDSPanel.Visible = False

    End Sub

    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        If DDSPanel.Visible = True Then
            DDSPanel.Visible = False
        Else
            DDSPanel.Visible = True
        End If



        DBS2Panel.Visible = False
        DBS1Panel.Visible = False

    End Sub
    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        Try
        ProgressBar.Visible = True
        ProgressBar.Value = 0
        accscone.Open()
        Dim Tab As New DataTable
        Dim maj, User As String
        Dim cmd As New OleDbCommand
        Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNANT ORDER BY Code_Ens", accscone)
        adapfil.Fill(Tab)
        ProgressBar.Maximum = Tab.Rows.Count
        For Each Ligne As DataRow In Tab.Rows
            ProgressBar.Value += 1
            If Ligne("NomUser").ToString = Nothing Then
                User = Generer_User(Ligne("NomEns").ToString.ToLower)
                maj = "UPDATE ENSEIGNANT SET NomUser ='" & User & "' WHERE NomPren = '" & Ligne("NomPren") & "'"
                cmd = New OleDbCommand(maj, accscone)
                cmd.ExecuteNonQuery()
            End If
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
        Finally
            accscone.Close()
            ProgressBar.Visible = False
        End Try
    End Sub

    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click
        Try
        ProgressBar.Visible = True
        ProgressBar.Value = 0
        Dim Tab As New DataTable
        Dim maj, Password As String
        Dim cmd As New OleDbCommand
        accscone.Open()
        cmd.Connection = accscone
        Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNANT ORDER BY Code_Ens", accscone)
        adapfil.Fill(Tab)
        ProgressBar.Maximum = Tab.Rows.Count
        For Each Ligne As DataRow In Tab.Rows
            ProgressBar.Value += 1
            If Ligne("Passwd").ToString = Nothing Then
                Password = Generer_Password(Ligne("NomPren").ToString.ToLower)
                maj = "UPDATE [ENSEIGNANT] SET [Passwd] ='" & Password & "' WHERE [NomPren] = '" & Ligne("NomPren") & "'"
                cmd.CommandText = maj
                cmd.ExecuteNonQuery()
            End If
        Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
        Finally
            ProgressBar.Visible = False
            accscone.Close()
        End Try
    End Sub

    Private Sub Button42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button42.Click
        Try
            Dim Tab As New DataTable
            Dim maj, User As String
            Dim cmd As New OleDbCommand
            ProgressBar.Visible = True
            ProgressBar.Value = 0

            accscone.Open()
            cmd.Connection = accscone
            Dim adapfil As New OleDbDataAdapter("SELECT * From ETUDIANTS ORDER BY Matricule", accscone)
            adapfil.Fill(Tab)
            ProgressBar.Maximum = Tab.Rows.Count

            For Each Ligne As DataRow In Tab.Rows
                ProgressBar.Value += 1

                If Ligne("NomUser").ToString = Nothing Then
                    User = Ligne("Prenoms").ToString(0) & "_" & Ligne("NomEtud").ToString
                    User = User.ToLower
                    User = User.Replace(" ", "_")
                    maj = "UPDATE ETUDIANTS SET NomUser ='" & User & "' WHERE NomEtud = '" & Ligne("NomEtud") & "' AND Prenoms = '" & Ligne("Prenoms") & "'"
                    cmd.CommandText = maj
                    cmd.ExecuteNonQuery()
                End If
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            ProgressBar.Visible = False
        End Try
    End Sub

    Private Sub Button43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button43.Click
        Try
        ProgressBar.Visible = True
        ProgressBar.Value = 0
        accscone.Open()
        Dim Tab As New DataTable
        Dim maj, Password As String
        Dim cmd As New OleDbCommand
        cmd.Connection = accscone
        Dim adapfil As New OleDbDataAdapter("SELECT * From ETUDIANTS ORDER BY Matricule", accscone)
        adapfil.Fill(Tab)
        ProgressBar.Maximum = Tab.Rows.Count
        For Each Ligne As DataRow In Tab.Rows
            ProgressBar.Value += 1
                If Ligne("PassWord").ToString = Nothing Then
                    Password = Ligne("Prenoms").ToString
                    Password = Password.ToLower()
                    Password = Generer_Password(Password)
                    maj = "UPDATE [ETUDIANTS] SET [PassWord] ='" & Password & "' WHERE [Matricule] = '" & Ligne("Matricule") & "'"
                    cmd.CommandText = maj
                    cmd.ExecuteNonQuery()
                End If
        Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
        Finally
            ProgressBar.Visible = False
            accscone.Close()
        End Try
    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click
        If cmptPanel.Visible = False Then
            cmptPanel.Visible = True
        Else
            cmptPanel.Visible = False

        End If
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
    End Sub

    Private Sub Button45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button45.Click
        If cmptensPanel.Visible = False Then
            cmptensPanel.Visible = True
        Else
            cmptensPanel.Visible = False
        End If
        cmptetudPanel.Visible = False

    End Sub

    Private Sub Button46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button46.Click
        If cmptetudPanel.Visible = False Then
            cmptetudPanel.Visible = True
        Else
            cmptetudPanel.Visible = False
        End If
        cmptensPanel.Visible = False

    End Sub

    Private Sub DateDBSem_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateDBSem.ValueChanged


    End Sub

    Private Sub DateDBSem_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateDBSem.Validated

    End Sub

    Private Sub DateDBSem_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DateDBSem.Validating

    End Sub

    Private Sub Button47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles valdbsem.Click
        impocon.Open()
        accscone.Open()
        Dim cmdutil As New SqlCommand
        Dim strutil As String
        Dim d1 As Date
        Dim tabfil As New DataTable
        d1 = CType(DateDBSem.Text, Date)
        Dim adapsqlfil As New OleDbDataAdapter("SELECT AnScol from INSCRITS", accscone)
        adapsqlfil.Fill(tabfil)
        If d1.Year = tabfil.Rows(0).Item(0) And d1.Month >= 9 And d1.Month <= 10 Then
            ' strutil = "UPDATE  Utilisateurs set Debut_S1=" & CType(d1, Date) & " WHere UtilId=1"
            cmdutil = New SqlCommand("UPDATE  Utilisateurs set Debut_S1=@jour WHere UtilId=1", impocon)
            cmdutil.Parameters.AddWithValue("@jour", d1)
            cmdutil.ExecuteNonQuery()
            cmdutil.Parameters.Clear()

            MsgBox("Operation Terminé avec Succés...")
            DBS1Panel.Visible = False
        Else
            MsgBox("Debut de semestre1  doit etre entre 09/ " & tabfil.Rows(0).Item(0).ToString & " et 10/ " & tabfil.Rows(0).Item(0).ToString)
        End If
        accscone.Close()
        impocon.Close()
    End Sub

    Private Sub Button48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annudbsem.Click
        DBS1Panel.Visible = False

    End Sub

    Private Sub valdbsem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles valdbsem2.Click
        impocon.Open()
        accscone.Open()
        Dim cmdutil As New SqlCommand
        Dim strutil As String
        Dim d1 As Date
        d1 = CType(Datedbsem2.Text, Date)
        Dim tabfil As New DataTable
        Dim adapsqlfil As New OleDbDataAdapter("SELECT AnScol from INSCRITS", accscone)
        adapsqlfil.Fill(tabfil)
        If d1.Year = tabfil.Rows(0).Item(0) + 1 And d1.Month >= 1 And d1.Month <= 3 Then

            cmdutil = New SqlCommand("UPDATE  Utilisateurs set Debut_S2=@jour WHere UtilId=1", impocon)
            cmdutil.Parameters.AddWithValue("@jour", d1)
            cmdutil.ExecuteNonQuery()
            cmdutil.Parameters.Clear()

            MsgBox("Operation Terminé avec Succés...")
            DBS2Panel.Visible = False
        Else
            Dim dateerr As Integer
            dateerr = tabfil.Rows(0).Item(0) + 1
            MsgBox("Debut de semestre2  doit etre entre 01/ " & dateerr.ToString & " et 03/ " & dateerr.ToString)
        End If
        accscone.Close()
        impocon.Close()
    End Sub

    Private Sub valsais_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles valsais.Click
        impocon.Open()
        accscone.Open()
        Dim cmdutil As New SqlCommand
        Dim strutil As String
        Dim d1 As Date
        d1 = CType(DateAlerte.Text, Date)
        Dim tabfil As New DataTable
        Dim adapsqlfil As New OleDbDataAdapter("SELECT AnScol from INSCRITS", accscone)
        adapsqlfil.Fill(tabfil)
        If d1.Year >= tabfil.Rows(0).Item(0) And d1.Year <= tabfil.Rows(0).Item(0) + 1 Then
            cmdutil = New SqlCommand("UPDATE  Utilisateurs set Dernier_Delai=@jour WHere UtilId=1", impocon)
            cmdutil.Parameters.AddWithValue("@jour", d1)
            cmdutil.ExecuteNonQuery()
            cmdutil.Parameters.Clear()
            MsgBox("Operation Terminé avec Succés...")
            DDSPanel.Visible = False
        Else
            Dim dateerr As Integer
            dateerr = tabfil.Rows(0).Item(0) + 1
            MsgBox("la delai est de l'année sclarité " & tabfil.Rows(0).Item(0) & "/" & dateerr.ToString)
        End If
        accscone.Close()
        impocon.Close()

    End Sub

    Private Sub annudbsem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annudbsem2.Click
        DBS2Panel.Visible = False
    End Sub

    Private Sub annusais_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annusais.Click
        DDSPanel.Visible = False
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        Codens.Visible = False
        codetud.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        Panel7.Visible = False
        dataview1.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False

        ''***************************************************

        'Dim chemin As String = path1 & "LOGIN2\bin\debug\Manuel Utilisateur_VF.pdf"
        Dim chemin As String = path1 & "Manuel Utilisateur_VF.pdf"
        Dim p As New Process
        p.StartInfo.FileName = chemin
        p.StartInfo.CreateNoWindow = True
        p.Start()
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click

        If Panel11.Visible = True Then
            Panel11.Visible = False
        Else
            Panel11.Visible = True
        End If
        Codens.Visible = False
        codetud.Visible = False
        Button13.Visible = False
        Panel8.Visible = False
        AlerPanel8.Visible = False
        DBS1Panel.Visible = False
        DBS2Panel.Visible = False
        DDSPanel.Visible = False
        Panel7.Visible = False
        dataview1.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        A_Panel.Visible = False
        IM_Panel.Visible = False
        ENS_Panel.Visible = False
        UE_Panel.Visible = False
        IPanel.Visible = False
        cmptPanel.Visible = False
        cmptensPanel.Visible = False
        cmptetudPanel.Visible = False
    End Sub



    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click

        If vidI = True Then
            Vider("INSCRITS")
            vidinscr = "fait"
            vidI = False
        End If
        If vidIM = True Then
            Vider("INSCRITMODULE")
            vidinscrmod = "fait"
            vidIM = False
        End If
        If vidUE = True Then
            Vider("INSCRITSUE")
            vidinscriue = "fait"
            vidUE = False
        End If
        If vidA = True Then
            Vider("ABSENCES")
            vidabs = "fait"
            vidA = False
        End If
        If vidENSM = True Then
            Vider("ENSEIGNEMENTS")
            vidensnm = "fait"
            vidA = False
        End If
        Panel8.Visible = False

    End Sub

    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click
        Panel8.Visible = False

    End Sub

    Private Sub Button47_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button47.Click
        vidA = True
        Panel8.Visible = True
    End Sub

    Private Sub Button48_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button48.Click

        If Codens.Visible = True Then
            Codens.Visible = False
        Else
            Codens.Visible = True
        End If
        Button13.Visible = False
        dataview1.Visible = False
        codetud.Visible = False
    End Sub

    Private Sub Button49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button49.Click
        If codetud.Visible = True Then
            codetud.Visible = False
        Else
            codetud.Visible = True
        End If
        Button13.Visible = False
        dataview1.Visible = False
        Codens.Visible = False

    End Sub

    Private Sub Button53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button53.Click
        impocon.Open()
        accscone.Open()
        Label6.Visible = False

        Dim tab As New DataTable
        Dim i, j As Integer
        Dim str1, str2, str3, str4 As String
        Dim trouv As Boolean = False
        Dim cmd1, cmd2, cmd3, cmd4 As New OleDbCommand
        Dim cmds1, cmds2, cmds3, cmds4 As New SqlCommand
        Dim adapfile As New OleDbDataAdapter("SELECT Matricule From ETUDIANTS", accscone)
        Dim adapsqlfile As New SqlDataAdapter("SELECT Matricule From ETUDIANTS", impocon)
        '**************************************************************************
        Dim adapfil As New OleDbDataAdapter("SELECT Matricule From INSCRITMODULE", accscone)
        Dim adapsqlfil As New SqlDataAdapter("SELECT Matricule From INSCRITMODULE", impocon)
        Dim adapfill As New OleDbDataAdapter("SELECT  From INSCRITSUE", accscone)
        Dim adapsqlfill As New SqlDataAdapter("SELECT Matricule From INSCRITSUE", impocon)
        Dim adapfilee As New OleDbDataAdapter("SELECT * From INSCRITS", accscone)
        Dim adapsqlfilee As New SqlDataAdapter("SELECT * From INSCRITS", impocon)
        adapfile.Fill(tab)
        i = tab.Rows.Count
        j = tab.Columns.Count
        For j = 0 To i - 1
            If (tab.Rows(j).Item(0) = TextBox2.Text) Then
                trouv = True
                str1 = "DELETE FROM ETUDIANTS WHERE Matricule='" & TextBox2.Text & "'"
                str2 = "DELETE FROM INSCRITS WHERE Matricule='" & TextBox2.Text & "'"
                str3 = "DELETE FROM INSCRITSUE WHERE Matricule='" & TextBox2.Text & "'"
                str4 = "DELETE FROM INSCRITMODULE WHERE Matricule='" & TextBox2.Text & "'"

                cmd1 = New OleDbCommand(str1, accscone)
                cmd1.ExecuteNonQuery()
                cmd2 = New OleDbCommand(str2, accscone)
                cmd2.ExecuteNonQuery()
                cmd3 = New OleDbCommand(str3, accscone)
                cmd3.ExecuteNonQuery()
                cmd4 = New OleDbCommand(str4, accscone)
                cmd4.ExecuteNonQuery()

                cmds1 = New SqlCommand(str1, impocon)
                cmds1.ExecuteNonQuery()
                cmds2 = New SqlCommand(str2, impocon)
                cmds2.ExecuteNonQuery()
                cmds3 = New SqlCommand(str3, impocon)
                cmds3.ExecuteNonQuery()
                cmds4 = New SqlCommand(str4, impocon)
                cmds4.ExecuteNonQuery()

                MsgBox("Operation Terminé avec Succés...")
                Exit For
            End If
        Next
        If (trouv = False) Then
            Label6.Text = "cet Etudiant n'existe pas dans la BDD"
            Label6.Visible = True
        End If
        impocon.Close()
        accscone.Close()
    End Sub

    Private Sub TextBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        Label6.Visible = False
    End Sub

    Private Sub Button50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button50.Click
        impocon.Open()
        accscone.Open()
        Label5.Visible = False

        Dim tab As New DataTable
        Dim i, j As Integer
        Dim str1, str2, str3, str4 As String
        Dim trouv As Boolean = False
        Dim cmd1, cmd2, cmd3, cmd4 As New OleDbCommand
        Dim cmds1, cmds2, cmds3, cmds4 As New SqlCommand
        Dim adapfile As New OleDbDataAdapter("SELECT Code_Ens From ENSEIGNANT", accscone)
        Dim adapsqlfile As New SqlDataAdapter("SELECT Code_Ens From ENSEIGNANT", impocon)
        '**************************************************************************

        adapfile.Fill(tab)
        i = tab.Rows.Count
        j = tab.Columns.Count

        For j = 0 To i - 1
            If (tab.Rows(j).Item(0).ToString = TextBox1.Text) Then
                trouv = True
                str1 = "DELETE FROM ENSEIGNANT WHERE Code_Ens=" & tab.Rows(j).Item(0)
                str2 = "DELETE FROM ENSEIGNEMENTS WHERE Code_Ens=" & tab.Rows(j).Item(0)
                str3 = "DELETE FROM Enregistrer WHERE Code_Ens=" & tab.Rows(j).Item(0)
                str4 = "DELETE FROM ImageEns WHERE Code_Ens=" & tab.Rows(j).Item(0)

                cmd1 = New OleDbCommand(str1, accscone)
                cmd1.ExecuteNonQuery()
                cmd2 = New OleDbCommand(str2, accscone)
                cmd2.ExecuteNonQuery()

                cmds1 = New SqlCommand(str1, impocon)
                cmds1.ExecuteNonQuery()
                cmds2 = New SqlCommand(str2, impocon)
                cmds2.ExecuteNonQuery()
                cmds3 = New SqlCommand(str3, impocon)
                cmds3.ExecuteNonQuery()
                cmds4 = New SqlCommand(str4, impocon)
                cmds4.ExecuteNonQuery()

                MsgBox("Operation Terminé avec Succés...")
                Exit For
            End If
        Next
        If (trouv = False) Then
            Label5.Text = "cet Enseignant n'existe pas dans la BDD"
            Label5.Visible = True
        End If
        impocon.Close()
        accscone.Close()
    End Sub
    Private Sub Button51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button51.Click
        Codens.Visible = False

    End Sub

    Private Sub Button52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button52.Click
        codetud.Visible = False

    End Sub

    Private Sub PictureBox4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox4.Click

        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If

    End Sub

    Private Sub PictureBox7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox7.Click
        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox6.Click
        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If
    End Sub

    Private Sub PictureBox5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox5.Click
        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If
    End Sub

    Private Sub PictureBox10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox10.Click
        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If
    End Sub

    Private Sub PictureBox11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox11.Click
        If (Panel1.Width = 45) Then
            PictureBox1.Visible = True
            Panel1.Width = 230
            PictureBox2.Location = New Point(187, 6)
        End If
    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        dataview1.Visible = False
        Button13.Visible = False
    End Sub
End Class
