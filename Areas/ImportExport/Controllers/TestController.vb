' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace ImportExport.Controllers
    <Authorize()>
    Public Class TestController
        Inherits PickProController

        ' GET: Test
        Function Index() As ActionResult
            ImportExport.LocationAssignmentCounts.AssignCountsSync(Session("WSID"), "testUser")
            Return View()
        End Function


        Function TestCounts(OrderNums As List(Of String)) As ActionResult
            ImportExport.LocationAssignmentCounts.AssignCountsSync(Session("WSID"), User.Identity.Name, SelectOTIDsbyOrderNumber(OrderNums, Session("WSID")))
            Return Content("Counts")
        End Function

        Function TestPicks(OrderNums As List(Of String)) As ActionResult
            'ImportExport.LocationAssignmentPicks.AssignPicks(Session("WSID"), User.Identity.Name, otids:=SelectOTIDsbyOrderNumber(OrderNums, Session("WSID")))
            Return Content("Counts")
        End Function

        Function TestPuts(OrderNums As List(Of String)) As ActionResult
            ImportExport.LocationAssignmentPutAways.AssignPutAwaysSync(Session("WSID"), User.Identity.Name, OrderNums)
            Return Content("Counts")
        End Function

        Function SelectOTIDsbyOrderNumber(OrderNumbers As List(Of String), WSID As String) As List(Of Integer)
            Dim IDs As New List(Of Integer)
            For Each ordernumber In OrderNumbers
                Dim Datareader As SqlDataReader = Nothing
                Try
                    Datareader = RunSPArray("selOTIDOrderNumberLocAss", WSID, {{"@OrderNumber", ordernumber, strVar}})
                    If Datareader.HasRows Then
                        While Datareader.Read
                            IDs.Add(Datareader(0))
                        End While
                    End If
                Catch ex As Exception
                    Debug.WriteLine(ex.Message)
                    insertErrorMessages("GenerateCert", "checkPCName", ex.Message, "", "")
                Finally
                    If Not IsNothing(Datareader) Then
                        Datareader.Dispose()
                    End If
                End Try
            Next
            Return IDs
        End Function
    End Class
End Namespace