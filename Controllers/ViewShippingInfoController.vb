' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace Controllers
    <Authorize()>
    Public Class ViewShippingInfoController
        Inherits PickProController

        '
        ' GET: /Home
        ''' <summary>
        ''' Returns the shipping info page
        ''' </summary>
        ''' <param name="orderNumber">The order nuumber whose information is being displayed</param>
        ''' <param name="app">Which application the page is loading from</param>
        ''' <returns>A view object that contains information for the html page</returns>
        Function Index(orderNumber As String, Optional app As String = "Admin") As ActionResult
            Return View(New With {.orderNumber = orderNumber, .packTable = ViewShippingInfo.GetOrderStatPack(orderNumber, Session("WSID"), User.Identity.Name), _
                                  .shipTable = ViewShippingInfo.GetOrderStatShip(orderNumber, Session("WSID"), User.Identity.Name), _
                                  .app = app})
        End Function
    End Class
End Namespace