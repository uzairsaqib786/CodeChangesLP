' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CycleCountModel
    Public Property DiscTable As List(Of List(Of String))
    Public Property ModalData As Object



    Sub New(DiscTable As List(Of List(Of String)), ModalData As Object)
        Me.DiscTable = DiscTable
        Me.ModalData = ModalData
    End Sub


End Class
