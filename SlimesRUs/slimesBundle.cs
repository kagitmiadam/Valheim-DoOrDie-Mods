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

namespace SlimesRUs
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class slimesBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.SlimesRUs";

		public const string PluginName = "SlimesRUs";

		public const string PluginVersion = "0.0.1";

		public static bool isModded = true;

		public static GameObject Slime1;
		public static GameObject Slime2;
		public static GameObject Slime3;
		public static GameObject Slime4;
		public static GameObject Slime5;
		public static GameObject Slime6;

		public static GameObject SBAttack1;
		public static GameObject SBAttack2;
		public static GameObject SBAttack3;
		public static GameObject SBAttack4;

		public static GameObject SGAttack1;
		public static GameObject SGAttack2;
		public static GameObject SGAttack3;
		public static GameObject SGAttack4;

		public static GameObject SRAttack1;
		public static GameObject SRAttack2;
		public static GameObject SRAttack3;
		public static GameObject SRAttack4;

		public static GameObject SPAttack1;
		public static GameObject SPAttack2;
		public static GameObject SPAttack3;
		public static GameObject SPAttack4;

		public static GameObject SPrAttack1;
		public static GameObject SPrAttack2;
		public static GameObject SPrAttack3;
		public static GameObject SPrAttack4;

		public static GameObject SYAttack1;
		public static GameObject SYAttack2;
		public static GameObject SYAttack3;
		public static GameObject SYAttack4;

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
		public static GameObject VFX6;
		public static GameObject VFX7;
		public static GameObject VFX8;
		public static GameObject VFX9;
		public static GameObject VFX10;
		public static GameObject VFX11;
		public static GameObject VFX12;
		public static GameObject VFX13;
		public static GameObject VFX14;

		public static GameObject FX1;
		public static GameObject FX2;

		//public static GameObject SlimeTrophy1;

		public AssetBundle SlimesBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;
		private void Awake()
		{
			Log = Logger;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.SlimesRUs");
			LoadBundle();
			LoadAssets();
			AddSlimes();
			PrefabManager.OnVanillaPrefabsAvailable += FixSFX;
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
			SlimesBundle = AssetUtils.LoadAssetBundleFromResources("slime", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Mobs
			//Debug.Log("Minotaurs: Mobs");
			Slime1 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_HS");
			Slime2 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_HS");
			Slime3 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_HS");
			Slime4 = SlimesBundle.LoadAsset<GameObject>("SlimePink_HS");
			Slime5 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_HS");
			Slime6 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_HS");
			// Ragdolls
			//Debug.Log("Minotaurs: Ragdolls");
			GameObject Ragdoll1 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_RD_HS");
			CustomPrefab RD1 = new CustomPrefab(Ragdoll1, true);
			PrefabManager.Instance.AddPrefab(RD1);
			GameObject Ragdoll2 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_RD_HS");
			CustomPrefab RD2 = new CustomPrefab(Ragdoll2, true);
			PrefabManager.Instance.AddPrefab(RD2);
			GameObject Ragdoll3 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_RD_HS");
			CustomPrefab RD3 = new CustomPrefab(Ragdoll3, true);
			PrefabManager.Instance.AddPrefab(RD3);
			GameObject Ragdoll4 = SlimesBundle.LoadAsset<GameObject>("SlimePink_RD_HS");
			CustomPrefab RD4 = new CustomPrefab(Ragdoll4, true);
			PrefabManager.Instance.AddPrefab(RD4);
			GameObject Ragdoll5 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_RD_HS");
			CustomPrefab RD5 = new CustomPrefab(Ragdoll5, true);
			PrefabManager.Instance.AddPrefab(RD5);
			GameObject Ragdoll6 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_RD_HS");
			CustomPrefab RD6 = new CustomPrefab(Ragdoll6, true);
			PrefabManager.Instance.AddPrefab(RD6);
			// Attacks
			//Debug.Log("Minotaurs: Attacks");
			SBAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_Attack1_HS");
			SBAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_Attack2_HS");
			SBAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_Attack3_HS");
			SBAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimeBlue_Attack4_HS");
			SGAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_Attack1_HS");
			SGAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_Attack2_HS");
			SGAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_Attack3_HS");
			SGAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimeGreen_Attack4_HS");
			SRAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_Attack1_HS");
			SRAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_Attack2_HS");
			SRAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_Attack3_HS");
			SRAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimeRed_Attack4_HS");
			SPAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimePink_Attack1_HS");
			SPAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimePink_Attack2_HS");
			SPAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimePink_Attack3_HS");
			SPAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimePink_Attack4_HS");
			SPrAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_Attack1_HS");
			SPrAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_Attack2_HS");
			SPrAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_Attack3_HS");
			SPrAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimePurple_Attack4_HS");
			SYAttack1 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_Attack1_HS");
			SYAttack2 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_Attack2_HS");
			SYAttack3 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_Attack3_HS");
			SYAttack4 = SlimesBundle.LoadAsset<GameObject>("SlimeYellow_Attack4_HS");
			GameObject attack1 = SBAttack1;
			CustomPrefab SlimeAttack1 = new CustomPrefab(attack1, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack1);
			GameObject attack2 = SBAttack2;
			CustomPrefab SlimeAttack2 = new CustomPrefab(attack2, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack2);
			GameObject attack3 = SBAttack3;
			CustomPrefab SlimeAttack3 = new CustomPrefab(attack3, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack3);
			GameObject attack4 = SBAttack4;
			CustomPrefab SlimeAttack4 = new CustomPrefab(attack4, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack4);
			GameObject attack5 = SRAttack1;
			CustomPrefab SlimeAttack5 = new CustomPrefab(attack5, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack5);
			GameObject attack6 = SRAttack2;
			CustomPrefab SlimeAttack6 = new CustomPrefab(attack6, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack6);
			GameObject attack7 = SRAttack3;
			CustomPrefab SlimeAttack7 = new CustomPrefab(attack7, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack7);
			GameObject attack8 = SRAttack4;
			CustomPrefab SlimeAttack8 = new CustomPrefab(attack8, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack8);
			GameObject attack9 = SGAttack1;
			CustomPrefab SlimeAttack9 = new CustomPrefab(attack9, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack9);
			GameObject attack10 = SGAttack2;
			CustomPrefab SlimeAttack10 = new CustomPrefab(attack10, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack10);
			GameObject attack11 = SGAttack3;
			CustomPrefab SlimeAttack11 = new CustomPrefab(attack11, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack11);
			GameObject attack12 = SGAttack4;
			CustomPrefab SlimeAttack12 = new CustomPrefab(attack12, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack12);
			GameObject attack13 = SPAttack1;
			CustomPrefab SlimeAttack13 = new CustomPrefab(attack13, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack13);
			GameObject attack14 = SPAttack2;
			CustomPrefab SlimeAttack14 = new CustomPrefab(attack14, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack14);
			GameObject attack15 = SPAttack3;
			CustomPrefab SlimeAttack15 = new CustomPrefab(attack15, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack15);
			GameObject attack16 = SPAttack4;
			CustomPrefab SlimeAttack16 = new CustomPrefab(attack16, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack16);
			GameObject attack17 = SPrAttack1;
			CustomPrefab SlimeAttack17 = new CustomPrefab(attack17, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack17);
			GameObject attack18 = SPrAttack2;
			CustomPrefab SlimeAttack18 = new CustomPrefab(attack18, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack18);
			GameObject attack19 = SPrAttack3;
			CustomPrefab SlimeAttack19 = new CustomPrefab(attack19, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack19);
			GameObject attack20 = SPrAttack4;
			CustomPrefab SlimeAttack20 = new CustomPrefab(attack20, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack20);
			GameObject attack21 = SYAttack1;
			CustomPrefab SlimeAttack21 = new CustomPrefab(attack21, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack21);
			GameObject attack22 = SYAttack2;
			CustomPrefab SlimeAttack22 = new CustomPrefab(attack22, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack22);
			GameObject attack23 = SYAttack3;
			CustomPrefab SlimeAttack23 = new CustomPrefab(attack23, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack23);
			GameObject attack24 = SYAttack4;
			CustomPrefab SlimeAttack24 = new CustomPrefab(attack24, false);
			PrefabManager.Instance.AddPrefab(SlimeAttack24);
			//SFX
			//Debug.Log("Minotaurs: SFX");
			SFX1 = SlimesBundle.LoadAsset<GameObject>("SFX_SlimeAlert_HS");
			SFX2 = SlimesBundle.LoadAsset<GameObject>("SFX_SlimeAttack_HS");
			SFX3 = SlimesBundle.LoadAsset<GameObject>("SFX_SlimeDeath_HS");
			SFX4 = SlimesBundle.LoadAsset<GameObject>("SFX_SlimeHit_HS");
			SFX5 = SlimesBundle.LoadAsset<GameObject>("SFX_SlimeIdle_HS");
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
			//Debug.Log("Minotaurs: VFX");
			VFX1 = SlimesBundle.LoadAsset<GameObject>("VFX_HitSparks_HS");
			VFX2 = SlimesBundle.LoadAsset<GameObject>("VFX_Water_Hit_HS");
			VFX3 = SlimesBundle.LoadAsset<GameObject>("VFX_PurifyWater_HS");
			VFX4 = SlimesBundle.LoadAsset<GameObject>("VFX_Acid_Hit_HS");
			VFX5 = SlimesBundle.LoadAsset<GameObject>("VFX_Fire_Hit_HS");
			VFX6 = SlimesBundle.LoadAsset<GameObject>("VFX_Pyro_Hit_HS");
			VFX7 = SlimesBundle.LoadAsset<GameObject>("VFX_Shadow_Hit_HS");
			VFX8 = SlimesBundle.LoadAsset<GameObject>("VFX_Sick_Hit_HS");
			VFX9 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayBlue_HS");
			VFX10 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayGreen_HS");
			VFX11 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayPink_HS");
			VFX12 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayPurple_HS");
			VFX13 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayRed_HS");
			VFX14 = SlimesBundle.LoadAsset<GameObject>("VFX_SlimeSprayYellow_HS");
			FX1 = SlimesBundle.LoadAsset<GameObject>("FX_Crit_HS");
			FX2 = SlimesBundle.LoadAsset<GameObject>("FX_Backstab_HS");
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
			CustomPrefab vfx7 = new CustomPrefab(VFX7, false);
			PrefabManager.Instance.AddPrefab(vfx7);
			CustomPrefab vfx8 = new CustomPrefab(VFX8, false);
			PrefabManager.Instance.AddPrefab(vfx8);
			CustomPrefab vfx9 = new CustomPrefab(VFX9, false);
			PrefabManager.Instance.AddPrefab(vfx9);
			CustomPrefab vfx10 = new CustomPrefab(VFX10, false);
			PrefabManager.Instance.AddPrefab(vfx10);
			CustomPrefab vfx11 = new CustomPrefab(VFX11, false);
			PrefabManager.Instance.AddPrefab(vfx11);
			CustomPrefab vfx12 = new CustomPrefab(VFX12, false);
			PrefabManager.Instance.AddPrefab(vfx12);
			CustomPrefab vfx13 = new CustomPrefab(VFX13, false);
			PrefabManager.Instance.AddPrefab(vfx13);
			CustomPrefab vfx14 = new CustomPrefab(VFX14, false);
			PrefabManager.Instance.AddPrefab(vfx14);
			CustomPrefab fx1 = new CustomPrefab(FX1, false);
			PrefabManager.Instance.AddPrefab(fx1);
			CustomPrefab fx2 = new CustomPrefab(FX2, false);
			PrefabManager.Instance.AddPrefab(fx2);
			// Trophies
			//SlimeTrophy1 = SlimesBundle.LoadAsset<GameObject>("Trophy_SlimeBlue_HS");
			//CustomItem customItem6 = new CustomItem(SlimeTrophy1, false);
			//ItemManager.Instance.AddItem(customItem6);
		}
		private void AddSlimes()
		{
			try
			{
				//Debug.Log("Slimes R Us: Blue Slime");
				var SlimeBlueMob = new CustomCreature(Slime1, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimeBlueMob);
				//Debug.Log("Slimes R Us: Green Slime");
				var SlimeGreenMob = new CustomCreature(Slime2, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimeGreenMob);
				//Debug.Log("Slimes R Us: Red Slime");
				var SlimeRedMob = new CustomCreature(Slime3, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimeRedMob);
				//Debug.Log("Slimes R Us: Pink Slime");
				var SlimePinkMob = new CustomCreature(Slime4, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimePinkMob);
				//Debug.Log("Slimes R Us: Purple Slime");
				var SlimePurpleMob = new CustomCreature(Slime5, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimePurpleMob);
				//Debug.Log("Slimes R Us: Yellow Slime");
				var SlimeYellowMob = new CustomCreature(Slime6, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 1,
								MaxAmount = 15,
								Chance = 100
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 25
							},
							new DropConfig
							{
								Item = "Guck",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 2
							},
							new DropConfig
							{
								Item = "BoneFragments",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 5
							}
						}
					});
				CreatureManager.Instance.AddCreature(SlimeYellowMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures: {ex}");
			}
			finally
			{
				SlimesBundle.Unload(false);
			}
		}
		private void FixSFX()
		{
			try
			{
				var sfxfab1 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_MinotaurAlert_HM");
				var sfxfab2 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_MinotaurAttack_HM");
				var sfxfab3 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_MinotaurDeath_HM");
				var sfxfab4 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_MinotaurHit_HM");
				var sfxfab5 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_MinotaurIdle_HM");
				if (sfxfab1 != null)
				{
					sfxfab1.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab2 != null)
				{
					sfxfab2.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab3 != null)
				{
					sfxfab3.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab4 != null)
				{
					sfxfab4.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab5 != null)
				{
					sfxfab5.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
			}
			catch
			{
				Debug.LogWarning("Minotaurs: SFX Fix Failed");
			}
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Minotaurs: Configure Spawns");
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
			//Debug.Log("Slimes R Us: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(26_205)
					.SetPrefabName("SlimeYellow_HS")
					.SetTemplateName("Yellow Slime")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					.SetModifierFaction(Character.Faction.PlainsMonsters)
					;
				config.ConfigureWorldSpawner(26_204)
					.SetPrefabName("SlimePurple_HS")
					.SetTemplateName("Purple Slime")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					.SetModifierFaction(Character.Faction.PlainsMonsters)
					;
				config.ConfigureWorldSpawner(26_203)
					.SetPrefabName("SlimePink_HS")
					.SetTemplateName("Pink Slime")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					;
				config.ConfigureWorldSpawner(26_202)
					.SetPrefabName("SlimeRed_HS")
					.SetTemplateName("Red Slime")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					.SetModifierFaction(Character.Faction.Demon)
					;
				config.ConfigureWorldSpawner(26_201)
					.SetPrefabName("SlimeGreen_HS")
					.SetTemplateName("Green Slime")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					.SetModifierFaction(Character.Faction.Undead)
					;
				config.ConfigureWorldSpawner(26_200)
					.SetPrefabName("SlimeBlue_HS")
					.SetTemplateName("Blue Slime")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(0)
					.SetConditionAltitudeMax(1)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
