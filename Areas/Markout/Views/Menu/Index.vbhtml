<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Menu"
    ViewData("PageName") = "| Menu"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code



<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-md-2">
                        <input autofocus type="text" style="font-size:x-large;" class="form-control" id="ToteIDScan" placeholder="Tote ID" />
                    </div>
                    <div Class="col-md-3">
                        <Label style="font-size:x-large"> Order Number:</Label>
                        <span id="OrderNum" style="font-size:x-large;color:black"></span>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label style="font-size:x-large">Tote Status:</label>
                            <span id="ToteStatus" style="font-size:x-large;color:black"></span>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <Label id="MarkoutStatus" style="font-size:x-large; color:white;background-color:blue; display:none; padding:6px">Markout Status</Label>
                        <Label id="Blossomed" style="font-size:x-large; color:white; background-color:blue; display:none; padding:6px;margin-left:4px">Blossomed <span id="BlossomCount"></span></Label>
                    </div>
                </div>
                <div Class="row">
                    <div Class="col-md-5">
                        <div Class="btn-toolbar">
                            <Button Class="btn btn-primary" style="font-size:x-large" id="Blossom" disabled="disabled">Blossom</Button>
                            <Button Class="btn btn-primary" style="font-size:x-large" id="BlossomComplete" disabled="disabled">Blossom Complete</Button>
                        </div>
                    </div>

                </div>
            </div>
            <div Class="panel-body">
                <div Class="row">
                    <div Class="col-md-12">
                        <div Class="table-responsive">
                            <table Class="table table-striped text-center">
                                <thead>
                                    <tr>
                                        <th style="font-size:x-large;text-align:center"> ToteID</th>
                                        <th style="font-size:x-large;text-align:center"> Item Number</th>
                                        <th style="font-size:x-large;text-align:center"> Location</th>
                                        <th style="font-size:x-large;text-align:center"> Trans Quantity</th>
                                        <th style="font-size:x-large;text-align:center"> Picked Quantity</th>
                                        <th style="font-size:x-large;text-align:center"> Short Quantity</th>
                                        <th style="font-size:x-large;text-align:center"> Status</th>
                                    </tr>
                                </thead>
                                <tbody id="markoutdata"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/Markout/Markout.js"></script>

@Html.Partial("BlossomModal")
@Html.Partial("ConfirmModal")
