' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Namespace Admin
    Public Class PreferencesHub
        Inherits Hub

        ''' <summary>
        ''' Overrides what happens each time a client has connected, in this case creates a group for each POD, and for Hardware Controls.  Adds calling user to a group.
        ''' </summary>
        ''' <returns>Task so that timeouts aren't as much of an issue.</returns>
        ''' <remarks></remarks>
        Public Overrides Function OnConnected() As Task

            'Adds a user to their own unique group by a value passed in during connection
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("currentUser"))
            Groups.Add(Context.ConnectionId, Context.QueryString.Get("ConnectionName"))
            'Calls the Original Onconnected Function pass control back to the server
            Return MyBase.OnConnected()
        End Function

        ''' <summary>
        ''' Gets the Alias field names
        ''' </summary>
        ''' <returns>An AliasModel that contains all the different titles</returns>
        ''' <remarks></remarks>
        Public Function getFieldNames() As Task(Of AliasModel)
            Return Task(Of AliasModel).Factory.StartNew(Function() As AliasModel
                                                            Return New AliasModel(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        End Function)
        End Function

        ''' <summary>
        ''' Adds or updates a connection string
        ''' </summary>
        ''' <param name="newConnection">The new connection string</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function changeConnectionString(newConnection As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Config.addupdateWSConnectionString(Context.QueryString.Get("WSID"), newConnection)
                                         End Sub)
        End Function

        ''' <summary>
        ''' Saves Alias field names
        ''' </summary>
        ''' <param name="itemAlias">Item Number alias</param>
        ''' <param name="uomAlias">Unit of Measure alias</param>
        ''' <param name="ufs">User Field1-10 aliases</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveFieldNames(itemAlias As String, uomAlias As String, ufs As List(Of String)) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateColumnAliases", Context.QueryString.Get("WSID"), {{"@ItemNumber", itemAlias, strVar}, _
                                                                                     {"@UoM", uomAlias, strVar}, _
                                                                                     {"@UF1", ufs(0), strVar}, _
                                                                                     {"@UF2", ufs(1), strVar}, _
                                                                                     {"@UF3", ufs(2), strVar}, _
                                                                                     {"@UF4", ufs(3), strVar}, _
                                                                                     {"@UF5", ufs(4), strVar}, _
                                                                                     {"@UF6", ufs(5), strVar}, _
                                                                                     {"@UF7", ufs(6), strVar}, _
                                                                                     {"@UF8", ufs(7), strVar}, _
                                                                                     {"@UF9", ufs(8), strVar}, _
                                                                                     {"@UF10", ufs(9), strVar}, _
                                                                                     {"@User", Context.User.Identity.Name, strVar}, _
                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveFieldNames", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets the user field suggestions for uf
        ''' </summary>
        ''' <param name="uf">1 or 2 for suggestions of that user field</param>
        ''' <returns>A list of string containing the suggesstions for the given user field</returns>
        ''' <remarks></remarks>
        Public Function getUserFieldLookupList(uf As Integer) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim lookup As New List(Of String)
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Try
                                                                     datareader = RunSPArray("selUFLookup", Context.QueryString.Get("WSID"), {{"@UF", uf, intVar}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             lookup.Add(IIf(IsDBNull(datareader(0)), "", datareader(0)))
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getUserFieldLookupList", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return lookup
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves a new or updated user field1/2 lookup entry
        ''' </summary>
        ''' <param name="oldValue">Value to update</param>
        ''' <param name="newValue">New value to save</param>
        ''' <param name="uf">1 or 2 for UF1/UF2</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveUFLookupEntry(oldValue As String, newValue As String, uf As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsUFLookup", Context.QueryString.Get("WSID"), {{"@OldValue", oldValue, strVar}, {"@NewValue", newValue, strVar}, {"@UF", uf, intVar}, _
                                                                                   {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveUFLookupEntry", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes the specified value from the user fields lookup list specified
        ''' </summary>
        ''' <param name="value">Value to delete</param>
        ''' <param name="uf">Lookup list to delete from 1/2</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteUFEntry(value As String, uf As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delUFLookupEntry", Context.QueryString.Get("WSID"), {{"@Value", value, strVar}, {"@UF", uf, intVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                                                  {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteUFEntry", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets Adjustment reasons
        ''' </summary>
        ''' <returns>A list of string containing the adjustment reasons</returns>
        ''' <remarks></remarks>
        Public Function getAdjustmentLookup() As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim lookup As New List(Of String)
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Try
                                                                     datareader = RunSPArray("selAdjustmentLookup", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             If Not IsDBNull(datareader(0)) Then
                                                                                 lookup.Add(datareader(0))
                                                                             End If
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getAdjustmentLookup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return lookup
                                                             End Function)
        End Function

        ''' <summary>
        ''' Deletes an adjustment reason
        ''' </summary>
        ''' <param name="reason">Reason to delete</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteAdjustReasonEntry(reason As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delAdjustmentReason", Context.QueryString.Get("WSID"), {{"@Reason", reason, strVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteAdjustReasonEntry", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Saves an adjustment reason
        ''' </summary>
        ''' <param name="oldValue">Value to update</param>
        ''' <param name="newValue">Value to save</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveAdjustmentReasonEntry(oldValue As String, newValue As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsAdjustmentReason", Context.QueryString.Get("WSID"), {{"@OldValue", oldValue, strVar}, {"@NewValue", newValue, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveAdjustmentReasonEntry", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets the pre-existing totes entered into preferences
        ''' </summary>
        ''' <returns>A list of object containing the tote id and cell for the tote id</returns>
        ''' <remarks></remarks>
        Public Function getToteSetup() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                                 Dim totes As New List(Of Object)
                                                                 Dim datareader As SqlDataReader = Nothing

                                                                 Try
                                                                     datareader = RunSPArray("selExistingTotes", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             totes.Add(New With {.ToteID = IIf(IsDBNull(datareader(0)), "", datareader(0)), _
                                                                                              .Cells = IIf(IsDBNull(datareader(1)), 0, datareader(1))})
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getToteSetup", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return totes
                                                             End Function)
        End Function

        ''' <summary>
        ''' Deletes a tote
        ''' </summary>
        ''' <param name="ToteID">Tote to delete</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteToteEntry(ToteID As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delExistingTote", Context.QueryString.Get("WSID"), {{"@ToteID", ToteID, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                 {"@User", Context.User.Identity.Name, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteToteEntry", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Saves a Tote ID and Cell
        ''' </summary>
        ''' <param name="OldToteID">Old value to update</param>
        ''' <param name="ToteID">New value to save</param>
        ''' <param name="Cell">Cells in the tote</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveTote(OldToteID As String, ToteID As String, Cell As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsTote", Context.QueryString.Get("WSID"), {{"@OldToteID", OldToteID, strVar}, _
                                                                               {"@ToteID", ToteID, strVar}, _
                                                                               {"@Cell", Cell, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveTote", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets bulk handheld settings
        ''' </summary>
        ''' <returns>A list of boolean telling the value for all the bulk settings</returns>
        ''' <remarks></remarks>
        Public Function getBulkSettings() As Task(Of List(Of Boolean))
            Return Task(Of List(Of Boolean)).Factory.StartNew(Function() As List(Of Boolean)
                                                                  Dim settings As New List(Of Boolean)
                                                                  Dim datareader As SqlDataReader = Nothing

                                                                  Try
                                                                      datareader = RunSPArray("selHandheldSettings", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                      If datareader.HasRows Then
                                                                          If datareader.Read() Then
                                                                              For x As Integer = 0 To datareader.FieldCount - 1
                                                                                  'Use this line to make sure you are setting the BulkSettings in Javascript correctly.
                                                                                  'Debug.WriteLine(x.ToString + ": " + datareader.GetName(x)
                                                                                  If ((IsDBNull(datareader(x)) = 0 Or IsDBNull(datareader(x)) = 1) And (datareader(x).GetType = GetType(Boolean))) Then
                                                                                      settings.Add(IIf(IsDBNull(datareader(x)), False, CBool(datareader(x))))
                                                                                  End If
                                                                              Next
                                                                          End If
                                                                      End If
                                                                  Catch ex As Exception
                                                                      Debug.WriteLine(ex.Message)
                                                                      insertErrorMessages("PreferencesHub", "getBulkSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                  End Try

                                                                  Return settings
                                                              End Function)
        End Function

        ''' <summary>
        ''' Saves bulk handheld settings, called from BulkHandheldSettings.js
        ''' </summary>
        ''' <param name="settings">All the currently set values for the bulk  handheld settings</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveBulkSettings(settings As List(Of Boolean)) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim intSettings As New List(Of Integer)
                                             For x As Integer = 0 To settings.Count - 1
                                                 intSettings.Add(IIf(settings(x), 1, 0))
                                             Next

                                             Try
                                                 RunActionSP("updateHandheldSettingsNew2", Context.QueryString.Get("WSID"),
                                                                                        {{"@PSVLocation", intSettings(0), intVar},
                                                                                        {"@Allow", intSettings(1), intVar},
                                                                                        {"@PSVItem", intSettings(2), intVar},
                                                                                        {"@POrderManifest", intSettings(3), intVar},
                                                                                        {"@PSVTote", intSettings(4), intVar},
                                                                                        {"@PTotemanifest", intSettings(5), intVar},
                                                                                        {"@ShowCount", intSettings(6), intVar},
                                                                                        {"@Demand", intSettings(7), intVar},
                                                                                        {"@CSVLocation", intSettings(8), intVar},
                                                                                        {"@Dynamic", intSettings(9), intVar},
                                                                                        {"@CSVItem", intSettings(10), intVar},
                                                                                        {"@ShortPicks", intSettings(11), intVar},
                                                                                        {"@PtSVLocation", intSettings(12), intVar},
                                                                                        {"@PtSVItem", intSettings(13), intVar},
                                                                                        {"@CountShort", intSettings(14), intVar},
                                                                                        {"@PPickLabels", intSettings(15), intVar},
                                                                                        {"@PPutLabels", intSettings(16), intVar},
                                                                                        {"@SAP", intSettings(17), intVar},
                                                                                        {"@HPARequest", intSettings(18), intVar},
                                                                                        {"@DynamicWH", intSettings(19), intVar},
                                                                                        {"@DirectedHPA", intSettings(20), intVar},
                                                                                        {"@AllowHP", intSettings(21), intVar},
                                                                                        {"@PickSequenceSort", intSettings(22), intVar},
                                                                                        {"@PrintNextToteLabel", intSettings(23), intVar},
                                                                                        {"@ContainerLogic", intSettings(24), intVar},
                                                                                        {"@PutAwayBatch", intSettings(25), intVar},
                                                                                        {"@UseF2TaskComp", intSettings(26), intVar},
                                                                                        {"@ChangeInvMastCellSizeonHPA", intSettings(27), intVar},
                                                                                        {"@HPAShowEmptyLocs", intSettings(28), intVar},
                                                                                        {"@CountIfLocEmptied", intSettings(29), intVar},
                                                                                        {"@CombineSameIMIDPick", intSettings(30), intVar},
                                                                                        {"@CarouselPutAway", intSettings(31), intVar},
                                                                                        {"@ConsolidationDelivery", intSettings(32), intVar},
                                                                                        {"@ShortPickFNL", intSettings(33), intVar},
                                                                                        {"@zoneChoice", intSettings(34), intVar},
                                                                                        {"@consolidationStaging", intSettings(35), intVar},
                                                                                        {"@useInventoryCaseQuantity", intSettings(36), intVar},
                                                                                        {"@combineOTTrans", intSettings(37), intVar},
                                                                                        {"@allowSuperBatch", intSettings(38), intVar},
                                                                                        {"@singleLineSuperBatch", intSettings(39), intVar},
                                                                                        {"@HPAShowVelocityCode", intSettings(40), intVar},
                                                                                        {"@pickScanVerifyQuantity", intSettings(41), intVar},
                                                                                        {"@hotMoveMultipleItems", intSettings(42), intVar},
                                                                                                                         {"@User", Context.User.Identity.Name, strVar},
                                                                                                                         {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveBulkSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
            '{"@LocationDelimiter", intSettings(35), intVar},
            '{"@CustomerCode", intSettings(23), intVar},
            '{"@CustomPick", intSettings(26), intVar},
        End Function


        ''' <summary>
        ''' Gets the company/general info tab information
        ''' </summary>
        ''' <returns>A list of string containing the company info</returns>
        ''' <remarks></remarks>
        Public Function getCompanyInfo() As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim companyInfo As New List(Of String)
                                                                 Try
                                                                     datareader = RunSPArray("selCompanyInfo", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                     If datareader.HasRows Then
                                                                         If datareader.Read() Then
                                                                             For x As Integer = 0 To datareader.FieldCount - 1
                                                                                 companyInfo.Add(IIf(IsDBNull(datareader(x)), "", datareader(x).ToString()))
                                                                             Next
                                                                         End If
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getCompanyInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 End Try
                                                                 Return companyInfo
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves company info/general info tab on system preferences
        ''' </summary>
        ''' <param name="prefs">A list of string containing the new preference values</param>
        ''' <param name="panel">Tells which settings are being saved</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveGeneralPreferences(prefs As List(Of String), panel As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim params As String(,) = Nothing
                                             Try
                                                 Select Case panel
                                                     Case 1
                                                         params = {{"@CompanyName", prefs(0), strVar}, {"@Address", prefs(1), strVar}, {"@City", prefs(2), strVar}, {"@State", prefs(3), strVar},
                                                                    {"@EarlyBreakTime", prefs(4), strVar}, {"@EarlyBreakDuration", If(prefs(5) = "", 0, prefs(5)), intVar},
                                                                    {"@MidBreakTime", prefs(6), strVar}, {"@MidBreakDuration", If(prefs(7) = "", 0, prefs(7)), intVar},
                                                                    {"@LateBreakTime", prefs(8), strVar}, {"@LateBreakDuration", If(prefs(9) = "", 0, prefs(9)), intVar},
                                                                   {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                     Case 2
                                                         params = {{"@Domain", CastAsSqlBool(prefs(0)), intVar}, {"@NTLM", CastAsSqlBool(prefs(1)), intVar}, _
                                                                   {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                     Case 3
                                                         params = {{"@OrderManifest", CastAsSqlBool(prefs(0)), intVar}, {"@FIFO", CastAsSqlBool(prefs(1)), intVar},
                                                                   {"@CheckTote", CastAsSqlBool(prefs(2)), intVar}, {"@Replenish", CastAsSqlBool(prefs(3)), intVar},
                                                                   {"@PickLabels", CastAsSqlBool(prefs(4)), intVar}, {"@ShortPickFind", CastAsSqlBool(prefs(5)), intVar},
                                                                   {"@ZeroLocationQty", CastAsSqlBool(prefs(6)), intVar}, {"@RequestPutAwayLabels", CastAsSqlBool(prefs(7)), intVar},
                                                                   {"@CarouselBatchID", CastAsSqlBool(prefs(8)), intVar}, {"@BulkBatchID", CastAsSqlBool(prefs(9)), intVar},
                                                                   {"@DynReelWIP", CastAsSqlBool(prefs(10)), intVar}, {"@Reel", prefs(11), strVar},
                                                                   {"@Multi", CastAsSqlBool(prefs(12)), intVar}, {"@ConfirmInv", CastAsSqlBool(prefs(13)), intVar},
                                                                   {"@OtTempToOtPending", CastAsSqlBool(prefs(19)), intVar}, {"@ShowQty", prefs(14), strVar}, {"@Tote", prefs(15), intVar},
                                                                   {"@Serial", prefs(16), intVar}, {"@MaxPutAway", prefs(17), intVar}, {"@PickType", prefs(18), strVar},
                                                                   {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                   {"@DistinctKitOrders", CastAsSqlBool(prefs(20)), intVar}, {"@PrintReplenPutLabels", CastAsSqlBool(prefs(21)), intVar},
                                                                   {"@GenTransQuarantine", CastAsSqlBool(prefs(22)), intVar}}
                                                     Case 4
                                                         params = {{"@OrderSort", prefs(0), strVar}, {"@CartonDisplay", prefs(1), strVar}, {"@DisplayImage", CastAsSqlBool(prefs(2)), intVar}, _
                                                                   {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                                 End Select
                                                 RunActionSP("updateCompanyInfo" & panel, Context.QueryString.Get("WSID"), params)
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveGeneralPreferences", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
        ''' <summary>
        ''' Selects the information for the three sections (Workstation Settings, Tote Managment, and Location Assignment Functions) for the current workstation id
        ''' </summary>
        ''' <returns>An object of objects with the initial set of objects beging each section and the other being the values of each section</returns>
        ''' <remarks></remarks>
        Public Function selectWorkstationSetupInfo() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Return Preferences.getWorkstationSetup(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                    End Function)
        End Function

        ''' <summary>
        ''' Selects the pod settings based in the workstation id
        ''' </summary>
        ''' <returns>An object containg all settings</returns>
        ''' <remarks></remarks>
        Public Function selPodSetupInfo() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim Index As Integer = 0
                                                        Dim CarSwPath As String = ""
                                                        Dim SpinSort As String = ""
                                                        Dim CFSeq As String = ""
                                                        Dim CIBDelay As Integer = -1
                                                        Dim Position20 As String = ""
                                                        Dim IgnoreTC As String = ""
                                                        Dim ItemOnLT As String = ""
                                                        Dim PullWhenFull As String = ""


                                                        Try
                                                            DataReader = RunSPArray("PrefSelPODPrefs", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                            While DataReader.HasRows
                                                                While DataReader.Read()
                                                                    For x As Integer = 0 To DataReader.FieldCount - 1
                                                                        If Index = 0 Then
                                                                            CarSwPath = IIf(Not IsDBNull(DataReader(0)), DataReader(0), "")
                                                                            SpinSort = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            CFSeq = IIf(Not IsDBNull(DataReader(2)), DataReader(2), "")
                                                                            CIBDelay = IIf(Not IsDBNull(DataReader(3)), DataReader(3), -1)
                                                                        ElseIf Index = 1 Then
                                                                            If DataReader(0) = "20 Position" Then
                                                                                Position20 = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "IgnoreTC" Then
                                                                                IgnoreTC = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "Item On LT" Then
                                                                                ItemOnLT = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "Pull When Full" Then
                                                                                PullWhenFull = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End While
                                                                Index += 1
                                                                DataReader.NextResult()
                                                            End While
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("PreferencesHub", "selPodSetupInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try

                                                        Dim PodInfo As Object = New With {.spinsort = SpinSort, .cfseq = CFSeq, .carsw = CarSwPath, .pos20 = Position20, .onlt = ItemOnLT, .pullwhenfull = PullWhenFull, _
                                                                        .ignoretc = IgnoreTC, .cibdelay = CIBDelay}
                                                        Return PodInfo

                                                    End Function)
        End Function
        ''' <summary>
        ''' Selects the pick level information based on workstation ids
        ''' </summary>
        ''' <returns>A list of object containg all the information for a pick level </returns>
        ''' <remarks></remarks>
        Public Function selPickLevels() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                                 Dim DataReader As SqlDataReader = Nothing
                                                                 Dim pickLevelInfo As New List(Of Object)
                                                                 Dim PickLevel As Integer = -1
                                                                 Dim StartShelf As Integer = -1
                                                                 Dim EndShelf As Integer = -1

                                                                 Try
                                                                     DataReader = RunSPArray("PrefSelPickLevels", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                                     If DataReader.HasRows Then
                                                                         While DataReader.Read()
                                                                             PickLevel = IIf(Not IsDBNull(DataReader(0)), DataReader(0), -1)
                                                                             StartShelf = IIf(Not IsDBNull(DataReader(1)), DataReader(1), -1)
                                                                             EndShelf = IIf(Not IsDBNull(DataReader(2)), DataReader(2), -1)
                                                                             pickLevelInfo.Add(New With {.picklevel = PickLevel, .startshelf = StartShelf, .endshelf = EndShelf})
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "selPickLevels", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(DataReader) Then
                                                                         DataReader.Dispose()
                                                                     End If
                                                                 End Try
                                                                 Debug.WriteLine(PickLevel)
                                                                 Return pickLevelInfo
                                                             End Function)
        End Function
        ''' <summary>
        ''' Selects the information for the miscellaneous section
        ''' </summary>
        ''' <returns>An object that contains all the information</returns>
        ''' <remarks></remarks>
        Public Function selMiscSettings() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim index As Integer = 0
                                                        Dim inactiveCars As New List(Of String)
                                                        Dim HotPickOrderNum As String = ""
                                                        Dim HotPutOrderNum As String = ""
                                                        Dim IdleCheck As String = ""
                                                        Dim IdleMess As String = ""
                                                        Dim CCBatPik As String = ""
                                                        Dim CCBatPut As String = ""
                                                        Dim CCHotPik As String = ""
                                                        Dim CCHotPut As String = ""

                                                        Try
                                                            DataReader = RunSPArray("PrefSelMiscSett", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                            While DataReader.HasRows
                                                                While DataReader.Read()
                                                                    For x As Integer = 0 To DataReader.FieldCount - 1
                                                                        If index = 0 Then
                                                                            If DataReader(0) = "Hot Pick Order Number" Then
                                                                                HotPickOrderNum = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "Hot Put Away Order Number" Then
                                                                                HotPutOrderNum = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "Idle Check" Then
                                                                                IdleCheck = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "Idle Message" Then
                                                                                IdleMess = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "CCBatPik" Then
                                                                                CCBatPik = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "CCBatPut" Then
                                                                                CCBatPut = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "CCHotPik" Then
                                                                                CCHotPik = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            ElseIf DataReader(0) = "CCHotPut" Then
                                                                                CCHotPut = IIf(Not IsDBNull(DataReader(1)), DataReader(1), "")
                                                                            End If
                                                                        ElseIf index = 1 Then
                                                                            inactiveCars.Add(IIf(Not IsDBNull(DataReader(x)), DataReader(x), ""))
                                                                        End If
                                                                    Next
                                                                End While
                                                                index += 1
                                                                DataReader.NextResult()
                                                            End While
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("PreferencesHub", "selPickLevels", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Dim MiscInfo As Object = New With {.hotpickordernum = HotPickOrderNum, .hotputordernum = HotPutOrderNum, .idlecheck = IdleCheck, .idlemess = IdleMess, _
                                                                         .ccbatpik = CCBatPik, .ccbatput = CCBatPut, .cchotpik = CCHotPik, .cchotput = CCHotPut, .inactivecars = inactiveCars}

                                                        Return MiscInfo

                                                    End Function)
        End Function
        ''' <summary>
        ''' Adds or deletes a bulk zone depeding on the identifier vlaue
        ''' </summary>
        ''' <param name="zone">The zone ot be added or deleted</param>
        ''' <param name="ident">The identifier, true is adding, false is deleting</param>
        ''' <returns>A boolean telling if the coperation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function addDeleteBulkZones(zone As String, ident As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim Success As Boolean = True

                                                         Try

                                                             DataReader = RunSPArray("PrefInsDelBulkZone", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", zone, strVar}, _
                                                                                                            {"@Ident", IIf(ident, 1, 0), intVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                             If DataReader.HasRows Then
                                                                 Success = False
                                                             End If

                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "addDeleteBulkZones", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return Success
                                                     End Function)
        End Function
        ''' <summary>
        ''' Gets the Company Logo's extension (.png, etc.)
        ''' </summary>
        ''' <returns>A string containing the location of the company logo</returns>
        ''' <remarks></remarks>
        Public Function getCompanyLogoExt() As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function()
                                                        Return GlobalFunctions.getCompanyLogoExtension(Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Function)
        End Function
        ''' <summary>
        ''' Updates information for the workstation settings section
        ''' </summary>
        ''' <param name="PodID">The currently set pod id value</param>
        ''' <param name="ScanPicks">The currently set value for the scan picks slider</param>
        ''' <param name="ScanCounts">The currently set value for the scan counts slier</param>
        ''' <param name="ScanPuts">The currently set value for the scan puts slider</param>
        ''' <param name="PrintRepLoc">The currently set report printer location</param>
        ''' <param name="PrintLabLoc">The currently set label printer location</param>
        ''' <param name="QuickPick">The currently set value for the quick pick slider</param>
        ''' <param name="DefQuickPick">The currently set value for the default quick pick slider</param>
        ''' <returns>A boolean signifying if the desired operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateWorkStationSettings(CFID As String, PodID As String, ScanPicks As Boolean, ScanCounts As Boolean, ScanPuts As Boolean, PrintRepLoc As String, PrintLabLoc As String,
                                                  QuickPick As Boolean, DefQuickPick As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim ID As String = ""
                                                         Dim Success As Boolean = True
                                                         Debug.WriteLine(CFID)
                                                         Try

                                                             RunActionSP("PrefUpdWorkstationSett", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@CartonFlowId", CFID, strVar},
                                                                                                    {"@PodID", PodID, strVar}, {"@ScanPicks", IIf(ScanPicks, 1, 0), intVar}, {"@ScanCounts", IIf(ScanCounts, 1, 0), intVar},
                                                                                                    {"@ScanPuts", IIf(ScanPuts, 1, 0), intVar}, {"@PrintRepLoc", PrintRepLoc, strVar}, {"@PrintLabLoc", PrintLabLoc, strVar},
                                                                                                    {"@QuickPick", IIf(QuickPick, "Yes", "No"), strVar}, {"@DefQuickPick", IIf(DefQuickPick, "Yes", "No"), strVar},
                                                                                                    {"@User", Context.User.Identity.Name, strVar}})




                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updateWorkStationSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)

        End Function
        ''' <summary>
        ''' Updates the information under the tote management section
        ''' </summary>
        ''' <param name="PickTotes">The currently set vlaue for the pick to tote slider</param>
        ''' <param name="PutTotes">The currently set value for the put awaytote slider</param>
        ''' <param name="AutoPrintTote">The currently set value for the auto print tote labels slider</param>
        ''' <param name="BatchPut">The currently set value for the batch carousel put away slider</param>
        ''' <param name="BatchHotPut">The currently set batch hot put away</param>
        ''' <param name="CarManifest">The currently set value for the carousel pick tote manifest slider</param>
        ''' <param name="OffCarManifest">The currently set value for the off carousel pick tote manifest slider</param>
        ''' <param name="AutoToteManifest">The currently set value for the auto print off carousel pick tote manifest slider</param>
        ''' <returns>A boolean signifying if the desired operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateToteManagement(PickTotes As Boolean, PutTotes As Boolean, AutoPrintTote As Boolean, BatchPut As Boolean, BatchHotPut As String, CarManifest As Boolean, _
                                             OffCarManifest As Boolean, AutoToteManifest As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim Success As Boolean = True

                                                         Try
                                                             RunActionSP("PrefUpdToteManagement", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@PickTotes", IIf(PickTotes, 1, 0), intVar}, _
                                                                                                   {"@PutTotes", IIf(PutTotes, 1, 0), intVar}, {"@AutoPrintTote", IIf(AutoPrintTote, 1, 0), intVar}, _
                                                                                                   {"@BatchPut", IIf(BatchPut, 1, 0), intVar}, {"@BatchHotPut", BatchHotPut, strVar}, _
                                                                                                   {"@CarManifest", IIf(CarManifest, "Yes", "No"), strVar}, {"@OffCarManifest", IIf(OffCarManifest, "Yes", "No"), strVar}, _
                                                                                                   {"@AutoToteManifest", IIf(AutoToteManifest, "Yes", "No"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updateToteManagement", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)

        End Function
        ''' <summary>
        ''' Updates the information under the Location assignment functions section
        ''' </summary>
        ''' <param name="LocAssOrderSel">The currently set value for the location assign single order selection slider</param>
        ''' <param name="PrintReprocRep">The currently set value fot the print reroces report after allocation slider</param>
        ''' <param name="PrintPickLab">The currently set value for the print pick label for each pick slider</param>
        ''' <param name="PrintPickLabBatch">The currently set value for the print all at once slider</param>
        ''' <param name="LocAssPickSort">The currently set pick locations assigned by value</param>
        ''' <param name="ShowTransQtyBulk">The currently set display bulk counts quantity value</param>
        ''' <param name="AutoCompBackOrders">The currently set value for the auto complete back order slider</param>
        ''' <param name="SapLocChange">The currently set value for the create sp location change transactions slider</param>
        ''' <returns>A boolean signifying if the desired operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateLocationAssignmentFunctions(LocAssOrderSel As Boolean, PrintReprocRep As Boolean, PrintPickLab As Boolean, PrintPickLabBatch As Boolean, LocAssPickSort As String, _
                                                          ShowTransQtyBulk As String, AutoCompBackOrders As Boolean, SapLocChange As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim Success As Boolean = True

                                                         Try
                                                             RunActionSP("PrefUpdLocAssFunctions", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@LocAssOrderSel", IIf(LocAssOrderSel, 1, 0), intVar}, _
                                                                                                    {"@PrintReprocRep", IIf(PrintReprocRep, 1, 0), intVar}, {"@PrintPickLab", IIf(PrintPickLab, 1, 0), intVar}, _
                                                                                                    {"@PrintPickLabBatch", IIf(PrintPickLabBatch, 1, 0), intVar}, {"@LocAssPickSort", LocAssPickSort, strVar}, _
                                                                                                    {"@ShowTransQtyBulk", ShowTransQtyBulk, strVar}, {"@AutoCompBackOrders", IIf(AutoCompBackOrders, "Yes", "No"), strVar}, _
                                                                                                    {"@SapLocChange", IIf(SapLocChange, "Yes", "No"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updateLocationAssignmentFunctions", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Gets Location Zones from LZ table
        ''' </summary>
        ''' <returns>A list of dictonary where each dictionary is a row containing a location zone</returns>
        ''' <remarks></remarks>
        Public Function getLocationZones() As Task(Of List(Of Dictionary(Of String, Object)))
            Return Task(Of List(Of Dictionary(Of String, Object))).Factory.StartNew(
                Function()
                    Return GetResultMapList("selLocationZones", Context.QueryString.Get("WSID"))
                End Function)
        End Function

        ''' <summary>
        ''' Saves a Location Zone
        ''' </summary>
        ''' <param name="OldZone">Zone to update</param>
        ''' <param name="settings">Preferences for that zone</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveLocationZone(OldZone As String, settings As Dictionary(Of String, String)) As Task
            Return Task.Factory.StartNew(
                Sub()
                    Try
                        ' settings properties: ['Zone', 'Carousel', 'Carton Flow', 'Replenishment Zone', 'Staging Zone', 'Include CF Carousel Pick', 'Parent Zone', 'Include In Auto Batch', 'Include In Transactions', 'Dynamic Warehouse', 'Allocable', 'Location Name', 'Label1', 'Label2', 'Label3', 'Label4', 'Sequence', 'Kanban Zone', 'Kanban Replenishment Zone'];

                        RunActionSP("updateLocationZone", Context.QueryString.Get("WSID"), {{"@OldZone", OldZone, strVar}, {"@NewZone", settings("Zone"), strVar}, {"@Carousel", CastAsSqlBool(settings("Carousel")), intVar}, _
                                                           {"@CartonFlow", CastAsSqlBool(settings("Carton Flow")), intVar}, {"@Replenishment", CastAsSqlBool(settings("Replenishment Zone")), intVar}, _
                                                           {"@Staging", CastAsSqlBool(settings("Staging Zone")), intVar}, {"@IncludeCF", CastAsSqlBool(settings("Include CF Carousel Pick")), intVar}, _
                                                           {"@IncludeBatch", CastAsSqlBool(settings("Include In Auto Batch")), intVar}, {"@CCS", CastAsSqlBool(settings("Include In Transactions")), intVar}, _
                                                           {"@Dynamic", CastAsSqlBool(settings("Dynamic Warehouse")), intVar}, {"@Allocable", CastAsSqlBool(settings("Allocable")), intVar}, _
                                                           {"@LocationName", settings("Location Name"), strVar}, {"@L1", settings("Label1"), strVar}, {"@L2", settings("Label2"), strVar}, _
                                                           {"@L3", settings("Label3"), strVar}, {"@L4", settings("Label4"), strVar}, {"@Sequence", If(settings("Sequence") = "", nullVar, settings("Sequence")), intVar}, _
                                                           {"@ParentZone", settings("Parent Zone"), strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                           {"@KB", CastAsSqlBool(settings("Kanban Zone")), intVar}, {"@KBReplen", CastAsSqlBool(settings("Kanban Replenishment Source Zone")), intVar}})
                    Catch ex As Exception
                        Debug.WriteLine(ex.Message)
                        insertErrorMessages("PreferencesHub", "saveLocationZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                    End Try
                End Sub)
        End Function

        ''' <summary>
        ''' Saves a new Location Zone
        ''' </summary>
        ''' <param name="zone">Zone</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveNewLocationZone(zone As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("insLocationZone", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveNewLocationZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes specified zone from location zones table if nothing is allocated to the zone and logs into the event log
        ''' </summary>
        ''' <param name="zone">The zone to be deleted</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteLocationZone(zone As String) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim datareader As SqlDataReader = Nothing

                                                         Try
                                                             datareader = RunSPArray("delLocationZone", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                             If datareader.HasRows Then
                                                                 Return False
                                                             Else
                                                                 Return True
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "deleteLocationZone", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Return False
                                                         Finally
                                                             If Not IsNothing(datareader) Then
                                                                 datareader.Dispose()
                                                             End If
                                                         End Try
                                                     End Function)
        End Function

        ''' <summary>
        ''' Adds or updates the Pod Prefrences Table
        ''' </summary>
        ''' <param name="Zone">The  pod zone value</param>
        ''' <param name="MaxOrder">The max order vlaue</param>
        ''' <returns>An object telling if the operation was successful and if it failed the zone value that cased the error</returns>
        ''' <remarks></remarks>
        Public Function addDeletePodZones(Zone As String, MaxOrder As String, Ident As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim Success As Boolean = True

                                                         If IsDBNull(MaxOrder) Or MaxOrder.Trim() = "" Then
                                                             MaxOrder = "0"
                                                         End If

                                                         Try
                                                             DataReader = RunSPArray("PrefInsDelPodZones", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", Zone, strVar}, _
                                                                                                            {"@MaxOrder", CInt(MaxOrder), intVar}, {"@Ident", IIf(Ident, 1, 0), intVar}, _
                                                                                                            {"@User", Context.User.Identity.Name, strVar}})
                                                             If DataReader.HasRows Then
                                                                 Success = False
                                                             End If


                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "addDeletePodZones", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return Success

                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates the information on the POD Setup page
        ''' </summary>
        ''' <param name="CarSW">Slider value of carousel workstation</param>
        ''' <param name="SpinSort">Value for carousel spin sort</param>
        ''' <param name="CFSeq">Value for carton flow sequence</param>
        ''' <param name="CIBDelay">Value for CIB delay</param>
        ''' <param name="Pos20">Slider value for 20 position matrix</param>
        ''' <param name="ItemLT">Slider value for item num on light tree</param>
        ''' <param name="PullFull">Slider value for pull when case full</param>
        ''' <param name="IgnoreTC">silder value for ignore task complete</param>
        ''' <returns>Boolean indicating if operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updatePodSetupInfo(CarSW As Boolean, SpinSort As String, CFSeq As String, CIBDelay As String, Pos20 As Boolean, ItemLT As Boolean, PullFull As Boolean, _
                                           IgnoreTC As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim Success As Boolean = True

                                                         Try
                                                             RunActionSP("PrefUpdPODPrefs", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@CarSW", IIf(CarSW, "Yes", "No"), strVar}, _
                                                                                             {"@SpinSort", SpinSort, strVar}, {"@CFSeq", CFSeq, strVar}, {"@CIB", CInt(CIBDelay), intVar}, _
                                                                                             {"@Pos20", IIf(Pos20, "Yes", "No"), strVar}, {"@ItemLT", IIf(ItemLT, "Yes", "No"), strVar}, _
                                                                                             {"@PullFull", IIf(PullFull, "Yes", "No"), strVar}, {"@IgnoreTC", IIf(IgnoreTC, "Yes", "No"), strVar},
                                                                                             {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updatePodSetupInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)

        End Function

        ''' <summary>
        ''' Gets Location Names from [Locations]
        ''' </summary>
        ''' <returns>A list of string containing the location names</returns>
        ''' <remarks></remarks>
        Public Function getLocationNames() As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim locnames As New List(Of String)
                                                                 Dim datareader As SqlDataReader = Nothing

                                                                 Try
                                                                     datareader = RunSPArray("selLocationNames", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             If Not IsDBNull(datareader(0)) Then
                                                                                 locnames.Add(datareader(0))
                                                                             End If
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("GlobalHub", "getLocationNames", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return locnames
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves a Location Name
        ''' </summary>
        ''' <param name="oldName">Old name to update or empty string</param>
        ''' <param name="newName">New name to save</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveLocationName(oldName As String, newName As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsLocationName", Context.QueryString.Get("WSID"), {{"@OldName", oldName, strVar}, {"@NewName", newName, strVar}, _
                                                                                       {"@User", Context.User.Identity.Name, strVar}, _
                                                                                       {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveLocationName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes specified location name and logs in the event log
        ''' </summary>
        ''' <param name="name">The location name to be deleted</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteLocationName(name As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delLocationName", Context.QueryString.Get("WSID"), {{"@Name", name, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteLocationName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Addsto or updates the Pick Levels Table
        ''' </summary>
        ''' <param name="value">The identifier for which sp to execute. If new will perfomr the insert, otherwise acts as the pick level value</param>
        ''' <param name="StartShelf">The new start shlef value being added or updated</param>
        ''' <param name="EndShelf">The new end shelf value being added or updated</param>
        ''' <returns>A boolean indicating if the desired operationwas completed successfully</returns>
        ''' <remarks></remarks>
        Public Function addUpdatePickLevel(value As String, StartShelf As Integer, EndShelf As Integer) As Task(Of Integer)
            'level only used in insert as it is the new pick level
            Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                         Dim datareader As SqlDataReader = Nothing
                                                         Dim identity As Integer = -1
                                                         Try
                                                             If value.Trim().ToLower() = "(new)" Then
                                                                 datareader = RunSPArray("PrefInsPickLevel", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                  {"@StartShelf", StartShelf, intVar}, {"@EndShelf", EndShelf, intVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                                 If datareader.HasRows Then
                                                                     If datareader.Read() Then
                                                                         If Not IsDBNull(datareader(0)) Then
                                                                             identity = datareader(0)
                                                                         End If
                                                                     End If
                                                                 End If
                                                             Else
                                                                 RunActionSP("PrefUpdPickLevel", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@PickLevel", value, intVar}, _
                                                                                                  {"@StartShelf", StartShelf, intVar}, {"@EndShelf", EndShelf, intVar}})
                                                             End If
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "addUpdatePickLevels", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(datareader) Then
                                                                 datareader.Dispose()
                                                             End If
                                                         End Try
                                                         Return identity
                                                     End Function)
        End Function

        ''' <summary>
        ''' Clears tote information from any incomplete open transactions
        ''' </summary>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function clearTotes() As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateOTClearTotes", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "clearTotes", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
        ''' <summary>
        ''' Deletes the desired pick level
        ''' </summary>
        ''' <param name="level">THe desired pick level to be deleted</param>
        ''' <returns>A boolean indicating if the procedure was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function deletePickLevel(level As Integer) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim Success As Boolean = True

                                                         Try
                                                             RunActionSP("PrefDelPickLevel", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@PickLevel", level, intVar}, _
                                                                                              {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "deletePickLevel", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Updates the information on the Miscellaneous section 
        ''' </summary>
        ''' <param name="IdleCheck">The minutes before the idle message appears</param>
        ''' <param name="IdleMess">The minutes the idle message is on before shut down</param>
        ''' <param name="HotPickOrderNum">The value of the force hot pick slider</param>
        ''' <param name="HotPutOrderNum">The value of the force hou put away slidder</param>
        ''' <returns>Boolean identifying if operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateMiscellaneousInfo(IdleCheck As String, IdleMess As String, HotPickOrderNum As Boolean, HotPutOrderNum As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim Success = True

                                                         Try
                                                             RunActionSP("PrefUpdMiscSett", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@IdleCheck", IdleCheck, strVar}, {"@IdleMess", IdleMess, strVar}, _
                                                                                             {"@HotPickOrderNum", IIf(HotPickOrderNum, "Yes", "No"), strVar}, _
                                                                                             {"@HotPutOrderNum", IIf(HotPutOrderNum, "Yes", "No"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updateMiscellaneousInfo", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return Success
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates the information on the Miscellaneous section 
        ''' </summary>
        ''' <param name="CCBatPik">stock count confirm car pick slider</param>
        ''' <param name="CCBatPut">stock count confirm car put away slider</param>
        ''' <param name="CCHotPik">stock count confirm hot pick slider</param>
        ''' <param name="CCHotPut">stock count confirm hot put away slider </param>
        ''' <returns>Boolean identifying if operation was completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateMiscStockCount(CCBatPik As Boolean, CCBatPut As Boolean, CCHotPik As Boolean, CCHotPut As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim success As Boolean = True

                                                         Try
                                                             RunActionSP("PrefUpdMiscStockCount", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@CCBatPik", IIf(CCBatPik, "Yes", "No"), strVar}, _
                                                                                             {"@CCBatPut", IIf(CCBatPut, "Yes", "No"), strVar}, {"@CCHotPik", IIf(CCHotPik, "Yes", "No"), strVar}, _
                                                                                             {"@CCHotPut", IIf(CCHotPut, "Yes", "No"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                                         Catch ex As Exception
                                                             success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "updateMiscStockCount", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try

                                                         Return success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Marks the carousel as inactive and quarantines any location in invenotry mpa with the same carousel
        ''' </summary>
        ''' <param name="Car">The inactive carousel</param>
        ''' <returns>An object telling if the operation was successful, and if wasnt the carousel value that gave the error</returns>
        ''' <remarks></remarks>
        Public Function addDeleteInactiveCarousels(Car As String, Ident As Boolean) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim Success As Boolean = True


                                                         Try
                                                             DataReader = RunSPArray("PrefInsDelInactiveCar", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Car", Car, strVar}, _
                                                                                                               {"@Ident", IIf(Ident, 1, 0), intVar}, {"@User", Context.User.Identity.Name, strVar}})

                                                             If DataReader.HasRows Then
                                                                 Success = False
                                                             End If
                                                         Catch ex As Exception
                                                             Success = False
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "addDeleteInactiveCarousels", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try

                                                         Return Success
                                                     End Function)
        End Function

        ''' <summary>
        ''' Gets the scan verify specifications for a specific Inv Map ID
        ''' </summary>
        ''' <param name="InvMapID">Inventory Map ID</param>
        ''' <returns>A list of object containing the scan verify infomration for the given inv map id</returns>
        ''' <remarks></remarks>
        Public Function getScanVerify(InvMapID As Integer) As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim verifies As New List(Of Object)

                                                                 Try
                                                                     datareader = RunSPArray("selScanVerifyByInvMapID", Context.QueryString.Get("WSID"), {{"@InvMapID", InvMapID, intVar}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             verifies.Add(New With { _
                                                                                          .TransType = IIf(IsDBNull(datareader(0)), "", datareader(0)), _
                                                                                          .ScanSequence = IIf(IsDBNull(datareader(1)), "", datareader(1)), _
                                                                                          .Field = IIf(IsDBNull(datareader(2)), "", datareader(2)), _
                                                                                          .VType = IIf(IsDBNull(datareader(3)), "", datareader(3)), _
                                                                                          .Start = IIf(IsDBNull(datareader(4)), "", datareader(4)), _
                                                                                          .End = IIf(IsDBNull(datareader(5)), "", datareader(5)) _
                                                                                      })
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getScanVerify", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return verifies
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves a scan verify entry
        ''' </summary>
        ''' <param name="InvMapID">Inv Map ID of the location the rule applies to</param>
        ''' <param name="prefs">Trans type, sequence, field, verify type, start and length as [new, old]</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveScanVerify(InvMapID As Integer, prefs As List(Of String)) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsScanVerify", Context.QueryString.Get("WSID"), {{"@InvMapID", InvMapID, intVar}, {"@TransType", prefs(0), strVar}, {"@Sequence", prefs(1), intVar}, _
                                                                                     {"@Field", prefs(2), strVar}, {"@VerifyType", prefs(3), strVar}, {"@Start", prefs(4), intVar}, _
                                                                                     {"@Length", prefs(5), intVar}, {"@OldTransType", prefs(6), strVar}, _
                                                                                     {"@User", Context.User.Identity.Name, strVar}, {"@OldSequence", prefs(7), intVar},
                                                                                     {"@OldField", prefs(8), strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveScanVerify", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes an entry in the Scan Verify Preferences table and logs information in the event log
        ''' </summary>
        ''' <param name="InvMapID">Inv Map ID to delete from</param>
        ''' <param name="TransType">Transaction type of the rule to delete</param>
        ''' <param name="Sequence">Scan sequence of the rule to delete</param>
        ''' <param name="Field">Fieldname of the rule to delete</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteScanVerify(InvMapID As Integer, TransType As String, Sequence As Integer, Field As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delScanVerify", Context.QueryString.Get("WSID"), {{"@InvMapID", InvMapID, intVar}, {"@TransType", TransType, strVar}, {"@Sequence", Sequence, intVar}, _
                                                                               {"@Field", Field, strVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteScanVerify", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets Scan Verification defaults
        ''' </summary>
        ''' <returns>A list of object containing al the scan verify defaults</returns>
        ''' <remarks></remarks>
        Public Function getSVDefaults() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim verifies As New List(Of Object)

                                                                 Try
                                                                     datareader = RunSPArray("selScanVerifyDefaults", Context.QueryString.Get("WSID"), {{"nothing"}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             verifies.Add(New With { _
                                                                                          .TransType = IIf(IsDBNull(datareader(0)), "", datareader(0)), _
                                                                                          .ScanSequence = IIf(IsDBNull(datareader(1)), "", datareader(1)), _
                                                                                          .Field = IIf(IsDBNull(datareader(2)), "", datareader(2)), _
                                                                                          .VType = IIf(IsDBNull(datareader(3)), "", datareader(3)), _
                                                                                          .Start = IIf(IsDBNull(datareader(4)), "", datareader(4)), _
                                                                                          .End = IIf(IsDBNull(datareader(5)), "", datareader(5)) _
                                                                                      })
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getSVDefaults", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return verifies
                                                             End Function)
        End Function
        ''' <summary>
        ''' Selects all the information for the Bulk Zones zection
        ''' </summary>
        ''' <returns>A list of object containg the zone, locattion name, and if it is a bulk zone</returns>
        ''' <remarks></remarks>
        Public Function selAllBulkZones() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)

                                                                 Return Preferences.selBulkZonesDropDown(Context.QueryString.Get("WSID"), Context.User.Identity.Name)

                                                             End Function)
        End Function

        ''' <summary>
        ''' Gets the sort bar configuration of the requesting workstation
        ''' </summary>
        ''' <returns>a list of string with each sortbar's value</returns>
        ''' <remarks></remarks>
        Public Function getSortBar() As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function()
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim sortbar As New List(Of String)

                                                                 Try
                                                                     datareader = RunSPArray("selSortBar", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                                     If datareader.HasRows Then
                                                                         If datareader.Read() Then
                                                                             For x As Integer = 0 To datareader.FieldCount - 1
                                                                                 sortbar.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                             Next
                                                                         End If
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getSortBar", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return sortbar
                                                             End Function)
        End Function
        ''' <summary>
        ''' Updates the sortbar values to their desired values
        ''' </summary>
        ''' <param name="settings">A list containing the new values to update to</param>
        ''' <param name="oldvalues">A list containg the old values</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveSortBar(settings As List(Of Integer), oldvalues As List(Of Integer)) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateBinPreferences", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                                                      {"@T1", settings(0), intVar}, {"@T2", settings(1), intVar}, {"@T3", settings(2), intVar}, _
                                                                                      {"@T4", settings(3), intVar}, {"@T5", settings(4), intVar}, {"@T6", settings(5), intVar}, _
                                                                                      {"@T7", settings(6), intVar}, {"@T8", settings(7), intVar}, {"@T9", settings(8), intVar}, _
                                                                                      {"@T10", settings(9), intVar}, _
                                                                                      {"@T11", settings(10), intVar}, {"@T12", settings(11), intVar}, {"@T13", settings(12), intVar}, _
                                                                                      {"@T14", settings(13), intVar}, {"@T15", settings(14), intVar}, {"@T16", settings(15), intVar}, _
                                                                                      {"@T17", settings(16), intVar}, {"@T18", settings(17), intVar}, {"@T19", settings(18), intVar}, _
                                                                                      {"@T20", settings(19), intVar}, _
                                                                                      {"@OT1", oldvalues(0), intVar}, {"@OT2", oldvalues(1), intVar}, {"@OT3", oldvalues(2), intVar}, _
                                                                                      {"@OT4", oldvalues(3), intVar}, {"@OT5", oldvalues(4), intVar}, {"@OT6", oldvalues(5), intVar}, _
                                                                                      {"@OT7", oldvalues(6), intVar}, {"@OT8", oldvalues(7), intVar}, {"@OT9", oldvalues(8), intVar}, _
                                                                                      {"@OT10", oldvalues(9), intVar}, _
                                                                                      {"@OT11", oldvalues(10), intVar}, {"@OT12", oldvalues(11), intVar}, {"@OT13", oldvalues(12), intVar}, _
                                                                                      {"@OT14", oldvalues(13), intVar}, {"@OT15", oldvalues(14), intVar}, {"@OT16", oldvalues(15), intVar}, _
                                                                                      {"@OT17", oldvalues(16), intVar}, {"@OT18", oldvalues(17), intVar}, {"@OT19", oldvalues(18), intVar}, _
                                                                                      {"@OT20", oldvalues(19), intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveSortBar", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function
        ''' <summary>
        ''' Selects the information for the Pods panel under POD Setup section
        ''' </summary>
        ''' <returns>A list of object with each zone, max oder, and if hey are a pod zone</returns>
        ''' <remarks></remarks>
        Public Function selAllPodZones() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                                 Return Preferences.selPodZonesDropDown(Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                             End Function)

        End Function

        ''' <summary>
        ''' Deletes the specified device from device preferences
        ''' </summary>
        ''' <param name="deviceID">Device ID column value</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteDevicePreference(deviceID As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delDevicePreference", Context.QueryString.Get("WSID"), {{"@DeviceID", deviceID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteDevicePreference", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets device and related information from device preferences
        ''' </summary>
        ''' <param name="deviceID">Device to get information for</param>
        ''' <returns>An object that contains the information for a device</returns>
        ''' <remarks></remarks>
        Public Function getDeviceInformation(deviceID As Integer) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Dim datareader As SqlDataReader = Nothing
                                                        Dim device As New List(Of String)
                                                        Dim zones As New List(Of String)
                                                        Dim controller As New List(Of String)
                                                        Dim model As New List(Of String)
                                                        Dim IPTI As New List(Of Object)
                                                        Try
                                                            datareader = RunSPArray("selDevicePreferencesByID", Context.QueryString.Get("WSID"), {{"@DeviceID", deviceID, intVar}})

                                                            For row As Integer = 0 To 4
                                                                While datareader.Read()
                                                                    If row <> 4 Then
                                                                        For x As Integer = 0 To datareader.FieldCount - 1
                                                                            If row = 0 Then
                                                                                device.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                            ElseIf row = 1 Then
                                                                                zones.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                            ElseIf row = 2 Then
                                                                                controller.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                            ElseIf row = 3 Then
                                                                                model.Add(IIf(IsDBNull(datareader(x)), "", datareader(x)))
                                                                            End If
                                                                        Next
                                                                    Else
                                                                        IPTI.Add(New With {.display = datareader(0), _
                                                                                           .bay = datareader(1), _
                                                                                           .baydisp = datareader(2), _
                                                                                           .comm = datareader(3)})
                                                                    End If
                                                                End While
                                                                datareader.NextResult()
                                                            Next
                                                        Catch ex As Exception
                                                            Debug.WriteLine(ex.Message)
                                                            insertErrorMessages("PreferencesHub", "getDeviceInformation", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(datareader) Then
                                                                datareader.Dispose()
                                                            End If
                                                        End Try

                                                        Return New With {.device = device, .zones = zones, .controllers = controller, .devModel = model, .IPTI = IPTI}
                                                    End Function)
        End Function

        ''' <summary>
        ''' Saves an IPTI entry
        ''' </summary>
        ''' <param name="prefs">IPTI display settings</param>
        ''' <param name="zone">Zone the IPTI entry is for</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveIPTI(prefs As List(Of Integer), zone As String, newEntry As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInsIPTI", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                               {"@Zone", zone, strVar}, {"@Display", prefs(0), intVar}, {"@ODisplay", prefs(1), intVar}, _
                                                                               {"@Bay", prefs(2), intVar}, {"@OBay", prefs(3), intVar}, {"@BayDisplay", prefs(4), intVar}, _
                                                                               {"@OBayDisplay", prefs(5), intVar}, {"@Comm", prefs(6), intVar}, {"@OComm", prefs(7), intVar}, _
                                                                               {"@New", CastAsSqlBool(newEntry), intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveIPTI", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Deletes the specified entry in IPTI Bay Locations
        ''' </summary>
        ''' <param name="disp">Display Number</param>
        ''' <param name="bay">Bay number</param>
        ''' <param name="baydisp">Bay display number</param>
        ''' <param name="comm">Comm port</param>
        ''' <param name="zone">Zone</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function deleteIPTI(disp As Integer, bay As Integer, baydisp As Integer, comm As Integer, zone As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("delIPTIEntry", Context.QueryString.Get("WSID"), {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                              {"@Zone", zone, strVar}, {"@Display", disp, intVar}, {"@Bay", bay, intVar}, {"@BayDisplay", baydisp, intVar}, _
                                                                              {"@Comm", comm, intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "deleteIPTI", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Updates device preferences settings
        ''' </summary>
        ''' <param name="zone"></param>
        ''' <param name="hostport"></param>
        ''' <param name="baud"></param>
        ''' <param name="parity"></param>
        ''' <param name="word"></param>
        ''' <param name="stopbit"></param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function updateAllOrZoneDevicePreferences(zone As String, hostport As String, baud As String, parity As String, word As String, stopbit As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim sp As String
                                             Dim params As String(,)

                                             If zone = "" Then
                                                 sp = "updateDevicePreferencesByComPort"
                                                 params = {{"@HostPort", hostport, strVar}, {"@Baud", baud, strVar}, {"@Parity", parity, strVar}, {"@Word", word, strVar}, {"@Stop", stopbit, strVar}, _
                                                           {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}}
                                             Else
                                                 sp = "updateDevicePreferencesByComPortAndZone"
                                                 params = {{"@HostPort", hostport, strVar}, {"@Baud", baud, strVar}, {"@Parity", parity, strVar}, {"@Word", word, strVar}, {"@Stop", stopbit, strVar}, _
                                                           {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", zone, strVar}}
                                             End If

                                             Try
                                                 RunActionSP(sp, Context.QueryString.Get("WSID"), params)
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "updateAllOrZoneDevicePreferences", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets the tray tilt for carousels in a specific zone
        ''' </summary>
        ''' <param name="zone">Zone filter</param>
        ''' <returns>A list of object containing the tarys and their data</returns>
        ''' <remarks></remarks>
        Public Function getTrayTilt(zone As String) As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function()
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim trays As New List(Of Object)

                                                                 Try
                                                                     datareader = RunSPArray("selTrayTilt", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                             {"@WSID", Context.QueryString.Get("WSID"), strVar}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             trays.Add(New With {.zone = zone, _
                                                                                                 .carousel = IIf(IsDBNull(datareader(0)), "", datareader(0)), _
                                                                                                 .tray = IIf(IsDBNull(datareader(1)), "", datareader(1)), _
                                                                                                 .tilt = IIf(IsDBNull(datareader(2)), "", datareader(2)) _
                                                                                                })
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getTrayTilt", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return trays
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves a tray tilt in Shuttle Tray Tilt table
        ''' </summary>
        ''' <param name="zone">Zone to update</param>
        ''' <param name="carousel">Carousel to update</param>
        ''' <param name="tray">Tray to update</param>
        ''' <param name="checked">Value to update to as boolean</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveTrayTilt(zone As String, carousel As String, tray As String, checked As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateShuttleTrayTilt", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@Carousel", carousel, strVar}, {"@Tray", tray, strVar}, _
                                                                                       {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                       {"@Tilt", CastAsSqlBool(checked), intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveTrayTilt", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Toggles all Tray Tilts in a particular zone to value
        ''' </summary>
        ''' <param name="zone">Zone to set</param>
        ''' <param name="value">Value to set</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function saveTrayToggleAll(zone As String, value As Boolean) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateShuttleTrayTiltAll", Context.QueryString.Get("WSID"), {{"@Zone", zone, strVar}, {"@Tilt", CastAsSqlBool(value), intVar}, {"@User", Context.User.Identity.Name, strVar}, _
                                                                                          {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveTrayToggleAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Saves a device preference, new or edit
        ''' </summary>
        ''' <param name="preferences">Device preferences</param>
        ''' <param name="DeviceID">Optional Device ID if the submission is an edit</param>
        ''' <returns>An integer telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function saveDevicePreference(preferences As List(Of String), shown As String, DeviceID As Integer) As Task(Of Integer)
            Return Task(Of Integer).Factory.StartNew(Function()
                                                         Dim nothingLoc As Integer = preferences.IndexOf(Nothing)
                                                         While nothingLoc <> -1
                                                             preferences(nothingLoc) = ""
                                                             nothingLoc = preferences.IndexOf(Nothing)
                                                         End While

                                                         Dim sp As String = ""
                                                         Dim params As String(,) = {{}}
                                                         If DeviceID = 0 Then
                                                             sp = "insDevicePreference"
                                                             params = {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", preferences(0), strVar}, _
                                                                       {"@Type", preferences(1), strVar}, {"@Number", preferences(2), strVar}, {"@Model", preferences(3), strVar}, _
                                                                       {"@CType", preferences(4), strVar}, {"@CPort", preferences(5), strVar}, {"@Arrow", preferences(6), strVar}, _
                                                                       {"@Light", preferences(7), strVar}, {"@Laser", CastAsSqlBool(preferences(8)), intVar}, {"@LightNum", preferences(9), intVar}, _
                                                                                   {"@Address", preferences(10), intVar}, {"@Positions", preferences(11), intVar}, {"@Characters", preferences(12), intVar}, _
                                                                                   {"@KeyPair", preferences(13), strVar}}

                                                             Dim datareader As SqlDataReader = Nothing
                                                             Try
                                                                 datareader = RunSPArray(sp, Context.QueryString.Get("WSID"), params)

                                                                 If datareader.HasRows Then
                                                                     If datareader.Read() Then
                                                                         If Not IsDBNull(datareader(0)) Then
                                                                             Return datareader(0)
                                                                         End If
                                                                     End If
                                                                 End If
                                                             Catch ex As Exception
                                                                 Debug.WriteLine(ex.Message)
                                                                 insertErrorMessages("PreferencesHub", "saveDevicePreference", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(datareader) Then
                                                                     datareader.Dispose()
                                                                 End If
                                                             End Try

                                                         ElseIf shown = "OTHER" Then
                                                             sp = "updateDevicePreferenceOther"
                                                             params = {{"@DeviceID", DeviceID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", preferences(0), strVar}, _
                                                                       {"@Type", preferences(1), strVar}, {"@Number", preferences(2), strVar}, {"@Model", preferences(3), strVar}, _
                                                                       {"@CType", preferences(4), strVar}, {"@CPort", preferences(5), strVar}, {"@Arrow", preferences(6), strVar}, _
                                                                       {"@Light", preferences(7), strVar}, {"@Laser", CastAsSqlBool(preferences(8)), intVar}, {"@LightNum", preferences(9), intVar}, _
                                                                       {"@Address", preferences(10), intVar}, {"@Positions", preferences(11), intVar}, {"@Characters", preferences(12), intVar}, _
                                                                       {"@KeyPair", preferences(13), strVar}, {"@COM", preferences(14), strVar}, {"@Baud", preferences(15), strVar}, {"@Parity", preferences(16), strVar}, _
                                                                       {"@Word", preferences(17), strVar}, {"@Stop", preferences(18), strVar}}
                                                         ElseIf shown = "WMI JMIF" Then
                                                             sp = "updateDevicePreferenceJMIF"
                                                             params = {{"@DeviceID", DeviceID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", preferences(0), strVar}, _
                                                                       {"@Type", preferences(1), strVar}, {"@Number", preferences(2), strVar}, {"@Model", preferences(3), strVar}, _
                                                                       {"@CType", preferences(4), strVar}, {"@CPort", preferences(5), strVar}, {"@Arrow", preferences(6), strVar}, _
                                                                       {"@Light", preferences(7), strVar}, {"@Laser", CastAsSqlBool(preferences(8)), intVar}, {"@LightNum", preferences(9), intVar}, _
                                                                       {"@Address", preferences(10), intVar}, {"@Positions", preferences(11), intVar}, {"@Characters", preferences(12), intVar}, _
                                                                       {"@KeyPair", preferences(13), strVar}, {"@JMIF", preferences(14), strVar}, {"@HostIP", preferences(15), strVar}, {"@HostPort", preferences(16), intVar}, _
                                                                       {"@WSName", preferences(17), strVar}}
                                                         ElseIf shown = "WMI" Or shown = "WMI SETUP" Then
                                                             sp = "updateDevicePreferenceWMI"
                                                             params = {{"@DeviceID", DeviceID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", preferences(0), strVar}, _
                                                                       {"@Type", preferences(1), strVar}, {"@Number", preferences(2), strVar}, {"@Model", preferences(3), strVar}, _
                                                                       {"@CType", preferences(4), strVar}, {"@CPort", preferences(5), strVar}, {"@Arrow", preferences(6), strVar}, _
                                                                       {"@Light", preferences(7), strVar}, {"@Laser", CastAsSqlBool(preferences(8)), intVar}, {"@LightNum", preferences(9), intVar}, _
                                                                       {"@Address", preferences(10), intVar}, {"@Positions", preferences(11), intVar}, {"@Characters", preferences(12), intVar}, _
                                                                       {"@KeyPair", preferences(13), strVar}, {"@HostIP", preferences(14), strVar}, {"@HostPort", preferences(15), intVar}, {"@WSName", preferences(16), strVar}}
                                                         Else
                                                             sp = "updateDevicePreferenceIPTI"
                                                             params = {{"@DeviceID", DeviceID, intVar}, {"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Zone", preferences(0), strVar}, _
                                                                       {"@Type", preferences(1), strVar}, {"@Number", preferences(2), strVar}, {"@Model", preferences(3), strVar}, _
                                                                       {"@CType", preferences(4), strVar}, {"@CPort", preferences(5), strVar}, {"@Arrow", preferences(6), strVar}, _
                                                                       {"@Light", preferences(7), strVar}, {"@Laser", CastAsSqlBool(preferences(8)), intVar}, {"@LightNum", preferences(9), intVar}, _
                                                                       {"@Address", preferences(10), intVar}, {"@Positions", preferences(11), intVar}, {"@Characters", preferences(12), intVar}, _
                                                                       {"@KeyPair", preferences(13), strVar}}
                                                         End If

                                                         Try
                                                             RunActionSP(sp, Context.QueryString.Get("WSID"), params)
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "saveDevicePreference", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Try
                                                         Return 0
                                                     End Function)
        End Function
        ''' <summary>
        ''' Updates the car sw path field to the desired vlaue when a pod gets chnaged
        ''' </summary>
        ''' <param name="carSw">The resulting vlaue form the user as to if the workstation is a carousel workstation</param>
        ''' <returns>A boolean telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateCarSW(carSw As Boolean) As Task(Of Boolean)
            Return Task.Factory.StartNew(Function() As Boolean
                                             Dim success As Boolean = True

                                             Try
                                                 RunActionSP("PrefUpdCarSW", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@CarSW", IIf(carSw, "Yes", "No"), strVar}, _
                                                                              {"@User", Context.User.Identity.Name, strVar}})
                                             Catch ex As Exception
                                                 success = False
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "updateCarSW", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                             Return success
                                         End Function)
        End Function

        ''' <summary>
        ''' Gets the user connected POD
        ''' </summary>
        ''' <returns>An object telling which pod is connected and the carton flow zones</returns>
        ''' <remarks></remarks>
        Public Function getDevicePreferencePODInfo() As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function()
                                                        Return New With {.connectedPOD = Menu.getPOD(Context.User.Identity.Name, Context.QueryString.Get("WSID")), _
                                                                         .CFZones = Preferences.getCartonFlowZones(Context.User.Identity.Name, Context.QueryString.Get("WSID"))}
                                                    End Function)
        End Function

        ''' <summary>
        ''' Stops the Carousel interface
        ''' </summary>
        ''' <param name="carousel">Carousel to stop</param>
        ''' <returns>Nothing runs a sub</returns>
        ''' <remarks></remarks>
        Public Function StopCarouselInterface(carousel As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Preferences.StopCarouselInterface(Context.QueryString.Get("WSID"), carousel, Context.User.Identity.Name)
                                         End Sub)
        End Function

        ''' <summary>
        ''' Gets the light tree configuration for specified zone, carousel, bin location (row)
        ''' </summary>
        ''' <param name="zone">Desired zone</param>
        ''' <param name="carousel">Desired carousel</param>
        ''' <param name="bin">Desired bin</param>
        ''' <returns>A list of object that contains the light tree info for the given zone, carousel, and bin combination</returns>
        ''' <remarks></remarks>
        Public Function getLightTrees(zone As String, carousel As String, bin As String, desc As Boolean) As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function()
                                                                 Dim datareader As SqlDataReader = Nothing
                                                                 Dim trees As New List(Of Object)

                                                                 Try
                                                                     datareader = RunSPArray("selLightTreeAltLights", Context.QueryString.Get("WSID"), _
                                                                                           {{"@Zone", zone, strVar}, {"@Carousel", carousel, strVar}, _
                                                                                            {"@Bin", bin, strVar}, {"@Sort", IIf(desc, "DESC", "ASC"), strVar}})

                                                                     If datareader.HasRows Then
                                                                         While datareader.Read()
                                                                             If Not IsDBNull(datareader(0)) Then
                                                                                 trees.Add(New With {.shelf = datareader(0), .light = IIf(IsDBNull(datareader(1)), "", datareader(1))})
                                                                             End If
                                                                         End While
                                                                     End If
                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getLightTrees", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                 Finally
                                                                     If Not IsNothing(datareader) Then
                                                                         datareader.Dispose()
                                                                     End If
                                                                 End Try

                                                                 Return trees
                                                             End Function)
        End Function

        ''' <summary>
        ''' Saves an alternate light value in the specified zone, carousel, bin with the specified value (alt)
        ''' </summary>
        ''' <param name="zone">The desired zone</param>
        ''' <param name="carousel">The desired carousel</param>
        ''' <param name="bin">The desired bin</param>
        ''' <param name="alt">The light value</param>
        ''' <param name="shelf">The desired shelf</param>
        ''' <returns>Nonce, runs a task of sub</returns>
        ''' <remarks></remarks>
        Public Function saveAltLight(zone As String, carousel As String, bin As String, shelf As String, alt As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInvMapAltLight", Context.QueryString.Get("WSID"), _
                                                             {{"@Zone", zone, strVar}, {"@Carousel", carousel, strVar}, {"@Shelf", shelf, strVar}, {"@BinLocation", bin, strVar}, _
                                                              {"@Light", alt, intVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@User", Context.User.Identity.Name, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveAltLight", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Updates all alternate lights on the shelf specified in the zone and carousel specified
        ''' </summary>
        ''' <param name="zone">The desired zone</param>
        ''' <param name="carousel">The desired carousel</param>
        ''' <param name="shelf">The desired shelf</param>
        ''' <param name="alt">The light value</param>
        ''' <returns>Nonce, task of sub</returns>
        ''' <remarks></remarks>
        Public Function saveAltLightAll(zone As String, carousel As String, shelf As String, alt As Integer) As Task
            Return Task.Factory.StartNew(Sub()
                                             Try
                                                 RunActionSP("updateInvMapAltLightAll", Context.QueryString.Get("WSID"), _
                                                             {{"@User", Context.User.Identity.Name, strVar}, {"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                              {"@Zone", zone, strVar}, {"@Carousel", carousel, strVar}, {"@Shelf", shelf, strVar}, {"@Light", alt, intVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "saveAltLightAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                             End Try
                                         End Sub)
        End Function

        ''' <summary>
        ''' Sets all alternate light positions in the zone and carousel provided to 0
        ''' </summary>
        ''' <param name="zone">The desired zone</param>
        ''' <param name="carousel">The desired carousel</param>
        ''' <returns>None, task of sub</returns>
        ''' <remarks></remarks>
        Public Function resetAltLight(zone As String, carousel As String) As Task
            Return Task.Factory.StartNew(Sub()
                                             Dim WSID As String = Context.QueryString.Get("WSID")
                                             Dim user As String = Context.User.Identity.Name
                                             Try
                                                 RunActionSP("updateInvMapResetAltLight", WSID, {{"@Zone", zone, strVar}, {"@Carousel", carousel, strVar}, {"@WSID", WSID, strVar}, {"@User", user, strVar}})
                                             Catch ex As Exception
                                                 Debug.WriteLine(ex.Message)
                                                 insertErrorMessages("PreferencesHub", "resetAltLight", ex.Message, user, WSID)
                                             End Try
                                         End Sub)
        End Function
        ''' <summary>
        ''' Gets a list of color filters
        ''' </summary>
        ''' 
        ''' <returns>A list of objects with color,field,criteria props</returns>
        ''' <remarks></remarks>
        Public Function getOSFilters() As Task(Of List(Of Object))
            Return Task(Of List(Of Object)).Factory.StartNew(Function()
                                                                 Dim WSID As String = Context.QueryString.Get("WSID")
                                                                 Dim user As String = Context.User.Identity.Name
                                                                 Try

                                                                     Dim datareader = RunSPArray("SelSystemPref", WSID, {{"nothing"}})

                                                                     If datareader IsNot Nothing AndAlso datareader.HasRows Then
                                                                         If datareader.Read() Then
                                                                             Dim columns = New List(Of String)
                                                                             For i = 0 To datareader.FieldCount - 1
                                                                                 columns.Add(datareader.GetName(i))
                                                                             Next

                                                                             Dim colors = {"Red", "Green", "Blue"}
                                                                                 Dim filters As New List(Of Object)
                                                                                 For Each color In colors
                                                                                     Dim field = datareader("OS Field Name " + color)
                                                                                     Dim criteria = datareader("OS Text " + color)
                                                                                     filters.Add(New With {color, field, criteria})
                                                                                 Next
                                                                                 Return filters
                                                                         End If
                                                                     End If
                                                                     Return Nothing

                                                                 Catch ex As Exception
                                                                     Debug.WriteLine(ex.Message)
                                                                     insertErrorMessages("PreferencesHub", "getOSFilters", ex.Message, user, WSID)
                                                                     Return Nothing
                                                                 End Try
                                                             End Function)


        End Function
        ''' <summary>
        ''' Sets the OS field filter props for a certain color
        ''' </summary>
        ''' <param name="params">Cell Size to filter on</param>
        ''' <remarks></remarks>
        Public Function setOSFilters(params As List(Of String())) As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function()
                                                         Dim WSID As String = Context.QueryString.Get("WSID")
                                                         Dim user As String = Context.User.Identity.Name
                                                         Try
                                                             Dim SQLparams = {
                                                             {"@RedField", params(0)(1), strVar}, {"@RedCriteria", params(0)(2), strVar},
                                                             {"@GreenField", params(1)(1), strVar}, {"@GreenCriteria", params(1)(2), strVar},
                                                             {"@BlueField", params(2)(1), strVar}, {"@BlueCriteria", params(2)(2), strVar}
                                                             }

                                                             RunActionSP("updateOSFilter", WSID, SQLparams)
                                                             Return True
                                                         Catch ex As Exception
                                                             Debug.WriteLine(ex.Message)
                                                             insertErrorMessages("PreferencesHub", "setOSFilter:" + params.ToString, ex.Message, user, WSID)
                                                             Return False
                                                         End Try

                                                     End Function)

        End Function
    End Class
End Namespace

