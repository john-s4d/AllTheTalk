Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace AllTheTalk.Controls

    <ToolboxData("<{0}:Advertisement runat=server></{0}:Advertisement>")> _
    Public Class Advertisement
        Inherits System.Web.UI.WebControls.AdRotator

        Private myKeywords As String
        Private mySize As AdSize
        Private myType As AdType
        Private myAdProvider As AdProvider

        Protected Sub Me_AdCreated(ByVal sender As Object, ByVal e As AdCreatedEventArgs) Handles Me.AdCreated

            'TODO: Get appropriate Ad from Local Database or Ad Provider

            'Populate Ad details
            e.ImageUrl = "images/Defaul1.jpg"
            e.NavigateUrl = ""
            e.AlternateText = ""

            'TODO: Use a redirector to track clicks

        End Sub

        Private Sub setSize()
            Select Case mySize
                Case AdSize.LeaderBoard_728x90
                    Me.Width = 728
                    Me.Height = 90
                Case AdSize.Banner_468x60
                    Me.Width = 468
                    Me.Height = 60
                Case AdSize.HalfBanner_234x60
                    Me.Width = 234
                    Me.Height = 60
                Case AdSize.Button_125x125
                    Me.Width = 125
                    Me.Height = 125
                Case AdSize.Skyscraper_120x600
                    Me.Width = 120
                    Me.Height = 600
                Case AdSize.WideSkyscraper_160x600
                    Me.Width = 160
                    Me.Height = 600
                Case AdSize.SmallRectangle_180x150
                    Me.Width = 180
                    Me.Height = 150
                Case AdSize.VerticalBanner_120x240
                    Me.Width = 120
                    Me.Height = 240
                Case AdSize.MediumRectangle_300x250
                    Me.Width = 300
                    Me.Height = 250
                Case AdSize.Square_250x250
                    Me.Width = 250
                    Me.Height = 250
                Case AdSize.LargeRectangle_336x280
                    Me.Width = 336
                    Me.Height = 280
                Case AdSize.SmallSquare_200x200
                    Me.Width = 200
                    Me.Height = 200
                Case AdSize.Rectangle_300x250
                    Me.Width = 300
                    Me.Height = 250
            End Select
        End Sub

        <Bindable(True), Category("Behavior"), DefaultValue(""), Localizable(True)> _
        Public Property Keywords() As String
            Get
                Return myKeywords
            End Get
            Set(ByVal value As String)
                myKeywords = value
            End Set
        End Property

        <Category("Layout"), DefaultValue(""), Localizable(True)> _
        Public Property Size() As AdSize
            Get
                Return mySize
            End Get
            Set(ByVal value As AdSize)
                mySize = value
                setSize()
            End Set
        End Property

        <Category("Behavior"), DefaultValue(""), Localizable(True)> _
        Public Property Type() As AdType
            Get
                Return myType
            End Get
            Set(ByVal value As AdType)
                myType = value
            End Set
        End Property

        <Category("Behavior"), DefaultValue(""), Localizable(True)> _
        Public Property Provider() As AdProvider
            Get
                Return myAdProvider
            End Get
            Set(ByVal value As AdProvider)
                myAdProvider = value
            End Set
        End Property

        <Browsable(False)> _
        Public Shadows Property KeywordFilter() As String
            Get
                Return Nothing
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        <Browsable(False)> _
        Public Shadows Property ImageUrlField() As String
            Get
                Return Nothing
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        <Browsable(False)> _
        Public Shadows Property NavigateUrlField() As String
            Get
                Return Nothing
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        <Browsable(False)> _
        Public Shadows Property AdvertisementFile() As String
            Get
                Return Nothing
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        <Browsable(False)> _
    Public Shadows Property AlternateTextField() As String
            Get
                Return Nothing
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Enum AdSize As Integer
            LeaderBoard_728x90 = 1
            Banner_468x60 = 2
            HalfBanner_234x60 = 3
            Button_125x125 = 4
            Skyscraper_120x600 = 5
            WideSkyscraper_160x600 = 6
            SmallRectangle_180x150 = 7
            VerticalBanner_120x240 = 8
            MediumRectangle_300x250 = 9
            Square_250x250 = 10
            LargeRectangle_336x280 = 11
            SmallSquare_200x200 = 12
            Rectangle_300x250 = 13
        End Enum

        Public Enum AdType As Integer
            Text = 1
            Image = 2
            Video = 3
        End Enum

        Public Enum AdProvider As Integer
            Local = 1
            GoogleAdSense = 2
        End Enum

    End Class
End Namespace


