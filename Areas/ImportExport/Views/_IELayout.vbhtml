<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "IE"
    ViewData("AppHome") = "IE"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@RenderBody()

<script src="/Scripts/moment.min.js"></script>
<script src="/Scripts/bootstrap-datetimepicker.js"></script>
<script src="~/Areas/ImportExport/Scripts/ImportExport.js"></script>


@Section AppName
    <input type="hidden" id="AppName" value="IE" data-version="3/4/16" />
End Section

@Section HelpLink
    <li><a href="/IE/IEHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            IE: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("ImportExport")
            </strong>
        </a>
    </li>
End Section