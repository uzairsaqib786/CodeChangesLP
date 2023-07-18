<!--PeakLogix Proprietary and Confidential-->
<!--Copyright Peaklogix 2022-->

@ModelType Dictionary(Of String, Object)
<div class="panel panel-info">
    <div class="panel-body">
        <div class="panel panel-info">
            <div class="panel-body white-bg">
                <table class="table">
                    <tbody>
                        <tr>
                            <td>
                                <label>Export Type</label>
                                <select class="form-control" name="ExportInvMapType">
                                    @GlobalHTMLHelpers.SelectOption(Model("Export Inv Map Type"), {"File", "Table"})
                                </select>
                            </td>
                            <td class="vert-bottom">
                                <button class="btn btn-lg btn-primary btn-block" style="vertical-align:middle" id="ExpInvMapManager">Export Inventory Map Manager</button>
                            </td>
                            <td>
                                <label>Inventory Map Export File</label>
                                <input type="text" class="form-control" name="InvMapExportFile" value="@Model("Inv Map Export File")" />
                            </td>
                            <td class="vert-bottom">
                                @If Model("Export Inv Map Type") = "File" Then
                                    @<button class="btn btn-lg btn-primary btn-block" id="ExpInvMap">Export Inventory Map</button>
                                Else
                                    @<button class="btn btn-lg btn-primary btn-block" disabled id="ExpInvMap">Export Inventory Map</button>
                                End If
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Import Type</label>
                                <select class="form-control" name="ImportInvMapType">
                                    @GlobalHTMLHelpers.SelectOption(Model("Import Inv Map Type"), {"File", "Table"})
                                </select>
                            </td>
                            <td class="vert-bottom">
                                <button class="btn btn-lg btn-primary btn-block" id="ImpInvMapManager">Import Inventory Map Manager</button>
                            </td>
                            <td>
                                <label>Inventory Map Import File</label>
                                <input type="file" id="InvMapFileSelect"/>
                                <input style="display:none;" type="text" class="form-control" name="InvMapImportFile" value="@Model("Inv Map Import File")" />
                            </td>
                            <td class="vert-bottom">
                                @If Model("Import Inv Map Type") = "File" Then
                                    @<button class="btn btn-lg btn-primary btn-block" id="ImpInvMap">Import Inventory Map</button>
                                Else
                                    @<button class="btn btn-lg btn-primary btn-block" disabled id="ImpInvMap">Import Inventory Map</button>
                                End If
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Export Type</label>
                                <select class="form-control" name="ExportInvType">
                                    @GlobalHTMLHelpers.SelectOption(Model("Export Inventory Type"), {"File", "Table"})
                                </select>
                            </td>
                            <td class="vert-bottom">
                                <button class="btn btn-lg btn-primary btn-block" id="ExpInvManager">Export Inventory Manager</button>
                            </td>
                            <td>
                                <label>Inventory Export File</label>
                                <input type="text" class="form-control" name="InvExportFile" value="@Model("Inventory Export File")" />
                            </td>
                            <td class="vert-bottom">
                                @If Model("Export Inventory Type") = "File" Then
                                    @<button class="btn btn-lg btn-primary btn-block" id="ExpInv">Export Inventory</button>
                                Else
                                    @<button class="btn btn-lg btn-primary btn-block" disabled id="ExpInv">Export Inventory</button>
                                End If
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Import Type</label>
                                <select class="form-control" name="ImportInvType">
                                    @GlobalHTMLHelpers.SelectOption(Model("Import Inventory Type"), {"File", "Table"})
                                </select>
                            </td>
                            <td class="vert-bottom">
                                <button class="btn btn-lg btn-primary btn-block" id="ImpInvManager">Import Inventory Manager</button>
                            </td>
                            <td>
                                <label>Inventory Import File</label>
                                <input type="file" id="InvFileSelect" />
                                <input style="display:none;" type="text" class="form-control" name="InvImportFile"value="@Model("Inventory File")" />
                            </td>
                            <td class="vert-bottom">
                                @If Model("Import Inventory Type") = "File" Then
                                    @<button class="btn btn-lg btn-primary btn-block" id="ImpInv">Import Inventory</button>
                                Else
                                    @<button class="btn btn-lg btn-primary btn-block" disabled id="ImpInv">Import Inventory</button>
                                End If
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Event Export File</label>
                            </td>
                            <td colspan="3">
                                <input type="text" class="form-control" name="EventExportFile" value="@Model("Event Export File")"/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</div>