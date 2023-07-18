' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Web.Mvc

Namespace Markout.Controllers
    <Authorize()>
    Public Class MenuController
        Inherits PickProController

        ' GET: Markout
        Function Index() As ActionResult
            Return View()
        End Function

        <HttpGet()>
        Public Function GetParamByParamName(ByVal ParamName As String) As ActionResult
            Return Json(SelectParamByName(Session("WSID"), ParamName), JsonRequestBehavior.AllowGet)
        End Function

        <HttpGet()>
        Public Function SelectToteData(ByVal ToteID As String) As ActionResult
            Return Json(GetToteData(Session("WSID"), ToteID, User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Public Function UpdateShortQuantity(ByVal OTID As Integer, ByVal Quant As Integer) As ActionResult
            Return Json(UpdateQuantity(Session("WSID"), OTID, Quant, User.Identity.Name), JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Public Function CompleteTrans(ByVal OTID As Integer, ByVal ShipShort As Boolean) As ActionResult
            If ShipShort Then
                Return Json(ShipShortTransaction(Session("WSID"), OTID, User.Identity.Name), JsonRequestBehavior.AllowGet)
            Else
                Return Json(CompleteTransaction(Session("WSID"), OTID, User.Identity.Name), JsonRequestBehavior.AllowGet)
            End If
        End Function

        <HttpPost()>
        Public Function ValidTote(ByVal ToteID As String) As ActionResult
            Return Json(ValidateTote(Session("WSID"), ToteID), JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost()>
        Public Function BlossTote(ByVal OTIDs As List(Of List(Of Integer)), ByVal NewTote As String, IsBlossComp As Boolean) As ActionResult
            Return Json(BlossomTote(Session("WSID"), OTIDs, NewTote, User.Identity.Name, IsBlossComp), JsonRequestBehavior.AllowGet)
        End Function

        
    End Class
End Namespace