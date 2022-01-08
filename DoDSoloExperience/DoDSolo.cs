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

		public const string PluginVersion = "1.2.5";

		public ConfigEntry<bool> DoDMessageEnable;
        public ConfigEntry<bool> DoDAltarMO;
		public AssetBundle DoDSoloAssets;

		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void LoadBundle()
		{
			DoDSoloAssets = AssetUtils.LoadAssetBundleFromResources("doordiesolo", Assembly.GetExecutingAssembly());
		}
		private void Awake()
		{
			CreateConfigurationValues();
			LoadAssets();
			ZoneManager.OnVanillaLocationsAvailable += EditStartTemple;
		}
		public void CreateConfigurationValues()
		{
			DoDAltarMO = base.Config.Bind("Magic Overhaul", "Enable", defaultValue: true, new ConfigDescription("Enables the Magic Overhaul Altar at the Trophy Ring", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DoDMessageEnable = base.Config.Bind("Start Message", "Enable", defaultValue: true, new ConfigDescription("Enables the Do or Die info book at the Trophy Ring", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void LoadAssets()
        {

			GameObject StartStone = DoDSoloAssets.LoadAsset<GameObject>("StartStone_DoD");
			PrefabManager.Instance.AddPrefab(StartStone);
		}
		private void EditStartTemple()
		{
			try
			{
				if (DoDMessageEnable.Value == true)
				{
					var startLocation = ZoneManager.Instance.GetZoneLocation("StartTemple");
					var dodMessage = PrefabManager.Instance.GetPrefab("StartStone_DoD");
					var startMessage = Instantiate(dodMessage, startLocation.m_prefab.transform);
					startMessage.transform.localPosition = new Vector3(-8.79f, -0.05f, -3.35f);
				}
				if (DoDAltarMO.Value == true)
				{
					var startLocation = ZoneManager.Instance.GetZoneLocation("StartTemple");
					var dodaltarmo = PrefabManager.Instance.GetPrefab("AltarPrefab");
					Destroy(dodaltarmo.GetComponent(typeof(ZNetView)));
					var altarMO = Instantiate(dodaltarmo, startLocation.m_prefab.transform);
					altarMO.transform.localPosition = new Vector3(0f, -0.05f, 0f);
					altarMO.transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
				}
			}
			finally
			{
				ZoneManager.OnVanillaLocationsAvailable -= EditStartTemple;
			}
		}
	}
}
