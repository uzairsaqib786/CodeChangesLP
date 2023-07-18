<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("App") = "Admin"
    ViewData("AppHome") = "Admin"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

@RenderBody()

@Section DropDownMenu
    @If permissions.Contains("Admin Menu") Then
        @<ul class="nav navbar-nav navbar-right">
            <li class="dropdown">
                <a style="max-height:45px;" href="#" class="dropdown-toggle" data-toggle="dropdown">Admin<span class="caret"></span></a>
                <ul class="dropdown-menu dropdown-column" role="menu">
                    @If permissions.Contains("Inventory Map") Then
                        @<li><a href="/Inventorymap?App=Admin" style="font-size:18px;">Inventory Map</a></li>
                    End If
                    @If permissions.Contains("Batch Manager") Then
                        @<li><a href="/Admin/BatchManager" style="font-size:18px;">Batch Manager</a></li>
                    End If
                    @If permissions.Contains("Move Items") Then
                        @<li><a href="/Admin/MoveItems" style="font-size:18px;">Move Items</a></li>
                    End If
                    @If permissions.Contains("Cycle Count Manager") Then
                        @<li><a href="/Admin/CycleCount" style="font-size:18px;">Cycle Count</a></li>
                    End If
                    @If permissions.Contains("Manual Transactions") Then
                        @<li><a href="/ManualTransactions?App=Admin" style="font-size:18px;">Manual Transactions</a></li>
                    End If
                    @If permissions.Contains("Event Log Manager") Then
                        @<li><a href="/EventLog?App=Admin" style="font-size:18px;">Event Log Manager</a></li>
                    End If
                    @If permissions.Contains("Replenishment") Then
                        @<li><a href="/Admin/SystemReplenishment" style="font-size:18px;">System Replenishment</a></li>
                    End If
                    @If permissions.Contains("Inventory") Then
                        @<li><a href="/InventoryMaster?App=Admin" style="font-size:18px;">Inventory</a></li>
                    End If
                    @If permissions.Contains("Location Assignment") Then
                        @<li><a href="/Admin/LocationAssignment" style="font-size:18px;">Location Assignment</a></li>
                    End If
                    @If permissions.Contains("Reports") Then
                        @<li><a href="/CustomReports?App=Admin" style="font-size:18px;">Reports</a></li>
                    End If
                    @If permissions.Contains("Employees") Then
                        @<li><a href="/Admin/Employees" style="font-size:18px;">Employees</a></li>
                    End If
                    @If permissions.Contains("De-Allocate Orders") Then
                        @<li><a href="/Admin/DeAllocateOrders" style="font-size:18px;">De-Allocate Orders</a></li>
                    End If
                    @If permissions.Contains("Preferences") Then
                        @<li><a href="/Admin/Preferences" style="font-size:18px;">Preferences</a></li>
                    End If
                    @If permissions.Contains("Transaction Journal") Then
                        @<li><a href="/Transactions/?viewToShow=2&App=Admin" style="font-size:18px;">Transactions</a></li>
                    End If
                </ul>
            </li>
        </ul>
    End If
End Section

@Section HelpLink
    <li><a href="/Help?initialPage=help" style="font-size:18px;">Help Documents</a></li>
End Section

@Section AppName
    <input type="hidden" id="AppName" value="Admin" data-version="3/4/16" />
End Section

@Section VersionDetails
    <li>
        <a id="CurrentVersion" style="font-size:18px">
            Admin: <strong style="font-size: 18px; color: dodgerblue;">
                @PickPro_Web.AppBuildInfo.DisplayVersion("ICSAdmin")
            </strong>
        </a>
    </li>
End Section
