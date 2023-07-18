' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Namespace ImportExport
    Public Class ImportExportHub
        Inherits Hub

        Public Function ExecLAPicks()
            AssignPicksSync(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
            Return True
        End Function

        Public Function ExecLAPuts()

            AssignPutAwaysSync(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
            Return True
        End Function

        Public Function ExecLACounts()
            AssignCountsSync(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
            Return True
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
                    insertErrorMessages("ImportExportHub", "SelectOTIDsbyOrderNumber", ex.Message, "", "")
                Finally
                    If Not IsNothing(Datareader) Then
                        Datareader.Dispose()
                    End If
                End Try
            Next
            Return IDs
        End Function

        Public Function GetStatus() As Dictionary(Of String, Object)
            Dim ret = StatusLogic.GetStatus(Context.QueryString.Get("WSID"))
            Return ret
        End Function

        Public Function GetLastImportExport()
            Dim ret = SystemSettingsLogic.GetLastImportExport(Context.QueryString.Get("WSID"))
            Return ret
        End Function

        Public Function GetUnallocatedTransactionsCount()
            Dim ret = AssignLocationsLogic.GetUnallocatedTransactionsCount(Context.QueryString.Get("WSID"))
            Return ret
        End Function

        Public Function GetPurgeTables()
            Dim ret = ArchivePurgeLogic.GetPurgeTables()
            Return ret
        End Function

        Public Function GetXferModifyFields()
            Dim ret = InvFieldsLogic.GetXferModifyFields()
            Return ret
        End Function

        Public Function UpdateSystemSettings(ByVal model As SystemSettingsModel)
            SystemSettingsLogic.UpdateSystemSettings(model)
            Return True
        End Function

        Public Function UpdateTransferSettings(ByVal model As TransferSettingsModel)
            TransferSettingsLogic.UpdateTransferSettings(model)
            Return True
        End Function

        Public Function UpdateAssignLocations(ByVal model As AssignLocationsModel)
            AssignLocationsLogic.UpdateAssignLocations(model, Context.QueryString.Get("WSID"))
            Return True
        End Function

        Public Function UpdateManageData(ByVal model As ManageDataModel)
            ManageDataLogic.UpdateManageData(model)
            Return True
        End Function

        Public Function UpdateArchivePurge(ByVal model As List(Of ArchivePurgeModel))
            ArchivePurgeLogic.UpdateArchivePurge(model)
            Return True
        End Function

        Public Function UpdateInvFields(ByVal model As List(Of InvFieldsModel))
            InvFieldsLogic.UpdateInvFields(model)
            Return True
        End Function

        Public Function UpdateFileBackup(ByVal model As FileBackupModel)
            Return Task(Of Object).Factory.StartNew(
                Function() As Object
                    FileBackupLogic.UpdateFileBackup(model)
                    Return True
                End Function)
        End Function

        Public Function UpdateFTP(ByVal model As FTPModel)
            FTPLogic.UpdateFTP(model)
            Return True
        End Function

        Public Function UpdateInventory(ByVal model As InventoryModel)
            InventoryLogic.UpdateInventory(model)
            Return True
        End Function

        ''' <summary>
        ''' Gets the field mappings
        ''' </summary>
        ''' <param name="TransType"></param>
        ''' <param name="XferType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetFieldMappings(ByVal TransType As String, ByVal XferType As String) As List(Of Dictionary(Of String, Object))
            Return GetResultMapList("selIEXferFieldMap", "IE", {{"@TransType", TransType, strVar}, {"@XferType", XferType, strVar}})
        End Function


        ''' <summary>
        ''' Inserts or updates a field mapping
        ''' </summary>
        ''' <param name="mapping"></param>
        ''' <param name="TransType"></param>
        ''' <param name="XferType"></param>
        ''' <param name="IsNew"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateFieldMappings(ByVal mapping As Dictionary(Of String, String), ByVal TransType As String, ByVal XferType As String, ByVal IsNew As Boolean)
            Try
                RunActionSP("insUpdXferFieldMapping", "IE", {{"@TransType", TransType, strVar}, {"@XferType", XferType, strVar},
                                                                {"@IsNew", CastAsSqlBool(IsNew), intVar}, {"@Fieldname", GlobalFunctions.NothingToStr(mapping("Xfer Fieldname")), strVar},
                                                                {"@SPosition", GlobalFunctions.NothingToStr(mapping("Start Position")), intVar}, {"@FieldLen", GlobalFunctions.NothingToStr(mapping("Field Length")), intVar},
                                                                {"@EPosition", GlobalFunctions.NothingToStr(mapping("End Position")), intVar}, {"@PadChar", GlobalFunctions.NothingToStr(mapping("Pad Character")), strVar},
                                                                {"@PadLeft", CastAsSqlBool(mapping("Pad From Left")), intVar}, {"@FieldType", GlobalFunctions.NothingToStr(mapping("Field Type")), strVar},
                                                                {"@Import", GlobalFunctions.NothingToStr(mapping("Import Format")), strVar}, {"@Export", GlobalFunctions.NothingToStr(mapping("Export Format")), strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "UpdateFieldMappings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Updates all matching maps to use the same settings as the current one
        ''' </summary>
        ''' <param name="transType"></param>
        ''' <param name="xferType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UpdateMapAll(transType As String, xferType As String)
            Try
                RunActionSP("updIEOtherFieldMaps", "IE", {{"@TransType", transType, strVar}, {"@XferType", xferType, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "UpdateMapAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Deletes a blank row from xfer Map definitions
        ''' </summary>
        ''' <param name="row"></param>
        ''' <param name="transType"></param>
        ''' <param name="xferType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function deleteXferMap(row As Dictionary(Of String, String), transType As String, xferType As String)
            Try
                Dim currFormat As String = IIf(xferType = "import", row("Import Format"), row("Export Format"))
                RunActionSP("delXferMapByID", "IE", {{"@TransType", transType, strVar},
                                                     {"@XferType", xferType, strVar},
                                                     {"@Start", row("Start Position"), intVar},
                                                     {"@End", row("End Position"), intVar},
                                                     {"@FieldLen", row("Field Length"), intVar},
                                                     {"@PadChar", row("Pad Character"), strVar},
                                                     {"@Format", currFormat, strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "deleteXferMap", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Gets the File Path Setup settings
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getXferFilePathSetup() As List(Of Dictionary(Of String, Object))

            Try
                Return GetResultMapList("selIEXferTransSettings", "IE")
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "getXferFilePathSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            End Try
            Return New List(Of Dictionary(Of String, Object))
        End Function

        ''' <summary>
        ''' Saves a file path setup row definition
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SaveFilePathSetup(ByVal row As Dictionary(Of String, String))
            Try
                RunActionSP("updIEXferFilePath", "IE", {{"@TransType", row("Transaction Type"), strVar},
                                                                {"@XferAppend", row("xfer Append"), strVar},
                                                                {"@ImportPath", row("Import File Path"), strVar},
                                                                {"@Active", CastAsSqlBool(row("Active")), intVar},
                                                                {"@ImportExt", row("File Extension Default"), strVar},
                                                                {"@HostType", row("Host Type"), strVar},
                                                                {"@ExportToFile", CastAsSqlBool(row("Export to File")), intVar},
                                                                {"@ExportPath", row("Export File Path"), strVar},
                                                                {"@ExportFile", row("Export Filename"), strVar},
                                                                {"@ExportExt", row("Export Extension"), strVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "SaveFilePathSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Verifies that there is a complete entry for the field mappings so that we can set the active variable
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function checkSetActiveFilePathSetup()
            Try
                Return New With {.success = True, .allowSet = GetResultSingleCol("selIECompleteHasMap", "IE")}
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "checkSetActiveFilePathSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            End Try
            Return New With {.success = False, .allowSet = False}
        End Function

        ''' <summary>
        ''' Gets the Inventory Map Export Schedule and the last export date/time
        ''' </summary>
        ''' <returns>Object with .rows = List(Dictionary(String, Object)), .last = datetime</returns>
        ''' <remarks></remarks>
        Public Function getIEInvMapExportConfig()
            Return New With {.rows = GetResultMapList("selIEInvMapExportConfig", "IE"), .last = GetResultSingleCol("selIELastInvMapExport", "IE")}
        End Function

        ''' <summary>
        ''' Saves the specified row in the map export schedule
        ''' </summary>
        ''' <param name="row"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function saveInvMapSchedule(row As Dictionary(Of String, String))
            Dim result As Integer = -1
            Try
                result = GetResultSingleCol("insUpdInvMapExportSchedule", "IE", {{"@Mon", CastAsSqlBool(row("Monday")), intVar},
                                                                 {"@Tue", CastAsSqlBool(row("Tuesday")), intVar},
                                                                 {"@Wed", CastAsSqlBool(row("Wednesday")), intVar},
                                                                 {"@Thu", CastAsSqlBool(row("Thursday")), intVar},
                                                                 {"@Fri", CastAsSqlBool(row("Friday")), intVar},
                                                                 {"@Sat", CastAsSqlBool(row("Saturday")), intVar},
                                                                 {"@Sun", CastAsSqlBool(row("Sunday")), intVar},
                                                                 {"@ExportHour", row("Export Hour"), strVar},
                                                                 {"@ExportMinute", row("Export Minute"), strVar},
                                                                 {"@AMPM", row("Export AMPM"), strVar},
                                                                 {"@EMS_ID", row("EMS_ID"), intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "saveInvMapSchedule", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
            End Try
            Return result
        End Function

        ''' <summary>
        ''' Deletes an entry from the Inventory Map Export Schedule definition
        ''' </summary>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function delIEInvMapSchedule(ID As Integer)
            Try
                RunActionSP("delIEInvMapSchedule", "IE", {{"@ID", ID, intVar}})
            Catch ex As Exception
                Debug.Print(ex.Message)
                insertErrorMessages("ImportExportHub", "delIEInvMapSchedule", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                Return False
            End Try
            Return True
        End Function

        ''' <summary>
        ''' For all records that have been in their current tables too long this procedure moves the records to the next table in the sequence and deletes them from their original table.
        ''' The procedure follows this order:
        ''' Working table -> History table -> Archive table -> Deleted permanently.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function PurgeHistory() As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(
                Function() As Boolean
                    Try
                        RunActionSP("PurgeFromIE", "IE", {{"nothing"}})
                    Catch ex As Exception
                        Debug.Print(ex.Message)
                        insertErrorMessages("ImportExportHub", "PurgeHistory", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                        Return False
                    End Try
                    Return True
                End Function)
        End Function

        Public Function delIETransactions(table As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(
                Function() As Boolean
                    Return ImportExport.IETransactionsLogic.DeleteTransactions(table, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                End Function)
        End Function
    End Class
End Namespace
