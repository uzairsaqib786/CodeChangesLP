Public Class SecurityConfig
    Public Shared Function getAD() As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ADPath").ConnectionString
        Return connectionString
    End Function

    Public Shared Function getADLDS() As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("ADLDSPath").ConnectionString
        Return connectionString
    End Function

    Public Shared Function getAZUREAD() As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("AZUREADPath").ConnectionString
        Return connectionString
    End Function

    Public Shared Function getSecurityEnvironment() As String
        Return ConfigurationManager.AppSettings("SecurityEnvironment")
    End Function

    Public Shared Function getAzureADConnectionUserName() As String
        Return ConfigurationManager.AppSettings("AzureADConnectionUserName")
    End Function

    Public Shared Function getAzureADConnectionPassword() As String
        Return ConfigurationManager.AppSettings("AzureADConnectionPassword")
    End Function

    Public Shared Function getADAttributes() As String
        Return ConfigurationManager.AppSettings("ADAttributes")
    End Function
End Class
