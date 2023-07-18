' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports Microsoft.AspNet.SignalR
Imports System.Threading.Tasks
Imports System.Data.SqlClient
Imports PickPro_Web

Public Class TransferSettingsHub
    Inherits Hub
    ''' <summary>
    ''' Either  calls import or export all depending on the type (import or export) and the jobh type
    ''' </summary>
    ''' <param name="Type">Tells if import or export</param>
    ''' <param name="Job">Rells if sql job or stored procedure</param>
    ''' <returns>Boolean telling of the job completed successfully</returns>
    ''' <remarks></remarks>
    Public Function ImportExportAll(Type As String, Job As String) As Task(Of Boolean)
        Return Task(Of Boolean).Factory.StartNew(Function() As Boolean

                                                     Try
                                                         RunActionSP("IE_Jobs", "IE", {{"@IEType", Type, strVar}, {"@JobType", Job, strVar}})
                                                     Catch ex As Exception
                                                         insertErrorMessages("TransferSettingsHub", "ImportExportAll", ex.ToString(), Context.User.Identity.Name, Context.QueryString.Get("WSID"))
                                                         Return False
                                                     End Try

                                                     Return True
                                                 End Function)
    End Function

End Class
