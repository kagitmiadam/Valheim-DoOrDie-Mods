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

namespace DoDNPCs
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.DoDBiomes", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.DoDItems", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.FantasyArmoury", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("GoldenJude_JudesEquipment", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.InstancedVillages", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDNPC : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoOrDieNPC";

		public const string PluginName = "DoOrDieNPC";

		public const string PluginVersion = "0.1.0";

		public static bool isModded = true;

		public AssetBundle NPCBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;

		public static GameObject NPCSmash;
		public static GameObject NPCSpin;
		public static GameObject NPCJab;
		public static GameObject NPCStab;
		public static GameObject NPCJump;
		public static GameObject NPCCone;

		public static GameObject Vidar;
		public static GameObject Skugga;
		public static GameObject SkuggaYoung;
		public static GameObject Nomad;
		public static GameObject GrayWolf;
		public static GameObject Njord;
		public static GameObject Einherjar;

		public static GameObject NPCAuraFire;
		public static GameObject NPCAuraIce;
		public static GameObject NPCAuraLightning;
		public static GameObject NPCAuraPoison;

		public static GameObject NPCAuraFireAoE;
		public static GameObject NPCAuraIceAoE;
		public static GameObject NPCAuraLightningAoE;
		public static GameObject NPCAuraPoisonAoE;

		private void Awake()
		{
			Log = Logger;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoOrDieNPC");
			LoadBundle();
			LoadAssets();
			AddNPCAttacks();
			AddNPCAuras();
			AddNPCs();
			CloneAndAddMeadowsBosses();
			CloneAndAddBlackForestBosses();
			CloneAndAddSwampBosses();
			CloneAndAddMountainBosses();
			CloneAndAddPlainsBosses();
			CloneAndAddMistlandsBosses();
			CloneAndAddDeepNorthBosses();
			CloneAndAddAshLandsBosses();
            try
			{
				SpawnerConfigurationManager.OnConfigure += ConfigureSpawners;

			}
			catch (Exception e)
			{
				System.Console.WriteLine(e);
			}
		}
		public void LoadBundle()
		{
			NPCBundle = AssetUtils.LoadAssetBundleFromResources("dodnpc", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			//Debug.Log("DoD NPC Assets: Attacks");
			NPCSmash = NPCBundle.LoadAsset<GameObject>("NPC_SmashAttack_DoD");
			NPCSpin = NPCBundle.LoadAsset<GameObject>("NPC_SpinAttack_DoD");
			NPCJab = NPCBundle.LoadAsset<GameObject>("NPC_JabAttack_DoD");
			NPCStab = NPCBundle.LoadAsset<GameObject>("NPC_StabAttack_DoD");
			NPCJump = NPCBundle.LoadAsset<GameObject>("NPC_JumpAttack_DoD");
			NPCCone = NPCBundle.LoadAsset<GameObject>("NPC_ConeAttack_DoD");

			//Debug.Log("DoD NPC Assets: NPCs");
			Vidar = NPCBundle.LoadAsset<GameObject>("Vidar_DoD");
			Skugga = NPCBundle.LoadAsset<GameObject>("Skugga_DoD");
			SkuggaYoung = NPCBundle.LoadAsset<GameObject>("Skugga_Young_DoD");
			Nomad = NPCBundle.LoadAsset<GameObject>("Nomad_DoD");
			GrayWolf = NPCBundle.LoadAsset<GameObject>("GrayWolf_DoD");
			Njord = NPCBundle.LoadAsset<GameObject>("Njord_DoD");
			Einherjar = NPCBundle.LoadAsset<GameObject>("Einherjar_DoD");

			//Debug.Log("DoD NPC Assets: Aura");
			NPCAuraFire = NPCBundle.LoadAsset<GameObject>("NPC_BossAuraF_Attack_DoD");
			NPCAuraIce = NPCBundle.LoadAsset<GameObject>("NPC_BossAuraI_Attack_DoD");
			NPCAuraLightning = NPCBundle.LoadAsset<GameObject>("NPC_BossAuraL_Attack_DoD");
			NPCAuraPoison = NPCBundle.LoadAsset<GameObject>("NPC_BossAuraP_Attack_DoD");

			//Debug.Log("DoD NPC Assets: Aoe");
			NPCAuraFireAoE = NPCBundle.LoadAsset<GameObject>("AoE_BossFire_DoD");
			NPCAuraIceAoE = NPCBundle.LoadAsset<GameObject>("AoE_BossIce_DoD");
			NPCAuraLightningAoE = NPCBundle.LoadAsset<GameObject>("AoE_BossLightning_DoD");
			NPCAuraPoisonAoE = NPCBundle.LoadAsset<GameObject>("AoE_BossPoison_DoD");
		}
		private void AddNPCAttacks()
		{
			//Debug.Log("DoD NPC: Attacks");
			GameObject monsteritem1 = NPCSmash;
			CustomItem customItem1 = new CustomItem(monsteritem1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject monsteritem2 = NPCSpin;
			CustomItem customItem2 = new CustomItem(monsteritem2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject monsteritem3 = NPCJab;
			CustomItem customItem3 = new CustomItem(monsteritem3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject monsteritem4 = NPCStab;
			CustomItem customItem4 = new CustomItem(monsteritem4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject monsteritem5 = NPCJump;
			CustomItem customItem5 = new CustomItem(monsteritem5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);
			GameObject monsteritem6 = NPCCone;
			CustomItem customItem6 = new CustomItem(monsteritem6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);
		}
		private void AddNPCAuras()
		{
			//Debug.Log("DoD NPC: Auras");
			GameObject monsteritem1 = NPCAuraFire;
			CustomItem customItem1 = new CustomItem(monsteritem1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject monsteritem2 = NPCAuraIce;
			CustomItem customItem2 = new CustomItem(monsteritem2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject monsteritem3 = NPCAuraLightning;
			CustomItem customItem3 = new CustomItem(monsteritem3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject monsteritem4 = NPCAuraPoison;
			CustomItem customItem4 = new CustomItem(monsteritem4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject monsteritem5 = NPCAuraFireAoE;
			PrefabManager.Instance.AddPrefab(monsteritem5);
			GameObject monsteritem6 = NPCAuraIceAoE;
			PrefabManager.Instance.AddPrefab(monsteritem6);
			GameObject monsteritem7 = NPCAuraLightningAoE;
			PrefabManager.Instance.AddPrefab(monsteritem7);
			GameObject monsteritem8 = NPCAuraPoisonAoE;
			PrefabManager.Instance.AddPrefab(monsteritem8);
		}
		private void AddNPCs()
		{
			try
			{
				//Debug.Log("DoD NPC: Vidar");
				var VidarMob = new CustomCreature(Vidar, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(VidarMob);
				//Debug.Log("DoD NPC: Skugga");
				var SkuggaMob = new CustomCreature(Skugga, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(SkuggaMob);
				//Debug.Log("DoD NPC: SkuggaYoung");
				var SkuggaYMob = new CustomCreature(SkuggaYoung, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(SkuggaYMob);
				//Debug.Log("DoD NPC: Nomad");
				var NomadMob = new CustomCreature(Nomad, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(NomadMob);
				//Debug.Log("DoD NPC: GrayWolf");
				var GrayWolfMob = new CustomCreature(GrayWolf, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(GrayWolfMob);
				//Debug.Log("DoD NPC: Njord");
				var NjordMob = new CustomCreature(Njord, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(NjordMob);
				//Debug.Log("DoD NPC: Einherjar");
				var EinherjarMob = new CustomCreature(Einherjar, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(EinherjarMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures: {ex}");
			}
			finally
			{
				//Debug.Log("DoD NPC: NPC's Done");
				//FantasyBundle.Unload(false);
			}
		}
		private void CloneAndAddMeadowsBosses()
		{
			try
			{
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraL_Attack_DoD");
				// Meadows - Upir Grim
				var boss1 = new CustomCreature("UpirGrim_DoD", "Vidar_DoD",
					new CreatureConfig
					{						
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "DeerHide",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new creature
					var upir = boss1.Prefab.GetComponent<Humanoid>();
						upir.m_name = "$boss_meadows_upirgrim";
						upir.m_health = 175f;
						Array.Resize(ref upir.m_defaultItems, upir.m_defaultItems.Length + 1);
						upir.m_defaultItems[upir.m_defaultItems.Length - 1] = aura;
						upir.m_defeatSetGlobalKey = "defeated_UpirGrim";
				// Add Creature
				CreatureManager.Instance.AddCreature(boss1);

				// Meadows - Zaine Evilian
				var boss2 = new CustomCreature("ZaineEvilian_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "DeerHide",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new creature
					var zaine = boss2.Prefab.GetComponent<Humanoid>();
						zaine.m_name = "$boss_meadows_zaineevilian";
						zaine.m_health = 175f;
						Array.Resize(ref zaine.m_defaultItems, zaine.m_defaultItems.Length + 1);
						zaine.m_defaultItems[zaine.m_defaultItems.Length - 1] = aura;
						zaine.m_defeatSetGlobalKey = "defeated_ZaineEvilian";
				// Add Creature
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom meadows bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddMeadowsBosses;
			}
		}
		private void CloneAndAddBlackForestBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraL_Attack_DoD");
				// Grail Thornheart
				var boss1 = new CustomCreature("GrailThornheart_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "CopperOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "TinOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new creature
					var grail = boss1.Prefab.GetComponent<Humanoid>();
						grail.m_name = "$boss_meadows_grailthornheart";
						grail.m_health = 500f;
						Array.Resize(ref grail.m_defaultItems, grail.m_defaultItems.Length + 1);
						grail.m_defaultItems[grail.m_defaultItems.Length - 1] = aura;
						grail.m_defeatSetGlobalKey = "defeated_GrailThornheart";
				// Add Creature
				CreatureManager.Instance.AddCreature(boss1);

				// Lazarus Autumn
				var boss2 = new CustomCreature("LazarusAutumn_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "CopperOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "TinOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "TrollHide",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new creature
					var lazarus = boss2.Prefab.GetComponent<Humanoid>();
						lazarus.m_name = "$boss_meadows_lazarusautumn";
						lazarus.m_health = 650f;
						Array.Resize(ref lazarus.m_defaultItems, lazarus.m_defaultItems.Length + 1);
						lazarus.m_defaultItems[lazarus.m_defaultItems.Length - 1] = aura;
						lazarus.m_defeatSetGlobalKey = "defeated_LazarusAutumn";
				// Add Creature
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom black forest bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddBlackForestBosses;
			}
		}
		private void CloneAndAddSwampBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraP_Attack_DoD");
				///////////////////////
				// JaydenShadowmend
				var boss1 = new CustomCreature("JaydenShadowmend_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "IronOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "IronScrap",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Chain",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var jayden = boss1.Prefab.GetComponent<Humanoid>();
						jayden.m_name = "$boss_meadows_jaydenshadowmend";
						jayden.m_health = 850f;
						Array.Resize(ref jayden.m_defaultItems, jayden.m_defaultItems.Length + 1);
						jayden.m_defaultItems[jayden.m_defaultItems.Length - 1] = aura;
						jayden.m_defeatSetGlobalKey = "defeated_JaydenShadowmend";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// CrisenthShadowsoul
				var boss2 = new CustomCreature("CrisenthShadowsoul_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "IronOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "IronScrap",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Chain",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var crisenth = boss2.Prefab.GetComponent<Humanoid>();
						crisenth.m_name = "$boss_meadows_crisenthshadowsoul";
						crisenth.m_health = 1000f;
						Array.Resize(ref crisenth.m_defaultItems, crisenth.m_defaultItems.Length + 1);
						crisenth.m_defaultItems[crisenth.m_defaultItems.Length - 1] = aura;
						crisenth.m_defeatSetGlobalKey = "defeated_CrisenthShadowsoul";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom swamp bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddSwampBosses;
			}
		}
		private void CloneAndAddMountainBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraI_Attack_DoD");
				///////////////////////
				// Firion Winter
				var boss1 = new CustomCreature("FirionWinter_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SilverOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "WolfPelt",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var firion = boss1.Prefab.GetComponent<Humanoid>();
						firion.m_name = "$boss_meadows_firionwinter";
						firion.m_health = 1250f;
						Array.Resize(ref firion.m_defaultItems, firion.m_defaultItems.Length + 1);
						firion.m_defaultItems[firion.m_defaultItems.Length - 1] = aura;
						firion.m_defeatSetGlobalKey = "defeated_FirionWinter";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// Lux Frost
				var boss2 = new CustomCreature("LuxFrost_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SilverOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "WolfPelt",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
						var lux = boss2.Prefab.GetComponent<Humanoid>();
						lux.m_name = "$boss_meadows_luxfrost";
						lux.m_health = 1100f;
						Array.Resize(ref lux.m_defaultItems, lux.m_defaultItems.Length + 1);
						lux.m_defaultItems[lux.m_defaultItems.Length - 1] = aura;
						lux.m_defeatSetGlobalKey = "defeated_LuxFrost";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom mountain bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddMountainBosses;
			}
		}
		private void CloneAndAddPlainsBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraF_Attack_DoD");
				///////////////////////
				// Mathian Serphent
				var boss1 = new CustomCreature("MathianSerphent_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "BlackmetalScrap",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Flax",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var mathian = boss1.Prefab.GetComponent<Humanoid>();
						mathian.m_name = "$boss_meadows_mathianserphent";
						mathian.m_health = 1500f;
						Array.Resize(ref mathian.m_defaultItems, mathian.m_defaultItems.Length + 1);
						mathian.m_defaultItems[mathian.m_defaultItems.Length - 1] = aura;
						mathian.m_defeatSetGlobalKey = "defeated_MathianSerphent";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// Echo Black
				var boss2 = new CustomCreature("EchoBlack_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "BlackmetalScrap",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Flax",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var echo = boss2.Prefab.GetComponent<Humanoid>();
						echo.m_name = "$boss_meadows_echoblack";
						echo.m_health = 1750f;
						Array.Resize(ref echo.m_defaultItems, echo.m_defaultItems.Length + 1);
						echo.m_defaultItems[echo.m_defaultItems.Length - 1] = aura;
						echo.m_defeatSetGlobalKey = "defeated_EchoBlack";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom mountain bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddPlainsBosses;
			}
		}
		private void CloneAndAddMistlandsBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraP_Attack_DoD");
				///////////////////////
				// Lazarus Deamonne
				var boss1 = new CustomCreature("LazarusDeamonne_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FelmetalOre_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Steel_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var lazarus = boss1.Prefab.GetComponent<Humanoid>();
						lazarus.m_name = "$boss_meadows_lazarusdeamonne";
						lazarus.m_health = 2500f;
						Array.Resize(ref lazarus.m_defaultItems, lazarus.m_defaultItems.Length + 1);
						lazarus.m_defaultItems[lazarus.m_defaultItems.Length - 1] = aura;
						lazarus.m_defeatSetGlobalKey = "defeated_LazarusDeamonne";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// Sceledrus Shadowend
				var boss2 = new CustomCreature("SceledrusShadowend_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FelmetalOre_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "Steel_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var sceledrus = boss2.Prefab.GetComponent<Humanoid>();
						sceledrus.m_name = "$boss_meadows_sceledrusshadowend";
						sceledrus.m_health = 2750f;
						Array.Resize(ref sceledrus.m_defaultItems, sceledrus.m_defaultItems.Length + 1);
						sceledrus.m_defaultItems[sceledrus.m_defaultItems.Length - 1] = aura;
						sceledrus.m_defeatSetGlobalKey = "defeated_SceledrusShadowend";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom mountain bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddMistlandsBosses;
			}
		}
		private void CloneAndAddDeepNorthBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraI_Attack_DoD");
				///////////////////////
				// Draven Nox
				var boss1 = new CustomCreature("DravenNox_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FrometalOre_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "DireWolfPelt_DoD",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var draven = boss1.Prefab.GetComponent<Humanoid>();
						draven.m_name = "$boss_meadows_dravennox";
						draven.m_health = 3500f;
						Array.Resize(ref draven.m_defaultItems, draven.m_defaultItems.Length + 1);
						draven.m_defaultItems[draven.m_defaultItems.Length - 1] = aura;
						draven.m_defeatSetGlobalKey = "defeated_DravenNox";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// Lincoln Hunt
				var boss2 = new CustomCreature("LincolnHunt_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FrometalOre_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "DireWolfPelt_DoD",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var lincoln = boss2.Prefab.GetComponent<Humanoid>();
						lincoln.m_name = "$boss_meadows_lincolnhunt";
						lincoln.m_health = 3750f;
						Array.Resize(ref lincoln.m_defaultItems, lincoln.m_defaultItems.Length + 1);
						lincoln.m_defaultItems[lincoln.m_defaultItems.Length - 1] = aura;
						lincoln.m_defeatSetGlobalKey = "defeated_LincolnHunt";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom mountain bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddDeepNorthBosses;
			}
		}
		private void CloneAndAddAshLandsBosses()
		{
			try
			{
				// Load Items to add to creatures
				var aura = PrefabManager.Cache.GetPrefab<GameObject>("NPC_BossAuraF_Attack_DoD");
				///////////////////////
				// Cinder Mortem
				var boss1 = new CustomCreature("CinderMortem_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FlametalOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var cinder = boss1.Prefab.GetComponent<Humanoid>();
						cinder.m_name = "$boss_meadows_cindermortem";
						cinder.m_health = 4500f;
						Array.Resize(ref cinder.m_defaultItems, cinder.m_defaultItems.Length + 1);
						cinder.m_defaultItems[cinder.m_defaultItems.Length - 1] = aura;
						cinder.m_defeatSetGlobalKey = "defeated_cindermortem";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss1);
				///////////////////////
				// Ash Vexx
				var boss2 = new CustomCreature("AshVexx_DoD", "Vidar_DoD",
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								Chance = 100,
								MinAmount = 27,
								MaxAmount = 87,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "FlametalOre",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "OakWood_DoD",
								Chance = 50,
								MinAmount = 3,
								MaxAmount = 8,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "GreyPearl_DoD",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 3,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "SkullToken_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							},
							new DropConfig
							{
								Item = "InfusedGemstone_DoD",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 5,
								OnePerPlayer = false,
								LevelMultiplier = false,
							}
						}
					});
					// Edit components of the new boss
					var ash = boss2.Prefab.GetComponent<Humanoid>();
						ash.m_name = "$boss_meadows_ashvexx";
						ash.m_health = 4750f;
						Array.Resize(ref ash.m_defaultItems, ash.m_defaultItems.Length + 1);
						ash.m_defaultItems[ash.m_defaultItems.Length - 1] = aura;
						ash.m_defeatSetGlobalKey = "defeated_AshVexx";
				// Add cloned and modified Boss
				CreatureManager.Instance.AddCreature(boss2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while modifying custom mountain bosses: {ex}");
			}
			finally
			{
				// Unregister the hook, modified and cloned creatures are kept over the whole game session
				CreatureManager.OnVanillaCreaturesAvailable -= CloneAndAddAshLandsBosses;
			}
		}
		public static void ConfigureSpawners(ISpawnerConfigurationCollection config)
		{
			try
			{
				ConfigureSkuggaLocationsByName(config);
				ConfigureBossLocationsByName(config);
				ConfigureWorldSpawners(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureArenaLocationByName(ISpawnerConfigurationCollection config)
		{
			try
			{
				LocalSpawnSettings skuggaCamp = new()
				{
					PrefabName = "Skugga_DoD",
					SpawnInterval = TimeSpan.FromSeconds(360),
				};

				config.ConfigureLocalSpawnerByName("Arena_SpawnerD_DoD")
					.WithSettings(skuggaCamp);
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
		private static void ConfigureSkuggaLocationsByName(ISpawnerConfigurationCollection config)
        {
            try
			{
				LocalSpawnSettings skuggaCamp = new()
				{
					PrefabName = "Skugga_DoD",
					SpawnInterval = TimeSpan.FromSeconds(360),
				};

				config.ConfigureLocalSpawnerByName("Camp_Spawner_DoD")
					.WithSettings(skuggaCamp);
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
		private static void ConfigureBossLocationsByName(ISpawnerConfigurationCollection config)
		{
			try
			{
				LocalSpawnSettings minibossAshLandsDay = new()
				{
					PrefabName = "AshVexx_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossAshLandsNight = new()
				{
					PrefabName = "CinderMortem_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossDeepNorthDay = new()
				{
					PrefabName = "LincolnHunt_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossDeepNorthNight = new()
				{
					PrefabName = "DravenNox_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMistlandsDay = new()
				{
					PrefabName = "SceledrusShadowend_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMistlandsNight = new()
				{
					PrefabName = "LazarusDeamonne_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossPlainsDay = new()
				{
					PrefabName = "EchoBlack_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossPlainsNight = new()
				{
					PrefabName = "MathianSerphent_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMountainDay = new()
				{
					PrefabName = "LuxFrost_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMountainNight = new()
				{
					PrefabName = "FirionWinter_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossSwampDay = new()
				{
					PrefabName = "CrisenthShadowsoul_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossSwampNight = new()
				{
					PrefabName = "JaydenShadowmend_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossBlackForestDay = new()
				{
					PrefabName = "LazarusAutumn_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossBlackForestNight = new()
				{
					PrefabName = "GrailThornheart_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = false,
					SpawnDuringNight = true,
				};

				LocalSpawnSettings minibossMeadowsDay = new()
				{
					PrefabName = "UpirGrim_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
					SpawnDuringDay = true,
					SpawnDuringNight = false,
				};

				LocalSpawnSettings minibossMeadowsNight = new()
				{
					PrefabName = "ZaineEvilian_DoD",
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
		private static void ConfigureWorldSpawners(ISpawnerConfigurationCollection config)
		{
			try
			{
				config.ConfigureWorldSpawner(22_003)
					.SetPrefabName("Nomad_DoD")
					.SetTemplateName("Nomad High")
					.SetConditionBiomes(Heightmap.Biome.BiomesMax)
					.SetSpawnChance(30)
					.SetSpawnInterval(TimeSpan.FromSeconds(120))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(25)
					.SetSpawnAtDistanceToPlayerMax(50)
					.SetMinDistanceToOther(25)
					.SetConditionNearbyPlayersCarryItem(75, "BerserkerAxe_DoD", "DeathknightAxe_DoD", "DruidSpear_DoD", "MageWand_DoD", "MonkMace_DoD", "NinjaSword_DoD", "PaladinMace_DoD", "RogueSword_DoD", "ShamanWand_DoD", "WarlockWand_DoD")
					;
				config.ConfigureWorldSpawner(22_002)
					.SetPrefabName("Nomad_DoD")
					.SetTemplateName("Nomad Low")
					.SetConditionBiomes(Heightmap.Biome.BiomesMax)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(120))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(25)
					.SetSpawnAtDistanceToPlayerMax(50)
					.SetMinDistanceToOther(25)
					.SetConditionNearbyPlayersCarryItem(75, "DruidItem_DoD", "ArcherItem_DoD", "BerserkerItem_DoD", "DeathKnightItem_DoD", "EngineerItem_DoD", "MageItem_DoD", "MonkItem_DoD", "NinjaItem_DoD", "PaladinItem_DoD", "RogueItem_DoD", "ShamanItem_DoD", "WarlockItem_DoD")
					;
				config.ConfigureWorldSpawner(22_001)
					.SetPrefabName("GrayWolf_DoD")
					.SetTemplateName("Gray Wolf")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(45)
					.SetSpawnAtDistanceToPlayerMax(60)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(22_000)
					.SetPrefabName("Einherjar_DoD")
					.SetTemplateName("Einherjar")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(45)
					.SetSpawnAtDistanceToPlayerMax(60)
					.SetMinDistanceToOther(100)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}

	}
}
