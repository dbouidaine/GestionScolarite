Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class suivant

    Dim A As Integer
    Dim B As Integer

    Dim moov As Boolean
    Dim videns As String = "nonfait"
    Dim videtud As String = "nonfait"
    Dim vidensnm As String = "nonfait"
    Dim vidinscr As String = "nonfait"
    Dim vidinscrmod As String = "nonfait"
    Dim vidinscriue As String = "nonfait"
    Dim path As String = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.LastIndexOf("\"))
    Dim path1 As String = path.Remove(path.LastIndexOf("L"))
    Dim path2 As String = path1 & "Base_de_donne\EXPORT BDD.mdb"
    Dim path3 As String = path1 & "Base_de_donne\Scolarite.mdf"
    Dim impocon As New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=" & path3 & ";Integrated Security=True;Connect Timeout=30;User Instance=True")
    Dim accscon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path2)
    Dim path4 As String = path1 & "Base_de_donne\IMPORTER BDD.mdb"
    Dim accscone As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path4)
   Dim cmdfil, cmdinit As New SqlCommand
    Dim cmdexport, cmdaccinit As New OleDbCommand






    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal


        Else

            Me.WindowState = FormWindowState.Maximized
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem4.Click
        Try
            Dim varcmd, strinit As String
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            impocon.Open()
            accscon.Open()
            If videns = "fait" Then

                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNANT", accscon)
                Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNANT", impocon)
                adapfil.Fill(tabfil)
                Dim i, j, t As New Integer
                t = tabfil.Rows.Count

                For i = 0 To t - 1
                    varcmd = "INSERT INTO ENSEIGNANT ([Code_Ens],[NomPren],[NomEns],[NomUser],[Passwd])VALUES('" & tabfil.Rows(i).Item(0) & "','" & tabfil.Rows(i).Item(1) & "','" & tabfil.Rows(i).Item(2) & "','" & tabfil.Rows(i).Item(3) & "' ,'" & tabfil.Rows(i).Item(4) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('ENSEIGNANT',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                ' Me.Close()
                videns = "nonfait"
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD ...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem5.Click
        Try
            Dim tabfil, tabsqlfil, tabfile, tabuser, tabprenom As New DataTable
            Dim varcmd, strinit, nomsel As String
            impocon.Open()
            accscon.Open()
            If videtud = "fait" Then
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfile As New OleDbDataAdapter("SELECT * From ETUDIANTS", accscone)
                Dim adapuser As New OleDbDataAdapter("SELECT NomEtud From ETUDIANTS", accscone)
                Dim adaprenom As New OleDbDataAdapter("SELECT Prenoms From ETUDIANTS", accscone)
                Dim adapsqlfile As New SqlDataAdapter("SELECT * From ETUDIANTS", impocon)

                adapfile.Fill(tabfile)
                adapuser.Fill(tabuser)
                adaprenom.Fill(tabprenom)
                Dim i, j, t, k As New Integer
                k = tabuser.Rows.Count
                t = tabfile.Rows.Count

                For i = 0 To t - 1

                    nomsel = tabprenom.Rows(i).Item(0)
                    nomsel.ElementAt(0)
                    ' '"&"_"&&nomsel.ElementAt(0)& "'
                    ' MsgBox(nomsel.ElementAt(0))
                    varcmd = "INSERT INTO ETUDIANTS ([Matricule],[NomEtud],[Prenoms],[NomUser],[PassWord])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & nomsel.ElementAt(0) & "_" & tabuser.Rows(i).Item(0) & "' ,'12345')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('ETUDIANTS',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                MsgBox("Operation Terminé avec Succés...")
                ' Me.Close()
                videtud = "nonfait"
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub IMPORTERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERToolStripMenuItem.Click
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
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfil.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count

            If (l <> 0) Then
                For k = 1 To l
                    varcmd = "DELETE From ABSENCES WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('ABSENCES',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            '************************************************************************
            Dim i, j, t As New Integer
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
            'Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem3.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String

            accscone.Open()

            If vidensnm = "fait" Then
                impocon.Open()
                accscon.Open()
                Dim cmdajoucol As New OleDbCommand
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNEMENTS", accscone)
                Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNEMENTS", impocon)
                adapfil.Fill(tabfil)
                Dim i, j, t As New Integer
                t = tabfil.Rows.Count
                ' cmdajoucol = New OleDbCommand("ALTER TABLE ENSEIGNEMENTS ADD SAISIE varchar(50)", accscone)
                'cmdajoucol.ExecuteNonQuery()

                For i = 0 To t - 1
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
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub EXPORTERLABDDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPORTERLABDDToolStripMenuItem.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable

            Dim varcmd, strinit As String
            Label1.Text = "OPERATION EN COURS D'EXECUTION..."
            Label2.Text = "ATTENDEZ SVP"
            If vidinscrmod = "fait" Then

                'MsgBox("dfdf")
                impocon.Open()
                accscone.Open()
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITMODULE", accscone)
                Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITMODULE", impocon)
                adapfile.Fill(tabfile)
                Dim i, j, t As New Integer
                t = tabfile.Rows.Count
                For i = 0 To t - 1
                    varcmd = "INSERT INTO INSCRITMODULE([AnScol],[Sem],[Promo],[Matricule],[Code_Mat])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & tabfile.Rows(i).Item(3) & "' ,'" & tabfile.Rows(i).Item(4) & "')"
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
                strinit = "DBCC CHECKIDENT('INSCRITMODULE',RESEED,0)"
                cmdinit = New SqlCommand(strinit, impocon)
                cmdinit.ExecuteNonQuery()
                Label1.Visible = False
                Label2.Visible = False
                MsgBox("Operation Terminé avec Succés...")
                'Me.Close()

                impocon.Close()
                accscon.Close()
                vidinscrmod = "nonfait"
            Else
                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub IMPORTERToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERToolStripMenuItem3.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            If vidinscr = "fait" Then
                impocon.Open()
                accscon.Open()
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITS", accscon)
                Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITS", impocon)
                adapfile.Fill(tabfile)
                Dim i, j, t As New Integer
                t = tabfile.Rows.Count
                For i = 0 To t - 1
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
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub IMPORTERToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERToolStripMenuItem2.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            If vidinscriue = "fait" Then
                impocon.Open()
                accscon.Open()
                Dim k, l, m As New Integer
                Dim cmdfil, cmdinit As New SqlCommand
                Dim adapfil As New OleDbDataAdapter("SELECT * From INSCRITSUE", accscon)
                Dim adapsqlfil As New SqlDataAdapter("SELECT * From INSCRITSUE", impocon)
                adapfil.Fill(tabfil)
                Dim i, j, t As New Integer
                t = tabfil.Rows.Count
                For i = 0 To t - 1
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
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub
    Private Sub IMPORTERLABDDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNANT", accscon)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNANT", impocon)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfil.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 1 To l
                    varcmd = "DELETE From ENSEIGNANT WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('ENSEIGNANT',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            videns = "fait"

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
        '************************************************************************
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem1.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From ETUDIANTS", accscon)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From ETUDIANTS", impocon)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfile.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 0 To l
                    varcmd = "DELETE From ETUDIANTS WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('ETUDIANTS',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            videtud = "fait"

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try '************************************************************************
    End Sub

    Private Sub VIDERLABDDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VIDERLABDDToolStripMenuItem.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit, cmddelcol As New SqlCommand
            Dim adapfil As New OleDbDataAdapter("SELECT * From ENSEIGNEMENTS", accscon)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From ENSEIGNEMENTS", impocon)

            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfil.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 0 To l
                    varcmd = "DELETE From ENSEIGNEMENTS WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('ENSEIGNEMENTS',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            vidensnm = "fait"

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
        '************************************************************************
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem6.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITMODULE", accscon)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITMODULE", impocon)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfile.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count


            If (l <> 0) Then
                For k = 0 To l
                    varcmd = "DELETE From INSCRITMODULE WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('INSCRITMODULE',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            vidinscrmod = "fait"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
        '************************************************************************
    End Sub

    Private Sub COPIERCOLLEREXCELToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COPIERCOLLEREXCELToolStripMenuItem.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From INSCRITS", accscon)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From INSCRITS", impocon)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfile.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 0 To l
                    varcmd = "DELETE From INSCRITS WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('INSCRITS',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            vidinscr = "fait"

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
        '************************************************************************
    End Sub

    Private Sub ENSEIGNANTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENSEIGNANTToolStripMenuItem.Click
        dataview1.Visible = False

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfile As New OleDbDataAdapter("SELECT * From ETUDIANTS", accscon)
            Dim adapsqlfile As New SqlDataAdapter("SELECT * From ETUDIANTS", impocon)
            adapfile.Fill(tabfile)
            '*********************partie de vider la BDD*****************************
            Dim k, l, m As New Integer
            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfile.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 1 To l
                    varcmd = "DELETE From ETUDIANTS WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('ETUDIANTS',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            '************************************************************************
            Dim i, j, t As New Integer
            t = tabfile.Rows.Count
            MsgBox(t)
            For i = 0 To t - 1
                varcmd = "INSERT INTO ETUDIANTS ([Matricule],[NomEtud],[Prenoms],[NomUser],[PassWord])VALUES('" & tabfile.Rows(i).Item(0) & "','" & tabfile.Rows(i).Item(1) & "','" & tabfile.Rows(i).Item(2) & "','" & tabfile.Rows(i).Item(3) & "' ,'" & tabfile.Rows(i).Item(4) & "')"
                cmdfil = New SqlCommand(varcmd, impocon)
                cmdfil.ExecuteNonQuery()
            Next
            strinit = "DBCC CHECKIDENT('ETUDIANTS',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            ' Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub
    Private Sub COPIERCOLLEREXCELToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COPIERCOLLEREXCELToolStripMenuItem1.Click
        Try
            Dim tabfil, tabsqlfil, tabfile As New DataTable
            Dim varcmd, strinit As String
            impocon.Open()
            accscon.Open()
            Dim k, l, m As New Integer
            Dim cmdfil, cmdinit As New SqlCommand
            Dim adapfil As New OleDbDataAdapter("SELECT * From INSCRITSUE", accscon)
            Dim adapsqlfil As New SqlDataAdapter("SELECT * From INSCRITSUE", impocon)
            '*********************partie de vider la BDD*****************************

            MsgBox("CLIQUER SUR OK POUR CONFIRMER LA VIDANGE DU BDD")
            adapsqlfil.Fill(tabsqlfil)
            l = tabsqlfil.Rows.Count
            If (l <> 0) Then
                For k = 0 To l
                    varcmd = "DELETE From INSCRITSUE WHERE ID=" & k
                    cmdfil = New SqlCommand(varcmd, impocon)
                    cmdfil.ExecuteNonQuery()
                Next
            End If
            strinit = "DBCC CHECKIDENT('INSCRITSUE',RESEED,0)"
            cmdinit = New SqlCommand(strinit, impocon)
            cmdinit.ExecuteNonQuery()
            MsgBox("Operation Terminé avec Succés...")
            vidinscriue = "fait"
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try

        '************************************************************************
    End Sub

    Private Sub IMPORTERLABDDToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERLABDDToolStripMenuItem2.Click

        Try
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
            For i = 0 To l - 1
                If tabsqlfil.Rows(i).Item(6) = "" Then
                    tabsqlfil.Rows(i).Item(6) = 0
                End If
                If tabsqlfil.Rows(i).Item(7) = "" Then
                    tabsqlfil.Rows(i).Item(7) = 0
                End If

                If tabsqlfil.Rows(i).Item(8) = "" Then
                    tabsqlfil.Rows(i).Item(8) = 0
                End If
                If tabsqlfil.Rows(i).Item(9) = "" Then
                    tabsqlfil.Rows(i).Item(9) = 0
                End If
                If tabsqlfil.Rows(i).Item(10) = "" Then
                    tabsqlfil.Rows(i).Item(10) = 0
                End If
                varcmd = "INSERT INTO INSCRITMODULE ([AnScol],[Sem],[Promo],[Matricule],[Code_Mat],[CcNote],[TpNote],[CiNote],[CfNote],[MoyMod])VALUES('" & tabsqlfil.Rows(i).Item(1) & "','" & tabsqlfil.Rows(i).Item(2) & "','" & tabsqlfil.Rows(i).Item(3) & "','" & tabsqlfil.Rows(i).Item(4) & "','" & tabsqlfil.Rows(i).Item(5) & "','" & tabsqlfil.Rows(i).Item(6) & "','" & tabsqlfil.Rows(i).Item(7) & "','" & tabsqlfil.Rows(i).Item(8) & "','" & tabsqlfil.Rows(i).Item(9) & "' ,'" & tabsqlfil.Rows(i).Item(10) & "')"
                cmdexpo = New OleDbCommand(varcmd, accscon)
                cmdexpo.ExecuteNonQuery()
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub CLOSEBDDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CLOSEBDDToolStripMenuItem.Click
        dataview1.Visible = False

        impocon.Open()
        accscon.Open()
        impocon.Close()
        accscon.Close()

    End Sub

    Private Sub DEPASSEABSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DEPASSEABSToolStripMenuItem.Click
        Dim tabcopy As New DataTable
        Dim j, k, i As New Integer
        Dim strno As String
        strno = "Non"

        Dim adapcopyabs As New SqlDataAdapter("SELECT Anscol,Sem,Matricule,Matiere,Jour,Justifie,TypAbs FROM ABSENCES", impocon)
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
        Else
            MsgBox("NO ABSENCES")

        End If
    End Sub

    Private Sub NONREMPLIRToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NONREMPLIRToolStripMenuItem.Click

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
        Else
            MsgBox("LES ENSEIGNANTS SONT TOUS REMPLIR...")
        End If

    End Sub

    Private Sub EXPORTERToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPORTERToolStripMenuItem.Click
        Try
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
            For j = 0 To k - 1
                strexpor = "insert into ABSENCES ([AnScol],[Sem],[Matricule],[Matiere],[Jour],[Justifie],[TypAbs])values('" & tabsql.Rows(j).Item(1) & "','" & tabsql.Rows(j).Item(2) & "','" & tabsql.Rows(j).Item(3) & "','" & tabsql.Rows(j).Item(4) & "','" & tabsql.Rows(j).Item(5) & "','" & tabsql.Rows(j).Item(6) & "','" & tabsql.Rows(j).Item(7) & "')"
                cmdacc2 = New OleDbCommand(strexpor, accscon)
                cmdacc2.ExecuteNonQuery()

            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

   

    Private Sub IMPORTERToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IMPORTERToolStripMenuItem1.Click

        Try
            impocon.Open()
            Dim strpath As String
            Dim tabexel As New DataTable
            Dim i, j, k As New Integer
            Dim cmdsql As New SqlCommand

            If vidinscr = "fait" Then
                OpenFileDialog1.ShowDialog()
                Dim exelcon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & OpenFileDialog1.FileName & "';Extended Properties='Excel 12.0;HDR=YES;';")
                Dim adapexel As New OleDbDataAdapter("select * from [INSCRITS$]", exelcon)
                adapexel.Fill(tabexel)

                i = tabexel.Rows.Count
                For j = 0 To i - 1
                    ' MsgBox(tabexel.Rows(j).Item(0))
                    strpath = "INSERT INTO INSCRITS ([AnScol],[Sem],[Promo],[Sect],[Gr],[Matricule],[Ne],[MoySem],[MoyRach],[Moyenne],[Rang])VALUES('" & tabexel.Rows(j).Item(0) & "','" & tabexel.Rows(j).Item(1) & "','" & tabexel.Rows(j).Item(2) & "','" & tabexel.Rows(j).Item(3) & "','" & tabexel.Rows(j).Item(4) & "','" & tabexel.Rows(j).Item(5) & "','" & tabexel.Rows(j).Item(6) & "','" & tabexel.Rows(j).Item(7) & "','" & tabexel.Rows(j).Item(8) & "','" & tabexel.Rows(j).Item(9) & "','" & tabexel.Rows(j).Item(10) & "')"
                    cmdsql = New SqlCommand(strpath, impocon)
                    cmdsql.ExecuteNonQuery()

                Next
                MsgBox("Operation Terminé avec Succés...")
                vidinscr = "nonfait"
            Else

                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub
    Private Sub EXPORTERLABDDToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPORTERLABDDToolStripMenuItem2.Click
        Try
            OpenFileDialog1.ShowDialog()
            Dim exelcon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & OpenFileDialog1.FileName & "';Extended Properties='Excel 12.0;HDR=YES;';")
            accscon.Open()
            MsgBox("CLIQUER SUR OK POUR CONFIRMER L'EXPORTATION DU BDD")
            Dim strpath As String
            Dim tabexel As New DataTable
            Dim i, j, k As New Integer
            Dim cmdsql As New OleDbCommand
            Dim cmdacc As New OleDbCommand("DELETE from INSCRITSUE", accscon)
            cmdacc.ExecuteNonQuery()
            Dim adapexel As New OleDbDataAdapter("select * from [INSCRITSUE$]", exelcon)
            adapexel.Fill(tabexel)
            i = tabexel.Rows.Count
            For j = 0 To i - 1
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
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub COPIERCOLLEREXCELToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COPIERCOLLEREXCELToolStripMenuItem2.Click
        Try
            impocon.Open()
            Dim strpath As String
            Dim tabexel As New DataTable
            Dim i, j, k As New Integer
            Dim cmdsql As New SqlCommand

            If vidinscriue = "fait" Then
                OpenFileDialog1.ShowDialog()
                Dim exelcon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & OpenFileDialog1.FileName & "';Extended Properties='Excel 12.0;HDR=YES;';")
                Dim adapexel As New OleDbDataAdapter("select * from [INSCRITSUE$]", exelcon)
                adapexel.Fill(tabexel)
                i = tabexel.Rows.Count
                For j = 0 To i - 1
                    ' MsgBox(tabexel.Rows(j).Item(0))
                    strpath = "INSERT INTO INSCRITSUE ([AnScol],[Promo],[Matricule],[MoySem1],[MoyRach1],[Ne1],[MoySem2],[MoyRach2],[Ne2],[MoyAnu],[MoyRan],[Rang],[Decision])VALUES('" & tabexel.Rows(j).Item(0) & "','" & tabexel.Rows(j).Item(1) & "','" & tabexel.Rows(j).Item(2) & "','" & tabexel.Rows(j).Item(3) & "','" & tabexel.Rows(j).Item(4) & "','" & tabexel.Rows(j).Item(5) & "','" & tabexel.Rows(j).Item(6) & "','" & tabexel.Rows(j).Item(7) & "','" & tabexel.Rows(j).Item(8) & "','" & tabexel.Rows(j).Item(9) & "','" & tabexel.Rows(j).Item(10) & "','" & tabexel.Rows(j).Item(11) & "','" & tabexel.Rows(j).Item(12) & "')"
                    cmdsql = New SqlCommand(strpath, impocon)
                    cmdsql.ExecuteNonQuery()

                Next
                MsgBox("Operation Terminé avec Succés...")
                vidinscriue = "nonfait"
            Else

                MsgBox("IL FAUT D'ABORD VIDER LA BDD...")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try
    End Sub

    Private Sub EXPORTERLABDDToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EXPORTERLABDDToolStripMenuItem1.Click
        Try
            OpenFileDialog1.ShowDialog()


            Dim exelcon As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & OpenFileDialog1.FileName & "';Extended Properties='Excel 12.0;HDR=YES;';")
            accscon.Open()
            MsgBox("CLIQUER SUR OK POUR CONFIRMER L'EXPORTATION DU BDD")
            Dim strpath As String
            Dim tabexel As New DataTable
            Dim i, j, k As New Integer
            Dim cmdsql As New OleDbCommand
            Dim cmdacc As New OleDbCommand("DELETE from INSCRITS", accscon)
            cmdacc.ExecuteNonQuery()
            Dim adapexel As New OleDbDataAdapter("select * from [INSCRITS$]", exelcon)
            adapexel.Fill(tabexel)
            i = tabexel.Rows.Count
            For j = 0 To i - 1

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

                strpath = "INSERT INTO INSCRITS ([AnScol],[Sem],[Promo],[Sect],[Gr],[Matricule],[Ne],[MoySem],[MoyRach],[Moyenne],[Rang])VALUES('" & tabexel.Rows(j).Item(0) & "','" & tabexel.Rows(j).Item(1) & "','" & tabexel.Rows(j).Item(2) & "','" & tabexel.Rows(j).Item(3) & "','" & tabexel.Rows(j).Item(4) & "','" & tabexel.Rows(j).Item(5) & "','" & tabexel.Rows(j).Item(6) & "','" & tabexel.Rows(j).Item(7) & "','" & tabexel.Rows(j).Item(8) & "','" & tabexel.Rows(j).Item(9) & "','" & tabexel.Rows(j).Item(10) & "')"
                cmdsql = New OleDbCommand(strpath, accscon)
                cmdsql.ExecuteNonQuery()
            Next
            MsgBox("Operation Terminé avec Succés...")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            accscone.Close()
            impocon.Close()
            accscon.Close()
        End Try

    End Sub

    Private Sub ETUDIANTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ETUDIANTToolStripMenuItem.Click
        dataview1.Visible = False
    End Sub

    Private Sub ADMINISTRATIONToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ADMINISTRATIONToolStripMenuItem.Click
        dataview1.Visible = False
    End Sub

    Private Sub Panel1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseDown
        moov = True

        A = Windows.Forms.Cursor.Position.X - Me.Left
        B = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

 
    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseMove
        If moov Then
            Me.Left = Windows.Forms.Cursor.Position.X - A
            Me.Top = Windows.Forms.Cursor.Position.Y - B

        End If
    End Sub

    Private Sub Panel1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseUp
        moov = False
    End Sub

    Private Sub RETOURToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RETOURToolStripMenuItem.Click

        Form1.Show()
        Me.Close()


    End Sub

    Private Sub SORTIEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SORTIEToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub suivant_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub INSCRITSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles INSCRITSToolStripMenuItem.Click

    End Sub
End Class