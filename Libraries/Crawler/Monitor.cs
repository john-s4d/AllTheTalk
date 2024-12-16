using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AllTheTalk.Crawl {

	public class Monitor {
				
	Monitor() {

		}
		
		private static void MonitorUrlQueue() {

			while (!Config.ShuttingDown) {

				if (Data.UrlQueueCount <= Config.UrlQueueReload) {
					
					Data.ReloadQueue();

				}
			}
		}
	}
}


