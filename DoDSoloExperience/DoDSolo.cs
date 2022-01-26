using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace DoDSoloExperience
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDSolo : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDSoloExperience";

		public const string PluginName = "DoOrDieSoloExperience";

		public const string PluginVersion = "1.2.11";

		private Harmony _harmony;

		public ConfigEntry<bool> DoDMessageEnable;
        public ConfigEntry<bool> DoDAltarMO;
		public ConfigEntry<bool> JVLDragon;
		public AssetBundle DoDSoloAssets;
		public AssetBundle JVL_Dragon;
		public GameObject lulzilla;
		public GameObject attack1;
		public GameObject attack2;

		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			DoDAltarMO = base.Config.Bind("Magic Overhaul", "Enable", defaultValue: true, new ConfigDescription("Enables the Magic Overhaul Altar at the Trophy Ring", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DoDMessageEnable = base.Config.Bind("Welcome Location", "Enable", defaultValue: true, new ConfigDescription("Enables the Do or Die welcome location near the Trophy Ring", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			JVLDragon = base.Config.Bind("Lulzilla", "Enable", defaultValue: true, new ConfigDescription("Rawr!", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		public void LoadBundle()
		{
			//DoDSoloAssets = AssetUtils.LoadAssetBundleFromResources("doordiesolo", Assembly.GetExecutingAssembly());
			JVL_Dragon = AssetUtils.LoadAssetBundleFromResources("jvldragon", Assembly.GetExecutingAssembly());
		}
		private void Awake()
		{
			CreateConfigurationValues();
			LoadBundle();
			AddLulzilla();
			ZoneManager.OnVanillaLocationsAvailable += AddSELocations;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDSoloExperience");
		}
		private void AddSELocations()
		{
			DoDSoloAssets = AssetUtils.LoadAssetBundleFromResources("doordiesolo", Assembly.GetExecutingAssembly());
			try
			{
				if (DoDMessageEnable.Value == true)
				{
					var Welcome = ZoneManager.Instance.CreateLocationContainer(DoDSoloAssets.LoadAsset<GameObject>("Welcome_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Welcome, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 1,
						Priotized = true,
						ExteriorRadius = 5f,
						MinAltitude = 0.5f,
						ClearArea = true,
						MaxDistance = 100f,
					}));
				}
				if (DoDAltarMO.Value == true)
				{
					var Welcome = ZoneManager.Instance.CreateLocationContainer(DoDSoloAssets.LoadAsset<GameObject>("MagicAltar_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Welcome, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 1,
						Priotized = true,
						ExteriorRadius = 4f,
						MinAltitude = 0.5f,
						ClearArea = true,
						MaxDistance = 100f,
					}));
					var dodaltarmo = PrefabManager.Instance.GetPrefab("AltarPrefab");
					var altarMO = Instantiate(dodaltarmo, Welcome.transform);
					altarMO.transform.localPosition = new Vector3(0f, 0.075f, 0f);
					altarMO.transform.localScale = new Vector3(0.75f, 0.5f, 0.75f);
				}
			}
            finally
			{
				ZoneManager.OnVanillaLocationsAvailable -= AddSELocations;
				DoDSoloAssets.Unload(false);
			}
		}
		private void AddLulzilla()
		{
			lulzilla = JVL_Dragon.LoadAsset<GameObject>("JVL_Dragon");
			attack1 = JVL_Dragon.LoadAsset<GameObject>("JVL_Bite_lulz");
			attack2 = JVL_Dragon.LoadAsset<GameObject>("JVL_Charge_lulz");
			GameObject monster1 = lulzilla;
			CustomPrefab creature1 = new CustomPrefab(monster1, true);
			PrefabManager.Instance.AddPrefab(creature1);
			GameObject monsterability1 = attack1;
			CustomItem customItem1 = new CustomItem(monsterability1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject monsterability2 = attack2;
			CustomItem customItem2 = new CustomItem(monsterability2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			JVL_Dragon.Unload(false);
		}
	}
}
