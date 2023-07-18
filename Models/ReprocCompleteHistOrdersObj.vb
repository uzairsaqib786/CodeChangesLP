' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ReprocCompleteHistOrdersObj
    Public Property Reprocess As List(Of Object)
    Public Property Complete As List(Of Object)
    Public Property History As List(Of Object)
    Public Property ReprocessCount As Integer
    Public Property CompleteCount As Integer
    Public Property HistoryCount As Integer

    Public Sub New()
        Me.Reprocess = New List(Of Object)
        Me.Complete = New List(Of Object)
        Me.History = New List(Of Object)
    End Sub
End Class
