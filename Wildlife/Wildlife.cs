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

namespace Wildlife
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class Wildlife : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.Wildlife";

		public const string PluginName = "Wildlife";

		public const string PluginVersion = "0.0.2";

		public static bool isModded = true;

		public static GameObject GreenFrog;
		public static GameObject BlackFrog;
		public static GameObject SpottedFrog;
		public static GameObject GreyRabbit;
		public static GameObject BrownRabbit;
		public static GameObject Sheep;
		public static GameObject Goat;
		public static GameObject Goose;
		public static GameObject Turtle;
		public static GameObject Salamander;
		public static GameObject Penguin;
		public static GameObject Rat;
		public static GameObject BrownLizard;
		public static GameObject GreenLizard;
		public static GameObject SpottedLizard;

		public static GameObject SnappingTurtle;
		public static GameObject Zebra;
		public static GameObject ComodoDragon;
		public static GameObject Camel;

		public AssetBundle WildlifeBundle;
		public AssetBundle ExoticBundle;
		private Harmony _harmony;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		private void Awake() 
		{
			LoadBundle();
			LoadAssets();
			AddNewAnimals();
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.Wildlife");
		}
		public void LoadBundle()
		{
			WildlifeBundle = AssetUtils.LoadAssetBundleFromResources("wildlife", Assembly.GetExecutingAssembly());
			ExoticBundle = AssetUtils.LoadAssetBundleFromResources("africa", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			//Debug.Log("Wildlife: 0");
			SnappingTurtle = ExoticBundle.LoadAsset<GameObject>("Animal_SnappingTurtle");
			Zebra = ExoticBundle.LoadAsset<GameObject>("Animal_Zebra");
			ComodoDragon = ExoticBundle.LoadAsset<GameObject>("Animal_ComodoDragon");
			Camel = ExoticBundle.LoadAsset<GameObject>("Animal_Camel");
			Sheep = WildlifeBundle.LoadAsset<GameObject>("Sheep_WL");
			Goat = WildlifeBundle.LoadAsset<GameObject>("Goat_WL");
			Goose = WildlifeBundle.LoadAsset<GameObject>("Goose_WL");
			Turtle = WildlifeBundle.LoadAsset<GameObject>("Turtle_WL");
			Salamander = WildlifeBundle.LoadAsset<GameObject>("Salamander_WL");
			Penguin = WildlifeBundle.LoadAsset<GameObject>("Penguin_WL");
			Rat = WildlifeBundle.LoadAsset<GameObject>("Rat_WL");
			BrownLizard = WildlifeBundle.LoadAsset<GameObject>("BrownLizard_WL");
			GreenLizard = WildlifeBundle.LoadAsset<GameObject>("GreenLizard_WL");
			SpottedLizard = WildlifeBundle.LoadAsset<GameObject>("SpottedLizard_WL");
			GreenFrog = WildlifeBundle.LoadAsset<GameObject>("GreenFrog_WL");
			BlackFrog = WildlifeBundle.LoadAsset<GameObject>("BlackFrog_WL");
			SpottedFrog = WildlifeBundle.LoadAsset<GameObject>("SpottedFrog_WL");
			GreyRabbit = WildlifeBundle.LoadAsset<GameObject>("GreyRabbit_WL");
			BrownRabbit = WildlifeBundle.LoadAsset<GameObject>("BrownRabbit_WL");
			//Debug.Log("Wildlife: 0");
			GameObject Mock1 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_fx_creature_tamed");
			PrefabManager.Instance.AddPrefab(Mock1);
			GameObject Mock2 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_vfx_creature_soothed");
			PrefabManager.Instance.AddPrefab(Mock2);
			GameObject Mock3 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_vfx_water_surface");
			PrefabManager.Instance.AddPrefab(Mock3);
			GameObject Mock4 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_fx_footstep_water");
			PrefabManager.Instance.AddPrefab(Mock4);
			GameObject Mock5 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_sfx_footstep_swim");
			PrefabManager.Instance.AddPrefab(Mock5);
			GameObject Mock6 = WildlifeBundle.LoadAsset<GameObject>("JVLmock_fx_boar_footstep_walk");
			PrefabManager.Instance.AddPrefab(Mock6);
			//Debug.Log("Wildlife: 0");
			GameObject SFXFrog1 = WildlifeBundle.LoadAsset<GameObject>("SFX_Frog1_Idle_DoD");
			GameObject SFXFrog2 = WildlifeBundle.LoadAsset<GameObject>("SFX_Frog2_Idle_DoD");
			GameObject SFXFrogD = WildlifeBundle.LoadAsset<GameObject>("SFX_Frog_Death_DoD");
			GameObject SFXGoatD = WildlifeBundle.LoadAsset<GameObject>("SFX_Goat_Death_DoD");
			GameObject SFXGoatI = WildlifeBundle.LoadAsset<GameObject>("SFX_Goat_Idle_DoD");
			GameObject SFXGooseD = WildlifeBundle.LoadAsset<GameObject>("SFX_Goose_Death_DoD");
			GameObject SFXGooseI = WildlifeBundle.LoadAsset<GameObject>("SFX_Goose_Idle_DoD");
			GameObject SFXRabbitD = WildlifeBundle.LoadAsset<GameObject>("SFX_Rabbit_Death_DoD");
			GameObject SFXSheepD = WildlifeBundle.LoadAsset<GameObject>("SFX_Sheep_Death_DoD");
			GameObject SFXSheepI = WildlifeBundle.LoadAsset<GameObject>("SFX_Sheep_Idle_DoD");
			GameObject SFXSheepF = WildlifeBundle.LoadAsset<GameObject>("SFX_Sheep_Footstep_DoD");
			GameObject SFXPenguinD = WildlifeBundle.LoadAsset<GameObject>("SFX_Penguin_Death_DoD");
			GameObject SFXSalamanderD = WildlifeBundle.LoadAsset<GameObject>("SFX_Salamander_Death_DoD");
			GameObject SFXRatD = WildlifeBundle.LoadAsset<GameObject>("SFX_Rat_Death_DoD");
			GameObject SFXLizardD = WildlifeBundle.LoadAsset<GameObject>("SFX_Lizard_Death_DoD");
			PrefabManager.Instance.AddPrefab(SFXFrog1);
			PrefabManager.Instance.AddPrefab(SFXFrog2);
			PrefabManager.Instance.AddPrefab(SFXFrogD);
			PrefabManager.Instance.AddPrefab(SFXRabbitD);
			PrefabManager.Instance.AddPrefab(SFXLizardD);
			PrefabManager.Instance.AddPrefab(SFXRatD);
			PrefabManager.Instance.AddPrefab(SFXSalamanderD);
			PrefabManager.Instance.AddPrefab(SFXPenguinD);
			PrefabManager.Instance.AddPrefab(SFXGoatI);
			PrefabManager.Instance.AddPrefab(SFXGoatD);
			PrefabManager.Instance.AddPrefab(SFXSheepF);
			PrefabManager.Instance.AddPrefab(SFXSheepI);
			PrefabManager.Instance.AddPrefab(SFXSheepD);
			PrefabManager.Instance.AddPrefab(SFXGooseI);
			PrefabManager.Instance.AddPrefab(SFXGooseD);
			//Debug.Log("Wildlife: 0");
			GameObject VFXGetHit = WildlifeBundle.LoadAsset<GameObject>("VFX_Blood_Hit_WL");
			GameObject VFXDeath = WildlifeBundle.LoadAsset<GameObject>("VFX_Animal_Death_WL");
			GameObject VFXPoof = WildlifeBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_WL");
			PrefabManager.Instance.AddPrefab(VFXGetHit);
			PrefabManager.Instance.AddPrefab(VFXDeath);
			PrefabManager.Instance.AddPrefab(VFXPoof);
			//Debug.Log("Wildlife: 0");
			GameObject GreenFrogRD = WildlifeBundle.LoadAsset<GameObject>("GreenFrog_RD_WL");
			CustomPrefab customRD1 = new CustomPrefab(GreenFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD1);
			GameObject BlackFrogRD = WildlifeBundle.LoadAsset<GameObject>("BlackFrog_RD_WL");
			CustomPrefab customRD2 = new CustomPrefab(BlackFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD2);
			GameObject SpottedFrogRD = WildlifeBundle.LoadAsset<GameObject>("SpottedFrog_RD_WL");
			CustomPrefab customRD3 = new CustomPrefab(SpottedFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD3);
			GameObject GreyRabbitRD = WildlifeBundle.LoadAsset<GameObject>("GreyRabbit_RD_WL");
			CustomPrefab customRD4 = new CustomPrefab(GreyRabbitRD, true);
			PrefabManager.Instance.AddPrefab(customRD4);
			GameObject BrownRabbitRD = WildlifeBundle.LoadAsset<GameObject>("BrownRabbit_RD_WL");
			CustomPrefab customRD5 = new CustomPrefab(BrownRabbitRD, true);
			PrefabManager.Instance.AddPrefab(customRD5);
			GameObject SheepRD = WildlifeBundle.LoadAsset<GameObject>("Sheep_RD_WL");
			CustomPrefab customRD6 = new CustomPrefab(SheepRD, true);
			PrefabManager.Instance.AddPrefab(customRD6);
			GameObject GoatRD = WildlifeBundle.LoadAsset<GameObject>("Goat_RD_WL");
			CustomPrefab customRD7 = new CustomPrefab(GoatRD, true);
			PrefabManager.Instance.AddPrefab(customRD7);
			GameObject GooseRD = WildlifeBundle.LoadAsset<GameObject>("Goose_RD_WL");
			CustomPrefab customRD8 = new CustomPrefab(GooseRD, true);
			PrefabManager.Instance.AddPrefab(customRD8);
			GameObject PenguinRD = WildlifeBundle.LoadAsset<GameObject>("Penguin_RD_WL");
			CustomPrefab customRD9 = new CustomPrefab(PenguinRD, true);
			PrefabManager.Instance.AddPrefab(customRD9);
			GameObject RatRD = WildlifeBundle.LoadAsset<GameObject>("Rat_RD_WL");
			CustomPrefab customRD10 = new CustomPrefab(RatRD, true);
			PrefabManager.Instance.AddPrefab(customRD10);
			GameObject SalamanderRD = WildlifeBundle.LoadAsset<GameObject>("FireSalamander_RD_WL");
			CustomPrefab customRD11 = new CustomPrefab(SalamanderRD, true);
			PrefabManager.Instance.AddPrefab(customRD11);
			GameObject TurtleRD = WildlifeBundle.LoadAsset<GameObject>("BoxTurtle_RD_WL");
			CustomPrefab customRD12 = new CustomPrefab(TurtleRD, true);
			PrefabManager.Instance.AddPrefab(customRD12);
			GameObject BrownLizardRD = WildlifeBundle.LoadAsset<GameObject>("BrownLizard_RD_WL");
			CustomPrefab customRD13 = new CustomPrefab(BrownLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD13);
			GameObject GreenLizardRD = WildlifeBundle.LoadAsset<GameObject>("GreenLizard_RD_WL");
			CustomPrefab customRD14 = new CustomPrefab(GreenLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD14);
			GameObject SpottedLizardRD = WildlifeBundle.LoadAsset<GameObject>("SpottedLizard_RD_WL");
			CustomPrefab customRD15 = new CustomPrefab(SpottedLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD15);
		}
		private void AddNewAnimals()
		{
			GameObject animal20 = Camel;
			CustomPrefab critter20 = new CustomPrefab(animal20, true);
			PrefabManager.Instance.AddPrefab(critter20);
			GameObject animal19 = ComodoDragon;
			CustomPrefab critter19 = new CustomPrefab(animal19, true);
			PrefabManager.Instance.AddPrefab(critter19);
			GameObject animal18 = Zebra;
			CustomPrefab critter18 = new CustomPrefab(animal18, true);
			PrefabManager.Instance.AddPrefab(critter18);
			GameObject animal17 = SnappingTurtle;
			CustomPrefab critter17 = new CustomPrefab(animal17, true);
			PrefabManager.Instance.AddPrefab(critter17);
			//Debug.Log("DoDMonsters: Loading and Creating Critters");
			GameObject animal16 = SpottedLizard;
			CustomPrefab critter16 = new CustomPrefab(animal16, true);
			PrefabManager.Instance.AddPrefab(critter16);
			GameObject animal15 = GreenLizard;
			CustomPrefab critter15 = new CustomPrefab(animal15, true);
			PrefabManager.Instance.AddPrefab(critter15);
			GameObject animal14 = BrownLizard;
			CustomPrefab critter14 = new CustomPrefab(animal14, true);
			PrefabManager.Instance.AddPrefab(critter14);
			GameObject animal13 = Penguin;
			CustomPrefab critter13 = new CustomPrefab(animal13, true);
			PrefabManager.Instance.AddPrefab(critter13);
			GameObject animal12 = Salamander;
			CustomPrefab critter12 = new CustomPrefab(animal12, true);
			PrefabManager.Instance.AddPrefab(critter12);
			GameObject animal11 = Rat;
			CustomPrefab critter11 = new CustomPrefab(animal11, true);
			PrefabManager.Instance.AddPrefab(critter11);
			GameObject animal10 = Turtle;
			CustomPrefab critter10 = new CustomPrefab(animal10, true);
			PrefabManager.Instance.AddPrefab(critter10);
			GameObject animal9 = Goose;
			CustomPrefab critter9 = new CustomPrefab(animal9, true);
			PrefabManager.Instance.AddPrefab(critter9);
			GameObject animal8 = Goat;
			CustomPrefab critter8 = new CustomPrefab(animal8, true);
			PrefabManager.Instance.AddPrefab(critter8);
			GameObject animal7 = Sheep;
			CustomPrefab critter7 = new CustomPrefab(animal7, true);
			PrefabManager.Instance.AddPrefab(critter7);
			//GameObject animal6 = Removed;
			//CustomPrefab critter6 = new CustomPrefab(animal6, true);
			//PrefabManager.Instance.AddPrefab(critter6);
			GameObject animal5 = BrownRabbit;
			CustomPrefab critter5 = new CustomPrefab(animal5, true);
			PrefabManager.Instance.AddPrefab(critter5);
			GameObject animal4 = GreyRabbit;
			CustomPrefab critter4 = new CustomPrefab(animal4, true);
			PrefabManager.Instance.AddPrefab(critter4);
			GameObject animal3 = SpottedFrog;
			CustomPrefab critter3 = new CustomPrefab(animal3, true);
			PrefabManager.Instance.AddPrefab(critter3);
			GameObject animal2 = BlackFrog;
			CustomPrefab critter2 = new CustomPrefab(animal2, true);
			PrefabManager.Instance.AddPrefab(critter2);
			GameObject animal1 = GreenFrog;
			CustomPrefab critter1 = new CustomPrefab(animal1, true);
			PrefabManager.Instance.AddPrefab(critter1);
		}
		private void UnloadBundle()
		{
			WildlifeBundle?.Unload(unloadAllLoadedObjects: false);
			ExoticBundle?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
