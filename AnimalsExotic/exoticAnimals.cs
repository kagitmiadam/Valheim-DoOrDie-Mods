using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace ExoticAnimals
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    public class exoticAnimals
	{
        public const string PluginGUID = "horemvore.ExoticAnimals";

        public const string PluginName = "ExoticAnimals";

        public const string PluginVersion = "0.0.1";

        public static GameObject SnappingTurtle;
        public static GameObject Zebra;
        public static GameObject ComodoDragon;
        public static GameObject Camel;

        public AssetBundle ExoticBundle;
        //private Harmony _harmony;

		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		private void Awake()
		{
			LoadBundle();
			LoadAssets();
			AddNewAnimals();
			UnloadBundle();
			//_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.ExoticAnimals");
		}
		public void LoadBundle()
		{
			ExoticBundle = AssetUtils.LoadAssetBundleFromResources("africa", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			//Debug.Log("Wildlife: 0");
			SnappingTurtle = ExoticBundle.LoadAsset<GameObject>("Animal_SnappingTurtle"); 
			Zebra = ExoticBundle.LoadAsset<GameObject>("Animal_Zebra");
			ComodoDragon = ExoticBundle.LoadAsset<GameObject>("Animal_ComodoDragon");
			Camel = ExoticBundle.LoadAsset<GameObject>("Animal_Camel");
			//Debug.Log("Wildlife: 0");
			/*GameObject VFXGetHit = ExoticBundle.LoadAsset<GameObject>("VFX_Blood_Hit_WL");
			GameObject VFXDeath = ExoticBundle.LoadAsset<GameObject>("VFX_Animal_Death_WL");
			GameObject VFXPoof = ExoticBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_WL");
			PrefabManager.Instance.AddPrefab(VFXGetHit);
			PrefabManager.Instance.AddPrefab(VFXDeath);
			PrefabManager.Instance.AddPrefab(VFXPoof);*/
		}
		private void AddNewAnimals()
		{
			GameObject animal4 = Camel;
			CustomPrefab critter4 = new CustomPrefab(animal4, true);
			PrefabManager.Instance.AddPrefab(critter4);
			GameObject animal3 = ComodoDragon;
			CustomPrefab critter3 = new CustomPrefab(animal3, true);
			PrefabManager.Instance.AddPrefab(critter3);
			GameObject animal2 = Zebra;
			CustomPrefab critter2 = new CustomPrefab(animal2, true);
			PrefabManager.Instance.AddPrefab(critter2);
			GameObject animal1 = SnappingTurtle;
			CustomPrefab critter1 = new CustomPrefab(animal1, true);
			PrefabManager.Instance.AddPrefab(critter1);
		}
		private void UnloadBundle()
		{
			ExoticBundle?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
