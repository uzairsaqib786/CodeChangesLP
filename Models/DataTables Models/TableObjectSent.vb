' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Object used by DataTables plugin.  Contains filters for various views for selecting data.
''' </summary>
''' <remarks></remarks>
Public Class TableObjectSent
    ' Shared data between all users of class
    ''' <summary>
    ''' Constant to keep user's requests current.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property draw As Integer
    ''' <summary>
    ''' Starting point to retrieve records from.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property start As Integer
    ''' <summary>
    ''' Number of records to retrieve.  Used in conjunction with start to get all records in the requested range.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property length As Integer
    ''' <summary>
    ''' Column to search on.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property searchColumn As String
    ' Open Transactions/Transaction History data, dates belong to Event Log as well, dates used in Employees Statistics
    ''' <summary>
    ''' Start Date
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sDate As DateTime
    ''' <summary>
    ''' End Date
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eDate As DateTime
    ''' <summary>
    ''' Transaction Status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property transStatus As String
    ''' <summary>
    ''' Transaction Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property transType As String
    ' order stat stuff (shared with OT)
    ''' <summary>
    ''' Order Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ordernum As String
    ''' <summary>
    ''' Tote ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property toteid As String
    ' exclusive Order Status vars
    ''' <summary>
    ''' Order Status variable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property checkvalue As Boolean
    ' Inventory Map
    ''' <summary>
    ''' Open, Quarantined or All (Locations in Inventory Map)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OQA As String
    ''' <summary>
    ''' Column to search on
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property searchString As String
    ''' <summary>
    ''' Column to sort on for System Replenishments current
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sortColumn As String
    ''' <summary>
    ''' Sorting direction for System Replenishments current
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property sortDirection As String
    ' Event Log
    ''' <summary>
    ''' Event Log Message Filter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property messageFilter As String
    ''' <summary>
    ''' Event Log Location filter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property eventLocation As String
    ''' <summary>
    ''' Username filter for Event Log
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property nameStamp As String
    ' Inventory Master
    ''' <summary>
    ''' Item Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property itemNumber As String
    'Employees Statistics
    Public Property users As String
    ' Hold Transactions
    ' reel, non, or both
    ''' <summary>
    ''' Hold Transactions variable to denote reels, non-reels or both
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property reel As String
    ' order number or item number
    ''' <summary>
    ''' Filter used as order or item number for hold transactions
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property entryFilter As String
    ''' <summary>
    ''' order or item value searched
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrderItem As String
    ''' <summary>
    ''' reprocess transactions for filtering on hold transactions
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property hold As Integer
    ''' <summary>
    ''' zone filter
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property zone As String
    ''' <summary>
    ''' Status filter on system replenishment
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property status As String
    ''' <summary>
    '''  reorder or all for system replenishment
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property reorder As Boolean
    ''' <summary>
    ''' Completed date for order status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property compdate As String

    Public Property _filter As String

    Public Property batchid As String

    Public Property totenum As Integer

    Public Property location As String
    Public Property top As Boolean
    Public Property which As String
    Public Property selected As String

    Public Property DictionaryData As New Dictionary(Of String, String)

    Private _cellSize As String
    Private _warehouse As String
    Private _InvMapID As Integer

    Public Property filter As String
        Set(value As String)
            _filter = IIf(value Is Nothing, "1 = 1", value)
        End Set
        Get
            Return _filter
        End Get
    End Property

    Public Property Cellsize As String
        Set(value As String)
            _cellSize = IIf(value Is Nothing, "", value)
        End Set
        Get
            Return _cellSize
        End Get
    End Property

    Public Property InvMapID As Integer
        Set(value As Integer)
            _InvMapID = IIf(IsNothing(value), -1, CInt(value))
        End Set
        Get
            Return _InvMapID
        End Get
    End Property


    Public Property Warehouse As String
        Set(value As String)
            _warehouse = IIf(value Is Nothing, "", value)
        End Set
        Get
            Return _warehouse
        End Get
    End Property

End Class