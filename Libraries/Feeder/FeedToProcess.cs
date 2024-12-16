using System;
using System.Collections.Generic;
using System.Text;

namespace AllTheTalk.Feeder
{
	internal class FeedToProcess
	{
		private Uri _feedUrl;
		private FeedType _feedType;
		// private int _feedId;

		internal Uri FeedUrl
		{
			get { return _feedUrl; }
			set { _feedUrl = value; }
		}

		internal FeedType FeedType
		{
			get { return _feedType; }
			set { _feedType = value; }
		}

		internal FeedToProcess(Uri FeedUrl, FeedType FeedType)
		{
			_feedUrl = FeedUrl;
			_feedType = FeedType;
		}
	}
}
