using System;
using System.Text;
using System.Data;
using System.Web;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;
using log4net;


namespace AllTheTalk.Crawl
{
	public class WebBrowser
	{
		// Logger
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// Global Vars
		private Uri _host;
		private Uri _currentUri;
		private ArrayList _deniedUrls;
		private ArrayList _localUrls;
		private ArrayList _externalUrls;
		private ArrayList _feedUrls;

		// Properties
		public Uri Host
		{
			get { return _host; }
			set { _host = value; }
		}

		// Constructors
		public WebBrowser()
		{
		}

		public WebBrowser(Uri Host)
			: this()
		{
			_host = Host;
		}

		// Methods
		public void Run()
		{
			if (_host == null)
			{
				log.Warn("Host not specified. Exiting.");
				return;
			}

			log.InfoFormat("Begin browsing of Host {0}", _host.AbsoluteUri);

			_localUrls = new ArrayList();
			_externalUrls = new ArrayList();
			_feedUrls = new ArrayList();

			_localUrls.Add(_host);

			LoadDenyList(_host);

			// TODO: Load sitemap xml

			int counter = 0;

			while (counter < _localUrls.Count)
			{
				_currentUri = (Uri)_localUrls[counter++];

				if (!UriDenied(_currentUri))
				{
					ProcessUrl(_currentUri);
				}

				// TODO: Handle sites with infinite pages

				if (Config.ShuttingDown)
				{
					log.WarnFormat("Browsing of Host {0} not completed.", _host.AbsoluteUri);
					break;
				}

				// Browse at a comfortable pace
				System.Threading.Thread.Sleep(Config.CrawlerWaitTime);
			}
			log.InfoFormat("End browsing of Host {0}", _host.AbsoluteUri);
		}

		private Boolean UriDenied(Uri UriToCheck)
		{
			foreach (String deniedUriString in _deniedUrls)
			{
				if (UriToCheck.PathAndQuery.StartsWith(deniedUriString))
				{
					return true;
				}
			}
			return false;
		}

		private void LoadDenyList(Uri StartUri)
		{
			log.DebugFormat("Checking for robot.txt on Host {0}", _host.AbsoluteUri);

			_deniedUrls = new ArrayList();

			// Check for robot.txt file
			HttpWebRequest request = GetWebRequest(new Uri(new Uri(StartUri.GetLeftPart(UriPartial.Authority)), "robots.txt"));
			HttpWebResponse response = null;

			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch
			{
				if (response != null)
					response.Close();
				return;
			}

			if ((response.StatusCode == HttpStatusCode.OK) && (response.ContentType.ToLower() == "text/plain"))
			{
				log.DebugFormat("Found robot.txt on Host {0}", _host);

				Stream rs = response.GetResponseStream();
				StreamReader sr = new StreamReader(rs, true);

				String line;
				Boolean lineIsSpecific = false;
				Boolean lineIsGlobal = false;
				Boolean listIsSpecific = false;

				while ((line = sr.ReadLine()) != null)
				{

					line = line.Trim().ToLower();

					// Skip # commented lines
					if (line.StartsWith("#"))
						continue;

					if (line.StartsWith("user-agent"))
					{

						string agent = (line.Substring(line.IndexOf(":") + 1).Trim());

						if (agent == "*")
						{
							lineIsSpecific = false;
							lineIsGlobal = true;
						}
						else if ((agent == "allthetalk") || (agent == "allthetalk crawler"))
						{
							lineIsSpecific = true;
							lineIsGlobal = false;
							if (!listIsSpecific)
							{
								_deniedUrls.Clear();
								listIsSpecific = true;
							}
						}
						else
						{
							lineIsSpecific = false;
							lineIsGlobal = false;
						}
						continue;
					}
					// TODO: !!! Check Logic
					if ((((!listIsSpecific) && (lineIsGlobal)) || (lineIsSpecific)) && (line.StartsWith("disallow")))
					{
						String item = line.Substring(line.IndexOf(":") + 1).Trim();

						if (String.IsNullOrEmpty(item))
						{
							log.DebugFormat("Found allow line: {0} in robot.txt", line);
							_deniedUrls.Clear();
						}
						else
						{
							log.DebugFormat("Found denied line: {0} in robot.txt", line);
							_deniedUrls.Add(item);
						}
					}
				}
			}
			if (response != null)
				response.Close();
		}

		private HttpWebRequest GetWebRequest(Uri Uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
			request.UserAgent = "AllTheTalk Crawler 1.0 (http://www.allthetalk.com/bots#crawler)";
			request.Headers.Add("From", "crawler_bot(at)allthetalk.com");
			request.Timeout = 22000;
			return request;
		}

		private void ProcessUrl(Uri Uri)
		{
			HttpWebRequest request = GetWebRequest(Uri);
			HttpWebResponse response = null;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch
			{
				if (response != null)
					response.Close();
				return;
			}

			// Ensure we did not get redirected to an external site
			// TODO: Verify that this is actually necessary?
			if (Uri.Host.Equals(response.ResponseUri.Host))
			{
				log.DebugFormat("Browsing {0} {1}.", Uri.AbsoluteUri, response.ResponseUri.Host);

				switch (response.StatusCode)
				{
					case HttpStatusCode.OK:
						ProcessResponse(response);
						break;

					default:
						log.WarnFormat("Browsing {0} with response {1} - not processed", Uri.AbsoluteUri, response.StatusDescription);
						// TODO: Handle other cases - Redirects, Not Found, etc...
						break;
				}
			}
			else
			{
				log.DebugFormat("Browser redirected from {0} to {1}. End page processing", Uri.AbsoluteUri, response.ResponseUri.Host);
				ProcessFoundRemoteUrl(response.ResponseUri);
			}
			if (response != null)
				response.Close();
		}

		private void ProcessResponse(HttpWebResponse WebResponse)
		{

			bool processed = false;

			foreach (String content in WebResponse.ContentType.Split(new String[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
			{

				switch (content.Trim().ToLower())
				{
					// Definitley a feed
					case "application/rss+xml":
					case "application/atom+xml":
					case "application/rdf+xml":
						ProcessFoundFeed(WebResponse.ResponseUri);
						processed = true;
						break;

					// Might be a feed, might be a web page
					case "text/xml":
					case "application/xml":
					case "application/xhtml+xml":

						HtmlDocument Document = GetHtmlDoc(WebResponse);
						if (DocIsFeed(Document))
						{
							ProcessFoundFeed(WebResponse.ResponseUri);
						}
						else
						{
							ProcessHtmlDoc(Document);
						}
						processed = true;
						break;

					// Definitley not a feed, definitley a web page
					case "text/html":
						ProcessHtmlDoc(GetHtmlDoc(WebResponse));
						processed = true;
						break;

					// Neither a feed nor a web page
					default:
						// TODO: Log file extension for future addition to exclusion list
						break;

				}
				if (processed)
					break;
			}
		}

		private Boolean DocIsFeed(HtmlDocument Document)
		{
			if (Document.DocumentNode.HasChildNodes)
			{
				foreach (HtmlNode node in Document.DocumentNode.ChildNodes)
				{
					switch (node.Name.Trim())
					{
						case "rss:rss":
						case "rss":
						case "atom:feed":
						case "atom":
						case "feed":
						case "rdf:rdf":
						case "rdf":
							return true;
					}
				}
			}
			return false;
		}

		private void ProcessHtmlDoc(HtmlDocument Document)
		{
			// Check robots META tag
			HtmlNodeCollection robotNodes = Document.DocumentNode.SelectNodes(
				"//html/head/meta[@name = 'robots'] " +
				"| //html/head/meta[@name = 'allthetalk'] " +
				"| //html/head/meta[@name = 'allthetalk crawler']"
				);
			if (robotNodes != null)
			{				
				foreach (HtmlNode node in robotNodes)
				{
					foreach (String content in node.Attributes["content"].Value.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries))
					{
						if (content == "nofollow")
						{
							log.Debug("Found nofollow robot meta tags");
							return;
						}
					}
				}
			}

			// Find all href="" attrbutes
			log.Debug("Parsing links");
			HtmlNodeCollection hrefNodes = Document.DocumentNode.SelectNodes("//@href");
			if (hrefNodes != null)
			{
				foreach (HtmlNode link in hrefNodes)
				{
					// Check for rel="nofollow" attribute
					HtmlAttribute rel = link.Attributes["rel"];
					if ((rel == null) || (rel.Value != "nofollow"))
					{

						// Process href="" attribute
						HtmlAttribute href = link.Attributes["href"];
						Uri linkUri = new Uri(href.Value, UriKind.RelativeOrAbsolute);
						if (!linkUri.IsAbsoluteUri)
							linkUri = new Uri(new Uri(_currentUri.GetLeftPart(UriPartial.Authority)), linkUri);

						// Pre-check Prefix and Extension
						if (PrefixExcluded(linkUri) || ExtensionExcluded(linkUri))
							continue;

						// Pre-check content type
						HtmlAttribute contentType = link.Attributes["content-type"];
						if (contentType != null)
						{
							switch (contentType.Value)
							{
								case "application/rss+xml":
								case "application/atom+xml":
								case "application/rdf+xml":
									// This is a feed - add to DB go to the next link
									ProcessFoundFeed(linkUri);
									continue;

								case "text/xml":
								case "text/html":
								case "application/xml":
								case "application/xhtml+xml":
									// This could be a feed or a webpage
									break;

								default:
									// Target is not readable - go to next link
									continue;
							}
						}

						// Check if url is local or remote
						if (_currentUri.GetLeftPart(UriPartial.Authority).Equals(linkUri.GetLeftPart(UriPartial.Authority)))
						{
							// Local Url
							ProcessFoundLocalUrl(linkUri);
						}
						else
						{
							// Remote Url
							ProcessFoundRemoteUrl(linkUri);
						}
					}
				}
			}
		}

		private HtmlDocument GetHtmlDoc(HttpWebResponse WebResponse)
		{
			HtmlDocument html = new HtmlDocument();
			StreamReader sr = new StreamReader(WebResponse.GetResponseStream(), true);
			html.LoadHtml(sr.ReadToEnd().ToLower()); // Convert the entire doc to Lower Case for simpler processing.
			WebResponse.Close();
			return html;
		}

		private void ProcessFoundFeed(Uri FeedUrl)
		{
			if (!_feedUrls.Contains(FeedUrl))
			{
				log.DebugFormat("Found feed {0}", FeedUrl.AbsoluteUri);
				_feedUrls.Add(FeedUrl);
				Queues.AddFeed(FeedUrl);
			}
		}

		private void ProcessFoundLocalUrl(Uri LocalUrl)
		{
			if (!_localUrls.Contains(LocalUrl))
			{
				log.DebugFormat("Found local Url {0}", LocalUrl.AbsoluteUri);
				_localUrls.Add(LocalUrl);
				Queues.AddLocalUrl(LocalUrl);
			}
		}

		private void ProcessFoundRemoteUrl(Uri RemoteUrl)
		{
			if (!_externalUrls.Contains(RemoteUrl))
			{
				log.DebugFormat("Found remote Url {0}", RemoteUrl.AbsoluteUri);
				_externalUrls.Add(RemoteUrl);
				Queues.AddRemoteUrl(RemoteUrl);
			}
		}

		private Boolean ExtensionExcluded(Uri TargetUri)
		{
			String thisExt = null;
			String thisPath = TargetUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			thisPath = thisPath.Substring(thisPath.LastIndexOf("/") + 1);

			int i = thisPath.LastIndexOf(".");
			if (i >= 0)
			{
				thisExt = thisPath.Substring(i + 1);
			}
			else
			{
				// There is no extension
				return false;
			}

			// Check the include list first
			foreach (String ext in Config.CrawlIncludeExts.Split(new char[] { ',' }))
			{
				if (ext.Trim().ToLower() == thisExt)
					return false;
			}

			// Check the exclude list second
			foreach (String ext in Config.CrawlExcludeExts.Split(new char[] { ',' }))
			{
				if (ext.Trim().ToLower() == thisExt)
					return true;
			}
			return false;
		}

		private Boolean PrefixExcluded(Uri TargetUri)
		{
			String thisPrefix = TargetUri.GetComponents(UriComponents.Scheme, UriFormat.Unescaped);

			// Check the include list first
			foreach (String pre in Config.CrawlIncludePrefix.Split(new char[] { ',' }))
			{
				if (pre.Trim().ToLower() == thisPrefix)
					return false;
			}

			// Check the exclude list second
			foreach (String pre in Config.CrawlExcludePrefix.Split(new char[] { ',' }))
			{
				if (pre.Trim().ToLower() == thisPrefix)
					return true;
			}
			return false;
		}
	}
}

