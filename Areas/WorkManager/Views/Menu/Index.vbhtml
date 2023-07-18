<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType List(Of boolean)
@Code
    ViewData("Title") = "Menu"
    ViewData("PageName") = "| Menu"
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-3">
            <a href="/WM/SelectWork" class="btn btn-primary btn-xl btn-block bottom-spacer">Select Work</a>
        </div>
        <div class="col-md-3">
            @If Model(1) Then
                @<a href="/WM/OrganizeWork" class="btn btn-primary btn-xl btn-block bottom-spacer">Organize Work</a>
            Else
                @<button class="btn btn-xl btn-primary btn-block bottom-spacer" disabled="disabled">Organize Work</button>
            End If
        </div>
        <div class="col-md-3">
            @If Model(2) Then
                @<a href="/CustomReports?App=WM" class="btn btn-xl btn-primary btn-block">Reports</a>
            Else
                @<button class="btn btn-xl btn-primary btn-block bottom-spacer" disabled="disabled">Reports</button>
            End If
        </div>
        <div class="col-md-3">
            @If Model(0) Then
                @<a href="/WM/Preferences" class="btn btn-xl btn-primary btn-block bottom-spacer">Work Preferences</a>
            Else
                @<button class="btn btn-xl btn-primary btn-block bottom-spacer" disabled="disabled">Work Preferences</button>
            End If
        </div>
    </div>
</div>

