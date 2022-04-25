Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json

Public Class Library

	Dim _sqlCon As String = ConfigurationManager.ConnectionStrings("sqlConnection").ConnectionString
	Dim _sql As New SqlConnection(_sqlCon)
	Dim _cmd As String = ""

	Public Function GetBaselineSchedule() As DataSet
		Dim _adapter As New SqlDataAdapter
		Dim _dataset As New DataSet
		Dim _showBaselineRelease As String = ""
		Dim _baselineColumns As String = "ProjectName as 'SOP', BaselineName as 'Baseline Name', CONVERT(VARCHAR(20), BuildDate, 100) as 'Build Date / Time', BranchName as 'Branch', ReleaseName as 'SW Release', VuCPlatform as 'VUC Platform (TVIP)', SoCPlatform as 'SOC Platform'"
		Try
			_sql.Open()
			_adapter = New SqlDataAdapter("SELECT " + _baselineColumns + " FROM VDbsBaseline2", _sql)
			_adapter.SelectCommand.CommandType = CommandType.Text
			_sql.Close()

			_dataset = New DataSet
			_adapter.Fill(_dataset, "SelectBaseline")

			'If record > 0
			If _dataset.Tables("SelectBaseline").Rows.Count > 0 Then
				_showBaselineRelease += "BUILD SCHEDULE TODAY:" + vbNewLine
				_showBaselineRelease += ":===================:" + vbNewLine
				For Each _row In _dataset.Tables("SelectBaseline").Rows
					' Project Name - BaselineVersion
					_showBaselineRelease += _row.Item("ProjectName").ToString & " - " & _row.Item("BaselineName").ToString & _row.Item("BuildDate").ToString + vbNewLine
					'MessageBox.Show(_showBaselineRelease)
				Next
			End If

			Return _dataset
		Catch ex As Exception
			Return _dataset
		End Try
	End Function

	Public Function GetBaselineScheduleToday() As DataSet
		Dim _adapter As New SqlDataAdapter
		Dim _dataset As New DataSet
		Dim _showBaselineRelease As String = ""

		Try
			_sql.Open()
			_adapter = New SqlDataAdapter("GetBaselineBuildByDateToday", _sql)
			_adapter.SelectCommand.CommandType = CommandType.StoredProcedure
			_sql.Close()

			_dataset = New DataSet
			_adapter.Fill(_dataset, "SelectBaseline")

			''If record > 0
			'If _dataset.Tables("SelectBaseline").Rows.Count > 0 Then
			'	For Each _row In _dataset.Tables("SelectBaseline").Rows
			'		' Project Name - BaselineName
			'		MessageBox.Show(_row.Item("ProjectName").ToString & " - " & _row.Item("BaselineName").ToString)
			'	Next
			'End If

			Return _dataset
		Catch ex As Exception
			Return _dataset
		End Try
	End Function

	Public Function GetBaselineScheduleForToday() As Integer
		Dim _value As Integer = 0
		Try
			Dim _adapter As New SqlDataAdapter
			Dim _dataset As New DataSet
			_sql.Open()

			_adapter = New SqlDataAdapter("GetBaselineBuildByDateToday", _sql)
			_adapter.SelectCommand.CommandType = CommandType.StoredProcedure

			_dataset = New DataSet
			_adapter.Fill(_dataset, "GetBaselineCountForBuildToday")

			_value = CInt(_dataset.Tables("GetBaselineCountForBuildToday").Rows(0).Item("BaselineCount").ToString)
			_sql.Close()

		Catch ex As Exception
			Return 0
		End Try

		Return _value

	End Function

	Public Function GetCompanyIdByCompanyName(ByVal CompanyName As String) As Integer
		Dim _value As Integer = 0
		Try
			Dim _adapter As New SqlDataAdapter
			Dim _dataset As New DataSet
			_sql.Open()

			_adapter = New SqlDataAdapter("SelectCompany-CompanyName", _sql)
			_adapter.SelectCommand.CommandType = CommandType.Text
			_adapter.SelectCommand.Parameters.AddWithValue("CompanyName", CompanyName)

			_dataset = New DataSet
			_adapter.Fill(_dataset, "SelectCompany-CompanyName")

			_value = CInt(_dataset.Tables("SelectCompany-CompanyName").Rows(0).Item("CompanyId-CompanyName").ToString)
			_sql.Close()
		Catch ex As Exception
			Return 0
		End Try

		Return _value

	End Function

	Public Function HasCompanyRecord(ByVal procedureName As String, ByVal CompanyName As String) As Boolean
		Dim _return As Boolean = False

		Try
			Dim _adapter As New SqlDataAdapter
			Dim _dataset As New DataSet
			_sql.Open()

			_adapter = New SqlDataAdapter(procedureName, _sql)
			_adapter.SelectCommand.CommandType = CommandType.StoredProcedure
			_adapter.SelectCommand.Parameters.AddWithValue("CompanyName", CompanyName)

			_dataset = New DataSet
			_adapter.Fill(_dataset, procedureName)

			If _dataset.Tables(procedureName).Rows.Count > 1 Then
				_return = True
			Else
				_return = False
			End If

			_sql.Close()
		Catch ex As Exception
			_return = False
		End Try

		Return _return

	End Function

	Public Function GetUserAssignedByCompanyName(ByVal companyName As String) As Integer
		Dim _result As Integer = 0
		Try
			Dim _adapter As New SqlDataAdapter
			Dim _dataset As New DataSet
			_sql.Open()

			_adapter = New SqlDataAdapter("Select UserAssigned from DbsCompany Where CompanyName = '" + companyName.Trim + "'", _sql)
			_adapter.SelectCommand.CommandType = CommandType.Text

			_dataset = New DataSet
			_adapter.Fill(_dataset, "SelectCompanyId")

			_result = CInt(_dataset.Tables("SelectCompanyId").Rows(0).Item("UserAssigned").ToString())
			_sql.Close()
		Catch ex As Exception
			Return _result
		End Try

		Return _result

	End Function

	'The function used to encrypt the text
	Private Shared Function Encrypt(ByVal strText As String, ByVal strEncrKey As String) As String
		Dim byKey() As Byte = {}
		Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

		Try
			byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))

			Dim des As New DESCryptoServiceProvider()
			Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
			Dim ms As New MemoryStream()
			Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
			cs.Write(inputByteArray, 0, inputByteArray.Length)
			cs.FlushFinalBlock()
			Return Convert.ToBase64String(ms.ToArray())

		Catch ex As Exception
			Return ex.Message
		End Try

	End Function

	'The function used to decrypt the text
	Private Shared Function Decrypt(ByVal strText As String, ByVal sDecrKey As String) As String
		Dim byKey() As Byte = {}
		Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
		Dim inputByteArray(strText.Length) As Byte

		Try
			byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
			Dim des As New DESCryptoServiceProvider()
			inputByteArray = Convert.FromBase64String(strText)
			Dim ms As New MemoryStream()
			Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)

			cs.Write(inputByteArray, 0, inputByteArray.Length)
			cs.FlushFinalBlock()
			Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8

			Return encoding.GetString(ms.ToArray())

		Catch ex As Exception
			Return ex.Message
		End Try

	End Function

	Public Function BuildDate(ByVal AnniversaryDate As Date) As String
		Dim _day As String
		Dim _month As String
		Dim _year As String

		With AnniversaryDate
			_day = CStr(.Day)
			_month = CStr(.Month)
			_year = CStr(.Year)
		End With

		If _day.Length = 1 Then
			_day = "0" + _day
		End If

		If _month.Length = 1 Then
			_month = "0" + _month
		End If

		Return _month + "/" + _day + "/" + _year
	End Function

	Public Function IsUserAllowToSave() As Boolean

		Dim _adapter As New SqlDataAdapter
		Dim _dataset As New DataSet
		Dim _allowToSave As Boolean

		Try
			_sql.Open()
			_adapter = New SqlDataAdapter("SELECT * FROM DbsUser", _sql)
			_adapter.SelectCommand.CommandType = CommandType.Text
			_dataset = New DataSet
			_adapter.Fill(_dataset, "DbsUser")

			For _index As Integer = 0 To _dataset.Tables("DbsUser").Rows.Count - 1
				If System.Environment.MachineName = _dataset.Tables("DbsUser").Rows(_index).Item("MachineName").ToString Then
					_allowToSave = True
					Exit For
				Else
					_allowToSave = False
				End If
			Next

			_sql.Close()
		Catch ex As Exception
			_allowToSave = False
		End Try

		Return _allowToSave
	End Function

End Class
