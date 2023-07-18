' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class UserFields

    ''' <summary>
    ''' Gets the user fields 1-10 from xfer open transactions (manual transactions table)
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="mt">Transaction ID of desired entry</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>User field values for manual transaction order number.</returns>
    ''' <remarks></remarks>
    Public Shared Function getUserFieldsMT(user As String, mt As Integer, WSID As String) As List(Of String)
        Dim DataReader As SqlDataReader = Nothing
        Dim result As New List(Of String)

        Try
            DataReader = RunSPArray("selUserFieldsMT", WSID, {{"@Transaction", mt, intVar}})

            If DataReader.HasRows Then
                If DataReader.Read() Then
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        result.Add(IIf(IsDBNull(DataReader(x)), "", DataReader(x)))
                    Next
                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("UserFields", "getUserFields", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Updates User fields
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <param name="userfields">User Field values</param>
    ''' <param name="transaction">Transaction ID</param>
    ''' <param name="WSID">The workstation that being worked on</param>
    ''' <remarks></remarks>
    Public Shared Sub saveUserFieldsMT(user As String, userfields As List(Of String), transaction As Integer, WSID As String)
        Try
            RunActionSP("updateUserFieldsMT", WSID, {{"@Transaction", transaction, intVar}, _
                             {"@UF1", userfields(0), strVar}, _
                             {"@UF2", userfields(1), strVar}, _
                             {"@UF3", userfields(2), strVar}, _
                             {"@UF4", userfields(3), strVar}, _
                             {"@UF5", userfields(4), strVar}, _
                             {"@UF6", userfields(5), strVar}, _
                             {"@UF7", userfields(6), strVar}, _
                             {"@UF8", userfields(7), strVar}, _
                             {"@UF9", userfields(8), strVar}, _
                             {"@UF10", userfields(9), strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("UserFields", "saveUserFields", ex.ToString(), user, WSID)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the user field 1/2 typeahead
    ''' </summary>
    ''' <param name="query">Valuet to get suggestions for</param>
    ''' <param name="ufs">User field 1/2 to get</param>
    ''' <param name="user">Requesting user</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A list of string that contains the typahead info for the user fields</returns>
    ''' <remarks></remarks>
    Public Shared Function getTypeahead(query As String, ufs As Integer, user As String, WSID As String) As List(Of String)
        Dim typeahead As New List(Of String)
        Dim datareader As SqlDataReader = Nothing

        Try
            datareader = RunSPArray("selUserFieldTypeahead", WSID, {{"@Value", query & "%", strVar}, {"@UF", ufs, intVar}})

            If datareader.HasRows Then
                While datareader.Read()
                    If Not IsDBNull(datareader(0)) Then
                        typeahead.Add(datareader(0))
                    End If
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("UserFields", "getTypeahead", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(datareader) Then
                datareader.Dispose()
            End If
        End Try

        Return typeahead
    End Function
End Class
