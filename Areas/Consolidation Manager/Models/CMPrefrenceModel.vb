' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class CMPrefrenceModel
    Property EmailPackingSlip As Boolean
    Property DefaultPackingList As String
    Property DefaultLookupType As String
    Property VerifyItems As String
    Property BlindVerifyItems As String
    Property PrintVerified As String
    Property PrintUnVerified As String
    Property CustomToteManifest As String
    Property AutoCompShipComplete As Boolean
    Property PackingList As String
    Property Packing As Boolean
    Property ConfirmPacking As Boolean
    Property AutoPrintContPL As Boolean
    Property AutoPrintOrderPL As Boolean
    Property AutoPrintContLabel As Boolean
    Property EnterContainerID As Boolean
    Property ContainerIDDefault As String
    Property ConfirmPackingQuant As Boolean
    Property Freight As Boolean
    Property Freight1 As Boolean
    Property Freight2 As Boolean
    Property Weight As Boolean
    Property Length As Boolean
    Property Width As Boolean
    Property Height As Boolean
    Property Cube As Boolean
    Property Shipping As Boolean
    Property NonPickproOrders As Boolean
    Property ValidateStagingLocations As Boolean
    ''' <summary>
    ''' Sets the properties to their desired values for the CM Preferences page. This contains both consolidation and shipping
    ''' </summary>
    ''' <param name="EmailPackingSlip"></param>
    ''' <param name="DefaultPackingList"></param>
    ''' <param name="DefaultLookupType"></param>
    ''' <param name="VerifyItems"></param>
    ''' <param name="BlindVerifyItems"></param>
    ''' <param name="PrintVerified"></param>
    ''' <param name="PrintUnVerified"></param>
    ''' <param name="CustomToteManifest"></param>
    ''' <param name="AutoCompShipComplete"></param>
    ''' <param name="PackingList"></param>
    ''' <param name="Packing"></param>
    ''' <param name="ConfirmPacking"></param>
    ''' <param name="AutoPrintContPL"></param>
    ''' <param name="AutoPrintOrderPL"></param>
    ''' <param name="AutoPrintContLabel"></param>
    ''' <param name="EnterContainerID"></param>
    ''' <param name="ContainerIDDefault"></param>
    ''' <param name="ConfirmPackingQuant"></param>
    ''' <param name="Freight"></param>
    ''' <param name="Freight1"></param>
    ''' <param name="Freight2"></param>
    ''' <param name="Weight"></param>
    ''' <param name="Length"></param>
    ''' <param name="Width"></param>
    ''' <param name="Height"></param>
    ''' <param name="Cube"></param>
    ''' <param name="Shipping"></param>
    ''' <remarks></remarks>
    Public Sub New(EmailPackingSlip As Integer, DefaultPackingList As String, DefaultLookupType As String, VerifyItems As String, BlindVerifyItems As String, _
                   PrintVerified As String, PrintUnVerified As String, CustomToteManifest As String, AutoCompShipComplete As Integer, _
                   PackingList As String, Packing As Integer, ConfirmPacking As Integer, AutoPrintContPL As Integer, _
                   AutoPrintOrderPL As Integer, AutoPrintContLabel As Integer, EnterContainerID As Integer, _
                   ContainerIDDefault As String, ConfirmPackingQuant As Integer, Freight As Integer, Freight1 As Integer, _
                   Freight2 As Integer, Weight As Integer, Length As Integer, Width As Integer, Height As Integer, _
                   Cube As Integer, Shipping As Integer, NonPickproOrders As Boolean, ValidateStagingLocations As Boolean)

        Me.EmailPackingSlip = EmailPackingSlip
        Me.DefaultPackingList = DefaultPackingList
        Me.DefaultLookupType = DefaultLookupType
        Me.VerifyItems = VerifyItems
        Me.BlindVerifyItems = BlindVerifyItems
        Me.PrintVerified = PrintVerified
        Me.PrintUnVerified = PrintUnVerified
        Me.CustomToteManifest = CustomToteManifest
        Me.AutoCompShipComplete = AutoCompShipComplete
        Me.PackingList = PackingList
        Me.Packing = Packing
        Me.ConfirmPacking = ConfirmPacking
        Me.AutoPrintContPL = AutoPrintContPL
        Me.AutoPrintOrderPL = AutoPrintOrderPL
        Me.AutoPrintContLabel = AutoPrintContLabel
        Me.EnterContainerID = EnterContainerID
        Me.ContainerIDDefault = ContainerIDDefault
        Me.ConfirmPackingQuant = ConfirmPackingQuant
        Me.Freight = Freight
        Me.Freight1 = Freight1
        Me.Freight2 = Freight2
        Me.Weight = Weight
        Me.Length = Length
        Me.Width = Width
        Me.Height = Height
        Me.Cube = Cube
        Me.Shipping = Shipping
        Me.NonPickproOrders = NonPickproOrders
        Me.ValidateStagingLocations = ValidateStagingLocations

    End Sub

End Class
