<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "OM"
    ViewData("AppHome") = "OM"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@RenderBody

@Section HelpLink
    <li><a href="/OM/OMHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="OM" data-version="3/4/16" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            OM: <strong style="font-size: 18px; color: dodgerblue;">
                    @PickPro_Web.AppBuildInfo.DisplayVersion("OrderManager")
            </strong>
        </a>
    </li>
End Section