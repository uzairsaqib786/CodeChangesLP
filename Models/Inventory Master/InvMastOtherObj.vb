' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Represents the "Other" tab in Inventory Master.
''' </summary>
''' <remarks></remarks>
Public Class InvMastOtherObj
    Public Property UnitCost As Decimal
    Public Property SupplierID As String
    Public Property ManufactID As String
    Public Property SpecFeats As String
    ''' <summary>
    ''' Creates a new object for the other tab  of Inventory Master.
    ''' </summary>
    ''' <param name="UnitCost">The unit cost field.</param>
    ''' <param name="SupplyID">The supplier id field.</param>
    ''' <param name="ManufactID">The manufacturer field.</param>>
    ''' <param name="SpecFeats">The special features field.</param>>
    ''' <remarks></remarks>
    Public Sub New(UnitCost As Decimal, SupplyID As String, ManufactID As String, SpecFeats As String)
        Me.UnitCost = UnitCost
        Me.SupplierID = SupplyID
        Me.ManufactID = ManufactID
        Me.SpecFeats = SpecFeats
    End Sub
End Class
