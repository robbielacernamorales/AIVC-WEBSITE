Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls
Public Class DailyBuildSchedules
    Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
    Dim _cn As New SqlConnection(_sqlCon)
    Dim _baselineColumns As String = "BaselineId, ProjectName, BuildDate, BaselineName, BranchName, ReleaseName, VuCPlatform, SoCPlatform"
    Dim _cmd As String = "SELECT " + _baselineColumns + " FROM VDbsBaseline2"

    Public Sub New()
        MyBase.New

    End Sub

    Public Function BuildScheduleToday() As Boolean
        Dim _adapter As New SqlDataAdapter
        Dim _dataset As New DataSet

        _cn.Open()
        _adapter = New SqlDataAdapter(_cmd, _cn)
        _adapter.SelectCommand.CommandType = CommandType.Text
        _adapter.Fill(_dataset, "BuildSchedules")
        _cn.Close()

        If _dataset.Tables("BuildSchedules").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If



    End Function


End Class
