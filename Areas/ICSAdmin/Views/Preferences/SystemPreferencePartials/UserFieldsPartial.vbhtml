<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    PickPro Field Name Mappings
                </h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        Enter the text to display in place of system field names.  These aliases will appear in place of system field names on forms and in reports.
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-primary" data-toggle="tooltip" data-original-title="Reset All to Defaults" data-placement="top" id="resetFieldNames"><span class="glyphicon glyphicon-remove"></span></button>
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-6">
                        <label>Item Number</label>
                        <input maxlength="50" id="itemNumberAlias" type="text" class="form-control" placeholder="Item Number Alias" value="@Model.aliases.ItemNumber" name="@Model.aliases.ItemNumber" />
                    </div>
                    <div class="col-md-6">
                        <label>Unit of Measure</label>
                        <input maxlength="50" id="UoMAlias" type="text" class="form-control" placeholder="Unit of Measure Alias" value="@Model.aliases.UoM" name="@Model.aliases.UoM" />
                    </div>
                </div>
                <div class="row" style="padding-top:15px;">
                    <div class="col-md-6">
                        <label>User Field1</label>
                        <input maxlength="50" id="UF1" type="text" class="form-control" placeholder="User Field 1 Alias" value="@Model.aliases.UserFields(0)" name="@Model.aliases.UserFields(0)" />
                    </div>
                    <div class="col-md-6">
                        <label>User Field2</label>
                        <input maxlength="50" id="UF2" type="text" class="form-control" placeholder="User Field 2 Alias" value="@Model.aliases.UserFields(1)" name="@Model.aliases.UserFields(1)" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>User Field3</label>
                        <input maxlength="50" id="UF3" type="text" class="form-control" placeholder="User Field 3 Alias" value="@Model.aliases.UserFields(2)" name="@Model.aliases.UserFields(2)" />
                    </div>
                    <div class="col-md-6">
                        <label>User Field4</label>
                        <input maxlength="50" id="UF4" type="text" class="form-control" placeholder="User Field 4 Alias" value="@Model.aliases.UserFields(3)" name="@Model.aliases.UserFields(3)" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>User Field5</label>
                        <input maxlength="50" id="UF5" type="text" class="form-control" placeholder="User Field 5 Alias" value="@Model.aliases.UserFields(4)" name="@Model.aliases.UserFields(4)" />
                    </div>
                    <div class="col-md-6">
                        <label>User Field6</label>
                        <input maxlength="50" id="UF6" type="text" class="form-control" placeholder="User Field 6 Alias" value="@Model.aliases.UserFields(5)" name="@Model.aliases.UserFields(5)" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>User Field7</label>
                        <input maxlength="50" id="UF7" type="text" class="form-control" placeholder="User Field 7 Alias" value="@Model.aliases.UserFields(6)" name="@Model.aliases.UserFields(6)" />
                    </div>
                    <div class="col-md-6">
                        <label>User Field8</label>
                        <input maxlength="50" id="UF8" type="text" class="form-control" placeholder="User Field 8 Alias" value="@Model.aliases.UserFields(7)" name="@Model.aliases.UserFields(7)" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>User Field9</label>
                        <input maxlength="50" id="UF9" type="text" class="form-control" placeholder="User Field 9 Alias" value="@Model.aliases.UserFields(8)" name="@Model.aliases.UserFields(8)" />
                    </div>
                    <div class="col-md-6">
                        <label>User Field10</label>
                        <input maxlength="50" id="UF10" type="text" class="form-control" placeholder="User Field 10 Alias" value="@Model.aliases.UserFields(9)" name="@Model.aliases.UserFields(9)" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>