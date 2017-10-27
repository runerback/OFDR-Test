using System;

namespace OFDR
{
	class Program
	{
		static void Main()
		{
			try
			{
				var scheduler = new Business.Scheduler();
				scheduler.Exited += delegate
				{
					Console.WriteLine("\n----------------------done----------------------");
				};
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e);
			}
			Console.ReadLine();
		}
	}
}
