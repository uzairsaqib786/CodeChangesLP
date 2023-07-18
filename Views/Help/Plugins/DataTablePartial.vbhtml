<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@code
Layout = nothing
End Code
<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">
            Tables
        </h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                Tables are the backbone of many functions of PickPro.  Most tables can be sorted by any column and/or filtered for specific results.  Some tables can be searched for certain values
                in certain columns.  Tables are paged to limit data to a manageable length.
            </div>
        </div>
        <hr />
        <div class="row" style="padding-top:15px;">
            <div class="col-md-6">
                <strong>Sorting a table:</strong>
                <ol>
                    <li>Click the column you wish to sort on.</li>
                    <li>A blue arrow will appear above the column indicating if it has been sorted ascending or descending.</li>
                    <li>Optional:  Click the column again to switch the type of sorting (ascending or descending).</li>
                </ol>
            </div>
            <div class="col-md-6">
                <strong>Searching a table:</strong>
                <ol>
                    <li>Choose the column you wish to search from the dropdown.</li>
                    <li>Type your query in the search box.</li>
                </ol>
                Notes: 
                <ul>
                    <li>Table searching on this page is disabled.  Normally searching will redraw the table with records filtered out.</li>
                    <li>Most tables contain a right-click context menu that will allow you to set custom filters on fields.  You would be able to search for something like: Transaction Quantity > 0 on the <strong>Transactions</strong> pages.
                      See the <a onclick="$('#stpaging').click();">Multi-Line Paging</a> tab for an example of this functionality in action.</li>
                </ul>
            </div>
        </div>
        <hr />
        <div class="row" style="padding-top:15px;">
            <div class="col-md-6">
                <strong>Paging a table:</strong>
                <ul>
                    <li>
                        Option #1: Click the desired page from the pages listed between Previous and Next
                    </li>
                    <li>
                        Option #2: Click the Next or Previous button.  If the button is grayed out you have reached the beginning or end of records and cannot go any farther.
                    </li>
                    <li>
                        Option #3: Press the N or P key to go to the Next or Previous page respectively.  If the Next/Previous button(s) are grayed out you have reached the beginning or end of records and cannot go any farther.
                    </li>
                    <li>
                        Option #4:  Change the number of records filter on the left hand side above the table.  The table will redraw with the selected number of entries displayed.  This feature is disabled on this page and some others.
                    </li>
                </ul>
            </div>
            <div class="col-md-6">
                <strong>Selecting a row:</strong>
                <ul>
                    <li>Click a row to select it.</li>
                    <li>With a row selected other options (like printing, or deleting) may become available for that entry.  Look for button controls above the table.</li>
                    <li>With the selected row choose an action from the buttons available.</li>
                    <li>Note: Some tables (like the Event Log) will open a detailed view of the selected row if it is clicked again.  Others will unselect the row.</li>
                </ul>
            </div>
        </div>
        <hr />
        <div class="row" style="padding-top:15px;">
            <div class="col-md-2">
                <select class="form-control">
                    <option>10</option>
                    <option>20</option>
                    <option>30</option>
                </select>
            </div>
            <div class="col-md-4">
                <button disabled="disabled" id="FakePrint" type="button" class="btn btn-primary" data-toggle="tooltip" data-original-title="Print Entry (non-functional)"><span class="glyphicon glyphicon-print"></span></button>
                <button disabled="disabled" id="FakeDelete" type="button" class="btn btn-danger" data-toggle="tooltip" data-original-title="Delete Entry (non-functional)"><span class="glyphicon glyphicon-trash"></span></button>
            </div>
            <div class="col-md-3 col-md-offset-3">
                <div class="row">
                    <div class="col-md-6">
                        <select id="FakeSearch" class="form-control">
                            <option>Item Number</option>
                            <option>Description</option>
                            <option>Supplier ID</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <input type="text" class="form-control" id="FakeSearchValue" placeholder="Search Value" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <table class="table table-condensed table-bordered table-striped" style="background-color:white;" id="DataTableExample">
                    <thead>
                        <tr>
                            <td>Item Number</td>
                            <td>Description</td>
                            <td>Supplier ID</td>
                        </tr>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>
        </div>
    </div>
</div>
<script src="~/Scripts/jquery.dataTables.js"></script>
<script src="~/Scripts/dataTables.bootstrap.js"></script>
<script src="~/Scripts/Help/DataTablesExample.js"></script>