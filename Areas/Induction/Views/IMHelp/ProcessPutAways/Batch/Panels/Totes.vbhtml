<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        This panel details the uses of the <strong>Totes</strong> panel.  In this area you may mark a tote as full, view a selected tote's contents or print a tote's contents.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        The <strong>Totes</strong> table is populated with the totes that make up the current batch (indicated by the <strong>Batch ID</strong> textbox under the <strong>Choose Batch</strong> panel).  
        See the <a href="../Help?initialPage=datatable">DataTables</a> section of the main help document for details about most tables in PickPro.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        <table class="table table-condensed table-bordered table-striped" style="background-color:white;">
            <thead>
                <tr>
                    <td>Position</td>
                    <td>Tote ID</td>
                    <td>Tote Capacity</td>
                    <td>Current Qty</td>
                    <td>Explanation</td>
                </tr>
            </thead>
            <tbody>
                <tr class="success">
                    <td>1</td>
                    <td>1234</td>
                    <td>10</td>
                    <td>10</td>
                    <td>This is a "full" tote row because the capacity = quantity.</td>
                </tr>
                <tr class="warning">
                    <td>2</td>
                    <td>5678</td>
                    <td>10</td>
                    <td>1</td>
                    <td>This is a partially full tote row because the capacity > quantity.</td>
                </tr>
                <tr class="active">
                    <td>3</td>
                    <td>9101112</td>
                    <td>10</td>
                    <td>5</td>
                    <td>This is a selected tote row.  This color will appear over any selected row regardless of whether the tote is full or not.</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        To mark a tote as full:
        <ol>
            <li>Ensure that the tote is not already marked as full.  This is indicated by the green background color of the row if it is not selected.  If the tote is currently selected it will be blue and <strong>Tote Capacity</strong> and <strong>Current Qty</strong> fields will indicate whether there are open cells in the tote.</li>
            <li>Select the tote in question by clicking it in the totes table.  When correctly selected it will be blue.</li>
            <li>Click the <button class="btn btn-primary">Mark Tote as Full</button> button.</li>
            <li>Confirm the action by clicking OK or deny the action by clicking Cancel.</li>
        </ol>
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        To view the tote's currently assigned contents:
        <ol>
            <li>Select the specified tote by clicking it.  It will turn blue when it is correctly selected.</li>
            <li>
                Click the <button class="btn btn-primary">View Tote Contents</button> button.
                <ul>
                    <li>Note:  If the tote has nothing in it then the button will be disabled and will not do anything.</li>
                </ul>
            </li>
        </ol>
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        To print a tote label:
        <ol>
            <li>Select the desired tote by clicking it.  It will turn blue when it is correctly selected.</li>
            <li>
                Click the <button type="button" class="btn btn-primary" data-toggle="tooltip" data-original-title="Print Off Carousel Put Away List" data-placement="top"><span class="glyphicon glyphicon-print"></span></button> button.
                <ul>
                    <li>Note:  The print button will be disabled when a tote is not selected or when the tote does not have any transactions assigned to it.</li>
                </ul>
            </li>
        </ol>
    </div>
</div>