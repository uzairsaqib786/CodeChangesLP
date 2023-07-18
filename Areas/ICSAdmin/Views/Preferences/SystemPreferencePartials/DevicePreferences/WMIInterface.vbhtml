<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <label>Host IP Address</label>
        <input type="text" class="form-control" id="HostIP" placeholder="Host IP Address" maxlength="50" />
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <label>Host Port</label>
        <input type="text" class="form-control" id="HostPort" placeholder="Host Port" />
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <label>Workstation Name</label>
        <input type="text" class="form-control" id="WSName" placeholder="Workstation Name" maxlength="50" />
    </div>
</div>
<div class="row" id="JMIF_row" style="display:none;">
    <div class="col-md-12">
        <label>JMIF Carousel Name</label>
        <input type="text" class="form-control" id="JMIF" placeholder="JMIF Carousel Name" maxlength="50" />
    </div>
</div>
<div class="row" style="padding-top:10px;">
    <div class="col-md-6">
        <button type="button" class="btn btn-primary" id="SetWS">Assign Device To This Workstation</button>
    </div>
    <div class="col-md-6" style="display:none;" id="SetupTilt_div">
        <button type="button" class="btn btn-primary" id="SetupTrayTilt">Setup Tray Tilt</button>
    </div>
</div>