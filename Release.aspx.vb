Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls

Public Class SWRelease
	Inherits Page

	Dim _sqlCon As String = "server=10.221.244.125\SQLEXPRESS; database=SyIntegration; user id=sys_int; password=continental1; connect timeout=0; timeout=0"
	Dim _cn As New SqlConnection(_sqlCon)
	Dim _cmd As String = ""

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		_cn.Open()

		_cmd = "SELECT ReleaseName, BaselineName FROM VDbsBaseline WHERE ProjectId = 1 And ReleaseTypeId = 4 ORDER BY BaselineId"
		Dim _sqlcommand As New SqlCommand(_cmd, _cn)
		Dim _reader As SqlDataReader = _sqlcommand.ExecuteReader

		With Me.GridViewSOP1
			.DataSource = _reader
			.DataBind()
		End With

		_cn.Close()

	End Sub
End Class