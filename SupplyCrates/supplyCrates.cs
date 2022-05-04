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

namespace SupplyCrates
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class supplyCrates : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.SupplyCrates";

		public const string PluginName = "SupplyCrates";

		public const string PluginVersion = "0.0.1";

		internal static ManualLogSource Log;

		public static GameObject Food1;
		public static GameObject Food2;
		public static GameObject Food3;
		public static GameObject Food4;
		public static GameObject Food5;
		public static GameObject Food6;
		public static GameObject Food7;
		public static GameObject Food8;
		public static GameObject Food9;
		public static GameObject Food10;
		public static GameObject Food11;
		public static GameObject Food12;
		public static GameObject Food13;
		public static GameObject Food14;
		public static GameObject Food15;
		public static GameObject Food16;
		public static GameObject Food17;
		public static GameObject Food18;
		public static GameObject Food19;
		public static GameObject Food20;
		public static GameObject Food21;
		public static GameObject Food22;
		public static GameObject Food23;
		public static GameObject Food24;
		public static GameObject Food25;
		public static GameObject Food26;
		public static GameObject Food27;
		public static GameObject Food28;

		public static GameObject Pickable1;
		public static GameObject Pickable2;
		public static GameObject Pickable3;

		public ConfigEntry<bool> MeadowsEnable;
		public ConfigEntry<bool> BlackForestEnable;
		public ConfigEntry<bool> SwampEnable;
		//public ConfigEntry<bool> MountainEnable;
		//public ConfigEntry<bool> PlainsEnable;

		public AssetBundle SupplyBundle;
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
			MeadowsEnable = base.Config.Bind("Meadows", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Meadows.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BlackForestEnable = base.Config.Bind("Black Forest", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Blackforest.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			SwampEnable = base.Config.Bind("Swamp", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Swamp.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake() 
		{
			CreateConfigurationValues();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.SupplyCrates");
			Log = Logger;
			LoadBundle();
			LoadAssets();
			AddItems();
			ZoneManager.OnVanillaLocationsAvailable += UpdateLocations;
		}
		public void LoadBundle()
		{
			SupplyBundle = AssetUtils.LoadAssetBundleFromResources("supplycrates", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Fruit
			Food1 = SupplyBundle.LoadAsset<GameObject>("Apple_SC");
			Food2 = SupplyBundle.LoadAsset<GameObject>("Banana_SC");
			Food3 = SupplyBundle.LoadAsset<GameObject>("Coconut_SC");
			Food4 = SupplyBundle.LoadAsset<GameObject>("Lemon_SC");
			Food5 = SupplyBundle.LoadAsset<GameObject>("Mango_SC");
			Food6 = SupplyBundle.LoadAsset<GameObject>("Orange_SC");
			Food7 = SupplyBundle.LoadAsset<GameObject>("Peach_SC");
			Food8 = SupplyBundle.LoadAsset<GameObject>("Pear_SC");
			Food9 = SupplyBundle.LoadAsset<GameObject>("Plum_SC");
			Food10 = SupplyBundle.LoadAsset<GameObject>("Watermelon_SC");
			Food11 = SupplyBundle.LoadAsset<GameObject>("Grapes_SC");
			// Veg
			Food12 = SupplyBundle.LoadAsset<GameObject>("BellPepper_SC");
			Food13 = SupplyBundle.LoadAsset<GameObject>("Broccoli_SC");
			Food14 = SupplyBundle.LoadAsset<GameObject>("Cabbage_SC");
			Food15 = SupplyBundle.LoadAsset<GameObject>("Corn_SC");
			Food16 = SupplyBundle.LoadAsset<GameObject>("Cucumber_SC");
			Food17 = SupplyBundle.LoadAsset<GameObject>("Lettuce_SC");
			Food18 = SupplyBundle.LoadAsset<GameObject>("Potato_SC");
			Food19 = SupplyBundle.LoadAsset<GameObject>("Pumpkin_SC");
			Food20 = SupplyBundle.LoadAsset<GameObject>("SpringOnion_SC");
			Food21 = SupplyBundle.LoadAsset<GameObject>("Squash_SC");
			Food22 = SupplyBundle.LoadAsset<GameObject>("SweetPotato_SC");
			Food23 = SupplyBundle.LoadAsset<GameObject>("Tomato_SC");
			// Dairy
			Food24 = SupplyBundle.LoadAsset<GameObject>("BlueCheese_SC");
			Food25 = SupplyBundle.LoadAsset<GameObject>("EdamCheese_SC");
			// Breads
			Food26 = SupplyBundle.LoadAsset<GameObject>("Bagel_SC");
			Food27 = SupplyBundle.LoadAsset<GameObject>("Bagette_SC");
			Food28 = SupplyBundle.LoadAsset<GameObject>("Pretzel_SC");
			// Boxes
			Pickable1 = SupplyBundle.LoadAsset<GameObject>("BoxOfFruits_SC");
			Pickable2 = SupplyBundle.LoadAsset<GameObject>("BoxOfVegetables_SC");
			Pickable3 = SupplyBundle.LoadAsset<GameObject>("BoxOfDairy_SC");
			// Add Prefabs
			CustomPrefab pick1 = new CustomPrefab(Pickable1, true);
			PrefabManager.Instance.AddPrefab(pick1);
			CustomPrefab pick2 = new CustomPrefab(Pickable2, true);
			PrefabManager.Instance.AddPrefab(pick2);
			CustomPrefab pick3 = new CustomPrefab(Pickable3, true);
			PrefabManager.Instance.AddPrefab(pick3);
		}
		private void AddItems()
		{
            try
			{
				// Fruit
				GameObject dropable1 = Food1;
				CustomItem customItem1 = new CustomItem(dropable1, false);
				ItemManager.Instance.AddItem(customItem1);
				GameObject dropable2 = Food2;
				CustomItem customItem2 = new CustomItem(dropable2, false);
				ItemManager.Instance.AddItem(customItem2);
				GameObject dropable3 = Food3;
				CustomItem customItem3 = new CustomItem(dropable3, false);
				ItemManager.Instance.AddItem(customItem3);
				GameObject dropable4 = Food4;
				CustomItem customItem4 = new CustomItem(dropable4, false);
				ItemManager.Instance.AddItem(customItem4);
				GameObject dropable5 = Food5;
				CustomItem customItem5 = new CustomItem(dropable5, false);
				ItemManager.Instance.AddItem(customItem5);
				GameObject dropable6 = Food6;
				CustomItem customItem6 = new CustomItem(dropable6, false);
				ItemManager.Instance.AddItem(customItem6);
				GameObject dropable7 = Food7;
				CustomItem customItem7 = new CustomItem(dropable7, false);
				ItemManager.Instance.AddItem(customItem7);
				GameObject dropable8 = Food8;
				CustomItem customItem8 = new CustomItem(dropable8, false);
				ItemManager.Instance.AddItem(customItem8);
				GameObject dropable9 = Food9;
				CustomItem customItem9 = new CustomItem(dropable9, false);
				ItemManager.Instance.AddItem(customItem9);
				GameObject dropable10 = Food10;
				CustomItem customItem10 = new CustomItem(dropable10, false);
				ItemManager.Instance.AddItem(customItem10);
				// Veg
				GameObject dropable11 = Food11;
				CustomItem customItem11 = new CustomItem(dropable11, false);
				ItemManager.Instance.AddItem(customItem11);
				GameObject dropable12 = Food12;
				CustomItem customItem12 = new CustomItem(dropable12, false);
				ItemManager.Instance.AddItem(customItem12);
				GameObject dropable13 = Food13;
				CustomItem customItem13 = new CustomItem(dropable13, false);
				ItemManager.Instance.AddItem(customItem13);
				GameObject dropable14 = Food14;
				CustomItem customItem14 = new CustomItem(dropable14, false);
				ItemManager.Instance.AddItem(customItem14);
				GameObject dropable15 = Food15;
				CustomItem customItem15 = new CustomItem(dropable15, false);
				ItemManager.Instance.AddItem(customItem15);
				GameObject dropable16 = Food16;
				CustomItem customItem16 = new CustomItem(dropable16, false);
				ItemManager.Instance.AddItem(customItem16);
				GameObject dropable17 = Food17;
				CustomItem customItem17 = new CustomItem(dropable17, false);
				ItemManager.Instance.AddItem(customItem17);
				GameObject dropable18 = Food18;
				CustomItem customItem18 = new CustomItem(dropable18, false);
				ItemManager.Instance.AddItem(customItem18);
				GameObject dropable19 = Food19;
				CustomItem customItem19 = new CustomItem(dropable19, false);
				ItemManager.Instance.AddItem(customItem19);
				GameObject dropable20 = Food20;
				CustomItem customItem20 = new CustomItem(dropable20, false);
				ItemManager.Instance.AddItem(customItem20);
				GameObject dropable21 = Food21;
				CustomItem customItem21 = new CustomItem(dropable21, false);
				ItemManager.Instance.AddItem(customItem21);
				GameObject dropable22 = Food22;
				CustomItem customItem22 = new CustomItem(dropable22, false);
				ItemManager.Instance.AddItem(customItem22);
				GameObject dropable23 = Food23;
				CustomItem customItem23 = new CustomItem(dropable23, false);
				ItemManager.Instance.AddItem(customItem23);
				// Dairy
				GameObject dropable24 = Food24;
				CustomItem customItem24 = new CustomItem(dropable24, false);
				ItemManager.Instance.AddItem(customItem24);
				GameObject dropable25 = Food25;
				CustomItem customItem25 = new CustomItem(dropable25, false);
				ItemManager.Instance.AddItem(customItem25);
				// Breads
				GameObject dropable26 = Food26;
				CustomItem customItem26 = new CustomItem(dropable26, false);
				ItemManager.Instance.AddItem(customItem26);
				GameObject dropable27 = Food27;
				CustomItem customItem27 = new CustomItem(dropable27, false);
				ItemManager.Instance.AddItem(customItem27);
				GameObject dropable28 = Food28;
				CustomItem customItem28 = new CustomItem(dropable28, false);
				ItemManager.Instance.AddItem(customItem28);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding fruit: {ex}");
			}
            /*finally
			{
				SupplyBundle?.Unload(unloadAllLoadedObjects: false);
			}*/
		}
		private void UpdateLocations()
        {
            try
			{
				// Load objects to add to locations
				var fruitPrefab = PrefabManager.Instance.GetPrefab("BoxOfFruits_SC");
				var vegPrefab = PrefabManager.Instance.GetPrefab("BoxOfVegetables_SC");
				var dairyPrefab = PrefabManager.Instance.GetPrefab("BoxOfDairy_SC");
				Debug.Log("Supply Crates: Box1");
				// Biomes
				if (MeadowsEnable.Value == true)
                {
					// Load Locations to Edit
					var house1Location = ZoneManager.Instance.GetZoneLocation("WoodHouse8");
					Debug.Log("Supply Crates: House1");
					var house2Location = ZoneManager.Instance.GetZoneLocation("WoodHouse6");
					Debug.Log("Supply Crates: House2");
					var house3Location = ZoneManager.Instance.GetZoneLocation("WoodHouse3");
					Debug.Log("Supply Crates: House3");
					var house4Location = ZoneManager.Instance.GetZoneLocation("WoodHouse11");
					Debug.Log("Supply Crates: House4");
					var house5Location = ZoneManager.Instance.GetZoneLocation("WoodHouse2");
					Debug.Log("Supply Crates: House5");
					var house6Location = ZoneManager.Instance.GetZoneLocation("WoodHouse7");
					Debug.Log("Supply Crates: House6");
					var house7Location = ZoneManager.Instance.GetZoneLocation("WoodHouse13");
					Debug.Log("Supply Crates: House7");
					var house8Location = ZoneManager.Instance.GetZoneLocation("Dolmen03");
					Debug.Log("Supply Crates: House8");
					// Add objects to locations
					// WoodHouse8
					var house1Fruit = Instantiate(fruitPrefab, house1Location.m_prefab.transform);
					house1Fruit.name = fruitPrefab.name;
					house1Fruit.transform.localPosition = new Vector3(3.379997f, 0f, 3.62999f);
					Debug.Log("Supply Crates: Loc1");
					// WoodHouse6
					var house2Fruit = Instantiate(fruitPrefab, house2Location.m_prefab.transform);
					house2Fruit.name = fruitPrefab.name;
					house2Fruit.transform.localPosition = new Vector3(-0.4f, 0f, 2.8f);
					Debug.Log("Supply Crates: Loc2");
					// WoodHouse3
					var house3Fruit = Instantiate(fruitPrefab, house3Location.m_prefab.transform);
					house3Fruit.name = fruitPrefab.name;
					house3Fruit.transform.localPosition = new Vector3(-4.48f, 0f, 3f);
					Debug.Log("Supply Crates: Loc3");
					// WoodHouse11
					var house4Fruit = Instantiate(fruitPrefab, house4Location.m_prefab.transform);
					house4Fruit.name = fruitPrefab.name;
					house4Fruit.transform.localPosition = new Vector3(1.55f, 0f, 3.34f);
					Debug.Log("Supply Crates: Loc4");
					// WoodHouse2
					var house1Veg = Instantiate(vegPrefab, house5Location.m_prefab.transform);
					house1Veg.name = vegPrefab.name;
					house1Veg.transform.localPosition = new Vector3(1.7f, 0f, 2.8f);
					Debug.Log("Supply Crates: Loc5");
					// WoodHouse7
					var house2Veg = Instantiate(vegPrefab, house6Location.m_prefab.transform);
					house2Veg.name = vegPrefab.name;
					house2Veg.transform.localPosition = new Vector3(1.9f, 0f, 1f);
					Debug.Log("Supply Crates: Loc6");
					// WoodHouse13
					var house3Veg = Instantiate(vegPrefab, house7Location.m_prefab.transform);
					house3Veg.name = vegPrefab.name;
					house3Veg.transform.localPosition = new Vector3(3.1f, 0f, -2.55f);
					Debug.Log("Supply Crates: Loc7");
					// WoodHouse13
					var house4Veg = Instantiate(vegPrefab, house8Location.m_prefab.transform);
					house4Veg.name = vegPrefab.name;
					house4Veg.transform.localPosition = new Vector3(1.53f, 0f, 3.54f);
					Debug.Log("Supply Crates: Loc8");
				}
				if (BlackForestEnable.Value == true)
				{
					// Load Locations to Edit
					var trollLocation = ZoneManager.Instance.GetZoneLocation("TrollCave02");
					var runeLocation = ZoneManager.Instance.GetZoneLocation("Runestone_Greydwarfs");
					var crypt2Location = ZoneManager.Instance.GetZoneLocation("Crypt2");
					var ruin1Location = ZoneManager.Instance.GetZoneLocation("Ruin1");
					var crypt3Location = ZoneManager.Instance.GetZoneLocation("Crypt3");
					var tower1Location = ZoneManager.Instance.GetZoneLocation("StoneTowerRuins03");
					// Add objects to locations
					// TrollCave02
					var trollFruit = Instantiate(fruitPrefab, trollLocation.m_prefab.transform);
					trollFruit.name = fruitPrefab.name;
					trollFruit.transform.localPosition = new Vector3(4.8f, 0f, 6f);
					Debug.Log("Supply Crates: Loc9");
					// Runestone_Greydwarfs
					var runeFruit = Instantiate(fruitPrefab, runeLocation.m_prefab.transform);
					runeFruit.name = fruitPrefab.name;
					runeFruit.transform.localPosition = new Vector3(0f, 0f, 1.7f);
					Debug.Log("Supply Crates: Loc10");
					// Crypt2
					var cryptFruit = Instantiate(fruitPrefab, crypt2Location.m_prefab.transform);
					cryptFruit.name = fruitPrefab.name;
					cryptFruit.transform.localPosition = new Vector3(-3.23f, 0f, -2.65f);
					Debug.Log("Supply Crates: Loc11");
					// Ruin1
					var ruinFruit = Instantiate(fruitPrefab, ruin1Location.m_prefab.transform);
					ruinFruit.name = fruitPrefab.name;
					ruinFruit.transform.localPosition = new Vector3(0f, 0f, 6.3f);
					Debug.Log("Supply Crates: Loc12");
					// Crypt3
					var cryptVeg = Instantiate(vegPrefab, crypt3Location.m_prefab.transform);
					cryptVeg.name = vegPrefab.name;
					cryptVeg.transform.localPosition = new Vector3(1.88f, -2.75f, 1.94f);
					Debug.Log("Supply Crates: Loc13");
					// StoneTowerRuins03
					var tower1Veg = Instantiate(vegPrefab, tower1Location.m_prefab.transform);
					tower1Veg.name = vegPrefab.name;
					tower1Veg.transform.localPosition = new Vector3(2.98f, 0f, -5.24f);
					Debug.Log("Supply Crates: Loc14");
				}
				if (SwampEnable.Value == true)
                {
					var rune1Location = ZoneManager.Instance.GetZoneLocation("Runestone_Draugr");
					var crypt4Location = ZoneManager.Instance.GetZoneLocation("SunkenCrypt1");
					var crypt5Location = ZoneManager.Instance.GetZoneLocation("SunkenCrypt4");
					// Runestone_Draugr
					var runeVeg = Instantiate(vegPrefab, rune1Location.m_prefab.transform);
					runeVeg.name = vegPrefab.name;
					runeVeg.transform.localPosition = new Vector3(2.98f, 0f, -5.24f);
					Debug.Log("Supply Crates: Loc15");
					// SunkenCrypt1
					var suncrypt1Veg = Instantiate(vegPrefab, crypt4Location.m_prefab.transform);
					suncrypt1Veg.name = vegPrefab.name;
					suncrypt1Veg.transform.localPosition = new Vector3(-4.2f, 0f, 1f);
					Debug.Log("Supply Crates: Loc16");
					// SunkenCrypt4
					var suncrypt2Veg = Instantiate(vegPrefab, crypt5Location.m_prefab.transform);
					suncrypt2Veg.name = vegPrefab.name;
					suncrypt2Veg.transform.localPosition = new Vector3(-3.64f, 0f, -2.77f);
					Debug.Log("Supply Crates: Loc17");
                }
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating Locations: {ex}");
			}
            finally
			{
				SupplyBundle?.Unload(unloadAllLoadedObjects: false);
			}
		}
	}
}
