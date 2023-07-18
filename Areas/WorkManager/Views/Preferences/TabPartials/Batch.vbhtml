<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

<div class="row top-spacer">
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">
                    Pick Sort
                </h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 1</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 2</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="PickSort1">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="PickSort2">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 3</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 4</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="PickSort3">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="PickSort4">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-10">
                        <label>Current Sequence Pick Sort Result</label>
                        <input type="text" id="PickSeqTempResult" disabled="disabled" class="form-control" />
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" data-toggle="tooltip" data-placement="top" title="Set Default Pick Sort" class="btn btn-primary" id="SavePickSortResult"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Default Pick Sort</label>
                        <input type="text" id="DefaultPickSort" class="form-control" disabled="disabled" value="@Model("Default Pick Sort")"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">
                    Put Sort
                </h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 1</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 2</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="PutSort1">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="PutSort2">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 3</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 4</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="PutSort3">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="PutSort4">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-10">
                        <label>Current Sequence Put Sort Result</label>
                        <input type="text" id="PutSeqTempResult" class="form-control" disabled="disabled" />
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" data-toggle="tooltip" data-placement="top" title="Set Default Put Sort" class="btn btn-primary" id="SavePutSortResult"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Default Put Away Sort</label>
                        <input type="text" id="DefaultPutSort" class="form-control" disabled="disabled" value="@Model("Default Put Sort")" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h4 class="panel-title">
                    Count Sort
                </h4>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 1</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 2</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="CountSort1">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="CountSort2">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label>Sort 3</label>
                    </div>
                    <div class="col-md-6">
                        <label>Sort 4</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <select class="form-control" id="CountSort3">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                    <div class="col-md-6">
                        <select class="form-control" id="CountSort4">
                            <option value=""></option>
                            <option value="[Required Date]">Required Date</option>
                            <option value="[Priority]">Priority</option>
                            <option value="[Order Number]">Order Number</option>
                            <option value="[User Field1]">User Field1</option>
                            <option value="[User Field2]">User Field2</option>
                            <option value="[User Field3]">User Field3</option>
                            <option value="[User Field4]">User Field4</option>
                            <option value="[User Field5]">User Field5</option>
                            <option value="[User Field6]">User Field6</option>
                            <option value="[User Field7]">User Field7</option>
                            <option value="[User Field8]">User Field8</option>
                            <option value="[User Field9]">User Field9</option>
                            <option value="[User Field10]">User Field10</option>
                        </select>
                    </div>
                </div>
                <div class="row top-spacer">
                    <div class="col-md-10">
                        <label>Current Sequence Count Sort Result</label>
                        <input type="text" id="CountSeqTempResult" class="form-control" disabled="disabled" />
                    </div>
                    <div class="col-md-2" style="padding-top:20px;">
                        <button type="button" data-toggle="tooltip" data-placement="top" title="Set Default Pick Sort" class="btn btn-primary" id="SaveCountSortResult"><span class="glyphicon glyphicon-floppy-disk"></span></button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Default Count Sort</label>
                        <input type="text" id="DefaultCountSort" disabled="disabled" class="form-control" value="@Model("Default Count Sort")" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
