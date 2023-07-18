' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Object which contains data relevant to printing Reprocess Transactions
''' </summary>
''' <remarks></remarks>
Public Class ReprocessTransModel
    Inherits GenericListModel

    Public Property ID As Integer
    Public Property OrderNumber As String
    Public Property reason As String
    Public Property message As String
    Public Property datestamp As DateTime

    ''' <summary>
    ''' Instantiates a new instance of the Reprocess Transactions printing model
    ''' </summary>
    ''' <param name="location">Location of the report design</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="userDirectory">User's print preferences directory</param>
    ''' <param name="WSID">Requesting user's current workstation ID</param>
    ''' <param name="ID">ID field of Reprocess Transactions table if printing a single record</param>
    ''' <param name="OrderNumber">Order Number filter for the reprocess transactions print</param>
    ''' <param name="reason">Reason to filter on</param>
    ''' <param name="message">Message to filter on</param>
    ''' <param name="datestamp">Date to filter on</param>
    ''' <remarks></remarks>
    Public Sub New(location As String, user As String, userDirectory As String, WSID As String, _
                   ID As Integer, OrderNumber As String, reason As String, _
                   message As String, datestamp As String)
        MyBase.New(location, user, userDirectory, "Reprocess Transactions", WSID)
        Me.ID = ID
        Me.OrderNumber = OrderNumber
        Me.reason = reason
        Me.message = message
        Try
            Me.datestamp = CDate(datestamp)
        Catch ex As Exception
            Me.datestamp = Nothing
        End Try
    End Sub

End Class
