<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <a data-toggle="collapse" data-target="#GCConnection" data-parent="#GCParent">
            <h3 class="panel-title">
                Connection Setup <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse accordion-toggle" id="GCConnection">
        <div class="row">
            <div class="col-md-12">
                <img src="~/images/Help/GlobalConfig/connection.png" style="width: 60%"/>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                The Connection Setup tab allows the user to set connections for production databases.  The Configuration Database is for application wide data and settings like system and workstation preferences.  
                The connection strings section below the config database provides the connection data to the production databases that contain all production data (like transactions and inventory locations.)
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Fields: </strong>
                <ul>
                    <li><strong>Connection Name</strong> is the user friendly database name.  This can be set to anything, but should be something recognizable for users.</li>
                    <li><strong>Database Name</strong> is the SQL Server instance's database name to use for production for PickPro.  This is typically named something like PickProSD and it must match the real DB name.</li>
                    <li><strong>Server Name</strong> is the SQL Server instance name.  This has to match the server's name in order for PickPro to find data.</li>
                </ul>
                <strong>Add a new connection: </strong>
                <ol>
                    <li>Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button> button.</li>
                    <li>Fill out the appended entry's fields.</li>
                    <li>Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> button.</li>
                </ol>
                <strong>Delete a connection: </strong>
                <ol>
                    <li>Click the <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button of the entry you wish to delete.</li>
                    <li>Confirm the deletion by clicking OK in the prompt that follows.</li>
                </ol>
                <strong>Edit a connection: </strong>
                <ul>
                    <li>Data connections: 
                        <ol>
                            <li>Edit the fields of the entry you wish to change.</li>
                            <li>Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> button.</li>
                        </ol>
                    </li>
                    <li>
                        Configuration connection:
                        <ol>
                            <li>Click the <button class="btn btn-primary">Change Database</button> button.</li>
                            <li>Edit the fields in the modal that appears.</li>
                            <li>Click the <button class="btn btn-primary">Submit</button> button.</li>
                        </ol>
                    </li>
                </ul>
                <strong>Notes:</strong>
                <ul>
                    <li>Only one configuration database may be specified and it must be specified by clicking the <button class="btn btn-primary">Change Database</button> button.</li>
                    <li>Databases being added or deleted from this screen are not changed by this process.  This means that although PickPro may not use the database its state and data are preserved
                    should the user ever want to use that database.</li>
                    <li>Each workstation can only connect to one database at a time.  Switching databases that a workstation is connected to can be changed via the <strong>Preferences</strong> page.</li>
                </ul>
            </div>
        </div>
    </div>
</div>