<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-body">
        <div class="col-md-3">
                <button class="btn btn-primary btn-lg btn-block bottom-spacer" id="PurgeNow">Purge Now</button>
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <ul>
                        <li>"Purge Days" are number of days to keep before archiving/purging</li>
                        <li>To never purge or archive, set "Purge Days" to 0</li>
                        <li>Leaving "Archive" unchecked with only send tables to history</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-9">
            <div class="panel panel-info">
                <div class="panel-body white-bg">
                    <table class="table center-th" id="purgetable">
                        <thead>
                            <tr>
                                <th>Table Name</th>
                                <th>Purge Days</th>
                                <th>Archive</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</div>