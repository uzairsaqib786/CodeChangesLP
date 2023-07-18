' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Contains user rights for Inventory Map view.
''' </summary>
''' <remarks></remarks>
Public Class InventoryMapModel
    ' Inventory Map's columns
    Public Property columnNames As List(Of String)
    ' individual accessor's rights on the page (delete, quarantine, clear, etc.)
    Public Property userRights As List(Of String)
    ' administrator or not
    Public Property AccessLevel As String
    Public Property ItemNumber As String
    Public ReadOnly Aliases As AliasModel

    Public Property App As String

    ''' <summary>
    ''' Instantiates an instance of the InventoryMapModel class
    ''' </summary>
    ''' <param name="columns">Sorted columns by user preference</param>
    ''' <param name="userRights">User's priveledges on the page</param>
    ''' <param name="accessLevel">Administrator or Staff Member</param>
    ''' <param name="ItemNumber">Item Number to filter on initially</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal columns As List(Of String), ByVal userRights As List(Of String), ByVal accessLevel As String, ItemNumber As String, app As String, Optional Aliases As AliasModel = Nothing)
        Me.columnNames = columns
        Me.userRights = userRights
        Me.AccessLevel = accessLevel
        Me.ItemNumber = ItemNumber
        Me.Aliases = Aliases
        Me.App = app
    End Sub
End Class
