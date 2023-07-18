' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19

''' <summary>
''' Report data model for Order Status for printing.
''' </summary>
''' <remarks></remarks>
Public Class OrderStatusReportModel
    Public Property orderNumber As String
    Public Property toteID As String
    ''' <summary>
    ''' Determines select executed.  0 - select from Open Transactions, Transaction History and Open Transactions Temp by Order Number. 1 - Open Transactions, Open Transactions Temp by Tote ID. 2 - Open Transactions, Open Transactions Temp by order number.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property identifier As Integer
    ''' <summary>
    ''' Report design location for user.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
    ''' <summary>
    ''' User requesting print.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property user As String
    ''' <summary>
    ''' User's print preferences directory.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userDirectory As String
    Public ReadOnly view As String = "Order Status"
    Public ReadOnly type As LlProject = LlProject.List
    Public Property WSID As String

    Public Sub New(orderNumber As String, toteID As String, identifier As Integer, location As String, user As String, userDirectory As String, WSID As String)
        Me.orderNumber = orderNumber
        Me.toteID = toteID
        Me.identifier = identifier
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.WSID = WSID
    End Sub
End Class
