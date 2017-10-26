using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OFDR.Business
{
	public class Scheduler
	{
		public Scheduler()
		{
			Host.Level.onCreate();
			creationTask().Wait();
			Host.Level.onMissionStart();
		}

		/// <summary>
		/// load game resources
		/// </summary>
		public Task creationTask()
		{
			return Task.Factory.StartNew(() =>
			{
				Thread.Sleep(3600);
			});
		}
	}
}
