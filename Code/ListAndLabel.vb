' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports combit.ListLabel19
Imports combit.ListLabel19.Dom

Public Class ListAndLabel
    Shared _ll As New ListLabel

    '*******************************************************************************
    ' List and Label Printing
    '*******************************************************************************
    Public Shared Sub PrintReport(server As HttpServerUtilityBase, dataSet As DataSet, printer As String, user As String, template As String)
        
        Try
            ' reports directory
            Dim currDirect = server.MapPath("~\Reports\" & user)
            ' report design location
            Dim reportDesign = server.MapPath("~\LLDesign\" & template & ".lst")


            Dim path As String = ""

            ' create output directory if it doesn't exist
            If Not System.IO.Directory.Exists(currDirect) Then
                System.IO.Directory.CreateDirectory(currDirect)
            End If
            ' naming convention by date for output files to ensure uniqueness
            Dim datstring As String = DateTime.Now.ToString.Replace(" ", "").Replace("/", "").Replace(":", "")
            path = System.IO.Path.Combine(currDirect, datstring + ".PDF")


            Dim exportConfiguration As New ExportConfiguration(LlExportTarget.Pdf, path, reportDesign)
            _ll.DataSource = dataSet
            ' EXPORT AS PDF
            _ll.Export(exportConfiguration)

            ' ACTUAL PRINTING
            'Dim startInfo As New ProcessStartInfo
            'startInfo.FileName = path
            ''startInfo.WindowStyle = ProcessWindowStyle.Hidden
            'startInfo.Verb = "PrintTo"
            'startInfo.Arguments = "\\syrstserv1\"
            'startInfo.UseShellExecute = True
            'startInfo.CreateNoWindow = False
            'Dim report As Process = New Process
            'report.StartInfo = startInfo
            'report.Start()

            Try
                Dim MyProcess = New Process
                MyProcess.StartInfo.FileName = "C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe"
                MyProcess.StartInfo.Arguments = "/p " + """" + path + """" '+ " " + """" + """" + printer + """"
                MyProcess.StartInfo.UseShellExecute = False
                MyProcess.StartInfo.CreateNoWindow = True
                MyProcess.StartInfo.Verb = "print"
                MyProcess.Start()
                MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                If MyProcess.HasExited = False Then
                    MyProcess.WaitForExit(10000)
                    MyProcess.Kill()
                End If
                MyProcess.EnableRaisingEvents = True
                MyProcess.CloseMainWindow()
                MyProcess.Close()

            Catch ex As Exception

                Debug.WriteLine(ex.Message)
            End Try


        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try

    End Sub

    

    

    


End Class
