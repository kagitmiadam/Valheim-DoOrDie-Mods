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

namespace Giants
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class giantsBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.FeeFiFoFum";

		public const string PluginName = "Fee Fi Fo Fum";

		public const string PluginVersion = "0.1.1";

		public static bool isModded = true;

		public static GameObject Giant1;
		public static GameObject Giant2;
		public static GameObject Giant3;
		public static GameObject Giant4;
		public static GameObject Giant5;
		public static GameObject Giant6;
		public static GameObject HGAttack1;
		public static GameObject HGAttack2;
		public static GameObject HGAttack3;
		public static GameObject MGAttack1;
		public static GameObject MGAttack2;
		public static GameObject MGAttack3;
		public static GameObject GAttack1;
		public static GameObject GAttack2;
		public static GameObject GAttack3;
		public static GameObject FGAttack1;
		public static GameObject FGAttack2;
		public static GameObject FGAttack3;
		public static GameObject JAttack1;
		public static GameObject JAttack2;
		public static GameObject JAttack3;
		public static GameObject JAttack4;
		public static GameObject JAttack5;
		public static GameObject FrGAttack1;
		public static GameObject FrGAttack2;
		public static GameObject FrGAttack3;
		public static GameObject SFX1;
		public static GameObject SFX2;
		public static GameObject SFX3;
		public static GameObject SFX4;
		public static GameObject SFX5;
		//public static GameObject SFX6;
		public static GameObject VFX1;
		public static GameObject VFX2;
		public static GameObject VFX3;
		public static GameObject VFX4;
		public static GameObject VFX5;
		public static GameObject Item1;
		public static GameObject Projectile1;
		public static GameObject Projectile2;
		public static GameObject FX1;
		public static GameObject FX2;
		public static GameObject GiantTrophy1;
		public static GameObject GiantTrophy2;
		public static GameObject GiantTrophy3;
		public static GameObject GiantTrophy4;
		public static GameObject GiantTrophy5;
		public static GameObject GiantTrophy6;

		public AssetBundle GiantBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;
		private void Awake()
		{
			Log = Logger;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FeeFiFoFum");
			LoadBundle();
			LoadAssets();
			AddGiants();
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
			GiantBundle = AssetUtils.LoadAssetBundleFromResources("giants", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Mobs
			//Debug.Log("Giants: Mobs");
			GameObject Ragdoll1 = GiantBundle.LoadAsset<GameObject>("FireGiant_RD_HG");
			CustomPrefab RD1 = new CustomPrefab(Ragdoll1, true);
			PrefabManager.Instance.AddPrefab(RD1);
			GameObject Ragdoll2 = GiantBundle.LoadAsset<GameObject>("FrostGiant_RD_HG");
			CustomPrefab RD2 = new CustomPrefab(Ragdoll2, true);
			PrefabManager.Instance.AddPrefab(RD2);
			GameObject Ragdoll3 = GiantBundle.LoadAsset<GameObject>("Giant_RD_HG");
			CustomPrefab RD3 = new CustomPrefab(Ragdoll3, true);
			PrefabManager.Instance.AddPrefab(RD3);
			GameObject Ragdoll4 = GiantBundle.LoadAsset<GameObject>("HillGiant_RD_HG");
			CustomPrefab RD4 = new CustomPrefab(Ragdoll4, true);
			PrefabManager.Instance.AddPrefab(RD4);
			GameObject Ragdoll5 = GiantBundle.LoadAsset<GameObject>("Jotunn_RD_HG");
			CustomPrefab RD5 = new CustomPrefab(Ragdoll5, true);
			PrefabManager.Instance.AddPrefab(RD5);
			GameObject Ragdoll6 = GiantBundle.LoadAsset<GameObject>("MountainGiant_RD_HG");
			CustomPrefab RD6 = new CustomPrefab(Ragdoll6, true);
			PrefabManager.Instance.AddPrefab(RD6);
			// Mobs
			//Debug.Log("Giants: Mobs");
			Giant1 = GiantBundle.LoadAsset<GameObject>("HillGiant_HG");
			Giant2 = GiantBundle.LoadAsset<GameObject>("MountainGiant_HG");
			Giant3 = GiantBundle.LoadAsset<GameObject>("Giant_HG");
			Giant4 = GiantBundle.LoadAsset<GameObject>("FrostGiant_HG");
			Giant5 = GiantBundle.LoadAsset<GameObject>("Jotunn_HG");
			Giant6 = GiantBundle.LoadAsset<GameObject>("FireGiant_HG");
			// Attacks
			//Debug.Log("Giants: Attacks");
			HGAttack1 = GiantBundle.LoadAsset<GameObject>("HillGiant_Attack1_HG");
			HGAttack2 = GiantBundle.LoadAsset<GameObject>("HillGiant_Attack2_HG");
			HGAttack3 = GiantBundle.LoadAsset<GameObject>("HillGiant_Attack3_HG");
			MGAttack1 = GiantBundle.LoadAsset<GameObject>("MountainGiant_Attack1_HG");
			MGAttack2 = GiantBundle.LoadAsset<GameObject>("MountainGiant_Attack2_HG");
			MGAttack3 = GiantBundle.LoadAsset<GameObject>("MountainGiant_Attack3_HG");
			GAttack1 = GiantBundle.LoadAsset<GameObject>("Giant_Attack1_HG");
			GAttack2 = GiantBundle.LoadAsset<GameObject>("Giant_Attack2_HG");
			GAttack3 = GiantBundle.LoadAsset<GameObject>("Giant_Attack3_HG");
			FGAttack1 = GiantBundle.LoadAsset<GameObject>("FrostGiant_Attack1_HG");
			FGAttack2 = GiantBundle.LoadAsset<GameObject>("FrostGiant_Attack2_HG");
			FGAttack3 = GiantBundle.LoadAsset<GameObject>("FrostGiant_Attack3_HG");
			JAttack1 = GiantBundle.LoadAsset<GameObject>("Jotunn_Attack1_HG");
			JAttack2 = GiantBundle.LoadAsset<GameObject>("Jotunn_Attack2_HG");
			JAttack3 = GiantBundle.LoadAsset<GameObject>("Jotunn_Attack3_HG");
			JAttack4 = GiantBundle.LoadAsset<GameObject>("Jotunn_AttackSpawn_HG");
			JAttack5 = GiantBundle.LoadAsset<GameObject>("Jotunn_AttackJump_HG");
			FrGAttack1 = GiantBundle.LoadAsset<GameObject>("FireGiant_Attack1_HG");
			FrGAttack2 = GiantBundle.LoadAsset<GameObject>("FireGiant_Attack2_HG");
			FrGAttack3 = GiantBundle.LoadAsset<GameObject>("FireGiant_Attack3_HG");
			GameObject attack1 = HGAttack1;
			CustomPrefab HGattack1 = new CustomPrefab(attack1, false);
			PrefabManager.Instance.AddPrefab(HGattack1);
			GameObject attack2 = HGAttack2;
			CustomPrefab HGattack2 = new CustomPrefab(attack2, false);
			PrefabManager.Instance.AddPrefab(HGattack2);
			GameObject attack3 = HGAttack3;
			CustomPrefab HGattack3 = new CustomPrefab(attack3, false);
			PrefabManager.Instance.AddPrefab(HGattack3);
			GameObject attack4 = MGAttack1;
			CustomPrefab MGattack1 = new CustomPrefab(attack4, false);
			PrefabManager.Instance.AddPrefab(MGattack1);
			GameObject attack5 = MGAttack2;
			CustomPrefab MGattack2 = new CustomPrefab(attack5, false);
			PrefabManager.Instance.AddPrefab(MGattack2);
			GameObject attack6 = MGAttack3;
			CustomPrefab MGattack3 = new CustomPrefab(attack6, false);
			PrefabManager.Instance.AddPrefab(MGattack3);
			GameObject attack7 = GAttack1;
			CustomPrefab Gattack1 = new CustomPrefab(attack7, false);
			PrefabManager.Instance.AddPrefab(Gattack1);
			GameObject attack8 = GAttack2;
			CustomPrefab Gattack2 = new CustomPrefab(attack8, false);
			PrefabManager.Instance.AddPrefab(Gattack2);
			GameObject attack9 = GAttack3;
			CustomPrefab Gattack3 = new CustomPrefab(attack9, false);
			PrefabManager.Instance.AddPrefab(Gattack3);
			GameObject attack10 = FGAttack1;
			CustomPrefab FGattack1 = new CustomPrefab(attack10, false);
			PrefabManager.Instance.AddPrefab(FGattack1);
			GameObject attack11 = FGAttack2;
			CustomPrefab FGattack2 = new CustomPrefab(attack11, false);
			PrefabManager.Instance.AddPrefab(FGattack2);
			GameObject attack12 = FGAttack3;
			CustomPrefab FGattack3 = new CustomPrefab(attack12, false);
			PrefabManager.Instance.AddPrefab(FGattack3);
			GameObject attack13 = JAttack1;
			CustomPrefab Jattack1 = new CustomPrefab(attack13, false);
			PrefabManager.Instance.AddPrefab(Jattack1);
			GameObject attack14 = JAttack2;
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
			PrefabManager.Instance.AddPrefab(Jattack5);
			GameObject attack18 = FrGAttack1;
			CustomPrefab FrGattack1 = new CustomPrefab(attack18, false);
			PrefabManager.Instance.AddPrefab(FrGattack1);
			GameObject attack19 = FrGAttack2;
			CustomPrefab FrGattack2 = new CustomPrefab(attack19, false);
			PrefabManager.Instance.AddPrefab(FrGattack2);
			GameObject attack20 = FrGAttack3;
			CustomPrefab FrGattack3 = new CustomPrefab(attack20, false);
			PrefabManager.Instance.AddPrefab(FrGattack3);
			//Projectile
			Projectile1 = GiantBundle.LoadAsset<GameObject>("Jotunn_Spawn_HG");
			Projectile2 = GiantBundle.LoadAsset<GameObject>("AoE_Jotunn_HG");
			GameObject projectile1 = Projectile1;
			CustomPrefab Proj1 = new CustomPrefab(projectile1, false);
			PrefabManager.Instance.AddPrefab(Proj1);
			GameObject projectile2 = Projectile2;
			CustomPrefab Proj2 = new CustomPrefab(projectile2, false);
			PrefabManager.Instance.AddPrefab(Proj2);
			// FX
			FX1 = GiantBundle.LoadAsset<GameObject>("FX_Backstab_HG");
			FX2 = GiantBundle.LoadAsset<GameObject>("FX_Crit_HG");
			CustomPrefab fx1 = new CustomPrefab(FX1, false);
			PrefabManager.Instance.AddPrefab(fx1);
			CustomPrefab fx2 = new CustomPrefab(FX2, false);
			PrefabManager.Instance.AddPrefab(fx2);
			//SFX
			//Debug.Log("Giants: SFX");
			SFX1 = GiantBundle.LoadAsset<GameObject>("SFX_GiantAlert_HG");
			SFX2 = GiantBundle.LoadAsset<GameObject>("SFX_GiantAttack_HG");
			SFX3 = GiantBundle.LoadAsset<GameObject>("SFX_GiantDeath_HG");
			SFX4 = GiantBundle.LoadAsset<GameObject>("SFX_GiantHit_HG");
			SFX5 = GiantBundle.LoadAsset<GameObject>("SFX_GiantIdle_HG");
			CustomPrefab sfx1 = new CustomPrefab(SFX1, false);
			PrefabManager.Instance.AddPrefab(sfx1);
			CustomPrefab sfx2 = new CustomPrefab(SFX2, false);
			PrefabManager.Instance.AddPrefab(sfx2);
			CustomPrefab sfx3 = new CustomPrefab(SFX3, false);
			PrefabManager.Instance.AddPrefab(sfx3);
			CustomPrefab sfx4 = new CustomPrefab(SFX4, false);
			PrefabManager.Instance.AddPrefab(sfx4);
			CustomPrefab sfx5 = new CustomPrefab(SFX5, false);
			PrefabManager.Instance.AddPrefab(sfx5);
			//VFX
			//Debug.Log("Giants: VFX");
			VFX1 = GiantBundle.LoadAsset<GameObject>("VFX_Blood_Hit_HG");
			VFX2 = GiantBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_HG");
			VFX3 = GiantBundle.LoadAsset<GameObject>("VFX_HitSparks_HG");
			VFX4 = GiantBundle.LoadAsset<GameObject>("VFX_GiantArrive_HG");
			VFX5 = GiantBundle.LoadAsset<GameObject>("VFX_JotunnLand_HG");
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
			// Items
			Item1 = GiantBundle.LoadAsset<GameObject>("HillGiantSword_HG");
			CustomItem customItem1 = new CustomItem(Item1, fixReference: false);
			ItemManager.Instance.AddItem(customItem1);
			// Trophies
			GiantTrophy1 = GiantBundle.LoadAsset<GameObject>("Trophy_HillGiant_HG");
			CustomItem customItem2 = new CustomItem(GiantTrophy1, fixReference: false);
			ItemManager.Instance.AddItem(customItem2);
			GiantTrophy2 = GiantBundle.LoadAsset<GameObject>("Trophy_MountainGiant_HG");
			CustomItem customItem3 = new CustomItem(GiantTrophy2, fixReference: false);
			ItemManager.Instance.AddItem(customItem3);
			GiantTrophy3 = GiantBundle.LoadAsset<GameObject>("Trophy_Giant_HG");
			CustomItem customItem4 = new CustomItem(GiantTrophy3, fixReference: false);
			ItemManager.Instance.AddItem(customItem4);
			GiantTrophy4 = GiantBundle.LoadAsset<GameObject>("Trophy_FrostGiant_HG");
			CustomItem customItem5 = new CustomItem(GiantTrophy4, fixReference: false);
			ItemManager.Instance.AddItem(customItem5);
			GiantTrophy5 = GiantBundle.LoadAsset<GameObject>("Trophy_Jotunn_HG");
			CustomItem customItem6 = new CustomItem(GiantTrophy5, fixReference: false);
			ItemManager.Instance.AddItem(customItem6);
			GiantTrophy6 = GiantBundle.LoadAsset<GameObject>("Trophy_FireGiant_HG");
			CustomItem customItem7 = new CustomItem(GiantTrophy6, fixReference: false);
			ItemManager.Instance.AddItem(customItem7);
		}
		private void AddGiants()
		{
			try
			{
				//Debug.Log("Giants: Fire Giant");
				var FireGiantMob = new CustomCreature(Giant6, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_FireGiant_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 43,
								MaxAmount = 98,
								Chance = 100
							},
							new DropConfig
							{
								Item = "SilverNecklace",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 15
							},
							new DropConfig
							{
								Item = "Flametal",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 25
							},
							new DropConfig
							{
								Item = "YmirRemains",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(FireGiantMob);
				//Debug.Log("Giants: Jotunn");
				var JotunnMob = new CustomCreature(Giant5, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Jotunn_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 113,
								MaxAmount = 178,
								Chance = 100
							},
							new DropConfig
							{
								Item = "SilverNecklace",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 15
							},
							new DropConfig
							{
								Item = "FreezeGland",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 25
							},
							new DropConfig
							{
								Item = "YmirRemains",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(JotunnMob);
				//Debug.Log("Giants: Frost");
				var FrostGiantMob = new CustomCreature(Giant4, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_FrostGiant_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 23,
								MaxAmount = 78,
								Chance = 100
							},
							new DropConfig
							{
								Item = "SilverNecklace",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 15
							},
							new DropConfig
							{
								Item = "FreezeGland",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 25
							},
							new DropConfig
							{
								Item = "YmirRemains",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(FrostGiantMob);
				//Debug.Log("Giants: Giant");
				var GiantMob = new CustomCreature(Giant3, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Giant_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 17,
								MaxAmount = 41,
								Chance = 100
							},
							new DropConfig
							{
								Item = "LoxPelt",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 10
							},
							new DropConfig
							{
								Item = "CookedLoxMeat",
								MinAmount = 2,
								MaxAmount = 8,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Cloudberry",
								MinAmount = 3,
								MaxAmount = 12,
								Chance = 25
							}
						}
					});
				CreatureManager.Instance.AddCreature(GiantMob);
				//Debug.Log("Giants: Mountain");
				var MountGiantMob = new CustomCreature(Giant2, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_MountainGiant_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 10,
								MaxAmount = 28,
								Chance = 100
							},
							new DropConfig
							{
								Item = "WolfPelt",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 15
							},
							new DropConfig
							{
								Item = "FreezeGland",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 10
							},
							new DropConfig
							{
								Item = "CookedWolfMeat",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 25
							}
						}
					});
				CreatureManager.Instance.AddCreature(MountGiantMob);
				//Debug.Log("Giants: Hill");
				var HillGiantMob = new CustomCreature(Giant1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_HillGiant_HG",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 100
							},
							new DropConfig
							{
								Item = "DeerHide",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 15
							},
							new DropConfig
							{
								Item = "Honey",
								MinAmount = 2,
								MaxAmount = 4,
								Chance = 10
							},
							new DropConfig
							{
								Item = "LeatherScraps",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 25
							}
						}
					});
				CreatureManager.Instance.AddCreature(HillGiantMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures: {ex}");
			}
			finally
			{
				GiantBundle.Unload(false);
			}
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Giants: Configure Spawns");
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
			//Debug.Log("Giants: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(26_004)
					.SetPrefabName("FireGiant_HG")
					.SetTemplateName("Fire Giant")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(1)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(26_003)
					.SetPrefabName("FrostGiant_HG")
					.SetTemplateName("Frost Giant")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(10)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(26_002)
					.SetPrefabName("Giant_HG")
					.SetTemplateName("Giant")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(10)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(26_001)
					.SetPrefabName("MountainGiant_HG")
					.SetTemplateName("Mountain Giant")
					.SetConditionBiomes(Heightmap.Biome.Mountain)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(280))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(95)
					.SetConditionAltitudeMax(2000)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(26_000)
					.SetPrefabName("HillGiant_HG")
					.SetTemplateName("Hill Giant")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(5)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(40)
					.SetConditionAltitudeMax(70)
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
