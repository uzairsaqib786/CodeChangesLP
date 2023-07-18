' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Reflection

Public Module AppBuildInfo
    ''' <summary>
    ''' Gives the current version number
    ''' </summary>
    ''' <param name="ProjectName">The project that is currently being viewed</param>
    ''' <returns></returns>
    Public Function DisplayVersion(ProjectName As String) As String
        Dim VersionString = Reflection.Assembly.Load(ProjectName).ToString
        Dim Version() = VersionString.Split(",")
        Version = Version(1).Split("=")
        Return Version(1)
    End Function

End Module
