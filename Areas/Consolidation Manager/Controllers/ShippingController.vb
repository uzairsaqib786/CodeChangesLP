' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Consolidation.Controllers
    <Authorize()>
    Public Class ShippingController
        Inherits PickProController

        ''' <summary>
        ''' Gets the Shipping controller page
        ''' </summary>
        ''' <param name="ordernumber">The order number to display</param>
        ''' <returns>View object that contains the information for the html page</returns>
        Function Index(ordernumber As String) As ActionResult
            Return View(New With {.OrderNum = ordernumber, .ShipData = Consolidation.Shipping.selShippingData(ordernumber, User.Identity.Name, Session("WSID")), _
                                  .Carriers = Consolidation.Shipping.selCarriers(User.Identity.Name, Session("WSID")), _
                                  .ShipPrefs = Consolidation.Shipping.selShippingPreferences(Session("WSID"), User.Identity.Name), _
                                  .ShipComp = Consolidation.Shipping.selShippingComp(ordernumber, User.Identity.Name, Session("WSID"))})
        End Function

        ''' <summary>
        ''' Prints the packing list for the shipping table based on the given order number 
        ''' </summary>
        ''' <param name="OrderNum">The order number to print for</param>
        ''' <returns>Boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintShipOrderPL(OrderNum As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selShippingAllLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}}
                Dim filename As String = "CMOrderPL.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "CM OrderPL", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ShippingController", "PrintShipOrderPL", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints the shipping packing list for the desired order number and container id
        ''' </summary>
        ''' <param name="OrderNum">The order number that is to be printed</param>
        ''' <param name="ContId">The container id that is to be printed</param>
        ''' <returns>Boolean telling if the print job completed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintShipContPL(OrderNum As String, ContId As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selShippingListLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}, {"@ContID", ContId, strVar}}
                Dim filename As String = "CMOrderPL.lst", LLType As String = "List"


                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "CM ContPL", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ShippingController", "PrintShipContPL", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace