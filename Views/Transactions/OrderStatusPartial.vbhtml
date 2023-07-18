<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->
@code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code
@modeltype PickPro_Web.TransactionsModel
    <div id="OrderView">
        <div class="row">
            <div class="col-md-4">
                <div class="panel panel-info">
                    <div class="panel-heading" style="">
                        <!--Order Data display window-->
                        <h1 class="panel-title">Select Order</h1>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div id="orderTypeAhead" class="form-group">
                                    <!--Textbox for the order number input-->
                                    <div class="form-group has-feedback has-warning forced-typeahead" style="margin-bottom:0px;">
                                        <input value="@Model.OrderStatusOrder" oninput="$('#ordernumCopy').val(this.value)" type="text" class="form-control input-sm typeahead" id="ordernumFilterOrder" placeholder="Order Number" maxlength="50" data-toggle="tooltip" 
                                               data-placement="top" data-original-title="Select an entry from the dropdown in order to see that order in the table." />
                                        <span class="glyphicon glyphicon-warning-sign form-control-feedback" style="top:0px;"></span>
                                        <input id="ordernumCopy"  style="display:none;"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <!--Textbox for the tote id input input-->
                                    <input oninput="$('#toteidCopy').val(this.value)" type="text" class="form-control input-sm" id="toteidFilterOrder" placeholder="Tote ID" maxlength="50">
                                    <input id="toteidCopy" style="display:none;" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <!--Clear button-->
                                <button id="toteClearButton" class="btn btn-primary btn-block"><u>C</u>lear</button>
                            </div>
                            <div class="col-md-4">
                                <!--Delete order button-->
                                 @If permissions.Contains("Order Status Delete") Then
                                     @<button id = "orderDeleteButton" Class="btn btn-danger btn-block" style="margin-bottom:5px;">Delete Order</button>
                                 End If
                            </div>
                            <div Class="col-md-4">
                                <div Class="checkbox " style="margin-top:10px;">
                                    <Label>
                                        <!--Filter by tote id checkbox-->
                                        <input type = "checkbox" id="toteCheck" value="" />Filter Info by Tote
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div Class="row ">
                            <div Class="col-md-4">
                                <!--First column in display-->
                                <dl>
                                                 <div Class="has-success">
                                        <dt Class="text-success text-center">
                                            Completed Lines
                                        </dt>
                                        <dd Class="text-success text-center">
                                            <!--Text for completed lines-->
                                            <p id = "complLines" > 0</p>
                                        </dd>
                                    </div>
                                    <dt Class="text-center">
                                        Order Type
                                    </dt>
                                    <dd Class="text-center">
                                        <!--Text for order type-->
                                        <p id = "ordType" > None</p>
                                    </dd>
                                </dl>
                            </div>
                            <div Class="col-md-4">
                                <!--Second column in display-->
                                <dl>
                                                                         <div Class="has-error">
                                        <dt Class="text-danger text-center">
                                            Re-Process Lines
                                        </dt>
                                        <dd Class="text-center text-danger">
                                            <!--Text for reprocess lines-->
                                            <p id = "reprocLines" > 0</p>
                                        </dd>
                                    </div>
                                    <dt Class="text-center">
                                        Total Lines
                                    </dt>
                                    <dd Class="text-center">
                                        <!--Text for total Lines-->
                                        <p id = "totLines" > 0</p>
                                    </dd>
                                </dl>
                            </div>
                            <div Class="col-md-4">
                                <!--Third column in display-->
                                <dl>
                                                                                                 <div Class="has-warning">
                                        <dt Class="text-warning text-center">
                                            Open Lines
                                        </dt>
                                        <dd Class="text-center text-warning">
                                            <!--Text for open lines-->
                                            <p id = "openLines" > 0</p>
                                        </dd>
                                    </div>

                                    <dt Class="text-center">
                                        Current Status
                                    </dt>
                                    <dd Class="text-center">
                                        <!--Text for current status-->
                                        <p id = "curStat" > None</p>
                                    </dd>
                                </dl>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div Class="col-md-4">
                <div Class="panel panel-info">
                    <div Class="panel-heading">
                        <!--On carousel location display window-->
                        <h4 Class="panel-title">Carousel Location Zones</h4>
                    </div>
                    <div Class="panel-body">
                        <div style = "overflow-y:scroll; height:190px" >
                            <table class="table table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <!--Columns for table-->
                                        <th>Zone</th>
                                        <th>Location Name</th>
                                        <th>Total Lines</th>
                                        <th>Open</th>
                                        <th>Completed</th>
                                    </tr>
                                </thead>
                                <tbody id="carOnTable"></tbody>
                            </table>
                                                                                                                                                                                                                                                                                                                                                                                                                          </div>
                    </div>
                </div>
            </div>
            <div Class="col-md-4">
                <div Class="panel panel-info">
                    <div Class="panel-heading">
                        <!--Off carousel location display window-->
                        <h4 Class="panel-title"> Off-Carousel Location Zones</h4>
                    </div>
                    <div Class="panel-body">
                        <div style = "overflow-y:scroll; height:190px" >
                            <table class="table table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <!--Columns for table-->
                                        <th>Zone</th>
                                        <th>Location Name</th>
                                        <th>Total Lines</th>
                                        <th>Open</th>
                                        <th>Completed</th>
                                    </tr>
                                </thead>
                                <tbody id="carOffTable"></tbody>
                            </table>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </div>
                    </div>
                </div>
            </div>
        </div>
         <div Class="row">
             <div Class="col-xs-6">
                 <Label># Entries</label>
                 <select id = "pageLength1" Class="form-control" style="width:auto; display:inline;">
                     <option>10</Option>
                     <option>15</Option>
                     <option>20</Option>
                     <option>25</Option>
                     <option>50</Option>
                     <option>100</Option>
                 </select>
                 <Button id = "printList" type="button" data-toggle="tooltip" data-placement="top" title="Print Report" Class="btn btn-primary Print-Report">
                     <span Class="glyphicon glyphicon-print"></span>
                 </button>
                 <Button id = "previewList" Class="btn btn-primary Print-Report" data-toggle="tooltip" data-placement="top" data-original-title="Preview (Top ~50 pages only)">
                     <span Class="glyphicon glyphicon-list-alt"></span>
                 </button>
                 <Button id = "shippingComplete" disabled="disabled" Class="btn btn-primary" onclick="window.location.href = '/ViewShippingInfo?orderNumber=' + $('#ordernumFilterOrder').val() + '&app=@(ViewData("App"))' ">Shipping Complete</button>
                 @If Model.app = "OM" Then
                     @<button id="ChangePriority" type="button" class="btn btn-primary">Change Priority</button>
                 End If
             </div>
             <div Class="col-xs-6" id="searchStringTypeAhead">
                 <Label Class="pull-right">
                     Search
                     <select Class="form-control" style="margin-bottom:5px;width:auto;display:inline;" id="selection1">
                         <!--Dropdown options for search-->
                         <option value = "Transaction Type" > Transaction Type</Option>
                         <option value = "Completed Date" > Completed Date</Option>
                         <option value = "Location" > Location</Option>
                         <option value = "Transaction Quantity" > Transaction Quantity</Option>
                         <option value = "Item Number" > Item Number</Option>
                         <option value = "Line Number" > Line Number</Option>
                         <option value = "Required Date" > Required Date</Option>
                         <option value = "Description" > Description</Option>
                         <option value = "Completed Quantity" > Completed Quantity</Option>
                         <option value = "Tote ID" > Tote ID</Option>
                         <option value = "Priority" > Priority</Option>
                         <option value = "Completed By" > Completed By</Option>
                         <option value = "Unit of Measure" > Unit Of Measure</Option>
                         <option value = "Lot Number" > Lot Number</Option>
                         <option value = "Expiration Date" > Expiration Date</Option>
                         <option value = "Serial Number" > Serial Number</Option>
                         <option value = "Revision" > Revision</Option>
                         <option value = "Warehouse" > Warehouse</Option>
                         <option value = "Import Date" > Import Date</Option>
                         <option value = "Batch Id" > Batch Id</Option>
                         <option value = "User Field1" > User Field1</Option>
                         <option value = "User Field2" > User Field2</Option>
                         <option value = "User Field3" > User Field3</Option>
                         <option value = "User Field4" > User Field4</Option>
                         <option value = "User Field5" > User Field5</Option>
                         <option value = "User Field6" > User Field6</Option>
                         <option value = "User Field7" > User Field7</Option>
                         <option value = "User Field" > User Field8</Option>
                         <option value = "User Field9<" > User Field9</Option>
                         <option value = "User Field10" > User Field10</Option>
                         <option value = "Tote Number" > Tote Number</Option>
                         <option value = "Cell" > Cell</Option>
                         <option value = "Host Transaction ID" > Host Transaction ID</Option>
                         <option value = "Zone" > Zone</Option>
                         <option value = "Emergency" > Emergency</Option>
                         <option value = "ID" > id</Option>
                     </select>
                     By
                     <input id = "searchString1" Class="form-control typeahead" type="text" style="width:auto; display:inline;" placeholder="Search" maxlength="255" />
                 </label>
             </div>
         </div>
        <div Class="row">
            <div Class="col-md-12">
                <!--Main table display-->
                <Table id = "data" Class="table table-bordered table-condensed" style="background-color:white;" cellspacing="0" role="grid">
                    <thead>
                                                                                                                                                                    <tr>
                            <!--Columns For main table-->
                            <th> Type</th>
                            <th> Completed Date</th>
                            <th> Location</th>
                            <th> Trans Qty</th>
                            <th> Item #</th>
                            <th> Line #</th>
                            <th> Req Date</th>
                            <th> Description</th>
                            <th> Completed Qty</th>
                            <th> Tote ID</th>
                            <th> Priority</th>
                            <th> Completed By</th>
                            <th> UOM</th>
                            <th> Lot #</th>
                            <th> Expiration Date</th>
                            <th> Serial #</th>
                            <th> Revision</th>
                            <th> Warehouse</th>
                            <th> Import Date</th>
                            <th> Batch Id</th>
                            <th> User Field1</th>
                            <th> User Field2</th>
                            <th> User Field3</th>
                            <th> User Field4</th>
                            <th> User Field5</th>
                            <th> User Field6</th>
                            <th> User Field7</th>
                            <th> User Field8</th>
                            <th> User Field9</th>
                            <th> User Field10</th>
                            <th> Tote #</th>
                            <th> Cell</th>
                            <th> Host Trans ID</th>
                            <th> Zone</th>
                            <th> Emergency</th>
                            <th> ID</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>
@Html.Partial("MultiDateModalPartial")
@Html.Partial("/Views/Transactions/UpdatePriorityModal.vbhtml")

 

