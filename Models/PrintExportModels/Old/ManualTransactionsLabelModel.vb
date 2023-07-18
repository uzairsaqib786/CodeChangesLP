' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Object which contains data relevant to printing Kit Reports.
''' </summary>
''' <remarks></remarks>
Public Class ManualTransactionsLabelModel
    Public Property ID As Integer
    ''' <summary>
    ''' List and Label Report Design location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
    ''' <summary>
    ''' User requesting print
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property user As String
    ''' <summary>
    ''' User's printing preferences directory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userDirectory As String
    Public ReadOnly view As String = "Manual Transactions"
    Public ReadOnly type As LlProject = LlProject.Label
    Public Property WSID As String

    Public Sub New(location As String, user As String, userDirectory As String, ID As Integer, WSID As String)
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.ID = ID
        Me.WSID = WSID
    End Sub

End Class