<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Multi-Line Paging
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                The Multi-Line Paging method is used in conjunction with a table-like structure.  The difference lies in the way the results are laid out.  Where a table will have one row per entry the multi-line approach does as its name suggests, provides multiple rows per entry.  
                This is particularly helpful when there are many fields that just do not fit properly in a table.  Functionality between tables and the multi-line methods are nearly identical.  If there are no records retrieved by the plugin you may not have any open transactions as this is where the plugin draws data (top 50 records only) from.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-6">
                        <strong>Paging with the plugin:</strong>
                        <ol>
                            <li>Click the corresponding next/previous button <button type="button" class="btn btn-primary"><span class="glyphicon glyphicon-arrow-right"></span></button> / <button type="button" class="btn btn-primary"><span class="glyphicon glyphicon-arrow-left"></span></button></li>
                            <li>If the button is grayed out you may have reached the beginning or end of records and cannot advance in that direction in the recordset.</li>
                        </ol>
                    </div>
                    <div class="col-md-6">
                        <strong>Filtering with the plugin:</strong>
                        <ul>
                            <li>Right click an input included in the plugin to filter it.  Choose a method for filtering (equal, greater than, like, etc.)  In some cases filtering will not be available.</li>
                            <li>Any area that can be paged will have a clear filter button (<button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" data-original-title="Clear Filters"><span class="glyphicon glyphicon-refresh"></span></button>)</li>
                            <li>If there are records remaining in the paging area when you wish to clear the filters you may also right click any input and select "Clear Filters" from the menu.</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" id="STPagingExample" style="overflow-y:scroll;">

            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <label id="Page" class="pull-right"></label>
            </div>
        </div>
        <div class="row">
            <div class="col-md-1 col-md-offset-8">
                <button type="button" class="btn btn-primary btn-block" id="Clear"><span class="glyphicon glyphicon-refresh"></span></button>
            </div>
            <div class="col-md-1">
                <select class="form-control" id="STPerPage">
                    <option>5</option>
                    <option>10</option>
                </select>
            </div>
            <div class="col-md-1">
                <button type="button" class="btn btn-primary btn-block" id="Prev"><span class="glyphicon glyphicon-arrow-left"></span></button>
            </div>
            <div class="col-md-1">
                <button type="button" class="btn btn-primary btn-block" id="Next"><span class="glyphicon glyphicon-arrow-right"></span></button>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/FilterMenu/MultiLinePaging.js"></script>
<script src="~/Scripts/Help/STPagingExample.js"></script>