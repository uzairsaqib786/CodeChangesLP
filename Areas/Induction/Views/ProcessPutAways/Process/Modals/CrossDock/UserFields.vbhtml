<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

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
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(0)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF1" placeholder="@Model.aliases.UserFields(0)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(1)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF2" placeholder="@Model.aliases.UserFields(1)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(2)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF3" placeholder="@Model.aliases.UserFields(2)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(3)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF4" placeholder="@Model.aliases.UserFields(3)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(4)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF5" placeholder="@Model.aliases.UserFields(4)"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(5)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF6" placeholder="@Model.aliases.UserFields(5)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(6)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF7" placeholder="@Model.aliases.UserFields(6)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(7)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF8" placeholder="@Model.aliases.UserFields(7)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(8)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF9" placeholder="@Model.aliases.UserFields(8)"></textarea>
                            </div>
                            <div class="col-md-12">
                                <label>@Model.aliases.UserFields(9)</label>
                                <textarea class="form-control no-horizontal" rows="3" maxlength="255" id="CDUF10" placeholder="@Model.aliases.UserFields(9)"></textarea>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div class="row">
                    <div class="col-md-4 col-md-offset-8 pull-right">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="userfields_dismiss">Close</button>
                        <button type="button" class="btn btn-primary" data-dismiss="modal" id="userfields_submit" style="margin-left:5px;">Submit</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
