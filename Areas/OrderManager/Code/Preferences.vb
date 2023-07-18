' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Namespace OrderManager
    Public Class Preferences
        ''' <summary>
        ''' Selects the order manager preferences for the construction of the model
        ''' </summary>
        ''' <param name="WSID">The workstation that is currently being worked on</param>
        ''' <param name="User">The user that is currently logged in</param>
        ''' <returns>Object containing all the order manager prferences</returns>
        ''' <remarks></remarks>
        Public Shared Function selectOMPreferences(WSID As String, User As String) As Object
            Dim DataReader As SqlDataReader = Nothing
            Dim index As Integer = 0
            Dim MaxOrders As Integer = 0
            Dim AllowInProc As Boolean = False
            Dim AllowPartRel As Boolean = False
            Dim DefUserFields As Boolean = False
            Dim CustomReport As String = ""
            Dim CustomAdmin As String = ""
            Dim CustomAdminText As String = ""
            Dim PrintDirect As Boolean = False

            Try
                DataReader = RunSPArray("selOMPrefs", WSID, {{"@WSID", WSID, strVar}})

                While DataReader.HasRows
                    While DataReader.Read()
                        If index = 0 Then
                            MaxOrders = DataReader("Max Orders")
                            AllowInProc = DataReader("Allow in Process")
                            AllowPartRel = DataReader("Allow Partial Release")
                            DefUserFields = DataReader("Default User Fields")
                            PrintDirect = DataReader("Print Directly")
                        ElseIf index = 1 Then
                            CustomReport = CheckDBNull(DataReader(0))
                        ElseIf index = 2 Then
                            CustomAdmin = CheckDBNull(DataReader(0))
                        ElseIf index = 3 Then
                            CustomAdminText = CheckDBNull(DataReader(0))
                        End If
                    End While
                    index += 1
                    DataReader.NextResult()
                End While
            Catch ex As Exception
                insertErrorMessages("OrderManager Preferences", "selectOMPreferences", ex.Message, User, WSID)
            Finally
                If Not IsNothing(DataReader) Then
                    DataReader.Close()
                End If
            End Try
            Return New OMPreferenceModel(MaxOrders, AllowInProc, AllowPartRel, DefUserFields, CustomReport, CustomAdmin, CustomAdminText, PrintDirect)
        End Function

    End Class

    'This is the preferences model for order manager
    Public Class OMPreferenceModel
        Public Property MaxOrders As Integer
        Public Property AllowInProc As Boolean
        Public Property AllowPartRel As Boolean
        Public Property DefUserFields As Boolean
        Public Property CustomReport As String
        Public Property CustomAdmin As String
        Public Property CustomAdminText As String
        Public Property PrintDirectly As Boolean
        ''' <summary>
        ''' Set the properties to their desired values when constructed
        ''' </summary>
        ''' <param name="MaxOrders"></param>
        ''' <param name="AllowInProc"></param>
        ''' <param name="AllowPartRel"></param>
        ''' <param name="DefUserFields"></param>
        ''' <param name="CustomReport"></param>
        ''' <param name="CustomAdmin"></param>
        ''' <param name="CustomAdminText"></param>
        ''' <remarks></remarks>
        Public Sub New(MaxOrders As Integer, AllowInProc As Boolean, AllowPartRel As Boolean, DefUserFields As Boolean, CustomReport As String, CustomAdmin As String, CustomAdminText As String, PrintDirectly As Boolean)
            Me.MaxOrders = MaxOrders
            Me.AllowInProc = AllowInProc
            Me.AllowPartRel = AllowPartRel
            Me.DefUserFields = DefUserFields
            Me.CustomReport = CustomReport
            Me.CustomAdmin = CustomAdmin
            Me.CustomAdminText = CustomAdminText
            Me.PrintDirectly = PrintDirectly
        End Sub
    End Class
End Namespace

