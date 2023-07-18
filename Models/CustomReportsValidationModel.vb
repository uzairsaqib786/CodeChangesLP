' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CustomReportsValidationModel
    Public Property errs As New List(Of String)
    Public Property success As Boolean = False
    Public Property fileObj As Object = Nothing
    Public Property sqlObj As Object = Nothing

    Public Sub New(errs As List(Of String), success As Boolean, fileObj As Object, sqlObj As Object)
        Me.errs = errs
        Me.success = success
        Me.fileObj = fileObj
        Me.sqlObj = sqlObj
    End Sub
End Class
