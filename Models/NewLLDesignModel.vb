' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel21

Public Class NewLLDesignModel
    Public Property description As String = ""
    Public Property filename As String = ""
    Public Property appName As String = ""
    Public Property dataSource As String = ""
    Public Property dataType As String = ""
    Public Property outputType As LlProject = LlProject.List
    Public Property exportFilename As String = ""

    Public Sub New()

    End Sub

    Public Sub New(description As String, filename As String, dataSource As String, dataType As String, outputType As LlProject, exportFilename As String, appName As String)
        Me.description = description
        Me.filename = filename
        Me.dataSource = dataSource
        Me.dataType = dataType
        Me.outputType = outputType
        Me.exportFilename = exportFilename
        Me.appName = appName
    End Sub
End Class