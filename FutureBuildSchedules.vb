Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls
Public Class FutureBuildSchedules
    Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
    Dim _cn As New SqlConnection(_sqlCon)
    Dim _baselineColumns As String = "ProjectName as 'SOP', BaselineName as 'Baseline Name', CONVERT(VARCHAR(20), BuildDate, 100) as 'Scheduled Build Date / Time', BranchName as 'Branch', ReleaseName as 'SW Release', VuCPlatform as 'VUC Platform (TVIP)', SoCPlatform as 'SOC Platform (OTP)'"
    Dim _viewTable As String = "VDbsBaselineFutureBuilds"
    Dim _cmd As String = "SELECT " + _baselineColumns + " FROM " + _viewTable

    Public Sub New()
        MyBase.New

    End Sub

    Public Function FutureBuildSchedule() As Boolean
        Dim _adapter As New SqlDataAdapter
        Dim _dataset As New DataSet

        _cn.Open()
        _adapter = New SqlDataAdapter(_cmd, _cn)
        _adapter.SelectCommand.CommandType = CommandType.Text
        _adapter.Fill(_dataset, "FutureBuildSchedule")
        _cn.Close()

        If _dataset.Tables("FutureBuildSchedule").Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function GetFutureBuildSchedules() As DataSet
        Dim _adapter As New SqlDataAdapter
        Dim _dataset As New DataSet

        Try
            _cn.Open()
            _adapter = New SqlDataAdapter("SELECT " + _baselineColumns + " FROM " + _viewTable, _cn)
            _adapter.SelectCommand.CommandType = CommandType.Text
            _cn.Close()

            _dataset = New DataSet
            _adapter.Fill(_dataset, "FutureBuildSchedule")

            Return _dataset
        Catch ex As Exception
            Return _dataset
        End Try
    End Function

End Class
