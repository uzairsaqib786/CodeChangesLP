' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports System.IO
Namespace Consolidation
    Public Class CMConsolidationHub
        Inherits Hub


        ''' <summary>
        ''' Calls the function which selects the data for the page
        ''' </summary>
        ''' <param name="Type">If the entered value is an order numebr or tote id</param>
        ''' <param name="SelVal">The value whose datat is dispalyed</param>
        ''' <returns>object containing the all the page information</returns>
        ''' <remarks></remarks>
        Public Function getConsolidationData(Type As String, SelVal As String) As Task(Of Object)
            Return Task(Of Object).Factory.StartNew(Function() As Object
                                                        Dim WSID = Context.QueryString.Get("WSID")
                                                        Dim User = Context.User.Identity.Name
                                                        Return CMConsolidation.GetConsolidationData(Type, SelVal, WSID, User)
                                                    End Function)

        End Function
        ''' <summary>
        ''' inserts the desired record into the consolidation transactions table which verifies it
        ''' </summary>
        ''' <param name="ID">The id of the record that is going to be verified</param>
        ''' <returns>string telling if the function errored out</returns>
        ''' <remarks></remarks>
        Public Function verifyItem(ID As Integer) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success As String = ""

                                                        Try
                                                            RunActionSP("insConsolTrans", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar}, _
                                                                                                                            {"@User", Context.User.Identity.Name, strVar}})
                                                        Catch ex As Exception
                                                            insertErrorMessages("Consolidation Hub", "verifyItem", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try

                                                        Return success
                                                    End Function)
        End Function
        ''' <summary>
        ''' Will isnert all ids that are not verified into the verified table 
        ''' </summary>
        ''' <param name="IDs">A list of ids that will be verified</param>
        ''' <returns>a string telling if the function sucessfully executed</returns>
        ''' <remarks></remarks>
        Public Function verifyAll(IDs As List(Of Integer)) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try
                                                            For Each id In IDs
                                                                RunActionSP("insConsolTrans", Context.QueryString.Get("WSID"), {{"@ID", id, intVar}, _
                                                                                                                            {"@User", Context.User.Identity.Name, strVar}})
                                                            Next
                                                        Catch ex As Exception
                                                            insertErrorMessages("Consolidation Hub", "verifyAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success

                                                    End Function)
        End Function
        ''' <summary>
        ''' deletes the desired record from the consolidation transactions table
        ''' </summary>
        ''' <param name="ID">The id to be deleted</param>
        ''' <returns>string depicting if the function executed successfully</returns>
        ''' <remarks></remarks>
        Public Function deleteVerifiedItem(ID As Integer) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success As String = ""

                                                        Try
                                                            RunActionSP("delVerItem", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar}})
                                                        Catch ex As Exception
                                                            insertErrorMessages("Consolidation Hub", "deleteVerifiedItem", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success
                                                    End Function)
        End Function
        ''' <summary>
        ''' will delete all desired records from the consolidation transactions table 
        ''' </summary>
        ''' <param name="IDs">A list of ids that will be inverified</param>
        ''' <returns>string telling if the function executed succesfully</returns>
        ''' <remarks></remarks>
        Public Function unVerifyAll(IDs As List(Of Integer)) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim success = ""
                                                        Try
                                                            For Each id In IDs
                                                                RunActionSP("delVerItem", Context.QueryString.Get("WSID"), {{"@ID", id, intVar}})
                                                            Next
                                                        Catch ex As Exception
                                                            insertErrorMessages("Consolidation Hub", "unVerifyAll", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                            success = "Fail"
                                                        End Try
                                                        Return success

                                                    End Function)
        End Function
        ''' <summary>
        ''' Calls the function which will select data if multiple of the same item exist
        ''' </summary>
        ''' <param name="orderNumber">The dispolpayed order number</param>
        ''' <param name="col">The column which being checked</param>
        ''' <param name="colVal">The value of the column</param>
        ''' <returns>list of list of string which will populate the select modal</returns>
        ''' <remarks></remarks>
        Public Function getItemSelectedData(orderNumber As String, col As String, colVal As String) As Task(Of List(Of List(Of String)))
            Return Task(Of List(Of List(Of String))).Factory.StartNew(Function() As List(Of List(Of String))

                                                                          Return CMConsolidation.selItemSelModelData(orderNumber, col, colVal, Context.QueryString.Get("WSID"), Context.User.Identity.Name)
                                                                      End Function)
        End Function
        ''' <summary>
        ''' Updates the desired staging location to the desired values
        ''' </summary>
        ''' <param name="orderNumber">The displayed order number</param>
        ''' <param name="toteID">The tote id that is being updated</param>
        ''' <param name="location">The new location for this tote</param>
        ''' <param name="clear">tells whether to clear the desired location</param>
        ''' <returns>a list of string telling of the function executed successfully</returns>
        ''' <remarks></remarks>
        Public Function updateStagingLocation(orderNumber As String, toteID As String, location As String, clear As Integer) As Task(Of List(Of String))
            Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                                 Dim success As New List(Of String)
                                                                 Dim DataReader As SqlDataReader = Nothing
                                                                 Dim day As String = ""
                                                                 Dim validLocation As Boolean = True
                                                                 Try
                                                                     If Context.QueryString.Get("currentUser") <> "" Then
                                                                         If (Preferences.selectCMPrefs(Context.User.Identity.Name, Context.QueryString.Get("WSID")).ValidateStagingLocations) Then
                                                                             validLocation = GetResultSingleCol("selStagingLocValid", Context.QueryString.Get("WSID"), {{"@Location", location, strVar}})
                                                                         End If

                                                                         If (validLocation) Then
                                                                             DataReader = RunSPArray("updStagingLoc", Context.QueryString.Get("WSID"), {{"@ordNum", orderNumber, strVar}, {"@totID", toteID, strVar},
                                                                                                                                                        {"@User", Context.QueryString.Get("currentUser"), strVar},
                                                                                                                                                        {"@location", location, strVar}, {"@clear", clear, intVar}})
                                                                             If DataReader.HasRows Then
                                                                                 DataReader.Read()
                                                                                 day = DataReader(0)
                                                                             End If
                                                                         Else
                                                                             success.Add("INVALID")
                                                                             Return success
                                                                         End If
                                                                     Else
                                                                         insertErrorMessages("Consolidation Hub", "updateStagingLocation", "User is Logged out on machine: " + Context.QueryString.Get("WSID"), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                         success = New List(Of String) From {"Redirect"}
                                                                     End If

                                                                 Catch ex As Exception
                                                                     insertErrorMessages("Consolidation Hub", "updateStagingLocation", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                                     success.Add("Fail")
                                                                 Finally
                                                                     If Not IsNothing(DataReader) Then
                                                                         DataReader.Dispose()
                                                                     End If
                                                                 End Try
                                                                 success.Add(Context.User.Identity.Name)
                                                                 success.Add(day)
                                                                 If success(0) = "Fail" Then
                                                                     Return success
                                                                 Else
                                                                     Return success
                                                                 End If
                                                             End Function)
        End Function
        ''' <summary>
        ''' selects the confirm and packing setting 
        ''' </summary>
        ''' <returns>a boolean telling if the confirm and packing preferences is true or false</returns>
        ''' <remarks></remarks>
        Public Function selectConfirmPackingPref() As Task(Of Boolean)
            Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim confPack As Boolean = False
                                                         Try
                                                             DataReader = RunSPArray("selConsolConfPack", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                             If DataReader.HasRows Then
                                                                 DataReader.Read()
                                                                 confPack = DataReader(0)
                                                             End If
                                                         Catch ex As Exception
                                                             insertErrorMessages("Consolidation Hub", "selectConfirmPackingPref", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return confPack
                                                     End Function)
        End Function
        ''' <summary>
        ''' Tells whether to enable or disable the shipping button on the page
        ''' </summary>
        ''' <param name="orderNum">The displayed order number</param>
        ''' <returns>an integer whose value will either enable or disable the button></returns>
        ''' <remarks></remarks>
        Public Function setShippingButt(orderNum As String) As Task(Of Integer)
            Return Task(Of Integer).Factory.StartNew(Function() As Integer
                                                         Dim DataReader As SqlDataReader = Nothing
                                                         Dim display As Integer = 0
                                                         Try
                                                             DataReader = RunSPArray("selCMPrefsShipButt", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                                             {"@OrderNumber", orderNum, strVar}})

                                                             If DataReader.HasRows Then
                                                                 DataReader.Read()
                                                                 display = CInt(DataReader(0))
                                                             End If
                                                         Catch ex As Exception
                                                             insertErrorMessages("Consolidation Hub", "setShippingButt", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Finally
                                                             If Not IsNothing(DataReader) Then
                                                                 DataReader.Dispose()
                                                             End If
                                                         End Try
                                                         Return display
                                                     End Function)
        End Function

        ''' <summary>
        ''' Determines whether a modal appears when printing the packaging report
        ''' </summary>
        ''' <param name="orderNum">The displayed order number</param>
        ''' <returns>a string telling whether or not to show the modal</returns>
        ''' <remarks></remarks>
        Public Function showCMPackPrintModal(orderNum As String) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim result As String = ""
                                                        Dim DataReader As SqlDataReader = Nothing

                                                        Try
                                                            DataReader = RunSPArray("selCMQuestionLL", Context.QueryString.Get("WSID"), {{"@OrderNum", orderNum, strVar}})
                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                result = DataReader(0)
                                                            End If
                                                        Catch ex As Exception
                                                            result = "Fail"
                                                            insertErrorMessages("Consolidation Hub", "showCMPackPrintModal", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Return result
                                                    End Function)
        End Function
        ''' <summary>
        ''' Inserts the order number into the email transactiosn tbale
        ''' </summary>
        ''' <param name="OrderNum">The dispalyed roder number</param>
        ''' <param name="resend">Tells if to resend an emial</param>
        ''' <returns>String telling if operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Function insertEmailTrans(OrderNum As String, resend As Integer) As Task(Of String)
            Return Task(Of String).Factory.StartNew(Function() As String
                                                        Dim DataReader As SqlDataReader = Nothing
                                                        Dim result As String = ""
                                                        Try
                                                            DataReader = RunSPArray("insEmailTransactions", Context.QueryString.Get("WSID"), {{"@OrderNumber", OrderNum, strVar},
                                                                                                                                              {"@User", Context.User.Identity.Name, strVar},
                                                                                                                                              {"@WSID", Context.QueryString.Get("WSID"), strVar},
                                                                                                                                              {"@Resend", resend, intVar}})
                                                            If DataReader.HasRows Then
                                                                DataReader.Read()
                                                                result = DataReader(0)
                                                            End If

                                                        Catch ex As Exception
                                                            result = "Error"
                                                            insertErrorMessages("Consolidation Hub", "insertEmailTrans", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                        Finally
                                                            If Not IsNothing(DataReader) Then
                                                                DataReader.Dispose()
                                                            End If
                                                        End Try
                                                        Return result
                                                    End Function)
        End Function

    End Class
End Namespace

