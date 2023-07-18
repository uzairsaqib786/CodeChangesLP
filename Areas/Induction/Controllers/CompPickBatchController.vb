' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.Data.SqlClient

Namespace Induction.Controllers
    <Authorize()>
    Public Class CompPickBatchController
        Inherits PickProController

        ' GET: PickBatch
        Function Index() As ActionResult
            Return View(New With {.Prefs = Induction.Preferences.selectIMPreferences(Session("WSID"), User.Identity.Name)})
        End Function

        Function GetPickBatchTransTable(ByVal Data As TableObjectSent) As ActionResult
            Dim columnSequence As New List(Of String) From {"ID", "Order Number", "Tote ID", "Item Number", "Description", "Transaction Quantity",
                                                            "Location", "Zone", "Carousel", "Row", "Shelf", "Bin"}

            Dim column As String = columnSequence(Request.QueryString.Get("order[0][column]"))
            Dim SortOrder As String = Request.QueryString.Get("order[0][dir]")
            Dim DataReader As SqlDataReader = Nothing
            Dim inners As New List(Of String)
            Dim TableData As New List(Of List(Of String))
            Dim RecordCount As Integer = 0

            Try

                DataReader = RunSPArray("selOTPickBatchTransDT", Session("WSID"), {{"@BatchID", Data.batchid, strVar}, {"@ToteID", Data.toteid, strVar}, {"@sRow", Data.start + 1, intVar},
                                                                                   {"@eRow", Data.length + Data.start, intVar}, {"@SortCol", column, strVar},
                                                                                   {"@SortOrder", SortOrder, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        inners = New List(Of String)
                        RecordCount = DataReader("TotalCount")
                        For Each col In columnSequence
                            inners.Add(CheckDBNull(DataReader(col)))
                        Next
                        TableData.Add(inners)
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("CompPickBatchController", "GetPickBatchTransTable", ex.Message, User.Identity.Name, Session("WSID"))
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Json(New TableObject(Data.draw, RecordCount, RecordCount, TableData), JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace