' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin.Controllers
    <Authorize()>
    Public Class ColumnSequenceController
        Inherits PickProController
        ''' <summary>
        ''' Reselects the user's default column sequence.
        ''' </summary>
        ''' <param name="user">User to select defaults for.</param>
        ''' <param name="viewName">View or page to select the data for.</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list of string that contains the default column sequence for the page based on the user</returns>
        ''' <remarks></remarks>
        Private Function restoreDefaults(user As String, viewName As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim defaultList As New List(Of String)
            Try
                DataReader = RunSPArray("selTJColSeq", WSID, {{"@User", user, strVar}, {"@View", viewName, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        For x As Integer = 0 To DataReader.FieldCount - 1
                            defaultList.Add(DataReader(x))
                        Next
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return defaultList
        End Function

        ''' <summary>
        ''' Returns the view of the Column Sequence page if the page is reached on a POST request
        ''' </summary>
        ''' <param name="table">Sending view (Open Transactions, Transaction History, etc.)</param>
        ''' <returns>A view that conntains the information in order to reset the previous page to its original state</returns>
        ''' <remarks></remarks>
        <HttpPost()>
        Function Index(table As String, App As String) As ActionResult
            ' passes along the necessary form POST information (table name so we know whether we're changing OT or TH column preferences)
            Dim tablePOST As New List(Of String) From {table}
            Dim pageData As New List(Of String)
            Dim variables = Request.Form
            Dim returnPage = New List(Of String) From {table}
            Select Case table
                Case "Open Transactions"
                    tablePOST.Add("2")
                    pageData.AddRange({variables("sDateOpen"), variables("eDateOpen"), variables("orderNumberOpen"), variables("toteIDOpen"), variables("transactType"), variables("transactStat")})
                Case "Transaction History"
                    pageData.AddRange({variables("sDateTrans"), variables("eDateTrans"), variables("orderNumberTrans")})
                    tablePOST.Add("3")
                Case "Open Transactions Temp"
                    tablePOST.Add("4")
                    pageData.AddRange({variables("RecToView"), variables("ReasFilt"), variables("OrderNumTemp"), variables("ItemNumTemp")})
                Case "ReProcessed"
                    tablePOST.Add("5")
                Case Else
                    tablePOST.Add("1")
            End Select
            ' default column sequence will be passed in
            Dim cols As New List(Of List(Of String)) From {GlobalFunctions.getColumns(table, User.Identity.Name, Session("WSID")), restoreDefaults(User.Identity.Name, table, Session("WSID")), tablePOST, pageData, returnPage}
            Return View(model:=New With {.columns = cols, .app = App})
        End Function

    End Class
End Namespace
