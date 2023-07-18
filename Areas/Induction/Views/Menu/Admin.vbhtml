<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Admin"
    ViewData("PageName") = "| Admin"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-4 col-md-offset-2">
            <div class="col-md-12">
                @If permissions.Contains("Tote Inventory Map") And permissions.Contains("Tote Admin Menu") Then
                    @<a id="IMInventoryMap" href="/InventoryMap?App=IM" class="btn btn-xl btn-primary btn-block bottom-spacer">Inventory Map</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="IMInventoryMap" disabled>Inventory Map</button>
                End If
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Batch Manager") And permissions.Contains("Tote Admin Menu") Then
                    @<a href="/IM/ToteTransactionManager" class="btn btn-primary btn-xl btn-block bottom-spacer">Tote Transaction Manager</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="ToteTransMan" disabled>Tote Transaction Manager</button>
                End If
            </div>
            <div class="col-md-12">
                <button class="btn btn-primary btn-xl btn-block bottom-spacer" onclick="launchToteModal('MenuButt', '', '', '')" id="ToteManButt">Tote Manager</button>
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Reports") And permissions.Contains("Tote Admin Menu") Then
                    @<a href="/CustomReports?App=IM" class="btn btn-xl btn-primary btn-block bottom-spacer">Reports</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="IMReports" disabled>Reports</button>
                End If
            </div>
        </div>
        <div class="col-md-4">
            <div class="col-md-12">
                @If permissions.Contains("Tote Inventory") And permissions.Contains("Tote Admin Menu") Then
                    @<a id="IMInventoryMaster" href="/InventoryMaster?App=IM" class="btn btn-xl btn-primary btn-block bottom-spacer">Inventory</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="IMInventory" disabled>Inventory</button>
                End If
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Manual Transactions") And permissions.Contains("Tote Admin Menu") Then
                    @<a id="IMManTrans" href="/ManualTransactions?App=IM" class="btn btn-xl btn-primary btn-block bottom-spacer">Manual Transactions</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="IMManTrans" disabled>Manual Transactions</button>
                End If
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Transaction Journal") And permissions.Contains("Tote Admin Menu") Then
                    @<a id="IMTransactions" href="/Transactions?viewToShow=2&App=IM" class="btn btn-xl btn-primary btn-block bottom-spacer">Transaction Journal</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="IMTransactions" disabled>Transaction Journal</button>
                End If
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Preferences") And permissions.Contains("Tote Admin Menu") Then
                    @<a href="/IM/Preferences" class="btn btn-primary btn-xl btn-block bottom-spacer">Preferences</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" disabled>Preferences</button>
                End If
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/Induction/Views/ProcessPutAways/Setup/TotesModalPartial.vbhtml")
