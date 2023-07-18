<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-3">
        <div class="panel panel-info">
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        @If Model.Prefs.AutoPrintDockLabel Then
                            @<b>Auto Print Cross Dock Label: </b>@<input type="checkbox" checked="checked" id="AutoPrintCrossDock" class="pull-right" />
                        Else
                            @<b>Auto Print Cross Dock Label: </b>@<input type="checkbox" id="AutoPrintCrossDock" class="pull-right" />
                        End If

                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintPickLabels Then
                            @<b>Auto Print Pick Labels: </b>@<input type="checkbox" checked="checked" id="AutoPrintPick" class="pull-right" />
                        Else
                            @<b>Auto Print Pick Labels: </b>@<input type="checkbox" id="AutoPrintPick" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.PickLabelsOnePer Then
                            @<b>Pick Labels One Per Qty: </b>@<input type="checkbox" checked="checked" id="PickLabelsOnePer" class="pull-right" />
                        Else
                            @<b>Pick Labels One Per Qty: </b>@<input type="checkbox" id="PickLabelsOnePer" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintPickTote Then
                            @<b>Auto Print Pick Tote Labels: </b>@<input type="checkbox" checked="checked" id="AutoPrintPickTote" class="pull-right" />
                        Else
                            @<b>Auto Print Pick Tote Labels: </b>@<input type="checkbox" id="AutoPrintPickTote" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintPutTote Then
                            @<b>Auto Print Put Away Tote Labels: </b>@<input type="checkbox" checked="checked" id="AutoPrintPutTote" class="pull-right" />
                        Else
                            @<b>Auto Print Put Away Tote Labels: </b>@<input type="checkbox" id="AutoPrintPutTote" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintOffCarPickList Then
                            @<b>Auto Print Off Carousel Pick List: </b>@<input type="checkbox" checked="checked" id="AutoPrintOffCarPickList" class="pull-right" />
                        Else
                            @<b>Auto Print Off Carousel Pick List: </b>@<input type="checkbox" id="AutoPrintOffCarPickList" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintOffCarPutList Then
                            @<b>Auto Print Off Carousel Put Away List: </b>@<input type="checkbox" checked="checked" id="AutoPrintOffCarPutList" class="pull-right" />
                        Else
                            @<b>Auto Print Off Carousel Put Away List: </b>@<input type="checkbox" id="AutoPrintOffCarPutList" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintPutLabels Then
                            @<b>Auto Print Put Away Label: </b>@<input type="checkbox" checked="checked" id="AutoPrintPutLabel" class="pull-right" />
                        Else
                            @<b>Auto Print Put Away Label: </b>@<input type="checkbox" id="AutoPrintPutLabel" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.ReqNumPutLabels Then
                            @<b>Request Number of Put Away Labels: </b>@<input type="checkbox" checked="checked" id="ReqNumPutLabels" class="pull-right" />
                        Else
                            @<b>Request Number of Put Away Labels: </b>@<input type="checkbox" id="ReqNumPutLabels" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.AutoPrintPickBatchList Then
                            @<b>Auto Print Pick Batch List: </b>@<input type="checkbox" checked="checked" id="AutoPrintPickBatchList" class="pull-right" />
                        Else
                            @<b>Auto Print Pick Batch List: </b>@<input type="checkbox" id="AutoPrintPickBatchList" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        @If Model.Prefs.PrintDirect Then
                            @<b>Print Directly to Printer: </b>@<input type="checkbox" checked="checked" id="PrintDirect" class="pull-right" />
                        Else
                            @<b>Print Directly to Printer: </b>@<input type="checkbox" id="PrintDirect" class="pull-right" />
                        End If
                    </div>
                    <div class="col-md-12 top-spacer">
                        <b>Max Number of Put Away Labels: </b><input type="text" oninput="setNumeric($(this))" id="MaxNumPutLabels" class="form-control" value="@Model.Prefs.MaxNumPutLabels" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>