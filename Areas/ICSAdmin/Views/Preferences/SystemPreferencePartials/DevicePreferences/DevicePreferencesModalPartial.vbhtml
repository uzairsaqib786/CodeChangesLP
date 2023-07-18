<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="modal fade" id="devpref_modal" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="devpref_label" aria-hidden="true">
    <div class="modal-dialog" style="width:1100px;">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="devpref_label">Devices - Add, Edit, Delete</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <legend>Device Details <span class="label label-default pull-right" id="devprefs_id" style="margin-bottom:20px;"></span></legend>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Zone</label>
                                <select class="form-control" id="devprefs_zone"></select>
                            </div>
                            <div class="col-md-4">
                                <label>Device Type</label>
                                <select class="form-control" id="devprefs_type">
                                    <option>Carousel</option>
                                    <option>Carton Flow</option>
                                    <option>Light Tree</option>
                                    <option>Scale</option>
                                    <option>Scanner</option>
                                    <option>Sort Bar</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Device Number</label>
                                <select class="form-control" id="devprefs_devnum">
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                    <option>6</option>
                                    <option>7</option>
                                    <option>8</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            
                            <div class="col-md-4">
                                <label>Device Model</label>
                                <select class="form-control" id="devprefs_model"></select>
                            </div>
                            <div class="col-md-4">
                                <label>Controller Type</label>
                                <select class="form-control" id="devprefs_ctype"></select>
                            </div>
                            <div class="col-md-4">
                                <label>Controller Terminal Port</label>
                                <select class="form-control" id="devprefs_port">
                                    <option>11</option>
                                    <option>12</option>
                                    <option>13</option>
                                    <option>21</option>
                                    <option>22</option>
                                    <option>23</option>
                                    <option>31</option>
                                    <option>32</option>
                                    <option>33</option>
                                    <option>41</option>
                                    <option>42</option>
                                    <option>43</option>
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>VC</option>
                                    <option>DL</option>
                                    <option>IP</option>
                                    <option>DR</option>
                                    <option>I1</option>
                                    <option>I2</option>
                                    <option>I3</option>
                                    <option>I4</option>
                                    <option>I5</option>
                                    <option>I6</option>
                                    <option>I7</option>
                                    <option>I8</option>
                                    <option>I9</option>
                                    <option>WMI</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            
                            <div class="col-md-4">
                                <label>Arrow Direction</label>
                                <select class="form-control" id="devprefs_arrow">
                                    <option value="L">Left</option>
                                    <option value="R">Right</option>
                                    <option value="U">Up</option>
                                    <option value="D">Down</option>
                                    <option value="B">Left and Right</option>
                                    <option value="P">Up and Down</option>
                                    <option value="N">None</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Light Direction</label>
                                <select class="form-control" id="devprefs_light">
                                    <option value="L">Left to Right</option>
                                    <option value="R">Right to Left</option>
                                    <option value="U">Bottom Up</option>
                                    <option value="D">Top Down</option>
                                    <option value="Twin Bin R to L">Twin Bin R to L</option>
                                    <option value="Twin Bin L to R">Twin Bin L to R</option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Use Light Tree Number</label>
                                <select class="form-control" id="devprefs_lightnum">
                                    <option>0</option>
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                    <option>6</option>
                                    <option>7</option>
                                    <option>8</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            
                            <div class="col-md-4">
                                <label>First Address</label>
                                <input type="text" class="form-control" id="devprefs_address" placeholder="First Address" maxlength="5" />
                            </div>
                            <div class="col-md-4">
                                <label>Display Positions</label>
                                <input type="text" class="form-control" id="devprefs_positions" placeholder="Display Positions" maxlength="9" />
                            </div>
                            <div class="col-md-4">
                                <label>Display Characters</label>
                                <input type="text" class="form-control" id="devprefs_characters" placeholder="Display Characters" maxlength="9" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <label>Pair Key</label>
                                <input type="text" class="form-control" id="PairKey" placeholder="Pair Key" maxlength="50" />
                            </div>
                            <div class="col-md-4" style="padding-top:5px;">
                                <label>Use Laser Pointer</label>
                                <div class="toggles toggle-modern pull-right" id="devprefs_laser"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="CommHeader">
                    <div class="col-md-12">
                        <legend>Communication Settings</legend>
                    </div>
                </div>
                <div class="row" style="display:none;" id="IPTI">
                    <div class="col-md-12">
                        @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferences/IPTILightTree.vbhtml")
                    </div>
                </div>
                <div class="row" style="display:none;" id="WMI">
                    <div class="col-md-12">
                        @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferences/WMIInterface.vbhtml")
                    </div>
                </div>
                <div class="row" style="display:none;" id="OtherInterface">
                    <div class="col-md-12">
                        @Html.Partial("~/Areas/ICSAdmin/Views/Preferences/SystemPreferencePartials/DevicePreferences/OtherInterface.vbhtml")
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="devpref_dismiss">Close</button>
                <button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Save and Close" class="btn btn-primary" id="devpref_saveclose"><span class="glyphicon glyphicon-floppy-disk"></span> And Close</button>
                <button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Save and Stay Open" class="btn btn-primary" id="devpref_save"><span class="glyphicon glyphicon-floppy-disk"></span> And Stay Open</button>
                <button type="button" data-toggle="tooltip" data-placement="top" data-original-title="Delete Device" class="btn btn-danger" id="devpref_delete"><span class="glyphicon glyphicon-trash"></span></button>
            </div>
        </div>
    </div>
</div>