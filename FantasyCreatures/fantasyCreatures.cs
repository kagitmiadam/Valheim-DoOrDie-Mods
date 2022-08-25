﻿using System.Reflection;
using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;
using SpawnThat.Spawners;
using SpawnThat.Spawners.LocalSpawner;
using SpawnThat.Spawners.WorldSpawner;

namespace FantasyCreatures
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.HardDependency)]
	internal class fantasyCreatures : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.FantasyCreatures";

		public const string PluginName = "FantasyCreatures";

		public const string PluginVersion = "0.2.3";

		public static bool isModded = true;

		public static GameObject EntA1;
		public static GameObject EntA2;
		public static GameObject EntA3;
		public static GameObject EntA4;
		public static GameObject EntA5;
		public static GameObject DemonLordA1;
		public static GameObject DemonLordA2;
		public static GameObject DemonLordA3;
		public static GameObject DemonLordA4;
		public static GameObject DemonLordA5;
		public static GameObject ElementalA1;
		public static GameObject ElementalA2;
		public static GameObject ElementalA3;
		public static GameObject ElementalA4;
		public static GameObject ElementalA5;
		public static GameObject ElementalA6;
		public static GameObject ElementalA7;
		public static GameObject ElementalA8;
		public static GameObject IceElementalA1;
		public static GameObject IceElementalA2;
		public static GameObject IceElementalA3;
		public static GameObject IceElementalA4;
		public static GameObject IceElementalA5;
		public static GameObject IceElementalA6;
		public static GameObject IceElementalA7;
		public static GameObject IceElementalA8;
		public static GameObject FireElementalA1;
		public static GameObject FireElementalA2;
		public static GameObject FireElementalA3;
		public static GameObject FireElementalA4;
		public static GameObject FireElementalA5;
		public static GameObject FireElementalA6;
		public static GameObject FireElementalA7;
		public static GameObject FireElementalA8;
		public static GameObject KoboldA1;
		public static GameObject KoboldA2;
		public static GameObject KoboldA3;
		public static GameObject KoboldA4;
		public static GameObject KoboldA5;
		public static GameObject KoboldA6;
		public static GameObject KoboldA7;
		public static GameObject KoboldA8;
		public static GameObject OgreA1;
		public static GameObject OgreA2;
		public static GameObject OgreA3;
		public static GameObject OgreA4;
		public static GameObject OgreA5;
		public static GameObject CyclopsA1;
		public static GameObject CyclopsA2;
		public static GameObject CyclopsA3;
		public static GameObject CyclopsA4;
		public static GameObject CyclopsA5;
		public static GameObject CyclopsA6;
		public static GameObject HobgoblinA1;
		public static GameObject HobgoblinA2;
		public static GameObject HobgoblinA3;
		public static GameObject HobgoblinA4;
		public static GameObject HobgoblinA5;
		public static GameObject SpiderA1;
		public static GameObject ViperA1;
		public static GameObject MummyA1;
		public static GameObject MummyA2;
		public static GameObject MummyA3;
		public static GameObject MummyA4;
		public static GameObject MummyA5;
		public static GameObject MummyR1;
		public static GameObject MummyR2;
		public static GameObject MummyR3;
		public static GameObject MummyR4;
		public static GameObject MummyR5;
		public static GameObject GhoulA1;
		public static GameObject GhoulA2;
		public static GameObject GhoulA3;
		public static GameObject GhoulA4;
		public static GameObject GhoulA5;
		public static GameObject ManticoreA1;
		public static GameObject ManticoreA2;
		public static GameObject ManticoreA3;
		public static GameObject ManticoreA4;
		public static GameObject ManticoreA5;

		public static GameObject Trophy1;
		public static GameObject Trophy2;
		public static GameObject Trophy3;
		public static GameObject Trophy4;
		public static GameObject Trophy5;
		public static GameObject Trophy6;
		public static GameObject Trophy7;
		public static GameObject Trophy8;
		public static GameObject Trophy9;
		public static GameObject Trophy10;
		public static GameObject Trophy11;
		public static GameObject Trophy12;
		public static GameObject Trophy13;
		public static GameObject Trophy14;

		public AssetBundle FantasyBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;

		public ConfigEntry<bool> BasicLoggingEnable;
		public ConfigEntry<bool> WorldSpawnsEnable;
		public void CreateConfigurationValues()
		{
			BasicLoggingEnable = base.Config.Bind("Logging", "Enable", defaultValue: false, new ConfigDescription("Enables some basic logging.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			WorldSpawnsEnable = base.Config.Bind("World Spawns", "Enable", defaultValue: true, new ConfigDescription("Enables Spawns around the world.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake() 
		{
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FantasyCreatures");
			CreateConfigurationValues();
			Log = Logger;
			LoadBundle();
			LoadAssets();
			AddNewCreatures();
			PrefabManager.OnVanillaPrefabsAvailable += FixSFX;
			if (WorldSpawnsEnable.Value == true)
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
		}
		public void LoadBundle()
		{
			FantasyBundle = AssetUtils.LoadAssetBundleFromResources("fantasycreatures", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ragdolls"); }
			GameObject Ragdoll1 = FantasyBundle.LoadAsset<GameObject>("Balrog_RD_FC");
			CustomPrefab RD1 = new CustomPrefab(Ragdoll1, true);
			PrefabManager.Instance.AddPrefab(RD1);
			GameObject Ragdoll2 = FantasyBundle.LoadAsset<GameObject>("Cyclops_RD_FC");
			CustomPrefab RD2 = new CustomPrefab(Ragdoll2, true);
			PrefabManager.Instance.AddPrefab(RD2);
			GameObject Ragdoll3 = FantasyBundle.LoadAsset<GameObject>("DarknessSpider_RD_FC");
			CustomPrefab RD3 = new CustomPrefab(Ragdoll3, true);
			PrefabManager.Instance.AddPrefab(RD3);
			GameObject Ragdoll4 = FantasyBundle.LoadAsset<GameObject>("EarthElemental_RD_FC");
			CustomPrefab RD4 = new CustomPrefab(Ragdoll4, true);
			PrefabManager.Instance.AddPrefab(RD4);
			GameObject Ragdoll5 = FantasyBundle.LoadAsset<GameObject>("Ent_RD_FC");
			CustomPrefab RD5 = new CustomPrefab(Ragdoll5, true);
			PrefabManager.Instance.AddPrefab(RD5);
			GameObject Ragdoll6 = FantasyBundle.LoadAsset<GameObject>("FireElemental_RD_FC");
			CustomPrefab RD6 = new CustomPrefab(Ragdoll6, true);
			PrefabManager.Instance.AddPrefab(RD6);
			GameObject Ragdoll7 = FantasyBundle.LoadAsset<GameObject>("Ghoul_RD_FC");
			CustomPrefab RD7 = new CustomPrefab(Ragdoll7, true);
			PrefabManager.Instance.AddPrefab(RD7);
			GameObject Ragdoll8 = FantasyBundle.LoadAsset<GameObject>("GiantViper_RD_FC");
			CustomPrefab RD8 = new CustomPrefab(Ragdoll8, true);
			PrefabManager.Instance.AddPrefab(RD8);
			GameObject Ragdoll9 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_RD_FC");
			CustomPrefab RD9 = new CustomPrefab(Ragdoll9, true);
			PrefabManager.Instance.AddPrefab(RD9);
			GameObject Ragdoll10 = FantasyBundle.LoadAsset<GameObject>("IceElemental_RD_FC");
			CustomPrefab RD10 = new CustomPrefab(Ragdoll10, true);
			PrefabManager.Instance.AddPrefab(RD10);
			GameObject Ragdoll11 = FantasyBundle.LoadAsset<GameObject>("Kobold_RD_FC");
			CustomPrefab RD11 = new CustomPrefab(Ragdoll11, true);
			PrefabManager.Instance.AddPrefab(RD11);
			GameObject Ragdoll12 = FantasyBundle.LoadAsset<GameObject>("Manitcore_RD_FC");
			CustomPrefab RD12 = new CustomPrefab(Ragdoll12, true);
			PrefabManager.Instance.AddPrefab(RD12);
			GameObject Ragdoll13 = FantasyBundle.LoadAsset<GameObject>("Mummy_RD_FC");
			CustomPrefab RD13 = new CustomPrefab(Ragdoll13, true);
			PrefabManager.Instance.AddPrefab(RD13);
			GameObject Ragdoll14 = FantasyBundle.LoadAsset<GameObject>("Ogre_RD_FC");
			CustomPrefab RD14 = new CustomPrefab(Ragdoll14, true);
			PrefabManager.Instance.AddPrefab(RD14);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Spider Attacks"); }
			SpiderA1 = FantasyBundle.LoadAsset<GameObject>("Spider_Attack1_DoD");
			CustomPrefab Spiderattack1 = new CustomPrefab(SpiderA1, false);
			PrefabManager.Instance.AddPrefab(Spiderattack1);
			ViperA1 = FantasyBundle.LoadAsset<GameObject>("Viper_Attack1_DoD");
			CustomPrefab Viperattack1 = new CustomPrefab(ViperA1, false);
			PrefabManager.Instance.AddPrefab(Viperattack1);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Manticore Attacks"); }
			ManticoreA1 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack1_DoD");
			CustomPrefab Manticoreattack1 = new CustomPrefab(ManticoreA1, false);
			PrefabManager.Instance.AddPrefab(Manticoreattack1);
			ManticoreA2 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack2_DoD");
			CustomPrefab Manticoreattack2 = new CustomPrefab(ManticoreA2, false);
			PrefabManager.Instance.AddPrefab(Manticoreattack2);
			ManticoreA3 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack3_DoD");
			CustomPrefab Manticoreattack3 = new CustomPrefab(ManticoreA3, false);
			PrefabManager.Instance.AddPrefab(Manticoreattack3);
			ManticoreA4 = FantasyBundle.LoadAsset<GameObject>("Manticore_AttackCombo1_DoD");
			CustomPrefab Manticoreattack4 = new CustomPrefab(ManticoreA4, false);
			PrefabManager.Instance.AddPrefab(Manticoreattack4);
			ManticoreA5 = FantasyBundle.LoadAsset<GameObject>("Manticore_AttackSting_DoD");
			CustomPrefab Manticoreattack5 = new CustomPrefab(ManticoreA5, false);
			PrefabManager.Instance.AddPrefab(Manticoreattack5);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ghoul Attacks"); }
			GhoulA1 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack1_DoD");
			CustomPrefab Ghoulattack1 = new CustomPrefab(GhoulA1, false);
			PrefabManager.Instance.AddPrefab(Ghoulattack1);
			GhoulA2 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack2_DoD");
			CustomPrefab Ghoulattack2 = new CustomPrefab(GhoulA2, false);
			PrefabManager.Instance.AddPrefab(Ghoulattack2);
			GhoulA3 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack3_DoD");
			CustomPrefab Ghoulattack3 = new CustomPrefab(GhoulA3, false);
			PrefabManager.Instance.AddPrefab(Ghoulattack3);
			GhoulA4 = FantasyBundle.LoadAsset<GameObject>("Ghoul_AttackCombo2_DoD");
			CustomPrefab Ghoulattack4 = new CustomPrefab(GhoulA4, false);
			PrefabManager.Instance.AddPrefab(Ghoulattack4);
			GhoulA5 = FantasyBundle.LoadAsset<GameObject>("Ghoul_AttackCombo3_DoD");
			CustomPrefab Ghoulattack5 = new CustomPrefab(GhoulA5, false);
			PrefabManager.Instance.AddPrefab(Ghoulattack5);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Mummy Attacks"); }
			MummyA1 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack1_DoD");
			CustomPrefab mummyattack1 = new CustomPrefab(MummyA1, false);
			PrefabManager.Instance.AddPrefab(mummyattack1);
			MummyA2 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack2_DoD");
			CustomPrefab mummyattack2 = new CustomPrefab(MummyA2, false);
			PrefabManager.Instance.AddPrefab(mummyattack2);
			MummyA3 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack3_DoD");
			CustomPrefab mummyattack3 = new CustomPrefab(MummyA3, false);
			PrefabManager.Instance.AddPrefab(mummyattack3);
			MummyA4 = FantasyBundle.LoadAsset<GameObject>("Mummy_AttackCombo2_DoD");
			CustomPrefab mummyattack4 = new CustomPrefab(MummyA4, false);
			PrefabManager.Instance.AddPrefab(mummyattack4);
			MummyA5 = FantasyBundle.LoadAsset<GameObject>("Mummy_AttackCombo3_DoD");
			CustomPrefab mummyattack5 = new CustomPrefab(MummyA5, false);
			PrefabManager.Instance.AddPrefab(mummyattack5);
			MummyR1 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip1_DoD");
			CustomPrefab mummyattack6 = new CustomPrefab(MummyR1, false);
			PrefabManager.Instance.AddPrefab(mummyattack6);
			MummyR2 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip2_DoD");
			CustomPrefab mummyattack7 = new CustomPrefab(MummyR2, false);
			PrefabManager.Instance.AddPrefab(mummyattack7);
			MummyR3 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip3_DoD");
			CustomPrefab mummyattack8 = new CustomPrefab(MummyR3, false);
			PrefabManager.Instance.AddPrefab(mummyattack8);
			MummyR4 = FantasyBundle.LoadAsset<GameObject>("Mummy_WhipCombo2_DoD");
			CustomPrefab mummyattack9 = new CustomPrefab(MummyR4, false);
			PrefabManager.Instance.AddPrefab(mummyattack9);
			MummyR5 = FantasyBundle.LoadAsset<GameObject>("Mummy_WhipCombo3_DoD");
			CustomPrefab mummyattack10 = new CustomPrefab(MummyR5, false);
			PrefabManager.Instance.AddPrefab(mummyattack10);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ent Arracks"); }
			EntA1 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack1_DoD");
			CustomPrefab attack3 = new CustomPrefab(EntA1, false);
			PrefabManager.Instance.AddPrefab(attack3);
			EntA2 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack2_DoD");
			CustomPrefab attack4 = new CustomPrefab(EntA2, false);
			PrefabManager.Instance.AddPrefab(attack4);
			EntA3 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack3_DoD");
			CustomPrefab attack5 = new CustomPrefab(EntA3, false);
			PrefabManager.Instance.AddPrefab(attack5);
			EntA4 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack2Combo_DoD");
			CustomPrefab attack6 = new CustomPrefab(EntA4, false);
			PrefabManager.Instance.AddPrefab(attack6);
			EntA5 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack3Combo_DoD");
			CustomPrefab attack7 = new CustomPrefab(EntA5, false);
			PrefabManager.Instance.AddPrefab(attack7);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Demon Attacks"); }
			DemonLordA1 = FantasyBundle.LoadAsset<GameObject>("DemonLord_Attack1_DoD");
			CustomPrefab attack8 = new CustomPrefab(DemonLordA1, false);
			PrefabManager.Instance.AddPrefab(attack8);
			DemonLordA2 = FantasyBundle.LoadAsset<GameObject>("DemonLord_Attack2_DoD");
			CustomPrefab attack9 = new CustomPrefab(DemonLordA2, false);
			PrefabManager.Instance.AddPrefab(attack9);
			DemonLordA3 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackWhip_DoD");
			CustomPrefab attack10 = new CustomPrefab(DemonLordA3, false);
			PrefabManager.Instance.AddPrefab(attack10);
			DemonLordA4 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackCombo2_DoD");
			CustomPrefab attack11 = new CustomPrefab(DemonLordA4, false);
			PrefabManager.Instance.AddPrefab(attack11);
			DemonLordA5 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackCombo3_DoD");
			CustomPrefab attack12 = new CustomPrefab(DemonLordA5, false);
			PrefabManager.Instance.AddPrefab(attack12);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Elemental Attacks"); }
			ElementalA1 = FantasyBundle.LoadAsset<GameObject>("Element_Attack1_DoD");
			CustomPrefab attack13 = new CustomPrefab(ElementalA1, false);
			PrefabManager.Instance.AddPrefab(attack13);
			ElementalA2 = FantasyBundle.LoadAsset<GameObject>("Element_Attack2_DoD");
			CustomPrefab attack14 = new CustomPrefab(ElementalA2, false);
			PrefabManager.Instance.AddPrefab(attack14);
			ElementalA3 = FantasyBundle.LoadAsset<GameObject>("Element_AttackGrab_DoD");
			CustomPrefab attack15 = new CustomPrefab(ElementalA3, false);
			PrefabManager.Instance.AddPrefab(attack15);
			ElementalA4 = FantasyBundle.LoadAsset<GameObject>("Element_AttackSmash_DoD");
			CustomPrefab attack16 = new CustomPrefab(ElementalA4, false);
			PrefabManager.Instance.AddPrefab(attack16);
			ElementalA5 = FantasyBundle.LoadAsset<GameObject>("Element_AttackSwipe_DoD");
			CustomPrefab attack17 = new CustomPrefab(ElementalA5, false);
			PrefabManager.Instance.AddPrefab(attack17);
			ElementalA6 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo1_DoD");
			CustomPrefab attack18 = new CustomPrefab(ElementalA6, false);
			PrefabManager.Instance.AddPrefab(attack18);
			ElementalA7 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo2_DoD");
			CustomPrefab attack19 = new CustomPrefab(ElementalA7, false);
			PrefabManager.Instance.AddPrefab(attack19);
			ElementalA8 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo3_DoD");
			CustomPrefab attack20 = new CustomPrefab(ElementalA8, false);
			PrefabManager.Instance.AddPrefab(attack20);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ice Elemental Attacks"); }
			IceElementalA1 = FantasyBundle.LoadAsset<GameObject>("IceElement_Attack1_DoD");
			CustomPrefab attack21 = new CustomPrefab(IceElementalA1, false);
			PrefabManager.Instance.AddPrefab(attack21);
			IceElementalA2 = FantasyBundle.LoadAsset<GameObject>("IceElement_Attack2_DoD");
			CustomPrefab attack22 = new CustomPrefab(IceElementalA2, false);
			PrefabManager.Instance.AddPrefab(attack22);
			IceElementalA3 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackGrab_DoD");
			CustomPrefab attack23 = new CustomPrefab(IceElementalA3, false);
			PrefabManager.Instance.AddPrefab(attack23);
			IceElementalA4 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackSmash_DoD");
			CustomPrefab attack24 = new CustomPrefab(IceElementalA4, false);
			PrefabManager.Instance.AddPrefab(attack24);
			IceElementalA5 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackSwipe_DoD");
			CustomPrefab attack25 = new CustomPrefab(IceElementalA5, false);
			PrefabManager.Instance.AddPrefab(attack25);
			IceElementalA6 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo1_DoD");
			CustomPrefab attack26 = new CustomPrefab(IceElementalA6, false);
			PrefabManager.Instance.AddPrefab(attack26);
			IceElementalA7 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo2_DoD");
			CustomPrefab attack27 = new CustomPrefab(IceElementalA7, false);
			PrefabManager.Instance.AddPrefab(attack27);
			IceElementalA8 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo3_DoD");
			CustomPrefab attack28 = new CustomPrefab(IceElementalA8, false);
			PrefabManager.Instance.AddPrefab(attack28);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Fire Elemental Attacks"); }
			FireElementalA1 = FantasyBundle.LoadAsset<GameObject>("FireElement_Attack1_DoD");
			CustomPrefab attack29 = new CustomPrefab(FireElementalA1, false);
			PrefabManager.Instance.AddPrefab(attack29);
			FireElementalA2 = FantasyBundle.LoadAsset<GameObject>("FireElement_Attack2_DoD");
			CustomPrefab attack30 = new CustomPrefab(FireElementalA2, false);
			PrefabManager.Instance.AddPrefab(attack30);
			FireElementalA3 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackGrab_DoD");
			CustomPrefab attack31 = new CustomPrefab(FireElementalA3, false);
			PrefabManager.Instance.AddPrefab(attack31);
			FireElementalA4 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackSmash_DoD");
			CustomPrefab attack32 = new CustomPrefab(FireElementalA4, false);
			PrefabManager.Instance.AddPrefab(attack32);
			FireElementalA5 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackSwipe_DoD");
			CustomPrefab attack33 = new CustomPrefab(FireElementalA5, false);
			PrefabManager.Instance.AddPrefab(attack33);
			FireElementalA6 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo1_DoD");
			CustomPrefab attack34 = new CustomPrefab(FireElementalA6, false);
			PrefabManager.Instance.AddPrefab(attack34);
			FireElementalA7 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo2_DoD");
			CustomPrefab attack35 = new CustomPrefab(FireElementalA7, false);
			PrefabManager.Instance.AddPrefab(attack35);
			FireElementalA8 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo3_DoD");
			CustomPrefab attack36 = new CustomPrefab(FireElementalA8, false);
			PrefabManager.Instance.AddPrefab(attack36);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Kobold Attacks"); }
			KoboldA1 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack1_DoD");
			CustomPrefab attack37 = new CustomPrefab(KoboldA1, false);
			PrefabManager.Instance.AddPrefab(attack37);
			KoboldA2 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack2_DoD");
			CustomPrefab attack38 = new CustomPrefab(KoboldA2, false);
			PrefabManager.Instance.AddPrefab(attack38);
			KoboldA3 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack3_DoD");
			CustomPrefab attack39 = new CustomPrefab(KoboldA3, false);
			PrefabManager.Instance.AddPrefab(attack39);
			KoboldA4 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack4_DoD");
			CustomPrefab attack40 = new CustomPrefab(KoboldA4, false);
			PrefabManager.Instance.AddPrefab(attack40);
			KoboldA5 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo1_DoD");
			CustomPrefab attack41 = new CustomPrefab(KoboldA5, false);
			PrefabManager.Instance.AddPrefab(attack41);
			KoboldA6 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo2_DoD");
			CustomPrefab attack42 = new CustomPrefab(KoboldA6, false);
			PrefabManager.Instance.AddPrefab(attack42);
			KoboldA7 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo3_DoD");
			CustomPrefab attack43 = new CustomPrefab(KoboldA7, false);
			PrefabManager.Instance.AddPrefab(attack43);
			KoboldA8 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo4_DoD");
			CustomPrefab attack44 = new CustomPrefab(KoboldA8, false);
			PrefabManager.Instance.AddPrefab(attack44);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ogre Attacks"); }
			OgreA1 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack1_DoD");
			CustomPrefab attack56 = new CustomPrefab(OgreA1, false);
			PrefabManager.Instance.AddPrefab(attack56);
			OgreA2 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack2_DoD");
			CustomPrefab attack57 = new CustomPrefab(OgreA2, false);
			PrefabManager.Instance.AddPrefab(attack57);
			OgreA3 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack3_DoD");
			CustomPrefab attack58 = new CustomPrefab(OgreA3, false);
			PrefabManager.Instance.AddPrefab(attack58);
			OgreA4 = FantasyBundle.LoadAsset<GameObject>("Ogre_AttackCombo1_DoD");
			CustomPrefab attack59 = new CustomPrefab(OgreA4, false);
			PrefabManager.Instance.AddPrefab(attack59);
			OgreA5 = FantasyBundle.LoadAsset<GameObject>("Ogre_AttackCombo2_DoD");
			CustomPrefab attack60 = new CustomPrefab(OgreA5, false);
			PrefabManager.Instance.AddPrefab(attack60);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Hobgoblin Attacks"); }
			HobgoblinA1 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack1_DoD");
			CustomPrefab attack61 = new CustomPrefab(HobgoblinA1, false);
			PrefabManager.Instance.AddPrefab(attack61);
			HobgoblinA2 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack2_DoD");
			CustomPrefab attack62 = new CustomPrefab(HobgoblinA2, false);
			PrefabManager.Instance.AddPrefab(attack62);
			HobgoblinA3 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack3_DoD");
			CustomPrefab attack63 = new CustomPrefab(HobgoblinA3, false);
			PrefabManager.Instance.AddPrefab(attack63);
			HobgoblinA4 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_AttackCombo3_DoD");
			CustomPrefab attack64 = new CustomPrefab(HobgoblinA4, false);
			PrefabManager.Instance.AddPrefab(attack64);
			HobgoblinA5 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_AttackCombo2_DoD");
			CustomPrefab attack65 = new CustomPrefab(HobgoblinA5, false);
			PrefabManager.Instance.AddPrefab(attack65);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Cyclops Attacks"); }
			CyclopsA1 = FantasyBundle.LoadAsset<GameObject>("Cyclops_Attack1_DoD");
			CustomPrefab attack66 = new CustomPrefab(CyclopsA1, false);
			PrefabManager.Instance.AddPrefab(attack66);
			CyclopsA2 = FantasyBundle.LoadAsset<GameObject>("Cyclops_Attack2_DoD");
			CustomPrefab attack67 = new CustomPrefab(CyclopsA2, false);
			PrefabManager.Instance.AddPrefab(attack67);
			CyclopsA3 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackSmash_DoD");
			CustomPrefab attack68 = new CustomPrefab(CyclopsA3, false);
			PrefabManager.Instance.AddPrefab(attack68);
			CyclopsA4 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCombo3_DoD");
			CustomPrefab attack69 = new CustomPrefab(CyclopsA4, false);
			PrefabManager.Instance.AddPrefab(attack69);
			CyclopsA5 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCombo2_DoD");
			CustomPrefab attack70 = new CustomPrefab(CyclopsA5, false);
			PrefabManager.Instance.AddPrefab(attack70);
			CyclopsA6 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCrush_DoD");
			CustomPrefab attack71 = new CustomPrefab(CyclopsA6, false);
			PrefabManager.Instance.AddPrefab(attack71);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: SFX"); }
			GameObject SFXEntGetHit = FantasyBundle.LoadAsset<GameObject>("SFX_EntGetHit_DoD");
			CustomPrefab SFX1 = new CustomPrefab(SFXEntGetHit, false);
			PrefabManager.Instance.AddPrefab(SFX1);
			GameObject SFXEntAlert = FantasyBundle.LoadAsset<GameObject>("SFX_EntAlert_DoD");
			CustomPrefab SFX2 = new CustomPrefab(SFXEntAlert, false);
			PrefabManager.Instance.AddPrefab(SFX2);
			GameObject SFXEntIdle = FantasyBundle.LoadAsset<GameObject>("SFX_EntIdle_DoD");
			CustomPrefab SFX3 = new CustomPrefab(SFXEntIdle, false);
			PrefabManager.Instance.AddPrefab(SFX3);
			GameObject SFXEntDeath = FantasyBundle.LoadAsset<GameObject>("SFX_EntDeath_DoD");
			CustomPrefab SFX4 = new CustomPrefab(SFXEntDeath, false);
			PrefabManager.Instance.AddPrefab(SFX4);
			GameObject SFXDLGetHit = FantasyBundle.LoadAsset<GameObject>("SFX_DLGetHit_DoD");
			CustomPrefab SFX5 = new CustomPrefab(SFXDLGetHit, false);
			PrefabManager.Instance.AddPrefab(SFX5);
			GameObject SFXDLAlert = FantasyBundle.LoadAsset<GameObject>("SFX_DLAlert_DoD");
			CustomPrefab SFX6 = new CustomPrefab(SFXDLAlert, false);
			PrefabManager.Instance.AddPrefab(SFX6);
			GameObject SFXDLIdle = FantasyBundle.LoadAsset<GameObject>("SFX_DLIdle_DoD");
			CustomPrefab SFX7 = new CustomPrefab(SFXDLIdle, false);
			PrefabManager.Instance.AddPrefab(SFX7);
			GameObject SFXDLDeath = FantasyBundle.LoadAsset<GameObject>("SFX_DLDeath_DoD");
			CustomPrefab SFX8 = new CustomPrefab(SFXDLDeath, false);
			PrefabManager.Instance.AddPrefab(SFX8);
			GameObject SFXDLFoot = FantasyBundle.LoadAsset<GameObject>("SFX_DLFootstep_DoD");
			CustomPrefab SFX9 = new CustomPrefab(SFXDLFoot, false);
			PrefabManager.Instance.AddPrefab(SFX9);

			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Trophies"); }
			Trophy1 = FantasyBundle.LoadAsset<GameObject>("Trophy_Cyclops_FC");
			CustomItem customItem1 = new CustomItem(Trophy1, false);
			ItemManager.Instance.AddItem(customItem1);
			Trophy2 = FantasyBundle.LoadAsset<GameObject>("Trophy_DarknessSpider_FC");
			CustomItem customItem2 = new CustomItem(Trophy2, false);
			ItemManager.Instance.AddItem(customItem2);
			Trophy3 = FantasyBundle.LoadAsset<GameObject>("Trophy_DemonLord_FC");
			CustomItem customItem3 = new CustomItem(Trophy3, false);
			ItemManager.Instance.AddItem(customItem3);
			Trophy4 = FantasyBundle.LoadAsset<GameObject>("Trophy_EarthElemental_FC");
			CustomItem customItem4 = new CustomItem(Trophy4, false);
			ItemManager.Instance.AddItem(customItem4);
			Trophy5 = FantasyBundle.LoadAsset<GameObject>("Trophy_Ent_FC");
			CustomItem customItem5 = new CustomItem(Trophy5, false);
			ItemManager.Instance.AddItem(customItem5);
			Trophy6 = FantasyBundle.LoadAsset<GameObject>("Trophy_FireElemental_FC");
			CustomItem customItem6 = new CustomItem(Trophy6, false);
			ItemManager.Instance.AddItem(customItem6);
			Trophy7 = FantasyBundle.LoadAsset<GameObject>("Trophy_Ghoul_FC");
			CustomItem customItem7 = new CustomItem(Trophy7, false);
			ItemManager.Instance.AddItem(customItem7);
			Trophy8 = FantasyBundle.LoadAsset<GameObject>("Trophy_GiantViper_FC");
			CustomItem customItem8 = new CustomItem(Trophy8, false);
			ItemManager.Instance.AddItem(customItem8);
			Trophy9 = FantasyBundle.LoadAsset<GameObject>("Trophy_Hobgoblin_FC");
			CustomItem customItem9 = new CustomItem(Trophy9, false);
			ItemManager.Instance.AddItem(customItem9);
			Trophy10 = FantasyBundle.LoadAsset<GameObject>("Trophy_IceElemental_FC");
			CustomItem customItem10 = new CustomItem(Trophy10, false);
			ItemManager.Instance.AddItem(customItem10);
			Trophy11 = FantasyBundle.LoadAsset<GameObject>("Trophy_Kobold_FC");
			CustomItem customItem11 = new CustomItem(Trophy11, false);
			ItemManager.Instance.AddItem(customItem11);
			Trophy12 = FantasyBundle.LoadAsset<GameObject>("Trophy_Manticore_FC");
			CustomItem customItem12 = new CustomItem(Trophy12, false);
			ItemManager.Instance.AddItem(customItem12);
			Trophy13 = FantasyBundle.LoadAsset<GameObject>("Trophy_Mummy_FC");
			CustomItem customItem13 = new CustomItem(Trophy13, false);
			ItemManager.Instance.AddItem(customItem13);
			Trophy14 = FantasyBundle.LoadAsset<GameObject>("Trophy_Ogre_FC");
			CustomItem customItem14 = new CustomItem(Trophy14, false);
			ItemManager.Instance.AddItem(customItem14);
			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: VFX"); }
			GameObject VFX1 = FantasyBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_M_FC");
			CustomPrefab vfx1 = new CustomPrefab(VFX1, false);
			PrefabManager.Instance.AddPrefab(vfx1);
			GameObject VFX2 = FantasyBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_L_FC");
			CustomPrefab vfx2 = new CustomPrefab(VFX2, false);
			PrefabManager.Instance.AddPrefab(vfx2);
			GameObject VFX3 = FantasyBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_S_FC");
			CustomPrefab vfx3 = new CustomPrefab(VFX3, false);
			PrefabManager.Instance.AddPrefab(vfx3);
			GameObject VFX4 = FantasyBundle.LoadAsset<GameObject>("FX_Backstab_FC");
			CustomPrefab vfx4 = new CustomPrefab(VFX4, false);
			PrefabManager.Instance.AddPrefab(vfx4);
			GameObject VFX5 = FantasyBundle.LoadAsset<GameObject>("FX_Crit_FC");
			CustomPrefab vfx5 = new CustomPrefab(VFX5, false);
			PrefabManager.Instance.AddPrefab(vfx5);
			GameObject VFX6 = FantasyBundle.LoadAsset<GameObject>("VFX_HitSparks_FC");
			CustomPrefab vfx6 = new CustomPrefab(VFX6, false);
			PrefabManager.Instance.AddPrefab(vfx6);
			GameObject VFX7 = FantasyBundle.LoadAsset<GameObject>("VFX_Blood_Hit_FC");
			CustomPrefab vfx7 = new CustomPrefab(VFX7, false);
			PrefabManager.Instance.AddPrefab(vfx7);
			GameObject VFX8 = FantasyBundle.LoadAsset<GameObject>("VFX_Hit_FC");
			CustomPrefab vfx8 = new CustomPrefab(VFX8, false);
			PrefabManager.Instance.AddPrefab(vfx8);
			GameObject VFX9 = FantasyBundle.LoadAsset<GameObject>("VFX_Poisonspit_Hit_FC");
			CustomPrefab vfx9 = new CustomPrefab(VFX9, false);
			PrefabManager.Instance.AddPrefab(vfx9);
			// Projectile
			if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Projectiles"); }
			GameObject Projectile1 = FantasyBundle.LoadAsset<GameObject>("Fireball_Projectile_FC");
			CustomPrefab proj1 = new CustomPrefab(Projectile1, false);
			PrefabManager.Instance.AddPrefab(proj1);
			GameObject Projectile2 = FantasyBundle.LoadAsset<GameObject>("Poisonspit_Projectile_FC");
			CustomPrefab proj2 = new CustomPrefab(Projectile2, false);
			PrefabManager.Instance.AddPrefab(proj2);
		}
		private void AddNewCreatures()
		{
			try
			{
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: DemonLord"); }
				var DemonLordFab = FantasyBundle.LoadAsset<GameObject>("DemonLord_DoD");
				var DemonLordMob = new CustomCreature(DemonLordFab, false,
					new CreatureConfig
					{
						//Name = "DemonLord",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_DemonLord_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "FlametalOre",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 45,
								MaxAmount = 75,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(DemonLordMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: FireElemental"); }
				var FireElementalFab = FantasyBundle.LoadAsset<GameObject>("FireElemental_DoD");
				var FireElementalMob = new CustomCreature(FireElementalFab, false,
					new CreatureConfig
					{
						//Name = "Fire Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_FireElemental_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "FlametalOre",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 45,
								MaxAmount = 65,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(FireElementalMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: IceElemental"); }
				var IceElementalFab = FantasyBundle.LoadAsset<GameObject>("IceElemental_DoD");
				var IceElementalMob = new CustomCreature(IceElementalFab, false,
					new CreatureConfig
					{
						//Name = "Ice Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_IceElemental_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "FreezeGland",
								MinAmount = 3,
								MaxAmount = 10
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 45,
								MaxAmount = 55,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(IceElementalMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: EarthElemental"); }
				var EarthElementalFab = FantasyBundle.LoadAsset<GameObject>("EarthElemental_DoD");
				var EarthElementalMob = new CustomCreature(EarthElementalFab, false,
					new CreatureConfig
					{
						//Name = "Earth Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_EarthElemental_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ruby",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 40,
								MaxAmount = 50,
								Chance = 100
							}
						},
					});
				CreatureManager.Instance.AddCreature(EarthElementalMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Manticore"); }
				var ManticoreFab = FantasyBundle.LoadAsset<GameObject>("Manticore_FC");
				var ManticoreMob = new CustomCreature(ManticoreFab, false,
					new CreatureConfig
					{
						//Name = "Manticore",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Manticore_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "BlackMetalScrap",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Flax",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 25,
								MaxAmount = 35,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(ManticoreMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Cyclops"); }
				var CyclopsFab = FantasyBundle.LoadAsset<GameObject>("Cyclops_DoD");
				var CyclopsMob = new CustomCreature(CyclopsFab, false,
					new CreatureConfig
					{
						//Name = "Cyclops",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Cyclops_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "BlackMetalScrap",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Flax",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 25,
								MaxAmount = 35,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(CyclopsMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ogre"); }
				var OgreFab = FantasyBundle.LoadAsset<GameObject>("Ogre_DoD");
				var OgreMob = new CustomCreature(OgreFab, false,
					new CreatureConfig
					{
						//Name = "Ogre",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Ogre_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "SilverOre",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Thistle",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 20,
								MaxAmount = 30,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(OgreMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Mummy"); }
				var MummyFab = FantasyBundle.LoadAsset<GameObject>("Mummy_FC");
				var MummyMob = new CustomCreature(MummyFab, false,
					new CreatureConfig
					{
						//Name = "Mummy",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Mummy_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Chain",
								MinAmount = 2,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 15,
								MaxAmount = 25,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(MummyMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ghoul"); }
				var GhoulFab = FantasyBundle.LoadAsset<GameObject>("Ghoul_FC");
				var GhoulMob = new CustomCreature(GhoulFab, false,
					new CreatureConfig
					{
						//Name = "Ghoul",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Ghoul_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "IronOre",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Chain",
								MinAmount = 2,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: TreeEnt"); }
				var TreeEntFab = FantasyBundle.LoadAsset<GameObject>("TreeEnt_DoD");
				var TreeEntMob = new CustomCreature(TreeEntFab, false,
					new CreatureConfig
					{
						//Name = "Tree Ent",						
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Ent_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "FineWood",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "ElderBark",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 10,
								MaxAmount = 20,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(TreeEntMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Ogre"); }
				var HobgoblinFab = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_DoD");
				var HobgoblinMob = new CustomCreature(HobgoblinFab, false,
					new CreatureConfig
					{
						//Name = "Hobgoblin",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Hobgoblin_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "CopperOre",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "TinOre",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 5,
								MaxAmount = 15,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(HobgoblinMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Viper"); }
				var ViperFab = FantasyBundle.LoadAsset<GameObject>("GiantViper_FC");
				var ViperMob = new CustomCreature(ViperFab, false,
					new CreatureConfig
					{
						//Name = "Viper",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_GiantViper_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "SerpentMeat",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 5,
								MaxAmount = 15,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(ViperMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Kobold"); }
				var KoboldFab = FantasyBundle.LoadAsset<GameObject>("Kobold_DoD");
				var KoboldMob = new CustomCreature(KoboldFab, false,
					new CreatureConfig
					{
						//Name = "Kobold",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_Kobold_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "FishRaw",
								MinAmount = 3,
								MaxAmount = 5,
								Chance = 50
							},
							new DropConfig
							{
								Item = "DeerHide",
								MinAmount = 1,
								MaxAmount = 2,
								Chance = 50
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(KoboldMob);
				if (BasicLoggingEnable.Value == true) { Debug.Log("Fantasy Creatures: Spider"); }
				var SpiderFab = FantasyBundle.LoadAsset<GameObject>("DarknessSpider_FC");
				var SpiderMob = new CustomCreature(SpiderFab, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Trophy_DarknessSpider_FC",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 10
							},
							new DropConfig
							{
								Item = "Ooze",
								MinAmount = 1,
								MaxAmount = 1,
								Chance = 5
							},
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10,
								Chance = 100
							}
						}
					});
				CreatureManager.Instance.AddCreature(SpiderMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures for Fantasy Creatures: {ex}");
			}
			finally
			{
				FantasyBundle.Unload(false);
			}
		}
		private void FixSFX()
		{
			try
			{
				var sfxfab1 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_EntGetHit_DoD");
				var sfxfab2 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_EntAlert_DoD");
				var sfxfab3 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_EntIdle_DoD");
				var sfxfab4 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_EntDeath_DoD");
				var sfxfab5 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_DLGetHit_DoD");
				var sfxfab6 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_DLAlert_DoD");
				var sfxfab7 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_DLIdle_DoD");
				var sfxfab8 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_DLDeath_DoD");
				var sfxfab9 = PrefabManager.Cache.GetPrefab<GameObject>("SFX_DLFootstep_DoD");
				if (sfxfab1 != null)
				{
					sfxfab1.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab2 != null)
				{
					sfxfab2.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab3 != null)
				{
					sfxfab3.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab4 != null)
				{
					sfxfab4.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab5 != null)
				{
					sfxfab5.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab6 != null)
				{
					sfxfab6.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab7 != null)
				{
					sfxfab7.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab8 != null)
				{
					sfxfab8.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
				if (sfxfab9 != null)
				{
					sfxfab9.GetComponent<AudioSource>().outputAudioMixerGroup = AudioMan.instance.m_ambientMixer;
				}
			}
			catch
			{
				Debug.LogWarning("Giants: SFX Fix Failed");
			}
		}
		public static void ConfigureUnderworldSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Fantasy Creatures: Configure Underworld Spawns");
			try
			{
				ConfigureUWSpawnerByNamed(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Fantasy Creatures: Configure Spawns");
			try
			{
				ConfigureWorldSpawner(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Something went horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureUWSpawnerByNamed(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Fantasy Creatures: Create Underworld Spawns");
			try
			{
				LocalSpawnSettings underworldT1Normal = new()
				{
					PrefabName = "DarknessSpider_FC",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT1Elite = new()
				{
					PrefabName = "Kobold_DoD",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT1MiniBoss = new()
				{
					PrefabName = "Kobold_DoD",
					SpawnInterval = TimeSpan.FromSeconds(900),
				};

				LocalSpawnSettings underworldT1SubBoss = new()
				{
					PrefabName = "Kobold_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
				};

				LocalSpawnSettings underworldT1Boss = new()
				{
					PrefabName = "Kobold_DoD",
					SpawnInterval = TimeSpan.FromSeconds(3600),
				};

				config.ConfigureLocalSpawnerByName("Spawner_UW_T1")
					.WithSettings(underworldT1Normal);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T1_Elite")
					.WithSettings(underworldT1Elite);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T1_MiniBoss")
					.WithSettings(underworldT1MiniBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T1_SubBoss")
					.WithSettings(underworldT1SubBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T1_Boss")
					.WithSettings(underworldT1Boss);

				LocalSpawnSettings underworldT2Normal = new()
				{
					PrefabName = "Hobgoblin_DoD",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT2Elite = new()
				{
					PrefabName = "Hobgoblin_DoD",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT2MiniBoss = new()
				{
					PrefabName = "TreeEnt_DoD",
					SpawnInterval = TimeSpan.FromSeconds(900),
				};

				LocalSpawnSettings underworldT2SubBoss = new()
				{
					PrefabName = "TreeEnt_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
				};

				LocalSpawnSettings underworldT2Boss = new()
				{
					PrefabName = "TreeEnt_DoD",
					SpawnInterval = TimeSpan.FromSeconds(3600),
				};

				config.ConfigureLocalSpawnerByName("Spawner_UW_T2")
					.WithSettings(underworldT2Normal);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T2_Elite")
					.WithSettings(underworldT2Elite);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T2_MiniBoss")
					.WithSettings(underworldT2MiniBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T2_SubBoss")
					.WithSettings(underworldT2SubBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T2_Boss")
					.WithSettings(underworldT2Boss);

				LocalSpawnSettings underworldT3Normal = new()
				{
					PrefabName = "Draugr",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT3Elite = new()
				{
					PrefabName = "Ghoul_FC",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				LocalSpawnSettings underworldT3MiniBoss = new()
				{
					PrefabName = "Mummy_FC",
					SpawnInterval = TimeSpan.FromSeconds(900),
				};

				LocalSpawnSettings underworldT3SubBoss = new()
				{
					PrefabName = "Mummy_FC",
					SpawnInterval = TimeSpan.FromSeconds(1800),
				};

				LocalSpawnSettings underworldT3Boss = new()
				{
					PrefabName = "Mummy_FC",
					SpawnInterval = TimeSpan.FromSeconds(3600),
				};

				config.ConfigureLocalSpawnerByName("Spawner_UW_T3")
					.WithSettings(underworldT3Normal);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T3_Elite")
					.WithSettings(underworldT3Elite);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T3_MiniBoss")
					.WithSettings(underworldT3MiniBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T3_SubBoss")
					.WithSettings(underworldT3SubBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T3_Boss")
					.WithSettings(underworldT3Boss);

				LocalSpawnSettings underworldT4Normal = new()
				{
					PrefabName = "Ogre_DoD",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};

				/*LocalSpawnSettings underworldT4Elite = new()
				{
					PrefabName = "",
					SpawnInterval = TimeSpan.FromSeconds(600),
				};*/

				LocalSpawnSettings underworldT4MiniBoss = new()
				{
					PrefabName = "Ogre_DoD",
					SpawnInterval = TimeSpan.FromSeconds(900),
				};

				LocalSpawnSettings underworldT4SubBoss = new()
				{
					PrefabName = "Cyclops_DoD",
					SpawnInterval = TimeSpan.FromSeconds(1800),
				};

				/*LocalSpawnSettings underworldT4Boss = new()
				{
					PrefabName = "",
					SpawnInterval = TimeSpan.FromSeconds(3600),
				};*/

				config.ConfigureLocalSpawnerByName("Spawner_UW_T4")
					.WithSettings(underworldT4Normal);

				//config.ConfigureLocalSpawnerByName("Spawner_UW_T4_Elite")
				//	.WithSettings(underworldT4Elite);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T4_MiniBoss")
					.WithSettings(underworldT4MiniBoss);

				config.ConfigureLocalSpawnerByName("Spawner_UW_T4_SubBoss")
					.WithSettings(underworldT4SubBoss);

				//config.ConfigureLocalSpawnerByName("Spawner_UW_T4_Boss")
				//	.WithSettings(underworldT4Boss);
			}
            catch (Exception e)
			{
				Log.LogError(e);
			}
		}
		private static void ConfigureWorldSpawner(ISpawnerConfigurationCollection config)
		{
			//Debug.Log("Fantasy Creatures: Create Spawns");
			try
			{
				config.ConfigureWorldSpawner(20_014)
					.SetPrefabName("DemonLord_DoD")
					.SetTemplateName("Demon Lord")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(210))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(4)
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_013)
					.SetPrefabName("FireElemental_DoD")
					.SetTemplateName("Fire Elemental")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_012)
					.SetPrefabName("IceElemental_DoD")
					.SetTemplateName("Ice Elemental")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(45)
					.SetSpawnAtDistanceToPlayerMax(60)
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_011)
					.SetPrefabName("EarthElemental_DoD")
					.SetTemplateName("Earth Elemental")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_010)
					.SetPrefabName("Manticore_FC")
					.SetTemplateName("Manticore")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetSpawnDuringDay(false)
					.SetConditionRequiredGlobalKey("defeated_goblinking")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_009)
					.SetPrefabName("Cyclops_DoD")
					.SetTemplateName("Cyclops")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetConditionRequiredGlobalKey("defeated_goblinking")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_008)
					.SetPrefabName("Ogre_DoD")
					.SetTemplateName("Ogre")
					.SetConditionBiomes(Heightmap.Biome.Mountain)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(150))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionRequiredGlobalKey("defeated_moder")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_006)
					.SetPrefabName("Mummy_FC")
					.SetTemplateName("Mummy")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(120))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					.SetConditionAltitudeMin(0.01f)
					;
				config.ConfigureWorldSpawner(20_005)
					.SetPrefabName("Ghoul_FC")
					.SetTemplateName("Ghoul")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(120))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					.SetConditionAltitudeMin(0.01f)
					;
				config.ConfigureWorldSpawner(20_004)
					.SetPrefabName("TreeEnt_DoD")
					.SetTemplateName("Tree Ent")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(120))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionRequiredGlobalKey("defeated_gdking")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_003)
					.SetPrefabName("Hobgoblin_DoD")
					.SetTemplateName("Hobgoblin")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(90))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionEnvironments("Misty", "DeepForest Mist")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_002)
					.SetPrefabName("GiantViper_FC")
					.SetTemplateName("Giant Viper")
					.SetConditionBiomes(Heightmap.Biome.Ocean)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(90))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetConditionAltitudeMin(-100f)
					.SetConditionAltitudeMax(-0.125f)
					.SetConditionOceanDepthMin(1.25f)
					.SetConditionOceanDepthMax(5f)
					;
				config.ConfigureWorldSpawner(20_001)
					.SetPrefabName("Kobold_DoD")
					.SetTemplateName("Kobold")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(60))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(4)
					.SetConditionEnvironments("Misty", "DeepForest Mist")
					.SetConditionAltitudeMin(0.1f)
					;
				config.ConfigureWorldSpawner(20_000)
					.SetPrefabName("DarknessSpider_FC")
					.SetTemplateName("Darkness Spider")
					.SetConditionBiomes(Heightmap.Biome.Meadows)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(60))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetSpawnDuringDay(false)
					.SetConditionAltitudeMin(0.1f)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
