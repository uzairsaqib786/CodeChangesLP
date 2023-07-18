<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

End Code

@RenderBody


@*Render section error when this code is not commented out.*@

@*@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            PP WEB: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("PickPro Web")
            </strong>
        </a>
    </li>
End Section*@