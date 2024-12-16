Imports System.Data
Imports System.Data.SqlClient

Partial Class _Default
    Inherits System.Web.UI.Page

    Private category As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TODO: Use TableAdapter to get as much data per request as possible.
        'TODO: Ajaxify

        If Not Page.IsPostBack Then
            category = Me.Request.QueryString.Item("category")
            dlCategory.DataSource = BuildCategoryList()
            dlCategory.DataBind()
        End If

        If Page.IsCrossPagePostBack Then

        End If

    End Sub

    Function BuildCategoryList() As DataTable

        Dim sqlConnection As SqlConnection = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("AllTheTalk").ConnectionString)
        Dim queryString As String = "SELECT [Name] FROM [Categories] ORDER BY [Rating] DESC, [Name]"
        Dim sqlCommand As SqlCommand = New SqlCommand(queryString, sqlConnection)
        Dim sqlAdapter As SqlDataAdapter = New SqlDataAdapter(sqlCommand)

        Dim dt As New DataTable()
        sqlAdapter.Fill(dt)

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i)("Name") = Me.category Then
                dlCategory.SelectedIndex = i
                Exit For
            End If
        Next

        Return dt
    End Function

    Private Sub Link(ByVal linkId As String)

        'Show appropriate page

    End Sub

    Private Sub Article(ByVal articleId As String)

        'Use Article Layout
        'Show appropriate article
        'Set Category for Nav Controls
        'Set Keywords for Ad Controls
        'Track Article Access

    End Sub


End Class