' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21

Public Class PreviewExportPrintModel

    'The file format
    Public Property desiredFormat As String

    'The rpeort that is being exported
    Public Property reportName As String

    'List of fields for the export
    Public Property fields As List(Of List(Of String))

    'List of reports for exporting
    Public Property reportTitles As List(Of String)

    'The report type (list or label)
    Public Property type As LlProject

    'The file name of the report
    Public Property backFilename As String = ""

    'If it's a new rpeort file
    Public Property newEntry As Boolean = False

    'The field names within the report
    Public Property objFields As String

    Public Sub New()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fields"></param>
    ''' <param name="reportTitles"></param>
    ''' <remarks></remarks>
    Public Sub New(fields As List(Of List(Of String)), reportTitles As List(Of String))
        Me.fields = fields
        Me.reportTitles = reportTitles
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fields"></param>
    ''' <param name="reportTitles"></param>
    ''' <param name="reportName"></param>
    ''' <param name="desiredFormat"></param>
    ''' <param name="type"></param>
    ''' <param name="backFilename"></param>
    ''' <param name="newEntry"></param>
    ''' <param name="objFields"></param>
    ''' <remarks></remarks>
    Public Sub New(fields As List(Of List(Of String)), reportTitles As List(Of String), reportName As String,
                   desiredFormat As String, type As LlProject, backFilename As String, newEntry As Boolean, Optional objFields As String = Nothing)
        Me.fields = fields
        Me.reportTitles = reportTitles
        Me.reportName = reportName
        Me.desiredFormat = desiredFormat
        Me.type = type
        Me.backFilename = backFilename
        Me.newEntry = newEntry
        Me.objFields = objFields
    End Sub
End Class
