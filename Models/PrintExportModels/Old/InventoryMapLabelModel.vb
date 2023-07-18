' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

''' <summary>
''' Inventory Map Label Model
''' </summary>
''' <remarks></remarks>
Public Class InventoryMapLabelModel
    ''' <summary>
    ''' Inv Map ID -- invMapID should be -1 if printing a range, otherwise it should be the Inv Map ID of the entry to print
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property invMapID As Integer
    ''' <summary>
    ''' Concatenation of zone, carousel, row, shelf, bin (as Location Number)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property startLoc As String
    ''' <summary>
    ''' Concatenation of zone, carousel, row, shelf, bin (as Location Number)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property endLoc As String
    ''' <summary>
    ''' Report Design location in the server.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property location As String
    ''' <summary>
    ''' User requesting printing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property user As String
    ''' <summary>
    ''' Location of user's printing preferences
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property userDirectory As String
    ''' <summary>
    ''' 1 = SQL GROUP BY (Zone, Carousel, Row, Shelf, Bin), otherwise duplicates may be printed.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property groupLikeLoc As Integer
    Public ReadOnly view As String = "Inventory Map"
    Public ReadOnly type As LlProject = LlProject.Label
    Public Property WSID As String

    Public Sub New(invMapID As Integer, startLoc As String, endLoc As String, user As String, userDirectory As String, location As String, groupLikeLoc As Integer, WSID As String)
        Me.invMapID = invMapID
        Me.startLoc = startLoc
        Me.endLoc = endLoc
        Me.user = user
        Me.userDirectory = userDirectory
        Me.location = location
        Me.groupLikeLoc = groupLikeLoc
        Me.WSID = WSID
    End Sub
End Class
