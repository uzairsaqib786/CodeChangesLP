' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Induction.Controllers
    <Authorize()>
    Public Class PalletReceivingController
        Inherits PickProController

        ' GET: PalletReceiving
        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace