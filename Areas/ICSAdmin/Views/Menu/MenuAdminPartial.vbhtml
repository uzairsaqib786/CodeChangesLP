<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
    Dim menuItems As Object() = {New With {.buttonName = "Inventory Map", .permissionName = "Inventory Map", .link = "/InventoryMap?App=Admin", .css = ""},
                                 New With {.buttonName = "Batch Manager", .permissionName = "Batch Manager", .link = "/Admin/BatchManager", .css = ""},
                                 New With {.buttonName = "Move Items", .permissionName = "Move Items", .link = "/Admin/MoveItems", .css = ""},
                                 New With {.buttonName = "Cycle Count", .permissionName = "Cycle Count Manager", .link = "/Admin/CycleCount", .css = ""},
                                 New With {.buttonName = "Manual Transactions", .permissionName = "Manual Transactions", .link = "/ManualTransactions?App=Admin", .css = ""},
                                 New With {.buttonName = "Event Log", .permissionName = "Event Log Manager", .link = "/EventLog?App=Admin", .css = ""},
                                 New With {.buttonName = "System Replenishment", .permissionName = "Replenishment", .link = "/Admin/SystemReplenishment", .css = ""},
                                 New With {.buttonName = "Inventory", .permissionName = "Inventory", .link = "/InventoryMaster?App=Admin", .css = ""},
                                 New With {.buttonName = "Location Assignment", .permissionName = "Location Assignment", .link = "/Admin/LocationAssignment", .css = ""},
                                 New With {.buttonName = "Reports", .permissionName = "Reports", .link = "/CustomReports?App=Admin", .css = ""},
                                 New With {.buttonName = "Employees", .permissionName = "Employees", .link = "/Admin/Employees", .css = ""},
                                 New With {.buttonName = "De-Allocate Orders", .permissionName = "De-Allocate Orders", .link = "/Admin/DeAllocateOrders", .css = ""},
                                 New With {.buttonName = "Preferences", .permissionName = "Preferences", .link = "/Admin/Preferences", .css = ""},
                                 New With {.buttonName = "Transactions", .permissionName = "Transaction Journal", .link = "/Transactions/?viewToShow=2&App=Admin", .css = ""}}
End Code
<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h2 class="panel-title text-center" style="font-size:20px;">Admin Menu</h2>
                    </div>
                    <div class="panel-body">
                        <div class="col-xs-6">
                            @code
                                Dim displayedCount = 0
                                For x As Integer = 0 To menuItems.Count - 1
                                    displayedCount += 1
                                    If permissions.Contains(menuItems(x).permissionName) Then
                            @<a style="margin-bottom:30px;" href="@menuItems(x).link" type="button" class="btn btn-xl btn-block btn-primary @menuItems(x).css">@menuItems(x).buttonName</a>
                                    Else
                            @<a style="margin-bottom:30px;" href="@menuItems(x).link" type="button" class="btn btn-xl btn-block btn-primary disabled">@menuItems(x).buttonName</a>
                                    End If
                                    If displayedCount = 7 Then
                                        displayedCount = x + 1
                                        Exit For
                                    End If
                                Next
                            End Code
                        </div>
                        @If displayedCount = 7 Then
                            @<div class="col-xs-6">
                                @code
                                For x As Integer = displayedCount To menuItems.Count - 1
                                displayedCount += 1
                                If permissions.Contains(menuItems(x).permissionName) Then
                        @<a style="margin-bottom:30px;" href="@menuItems(x).link" type="button" class="btn btn-xl btn-block  btn-primary  @menuItems(x).css">@menuItems(x).buttonName</a>
                                Else
                        @<a style="margin-bottom:30px;" href="@menuItems(x).link" type="button" class="btn btn-xl btn-block  btn-primary disabled">@menuItems(x).buttonName</a>
                                End If

                                Next
                                End Code
                            </div>
                        End If
                    </div>
                </div>
            </div>
            <div class="col-md-7">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h2 class="panel-title text-center" style="font-size:20px;">Orders</h2>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="overflow-y:scroll;max-height:380px;">
                                    <table id="allocatedTransTable" class="table table-bordered table-striped table-condensed" cellspacing="0" role="grid" style="background-color:white;">
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
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    @If permissions.Contains("Order Status") Then
                                        @<h3 style="margin-top:0px;" class="text-center"><a href="/Transactions" type="button" class="btn btn-lg btn-block btn-blue">Order Status</a></h3>
                                    Else
                                        @<h3 style="margin-top:0px;" class="text-center"><a href="/Transactions" type="button" class="btn btn-lg btn-block btn-blue disabled">Order Status</a></h3>
                                    End If

                                    <h3>Open Picks: <label id="openPicks" class="pull-right">@Model.CountsData(0)</label></h3>
                                    <h3>
                                        Completed Picks Today: <label id="compPicks" class="pull-right">@Model.CountsData(1)</label>
                                    </h3>
                                    <h3> Completed Picks/Hour: <label id="compPicksHour" class="pull-right">@Model.CountsData(8)</label></h3>
                                    <h3>
                                        Open Puts: <label id="openPuts" class="pull-right">@Model.CountsData(2)</label>
                                    </h3>
                                    <h3> Completed Puts Today: <label id="compPuts" class="pull-right">@Model.CountsData(3)</label></h3>
                                </div>
                                <div class="col-md-6">
                                    <a style="margin-top:0px;" class="text-center btn btn-lg btn-block btn-blue" href="/Admin/InventoryDetail" type="button" id="invDetail">Inventory Detail</a>
                                    <h3>
                                        Open Counts: <label id="openCounts" class="pull-right">@Model.CountsData(4)</label>
                                    </h3>
                                    <h3>
                                        Completed Counts Today: <label id="compCounts" class="pull-right">@Model.CountsData(5)</label>
                                    </h3>
                                    <h3>
                                        Adjustments Today: <label id="adjust" class="pull-right">@Model.CountsData(6)</label>
                                    </h3>
                                    <h3>Reprocess: <label id="reproc" class="pull-right">@Model.CountsData(7)</label></h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
