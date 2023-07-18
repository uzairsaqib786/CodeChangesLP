' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Model for printing an Event Log Report.  Used by print service as well.
''' </summary>
''' <remarks></remarks>
Public Class EventLogReportModel
    ''' <summary>
    ''' Starting Date to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sDate As String
    ''' <summary>
    ''' Ending Date to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eDate As String
    ''' <summary>
    ''' Event ID to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eID As Integer
    ''' <summary>
    ''' Message to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property message As String
    ''' <summary>
    ''' Event Location to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eLocation As String
    ''' <summary>
    ''' User or name stamp to filter by.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property nStamp As String
    ''' <summary>
    ''' List and Label Report Design location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
    ''' <summary>
    ''' User requesting the print.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property user As String
    ''' <summary>
    ''' User's internal directory on the server where print options are specified.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userDirectory As String
    Public ReadOnly view As String = "Event Log"
    Public ReadOnly type As LlProject = LlProject.List
    Public Property WSID As String

    Public Sub New(sDate As String, eDate As String, eID As Integer, location As String, user As String, userDirectory As String, message As String, eLocation As String, nStamp As String, WSID As String)
        Me.sDate = sDate
        Me.eDate = eDate
        Me.eID = eID
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.message = message
        Me.eLocation = eLocation
        Me.nStamp = nStamp
        Me.WSID = WSID
    End Sub
End Class
