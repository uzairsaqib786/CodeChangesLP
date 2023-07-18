' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports PickPro_Web
Namespace Consolidation
    Public Class Preferences
        ''' <summary>
        ''' Selects all the preferences for the consolidation page
        ''' </summary>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>Object containg all the preferences values</returns>
        ''' <remarks></remarks>
        Public Shared Function selectCMPrefs(User As String, WSID As String) As CMPrefrenceModel
            Dim Prefs As CMPrefrenceModel = Nothing
            Dim DataReader As SqlDataReader = Nothing
            Dim index As Integer = 0
            Dim EmailSlip As Integer = 0

            Try
                'hardcoded in till actual wsids are added
                DataReader = RunSPArray("selCMPrefs", WSID, {{"@WSID", WSID, strVar}})

                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            Dim slipCheck = CheckDBNull(DataReader(0))
                            If String.IsNullOrEmpty(slipCheck) Then
                                EmailSlip = 0
                            Else
                                EmailSlip = CInt(slipCheck)
                            End If
                        Else
                            Prefs = New CMPrefrenceModel(EmailSlip,
                                                         CheckDBNull(DataReader("Default Packing List")),
                                                         CheckDBNull(DataReader("Default Lookup Type")),
                                                         CheckDBNull(DataReader("Verify Items")),
                                                         CheckDBNull(DataReader("Blind Verify Items")),
                                                         CheckDBNull(DataReader("Print Verified")),
                                                         CheckDBNull(DataReader("Print UnVerified")),
                                                         CheckDBNull(DataReader("Custom Tote Manifest")),
                                                         CheckDBNull(Of Integer)(DataReader("Auto Comp BO Ship Complete")),
                                                         CheckDBNull(DataReader("Packing List Sort")),
                                                         CheckDBNull(Of Integer)(DataReader("Packing")),
                                                         CheckDBNull(Of Integer)(DataReader("Confirm and Packing")),
                                                         CheckDBNull(Of Integer)(DataReader("Auto Print ContPL")),
                                                         CheckDBNull(Of Integer)(DataReader("Auto Print OrderPL")),
                                                         CheckDBNull(Of Integer)(DataReader("Auto Print ContLabel")),
                                                         CheckDBNull(Of Integer)(DataReader("Enter Container ID")),
                                                         CheckDBNull(DataReader("Container ID Default")),
                                                         CheckDBNull(Of Integer)(DataReader("Confirm and Packing Confirm Quantity")),
                                                         CheckDBNull(Of Integer)(DataReader("Freight")),
                                                         CheckDBNull(Of Integer)(DataReader("Freight1")),
                                                         CheckDBNull(Of Integer)(DataReader("Freight2")),
                                                         CheckDBNull(Of Integer)(DataReader("Weight")),
                                                         CheckDBNull(Of Integer)(DataReader("Length")),
                                                         CheckDBNull(Of Integer)(DataReader("Width")),
                                                         CheckDBNull(Of Integer)(DataReader("Height")),
                                                         CheckDBNull(Of Integer)(DataReader("Cube")),
                                                         CheckDBNull(Of Integer)(DataReader("Shipping")),
                                                         CheckDBNull(DataReader("Stage Non Pickpro Orders")),
                                                         CheckDBNull(DataReader("Validate Staging Locations")))
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                insertErrorMessages("Preferences", "selectCMPrefs", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Dispose()
                End If
            End Try

            Return Prefs
        End Function
    End Class
End Namespace

