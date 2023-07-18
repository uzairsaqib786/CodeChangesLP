

Imports System.DirectoryServices.AccountManagement
''' <summary>
''' Principal Extensions Class
''' </summary> <DirectoryRdnPrefix("CN")>
<DirectoryObjectClass("userProxy")>
Public Class UserProxyPrincipal
    Inherits UserPrincipal
    ' Inplement the constructor using the base class constructor. 
    Public Sub New(ByVal context As PrincipalContext)
        MyBase.New(context)

    End Sub

    ' Implement the constructor with initialization parameters.    
    Public Sub New(ByVal context As PrincipalContext, ByVal samAccountName As String, ByVal password As String, ByVal enabled As Boolean)
        MyBase.New(context, samAccountName, password, enabled)
    End Sub

    Private searchFilter As UserProxyPrincipalSearchFilter

    'Public Overloads ReadOnly Property AdvancedSearchFilter As UserProxyPrincipalSearchFilter
    '    Get
    '        If Nothing Is searchFilter Then searchFilter = New UserProxySearchFilter(Me)

    '        Return searchFilter
    '    End Get
    'End Property




    'CUSTOM ATTRIBUTE
    <DirectoryProperty("User-PostingID")>
    Public Property PostingId As String
        Get
            If ExtensionGet("User-PostingID").Length <> 1 Then Return Nothing

            Return CStr(ExtensionGet("User-PostingID")(0))
        End Get

        Set(ByVal value As String)
            ExtensionSet("User-PostingID", value)
        End Set
    End Property

    <DirectoryProperty("distinguishedName")>
    Public Property DistinguishedName As String
        Get
            If ExtensionGet("distinguishedName").Length <> 1 Then Return Nothing

            Return CStr(ExtensionGet("distinguishedName")(0))
        End Get

        Set(ByVal value As String)
            ExtensionSet("distinguishedName", value)
        End Set
    End Property

    ' Create the other home phone property.    
    <DirectoryProperty("otherHomePhone")>
    Public Property HomePhoneOther As String()
        Get
            Dim len As Integer = ExtensionGet("otherHomePhone").Length

            Dim otherHomePhone = New String(len - 1) {}
            Dim otherHomePhoneRaw As Object() = ExtensionGet("otherHomePhone")

            For i = 0 To len - 1
                otherHomePhone(i) = CStr(otherHomePhoneRaw(i))
            Next

            Return otherHomePhone
        End Get

        Set(ByVal value As String())
            ExtensionSet("otherHomePhone", value)
        End Set
    End Property

    ' Create the logoncount property.    
    <DirectoryProperty("LogonCount")>
    Public ReadOnly Property LogonCount As Integer?
        Get
            If ExtensionGet("LogonCount").Length <> 1 Then Return Nothing

            Return CType(ExtensionGet("LogonCount")(0), Integer?)
        End Get
    End Property

    ' Implement the overloaded search method FindByIdentity.
    Public Overloads Shared Function FindByIdentity(ByVal context As PrincipalContext, ByVal identityValue As String) As UserProxyPrincipal
        Return CType(FindByIdentityWithType(context, GetType(UserProxyPrincipal), identityValue), UserProxyPrincipal)
    End Function

    ' Implement the overloaded search method FindByIdentity. 
    Public Overloads Shared Function FindByIdentity(ByVal context As PrincipalContext, ByVal identityType As IdentityType, ByVal identityValue As String) As UserProxyPrincipal
        Return CType(FindByIdentityWithType(context, GetType(UserProxyPrincipal), identityType, identityValue), UserProxyPrincipal)
    End Function



End Class

Public Class UserProxyPrincipalSearchFilter
    Inherits AdvancedFilters
    Public Sub New(ByVal p As Principal)
        MyBase.New(p)
    End Sub
    Public Sub LogonCount(ByVal value As Integer, ByVal mt As MatchType)
        Me.AdvancedFilterSet("LogonCount", value, GetType(Integer), mt)
    End Sub

End Class
'

