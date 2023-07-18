<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-12">
                <div class="panel-group" id="OpenTransAccordion">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-target="#OTOverview" data-parent="#OpenTransAccordion"><h3 class="panel-title">Overview <span class="accordion-caret-down"></span></h3></a>
                        </div>
                        <div class="panel-body collapse in accordion-toggle" id="OTOverview">
                            <div class="row">
                                <div class="col-md-12">
                                    This is the Open Transactions page. It contains an easy way to view open or completed transactions by a date range, the status (open or completed), order number,
                                    tote id, and by the transaction type. This page also supports searching on various columns in the table. From this page a user is able to delete transactions,
                                    as well as send completed transactions to transaction history.
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <img src="/images/Help/Transactions/OpenTransactions/InitialScreen.PNG" style="width: 60%" usemap="#opentrans" alt="Open Transaction page">
                                </div>
                            </div>
                            <div class="panel-group" id="OTOverviewDetail">
                                <div class="panel panel-info">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#OTOverviewDetail" data-target="#panelOT_1">
                                            <h3 id="displayTransPanel" class="panel-title">
                                                1 | Display Transactions Panel
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" style="background-color:white;" id="panelOT_1">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Open Transactions Display Transactions Panel allows users to filter their results based on a date range, the status of the transaction,
                                                order number, tote id, and the transaction type. For more information on the date see the <strong>Date Filter</strong> section.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li>The <strong>Reset To Today's Date</strong> button resets the date fields to today's date.</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Filters:</strong>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li>The <strong>Import Date</strong> filter is the starting date for displayed records. No records who have an earlier import date will be shown.</li>
                                                    <li>The <strong>End Date</strong> filter is the ending date for displayed records. No records with a later import date will be shown.</li>
                                                    <li>
                                                        The <strong>Status</strong> filter is the status (open or completed) for all completed records. If a specific status is selected then no records with a
                                                        different status are shown.
                                                    </li>
                                                    <li>
                                                        The <strong>Order Number</strong> filter is the order number for all displayed records. No records with a different order number will be displayed. For
                                                        more information see the <strong>Typeaheads</strong> section.
                                                    </li>
                                                    <li>The <strong>Tote ID</strong> filter is the tote id for all displayed records. No records with a different tote id are displayed.</li>
                                                    <li>
                                                        The <strong>By Trans Type</strong> filter is the transaction type for all displayed records. If a specific type is selected then no records with a
                                                        different type will be shown.
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info" style="background-color:white;">
                                    <div class="panel-heading">
                                        <a data-toggle="collapse" data-parent="#OTOverviewDetail" data-target="#panelOT_2">
                                            <h3 class="panel-title" id="DeleteTransPanel">
                                                2 | Delete Transactions Panel
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="panelOT_2">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Delete Transactions Panel displays information about deleting single and multiple transactions.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <ul>
                                                    <li>The <strong>Delete</strong> button allows the user to delete either the selected transaction or all transactions with the same type.</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Fields:</strong>
                                                <ul>
                                                    <li><strong>Selected Order Number</strong>: the order number of the currently selected row.</li>
                                                    <li><strong>Selected Transaction Type</strong>: the transaction type of the currently selected row.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel panel-info" style="background-color:white;">
                                    <div class="panel-heading">
                                        <a data-target="#panelOT_3" data-toggle="collapse" data-parent="#OTOverviewDetail">
                                            <h3 class="panel-title" id="OTTablePanel">
                                                3 | Open Transactions Table
                                                <span class="accordion-caret-down"></span>
                                            </h3>
                                        </a>
                                    </div>
                                    <div class="panel-body collapse accordion-toggle" id="panelOT_3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                The Open Transactions table displays all open or completed transactions that satisfy the select filters. See <strong>Tables</strong> for more information.
                                            </div>
                                        </div>
                                        <div class="row" style="padding-top:5px;">
                                            <div class="col-md-12">
                                                <strong>Buttons:</strong>
                                                <ul>
                                                    <li>
                                                        <button type="button" data-toggle="tooltip" data-placement="top" class="btn btn-primary" data-original-title="Set Column Sequence">
                                                            <span class="glyphicon glyphicon-list"></span>
                                                        </button>
                                                    </li>
                                                    <li>
                                                        The <strong>Set Column Sequence</strong> (shown above) button allows a user to set their own column sequence for the table. For more information
                                                        see the <strong>Set Column Sequence</strong> section.
                                                    </li>
                                                    <li>The <strong>Send Completed To History</strong> button will send all completed transactions to transaction history when pressed.</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white">
                        <div class="panel-heading">
                            <a data-target="#OTDisplayTrans" data-toggle="collapse" data-parent="#OpenTransAccordion">
                                <h3 class="panel-title">
                                    Display Transactions By Filters
                                    <span class="accordion-caret-down"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OTDisplayTrans">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                       <div class="col-md-12">
                                            <strong>Important:</strong> If the date filters are not set correctly then the desired data may not be displayed, as it maybe outside the date range.
                                       </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <img src="/images/Help/Transactions/OpenTransactions/displayInfo.png" style="width: 60%" usemap="displaytrans" alt="Display Desired Transactions" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="panel-group" id="OTDisplayInfo">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#OTDisplayInfo" data-target="#panelOTDisp_1">
                                                    <h3 id="displayInfoDate" class="panel-title">
                                                        1 | Using The Date Filters
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" style="background:white;" id="panelOTDisp_1">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        When trying to display transactions be aware of the date range. If the desired records are outside the date range then they will
                                                        not be shown in the table. For more information on the date filters see the <strong>Date Filters</strong> section for more information.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info" style="background-color:white;">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#OTDisplayInfo" data-target="#panelOTDisp_2">
                                                    <h3 class="panel-title" id="displayInfoStatus">
                                                        2 | Using the Status Dropdown
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelOTDisp_2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <img src="/images/Help/Transactions/OpenTransactions/ByStatusDropDown.png" alt="Status Dropdown" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The status dropdown (shown above) will filter the transactions by the selected status. If open is selected then only open transactions will
                                                        be shown. However, if completed is selected then only completed transactions will be shown. When all transactions is selected, all transactions
                                                        regardless if  they're open or completed will be shown.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info" style="background-color:white;">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#OTDisplayInfo" data-target="#panelOTDisp_3">
                                                    <h3 class="panel-title" id="displayInfoOrderNumber">
                                                        3 | Order Number Filter and Typeahead
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelOTDisp_3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <img src="/images/Help/Transactions/OpenTransactions/OrderNumberTypeAhead.png" alt="Order Number Typeahead" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        When typing into the order number field, a box drops down (as shown above) that displays order numbers beginning with
                                                        the input value. From this drop down the order is able to be selected, or more characters of the desired order number are able to be entered.
                                                        For more information on typeaheads see the <strong>Typeaheads</strong> section.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info" style="background-color:white;">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#OTDisplayInfo" data-target="#panelOTDisp_4">
                                                    <h3 class="panel-title" id="displayInfoToteID">
                                                        4 | Tote ID Filter
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelOTDisp_4">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The tote id filter will limit the displayed records to those only containing the tote id entered. If no order number is specified
                                                        then multiple order numbers with the same tote id will be shown.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-info" style="background-color:white;">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#OTDisplayInfo" data-target="#panelOTDisp_5">
                                                    <h3 class="panel-title" id="displayInfoTransType">
                                                        5 | Using By Transaction Type Dropdown
                                                        <span class="accordion-caret-down"></span>
                                                    </h3>
                                                </a>
                                            </div>
                                            <div class="panel-body collapse accordion-toggle" id="panelOTDisp_5">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <img src="/images/Help/Transactions/OpenTransactions/BytransTypeDropDown.png" alt="Transaction Type Dropdown" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        The transaction type dropdown (shown above) will filter the transactions by the selected transaction type. If the selected type is all transactions, then
                                                        all transactions will be displayed regardless of type.
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OTDeleteTrans" data-toggle="collapse" data-parent="#OpenTransAccordion">
                                <h3 class="panel-title" id="deleteInfo">
                                    Delete Transaction(s)
                                    <span class="accordion-caret-down"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OTDeleteTrans">
                            <div class="row">
                                <div class="col-md-12">
                                    <img src="/images/Help/Transactions/OpenTransactions/DeleteDropDown.png" alt="Delete Panel" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <strong>Steps:</strong>
                                    <ol>
                                        <li>Click on record in table. The <strong>Selected Order Number</strong> (1 on above image) and <strong>Selected Transaction Type</strong> 
                                        (2 on above image) will be filled with the order number and transaction type of the selected row.</li>
                                        <li>Click on the delete button and select the desired delete function from the dropdown (3 on above image). The <strong>Selected Only</strong>
                                        option will only delete the selected row. The <strong>All By Transaction Type</strong> option will delete all of the transactions that
                                        have the same order number and transaction type.</li>
                                        <li>Once this is done a pop up (see below) will appear confirming the delete action
                                            <ul>
                                                <li>The selected only pop up
                                                    <img src="/images/Help/Transactions/OpenTransactions/DeletePopUp.png" alt="Delete Pop Up" /></li>
                                                <li> The transaction type pop up
                                                    <img src="/images/Help/Transactions/OpenTransactions/DeletePopUpByType.png" alt="Delete Pop Up" /></li>
                                            </ul>
                                        </li>
                                        <li>Once the pop up appears to continue with the delete press okay on the pop up. If cancel is pressed, then the delete is canceled.</li>
                                    </ol>
                                    <strong>Results:</strong>
                                    <ul>
                                        <li>If <strong>Selected Only</strong> row is deleted from the database. If <strong>All By Transaction Type</strong>
                                         all transactions type of the <strong>Selected Transaction Type</strong> with the same order number are deleted from the database</li>
                                        <li>Message informing that a delete occurred is added to the event log.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-info" style="background-color:white;">
                        <div class="panel-heading">
                            <a data-target="#OTSendCompHist" data-toggle="collapse" data-parent="#OpenTransAccordion">
                                <h3 class="panel-title" id="compToHist">
                                    Send Completed Transactions To History
                                    <span class="accordion-caret-down"></span>
                                </h3>
                            </a>
                        </div>
                        <div class="panel-body collapse accordion-toggle" id="OTSendCompHist">
                            <div class="row">
                                <div class="col-md-12">
                                    <button class="btn btn-primary">Send Completed to History</button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    When the <strong>Send Completed To History</strong> button is clicked, all completed transactions will be sent to transaction history, 
                                    and will no longer be viewable on the Open Transactions page.
                                </div>
                            </div>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    </div>
</div>
<map name="opentrans">
    <area shape="rect" coords="6, 35, 590, 221" data-target="#panelOT_1" data-toggle="collapse" data-parent="#OTOverviewDetail" />
    <area shape="rect" coords="608, 62, 889, 176" data-target="#panelOT_2" data-toggle="collapse" data-parent="#OTOverviewDetail" />
    <area shape="rect" coords="5, 232, 889, 534" data-target="#panelOT_3" data-toggle="collapse" data-parent="#OTOverviewDetail" />
</map>
<map name="displaytrans">
    <area shape="rect" coords="6, 41, 652, 92" data-target="#panelOTDisp_1" data-toggle="collapse" data-parent="#OTDisplayInfo" />
    <area shape="rect" coords="661, 41, 982, 92" data-target="#panelOTDisp_2" data-toggle="collapse" data-parent="#OTDisplayInfo" />
    <area shape="rect" coords="10, 104, 322, 145" data-target="#panelOTDisp_3" data-toggle="collapse" data-parent="#OTDisplayInfo" />
    <area shape="rect" coords="336, 100, 649, 148" data-target="#panelOTDisp_4" data-toggle="collapse" data-parent="#OTDisplayInfo" />
    <area shape="rect" coords="666, 104, 982, 151" data-target="#panelOTDisp_5" data-toggle="collapse" data-parent="#OTDisplayInfo" />
</map>
