' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks

Public Class SelectWorkHub
    Inherits Hub

    ''' <summary>
    ''' Gets the counts for the six readonly fields at the top of SelectWork
    ''' </summary>
    ''' <param name="transType">The currently selected transaction count</param>
    ''' <returns>A list of integer that contains the count info for the desired transaction type</returns>
    ''' <remarks></remarks>
    Public Function GetWorkCounts(transType As String) As Task(Of List(Of Integer))
        Return Task(Of List(Of Integer)).Factory.StartNew(Function() As List(Of Integer)
                                                              Return WorkManager.SelectWork.GetWorkCounts(Context.User.Identity.Name, transType, Context.QueryString.Get("WSID"))
                                                          End Function)
    End Function

    ''' <summary>
    ''' Clears the selected batches
    ''' </summary>
    ''' <param name="batches">The batches to be cleared</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ClearBatches(batches As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("updWMClearBatch", WSID, {{"@Batches", batches, strVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("SelectWorkHub", "ClearBatches", ex.Message, user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Assign the desired batches to the given user
    ''' </summary>
    ''' <param name="selectedUser">The user who is assigned these bacthes</param>
    ''' <param name="batches">The batches to assign</param>
    ''' <returns>A boolean telling if the operation completed successfully</returns>
    Public Function AssignBatchToUser(selectedUser As String, batches As List(Of String)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim batchStr As String = String.Join(",", batches)
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), username As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("updWMAssignBatchesToUser", WSID, {{"@User", selectedUser, strVar}, {"@Batches", batchStr, strVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("SelectWorkHub", "AssignBatchToUser", ex.Message, username, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Assigns a batch ID to the next set of eligible transactions
    ''' </summary>
    ''' <param name="selected">A comma seperated string that contians all the desired transctions</param>
    ''' <param name="transType">The transaction type of the transactions</param>
    ''' <param name="isTote">If the transactions are tote ids</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AssignBatchID(selected As String, transType As String, isTote As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return WorkManager.SelectWork.AssignBatchID(selected, transType, isTote, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function
End Class
