' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace Consolidation
    Public Class Shipping
        ''' <summary>
        ''' Selects the shipping data for the given order number
        ''' </summary>
        ''' <param name="orderNum">The displayed order number</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>List of object where each object contains the information about a shipping record (every object is a different record)</returns>
        ''' <remarks></remarks>
        Public Shared Function selShippingData(orderNum As String, User As String, WSID As String) As List(Of Object)
            Dim DataReader As SqlDataReader = Nothing
            Dim Data As New List(Of Object)

            Try
                DataReader = RunSPArray("selShippingData", WSID, {{"@orderNum", orderNum, strVar}, _
                                                                   {"@user", User, strVar}})

                If DataReader.HasRows Then
                    While DataReader.Read()
                        Data.Add(New With {.ID = CheckDBNull(DataReader("ID")), .ContainerID = CheckDBNull(DataReader("Container ID")), _
                                           .Carrier = CheckDBNull(DataReader("Carrier Name")), .TrackingNum = CheckDBNull(DataReader("Tracking Number")), _
                                           .Freight = CheckMoney(DataReader("Freight")), .Freight1 = CheckMoney(DataReader("Freight1")), _
                                           .Freight2 = CheckMoney(DataReader("Freight2")), .Weight = CheckDBNull(DataReader("Weight")), _
                                           .Length = CheckDBNull(DataReader("Length")), .Width = CheckDBNull(DataReader("Width")),
                                           .Height = CheckDBNull(DataReader("Height")), .Cube = CheckDBNull(DataReader("Cube"))})
                    End While
                End If


            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Shipping", "selShippingData", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Data
        End Function
        ''' <summary>
        ''' Tells if the order number is shipping complete
        ''' </summary>
        ''' <param name="orderNum">The displayed order number</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>Boolean telling if the order number is shipping complete</returns>
        ''' <remarks></remarks>
        Public Shared Function selShippingComp(orderNum As String, User As String, WSID As String) As Boolean
            Dim DataReader As SqlDataReader = Nothing
            Dim comp As Integer = -2

            Try
                DataReader = RunSPArray("selShippingComplete", WSID, {{"@orderNum", orderNum, strVar}})

                If DataReader.HasRows Then
                    DataReader.Read()
                    comp = DataReader(0)
                End If


            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Shipping", "selShippingData", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            If comp = 1 Then
                Return True
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Selects the carriers for the carrier dropdown
        ''' </summary>
        ''' <param name="User">User that is currently logged in</param>
        ''' <param name="WSID">Workstation that is being worked on</param>
        ''' <returns>a list containg all the carriers</returns>
        ''' <remarks></remarks>
        Public Shared Function selCarriers(User As String, WSID As String) As List(Of String)
            Dim DataReader As SqlDataReader = Nothing
            Dim Carriers As New List(Of String)

            Try
                DataReader = RunSPArray("selCarriers", WSID, {{"nothing"}})
                If DataReader.HasRows Then
                    While DataReader.Read()
                        Carriers.Add(CheckDBNull(DataReader(0)))
                    End While
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Shipping", "selCarriers", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try
            Return Carriers
        End Function
        ''' <summary>
        ''' Selects the preferences that are associated with the shipping page
        ''' </summary>
        ''' <param name="wsid">The workstation that is being worked on</param>
        ''' <param name="user">The suer that is logged in</param>
        ''' <returns>object containing the values of the desired preferences</returns>
        ''' <remarks></remarks>
        Public Shared Function selShippingPreferences(wsid As String, user As String) As Object
            Dim dataReader As SqlDataReader = Nothing
            Dim prefs As Object = Nothing
            Try
                dataReader = RunSPArray("selShipPrefs", wsid, {{"@wsid", wsid, strVar}})
                If dataReader.HasRows Then
                    While dataReader.Read()
                        prefs = New With {.Freight = CheckDBNull(dataReader("Freight")), .Freight1 = CheckDBNull(dataReader("Freight1")), _
                        .Freight2 = CheckDBNull(dataReader("Freight2")), .Weight = CheckDBNull(dataReader("Weight")), _
                        .Length = CheckDBNull(dataReader("Length")), .Width = CheckDBNull(dataReader("Width")),
                        .Height = CheckDBNull(dataReader("Height")), .Cube = CheckDBNull(dataReader("Cube"))}
                    End While
                End If
            Catch ex As Exception
                insertErrorMessages("Shipping", "selShippingPreferences", ex.Message, user, wsid)
            Finally
                If Not IsNothing(dataReader) Then
                    dataReader.Close()
                End If
            End Try
            Return prefs
        End Function


    End Class
End Namespace

