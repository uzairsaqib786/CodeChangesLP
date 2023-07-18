' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web

Public Class FTPSettingsHub
    Inherits Hub
    ''' <summary>
    ''' Gets the FTP settings for import and export from the xfer transactions settings table
    ''' </summary>
    ''' <returns>An object containg 2 lists of object. One is for the import data, while the other is for the export data</returns>
    ''' <remarks></remarks>
    Public Function getFTPSettings() As Task(Of Object)
        Return Task(Of Object).Factory.StartNew(Function() As Object
                                                    Dim ImportData As New List(Of Object)
                                                    Dim ExportData As New List(Of Object)
                                                    Dim DataReader As SqlDataReader = Nothing

                                                    Try
                                                        DataReader = RunSPArray("SelXferTransSettingsFTP", "IE", {{"nothing"}})

                                                        If DataReader.HasRows Then
                                                            While DataReader.Read()
                                                                ImportData.Add(New With {.Type = CheckDBNull(DataReader("Transaction Type")), .FTP = CheckDBNull(DataReader("FTP Import")), _
                                                                                         .Location = CheckDBNull(DataReader("FTP Import Location")), .Username = CheckDBNull(DataReader("FTP Import Username")), _
                                                                                         .Password = CheckDBNull(DataReader("FTP Import Password")), .Filename = CheckDBNull(DataReader("FTP Import Filename")), _
                                                                                         .Extension = CheckDBNull(DataReader("FTP Import Extension")), .Ready = CheckDBNull(DataReader("FTP Import Ready File"))})
                                                                ExportData.Add(New With {.Type = CheckDBNull(DataReader("Host Type")), .FTP = CheckDBNull(DataReader("FTP Export")), _
                                                                                         .Location = CheckDBNull(DataReader("FTP Export Location")), .Username = CheckDBNull(DataReader("FTP Export Username")), _
                                                                                         .Password = CheckDBNull(DataReader("FTP Export Password")), .Filename = CheckDBNull(DataReader("FTP Export Filename")), _
                                                                                         .Extension = CheckDBNull(DataReader("FTP Export Extension")), .Ready = CheckDBNull(DataReader("FTP Export Ready File"))})
                                                            End While
                                                        End If
                                                    Catch ex As Exception
                                                        insertErrorMessages("FTPSettingsHub", "getFTPSettings", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                    Finally
                                                        If Not IsNothing(DataReader) Then
                                                            DataReader.Dispose()
                                                        End If
                                                    End Try

                                                    Return New With {.Import = ImportData, .Export = ExportData}
                                                End Function)
        
    End Function
    ''' <summary>
    ''' Updates the desired import ftp with the given data
    ''' </summary>
    ''' <param name="OldTransType">The original transaction type</param>
    ''' <param name="TransType">Thew new transaction type</param>
    ''' <param name="FTP">If the FTP check box is checked</param>
    ''' <param name="Location">The FTP location for this transaction type</param>
    ''' <param name="Username">The usernmae for this transaction type</param>
    ''' <param name="Password">The password for this transaction type</param>
    ''' <param name="Filename">The file name for this tranaction type</param>
    ''' <param name="Extension">The file extension</param>
    ''' <param name="Ready">The ftp ready file</param>
    ''' <returns>Boolean telling if the operation was completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updImportSett(OldTransType As String, TransType As String, FTP As Boolean, Location As String, Username As String, Password As String, _
                                  Filename As String, Extension As String, Ready As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("updXferTransSettingsImport", "IE", {{"@OldTransType", OldTransType, strVar}, _
                                                                                                          {"@TransType", TransType, strVar}, _
                                                                                                          {"@FTP", FTP, boolVar}, _
                                                                                                          {"@Location", Location, strVar}, _
                                                                                                          {"@Username", Username, strVar}, _
                                                                                                          {"@Password", Password, strVar}, _
                                                                                                          {"@Filename", Filename, strVar}, _
                                                                                                          {"@Extension", Extension, strVar}, _
                                                                                                          {"@Ready", Ready, strVar}, _
                                                                                                          {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                          {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("FTPSettingsHub", "updImportSett", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function
    ''' <summary>
    ''' Updates the desired export ftp with the given data
    ''' </summary>
    ''' <param name="OldHostType">The original host type</param>
    ''' <param name="HostType">The new host type</param>
    ''' <param name="FTP">If the FTP chekc box was checked</param>
    ''' <param name="Location">The FTP location for this host type</param>
    ''' <param name="Username">The Username fo this host type</param>
    ''' <param name="Password">The Password for this host type</param>
    ''' <param name="Filename">The file name for this host type</param>
    ''' <param name="Extension">The file extension</param>
    ''' <param name="Ready">The ftp ready file</param>
    ''' <returns>Boolean telling if the operation completed successfully</returns>
    ''' <remarks></remarks>
    Public Function updExportSett(OldHostType As String, HostType As String, FTP As Boolean, Location As String, Username As String, Password As String, _
                                  Filename As String, Extension As String, Ready As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("updXferTransSettingsExport", "IE", {{"@OldHostType", OldHostType, strVar}, _
                                                                                                          {"@HostType", HostType, strVar}, _
                                                                                                          {"@FTP", FTP, boolVar}, _
                                                                                                          {"@Location", Location, strVar}, _
                                                                                                          {"@Username", Username, strVar}, _
                                                                                                          {"@Password", Password, strVar}, _
                                                                                                          {"@Filename", Filename, strVar}, _
                                                                                                          {"@Extension", Extension, strVar}, _
                                                                                                          {"@Ready", Ready, strVar}, _
                                                                                                          {"@User", Context.User.Identity.Name, strVar}, _
                                                                                                          {"@WSID", Context.QueryString.Get("WSID"), strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("FTPSettingsHub", "updImportSett", ex.Message, Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try
                                                     Return True
                                                 End Function)
    End Function
End Class
