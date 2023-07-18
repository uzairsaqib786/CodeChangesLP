<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<div class="col-md-12">
    <div class="row">
        <div class="col-md-12">
            <div id="detail_alerts">

            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                        <label>Item Number:</label>
                        <input type="text" readonly="readonly" id="UpdateItemNum" class="form-control modal-launch-style inv-text" data-colname="[Inventory].[Item Number]"  value="@IIf(Model.ItemNum Is Nothing Or Model.ItemNum = "", Model.FirstItemNum, Model.ItemNum)">
                        <i class="glyphicon glyphicon-resize-full form-control-feedback description-modal-edit modal-launch-style"></i>
                     </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                        <label class="control-label">Description</label>
                        <input type="text" readonly="readonly" data-colname="[Inventory].[Description]" class="form-control description-modal-edit modal-launch-style inv-edit inv-text"  id="itemDescription">
                        <i class="glyphicon glyphicon-resize-full form-control-feedback description-modal-edit modal-launch-style"></i>
                    </div>
                </div>
                <div class="col-md-12">
                    <label>Supplier Item ID:</label><input type="text" maxlength="50" data-colname="[Inventory].[Supplier Item ID]" class="form-control inv-edit inv-text" placeholder="Supplier Item ID" id="supplierID" />
                </div>
                
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Reorder Point:</label><input type="text" maxlength="9" data-colname="[Inventory].[Reorder Point]" class="form-control inv-edit inv-num" placeholder="Reorder Point" id="reorderPoint" />
                </div>
                <div class="col-md-4">
                    <label>Replenishment Point:</label><input type="text" maxlength="9" data-colname="[Inventory].[Replenishment Point]" class="form-control inv-edit inv-num" placeholder="Replenishment Point" id="replenishPoint" />
                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                        <label class="control-label">Category</label>
                        <input type="text" name="Category 1" readonly="readonly" data-colname="[Inventory].[Category]" class="form-control category-modal modal-launch-style inv-edit inv-text"  id="category">
                        <i class="glyphicon glyphicon-resize-full form-control-feedback category-modal modal-launch-style"></i>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <label>Reorder Quantity:</label><input type="text" maxlength="9" data-colname="[Inventory].[Reorder Quantity]" class="form-control inv-edit inv-num" placeholder="Reorder Quantity" id="reorderQty" />
                </div>
                <div class="col-md-4">
                    <label>Replenishment Level:</label><input type="text" maxlength="9" data-colname="[Inventory].[Replenishment Level]" class="form-control inv-edit inv-num" placeholder="Replenishment Level" id="replenishLevel" />
                </div>
                <div class="col-md-4">
                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                        <label class="control-label">Sub Category</label>
                        <input type="text" name="Sub Category 1" readonly="readonly" data-colname="[Inventory].[Sub Category]" class="form-control subcategory-modal modal-launch-style inv-edit inv-text"  id="subCategory">
                        <i class="glyphicon glyphicon-resize-full form-control-feedback subcategory-modal modal-launch-style"></i>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group has-feedback" style="margin-bottom:0px;">
                        <label class="control-label">@Model.Alias.UoM</label>
                        <input type="text" readonly="readonly" class="form-control uom-modal modal-launch-style inv-edit inv-text" data-colname="[Inventory].[Unit of Measure]"  id="UoM">
                        <i class="glyphicon glyphicon-resize-full form-control-feedback uom-modal modal-launch-style"></i>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>KanBan Replenishment Point</label><input type="text" maxlength="9" data-colname="[Inventory].[Kanban Replenishment Point]" class="form-control inv-edit inv-num" placeholder="Kanban Replen Point" id="KanBanReplenPoint" />
                </div>
                <div class="col-md-4">
                    <label>KanBan Replenishment Level</label><input type="text" maxlength="9" data-colname="[Inventory].[Kanban Replenishment Level]" class="form-control inv-edit inv-num" placeholder="Kanban Replen Level" id="KanBanReplenLevel" />
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-6">
                    <dl>
                        <dt class="text-center">
                            Total Qty
                        </dt>
                        <dd class="text-center">
                            <p id="totQty">0</p>
                        </dd>
                    </dl>
                </div>
                <div class="col-md-6">
                    <dl>
                        <dt class="text-center">
                            WIP Qty
                        </dt>
                        <dd class="text-center">
                            <p id="allWip">0</p>
                        </dd>
                    </dl>
                </div>
                <div class="col-md-6">
                    <dl>
                        <dt class="text-center">
                            Allocated Picks
                        </dt>
                        <dd class="text-center">
                            <p id="allPicks">0</p>
                        </dd>
                    </dl>
                </div>
                <div class="col-md-6">
                    <dl>
                        <dt class="text-center">
                            Allocated Put Aways
                        </dt>
                        <dd class="text-center">
                            <p id="allPuts">0</p>
                        </dd>
                    </dl>
                </div>
                
            </div>
            <div class="row">
                <div class="col-md-4">
                    <dl>
                        <dt class="text-center">
                            Open Transactions
                        </dt>
                        <dd class="text-center">
                            <p id="OTcount">0</p>
                        </dd>
                        <dt class="text-center">
                        </dt>
                        <dd class="text-center">
                            <button type="button" class="btn btn-primary btn-block" id="OTbutton"><u>V</u>iew Open</button>
                        </dd>
                    </dl>
                </div>
                <div class="col-md-4">
                    <dl>
                        <dt class="text-center">
                            Transaction History
                        </dt>
                        <dd class="text-center">
                            <p id="THcount">0</p>
                        </dd>
                        <dt class="text-center">
                        </dt>
                        <dd class="text-center">
                            <button type="button" class="btn btn-primary btn-block" id="THbutton"> View <u>H</u>istory</button>
                        </dd>
                    </dl>
                </div>
                <div class="col-md-4">
                    <dl>
                        <dt class="text-center">
                            Reprocess Transactions
                        </dt>
                        <dd class="text-center">
                            <p id="RPcount">0</p>
                        </dd>
                        <dt class="text-center">
                        </dt>
                        <dd class="text-center">
                            <button type="button" class="btn btn-primary btn-block" id="Reprocbutton"> View <u>R</u>eprocess</button>
                        </dd>
                    </dl>
                </div>
            </div>
        </div>

    </div>
</div>