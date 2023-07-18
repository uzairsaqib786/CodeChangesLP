' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports combit.ListLabel21

''' <summary>
''' Contains all DataSet creation functions for selecting data to print through List and Label.
''' </summary>
''' <remarks></remarks>
Module CreateDataSet
    ''' <summary>
    ''' Creates a dataset for the List and Label object needing it
    ''' </summary>
    ''' <param name="data">Object with parameters for getting the dataset</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDataSet(data As CustomReportsModel, Optional returnDR As Boolean = False)
        Select Case data.view
            Case "Custom Report"
                If data.REPORTVIEW.Trim() = "" Then
                    Return CreateDataSetCustomDesign(data, returnDR)
                Else
                    Return CreateDataSetCustomReport(data, returnDR)
                End If
            Case Else
                insertDataSetError(data, 1, "CreateDataSet", "Report data.view not found.  View: " & data.view)
                Return Nothing
        End Select
    End Function
    ''' <summary>
    ''' Creates a new data set from the given ll report model
    ''' </summary>
    ''' <param name="data">Contains the information needed to create a new datatset</param>
    ''' <returns>The datatset</returns>
    ''' <remarks></remarks>
    Public Function CreateDataSetNew(data As LLReportModel)
        Dim DS As New DataSet
        Dim reader As SqlDataReader = Nothing
        Try
            reader = RunSPArray(data.DataSPName, data.DataParameters, _
                                    data.ConnectionString)
            If reader Is Nothing Then
                Throw New Exception($"An error occurred getting data from stored procedure {data.DataSPName}")
            End If
            If data.DataSPName = "selPickShortageReportFPZLL" Then
                DS.Load(reader, LoadOption.PreserveChanges, {"Data", "Data1", "Data2", "Data3"})
                Dim itemnum1 As DataRelation = DS.Relations.Add("ItemNum1", DS.Tables("Data").Columns("Item Number"), DS.Tables("Data1").Columns("Item Number"), False)
                Dim itemnum2 As DataRelation = DS.Relations.Add("ItemNum2", DS.Tables("Data").Columns("Item Number"), DS.Tables("Data2").Columns("Item Number"), False)
                Dim itemnum3 As DataRelation = DS.Relations.Add("ItemNum3", DS.Tables("Data").Columns("Item Number"), DS.Tables("Data3").Columns("Item Number"), False)
            Else
                DS.Load(reader, LoadOption.PreserveChanges, {"Data"})
            End If
        Catch ex As Exception
            insertDataSetError(data, 1, "CreateDataset SP : " & data.DataSPName, ex.ToString())
            Return Nothing
        Finally
            If Not IsNothing(reader) Then
                reader.Dispose()
            End If
        End Try
        Return DS
    End Function

    ''' <summary>
    ''' Creates a dataset for a custom design report (either created by end user or able to be edited by end user)
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataSetCustomDesign(data As CustomReportsModel, Optional returnDR As Boolean = False)
        Dim ds As New DataSet
        Dim reader As SqlDataReader = Nothing, SQL As String = "", SQLType As String = ""
        Try
            ' get the test data source
            reader = RunSPArray("selListsAndLabelsReportDS", {{"@WSID", data.WSID, strVar}, {"@Filename", data.reportName & IIf(data.type = LlProject.List, ".lst", ".lbl"), strVar}}, data.ConnectionString)

            If reader.HasRows Then
                If reader.Read() Then
                    ' test data source is .item (0)
                    If IsDBNull(reader(0)) Or IsDBNull(reader(1)) Then
                        Return New DataSet
                    Else
                        SQL = reader(0)
                        SQLType = reader(1)
                    End If
                End If
            End If

            reader.Dispose()
            reader = Nothing
            ' get the data
            If SQLType.Trim().ToUpper() = "SP" Then
                reader = RunSPArray(SQL, {{"nothing"}}, data.ConnectionString)
            Else
                reader = RunSPArray("selListsAndLabelsDSSQLString", {{"@SQLString", SQL, strVar}}, data.ConnectionString)
            End If
            

            If returnDR Then
                Return reader
            Else
                ' load it for list and label
                ds.Load(reader, LoadOption.PreserveChanges, {"Data", "Report Titles"})
            End If

        Catch ex As Exception
            insertDataSetError(data, 1, "CreateDataOrderStatus", ex.ToString())
            Return Nothing
        Finally
            If Not IsNothing(reader) And Not returnDR Then
                reader.Dispose()
            End If
        End Try

        Return ds
    End Function

    ''' <summary>
    ''' Creates ALL datasets for the custom reports screen
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateDataSetCustomReport(data As CustomReportsModel, Optional returnDR As Boolean = False)
        Dim ds As New DataSet()
        Dim reader As SqlDataReader = Nothing
        Try
            reader = RunSPArray("selReportData", _
                                {{"@WHERE", data.WHERE, strVar}, {"@ORDERBY", data.ORDERBY, strVar}, {"@VIEW", data.REPORTVIEW, strVar}, _
                                 {"@WSID", data.WSID, strVar}, {"@REPORT", data.reportName, strVar}, {"@TOP", data.TOP, strVar}}, data.ConnectionString)


            If returnDR Then
                Return reader
            Else
                ds.Load(reader, LoadOption.PreserveChanges, {"Data", "Report Titles"})
            End If
        Catch ex As Exception
            insertDataSetError(data, 1, "CreateDataSetCustomReport", ex.ToString())
            Return Nothing
        Finally
            If Not IsNothing(reader) And Not returnDR Then
                reader.Dispose()
            End If
        End Try
       
        Return ds
    End Function

End Module