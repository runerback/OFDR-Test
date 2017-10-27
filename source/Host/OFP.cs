using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFDR.Host
{
	public class OFP
	{
		internal OFP() { }

		private DateTime startDT = DateTime.Now;

		public void displaySystemMessage(object obj)
		{
			if (obj != null)
				Console.WriteLine(obj);
		}

		public double getMissionTime()
		{
			return (DateTime.Now - startDT).TotalMilliseconds;
		}

		#region mission controll

		public void missionCompleted()
		{
			raiseMissionEnding(Common.MissionEndingType.Completed);
		}

		public void missionFailed()
		{
			raiseMissionEnding(Common.MissionEndingType.Failed);
		}

		public void missionFailedKIA()
		{
			raiseMissionEnding(Common.MissionEndingType.KIA);
		}

		public void missionFailedMIA()
		{
			raiseMissionEnding(Common.MissionEndingType.MIA);
		}

		public event EventHandler<Common.EventArgs<Common.MissionEndingType>> MissionEnding;
		private void raiseMissionEnding(Common.MissionEndingType type)
		{
			if (MissionEnding != null)
				MissionEnding(this, new Common.EventArgs<Common.MissionEndingType>(type));
		}

		#endregion mission controll

		
	}
}
