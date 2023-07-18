' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Imports PickPro_Web.Certificates

Public Class WMPreferencesHub
    Inherits Hub
    ''' <summary>
    ''' Updates the general settings 
    ''' </summary>
    ''' All paramaters below are settings under the general section for work manager
    ''' each paramater is the new value for each respective preference
    ''' <param name="pick"></param>
    ''' <param name="put"></param>
    ''' <param name="count"></param>
    ''' <param name="customApp"></param>
    ''' <param name="description"></param>
    ''' <param name="printDirect"></param>
    ''' <returns>boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function SaveGeneralSettings(pick As Integer, put As Integer, count As Integer, customApp As String, description As String, printDirect As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("updWMGeneral", WSID, {{"@Pick", pick, intVar}, {"@Put", put, intVar}, {"@Count", count, intVar},
                                                                                           {"@WSID", WSID, strVar},
                                                                                            {"@PrintDirect", printDirect, intVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("WMPreferencesHub", "SaveGeneralSettings", ex.Message, user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the sort strings for pick, put away, and count batches
    ''' </summary>
    ''' <param name="PickSort">The sorting for picks</param>
    ''' <param name="PutSort">The sorrting for put aways</param>
    ''' <param name="CountSort">The sorting for counts</param>
    ''' <param name="Type">Determines whihc sort is getting updated</param>
    ''' <returns>boolean telling if the desired operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveDefaultSorts(PickSort As String, PutSort As String, CountSort As String, Type As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         RunActionSP("updWMSettingsDefSorts", Context.QueryString.Get("WSID"), {{"@PickSort", PickSort, strVar},
                                                                                                                                {"@PutSort", PutSort, strVar},
                                                                                                                                {"@CountSort", CountSort, strVar},
                                                                                                                                {"@Type", Type, intVar},
                                                                                                                                {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "SaveGeneralSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Tells if the given range name is valid 
    ''' </summary>
    ''' <param name="RangeName">The range name to check</param>
    ''' <returns>string telling the rnage name is valid or not</returns>
    ''' <remarks></remarks>
    Public Function verifyLocationRangeName(RangeName As String) As Task(Of String)
        Return task(Of String).Factory.StartNew(Function() As String
                                                    Dim res As String = ""
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("selWMLocRangesVerify", Context.QueryString.Get("WSID"), {{"@RangeName", RangeName, strVar}})

                                                        If DataReader.HasRows Then
                                                            DataReader.Read()
                                                            res = DataReader(0)
                                                        End If
                                                    Catch ex As Exception
                                                        res = "Error"
                                                        insertErrorMessages("WMPreferencesHub", "verifyLocationRangeName", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try
                                                    Return res
                                                End Function)
    End Function

    ''' <summary>
    ''' Updates the information for the desired location range
    ''' </summary>
    ''' <param name="ID">The id of the range to updpate</param>
    ''' <param name="RangeName">The new name of the rnage</param>
    ''' <param name="StartLocation">The new start location</param>
    ''' <param name="EndLocation">The new end location</param>
    ''' <param name="MulitWorker">If the range is multiworker</param>
    ''' <param name="Acitve">If the rnage is active</param>
    ''' <returns>boolean telling if the desired operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateLocationRange(ID As Integer, RangeName As String, StartLocation As String, EndLocation As String, MulitWorker As String, Acitve As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True
                                                     Dim multi As Integer = 0
                                                     Dim act As Integer = 0
                                                     Try
                                                         If MulitWorker = "True" Then
                                                             multi = 1
                                                         End If
                                                         If Acitve = "True" Then
                                                             act = 1
                                                         End If

                                                         RunActionSP("updWMLocationRanges", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar},
                                                                                                                              {"@RangeName", RangeName, strVar},
                                                                                                                              {"@StartLocation", StartLocation, strVar},
                                                                                                                              {"@EndLocation", EndLocation, strVar},
                                                                                                                              {"@MultiWorker", multi, intVar},
                                                                                                                              {"@Active", act, intVar}})

                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "vupdateLocationRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try


                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Updates the information of the given user
    ''' </summary>
    ''' <param name="username">The username ot update the info for</param>
    ''' All paramaters below contain the new values for settings for the given user
    ''' <param name="active"></param>
    ''' <param name="wmSettings"></param>
    ''' <param name="organize"></param>
    ''' <param name="reports"></param>
    ''' <param name="picks">A list that contains the integer values for the pick settings</param>
    ''' <param name="puts">A list that contains the integer values for the put away settings</param>
    ''' <param name="counts">A list that contains the integer values for the count settings</param>
    ''' <returns>Boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function SaveWMUser(username As String, active As Integer, wmSettings As Integer, organize As Integer, reports As Integer, picks As List(Of Integer), puts As List(Of Integer), counts As List(Of Integer)) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("updWMUser", WSID, {{"@Username", username, strVar}, {"@Active", active, intVar}, {"@WMSettings", wmSettings, intVar},
                                                                                        {"@Organize", organize, intVar}, {"@Reports", reports, intVar}, {"@PickAllow", picks(0), intVar},
                                                                                        {"@PickBatch", picks(1), intVar}, {"@PickDefault", picks(2), intVar}, {"@PickMax", picks(3), intVar},
                                                                                        {"@PickLines", picks(4), intVar}, {"@PutAllow", puts(0), intVar}, {"@PutBatch", puts(1), intVar},
                                                                                        {"@PutDefault", puts(2), intVar}, {"@PutMax", puts(3), intVar}, {"@PutLines", puts(4), intVar},
                                                                                        {"@CountAllow", counts(0), intVar}, {"@CountBatch", counts(1), intVar}, {"@CountDefault", counts(2), intVar},
                                                                                        {"@CountMax", counts(3), intVar}, {"@CountLines", counts(4), intVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("WMPreferencesHub", "SaveWMUser", ex.Message, user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Inserts a new worker
    ''' </summary>
    ''' <param name="username">The username of the new worker</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function AddNewWorker(username As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Return WorkManager.WMPreferences.InsertNewUserOrWSID(username, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                 End Function)
    End Function

    ''' <summary>
    ''' Deletes the desired worker 
    ''' </summary>
    ''' <param name="username">The suername of the worker to delete</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function RemoveWorker(username As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("delWMUser", WSID, {{"@User", username, strVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("WMPreferencesHub", "RemoveWorker", ex.Message, user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Clears batches for the given marked transaction types 
    ''' </summary>
    ''' <param name="username">The username ot clear batches for</param>
    ''' <param name="pick">If pick batches should be cleared</param>
    ''' <param name="put">If put batches should be cleared</param>
    ''' <param name="count">If countbatches should be cleared</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ClearBatches(username As String, pick As Integer, put As Integer, count As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim WSID As String = Context.QueryString.Get("WSID"), user As String = Context.User.Identity.Name
                                                     Try
                                                         RunActionSP("wmClearWorkers", WSID, {{"@Username", username, strVar}, {"@PickClear", pick, intVar}, {"@PutClear", put, intVar}, {"@CountClear", count, intVar}})
                                                     Catch ex As Exception
                                                         Debug.Print(ex.Message)
                                                         insertErrorMessages("WMPreferencesHub", "ClearBatches", ex.Message, user, WSID)
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function

    ''' <summary>
    ''' Inserts a new location with the desired infomration
    ''' </summary>
    ''' <param name="RangeName">The new range name</param>
    ''' <param name="StartLocation">The statring location for the range</param>
    ''' <param name="EndLocation">The ending location for the range</param>
    ''' <param name="MulitWorker">If the range is a multi worker one</param>
    ''' <param name="Active">If the range is active</param>
    ''' <returns>boolean telling of the desired operation was completed</returns>
    ''' <remarks></remarks>
    Public Function createLocationRange(RangeName As String, StartLocation As String, EndLocation As String, MulitWorker As String, Active As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True
                                                     Dim multi As Integer = 0
                                                     Dim act As Integer = 0
                                                     Try
                                                         If MulitWorker = "True" Then
                                                             multi = 1
                                                         End If
                                                         If Active = "True" Then
                                                             act = 1
                                                         End If

                                                         RunActionSP("insWMLocationRanges", Context.QueryString.Get("WSID"), {{"@RangeName", RangeName, strVar},
                                                                                                                             {"@StartLocation", StartLocation, strVar},
                                                                                                                             {"@EndLocation", EndLocation, strVar},
                                                                                                                             {"@MultiWorker", multi, intVar},
                                                                                                                             {"@Active", act, intVar}})
                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "createLocationRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Empties the location ranges table and reinserts the records. 
    ''' </summary>
    ''' <returns>Boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function refreshLocationRanges() As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         RunActionSP("updWMLocationRangesRefresh", Context.QueryString.Get("WSID"), {{"nothing"}})
                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "refreshLocationRanges", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try
                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Gets data from the wmusers table based on the last name
    ''' </summary>
    ''' <param name="LastName">The last name of the user to get data for</param>
    ''' <returns>list of object containg the information</returns>
    ''' <remarks></remarks>
    Public Function getWMUserData(LastName As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return WorkManager.WMPreferences.selectWMUsersLastNameTypeahead(LastName, 1, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Gets the location range data for the given range name
    ''' </summary>
    ''' <param name="RangeName">The range name to get data for</param>
    ''' <returns>list of object contiang the information</returns>
    ''' <remarks></remarks>
    Public Function getWMLocRangeData(RangeName As String) As Task(Of List(Of Object))
        Return Task(Of List(Of Object)).Factory.StartNew(Function() As List(Of Object)
                                                             Return WorkManager.WMPreferences.selectWMLocRangesRangeNameTypeahead(RangeName, 1, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         End Function)
    End Function

    ''' <summary>
    ''' Updates the desired user range with the given information
    ''' </summary>
    ''' <param name="ID">The id of the user rnage to update</param>
    ''' <param name="Username">The username that is assigned ot the range</param>
    ''' <param name="FirstName">The first name of the suer</param>
    ''' <param name="LastName">The last name of the user</param>
    ''' <param name="RangeName">The range name</param>
    ''' <param name="StartLocation">The starting location fo the range</param>
    ''' <param name="EndLocation">The ending location of the range</param>
    ''' <param name="Active">If the range is active</param>
    ''' <returns>a boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateUserRange(ID As Integer, Username As String, FirstName As String, LastName As String, RangeName As String,
                                    StartLocation As String, EndLocation As String, Active As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True
                                                     Dim act As Integer = 0
                                                     Try
                                                         If Active = "True" Then
                                                             act = 1
                                                         End If

                                                         RunActionSP("updWMUserRanges", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar},
                                                                                                                          {"@Username", Username, strVar},
                                                                                                                          {"@FirstName", FirstName, strVar},
                                                                                                                          {"@LastName", LastName, strVar},
                                                                                                                          {"@RangeName", RangeName, strVar},
                                                                                                                          {"@StartLocation", StartLocation, strVar},
                                                                                                                          {"@EndLocation", EndLocation, strVar},
                                                                                                                          {"@Active", act, intVar}})

                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "updateUserRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Inserts a new user range with the users info 
    ''' </summary>
    ''' <param name="LastName">The last name of the user</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function insertWMUserRange(LastName As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         Dim UserData As List(Of Object) = WorkManager.WMPreferences.selectWMUsersLastNameTypeahead(LastName, 1, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Dim Username As String = UserData(0).Username
                                                         Dim FirstName As String = UserData(0).FirstName

                                                         RunActionSP("insWMUserRanges", Context.QueryString.Get("WSID"), {{"@Username", Username, strVar},
                                                                                                                          {"@FirstName", FirstName, strVar},
                                                                                                                          {"@LastName", LastName, strVar}})

                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "insertWMUserRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function

    ''' <summary>
    ''' Deletes the desired worker range
    ''' </summary>
    ''' <param name="ID">The id of the worker range to delete</param>
    ''' <returns>boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteWorkerRange(ID As Integer) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean
                                                     Dim success As Boolean = True

                                                     Try
                                                         RunActionSP("delWMUserRanges", Context.QueryString.Get("WSID"), {{"@ID", ID, intVar}})
                                                     Catch ex As Exception
                                                         success = False
                                                         insertErrorMessages("WMPreferencesHub", "deleteWorkerRange", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                     End Try

                                                     Return success
                                                 End Function)
    End Function
End Class