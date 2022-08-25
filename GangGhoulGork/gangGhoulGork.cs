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
using SpawnThat.Spawners;
using SpawnThat.Spawners.LocalSpawner;
using SpawnThat.Spawners.WorldSpawner;

namespace GangGhoulGork
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class gangGhoulGork : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.GangGhoulGork";

		public const string PluginName = "GangOfGhoulGork";

		public const string PluginVersion = "0.0.1";

		public static GameObject Ghoul1;
		public static GameObject Ghoul2;
		public static GameObject Ghoul3;
		public static GameObject Ghoul4;
		public static GameObject Ghoul5;
		public static GameObject JAttack1;
		//public static GameObject JAttack2;
		//public static GameObject JAttack3;
		//public static GameObject JAttack4;
		//public static GameObject JAttack5;
		public static GameObject VFX1;
		public static GameObject VFX2;
		public static GameObject VFX3;
		public static GameObject VFX4;
		public static GameObject VFX5;

		public AssetBundle GhoulBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		private void Awake()
		{
			Log = Logger;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.GangGhoulGork");
			LoadBundle();
			LoadAssets();
			AddGhouls();
			try
			{
				SpawnerConfigurationManager.OnConfigure += ConfigureBiomeSpawners;
			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
			}
		}
		public void LoadBundle()
		{
			GhoulBundle = AssetUtils.LoadAssetBundleFromResources("ghouls", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Mobs
			Debug.Log("Ghouls: Mobs");
			Ghoul1 = GhoulBundle.LoadAsset<GameObject>("GhoulMob_GGG");
			Ghoul2 = GhoulBundle.LoadAsset<GameObject>("GhoulBoss_GGG");
			Ghoul3 = GhoulBundle.LoadAsset<GameObject>("GhoulFestering_GGG");
			Ghoul4 = GhoulBundle.LoadAsset<GameObject>("GhoulGrotesque_GGG");
			Ghoul5 = GhoulBundle.LoadAsset<GameObject>("GhoulScavenger_GGG");
			// Attacks
			Debug.Log("Ghouls: Attacks");
			JAttack1 = GhoulBundle.LoadAsset<GameObject>("Attack1_GGG");
			//JAttack2 = GhoulBundle.LoadAsset<GameObject>("GhoulBoss_Attack1_GGG");
			//JAttack3 = GhoulBundle.LoadAsset<GameObject>("GhoulFestering_Attack1_GGG");
			//JAttack4 = GhoulBundle.LoadAsset<GameObject>("GhoulGrotesque_Attack1_GGG");
			//JAttack5 = GhoulBundle.LoadAsset<GameObject>("GhoulScavenger_Attack1_GGG");
			GameObject attack13 = JAttack1;
			CustomPrefab Jattack1 = new CustomPrefab(attack13, false);
			PrefabManager.Instance.AddPrefab(Jattack1);
			/*GameObject attack14 = JAttack2;
			CustomPrefab Jattack2 = new CustomPrefab(attack14, false);
			PrefabManager.Instance.AddPrefab(Jattack2);
			GameObject attack15 = JAttack3;
			CustomPrefab Jattack3 = new CustomPrefab(attack15, false);
			PrefabManager.Instance.AddPrefab(Jattack3);
			GameObject attack16 = JAttack4;
			CustomPrefab Jattack4 = new CustomPrefab(attack16, false);
			PrefabManager.Instance.AddPrefab(Jattack4);
			GameObject attack17 = JAttack5;
			CustomPrefab Jattack5 = new CustomPrefab(attack17, false);
			PrefabManager.Instance.AddPrefab(Jattack5);*/
			//VFX
			Debug.Log("Ghouls: VFX");
			VFX1 = GhoulBundle.LoadAsset<GameObject>("FX_Backstab_GGG");
			VFX2 = GhoulBundle.LoadAsset<GameObject>("FX_Crit_GGG");
			VFX3 = GhoulBundle.LoadAsset<GameObject>("VFX_Blood_Hit_GGG");
			VFX4 = GhoulBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_GGG");
			VFX5 = GhoulBundle.LoadAsset<GameObject>("VFX_HitSparks_GGG");
			CustomPrefab vfx1 = new CustomPrefab(VFX1, false);
			PrefabManager.Instance.AddPrefab(vfx1);
			CustomPrefab vfx2 = new CustomPrefab(VFX2, false);
			PrefabManager.Instance.AddPrefab(vfx2);
			CustomPrefab vfx3 = new CustomPrefab(VFX3, false);
			PrefabManager.Instance.AddPrefab(vfx3);
			CustomPrefab vfx4 = new CustomPrefab(VFX4, false);
			PrefabManager.Instance.AddPrefab(vfx4);
			CustomPrefab vfx5 = new CustomPrefab(VFX5, false);
			PrefabManager.Instance.AddPrefab(vfx5);
		}
		private void AddGhouls()
		{
			try
			{
				Debug.Log("Ghouls: Ghoul");
				var GhoulMob1 = new CustomCreature(Ghoul1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob1);
				Debug.Log("Ghouls: Ghoul Boss");
				var GhoulMob2 = new CustomCreature(Ghoul2, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob2);
				Debug.Log("Ghouls: Ghoul Festering");
				var GhoulMob3 = new CustomCreature(Ghoul3, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob3);
				Debug.Log("Ghouls: Ghoul Grotesque");
				var GhoulMob4 = new CustomCreature(Ghoul4, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob4);
				Debug.Log("Ghouls: Ghoul Scavenger");
				var GhoulMob5 = new CustomCreature(Ghoul5, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob5);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures: {ex}");
			}
			finally
			{
				GhoulBundle.Unload(false);
			}
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			Debug.Log("Ghouls: Configure Spawns");
			try
			{
				ConfigureWorldSpawner(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureWorldSpawner(ISpawnerConfigurationCollection config)
		{
			Debug.Log("Ghouls: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(27_004)
					.SetPrefabName("GhoulScavenger_GGG")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(27_003)
					.SetPrefabName("GhoulGrotesque_GGG")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(27_002)
					.SetPrefabName("GhoulFestering_GGG")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(27_001)
					.SetPrefabName("GhoulBoss_GGG")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(27_000)
					.SetPrefabName("GhoulMob_GGG")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
