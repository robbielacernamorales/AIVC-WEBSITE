Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls
Public Class _Default
    Inherits Page

    Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
    Dim _cn As New SqlConnection(_sqlCon)
    Dim _cmd As String = ""
    Dim _gridview As GridView
    Dim _dataset As DataSet
    Dim _library As New Library
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim _dailyBuildSchedule As New DailyBuildSchedules
        'If there is a schedule
        If _dailyBuildSchedule.BuildScheduleToday Then
            wehavebuild.Visible = True
            wedonthavebuild.Visible = False
        Else 'If no schedule
            'Response.Write("Output: No Schedule(s) Today!")
            wehavebuild.Visible = False
            wedonthavebuild.Visible = True
        End If


        Dim _futurebuildSchedule As New FutureBuildSchedules
        'If there is a future schedules
        If _futurebuildSchedule.FutureBuildSchedule Then
            wehavefuturebuilds.Visible = True
            wedonthavefuturebuilds.Visible = False
        Else 'no future schedules
            wehavefuturebuilds.Visible = False
            wedonthavefuturebuilds.Visible = True
        End If
        'CONTINUE TOMORROW'

        _gridview = GridView1
        Me.LoadBuildScheduleToday()

    End Sub

    Private Sub LoadBuildScheduleToday()
        _dataset = _library.GetBaselineSchedule()

        With _gridview
            .DataSource = _dataset.Tables("SelectBaseline").DefaultView
            .DataBind()
        End With

    End Sub
End Class