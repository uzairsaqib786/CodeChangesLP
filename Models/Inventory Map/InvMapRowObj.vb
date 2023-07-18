' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Represents an entry in the Inventory Map for the purposes of editing or adding an entry
''' </summary>
''' <remarks></remarks>
Public Class InvMapRowObj
    ' fields for adding a new entry
    Public Property Location As String = nullVar
    Public Property LaserX As Integer = 0
    Public Property LaserY As Integer = 0
    Public Property Warehouse As String = nullVar
    Public Property Zone As String = nullVar
    Public Property Carousel As String = nullVar
    Public Property Row As String = nullVar
    Public Property Shelf As String = nullVar
    Public Property Bin As String = nullVar
    Public Property CellSize As String = nullVar
    Public Property VelocityCode As String = nullVar
    Public Property AltLight As Integer = 0
    Public Property UF1 As String = nullVar
    Public Property UF2 As String = nullVar
    Public Property Dedicated As Boolean = False
    Public Property DateSensitive As Boolean = False
    Public Property ItemNumber As String = nullVar
    Public Property MaxQty As Integer = 0
    Public Property MinQty As Integer = 0

    ' additional fields needed for editting an entry
    Public Property InvMapID As Integer = 0
    Public Property MasterInvMapID As Integer = 0
    Public Property MasterLocation As Boolean = True
End Class
