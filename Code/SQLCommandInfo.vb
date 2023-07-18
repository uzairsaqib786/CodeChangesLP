' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class SQLCommandInfo
    Private SP_ As String
    Private Parms_ As String(,)

    Public Property SP As String
        Get
            Return SP_
        End Get
        Set(value As String)
            SP_ = value
        End Set
    End Property

    Public Property Params As String(,)
        Get
            Return Parms_
        End Get
        Set(value As String(,))
            Parms_ = value
        End Set
    End Property
End Class
