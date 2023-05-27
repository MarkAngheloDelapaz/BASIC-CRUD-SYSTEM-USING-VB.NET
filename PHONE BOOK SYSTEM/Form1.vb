
Imports System.ComponentModel
Public Class Form1
    Dim gender As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'S set the size of my form
        Me.MaximumSize = New Size(850, 640)
        Me.MinimumSize = New Size(850, 640)
        'E
        'S tooltips for my buttons
        ToolTip1.SetToolTip(Button13, "UPDATE PERSONAL INFORMATION")
        ToolTip1.SetToolTip(Button3, "ADD ADDRESS INFORMATION")
        ToolTip1.SetToolTip(Button4, "DELETE ADDRESS INFORMATION")
        ToolTip1.SetToolTip(Button12, "ADD CONTACT# INFORMATION")
        ToolTip1.SetToolTip(Button11, "DELETE CONTACT# INFORMATION")
        'E
        'S TO REFRESH MY TABLES DATA
        loadtable_info()
        loadtable_address()
        loadtable_contact()
        'E
        Button6.Hide()
        Button7.Hide()
        Button9.Hide()
        Button10.Hide()
        Button16.Hide()
        Button17.Hide()
        Button18.Hide()
        Button19.Hide()
    End Sub
    Sub loadtable_info()
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "Select * FROM TBLINFO"
            adapter.SelectCommand = cmd
            table_info.Clear()
            adapter.Fill(table_info)
            DataGridView1.DataSource = table_info
            con.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub loadtable_address()
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "Select CONTACT_ID,ADDRESS,MAILING_ADDRESS FROM TBLADD"
            adapter.SelectCommand = cmd
            table_address.Clear()
            adapter.Fill(table_address)
            DataGridView2.DataSource = table_address
            con.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub loadtable_contact()
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "SELECT CONTACT_ID, PHONE_NUMBER FROM TBLCONTACT"
            adapter.SelectCommand = cmd
            table_contact.Clear()
            adapter.Fill(table_contact)
            DataGridView3.DataSource = table_contact
            con.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dateValue As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "INSERT INTO TBLINFO (`NAME`,`BIRTHDAY`,`GENDER`,`EMAILADDRESS`) VALUES ('" & TextBox1.Text & "','" & dateValue & "','" & gender & "','" & TextBox2.Text & "'); SELECT LAST_INSERT_ID();"
            Dim lastInsertedId As Integer = CInt(cmd.ExecuteScalar())
            Label9.Text = lastInsertedId.ToString()
            cmd.CommandText = "INSERT INTO TBLADD (`CONTACT_ID`,`ADDRESS`,`MAILING_ADDRESS`) VALUES ('" & lastInsertedId & "','" & TextBox3.Text & "','1')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "INSERT INTO TBLCONTACT (`CONTACT_ID`,`PHONE_NUMBER`) VALUES ('" & lastInsertedId & "','" & MaskedTextBox1.Text & "')"
            cmd.ExecuteNonQuery()
            MsgBox("Successfully Added record!")
            con.Close()
            TextBox1.Clear()
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            TextBox2.Clear()
            TextBox3.Clear()
            MaskedTextBox1.Clear()
            loadtable_info() 'put function of data grid view to automatically refresh the table when had a new record
            loadtable_address()
            loadtable_contact()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try




    End Sub
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        gender = "Male"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        gender = "Female"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'START CLEAR INFO INPUT DATA
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
        TextBox1.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        TextBox2.Enabled = False
        MaskedTextBox1.Enabled = False
        DateTimePicker1.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        Button9.Show()
        Button10.Show()
        Button4.Hide()
        Button11.Hide()
        Button12.Hide()
        Button13.Hide()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Label9.Text = ""
        TextBox1.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        TextBox2.Enabled = True
        MaskedTextBox1.Enabled = True
        DateTimePicker1.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        TextBox3.Clear()
        Button9.Hide()
        Button10.Hide()
        Button4.Show()
        Button11.Show()
        Button12.Show()
        Button13.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Label9.Text = "" Then
            MsgBox("Please choose id number")
            TextBox3.Clear()
        Else
            openCon()
            Try
                cmd.Connection = con
                cmd.CommandText = "INSERT INTO TBLADD (`CONTACT_ID`,`ADDRESS`,`MAILING_ADDRESS`) VALUES ('" & Label9.Text & "', '" & TextBox3.Text & "', '0')"
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Added address!")
                con.Close()
                TextBox3.Clear()
                Label9.Text = ""
                loadtable_info()
                loadtable_address()
                loadtable_contact()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If


    End Sub
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Label9.Text = DataGridView1.Item("ID", DataGridView1.CurrentRow.Index).Value
        TextBox1.Text = DataGridView1.Item("NAME", DataGridView1.CurrentRow.Index).Value
        Dim gen As String
        gen = DataGridView1.Item("GENDER", DataGridView1.CurrentRow.Index).Value
        If gen = "Male" Then
            RadioButton1.Checked = True
        ElseIf gen = "Female" Then
            RadioButton2.Checked = True
        End If
        DateTimePicker1.Value = DataGridView1.Item("BIRTHDAY", DataGridView1.CurrentRow.Index).Value
        TextBox2.Text = DataGridView1.Item("EMAILADDRESS", DataGridView1.CurrentRow.Index).Value
        'START CLEAR ADDRESS AND PHONE NUMBER INPUT DATA
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        ' END CLEAR INFO INPUT DATA
    End Sub
    Private Sub DataGridView2_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView2.DoubleClick
        Label9.Text = DataGridView2.Item("CONTACT_ID", DataGridView2.CurrentRow.Index).Value
        TextBox3.Text = DataGridView2.Item("ADDRESS", DataGridView2.CurrentRow.Index).Value
        'START CLEAR INFO AND PHONE NUMBER INPUT DATA
        TextBox1.Clear()
        TextBox2.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
    End Sub
    Private Sub DataGridView3_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView3.DoubleClick
        Label9.Text = DataGridView3.Item("CONTACT_ID", DataGridView3.CurrentRow.Index).Value
        MaskedTextBox1.Text = DataGridView3.Item("PHONE_NUMBER", DataGridView3.CurrentRow.Index).Value
        'START CLEAR INFO AND ADDRESS INPUT DATA
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Label9.Text = ""
        TextBox1.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        DateTimePicker1.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        TextBox3.Clear()
        Button6.Hide()
        Button7.Hide()
        Button3.Show()
        Button4.Show()
        Button11.Show()
        Button13.Show()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'START CLEAR INFO INPUT DATA
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
        TextBox1.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        DateTimePicker1.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        Button7.Show()
        Button6.Show()
        Button3.Hide()
        Button4.Hide()
        Button11.Hide()
        Button13.Hide()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If Label9.Text = "" Then
            MsgBox("Please choose id number")
            MaskedTextBox1.Clear()
        Else
            openCon()
            Try
                cmd.Connection = con
                cmd.CommandText = "INSERT INTO TBLCONTACT (`CONTACT_ID`,`PHONE_NUMBER`) VALUES ('" & Label9.Text & "','" & MaskedTextBox1.Text & "')"
                cmd.ExecuteNonQuery()
                MsgBox("Successfully Added contact!")
                con.Close()
                MaskedTextBox1.Clear()
                Label9.Text = ""
                loadtable_info()
                loadtable_address()
                loadtable_contact()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        'START CLEAR INFO INPUT DATA
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
        TextBox3.Enabled = False
        MaskedTextBox1.Enabled = False
        Button14.Enabled = True
        Button15.Enabled = True
        Button1.Hide()
        Button2.Hide()
        Button3.Hide()
        Button4.Hide()
        Button11.Hide()
        Button12.Hide()

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Button1.Show()
        Button2.Show()
        TextBox3.Enabled = True
        MaskedTextBox1.Enabled = True
        Button3.Show()
        Button4.Show()
        Button11.Show()
        Button12.Show()
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim dateValue As String = DateTimePicker1.Value.ToString("yyyy/MM/dd")
        openCon()
        Try
            cmd.Connection = con
            cmd.CommandText = "UPDATE TBLINFO SET NAME = '" & TextBox1.Text & "', BIRTHDAY = '" & dateValue & "', GENDER = '" & gender & "' , EMAILADDRESS = '" & TextBox2.Text & "' WHERE ID = '" & Label9.Text & "'"
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            If rowsAffected > 0 Then
                MsgBox("Successfully updated record!")
            Else
                MsgBox("No record was updated.")
            End If
            con.Close()
            'START CLEAR INFO INPUT DATA
            Label9.Text = ""
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            MaskedTextBox1.Clear()
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            ' END CLEAR INFO INPUT DATA
            loadtable_info()
            loadtable_address()
            loadtable_contact()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'START CLEAR INFO INPUT DATA
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
        Button16.Show()
        Button17.Show()
        TextBox1.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        TextBox2.Enabled = False
        MaskedTextBox1.Enabled = False
        DateTimePicker1.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        Button9.Hide()
        Button10.Hide()
        Button3.Hide()
        Button11.Hide()
        Button12.Hide()
        Button13.Hide()
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Label9.Text = ""
        TextBox1.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        MaskedTextBox1.Enabled = True
        DateTimePicker1.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        Button14.Enabled = True
        Button15.Enabled = True
        TextBox3.Clear()
        Button16.Hide()
        Button17.Hide()
        Button3.Show()
        Button4.Show()
        Button11.Show()
        Button12.Show()
        Button13.Show()
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If Label9.Text = "" Then
            MsgBox("Please choose id number")
            TextBox3.Clear()
        Else
            openCon()
            Try
                cmd.Connection = con
                cmd.CommandText = "DELETE FROM TBLADD WHERE CONTACT_ID = '" & Label9.Text & "' AND ADDRESS = '" & TextBox3.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Data deleted successfully.")
                con.Close()
                TextBox3.Clear()
                Label9.Text = ""
                loadtable_info()
                loadtable_address()
                loadtable_contact()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'START CLEAR INFO INPUT DATA
        Label9.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ' END CLEAR INFO INPUT DATA
        Button18.Show()
        Button19.Show()
        TextBox1.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        DateTimePicker1.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        Button9.Hide()
        Button10.Hide()
        Button3.Hide()
        Button4.Hide()
        Button12.Hide()
        Button13.Hide()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Label9.Text = ""
        TextBox1.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        DateTimePicker1.Enabled = True
        Button1.Enabled = True
        Button2.Enabled = True
        Button14.Enabled = True
        Button15.Enabled = True

        TextBox3.Clear()
        Button16.Hide()
        Button17.Hide()
        Button18.Hide()
        Button19.Hide()
        Button3.Show()
        Button4.Show()
        Button11.Show()
        Button12.Show()
        Button13.Show()

        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        MaskedTextBox1.Clear()
        RadioButton1.Checked = False
        RadioButton2.Checked = False
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If Label9.Text = "" Then
            MsgBox("Please choose id number")
            TextBox3.Clear()
        Else
            openCon()
            Try
                cmd.Connection = con
                cmd.CommandText = "DELETE FROM TBLCONTACT WHERE CONTACT_ID = '" & Label9.Text & "' AND PHONE_NUMBER = '" & MaskedTextBox1.Text & "'"
                cmd.ExecuteNonQuery()
                MsgBox("Data deleted successfully.")
                con.Close()
                TextBox3.Clear()
                Label9.Text = ""
                loadtable_info()
                loadtable_address()
                loadtable_contact()

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub


    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = "" Then
            loadtable_info()
            loadtable_address()
            loadtable_contact()
        Else
            openCon()
            Try
                cmd.Connection = con
                adapter.SelectCommand = cmd
                cmd.CommandText = "Select * FROM TBLINFO WHERE ID LIKE '%" & TextBox4.Text & "%' OR NAME LIKE '%" & TextBox4.Text & "%'"
                table_info.Clear()
                adapter.Fill(table_info)
                DataGridView1.DataSource = table_info
                cmd.CommandText = "Select * FROM TBLADD WHERE CONTACT_ID = '" & TextBox4.Text & "'"
                table_address.Clear()
                adapter.Fill(table_address)
                DataGridView2.DataSource = table_address
                cmd.CommandText = "Select * FROM TBLCONTACT WHERE CONTACT_ID = '" & TextBox4.Text & "'"
                table_contact.Clear()
                adapter.Fill(table_contact)
                DataGridView3.DataSource = table_contact
                con.Close()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub


    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy/MM/dd"

    End Sub
End Class