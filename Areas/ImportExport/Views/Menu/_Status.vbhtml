<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="row">
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-body white-bg">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Import Ready Transactions</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("importcount")
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-8">Transactions to be Re-Processed</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("reprocess")
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="statusRefresh">Refresh Status</button>
                <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="transJournal">Transaction Journal</button>
                <button class="btn btn-primary btn-xl btn-block bottom-spacer" id="eventLog">Event Log Manager</button>
            </div>
            <div class="col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Status
                    </div>
                    <div class="panel-body white-bg">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Open Picks</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("openpicks")
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-8">Completed Picks to Export</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("completedpicks")
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Open Put Aways</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("openputaways")
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-8">Closed Put Aways to Export</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("closedputaways")
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Open counts</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("opencounts")
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-md-8">Closed Counts to Export</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("closedcounts")
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Adjustments to Export</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("adjustments")
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <label class="control-label col-md-8">Number of Location Changes to Export</label>
                                <div class="col-md-4 form-control-static">
                                    @StatusVal("locationchanges")
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

@helper StatusVal(ByVal id As String)
    @<div class="statusVal" id="@id"><span></span><i class="fa fa-spinner fa-spin"></i></div>
End Helper