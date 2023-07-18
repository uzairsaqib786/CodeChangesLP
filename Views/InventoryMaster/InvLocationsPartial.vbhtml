<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@modeltype Object
<!-- Added select for searching by column -->
<div class="row">
    <div class="col-md-4">
        <label># Entries</label>
        <select id="pageLength" class="form-control" style="width:auto; display:inline;">
            <option>10</option>
            <option>15</option>
            <option>20</option>
            <option>25</option>
            <option>50</option>
            <option>100</option>
        </select>
    </div>
    <div class="col-md-8">
        <button type="button" class="btn btn-primary pull-right" id="refreshTable">Refresh Table</button>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <table class="table table-condensed table-bordered table-striped" id="invMasterTable" style="background-color:#fff">
            <thead>
                <tr>
                    <th>Location</th>
                    <th>WH</th>
                    <th>Zone</th>
                    <th>Carousel</th>
                    <th>Row</th>
                    <th>Shelf</th>
                    <th>Bin</th>
                    <th>Lot #</th>
                    <th>Expiration</th>
                    <th>Serial #</th>
                    <th>Cell Size</th>
                    <th>@Model.Alias.UserFields(0)</th>
                    <th>@Model.Alias.UserFields(1)</th>
                    <th>Qty All. Pick</th>
                    <th>Qty All. Put</th>
                    <th>UoM</th>
                    <th>Item Qty</th>
                    <th>Stock Date</th>
                    <th>Velocity</th>
                </tr>
            </thead>
            <tbody></tbody>

        </table>
    </div>
</div>
