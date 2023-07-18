' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Public Class ccSetting
    Property StartIndex As Integer
    Property LenIndex As Integer

    Sub New(StartIndex As Integer, LenIndex As Integer)
        Me.StartIndex = StartIndex
        Me.LenIndex = LenIndex
    End Sub
End Class
