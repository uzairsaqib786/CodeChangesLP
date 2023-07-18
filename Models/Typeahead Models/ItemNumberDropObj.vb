' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ItemNumberDropObj
    ''' <summary>
    ''' Item Number.  Used in typeaheads.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemNumber As String
    Public Property Description As String
    Public Property UnitOfMeasure As String
    Public Property CellSize As String
    Public Property WarehouseSensitive As Boolean
    Public Property DateSensitive As Boolean
    Public Property Velocity As String
    Public Property MaxQty As Integer
    Public Property MinQty As Integer

    Public Sub New(ItemNumber As String, Description As String, UnitOfMeasure As String, CellSize As String, Whse As Boolean, dte As Boolean, vel As String, max As Integer, min As Integer)
        Me.ItemNumber = ItemNumber
        Me.Description = Description
        Me.UnitOfMeasure = UnitOfMeasure
        Me.CellSize = CellSize
        Me.WarehouseSensitive = Whse
        Me.DateSensitive = dte
        Me.Velocity = vel
        Me.MaxQty = max
        Me.MinQty = min
    End Sub
End Class
