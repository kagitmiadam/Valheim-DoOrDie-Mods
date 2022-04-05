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

		public const string PluginVersion = "0.0.12";

		public static GameObject SteelPick;
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
		public static GameObject Tree_Banana_Pickable_DoD;
		public static GameObject Tree_Apple_Pickable_DoD;
		public static GameObject Mushroom_Cave_Pickable_DoD;
		public static GameObject CaveMushroom;
		public static GameObject HardLog;
		public static GameObject HardLogHalf;
		// anvils
		public static GameObject AnvilsFel;
		public static GameObject AnvilsFro;
		public static GameObject AnvilsFlam;
		public static Sprite TexFlaAnvil;
		public static Sprite TexFroAnvil;
		public static Sprite TexFelAnvil;
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
		//public ConfigEntry<bool> UnderworldEnable;

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
			/*UnderworldEnable = base.Config.Bind("Underworld", "Enable", defaultValue: true, new ConfigDescription("Enables the Underworld Locations", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));*/
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
			UpdateBlastFurnace();
			CreatePickAxe();
			CreateAnvils();
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
			Debug.Log("DoDBiomes: Chests");
			GameObject TCMistlands = DoDBiome.LoadAsset<GameObject>("TreasureChest_Mistlands_DoD");
			GameObject TCDeepNorth = DoDBiome.LoadAsset<GameObject>("TreasureChest_DeepNorth_DoD");
			GameObject TCAshLands = DoDBiome.LoadAsset<GameObject>("TreasureChest_AshLands_DoD");
			PrefabManager.Instance.AddPrefab(TCMistlands);
			PrefabManager.Instance.AddPrefab(TCDeepNorth);
			PrefabManager.Instance.AddPrefab(TCAshLands);

			Debug.Log("DoDBiomes: SFX");
			GameObject SFXRockHit = DoDBiome.LoadAsset<GameObject>("loc_sfx_rock_hit_dod");
			GameObject SFXRockDest = DoDBiome.LoadAsset<GameObject>("loc_sfx_rock_destroyed_dod");
			GameObject SFXBossSpawn = DoDBiome.LoadAsset<GameObject>("SFX_BossSpawn_DoD");
			GameObject SFXSummoning = DoDBiome.LoadAsset<GameObject>("SFX_BossSummon_DoD");
			GameObject SFXBushChop = DoDBiome.LoadAsset<GameObject>("SFX_Bush_Chop_DoD");
			GameObject SFXOfferingBones = DoDBiome.LoadAsset<GameObject>("SFX_OfferingBones_DoD");
			GameObject SFXPickable = DoDBiome.LoadAsset<GameObject>("SFX_Pickable_Pick_DoD");
			GameObject SFXLoc1 = DoDBiome.LoadAsset<GameObject>("SFX_Rock_Destroyed_DoD");
			GameObject SFXLoc2 = DoDBiome.LoadAsset<GameObject>("SFX_Rock_Hit_DoD");
			GameObject SFXTreeFallS = DoDBiome.LoadAsset<GameObject>("SFX_SmallTree_Falling_DoD");
			GameObject SFXTreeFall = DoDBiome.LoadAsset<GameObject>("SFX_Tree_Falling_DoD");
			GameObject SFXWoodChop = DoDBiome.LoadAsset<GameObject>("SFX_Wood_Chop_DoD");
			GameObject SFXWoodDestroy = DoDBiome.LoadAsset<GameObject>("SFX_Wood_Destroyed_DoD");
			PrefabManager.Instance.AddPrefab(SFXLoc1);
			PrefabManager.Instance.AddPrefab(SFXLoc2);
			PrefabManager.Instance.AddPrefab(SFXWoodDestroy);
			PrefabManager.Instance.AddPrefab(SFXWoodChop);
			PrefabManager.Instance.AddPrefab(SFXTreeFall);
			PrefabManager.Instance.AddPrefab(SFXTreeFallS);
			PrefabManager.Instance.AddPrefab(SFXBushChop);
			PrefabManager.Instance.AddPrefab(SFXRockHit);
			PrefabManager.Instance.AddPrefab(SFXRockDest);
			PrefabManager.Instance.AddPrefab(SFXPickable);
			PrefabManager.Instance.AddPrefab(SFXOfferingBones);
			PrefabManager.Instance.AddPrefab(SFXBossSpawn);
			PrefabManager.Instance.AddPrefab(SFXSummoning);

			Debug.Log("DoDBiomes: VFX");
			GameObject VFXBiiterSpawn = DoDBiome.LoadAsset<GameObject>("VFX_BiiterSpawn_DoD");
			GameObject VFXBitterSpawnIn = DoDBiome.LoadAsset<GameObject>("VFX_BitterSpawnIn_DoD");
			GameObject VFXDustPiece = DoDBiome.LoadAsset<GameObject>("VFX_Dust_Piece_DoD");
			GameObject VFXFelOreDestroy = DoDBiome.LoadAsset<GameObject>("VFX_Felore_Destroy_DoD");
			GameObject VFXMineHit = DoDBiome.LoadAsset<GameObject>("VFX_Mine_Hit_DoD");
			GameObject VFXOfferingBowl = DoDBiome.LoadAsset<GameObject>("VFX_OfferingBowl_DoD");
			GameObject VFXPickable = DoDBiome.LoadAsset<GameObject>("VFX_Pickable_Pick_DoD");
			GameObject VFXRockDestroyed = DoDBiome.LoadAsset<GameObject>("VFX_RockDestroyed_DoD");
			GameObject VFXRockHit = DoDBiome.LoadAsset<GameObject>("VFX_RockHit_DoD");
			GameObject VFXDestroyed = DoDBiome.LoadAsset<GameObject>("VFX_Destroyed_DoD");
			GameObject VFXHit = DoDBiome.LoadAsset<GameObject>("VFX_Hit_DoD");
			PrefabManager.Instance.AddPrefab(VFXMineHit);
			PrefabManager.Instance.AddPrefab(VFXBiiterSpawn);
			PrefabManager.Instance.AddPrefab(VFXBitterSpawnIn);
			PrefabManager.Instance.AddPrefab(VFXFelOreDestroy);
			PrefabManager.Instance.AddPrefab(VFXPickable);
			PrefabManager.Instance.AddPrefab(VFXDustPiece);
			PrefabManager.Instance.AddPrefab(VFXRockDestroyed);
			PrefabManager.Instance.AddPrefab(VFXDestroyed);
			PrefabManager.Instance.AddPrefab(VFXOfferingBowl);
			PrefabManager.Instance.AddPrefab(VFXHit);
			PrefabManager.Instance.AddPrefab(VFXRockHit);

			Debug.Log("DoDBiomes: Items");
			OakWood = DoDBiome.LoadAsset<GameObject>("OakWood_DoD");
			SteelPick = DoDBiome.LoadAsset<GameObject>("SteelPickaxe_DoD");
			Walnuts = DoDBiome.LoadAsset<GameObject>("Walnuts_DoD");
			Apple = DoDBiome.LoadAsset<GameObject>("Apple_DoD");
			Cherry = DoDBiome.LoadAsset<GameObject>("Cherries_DoD");
			Banana = DoDBiome.LoadAsset<GameObject>("Banana_DoD");
			CaveMushroom = DoDBiome.LoadAsset<GameObject>("CaveMushroom_DoD");
			HardLog = DoDBiome.LoadAsset<GameObject>("Hardwood_Log_DoD");
			CustomPrefab Log2 = new CustomPrefab(HardLog, true);
			PrefabManager.Instance.AddPrefab(Log2);
			HardLogHalf = DoDBiome.LoadAsset<GameObject>("Hardwood_LogHalf_DoD");
			CustomPrefab Log1 = new CustomPrefab(HardLogHalf, true);
			PrefabManager.Instance.AddPrefab(Log1);

			Debug.Log("DoDBiomes: Anvils");
			TexFlaAnvil = DoDBiome.LoadAsset<Sprite>("FlaAnvil_Icon_DoD");
			TexFroAnvil = DoDBiome.LoadAsset<Sprite>("FroAnvil_Icon_DoD");
			TexFelAnvil = DoDBiome.LoadAsset<Sprite>("FelAnvil_Icon_DoD");
			AnvilsFel = DoDBiome.LoadAsset<GameObject>("FelmetalAnvils_DoD");
			AnvilsFro = DoDBiome.LoadAsset<GameObject>("FrometalAnvils_DoD");
			AnvilsFlam = DoDBiome.LoadAsset<GameObject>("FlametalAnvils_DoD");
			// ores
			Debug.Log("DoDBiomes: Ores");
			MineRock_FroOre_DoD = DoDBiome.LoadAsset<GameObject>("MineRock_FroOre_DoD");
			MineRock_FelOre_DoD = DoDBiome.LoadAsset<GameObject>("MineRock_FelOre_DoD");

			// fruit trees
			Debug.Log("DoDBiomes: Fruit Trees");
			Tree_Banana_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Banana_Pickable_DoD");
			Tree_Apple_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Apple_Pickable_DoD");
			Mushroom_Cave_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Mushroom_Cave_Pickable_DoD");
			Tree_Walnut_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Walnut_Pickable_DoD");
			// mistlands veg
			//Debug.Log("DoDMonsters: 31");
			Debug.Log("DoDBiomes: Mistlands Veg");
			BlueMushroom_DoD = DoDBiome.LoadAsset<GameObject>("BlueMushroom_DoD");
			PurpleMushroom_DoD = DoDBiome.LoadAsset<GameObject>("PurpleMushroom_DoD");
			Tree_Willow02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow02_DoD");
			Tree_Willow01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Willow01_DoD");
			Tree_Poplar02_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar02_DoD");
			Tree_Poplar01_DoD = DoDBiome.LoadAsset<GameObject>("Tree_Poplar01_DoD");
			Bush_RedBerries_Pickable_DoD = DoDBiome.LoadAsset<GameObject>("Bush_RedBerries_Pickable_DoD");
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
			//Debug.Log("DoDMonsters: 32");
			// deep north
			Debug.Log("DoDBiomes: Deep North Veg");
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
			//Debug.Log("DoDMonsters: 33");
			// ash lands
			Debug.Log("DoDBiomes: Ashlands Veg");
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
		}
		private void AddLocations()
		{
			//Debug.Log("DoDMonsters: 35");
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
					var BossPlatformAL = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_AL_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformAL, true, new LocationConfig
					{
						Biome = Heightmap.Biome.AshLands,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
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
				if (DeepNorthLocations.Value == true)
				{
					var FroOreMine = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_FroOreMine_DoD"));
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
					var BossPlatformDN = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_DN_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformDN, true, new LocationConfig
					{
						Biome = Heightmap.Biome.DeepNorth,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
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
					var MistLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_OreMine_DoD"));
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
					var BossPlatformMist = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_Mist_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformMist, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
				}
				if (DoDLocEnable.Value == true)
				{
					var BossFarkas = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Farkas_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossFarkas, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Mountain,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 9f,
						MinAltitude = 100f,
						ClearArea = true,
						MinDistance = 3000f,
						MaxDistance = 7000f,
						MinDistanceFromSimilar = 1000f,
					}));
					var BossBhygshan = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Bhygshan_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossBhygshan, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Swamp,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 12f,
						MinAltitude = 0.5f,
						ClearArea = true,
						MinDistance = 3000f,
						MaxDistance = 7000f,
						MinDistanceFromSimilar = 1000f,
					}));
					var BossPlatformPlains = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_Plains_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformPlains, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Plains,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
					var BossPlatformMount = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_Mount_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformMount, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Mountain,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
					var BossPlatformSwamp = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_Swamp_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformSwamp, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Swamp,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 0.5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
					var BossPlatformBF = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_BF_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformBF, true, new LocationConfig
					{
						Biome = Heightmap.Biome.BlackForest,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
					var BossPlatformMead = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_BossPlatform_Meadows_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(BossPlatformMead, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 8,
						Priotized = true,
						ExteriorRadius = 17f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistanceFromSimilar = 750f,
					}));
					var Rambore = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Rambore_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Rambore, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
						MaxDistance = 3000f,
						MinDistanceFromSimilar = 1000f,
					}));
					var Bitterstump = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Boss_Bitterstump_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Bitterstump, true, new LocationConfig
					{
						Biome = Heightmap.Biome.BlackForest,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 3f,
						MinAltitude = 5f,
						ClearArea = true,
						SlopeRotation = true,
						MaxDistance = 3000f,
						MinDistanceFromSimilar = 1000f,
					}));
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
					var AnyLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Camp_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc2, true, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Mountain, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 300,
						Priotized = true,
						ExteriorRadius = 1f,
						MinAltitude = 2f,
						ClearArea = true,
						MinDistanceFromSimilar = 500f,
					}));
				}
				/*if (UnderworldEnable.Value == true)
                {
					var AnyLoc3 = ZoneManager.Instance.CreateLocationContainer(DoDBiome.LoadAsset<GameObject>("Loc_Underworld_DoD"));
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc3, true, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 1,
						Priotized = true,
						ExteriorRadius = 15f,
						MinAltitude = 5f,
						ClearArea = true,
						MinDistance = 150f,
						MaxDistance = 400f,
					}));
                }*/
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
			CustomVegetation customBananaTree = new CustomVegetation(Tree_Banana_Pickable_DoD, true, new VegetationConfig
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
			CustomVegetation customAppleTree = new CustomVegetation(Tree_Apple_Pickable_DoD, true, new VegetationConfig
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
		private void AddMistlandVegetation()
		{
			//Debug.Log("DoDMonsters: 37");
			var mistlandsVeg = new List<CustomVegetation>
			{
				new CustomVegetation(BlueMushroom_DoD, true, new VegetationConfig
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
				new CustomVegetation(PurpleMushroom_DoD, true, new VegetationConfig
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
				new CustomVegetation(Bush_RedBerries_Pickable_DoD, true, new VegetationConfig
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
				}),
				new CustomVegetation(Tree_Walnut_Pickable_DoD, true, new VegetationConfig
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
			//Debug.Log("DoDMonsters: 39");
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
		private void CreatePickAxe()
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
		private void CreateFruit()
		{
			GameObject dropable53 = OakWood;
			CustomItem customItem53 = new CustomItem(dropable53, fixReference: true);
			ItemManager.Instance.AddItem(customItem53);

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
		private void UpdateBlastFurnace()
		{
			GameObject itemPrefab = DoDBiome.LoadAsset<GameObject>("SteelBar_DoD");
			CustomItem customItem = new CustomItem(itemPrefab, fixReference: false);
			ItemManager.Instance.AddItem(customItem);

			CustomItemConversion itemConversion = new CustomItemConversion(new SmelterConversionConfig
			{
				Station = "blastfurnace",
				FromItem = "Iron",
				ToItem = "SteelBar_DoD"
			});
			ItemManager.Instance.AddItemConversion(itemConversion);
			GameObject itemPrefab2 = DoDBiome.LoadAsset<GameObject>("FrometalOre_DoD");
			GameObject itemPrefab3 = DoDBiome.LoadAsset<GameObject>("FrometalBar_DoD");
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
			GameObject itemPrefab4 = DoDBiome.LoadAsset<GameObject>("FelmetalOre_DoD");
			GameObject itemPrefab5 = DoDBiome.LoadAsset<GameObject>("FelmetalBar_DoD");
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
		private void CreateAnvils()
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
