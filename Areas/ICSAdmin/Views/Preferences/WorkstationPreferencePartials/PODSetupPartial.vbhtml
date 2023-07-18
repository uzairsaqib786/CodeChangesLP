<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row">
    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">PODs</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <label>Zone</label>
                    </div>
                    <div class="col-md-5">
                        <label>Maximum Orders</label>
                    </div>
                    <div class="col-md-2">
                        <label>POD Zone</label>
                    </div>
                </div>
                <div class="row" style="max-height:625px;overflow-y:scroll;">
                    <div class="col-md-12" id="PODContainer">

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-6">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">POD Preferences</h3>
            </div>
            <div class="panel-body" id="PODPrefs">
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Choose Carousel Spin Sort</label>
                                        <select class="form-control" id="ChooseCarSpinSort">
                                            <option value="First Available">First Available</option>
                                            <option value="Round Robin">Round Robin</option>
                                            <option value="Round Robin - Whole Bin">Round Robin-Whole Bin</option>
                                            <option value="Item Number Pick Sequence">Item Number Pick Sequence</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Carton Flow Sequence</label>
                                        <select class="form-control" id="CartonFlowSequence">
                                            <option value="Before Carousel Transactions">Before Carousel Transactions</option>
                                            <option value="First">First</option>
                                            <option value="After Carousel Transactions">After Carousel Transactions</option>
                                            <option value="Last">Last</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:30px;">
                                    <div class="col-xs-6">
                                        <label>Carousel Workstation</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="CarWorkstation"></div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-xs-6">
                                        <label>Use 20 Position Matrix</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="Use20Matrix"></div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-xs-6">
                                        <label>Item Num On Light Tree</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="ItemNumLightTree"></div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-xs-6">
                                        <label>Pull When Case Full</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="PullWhenFull"></div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-md-6">
                                        <label>Ignore Task Complete</label>
                                    </div>
                                    <div class="col-xs-6">
                                        <div class="toggles toggle-modern pull-right" id="IgnoreTaskComplete"></div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top:10px;">
                                    <div class="col-md-6">
                                        <label>CIB Delay</label>
                                        <input type="text" class="form-control" id="cibDelay" placeholder="CIB Delay" />
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
