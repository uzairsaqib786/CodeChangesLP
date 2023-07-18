<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="panel panel-info">
    <div class="panel-heading">
        <a data-toggle="collapse" data-target="#GCPrinters" data-parent="#GCParent">
            <h3 class="panel-title">
                Printer Setup <span class="accordion-caret-down"></span>
            </h3>
        </a>
    </div>
    <div class="panel-body collapse accordion-toggle" id="GCPrinters">
        <div class="row">
            <div class="col-md-12">
                <img src="~/images/Help/GlobalConfig/printers.png" style="width: 60%" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                The Printer Setup tab allows the user to define networked printers for use in the print service.  
                The title will indicate whether the print service is connected to the web application at the current time.  
                Any printer to be used by the PickPro application is required to be networked and available to the Print Service's host machine.  
                Print previews will allow the user to print to any printer that is known to their local machine or to print to an electronic format, like PDF.
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Stop the Print Service:</strong> Click the <button class="btn btn-danger">Stop Print Service</button> button.
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Start the Print Service:</strong> Click the <button class="btn btn-primary">Start Print Service</button> button.
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Restart the Print Service:</strong> Click the <button class="btn btn-warning">Restart Print Service</button> button.
            </div>
        </div>
        <div class="row top-spacer">
            <div class="col-md-12">
                <strong>Test the Print Service:</strong>  Click the <button class="btn btn-primary">Test Print Service</button> button.  This will launch a modal with several configuration options.  This test print should 
                only be used when testing ALL the networked printers or ALL the reports.  Generally this test will only be run when the initial program setup is done.  Be sure to pause any printers that you are testing beforehand to 
                ensure that you're not wasting paper!  This test function also has the ability to test export types which will not be sent to the printer but will generate files on the server.
            </div>
            <div class="col-md-12">
                Other testing options are available next to each printer listed on the page.  Clicking the <button class="btn btn-primary"><span class="glyphicon glyphicon-print"></span></button> button 
                will send a single test page to the printer in question (label or report depending on the setting of the <strong>Label Printer</strong> field.)
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Notes:</strong> The <button class="btn btn-danger">Stop Print Service</button> and <button class="btn btn-primary">Start Print Service</button> buttons are the same button with different text.
                If the service is running the stop button will be available otherwise the start button will be available.  Restart just stops and then starts the service.  The service must reside on the same
                server as the web application, otherwise the web application will not be able to reach the service in order to start it.
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Adding a new printer:</strong>
                <ol>
                    <li>
                        Click the add new printer button (<button class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span></button>)
                    </li>
                    <li>
                        Type the printer's user friendly name so that it can be identified by users choosing a printer on the <strong>Preferences</strong> page.
                    </li>
                    <li>
                        Type the printer's address.  This should be structured as \\computer_name\printer name if it is a network printer.  If it is connected directly to the server then it should be the printer name
                        as seen in Devices and Printers in Windows.
                    </li>
                    <li>
                        Choose whether the specified printer is a label printer.  If it is being used for both a second entry should be made specifying the same information with a different friendly printer name.
                    </li>
                    <li>
                        Click the <button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button> button.
                    </li>
                </ol>
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Editing a printer:</strong>
                <ol>
                    <li>
                        Edit the fields desired.
                    </li>
                    <li>
                        Click the save button. (<button class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span></button>)
                    </li>
                </ol>
            </div>
        </div>
        <div class="row" style="padding-top:15px;">
            <div class="col-md-12">
                <strong>Deleting a printer:</strong>
                <ol>
                    <li>Click the <button class="btn btn-danger"><span class="glyphicon glyphicon-remove"></span></button> button.</li>
                    <li>Confirm deletion if the printer was already saved.  If it is a new entry it will delete without prompt.</li>
                </ol>
            </div>
        </div>
    </div>
</div>