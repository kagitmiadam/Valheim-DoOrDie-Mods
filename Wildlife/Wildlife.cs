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

namespace WildlifeMobs
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class Wildlife : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.Wildlife";

		public const string PluginName = "Wildlife";

		public const string PluginVersion = "0.0.1";

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

		public AssetBundle WildlifeBundle;
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
		}
		public void LoadBundle()
		{
			WildlifeBundle = AssetUtils.LoadAssetBundleFromResources("wildlife", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			//Debug.Log("Wildlife: 0");
			Sheep = WildlifeBundle.LoadAsset<GameObject>("Sheep_DoD");
			Goat = WildlifeBundle.LoadAsset<GameObject>("Goat_DoD");
			Goose = WildlifeBundle.LoadAsset<GameObject>("Goose_DoD");
			Turtle = WildlifeBundle.LoadAsset<GameObject>("Turtle_DoD");
			Salamander = WildlifeBundle.LoadAsset<GameObject>("Salamander_DoD");
			Penguin = WildlifeBundle.LoadAsset<GameObject>("Penguin_DoD");
			Rat = WildlifeBundle.LoadAsset<GameObject>("Rat_DoD");
			BrownLizard = WildlifeBundle.LoadAsset<GameObject>("BrownLizard_DoD");
			GreenLizard = WildlifeBundle.LoadAsset<GameObject>("GreenLizard_DoD");
			SpottedLizard = WildlifeBundle.LoadAsset<GameObject>("SpottedLizard_DoD");
			GreenFrog = WildlifeBundle.LoadAsset<GameObject>("GreenFrog_DoD");
			BlackFrog = WildlifeBundle.LoadAsset<GameObject>("BlackFrog_DoD");
			SpottedFrog = WildlifeBundle.LoadAsset<GameObject>("SpottedFrog_DoD");
			GreyRabbit = WildlifeBundle.LoadAsset<GameObject>("GreyRabbit_DoD");
			BrownRabbit = WildlifeBundle.LoadAsset<GameObject>("BrownRabbit_DoD");
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
			GameObject GreenFrogRD = WildlifeBundle.LoadAsset<GameObject>("GreenFrog_RD_DoD");
			CustomPrefab customRD1 = new CustomPrefab(GreenFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD1);
			GameObject BlackFrogRD = WildlifeBundle.LoadAsset<GameObject>("BlackFrog_RD_DoD");
			CustomPrefab customRD2 = new CustomPrefab(BlackFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD2);
			GameObject SpottedFrogRD = WildlifeBundle.LoadAsset<GameObject>("SpottedFrog_RD_DoD");
			CustomPrefab customRD3 = new CustomPrefab(SpottedFrogRD, true);
			PrefabManager.Instance.AddPrefab(customRD3);
			GameObject GreyRabbitRD = WildlifeBundle.LoadAsset<GameObject>("GreyRabbit_RD_DoD");
			CustomPrefab customRD4 = new CustomPrefab(GreyRabbitRD, true);
			PrefabManager.Instance.AddPrefab(customRD4);
			GameObject BrownRabbitRD = WildlifeBundle.LoadAsset<GameObject>("BrownRabbit_RD_DoD");
			CustomPrefab customRD5 = new CustomPrefab(BrownRabbitRD, true);
			PrefabManager.Instance.AddPrefab(customRD5);
			GameObject SheepRD = WildlifeBundle.LoadAsset<GameObject>("Sheep_RD_DoD");
			CustomPrefab customRD6 = new CustomPrefab(SheepRD, true);
			PrefabManager.Instance.AddPrefab(customRD6);
			GameObject GoatRD = WildlifeBundle.LoadAsset<GameObject>("Goat_RD_DoD");
			CustomPrefab customRD7 = new CustomPrefab(GoatRD, true);
			PrefabManager.Instance.AddPrefab(customRD7);
			GameObject GooseRD = WildlifeBundle.LoadAsset<GameObject>("Goose_RD_DoD");
			CustomPrefab customRD8 = new CustomPrefab(GooseRD, true);
			PrefabManager.Instance.AddPrefab(customRD8);
			GameObject PenguinRD = WildlifeBundle.LoadAsset<GameObject>("Penguin_RD_DoD");
			CustomPrefab customRD9 = new CustomPrefab(PenguinRD, true);
			PrefabManager.Instance.AddPrefab(customRD9);
			GameObject RatRD = WildlifeBundle.LoadAsset<GameObject>("Rat_RD_DoD");
			CustomPrefab customRD10 = new CustomPrefab(RatRD, true);
			PrefabManager.Instance.AddPrefab(customRD10);
			GameObject SalamanderRD = WildlifeBundle.LoadAsset<GameObject>("FireSalamander_RD_DoD");
			CustomPrefab customRD11 = new CustomPrefab(SalamanderRD, true);
			PrefabManager.Instance.AddPrefab(customRD11);
			GameObject TurtleRD = WildlifeBundle.LoadAsset<GameObject>("BoxTurtle_RD_DoD");
			CustomPrefab customRD12 = new CustomPrefab(TurtleRD, true);
			PrefabManager.Instance.AddPrefab(customRD12);
			GameObject BrownLizardRD = WildlifeBundle.LoadAsset<GameObject>("BrownLizard_RD_DoD");
			CustomPrefab customRD13 = new CustomPrefab(BrownLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD13);
			GameObject GreenLizardRD = WildlifeBundle.LoadAsset<GameObject>("GreenLizard_RD_DoD");
			CustomPrefab customRD14 = new CustomPrefab(GreenLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD14);
			GameObject SpottedLizardRD = WildlifeBundle.LoadAsset<GameObject>("SpottedLizard_RD_DoD");
			CustomPrefab customRD15 = new CustomPrefab(SpottedLizardRD, true);
			PrefabManager.Instance.AddPrefab(customRD15);
		}
		private void AddNewAnimals()
		{
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
		}
	}
}
