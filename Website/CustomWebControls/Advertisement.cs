using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AllTheTalk.Controls
{
    // [DefaultProperty("Text")]
    [ToolboxData("<{0}:Advertisement runat=server></{0}:Advertisement>")]
    public class Advertisement : WebControl
    {
        #region Global Declarations
        
        private AdProvider _provider;
        private AdType _displayType;
        private AdSize _displaySize;
        private int _width;
        private int _height;
        private Uri _navigateUrl;
        private Uri _imageUrl;
        private string _alternateText;
        private string[] _keywords;
        
        public event clickEventHandler click;
        public delegate void clickEventHandler(object sender, EventArgs e);

        #endregion

        #region Public Properties

        [Browsable(true)]
        public AdProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        [Browsable(true)]
        public AdType DisplayType
        {
            get { return _displayType; }
            set { _displayType = value; }
        }

        [Browsable(true)]
        public AdSize DisplaySize
        {
            get { return _displaySize; }
            set { _displaySize = value; }
        }

        [Browsable(true)]
        public string[] Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        #endregion

        #region Methods

        public Advertisement()
        {
            // TODO: Constructor Logic Here
            click += new clickEventHandler(Advertisement_click);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write("");
            
            //<a href=""><img alt="" src="" border="0"></a>
            // do a postback -> redirect for tracking - Not sure how to render a postback.
        }

        #endregion

        #region Event Handlers

        void Advertisement_click(object sender, EventArgs e)
        {
            //TODO: Log Click Event in DB
            HttpContext.Current.Response.Redirect(_navigateUrl.ToString(), true);
        }


       
        #endregion

        #region Enums

        public enum AdType { Text, Image, Video }
        public enum AdProvider { Auto, Local, Google }
        public enum AdSize
        {
            LeaderBoard_728x90,
            Banner_468x60,
            HalfBanner_234x60,
            Button_125x125,
            Skyscraper_120x600,
            WideSkyscraper_160x600,
            SmallRectangle_180x150,
            VerticalBanner_120x240,
            MediumRectangle_300x250,
            Square_250x250,
            LargeRectangle_336x280,
            SmallSquare_200x200,
            Rectangle_300x250
        }
        #endregion

    }
}
