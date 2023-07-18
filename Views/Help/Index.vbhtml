<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Help"
    ViewData("PageName") = "&nbsp; | &nbsp; Help"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-2" style="overflow-y:scroll;" id="HelpSelector">
            <ul class="nav nav-pills nav-stacked" role="tablist">
                <li><a id="help" class="request-partial">Help Home</a></li>
                @Code
                    Dim pages As Dictionary(Of String, string) = TryCast(Model(3), Dictionary(of String, string))
                    Dim keys As List(Of String) = pages.Keys.ToList()
                    keys.Sort()
                    For Each key As String In keys
                        @<li><a id="@pages(key)" class="request-partial">@key</a></li>
                    Next
                End Code
            </ul>
        </div>
        <div class="col-md-10" id="HelpContent">
            
        </div>
    </div>
</div>
<input type="hidden" value="@Model(0)" id="initialPage" />
<input type="hidden" value="@Model(1)" id="subPage" />
<script src="~/Scripts/Help/SharedHelp.js"></script>

<link href="~/Content/toggles.css" rel="stylesheet" />
<link href="~/Content/toggles-modern.css" rel="stylesheet" />
<script src="~/Scripts/toggles.min.js"></script>
