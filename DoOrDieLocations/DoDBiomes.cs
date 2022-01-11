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

		public const string PluginVersion = "0.0.1";
		// Fruit
		public static GameObject Walnuts;
		public static GameObject Cherry;
		public static GameObject Apple;
		public static GameObject Banana;
		// Mistlands Veg
		public static GameObject MineRock_FelOre_DoD;
		public static GameObject BlueMushroom_DoD;
		public static GameObject PurpleMushroom_DoD;
		public static GameObject Tree_Willow02_DoD;
		public static GameObject Tree_Willow01_DoD;
		public static GameObject Tree_Poplar02_DoD;
		public static GameObject Tree_Poplar01_DoD;
		public static GameObject Bush_RedBerries_Pickable_DoD;
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
		public static GameObject Tree_Walnut_Pickable_DoD;
		// Deep North Veg
		public static GameObject MineRock_FroOre_DoD;
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
		// fruit trees
		public static GameObject Tree_Banana_Pickable_DoD;
		public static GameObject Tree_Apple_Pickable_DoD;
		public static GameObject Mushroom_Cave_Pickable_DoD;
		public static GameObject CaveMushroom;
		public static GameObject HardLog;
		public static GameObject HardLogHalf;
		// Config
		public ConfigEntry<bool> DeepNorthLocations;
		public ConfigEntry<bool> DeepNorthVegEnable;
		public ConfigEntry<bool> AshLandsLocations;
		public ConfigEntry<bool> AshLandsVegEnable;
		public ConfigEntry<bool> MistlandsLocEnable;
		public ConfigEntry<bool> MistlandsVegEnable;
		public ConfigEntry<bool> DoDLocEnable;
		public ConfigEntry<bool> FruitEnable;

		// Bundle
		public AssetBundle DoDBiome;
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
		}
		private void Awake()
        {
			CreateConfigurationValues();
			LoadBundle();
			LoadDoDBiomes();
			if (MistlandsVegEnable.Value == true)
			{
				AddMistlandVegetation();
			}
			if (FruitEnable.Value == true)
			{
				AddCustomFruitTrees();
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
		}
		public void LoadBundle()
		{
			DoDBiome = AssetUtils.LoadAssetBundleFromResources("dodbiomes", Assembly.GetExecutingAssembly());
		}
		private void LoadDoDBiomes()
		{
			//Debug.Log("DoDMonsters: 28");
			GameObject FDSpawner = DoDBiome.LoadAsset<GameObject>("Spawner_FireDrake_DoD");
			CustomPrefab spawn1 = new CustomPrefab(FDSpawner, true);
			GameObject AreaSpawner = DoDBiome.LoadAsset<GameObject>("Area_Spawner_DoD");
			CustomPrefab spawn2 = new CustomPrefab(AreaSpawner, true);
			GameObject CampSpawner = DoDBiome.LoadAsset<GameObject>("Camp_Spawner_DoD");
			CustomPrefab spawn3 = new CustomPrefab(CampSpawner, true);
			GameObject LocationSpawner = DoDBiome.LoadAsset<GameObject>("Location_Spawner_DoD");
			CustomPrefab spawn4 = new CustomPrefab(LocationSpawner, true);
			GameObject TraderSpawner = DoDBiome.LoadAsset<GameObject>("Spawn_Trader_DoD");
			CustomPrefab spawn5 = new CustomPrefab(TraderSpawner, true);
			GameObject GSSpawner = DoDBiome.LoadAsset<GameObject>("Spawner_GreaterSurtling_DoD");
			CustomPrefab spawn6 = new CustomPrefab(GSSpawner, true);
			PrefabManager.Instance.AddPrefab(spawn1);
			PrefabManager.Instance.AddPrefab(spawn2);
			PrefabManager.Instance.AddPrefab(spawn3);
			PrefabManager.Instance.AddPrefab(spawn4);
			PrefabManager.Instance.AddPrefab(spawn5);
			PrefabManager.Instance.AddPrefab(spawn6);
			//Debug.Log("DoDMonsters: 29");
			GameObject loca4 = DoDBiome.LoadAsset<GameObject>("Loc_Boss_Bitterstump_DoD");
			CustomPrefab loc4 = new CustomPrefab(loca4, true);
			PrefabManager.Instance.AddPrefab(loc4);
			GameObject loca6 = DoDBiome.LoadAsset<GameObject>("Loc_Boss_Rambore_DoD");
			CustomPrefab loc6 = new CustomPrefab(loca6, true);
			PrefabManager.Instance.AddPrefab(loc6);
			GameObject loca7 = DoDBiome.LoadAsset<GameObject>("Loc_Camp_DoD");
			CustomPrefab loc7 = new CustomPrefab(loca7, true);
			PrefabManager.Instance.AddPrefab(loc7);
			GameObject loca8 = DoDBiome.LoadAsset<GameObject>("Loc_CastleArena_DoD");
			CustomPrefab loc8 = new CustomPrefab(loca8, true);
			PrefabManager.Instance.AddPrefab(loc8);
			GameObject loca11 = DoDBiome.LoadAsset<GameObject>("Loc_OreMine_DoD");
			CustomPrefab loc11 = new CustomPrefab(loca11, true);
			PrefabManager.Instance.AddPrefab(loc11);
			GameObject loca12 = DoDBiome.LoadAsset<GameObject>("Loc_Underworld_DoD");
			CustomPrefab loc12 = new CustomPrefab(loca12, true);
			PrefabManager.Instance.AddPrefab(loc12);
			GameObject loca13 = DoDBiome.LoadAsset<GameObject>("Loc_FroOreMine_DoD");
			CustomPrefab loc13 = new CustomPrefab(loca13, true);
			PrefabManager.Instance.AddPrefab(loc13);
			GameObject loca14 = DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformA_DoD");
			CustomPrefab loc14 = new CustomPrefab(loca14, true);
			PrefabManager.Instance.AddPrefab(loc14);
			GameObject loca15 = DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformB_DoD");
			CustomPrefab loc15 = new CustomPrefab(loca15, true);
			PrefabManager.Instance.AddPrefab(loc15);
			GameObject loca16 = DoDBiome.LoadAsset<GameObject>("Loc_AshTower_DoD");
			CustomPrefab loc16 = new CustomPrefab(loca16, true);
			PrefabManager.Instance.AddPrefab(loc16);
			CaveMushroom = DoDBiome.LoadAsset<GameObject>("CaveMushroom_DoD");
			GameObject MassiveCave = DoDBiome.LoadAsset<GameObject>("MassiveCave_DoD");
			GameObject TopCave = DoDBiome.LoadAsset<GameObject>("TopCave_DoD");
			GameObject MiddleCave = DoDBiome.LoadAsset<GameObject>("MiddleCave_DoD");
			GameObject BotttomCave = DoDBiome.LoadAsset<GameObject>("BottomCave_DoD");
			GameObject CastleGate = DoDBiome.LoadAsset<GameObject>("CastleGate_DoD");
			GameObject CastleWall = DoDBiome.LoadAsset<GameObject>("CastleWall_DoD");
			GameObject CastleStairs = DoDBiome.LoadAsset<GameObject>("CastleWallStairs_DoD");
			GameObject CastleDetailed = DoDBiome.LoadAsset<GameObject>("CastleWall_Detailed_DoD");
			GameObject Leveler = DoDBiome.LoadAsset<GameObject>("LevelGround_DoD");
			GameObject Camp = DoDBiome.LoadAsset<GameObject>("Camp_DoD");
			GameObject SmallCave = DoDBiome.LoadAsset<GameObject>("MiniCave_DoD");
			GameObject Runestone = DoDBiome.LoadAsset<GameObject>("RunestoneRam_DoD");
			GameObject VisirBitter = DoDBiome.LoadAsset<GameObject>("Vegvisir_Bitterstump_DoD");
			GameObject VisirRambone = DoDBiome.LoadAsset<GameObject>("Vegvisir_Rambore_DoD");
			GameObject CaveDeep = DoDBiome.LoadAsset<GameObject>("CaveDeep_DoD");
			GameObject CaveEnter = DoDBiome.LoadAsset<GameObject>("CaveEntrance_DoD");
			GameObject BeechGround = DoDBiome.LoadAsset<GameObject>("BeechGroundCover_DoD");
			PrefabManager.Instance.AddPrefab(TopCave);
			PrefabManager.Instance.AddPrefab(MiddleCave);
			PrefabManager.Instance.AddPrefab(BotttomCave);
			PrefabManager.Instance.AddPrefab(MassiveCave);
			PrefabManager.Instance.AddPrefab(CastleGate);
			PrefabManager.Instance.AddPrefab(CastleWall);
			PrefabManager.Instance.AddPrefab(CastleStairs);
			PrefabManager.Instance.AddPrefab(CastleDetailed);
			PrefabManager.Instance.AddPrefab(Leveler);
			PrefabManager.Instance.AddPrefab(Camp);
			PrefabManager.Instance.AddPrefab(SmallCave);
			PrefabManager.Instance.AddPrefab(Runestone);
			PrefabManager.Instance.AddPrefab(VisirBitter);
			PrefabManager.Instance.AddPrefab(VisirRambone);
			PrefabManager.Instance.AddPrefab(CaveDeep);
			PrefabManager.Instance.AddPrefab(CaveEnter);
			PrefabManager.Instance.AddPrefab(BeechGround);

			//Debug.Log("DoDMonsters: 30");
			GameObject vegvisirUnder = DoDBiome.LoadAsset<GameObject>("Vegvisir_Underworld_DoD");
			CustomPrefab zone1 = new CustomPrefab(vegvisirUnder, false);
			PrefabManager.Instance.AddPrefab(zone1);
			GameObject EventZone2 = DoDBiome.LoadAsset<GameObject>("Eventzone_Rambore_DoD");
			CustomPrefab zone3 = new CustomPrefab(EventZone2, false);
			PrefabManager.Instance.AddPrefab(zone3);
			GameObject EnvZone2 = DoDBiome.LoadAsset<GameObject>("InteriorEnvironmentZone");
			CustomPrefab zone5 = new CustomPrefab(EnvZone2, false);
			PrefabManager.Instance.AddPrefab(zone5);

			HardLog = DoDBiome.LoadAsset<GameObject>("Hardwood_Log_DoD");
			CustomPrefab Log2 = new CustomPrefab(HardLog, true);
			PrefabManager.Instance.AddPrefab(Log2);
			HardLogHalf = DoDBiome.LoadAsset<GameObject>("Hardwood_LogHalf_DoD");
			CustomPrefab Log1 = new CustomPrefab(HardLogHalf, true);
			PrefabManager.Instance.AddPrefab(Log1);

			Walnuts = DoDBiome.LoadAsset<GameObject>("Walnuts_DoD");
			Apple = DoDBiome.LoadAsset<GameObject>("Apple_DoD");
			Cherry = DoDBiome.LoadAsset<GameObject>("Cherries_DoD");
			Banana = DoDBiome.LoadAsset<GameObject>("Banana_DoD");
			// mistlands veg
			//Debug.Log("DoDMonsters: 31");
			MineRock_FelOre_DoD = DoDBiome.LoadAsset<GameObject>("MineRock_FelOre_DoD");
			CustomPrefab FelOre = new CustomPrefab(MineRock_FelOre_DoD, true);
			PrefabManager.Instance.AddPrefab(FelOre);

			BlueMushroom_DoD = DoDBiome.LoadAsset<GameObject>("BlueMushroom_DoD");
			PurpleMushroom_DoD = DoDBiome.LoadAsset<GameObject>("PurpleMushroom_DoD");

			Tree_Willow02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow02_DoD");
			CustomPrefab MTree8 = new CustomPrefab(Tree_Willow02_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree8);
			Tree_Willow01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow01_DoD");
			CustomPrefab MTree7 = new CustomPrefab(Tree_Willow01_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree7);
			Tree_Poplar02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar02_DoD");
			CustomPrefab MTree6 = new CustomPrefab(Tree_Poplar02_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree6);
			Tree_Poplar01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar01_DoD");
			CustomPrefab MTree5 = new CustomPrefab(Tree_Poplar01_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree5);
			Bush_RedBerries_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Bush_RedBerries_Pickable_DoD");
			CustomPrefab MBush3 = new CustomPrefab(Bush_RedBerries_Pickable_DoD, true);
			PrefabManager.Instance.AddPrefab(MBush3);
			Tree_OldOak02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_OldOak02_DoD");
			CustomPrefab MTree4 = new CustomPrefab(Tree_OldOak02_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree4);
			Mineable_RockMS_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMS_DoD");
			CustomPrefab Flora13 = new CustomPrefab(Mineable_RockMS_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora13);
			Mineable_RockMM_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMM_DoD");
			CustomPrefab Flora12 = new CustomPrefab(Mineable_RockMM_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora12);
			Mineable_RockML_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockML_DoD");
			CustomPrefab Flora11 = new CustomPrefab(Mineable_RockML_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora11);
			Mineable_RockMH_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMH_DoD");
			CustomPrefab Flora10 = new CustomPrefab(Mineable_RockMH_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora10);
			Tree_OldOak01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_OldOak01_DoD");
			CustomPrefab MTree3 = new CustomPrefab(Tree_OldOak01_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree3);
			Tree_Oak02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Oak02_DoD");
			CustomPrefab MTree2 = new CustomPrefab(Tree_Oak02_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree2);
			Tree_Oak01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Oak01_DoD");
			CustomPrefab MTree1 = new CustomPrefab(Tree_Oak01_DoD, true);
			PrefabManager.Instance.AddPrefab(MTree1);
			Bush_02_DoD = DoDBiome.LoadAsset<GameObject>("Bush_02_DoD");
			CustomPrefab MBush2 = new CustomPrefab(Bush_02_DoD, true);
			PrefabManager.Instance.AddPrefab(MBush2);
			Bush_01_DoD = DoDBiome.LoadAsset<GameObject>("Bush_01_DoD");
			CustomPrefab MBush1 = new CustomPrefab(Bush_01_DoD, true);
			PrefabManager.Instance.AddPrefab(MBush1);
			Mineable_RockMRFL_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMRFL_DoD");
			CustomPrefab Flora9 = new CustomPrefab(Mineable_RockMRFL_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora9);
			Mineable_RockMRFM_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockMRFM_DoD");
			CustomPrefab Flora8 = new CustomPrefab(Mineable_RockMRFM_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora8);
			Flora_LargeBroad_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeBroad_DoD");
			CustomPrefab Flora7 = new CustomPrefab(Flora_LargeBroad_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora7);
			Flora_SmallMulti_B_DoD = DoDBiome.LoadAsset<GameObject>("Flora_SmallMulti_B_DoD");
			CustomPrefab Flora6 = new CustomPrefab(Flora_SmallMulti_B_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora6);
			Flora_LargeSingle_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeSingle_DoD");
			CustomPrefab Flora5 = new CustomPrefab(Flora_LargeSingle_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora5);
			Flora_MediumSingle_DoD = DoDBiome.LoadAsset<GameObject>("Flora_MediumSingle_DoD");
			CustomPrefab Flora4 = new CustomPrefab(Flora_MediumSingle_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora4);
			Flora_Large_DoD = DoDBiome.LoadAsset<GameObject>("Flora_Large_DoD");
			CustomPrefab Flora3 = new CustomPrefab(Flora_Large_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora3);
			Flora_LargeTrio_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeTrio_DoD");
			CustomPrefab Flora2 = new CustomPrefab(Flora_LargeTrio_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora2);
			Flora_LargeDuo_DoD = DoDBiome.LoadAsset<GameObject>("Flora_LargeDuo_DoD");
			CustomPrefab Flora1 = new CustomPrefab(Flora_LargeDuo_DoD, true);
			PrefabManager.Instance.AddPrefab(Flora1);
			Tree_Walnut_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Walnut_Pickable_DoD");
			CustomPrefab WalTree = new CustomPrefab(Tree_Walnut_Pickable_DoD, true);
			PrefabManager.Instance.AddPrefab(WalTree);

			//Debug.Log("DoDMonsters: 32");
			// deep north
			MineRock_FroOre_DoD = DoDBiome.LoadAsset<GameObject>("MineRock_FroOre_DoD");
			CustomPrefab FroOre = new CustomPrefab(MineRock_FroOre_DoD, true);
			PrefabManager.Instance.AddPrefab(FroOre);
			Bush3_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush3_DeepNorth_DoD");
			CustomPrefab Bush3 = new CustomPrefab(Bush3_DeepNorth_DoD, true);
			PrefabManager.Instance.AddPrefab(Bush3);
			Bush2_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush2_DeepNorth_DoD");
			CustomPrefab Bush2 = new CustomPrefab(Bush2_DeepNorth_DoD, true);
			PrefabManager.Instance.AddPrefab(Bush2);
			Bush1_DeepNorth_DoD = DoDBiome.LoadAsset<GameObject>("Bush1_DeepNorth_DoD");
			CustomPrefab Bush1 = new CustomPrefab(Bush1_DeepNorth_DoD, true);
			PrefabManager.Instance.AddPrefab(Bush1);
			Mineable_RockDN10_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN10_DoD");
			CustomPrefab Rock10 = new CustomPrefab(Mineable_RockDN10_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock10);
			Mineable_RockDN9_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN9_DoD");
			CustomPrefab Rock9 = new CustomPrefab(Mineable_RockDN9_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock9);
			Mineable_RockDN8_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN8_DoD");
			CustomPrefab Rock8 = new CustomPrefab(Mineable_RockDN8_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock8);
			Mineable_RockDN7_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN7_DoD");
			CustomPrefab Rock7 = new CustomPrefab(Mineable_RockDN7_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock7);
			Mineable_RockDN6_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN6_DoD");
			CustomPrefab Rock6 = new CustomPrefab(Mineable_RockDN6_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock6);
			Mineable_RockDN5_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN5_DoD");
			CustomPrefab Rock5 = new CustomPrefab(Mineable_RockDN5_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock5);
			Mineable_RockDN4_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN4_DoD");
			CustomPrefab Rock4 = new CustomPrefab(Mineable_RockDN4_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock4);
			Mineable_RockDN3_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN3_DoD");
			CustomPrefab Rock3 = new CustomPrefab(Mineable_RockDN3_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock3);
			Mineable_RockDN2_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN2_DoD");
			CustomPrefab Rock2 = new CustomPrefab(Mineable_RockDN2_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock2);
			Mineable_RockDN1_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_RockDN1_DoD");
			CustomPrefab Rock1 = new CustomPrefab(Mineable_RockDN1_DoD, true);
			PrefabManager.Instance.AddPrefab(Rock1);
			WinterPine7_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine7_DoD");
			CustomPrefab Tree7 = new CustomPrefab(WinterPine7_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree7);
			WinterPine6_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine6_DoD");
			CustomPrefab Tree6 = new CustomPrefab(WinterPine6_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree6);
			WinterPine5_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine5_DoD");
			CustomPrefab Tree5 = new CustomPrefab(WinterPine5_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree5);
			WinterPine4_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine4_DoD");
			CustomPrefab Tree4 = new CustomPrefab(WinterPine4_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree4);
			WinterPine3_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine3_DoD");
			CustomPrefab Tree3 = new CustomPrefab(WinterPine3_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree3);
			WinterPine2_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine2_DoD");
			CustomPrefab Tree2 = new CustomPrefab(WinterPine2_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree2);
			WinterPine1_DoD = DoDBiome.LoadAsset<GameObject>("WinterPine1_DoD");
			CustomPrefab Tree1 = new CustomPrefab(WinterPine1_DoD, true);
			PrefabManager.Instance.AddPrefab(Tree1);

			//Debug.Log("DoDMonsters: 33");
			// ashlands
			Mineable_SandRock16_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock16_DoD");
			CustomPrefab ARock16 = new CustomPrefab(Mineable_SandRock16_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock16);
			Mineable_SandRock15_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock15_DoD");
			CustomPrefab ARock15 = new CustomPrefab(Mineable_SandRock15_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock15);
			Mineable_SandRock14_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock14_DoD");
			CustomPrefab ARock14 = new CustomPrefab(Mineable_SandRock14_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock14);
			Mineable_SandRock13_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock13_DoD");
			CustomPrefab ARock13 = new CustomPrefab(Mineable_SandRock13_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock13);
			Mineable_SandRock12_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock12_DoD");
			CustomPrefab ARock12 = new CustomPrefab(Mineable_SandRock12_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock12);
			Mineable_SandRock11_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock11_DoD");
			CustomPrefab ARock11 = new CustomPrefab(Mineable_SandRock11_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock11);
			Mineable_SandRock10_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock10_DoD");
			CustomPrefab ARock10 = new CustomPrefab(Mineable_SandRock10_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock10);
			Mineable_SandRock9_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock9_DoD");
			CustomPrefab ARock9 = new CustomPrefab(Mineable_SandRock9_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock9);
			Mineable_SandRock8_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock8_DoD");
			CustomPrefab ARock8 = new CustomPrefab(Mineable_SandRock8_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock8);
			Mineable_SandRock5_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock5_DoD");
			CustomPrefab ARock7 = new CustomPrefab(Mineable_SandRock5_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock7);
			Mineable_SandRock4_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock4_DoD");
			CustomPrefab ARock6 = new CustomPrefab(Mineable_SandRock4_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock6);
			Mineable_SandRock3_DoD = DoDBiome.LoadAsset<GameObject>("Mineable_SandRock3_DoD");
			CustomPrefab ARock5 = new CustomPrefab(Mineable_SandRock3_DoD, true);
			PrefabManager.Instance.AddPrefab(ARock5);

			//Debug.Log("DoDMonsters: 34");
			// fruit trees
			Tree_Banana_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Banana_Pickable_DoD");
			CustomPrefab Pick3 = new CustomPrefab(Tree_Banana_Pickable_DoD, true);
			PrefabManager.Instance.AddPrefab(Pick3);
			Tree_Apple_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Apple_Pickable_DoD");
			CustomPrefab Pick2 = new CustomPrefab(Tree_Apple_Pickable_DoD, true);
			PrefabManager.Instance.AddPrefab(Pick2);
			Mushroom_Cave_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Mushroom_Cave_Pickable_DoD");
			CustomPrefab Pick1 = new CustomPrefab(Mushroom_Cave_Pickable_DoD, true);
			PrefabManager.Instance.AddPrefab(Pick1);
		}
		private void AddLocations()
		{
			//Debug.Log("DoDMonsters: 35");
			DoDBiome = AssetUtils.LoadAssetBundleFromResources("dodbiomes", Assembly.GetExecutingAssembly());
			try
			{
				if (AshLandsLocations.Value == true)
				{
					var HellPlatformA = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformA_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(HellPlatformA, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
					var HellPlatformB = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_HellPlatformB_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(HellPlatformB, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
					var AshTower = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_AshTower_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AshTower, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 75,
						Priotized = true,
						ExteriorRadius = 5f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
				}
				if (DeepNorthLocations.Value == true)
				{
					var FroOreMine = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_FroOreMine_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(FroOreMine, new LocationConfig
					{
						Biome = Heightmap.Biome.DeepNorth,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
					}));
				}
				if (MistlandsLocEnable.Value == true)
				{
					var MistLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_OreMine_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistLoc2, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
					}));
				}
				if (DoDLocEnable.Value == true)
				{
					var Rambore = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Rambore_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Rambore, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
					}));
					var Bitterstump = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Bitterstump_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Bitterstump, new LocationConfig
					{
						Biome = Heightmap.Biome.BlackForest,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
					}));
					var AnyLoc1 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_CastleArena_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc1, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 10,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 10f,
						ClearArea = true,
					}));
					var AnyLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Camp_DoD"), true);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc2, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Mountain, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 300,
						Priotized = true,
						ExteriorRadius = 1f,
						MinAltitude = 2f,
						ClearArea = true,
					}));
				}
			}
			finally
			{
				ZoneManager.OnVanillaLocationsAvailable -= AddLocations;
				DoDBiome.Unload(false);
			}
		}
		private void AddCustomFruitTrees()
		{
			//Debug.Log("DoDMonsters: 36");
			CustomVegetation customBananaTree = new CustomVegetation(Tree_Banana_Pickable_DoD, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 2,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Swamp, Heightmap.Biome.Plains),
				MinAltitude = 1f,
				MaxAltitude = 100f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customBananaTree);

			CustomVegetation customAppleTree = new CustomVegetation(Tree_Apple_Pickable_DoD, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 1,
				GroupSizeMax = 3,
				GroupRadius = 10f,
				BlockCheck = true,
				Biome = Heightmap.Biome.BlackForest,
				MinAltitude = 1f,
				MaxAltitude = 500f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customAppleTree);
		}
		private void AddMistlandVegetation()
		{
			//Debug.Log("DoDMonsters: 37");
			var mistlandsVeg = new List<CustomVegetation>
			{
				new CustomVegetation(MineRock_FelOre_DoD, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 1,
					GroupSizeMax = 2,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 15f,
					MaxTilt = 20f
				}),
				new CustomVegetation(BlueMushroom_DoD, new VegetationConfig
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
				new CustomVegetation(PurpleMushroom_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Willow02_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Willow01_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Poplar02_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Poplar01_DoD, new VegetationConfig
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
				new CustomVegetation(Bush_RedBerries_Pickable_DoD, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 3,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 1f,
					MaxAltitude = 50f,
					MaxTilt = 20f
				}),
				new CustomVegetation(Tree_OldOak02_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockMS_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockMM_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockML_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockMH_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_OldOak01_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Oak02_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Oak01_DoD, new VegetationConfig
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
				new CustomVegetation(Bush_02_DoD, new VegetationConfig
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
				new CustomVegetation(Bush_01_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockMRFL_DoD, new VegetationConfig
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
				new CustomVegetation(Mineable_RockMRFM_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_LargeBroad_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_SmallMulti_B_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_LargeSingle_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_MediumSingle_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_Large_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_LargeTrio_DoD, new VegetationConfig
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
				new CustomVegetation(Flora_LargeDuo_DoD, new VegetationConfig
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
				new CustomVegetation(Tree_Walnut_Pickable_DoD, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 3,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 20f,
					MaxAltitude = 750f,
					MaxTilt = 30f
				})
			};

			foreach (var veg in mistlandsVeg)
			{
				ZoneManager.Instance.AddCustomVegetation(veg);
			}

		}
		private void AddDeepNorthVegetation()
		{
			//Debug.Log("DoDMonsters: 38");
			CustomVegetation customVegetation21 = new CustomVegetation(MineRock_FroOre_DoD, new VegetationConfig
			{
				Max = 2f,
				GroupSizeMin = 1,
				GroupSizeMax = 2,
				GroupRadius = 64f,
				BlockCheck = true,
				Biome = Heightmap.Biome.DeepNorth,
				MinAltitude = 0f,
				MaxTilt = 30f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation21);
			CustomVegetation customVegetation20 = new CustomVegetation(Bush3_DeepNorth_DoD, new VegetationConfig
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
			CustomVegetation customVegetation19 = new CustomVegetation(Bush2_DeepNorth_DoD, new VegetationConfig
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
			CustomVegetation customVegetation18 = new CustomVegetation(Bush1_DeepNorth_DoD, new VegetationConfig
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
			CustomVegetation customVegetation17 = new CustomVegetation(WinterPine7_DoD, new VegetationConfig
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
			CustomVegetation customVegetation16 = new CustomVegetation(Mineable_RockDN10_DoD, new VegetationConfig
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
			CustomVegetation customVegetation15 = new CustomVegetation(Mineable_RockDN9_DoD, new VegetationConfig
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
			CustomVegetation customVegetation14 = new CustomVegetation(Mineable_RockDN8_DoD, new VegetationConfig
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
			CustomVegetation customVegetation13 = new CustomVegetation(Mineable_RockDN7_DoD, new VegetationConfig
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
			CustomVegetation customVegetation12 = new CustomVegetation(Mineable_RockDN6_DoD, new VegetationConfig
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
			CustomVegetation customVegetation11 = new CustomVegetation(Mineable_RockDN5_DoD, new VegetationConfig
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
			CustomVegetation customVegetation10 = new CustomVegetation(Mineable_RockDN4_DoD, new VegetationConfig
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
			CustomVegetation customVegetation9 = new CustomVegetation(Mineable_RockDN3_DoD, new VegetationConfig
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
			CustomVegetation customVegetation8 = new CustomVegetation(Mineable_RockDN2_DoD, new VegetationConfig
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
			CustomVegetation customVegetation7 = new CustomVegetation(Mineable_RockDN1_DoD, new VegetationConfig
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
			CustomVegetation customVegetation6 = new CustomVegetation(WinterPine6_DoD, new VegetationConfig
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
			CustomVegetation customVegetation5 = new CustomVegetation(WinterPine5_DoD, new VegetationConfig
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
			CustomVegetation customVegetation4 = new CustomVegetation(WinterPine4_DoD, new VegetationConfig
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
			CustomVegetation customVegetation3 = new CustomVegetation(WinterPine3_DoD, new VegetationConfig
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
			CustomVegetation customVegetation2 = new CustomVegetation(WinterPine2_DoD, new VegetationConfig
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
			CustomVegetation customVegetation1 = new CustomVegetation(WinterPine1_DoD, new VegetationConfig
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
			//Debug.Log("DoDMonsters: 39");
			CustomVegetation customVegetation14 = new CustomVegetation(Mineable_SandRock16_DoD, new VegetationConfig
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
			CustomVegetation customVegetation13 = new CustomVegetation(Mineable_SandRock15_DoD, new VegetationConfig
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
			CustomVegetation customVegetation12 = new CustomVegetation(Mineable_SandRock14_DoD, new VegetationConfig
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
			CustomVegetation customVegetation11 = new CustomVegetation(Mineable_SandRock13_DoD, new VegetationConfig
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
			CustomVegetation customVegetation10 = new CustomVegetation(Mineable_SandRock12_DoD, new VegetationConfig
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
			CustomVegetation customVegetation9 = new CustomVegetation(Mineable_SandRock11_DoD, new VegetationConfig
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
			CustomVegetation customVegetation8 = new CustomVegetation(Mineable_SandRock10_DoD, new VegetationConfig
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
			CustomVegetation customVegetation7 = new CustomVegetation(Mineable_SandRock9_DoD, new VegetationConfig
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
			CustomVegetation customVegetation6 = new CustomVegetation(Mineable_SandRock8_DoD, new VegetationConfig
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
			CustomVegetation customVegetation3 = new CustomVegetation(Mineable_SandRock5_DoD, new VegetationConfig
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
			CustomVegetation customVegetation2 = new CustomVegetation(Mineable_SandRock4_DoD, new VegetationConfig
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
			CustomVegetation customVegetation1 = new CustomVegetation(Mineable_SandRock3_DoD, new VegetationConfig
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
			GameObject food5 = CaveMushroom;
			CustomItem customFood5 = new CustomItem(food5, fixReference: true);
			ItemManager.Instance.AddItem(customFood5);

			GameObject food4 = Walnuts;
			CustomItem customFood4 = new CustomItem(food4, fixReference: true);
			ItemManager.Instance.AddItem(customFood4);

			GameObject food3 = Apple;
			CustomItem customFood3 = new CustomItem(food3, fixReference: true);
			ItemManager.Instance.AddItem(customFood3);

			GameObject food2 = Cherry;
			CustomItem customFood2 = new CustomItem(food2, fixReference: true);
			ItemManager.Instance.AddItem(customFood2);

			GameObject food1 = Banana;
			CustomItem customFood1 = new CustomItem(food1, fixReference: true);
			ItemManager.Instance.AddItem(customFood1);
		}
		private void UnloadBundle()
		{
			DoDBiome?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
