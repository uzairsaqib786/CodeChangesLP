' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Inventory Master Item Setup tab object.
''' </summary>
''' <remarks></remarks>
Public Class InvMastItemSetupObj
    Public Property DateSense As Boolean
    Public Property WareSense As Boolean
    Public Property FIFO As Boolean
    Public Property PrimeZone As String
    Public Property SecZone As String
    Public Property PickFenceQty As Integer
    Public Property SplitCase As Boolean
    Public Property CaseQuant As Integer
    Public Property PickSeq As Integer
    Public Property Active As Boolean
    Public Property CarCell As String
    Public Property CarVel As String
    Public Property CarMin As Integer
    Public Property CarMax As Integer
    Public Property BulkCell As String
    Public Property BulkVel As String
    Public Property BulkMin As Integer
    Public Property BulkMax As Integer
    Public Property CfCell As String
    Public Property CfVel As String
    Public Property CfMin As Integer
    Public Property CfMax As Integer
    Public Property OpenQuan As Integer
    Public Property HistQuan As Integer
    Public Property ReProcQuan As Integer
    Public Property FIFODate As String

    ''' <summary>
    ''' Creates a new object for the item setup tab of Inventory Master.
    ''' </summary>
    ''' <param name="DateSense">The date sesnitive field.</param>
    ''' <param name="WareSense">The warehouse sensitive field.</param>
    ''' <param name="FIFO">The fifo field.</param>
    ''' <param name="PrimeZone">The primary zone  field.</param>
    ''' <param name="SecZone">The secondary zone field.</param>
    ''' <param name="PickFence">The pick fence quantity field.</param>
    ''' <param name="SplitCase">The split case field.</param>
    ''' <param name="CaseQuant">The case quantity field.</param>
    ''' <param name="PickSeq">The pick sequence field.</param>
    ''' <param name="Active">The active field.</param>
    ''' <param name="CarCell">The carousel cell size field.</param>
    ''' <param name="CarVel">The carousel velocity code (golden zone) field.</param>
    ''' <param name="CarMin">The carousel minimum quantity field.</param>
    ''' <param name="CarMax">The carousel maximum quantity field.</param>
    ''' <param name="BulkCell">The bulk cell size field.</param>
    ''' <param name="BulkVel">The bulk velocity code (golden zone) field.</param>
    ''' <param name="BulkMin">The bulk minimum quantity field.</param>
    ''' <param name="BulkMax">The bulk maximum quantity field.</param>
    ''' <param name="CfCell">The carton flow cell size field.</param>
    ''' <param name="CfVel">The carton flow velocity code (golden zone) field.</param>
    ''' <param name="CfMin">The carton flow minimum quantity field.</param>
    ''' <param name="CfMax">The carton flow maximum quantity field.</param>
    ''' <param name="OpenQuan">The number of open transactions.</param>
    ''' <param name="HistQuan">The number of transactions in history.</param>
    ''' <param name="ProcQuan">The number of transactions in reproc.</param>
    ''' <remarks></remarks>
    Public Sub New(DateSense As Boolean, WareSense As Boolean, FIFO As Boolean, FIFODate As String, PrimeZone As String, SecZone As String, PickFence As Integer, SplitCase As Boolean, CaseQuant As Integer, PickSeq As Integer, _
                   Active As Boolean, CarCell As String, CarVel As String, CarMin As String, CarMax As String, BulkCell As String, BulkVel As String, BulkMin As String, BulkMax As String, _
                   CfCell As String, CfVel As String, CfMin As String, CfMax As String, OpenQuan As Integer, HistQuan As Integer, ProcQuan As Integer)
        Me.DateSense = DateSense
        Me.WareSense = WareSense
        Me.FIFO = FIFO
        Me.PrimeZone = PrimeZone
        Me.SecZone = SecZone
        Me.PickFenceQty = PickFence
        Me.SplitCase = SplitCase
        Me.CaseQuant = CaseQuant
        Me.PickSeq = PickSeq
        Me.Active = Active
        Me.CarCell = CarCell
        Me.CarVel = CarVel
        Me.CarMin = CarMin
        Me.CarMax = CarMax
        Me.BulkCell = BulkCell
        Me.BulkVel = BulkVel
        Me.BulkMin = BulkMin
        Me.BulkMax = BulkMax
        Me.CfCell = CfCell
        Me.CfVel = CfVel
        Me.CfMin = CfMin
        Me.CfMax = CfMax
        Me.OpenQuan = OpenQuan
        Me.HistQuan = HistQuan
        Me.ReProcQuan = ProcQuan
        Me.FIFODate = FIFODate
    End Sub
End Class
