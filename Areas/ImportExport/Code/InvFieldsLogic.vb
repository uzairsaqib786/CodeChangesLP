' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Namespace ImportExport
    Module InvFieldsLogic
        ''' <summary>
        ''' Gets all the data from the xfer modify fields table
        ''' </summary>
        ''' <returns>A list of dictionary that contains all data from the table</returns>
        Public Function GetXferModifyFields() As List(Of Dictionary(Of String, Object))
            Dim ret = GetResultMapList("SelXferModifyFields", "IE")
            Return ret
        End Function

        ''' <summary>
        ''' Updates the desired xfer fields
        ''' </summary>
        ''' <param name="listOfInvFields">A list of models that contains the new valuesfor all the fields that are changing</param>
        Public Sub UpdateInvFields(ByVal listOfInvFields As List(Of InvFieldsModel))
            For Each model As InvFieldsModel In listOfInvFields
                RunSP("UpdXferModifyFields", "IE", {
                    {"@ID", model.ID, intVar},
                    {"@Modify", model.Modify, boolVar}
                })
            Next
        End Sub
    End Module
End Namespace

