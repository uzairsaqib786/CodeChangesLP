' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Namespace ImportExport.Controllers
    <Authorize>
    Public Class MenuController
        Inherits PickProController

        ' GET: Menu
        Function Index() As ActionResult
            Return View(ImportExportLogic.GetPreferences(Session("WSID")))
        End Function
        ''' <summary>
        ''' Gets the Manage Data Inventory Map datatable data 
        ''' </summary>
        ''' <param name="data">Contains the paramaters for the datatable function</param>
        ''' <returns>A tableobject which contians the data for populating the datatable</returns>
        ''' <remarks></remarks>
        Function getManageDataInvMapTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(ImportExport.ManageDataModalTables.getInvMapTables(data.draw, data.start + 1, data.length + data.start, _
                                                                           Request.QueryString.Get("order[0][column]"), _
                                                                           Request.QueryString.Get("order[0][dir]"), data.filter, _
                                                                           data.location, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function
        ''' <summary>
        ''' Gets the Manage Data Inventory datatable data
        ''' </summary>
        ''' <param name="data">Contains the paramaters for the datatable function</param>
        ''' <returns>A tableobject which contians the data for populating the datatable</returns>
        ''' <remarks></remarks>
        Function getManageDataInvTable(ByVal data As TableObjectSent) As ActionResult
            Return Json(ImportExport.ManageDataModalTables.getInvTables(data.draw, data.start + 1, data.length + data.start, _
                                                                           Request.QueryString.Get("order[0][column]"), _
                                                                           Request.QueryString.Get("order[0][dir]"), data.filter, _
                                                                           data.location, User.Identity.Name, Session("WSID")), JsonRequestBehavior.AllowGet)
        End Function

        Function ImportInventoryCSV() As ActionResult
            Try
                Dim reader As New StreamReader(Request.Files(0).InputStream)

                Dim parser As New TextFieldParser(reader.BaseStream)
                parser.HasFieldsEnclosedInQuotes = True
                parser.SetDelimiters(",")


                Dim ColumnList As New List(Of String)
                For Each column In parser.ReadFields
                    ColumnList.Add(column.Replace(" ", "").ToLower)
                Next
                While Not parser.EndOfData
                    Dim currentLine() As String = parser.ReadFields

                    RunSP("insInvetoryFromCSV", Session("WSID"), CreateInventoryParameters(ColumnList, currentLine))
                End While
            Catch ex As Exception
                insertErrorMessages("ImportExport", "ImportInvCSV", ex.Message, User.Identity.Name, Session("WSID"))
                Return Json(False)
            End Try

            Return Json(True)
        End Function

        Private Function CreateInventoryParameters(columnList As List(Of String), currentline() As String) As String(,)
            Try
                columnList = getAlteredColumnList("Inventory", columnList)
                Dim parameters(columnList.Count - 1, 2) As String
                Dim intColumnList As New List(Of String) From {"kanbanreplenishmentpoint", "kanbanreplenishmentlevel", "picksequence", "minimumrtsreelquantity", "minusescalequantity", "samplequantity", "avgpieceweight", "replenishmentpoint", "replenishmentlevel", "cfmaxqty", "cfminqty", "bulkminqty", "bulkmaxqty", "casequantity", "pickfenceqty", "maximumquantity", "minquantity", "reorderquantity", "reorderpoint", "securitylevel"}
                Dim stringColumnList As New List(Of String) From {"itemnumber", "description", "supplieritemid", "category", "subcategory", "primarypickzone", "suppliernumber", "manufacturer", "model", "specialfeatures", "unitofmeasure", "upccode", "cellsize", "secondarypickzone", "fifodate", "goldenzone", "bulkcellsize", "bulkvelocity", "cfcellsize", "cfvelocity"}
                Dim bitColumnList As New List(Of String) From {"cagestorage", "refrigeratedstorage", "expirationdate", "serialized", "fifo", "active", "datedensitive", "warehousesensitive", "splitcase", "usescale", "includeinautortsupdate"}
                For Each column In columnList
                    Dim index = columnList.IndexOf(column)
                    If stringColumnList.Contains(column) Or bitColumnList.Contains(column) Or intColumnList.Contains(column) Then
                        parameters(index, 0) = "@" & column
                        parameters(index, 1) = currentline(columnList.IndexOf(column))
                        If stringColumnList.Contains(column) Then
                            parameters(index, 2) = strVar
                        ElseIf (bitColumnList.Contains(column)) Then
                            parameters(index, 2) = boolVar
                        ElseIf intColumnList.Contains(column) Then
                            parameters(index, 2) = intVar
                        End If
                    End If
                Next
                Return parameters
            Catch ex As Exception
                insertErrorMessages("ImportExport", "ImportInvCSV", ex.Message, User.Identity.Name, Session("WSID"))
                Debug.WriteLine(ex.Message)
            End Try
        End Function

        Function ImportInvMapCSV() As ActionResult
            Try
                Dim reader As New StreamReader(Request.Files(0).InputStream)
                Dim ColumnList As New List(Of String)
                For Each column In reader.ReadLine.Split(",")
                    ColumnList.Add(column.Replace(" ", "").ToLower)
                Next
                While Not reader.EndOfStream
                    Dim currentLine() As String = reader.ReadLine.Split(",")

                    RunSP("insInvMapFromCSV", Session("WSID"), CreateInvMapParameters(ColumnList, currentLine))
                End While
            Catch ex As Exception
                insertErrorMessages("ImportExport", "ImportInvMapCSV", ex.Message, User.Identity.Name, Session("WSID"))
                Return Json(False)
            End Try

            Return Json(True)
        End Function

        Private Function CreateInvMapParameters(columnList As List(Of String), currentline() As String)
            columnList = getAlteredColumnList("Inventory Map", columnList)
            Dim parameters(columnList.Count - 1, 2) As String
            Dim intColumnList As New List(Of String) From {"locationid", "itemquantity", "maximumquantity", "masterinvmapid", "quantityallocatedpick", "quantityallocatedputaway", "minquantity"}
            Dim stringColumnList As New List(Of String) From {"itemnumber", "description", "location", "warehouse", "zone", "carousel", "row", "shelf", "bin", "revision", "serialnumber", "lotnumber", "unitofmeasure", "cellsize", "goldenzone", "userfield1", "userfield2"}
            Dim bitColumnList As New List(Of String) From {"dedicated", "datesensitive", "masterlocation"}
            Dim dateColumnList As New List(Of String) From {"putawaydate", "expirationdate"}
            For Each column In columnList
                Dim index = columnList.IndexOf(column)
                If stringColumnList.Contains(column) Or bitColumnList.Contains(column) Or intColumnList.Contains(column) Or dateColumnList.Contains(column) Then
                    parameters(index, 0) = "@" & column
                    parameters(index, 1) = currentline(columnList.IndexOf(column))
                    If stringColumnList.Contains(column) Then
                        parameters(index, 2) = strVar
                    ElseIf (bitColumnList.Contains(column)) Then
                        parameters(index, 2) = boolVar
                    ElseIf intColumnList.Contains(column) Then
                        parameters(index, 2) = intVar
                    ElseIf dateColumnList.Contains(column) Then
                        parameters(index, 2) = dteVar
                    End If
                End If
            Next
            Return parameters
        End Function

        ''' <summary>
        ''' Gets file mappings from database, and replaces import columns with system column names
        ''' </summary>
        ''' <param name="table"></param>
        ''' <param name="columnlist"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function getAlteredColumnList(table As String, columnlist As List(Of String))
            Dim Filemap As List(Of Dictionary(Of String, Object)) = GetResultMapList("selXferFileMap", "IE", {{"@Table", table, strVar}})
            For Each column In Filemap
                'If alternate mapping for column is provided and the imported columnlist contains that string, replace it with the system filename
                If Not String.IsNullOrEmpty(column("Import Fieldname")) AndAlso columnlist.Contains(column("Import Fieldname").ToString.ToLower) Then
                    columnlist(columnlist.IndexOf(column("Import Fieldname").ToString.ToLower)) = column("BP Fieldname").ToLower
                End If
            Next
            Return columnlist
        End Function


    End Class
End Namespace