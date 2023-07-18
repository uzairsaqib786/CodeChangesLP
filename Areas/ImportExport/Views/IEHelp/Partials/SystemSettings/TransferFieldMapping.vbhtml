<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <img src="~/Areas/ImportExport/Images/Help/transfer_modal.png" alt="Transfer File Field Mapping modal" />
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        The modal pictured above is for mapping file fields.  Each <strong>PickPro Field</strong> entry has a position based definition for each file to import or export.  A pad character can be set to enable the use of a separator, like a comma.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        The following fields are available:
        <ul>
            <li><strong>Start Position</strong> - Integer describing the first index of the field value.</li>
            <li><strong>Field Length</strong> - Integer describing how long the value of the field is.</li>
            <li><strong>End Position</strong> - Integer calculated to be the last position of the field's value according to the <strong>Start Position</strong> and <strong>Field Length</strong>.</li>
            <li><strong>Pad Char.</strong> - Pad Character that will separate every field value.  Single character in length.</li>
            <li><strong>Pad Field From Left</strong> - Whether the pad character will be applied to the right or left of the value.</li>
            <li><strong>Field Type</strong> - Predefined PickPro field type indicating whether the value is expected to be a number, a date, or something else.</li>
            <li><strong>Import/Export Format</strong> - Style of import/export field value, like with dates.  (MM/DD/YYYY or YYYY-MM-DD, etc.)</li>
        </ul>
        Some of the aforementioned fields are only available on transaction types that are imports or exports.  Some transaction types may have definitions for both importing and exporting, while others may have one or the other.
    </div>
    <div class="col-md-12">
        The following functions are available in the modal:
        <ul>
            <li>
                <div class="btn-group">
                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown">Sort <span class="caret"></span></button>
                    <ul class="dropdown-menu" role="menu">
                        <li><a>Alphabetically</a></li>
                        <li><a>By Start Position</a></li>
                    </ul>
                </div> provides the ability to sort the <strong>PickPro Fields</strong> column alphabetically or by the <strong>Start Position</strong> column.
            </li>
            <li>
                <button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button> provides the ability to define blank or irrelevant space in the import/export file.  
                This can be helpful in situations where the file being imported may have additional fields that PickPro does not map.
            </li>
            <li>
                <button class="btn btn-primary">Update All Other Maps</button> allows the user to alter the fields contained in the current mapping to any other applicable mappings. (Example: Warehouse is Start Position 10 in every import file, so update all other maps may be helpful to set the warehouse positions.)
            </li>
            <li>
                <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> is available on every record to provide a means to save any changes to that record.
            </li>
            <li>
                <div class="row">
                    <div class="col-md-5">
                        <label>Transaction Type | Transfer Type</label>
                        <select class="form-control">
                            <option value="">(None)</option>
                            <option data-xfer-type="Export" value="Adjustment">Adjustment | Export</option>
                            <option data-xfer-type="Import" value="Audit">Audit | Import</option>
                            <option data-xfer-type="Export" value="Complete">Complete | Export</option>
                            <option data-xfer-type="Export" value="Count">Count | Export</option>
                            <option data-xfer-type="Import" value="Count">Count | Import</option>
                            <option data-xfer-type="Export" value="Employees">Employees | Export</option>
                            <option data-xfer-type="Export" value="Epick">Epick | Export</option>
                            <option data-xfer-type="Import" value="Epick">Epick | Import</option>
                            <option data-xfer-type="Export" value="Inventory">Inventory | Export</option>
                            <option data-xfer-type="Import" value="Inventory">Inventory | Import</option>
                            <option data-xfer-type="Export" value="Inventory Map">Inventory Map | Export</option>
                            <option data-xfer-type="Import" value="Inventory Map">Inventory Map | Import</option>
                            <option data-xfer-type="Export" value="Location Change">Location Change | Export</option>
                            <option data-xfer-type="Export" value="Pick">Pick | Export</option>
                            <option data-xfer-type="Import" value="Pick">Pick | Import</option>
                            <option data-xfer-type="Export" value="Put Away">Put Away | Export</option>
                            <option data-xfer-type="Import" value="Put Away">Put Away | Import</option>
                            <option data-xfer-type="Export" value="Shipping Complete">Shipping Complete | Export</option>
                        </select>
                    </div>
                    <div class="col-md-7">
                        The control at the left provides a means to change what type of transaction you are mapping.
                    </div>
                </div>
            </li>
        </ul>
    </div>
</div>