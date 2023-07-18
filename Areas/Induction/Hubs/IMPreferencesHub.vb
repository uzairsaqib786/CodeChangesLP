' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports PickPro_Web.Certificates

Public Class IMPreferencesHub
    Inherits Hub
    ''' <summary>
    ''' Gets the name of the computer being used
    ''' </summary>
    ''' <returns>A string that tells what the computer's name is</returns>
    ''' <remarks></remarks>
    Public Function getCompName() As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim CompName As String = ""
                                                    Try
                                                        CompName = getPCName(Context.QueryString.Get("WSID"))
                                                    Catch ex As Exception
                                                        CompName = "Error"
                                                        insertErrorMessages("IMPreferencesHub", "getCompName", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return CompName
                                                End Function)
    End Function

    ''' <summary>
    ''' updates the preferences under the system settings tab
    ''' </summary>
    ''' All the parameters below are the preferences that are getting updated
    ''' <param name="AutoPickOrder"></param>
    ''' <param name="OrderSort"></param>
    ''' <param name="AutoPickTote"></param>
    ''' <param name="CarTotePicks"></param>
    ''' <param name="OffCarTotePicks"></param>
    ''' <param name="UsePickBatch"></param>
    ''' <param name="UseDefFilter"></param>
    ''' <param name="UseDefZone"></param>
    ''' <param name="AutoPutTote"></param>
    ''' <param name="DefPutPrior"></param>
    ''' <param name="DefPutQuant"></param>
    ''' <param name="PickBatchQuant"></param>
    ''' <param name="DefCells"></param>
    ''' <param name="SplitShortPut"></param>
    ''' <param name="SelIfOne"></param>
    ''' <param name="PutInductScreen"></param>
    ''' <param name="ValTote"></param>
    ''' <param name="CarBatchPicks"></param>
    ''' <param name="CarBatchPuts"></param>
    ''' <param name="OffCarBatchPicks"></param>
    ''' <param name="OffCarBatchPuts"></param>
    ''' <param name="AutoForReplen"></param>
    ''' <param name="CreateItemMast"></param>
    ''' <param name="SAPLocTrans"></param>
    ''' <param name="CreatePutAdjusts"></param>
    ''' <param name="StripScan"></param>
    ''' <param name="StripSide"></param>
    ''' <param name="StripNum"></param>
    ''' <returns>A string that tells if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updIMSytemSettings(AutoPickOrder As Boolean, OrderSort As String, AutoPickTote As Boolean, CarTotePicks As Boolean,
                                       OffCarTotePicks As Boolean, UsePickBatch As Boolean, UseDefFilter As Boolean, UseDefZone As Boolean,
                                       AutoPutTote As Boolean, DefPutPrior As Integer, DefPutQuant As Integer, PickBatchQuant As Integer,
                                       DefCells As Integer, SplitShortPut As Boolean, SelIfOne As Boolean, PutInductScreen As String,
                                       ValTote As Boolean, CarBatchPicks As Boolean, CarBatchPuts As Boolean, OffCarBatchPicks As Boolean,
                                       OffCarBatchPuts As Boolean, AutoForReplen As Boolean, CreateItemMast As Boolean, SAPLocTrans As Boolean,
                                       CreatePutAdjusts As Boolean, StripScan As Boolean, StripSide As String, StripNum As Integer, PutScan As String,
                                       UseInZonePickScreen As Boolean, AutoPrintCaseLabel As Boolean, ShortMethod As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updIMPrefsSystSett", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                            {"@AutoPickOrder", AutoPickOrder, boolVar},
                                                                                                                            {"@OrderSort", OrderSort, strVar},
                                                                                                                            {"@AutoPickTote", AutoPickTote, boolVar},
                                                                                                                            {"@CarTotePicks", CarTotePicks, boolVar},
                                                                                                                            {"@OffCarTotePicks", OffCarTotePicks, boolVar},
                                                                                                                            {"@UsePickBatch", UsePickBatch, boolVar},
                                                                                                                            {"@UseDefFilter", UseDefFilter, boolVar},
                                                                                                                            {"@UseDefZone", UseDefZone, boolVar},
                                                                                                                            {"@AutoPutTote", AutoPutTote, boolVar},
                                                                                                                            {"@DefPutPrior", DefPutPrior, intVar},
                                                                                                                            {"@DefPutQuant", DefPutQuant, intVar},
                                                                                                                            {"@PickBatchQuant", PickBatchQuant, intVar},
                                                                                                                            {"@DefCells", DefCells, intVar},
                                                                                                                            {"@SplitShortPut", SplitShortPut, boolVar},
                                                                                                                            {"@SelIfOne", SelIfOne, boolVar},
                                                                                                                            {"@PutInductScreen", PutInductScreen, strVar},
                                                                                                                            {"@ValTote", ValTote, boolVar},
                                                                                                                            {"@CarBatchPicks", CarBatchPicks, boolVar},
                                                                                                                            {"@CarBatchPuts", CarBatchPuts, boolVar},
                                                                                                                            {"@OffCarBatchPicks", OffCarBatchPicks, boolVar},
                                                                                                                            {"@OffCarBatchPuts", OffCarBatchPuts, boolVar},
                                                                                                                            {"@AutoForReplen", AutoForReplen, boolVar},
                                                                                                                            {"@CreateItemMast", CreateItemMast, boolVar},
                                                                                                                            {"@SAPLocTrans", SAPLocTrans, boolVar},
                                                                                                                            {"@CreatePutAdjusts", CreatePutAdjusts, boolVar},
                                                                                                                            {"@StripScan", StripScan, boolVar},
                                                                                                                            {"@StripSide", StripSide, strVar},
                                                                                                                            {"@StripNum", StripNum, intVar},
                                                                                                                            {"@PutScan", PutScan, strVar},
                                                                                                                            {"@UseInZonePickScreen", UseInZonePickScreen, boolVar},
                                                                                                                            {"@AutoPrintCaseLabel", AutoPrintCaseLabel, boolVar},
                                                                                                                            {"@ShortMethod", ShortMethod, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("IMPreferencesHub", "updIMSytemSettings", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' updates the preferences under the print settings tab
    ''' </summary>
    ''' All the parameters below are the preferences that are getting updated
    ''' <param name="AutoPrintCross"></param>
    ''' <param name="AutoPrintPickLabs"></param>
    ''' <param name="PickLabsOnePer"></param>
    ''' <param name="AutoPrintPickToteLabs"></param>
    ''' <param name="AutoPrintPutToteLabs"></param>
    ''' <param name="AutoPrintOffCarPickList"></param>
    ''' <param name="AutoPrintOffCarPutList"></param>
    ''' <param name="AutoPrintPutLabs"></param>
    ''' <param name="ReqNumPutLabs"></param>
    ''' <param name="MaxNumPutLabs"></param>
    ''' <param name="PrintDirect"></param>
    ''' <returns>A string that tells if the oeration was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updIMPrintSettings(AutoPrintCross As Boolean, AutoPrintPickLabs As Boolean, PickLabsOnePer As Boolean,
                                       AutoPrintPickToteLabs As Boolean, AutoPrintPutToteLabs As Boolean, AutoPrintOffCarPickList As Boolean,
                                       AutoPrintOffCarPutList As Boolean, AutoPrintPutLabs As Boolean, ReqNumPutLabs As Boolean,
                                       MaxNumPutLabs As Integer, PrintDirect As Boolean, AutoPrintPickBatchList As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updIMPrefsPrintSett", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                             {"@AutoPrintCross", AutoPrintCross, boolVar},
                                                                                                                             {"@AutoPrintPickLabs", AutoPrintPickLabs, boolVar},
                                                                                                                             {"@PickLabsOnePer", PickLabsOnePer, boolVar},
                                                                                                                             {"@AutoPrintPickToteLabs", AutoPrintPickToteLabs, boolVar},
                                                                                                                             {"@AutoPrintPutToteLabs", AutoPrintPutToteLabs, boolVar},
                                                                                                                             {"@AutoPrintOffCarPickList", AutoPrintOffCarPickList, boolVar},
                                                                                                                             {"@AutoPrintOffCarPutList", AutoPrintOffCarPutList, boolVar},
                                                                                                                             {"@AutoPrintPutLabs", AutoPrintPutLabs, boolVar},
                                                                                                                             {"@ReqNumPutLabs", ReqNumPutLabs, boolVar},
                                                                                                                             {"@MaxNumPutLabs", MaxNumPutLabs, intVar},
                                                                                                                             {"@PrintDirect", PrintDirect, boolVar},
                                                                                                                             {"@AutoPrintPickBatchList", AutoPrintPickBatchList, boolVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("IMPreferencesHub", "updIMPrintSettings", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' updates the preferences under the misc setup tab
    ''' </summary>
    ''' All the parameters below are the preferences that are getting updated
    ''' <param name="TrackInductTrans"></param>
    ''' <param name="InductLoc"></param>
    ''' <param name="StageBulkPro"></param>
    ''' <param name="StageVelCode"></param>
    ''' <returns>A string that tells if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updIMMiscSetup(TrackInductTrans As Boolean, InductLoc As String, StageBulkPro As Boolean, StageVelCode As String, DefaultSuperBatch As Integer, ConfirmSuperBatch As Boolean, superBatchFilt As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updIMPrefsMiscSetupNew", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                             {"@TrackInductTrans", TrackInductTrans, boolVar},
                                                                                                                             {"@InductLoc", InductLoc, strVar},
                                                                                                                             {"@StageBulkPro", StageBulkPro, boolVar},
                                                                                                                             {"@StageVelCode", StageVelCode, strVar},
                                                                                                                             {"@DefaultSuperBatch", DefaultSuperBatch, intVar},
                                                                                                                             {"@ConfirmSuperBatch", ConfirmSuperBatch, boolVar},
                                                                                                                             {"@superBatchFilt", superBatchFilt, boolVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("IMPreferencesHub", "updIMMiscSetup", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

    ''' <summary>
    ''' updates the preferences under the reel tracking tab
    ''' </summary>
    ''' All the parameters below are the preferences that are getting updated
    ''' <param name="User1"></param>
    ''' <param name="User2"></param>
    ''' <param name="User3"></param>
    ''' <param name="User4"></param>
    ''' <param name="User5"></param>
    ''' <param name="User6"></param>
    ''' <param name="User7"></param>
    ''' <param name="User8"></param>
    ''' <param name="User9"></param>
    ''' <param name="User10"></param>
    ''' <param name="OrderNumPre"></param>
    ''' <returns>A string that tells if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updRTSUserData(User1 As String, User2 As String, User3 As String, User4 As String, User5 As String, User6 As String, _
                                   User7 As String, User8 As String, User9 As String, User10 As String, OrderNumPre As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Try
                                                        RunActionSP("updRTSUserFieldData", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, _
                                                                                                                             {"@User1", User1, strVar}, _
                                                                                                                             {"@User2", User2, strVar}, _
                                                                                                                             {"@User3", User3, strVar}, _
                                                                                                                             {"@User4", User4, strVar}, _
                                                                                                                             {"@User5", User5, strVar}, _
                                                                                                                             {"@User6", User6, strVar}, _
                                                                                                                             {"@User7", User7, strVar}, _
                                                                                                                             {"@User8", User8, strVar}, _
                                                                                                                             {"@User9", User9, strVar}, _
                                                                                                                             {"@User10", User10, strVar}, _
                                                                                                                             {"@OrderNumPre", OrderNumPre, strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        insertErrorMessages("IMPreferencesHub", "updRTSUserData", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function

End Class
