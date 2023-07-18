' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Namespace Admin
    Public Class BatchManagerHub
        Inherits Hub

        ''' <summary>
        ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
        ''' </summary>
        ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
        ''' <remarks></remarks>
        Public Overrides Function OnConnected() As Task

            'Adds a user to their own unique group by a value passed in during connection
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function

        ''' <summary>
        ''' Gets the Batch Manager left table data
        ''' </summary>
        ''' <param name="TransType">The transaction type</param>
        ''' <returns>A list of list of string that contains the table rows</returns>
        ''' <remarks></remarks>
        Public Function getTableData(TransType As String) As Task(Of List(Of List(Of String)))
            Return Task(Of List(Of List(Of String))).Factory.StartNew(Function()
                                                                          Return BatchManager.getBMTable(TransType, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                      End Function)
        End Function

        ''' <summary>
        ''' Creates a batched order from the selected orders
        ''' </summary>
        ''' <param name="Batch">The data in the datatable</param>
        ''' <param name="NextBatchID">The new next batch id value</param>
        ''' <param name="TransType">The transaction type</param>
        ''' <returns>A list of list of string that cotians the new bm settings</returns>
        ''' <remarks></remarks>
        Public Function createBatch(Batch As List(Of List(Of String)), NextBatchID As String, TransType As String) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                                 Try
                                                                     Dim Commands As New List(Of SQLCommandInfo)

                                                                     For Each order In Batch
                                                                         'RunActionSP("updOTOTTempBatchID", Context.QueryString.Get("WSID"), {{"@OrderNum", order(0), strVar},
                                                                         '                                                                     {"@ToteNum", order(1), intVar},
                                                                         '                                                                     {"@NextBatchID", NextBatchID, strVar},
                                                                         '                                                                     {"@TransType", TransType, strVar}})


                                                                         Dim Command As New SQLCommandInfo With {.SP = "updOTOTTempBatchID", .Params = {{"@OrderNum", order(0), strVar},
                                                                                                                                              {"@ToteNum", order(1), intVar},
                                                                                                                                              {"@NextBatchID", NextBatchID, strVar},
                                                                                                                                              {"@TransType", TransType, strVar}}}
                                                                         Commands.Add(Command)

                                                                     Next
                                                                     RunActionSPMulti(Commands, Context.QueryString.Get("WSID"))
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.ToString())
                                                                     insertErrorMessages("BatchManagerHub", "createBatch", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                     Return New List(Of String) From {"createBatchError"}
                                                                 End Try
                                                                 Return BatchManager.getBMSettings(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             End Function)
        End Function

        ''' <summary>
        ''' Deletes all batches
        ''' </summary>
        ''' <param name="BatchID">The batch id to be deleted</param>
        ''' <param name="Ident">Tells if all or only the given batch id should be deleted</param>
        ''' <param name="TransType">The transaction type</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteAllBatches(BatchID As String, Ident As Integer, TransType As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim success As Boolean = True

                                                         Try
                                                             DataReader = RunSPArray("updOTBatchID", Context.QueryString.Get("WSID"), {{"@BatchID", BatchID, strVar}, _
                                                                                                                                             {"@Ident", Ident, intVar}, _
                                                                                                                                             {"@TransType", TransType, strVar}, _
                                                                                                                                             {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                                                             {"WSID", Context.QueryString.Get("WSID"), strVar}})

                                                             If DataReader.HasRows Then
                                                                 success = False
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("BatchManagerHub", "deleteAllBatches", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             success = False
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Gets data for populating the select dropdown for batches
        ''' </summary>
        ''' <param name="TransType">The transaction type</param>
        ''' <returns>A list of string in order to populate the batch dropdown to select existing batches</returns>
        ''' <remarks></remarks>
        Public Function grabBatchDeleteDrop(TransType As String) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Return BatchManager.selectBatchesDeleteDrop(TransType, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             End Function)
        End Function

        ''' <summary>
        ''' Updates tote ids for picks where specified
        ''' </summary>
        ''' <param name="orders">A list of order numbers and tote ids to be updated</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function updatePickToteIDs(orders As List(Of List(Of String))) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim WSID As String = Context.QueryString.Get("WSID")
                                                         Try
                                                             Dim Commands As New List(Of SQLCommandInfo)

                                                             For Each order As List(Of String) In orders
                                                                 'RunActionSP("updOTToteID", WSID, {{"@Order", order(0), strVar}, {"@ToteID", order(1), intVar}})

                                                                 Dim Command As New SQLCommandInfo With {.SP = "updOTToteID", .Params = {{"@Order", order(0), strVar}, {"@ToteID", order(1), intVar}}}
                                                                 Commands.Add(Command)
                                                             Next

                                                             RunActionSPMulti(Commands, Context.QueryString.Get("WSID"))
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.ToString())
                                                             insertErrorMessages("BatchManagerHub", "updatePickToteIDs", ex.ToString(), Context.User.Identity.Name, WSID)
                                                             Return False
                                                         End Try
                                                         Return True
                                                     End Function)
        End Function

        ''' <summary>
        ''' Gets the data necessary for the order status view if the user does not have permission to view order status
        ''' </summary>
        ''' <param name="order">The order number</param>
        ''' <param name="transType">The transaction type</param>
        ''' <returns>A list of list of string containing the data for this order number and transaction type</returns>
        ''' <remarks></remarks>
        Public Function getDetailView(order As String, transType As String) As Task(Of List(Of List(Of String)))
            Return Task(Of List(Of List(Of String))).Factory.StartNew(Function()
                                                                          Dim datareader As SqlDataReader = Nothing, details As New List(Of List(Of String)), inner As New List(Of String)
                                                                          Try
                                                                              datareader = RunSPArray("selBMOrderStatus", Context.QueryString.Get("WSID"), {{"@Order", order, strVar}, {"@TransType", transType, strVar}})

                                                                              If datareader.HasRows Then
                                                                                  While datareader.Read()
                                                                                      For x As Integer = 0 To datareader.FieldCount - 1
                                                                                          inner.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                                      Next
                                                                                      details.Add(inner)
                                                                                      inner = New List(Of String)
                                                                                  End While
                                                                              End If
                                                                          Catch ex As Exception
                                                                              Debug.WriteLine(ex.ToString())
                                                                              insertErrorMessages("BatchManagerHub", "getDetailView", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                          Finally
                                                                              If Not IsNothing(datareader) Then
                                                                                  datareader.Dispose()
                                                                              End If
                                                                          End Try
                                                                          Return details
                                                                      End Function)
        End Function

        ''' <summary>
        ''' Creates a super batch based on input priorities
        ''' </summary>
        ''' <param name="priorities">A string of all the priorities to be super batched</param>
        ''' <param name="maxLinesPerBatch">The maximum number of transactiosn that can be assigned ot this batch</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function createSuperBatch(priorities As String, maxLinesPerBatch As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim datareader As SqlDataReader = Nothing, orders As New List(Of Object)

                                             ' make sure that the temporary table being built in the sql has a comma at the end so that the last entry in the comma separated list gets included
                                             If priorities <> "" Then
                                                 priorities = priorities.Trim()
                                                 If priorities(priorities.Length - 1).ToString() <> "" Then
                                                     priorities = priorities & ","
                                                 End If
                                             End If

                                             Try
                                                 datareader = RunSPArray("selOTSuperBatchPriority", Context.QueryString.Get("WSID"), {{"@Priorities", priorities, strVar}})

                                                 If datareader.HasRows Then
                                                     While datareader.Read()
                                                         orders.Add(New With {.order = IIf(IsDBNull(datareader(0)), "", datareader(0)), _
                                                                              .zone = IIf(IsDBNull(datareader(1)), "", datareader(1))})
                                                     End While
                                                 End If
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("BatchManagerHub", "createSuperBatch try#1", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             Finally
                                                 If Not IsNothing(datareader) Then
                                                     datareader.Dispose()
                                                 End If
                                             End Try

                                             Try
                                                 Dim zone As String = "", currentBatch As Integer = 0, appendBatch As Integer = 0
                                                 Dim batchID As String = DateTime.Now().ToString("yyyyMMddhhmm") & "S-"
                                                 For x As Integer = 0 To orders.Count - 1
                                                     RunActionSP("updOTSuperBatchID", Context.QueryString.Get("WSID"), {{"@BatchID", batchID & Format(appendBatch, "000"), strVar}, _
                                                                                                                          {"@Order", orders(x).order, strVar}})
                                                     If currentBatch < maxLinesPerBatch And zone = orders(x).zone Then
                                                         currentBatch += 1
                                                     Else
                                                         currentBatch = 1
                                                         appendBatch += 1
                                                     End If
                                                 Next
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("BatchManagerHub", "createSuperBatch try#2", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Clears all super batches
        ''' </summary>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function clearSuperBatches() As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updOTSuperBatch", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, _
                                                                                                             {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.ToString())
                                                 insertErrorMessages("BatchManagerHub", "clearSuperBatches", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
    End Class
End Namespace
