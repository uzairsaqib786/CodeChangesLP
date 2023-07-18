' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Induction
    Public Class ToteManager

        ''' <summary>
        ''' Selects all totes in orde rto populate modal
        ''' </summary>
        ''' <param name="User">user currently logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>list of object containg the tote, cel, and if it is allocated</returns>
        ''' <remarks></remarks>
        Public Shared Function selectTotes(User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim Totes As New List(Of Object)

            Try
                DataReader = RunSPArray("selTotes", WSID, {{"nothing"}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Totes.Add(New With {.ToteID = DataReader(0), .Cell = DataReader(1), .Allocated = (CInt(DataReader(2)) = 1)})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ToteManager", "selectTotes", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return Totes
        End Function

        ''' <summary>
        ''' Getsa the typeahead datat for the from tote id typeahead
        ''' </summary>
        ''' <param name="ToteID">the value entered in the typeahead</param>
        ''' <param name="User">user that is logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>list of object containing all the tote ids and cells</returns>
        ''' <remarks></remarks>
        Public Shared Function selFromToteID(ToteID As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim Totes As New List(Of Object)

            Try
                DataReader = RunSPArray("selTotesFromTA", WSID, {{"@ToteID", ToteID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Totes.Add(New With {.ToteID = DataReader(0), .Cell = DataReader(1)})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ToteManager", "selFromToteID", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return Totes
        End Function

        ''' <summary>
        ''' Selects the typeahead datata for the to tote id typeahead
        ''' </summary>
        ''' <param name="ToteID"> The vlaue entered in the typeahead</param>
        ''' <param name="FromToteID">The vlaue within the from tote id field</param>
        ''' <param name="User">user logged in</param>
        ''' <param name="WSID">workstation id</param>
        ''' <returns>list of object containing all the tote ids and cells</returns>
        ''' <remarks></remarks>
        Public Shared Function selToToteID(ToteID As String, FromToteID As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim Totes As New List(Of Object)

            Try
                DataReader = RunSPArray("selTotesToTA", WSID, {{"@ToteID", ToteID, strVar}, {"@FromToteID", FromToteID, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Totes.Add(New With {.ToteID = DataReader(0), .Cell = DataReader(1)})
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.ToString())
                insertErrorMessages("ToteManager", "selToToteID", ex.ToString(), User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return Totes
        End Function
    End Class
End Namespace
