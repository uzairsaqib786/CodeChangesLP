' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class EmployeesModel
    Public Property groupsAllowed As List(Of String)
    Public Property functionsAllowed As List(Of String)
    Public Property employeePickLevels As List(Of Tuple(Of String, String, String, String, String, String))
    Public Property bulkProMaxOrders As Integer
    Public Property bulkProZones As List(Of String)
    Public Property bulkProRangeAssign As List(Of Tuple(Of String, String))
    Public Property allZones As List(Of String)
End Class
