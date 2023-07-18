<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Lookup List Setup
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <ul class="nav nav-tabs" role="tablist" id="LookupListSetupNav">
                            <li class="active"><a href="#user1Setup" role="tab" data-toggle="tab">@Model.aliases.UserFields(0) Setup</a></li>
                            <li><a href="#user2Setup" role="tab" data-toggle="tab">@Model.aliases.UserFields(1) Setup</a></li>
                            <li><a href="#adjustmentSetup" role="tab" data-toggle="tab">Adjustment Lookup Setup</a></li>
                            <li><a href="#toteSetup" role="tab" data-toggle="tab">Tote Setup</a></li>
                        </ul>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="tab-content">
                            <div class="tab-pane active" id="user1Setup">
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-12">
                                        <h4>The @Model.aliases.UserFields(0) Lookup List can be configured by adding, deleting or modifying the suggestions that will be rendered to the user on related screens.</h4>                                    
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="UF1Alerts">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <button type="button" class="btn btn-primary" id="UF1Add" data-toggle="tooltip" data-placement="top" data-original-title="Add UF1 Suggestion"><span class="glyphicon glyphicon-plus"></span></button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="UF1Setup_Content" style="overflow-y:scroll;">

                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="user2Setup">
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-12">
                                        <h4>The @Model.aliases.UserFields(1) Lookup List can be configured by adding, deleting or modifying the suggestions that will be rendered to the user on related screens.</h4>
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="UF2Alerts">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <button type="button" class="btn btn-primary" id="UF2Add" data-toggle="tooltip" data-placement="top" data-original-title="Add UF2 Suggestion"><span class="glyphicon glyphicon-plus"></span></button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="UF2Setup_Content" style="overflow-y:scroll;">

                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="adjustmentSetup">
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-12">
                                        <h4>The Adjustment Lookup List can be configured by adding, deleting or modifying the suggestions that will be rendered to the user on related screens.</h4>
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="AdjustAlerts">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <button type="button" class="btn btn-primary" id="AdjustAdd" data-toggle="tooltip" data-placement="top" data-original-title="Add Adjustment Reason"><span class="glyphicon glyphicon-plus"></span></button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="AdjustmentSetup_Content" style="overflow-y:scroll;">

                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="toteSetup">
                                <div class="row" style="padding-top:15px;">
                                    <div class="col-md-12">
                                        <h4>Tote Management can be configured by adding, deleting or modifying the pre-existing totes from your facility.  If configured, the system will check Tote ID entries against this list for validation.</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="ToteAlerts">

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <button type="button" class="btn btn-primary" id="ToteAdd" data-toggle="tooltip" data-placement="top" data-original-title="Add Existing Tote ID"><span class="glyphicon glyphicon-plus"></span></button> 
                                        <button type="button" class="btn btn-primary" id="ToteClear" data-toggle="tooltip" data-placement="top" data-original-title="Clear ALL Tote IDs on incomplete open transactions">Clear ALL Totes</button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Tote ID</label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Cells</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="ToteSetup_Content" style="overflow-y:scroll;">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
