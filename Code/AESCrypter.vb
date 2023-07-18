' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Security.Cryptography
Imports System.Data.SqlClient


Public Class AESCrypter

    ''' <summary>
    ''' Used to decrypt Company Key and number of licenses the customer has
    ''' </summary>
    ''' <param name="input">String you want to decrypt</param>
    ''' <param name="CompanyCode">Comapny Code/Key used as hash</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AES_Decrypt(ByVal input As String, ByVal CompanyCode As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(CompanyCode))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    ''' <summary>
    ''' Builds Dictionary of registered Apps, and checks if you have a valid license for them
    ''' </summary>
    ''' <returns>Dictionary mapping App name to Licensing Info for the App</returns>
    ''' <remarks></remarks>
    Public Shared Function GetAppPermissions() As Dictionary(Of String, AppLicense)

        Dim AppPermissionDictionary = New Dictionary(Of String, AppLicense)

        Try
            Config.AppUsers = New Concurrent.ConcurrentDictionary(Of String, Concurrent.ConcurrentDictionary(Of String, ConnectedUser))
            Config.AppDatabyURL = New Dictionary(Of String, AppData)

            'Gets Collection of all modules registered with the Application
            Dim routes_ = RouteTable.Routes.OfType(Of Route)()
            Dim AppNames = routes_.Where(Function(d) d.DataTokens IsNot Nothing AndAlso d.DataTokens.ContainsKey("area")).Select(Function(r) r.DataTokens("area")).ToArray()


            'Retreivies all Licenses stored in Database
            Dim DBAppList As List(Of AppData) = GetAppLicenseData()


            'Looks to see if App is installed. If it is, checks for valid license, otherwise registers as not available
            For Each app In DBAppList
                If AppNames.Contains(app.Name) AndAlso app.LicenseString <> "" Then
                    Dim appLicenses = ValidateLicense(app.LicenseString, app.Name)
                    'If License Starts with Valid String, check number of Licesnes and add
                    If appLicenses > 0 Then
                        AppPermissionDictionary.Add(app.Name, New AppLicense(True, appLicenses, app))
                    Else
                        AppPermissionDictionary.Add(app.Name, New AppLicense(False, 0, app))
                    End If
                Else
                    AppPermissionDictionary.Add(app.Name, New AppLicense(False, 0, app))
                End If
                'Add App to the connected user dictionary
                Config.AppUsers.TryAdd(app.Name, New Concurrent.ConcurrentDictionary(Of String, ConnectedUser))
                'Add App to the URL map user for converting the URL to the App Name

                Config.AppDatabyURL.Add(app.URL, app)
            Next

            Dim keyList As List(Of String) = AppPermissionDictionary.Keys.ToList
            'Looks at Registered apps to see if license was provided, otherwise marks permission as false
            For Each regApp In AppNames
                If Not keyList.Contains(regApp) Then
                    AppPermissionDictionary.Add(regApp, New AppLicense(False, 0, New AppData("", "", "", regApp)))
                    Config.AppUsers.TryAdd(regApp, New Concurrent.ConcurrentDictionary(Of String, ConnectedUser))
                End If
            Next

            'Look up appKey encrypted AppKey

        Catch ex As Exception
            insertErrorMessages("Appstart", "AppStartErr", ex.Message, "No User", "Error In AESCrypter GetAppPermissions")
        End Try

        Return AppPermissionDictionary
    End Function

    ''' <summary>
    ''' Parses the License String, and returns the number of licenses given by it
    ''' </summary>
    ''' <param name="LicenseString"></param>
    ''' <param name="AppName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateLicense(LicenseString As String, AppName As String) As Integer
        If (LicenseString Is Nothing OrElse LicenseString = "") Then
            Return 0
        End If
        'Gets Company code that is registered in WebConfig, and Decrypts it
        Dim companyCodeEncrypted = ConfigurationManager.AppSettings("CompanyCode").ToString
        Dim companyName = AES_Decrypt(companyCodeEncrypted, "3/25/92")

        Dim appKey = AES_Decrypt(LicenseString, companyName & "3/25/92")

        If appKey <> Nothing AndAlso appKey.StartsWith(companyName & AppName & ":") Then
            Return CInt(appKey.Substring(appKey.LastIndexOf(":") + 1))
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Returns all License data currently stored in the Database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAppLicenseData() As List(Of AppData)
        Dim DataReader As SqlDataReader = Nothing
        Dim DBAppList As New List(Of AppData)
        Try

            DataReader = RunSPArray("selAppLicenseAll", "CONFIG", {{"nothing"}})
            While DataReader.Read
                DBAppList.Add(New AppData(DataReader(0), CheckDBNull(DataReader(1)), CheckDBNull(DataReader(2)), CheckDBNull(DataReader(3))))
            End While
        Catch ex As Exception
            insertErrorMessages("Licensing", "GetAppLicenseData", ex.Message, "Application", "Application")
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Close()
            End If
        End Try
        Return DBAppList
    End Function

End Class

Public Class AppLicense
    Public Property isLicenseValid As Boolean
    Public Property numLicenses As Integer
    Public Property Info As AppData

    Sub New(isValid As Boolean, Licenses As Integer, Info As AppData)
        Me.isLicenseValid = isValid
        Me.numLicenses = Licenses
        Me.Info = Info
    End Sub
End Class

Public Class AppData
    Public Property Name As String = ""
    Public Property LicenseString As String = ""
    Public Property URL As String = ""
    Public Property DisplayName As String = ""

    ''' <summary>
    ''' Creates new Appdata object with all information needed for Licensing
    ''' </summary>
    ''' <param name="Name"></param>
    ''' <param name="LicenseString"></param>
    ''' <param name="URL"></param>
    ''' <param name="DisplayName"></param>
    ''' <remarks></remarks>
    Sub New(Name As String, LicenseString As String, URL As String, DisplayName As String)
        Me.Name = Name
        Me.LicenseString = LicenseString
        Me.URL = URL
        Me.DisplayName = DisplayName
    End Sub
    ''' <summary>
    ''' Creates new AppData object with Display information
    ''' </summary>
    ''' <param name="URL"></param>
    ''' <param name="Displayname"></param>
    ''' <remarks></remarks>
    Sub New(URL As String, Displayname As String)
        Me.URL = URL
        Me.DisplayName = Displayname
    End Sub
End Class
