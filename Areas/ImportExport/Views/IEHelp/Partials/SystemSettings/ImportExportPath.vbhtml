<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <img src="~/Areas/ImportExport/Images/Help/ie_file_modal.png" alt="Import/Export File Path Setup modal" />
    </div>
    <div class="col-md-12">
        The Import/Export File Path Setup modal allows users to assign a folder to import from or export to for each transaction type.  Certain transaction types are limited to either imports or exports and some of the controls will not be enabled because of this.  
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        The following fields are available for editing:
        <ul>
            <li><strong>Transaction Type</strong> - Available for edit only for the Host definition (<strong>Host Type</strong>).  The field indicates which <strong>Host Type</strong> is mapped to the corresponding PickPro field.</li>
            <li><strong>Use Date/Time for Filenames</strong> - Indicates whether export files will be named according to their time created.</li>
            <li><strong>Import File Path</strong> - File folder where the record's <strong>Transaction Type</strong> files are stored before import.  Export only transaction types do not use this field.</li>
            <li><strong>Active</strong> - Indicates whether the currently record's definition is to be used or not.</li>
            <li><strong>Import Ext. - File extension of the import files.  (txt, csv, etc.)</strong></li>
            <li><strong>Host Type</strong> - Host transaction type.</li>
            <li><strong>Export to File/Table</strong> - Indicates whether the current record will be exported to a file or to a table when the export process is initiated.</li>
            <li><strong>Export File Path</strong> - File folder where the record's exports will be stored.</li>
            <li><strong>Export Filename</strong> - File name used in addition to the date time created (if <strong>Use Date/Time for Filenames</strong> is checked.)</li>
            <li><strong>Export Extension</strong> - File extension to export to (csv, txt, etc.)</li>
        </ul>
        Note:  All file paths are relative to the server's path, NOT your local machine.
    </div>
</div>
<div class="row top-spacer">
    <div class="col-md-12">
        The following actions are available:
        <ul>
            <li><button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button></li> - Saves the current record if there were any changes.
            <li><button class="btn btn-default">Close</button> - Closes the modal without saving any pending changes.</li>
        </ul>
    </div>
</div>