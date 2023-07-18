' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlTypes
Imports System.Threading

Public Class ReprocessTransactionsHub
    Inherits Hub

    Private Shared _lockObject As Object = New Object()

    ''' <summary>
    ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function OnConnected() As Task

        'Adds a user to their own unique group by a value passed in during connection
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
        Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
        'Calls the Original Onconnected Function pass control back to the server
        Return MyBase.OnConnected()
    End Function

    ''' <summary>
    ''' Gets data for a selected transactions in Reprocess Transactions
    ''' </summary>
    ''' <param name="id">Id of a transaction in Open Transactions Temp</param>
    ''' <returns></returns>
    Public Function getReprocTransData(id As String) As Dictionary(Of String, Object)
        Dim data = New Dictionary(Of String, Object)
        Try
            data = GetResultMap("selOTTempData", Context.QueryString.Get("WSID"), {{"@id", id, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            insertErrorMessages("ReprocessTransactionsHub", "getReprocTransData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
        End Try
        Return data
    End Function


    ''' <summary>
    ''' Deletes selected entries in the Reprocess Transactions table
    ''' </summary>
    ''' <param name="ID">ID of the transaction to delete or 0</param>
    ''' <param name="reason">Reason filter</param>
    ''' <param name="message">Message filter</param>
    ''' <param name="datestamp">Date filter</param>
    ''' <param name="itemnumber">Item Number filter</param>
    ''' <param name="ordernumber">Order Number filter</param>
    ''' <param name="replenishments">All and only Replenishments (t/f yes/no)</param>
    ''' <returns>None task of of a sub</returns>
    ''' <remarks></remarks>
    Public Function deleteReprocessTransactions(history As Boolean, ID As Integer, reason As String, message As String, datestamp As String, itemnumber As String, ordernumber As String, replenishments As Boolean) As Task
        Return Task.Factory.StartNew(Sub()
                                         If history Then
                                             If reason <> "" Then
                                                 reason += "%"
                                             ElseIf message <> "" Then
                                                 message += "%"
                                             End If
                                         End If
                                         Try
                                             RunActionSP("delOTTempCLV", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar},
                                                                                      {"@Reason", reason, strVar},
                                                                                      {"@Message", message, strVar},
                                                                                      {"@Date", datestamp, dteVar},
                                                                                      {"@ItemNumber", itemnumber, strVar},
                                                                                      {"@OrderNumber", ordernumber, strVar},
                                                                                      {"@Replenishments", IIf(replenishments, 1, 0), intVar},
                                                                                      {"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                      {"@User", Context.User.Identity.Name, strVar},
                                                                                      {"@History", IIf(history, 1, 0), intVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("ReprocessTransactionsHub", "deleteReprocessTransactions", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

    ''' <summary>
    ''' Sets the reprocess, post as complete and send to history fields in reprocess transactions
    ''' </summary>
    ''' <param name="ID">ID of the transaction to update</param>
    ''' <param name="reprocess">Reprocess field</param>
    ''' <param name="postcomplete">Post as complete field</param>
    ''' <param name="sendhist">Send to history field</param>
    ''' <returns>None task of a sub</returns>
    ''' <remarks></remarks>
    Public Function setReprocessTransactionsInclude(ID As Integer, reprocess As Integer, postcomplete As Integer, sendhist As Integer, field As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         ReprocessTransactions.setReprocessInclude(ID, reprocess, postcomplete, sendhist, field, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         Clients.All.updatePostOrders()
                                     End Sub)
    End Function

    ''' <summary>
    ''' Posts reprocess transactions each of their desired choices
    ''' </summary>
    ''' <returns>A string telling if the operation completed successfully</returns>
    Public Function postReprocessTransactions() As String
        SyncLock _lockObject

            Try
                'Inserts records marked as Reprocess into Open Transactions
                Dim dr = RunSPArray("selSystemPref", Context.QueryString.Get("WSID"), {{"nothing"}})
                If dr Is Nothing OrElse Not dr.HasRows Then
                    Return "Error"
                End If
                dr.Read()
                Dim TempToPending = dr("OT Temp to OT Pending")

                Dim SPs As New List(Of SQLCommandInfo)

                If TempToPending Then
                    SPs.Add(New SQLCommandInfo With {.SP = "insOTPendingFromReprocess", .Params = {{"nothing"}}})
                    'RunSPArray("insOTPendingFromReprocess", Context.QueryString.Get("WSID"), {{"nothing"}})
                Else
                    SPs.Add(New SQLCommandInfo With {.SP = "insOTFromReprocess", .Params = {{"nothing"}}})
                    'RunSPArray("insOTFromReprocess", Context.QueryString.Get("WSID"), {{"nothing"}})
                End If

                'Insert records marked as Reprocess into Reprocessed table for audit trail
                SPs.Add(New SQLCommandInfo With {.SP = "insReprocessed", .Params = {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}})
                'RunSPArray("insReprocessed", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, _
                '                                                               {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                'Insert records marked as Post as complete into Open Transactions
                SPs.Add(New SQLCommandInfo With {.SP = "insOTReprocessComplete", .Params = {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}})
                'RunSPArray("insOTReprocessComplete", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, _
                '                                                                                                               {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                'Insert records marked as Post as complete in Reprocesssed Table
                SPs.Add(New SQLCommandInfo With {.SP = "insReprocessedPostComplete", .Params = {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}})
                'RunSPArray("insReprocessedPostComplete", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, _
                '                                                                                                                    {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                'Update Master Record ID for OT Transactions after Reprocessing
                SPs.Add(New SQLCommandInfo With {.SP = "updateOTMasterID", .Params = {{"nothing"}}})
                'RunSPArray("updateOTMasterID", Context.QueryString.Get("WSID"), {{"nothing"}})

                'Inserts records marked as Post to Reprovess History to Open Trans Temp history table
                SPs.Add(New SQLCommandInfo With {.SP = "insReprocessHistory", .Params = {{"nothing"}}})
                ' RunSPArray("insReprocessHistory", Context.QueryString.Get("WSID"), {{"nothing"}})

                'Deletes all records that were posted from the Open Trans Temp Table
                SPs.Add(New SQLCommandInfo With {.SP = "delReprocessed", .Params = {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}})
                'RunSPArray("delReprocessed", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, _
                '                                                               {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                RunActionSPMulti(SPs, Context.QueryString.Get("WSID"))
                Return "Success"
            Catch ex As Exception
                insertErrorMessages("Reprocess Transactions", "postReprocessTransactions", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return "Error"
            End Try

            Return "Error"
        End SyncLock

    End Function

    ''' <summary>
    ''' Gets the filtered orders from reprocess
    ''' </summary>
    ''' <param name="ItemNumber">Item Number filter</param>
    ''' <param name="OrderNumber">Order Number filter</param>
    ''' <param name="Holds">Hold transactions or all</param>
    ''' <returns>A list of string that contains the order numbers that satisfy the filters</returns>
    ''' <remarks></remarks>
    Public Function getFilteredOrders(ItemNumber As String, OrderNumber As String, Holds As Boolean, history As Boolean) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return ReprocessTransactions.getFilteredOrders(ItemNumber, OrderNumber, Holds, history, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Gets the orders marked for reprocess, post as complete or send to history
    ''' </summary>
    ''' <returns>An object that contains the order numbers marked for each post choice</returns>
    ''' <remarks></remarks>
    Public Function getOrdersToPost() As Task(Of ReprocCompleteHistOrdersObj)
        Return Task(Of ReprocCompleteHistOrdersObj).Factory.StartNew(Function() As ReprocCompleteHistOrdersObj
                                                                         Return ReprocessTransactions.getOrdersToPost(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                     End Function)
    End Function

    ''' <summary>
    ''' Gets data for a specific reprocess transaction for editing
    ''' </summary>
    ''' <param name="ID">ID of the transaction to get</param>
    ''' <param name="history">If the ID is in the history table</param>
    ''' <returns>A list of string that contains the information about the desired transaction</returns>
    ''' <remarks></remarks>
    Public Function getTransaction(ID As Integer, history As Boolean) As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Return ReprocessTransactions.getTransaction(ID, history, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Saves an edited transaction from reprocess
    ''' </summary>
    ''' <param name="ID">ID of the edited trans</param>
    ''' <param name="newvalues">List of new values to set</param>
    ''' <param name="oldvalues">List of old values to compare for conflicts</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveTransaction(ID As Integer, oldvalues As List(Of String), newvalues As List(Of String)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return ReprocessTransactions.saveTransaction(ID, oldvalues, newvalues, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Marks the specified column as the markAsTrue boolean in reprocess transactions
    ''' </summary>
    ''' <param name="column">Column to set value of</param>
    ''' <param name="markAsTrue">Value to set column to</param>
    ''' <returns>None task of a sub</returns>
    ''' <remarks></remarks>
    Public Function setAll(column As String, markAsTrue As Boolean) As Task
        Return Task.Factory.StartNew(Sub()
                                         Try
                                             RunActionSP("updateAllReprocessCols", Context.QueryString.Get("WSID"), {{"@Col", column, strVar}, {"@Val", IIf(markAsTrue, 1, 0), intVar}})
                                         Catch ex As Exception
                                             Debug.WriteLine(ex.Message)
                                             insertErrorMessages("ReprocessTransactionsHub", "markAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                         End Try
                                     End Sub)
    End Function

End Class
