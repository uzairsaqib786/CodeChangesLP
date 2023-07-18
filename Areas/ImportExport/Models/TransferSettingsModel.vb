' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class TransferSettingsModel
    Property Complete As Boolean
    Property ExportType As String
    Property ExportAdjustments As Boolean
    Property ExportDateRange As Boolean
    Property ExportFromDate As Date
    Property ExportHotPicks As Boolean
    Property ExportHotPutAways As Boolean
    Property ExportJobType As String
    Property ExportToDate As Date
    Property HoldPickUntilOrderComplete As Boolean
    Property HoldPickUntilShipComplete As Boolean
    Property HoldPickUntilToteComplete As Boolean
    Property ImportJobType As String
    Property RecombineSplit As Boolean
    Property Shipping As Boolean
    Property ShippingComplete As Boolean
    Property ShippingTransactions As Boolean
    Property VerifyLocations As Boolean
    Property WaitForSplit As Boolean
End Class
