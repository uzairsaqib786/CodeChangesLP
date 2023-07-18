<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Scripts.Render("~/bundles/jquery")
<link href="~/Content/jquery.mobile-1.4.5.min.css" rel="stylesheet" />
<script src="~/Scripts/jquery.mobile-1.4.5.min.js"></script>
@ModelType PickPro_Web.ExportServiceModel
@Imports PickPro_Web
@Imports combit.ListLabel21.Web
@Html.Html5Viewer(DirectCast(ViewBag.FileUrl, String), New Html5ViewerOptions() With {.CustomData = Model, .UseCDNType = CDNType.Inherited })
