using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
	public class OFP
	{
		public OFP() { }

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
	}
}
