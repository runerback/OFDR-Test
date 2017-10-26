using Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFDR
{
	class Program
	{
		static void Main()
		{
			try
			{
				var scheduler = new Business.Scheduler();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e);
			}
			finally
			{
				Level.Dispose();
			}

			Console.WriteLine("\n----------------------done----------------------");
			Console.ReadLine();
		}
	}
}
