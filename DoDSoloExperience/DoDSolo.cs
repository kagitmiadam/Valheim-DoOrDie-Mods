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
using SpawnThat.Integrations.CLLC.Models;
using SpawnThat.Integrations.EpicLoot.Models;
using SpawnThat.Options.Conditions;
using SpawnThat.Spawners;
using SpawnThat.Spawners.LocalSpawner;
using SpawnThat.Spawners.WorldSpawner;

namespace DoDSoloExperience
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDSolo : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDSoloExperience";

		public const string PluginName = "DoOrDieSoloExperience";

		public const string PluginVersion = "2.0.7";

		public static bool isModded = true;

		private Harmony _harmony;

		public ConfigEntry<bool> DoDMessageEnable;
        public ConfigEntry<bool> DoDAltarMO;
		public AssetBundle DoDSoloAssets;
		internal static ManualLogSource Log;

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
		}
		public void LoadBundle()
		{
			//DoDSoloAssets = AssetUtils.LoadAssetBundleFromResources("doordiesolo", Assembly.GetExecutingAssembly());
		}
		private void Awake()
		{
			try
			{
				Log = Logger;
				CreateConfigurationValues();
				ZoneManager.OnVanillaLocationsAvailable += AddSELocations;
				//SpawnerConfigurationManager.OnConfigure += ConfigureSpawners;
				_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDSoloExperience"); 
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
			}
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
						MinAltitude = 0.1f,
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
						MinAltitude = 0.1f,
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
				//ZoneManager.OnVanillaLocationsAvailable -= AddSELocations;
				DoDSoloAssets.Unload(false);
			}
		}
		public static void ConfigureSpawners(ISpawnerConfigurationCollection config)
		{
			try
			{
				ConfigureLocalSpawnerByNamed(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureLocalSpawnerByNamed(ISpawnerConfigurationCollection config)
		{
			try
			{
				LocalSpawnSettings minibossAshLandsDay = new()
				{
					PrefabName = "RRRN_AshVexx_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossAshLandsNight = new()
				{
					PrefabName = "RRRN_CinderMortem_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossDeepNorthDay = new()
				{
					PrefabName = "RRRN_LincolnHunt_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossDeepNorthNight = new()
				{
					PrefabName = "RRRN_DravenNox_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMistlandsDay = new()
				{
					PrefabName = "RRRN_SceledrusShadowend_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMistlandsNight = new()
				{
					PrefabName = "RRRN_LazarusDeamonne_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossPlainsDay = new()
				{
					PrefabName = "RRRN_EchoBlack_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossPlainsNight = new()
				{
					PrefabName = "RRRN_MathianSerphent_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMountainDay = new()
				{
					PrefabName = "RRRN_LuxFrost_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMountainNight = new()
				{
					PrefabName = "RRRN_FirionWinter_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossSwampDay = new()
				{
					PrefabName = "RRRN_CrisenthShadowsoul_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossSwampNight = new()
				{
					PrefabName = "RRRN_JaydenShadowmend_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossBlackForestDay = new()
				{
					PrefabName = "RRRN_LazarusAutumn_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossBlackForestNight = new()
				{
					PrefabName = "RRRN_GrailThornheart_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMeadowsDay = new()
				{
					PrefabName = "RRRN_UpirGrim_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMeadowsNight = new()
				{
					PrefabName = "RRRN_ZaineEvilian_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings traderKnarr = new()
				{
					PrefabName = "Knarr",
					SpawnInterval = TimeSpan.FromSeconds(300),
					SpawnDuringDay = true,
					SpawnDuringNight = true,
				};

				config.ConfigureLocalSpawnerByName("Spawn_Trader_DoD")
					.WithSettings(traderKnarr);

				config.ConfigureLocalSpawnerByName("Spawn_TraderHome_DoD")
					.WithSettings(traderKnarr);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_AL")
					.WithSettings(minibossAshLandsDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_AL")
					.WithSettings(minibossAshLandsNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_DN")
					.WithSettings(minibossDeepNorthDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_DN")
					.WithSettings(minibossDeepNorthNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_Mist")
					.WithSettings(minibossMistlandsDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_Mist")
					.WithSettings(minibossMistlandsNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_Plains")
					.WithSettings(minibossPlainsDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_Plains")
					.WithSettings(minibossPlainsNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_Mount")
					.WithSettings(minibossMountainDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_Mount")
					.WithSettings(minibossMountainNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_Swamp")
					.WithSettings(minibossSwampDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_Swamp")
					.WithSettings(minibossSwampNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_BF")
					.WithSettings(minibossBlackForestDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_BF")
					.WithSettings(minibossBlackForestNight);

				config.ConfigureLocalSpawnerByName("Spawn_BossD_Meadows")
					.WithSettings(minibossMeadowsDay);

				config.ConfigureLocalSpawnerByName("Spawn_BossN_Meadows")
					.WithSettings(minibossMeadowsNight);
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
