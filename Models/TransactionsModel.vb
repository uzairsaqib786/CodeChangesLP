' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

''' <summary>
''' This object is for the entire transactions page, and depending on the view, the values will be set differently. 
''' The properties are toShow which is assigned to the view input, the openColumns which is tied to a list of all the columns for open transactions, 
''' transColumns which are tied to a list of all the columns in transaction history, and defaultValues  whcih is a list fo all the 
''' filters for the view
''' </summary>
''' <remarks></remarks>
Public Class TransactionsModel
    ''' <summary>
    ''' Item Number to automatically filter on when page loads
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property itemNumber As String

    Property Location As String
    ''' <summary>
    ''' View to show (Order Status, Open Transactions, Transaction History, Reprocess Transactions)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property toShow As String
    ''' <summary>
    ''' Open Transactions columns
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property openColumns As List(Of String)
    ''' <summary>
    ''' Transaction History columns
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property transColumns As List(Of String)
    Property defaultValues As List(Of String)
    Property transTempColumns As List(Of String)
    ''' <summary>
    ''' Item Number, Unit of Measure, etc. Aliases
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly aliases As AliasModel
    ''' <summary>
    ''' Reprocess Transactions modal units of measure
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly UoMs As List(Of String)
    ''' <summary>
    ''' Reprocess Transactions modal warehouses
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly warehouses As List(Of String)
    ''' <summary>
    ''' Reprocess Transactions boolean -> holds or all
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly holds As Boolean
    Public ReadOnly OrderStatusOrder As String

    Public Property reprocessedCols As List(Of String)
    Public Property app As String


    ''' <summary>
    ''' Instantiates a new instance of the TransactionsModel class
    ''' </summary>
    ''' <param name="view">Requested view (order status, open transactions, etc.)</param>
    ''' <param name="columns1">Open Transactions columns</param>
    ''' <param name="columns2">Transaction history columns</param>
    ''' <param name="columns3">Reprocess Transaction columns</param>
    ''' <param name="defaultVal">Filters including start and end date among others if applicable</param>
    ''' <param name="itemNum">Initial Item Number to search on</param>
    ''' <param name="aliases">Instance of the AliasModel class</param>
    ''' <param name="holds">Reprocess Transactions: Show holds or all</param>
    ''' <param name="UoMs">Reprocess Transactions modal: Select tag Units of Measure</param>
    ''' <param name="warehouses">Reprocess Transactions modal: Select tag Warehouses</param>
    ''' <remarks></remarks>
    Public Sub New(view As String, columns1 As List(Of String), columns2 As List(Of String), columns3 As List(Of String), columns4 As List(Of String), defaultVal As List(Of String), location As String, itemNum As String, aliases As AliasModel, UoMs As List(Of String), warehouses As List(Of String), holds As Boolean, OSO As String, app As String)
        'Stores the instance of the object so that values can be derived from the object.value  call
        toShow = view
        openColumns = columns1
        transColumns = columns2
        transTempColumns = columns3
        reprocessedCols = columns4
        defaultValues = defaultVal
        itemNumber = itemNum
        Me.aliases = aliases
        Me.UoMs = UoMs
        Me.warehouses = warehouses
        Me.holds = holds
        Me.OrderStatusOrder = OSO
        Me.Location = location
        Me.app = app
    End Sub
End Class
