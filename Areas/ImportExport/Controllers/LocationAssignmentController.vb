' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc
Imports PickPro_Web.ImportExport

Namespace Controllers
    Public Class LocationAssignmentController
        Inherits Controller

        Private Const MinOffset = 1

        ' init at thirty minutes ago
        Private Shared LastLogDate As Date = Now().Subtract(New TimeSpan(0, MinOffset, 0))
        Private Shared LastPickLogDate As Date = Now().Subtract(New TimeSpan(0, MinOffset, 0))
        Private Shared LastPutLogDate As Date = Now().Subtract(New TimeSpan(0, MinOffset, 0))
        Private Shared LastCountLogDate As Date = Now().Subtract(New TimeSpan(0, MinOffset, 0))

        Function ExecLA() As ActionResult
            Dim ieWSID = Config.getIEWSID()
            Dim pref = ImportExportLogic.GetPreferences(ieWSID)
            Dim loc As Integer = 0
            Try

                ' only log every thirty minutes to see if it's still up
                If Now().Subtract(LastLogDate).TotalMinutes > MinOffset Then
                    RunSP("insEventLog", ieWSID, {
                  {"@EVENTTYPE", "AutoLA", strVar},
                  {"@EVENTCODE", 0, intVar},
                  {"@EVENTMSG", "Starting auto-location-assignment", strVar},
                  {"@EVENTLOC", "LocationAssignmentController", strVar},
                  {"@EVENTPROC", "ExecLA", strVar},
                  {"@EVENTLINE", "", strVar},
                  {"@EVENTID", 42, intVar}
                  })
                    LastLogDate = Now()
                End If

                Dim laProc = Sub()
                                 Try
                                     loc = 1
                                     Dim autoPicks As Boolean = pref("AutoLocPicks")
                                     loc = 2
                                     Dim autoPuts As Boolean = pref("AutoLocPutAways")
                                     loc = 3
                                     Dim autoCounts As Boolean = pref("AutoLocCounts")
                                     loc = 4
                                     If autoPicks Then
                                         loc = 5
                                         AssignPicksSync(ieWSID, "AutoLocAssign")
                                         loc = 6
                                     End If
                                     If autoPuts Then
                                         loc = 7
                                         AssignPutAwaysSync(ieWSID, "AutoLocAssign")
                                         loc = 8
                                     End If
                                     If autoCounts Then
                                         loc = 9
                                         AssignCountsSync(ieWSID, "AutoLocAssign")
                                         loc = 10
                                     End If
                                 Catch ex As Exception
                                     insertErrorMessages("LocationAssignmentController", "ExecLA: loc = " + loc.ToString, ex.ToString(), "AutoLA", ieWSID)
                                 End Try
                             End Sub
                loc = 11
                Dim laThread = New Threading.Thread(laProc)
                loc = 12
                laThread.Start()
            Catch ex As Exception
                insertErrorMessages("LocationAssignmentController", "ExecLA2: loc = " + loc.ToString, ex.ToString(), "AutoLA", ieWSID)
            End Try
            Return New HttpStatusCodeResult(Net.HttpStatusCode.OK)
        End Function

        Function ExecPickLA() As ActionResult
            Dim ieWSID = Config.getIEWSID()
            Try
                ' only log every thirty minutes to see if it's still up
                'If Now().Subtract(LastPickLogDate).TotalMinutes > MinOffset Then
                '    RunSP("insEventLog", ieWSID, {
                '  {"@EVENTTYPE", "AutoLA", strVar},
                '  {"@EVENTCODE", 0, intVar},
                '  {"@EVENTMSG", "Starting auto-location-assignment", strVar},
                '  {"@EVENTLOC", "LocationAssignmentController", strVar},
                '  {"@EVENTPROC", "ExecPickLA", strVar},
                '  {"@EVENTLINE", "", strVar},
                '  {"@EVENTID", 42, intVar}
                '  })
                '    LastPickLogDate = Now()
                'End If

                Dim laProc = Sub()
                                 AssignPicksSync(ieWSID, "AutoLocAssign")
                             End Sub
                Dim laThread = New Threading.Thread(laProc)
                laThread.Start()
            Catch ex As Exception
                insertErrorMessages("LocationAssignmentController", "ExecPickLA", ex.ToString(), "AutoLA", ieWSID)
            End Try
            Return New HttpStatusCodeResult(Net.HttpStatusCode.OK)
        End Function

        Function ExecPutLA() As ActionResult
            Dim ieWSID = Config.getIEWSID()
            Try
                ' only log every thirty minutes to see if it's still up
                'If Now().Subtract(LastPutLogDate).TotalMinutes > MinOffset Then
                '    RunSP("insEventLog", ieWSID, {
                '  {"@EVENTTYPE", "AutoLA", strVar},
                '  {"@EVENTCODE", 0, intVar},
                '  {"@EVENTMSG", "Starting auto-location-assignment", strVar},
                '  {"@EVENTLOC", "LocationAssignmentController", strVar},
                '  {"@EVENTPROC", "ExecPutLA", strVar},
                '  {"@EVENTLINE", "", strVar},
                '  {"@EVENTID", 42, intVar}
                '  })
                '    LastPutLogDate = Now()
                'End If

                Dim laProc = Sub()
                                 AssignPutAwaysSync(ieWSID, "AutoLocAssign")
                             End Sub
                Dim laThread = New Threading.Thread(laProc)
                laThread.Start()
            Catch ex As Exception
                insertErrorMessages("LocationAssignmentController", "ExecPutLA", ex.ToString(), "AutoLA", ieWSID)
            End Try
            Return New HttpStatusCodeResult(Net.HttpStatusCode.OK)
        End Function

        Function ExecCountLA() As ActionResult
            Dim ieWSID = Config.getIEWSID()
            Try
                ' only log every thirty minutes to see if it's still up
                'If Now().Subtract(LastCountLogDate).TotalMinutes > MinOffset Then
                '    RunSP("insEventLog", ieWSID, {
                '  {"@EVENTTYPE", "AutoLA", strVar},
                '  {"@EVENTCODE", 0, intVar},
                '  {"@EVENTMSG", "Starting auto-location-assignment", strVar},
                '  {"@EVENTLOC", "LocationAssignmentController", strVar},
                '  {"@EVENTPROC", "ExecCountLA", strVar},
                '  {"@EVENTLINE", "", strVar},
                '  {"@EVENTID", 42, intVar}
                '  })
                '    LastCountLogDate = Now()
                'End If

                Dim laProc = Sub()
                                 AssignCountsSync(ieWSID, "AutoLocAssign")
                             End Sub
                Dim laThread = New Threading.Thread(laProc)
                laThread.Start()
            Catch ex As Exception
                insertErrorMessages("LocationAssignmentController", "ExecCountLA", ex.ToString(), "AutoLA", ieWSID)
            End Try
            Return New HttpStatusCodeResult(Net.HttpStatusCode.OK)
        End Function

    End Class
End Namespace