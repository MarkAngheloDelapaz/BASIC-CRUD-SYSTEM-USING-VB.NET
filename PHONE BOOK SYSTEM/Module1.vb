Imports MySql.Data.MySqlClient
Module Module1

    Public con As New MySqlConnection 'As a connection
    Public cmd As New MySqlCommand 'to use commands or query
    Public adapter As New MySqlDataAdapter 'to get data on the data base or to use select command
    Public data As New DataSet 'data bindings to show on textboxes or other input objects
    Public table_info As New DataTable 'use to show data of personal info on data grid view1
    Public table_address As New DataTable 'use to show data of address info on data grid view2
    Public table_contact As New DataTable 'use to show data of contact number on data grid view3


    Sub openCon()
        con.ConnectionString = "Server=localhost;username=root;password=root;database=DBPHONEBOOK" 'my database  - DBPHONEBOOK
        con.Open()
    End Sub
End Module
