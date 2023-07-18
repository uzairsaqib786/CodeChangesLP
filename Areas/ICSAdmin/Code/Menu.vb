' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Admin
    Public Class Menu
        ''' <summary>
        ''' Gathers the information for the allocated transaction menu table 
        ''' </summary>
        ''' <param name="user">The current suer that is signed in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A list containg all the tbale information</returns>
        ''' <remarks></remarks>
        Public Shared Function getMenuData(user As String, WSID As String) As Object
            Dim innerResult As New List(Of String), result As New List(Of List(Of String)), DataReader As SqlDataReader = Nothing

            Try
                ' first result is the table's data, next x results are count data.
                DataReader = RunSPArray("selMenuData", WSID, {{"nothing"}})
                ' refactored from while loop, could have caused issue with the first resultset being empty and the others not
                For resultSet As Integer = 0 To 9
                    If DataReader.HasRows Then
                        While DataReader.Read()
                            For x As Integer = 0 To DataReader.FieldCount - 1
                                If Not IsDBNull(DataReader(x)) Then
                                    innerResult.Add(DataReader(x))
                                Else
                                    innerResult.Add("")
                                End If
                            Next
                            If resultSet = 0 Then
                                result.Add(innerResult)
                                innerResult = New List(Of String)
                            End If
                        End While
                    End If
                    DataReader.NextResult()
                Next
            Catch ex As Exception
                insertErrorMessages("Main Menu", "allocatedTransactionsTable", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return New With {.table = result, .counts = innerResult}
        End Function
        ''' <summary>
        ''' Grbas the pod ID that the workstation is currently hooked to
        ''' </summary>
        ''' <param name="user">The user who is signed in</param>
        ''' <param name="WSID">The workstation id </param>
        ''' <returns>The pod ID assciatted with the worksattion id</returns>
        ''' <remarks></remarks>
        Public Shared Function getPOD(user As String, WSID As String) As String
            Dim DataReader As SqlDataReader = Nothing
            Dim returnVal As String = "-1"
            Try
                DataReader = RunSPArray("selWSPrefsPodID", WSID, {{"@WSID", WSID, strVar}})
                If DataReader.HasRows Then
                    If DataReader.Read() Then
                        If Not IsDBNull(DataReader(0)) Then
                            returnVal = DataReader(0)
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("MenuHub", "getPOD", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            If returnVal.Length = 1 Then
                returnVal = "0" + returnVal
            End If
            Return returnVal
        End Function
        ''' <summary>
        ''' sees if the user is currently in diagnostic mode
        ''' </summary>
        ''' <param name="user">current user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>True if the the user is currently in diagnostic mode, fasle if they are not</returns>
        ''' <remarks></remarks>
        Public Shared Function getDiagMode(user As String, WSID As String) As Boolean
            Dim DataReader As SqlDataReader = Nothing
            Try
                DataReader = RunSPArray("selDiagMode", WSID, {{"@User", user, strVar}})
                If DataReader.HasRows Then
                    If DataReader.Read() Then
                        Return CBool(DataReader(0))
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Menu", "getDiagMode", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return False
        End Function
        ''' <summary>
        ''' Determines if the user is currently working 
        ''' </summary>
        ''' <param name="user">The user that is currenlty logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A string either saying Start Work, or End Work depending on if the user is currenlt not working or still working</returns>
        ''' <remarks></remarks>
        Public Shared Function inWorkMode(user As String, WSID As String) As String
            Dim DataReader As SqlDataReader = Nothing
            Dim returnVal As String = "End Work"
            Try
                DataReader = RunSPArray("selUserStatsWorking", WSID, {{"@User", user, strVar}})
                If DataReader.HasRows Then
                    If DataReader.Read() Then
                        If DataReader(0) = "Not Working" Then
                            returnVal = "Start Work"
                        End If
                    End If
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Menu", "inWorkMode", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return returnVal
        End Function

        ''' <summary>
        ''' Determines if the workstation/user combination logged in is a pick workstation or not.
        ''' </summary>
        ''' <param name="user">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>True if the workstation specified is a pick workstation.</returns>
        ''' <remarks></remarks>
        Public Shared Function grabPickWorkstation(user As String, WSID As String) As Boolean
            Dim DataReader As SqlDataReader = Nothing, PickWorkstat As Boolean = True

            Try
                DataReader = RunSPArray("selWSPrefsCarWS", WSID, {{"@WSID", WSID, strVar}})
                If DataReader.HasRows Then
                    PickWorkstat = True
                Else
                    PickWorkstat = False
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Menu", " grabPickWorkstation", ex.Message, user, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return PickWorkstat
        End Function
    End Class
End Namespace

