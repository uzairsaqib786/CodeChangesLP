<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "CM"
    ViewData("AppHome") = "CM"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@RenderBody

@Section HelpLink
    <li><a href="/CM/CMHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="CM" data-version="3/4/16" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            CM: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("Consolidation_Manager")
            </strong>
        </a>
    </li>
End Section