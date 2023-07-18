' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports System.Threading

Namespace Admin
    Public Class SystemReplenishmentHub
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
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("WSID"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function
        ''' <summary>
        ''' Deletes the desired replenishment records
        ''' </summary>
        ''' <param name="Ident">Desginates which type of repelnishments to delete</param>
        ''' <param name="Filter1">The value for the identifier</param>
        ''' <param name="Filter2">The second value for ranges</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteReplenishmentsBy(Ident As String, Filter1 As String, Filter2 As String, searchString As String, searchCol As String, Status As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing, success As Boolean = True

                                                         Try
                                                             DataReader = RunSPArray("delReplenReport", Context.QueryString.Get("WSID"), {{"@Ident", Ident, strVar}, {"@Filter1", Filter1, strVar}, _
                                                                                                                                       {"@Filter2", Filter2, strVar}, {"@searchString", searchString, strVar}, _
                                                                                                                                       {"@searchCol", searchCol, strVar}, {"@Status", Status, strVar}, _
                                                                                                                                       {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                                       {"@User", Context.User.Identity.Name, strVar}})
                                                             If DataReader.HasRows Then
                                                                 success = False
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("SystemReplenishmentHub", "deleteReplenishmentsBy", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return success
                                                     End Function)
        End Function


        ''' <summary>
        ''' Updates a specified RP_ID in Replenishment Queue to the Replenish value specified
        ''' </summary>
        ''' <param name="RPID">The replenishment id that is being updated</param>
        ''' <param name="replenish">If the id is going ot be marked for replenishment</param>
        ''' <returns>None, task of sub</returns>
        ''' <remarks></remarks>
        Public Function updateReplenishmentInclude(RPID As Integer, replenish As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateReplenishmentsInclude", Context.QueryString.Get("WSID"), {{"@RP_ID", RPID, intVar}, {"@Replenish", CastAsSqlBool(replenish), intVar}})

                                                 Clients.Others.updatedReplenishments()
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishment", "updateReplenishmentInclude", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Updates all Replenishments to be included/excluded if they qualify
        ''' </summary>
        ''' <param name="replenish">Replenishment value</param>
        ''' <param name="reorder">Reorder view</param>
        ''' <returns>None, task of sub</returns>
        ''' <remarks></remarks>
        Public Function updateReplenishmentIncludeAll(replenish As Boolean, reorder As Boolean, searchString As String, searchColumn As String, Filter As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 Filter = Filter.Replace("[", "[Replenishment Queue].[").Replace("Item Num.", "Item Number").Replace("Repln, Point", "Replenishment Point").Replace("Repln. Level", "Replenishment Level").Replace("Avail. Qty.", "Available Qty").Replace("Repln. Qty", "Replenishment Qty").Replace("Trans. Qty", "Transaction Qty")

                                                 RunActionSP("updateReplenishmentQueueAll", Context.QueryString.Get("WSID"),
                                                             {{"@ReOrder", CastAsSqlBool(reorder), intVar}, {"@Replenish", CastAsSqlBool(replenish), intVar},
                                                             {"@searchString", searchString, strVar}, {"@searchColumn", searchColumn, strVar}, {"@Where", Filter, strVar}})

                                                 Clients.Others.updatedReplenishments()
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "updateReplenishmentIncludeAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function



        ''' <summary>
        ''' Repopulates Replenishment Queue with new data
        ''' </summary>
        ''' <param name="kanban">The value of the kanabn preference</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function createNewReplenishments(kanban As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delReplenItems", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                 RunActionSP("insReplenishmentQueue", Context.QueryString.Get("WSID"), {{"@Kanban", CastAsSqlBool(kanban), intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "createNewReplenishments", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function


        ''' <summary>
        ''' Updates a transaction quantity in the replenishment queue
        ''' </summary>
        ''' <param name="rp_id">ID of entry to edit</param>
        ''' <param name="trans_qty">New Transaction Quantity</param>
        ''' <returns>None, task of sub</returns>
        ''' <remarks></remarks>
        Public Function editTransactionQtyReplenishmentQueue(rp_id As Integer, trans_qty As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateReplenQueueTransQty", Context.QueryString.Get("WSID"), {{"@RP_ID", rp_id, intVar}, {"@TransQty", trans_qty, intVar}})

                                                 Clients.Others.updatedReplenishments()
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "editTransactionQtyReplenishmentQueue", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets count data for the current system replenishments tab
        ''' </summary>
        ''' <returns>Object that contains the count data for the replenishments page</returns>
        ''' <remarks></remarks>
        Public Function grabCountInfos() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Return SystemReplenishment.selReplenCountInfo(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                    End Function)
        End Function

        ''' <summary>
        ''' Processes Replenishments and allocates the created orders to a location if available or sends them to reprocess transactions
        ''' </summary>
        ''' <param name="kanban">True if kanban, false otherwise</param>
        ''' <remarks></remarks>
        Public Function processReplenishments(kanban As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             SystemReplenishmentLocationAssignment.errorOccured = ""
                                             SystemReplenishmentLocationAssignment.exMessage = ""
                                             SystemReplenishmentLocationAssignment.ReplenishmentsInProgress = True
                                             Dim TimeStart As New Stopwatch
                                             TimeStart.Start()
                                             Dim reprocess As Boolean = SystemReplenishmentLocationAssignment.processSystemReplenishments(Clients, Context.QueryString.Get("WSID"), Context.User.Identity.Name, kanban)
                                             TimeStart.Stop()
                                             Try
                                                 RunActionSP("insEventLogPP", Context.QueryString.Get("WSID"), {{"@Message", "System Replenishments completed.  Time elapsed during process (minutes): " & (TimeStart.ElapsedMilliseconds / (1000 * 60)), strVar}, {"@EventType", "System Replenishments", strVar}, _
                                                                                                    {"@EventCode", "", strVar}, {"@Location", Context.QueryString.Get("WSID"), strVar}, {"@TransactionID", 0, intVar}, _
                                                                                                    {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "processReplenishments", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))

                                             Finally
                                                 ' if the stop button was used to halt replenishments
                                                 If SystemReplenishmentLocationAssignment.StopReplenishments Then
                                                     SystemReplenishmentLocationAssignment.StopReplenishments = False
                                                     SystemReplenishmentLocationAssignment.ReplenishmentsAborted(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     Clients.All.updateReplenishmentStatus("Aborted", "Processing Aborted", "stopped")
                                                 ElseIf SystemReplenishmentLocationAssignment.errorOccured <> "" Then
                                                     Clients.All.updateReplenishmentStatus("0", "Error Occured: ", "Error Occured: " + SystemReplenishmentLocationAssignment.exMessage)
                                                 ElseIf Not reprocess Then
                                                     ' if the process finished and we do not have reprocess, alert everyone
                                                     Clients.All.updateReplenishmentStatus("All", "Processing Finished", "done")
                                                 Else
                                                     ' otherwise the process finished and there are reprocess.  So only alert the non-calling users of the end.  The calling user will be alerted to 
                                                     ' the reprocess transactions being present and offered the opportunity to print a reprocess report
                                                     Clients.Others.updateReplenishmentStatus("All", "Processing Finished", "done")
                                                 End If
                                                 ' the replenishment process is stopped, so reset the stopped replenishment variable just in case there was an early termination in the previous run
                                                 SystemReplenishmentLocationAssignment.StopReplenishments = False
                                                 SystemReplenishmentLocationAssignment.ReplenishmentsInProgress = False
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Halts replenishments
        ''' </summary>
        ''' <returns>None task of sub</returns>
        ''' <remarks></remarks>
        Public Function haltReplenishments() As Task
            Return Task.Factory.StartNew(Sub()
                                             ' Stop replenishment processing
                                             SystemReplenishmentLocationAssignment.StopReplenishments = True
                                             ' Log in the event log who stopped the processing, as it can be someone who did not start the process
                                             Try
                                                 RunActionSP("insEventLogPP", Context.QueryString.Get("WSID"),
                                                             {{"@EventType", "Replenishments Halted", strVar}, {"@Message", "System Replenishment process halted", strVar},
                                                                {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Location", Context.QueryString.Get("WSID"), strVar},
                                                                {"@User", Context.User.Identity.Name, strVar}, {"@EventCode", "0", strVar}, {"@TransactionID", 0, intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "haltReplenishments", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        Public Function AddFilterItemNums(Items As String) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Try
                                                 RunActionSP("delReplenItems", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                 If Items <> "" Then
                                                     RunActionSP("insReplenItems", Context.QueryString.Get("WSID"), {{"@ItemNums", Items, strVar}})
                                                 End If


                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("SystemReplenishmentHub", "AddFilterItemNums", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 Return False
                                             End Try

                                             Return True
                                         End Function)
        End Function


    End Class
End Namespace

