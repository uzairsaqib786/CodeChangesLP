' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports System.IO

Namespace Admin
    Public Class CycleCountHub
        Inherits Hub

        ''' <summary>
        ''' Gets all rows that matched the Filter, to be used to create Cycle count Records
        ''' </summary>
        ''' <param name="QueryData">Object encapsulation of all Possible Queries</param>
        ''' <returns>An object that contains the rows that macthed the given querydata input</returns>
        ''' <remarks></remarks>
        Public Function GetBatchResultTable(QueryData As BatchResultQuery) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Return CycleCount.BatchResultTable(QueryData, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function

        ''' <summary>
        ''' Inserts Inv Map Record into ccQueue Table
        ''' </summary>
        ''' <param name="InvMapIDs">A list of invenotry map ids that are going to be inserted</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function InsertccQueue(InvMapIDs As List(Of String)) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = "Done"
                                                        Dim WSID As String = Context.QueryString.Get("WSID")
                                                        For Each InvMapID In InvMapIDs
                                                            Try
                                                                RunActionSP("insccQueue", WSID, {{"@InvMapID", InvMapID, intVar}, {"@WSID", WSID, strVar}})
                                                            Catch ex As Exception
                                                                returnMessage = "One or More records failed to be moved into the Queue, check the error log for more information"
                                                                insertErrorMessages("Cycle Count", "InsertccQueue", ex.Message, Context.User.Identity.Name, WSID)
                                                                RemoveccQueueAll()
                                                            End Try
                                                        Next
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Removes Individual records from the ccQueue Table
        ''' </summary>
        ''' <param name="InvMapID">InvMapID of record to remove</param>
        ''' <returns>A string telling of the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function RemoveccQueueRow(InvMapID As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = "Done"
                                                        Dim WSID As String = Context.QueryString.Get("WSID")
                                                        Try
                                                            RunActionSP("delccQueueInvMapID", WSID, {{"@InvMapID", InvMapID, intVar}, {"@WSID", WSID, strVar}})
                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "RemoveccQueueRow", ex.Message, Context.User.Identity.Name, WSID)
                                                        End Try
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Removes all records from ccQueue
        ''' </summary>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function RemoveccQueueAll() As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = "Done"
                                                        Try
                                                            RunActionSP("delccQueue", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "RemoveccQueueAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Try
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Creates count transactions for the records in the ccQueue
        ''' </summary>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function CreateCountRecords() As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = "Done"
                                                        Dim batchID = GlobalFunctions.getNextBatchID(Context.QueryString.Get("WSID"), Context.User.Identity.Name)

                                                        Try
                                                            batchID = batchID + "cc"
                                                            'Insert new records into Open Transactions
                                                            RunActionSP("insOTccQueue", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                          {"@User", Context.User.Identity.Name, strVar},
                                                                                                                          {"@BatchID", batchID, strVar}})
                                                            'Update Master record IDs for Open Transactions
                                                            RunActionSP("updOTMastRecID", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                            'Deletes all records from ccQueue that Count Trans. were just created for
                                                            RunActionSP("delccQueue", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "CreateCountRecords", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Try
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Deletes count order for the desired order number
        ''' </summary>
        ''' <param name="ordernumber">The order number to be deleted</param>
        ''' <param name="Ident">Tells whetehr to delete all transactions for this order number or just ones that are not completed</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function DeleteCountOrders(ordernumber As String, Ident As Integer) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success As String = "Ran"

                                                        Try
                                                            RunActionSP("delOTCountOrderNum", Context.QueryString.Get("WSID"), {{"@OrderNum", ordernumber, strVar}, _
                                                                                                                                 {"@Ident", Ident, strVar}, _
                                                                                                                                 {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                                                 {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        Catch ex As Exception
                                                            success = "Error"
                                                        End Try
                                                        Return success
                                                    End Function)
        End Function

        ''' <summary>
        ''' Gets the dataset loaded for printing the count detal report for the given order number
        ''' </summary>
        ''' <param name="ordernumber">The ordernumber whose deatils are going to be printed</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function PrintCycleCountDetailReport(ordernumber As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim Success As String = "Printed"
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim dataSet As New DataSet()
                                                        Try
                                                            DataReader = RunSPArray("selOTCountOrderNumLL", Context.QueryString.Get("WSID"), {{"@OrderNum", ordernumber, strVar}})

                                                            dataSet.Load(DataReader, LoadOption.PreserveChanges, {"Data"})
                                                        Catch ex As Exception
                                                            insertErrorMessages("Cycle Count Hub", "PrintCycleCountDetailReport", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            Success = "Fail"
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Close()
                                                            End If
                                                        End Try

                                                        Return Success
                                                    End Function)
        End Function

        ''' <summary>
        ''' Get the number of Locations for the current Item number and warehouse combitnation
        ''' </summary>
        ''' <param name="itemNum">The selected item number</param>
        ''' <param name="wareHouse">The selected warehouse</param>
        ''' <returns>A string that tells the number of locations</returns>
        ''' <remarks></remarks>
        Public Function getCycleCountQty(itemNum As String, wareHouse As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim countNum As String = ""
                                                        Try
                                                            DataReader = RunSPArray("selInvMapCycleQty", Context.QueryString.Get("WSID"), {{"@itemNum", itemNum, strVar}, _
                                                                                                                                            {"@wareHouse", wareHouse, strVar}})
                                                            If (DataReader.HasRows) Then
                                                                While (DataReader.Read())
                                                                    countNum = CheckDBNull(DataReader(0))
                                                                End While
                                                            End If
                                                        Catch ex As Exception

                                                            insertErrorMessages("Cycle Count Hub", "getCycleCountQty", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            countNum = "Error"
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Close()
                                                            End If

                                                        End Try
                                                        Return countNum
                                                    End Function)
        End Function

        ''' <summary>
        ''' Deletes any discrepancies
        ''' </summary>
        ''' <returns>String telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteDiscrepancies() As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = ""
                                                        Try
                                                            RunActionSP("delCCDiscrepant", Context.QueryString.Get("WSID"), {{"@Username", Context.User.Identity.Name, strVar}, _
                                                                                                                             {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "delCCDiscrepant", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))

                                                        End Try
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Inserts count transactions into OT and clears them from the discrepant table
        ''' </summary>
        ''' <param name="ID">A lits of the count ids to be inserted</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function cycleCountCreateTrans(ID As List(Of Integer)) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = ""
                                                        Try
                                                            For x As Integer = 0 To ID.Count - 1
                                                                RunActionSP("CycleCountCreateTrans", Context.QueryString.Get("WSID"), {{"@ID", ID(x), intVar},
                                                                                                                                       {"@Username", Context.User.Identity.Name, strVar}})
                                                            Next

                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "cycleCountCreateTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Try
                                                        Return returnMessage
                                                    End Function)

        End Function

        ''' <summary>
        ''' Gets the number of cycle count records
        ''' </summary>
        ''' <returns>An integer telling the number of records</returns>
        Public Function checkCountQueue() As Task(Of Integer)
            Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                         Dim count As Integer
                                                         count = GetResultSingleCol("selccQueueGetNumRecords", Context.QueryString.Get("WSID"), {{"@wsid", Context.QueryString.Get("WSID"), strVar}})
                                                         Return count
                                                     End Function)
        End Function

        ''' <summary>
        ''' Clears the cycle count queue
        ''' </summary>
        ''' <returns>None, task of sub</returns>
        Public Function clearCountQueue() As Task
            Return Task.Factory.StartNew(Sub()
                                             GetResultSingleCol("updateccQueueClearRecords", Context.QueryString.Get("WSID"), {{"@wsid", Context.QueryString.Get("WSID"), strVar}})
                                         End Sub)
        End Function

        ''' <summary>
        ''' Updates the settings for reading in the audit file
        ''' </summary>
        ''' <param name="wareHouseStart">The character number where the warehouse field starts</param>
        ''' <param name="warehouseLength">The length of the warehouse field</param>
        ''' <param name="warehouseEnd">The character number where the warehouse field ends</param>
        ''' <param name="warehousePadLeft">If the warehouse field is padded</param>
        ''' <param name="warehouseImpFormat">The format when it is being imported</param>
        ''' <param name="expdateStart">The character number where the expiration date field starts</param>
        ''' <param name="expdateLength">The length of the expiration date field</param>
        ''' <param name="expdateEnd">The end character of the the expiration date field</param>
        ''' <param name="expdatePadLeft">If the expiration date field is padded</param>
        ''' <param name="expdateImpFormat">The format when this field gets imnported</param>
        ''' <param name="itemnumStart">The character number where the item number field starts</param>
        ''' <param name="itemnumLength">The length of the item number field</param>
        ''' <param name="itemnumEnd">The character number where the item number field ends</param>
        ''' <param name="itemnumPadLeft">If the item number field is padded</param>
        ''' <param name="itemnumImpFormat">The format that the item is imported in</param>
        ''' <param name="lotnumStart">The character number where the lot number field starts</param>
        ''' <param name="lotnumLength">The length of the lot number field</param>
        ''' <param name="lotnumEnd">The character number where the lot number field ends</param>
        ''' <param name="lotnumPadLeft">If the lot number field is padded</param>
        ''' <param name="lotnumImpFormat">The format that the lot number is imported in</param>
        ''' <param name="sernumStart">The character number where the serial number starts</param>
        ''' <param name="sernumLength">The length of the of serial number field</param>
        ''' <param name="sernumEnd">The charatcer number where the serial number field ends</param>
        ''' <param name="sernumPadLeft">If the serial number field is padded</param>
        ''' <param name="sernumImpFormat">The format that the serial number field is imported in</param>
        ''' <param name="hostQStart">The character number where the host quantity field starts</param>
        ''' <param name="hostQLength">The length of the host quantity field</param>
        ''' <param name="hostQEnd">The end character of the host quantity field</param>
        ''' <param name="hostQPadLeft">If the host quantity field is padded</param>
        ''' <param name="hostQImpFormat">The format the host quantiy field is imported in</param>
        ''' <param name="importPath">The location of the audit file</param>
        ''' <param name="fileDefault">The default file extension for the file</param>
        ''' <param name="active">If this file location is still active</param>
        ''' <param name="backup">The path to backup the file in</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateFieldMapModel(wareHouseStart As Integer, warehouseLength As Integer, warehouseEnd As Integer, warehousePadLeft As Boolean, warehouseImpFormat As String, _
                                            expdateStart As Integer, expdateLength As Integer, expdateEnd As Integer, expdatePadLeft As Boolean, expdateImpFormat As String, _
                                            itemnumStart As Integer, itemnumLength As Integer, itemnumEnd As Integer, itemnumPadLeft As Boolean, itemnumImpFormat As String, _
                                            lotnumStart As Integer, lotnumLength As Integer, lotnumEnd As Integer, lotnumPadLeft As Boolean, lotnumImpFormat As String, _
                                            sernumStart As Integer, sernumLength As Integer, sernumEnd As Integer, sernumPadLeft As Boolean, sernumImpFormat As String, _
                                            hostQStart As Integer, hostQLength As Integer, hostQEnd As Integer, hostQPadLeft As Boolean, hostQImpFormat As String, _
                                            importPath As String, fileDefault As String, active As Boolean, backup As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Dim returnMessage = ""

                                                        Try



                                                            RunActionSP("updateInfoFieldMapModel", Context.QueryString.Get("WSID"), {{"@WareHouseStart", wareHouseStart, intVar}, {"@WareHouseLength", warehouseLength, intVar}, _
                                                                                                                                     {"@WareHouseEnd", warehouseEnd, intVar}, {"@WareHousePadLeft", warehousePadLeft, boolVar}, _
                                                                                                                                     {"@WareHouseImpFormat", warehouseImpFormat, strVar}, {"@ExpDateStart", expdateStart, intVar}, _
                                                                                                                                     {"@ExpDateLength", expdateLength, intVar}, {"@ExpDateEnd", expdateEnd, intVar}, _
                                                                                                                                     {"@ExpDatePadLeft", expdatePadLeft, boolVar}, {"@ExpDateImpFormat", expdateImpFormat, strVar}, _
                                                                                                                                     {"@ItemNumStart", itemnumStart, intVar}, {"@ItemNumLength", itemnumLength, intVar}, _
                                                                                                                                     {"@ItemNumEnd", itemnumEnd, intVar}, {"@ItemNumPadLeft", itemnumPadLeft, boolVar}, _
                                                                                                                                     {"@ItemNumImpFormat", itemnumImpFormat, strVar}, {"@LotNumStart", lotnumStart, intVar}, _
                                                                                                                                     {"@LotNumLength", lotnumLength, intVar}, {"@LotNumEnd", lotnumEnd, intVar}, _
                                                                                                                                     {"@LotNumPadLeft", lotnumPadLeft, boolVar}, {"@LotNumImpFormat", lotnumImpFormat, strVar}, _
                                                                                                                                     {"@SerNumStart", sernumStart, intVar}, {"@SerNumLength", sernumLength, intVar}, _
                                                                                                                                     {"@SerNumEnd", sernumEnd, intVar}, {"@SerNumPadLeft", sernumPadLeft, boolVar}, _
                                                                                                                                     {"@SerNumImpFormat", sernumImpFormat, strVar}, {"@HostQStart", hostQStart, intVar}, _
                                                                                                                                     {"@HostQLength", hostQLength, intVar}, {"@HostQEnd", hostQEnd, intVar}, _
                                                                                                                                     {"@HostQPadLeft", hostQPadLeft, boolVar}, {"@HostQImpFormat", hostQImpFormat, strVar}, _
                                                                                                                                     {"@ImportPath", importPath, strVar}, {"@FileDefault", fileDefault, strVar}, _
                                                                                                                                     {"@Active", active, boolVar}, {"@Backup", backup, strVar}})
                                                        Catch ex As Exception
                                                            returnMessage = "Error"
                                                            insertErrorMessages("Cycle Count", "updateFieldMapModel", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Try
                                                        Return returnMessage
                                                    End Function)
        End Function

        ''' <summary>
        ''' Will read from the file location and create discrepancies for each line within the file
        ''' </summary>
        ''' <returns>A string telling if the oepration completed successfully</returns>
        ''' <remarks></remarks>
        Public Function importHostQtyFile() As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = "success"
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim ImportSettings As New Dictionary(Of String, ccSetting)
                                                        Dim ImportFileDirectory As DirectoryInfo = Nothing
                                                        Dim Extension As String = ""
                                                        Dim Backup As String = ""
                                                        Dim index As Integer = 0

                                                        Try
                                                            DataReader = RunSPArray("selXferFeildMapAudit", Context.QueryString.Get("WSID"), {{"nothing"}})
                                                            'Get thesettings fro each field
                                                            While DataReader.HasRows
                                                                While DataReader.Read()
                                                                    If index = 0 Then
                                                                        ImportSettings.Add(DataReader("xfer Fieldname"), _
                                                                                           New ccSetting(DataReader("Start Position"), DataReader("Field Length")))
                                                                    ElseIf index = 1 Then
                                                                        Extension = DataReader("File Extension Default")
                                                                        ImportFileDirectory = New DirectoryInfo(DataReader("Import File Path"))
                                                                    Else
                                                                        Backup = CheckDBNull(DataReader(0))
                                                                    End If
                                                                End While
                                                                index += 1
                                                                DataReader.NextResult()
                                                            End While

                                                            Dim AuditFiles() = ImportFileDirectory.GetFiles("*." & Extension)

                                                            If AuditFiles.Length > 0 Then
                                                                'delete from the dicrepancy table
                                                                RunActionSP("delCCDiscrepant", Context.QueryString.Get("WSID"), {{"@Username", Context.User.Identity.Name, strVar}, _
                                                                                                                             {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                                For Each CurrentFile In AuditFiles
                                                                    If Not Backup = "" Then
                                                                        CurrentFile.CopyTo(Backup & "\" & CurrentFile.ToString())
                                                                    End If
                                                                    For Each line As String In File.ReadLines(CurrentFile.FullName)
                                                                        'create nwe object with datat from read line in the file
                                                                        Dim ccItem As Object = New With { _
                                                                            .ItemNumber = Trim(line.Substring(ImportSettings("Item Number").StartIndex - 1, ImportSettings("Item Number").LenIndex)), _
                                                                            .HostQuantity = Trim(line.Substring(ImportSettings("Host Quantity").StartIndex - 1, ImportSettings("Host Quantity").LenIndex)), _
                                                                            .Warehouse = Trim(line.Substring(ImportSettings("Warehouse").StartIndex - 1, ImportSettings("Warehouse").LenIndex)), _
                                                                            .SerialNumber = Trim(line.Substring(ImportSettings("Serial Number").StartIndex - 1, ImportSettings("Serial Number").LenIndex)), _
                                                                            .LotNumber = Trim(line.Substring(ImportSettings("Lot Number").StartIndex - 1, ImportSettings("Lot Number").LenIndex)), _
                                                                            .ExpirationDate = Trim(line.Substring(ImportSettings("Expiration Date").StartIndex - 1, ImportSettings("Expiration Date").LenIndex)) _
                                                                            }
                                                                        'update discrepancy table
                                                                        RunActionSP("updCcDiscFromFile", Context.QueryString.Get("WSID"), {{"@ItemNum", ccItem.ItemNumber, strVar}, _
                                                                                                                                           {"@HostQty", ccItem.HostQuantity, intVar}, _
                                                                                                                                           {"@Warehouse", ccItem.Warehouse, strVar}, _
                                                                                                                                           {"@SerialNum", ccItem.SerialNumber, strVar}, _
                                                                                                                                           {"@LotNum", ccItem.LotNumber, strVar}, _
                                                                                                                                           {"@ExpirationDate", CDate(ccItem.ExpirationDate), dteVar}})
                                                                    Next
                                                                    File.Delete(CurrentFile.FullName)
                                                                Next
                                                            Else
                                                                success = "File does not exist"
                                                                Return success
                                                            End If




                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("Cycle Count", "importHostQtyFile", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Error has occured"
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try

                                                        Return success
                                                    End Function)
        End Function

    End Class

End Namespace
