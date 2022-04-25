Imports System
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI.WebControls

Public Class SWRelease
	Inherits Page

	Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
	'Dim _con As String = "server=10.221.244.125\SQLEXPRESS; database=SyIntegration; user id=sys_int; password=continental1; connect timeout=0; timeout=0"
	Dim _cn As New SqlConnection(_sqlCon)
	Dim _cmd As String = ""
	Private WithEvents _gridView As GridView

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

		Dim _getSOP1 As New GetSOPData(1, Me.GridView1)
		Dim _getSOP2 As New GetSOPData(2, Me.GridView2)
		Dim _getSOP3 As New GetSOPData(3, Me.GridView3)

	End Sub

	Private Sub GridView1Sorting(sender As Object, e As EventArgs) Handles GridView1.Sorting

	End Sub

End Class