<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="row">
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Stock Count Confirmation</h3>
            </div>
            <div class="panel-body" id="stockCountConfirmation">
                <div class="row">
                    <div class="col-xs-6">
                        <label>Carousel Pick</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="carouselPick"></div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-xs-6">
                        <label>Carousel Put Away</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="carouselPut"></div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-xs-6">
                        <label>Hot Pick</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="hotPick"></div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-xs-6">
                        <label>Hot Put Away</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="hotPut"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Other Settings</h3>
            </div>
            <div class="panel-body" id="otherSettings">
                <div class="row">
                    <div class="col-md-6">
                        <label>Idle Warning Time</label>
                        <input type="text" class="form-control" id="idleMessTime" placeholder="Idle Message Time" />
                    </div>
                    <div class="col-md-6">
                        <label>Idle Log-Off Time</label>
                        <input type="text" class="form-control" id="idleMessShutDownTime" placeholder="Idle Message Shut Down Time" />
                    </div>
                </div>
                <div class="row" style="padding-top:30px;">
                    <div class="col-xs-6">
                        <label>Force Order Number for Hot Pick</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="forceOrderNumHotPick"></div>
                    </div>
                </div>
                <div class="row" style="padding-top:10px;">
                    <div class="col-xs-6">
                        <label>Force Order Number for Hot Put Away</label>
                    </div>
                    <div class="col-xs-6">
                        <div class="toggles toggle-modern pull-right" id="forceOrderNumHotPut"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">Inactive Carousels</h3>
            </div>
            <div class="panel-body" id="inactiveCarousels">
                <div class="row">
                    <div class="col-md-10">
                        <label>Carousels</label>
                    </div>
                    <div class="col-md-2">
                        <label>Inactive Carousel</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12" id="inactiveCars">
                        @For x As Integer = 1 To 8
                            @<div class="row">
                                <div class="col-md-10">
                                    <input style="margin-top:5px;" disabled type="text" class="form-control" value="@x" />
                                </div>
                                <div class="col-md-2">
                                    <div style="margin-top:7px;" class="toggles toggle-modern Inactive_Cars" data-toggle-ontext="Yes" data-toggle-offtext="No" data-name="@x">
                                    </div>
                                </div>
                            </div>
                        Next
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
