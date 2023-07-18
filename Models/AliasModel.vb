' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class AliasModel

    Public ReadOnly UserFields As New List(Of String)
    Public ReadOnly ItemNumber As String = ""
    Public ReadOnly UoM As String = ""
    Public Property App As String = ""

    ''' <summary>
    ''' Instantiates a new instance of the AliasModel class
    ''' </summary>
    ''' <param name="user">Requesting user</param>
    ''' <remarks></remarks>
    Public Sub New(user As String, WSID As String)
        Dim DataReader As SqlDataReader = Nothing

        Try
            DataReader = RunSPArray("selColumnAlias", WSID, {{"nothing"}})

            If DataReader.HasRows Then
                If DataReader.Read() Then
                    For x As Integer = 0 To DataReader.FieldCount - 1
                        If x < 10 Then
                            If IsDBNull(DataReader(x)) OrElse DataReader(x).ToString().Trim() = "" Then
                                Me.UserFields.Add("User Field" & (x + 1))
                            Else
                                Me.UserFields.Add(DataReader(x))
                            End If
                        ElseIf x = 10 Then
                            If IsDBNull(DataReader(x)) OrElse DataReader(x).ToString().Trim() = "" Then
                                Me.ItemNumber = "Item Number"
                            Else
                                Me.ItemNumber = DataReader(x).ToString().Trim()
                            End If
                        ElseIf x = 11 Then
                            If IsDBNull(DataReader(x)) OrElse DataReader(x).ToString().Trim() = "" Then
                                Me.UoM = "Unit of Measure"
                            Else
                                Me.UoM = DataReader(x).ToString().Trim()
                            End If
                        End If
                    Next

                End If
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("AliasModel", "getUserFieldAlias", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then DataReader.Dispose()
        End Try
    End Sub
End Class
