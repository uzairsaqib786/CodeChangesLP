<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@Code
    ViewData("Title") = "Pallet Receiving"
    ViewData("PageName") = "| Pallet Receiving"
End Code

<!DOCTYPE html>

<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        Receive Pallet
                    </h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <label>Tote ID</label>
                            <input type="text" id="PalletToteID" placeholder="Tote ID" class="form-control" />
                        </div>
                        <div class="col-md-4">
                            <label>Item Number</label>
                            <input type="text" id="PalletItemNum" placeholder="Item Number" class="form-control" />
                        </div>
                        <div class="col-md-4">
                            <label>Quantity</label>
                            <input type="number" id="PalletQuant" placeholder="Item Quantity" class="form-control" />
                        </div>
                    </div>
                    <div class="row top-spacer">
                        <div class="col-md-4">
                            <button type="button" class="btn btn-success btn-block" id="ProcessPallet">Process Pallet</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script src="~/Areas/Induction/Scripts/PalletReceiving/PalletReceiving.js"></script>