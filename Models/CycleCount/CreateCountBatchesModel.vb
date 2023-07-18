' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CreateCountBatchesModel
    Property Warehouses As List(Of String)
    Property CycleOrders As List(Of Object)

    Sub New(Warehouses As List(Of String), CycleOrders As List(Of Object))
        Me.Warehouses = Warehouses
        Me.CycleOrders = CycleOrders
    End Sub
End Class
