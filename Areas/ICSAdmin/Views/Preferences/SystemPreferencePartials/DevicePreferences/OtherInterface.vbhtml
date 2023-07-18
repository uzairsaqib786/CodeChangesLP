<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row">
    <div class="col-md-12">
        <div class="row">
            <div class="col-md-6">
                <label>COM Port</label>
                <select class="form-control" id="COMPort">
                    <option>1</option>
                    <option>2</option>
                    <option>3</option>
                    <option>4</option>
                    <option>5</option>
                    <option>6</option>
                    <option>7</option>
                    <option>8</option>
                    <option>9</option>
                    <option>10</option>
                    <option>11</option>
                    <option>12</option>
                </select>
            </div>
            <div class="col-md-6">
                <label>Baud Rate</label>
                <select class="form-control" id="Baud">
                    <option>4800</option>
                    <option>9600</option>
                    <option>19200</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Parity</label>
                <select class="form-control" id="Parity">
                    <option>Odd</option>
                    <option>Even</option>
                    <option>None</option>
                </select>
            </div>
            <div class="col-md-6">
                <label>Word Length</label>
                <select class="form-control" id="WordLength">
                    <option>7</option>
                    <option>8</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <label>Stop Bit</label>
                <select class="form-control" id="StopBit">
                    <option>0</option>
                    <option>1</option>
                    <option>2</option>
                </select>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <button type="button" class="btn btn-primary" id="UpdateAllInterface">Update All Devices</button>
            </div>
            <div class="col-md-6">
                <button type="button" class="btn btn-primary" id="UpdateAllInterfaceZone">Update All With Same Zone</button>
            </div>
        </div>
    </div>
</div>