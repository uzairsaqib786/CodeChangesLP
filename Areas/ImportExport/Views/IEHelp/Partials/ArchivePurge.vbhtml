<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <h3 class="panel-title">Archive/Purge</h3>
    </div>
    <div class="panel-body">
        <div class="row">
            <div class="col-md-12">
                <img src="~/Areas/ImportExport/Images/Help/archive.png" alt="Archive/Purge tab" />
            </div>
            <div class="col-md-12">
                The <strong>Archive/Purge</strong> tab allows the user to archive and/or purge old records from their PickPro application.  Bloating in tables can eventually have a negative effect on performance or
                cause issues with trying to find specific records.  After a period of time you may wish to expunge old, completed records to treat the symptoms of bloating or prevent it altogether.  The tab allows 
                the user to set time periods defining how long records should be kept automatically, or allow the user to immediately eliminate old records.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                The fields available on the tab are:
                <ul>
                    <li><strong>Table Name</strong> - Indicates which table you are setting purge data for.</li>
                    <li><strong>Purge Days</strong> - Indicates the number of days that must pass before a purge will automatically take place on the current record's table.  If you do not wish to use purging/archiving at all simply set this to 0.</li>
                    <li><strong>Archive</strong> - Indicates whether the current record's table has an archive version.  After going to the history table the history table will be sent to the archive table if the settings permit.</li>
                </ul>
                If a table has an archive table associated with it there will be multiple steps to the purge process.  First the records that are marked for deletion (by their "completed" date and the <strong>Purge Days</strong> field.) will be moved 
                to an archive table if it exists with a new archive date.  Then the records that were migrated will be deleted from their original table.  Lastly any records in the corresponding archive table that have exceeded their maximum purge days 
                will be permanently deleted.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                The singular action available on this tab is the <button class="btn btn-primary">Purge Now</button> button which will cause the purge process to begin immediately after confirmation from the user.
            </div>
        </div>
    </div>
</div>