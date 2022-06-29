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

namespace FruitTrees
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class FruitTrees : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.FruitTrees";

		public const string PluginName = "FruitTrees";

		public const string PluginVersion = "0.0.2";

		public static bool isModded = true;

		// Fruit
		public static GameObject Walnuts;
		public static GameObject Cherry;
		public static GameObject Apple;
		public static GameObject Banana;
		public static GameObject Grape;
		// Vegetation
		public static GameObject BushCherry;
		public static GameObject BushGrape;
		public static GameObject TreeWalnut;
		public static GameObject TreeBanana;
		public static GameObject TreeApple;

		public AssetBundle fruitTreesBundle;

		private Harmony _harmony;
		private void Awake() 
		{
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FruitTrees");
			LoadBundle();
			LoadAssets();
			CreateFruit(); 
			AddCustomFruitTrees();
		}
		public void LoadBundle()
		{
			fruitTreesBundle = AssetUtils.LoadAssetBundleFromResources("fruittrees", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
        {
            try
			{
				// Fruit
				Walnuts = fruitTreesBundle.LoadAsset<GameObject>("Walnuts_DoD");
				Apple = fruitTreesBundle.LoadAsset<GameObject>("Apple_DoD");
				Cherry = fruitTreesBundle.LoadAsset<GameObject>("Cherries_DoD");
				Banana = fruitTreesBundle.LoadAsset<GameObject>("Banana_DoD");
				Grape = fruitTreesBundle.LoadAsset<GameObject>("Grapes_DoD");
				// Trees
				TreeBanana = fruitTreesBundle.LoadAsset<GameObject>("Tree_Banana_Pickable_DoD");
				TreeApple = fruitTreesBundle.LoadAsset<GameObject>("Tree_Apple_Pickable_DoD");
				TreeWalnut = fruitTreesBundle.LoadAsset<GameObject>("Tree_Walnut_Pickable_DoD");
				BushCherry = fruitTreesBundle.LoadAsset<GameObject>("Bush_RedBerries_Pickable_DoD");
				BushGrape = fruitTreesBundle.LoadAsset<GameObject>("Bush_Grape_Pickable_DoD");
				// SFX
				GameObject SFX1 = fruitTreesBundle.LoadAsset<GameObject>("SFX_Pickable_Pick_FT");
				CustomPrefab sfx1 = new CustomPrefab(SFX1, false);
				PrefabManager.Instance.AddPrefab(sfx1);
				GameObject SFX2 = fruitTreesBundle.LoadAsset<GameObject>("SFX_SmallTree_Falling_FT");
				CustomPrefab sfx2 = new CustomPrefab(SFX2, false);
				PrefabManager.Instance.AddPrefab(sfx2);
				GameObject SFX3 = fruitTreesBundle.LoadAsset<GameObject>("SFX_Wood_Chop_FT");
				CustomPrefab sfx3 = new CustomPrefab(SFX3, false);
				PrefabManager.Instance.AddPrefab(sfx3);
				// VFX
				GameObject VFX1 = fruitTreesBundle.LoadAsset<GameObject>("VFX_Dust_Piece_FT");
				CustomPrefab vfx1 = new CustomPrefab(VFX1, false);
				PrefabManager.Instance.AddPrefab(vfx1);
				GameObject VFX2 = fruitTreesBundle.LoadAsset<GameObject>("VFX_Pickable_Pick_FT");
				CustomPrefab vfx2 = new CustomPrefab(VFX2, false);
				PrefabManager.Instance.AddPrefab(vfx2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Assets for Fruit Trees: {ex}");
			}
		}
		private void CreateFruit()
		{
            try
			{
				GameObject food5 = Grape;
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
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Fruits for Fruit Trees: {ex}");
			}
		}
		private void AddCustomFruitTrees()
		{
            try
			{
				////Debug.Log("Fruit Trees: Vegetation");
				CustomVegetation customBananaTree = new CustomVegetation(TreeBanana, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 1,
					GroupSizeMax = 2,
					GroupRadius = 64f,
					BlockCheck = true,
					Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Swamp, Heightmap.Biome.Plains),
					MinAltitude = 1f,
					MaxAltitude = 75f,
					MaxTilt = 30f
				});
				ZoneManager.Instance.AddCustomVegetation(customBananaTree);
				CustomVegetation customAppleTree = new CustomVegetation(TreeApple, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 1,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Meadows,
					MinAltitude = 1f,
					MaxAltitude = 75f,
					MaxTilt = 30f
				});
				ZoneManager.Instance.AddCustomVegetation(customAppleTree);
				CustomVegetation customCherryTree = new CustomVegetation(BushCherry, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 3,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Swamp, Heightmap.Biome.BlackForest),
					MinAltitude = 1f,
					MaxAltitude = 50f,
					MaxTilt = 20f
				});
				ZoneManager.Instance.AddCustomVegetation(customCherryTree);
				CustomVegetation customWalnutTree = new CustomVegetation(TreeWalnut, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 3,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = Heightmap.Biome.Mistlands,
					MinAltitude = 20f,
					MaxAltitude = 75f,
					MaxTilt = 30f
				});
				ZoneManager.Instance.AddCustomVegetation(customWalnutTree);
				CustomVegetation customGrapeTree = new CustomVegetation(BushGrape, true, new VegetationConfig
				{
					Max = 1f,
					GroupSizeMin = 3,
					GroupSizeMax = 3,
					GroupRadius = 10f,
					BlockCheck = true,
					Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Plains),
					MinAltitude = 1f,
					MaxAltitude = 50f,
					MaxTilt = 20f
				});
				ZoneManager.Instance.AddCustomVegetation(customGrapeTree);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Trees for Fruit Trees: {ex}");
			}
		}
	}
}
