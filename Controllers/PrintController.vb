' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc


Namespace Controllers
    ''' <summary>
    ''' This Controller Handles any auxillary Apps Print requests, which will be relayed to the Print Service
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PrintController
        Inherits Controller

        Public WSID As String = ""
        Public Username As String = ""
        Public LabelName As String = "PickLabel"

        ''' <summary>
        ''' Starts a connection to the server
        ''' </summary>
        ''' <param name="filterContext"></param>
        Protected Overrides Sub OnActionExecuting(filterContext As ActionExecutingContext)
            WSID = Request("WSID")
            Username = Request("Username")
            LabelName = Request("Label")
            Dim conn = Config.getConnectionString(WSID, Session)
            userCS.TryAdd(WSID, conn)
        End Sub

        ''' <summary>
        ''' Prints the pick label
        ''' </summary>
        ''' <param name="OTID">The id of the transaction to print</param>
        ''' <returns>A boolean telling if the print was completed successfully</returns>
        Function PickLabel(OTID As String) As ActionResult
            If WSID Is Nothing Then
                WSID = Session("WSID")
            End If
            Dim params(,) As String = {{"@OTID", OTID, intVar}}, filename As String = LabelName & ".lbl", spname As String = "selTransByID"
            Try
                Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance
                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(Username, WSID, Server, "Super Batch Label", "Label", filename, spname, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("PrintController", "printPickLabel", ex.Message, Username, WSID)
                Return Json(False, JsonRequestBehavior.AllowGet)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace