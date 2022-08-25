﻿using System.Collections.Generic;
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

namespace FarmyardAnimals
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.HardDependency)]
	internal class farmyardBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.FarmyardAnimals";

		public const string PluginName = "FarmyardAnimals";

		public const string PluginVersion = "0.2.1";

		public static bool isModded = true;

		internal static ManualLogSource Log;
		// Animals
		public static GameObject Sheep;
		public static GameObject Lamb;
		public static GameObject Goat;
		public static GameObject Goose;
		public static GameObject Gosling;
		public static GameObject ChickenB;
		public static GameObject ChickB;
		public static GameObject EggB;
		public static GameObject ChickenBW;
		public static GameObject ChickBW;
		public static GameObject EggBW;
		public static GameObject ChickenW;
		public static GameObject ChickW;
		public static GameObject EggW;
		public static GameObject CowBW;
		public static GameObject LonghornB;
		public static GameObject LonghornW;
		public static GameObject Highland;
		public static GameObject CowB;
		public static GameObject Chester;
		public static GameObject PiggletC;
		public static GameObject Oxford;
		public static GameObject PiggletO;
		public static GameObject Mulefoot;
		public static GameObject PiggletM;
		public static GameObject OldSpots;
		public static GameObject PiggletOS;
		public static GameObject EggG;
		public static GameObject TurkeyB;
		public static GameObject TurkeyR;
		public static GameObject TurkeyW;
		public static GameObject TurkeyChickB;
		public static GameObject TurkeyChickR;
		public static GameObject TurkeyChickW;
		// Carcass
		public static GameObject Poultry;
		public static GameObject LegS;
		public static GameObject PieceS;
		public static GameObject QuarterS;
		// Materials
		public static GameObject PoultryLeg;
		public static GameObject PoultryBreast;
		public static GameObject PoultryWhole;
		public static GameObject MeatRoll;
		public static GameObject SmallSteak;
		public static GameObject Steak;
		public static GameObject BurgerMeat;
		public static GameObject MeatChunks;
		public static GameObject PrimeCut;
		public static GameObject CowItem;
		public static GameObject GoatItem;
		public static GameObject chickenItem;
		// Food
		public static GameObject BurgerRound;
		public static GameObject Chop;
		public static GameObject CookedSteak;
		public static GameObject FriedSteak;
		public static GameObject FriedMeat;
		public static GameObject CookedJoint;
		public static GameObject Milk;
		public static GameObject Drumstick;
		public static GameObject CookedBreast;
		public static GameObject RoastedPoultry;
		// Stations
		public static GameObject ButcherStation;
		public static GameObject Marl;
		public static GameObject Thon;
		// Pieces
		public static GameObject MilkCow;
		public static GameObject MilkGoat;
		public static GameObject chickenCoop;
		// Attacks
		public static GameObject AttackCow;
		public static GameObject AttackSheep;
		public static GameObject AttackTurkey;
		// Asset Bundles
		public AssetBundle FarmyardBundle;
		// Config Entries
		public ConfigEntry<bool> SpawnsEnable;
		public ConfigEntry<bool> MilkingEnable;
		public ConfigEntry<bool> BAEnable;
		// Items
		public static GameObject ButcherAxe;
		// Harmony (for localization)
		private Harmony _harmony;
		public void CreateConfigurationValues()
		{
			SpawnsEnable = base.Config.Bind("Spawns", "Enable", defaultValue: true, new ConfigDescription("Enables World Spawns.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MilkingEnable = base.Config.Bind("Milking", "Enable", defaultValue: true, new ConfigDescription("Enables Cow and Goat Milking.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BAEnable = base.Config.Bind("Bone Appetit", "Enable", defaultValue: true, new ConfigDescription("Enables the Chicken Coop.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
		{
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FarmyardAnimals");
			Log = Logger;
			LoadBundle();
			LoadAssets();
			CreateConfigurationValues();
			CreateStations();
			CreateMiscItems();
			CreateMaterials();
			AddRecipes();
			AddFoodItems();
			AddButcherItems();
			AddAttacks();
			AddGoats();
			AddGeese();
			AddSheep();
			AddChickens();
			AddCows();
			AddPigs();
			AddTurkeys();
			if (BAEnable.Value == true) 
			{ 
				AddCoop();
				CreatureManager.OnVanillaCreaturesAvailable += UpdateChickens;
			}
			if (SpawnsEnable.Value == true)
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
			if (MilkingEnable.Value == true)
            {
				CreatePieces();
				CreateMilkItems();
			}
			UnloadBundle();
		}
		public void LoadBundle()
		{
			FarmyardBundle = AssetUtils.LoadAssetBundleFromResources("farmyard", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
            try { 
				//Debug.Log("FarmyardAnimals: Ragdolls");
				GameObject Ragdoll1 = FarmyardBundle.LoadAsset<GameObject>("Chester_RD_FYA");
				CustomPrefab RD1 = new CustomPrefab(Ragdoll1, true);
				PrefabManager.Instance.AddPrefab(RD1);
				GameObject Ragdoll2 = FarmyardBundle.LoadAsset<GameObject>("Chick_RD_FYA");
				CustomPrefab RD2 = new CustomPrefab(Ragdoll2, true);
				PrefabManager.Instance.AddPrefab(RD2);
				GameObject Ragdoll3 = FarmyardBundle.LoadAsset<GameObject>("ChickenB_RD_FYA");
				CustomPrefab RD3 = new CustomPrefab(Ragdoll3, true);
				PrefabManager.Instance.AddPrefab(RD3);
				GameObject Ragdoll4 = FarmyardBundle.LoadAsset<GameObject>("ChickenBW_RD_FYA");
				CustomPrefab RD4 = new CustomPrefab(Ragdoll4, true);
				PrefabManager.Instance.AddPrefab(RD4);
				GameObject Ragdoll5 = FarmyardBundle.LoadAsset<GameObject>("ChickenW_RD_FYA");
				CustomPrefab RD5 = new CustomPrefab(Ragdoll5, true);
				PrefabManager.Instance.AddPrefab(RD5);
				GameObject Ragdoll6 = FarmyardBundle.LoadAsset<GameObject>("CowB_RD_FYA");
				CustomPrefab RD6 = new CustomPrefab(Ragdoll6, true);
				PrefabManager.Instance.AddPrefab(RD6);
				GameObject Ragdoll7 = FarmyardBundle.LoadAsset<GameObject>("CowBW_RD_FYA");
				CustomPrefab RD7 = new CustomPrefab(Ragdoll7, true);
				PrefabManager.Instance.AddPrefab(RD7);
				GameObject Ragdoll8 = FarmyardBundle.LoadAsset<GameObject>("Goat_RD_FYA");
				CustomPrefab RD8 = new CustomPrefab(Ragdoll8, true);
				PrefabManager.Instance.AddPrefab(RD8);
				GameObject Ragdoll9 = FarmyardBundle.LoadAsset<GameObject>("Goose_RD_FYA");
				CustomPrefab RD9 = new CustomPrefab(Ragdoll9, true);
				PrefabManager.Instance.AddPrefab(RD9);
				GameObject Ragdoll10 = FarmyardBundle.LoadAsset<GameObject>("Gosling_RD_FYA");
				CustomPrefab RD10 = new CustomPrefab(Ragdoll10, true);
				PrefabManager.Instance.AddPrefab(RD10);
				GameObject Ragdoll11 = FarmyardBundle.LoadAsset<GameObject>("Highland_RD_FYA");
				CustomPrefab RD11 = new CustomPrefab(Ragdoll11, true);
				PrefabManager.Instance.AddPrefab(RD11);
				GameObject Ragdoll12 = FarmyardBundle.LoadAsset<GameObject>("Lamb_RD_FYA");
				CustomPrefab RD12 = new CustomPrefab(Ragdoll12, true);
				PrefabManager.Instance.AddPrefab(RD12);
				GameObject Ragdoll13 = FarmyardBundle.LoadAsset<GameObject>("LonghornB_RD_FYA");
				CustomPrefab RD13 = new CustomPrefab(Ragdoll13, true);
				PrefabManager.Instance.AddPrefab(RD13);
				GameObject Ragdoll14 = FarmyardBundle.LoadAsset<GameObject>("LonghornW_RD_FYA");
				CustomPrefab RD14 = new CustomPrefab(Ragdoll14, true);
				PrefabManager.Instance.AddPrefab(RD14);
				GameObject Ragdoll15 = FarmyardBundle.LoadAsset<GameObject>("Mulefoot_RD_FYA");
				CustomPrefab RD15 = new CustomPrefab(Ragdoll15, true);
				PrefabManager.Instance.AddPrefab(RD15);
				GameObject Ragdoll16 = FarmyardBundle.LoadAsset<GameObject>("OldSpots_RD_FYA");
				CustomPrefab RD16 = new CustomPrefab(Ragdoll16, true);
				PrefabManager.Instance.AddPrefab(RD16);
				GameObject Ragdoll17 = FarmyardBundle.LoadAsset<GameObject>("Oxford_RD_FYA");
				CustomPrefab RD17 = new CustomPrefab(Ragdoll17, true);
				PrefabManager.Instance.AddPrefab(RD17);
				GameObject Ragdoll18 = FarmyardBundle.LoadAsset<GameObject>("PiggletC_RD_FYA");
				CustomPrefab RD18 = new CustomPrefab(Ragdoll18, true);
				PrefabManager.Instance.AddPrefab(RD18);
				GameObject Ragdoll19 = FarmyardBundle.LoadAsset<GameObject>("PiggletM_RD_FYA");
				CustomPrefab RD19 = new CustomPrefab(Ragdoll19, true);
				PrefabManager.Instance.AddPrefab(RD19);
				GameObject Ragdoll20 = FarmyardBundle.LoadAsset<GameObject>("PiggletO_RD_FYA");
				CustomPrefab RD20 = new CustomPrefab(Ragdoll20, true);
				PrefabManager.Instance.AddPrefab(RD20);
				GameObject Ragdoll21 = FarmyardBundle.LoadAsset<GameObject>("PiggletOS_RD_FYA");
				CustomPrefab RD21 = new CustomPrefab(Ragdoll21, true);
				PrefabManager.Instance.AddPrefab(RD21);
				GameObject Ragdoll22 = FarmyardBundle.LoadAsset<GameObject>("Sheep_RD_FYA");
				CustomPrefab RD22 = new CustomPrefab(Ragdoll22, true);
				PrefabManager.Instance.AddPrefab(RD22);
				GameObject Ragdoll23 = FarmyardBundle.LoadAsset<GameObject>("TurkeyB_RD_FYA");
				CustomPrefab RD23 = new CustomPrefab(Ragdoll23, true);
				PrefabManager.Instance.AddPrefab(RD23);
				GameObject Ragdoll24 = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickB_RD_FYA");
				CustomPrefab RD24 = new CustomPrefab(Ragdoll24, true);
				PrefabManager.Instance.AddPrefab(RD24);
				GameObject Ragdoll25 = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickR_RD_FYA");
				CustomPrefab RD25 = new CustomPrefab(Ragdoll25, true);
				PrefabManager.Instance.AddPrefab(RD25);
				GameObject Ragdoll26 = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickW_RD_FYA");
				CustomPrefab RD26 = new CustomPrefab(Ragdoll26, true);
				PrefabManager.Instance.AddPrefab(RD26);
				GameObject Ragdoll27 = FarmyardBundle.LoadAsset<GameObject>("TurkeyR_RD_FYA");
				CustomPrefab RD27 = new CustomPrefab(Ragdoll27, true);
				PrefabManager.Instance.AddPrefab(RD27);
				GameObject Ragdoll28 = FarmyardBundle.LoadAsset<GameObject>("TurkeyW_RD_FYA");
				CustomPrefab RD28 = new CustomPrefab(Ragdoll28, true);
				PrefabManager.Instance.AddPrefab(RD28);
				//Debug.Log("FarmyardAnimals: Chicken Items");
				chickenCoop = FarmyardBundle.LoadAsset<GameObject>("ChickenCoop_BAA");
				chickenItem = FarmyardBundle.LoadAsset<GameObject>("ChickenItem_BAA");
				ButcherAxe = FarmyardBundle.LoadAsset<GameObject>("ButcherAxe_FYA");
				//Debug.Log("FarmyardAnimals: Animal Items");
				CowItem = FarmyardBundle.LoadAsset<GameObject>("CowItem_FYA");
				GoatItem = FarmyardBundle.LoadAsset<GameObject>("GoatItem_FYA");

				//Debug.Log("FarmyardAnimals: Attacks");
				AttackCow = FarmyardBundle.LoadAsset<GameObject>("Cow_Attack_FYA");
				AttackSheep = FarmyardBundle.LoadAsset<GameObject>("Sheep_Attack_FYA");
				AttackTurkey = FarmyardBundle.LoadAsset<GameObject>("Turkey_Attack_FYA");

				//Debug.Log("FarmyardAnimals: Carcass Parts");
				Poultry = FarmyardBundle.LoadAsset<GameObject>("PoultryCarcass_FYA");
				LegS = FarmyardBundle.LoadAsset<GameObject>("LegS_FYA");
				PieceS = FarmyardBundle.LoadAsset<GameObject>("PieceS_FYA");
				QuarterS = FarmyardBundle.LoadAsset<GameObject>("QuarterS_FYA");

				//Debug.Log("FarmyardAnimals: Materials");
				PoultryLeg = FarmyardBundle.LoadAsset<GameObject>("PoultryLeg_FYA");
				PoultryBreast = FarmyardBundle.LoadAsset<GameObject>("PoultryBreast_FYA");
				PoultryWhole = FarmyardBundle.LoadAsset<GameObject>("PoultryWhole_FYA");
				MeatRoll = FarmyardBundle.LoadAsset<GameObject>("MeatRoll_FYA");
				SmallSteak = FarmyardBundle.LoadAsset<GameObject>("SmallSteak_FYA");
				Steak = FarmyardBundle.LoadAsset<GameObject>("Steak_FYA");
				BurgerMeat = FarmyardBundle.LoadAsset<GameObject>("BurgerMeat_FYA");
				MeatChunks = FarmyardBundle.LoadAsset<GameObject>("DicedMeat_FYA");
				PrimeCut = FarmyardBundle.LoadAsset<GameObject>("PrimeCut_FYA");

				//Debug.Log("FarmyardAnimals: Food");
				BurgerRound = FarmyardBundle.LoadAsset<GameObject>("BurgerRound_FYA");
				Chop = FarmyardBundle.LoadAsset<GameObject>("Chop_FYA");
				CookedSteak = FarmyardBundle.LoadAsset<GameObject>("CookedSteak_FYA");
				FriedSteak = FarmyardBundle.LoadAsset<GameObject>("FriedSteak_FYA");
				FriedMeat = FarmyardBundle.LoadAsset<GameObject>("FriedMeat_FYA");
				CookedJoint = FarmyardBundle.LoadAsset<GameObject>("CookedJoint_FYA");
				Drumstick = FarmyardBundle.LoadAsset<GameObject>("Drumstick_FYA");
				CookedBreast = FarmyardBundle.LoadAsset<GameObject>("CookedBreast_FYA");
				RoastedPoultry = FarmyardBundle.LoadAsset<GameObject>("RoastPoultry_FYA");
				Milk = FarmyardBundle.LoadAsset<GameObject>("Milk_FYA");

				//Debug.Log("FarmyardAnimals: Stations");
				ButcherStation = FarmyardBundle.LoadAsset<GameObject>("ButchersBench_FYA");
				Marl = FarmyardBundle.LoadAsset<GameObject>("Piece_Marl_FYA");
				Thon = FarmyardBundle.LoadAsset<GameObject>("Piece_Thon_FYA");

				//Debug.Log("FarmyardAnimals: Pieces");
				MilkCow = FarmyardBundle.LoadAsset<GameObject>("CowStall_FYA");
				MilkGoat = FarmyardBundle.LoadAsset<GameObject>("GoatStall_FYA");

				//Debug.Log("FarmyardAnimals: Creatures");
				TurkeyB = FarmyardBundle.LoadAsset<GameObject>("TurkeyB_FYA");
				TurkeyR = FarmyardBundle.LoadAsset<GameObject>("TurkeyR_FYA");
				TurkeyW = FarmyardBundle.LoadAsset<GameObject>("TurkeyW_FYA");
				TurkeyChickB = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickB_FYA");
				TurkeyChickR = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickR_FYA");
				TurkeyChickW = FarmyardBundle.LoadAsset<GameObject>("TurkeyChickW_FYA");
				Sheep = FarmyardBundle.LoadAsset<GameObject>("Sheep_FYA");
				Lamb = FarmyardBundle.LoadAsset<GameObject>("Lamb_FYA");
				Goat = FarmyardBundle.LoadAsset<GameObject>("Goat_FYA");
				Gosling = FarmyardBundle.LoadAsset<GameObject>("Gosling_FYA");
				Goose = FarmyardBundle.LoadAsset<GameObject>("Goose_FYA");
				ChickenB = FarmyardBundle.LoadAsset<GameObject>("ChickenB_FYA");
				ChickB = FarmyardBundle.LoadAsset<GameObject>("ChickB_FYA");
				EggB = FarmyardBundle.LoadAsset<GameObject>("EggB_FYA");
				ChickenBW = FarmyardBundle.LoadAsset<GameObject>("ChickenBW_FYA");
				ChickBW = FarmyardBundle.LoadAsset<GameObject>("ChickBW_FYA");
				EggBW = FarmyardBundle.LoadAsset<GameObject>("EggBW_FYA");
				ChickenW = FarmyardBundle.LoadAsset<GameObject>("ChickenW_FYA");
				ChickW = FarmyardBundle.LoadAsset<GameObject>("ChickW_FYA");
				EggW = FarmyardBundle.LoadAsset<GameObject>("EggW_FYA");
				CowB = FarmyardBundle.LoadAsset<GameObject>("CowB_FYA");
				CowBW = FarmyardBundle.LoadAsset<GameObject>("CowBW_FYA");
				LonghornB = FarmyardBundle.LoadAsset<GameObject>("LonghornB_FYA");
				LonghornW = FarmyardBundle.LoadAsset<GameObject>("LonghornW_FYA");
				Highland = FarmyardBundle.LoadAsset<GameObject>("Highland_FYA");
				Chester = FarmyardBundle.LoadAsset<GameObject>("Chester_FYA");
				PiggletC = FarmyardBundle.LoadAsset<GameObject>("PiggletC_FYA");
				Oxford = FarmyardBundle.LoadAsset<GameObject>("Oxford_FYA");
				PiggletO = FarmyardBundle.LoadAsset<GameObject>("PiggletO_FYA");
				Mulefoot = FarmyardBundle.LoadAsset<GameObject>("Mulefoot_FYA");
				PiggletM = FarmyardBundle.LoadAsset<GameObject>("PiggletM_FYA");
				OldSpots = FarmyardBundle.LoadAsset<GameObject>("OldSpots_FYA");
				PiggletOS = FarmyardBundle.LoadAsset<GameObject>("PiggletOS_FYA");
				EggG = FarmyardBundle.LoadAsset<GameObject>("EggG_FYA");

				//Debug.Log("FarmyardAnimals: SFX");
				GameObject SFXCattle1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Cattle_Idle_WL");
				CustomPrefab SFX1 = new CustomPrefab(SFXCattle1, false);
				PrefabManager.Instance.AddPrefab(SFX1);
				GameObject SFXCattle2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Cattle_Hit_WL");
				CustomPrefab SFX2 = new CustomPrefab(SFXCattle2, false);
				PrefabManager.Instance.AddPrefab(SFX2);
				GameObject SFXCattle3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Cattle_GetHit_WL");
				CustomPrefab SFX3 = new CustomPrefab(SFXCattle3, false);
				PrefabManager.Instance.AddPrefab(SFX3);
				GameObject SFXLonghorn1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Longhorn_Idle_WL");
				CustomPrefab SFX4 = new CustomPrefab(SFXLonghorn1, false);
				PrefabManager.Instance.AddPrefab(SFX4);
				GameObject SFXHighland1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Scotland_Idle_WL");
				CustomPrefab SFX5 = new CustomPrefab(SFXHighland1, false);
				PrefabManager.Instance.AddPrefab(SFX5);
				GameObject SFXChicken1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Chicken_Idle_WL");
				CustomPrefab SFX6 = new CustomPrefab(SFXChicken1, false);
				PrefabManager.Instance.AddPrefab(SFX6);
				GameObject SFXChicken2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Chicken_Hit_WL");
				CustomPrefab SFX7 = new CustomPrefab(SFXChicken2, false);
				PrefabManager.Instance.AddPrefab(SFX7);
				GameObject SFXChicken3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Chicken_GetHit_WL");
				CustomPrefab SFX8 = new CustomPrefab(SFXChicken3, false);
				PrefabManager.Instance.AddPrefab(SFX8);
				GameObject SFXPig1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Pig_Idle_WL");
				CustomPrefab SFX9 = new CustomPrefab(SFXPig1, false);
				PrefabManager.Instance.AddPrefab(SFX9);
				GameObject SFXPig2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Pig_Hit_WL");
				CustomPrefab SFX10 = new CustomPrefab(SFXPig2, false);
				PrefabManager.Instance.AddPrefab(SFX10);
				GameObject SFXPig3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Pig_GetHit_WL");
				CustomPrefab SFX11 = new CustomPrefab(SFXPig3, false);
				PrefabManager.Instance.AddPrefab(SFX11);
				GameObject SFXButcherChop = FarmyardBundle.LoadAsset<GameObject>("SFX_ButcherChop_FYA");
				CustomPrefab SFX12 = new CustomPrefab(SFXButcherChop, false);
				PrefabManager.Instance.AddPrefab(SFX12);
				GameObject SFXSheep1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Sheep_Death_FYA");
				CustomPrefab SFX13 = new CustomPrefab(SFXSheep1, false);
				PrefabManager.Instance.AddPrefab(SFX13);
				GameObject SFXSheep2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Sheep_Idle_FYA");
				CustomPrefab SFX14 = new CustomPrefab(SFXSheep2, false);
				PrefabManager.Instance.AddPrefab(SFX14);
				GameObject SFXSheep3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Sheep_Footstep_FYA");
				CustomPrefab SFX15 = new CustomPrefab(SFXSheep3, false);
				PrefabManager.Instance.AddPrefab(SFX15);
				GameObject SFXGoat1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Goat_Death_FYA");
				CustomPrefab SFX16 = new CustomPrefab(SFXGoat1, false);
				PrefabManager.Instance.AddPrefab(SFX16);
				GameObject SFXGoat2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Goat_Idle_FYA");
				CustomPrefab SFX17 = new CustomPrefab(SFXGoat2, false);
				PrefabManager.Instance.AddPrefab(SFX17);
				GameObject SFXGoat3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Goat_Alert_FYA");
				CustomPrefab SFX18 = new CustomPrefab(SFXGoat3, false);
				PrefabManager.Instance.AddPrefab(SFX18);
				GameObject SFXGoose1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Goose_Death_FYA");
				CustomPrefab SFX19 = new CustomPrefab(SFXGoose1, false);
				PrefabManager.Instance.AddPrefab(SFX19);
				GameObject SFXGoose2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Goose_Idle_FYA");
				CustomPrefab SFX20 = new CustomPrefab(SFXGoose2, false);
				PrefabManager.Instance.AddPrefab(SFX20);
				GameObject SFXTurkey1 = FarmyardBundle.LoadAsset<GameObject>("SFX_Turkey_Alert_FYA");
				CustomPrefab SFX21 = new CustomPrefab(SFXTurkey1, false);
				PrefabManager.Instance.AddPrefab(SFX21);
				GameObject SFXTurkey2 = FarmyardBundle.LoadAsset<GameObject>("SFX_Turkey_Death_FYA");
				CustomPrefab SFX22 = new CustomPrefab(SFXTurkey2, false);
				PrefabManager.Instance.AddPrefab(SFX22);
				GameObject SFXTurkey3 = FarmyardBundle.LoadAsset<GameObject>("SFX_Turkey_GetHit_FYA");
				CustomPrefab SFX23 = new CustomPrefab(SFXTurkey3, false);
				PrefabManager.Instance.AddPrefab(SFX23);
				GameObject SFXTurkey4 = FarmyardBundle.LoadAsset<GameObject>("SFX_Turkey_Idle_FYA");
				CustomPrefab SFX24 = new CustomPrefab(SFXTurkey4, false);
				PrefabManager.Instance.AddPrefab(SFX24);

				//Debug.Log("FarmyardAnimals: VFX");
				GameObject VFXCarcass = FarmyardBundle.LoadAsset<GameObject>("VFX_Carcass_Destruction_FYA");
				CustomPrefab VFX1 = new CustomPrefab(VFXCarcass, false);
				PrefabManager.Instance.AddPrefab(VFX1);
				GameObject VFXCorpse = FarmyardBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_FYA");
				CustomPrefab VFX2 = new CustomPrefab(VFXCorpse, false);
				PrefabManager.Instance.AddPrefab(VFX2);
				GameObject VFXHit = FarmyardBundle.LoadAsset<GameObject>("VFX_Blood_Hit_FYA");
				CustomPrefab VFX3 = new CustomPrefab(VFXHit, false);
				PrefabManager.Instance.AddPrefab(VFX3);
				GameObject VFXDeath = FarmyardBundle.LoadAsset<GameObject>("VFX_Animal_Death_FYA");
				CustomPrefab VFX4 = new CustomPrefab(VFXDeath, false);
				PrefabManager.Instance.AddPrefab(VFX4);
				GameObject VFXHeart = FarmyardBundle.LoadAsset<GameObject>("VFX_Heart_FYA");
				CustomPrefab VFX5 = new CustomPrefab(VFXHeart, false);
				PrefabManager.Instance.AddPrefab(VFX5);
				GameObject VFXStar = FarmyardBundle.LoadAsset<GameObject>("VFX_Star_FYA");
				CustomPrefab VFX6 = new CustomPrefab(VFXStar, false);
				PrefabManager.Instance.AddPrefab(VFX6);

				//Debug.Log("FarmyardAnimals: Carcass");
				GameObject Corpse = FarmyardBundle.LoadAsset<GameObject>("CarcassS_FYA");
				CustomPrefab Corpse1 = new CustomPrefab(Corpse, false);
				PrefabManager.Instance.AddPrefab(Corpse1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding FYA assets: {ex}");
			}
		}
		private void AddCoop()
		{
            try
			{
				// Chicken Item
				GameObject dropable1 = chickenItem;
				CustomItem customItem1 = new CustomItem(dropable1, true);
				ItemManager.Instance.AddItem(customItem1);
				// Piece
				var customPiece1 = new CustomPiece(chickenCoop, true, new PieceConfig
				{
					PieceTable = "_HammerPieceTable",
					Category = "Misc",
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "rk_egg",
							Amount = 10,
							Recover = true
						},
						new RequirementConfig
						{
							Item = "FineWood",
							Amount = 25,
							Recover = true
						},
						new RequirementConfig
						{
							Item = "Stone",
							Amount = 10,
							Recover = true
						},
						new RequirementConfig
						{
							Item = "ChickenItem_BAA",
							Amount = 1,
							Recover = true
						}
					}
				});
				PieceManager.Instance.AddPiece(customPiece1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Chicken Coop for Bone Appetite: {ex}");
			}
		}
		private void UpdateChickens()
        {
            try
			{
				// Update Chickens
				var chicken = PrefabManager.Instance.GetPrefab("ChickenItem_BAA");
				var chickenB = CreatureManager.Instance.GetCreaturePrefab("ChickenB_FYA");
					chickenB.GetComponent<CharacterDrop>().m_drops.Add(new CharacterDrop.Drop { 
						m_prefab = chicken,
						m_chance = 10f,
						m_amountMin = 1,
						m_amountMax = 1
				});
				var chickenBW = CreatureManager.Instance.GetCreaturePrefab("ChickenBW_FYA");
					chickenBW.GetComponent<CharacterDrop>().m_drops.Add(new CharacterDrop.Drop
					{
						m_prefab = chicken,
						m_chance = 10f,
						m_amountMin = 1,
						m_amountMax = 1
				});
				var chickenW = CreatureManager.Instance.GetCreaturePrefab("ChickenW_FYA");
					chickenW.GetComponent<CharacterDrop>().m_drops.Add(new CharacterDrop.Drop
					{
						m_prefab = chicken,
						m_chance = 10f,
						m_amountMin = 1,
						m_amountMax = 1
				});
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating Chickens for Bone Appetite: {ex}");
			}
			finally
			{
				CreatureManager.OnVanillaCreaturesAvailable -= UpdateChickens;
			}
		}
		private void CreateMilkItems()
        {
			GameObject dropable7 = CowItem;
			CustomItem customItem7 = new CustomItem(dropable7, false);
			ItemManager.Instance.AddItem(customItem7);
			GameObject dropable6 = GoatItem;
			CustomItem customItem6 = new CustomItem(dropable6, false);
			ItemManager.Instance.AddItem(customItem6);
			GameObject dropable2 = Milk;
			CustomItem customItem2 = new CustomItem(dropable2, false);
			ItemManager.Instance.AddItem(customItem2);
		}
		private void CreateMiscItems()
		{
			GameObject dropable5 = QuarterS;
			CustomItem customItem5 = new CustomItem(dropable5, false);
			ItemManager.Instance.AddItem(customItem5);
			GameObject dropable4 = PieceS;
			CustomItem customItem4 = new CustomItem(dropable4, false);
			ItemManager.Instance.AddItem(customItem4);
			GameObject dropable3 = LegS;
			CustomItem customItem3 = new CustomItem(dropable3, false);
			ItemManager.Instance.AddItem(customItem3);
			GameObject dropable1 = Poultry;
			CustomItem customItem1 = new CustomItem(dropable1, false);
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateMaterials()
		{
			// Burger Meat
			//Debug.Log("FarmyardAnimals: BurgerMeat");
			GameObject material9 = BurgerMeat;
			CustomItem customMat9 = new CustomItem(material9, false, new ItemConfig
			{
				Amount = 4,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "DicedMeat_FYA",
						Amount = 6
					}
				}
			});
			ItemManager.Instance.AddItem(customMat9);
			// Chop
			//Debug.Log("FarmyardAnimals: PrimeCut");
			GameObject material8 = PrimeCut;
			CustomItem customMat8 = new CustomItem(material8, false, new ItemConfig
			{
				Amount = 8,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "QuarterS_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat8);
			// Small Steak
			//Debug.Log("FarmyardAnimals: SmallSteak");
			GameObject material7 = SmallSteak;
			CustomItem customMat7 = new CustomItem(material7, false, new ItemConfig
			{
				Amount = 4,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "QuarterS_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat7);
			// Steak
			//Debug.Log("FarmyardAnimals: Steak");
			GameObject material6 = Steak;
			CustomItem customMat6 = new CustomItem(material6, false, new ItemConfig
			{
				Amount = 4,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "QuarterS_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat6);
			// Meat Chunks
			//Debug.Log("FarmyardAnimals: MeatChunks");
			GameObject material5 = MeatChunks;
			CustomItem customMat5 = new CustomItem(material5, false, new ItemConfig
			{
				Amount = 4,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "LegS_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat5);
			// Meat Roll
			//Debug.Log("FarmyardAnimals: MeatRoll");
			GameObject material4 = MeatRoll;
			CustomItem customMat4 = new CustomItem(material4, false, new ItemConfig
			{
				Amount = 1,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "LegS_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat4);
			// Poultry Whole
			//Debug.Log("FarmyardAnimals: PoultryWhole");
			GameObject material3 = PoultryWhole;
			CustomItem customMat3 = new CustomItem(material3, false, new ItemConfig
			{
				Amount = 1,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "PoultryCarcass_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat3);
			// Poultry Breast
			//Debug.Log("FarmyardAnimals: PoultryBreast");
			GameObject material2 = PoultryBreast;
			CustomItem customMat2 = new CustomItem(material2, false, new ItemConfig
			{
				Amount = 2,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "PoultryCarcass_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat2);
			// Poultry Leg
			//Debug.Log("FarmyardAnimals: PoultryLeg");
			GameObject material1 = PoultryLeg;
			CustomItem customMat1 = new CustomItem(material1, false, new ItemConfig
			{
				Amount = 2,
				CraftingStation = "ButchersBench_FYA",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "PoultryCarcass_FYA",
						Amount = 1
					}
				}
			});
			ItemManager.Instance.AddItem(customMat1);
		}
		private void AddRecipes()
		{
			// Meat Chunks Small
			CustomRecipe meatRecipe1 = new CustomRecipe(new RecipeConfig()
			{
				Name = "$item_meatchunks3_fya",
				Amount = 3,
				Item = "DicedMeat_FYA",
				CraftingStation = "ButchersBench_FYA",
				Requirements = new RequirementConfig[]
				{
					new RequirementConfig { Item = "PieceS_FYA", Amount = 1 }
				}
			});
			ItemManager.Instance.AddRecipe(meatRecipe1);
			// Meat Chunks Small
			CustomRecipe meatRecipe2 = new CustomRecipe(new RecipeConfig()
			{
				Name = "$item_meatchunks16_fya",
				Amount = 16,
				Item = "DicedMeat_FYA",
				CraftingStation = "ButchersBench_FYA",
				Requirements = new RequirementConfig[]
				{
					new RequirementConfig { Item = "QuarterS_FYA", Amount = 1 }
				}
			});
			ItemManager.Instance.AddRecipe(meatRecipe2);
		}
		private void AddFoodItems()
		{
			GameObject dropable9 = RoastedPoultry;
			CustomItem customItem9 = new CustomItem(dropable9, false);
			ItemManager.Instance.AddItem(customItem9);
			GameObject dropable8 = CookedBreast;
			CustomItem customItem8 = new CustomItem(dropable8, false);
			ItemManager.Instance.AddItem(customItem8);
			GameObject dropable7 = Drumstick;
			CustomItem customItem7 = new CustomItem(dropable7, false);
			ItemManager.Instance.AddItem(customItem7);
			GameObject dropable6 = CookedJoint;
			CustomItem customItem6 = new CustomItem(dropable6, false);
			ItemManager.Instance.AddItem(customItem6);
			GameObject dropable5 = FriedMeat;
			CustomItem customItem5 = new CustomItem(dropable5, false);
			ItemManager.Instance.AddItem(customItem5);
			GameObject dropable4 = FriedSteak;
			CustomItem customItem4 = new CustomItem(dropable4, false);
			ItemManager.Instance.AddItem(customItem4);
			GameObject dropable3 = CookedSteak;
			CustomItem customItem3 = new CustomItem(dropable3, false);
			ItemManager.Instance.AddItem(customItem3);
			GameObject dropable2 = Chop;
			CustomItem customItem2 = new CustomItem(dropable2, false);
			ItemManager.Instance.AddItem(customItem2);
			GameObject dropable1 = BurgerRound;
			CustomItem customItem1 = new CustomItem(dropable1, false);
			ItemManager.Instance.AddItem(customItem1);
		}
		private void AddAttacks()
		{
			GameObject attack1 = AttackCow;
			CustomItem cowAttack = new CustomItem(attack1, false);
			ItemManager.Instance.AddItem(cowAttack);
			GameObject attack2 = AttackSheep;
			CustomItem sheepAttack = new CustomItem(attack2, false);
			ItemManager.Instance.AddItem(sheepAttack);
			GameObject attack3 = AttackTurkey;
			CustomItem turkeyAttack = new CustomItem(attack3, false);
			ItemManager.Instance.AddItem(turkeyAttack);
		}
		private void CreateStations()
		{
			var customPiece1 = new CustomPiece(ButcherStation, false, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[3
				]
				{
					new RequirementConfig
					{
						Item = "LeatherScraps",
						Amount = 10,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 10,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 15,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);

			var customPiece2 = new CustomPiece(Marl, false, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[3
				]
				{
					new RequirementConfig
					{
						Item = "Copper",
						Amount = 8,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 8,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 3,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);

			var customPiece3 = new CustomPiece(Thon, false, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[3
				]
				{
					new RequirementConfig
					{
						Item = "Copper",
						Amount = 8,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 8,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 3,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
		}
		private void CreatePieces()
		{
			var customPiece2 = new CustomPiece(MilkCow, false, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Milk_FYA",
						Amount = 12,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "CowItem_FYA",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Carrot",
						Amount = 20,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 50,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			var customPiece3 = new CustomPiece(MilkGoat, false, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Milk_FYA",
						Amount = 12,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "GoatItem_FYA",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Carrot",
						Amount = 12,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 30,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
		}
		private void AddButcherItems()
		{
			GameObject item1 = ButcherAxe;
			CustomItem butcheraxe = new CustomItem(item1, true, new ItemConfig
			{
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Iron",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DeerHide",
					Amount = 1,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(butcheraxe);
		}
        private void AddTurkeys()
        {
            try
			{
				//Debug.Log("FYA: TurkeyB");
				var turkeyBFab = TurkeyB;
				var TurkeyBMob = new CustomCreature(turkeyBFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 5
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyBMob);

				//Debug.Log("FYA: TurkeyR");
				var turkeyRFab = TurkeyR;
				var TurkeyRMob = new CustomCreature(turkeyRFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 5
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyRMob);

				//Debug.Log("FYA: TurkeyW");
				var turkeyWFab = TurkeyW;
				var TurkeyWMob = new CustomCreature(turkeyWFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 5
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyWMob);

				//Debug.Log("FYA: TurkeyChickB");
				var turkeyChickBFab = TurkeyChickB;
				var TurkeyChickBMob = new CustomCreature(turkeyChickBFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyChickBMob);

				//Debug.Log("FYA: TurkeyChickR");
				var turkeyChickRFab = TurkeyChickR;
				var TurkeyChickRMob = new CustomCreature(turkeyChickRFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyChickRMob);

				//Debug.Log("FYA: TurkeyChickW");
				var turkeyChickWFab = TurkeyChickW;
				var TurkeyChickWMob = new CustomCreature(turkeyChickWFab, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(TurkeyChickWMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Turkeys: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Turkeys Added");
			}
		}
		private void AddPigs()
        {
            try
			{
				//Debug.Log("FYA: PiggletOS");
				var mobFab1 = PiggletOS;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);

				//Debug.Log("FYA: OldSpots");
				var mobFab2 = OldSpots;
				var customMob2 = new CustomCreature(mobFab2, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob2);

				//Debug.Log("FYA: PiggletM");
				var mobFab3 = PiggletM;
				var customMob3 = new CustomCreature(mobFab3, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob3);

				//Debug.Log("FYA: Mulefoot");
				var mobFab4 = Mulefoot;
				var customMob4 = new CustomCreature(mobFab4, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob4);

				//Debug.Log("FYA: PiggletC");
				var mobFab5 = PiggletC;
				var customMob5 = new CustomCreature(mobFab5, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob5);

				//Debug.Log("FYA: Chester");
				var mobFab6 = Chester;
				var customMob6 = new CustomCreature(mobFab6, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob6);

				//Debug.Log("FYA: PiggletO");
				var mobFab7 = PiggletO;
				var customMob7 = new CustomCreature(mobFab7, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob7);

				//Debug.Log("FYA: Oxford");
				var mobFab8 = Oxford;
				var customMob8 = new CustomCreature(mobFab8, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob8);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Pigs: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Pigs Added");
			}
		}
		private void AddCows()
        {
            try
			{
				//Debug.Log("FYA: Highland");
				var mobFab1 = Highland;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 33,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "CowItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);

				//Debug.Log("FYA: LonghornW");
				var mobFab2 = LonghornW;
				var customMob2 = new CustomCreature(mobFab2, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 33,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "CowItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob2);

				//Debug.Log("FYA: LonghornB");
				var mobFab3 = LonghornB;
				var customMob3 = new CustomCreature(mobFab3, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 33,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "CowItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob3);

				//Debug.Log("FYA: CowBW");
				var mobFab4 = CowBW;
				var customMob4 = new CustomCreature(mobFab4, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 33,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "CowItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob4);

				//Debug.Log("FYA: CowB");
				var mobFab5 = CowB;
				var customMob5 = new CustomCreature(mobFab5, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 100,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 33,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "CowItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob5);

			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Cows: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Cows Added");
			}
		}
		private void AddChickens()
		{
			try
			{
				//Debug.Log("FYA: ChickW");
				var mobFab1 = ChickW;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);

				//Debug.Log("FYA: ChickenW");
				var mobFab2 = ChickenW;
				var customMob2 = new CustomCreature(mobFab2, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob2);

				//Debug.Log("FYA: ChickBW");
				var mobFab3 = ChickBW;
				var customMob3 = new CustomCreature(mobFab3, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob3);

				//Debug.Log("FYA: ChickenBW");
				var mobFab4 = ChickenBW;
				var customMob4 = new CustomCreature(mobFab4, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob4);

				//Debug.Log("FYA: ChickB");
				var mobFab5 = ChickB;
				var customMob5 = new CustomCreature(mobFab5, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob5);

				//Debug.Log("FYA: ChickenB");
				var mobFab6 = ChickenB;
				var customMob6 = new CustomCreature(mobFab6, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob6);

			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Chickens: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Chickens Added");
			}
		}
		private void AddSheep()
        {
            try
			{
				//Debug.Log("FYA: Lamb");
				var mobFab1 = Lamb;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);

				//Debug.Log("FYA: Sheep");
				var mobFab2 = Sheep;
				var customMob2 = new CustomCreature(mobFab2, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob2);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Sheep: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Sheep Added");
			}
		}
		private void AddGeese()
        {
            try
			{
				//Debug.Log("FYA: Gosling");
				var mobFab1 = Gosling;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 50,
								MinAmount = 1,
								MaxAmount = 2
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);

				//Debug.Log("FYA: Goose");
				var mobFab2 = Goose;
				var customMob2 = new CustomCreature(mobFab2, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Feathers",
								Chance = 100,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "PoultryCarcass_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob2);

			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Geese: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Geese Added");
			}
		}
		private void AddGoats()
		{
			try
			{
				//Debug.Log("FYA: Goat");
				var mobFab1 = Goat;
				var customMob1 = new CustomCreature(mobFab1, true,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "LeatherScraps",
								Chance = 50,
								MinAmount = 2,
								MaxAmount = 4
							},
							new DropConfig
							{
								Item = "CarcassS_FYA",
								Chance = 75,
								MinAmount = 1,
								MaxAmount = 1
							},
							new DropConfig
							{
								Item = "Milk_FYA",
								Chance = 25,
								MinAmount = 1,
								MaxAmount = 3
							},
							new DropConfig
							{
								Item = "GoatItem_FYA",
								Chance = 10,
								MinAmount = 1,
								MaxAmount = 1
							}
						}
					});
				CreatureManager.Instance.AddCreature(customMob1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom Goats: {ex}");
			}
			finally
			{
				//Debug.Log("FYA: Goats Added");
			}
		}
		private void AddEggs()
		{
			GameObject animal15 = EggW;
			CustomPrefab critter15 = new CustomPrefab(animal15, true);
			PrefabManager.Instance.AddPrefab(critter15);
			GameObject animal12 = EggBW;
			CustomPrefab critter12 = new CustomPrefab(animal12, true);
			PrefabManager.Instance.AddPrefab(critter12);
			GameObject animal9 = EggB;
			CustomPrefab critter9 = new CustomPrefab(animal9, true);
			PrefabManager.Instance.AddPrefab(critter9);
			GameObject animal5 = EggG;
			CustomPrefab critter5 = new CustomPrefab(animal5, true);
			PrefabManager.Instance.AddPrefab(critter5);
		}
		private void UnloadBundle()
		{
			FarmyardBundle?.Unload(unloadAllLoadedObjects: false);
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Farmyard Animals: Configure Spawns");
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
			//Debug.Log("Farmyard Animals: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(25_024)
					.SetPrefabName("TurkeyW_FYA")
					.SetTemplateName("Turkey White")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("StoneTowerRuins01")
					;
				config.ConfigureWorldSpawner(25_023)
					.SetPrefabName("TurkeyW_FYA")
					.SetTemplateName("Turkey White")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Greydwarf_camp1")
					;
				config.ConfigureWorldSpawner(25_022)
					.SetPrefabName("TurkeyW_FYA")
					.SetTemplateName("Turkey White")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Crypt4")
					;
				config.ConfigureWorldSpawner(25_021)
					.SetPrefabName("TurkeyR_FYA")
					.SetTemplateName("Turkey Red")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Ruin2")
					;
				config.ConfigureWorldSpawner(25_020)
					.SetPrefabName("TurkeyR_FYA")
					.SetTemplateName("Turkey Red")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Crypt2")
					;
				config.ConfigureWorldSpawner(25_019)
					.SetPrefabName("TurkeyR_FYA")
					.SetTemplateName("Turkey Red")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Crypt3")
					;
				config.ConfigureWorldSpawner(25_018)
					.SetPrefabName("TurkeyB_FYA")
					.SetTemplateName("Turkey Black")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("Ruin1")
					;
				config.ConfigureWorldSpawner(25_017)
					.SetPrefabName("TurkeyB_FYA")
					.SetTemplateName("Turkey Black")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("TrollCamp")
					;
				config.ConfigureWorldSpawner(25_016)
					.SetPrefabName("TurkeyB_FYA")
					.SetTemplateName("Turkey Black")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("TrollCave")
					;
				config.ConfigureWorldSpawner(25_015)
					.SetPrefabName("LonghornB_FYA")
					.SetTemplateName("Longhorn Brown")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("StoneHenge3")
					.SetModifierFaction(Character.Faction.PlainsMonsters)
					;
				config.ConfigureWorldSpawner(25_014)
					.SetPrefabName("LonghornW_FYA")
					.SetTemplateName("Longhorn White")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("StoneHenge4")
					.SetModifierFaction(Character.Faction.PlainsMonsters)
					;
				config.ConfigureWorldSpawner(25_013)
					.SetPrefabName("Highland_FYA")
					.SetTemplateName("Highland")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodFarm1")
					;
				config.ConfigureWorldSpawner(25_012)
					.SetPrefabName("CowBW_FYA")
					.SetTemplateName("Cattle")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodFarm1")
					;
				config.ConfigureWorldSpawner(25_011)
					.SetPrefabName("CowB_FYA")
					.SetTemplateName("Cattle")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodFarm1")
					;
				config.ConfigureWorldSpawner(25_010)
					.SetPrefabName("OldSpots_FYA")
					.SetTemplateName("Oxford Pig")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodFarm1")
					;
				config.ConfigureWorldSpawner(25_009)
					.SetPrefabName("Mulefoot_FYA")
					.SetTemplateName("Oxford Pig")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodFarm1")
					;
				config.ConfigureWorldSpawner(25_008)
					.SetPrefabName("Oxford_FYA")
					.SetTemplateName("Oxford Pig")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodVillage1")
					;
				config.ConfigureWorldSpawner(25_007)
					.SetPrefabName("Chester_FYA")
					.SetTemplateName("Chester Pig")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodVillage1")
					;
				config.ConfigureWorldSpawner(25_005)
					.SetPrefabName("Goat_FYA")
					.SetTemplateName("Goat")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(2)
					.SetSpawnDuringNight(false)
					.SetConditionLocation("WoodVillage1")
					;
				config.ConfigureWorldSpawner(25_004)
					.SetPrefabName("Sheep_FYA")
					.SetTemplateName("Sheep")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(2)
					.SetSpawnDuringNight(false)
					.SetConditionLocation("WoodVillage1")
					;
				config.ConfigureWorldSpawner(25_003)
					.SetPrefabName("Goose_FYA")
					.SetTemplateName("Goose")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(2)
					.SetSpawnDuringNight(false)
					.SetConditionLocation("WoodHouse8")
					;
				config.ConfigureWorldSpawner(25_002)
					.SetPrefabName("ChickenW_FYA")
					.SetTemplateName("White Chicken")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(2)
					.SetConditionLocation("WoodHouse8")
					;
				config.ConfigureWorldSpawner(25_001)
					.SetPrefabName("ChickenBW_FYA")
					.SetTemplateName("Brown White Chicken")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(2)
					.SetSpawnDuringNight(false)
					.SetConditionLocation("WoodHouse8")
					;
				config.ConfigureWorldSpawner(25_000)
					.SetPrefabName("ChickenB_FYA")
					.SetTemplateName("Brown Chicken")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(18)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(2)
					.SetSpawnDuringNight(false)
					.SetConditionLocation("WoodHouse8")
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
