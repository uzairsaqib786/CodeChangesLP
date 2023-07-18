' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class ManageDataHub
    Inherits Hub
    ''' <summary>
    ''' Gets file field map data for the given table
    ''' </summary>
    ''' <param name="table">The table whose field mappings will be returned</param>
    ''' <returns>List of object, where each object contains the field mpa data for each column</returns>
    ''' <remarks></remarks>
    Public Function getXferFileFieldMapData(table As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(
            Function() As List(Of Object)
                Dim data As New List(Of Object)
                Dim DataReader As SqlDataReader = Nothing

                Try
                    DataReader = RunSPArray("selXferFileMap", "IE", {{"@Table", table, strVar}})

                    If DataReader.HasRows Then
                        While DataReader.Read()
                            data.Add(New With {.field = CheckDBNull(DataReader("Import Fieldname")), .system = DataReader("BP Fieldname")})
                        End While
                    End If

                Catch ex As Exception
                    insertErrorMessages("ManageDataHub", "getXferFileFieldMapData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    data = New List(Of Object)
                    data.Add(New With {.field = "Error", .system = "Error"})
                Finally
                    If Not IsNothing(DataReader) Then
                        DataReader.Dispose()
                    End If
                End Try

                Return data
            End Function)
    End Function
    ''' <summary>
    ''' Updates the desired column within the field map for the given table
    ''' </summary>
    ''' <param name="table">The table whose column is being chnaged</param>
    ''' <param name="systemfield">The system column name</param>
    ''' <param name="field">The new value for the column</param>
    ''' <returns>Boolean telling if the operation completed succesfully</returns>
    ''' <remarks></remarks>
    Public Function updateXferFileField(table As String, systemfield As String, field As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Try
                    RunActionSP("updXferFileMap", "IE", {{"@Table", table, strVar}, _
                                                         {"@FieldName", field, strVar}, _
                                                         {"@SystemFieldName", systemfield, strVar}})
                Catch ex As Exception
                    insertErrorMessages("ManageDataHub", "updateXferFieldMapData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function
    ''' <summary>
    ''' Deletes the desired ids from the desired inventory map table
    ''' </summary>
    ''' <param name="table">The inventory map table whose records are going to be deleted</param>
    ''' <param name="ids">The list of ids that are going to be deleted</param>
    ''' <returns>Boolean telling of the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteInvMapDataMan(table As String, ids As List(Of String)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean

                Try
                    RunActionSP("delIEInvMaps", "IE", {{"@Table", table, strVar}, _
                                                       {"@IDS", String.Join(",", ids.ToArray), strVar}})
                Catch ex As Exception
                    insertErrorMessages("ManageDataHub", "deleteInvMapDataMan", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function
    ''' <summary>
    ''' Deletes the desired data from the desired inventory tables
    ''' </summary>
    ''' <param name="table">The inventory table that is being deleted from</param>
    ''' <param name="itemnums">The list of item numbers that are going to be deleted</param>
    ''' <param name="filter">THe filter that is being applied to the table</param>
    ''' <returns>Boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteInvDataMan(table As String, itemnums As List(Of String), filter As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Try
                    If table = "kit" Or table = "scan codes" Then
                        filter = filter.Replace("[Item Number]", IIf(table = "kit", "[Kit Inventory].[Item Number]", "[Scan Codes].[Item Number]"))
                        RunActionSP("delIEInvKitScan", Context.QueryString.Get("WSID"), {{"@Table", table, strVar}, _
                                                                                         {"@filter", filter, strVar}})
                    Else
                        RunActionSP("delIEInvs", "IE", {{"@Table", table, strVar}, _
                                                        {"@ItemNums", String.Join(",", itemnums.ToArray), strVar}})
                    End If

                Catch ex As Exception
                    insertErrorMessages("ManageDataHub", "deleteInvDataMan", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function
    ''' <summary>
    ''' Executes the exporting sp for inventory map
    ''' </summary>
    ''' <returns>Boolean telling of the operation completed succcessfully</returns>
    ''' <remarks></remarks>
    Public Function ExportInventoryMap() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Try
                    RunActionSP("ExportInventoryMap", "IE", {{"nothing"}})
                Catch ex As Exception
                    Debug.Print(ex.Message)
                    insertErrorMessages("ManageDataHub", "ExportInventoryMap", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function
    ''' <summary>
    ''' Executes the exporting sp for inventory
    ''' </summary>
    ''' <returns>Boolean telling of the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ExportInventory() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(
            Function() As Boolean
                Try
                    RunActionSP("ExportInventory", "IE", {{"nothing"}})
                Catch ex As Exception
                    Debug.Print(ex.Message)
                    insertErrorMessages("ManageDataHub", "ExportInventory", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    Return False
                End Try
                Return True
            End Function)
    End Function
    ''' <summary>
    ''' Populates the the export inventory map with either all locations or non-empty locations
    ''' </summary>
    ''' <param name="allLocs">Tells if to use all lcoations or non-empty ones</param>
    ''' <returns>Boolean telling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function getInvMapLocations(allLocs As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("insIEExpInvMap", Context.QueryString.Get("WSID"), {{"@AllLocs", allLocs, intVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "getInvMapLocations", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function
    ''' <summary>
    ''' Populates the export inventory map with all item numbers
    ''' </summary>
    ''' <returns>Boolean telling of the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function getInvRecords() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("insIEExpInv", Context.QueryString.Get("WSID"), {{"nothing"}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "getInvRecords", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function
    ''' <summary>
    ''' Exports the given scan codes
    ''' </summary>
    ''' <param name="filter">Controls which scan codes are exported</param>
    ''' <returns>Boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function exportScanCodes(filter As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     filter = filter.Replace("[Item Number]", "[Scan Codes].[Item Number]")
                                                     Try
                                                         RunActionSP("insIEExpInvScanCode", Context.QueryString.Get("WSID"), {{"@filter", filter, strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "exportScanCodes", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function
    ''' <summary>
    ''' Runs the import from the Data Manager Inventory Map Modal
    ''' </summary>
    ''' <param name="autoImp">Tells if auto importing is enabled</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function importInvMapLocModal(autoImp As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String

                                                    Try
                                                        Dim tableCount As Integer = ImportExport.ManageDataModalTables.getInvMapTables(0, 1, 10, 0, "", "1=1", "import", Context.User.Identity.Name, Context.QueryString.Get("WSID")).recordsTotal

                                                        If (tableCount = 0 And autoImp = False) Then
                                                            Return "No Inventory Map Transactions to Import"
                                                        End If

                                                        RunActionSP("ImportInvMap", "IE", {{"nothing"}})

                                                    Catch ex As Exception
                                                        insertErrorMessages("ManageDataHub", "importInvMapLocModal", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Error"
                                                    End Try
                                                    Return "Imported"
                                                End Function)
    End Function
    ''' <summary>
    ''' Runs the import from the Data Manager Inventory Modal
    ''' </summary>
    ''' <param name="autoImp">Tells if auto importing is enabled</param>
    ''' <returns>String telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function procInventoryRecordsModal(autoImp As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String

                                                    Try

                                                        Dim tableCount As Integer = ImportExport.ManageDataModalTables.getInvTables(0, 1, 10, 0, "", "1=1", "import", Context.User.Identity.Name, Context.QueryString.Get("WSID")).recordsTotal

                                                        If (tableCount = 0 And autoImp = False) Then
                                                            Return "No Inventory Records to Import"
                                                        End If

                                                        RunActionSP("ImportInventory", "IE", {{"nothing"}})
                                                    Catch ex As Exception
                                                        insertErrorMessages("ManageDataHub", "procInventoryRecordsModal", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Return "Error"
                                                    End Try
                                                    Return "Proccessed"
                                                End Function)
    End Function

    Public Function selPickOTXMLData() As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Dim Datareader As SqlDataReader = Nothing
                                                             Dim Data As New List(Of Object)
                                                             Try

                                                                 Datareader = RunSPArray("selIEXferFieldMapOTXMLPick", "IE", {{"nothing"}})

                                                                 If Datareader.HasRows Then
                                                                     While Datareader.Read()
                                                                         Data.Add(New With {.ID = Datareader("XFM_ID"), .OTField = Datareader("Xfer Fieldname"), .XMLNode = Datareader("XMLNode"), .Field = Datareader("Field Type")})
                                                                     End While
                                                                 End If

                                                             Catch ex As Exception
                                                                 insertErrorMessages("ManageDataHub", "selPickOTXMLData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Data = New List(Of Object)
                                                                 Data.Add(New With {.ID = 0, .OTField = "Error", .XMLNode = "Error", .Field = "Error"})
                                                             Finally
                                                                 If Not IsNothing(Datareader) Then
                                                                     Datareader.Dispose()
                                                                 End If
                                                             End Try

                                                             Return Data
                                                         End Function)
    End Function

    Public Function delPickOTXMLData(ID As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("delXferFieldMap", "IE", {{"@ID", ID, intVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "delPickOTXMLData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function


    Public Function insPickOTXMLData(FieldName As String, Node As String, Type As String) As Task(Of Integer)
        Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                     Dim Datareader As SqlDataReader = Nothing
                                                     Dim RetID As Integer = -1
                                                     Try

                                                         Datareader = RunSPArray("insIEXferFieldMapOTXMLPick", "IE", {{"@FieldName", FieldName, strVar}, {"@Node", Node, strVar}, {"@Type", Type, strVar}})

                                                         If Datareader.HasRows Then
                                                             Datareader.Read()
                                                             RetID = Datareader("ID")
                                                         End If

                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "insPickOTXMLData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return RetID
                                                     Finally
                                                         If Not IsNothing(Datareader) Then
                                                             Datareader.Dispose()
                                                         End If
                                                     End Try

                                                     Return RetID
                                                 End Function)
    End Function

    Public Function updPickOTXMLData(ID As Integer, Node As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Try
                                                         RunActionSP("updIEXferFieldMapXMLNode", "IE", {{"@ID", ID, intVar}, {"@Node", Node, strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("ManageDataHub", "updPickOTXMLData", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

End Class
