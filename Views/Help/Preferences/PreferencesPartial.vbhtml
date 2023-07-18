<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout=nothing
End Code
<div class="panel-group" id="PrefsAccordion">
    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#PrefsAccordion" data-target="#WSPrefs">
                <h3 class="panel-title">
                    Workstation Preferences
                    <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-info collapse accordion-toggle" id="WSPrefs">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Preferences/WSPrefsPartial.vbhtml")
            </div>
        </div>
    </div>

    <div class="panel panel-info">
        <div class="panel-heading">
            <a data-toggle="collapse" data-parent="#PrefsAccordion" data-target="#SystPrefs">
                <h3 class="panel-title">
                    System Preferences
                    <span class="accordion-caret-down"></span>
                </h3>
            </a>
        </div>
        <div class="panel-info collapse accordion-toggle" id="SystPrefs">
            <div class="panel-body">
                @Html.Partial("~/Views/Help/Preferences/SystPrefsPartial.vbhtml")
            </div>
        </div>
    </div>
</div>