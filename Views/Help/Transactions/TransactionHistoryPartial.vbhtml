<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="panel-group" id="THAccordion">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-target="#THOverview" data-parent="#THAccordion" data-toggle="collapse"><h3 class="panel-title">Overview <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse in accordion-toggle" id="THOverview">
                    <div class="row">
                        <div class="col-md-12">
                            This is the <strong>Transaction History</strong> page.  It contains an easy way to look up completed orders by order number or date and supports filtering on 
                            various columns by a searched value.  The black outlined boxes can be clicked to navigate to an area with details about the clicked panel.
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <img src="/images/Help/Transactions/TransactionHistory/load.png" style="width: 60%" usemap="#TransactionHistoryMap" />
                        </div>
                    </div>
                    <div class="panel-group" id="THOverviewAccordion">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#THOverviewAccordion" data-target="#panelTH_1"><h3 class="panel-title">1 | Filters Panel <span class="accordion-caret-down"></span></h3></a>
                            </div>
                            <div class="panel-body collapse accordion-toggle" id="panelTH_1">
                                <strong>Filters:</strong>
                                <ul>
                                    <li>
                                        Date filters restrict the data by the date the transactions were marked as completed.
                                        <ul>
                                            <li><strong>Completed Date (Start)</strong> is the beginning date to start filtering on.</li>
                                            <li><strong>End Date</strong> is the ending date to finish filtering on.</li>
                                            <li><strong>Completed Date (Start)</strong> must have occurred before <strong>End Date</strong> for results to be selected.</li>
                                        </ul>
                                        Ex: A <strong>Completed Date (Start)</strong> filter of 8/25/2014 and a <strong>End Date</strong> filter of 8/26/2014 yields all transactions marked as completed
                                        between 8/25/2014 and 8/26/2014.
                                    </li>
                                    <li><strong>Order Number</strong> restricts data to completed entries with a specific order number.</li>
                                </ul>
                                <strong>Buttons:</strong>
                                <ul>
                                    <li>
                                        <strong>Reset to Today's Date</strong> resets the date filters to the current date when clicked.  Alternatively the <strong>T</strong>
                                        key may be used when the cursor is not inside a text box input.
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#THOverviewAccordion" data-target="#panelTH_2"><h3 class="panel-title">2 | Transaction History Table <span class="accordion-caret-down"></span></h3></a>
                            </div>
                            <div class="panel-body collapse accordion-toggle" id="panelTH_2">
                                <div class="row">
                                    <div class="col-md-12">
                                        The <strong>Transaction History</strong> table displays all completed lines in the selected order between the start and end date filters. 
                                        See <strong>Tables</strong> for more detailed information.
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <strong>Buttons:</strong>
                                        <ul>
                                            <li>
                                                Set Default Column Sequence <ul>
                                                    <li>
                                                        <button data-toggle="tooltip" data-placement="top" title="" class="btn btn-primary" data-original-title="Set Column Sequence">
                                                            <span class="glyphicon glyphicon-list"></span>
                                                        </button>
                                                    </li>
                                                <li>
                                                    Allows user to open the <strong>Set Default Column Sequence</strong> page.  See <strong>Set Default Column Sequence</strong> for information.
                                                </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <a data-toggle="collapse" data-target="#THSelectOrder" data-parent="#THAccordion"><h3 class="panel-title">Select data <span class="accordion-caret-down"></span></h3></a>
                </div>
                <div class="panel-body collapse accordion-toggle" id="THSelectOrder">
                    <div class="row">
                        <div class="col-md-12">
                            <img src="/images/Help/Transactions/TransactionHistory/filters.png" style="width: 60%" alt="Transaction History Filters" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong>Steps:</strong>
                            <ol>
                                <li>Choose date filters for boxes #1, #2.  See <strong>DatePickers</strong> for detailed instructions.</li>
                                <li>Start typing an order number in box #3.  A list of suggestions will pop up.  An order can be optionally selected from here.</li>
                                <li>Once an order number is selected or entered that matches the database the table will populate with data.</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<map name="TransactionHistoryMap">
    <area shape="rect" data-toggle="collapse" data-target="#panelTH_1" data-parent="#THOverviewAccordion" coords="0, 47, 1064, 275" />
    <area shape="rect" data-toggle="collapse" data-target="#panelTH_2" data-parent="#THOverviewAccordion" coords="0, 291, 1066, 511" />
</map>