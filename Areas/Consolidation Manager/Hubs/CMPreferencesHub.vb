' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web
Public Class CMPreferencesHub
    Inherits Hub
    ''' <summary>
    ''' Updates all the CM Preferences under the consolidation tab
    ''' </summary>
    ''' <param name="AutoCompShip">Auto Complete Back Orders</param>
    ''' <param name="DefPackList">Default Packing List</param>
    ''' <param name="DeffLookType">Default lookup type</param>
    ''' <param name="VerifyItems">Tells if to verify each item</param>
    ''' <param name="BlindVerify">Tells if verify items are not shown</param>
    ''' <param name="PrintVerified">Allow printing when unverfied line items exist</param>
    ''' <param name="PrintUnVerified">Allow printing of unverified items</param>
    ''' <param name="PackingListSort">The sort for the packing list report</param>
    ''' <returns>String telling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function updateCMPrefsPrefs(AutoCompShip As Boolean, DefPackList As String, DeffLookType As String, VerifyItems As String, BlindVerify As String, _
                                       PrintVerified As String, PrintUnVerified As String, PackingListSort As String, NonPickpro As String, ValidateStaingLocs As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim returnMessage As String = "Done"
                                                    Try
                                                        'change wsid later
                                                        RunActionSP("updCMPrefsPrefs", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@DefPackList", DefPackList, strVar}, _
                                                                                                                        {"@DefLookType", DeffLookType, strVar}, {"@VerifyItems", VerifyItems, strVar}, {"@BlindVerify", BlindVerify, strVar}, _
                                                                                                                         {"@PrintVerified", PrintVerified, strVar}, {"@PrintUnVerified", PrintUnVerified, strVar}, _
                                                                                                                         {"@PackingListSort", PackingListSort, strVar}, {"@AutoCompShip", AutoCompShip, boolVar}, {"@NonPickpro", NonPickpro, boolVar}, _
                                                                                                                         {"@ValidateStagingLocs", ValidateStaingLocs, boolVar}})

                                                    Catch ex As Exception
                                                        returnMessage = "Error"
                                                        insertErrorMessages("Cycle Count", "RemoveccQueueRow", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return returnMessage
                                                End Function)
    End Function
    ''' <summary>
    ''' Updates all the CM Preferneces under the Shipping tab
    ''' </summary>
    ''' <param name="Packing">If this workstation is allowed to pack orders</param>
    ''' <param name="ConfirmPack">If this workstation is allwoed to confirm and acpk orders</param>
    ''' <param name="PrintContPL">If the container packing list should be auto printed</param>
    ''' <param name="PrintOrderPL">If the order packing list should be auto printed</param>
    ''' <param name="PrintContLabel">If the container label should be auto printed</param>
    ''' <param name="EntContainerID">If the user should have to enter a container id</param>
    ''' <param name="ContainerIDDEF">Default text for container id</param>
    ''' <param name="ConfPackQuant">If the packing quantity should be confirmed</param>
    ''' <param name="Freight">If this workstation can edit a containers freight field</param>
    ''' <param name="Freight1">If this workstation can edit a containers freight1 field</param>
    ''' <param name="Freight2">If this workstation can edit a containers freight2 field</param>
    ''' <param name="Weight">If this workstation can edit a containers weight field</param>
    ''' <param name="Length">If this workstation can edit a containers length field</param>
    ''' <param name="Width">If this workstation can edit a containers width field</param>
    ''' <param name="Height">If this workstation can edit a containers height field</param>
    ''' <param name="Cube">If this workstation can edit a containers cube field</param>
    ''' <param name="Shipping">If this wokrstation is alllowed to ship orders</param>
    ''' <returns>String telling if the function ran successfully</returns>
    ''' <remarks></remarks>
    Public Function updCMPrefsShipPrefs(Packing As Boolean, ConfirmPack As Boolean, PrintContPL As Boolean, PrintOrderPL As Boolean, PrintContLabel As Boolean, _
                                        EntContainerID As Boolean, ContainerIDDEF As String, ConfPackQuant As Boolean, Freight As Boolean, Freight1 As Boolean, Freight2 As Boolean, _
                                        Weight As Boolean, Length As Boolean, Width As Boolean, Height As Boolean, Cube As Boolean, Shipping As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim returnMessage As String = "Done"
                                                    Try
                                                        'change wsid later
                                                        RunActionSP("updCMPrefsShipPrefs", Context.QueryString.Get("WSID"), {{"@WSID", Context.QueryString.Get("WSID"), strVar}, {"@Packing", Packing, boolVar}, {"@ConfirmPack", ConfirmPack, boolVar}, _
                                                                                                                             {"@PrintContPL", PrintContPL, boolVar}, {"@PrintOrderPL", PrintOrderPL, boolVar}, {"@PrintContLabel", PrintContLabel, boolVar}, {"@EntContainerID", EntContainerID, boolVar}, _
                                                                                                                             {"@ContainerIDDEF", ContainerIDDEF, strVar}, {"@ConfPackQuant", ConfPackQuant, boolVar}, {"@Freight", Freight, boolVar}, {"@Freight1", Freight1, boolVar}, {"@Freight2", Freight2, boolVar}, _
                                                                                                                             {"@Weight", Weight, boolVar}, {"@Length", Length, boolVar}, {"@Width", Width, boolVar}, {"Height", Height, boolVar}, {"@Cube", Cube, boolVar}, {"@Shipping", Shipping, boolVar}})

                                                    Catch ex As Exception
                                                        returnMessage = "Error"
                                                        insertErrorMessages("Cycle Count", "RemoveccQueueRow", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return returnMessage
                                                End Function)
    End Function
    ''' <summary>
    ''' Updates the Email packing slip preference
    ''' </summary>
    ''' <param name="EmailPackSlip">Tells if the packing hsould be emailed when it is printed</param>
    ''' <returns>string telling the function ran successfully</returns>
    ''' <remarks></remarks>
    Public Function updSystPrefsEmailSlip(EmailPackSlip As Boolean) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim returnMessage As String = "Done"
                                                    Try

                                                        RunActionSP("updSystPrefsEmailSlip", Context.QueryString.Get("WSID"), {{"@EmailPackSlip", EmailPackSlip, boolVar}})
                                                    Catch ex As Exception
                                                        returnMessage = "Error"
                                                        insertErrorMessages("Cycle Count", "RemoveccQueueRow", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return returnMessage
                                                End Function)
    End Function
    ''' <summary>
    ''' Selects carriers for the carrier modal on the page 
    ''' </summary>
    ''' <returns>List containing all carriers</returns>
    ''' <remarks></remarks>
    Public Function selectCarriers() As Task(Of List(Of String))
        Return Task(Of List(Of String)).Factory.StartNew(Function() As List(Of String)
                                                             Dim DataReader As SqlDataReader = Nothing
                                                             Dim Carriers As New List(Of String)

                                                             Try
                                                                 DataReader = RunSPArray("selCarriers", Context.QueryString.Get("WSID"), {{"nothing"}})
                                                                 If DataReader.HasRows Then
                                                                     While DataReader.Read()
                                                                         Carriers.Add(CheckDBNull(DataReader(0)))
                                                                     End While
                                                                 End If
                                                             Catch ex As Exception
                                                                 Debug.WriteLine(ex.ToString())
                                                                 insertErrorMessages("Preferences", "selectCarriers", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                             Finally
                                                                 If Not IsNothing(DataReader) Then
                                                                     DataReader.Dispose()
                                                                 End If
                                                             End Try
                                                             Return Carriers
                                                         End Function)

    End Function
    ''' <summary>
    ''' Deletes the desired carrier from the Carriers table 
    ''' </summary>
    ''' <param name="Carrier">The Carrier to be deleted</param>
    ''' <returns>string telling if the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function deleteCarrier(Carrier As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""

                                                    Try
                                                        RunActionSP("delCarriers", Context.QueryString.Get("WSID"), {{"@Carrier", Carrier, strVar}, _
                                                                                                                     {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                                     {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("Prefrences", "deleteCarrier", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function
    ''' <summary>
    ''' Will eiterh update or insert a new carrier within the Carriers table
    ''' </summary>
    ''' <param name="Carrier">The new carrier name</param>
    ''' <param name="OldCarrier">The old carrier name</param>
    ''' <returns>a string telling the function executed successfully</returns>
    ''' <remarks></remarks>
    Public Function saveCarrier(Carrier As String, OldCarrier As String) As Task(Of String)
        Return Task(Of String).Factory.StartNew(Function() As String
                                                    Dim success As String = ""
                                                    Dim SP As String = IIf(OldCarrier = "", "insCarriers", "updCarriers")
                                                    Dim params As String(,) = IIf(OldCarrier = "", {{"@Carrier", Carrier, strVar}}, _
                                                                                  {{"@OldCarrier", OldCarrier, strVar}, {"@Carrier", Carrier, strVar}})

                                                    Try
                                                        RunActionSP(SP, Context.QueryString.Get("WSID"), params)
                                                    Catch ex As Exception
                                                        success = "Error"
                                                        Debug.WriteLine(ex.ToString())
                                                        insertErrorMessages("Prefrences", "saveCarrier", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    End Try
                                                    Return success
                                                End Function)
    End Function



End Class

