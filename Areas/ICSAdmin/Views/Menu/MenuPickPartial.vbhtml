<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title text-center">Auto</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Carousel Pick") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Carousel Put Away") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put-Away</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put-Away</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Carousel Count") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Count</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Count</button>
                                End If
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title text-center">Bulk</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Bulk Pick") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Bulk Put Away") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put Away</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put Away</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Bulk Cycle Count") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Count</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Count</button>
                                End If
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title text-center">Hot</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Hot Pick") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Pick</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Hot Put Away") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put Away</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Put Away</button>
                                End If
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10 col-md-offset-1">
                                @If permissions.Contains("Blind Spin") Then
                                    @<button style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Spin to Location</button>
                                Else
                                    @<button disabled="disabled" style="margin-bottom:20px;" type="button" class="btn btn-lg btn-block btn-primary">Spin to Location</button>
                                End If
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h2 class="panel-title text-center" style="font-size:20px;">Connected to Automated POD:   <label class="label label-default">@Model.AutomatedPOD</label></h2>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div id="allocatedTableScroll" style="overflow-y:scroll;">
                            <table class="table table-bordered table-striped table-condensed" cellspacing="0" role="grid" style="background-color:white;">
                                <thead>
                                    <tr>
                                        <th class="text-center">Zone</th>
                                        <th class="text-center">Warehouse</th>
                                        <th class="text-center">Location</th>
                                        <th class="text-center"># Lines</th>
                                        <th class="text-center">Trans. Type</th>
                                    </tr>
                                </thead>
                                <tbody id="allocatedTransTBody">
                                    <!-- table body, tr, td, etc -->
                                    @For x As Integer = 0 To Model.TableData.Count - 1
                                        @<tr>
                                            @For y As Integer = 0 To Model.TableData(x).Count - 1
                                                @<td>@Model.TableData(x)(y)</td>
                                            Next
                                        </tr>
                                    Next
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-6">
                            @If permissions.Contains("Order Status") Then
                                @<h3 style="margin-top:0px;" class="text-center"><a href="/Transactions" type="button" class="btn btn-lg btn-block btn-blue">Order Status</a></h3>
                            Else
                                @<h3 style="margin-top:0px;" class="text-center"><a href="/Transactions" type="button" class="btn btn-lg btn-block btn-blue disabled">Order Status</a></h3>
                            End If

                            <h3>Open Picks: <label id="openPicks" class="pull-right">@Model.CountsData(0)</label></h3>
                            <h3>
                                Completed Picks: <label id="compPicks" class="pull-right">@Model.CountsData(1)</label>
                            </h3>
                            <h3>
                                Open Puts: <label id="openPuts" class="pull-right">@Model.CountsData(2)</label>
                            </h3>
                            <h3> Completed Puts: <label id="compPuts" class="pull-right">@Model.CountsData(3)</label></h3>
                        </div>
                        <div class="col-md-6">
                            <a style="margin-top:0px;" class="text-center btn btn-lg btn-block btn-blue" href="Admin/InventoryDetail" type="button" id="invDetail">Inventory Detail</a>
                            <h3>
                                Open Counts: <label id="openCounts" class="pull-right">@Model.CountsData(4)</label>
                            </h3>
                            <h3>
                                Completed Counts: <label id="compCounts" class="pull-right">@Model.CountsData(5)</label>
                            </h3>
                            <h3>
                                Adjustments: <label id="adjust" class="pull-right">@Model.CountsData(6)</label>
                            </h3>
                            <h3>Reprocess: <label id="reproc" class="pull-right">@Model.CountsData(7)</label></h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



