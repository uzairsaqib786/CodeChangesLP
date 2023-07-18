' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' Handles typeahead for Inventory Master
''' </summary>
''' <remarks></remarks>
Public Class InventoryMasterTypeaheadModel
    Public Property ItemNumber As String
    Public Property Description As String
    Public Property Category As String
    Public Property SubCategory As String

    ''' <summary>
    ''' Instantiates a new instance of the Inventory Master Typeahead model
    ''' </summary>
    ''' <param name="ItemNumber">Item Number suggestion</param>
    ''' <param name="Description">Description of the related item number</param>
    ''' <param name="Category">Category of the related item number</param>
    ''' <param name="SubCategory">Subcategory of the related item number</param>
    ''' <remarks></remarks>
    Public Sub New(ItemNumber As String, Description As String, Category As String, SubCategory As String)
        Me.ItemNumber = ItemNumber
        Me.Description = Description
        Me.Category = Category
        Me.SubCategory = SubCategory

    End Sub
End Class
