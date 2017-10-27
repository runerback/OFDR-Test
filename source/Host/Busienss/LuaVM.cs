using LuaInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

			state.NewTable("scripts");
			state.NewTable("scripts.mission");

			state.DoString(_call);
			this.call = state.GetFunction("call");

			loadScripts();

			//check module states
			if (System.Diagnostics.Debugger.IsAttached)
			{
				state.DoString(@"
OFP:displaySystemMessage(""listing functions..."")
for k, v in pairs(scripts.mission) do
OFP:displaySystemMessage(""\t""..k)
for k1, _ in pairs(v) do
OFP:displaySystemMessage(""\t\t""..k1)
end
end
");
			}
		}

		private static readonly string _call = @"function call(name, ...)
--OFP:displaySystemMessage(""calling ""..name)
for k, v in pairs(scripts.mission) do
--OFP:displaySystemMessage(""searching in ""..k)
if v[name] then
--OFP:displaySystemMessage(""found in ""..k)
v[name](...)
else
--OFP:displaySystemMessage(""not found"")
end
end
end";
		private LuaFunction call;

		/// <summary>
		/// load all lua scripts in "scripts" folder. the order is: waypoints, level, others
		/// </summary>
		private void loadScripts()
		{
			var scriptFiles = Directory.GetFiles("scripts", "*.lua");
			var scripts = new Dictionary<string, string>(scriptFiles.Length);
			scripts.Add("waypoints", null);
			scripts.Add("level", null);
			foreach (var file in scriptFiles)
			{
				string content = File.ReadAllText(file);
				string scriptName = Path.GetFileNameWithoutExtension(file);

				if (scriptName == "waypoints")
					scripts[scriptName] = content;
				else if (scriptName == "level")
					scripts[scriptName] = content;
				else
					scripts.Add(scriptName, content);
			}

			var global_state = this.global_state;
			var regex = new Regex(@"function\s+(?<func>\w+)\s*|(?<func>\w+)\s*\=\s*function");
			foreach (var script in scripts)
			{
				string scriptName = script.Key;
				string scriptContent = script.Value;

				StringBuilder builder = new StringBuilder();
				builder.AppendFormat("{0} = {{}}\n", scriptName);

				//pack scripts
				int index = 0;
				var matches = regex.Matches(scriptContent);
				foreach (var group in matches
					.Cast<Match>()
					.Select(match => match.Groups["func"])
					.Where(group => group.Success))
				{
					builder.Append(scriptContent.Substring(index, group.Index - index));
					builder.AppendFormat("{0}.", scriptName);
					builder.Append(group.Value);
					index = group.Index + group.Length;
				}
				builder.Append(scriptContent.Substring(index));
				string packableScript = builder.ToString();

				global_state.DoString(packableScript);
				global_state.DoString(string.Format(@"scripts.mission[""{0}""] = {0}", scriptName));
			}
		}

		/// <summary>
		/// Call build-in callback functions in level and waypoints
		/// </summary>
		internal void Call(string functionName, params object[] args)
		{
			this.call.Call(functionName, args);
		}

		public void Dispose()
		{
			this.global_state.Dispose();
		}
	}
}
