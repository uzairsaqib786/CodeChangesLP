<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Location Zones
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <button type="button" class="btn btn-primary dropdown-toggle" id="addNewLocationZone" data-toggle="tooltip" data-placement="top" data-original-title="Add New Location Zone"><span class="glyphicon glyphicon-plus"></span></button>
                        <div class="btn-group" data-toggle="tooltip" data-placement="right" data-original-title="Delete Location Zone">
                            <button data-toggle="dropdown" type="button" class="btn btn-danger dropdown-toggle"><span class="glyphicon glyphicon-trash"></span><span class="caret"></span></button>
                            <ul id="locationZoneList" class="dropdown-menu" role="menu">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="row" style="margin-top:5px;">
                    <div class="col-md-4">
                        <input type="text" maxlength="2" style="display:none;" class="form-control" id="newLocationZone" placeholder="New Zone Name" />
                    </div>
                    <div class="col-md-2">
                        <button style="display:none;" type="button" class="btn btn-primary" id="submitNewLZ" disabled="disabled" data-toggle="tooltip" data-placement="top" data-original-title="Save New Location Zone"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                        <button style="display:none;" type="button" class="btn btn-danger" id="removeNewLZ" data-toggle="tooltip" data-placement="top" data-original-title="Cancel New Location Zone"><span class="glyphicon glyphicon-remove"></span></button>
                    </div>
                </div>
                <div class="row" style="padding-top:5px;">
                    <div class="col-md-12" id="LocationZonesAccordion" style="overflow-y:scroll;max-height:650px;">

                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/LocationNameModalPartial.vbhtml")