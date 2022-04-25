Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls

Public Class GetSOPData
    Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
    Dim _cn As New SqlConnection(_sqlCon)
    Dim _cmd As String = ""
    Dim _gridview As GridView
    Dim _projectId As Integer

    Public Sub New(ByVal projectId As Integer, ByVal gridView As GridView)
        MyBase.New

        _projectId = projectId
        _gridview = gridView
        _cmd = "SELECT ReleaseName as 'Release Name', CASE WHEN ReleaseDate IS NULL THEN 'NOT YET RELEASE' ELSE BaselineName END AS 'Baseline Version' FROM VDbsBaseline WHERE ProjectId = " & _projectId & " And ReleaseTypeId = 4 ORDER BY BaselineId DESC"

        Me.loadSOPData(_projectId, _gridview)
    End Sub

    Public Sub loadSOPData(ByVal projId As Integer, ByVal datagridView As GridView)
        _cn.Open()

        Dim _sqlcommand As New SqlCommand(_cmd, _cn)
        Dim _reader As SqlDataReader = _sqlcommand.ExecuteReader

        With _gridview
            .DataSource = _reader
            .DataBind()
        End With

        _cn.Close()
    End Sub

End Class
