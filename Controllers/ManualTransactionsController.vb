' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Controllers
    <Authorize()>
    Public Class ManualTransactionsController
        Inherits PickProController

        ' GET: /ManualTransactions
        ''' <summary>
        ''' Returns the Manual Transactions View
        ''' </summary>
        ''' <returns>A view object that contains the infomration for the html page</returns>
        ''' <remarks></remarks>
        Function Index(Optional App As String = "Admin") As ActionResult
            Dim model = New AliasModel(User.Identity.Name, Session("WSID"))
            model.App = App
            Return View(model)
        End Function

        ''' <summary>
        ''' Gets the data the location typeahead
        ''' </summary>
        ''' <param name="query">The value to get suggestions for</param>
        ''' <returns>A list of object that contains the suggestiosn for the locations typeahead</returns>
        Function getLocations(query As String)
            Dim datareader As SqlDataReader = Nothing, objList As New List(Of Object)
            Try
                datareader = RunSPArray("selInvMapItemQuant", Session("WSID"), {{"@ItemNumber", query, strVar}})

                If datareader.HasRows Then
                    While datareader.Read()
                        objList.Add(New With {.qty = IIf(IsDBNull(datareader(0)), 0, datareader(0)),
                                              .location = IIf(IsDBNull(datareader(1)), "", datareader(1)),
                                              .mapID = IIf(IsDBNull(datareader(2)), 0, datareader(2))})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ManualTransactionsHub", "getQty", ex.Message, User.Identity.Name, Session("WSID"))
            Finally
                If Not IsNothing(datareader) Then
                    datareader.Dispose()
                End If
            End Try
            Return Json(objList, JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Gets typeahead for Manual Transactions Temporary Order Numbers
        ''' </summary>
        ''' <param name="query">Order Number for temporary transaction</param>
        ''' <returns>A list of object that contains the suggestions information for the typeahead</returns>
        ''' <remarks></remarks>
        Function getManualTransactions(query As String) As ActionResult
            Return Json(ManualTransactions.manualTransactionsTypeahead(query, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        ''' <summary>
        ''' Sends a print call to the service for Manual Trans Label
        ''' </summary>
        ''' <param name="transactionID">Transaction ID of the manual transaction to print</param>
        ''' <returns>A boolena telling if the print job was successful</returns>
        ''' <remarks></remarks>
        Public Function printMTLabel(transactionID As Integer) As ActionResult

            Dim WSID As String = Session("WSID"), username As String = User.Identity.Name
            Dim Clients As CustomReportsBroadcaster = CustomReportsBroadcaster.Instance

            Try
                Dim sp As String = "selXferOTLL"
                Dim params As String(,) = {{"@ID", transactionID, intVar}, {"@User", username, strVar}}
                Dim filename As String = "ManTran.lbl", LLType As String = "Label"

                Dim m As LLReportModel = ListLabelHelperFunctions.GetStandardLLPrintProperties(username, WSID, Server, "Manual Transaction Label", LLType, filename, sp, params)
                Clients.Print(m)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("ManualTransactionsController", "printLabel", ex.Message, username, WSID)
            End Try
            Return Json(True, JsonRequestBehavior.AllowGet)
        End Function

        Public Function getOrderTable(data As TableObjectSent) As ActionResult
            Return Json(ManualTransactions.getGenOrderTable(data.ordernum, data.transType, data.draw, data.start + 1, data.length + data.start,
                                                            Request.QueryString.Get("order[0][column]"), Request.QueryString.Get("order[0][dir]"),
                                                            User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function


        Public Function getManualOrderTA(OrderNumber As String, TransType As String) As ActionResult
            Return Json(ManualTransactions.selManualOrderTypeahead(OrderNumber, TransType, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace
