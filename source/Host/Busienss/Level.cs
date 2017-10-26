using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Host
{
	public static class Level
	{
		private static LuaVM vm = new LuaVM();

		public static void onCreate()
		{
			vm.Call("onCreate");
		}

		public static void onMissionStart()
		{
			vm.Call("onMissionStart");
		}

		public static void Dispose()
		{
			vm.Dispose();
		}
	}
}
