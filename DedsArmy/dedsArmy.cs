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

namespace DedsArmy
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class dedsArmy : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DedsArmy";

		public const string PluginName = "DedsArmy";

		public const string PluginVersion = "0.0.8";

		public static GameObject Ghoul1;
		public static GameObject Ghoul2;
		public static GameObject Ghoul3;
		public static GameObject Ghoul4;
		public static GameObject Ghoul5;
		public static GameObject Skeleton1;
		public static GameObject Skeleton2;
		public static GameObject Vampire1;
		public static GameObject Boss1;

		public static GameObject JAttack1;
		public static GameObject JAttack2;
		public static GameObject JAttack3;
		public static GameObject JAttack4;
		public static GameObject JAttack5;
		public static GameObject JAttack6;
		public static GameObject SAttack1;
		public static GameObject SAttack2;
		public static GameObject SAttack3;
		public static GameObject SAttack4;
		public static GameObject SAttack5;
		public static GameObject SAttack6;
		public static GameObject VAttack1;
		public static GameObject VAttack2;
		public static GameObject VAttack3;
		public static GameObject VAttack4;
		public static GameObject VAttack5;
		public static GameObject BossAttack1;
		public static GameObject BossAttack2;
		public static GameObject BossAttack3;
		public static GameObject BossAttack4;
		public static GameObject BossAttack5;
		public static GameObject BossAttack6;
		public static GameObject BossAttack7;
		public static GameObject BossAttack8;

		public static GameObject BossAoE1;
		public static GameObject BossSpawn1;

		public static GameObject VFX1;
		public static GameObject VFX2;
		public static GameObject VFX3;
		public static GameObject VFX4;
		public static GameObject VFX5;
		public static GameObject VFX6;

		public static GameObject SFX1;
		public static GameObject SFX2;
		public static GameObject SFX3;
		public static GameObject SFX4;
		public static GameObject SFX5;
		public static GameObject SFX6;
		public static GameObject SFX7;
		public static GameObject SFX8;
		public static GameObject SFX9;
		public static GameObject SFX10;
		public static GameObject SFX11;
		public static GameObject SFX12;
		public static GameObject SFX13;
		public static GameObject SFX14;
		public static GameObject SFX15;
		public static GameObject SFX16;
		public static GameObject SFX17;
		public static GameObject SFX18;
		public static GameObject SFX19;
		public static GameObject SFX20;
		public static GameObject SFX21;
		public static GameObject SFX22;

		public static GameObject GhoulTrophy1;
		public static GameObject GhoulTrophy2;
		public static GameObject GhoulTrophy3;
		public static GameObject GhoulTrophy4;
		public static GameObject GhoulTrophy5;
		public static GameObject SkeletonTrophy1;
		public static GameObject SkeletonTrophy2;
		public static GameObject VampireTrophy1;
		public static GameObject BossTrophy1;

		public ConfigEntry<bool> EnableSpawns;
		public ConfigEntry<bool> UndeadEnable;
		public ConfigEntry<bool> SkeletonsEnable;
		public ConfigEntry<bool> VampireEnable;
		public ConfigEntry<bool> UndeadBossEnable;

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
		private void CreateConfigurationValues()
		{
			EnableSpawns = base.Config.Bind("Spawning", "Enable", defaultValue: true, new ConfigDescription("Enables default swamp biome spawning.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			UndeadEnable = base.Config.Bind("Undead Monsters", "Enable", defaultValue: true, new ConfigDescription("Enables Undead prefabs, must disable Spawning if disabled.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			SkeletonsEnable = base.Config.Bind("Skeleton Monsters", "Enable", defaultValue: true, new ConfigDescription("Enables Skeleton prefabs, must disable Spawning if disabled.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			VampireEnable = base.Config.Bind("Vampire Monster", "Enable", defaultValue: true, new ConfigDescription("Enables Vampire prefab, must disable Spawning if disabled.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			UndeadBossEnable = base.Config.Bind("Boss", "Enable", defaultValue: true, new ConfigDescription("Enables Undead Boss prefab. No Spawner included, down to user to use a means of their choice.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
		{
			Log = Logger;
			CreateConfigurationValues();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DedsArmy");
			LoadBundle();
			LoadAssets();
			if (UndeadEnable.Value == true) AddUndead();
			if (SkeletonsEnable.Value == true) AddSkeletons();
			if (VampireEnable.Value == true) AddVampire();
			if (UndeadBossEnable.Value == true) AddBosses();
			if (EnableSpawns.Value == true)
			{
				try
				{
					SpawnerConfigurationManager.OnConfigure += ConfigureBiomeSpawners;
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e);
				}
            }
		}
		public void LoadBundle()
		{
			GhoulBundle = AssetUtils.LoadAssetBundleFromResources("dedsarmy", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Mobs
			//Debug.Log("Ded's Army: Mobs");
			Ghoul1 = GhoulBundle.LoadAsset<GameObject>("Undead_DA");
			Ghoul2 = GhoulBundle.LoadAsset<GameObject>("UndeadCarver_DA");
			Ghoul3 = GhoulBundle.LoadAsset<GameObject>("UndeadDesecrator_DA");
			Ghoul4 = GhoulBundle.LoadAsset<GameObject>("UndeadReaver_DA");
			Ghoul5 = GhoulBundle.LoadAsset<GameObject>("UndeadRipper_DA");
			Skeleton1 = GhoulBundle.LoadAsset<GameObject>("Skeleton1H_DA");
			Skeleton2 = GhoulBundle.LoadAsset<GameObject>("Skeleton2H_DA");
			Vampire1 = GhoulBundle.LoadAsset<GameObject>("Vampire_DA");
			Boss1 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_DA");
			// Attacks
			//Debug.Log("Ded's Army: Attacks");
			JAttack1 = GhoulBundle.LoadAsset<GameObject>("Undead_Attack1_DA");
			JAttack2 = GhoulBundle.LoadAsset<GameObject>("Undead_Attack2_DA");
			JAttack3 = GhoulBundle.LoadAsset<GameObject>("Undead_Attack3_DA");
			JAttack4 = GhoulBundle.LoadAsset<GameObject>("Undead_AttackShield1_DA");
			JAttack5 = GhoulBundle.LoadAsset<GameObject>("Undead_AttackShield2_DA");
			JAttack6 = GhoulBundle.LoadAsset<GameObject>("Undead_AttackShield3_DA");
			SAttack1 = GhoulBundle.LoadAsset<GameObject>("Skeleton1H_Attack1_DA");
			SAttack2 = GhoulBundle.LoadAsset<GameObject>("Skeleton1H_Attack2_DA");
			SAttack3 = GhoulBundle.LoadAsset<GameObject>("Skeleton1H_Attack3_DA");
			SAttack4 = GhoulBundle.LoadAsset<GameObject>("Skeleton2H_Attack1_DA");
			SAttack5 = GhoulBundle.LoadAsset<GameObject>("Skeleton2H_Attack2_DA");
			SAttack6 = GhoulBundle.LoadAsset<GameObject>("Skeleton2H_Attack3_DA");
			VAttack1 = GhoulBundle.LoadAsset<GameObject>("Vampire_Attack1_DA");
			VAttack2 = GhoulBundle.LoadAsset<GameObject>("Vampire_Attack2_DA");
			VAttack3 = GhoulBundle.LoadAsset<GameObject>("Vampire_Attack3_DA");
			VAttack4 = GhoulBundle.LoadAsset<GameObject>("Vampire_Attack4_DA");
			VAttack5 = GhoulBundle.LoadAsset<GameObject>("Vampire_Attack5_DA");
			BossAttack1 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack1_DA");
			BossAttack2 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack2_DA");
			BossAttack3 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack3_DA");
			BossAttack4 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack4_DA");
			BossAttack5 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack5_DA");
			BossAttack6 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Attack6_DA");
			BossAttack7 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_AttackSpawn_DA");
			BossAttack8 = GhoulBundle.LoadAsset<GameObject>("AuraGreenFire_DA");
			CustomPrefab bossattack1 = new CustomPrefab(BossAttack1, false);
			PrefabManager.Instance.AddPrefab(bossattack1);
			CustomPrefab bossattack2 = new CustomPrefab(BossAttack2, false);
			PrefabManager.Instance.AddPrefab(bossattack2);
			CustomPrefab bossattack3 = new CustomPrefab(BossAttack3, false);
			PrefabManager.Instance.AddPrefab(bossattack3);
			CustomPrefab bossattack4 = new CustomPrefab(BossAttack4, false);
			PrefabManager.Instance.AddPrefab(bossattack4);
			CustomPrefab bossattack5 = new CustomPrefab(BossAttack5, false);
			PrefabManager.Instance.AddPrefab(bossattack5);
			CustomPrefab bossattack6 = new CustomPrefab(BossAttack6, false);
			PrefabManager.Instance.AddPrefab(bossattack6);
			CustomPrefab bossattack7 = new CustomPrefab(BossAttack7, false);
			PrefabManager.Instance.AddPrefab(bossattack7);
			CustomPrefab bossattack8 = new CustomPrefab(BossAttack8, false);
			PrefabManager.Instance.AddPrefab(bossattack8);
			GameObject attack1 = JAttack1;
			CustomPrefab Jattack1 = new CustomPrefab(attack1, false);
			PrefabManager.Instance.AddPrefab(Jattack1);
			GameObject attack2 = JAttack2;
			CustomPrefab Jattack2 = new CustomPrefab(attack2, false);
			PrefabManager.Instance.AddPrefab(Jattack2);
			GameObject attack3 = JAttack3;
			CustomPrefab Jattack3 = new CustomPrefab(attack3, false);
			PrefabManager.Instance.AddPrefab(Jattack3);
			GameObject attack4 = JAttack4;
			CustomPrefab Jattack4 = new CustomPrefab(attack4, false);
			PrefabManager.Instance.AddPrefab(Jattack4);
			GameObject attack5 = JAttack5;
			CustomPrefab Jattack5 = new CustomPrefab(attack5, false);
			PrefabManager.Instance.AddPrefab(Jattack5);
			GameObject attack6 = JAttack6;
			CustomPrefab Jattack6 = new CustomPrefab(attack6, false);
			PrefabManager.Instance.AddPrefab(Jattack6);
			GameObject attack7 = SAttack1;
			CustomPrefab Sattack1 = new CustomPrefab(attack7, false);
			PrefabManager.Instance.AddPrefab(Sattack1);
			GameObject attack8 = SAttack2;
			CustomPrefab Sattack2 = new CustomPrefab(attack8, false);
			PrefabManager.Instance.AddPrefab(Sattack2);
			GameObject attack9 = SAttack3;
			CustomPrefab Sattack3 = new CustomPrefab(attack9, false);
			PrefabManager.Instance.AddPrefab(Sattack3);
			GameObject attack10 = SAttack4;
			CustomPrefab Sattack4 = new CustomPrefab(attack10, false);
			PrefabManager.Instance.AddPrefab(Sattack4);
			GameObject attack11 = SAttack5;
			CustomPrefab Sattack5 = new CustomPrefab(attack11, false);
			PrefabManager.Instance.AddPrefab(Sattack5);
			GameObject attack12 = SAttack6;
			CustomPrefab Sattack6 = new CustomPrefab(attack12, false);
			PrefabManager.Instance.AddPrefab(Sattack6);
			GameObject attack13 = VAttack1;
			CustomPrefab Vattack1 = new CustomPrefab(attack13, false);
			PrefabManager.Instance.AddPrefab(Vattack1);
			GameObject attack14 = VAttack2;
			CustomPrefab Vattack2 = new CustomPrefab(attack14, false);
			PrefabManager.Instance.AddPrefab(Vattack2);
			GameObject attack15 = VAttack3;
			CustomPrefab Vattack3 = new CustomPrefab(attack15, false);
			PrefabManager.Instance.AddPrefab(Vattack3);
			GameObject attack16 = VAttack4;
			CustomPrefab Vattack4 = new CustomPrefab(attack16, false);
			PrefabManager.Instance.AddPrefab(Vattack4);
			GameObject attack17 = VAttack5;
			CustomPrefab Vattack5 = new CustomPrefab(attack17, false);
			PrefabManager.Instance.AddPrefab(Vattack5);
			// AOE
			BossAoE1 = GhoulBundle.LoadAsset<GameObject>("AoE_AuraGreenFire_DA");
			BossSpawn1 = GhoulBundle.LoadAsset<GameObject>("UndeadBoss_Spawn_DA");
			CustomPrefab aoe1 = new CustomPrefab(BossAoE1, false);
			PrefabManager.Instance.AddPrefab(aoe1);
			CustomPrefab aoe2 = new CustomPrefab(BossSpawn1, false);
			PrefabManager.Instance.AddPrefab(aoe2);
			//VFX
			//Debug.Log("Ded's Army: VFX");
			VFX1 = GhoulBundle.LoadAsset<GameObject>("FX_Backstab_DA");
			VFX2 = GhoulBundle.LoadAsset<GameObject>("FX_Crit_DA");
			VFX3 = GhoulBundle.LoadAsset<GameObject>("VFX_Blood_Hit_DA");
			VFX4 = GhoulBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_DA");
			VFX5 = GhoulBundle.LoadAsset<GameObject>("VFX_HitSparks_DA");
			VFX6 = GhoulBundle.LoadAsset<GameObject>("VFX_SpawnArrive_DA");
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
			CustomPrefab vfx6 = new CustomPrefab(VFX6, false);
			PrefabManager.Instance.AddPrefab(vfx6);
			//SFX
			//Debug.Log("Ded's Army: SFX");
			SFX1 = GhoulBundle.LoadAsset<GameObject>("SFX_VampireAlert_DA");
			SFX2 = GhoulBundle.LoadAsset<GameObject>("SFX_VampireAttack_DA");
			SFX3 = GhoulBundle.LoadAsset<GameObject>("SFX_VampireDeath_DA");
			SFX4 = GhoulBundle.LoadAsset<GameObject>("SFX_VampireGetHit_DA");
			SFX5 = GhoulBundle.LoadAsset<GameObject>("SFX_VampireIdle_DA");
			SFX6 = GhoulBundle.LoadAsset<GameObject>("SFX_ZombieAlert_DA");
			SFX7 = GhoulBundle.LoadAsset<GameObject>("SFX_ZombieAttack_DA");
			SFX8 = GhoulBundle.LoadAsset<GameObject>("SFX_ZombieDeath_DA");
			SFX9 = GhoulBundle.LoadAsset<GameObject>("SFX_ZombieGetHit_DA");
			SFX10 = GhoulBundle.LoadAsset<GameObject>("SFX_ZombieIdle_DA");
			SFX11 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonAlert_DA");
			SFX12 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonAttack_DA");
			SFX13 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonDeath_DA");
			SFX14 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonGetHit_DA");
			SFX15 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonIdle_DA");
			SFX16 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossAlert_DA");
			SFX17 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossAttack_DA");
			SFX18 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossDeath_DA");
			SFX19 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossGetHit_DA");
			SFX20 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossIdle_DA");
			SFX21 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossAttackAoE_DA");
			SFX22 = GhoulBundle.LoadAsset<GameObject>("SFX_SkeletonBossAttackSpawn_DA");
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
			CustomPrefab sfx6 = new CustomPrefab(SFX6, false);
			PrefabManager.Instance.AddPrefab(sfx6);
			CustomPrefab sfx7 = new CustomPrefab(SFX7, false);
			PrefabManager.Instance.AddPrefab(sfx7);
			CustomPrefab sfx8 = new CustomPrefab(SFX8, false);
			PrefabManager.Instance.AddPrefab(sfx8);
			CustomPrefab sfx9 = new CustomPrefab(SFX9, false);
			PrefabManager.Instance.AddPrefab(sfx9);
			CustomPrefab sfx10 = new CustomPrefab(SFX10, false);
			PrefabManager.Instance.AddPrefab(sfx10);
			CustomPrefab sfx11 = new CustomPrefab(SFX11, false);
			PrefabManager.Instance.AddPrefab(sfx11);
			CustomPrefab sfx12 = new CustomPrefab(SFX12, false);
			PrefabManager.Instance.AddPrefab(sfx12);
			CustomPrefab sfx13 = new CustomPrefab(SFX13, false);
			PrefabManager.Instance.AddPrefab(sfx13);
			CustomPrefab sfx14 = new CustomPrefab(SFX14, false);
			PrefabManager.Instance.AddPrefab(sfx14);
			CustomPrefab sfx15 = new CustomPrefab(SFX15, false);
			PrefabManager.Instance.AddPrefab(sfx15);
			CustomPrefab sfx16 = new CustomPrefab(SFX16, false);
			PrefabManager.Instance.AddPrefab(sfx16);
			CustomPrefab sfx17 = new CustomPrefab(SFX17, false);
			PrefabManager.Instance.AddPrefab(sfx17);
			CustomPrefab sfx18 = new CustomPrefab(SFX18, false);
			PrefabManager.Instance.AddPrefab(sfx18);
			CustomPrefab sfx19 = new CustomPrefab(SFX19, false);
			PrefabManager.Instance.AddPrefab(sfx19);
			CustomPrefab sfx20 = new CustomPrefab(SFX20, false);
			PrefabManager.Instance.AddPrefab(sfx20);
			CustomPrefab sfx21 = new CustomPrefab(SFX21, false);
			PrefabManager.Instance.AddPrefab(sfx21);
			CustomPrefab sfx22 = new CustomPrefab(SFX22, false);
			PrefabManager.Instance.AddPrefab(sfx22);
			// Trophies
			//Debug.Log("Ded's Army: Trophies");
			GhoulTrophy1 = GhoulBundle.LoadAsset<GameObject>("Trophy_Undead_DA");
			CustomItem customItem1 = new CustomItem(GhoulTrophy1, fixReference: false);
			ItemManager.Instance.AddItem(customItem1);

			GhoulTrophy2 = GhoulBundle.LoadAsset<GameObject>("Trophy_UndeadCarver_DA");
			CustomItem customItem2 = new CustomItem(GhoulTrophy2, fixReference: false);
			ItemManager.Instance.AddItem(customItem2);

			GhoulTrophy3 = GhoulBundle.LoadAsset<GameObject>("Trophy_UndeadDesecrator_DA");
			CustomItem customItem3 = new CustomItem(GhoulTrophy3, fixReference: false);
			ItemManager.Instance.AddItem(customItem3);

			GhoulTrophy4 = GhoulBundle.LoadAsset<GameObject>("Trophy_UndeadReaver_DA");
			CustomItem customItem4 = new CustomItem(GhoulTrophy4, fixReference: false);
			ItemManager.Instance.AddItem(customItem4);

			GhoulTrophy5 = GhoulBundle.LoadAsset<GameObject>("Trophy_UndeadRipper_DA");
			CustomItem customItem5 = new CustomItem(GhoulTrophy5, fixReference: false);
			ItemManager.Instance.AddItem(customItem5);

			SkeletonTrophy1 = GhoulBundle.LoadAsset<GameObject>("Trophy_Skeleton1H_DA");
			CustomItem customItem6 = new CustomItem(SkeletonTrophy1, fixReference: false);
			ItemManager.Instance.AddItem(customItem6);

			SkeletonTrophy2 = GhoulBundle.LoadAsset<GameObject>("Trophy_Skeleton2H_DA");
			CustomItem customItem7 = new CustomItem(SkeletonTrophy2, fixReference: false);
			ItemManager.Instance.AddItem(customItem7);

			VampireTrophy1 = GhoulBundle.LoadAsset<GameObject>("Trophy_Vampire_DA");
			CustomItem customItem8 = new CustomItem(VampireTrophy1, fixReference: false);
			ItemManager.Instance.AddItem(customItem8);

			BossTrophy1 = GhoulBundle.LoadAsset<GameObject>("Trophy_UndeadBoss_DA");
			CustomItem customItem9 = new CustomItem(BossTrophy1, fixReference: false);
			ItemManager.Instance.AddItem(customItem9);
		}
		private void AddBosses()
		{
			try
			{
				//Debug.Log("Ded's Army: Boss");
				var BossMob = new CustomCreature(Boss1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_UndeadBoss_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 100
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Bloodbag",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 110,
								MaxAmount = 220,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(BossMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom boss for Ded's Army: {ex}");
			}
			finally
			{
				GhoulBundle.Unload(false);
			}
		}
		private void AddVampire()
        {
			try
			{
				//Debug.Log("Ded's Army: Vampire");
				var VampireMob = new CustomCreature(Vampire1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Vampire_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Bloodbag",
								MinAmount = 2,
								MaxAmount = 5,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(VampireMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom vampire for Ded's Army: {ex}");
			}
		}
		private void AddSkeletons()
        {
            try
			{
				//Debug.Log("Ded's Army: Skeleton 1H");
				var SkelMob1 = new CustomCreature(Skeleton1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Skeleton1H_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 2,
								MaxAmount = 5,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(SkelMob1);
				//Debug.Log("Ded's Army: Skeleton 2H");
				var SkelMob2 = new CustomCreature(Skeleton2, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Skeleton2H_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 2,
								MaxAmount = 5,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 11,
								MaxAmount = 22,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(SkelMob2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom skeletons for Ded's Army: {ex}");
			}
		}
		private void AddUndead()
		{
			try
			{
				//Debug.Log("Ded's Army: Undead");
				var GhoulMob1 = new CustomCreature(Ghoul1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Undead_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 25
							},
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
				//Debug.Log("Ded's Army: Carver");
				var GhoulMob2 = new CustomCreature(Ghoul2, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_UndeadCarver_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 25
							},
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
				//Debug.Log("Ded's Army: Desecrator");
				var GhoulMob3 = new CustomCreature(Ghoul3, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_UndeadDesecrator_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 25
							},
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
				//Debug.Log("Ded's Army: Reaver");
				var GhoulMob4 = new CustomCreature(Ghoul4, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_UndeadReaver_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 25
							},
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
				//Debug.Log("Ded's Army: Ripper");
				var GhoulMob5 = new CustomCreature(Ghoul5, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_UndeadRipper_DA",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronScrap",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 3
							},
							new DropConfig
							{
								Item = "Turnip",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 15
							},
							new DropConfig
							{
								Item = "TurnipSeeds",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 75
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 3,
								Chance = 25
							},
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
				Logger.LogWarning($"Exception caught while adding custom undead for Ded's Army: {ex}");
			}
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Ghouls: Configure Spawns");
			try
			{
				ConfigureWorldSpawner(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong adding spawners for Ded's Army: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureWorldSpawner(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Ded's Army: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(27_107)
					.SetPrefabName("Vampire_DA")
					.SetTemplateName("Vampire")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_106)
					.SetPrefabName("Skeleton2H_DA")
					.SetTemplateName("Skeleton")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_105)
					.SetPrefabName("Skeleton1H_DA")
					.SetTemplateName("Skeleton")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_104)
					.SetPrefabName("UndeadDesecrator_DA")
					.SetTemplateName("Undead")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_103)
					.SetPrefabName("UndeadReaver_DA")
					.SetTemplateName("Undead")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_102)
					.SetPrefabName("UndeadRipper_DA")
					.SetTemplateName("Undead")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_101)
					.SetPrefabName("UndeadCarver_DA")
					.SetTemplateName("Undead")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
				config.ConfigureWorldSpawner(27_100)
					.SetPrefabName("Undead_DA")
					.SetTemplateName("Undead")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(12)
					.SetSpawnInterval(TimeSpan.FromSeconds(350))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-0.25f)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
