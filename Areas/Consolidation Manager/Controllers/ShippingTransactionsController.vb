' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace Consolidation.Controllers
    <Authorize()>
    Public Class ShippingTransactionsController
        Inherits PickProController

        ''' <summary>
        ''' Gets the shipping transactions page
        ''' </summary>
        ''' <param name="ordernumber">Order number ot appear on page</param>
        ''' <returns>View object that contains the infomation needed for the html page</returns>
        Function Index(ordernumber As String) As ActionResult
            Return View(New With {.orderNumber = ordernumber, .tableData = Consolidation.ShippingTransactions.insShipLabelData(ordernumber, User.Identity.Name, Session("WSID")), _
                                  .reasons = Consolidation.ShippingTransactions.getAdjustmentReason(Session("WSID"), User.Identity.Name)})
        End Function

        ''' <summary>
        ''' Prints the item label for the desired record based on the id from the shipping transactions table
        ''' </summary>
        ''' <param name="id">The id of the record to be printed</param>
        ''' <returns>A boolean telling if the print job executed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintShipTransLabel(id As Integer) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
            Try
                Dim sp As String = "selShipTransLabelLL"
                Dim params As String(,) = {{"@ST_ID", id, intVar}}
                Dim filename As String = "CMItem.lbl", LLType As String = "Label"


                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "CM Item Label", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ShippingTransactionsController", "PrintShipTransLabel", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace