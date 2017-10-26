using LuaInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Host
{
	public class LuaVM : IDisposable
	{
		private Lua global_state;

		public LuaVM()
		{
			var state = new Lua();
			state["OFP"] = new OFP();
			this.global_state = state;

			state["scripts"] = new scripts();
			state.NewTable("scripts_mission");
			scripts.mission = state.GetTable("scripts_mission");

			loadScripts();

			//create scripts.mission in sub Lua states, and copy data from global state
			foreach (DictionaryEntry pair0 in scripts.mission)
			{
				var subState = pair0.Value as Lua;
				subState.NewTable("scripts");
				subState.NewTable("scripts.mission");
				var mission = subState.GetTable("scripts.mission");
				foreach (DictionaryEntry pair1 in scripts.mission)
				{
					mission[pair1.Key] = pair1.Value;
				}
			}
		}

		/// <summary>
		/// load all lua scripts in "scripts" folder
		/// </summary>
		private void loadScripts()
		{
			var global_state = this.global_state;

			foreach (string file in Directory.GetFiles("scripts", "*.lua"))
			{
				string scriptName = Path.GetFileNameWithoutExtension(file);

				Lua state = new Lua();
				state.DoFile(file);

				state["OFP"] = global_state["OFP"]; //transfer OFP build-in function object
				scripts.mission[scriptName] = state; //add current script to scripts.mission
			}
		}

		/// <summary>
		/// Call build-in callback functions in level and waypoints
		/// </summary>
		internal void Call(string functionName, params object[] args)
		{
			foreach (DictionaryEntry script in scripts.mission)
			{
				string scriptName = (string)script.Key;
				if (scriptName == "level" || scriptName == "waypoints")
				{
					Console.WriteLine("calling function \"{0}\" in script \"{1}\"", functionName, scriptName);

					var state = script.Value as Lua;

					var func = state.GetFunction(functionName);
					if (func != null) 
						func.Call(args);
				}
			}
		}

		public void Dispose()
		{
			scripts.mission.Dispose();
			this.global_state.Dispose();
		}
	}
}
