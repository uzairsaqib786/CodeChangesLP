' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Consolidation.Controllers
    <Authorize()>
    Public Class ConfirmAndPackingController
        Inherits PickProController

        ' GET: ConfirmAndPacking
        ''' <summary>
        ''' Gets the confirm and packing page
        ''' </summary>
        ''' <param name="ordernumber">The order number to be dipslayed on page load</param>
        ''' <returns>View object that contains the information needed for the html page</returns>
        Function Index(ordernumber As String) As ActionResult
            Return View(New With {.orderNumber = ordernumber, .toteTable = Consolidation.ConfirmAndPacking.selConfPackToteTable(ordernumber, Session("WSID"), User.Identity.Name), _
                               .transTable = Consolidation.ConfirmAndPacking.selConfPackShipTransTable(ordernumber, Session("WSID"), User.Identity.Name), _
                               .contIDDrop = Consolidation.ConfirmAndPacking.selConfPackContIDDrop(ordernumber, Session("WSID"), User.Identity.Name), _
                               .complete = Consolidation.ConfirmAndPacking.selConfPackEnable(ordernumber, Session("WSID"), User.Identity.Name), _
                               .contID = Consolidation.ConfirmAndPacking.selContIDConfirmPack(ordernumber, Session("WSID"), User.Identity.Name), _
                               .reasons = Consolidation.ShippingTransactions.getAdjustmentReason(Session("WSID"), User.Identity.Name), _
                               .shipComp = Consolidation.ConfirmAndPacking.selConfPackShipComp(ordernumber, Session("WSID"), User.Identity.Name), _
                               .PrintPrefs = Consolidation.ConfirmAndPacking.SelCMPrefs(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' Prints the packing list from confirm and packing based on the given order number
        ''' </summary>
        ''' <param name="OrderNum">The order number to be printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintConfPackPackList(OrderNum As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selConfPackPackListLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}}
                Dim filename As String = "CMOrderPL.lst", LLType As String = "List"



                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Packing List - Container Order", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ConfirmAndPackingController", "PrintConfPackPackList", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints the packing list from confirm and packing based on the given order number and container id 
        ''' </summary>
        ''' <param name="OrderNum">The orer numbe rot be printed</param>
        ''' <param name="contID">The container id to be printed</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintConfPackPrintCont(OrderNum As String, contID As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selConfPackPrintContLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}, {"@ContID", contID, strVar}}
                Dim filename As String = "CMOrderPL.lst", LLType As String = "List"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Packing List - Container", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ConfirmAndPackingController", "PrintConfPackPrintCont", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Prints the confirm and packing label based on the order number and container id 
        ''' </summary>
        ''' <param name="OrderNum">The order number to be printed</param>
        ''' <param name="contID">The container id to be printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintConfPackLabel(OrderNum As String, contID As String) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selConfPackPrintLabelLL"
                Dim params As String(,) = {{"@OrderNum", OrderNum, strVar}, {"@ContID", contID, strVar}}
                Dim filename As String = "CMContID.lbl", LLType As String = "Label"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Container ID Label", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ConfirmAndPackingController", "PrintConfPackLabel", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Print the confirm and packing item label based on the given order number and id of the desired record
        ''' </summary>
        ''' <param name="orderNum">The order number to be printed</param>
        ''' <param name="ST_ID">The id of the record to be printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintConfPackItemLabel(orderNum As String, ST_ID As Integer) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selConfPackItemLabelLL"
                Dim params As String(,) = {{"@OrderNum", orderNum, strVar}, {"@ST_ID", ST_ID, intVar}}
                Dim filename As String = "CMItem.lbl", LLType As String = "Label"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Item Label - Packing", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ConfirmAndPackingController", "PrintConfPackitemLabel", ex.Message, username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace