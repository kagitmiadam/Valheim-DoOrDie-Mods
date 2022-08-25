﻿using System.Collections.Generic;
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
using UnityEngine.SceneManagement;

namespace DoOrDieBiomes
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDBiomes : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDBiomes";

		public const string PluginName = "DoOrDieBiomes";

		public const string PluginVersion = "0.1.1";

		public static bool isModded = true;
		
		// Mistlands Veg
		public static GameObject BlueMushroom_DoD;
		public static GameObject PurpleMushroom_DoD;
		public static GameObject Tree_Willow02_DoD;
		public static GameObject Tree_Willow01_DoD;
		public static GameObject Tree_Poplar02_DoD;
		public static GameObject Tree_Poplar01_DoD;
		public static GameObject Tree_OldOak02_DoD;
		public static GameObject Mineable_RockMS_DoD;
		public static GameObject Mineable_RockMM_DoD;
		public static GameObject Mineable_RockML_DoD;
		public static GameObject Mineable_RockMH_DoD;
		public static GameObject Tree_OldOak01_DoD;
		public static GameObject Tree_Oak02_DoD;
		public static GameObject Tree_Oak01_DoD;
		public static GameObject Bush_02_DoD;
		public static GameObject Bush_01_DoD;
		public static GameObject Mineable_RockMRFL_DoD;
		public static GameObject Mineable_RockMRFM_DoD;
		public static GameObject Flora_LargeBroad_DoD;
		public static GameObject Flora_SmallMulti_B_DoD;
		public static GameObject Flora_LargeSingle_DoD;
		public static GameObject Flora_MediumSingle_DoD;
		public static GameObject Flora_Large_DoD;
		public static GameObject Flora_LargeTrio_DoD;
		public static GameObject Flora_LargeDuo_DoD;
		// Deep North Veg
		public static GameObject Bush3_DeepNorth_DoD;
		public static GameObject Bush2_DeepNorth_DoD;
		public static GameObject Bush1_DeepNorth_DoD;
		public static GameObject WinterPine7_DoD;
		public static GameObject Mineable_RockDN10_DoD;
		public static GameObject Mineable_RockDN9_DoD;
		public static GameObject Mineable_RockDN8_DoD;
		public static GameObject Mineable_RockDN7_DoD;
		public static GameObject Mineable_RockDN6_DoD;
		public static GameObject Mineable_RockDN5_DoD;
		public static GameObject Mineable_RockDN4_DoD;
		public static GameObject Mineable_RockDN3_DoD;
		public static GameObject Mineable_RockDN2_DoD;
		public static GameObject Mineable_RockDN1_DoD;
		public static GameObject WinterPine6_DoD;
		public static GameObject WinterPine5_DoD;
		public static GameObject WinterPine4_DoD;
		public static GameObject WinterPine3_DoD;
		public static GameObject WinterPine2_DoD;
		public static GameObject WinterPine1_DoD;
		// Ashlands Veg
		public static GameObject Mineable_SandRock16_DoD;
		public static GameObject Mineable_SandRock15_DoD;
		public static GameObject Mineable_SandRock14_DoD;
		public static GameObject Mineable_SandRock13_DoD;
		public static GameObject Mineable_SandRock12_DoD;
		public static GameObject Mineable_SandRock11_DoD;
		public static GameObject Mineable_SandRock10_DoD;
		public static GameObject Mineable_SandRock9_DoD;
		public static GameObject Mineable_SandRock8_DoD;
		public static GameObject Mineable_SandRock5_DoD;
		public static GameObject Mineable_SandRock4_DoD;
		public static GameObject Mineable_SandRock3_DoD;
		public static GameObject LavaRock1;
		public static GameObject LavaRock2;
		public static GameObject LavaRock3;
		public static GameObject LavaRock4;
		public static GameObject LavaRock5;
		public static GameObject LavaRock6;
		public static GameObject LavaRock7;
		public static GameObject LavaRock8;
		public static GameObject LavaRock9;
		public static GameObject LavaRock10;
		public static GameObject LavaRock11;
		// fruit trees
		public static GameObject Mushroom_Cave_Pickable_DoD;
		public static GameObject CaveMushroom;
		public static GameObject HardLog;
		public static GameObject HardLogHalf;
		public static GameObject PickBlueMushroom;
		public static GameObject PickPurpleMushroom;
		// oak
		public static GameObject OakWood;
		// Config
		public ConfigEntry<bool> DeepNorthLocations;
		public ConfigEntry<bool> DeepNorthVegEnable;
		public ConfigEntry<bool> AshLandsLocations;
		public ConfigEntry<bool> AshLandsVegEnable;
		public ConfigEntry<bool> MistlandsLocEnable;
		public ConfigEntry<bool> MistlandsVegEnable;
		public ConfigEntry<bool> DoDLocEnable;
		public ConfigEntry<bool> FruitEnable;
		public ConfigEntry<bool> KnarrEnable;
		public ConfigEntry<bool> TowerEnable;
		public ConfigEntry<bool> UnderworldEnable;

		// Bundle
		public AssetBundle DoDBiome;
		private Harmony _harmony;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			DeepNorthLocations = base.Config.Bind("Deep North Locations", "Enable", defaultValue: true, new ConfigDescription("Adds Locatio'sn to Deep North", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DeepNorthVegEnable = base.Config.Bind("Deep North Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Deep North Trees, Bushes and Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			AshLandsLocations = base.Config.Bind("Ash Lands Locations", "Enable", defaultValue: true, new ConfigDescription("Adds Location's to Ash Lands", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			AshLandsVegEnable = base.Config.Bind("Ash Lands Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MistlandsLocEnable = base.Config.Bind("Mistlands Locations", "Enable", defaultValue: true, new ConfigDescription("Enables Locations in Mistlands", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MistlandsVegEnable = base.Config.Bind("Mistlands Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Trees, Bushes, Pickables and Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DoDLocEnable = base.Config.Bind("Custom Locations", "Enable", defaultValue: true, new ConfigDescription("Enables Castle Arena, Event Ring and Camp Locations.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			FruitEnable = base.Config.Bind("Custom Resources", "Enable", defaultValue: true, new ConfigDescription("Enables Felmetal Ore, Banana Tree and Apple Tree", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			KnarrEnable = base.Config.Bind("Custom Trader", "Enable", defaultValue: true, new ConfigDescription("Enables Knarr's Tower Location", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			TowerEnable = base.Config.Bind("Towers", "Enable", defaultValue: true, new ConfigDescription("Enables Abandonded Tower Locations", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			UnderworldEnable = base.Config.Bind("Underworld", "Enable", defaultValue: false, new ConfigDescription("Enables the Underworld Locations", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
        {
			CreateConfigurationValues();
			LoadBundle();
			LoadDoDBiomes();
			CreateOakWood();
			if (MistlandsVegEnable.Value == true)
			{
				AddMistlandVegetation();
			}
			if (FruitEnable.Value == true)
			{
				CreateFruit();
			}
			if (DeepNorthVegEnable.Value == true)
			{
				AddDeepNorthVegetation();
			}
			if (AshLandsVegEnable.Value == true)
			{
				AddAshLandsVegetation();
			}
			ZoneManager.OnVanillaLocationsAvailable += AddLocations;
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDBiomes");
		}
		public void LoadBundle()
		{
			DoDBiome = AssetUtils.LoadAssetBundleFromResources("dodbiomes", Assembly.GetExecutingAssembly());
		}
		private void LoadDoDBiomes()
		{
			//Debug.Log("DoDBiomes: Chests");
			GameObject TCMistlands = DoDBiome.LoadAsset<GameObject>("TreasureChest_Mistlands_DoD");
			GameObject TCDeepNorth = DoDBiome.LoadAsset<GameObject>("TreasureChest_DeepNorth_DoD");
			GameObject TCAshLands = DoDBiome.LoadAsset<GameObject>("TreasureChest_AshLands_DoD");
			CustomPrefab TC1 = new CustomPrefab(TCMistlands, true);
			PrefabManager.Instance.AddPrefab(TC1);
			CustomPrefab TC2 = new CustomPrefab(TCDeepNorth, true);
			PrefabManager.Instance.AddPrefab(TC2);
			CustomPrefab TC3 = new CustomPrefab(TCAshLands, true);
			PrefabManager.Instance.AddPrefab(TC3);

			//Debug.Log("DoDBiomes: SFX");
			GameObject SFXRockHit = DoDBiome.LoadAsset<GameObject>("loc_sfx_rock_hit_dod");
			GameObject SFXRockDest = DoDBiome.LoadAsset<GameObject>("loc_sfx_rock_destroyed_dod");
			GameObject SFXBushChop = DoDBiome.LoadAsset<GameObject>("SFX_Bush_Chop_DoD");
			GameObject SFXPickable = DoDBiome.LoadAsset<GameObject>("SFX_Pickable_Pick_DoD");
			GameObject SFXLoc1 = DoDBiome.LoadAsset<GameObject>("SFX_Rock_Destroyed_DoD");
			GameObject SFXLoc2 = DoDBiome.LoadAsset<GameObject>("SFX_Rock_Hit_DoD");
			GameObject SFXTreeFallS = DoDBiome.LoadAsset<GameObject>("SFX_SmallTree_Falling_DoD");
			GameObject SFXTreeFall = DoDBiome.LoadAsset<GameObject>("SFX_Tree_Falling_DoD");
			GameObject SFXWoodChop = DoDBiome.LoadAsset<GameObject>("SFX_Wood_Chop_DoD");
			GameObject SFXWoodDestroy = DoDBiome.LoadAsset<GameObject>("SFX_Wood_Destroyed_DoD");
			CustomPrefab SFX1 = new CustomPrefab(SFXRockHit, false);
			PrefabManager.Instance.AddPrefab(SFX1);
			CustomPrefab SFX2 = new CustomPrefab(SFXRockDest, false);
			PrefabManager.Instance.AddPrefab(SFX2);
			CustomPrefab SFX3 = new CustomPrefab(SFXBushChop, false);
			PrefabManager.Instance.AddPrefab(SFX3);
			CustomPrefab SFX4 = new CustomPrefab(SFXPickable, false);
			PrefabManager.Instance.AddPrefab(SFX4);
			CustomPrefab SFX5 = new CustomPrefab(SFXLoc1, false);
			PrefabManager.Instance.AddPrefab(SFX5);
			CustomPrefab SFX6 = new CustomPrefab(SFXLoc2, false);
			PrefabManager.Instance.AddPrefab(SFX6);
			CustomPrefab SFX7 = new CustomPrefab(SFXTreeFallS, false);
			PrefabManager.Instance.AddPrefab(SFX7);
			CustomPrefab SFX8 = new CustomPrefab(SFXTreeFall, false);
			PrefabManager.Instance.AddPrefab(SFX8);
			CustomPrefab SFX9 = new CustomPrefab(SFXWoodChop, false);
			PrefabManager.Instance.AddPrefab(SFX9);
			CustomPrefab SFX10 = new CustomPrefab(SFXWoodDestroy, false);
			PrefabManager.Instance.AddPrefab(SFX10);

			//Debug.Log("DoDBiomes: VFX");
			GameObject VFXDustPiece = DoDBiome.LoadAsset<GameObject>("VFX_Dust_Piece_DoD");
			GameObject VFXFelOreDestroy = DoDBiome.LoadAsset<GameObject>("VFX_Felore_Destroy_DoD");
			GameObject VFXMineHit = DoDBiome.LoadAsset<GameObject>("VFX_Mine_Hit_DoD");
			GameObject VFXPickable = DoDBiome.LoadAsset<GameObject>("VFX_Pickable_Pick_DoD");
			GameObject VFXRockDestroyed = DoDBiome.LoadAsset<GameObject>("VFX_RockDestroyed_DoD");
			GameObject VFXRockHit = DoDBiome.LoadAsset<GameObject>("VFX_RockHit_DoD");
			GameObject VFXDestroyed = DoDBiome.LoadAsset<GameObject>("VFX_Destroyed_DoD");
			GameObject VFXHit = DoDBiome.LoadAsset<GameObject>("VFX_Hit_DoD");
			CustomPrefab VFX1 = new CustomPrefab(VFXDustPiece, false);
			PrefabManager.Instance.AddPrefab(VFX1);
			CustomPrefab VFX2 = new CustomPrefab(VFXFelOreDestroy, false);
			PrefabManager.Instance.AddPrefab(VFX2);
			CustomPrefab VFX3 = new CustomPrefab(VFXMineHit, false);
			PrefabManager.Instance.AddPrefab(VFX3);
			CustomPrefab VFX4 = new CustomPrefab(VFXPickable, false);
			PrefabManager.Instance.AddPrefab(VFX4);
			CustomPrefab VFX5 = new CustomPrefab(VFXRockDestroyed, false);
			PrefabManager.Instance.AddPrefab(VFX5);
			CustomPrefab VFX6 = new CustomPrefab(VFXRockHit, false);
			PrefabManager.Instance.AddPrefab(VFX6);
			CustomPrefab VFX7 = new CustomPrefab(VFXDestroyed, false);
			PrefabManager.Instance.AddPrefab(VFX7);
			CustomPrefab VFX8 = new CustomPrefab(VFXHit, false);
			PrefabManager.Instance.AddPrefab(VFX8);

			//Debug.Log("DoDBiomes: Items");
			CaveMushroom = DoDBiome.LoadAsset<GameObject>("CaveMushroom_DoD");
			HardLog = DoDBiome.LoadAsset<GameObject>("Hardwood_Log_DoD");
			CustomPrefab Log2 = new CustomPrefab(HardLog, true);
			PrefabManager.Instance.AddPrefab(Log2);
			HardLogHalf = DoDBiome.LoadAsset<GameObject>("Hardwood_LogHalf_DoD");
			CustomPrefab Log1 = new CustomPrefab(HardLogHalf, true);
			PrefabManager.Instance.AddPrefab(Log1);

			// fruit trees
			//Debug.Log("DoDBiomes: Fruit Trees");
			Mushroom_Cave_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Mushroom_Cave_Pickable_DoD");
			PickBlueMushroom = DoDBiome.LoadAsset<GameObject>("BlueMushroom_Pickable_DoD");
			PickPurpleMushroom = DoDBiome.LoadAsset<GameObject>("PurpleMushroom_Pickable_DoD");
			// mistlands veg
			//Debug.Log("DoDBiomes: Mistlands Veg");
			BlueMushroom_DoD = DoDBiome.LoadAsset<GameObject>("BlueMushroom_DoD");
			PurpleMushroom_DoD = DoDBiome.LoadAsset<GameObject>("PurpleMushroom_DoD");
			Tree_Willow02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow02_DoD");
			Tree_Willow01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow01_DoD");
			Tree_Poplar02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar02_DoD");
			Tree_Poplar01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar01_DoD");
			Tree_OldOak02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_OldOak02_DoD");
			Mineable_RockMS_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMS_DoD");
			Mineable_RockMM_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMM_DoD");
			Mineable_RockML_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockML_DoD");
			Mineable_RockMH_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMH_DoD");
			Tree_OldOak01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_OldOak01_DoD");
			Tree_Oak02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Oak02_DoD");
			Tree_Oak01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Oak01_DoD");
			Bush_02_DoD = DoDBiome.LoadAsset<GameObject>("Bush_02_DoD");
			Bush_01_DoD = DoDBiome.LoadAsset<GameObject>("Bush_01_DoD");
			Mineable_RockMRFL_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMRFL_DoD");
			Mineable_RockMRFM_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMRFM_DoD");
			Flora_LargeBroad_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeBroad_DoD");
			Flora_SmallMulti_B_DoD = DoDBiome.LoadAsset<GameObject>("Flora_SmallMulti_B_DoD");
			Flora_LargeSingle_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeSingle_DoD");
			Flora_MediumSingle_DoD = DoDBiome.LoadAsset<GameObject>("Flora_MediumSingle_DoD");
			Flora_Large_DoD = DoDBiome.LoadAsset<GameObject>("Flora_Large_DoD");
			Flora_LargeTrio_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeTrio_DoD");
			Flora_LargeDuo_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeDuo_DoD");
			// deep north
			//Debug.Log("DoDBiomes: Deep North Veg");
			Bush3_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush3_DeepNorth_DoD");
			Bush2_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush2_DeepNorth_DoD");
			Bush1_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush1_DeepNorth_DoD");
			Mineable_RockDN10_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN10_DoD");
			Mineable_RockDN9_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN9_DoD");
			Mineable_RockDN8_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN8_DoD");
			Mineable_RockDN7_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN7_DoD");
			Mineable_RockDN6_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN6_DoD");
			Mineable_RockDN5_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN5_DoD");
			Mineable_RockDN4_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN4_DoD");
			Mineable_RockDN3_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN3_DoD");
			Mineable_RockDN2_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN2_DoD");
			Mineable_RockDN1_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN1_DoD");
			WinterPine7_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine7_DoD");
			WinterPine6_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine6_DoD");
			WinterPine5_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine5_DoD");
			WinterPine4_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine4_DoD");
			WinterPine3_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine3_DoD");
			WinterPine2_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine2_DoD");
			WinterPine1_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine1_DoD");
			// ash lands
			//Debug.Log("DoDBiomes: Ashlands Veg");
			Mineable_SandRock16_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock16_DoD");
			Mineable_SandRock15_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock15_DoD");
			Mineable_SandRock14_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock14_DoD");
			Mineable_SandRock13_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock13_DoD");
			Mineable_SandRock12_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock12_DoD");
			Mineable_SandRock11_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock11_DoD");
			Mineable_SandRock10_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock10_DoD");
			Mineable_SandRock9_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock9_DoD");
			Mineable_SandRock8_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock8_DoD");
			Mineable_SandRock5_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock5_DoD");
			Mineable_SandRock4_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock4_DoD");
			Mineable_SandRock3_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock3_DoD");
			LavaRock1 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock1_DoD");
			LavaRock2 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock2_DoD");
			LavaRock3 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock3_DoD");
			LavaRock4 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock4_DoD");
			LavaRock5 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock5_DoD");
			LavaRock6 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock6_DoD");
			LavaRock7 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock7_DoD");
			LavaRock8 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock8_DoD");
			LavaRock9 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaRock9_DoD");
			LavaRock10 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaGroup1_DoD");
			LavaRock11 = DoDBiome.LoadAsset<GameObject>("Mineable_LavaGroup2_DoD");
			// Runestones
			GameObject Runestone1 = DoDBiome.LoadAsset<GameObject>("RuneTablet_Bhygshan_DoD");
			CustomPrefab Rune1 = new CustomPrefab(Runestone1, false);
			PrefabManager.Instance.AddPrefab(Rune1);
			GameObject Runestone2 = DoDBiome.LoadAsset<GameObject>("RuneTablet_Bitterstump_DoD");
			CustomPrefab Rune2 = new CustomPrefab(Runestone2, false);
			PrefabManager.Instance.AddPrefab(Rune2);
			GameObject Runestone3 = DoDBiome.LoadAsset<GameObject>("RuneTablet_Farkas_DoD");
			CustomPrefab Rune3 = new CustomPrefab(Runestone3, false);
			PrefabManager.Instance.AddPrefab(Rune3);
			GameObject Runestone4 = DoDBiome.LoadAsset<GameObject>("RuneTablet_Skri_DoD");
			CustomPrefab Rune4 = new CustomPrefab(Runestone4, false);
			PrefabManager.Instance.AddPrefab(Rune4);
		}
		private void AddLocations()
		{
			////Debug.Log("DoDMonsters: 35");
			DoDBiome = AssetUtils.LoadAssetBundleFromResources("dodbiomes", Assembly.GetExecutingAssembly());
			try
			{
				if (TowerEnable.Value == true)
				{
					var towerLocA = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_AbandonedTowerL_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(towerLocA, true, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 2f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
					var towerLocB = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_AbandonedTowerS_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(towerLocB, true, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 0.5f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
				}
				if (KnarrEnable.Value == true)
				{
					var knarrLoc = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_KnarrTrader_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(knarrLoc, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 1,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 2f,
						ClearArea = true,
						MinDistance = 500f,
						MaxDistance = 1000f,
					}));
				}
				if (AshLandsLocations.Value == true)
				{
					var HellPlatformA = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformA_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(HellPlatformA, true, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
					var HellPlatformB = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformB_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(HellPlatformB, true, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
					var AshTower = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_AshTower_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AshTower, true, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 5f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
				}
				if (MistlandsLocEnable.Value == true)
				{
					var MistLoc1 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_MistlandsCave_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistLoc1, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 150,
						Priotized = true,
						ExteriorRadius = 10f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
						MinDistanceFromSimilar = 300f,
					}));
				}
				if (DoDLocEnable.Value == true)
				{
					var AnyLoc1 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_CastleArena_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc1, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 1,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 10f,
						ClearArea = true,
						MinDistance = 1000,
					}));
				}
			}
			finally
			{
				DoDBiome.Unload(false);
			}
		}
		private void AddMistlandVegetation()
		{
			////Debug.Log("DoDMonsters: 37");
			var mistlandsVeg = new List<CustomVegetation>
			{
				new CustomVegetation(PickBlueMushroom, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 2,
					GroupSizeMax = 5,
					GroupRadius = 2f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 100f,
					MaxTilt = 45f
				}),
				new CustomVegetation(PickPurpleMushroom, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 2,
					GroupSizeMax = 5,
					GroupRadius = 2f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 100f,
					MaxTilt = 45f
				}),
				new CustomVegetation(Tree_Willow02_DoD, true, new VegetationConfig
				{
					Max = 5f,
					GroupSizeMin = 3,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxAltitude = 750f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Tree_Willow01_DoD, true, new VegetationConfig
				{
					Max = 5f,
					GroupSizeMin = 3,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxAltitude = 1500f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Tree_Poplar02_DoD, true, new VegetationConfig
				{
					Max = 5f,
					GroupSizeMin = 3,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxAltitude = 750f,
					MaxTilt = 40f
				}),
				new CustomVegetation(Tree_Poplar01_DoD, true, new VegetationConfig
				{
					Max = 5f,
					GroupSizeMin = 3,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxAltitude = 1500f,
					MaxTilt = 40f
				}),
				new CustomVegetation(Tree_OldOak02_DoD, true, new VegetationConfig
				{
					Max = 6f,
					GroupSizeMin = 1,
					GroupSizeMax = 2,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 10f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Mineable_RockMS_DoD, true, new VegetationConfig
				{
					Max = 12f,
					GroupSizeMin = 1,
					GroupSizeMax = 5,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Mineable_RockMM_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 1,
					GroupSizeMax = 5,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Mineable_RockML_DoD, true, new VegetationConfig
				{
					Max = 8f,
					GroupSizeMin = 1,
					GroupSizeMax = 5,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Mineable_RockMH_DoD, true, new VegetationConfig
				{
					Max = 6f,
					GroupSizeMin = 1,
					GroupSizeMax = 5,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Tree_OldOak01_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 1,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 10f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Tree_Oak02_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 1,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 10f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Tree_Oak01_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 1,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 5f,
					MaxTilt = 30f
				}),
				new CustomVegetation(Bush_02_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 5f,
					MaxTilt = 45f
				}),
				new CustomVegetation(Bush_01_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxTilt = 45f
				}),
				new CustomVegetation(Mineable_RockMRFL_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxTilt = 45f
				}),
				new CustomVegetation(Mineable_RockMRFM_DoD, true, new VegetationConfig
				{
					Max = 2f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxTilt = 45f
				}),
				new CustomVegetation(Flora_LargeBroad_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_SmallMulti_B_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_LargeSingle_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_MediumSingle_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_Large_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_LargeTrio_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				}),
				new CustomVegetation(Flora_LargeDuo_DoD, true, new VegetationConfig
				{
					Max = 10f,
					GroupSizeMin = 4,
					GroupSizeMax = 10,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 0f,
					MaxAltitude = 300f
				})
			};

			foreach (var veg in mistlandsVeg)
			{
				ZoneManager.Instance.AddCustomVegetation(veg);
			}

		}
		private void AddDeepNorthVegetation()
		{
			////Debug.Log("DoDMonsters: 38");
			CustomVegetation customVegetation20 = new CustomVegetation(Bush3_DeepNorth_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation20);
			CustomVegetation customVegetation19 = new CustomVegetation(Bush2_DeepNorth_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation19);
			CustomVegetation customVegetation18 = new CustomVegetation(Bush1_DeepNorth_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation18);
			CustomVegetation customVegetation17 = new CustomVegetation(WinterPine7_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation17);
			CustomVegetation customVegetation16 = new CustomVegetation(Mineable_RockDN10_DoD, true, new VegetationConfig
			{
				Max = 4f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation16);
			CustomVegetation customVegetation15 = new CustomVegetation(Mineable_RockDN9_DoD, true, new VegetationConfig
			{
				Max = 4f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation15);
			CustomVegetation customVegetation14 = new CustomVegetation(Mineable_RockDN8_DoD, true, new VegetationConfig
			{
				Max = 5f,
				GroupSizeMin = 1,
				GroupSizeMax = 1,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation14);
			CustomVegetation customVegetation13 = new CustomVegetation(Mineable_RockDN7_DoD, true, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation13);
			CustomVegetation customVegetation12 = new CustomVegetation(Mineable_RockDN6_DoD, true, new VegetationConfig
			{
				Max = 5f,
				GroupSizeMin = 1,
				GroupSizeMax = 1,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation12);
			CustomVegetation customVegetation11 = new CustomVegetation(Mineable_RockDN5_DoD, true, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation11);
			CustomVegetation customVegetation10 = new CustomVegetation(Mineable_RockDN4_DoD, true, new VegetationConfig
			{
				Max = 5f,
				GroupSizeMin = 1,
				GroupSizeMax = 1,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation10);
			CustomVegetation customVegetation9 = new CustomVegetation(Mineable_RockDN3_DoD, true, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation9);
			CustomVegetation customVegetation8 = new CustomVegetation(Mineable_RockDN2_DoD, true, new VegetationConfig
			{
				Max = 12f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation8);
			CustomVegetation customVegetation7 = new CustomVegetation(Mineable_RockDN1_DoD, true, new VegetationConfig
			{
				Max = 12f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation7);
			CustomVegetation customVegetation6 = new CustomVegetation(WinterPine6_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 1500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation6);
			CustomVegetation customVegetation5 = new CustomVegetation(WinterPine5_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 1500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation5);
			CustomVegetation customVegetation4 = new CustomVegetation(WinterPine4_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 1500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation4);
			CustomVegetation customVegetation3 = new CustomVegetation(WinterPine3_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 1500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation3);
			CustomVegetation customVegetation2 = new CustomVegetation(WinterPine2_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 3,
				GroupSizeMax = 5,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 2500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation2);
			CustomVegetation customVegetation1 = new CustomVegetation(WinterPine1_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 3,
				GroupSizeMax = 5,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 1f,
				MaxAltitude = 2500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation1);
		}
		private void AddAshLandsVegetation()
		{
			CustomVegetation customVegetation25 = new CustomVegetation(LavaRock11, true, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 2,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation25);
			CustomVegetation customVegetation24 = new CustomVegetation(LavaRock10, true, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 2,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation24);
			CustomVegetation customVegetation23 = new CustomVegetation(LavaRock9, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation23);
			CustomVegetation customVegetation22 = new CustomVegetation(LavaRock8, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation22);
			CustomVegetation customVegetation21 = new CustomVegetation(LavaRock7, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation21);
			CustomVegetation customVegetation20 = new CustomVegetation(LavaRock6, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation20);
			CustomVegetation customVegetation19 = new CustomVegetation(LavaRock5, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation19);
			CustomVegetation customVegetation18 = new CustomVegetation(LavaRock4, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation18);
			CustomVegetation customVegetation17 = new CustomVegetation(LavaRock3, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation17);
			CustomVegetation customVegetation16 = new CustomVegetation(LavaRock2, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation16);
			CustomVegetation customVegetation15 = new CustomVegetation(LavaRock1, true, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation15);
			////Debug.Log("DoDMonsters: 39");
			CustomVegetation customVegetation14 = new CustomVegetation(Mineable_SandRock16_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation14);
			CustomVegetation customVegetation13 = new CustomVegetation(Mineable_SandRock15_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation13);
			CustomVegetation customVegetation12 = new CustomVegetation(Mineable_SandRock14_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation12);
			CustomVegetation customVegetation11 = new CustomVegetation(Mineable_SandRock13_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation11);
			CustomVegetation customVegetation10 = new CustomVegetation(Mineable_SandRock12_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation10);
			CustomVegetation customVegetation9 = new CustomVegetation(Mineable_SandRock11_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation9);
			CustomVegetation customVegetation8 = new CustomVegetation(Mineable_SandRock10_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation8);
			CustomVegetation customVegetation7 = new CustomVegetation(Mineable_SandRock9_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation7);
			CustomVegetation customVegetation6 = new CustomVegetation(Mineable_SandRock8_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			//5
			//4
			CustomVegetation customVegetation3 = new CustomVegetation(Mineable_SandRock5_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation3);
			CustomVegetation customVegetation2 = new CustomVegetation(Mineable_SandRock4_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation2);
			CustomVegetation customVegetation1 = new CustomVegetation(Mineable_SandRock3_DoD, true, new VegetationConfig
			{
				Max = 3f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.AshLands,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation1);
		}
		private void CreateFruit()
		{
			GameObject food3 = BlueMushroom_DoD;
			CustomItem customFood3 = new CustomItem(food3, false);
			ItemManager.Instance.AddItem(customFood3);
			GameObject food4 = PurpleMushroom_DoD;
			CustomItem customFood4 = new CustomItem(food4, false);
			ItemManager.Instance.AddItem(customFood4);
			GameObject food5 = CaveMushroom;
			CustomItem customFood5 = new CustomItem(food5, false);
			ItemManager.Instance.AddItem(customFood5);
		}
		private void CreateOakWood()
		{
			try
			{
				CustomItem ow = ItemManager.Instance.GetItem("OakWood_DoD");
				if (ow != null)
				{
					Debug.Log("OakWood already added by DoD Items");
				}
				else
                {
					// Add Oak Item
					OakWood = DoDBiome.LoadAsset<GameObject>("OakWood_DoD");
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
		private void ModWorldObjects()
		{
			TreeBase prefab1 = PrefabManager.Cache.GetPrefab<TreeBase>("Oak1");
			prefab1.m_minToolTier = 4;
			ItemManager.OnItemsRegistered -= ModWorldObjects;
		}
		private void UnloadBundle()
		{
			DoDBiome?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
