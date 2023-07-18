' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Induction
    Public Class Preferences
        ''' <summary>
        ''' Selects all the preferences from the im preferences table for the given wsid
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currenlty logged in</param>
        ''' <returns>An object that contains the values for all the preferences</returns>
        ''' <remarks></remarks>
        Public Shared Function selectIMPreferences(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim prefs As New Object
            Try
                DataReader = RunSPArray("selIMPrefsNew", WSID, {{"@WSID", WSID, strVar}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        prefs = New With {.AutoPickOrderSel = DataReader("Auto Pick Order Selection"), .OrderSort = DataReader("Order Sort"),
                                          .AutoPickTote = DataReader("Auto Pick Tote ID"), .CarTotePicks = DataReader("Carousel Tote ID Picks"),
                                          .OffCarTotePicks = DataReader("Off Carousel Tote ID Picks"), .UseBatchMan = DataReader("Use Pick Batch Manager"),
                                          .UseDefFilter = DataReader("Use Default Filter"), .UseDefZone = DataReader("Use Default Zone"),
                                          .AutoPutTote = DataReader("Auto Put Away Tote ID"), .DefPutPrior = DataReader("Default Put Away Priority"),
                                          .DefPutQuant = DataReader("Default Put Away Quantity"), .PickBatchQuant = DataReader("Pick Batch Quantity"),
                                          .DefCells = DataReader("Default Cells"), .SplitShortPut = DataReader("Split Short Put Away"),
                                          .SelIfOne = DataReader("Select If One"), .PutInductScreen = DataReader("Put Away Induction Screen"),
                                          .ValTotes = DataReader("Validate Totes"), .CarBatchPicks = DataReader("Carousel Batch ID Picks"),
                                          .CarBatchPuts = DataReader("Carousel Batch ID Put Aways"), .OffCarBatchPicks = DataReader("Off Carousel Batch ID Picks"),
                                          .OffCarBatchPuts = DataReader("Off Carousel Batch ID Put Aways"), .AutoForReplen = DataReader("Auto Forward Replenish"),
                                          .CreateItemMast = DataReader("Create Item Master"), .SAPLocTrans = DataReader("SAP Location Transactions"),
                                          .CreatePutAdjusts = DataReader("Create Put Away Adjustments"), .StripScan = DataReader("Strip Scan"),
                                          .StripSide = DataReader("Strip Side"), .StripNum = DataReader("Strip Number"),
                                          .AutoPrintDockLabel = DataReader("Auto Print Cross Dock Label"), .AutoPrintPickLabels = DataReader("Auto Print Pick Labels"),
                                          .PickLabelsOnePer = DataReader("Pick Labels One Per Qty"), .AutoPrintPickTote = DataReader("Auto Print Pick Tote Labels"),
                                          .AutoPrintPutTote = DataReader("Auto Print Put Away Tote Labels"), .AutoPrintOffCarPickList = DataReader("Auto Print Off Carousel Pick List"),
                                          .AutoPrintOffCarPutList = DataReader("Auto Print Off Carousel Put Away List"), .AutoPrintPutLabels = DataReader("Auto Print Put Away Labels"),
                                          .ReqNumPutLabels = DataReader("Request Number Of Put Away Labels"), .MaxNumPutLabels = DataReader("Max Number of Put Away Labels"),
                                          .PrintDirect = DataReader("Print Directly"), .TrackInductTrans = DataReader("Track Induction Transactions"),
                                          .InductLoc = DataReader("Induction Location"), .StageBulkPro = DataReader("Stage Using Bulk Pro"), .StageVelCode = DataReader("Stage Velocity Code"),
                                          .PutScanType = DataReader("Default Put Away Scan Type"), .DefaultSuperBatch = DataReader("Default Super Batch Size"),
                                          .ConfirmSuperBatch = DataReader("Confirm Super Batch"), .SBByToteID = DataReader("Super Batch By Tote ID"),
                                          .UseInZonePickScreen = DataReader("UseInZonePickScreen"), .AutoPrintCaseLabel = DataReader("AutoPrintCaseLabel"),
                                          .ShortMehtod = DataReader("ShortMethod"), .AutoPrintPickBatchList = DataReader("AutoPrintPickBatchList")}
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "selectIMPreferences", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return prefs
        End Function

        ''' <summary>
        ''' selects all the preferences from the rts uder field data table for the given wsid
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The suer that is currently logged in</param>
        ''' <returns>An objectthat contains the values for all the user fields</returns>
        ''' <remarks></remarks>
        Public Shared Function selectRTSUserFieldData(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim UserFields As New Object

            Try
                DataReader = RunSPArray("selRTSUserFieldData", WSID, {{"@WSID", WSID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        UserFields = New With {.User1 = CheckDBNull(DataReader("User Field1")), .User2 = CheckDBNull(DataReader("User Field2")),
                                               .User3 = CheckDBNull(DataReader("User Field3")), .User4 = CheckDBNull(DataReader("User Field4")),
                                               .User5 = CheckDBNull(DataReader("User Field5")), .User6 = CheckDBNull(DataReader("User Field6")),
                                               .User7 = CheckDBNull(DataReader("User Field7")), .User8 = CheckDBNull(DataReader("User Field8")),
                                               .User9 = CheckDBNull(DataReader("User Field9")), .User10 = CheckDBNull(DataReader("User Field10")),
                                               .OrderNumPre = CheckDBNull(DataReader("Order Number Prefix"))}
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("Preferences", "selectRTSUserFieldData", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return UserFields
        End Function

        ''' <summary>
        ''' Selects the reel logic for the given wsid
        ''' </summary>
        ''' <param name="user">user currently logged in </param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>string containing the reel logic</returns>
        ''' <remarks></remarks>
        Public Shared Function selectReelLogic(user As String, WSID As String) As String
            Dim reader As SqlDataReader = Nothing
            Dim logic As String = ""
            Try
                reader = RunSPArray("selIMRTLogic", WSID, {{"nothing"}})
                If reader.HasRows Then
                    If reader.Read Then
                        If Not IsDBNull(reader(0)) Then
                            logic = reader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.Print(ex.ToString())
                insertErrorMessages("Preferences", "selectReelLogic", ex.ToString(), user, WSID)
            Finally
                If Not IsNothing(reader) Then
                    reader.Dispose()
                End If
            End Try
            Return logic
        End Function

    End Class
End Namespace