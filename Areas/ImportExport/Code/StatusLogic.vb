' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports PickPro_Web.SQLHelpers

Namespace ImportExport
    Module StatusLogic
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="wsid"></param>
        ''' <returns></returns>
        Public Function GetStatus(wsid As String) As Dictionary(Of String, Object)
            Dim ret As Dictionary(Of String, Object) = Nothing
            Try
                Dim importCount = GetResultSingleCol("SelImportTransactionStatus", "IE")
                ret = GetResultMap("SelImportTransactionStatus", wsid)
                ret.Add("Import Count", importCount)
            Catch ex As Exception
                insertErrorMessages("StatusLogic", "GetStatus", ex.ToString(), "", wsid)
            End Try
            Return ret
        End Function
    End Module
End Namespace
