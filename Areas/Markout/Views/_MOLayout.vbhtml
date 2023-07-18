<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "MO"
    ViewData("AppHome") = "MO"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@RenderBody

@Section HelpLink
    <li><a href="/MO/MOHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="MO" data-version="10/7/2022" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            IM: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("Markout")
            </strong>
        </a>
    </li>
End Section
