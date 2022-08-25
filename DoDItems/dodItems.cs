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

namespace DoDItems
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.DoDMonsters", BepInDependency.DependencyFlags.HardDependency)]
	internal class dodItems : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDItems";

		public const string PluginName = "DoOrDieItems";

		public const string PluginVersion = "0.1.5";

		public static bool isModded = true;

		private Harmony _harmony;
		public static readonly ManualLogSource DoDLogger = BepInEx.Logging.Logger.CreateLogSource(PluginName);

		// anvils
		public static GameObject AnvilsFel;
		public static GameObject AnvilsFro;
		public static GameObject AnvilsFlam;
		public static Sprite TexFlaAnvil;
		public static Sprite TexFroAnvil;
		public static Sprite TexFelAnvil;
		public static GameObject MineRock_FelOre_DoD;
		public static GameObject MineRock_FroOre_DoD;

		public static GameObject Firesoul;
		public static GameObject Solarflare;
		public static GameObject Reaper;
		public static GameObject Desolation;
		public static GameObject Soulstorm;
		public static GameObject Silverlight;
		public static GameObject Coldflame;
		public static GameObject Frostflame;
		public static GameObject Nethersbane;

		public static GameObject WandLightning;
		public static GameObject WandFire;
		public static GameObject WandShadow;
		public static GameObject MaceDivine;
		public static GameObject SwordAssassin;
		public static GameObject AxeKnight;
		public static GameObject SwordVoid;
		public static GameObject SpearAcid;
		public static GameObject AxeBattle;
		public static GameObject MaceShock;
		public static GameObject SpearAoE;
		public static GameObject SpartanBlade;
		public static GameObject HolyStaff;

		public static GameObject T1ArmorKit;
		public static GameObject T2ArmorKit;
		public static GameObject T3ArmorKit;
		public static GameObject T4ArmorKit;
		public static GameObject T5ArmorKit;
		public static GameObject T6ArmorKit;
		public static GameObject T7ArmorKit;
		public static GameObject T8ArmorKit;

		public static GameObject T1WeaponKit;
		public static GameObject T2WeaponKit;
		public static GameObject T3WeaponKit;
		public static GameObject T4WeaponKit;
		public static GameObject T5WeaponKit;
		public static GameObject T6WeaponKit;
		public static GameObject T7WeaponKit;
		public static GameObject T8WeaponKit;

		public static GameObject DeathKnightItem;
		public static GameObject ArcherItem;
		public static GameObject BerserkerItem;
		public static GameObject DruidItem;
		public static GameObject MageItem;
		public static GameObject MonkItem;
		public static GameObject NinjaItem;
		public static GameObject PaladinItem;
		public static GameObject RogueItem;
		public static GameObject ShamanItem;
		public static GameObject WarlockItem;
		public static GameObject EngineerItem;
		public static GameObject PriestItem;
		public static GameObject SpartanItem;

		public static GameObject ShieldGSkull;
		public static GameObject ShieldBGSkull;
		public static GameObject BhygshanMace;
		public static GameObject ShieldEikthyr;
		public static GameObject ShieldRambore;
		public static GameObject ShieldElder;
		public static GameObject ShieldBitter;
		public static GameObject ShieldBonemass;
		public static GameObject ShieldModer;
		public static GameObject ShieldFarkas;
		public static GameObject ShieldSkir;
		public static GameObject ShieldYagluth;
		public static GameObject ShieldBEikthyr;
		public static GameObject ShieldBRambore;
		public static GameObject ShieldBElder;
		public static GameObject ShieldBBitter;
		public static GameObject ShieldBBonemass;
		public static GameObject ShieldBModer;
		public static GameObject ShieldBFarkas;
		public static GameObject ShieldBSkir;
		public static GameObject ShieldBYagluth;

		public static GameObject BowBlackForest;
		public static GameObject BowSwamp;
		public static GameObject BowMountain;
		public static GameObject BowPlains;
		public static GameObject BowMistlands;
		public static GameObject BowDeepNorth;

		public static GameObject SwordMeadows;
		public static GameObject SwordSwamp;
		public static GameObject SwordPlains;
		public static GameObject SwordMistlands;
		public static GameObject SwordDeepNorth;
		public static GameObject SwordAshLands;

		public static GameObject WandMountains;
		public static GameObject MaceMistlands;
		public static GameObject MaceDeepNorth;

		public static GameObject SwordMoonlight;
		public static GameObject SteelPick;
		public static GameObject OakWood;

		public ConfigEntry<bool> AModEnable;
		public ConfigEntry<bool> ArmorCrateEnable;
		public ConfigEntry<bool> WeaponCrateEnable;
		public ConfigEntry<bool> ClassWeaponEnable;
		public ConfigEntry<bool> WeaponsEnable;
		public ConfigEntry<bool> BossesEnable;
		public ConfigEntry<bool> DeepNorthLocations;
		public ConfigEntry<bool> MistlandsLocations;

		public AssetBundle DoDAssets; 
		public void CreateConfigurationValues()
		{
			AModEnable = base.Config.Bind("Enable Mod", "Enable", defaultValue: true, new ConfigDescription("Enables the mod", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			ArmorCrateEnable = base.Config.Bind("Armor Kits", "Enable", defaultValue: true, new ConfigDescription("Enables Armor Kits, if you disable these you will have to disable bosses below", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			WeaponCrateEnable = base.Config.Bind("Weapon Kits", "Enable", defaultValue: true, new ConfigDescription("Enables Weapon Kits, if you disable these you will have to disable all the Weapons below", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			ClassWeaponEnable = base.Config.Bind("Weapons - Magic Overhaul Themed", "Enable", defaultValue: true, new ConfigDescription("Enables Magic Overhaul Themed Class Weapons and items, requires Weapon Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			WeaponsEnable = base.Config.Bind("Weapons", "Enable", defaultValue: true, new ConfigDescription("Enables Weapons, requires Weapon Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BossesEnable = base.Config.Bind("Shields", "Enable", defaultValue: true, new ConfigDescription("Shields, requires Armor Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DeepNorthLocations = base.Config.Bind("Deep North Ore Location", "Enable", defaultValue: true, new ConfigDescription("A location consisting of a large deposit of Valhallium", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MistlandsLocations = base.Config.Bind("Mistlands Ore Location", "Enable", defaultValue: true, new ConfigDescription("A location consisting of a large deposit of Adamantine", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
		{
			try
			{
				CreateConfigurationValues();
				if (AModEnable.Value == true)
				{
					Debug.Log("DoDItems: Loading and Creating Assets");
					LoadBundle();
					LoadDoDAssets();
					CreateOakWood();
					UpdateBlastFurnace();
					CreateDropables();
					CreatePickAxe();
					CreateAnvils(); 
					AddCustomOreDeposits();
					if (ArmorCrateEnable.Value == true) {
						CreateArmorCrates(); 
					}
					if (WeaponCrateEnable.Value == true) {
						CreateWeaponCrates(); 
					}
					if (ClassWeaponEnable.Value == true) {
						CreateClassWeapons(); 
					}
					if (WeaponsEnable.Value == true) {
						CreateArrows();
						CreateMaces();
						CreateWands();
						CreateMagicSwords();
						CreateBows();
						CreateSwords(); 
					}
					if (BossesEnable.Value == true) {
						CustomItem shields = ItemManager.Instance.GetItem("BrokenShieldBhygshan_DoD");
						if (shields != null)
						{
							Debug.Log("Shields already added by DoD Shields");
						}
						else
						{
							CreateTierItems();
							CreateShields();
						}
					}
					UnloadBundle();
					ZoneManager.OnVanillaLocationsAvailable += AddLocations;
					_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDItems");
				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding DoD Items: {ex}");
			}
		}
		public void LoadBundle()
		{
			DoDAssets = AssetUtils.LoadAssetBundleFromResources("doditems", Assembly.GetExecutingAssembly());
		}
		private void LoadDoDAssets()
		{
			Debug.Log("DoDItems: Anvils");
			TexFlaAnvil = DoDAssets.LoadAsset<Sprite>("FlaAnvil_Icon_DoD");
			TexFroAnvil = DoDAssets.LoadAsset<Sprite>("FroAnvil_Icon_DoD");
			TexFelAnvil = DoDAssets.LoadAsset<Sprite>("FelAnvil_Icon_DoD");
			AnvilsFel = DoDAssets.LoadAsset<GameObject>("FelmetalAnvils_DoD");
			AnvilsFro = DoDAssets.LoadAsset<GameObject>("FrometalAnvils_DoD");
			AnvilsFlam = DoDAssets.LoadAsset<GameObject>("FlametalAnvils_DoD");
			// ores
			Debug.Log("DoDItems: Ores");
			MineRock_FroOre_DoD = DoDAssets.LoadAsset<GameObject>("MineRock_FroOre_DoD");
			MineRock_FelOre_DoD = DoDAssets.LoadAsset<GameObject>("MineRock_FelOre_DoD");
			SteelPick = DoDAssets.LoadAsset<GameObject>("SteelPickaxe_DoD");
			Debug.Log("DoDItems: Broken Shields");
			// Broken Shields
			ShieldBGSkull = DoDAssets.LoadAsset<GameObject>("BrokenShieldBhygshan_DoD");
			ShieldBEikthyr = DoDAssets.LoadAsset<GameObject>("BrokenShieldEikthyr_DoD");
			ShieldBRambore = DoDAssets.LoadAsset<GameObject>("BrokenShieldRambore_DoD");
			ShieldBElder = DoDAssets.LoadAsset<GameObject>("BrokenShieldElder_DoD");
			ShieldBBitter = DoDAssets.LoadAsset<GameObject>("BrokenShieldBitterstump_DoD");
			ShieldBBonemass = DoDAssets.LoadAsset<GameObject>("BrokenShieldBonemass_DoD");
			ShieldBModer = DoDAssets.LoadAsset<GameObject>("BrokenShieldModer_DoD");
			ShieldBFarkas = DoDAssets.LoadAsset<GameObject>("BrokenShieldFarkas_DoD");
			ShieldBSkir = DoDAssets.LoadAsset<GameObject>("BrokenShieldSkir_DoD");
			ShieldBYagluth = DoDAssets.LoadAsset<GameObject>("BrokenShieldYagluth_DoD");
			Debug.Log("DoDItems: Shields");
			// Shields
			ShieldGSkull = DoDAssets.LoadAsset<GameObject>("ShieldSkullGreen_DoD");
			ShieldEikthyr = DoDAssets.LoadAsset<GameObject>("ShieldEikthyr_DoD");
			ShieldRambore = DoDAssets.LoadAsset<GameObject>("ShieldRambore_DoD");
			ShieldElder = DoDAssets.LoadAsset<GameObject>("ShieldElder_DoD");
			ShieldBitter = DoDAssets.LoadAsset<GameObject>("ShieldBitterstump_DoD");
			ShieldBonemass = DoDAssets.LoadAsset<GameObject>("ShieldBonemass_DoD");
			ShieldModer = DoDAssets.LoadAsset<GameObject>("ShieldModer_DoD");
			ShieldFarkas = DoDAssets.LoadAsset<GameObject>("ShieldFarkas_DoD");
			ShieldSkir = DoDAssets.LoadAsset<GameObject>("ShieldSkir_DoD");
			ShieldYagluth = DoDAssets.LoadAsset<GameObject>("ShieldYagluth_DoD");
			Debug.Log("DoDItems: Weapons");
			// Weapons
			SwordMoonlight = DoDAssets.LoadAsset<GameObject>("MoonSword_DoD");
			BhygshanMace = DoDAssets.LoadAsset<GameObject>("BhygshanMace_DoD");
			BowBlackForest = DoDAssets.LoadAsset<GameObject>("Bow_BlackForest_DoD");
			BowSwamp = DoDAssets.LoadAsset<GameObject>("Bow_Swamp_DoD");
			BowMountain = DoDAssets.LoadAsset<GameObject>("Bow_Mountain_DoD");
			BowPlains = DoDAssets.LoadAsset<GameObject>("Bow_Plains_DoD");
			BowMistlands = DoDAssets.LoadAsset<GameObject>("Bow_Mistlands_DoD");
			BowDeepNorth = DoDAssets.LoadAsset<GameObject>("Bow_DeepNorth_DoD");
			SwordMeadows = DoDAssets.LoadAsset<GameObject>("Sword_Meadows_DoD");
			SwordSwamp = DoDAssets.LoadAsset<GameObject>("Sword_Swamp_DoD");
			SwordPlains = DoDAssets.LoadAsset<GameObject>("Sword_Plains_DoD");
			SwordMistlands = DoDAssets.LoadAsset<GameObject>("Sword_Mistlands_DoD");
			SwordDeepNorth = DoDAssets.LoadAsset<GameObject>("Sword_DeepNorth_DoD");
			SwordAshLands = DoDAssets.LoadAsset<GameObject>("Sword_AshLands_DoD");
			WandMountains = DoDAssets.LoadAsset<GameObject>("Wand_Mountain_DoD");
			MaceMistlands = DoDAssets.LoadAsset<GameObject>("Mace_Mistlands_DoD");
			MaceDeepNorth = DoDAssets.LoadAsset<GameObject>("Mace_DeepNorth_DoD");
			Debug.Log("DoDItems: Silver Swords");
			// Sword Assets
			Firesoul = DoDAssets.LoadAsset<GameObject>("SwordFlametal1_DoD");
			Solarflare = DoDAssets.LoadAsset<GameObject>("SwordFlametal2_DoD");
			Reaper = DoDAssets.LoadAsset<GameObject>("SwordFelmetal1_DoD");
			Desolation = DoDAssets.LoadAsset<GameObject>("SwordFelmetal2_DoD");
			Soulstorm = DoDAssets.LoadAsset<GameObject>("Stormblade_DoD");
			Silverlight = DoDAssets.LoadAsset<GameObject>("SwordFrometal2_DoD");
			Coldflame = DoDAssets.LoadAsset<GameObject>("SwordFrometal1_DoD");
			Frostflame = DoDAssets.LoadAsset<GameObject>("SwordFrometal3_DoD");
			Nethersbane = DoDAssets.LoadAsset<GameObject>("SwordSpirit_DoD");
			Debug.Log("DoDItems: Magic Overhaul Weapons");
			// Class Weapon Assets
			WandLightning = DoDAssets.LoadAsset<GameObject>("ShamanWand_DoD");
			WandFire = DoDAssets.LoadAsset<GameObject>("MageWand_DoD");
			WandShadow = DoDAssets.LoadAsset<GameObject>("WarlockWand_DoD");
			MaceDivine = DoDAssets.LoadAsset<GameObject>("PaladinMace_DoD");
			SwordAssassin = DoDAssets.LoadAsset<GameObject>("RogueSword_DoD");
			AxeKnight = DoDAssets.LoadAsset<GameObject>("DeathknightAxe_DoD");
			SwordVoid = DoDAssets.LoadAsset<GameObject>("NinjaSword_DoD");
			SpearAcid = DoDAssets.LoadAsset<GameObject>("DruidSpear_DoD");
			AxeBattle = DoDAssets.LoadAsset<GameObject>("BerserkerAxe_DoD");
			MaceShock = DoDAssets.LoadAsset<GameObject>("MonkMace_DoD");
			SpearAoE = DoDAssets.LoadAsset<GameObject>("AoE_AuraHealing_DoD");
			SpartanBlade = DoDAssets.LoadAsset<GameObject>("SpartanSword_DoD");
			HolyStaff = DoDAssets.LoadAsset<GameObject>("PriestStaff_DoD");
			Debug.Log("DoDItems: Armor Kits");
			// Armor Kit Assets
			T1ArmorKit = DoDAssets.LoadAsset<GameObject>("CrudeArmorKit_DoD");
			T2ArmorKit = DoDAssets.LoadAsset<GameObject>("BasicArmorKit_DoD");
			T3ArmorKit = DoDAssets.LoadAsset<GameObject>("GoodArmorKit_DoD");
			T4ArmorKit = DoDAssets.LoadAsset<GameObject>("GreatArmorKit_DoD");
			T5ArmorKit = DoDAssets.LoadAsset<GameObject>("SuperiorArmorKit_DoD");
			T6ArmorKit = DoDAssets.LoadAsset<GameObject>("ExcellentArmorKit_DoD");
			T7ArmorKit = DoDAssets.LoadAsset<GameObject>("ExceptionalArmorKit_DoD");
			T8ArmorKit = DoDAssets.LoadAsset<GameObject>("ExtraordinaryArmorKit_DoD");
			Debug.Log("DoDItems: Weapon Kits");
			// Weapon Kit Assets
			T1WeaponKit = DoDAssets.LoadAsset<GameObject>("CrudeWeaponKit_DoD");
			T2WeaponKit = DoDAssets.LoadAsset<GameObject>("BasicWeaponKit_DoD");
			T3WeaponKit = DoDAssets.LoadAsset<GameObject>("GoodWeaponKit_DoD");
			T4WeaponKit = DoDAssets.LoadAsset<GameObject>("GreatWeaponKit_DoD");
			T5WeaponKit = DoDAssets.LoadAsset<GameObject>("SuperiorWeaponKit_DoD");
			T6WeaponKit = DoDAssets.LoadAsset<GameObject>("ExcellentWeaponKit_DoD");
			T7WeaponKit = DoDAssets.LoadAsset<GameObject>("ExceptionalWeaponKit_DoD");
			T8WeaponKit = DoDAssets.LoadAsset<GameObject>("ExtraordinaryWeaponKit_DoD");
			Debug.Log("DoDItems: Magic Overhaul Items");
			// Material Assets
			EngineerItem = DoDAssets.LoadAsset<GameObject>("EngineerItem_DoD");
			DeathKnightItem = DoDAssets.LoadAsset<GameObject>("DeathKnightItem_DoD");
			ArcherItem = DoDAssets.LoadAsset<GameObject>("ArcherItem_DoD");
			BerserkerItem = DoDAssets.LoadAsset<GameObject>("BerserkerItem_DoD");
			DruidItem = DoDAssets.LoadAsset<GameObject>("DruidItem_DoD");
			MageItem = DoDAssets.LoadAsset<GameObject>("MageItem_DoD");
			MonkItem = DoDAssets.LoadAsset<GameObject>("MonkItem_DoD");
			NinjaItem = DoDAssets.LoadAsset<GameObject>("NinjaItem_DoD");
			PaladinItem = DoDAssets.LoadAsset<GameObject>("PaladinItem_DoD");
			RogueItem = DoDAssets.LoadAsset<GameObject>("RogueItem_DoD");
			ShamanItem = DoDAssets.LoadAsset<GameObject>("ShamanItem_DoD");
			WarlockItem = DoDAssets.LoadAsset<GameObject>("WarlockItem_DoD");
			PriestItem = DoDAssets.LoadAsset<GameObject>("PriestItem_DoD");
			SpartanItem = DoDAssets.LoadAsset<GameObject>("SpartanItem_DoD");

			Debug.Log("DoDItems: AoE");
			GameObject AoEDivineMace = DoDAssets.LoadAsset<GameObject>("AoE_DivineMace_DoD");
			GameObject AoEMoonSword = DoDAssets.LoadAsset<GameObject>("AoE_MoonSword_DoD");
			GameObject AoENinjaSword = DoDAssets.LoadAsset<GameObject>("AoE_NinjaSword_DoD");
			GameObject AoERogueSword = DoDAssets.LoadAsset<GameObject>("AoE_RogueSword_DoD");
			CustomPrefab AoE1 = new CustomPrefab(AoEDivineMace, true);
			PrefabManager.Instance.AddPrefab(AoE1);
			CustomPrefab AoE2 = new CustomPrefab(AoEMoonSword, true);
			PrefabManager.Instance.AddPrefab(AoE2);
			CustomPrefab AoE3 = new CustomPrefab(AoENinjaSword, true);
			PrefabManager.Instance.AddPrefab(AoE3);
			CustomPrefab AoE4 = new CustomPrefab(AoERogueSword, true);
			PrefabManager.Instance.AddPrefab(AoE4);

			Debug.Log("DoDItems: SFX");
			GameObject SFX1 = DoDAssets.LoadAsset<GameObject>("IT_loc_sfx_rock_destroyed_dod");
			CustomPrefab sfx1 = new CustomPrefab(SFX1, false);
			PrefabManager.Instance.AddPrefab(sfx1);
			GameObject SFX2 = DoDAssets.LoadAsset<GameObject>("IT_loc_sfx_rock_hit_dod");
			CustomPrefab sfx2 = new CustomPrefab(SFX2, false);
			PrefabManager.Instance.AddPrefab(sfx2);
			GameObject SFX3 = DoDAssets.LoadAsset<GameObject>("IT_SFX_Rock_Destroyed_DoD");
			CustomPrefab sfx3 = new CustomPrefab(SFX3, false);
			PrefabManager.Instance.AddPrefab(sfx3);
			GameObject SFX4 = DoDAssets.LoadAsset<GameObject>("IT_SFX_Rock_Hit_DoD");
			CustomPrefab sfx4 = new CustomPrefab(SFX4, false);
			PrefabManager.Instance.AddPrefab(sfx4);
			Debug.Log("DoDItems: VFX");
			GameObject VFX1 = DoDAssets.LoadAsset<GameObject>("IT_VFX_Felore_Destroy_DoD");
			CustomPrefab vfx1 = new CustomPrefab(VFX1, false);
			PrefabManager.Instance.AddPrefab(vfx1);
			GameObject VFX2 = DoDAssets.LoadAsset<GameObject>("IT_VFX_Mine_Hit_DoD");
			CustomPrefab vfx2 = new CustomPrefab(VFX2, false);
			PrefabManager.Instance.AddPrefab(vfx2);
			GameObject VFX3 = DoDAssets.LoadAsset<GameObject>("IT_VFX_RockDestroyed_DoD");
			CustomPrefab vfx3 = new CustomPrefab(VFX3, false);
			PrefabManager.Instance.AddPrefab(vfx3);
			GameObject VFX4 = DoDAssets.LoadAsset<GameObject>("IT_VFX_RockHit_DoD");
			CustomPrefab vfx4 = new CustomPrefab(VFX4, false);
			PrefabManager.Instance.AddPrefab(vfx4);
			GameObject VFX5 = DoDAssets.LoadAsset<GameObject>("IT_VFX_Hit_DoD");
			CustomPrefab vfx5 = new CustomPrefab(VFX5, false);
			PrefabManager.Instance.AddPrefab(vfx5);
		}
		private void CreateWeaponCrates()
		{
			GameObject T1 = T1WeaponKit;
			CustomItem customItem1 = new CustomItem(T1, fixReference: true, new ItemConfig
			{
				//Name = "Crude Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T2 = T2WeaponKit;
			CustomItem customItem2 = new CustomItem(T2, fixReference: true, new ItemConfig
			{
				//Name = "Basic Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T3 = T3WeaponKit;
			CustomItem customItem3 = new CustomItem(T3, fixReference: true, new ItemConfig
			{
				//Name = "Good Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Silver",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T4 = T4WeaponKit;
			CustomItem customItem4 = new CustomItem(T4, fixReference: true, new ItemConfig
			{
				//Name = "Great Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "BlackMetal",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T5 = T5WeaponKit;
			CustomItem customItem = new CustomItem(T5, fixReference: true, new ItemConfig
			{
				//Name = "Superior Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "SteelBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T6 = T6WeaponKit;
			CustomItem customItem6 = new CustomItem(T6, fixReference: true, new ItemConfig
			{
				//Name = "Excellent Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FelmetalBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T7 = T7WeaponKit;
			CustomItem customItem7 = new CustomItem(T7, fixReference: true, new ItemConfig
			{
				//Name = "Exceptional Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FrometalBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T8 = T8WeaponKit;
			CustomItem customItem8 = new CustomItem(T8, fixReference: true, new ItemConfig
			{
				//Name = "Extraordinary Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Flametal",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
		}
		private void CreateArmorCrates()
		{
			GameObject T1 = T1ArmorKit;
			CustomItem customItem1 = new CustomItem(T1, fixReference: true, new ItemConfig
			{
				//Name = "Crude Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "DeerHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T2 = T2ArmorKit;
			CustomItem customItem2 = new CustomItem(T2, fixReference: true, new ItemConfig
			{
				//Name = "Basic Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "DeerHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T3 = T3ArmorKit;
			CustomItem customItem3 = new CustomItem(T3, fixReference: true, new ItemConfig
			{
				//Name = "Good Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Silver",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T4 = T4ArmorKit;
			CustomItem customItem4 = new CustomItem(T4, fixReference: true, new ItemConfig
			{
				//Name = "Great Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "BlackMetal",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T5 = T5ArmorKit;
			CustomItem customItem = new CustomItem(T5, fixReference: true, new ItemConfig
			{
				//Name = "Superior Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "SteelBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T6 = T6ArmorKit;
			CustomItem customItem6 = new CustomItem(T6, fixReference: true, new ItemConfig
			{
				//Name = "Excellent Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FelmetalBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T7 = T7ArmorKit;
			CustomItem customItem7 = new CustomItem(T7, fixReference: true, new ItemConfig
			{
				//Name = "Exceptional Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FrometalBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T8 = T8ArmorKit;
			CustomItem customItem8 = new CustomItem(T8, fixReference: true, new ItemConfig
			{
				//Name = "Extraordinary Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Flametal",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
		}
		private void CreateClassWeapons()
		{
			GameObject monkmace = MaceShock;
			CustomItem customItem1 = new CustomItem(monkmace, fixReference: true, new ItemConfig
			{
				//Name = "Striking Mace",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "MonkItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: MaceShock");
			GameObject berserkeraxe = AxeBattle;
			CustomItem customItem2 = new CustomItem(berserkeraxe, fixReference: true, new ItemConfig
			{
				//Name = "Raging Battleaxe",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "BerserkerItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: AxeBattle");
			GameObject druidspear = SpearAcid;
			CustomItem customItem3 = new CustomItem(druidspear, fixReference: true, new ItemConfig
			{
				//Name = "Acid Spear",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DruidItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			//Jotunn.Logger.LogMessage("Created Recipie: SpearAcid");
			GameObject shamanwand = WandLightning;
			CustomItem customItem4 = new CustomItem(shamanwand, fixReference: true, new ItemConfig
			{
				//Name = "Lightning Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamanItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: WandLightning");
			GameObject wandmage = WandFire;
			CustomItem customItem5 = new CustomItem(wandmage, fixReference: true, new ItemConfig
			{
				//Name = "Fire Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "MageItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			//Jotunn.Logger.LogMessage("Created Recipie: WandFire");
			GameObject wandwarlock = WandShadow;
			CustomItem customItem6 = new CustomItem(wandwarlock, fixReference: true, new ItemConfig
			{
				//Name = "Shadow Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "WarlockItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: WandShadow");
			GameObject paladinmace = MaceDivine;
			CustomItem customItem7 = new CustomItem(paladinmace, fixReference: true, new ItemConfig
			{
				//Name = "Divine Mace",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "PaladinItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: MaceDivine");
			GameObject roguesword = SwordAssassin;
			CustomItem customItem8 = new CustomItem(roguesword, fixReference: true, new ItemConfig
			{
				//Name = "Assassin's Blade",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "RogueItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: SwordAssassin");
			GameObject knightaxe = AxeKnight;
			CustomItem customItem9 = new CustomItem(knightaxe, fixReference: true, new ItemConfig
			{
				//Name = "Brutal Axe",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DeathKnightItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			//Jotunn.Logger.LogMessage("Created Recipie: AxeKnight");
			GameObject ninjasword = SwordVoid;
			CustomItem customItem10 = new CustomItem(ninjasword, fixReference: true, new ItemConfig
			{
				//Name = "Void Sword",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "NinjaItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem10);
			//Jotunn.Logger.LogMessage("Created Recipie: SwordNinja");
			GameObject prieststaff = HolyStaff;
			CustomItem customItem11 = new CustomItem(prieststaff, fixReference: true, new ItemConfig
			{
				//Name = "Void Sword",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "PriestItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem11);
			//Jotunn.Logger.LogMessage("Created Recipie: HolyStaff");
			GameObject spartansword = SpartanBlade;
			CustomItem customItem12 = new CustomItem(spartansword, fixReference: true, new ItemConfig
			{
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SpartanItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem12);
			//Jotunn.Logger.LogMessage("Created Recipie: SpartanBlade");
		}
		private void CreateMagicSwords()
		{
			GameObject firesoul = Firesoul;
			CustomItem customItem1 = new CustomItem(firesoul, fixReference: false, new ItemConfig
			{
				//Name = "Firesoul",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "Flametal",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Firesoul");
			GameObject solarflare = Solarflare;
			CustomItem customItem2 = new CustomItem(solarflare, fixReference: false, new ItemConfig
			{
				//Name = "Solarflare",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "Flametal",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Solarflare");
			GameObject reaper = Reaper;
			CustomItem customItem3 = new CustomItem(reaper, fixReference: false, new ItemConfig
			{
				//Name = "Reaper",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "VoidlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			//Jotunn.Logger.LogMessage("Created Recipie: Reaper");
			GameObject desolation = Desolation;
			CustomItem customItem4 = new CustomItem(desolation, fixReference: false, new ItemConfig
			{
				//Name = "Desolation",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Desolation");
			GameObject soulstorm = Soulstorm;
			CustomItem customItem5 = new CustomItem(soulstorm, fixReference: false, new ItemConfig
			{
				//Name = "Soulstorm",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			//Jotunn.Logger.LogMessage("Created Recipie: Soulstorm");
			GameObject silverlight = Silverlight;
			CustomItem customItem6 = new CustomItem(silverlight, fixReference: false, new ItemConfig
			{
				//Name = "Silverlight",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Silverlight");
			GameObject coldflame = Coldflame;
			CustomItem customItem7 = new CustomItem(coldflame, fixReference: false, new ItemConfig
			{
				//Name = "Coldflame",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FrometalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Coldflame");
			GameObject frostflame = Frostflame;
			CustomItem customItem8 = new CustomItem(frostflame, fixReference: false, new ItemConfig
			{
				//Name = "Frostflame",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FrometalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Frostflame");
			GameObject nethersbane = Nethersbane;
			CustomItem customItem9 = new CustomItem(nethersbane, fixReference: false, new ItemConfig
			{
				//Name = "Nethersbane",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "VoidlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			//Jotunn.Logger.LogMessage("Created Recipie: Nethersbane");
		}
		private void CreateArrows()
		{
			GameObject arrow3 = DoDAssets.LoadAsset<GameObject>("ArrowMistlands_DoD");
			CustomItem customItem3 = new CustomItem(arrow3, fixReference: true, new ItemConfig
			{
				//Name = "Shocking Arrow",
				Amount = 100,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			GameObject arrow2 = DoDAssets.LoadAsset<GameObject>("ArrowDeepNorth_DoD");
			CustomItem customItem2 = new CustomItem(arrow2, fixReference: true, new ItemConfig
			{
				//Name = "Frosty Arrow",
				Amount = 100,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			GameObject arrow1 = DoDAssets.LoadAsset<GameObject>("ArrowAshLands_DoD");
			CustomItem customItem1 = new CustomItem(arrow1, fixReference: true, new ItemConfig
			{
				//Name = "Fiery Arrow",
				Amount = 100,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "ExtraordinaryWeaponKit_DoD",
					Amount = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateDropables()
		{
			GameObject dropable26 = DeathKnightItem;
			CustomItem customItem26 = new CustomItem(dropable26, fixReference: true);
			ItemManager.Instance.AddItem(customItem26);

			GameObject dropable27 = ArcherItem;
			CustomItem customItem27 = new CustomItem(dropable27, fixReference: true);
			ItemManager.Instance.AddItem(customItem27);

			GameObject dropable28 = BerserkerItem;
			CustomItem customItem28 = new CustomItem(dropable28, fixReference: true);
			ItemManager.Instance.AddItem(customItem28);

			GameObject dropable29 = DruidItem;
			CustomItem customItem29 = new CustomItem(dropable29, fixReference: true);
			ItemManager.Instance.AddItem(customItem29);

			GameObject dropable30 = MageItem;
			CustomItem customItem30 = new CustomItem(dropable30, fixReference: true);
			ItemManager.Instance.AddItem(customItem30);

			GameObject dropable31 = MonkItem;
			CustomItem customItem31 = new CustomItem(dropable31, fixReference: true);
			ItemManager.Instance.AddItem(customItem31);

			GameObject dropable32 = NinjaItem;
			CustomItem customItem32 = new CustomItem(dropable32, fixReference: true);
			ItemManager.Instance.AddItem(customItem32);

			GameObject dropable33 = PaladinItem;
			CustomItem customItem33 = new CustomItem(dropable33, fixReference: true);
			ItemManager.Instance.AddItem(customItem33);

			GameObject dropable34 = RogueItem;
			CustomItem customItem34 = new CustomItem(dropable34, fixReference: true);
			ItemManager.Instance.AddItem(customItem34);

			GameObject dropable35 = ShamanItem;
			CustomItem customItem35 = new CustomItem(dropable35, fixReference: true);
			ItemManager.Instance.AddItem(customItem35);

			GameObject dropable36 = WarlockItem;
			CustomItem customItem36 = new CustomItem(dropable36, fixReference: true);
			ItemManager.Instance.AddItem(customItem36);

			GameObject dropable56 = EngineerItem;
			CustomItem customItem56 = new CustomItem(dropable56, fixReference: true);
			ItemManager.Instance.AddItem(customItem56);

			GameObject dropable57 = PriestItem;
			CustomItem customItem57 = new CustomItem(dropable57, fixReference: true);
			ItemManager.Instance.AddItem(customItem57);

			GameObject dropable58 = SpartanItem;
			CustomItem customItem58 = new CustomItem(dropable58, fixReference: true);
			ItemManager.Instance.AddItem(customItem58);

		}
		private void CreateTierItems()
        {
			GameObject tieritem1 = ShieldBGSkull;
			CustomItem customItem1 = new CustomItem(tieritem1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject tieritem2 = ShieldBEikthyr;
			CustomItem customItem2 = new CustomItem(tieritem2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject tieritem3 = ShieldBRambore;
			CustomItem customItem3 = new CustomItem(tieritem3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject tieritem4 = ShieldBElder;
			CustomItem customItem4 = new CustomItem(tieritem4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject tieritem5 = ShieldBBitter;
			CustomItem customItem5 = new CustomItem(tieritem5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);
			GameObject tieritem6 = ShieldBBonemass;
			CustomItem customItem6 = new CustomItem(tieritem6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);
			GameObject tieritem7 = ShieldBModer;
			CustomItem customItem7 = new CustomItem(tieritem7, fixReference: true);
			ItemManager.Instance.AddItem(customItem7);
			GameObject tieritem8 = ShieldBFarkas;
			CustomItem customItem8 = new CustomItem(tieritem8, fixReference: true);
			ItemManager.Instance.AddItem(customItem8);
			GameObject tieritem9 = ShieldBSkir;
			CustomItem customItem9 = new CustomItem(tieritem9, fixReference: true);
			ItemManager.Instance.AddItem(customItem9);
			GameObject tieritem10 = ShieldBYagluth;
			CustomItem customItem10 = new CustomItem(tieritem10, fixReference: true);
			ItemManager.Instance.AddItem(customItem10);
		}
		private void CreateShields()
		{
			GameObject shield10 = ShieldYagluth;
			CustomItem customItem10 = new CustomItem(shield10, fixReference: true, new ItemConfig
			{
				//Name = "Deathgate",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldYagluth_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem10);
			GameObject shield9 = ShieldSkir;
			CustomItem customItem9 = new CustomItem(shield9, fixReference: true, new ItemConfig
			{
				//Name = "Curator Ward",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldSkir_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			GameObject shield8 = ShieldFarkas;
			CustomItem customItem8 = new CustomItem(shield8, fixReference: true, new ItemConfig
			{
				//Name = "Frozen Light",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldFarkas_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			GameObject shield7 = ShieldModer;
			CustomItem customItem7 = new CustomItem(shield7, fixReference: true, new ItemConfig
			{
				//Name = "Perfect Storm",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldModer_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DragonEgg",
					Amount = 1,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			GameObject shield6 = ShieldBonemass;
			CustomItem customItem6 = new CustomItem(shield6, fixReference: true, new ItemConfig
			{
				//Name = "Ghostly Wall",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBonemass_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			GameObject shield5 = ShieldBitter;
			CustomItem customItem5 = new CustomItem(shield5, fixReference: true, new ItemConfig
			{
				//Name = "Darkheart",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBitterstump_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			GameObject shield4 = ShieldElder;
			CustomItem customItem4 = new CustomItem(shield4, fixReference: true, new ItemConfig
			{
				//Name = "Ironbark",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldElder_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			GameObject shield3 = ShieldRambore;
			CustomItem customItem3 = new CustomItem(shield3, fixReference: true, new ItemConfig
			{
				//Name = "Enforcer",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldRambore_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "CrudeArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			GameObject shield2 = ShieldEikthyr;
			CustomItem customItem2 = new CustomItem(shield2, fixReference: true, new ItemConfig
			{
				//Name = "Thundercloud",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldEikthyr_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "CrudeArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			GameObject shield1 = ShieldGSkull;
			CustomItem customItem1 = new CustomItem(shield1, fixReference: true, new ItemConfig
			{
				//Name = "Vortex, Conservator of the Dead",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBhygshan_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Wishbone",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateBows()
		{
			GameObject bow6 = BowDeepNorth;
			CustomItem customItem6 = new CustomItem(bow6, fixReference: true, new ItemConfig
			{
				//Name = "Siren's Song",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);

			GameObject bow5 = BowMistlands;
			CustomItem customItem5 = new CustomItem(bow5, fixReference: true, new ItemConfig
			{
				//Name = "Soulstring",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);

			GameObject bow4 = BowPlains;
			CustomItem customItem4 = new CustomItem(bow4, fixReference: true, new ItemConfig
			{
				//Name = "Stryker",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GreatWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);

			GameObject bow3 = BowMountain;
			CustomItem customItem3 = new CustomItem(bow3, fixReference: true, new ItemConfig
			{
				//Name = "Razorwind",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			GameObject bow2 = BowSwamp;
			CustomItem customItem2 = new CustomItem(bow2, fixReference: true, new ItemConfig
			{
				//Name = "Sting",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SkeletonBones_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject bow1 = BowBlackForest;
			CustomItem customItem1 = new CustomItem(bow1, fixReference: true, new ItemConfig
			{
				//Name = "Hornet",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "GreydwarfHeart_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateSwords()
		{
			GameObject sword7 = SwordMoonlight;
			CustomItem customItem7 = new CustomItem(sword7, fixReference: true, new ItemConfig
			{
				//Name = "Moonlight",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "CrudeWeaponKit_DoD",
						Amount = 1,
						AmountPerLevel = 1
					},
					new RequirementConfig
					{
						Item = "InfusedGemstone_DoD",
						Amount = 5,
						AmountPerLevel = 2
					}
				}
			});
			ItemManager.Instance.AddItem(customItem7);

			GameObject sword6 = SwordAshLands;
			CustomItem customItem6 = new CustomItem(sword6, fixReference: true, new ItemConfig
			{
				//Name = "Hellfire",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);

			GameObject sword5 = SwordDeepNorth;
			CustomItem customItem5 = new CustomItem(sword5, fixReference: true, new ItemConfig
			{
				//Name = "Fate",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 8,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);

			GameObject sword4 = SwordMistlands;
			CustomItem customItem4 = new CustomItem(sword4, fixReference: true, new ItemConfig
			{
				//Name = "Misery",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SpiderChitin_DoD",
					Amount = 6,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);

			GameObject sword3 = SwordPlains;
			CustomItem customItem3 = new CustomItem(sword3, fixReference: true, new ItemConfig
			{
				//Name = "Light's Bane",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GreatWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			GameObject sword2 = SwordSwamp;
			CustomItem customItem2 = new CustomItem(sword2, fixReference: true, new ItemConfig
			{
				//Name = "Ghost Reaver",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SkeletonBones_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject sword1 = SwordMeadows;
			CustomItem customItem1 = new CustomItem(sword1, fixReference: true, new ItemConfig
			{
				//Name = "Betrayer",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "BoarTusk_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateMaces()
		{
			GameObject mace2 = MaceDeepNorth;
			CustomItem customItem2 = new CustomItem(mace2, fixReference: true, new ItemConfig
			{
				//Name = "Silence",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "FrozenBone_DoD",
					Amount = 20,
					AmountPerLevel = 20
				},
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject mace1 = MaceMistlands;
			CustomItem customItem1 = new CustomItem(mace1, fixReference: true, new ItemConfig
			{
				//Name = "Abomination",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 3
				},
				new RequirementConfig
				{
					Item = "SpiderChitin_DoD",
					Amount = 10,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);

		}
		private void CreateWands()
		{
			GameObject wand1 = WandMountains;
			CustomItem customItem1 = new CustomItem(wand1, fixReference: true, new ItemConfig
			{
				//Name = "Snowfall",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void UpdateBlastFurnace()
		{
            try
			{
				GameObject itemPrefab = DoDAssets.LoadAsset<GameObject>("SteelBar_DoD");
				CustomItem customItem = new CustomItem(itemPrefab, fixReference: false);
				ItemManager.Instance.AddItem(customItem);

				CustomItemConversion itemConversion = new CustomItemConversion(new SmelterConversionConfig
				{
					Station = "blastfurnace",
					FromItem = "Iron",
					ToItem = "SteelBar_DoD"
				});
				ItemManager.Instance.AddItemConversion(itemConversion);

				GameObject itemPrefab2 = DoDAssets.LoadAsset<GameObject>("FrometalOre_DoD");
				GameObject itemPrefab3 = DoDAssets.LoadAsset<GameObject>("FrometalBar_DoD");
				CustomItem customItem2 = new CustomItem(itemPrefab2, fixReference: false);
				CustomItem customItem3 = new CustomItem(itemPrefab3, fixReference: false);
				ItemManager.Instance.AddItem(customItem2);
				ItemManager.Instance.AddItem(customItem3);
				CustomItemConversion itemConversion2 = new CustomItemConversion(new SmelterConversionConfig
				{
					Station = "blastfurnace",
					FromItem = "FrometalOre_DoD",
					ToItem = "FrometalBar_DoD"
				});
				ItemManager.Instance.AddItemConversion(itemConversion2);

				GameObject itemPrefab4 = DoDAssets.LoadAsset<GameObject>("FelmetalOre_DoD");
				GameObject itemPrefab5 = DoDAssets.LoadAsset<GameObject>("FelmetalBar_DoD");
				CustomItem customItem4 = new CustomItem(itemPrefab4, fixReference: false);
				CustomItem customItem5 = new CustomItem(itemPrefab5, fixReference: false);
				ItemManager.Instance.AddItem(customItem4);
				ItemManager.Instance.AddItem(customItem5);
				CustomItemConversion itemConversion3 = new CustomItemConversion(new SmelterConversionConfig
				{
					Station = "blastfurnace",
					FromItem = "FelmetalOre_DoD",
					ToItem = "FelmetalBar_DoD"
				});
				ItemManager.Instance.AddItemConversion(itemConversion3);

			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Ores and Metals: {ex}");
			}
		}
		private void AddCustomOreDeposits()
		{
            try
			{
				Debug.Log("DoDItems: Ore Deposits");
				CustomVegetation customFroOre = new CustomVegetation(MineRock_FroOre_DoD, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 1,
					GroupSizeMax = 2,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.DeepNorth,
					MinAltitude = 1f,
					MaxTilt = 30f
				});
				ZoneManager.Instance.AddCustomVegetation(customFroOre);
				CustomVegetation customFelOre = new CustomVegetation(MineRock_FelOre_DoD, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 1,
					GroupSizeMax = 2,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 15f,
					MaxTilt = 30f
				});
				ZoneManager.Instance.AddCustomVegetation(customFelOre);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Ore Deposits: {ex}");
			}
		}
		private void CreatePickAxe()
		{
            try
			{
				GameObject P1 = SteelPick;
				CustomItem customItem1 = new CustomItem(P1, fixReference: true, new ItemConfig
				{
					Name = "Steel Pickaxe",
					Amount = 1,
					CraftingStation = "forge",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "SteelBar_DoD",
							Amount = 25
						},
						new RequirementConfig
						{
							Item = "OakWood_DoD",
							Amount = 10
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Pick's: {ex}");
			}
		}
		private void CreateAnvils()
		{
            try
			{
				GameObject gameObject1 = AnvilsFlam;
				CustomPiece customPiece1 = new CustomPiece(gameObject1, true, new PieceConfig
				{
					Description = "Increases Forge level by one",
					Icon = TexFlaAnvil,
					PieceTable = "Hammer",
					Category = "Crafting",
					Requirements = new RequirementConfig[3]
					{
					new RequirementConfig
					{
						Item = "Flametal",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SurtlingCore",
						Amount = 2,
						Recover = true
					}
					}
				});
				PieceManager.Instance.AddPiece(customPiece1);

				GameObject gameObject2 = AnvilsFro;
				CustomPiece customPiece2 = new CustomPiece(gameObject2, true, new PieceConfig
				{
					Description = "Increases Forge level by one",
					Icon = TexFroAnvil,
					PieceTable = "Hammer",
					Category = "Crafting",
					Requirements = new RequirementConfig[3]
					{
					new RequirementConfig
					{
						Item = "FrometalBar_DoD",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "FrostlingCore_DoD",
						Amount = 2,
						Recover = true
					}
					}
				});
				PieceManager.Instance.AddPiece(customPiece2);

				GameObject gameObject3 = AnvilsFel;
				CustomPiece customPiece3 = new CustomPiece(gameObject3, true, new PieceConfig
				{
					Description = "Increases Forge level by one",
					Icon = TexFelAnvil,
					PieceTable = "Hammer",
					Category = "Crafting",
					Requirements = new RequirementConfig[3]
					{
					new RequirementConfig
					{
						Item = "FelmetalBar_DoD",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "VoidlingCore_DoD",
						Amount = 2,
						Recover = true
					}
					}
				});
				PieceManager.Instance.AddPiece(customPiece3);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Anvils: {ex}");
			}
		}
		private void CreateOakWood()
		{
			try
			{
				CustomItem ow = ItemManager.Instance.GetItem("OakWood_DoD");
				if (ow != null)
				{
					Debug.Log("OakWood already added by DoD Biomes");
				}
				else
                {
					// Add Oak Item
					OakWood = DoDAssets.LoadAsset<GameObject>("OakWood_DoD");
					GameObject dropable1 = OakWood;
					CustomItem customItem1 = new CustomItem(dropable1, true);
					ItemManager.Instance.AddItem(customItem1);
                }
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Oak material: {ex}");
			}
		}
		private void AddLocations()
		{
			Debug.Log("DoDItems: Locations");
			DoDAssets = AssetUtils.LoadAssetBundleFromResources("doditems", Assembly.GetExecutingAssembly());
			try
			{
				if (DeepNorthLocations.Value == true)
				{
					var FroOreMine = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_FroOreMine_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(FroOreMine, true, new LocationConfig
					{
						Biome = Heightmap.Biome.DeepNorth,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
						MinDistanceFromSimilar = 250f,
					}));
				}
				if (MistlandsLocations.Value == true)
				{
					var MistLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_OreMine_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistLoc2, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
						MinDistanceFromSimilar = 300f,
					}));
				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Ore Locations: {ex}");
			}
			finally
			{
				DoDAssets.Unload(false);
			}
		}
		private void UnloadBundle()
		{
			DoDAssets?.Unload(unloadAllLoadedObjects: false);
		}
	}
}