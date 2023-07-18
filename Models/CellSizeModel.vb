' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CellSizeModel
    Property cellsize As New List(Of String)
    Property celltype As New List(Of String)

    Public Sub New(cs As List(Of String), ct As List(Of String))
        Me.cellsize = cs
        Me.celltype = ct
    End Sub
End Class
