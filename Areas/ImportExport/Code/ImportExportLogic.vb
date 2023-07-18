' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module ImportExportLogic
        ''' <summary>
        ''' Gets the Import Export Preferences for the given wsid
        ''' </summary>
        ''' <param name="wsid">The workstation that is currently being worked on</param>
        ''' <returns>A dictionary that contaisn the preferences and thier values</returns>
        Public Function GetPreferences(ByVal wsid As String) As Dictionary(Of String, Object)
            Dim counter = 0
            Try
                Dim ret = GetResultMap("SelXferPref", "IE")
                counter += 1
                Dim xferTransType = GetResultList("SelXferTransType", "IE")
                counter += 1
                ret.Add("Export Trans Types", xferTransType)
                counter += 1

                Dim sysPref = GetResultMap("SelSystemPref", wsid)
                counter += 1
                sysPref.Remove("ID")
                counter += 1
                Try
                    ret = ret.Concat(sysPref).ToDictionary(Function(k) k.Key, Function(v) v.Value)
                Catch ex As Exception
                    Dim keyDup = ""
                    For Each k In ret.Keys
                        If sysPref.ContainsKey(k) Then
                            keyDup &= k
                        End If
                    Next
                    insertErrorMessages("IE", "GetPreferencesConcat", keyDup, "IEPref", "")
                    Throw
                End Try
                counter += 1

                Dim cellSize = GetResultList("SelCellSizes", "IE")
                counter += 1
                Dim goldenZoneTypes = GetResultList("SelGoldenZoneTypes", "IE")

                counter += 1
                ret("Cell Sizes") = cellSize
                counter += 1
                ret("Golden Zone Types") = goldenZoneTypes

                counter += 1
                ret("Transfer Field Mapping") = GetResultMapList("selIEXferFieldMapTransTypes", "IE")

                Return ret
            Catch ex As Exception
                insertErrorMessages("IE", "GetPreferences", counter & " " & ex.ToString(), "IEPref", wsid)
                Throw
            End Try
        End Function
    End Module
End Namespace

