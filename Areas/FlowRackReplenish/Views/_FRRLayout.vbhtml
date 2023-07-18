<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->


@Code
    ViewData("App") = "FRR"
    ViewData("AppHome") = "FRR"
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code

@RenderBody

@Section HelpLink
    <li><a href="/FRR/FRRHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="FRR" data-version="3/4/16" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            FRR: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("FlowRackReplenish")
            </strong>
        </a>
    </li>
End Section
