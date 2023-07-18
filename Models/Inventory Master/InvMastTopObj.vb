' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class InvMastTopObj

    Public Property StockCode As String
    Public Property SupplyItemID As String
    Public Property Description As String
    Public Property Category As String
    Public Property SubCategory As String
    Public Property UnitMeas As String
    Public Property TotalQuant As Integer
    Public Property TotalPicks As Integer
    Public Property TotalPuts As Integer
    Public Property TotalWip As Integer
    Public Property RepPoint As Integer
    Public Property RepLevel As Integer
    Public Property ReorPoint As Integer
    Public Property ReorQuant As Integer
    Public Property KanBanPoint As Integer
    Public Property KanBanLevel As Integer

    ''' <summary>
    ''' Creates a new object for the top part of Inventory Master.
    ''' </summary>
    ''' <param name="SC">The Stock Code (Item Number) field.</param>
    ''' <param name="SID">The Supplier Item ID field.</param>
    ''' <param name="Desc">The Description field.</param>
    ''' <param name="Cat">The category field.</param>
    ''' <param name="SubCat">The sub category field.</param>
    ''' <param name="UnitMeas">The unit of measure field.</param>
    ''' <param name="TotQty">The total quantity field.</param>
    ''' <param name="TotPicks">The total number of allocated picks field.</param>
    ''' <param name="TotPuts">The total number of allocated put aways field.</param>
    ''' <param name="TotWip">The total number of items with a zone WIP</param>
    ''' <param name="RepPoint">The replenishment point field.</param>
    ''' <param name="RepLevel">The replenishment level field.</param>
    ''' <param name="ReorPoint">The reorder point field.</param>
    ''' <param name="ReorQuant">The reorder quantity field.</param>
    ''' <remarks></remarks>
    Public Sub New(SC As String, SID As String, Desc As String, Cat As String, SubCat As String, UnitMeas As String, TotQty As Integer, TotPicks As Integer, TotPuts As Integer, TotWip As Integer, _
                   RepPoint As Integer, RepLevel As Integer, ReorPoint As Integer, ReorQuant As Integer, KanbanPoint As Integer, KanbanLevel As Integer)
        Me.StockCode = SC
        Me.SupplyItemID = SID
        Me.Description = Desc
        Me.Category = Cat
        Me.SubCategory = SubCat
        Me.UnitMeas = UnitMeas
        Me.TotalQuant = TotQty
        Me.TotalPicks = TotPicks
        Me.TotalPuts = TotPuts
        Me.TotalWip = TotWip
        Me.RepPoint = RepPoint
        Me.RepLevel = RepLevel
        Me.ReorPoint = ReorPoint
        Me.ReorQuant = ReorQuant
        Me.KanBanPoint = KanbanPoint
        Me.KanBanLevel = KanbanLevel
    End Sub
End Class

