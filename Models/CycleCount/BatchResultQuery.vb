' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class BatchResultQuery
    Private _ToItem As String
    Private _NotCounted As Date
    Private _PickStart As Date
    Private _PickEnd As Date
    Private _PutAwayStart As Date
    Private _PutAwayEnd As Date

    Public Property CountType As String
    Public Property FromLocation As String
    Public Property ToLocation As String
    Public Property IncludeEmpty As Boolean
    Public Property IncludeOther As Boolean
    Public Property FromItem As String
    Public Property ToItem As String
        Get
            Return _ToItem
        End Get
        Set(value As String)
            _ToItem = IIf(IsNothing(value), "", value)
        End Set
    End Property
    Public Property Description As String
    Public Property Category As String
    Public Property SubCategory As String
    Public Property NotCounted As Date
        Set(value As Date)
            _NotCounted = IIf(value.CompareTo(New Date(1111, 1, 11)) = 0, Nothing, value)
        End Set
        Get
            Return _NotCounted
        End Get
    End Property
    Public Property PickStart As Date
        Set(value As Date)
            _PickStart = IIf(value.CompareTo(New Date(1111, 1, 11)) = 0, Nothing, value)
        End Set
        Get
            Return _PickStart
        End Get
    End Property
    Public Property PickEnd As Date
        Set(value As Date)
            _PickEnd = IIf(value.CompareTo(New Date(1111, 1, 11)) = 0, Nothing, value)
        End Set
        Get
            Return _PickEnd
        End Get
    End Property
    Public Property PutAwayStart As Date
        Set(value As Date)
            _PutAwayStart = IIf(value.CompareTo(New Date(1111, 1, 11)) = 0, Nothing, value)
        End Set
        Get
            Return _PutAwayStart
        End Get
    End Property
    Public Property PutAwayEnd As Date
        Set(value As Date)
            _PutAwayEnd = IIf(value.CompareTo(New Date(1111, 1, 11)) = 0, Nothing, value)
        End Set
        Get
            Return _PutAwayEnd
        End Get
    End Property
    Public Property CostStart As String
    Public Property CostEnd As String
    Public Property WarehouseFilter As String
End Class
