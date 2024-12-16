using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using Rss;

namespace AllTheTalk.Feeder
{
	class FeedRetriever
	{
		private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		// Global Vars
		private FeedToProcess _feed;

		// Properties
		public FeedToProcess Feed
		{
			get { return _feed; }
			set { _feed = value; }
		}

		// Constructors
		public FeedRetriever()
		{
		}

		public FeedRetriever(FeedToProcess Feed)
			: this()
		{
			_feed = Feed;
		}

		// Methods
		public void Run()
		{
			if (_feed == null)
			{
				log.Warn("Feed not specified. Exiting.");
				throw new ArgumentNullException("Feed", "Feed not specified");
				// return;
			}

			// Check the Feed Type
			switch (_feed.FeedType)
			{
				case FeedType.RSS:
					ProcessRssFeed(_feed.FeedUrl);
					break;
				case FeedType.Atom:
					ProcessAtomFeed(_feed.FeedUrl);
					break;
			}
		}

		private void ProcessRssFeed(Uri FeedUrl)
		{
			log.InfoFormat("Begin retrieving RSS Feed from {0}", FeedUrl.AbsoluteUri);

			// Download the Feed
			RssFeed feed = RssFeed.Read(GetWebRequest(FeedUrl.AbsoluteUri));

			foreach (RssChannel channel in feed.Channels)
			{
		
				// TODO: Enable Cloud Protocol
			
				if (Data.ChannelWasUpdated(channel))
				{
					// Ensure data in DB is up to date.
					Data.UpsertChannel(channel);

					// FIXME: Handle skipHours, skipDays, and ttl

					foreach (RssItem item in channel.Items)
					{
						if (!Data.ItemExists(item))
						{
							Data.InsertItem(item);
						}
						else if (Data.ItemIsOutDated(item))
						{
							Data.UpdateItem(item);
						}
					}
				}
			}
		}

		private void ProcessAtomFeed(Uri FeedUrl)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		private HttpWebRequest GetWebRequest(Uri Uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Uri);
			request.UserAgent = "AllTheTalk Crawler 1.0 (http://www.allthetalk.com/bots#feeder)";
			request.Headers.Add("From", "feeder_bot(at)allthetalk.com");
			request.Timeout = 22000;
			return request;
		}
	}
}
