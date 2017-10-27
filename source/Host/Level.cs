using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFDR.Host
{
	public static class Level
	{
		private static OFP _OFP = new OFP();
		public static OFP OFP
		{
			get { return _OFP; }
		}

		private static LuaVM vm;

		static Level()
		{
			vm = new LuaVM(_OFP);
		}

		


		#region Mission Time

		public static void OnCreate()
		{
			vm.Call("onCreate");
		}

		public static void OnMissionStart()
		{
			vm.Call("onMissionStart", _OFP.getMissionTime());
		}

		public static void UpdateFrame(double currentTime, int frameNo)
		{
			vm.Call("updateFrame", currentTime, frameNo);
		}

		#endregion Mission Time

		#region Objective Events

		public static void OnObjectiveCompleted(string objectiveName)
		{
			vm.Call("onObjectiveCompleted", objectiveName);
		}

		public static void OnObjectiveFailed(string objectiveName)
		{
			vm.Call("onObjectiveFailed", objectiveName);
		}

		public static void OnObjectiveVisible(string objectiveName)
		{
			vm.Call("onObjectiveVisible", objectiveName);
		}

		#endregion Objective Events

		#region Spawn Callbacks

		public static void OnDespawnEntity(string entityName)
		{
			vm.Call("onDespawnEntity", entityName);
		}

		public static void OnDespawnEntitySet(int setID)
		{
			vm.Call("onDespawnEntitySet", setID);
		}

		public static void OnRespawn(string entityName)
		{
			vm.Call("onRespawn", entityName);
		}

		public static void OnSmartSpawned(string spawnPoint)
		{
			vm.Call("onSmartSpawned", spawnPoint);
		}

		public static void OnSpawnedReady(string setName, int setID, string[] tableOfEntities, int errorCode)
		{
			vm.Call("onSpawnedReady", setName, setID, tableOfEntities, errorCode);
		}

		#endregion Spawn Callbacks

		#region Speech

		public static void OnSpeechEnd(string soldier, string sentence, int handle)
		{
			vm.Call("onSpeechEnd", soldier, sentence, handle);
		}

		#endregion Speech

		#region Suppression Events

		public static void OnPinned(string entityID)
		{
			vm.Call("onPinned", entityID);
		}

		public static void OnSuppressed(string entityID)
		{
			vm.Call("onSuppressed", entityID);
		}

		public static void OnUnsuppressed(string entityID)
		{
			vm.Call("onUnsuppressed", entityID);
		}

		#endregion Suppression Events

		#region Suspected Events

		public static void OnSuspected(string victim, string suspector)
		{
			vm.Call("onSuspected", victim, suspector);
		}

		#endregion Suspected Events


		

		public static void Dispose()
		{
			vm.Dispose();
		}
	}
}
