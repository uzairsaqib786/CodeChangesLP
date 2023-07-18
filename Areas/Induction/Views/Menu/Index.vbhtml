<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype object
@Code
    ViewData("Title") = "Menu"
    ViewData("PageName") = "| Menu"
    Dim permissions As List(Of String) = TryCast(Session("Permissions"), List(Of String))
End Code

<div class="container-fluid">
    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            <div class="col-md-12">
                @If permissions.Contains("Tote Transactions") Then
                    @<a href="/IM/ProcessPutAways" class="btn btn-primary btn-xl btn-block bottom-spacer">Process Put Aways</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="ProcTrans" disabled>Process Put Aways</button>
                End If
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Transactions") Then
                    If Model.Preferences.UseInZonePickScreen Then
                        @<a href="/IM/PickToteSetup/InZonePickScreen" class="btn btn-primary btn-xl btn-block bottom-spacer">Process Picks</a>
                    Else
                        @<a href="/IM/PickToteSetup" class="btn btn-primary btn-xl btn-block bottom-spacer">Process Picks</a>
                    End If
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="ProcTrans" disabled>Process Picks</button>
                End If
            </div>
            <div class="col-md-12">
                @If Model.Preferences.UseInZonePickScreen Then
                    @<a href="/IM/CompPickBatch" class="btn btn-primary btn-xl btn-block bottom-spacer">Complete Pick Batch</a>
                End If
            </div>
            <div class="col-md-12">
                <a href="/IM/SuperBatch" class="btn btn-primary btn-xl btn-block bottom-spacer">Super Batch</a>
            </div>
            <div class="col-md-12">
                <a href="/IM/PalletReceiving" class="btn btn-primary btn-xl btn-block bottom-spacer">Pallet Receiving</a>
            </div>
            <div class="col-md-12">
                <button class="btn btn-primary btn-xl btn-block bottom-spacer" onclick="$('#MarkEmptyReelModal').modal('show')" id="MarkEmptyReels">Mark Empty Reels</button>
            </div>
            <div class="col-md-12">
                @If permissions.Contains("Tote Admin Menu") Then
                    @<a href="/IM/Menu/Admin" class="btn btn-primary btn-xl btn-block bottom-spacer">Admin</a>
                Else
                    @<button class="btn btn-primary btn-xl btn-block bottom-spacer" id="PIckTote" disabled>Admin</button>
                End If
            </div>
        </div>
    </div>
</div>
@Html.Partial("~/Areas/Induction/Views/Modal/MarkEmptyReelModalPartial.vbhtml")