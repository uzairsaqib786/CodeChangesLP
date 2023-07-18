<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Dim ColumnAlias As PickPro_Web.AliasModel = Model
    Dim userAlias As List(Of String) = ColumnAlias.UserFields
    
End Code
<div class="modal fade" id="userfields_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="userfields_label" aria-hidden="true">
    <div class="modal-dialog" style="width:900px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="userfields_label">User Fields - Edit</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <input type="text" hidden="hidden" id="userfields_sender" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>User Fields</label>
                    </div>
                    <div class="col-md-6">
                        <button disabled="disabled" type="button" class="btn btn-primary pull-right" id="userfields_save"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <label>@userAlias(0)</label>
                                <input class="form-control" maxlength="255" id="UF1" placeholder="@userAlias(0)" />
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(1)</label>
                                <input class="form-control" maxlength="255" id="UF2" placeholder="@userAlias(1)" />
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(2)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF3" placeholder="@userAlias(2)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(3)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF4" placeholder="@userAlias(3)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(4)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF5" placeholder="@userAlias(4)"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <label>@userAlias(5)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF6" placeholder="@userAlias(5)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(6)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF7" placeholder="@userAlias(6)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(7)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF8" placeholder="@userAlias(7)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(8)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF9" placeholder="@userAlias(8)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@userAlias(9)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="UF10" placeholder="@userAlias(9)"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-4 col-md-offset-8 pull-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="userfields_dismiss">Close</button>
                        <button disabled="disabled" type="button" class="btn btn-primary" data-dismiss="modal" id="userfields_sd" style="margin-left:5px;">Save & Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Modals/UserFieldsModal.js"></script>