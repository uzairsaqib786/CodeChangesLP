<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "IM"
    ViewData("AppHome") = "IM"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

@RenderBody

@Section HelpLink
    <li><a href="/IM/IMHelp?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="IM" data-version="3/4/16" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            IM: <strong style="font-size: 18px; color: dodgerblue;">
                    @PickPro_Web.AppBuildInfo.DisplayVersion("Induction")
            </strong>
        </a>
    </li>
End Section
