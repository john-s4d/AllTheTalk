using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AllTheTalk.Crawl {

	public class ThreadManager {

		private static Thread _webProcessWorker = null;
		private static Thread _urlQueueWorker = null;
		private static Stack<Thread> _webProcessWorkers = null;

		ThreadManager() {

		}

		public static void StartUrlQueueMonitor() {

			_urlQueueWorker = new Thread(new ThreadStart(Data.MonitorUrlQueue));
			_urlQueueWorker.Start();

		}

		public static void StartNewWorkerThread() {

			Thread webProcessWorker = new Thread(new ThreadStart(Web.ProcessUrls));
			_webProcessWorkers.Push(webProcessWorker);
			webProcessWorker.Start();

		}
	}
}

