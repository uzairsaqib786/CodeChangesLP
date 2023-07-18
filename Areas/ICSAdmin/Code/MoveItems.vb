' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Namespace Admin
    Public Class MoveItems
        ''' <summary>
        ''' Creates the desired move transaction
        ''' </summary>
        ''' <param name="MoveFromID">The id of the loction to move from</param>
        ''' <param name="MoveToID">The id of the location to move to</param>
        ''' <param name="MoveFromItemNumber">The item number that is being moved</param>
        ''' <param name="MoveToItemNumber">Tells if the move transction is increasing the quantity of the item number at the location</param>
        ''' <param name="MoveToZone">The zone that is being move that is being moved into</param>
        ''' <param name="MoveQty">The quantity that is being moved</param>
        ''' <param name="ReqDate">The required date for the item number that is being moved</param>
        ''' <param name="Priority">The priority of the item number that is being moved</param>
        ''' <param name="dedicateMoveTo">IF the move to location is a dedicated location</param>
        ''' <param name="unDedicateMoveFrom">If the move from location is a dedicated location</param>
        ''' <param name="User">The user that is logged in</param>
        ''' <param name="WSID">The workstation that is being worked on</param>
        ''' <returns>A string telling if the operation completed successfully</returns>
        ''' <remarks></remarks>
        Public Shared Function CreateMoveTransactions(MoveFromID As Integer, MoveToID As Integer, MoveFromItemNumber As String, MoveToItemNumber As String, _
                                                      MoveToZone As String, MoveQty As Integer, ReqDate As DateTime, Priority As Integer, _
                                                      dedicateMoveTo As Boolean, unDedicateMoveFrom As Boolean, User As String, WSID As String) As String
            Dim OrderNumber = GlobalFunctions.getNextBatchID(WSID, User)
            If OrderNumber = "" Then
                Return "Error"
            Else
                Try
                    'Create the Pick Transaction
                    RunActionSP("insOpenTransMoveFrom", WSID, {{"@OrderNum", OrderNumber, strVar}, _
                                                              {"@InvMapID", MoveFromID, intVar}, _
                                                              {"@MoveQty", MoveQty, intVar}, _
                                                              {"@User", User, strVar}, _
                                                              {"@ReqDate", ReqDate, dteVar}, _
                                                              {"@Priority", Priority, intVar}})
                    'Create the Put Away Transaction
                    RunActionSP("insOpenTransMoveTo", WSID, {{"@OrderNum", OrderNumber, strVar}, _
                                                              {"@MoveFromID", MoveFromID, intVar}, _
                                                              {"@MoveToID", MoveToID, intVar}, _
                                                              {"@MoveQty", MoveQty, intVar}, _
                                                              {"@User", User, strVar}, _
                                                              {"@ReqDate", ReqDate, dteVar}, _
                                                              {"@Priority", Priority, intVar}})
                    If MoveToItemNumber = "" Then
                        'No Item in moveTo, update record depending on Warehouse settings for Zone
                        RunActionSP("updMoveTransAllocated", WSID, {{"@MoveFromID", MoveFromID, intVar}, _
                                                                  {"@MoveToID", MoveToID, intVar}, _
                                                                  {"@MoveToZone", MoveToZone, strVar}, _
                                                                  {"@ItemNumber", MoveFromItemNumber, strVar}})
                    Else
                        'Item in MoveTo, update record with only Qty Data
                        RunActionSP("updMoveTransAllocatedQtyOnly", WSID, {{"@MoveFromID", MoveFromID, intVar}, _
                                                                  {"@MoveToID", MoveToID, intVar}, _
                                                                  {"@MoveToZone", MoveToZone, strVar}})
                    End If

                    If dedicateMoveTo Then
                        'Make MoveTo locatino dedicated
                        RunActionSP("updInvMapDedicated", WSID, {{"@Dedicate", True, boolVar}, _
                                                                  {"@InvMapID", MoveToID, intVar}})
                    End If
                    If unDedicateMoveFrom Then
                        'Make MoveFrom location no longer dedicated
                        RunActionSP("updInvMapDedicated", WSID, {{"@Dedicate", False, boolVar}, _
                                                                    {"@InvMapID", MoveFromID, intVar}})
                    End If

                    'Update Master record IDs for Open Transactions
                    RunActionSP("updOTMastRecID", WSID, {{"nothing"}})
                    Return "Success"
                Catch ex As Exception
                    insertErrorMessages("Move Items", "CreateMoveTransactions", ex.Message, User, WSID)
                End Try

                Return "Error"
            End If
        End Function


       
    End Class
End Namespace

