' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21

Public Class CustomReportsModel
    ' necessary generic fields
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

    'The that the request came from
    Public ReadOnly view As String = "Custom Report"

    'The report type (list or label)
    Public Property type As LlProject

    'The workstation that is currently being worked on
    Public Property WSID As String

    'The sql connections string
    Public Property ConnectionString As String

    'The view the request came from
    Public Property REPORTVIEW As String

    'The order by clause associated wiht the report
    Public Property ORDERBY As String

    'The where clause associated wiht the report
    Public Property WHERE As String

    'The top cluase to determine if on a certain numbe rof records should be selected
    Public Property TOP As String

    'The report title
    Public Property reportName As String

    Public Sub New()

    End Sub

    ''' <summary>
    ''' Constructor for the object
    ''' </summary>
    ''' <param name="location"></param>
    ''' <param name="user"></param>
    ''' <param name="userDirectory"></param>
    ''' <param name="WSID"></param>
    ''' <param name="ConnectionString"></param>
    ''' <param name="type"></param>
    ''' <param name="REPORTVIEW"></param>
    ''' <param name="ORDERBY"></param>
    ''' <param name="WHERE"></param>
    ''' <param name="reportName"></param>
    ''' <param name="TOP"></param>
    ''' <remarks></remarks>
    Public Sub New(location As String, user As String, userDirectory As String, WSID As String, ConnectionString As String, type As LlProject, _
                   REPORTVIEW As String, ORDERBY As String, WHERE As String, reportName As String, TOP As String)
        Me.location = location
        Me.user = user
        Me.userDirectory = userDirectory
        Me.WSID = WSID
        Me.ConnectionString = ConnectionString
        Me.type = type

        Me.REPORTVIEW = REPORTVIEW
        Me.ORDERBY = ORDERBY
        Me.WHERE = WHERE
        Me.reportName = reportName
        Me.TOP = TOP
    End Sub
End Class