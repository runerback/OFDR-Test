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
			Host.Level.OFP.MissionEnding += onMissionEnding;

			Host.Level.OnCreate();
			loading().ContinueWith(t =>
			{
				Host.Level.OnMissionStart();
			});
		}

		/// <summary>
		/// load game resources
		/// </summary>
		public Task loading()
		{
			return Task.Factory.StartNew(() =>
			{
				Thread.Sleep(3600);
			});
		}

		#region mission end

		private void onMissionEnding(object sender, Common.EventArgs<Common.MissionEndingType> e)
		{
			Console.WriteLine(e.Value);

			Host.Level.OFP.MissionEnding -= onMissionEnding;
			Host.Level.Dispose();
			raiseExited();
		}

		public event EventHandler Exited;
		private void raiseExited()
		{
			if (Exited != null)
				Exited(this, EventArgs.Empty);
		}

		#endregion mission end

		
	}
}
