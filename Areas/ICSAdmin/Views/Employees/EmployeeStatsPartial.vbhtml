<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
    Dim dateReturn As DateTime
    Dim edateReturn As DateTime
    Dim sreturnString As String = ""
    Dim ereturnString As String = ""
End Code
<div id="EmployeeStats">
    <div class="row">
        <div class="col-md-3">
            <div class="panel panel-info">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">Filters</h4> 
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Start Date</label>
                                <input type="text" class="form-control date-picker" id="sDateFilterEmploy" maxlength="50" data-init-date="@Now().ToString("MM/dd/yyyy")" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>End Date</label>
                                <input type="text" class="form-control date-picker" id="eDateFilterEmploy" maxlength="50" data-init-date="@Now().ToString("MM/dd/yyyy")" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <button class="btn btn-primary btn-block" id="setDate">Set Date to today</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <legend class="text-center">Employees</legend>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <button class="btn btn-primary btn-block" id="selEmployees">Select All</button>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <button class="btn btn-primary btn-block" id="clearSelEmployees">Clear Selected</button>
                            </div>
                        </div>
                        
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div style="max-height:500px;overflow:scroll;">
                                <ol style="width:100%;padding-left:0px;list-style-type:none;" id="selectable">
                                    <li class="ui-widget-content" data-val="1234">1234</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 2</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 3</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 4</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 5</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 6</li>
                                    <li class="ui-widget-content" data-val="1234">Employee 7</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-9">
            <div class="panel panel-info">
                <div class="panel-heading clearfix">
                    <h4 class="panel-title pull-left">Statistics</h4> 
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label># Entries</label>
                            <select id="pageLength3" class="form-control" style="width:auto; display:inline;">
                                <option>10</option>
                                <option>15</option>
                                <option>20</option>
                                <option>25</option>
                                <option>50</option>
                                <option>100</option>
                            </select>
                        </div>
                        <div class="col-md-6">
                            <div class="btn-group pull-right">
                                <button id="goTo" class="btn btn-primary dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-print"></span> <span class="caret"></span></button>
                                <ul class="dropdown-menu" role="menu">
                                    <li><a id="printStatsUser" class="Print-Report">Print by Users</a></li>
                                    <li><a id="printStatsDate" class="Print-Report">Print by Date</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table id="empStatsTable" class="table table-bordered table-striped table-condensed" cellspacing="0" role="grid" style="background-color:white;">
                                <thead>
                                    <tr>
                                        <td>User Name</td>
                                        <td>Work Done By</td>
                                        <td>Start Time</td>
                                        <td>End Time</td>
                                        <td>Minutes Worked</td>
                                        <td>Picks</td>
                                        <td>Puts</td>
                                        <td>Counts</td>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
