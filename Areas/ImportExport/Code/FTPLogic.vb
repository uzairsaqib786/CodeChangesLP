' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module FTPLogic
        ''' <summary>
        ''' Updates the FTP import and export fields
        ''' </summary>
        ''' <param name="ftp">Model that contains the new ftp setting values</param>
        Public Sub UpdateFTP(ByVal ftp As FTPModel)
            RunSP("UpdFTP", "IE", {
                {"@FTPImport", ftp.FTPImport, strVar},
                {"@FTPExport", ftp.FTPExport, strVar}
            })
        End Sub
    End Module
End Namespace
