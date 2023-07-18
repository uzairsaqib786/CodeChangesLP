' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports PickPro_Web.Certificates

Public Class OrganizeWorkHub
    Inherits Hub
    ''' <summary>
    ''' Calls the vb function in order to clear the current work
    ''' </summary>
    ''' <param name="pick">Tells if pick transactions should be cleared</param>
    ''' <param name="put">Tells if putaway transactions should be cleared</param>
    ''' <param name="count">Tells if count trasnactions should be cleared</param>
    ''' <param name="username">The username to clear work for</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ClearCurrentWork(username As String, pick As Integer, put As Integer, count As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return WorkManager.OrganizeWork.ClearCurrentWork(pick, put, count, username, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Calls the vb function in order ot clear bacth ids
    ''' </summary>
    ''' <param name="pick">Tells if pick batches should be cleared</param>
    ''' <param name="put">Tells if put away batches should be cleared</param>
    ''' <param name="count">Tells if count batches should be cleared</param>
    ''' <param name="username">The username to clear batches for</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ClearBatchIDs(username As String, pick As Integer, put As Integer, count As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return WorkManager.OrganizeWork.ClearBatchIDs(pick, put, count, username, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' calls the vb function in order to create a batch 
    ''' </summary>
    ''' <param name="withUsername">If the username should be included in the batch id</param>
    ''' <param name="username">The username who is getting the batches</param>
    ''' <param name="pick">If pick batches should be cleared</param>
    ''' <param name="put">If put away batches should be cleared</param>
    ''' <param name="count">If count batches should be cleared</param>
    ''' <param name="clearBatches">If he batcehs should be cleared</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function CreateBatch(withUsername As Boolean, username As String, pick As Integer, put As Integer, count As Integer, clearBatches As Boolean) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return WorkManager.OrganizeWork.CreateBatch(withUsername, username, pick, put, count, clearBatches, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the refresh timer setting with the desired value
    ''' </summary>
    ''' <param name="Time">The new time value</param>
    ''' <returns>A boolean telling if the desired operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateRefreshTimer(Time As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         RunActionSP("updWMSettingsRefresh", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                               {"@Time", Time, intVar}})
                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("OrganizeWorkHub", "SaveGeneralSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Refreshes the count data
    ''' </summary>
    ''' <returns>An integer that contains the new count value</returns>
    Public Function refreshTotalCount() As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Return CInt(WorkManager.OrganizeWork.getOrgainzeWorkData(Context.User.Identity.Name, Context.QueryString.Get("WSID")).OpenCount)
                                                 End Function)
    End Function
End Class
