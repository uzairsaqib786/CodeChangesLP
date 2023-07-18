' PeakLogix Proprietary and Confidential
' Copyright Peaklogix 2022

Imports System.Data.SqlClient

Public Class Category
    ''' <summary>
    ''' Gets all categories/subcategories
    ''' </summary>
    ''' <param name="user">Requesting user.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>A object that contains the the lists of both categories and subcategories</returns>
    ''' <remarks></remarks>
    Public Shared Function getCategories(user As String, WSID As String) As Object
        Dim DataReader As SqlDataReader = Nothing
        Dim categories As New List(Of String)
        Dim subcategories As New List(Of String)

        Try
            DataReader = RunSPArray("selCategories", WSID, {{"nothing"}})

            If DataReader.HasRows Then
                While DataReader.Read()
                    categories.Add(IIf(IsDBNull(DataReader(0)), "", DataReader(0)))
                    subcategories.Add(IIf(IsDBNull(DataReader(1)), "", DataReader(1)))
                End While
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Category", "getCategories", ex.ToString(), user, WSID)
        Finally
            If Not IsNothing(DataReader) Then
                DataReader.Dispose()
            End If
        End Try

        Return New With {.categories = categories, .subcategories = subcategories}
    End Function

    ''' <summary>
    ''' Deletes a category/subcategory combination and logs the event in the Event Log
    ''' </summary>
    ''' <param name="user">User requesting deletion.</param>
    ''' <param name="category">Category to delete.</param>
    ''' <param name="subcategory">Subcategory of the entry to delete.</param>
    ''' <param name="WSID">The workstation that is being worked on</param>
    ''' <returns>T/F = Success/Failure</returns>
    ''' <remarks></remarks>
    Public Shared Function delCategory(user As String, category As String, subcategory As String, WSID As String) As Boolean
        Try
            RunActionSP("delCategory", WSID, {{"@Category", category, strVar}, {"@Subcategory", subcategory, strVar}, {"@User", user, strVar}, {"@WSID", WSID, strVar}})
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Category", "delCategory", ex.ToString(), user, WSID)
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' Saves a new or edited category/subcategory
    ''' </summary>
    ''' <param name="cat">Category value to save.</param>
    ''' <param name="subcat">Subcategory value to save.</param>
    ''' <param name="user">User requesting save.</param>
    ''' <param name="oldcat">Old category value to update.</param>
    ''' <param name="oldsub">Old subcategory value to update.</param>
    ''' <returns>T/F = success/failure</returns>
    ''' <remarks></remarks>
    Public Shared Function saveCategory(cat As String, subcat As String, user As String, oldcat As String, oldsub As String, WSID As String) As Boolean
        Dim SP As String = IIf(oldcat = "", "insNewCategory", "updateCategory")
        Dim params As String(,) = IIf(oldcat = "", {{"@Category", cat, strVar}, {"@Subcategory", subcat, strVar}}, _
                                      {{"@OldCategory", oldcat, strVar}, {"@NewCategory", cat, strVar}, {"@OldSubcategory", oldsub, strVar}, {"@NewSubcategory", subcat, strVar}})
        Try
            RunActionSP(SP, WSID, params)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString())
            insertErrorMessages("Category", "saveCategory", ex.ToString(), user, WSID)
            Return False
        End Try
        Return True
    End Function
End Class
