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
using SpawnThat.Integrations.CLLC.Models;
using SpawnThat.Integrations.EpicLoot.Models;
using SpawnThat.Options.Conditions;
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

		public const string PluginVersion = "0.0.1";

		//public static GameObject BeholderA1;
		//public static GameObject BeholderA2;
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
		/*public static GameObject HydraA1;
		public static GameObject HydraA2;
		public static GameObject HydraA3;
		public static GameObject HydraA4;
		public static GameObject DragonA1;
		public static GameObject DragonA2;
		public static GameObject DragonA3;
		public static GameObject DragonA4;
		public static GameObject DragonA5;
		public static GameObject DragonA6;
		public static GameObject DragonA7;*/
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
		/*public static GameObject GriffinA1;
		public static GameObject GriffinA2;
		public static GameObject GriffinA3;
		public static GameObject GriffinA4;
		public static GameObject GriffinA5;
		public static GameObject GriffinA6;
		public static GameObject HarpyA1;
		public static GameObject HarpyA2;
		public static GameObject HarpyA3;
		public static GameObject HarpyA4;
		public static GameObject HarpyA5;*/
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

		public AssetBundle FantasyBundle;
		private Harmony _harmony;
		internal static ManualLogSource Log;

		public ConfigEntry<bool> WorldSpawnsEnable;
		public ConfigEntry<bool> UnderworldSpawnsEnable;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			WorldSpawnsEnable = base.Config.Bind("World Spawns", "Enable", defaultValue: true, new ConfigDescription("Enables Spawns around the world.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			UnderworldSpawnsEnable = base.Config.Bind("Do Or Die Biomes", "Enable", defaultValue: false, new ConfigDescription("Enables Spawns in the Underworld.", null, new ConfigurationManagerAttributes
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
			if (UnderworldSpawnsEnable.Value == true)
            {
				try
				{
					SpawnerConfigurationManager.OnConfigure += ConfigureUnderworldSpawners;
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
			Debug.Log("Fantasy Creatures: Spider Attacks");
			SpiderA1 = FantasyBundle.LoadAsset<GameObject>("Spider_Attack1_DoD");
			CustomPrefab Spiderattack1 = new CustomPrefab(SpiderA1, true);
			PrefabManager.Instance.AddPrefab(Spiderattack1);
			ViperA1 = FantasyBundle.LoadAsset<GameObject>("Viper_Attack1_DoD");
			CustomPrefab Viperattack1 = new CustomPrefab(ViperA1, true);
			PrefabManager.Instance.AddPrefab(Viperattack1);

			Debug.Log("Fantasy Creatures: Manticore Attacks");
			ManticoreA1 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack1_DoD");
			CustomPrefab Manticoreattack1 = new CustomPrefab(ManticoreA1, true);
			PrefabManager.Instance.AddPrefab(Manticoreattack1);
			ManticoreA2 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack2_DoD");
			CustomPrefab Manticoreattack2 = new CustomPrefab(ManticoreA2, true);
			PrefabManager.Instance.AddPrefab(Manticoreattack2);
			ManticoreA3 = FantasyBundle.LoadAsset<GameObject>("Manticore_Attack3_DoD");
			CustomPrefab Manticoreattack3 = new CustomPrefab(ManticoreA3, true);
			PrefabManager.Instance.AddPrefab(Manticoreattack3);
			ManticoreA4 = FantasyBundle.LoadAsset<GameObject>("Manticore_AttackCombo1_DoD");
			CustomPrefab Manticoreattack4 = new CustomPrefab(ManticoreA4, true);
			PrefabManager.Instance.AddPrefab(Manticoreattack4);
			ManticoreA5 = FantasyBundle.LoadAsset<GameObject>("Manticore_AttackSting_DoD");
			CustomPrefab Manticoreattack5 = new CustomPrefab(ManticoreA5, true);
			PrefabManager.Instance.AddPrefab(Manticoreattack5);

			/*Debug.Log("Fantasy Creatures: Harpy Attacks");
			HarpyA1 = FantasyBundle.LoadAsset<GameObject>("Harpy_Attack1_DoD");
			CustomPrefab Harpyattack1 = new CustomPrefab(HarpyA1, true);
			PrefabManager.Instance.AddPrefab(Harpyattack1);
			HarpyA2 = FantasyBundle.LoadAsset<GameObject>("Harpy_Attack2_DoD");
			CustomPrefab Harpyattack2 = new CustomPrefab(HarpyA2, true);
			PrefabManager.Instance.AddPrefab(Harpyattack2);
			HarpyA3 = FantasyBundle.LoadAsset<GameObject>("Harpy_Attack3_DoD");
			CustomPrefab Harpyattack3 = new CustomPrefab(HarpyA3, true);
			PrefabManager.Instance.AddPrefab(Harpyattack3);
			HarpyA4 = FantasyBundle.LoadAsset<GameObject>("Harpy_AttackCombo3_DoD");
			CustomPrefab Harpyattack4 = new CustomPrefab(HarpyA4, true);
			PrefabManager.Instance.AddPrefab(Harpyattack4);
			HarpyA5 = FantasyBundle.LoadAsset<GameObject>("Harpy_AttackCombo4_DoD");
			CustomPrefab Harpyattack5 = new CustomPrefab(HarpyA5, true);
			PrefabManager.Instance.AddPrefab(Harpyattack5);*/

			/*Debug.Log("Fantasy Creatures: Griffin Attacks");
			GriffinA1 = FantasyBundle.LoadAsset<GameObject>("Griffin_Attack1_DoD");
			CustomPrefab Griffinattack1 = new CustomPrefab(GriffinA1, true);
			PrefabManager.Instance.AddPrefab(Griffinattack1);
			GriffinA2 = FantasyBundle.LoadAsset<GameObject>("Griffin_Attack2_DoD");
			CustomPrefab Griffinattack2 = new CustomPrefab(GriffinA2, true);
			PrefabManager.Instance.AddPrefab(Griffinattack2);
			GriffinA3 = FantasyBundle.LoadAsset<GameObject>("Griffin_Attack3_DoD");
			CustomPrefab Griffinattack3 = new CustomPrefab(GriffinA3, true);
			PrefabManager.Instance.AddPrefab(Griffinattack3);
			GriffinA4 = FantasyBundle.LoadAsset<GameObject>("Griffin_AttackCombo2_DoD");
			CustomPrefab Griffinattack4 = new CustomPrefab(GriffinA4, true);
			PrefabManager.Instance.AddPrefab(Griffinattack4);
			GriffinA5 = FantasyBundle.LoadAsset<GameObject>("Griffin_AttackCombo3_DoD");
			CustomPrefab Griffinattack5 = new CustomPrefab(GriffinA5, true);
			PrefabManager.Instance.AddPrefab(Griffinattack5);
			GriffinA6 = FantasyBundle.LoadAsset<GameObject>("Griffin_AttackFlight_DoD");
			CustomPrefab Griffinattack6 = new CustomPrefab(GriffinA6, true);
			PrefabManager.Instance.AddPrefab(Griffinattack6);*/

			Debug.Log("Fantasy Creatures: Ghoul Attacks");
			GhoulA1 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack1_DoD");
			CustomPrefab Ghoulattack1 = new CustomPrefab(GhoulA1, true);
			PrefabManager.Instance.AddPrefab(Ghoulattack1);
			GhoulA2 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack2_DoD");
			CustomPrefab Ghoulattack2 = new CustomPrefab(GhoulA2, true);
			PrefabManager.Instance.AddPrefab(Ghoulattack2);
			GhoulA3 = FantasyBundle.LoadAsset<GameObject>("Ghoul_Attack3_DoD");
			CustomPrefab Ghoulattack3 = new CustomPrefab(GhoulA3, true);
			PrefabManager.Instance.AddPrefab(Ghoulattack3);
			GhoulA4 = FantasyBundle.LoadAsset<GameObject>("Ghoul_AttackCombo2_DoD");
			CustomPrefab Ghoulattack4 = new CustomPrefab(GhoulA4, true);
			PrefabManager.Instance.AddPrefab(Ghoulattack4);
			GhoulA5 = FantasyBundle.LoadAsset<GameObject>("Ghoul_AttackCombo3_DoD");
			CustomPrefab Ghoulattack5 = new CustomPrefab(GhoulA5, true);
			PrefabManager.Instance.AddPrefab(Ghoulattack5);

			Debug.Log("Fantasy Creatures: Mummy Attacks");
			MummyA1 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack1_DoD");
			CustomPrefab mummyattack1 = new CustomPrefab(MummyA1, true);
			PrefabManager.Instance.AddPrefab(mummyattack1);
			MummyA2 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack2_DoD");
			CustomPrefab mummyattack2 = new CustomPrefab(MummyA2, true);
			PrefabManager.Instance.AddPrefab(mummyattack2);
			MummyA3 = FantasyBundle.LoadAsset<GameObject>("Mummy_Attack3_DoD");
			CustomPrefab mummyattack3 = new CustomPrefab(MummyA3, true);
			PrefabManager.Instance.AddPrefab(mummyattack3);
			MummyA4 = FantasyBundle.LoadAsset<GameObject>("Mummy_AttackCombo2_DoD");
			CustomPrefab mummyattack4 = new CustomPrefab(MummyA4, true);
			PrefabManager.Instance.AddPrefab(mummyattack4);
			MummyA5 = FantasyBundle.LoadAsset<GameObject>("Mummy_AttackCombo3_DoD");
			CustomPrefab mummyattack5 = new CustomPrefab(MummyA5, true);
			PrefabManager.Instance.AddPrefab(mummyattack5);
			MummyR1 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip1_DoD");
			CustomPrefab mummyattack6 = new CustomPrefab(MummyR1, true);
			PrefabManager.Instance.AddPrefab(mummyattack6);
			MummyR2 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip2_DoD");
			CustomPrefab mummyattack7 = new CustomPrefab(MummyR2, true);
			PrefabManager.Instance.AddPrefab(mummyattack7);
			MummyR3 = FantasyBundle.LoadAsset<GameObject>("Mummy_Whip3_DoD");
			CustomPrefab mummyattack8 = new CustomPrefab(MummyR3, true);
			PrefabManager.Instance.AddPrefab(mummyattack8);
			MummyR4 = FantasyBundle.LoadAsset<GameObject>("Mummy_WhipCombo2_DoD");
			CustomPrefab mummyattack9 = new CustomPrefab(MummyR4, true);
			PrefabManager.Instance.AddPrefab(mummyattack9);
			MummyR5 = FantasyBundle.LoadAsset<GameObject>("Mummy_WhipCombo3_DoD");
			CustomPrefab mummyattack10 = new CustomPrefab(MummyR5, true);
			PrefabManager.Instance.AddPrefab(mummyattack10);

			/*Debug.Log("Fantasy Creatures: Beholder Attacks");
			BeholderA1 = FantasyBundle.LoadAsset<GameObject>("Beholder_Attack1_DoD");
			CustomPrefab attack1 = new CustomPrefab(BeholderA1, true);
			PrefabManager.Instance.AddPrefab(attack1);
			BeholderA2 = FantasyBundle.LoadAsset<GameObject>("Beholder_Attack2_DoD");
			CustomPrefab attack2 = new CustomPrefab(BeholderA2, true);
			PrefabManager.Instance.AddPrefab(attack2);*/

			Debug.Log("Fantasy Creatures: Ent Arracks");
			EntA1 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack1_DoD");
			CustomPrefab attack3 = new CustomPrefab(EntA1, true);
			PrefabManager.Instance.AddPrefab(attack3);
			EntA2 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack2_DoD");
			CustomPrefab attack4 = new CustomPrefab(EntA2, true);
			PrefabManager.Instance.AddPrefab(attack4);
			EntA3 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack3_DoD");
			CustomPrefab attack5 = new CustomPrefab(EntA3, true);
			PrefabManager.Instance.AddPrefab(attack5);
			EntA4 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack2Combo_DoD");
			CustomPrefab attack6 = new CustomPrefab(EntA4, true);
			PrefabManager.Instance.AddPrefab(attack6);
			EntA5 = FantasyBundle.LoadAsset<GameObject>("Ent_Attack3Combo_DoD");
			CustomPrefab attack7 = new CustomPrefab(EntA5, true);
			PrefabManager.Instance.AddPrefab(attack7);

			Debug.Log("Fantasy Creatures: Demon Attacks");
			DemonLordA1 = FantasyBundle.LoadAsset<GameObject>("DemonLord_Attack1_DoD");
			CustomPrefab attack8 = new CustomPrefab(DemonLordA1, true);
			PrefabManager.Instance.AddPrefab(attack8);
			DemonLordA2 = FantasyBundle.LoadAsset<GameObject>("DemonLord_Attack2_DoD");
			CustomPrefab attack9 = new CustomPrefab(DemonLordA2, true);
			PrefabManager.Instance.AddPrefab(attack9);
			DemonLordA3 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackWhip_DoD");
			CustomPrefab attack10 = new CustomPrefab(DemonLordA3, true);
			PrefabManager.Instance.AddPrefab(attack10);
			DemonLordA4 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackCombo2_DoD");
			CustomPrefab attack11 = new CustomPrefab(DemonLordA4, true);
			PrefabManager.Instance.AddPrefab(attack11);
			DemonLordA5 = FantasyBundle.LoadAsset<GameObject>("DemonLord_AttackCombo3_DoD");
			CustomPrefab attack12 = new CustomPrefab(DemonLordA5, true);
			PrefabManager.Instance.AddPrefab(attack12);

			Debug.Log("Fantasy Creatures: Elemental Attacks");
			ElementalA1 = FantasyBundle.LoadAsset<GameObject>("Element_Attack1_DoD");
			CustomPrefab attack13 = new CustomPrefab(ElementalA1, true);
			PrefabManager.Instance.AddPrefab(attack13);
			ElementalA2 = FantasyBundle.LoadAsset<GameObject>("Element_Attack2_DoD");
			CustomPrefab attack14 = new CustomPrefab(ElementalA2, true);
			PrefabManager.Instance.AddPrefab(attack14);
			ElementalA3 = FantasyBundle.LoadAsset<GameObject>("Element_AttackGrab_DoD");
			CustomPrefab attack15 = new CustomPrefab(ElementalA3, true);
			PrefabManager.Instance.AddPrefab(attack15);
			ElementalA4 = FantasyBundle.LoadAsset<GameObject>("Element_AttackSmash_DoD");
			CustomPrefab attack16 = new CustomPrefab(ElementalA4, true);
			PrefabManager.Instance.AddPrefab(attack16);
			ElementalA5 = FantasyBundle.LoadAsset<GameObject>("Element_AttackSwipe_DoD");
			CustomPrefab attack17 = new CustomPrefab(ElementalA5, true);
			PrefabManager.Instance.AddPrefab(attack17);
			ElementalA6 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo1_DoD");
			CustomPrefab attack18 = new CustomPrefab(ElementalA6, true);
			PrefabManager.Instance.AddPrefab(attack18);
			ElementalA7 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo2_DoD");
			CustomPrefab attack19 = new CustomPrefab(ElementalA7, true);
			PrefabManager.Instance.AddPrefab(attack19);
			ElementalA8 = FantasyBundle.LoadAsset<GameObject>("Element_AttackCombo3_DoD");
			CustomPrefab attack20 = new CustomPrefab(ElementalA8, true);
			PrefabManager.Instance.AddPrefab(attack20);

			Debug.Log("Fantasy Creatures: Ice Elemental Attacks");
			IceElementalA1 = FantasyBundle.LoadAsset<GameObject>("IceElement_Attack1_DoD");
			CustomPrefab attack21 = new CustomPrefab(IceElementalA1, true);
			PrefabManager.Instance.AddPrefab(attack21);
			IceElementalA2 = FantasyBundle.LoadAsset<GameObject>("IceElement_Attack2_DoD");
			CustomPrefab attack22 = new CustomPrefab(IceElementalA2, true);
			PrefabManager.Instance.AddPrefab(attack22);
			IceElementalA3 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackGrab_DoD");
			CustomPrefab attack23 = new CustomPrefab(IceElementalA3, true);
			PrefabManager.Instance.AddPrefab(attack23);
			IceElementalA4 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackSmash_DoD");
			CustomPrefab attack24 = new CustomPrefab(IceElementalA4, true);
			PrefabManager.Instance.AddPrefab(attack24);
			IceElementalA5 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackSwipe_DoD");
			CustomPrefab attack25 = new CustomPrefab(IceElementalA5, true);
			PrefabManager.Instance.AddPrefab(attack25);
			IceElementalA6 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo1_DoD");
			CustomPrefab attack26 = new CustomPrefab(IceElementalA6, true);
			PrefabManager.Instance.AddPrefab(attack26);
			IceElementalA7 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo2_DoD");
			CustomPrefab attack27 = new CustomPrefab(IceElementalA7, true);
			PrefabManager.Instance.AddPrefab(attack27);
			IceElementalA8 = FantasyBundle.LoadAsset<GameObject>("IceElement_AttackCombo3_DoD");
			CustomPrefab attack28 = new CustomPrefab(IceElementalA8, true);
			PrefabManager.Instance.AddPrefab(attack28);

			Debug.Log("Fantasy Creatures: Fire Elemental Attacks");
			FireElementalA1 = FantasyBundle.LoadAsset<GameObject>("FireElement_Attack1_DoD");
			CustomPrefab attack29 = new CustomPrefab(FireElementalA1, true);
			PrefabManager.Instance.AddPrefab(attack29);
			FireElementalA2 = FantasyBundle.LoadAsset<GameObject>("FireElement_Attack2_DoD");
			CustomPrefab attack30 = new CustomPrefab(FireElementalA2, true);
			PrefabManager.Instance.AddPrefab(attack30);
			FireElementalA3 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackGrab_DoD");
			CustomPrefab attack31 = new CustomPrefab(FireElementalA3, true);
			PrefabManager.Instance.AddPrefab(attack31);
			FireElementalA4 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackSmash_DoD");
			CustomPrefab attack32 = new CustomPrefab(FireElementalA4, true);
			PrefabManager.Instance.AddPrefab(attack32);
			FireElementalA5 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackSwipe_DoD");
			CustomPrefab attack33 = new CustomPrefab(FireElementalA5, true);
			PrefabManager.Instance.AddPrefab(attack33);
			FireElementalA6 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo1_DoD");
			CustomPrefab attack34 = new CustomPrefab(FireElementalA6, true);
			PrefabManager.Instance.AddPrefab(attack34);
			FireElementalA7 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo2_DoD");
			CustomPrefab attack35 = new CustomPrefab(FireElementalA7, true);
			PrefabManager.Instance.AddPrefab(attack35);
			FireElementalA8 = FantasyBundle.LoadAsset<GameObject>("FireElement_AttackCombo3_DoD");
			CustomPrefab attack36 = new CustomPrefab(FireElementalA8, true);
			PrefabManager.Instance.AddPrefab(attack36);

			Debug.Log("Fantasy Creatures: Kobold Attacks");
			KoboldA1 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack1_DoD");
			CustomPrefab attack37 = new CustomPrefab(KoboldA1, true);
			PrefabManager.Instance.AddPrefab(attack37);
			KoboldA2 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack2_DoD");
			CustomPrefab attack38 = new CustomPrefab(KoboldA2, true);
			PrefabManager.Instance.AddPrefab(attack38);
			KoboldA3 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack3_DoD");
			CustomPrefab attack39 = new CustomPrefab(KoboldA3, true);
			PrefabManager.Instance.AddPrefab(attack39);
			KoboldA4 = FantasyBundle.LoadAsset<GameObject>("Kobold_Attack4_DoD");
			CustomPrefab attack40 = new CustomPrefab(KoboldA4, true);
			PrefabManager.Instance.AddPrefab(attack40);
			KoboldA5 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo1_DoD");
			CustomPrefab attack41 = new CustomPrefab(KoboldA5, true);
			PrefabManager.Instance.AddPrefab(attack41);
			KoboldA6 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo2_DoD");
			CustomPrefab attack42 = new CustomPrefab(KoboldA6, true);
			PrefabManager.Instance.AddPrefab(attack42);
			KoboldA7 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo3_DoD");
			CustomPrefab attack43 = new CustomPrefab(KoboldA7, true);
			PrefabManager.Instance.AddPrefab(attack43);
			KoboldA8 = FantasyBundle.LoadAsset<GameObject>("Kobold_AttackCombo4_DoD");
			CustomPrefab attack44 = new CustomPrefab(KoboldA8, true);
			PrefabManager.Instance.AddPrefab(attack44);

			/*Debug.Log("Fantasy Creatures: Hydra Attacks");
			HydraA1 = FantasyBundle.LoadAsset<GameObject>("Hydra_Attack1_DoD");
			CustomPrefab attack45 = new CustomPrefab(HydraA1, true);
			PrefabManager.Instance.AddPrefab(attack45);
			HydraA2 = FantasyBundle.LoadAsset<GameObject>("Hydra_Attack2_DoD");
			CustomPrefab attack46 = new CustomPrefab(HydraA2, true);
			PrefabManager.Instance.AddPrefab(attack46);
			HydraA3 = FantasyBundle.LoadAsset<GameObject>("Hydra_AttackBreath_DoD");
			CustomPrefab attack47 = new CustomPrefab(HydraA3, true);
			PrefabManager.Instance.AddPrefab(attack47);
			HydraA4 = FantasyBundle.LoadAsset<GameObject>("Hydra_AttackSpit_DoD");
			CustomPrefab attack48 = new CustomPrefab(HydraA4, true);
			PrefabManager.Instance.AddPrefab(attack48);*/

			/*Debug.Log("Fantasy Creatures: Dragon Attacks");
			DragonA1 = FantasyBundle.LoadAsset<GameObject>("Dragon_Attack1_DoD");
			CustomPrefab attack49 = new CustomPrefab(DragonA1, true);
			PrefabManager.Instance.AddPrefab(attack49);
			DragonA2 = FantasyBundle.LoadAsset<GameObject>("Dragon_Attack2_DoD");
			CustomPrefab attack50 = new CustomPrefab(DragonA2, true);
			PrefabManager.Instance.AddPrefab(attack50);
			DragonA3 = FantasyBundle.LoadAsset<GameObject>("Dragon_AttackBreath_DoD");
			CustomPrefab attack51 = new CustomPrefab(DragonA3, true);
			PrefabManager.Instance.AddPrefab(attack51);
			DragonA4 = FantasyBundle.LoadAsset<GameObject>("Dragon_AttackSpit_DoD");
			CustomPrefab attack52 = new CustomPrefab(DragonA4, true);
			PrefabManager.Instance.AddPrefab(attack52);
			DragonA5 = FantasyBundle.LoadAsset<GameObject>("Dragon_AttackCombo1_DoD");
			CustomPrefab attack53 = new CustomPrefab(DragonA5, true);
			PrefabManager.Instance.AddPrefab(attack53);
			DragonA6 = FantasyBundle.LoadAsset<GameObject>("Dragon_AttackBreathFly_DoD");
			CustomPrefab attack54 = new CustomPrefab(DragonA6, true);
			PrefabManager.Instance.AddPrefab(attack54);
			DragonA7 = FantasyBundle.LoadAsset<GameObject>("Dragon_AttackSpitFly_DoD");
			CustomPrefab attack55 = new CustomPrefab(DragonA7, true);
			PrefabManager.Instance.AddPrefab(attack55);*/

			Debug.Log("Fantasy Creatures: Ogre Attacks");
			OgreA1 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack1_DoD");
			CustomPrefab attack56 = new CustomPrefab(OgreA1, true);
			PrefabManager.Instance.AddPrefab(attack56);
			OgreA2 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack2_DoD");
			CustomPrefab attack57 = new CustomPrefab(OgreA2, true);
			PrefabManager.Instance.AddPrefab(attack57);
			OgreA3 = FantasyBundle.LoadAsset<GameObject>("Ogre_Attack3_DoD");
			CustomPrefab attack58 = new CustomPrefab(OgreA3, true);
			PrefabManager.Instance.AddPrefab(attack58);
			OgreA4 = FantasyBundle.LoadAsset<GameObject>("Ogre_AttackCombo1_DoD");
			CustomPrefab attack59 = new CustomPrefab(OgreA4, true);
			PrefabManager.Instance.AddPrefab(attack59);
			OgreA5 = FantasyBundle.LoadAsset<GameObject>("Ogre_AttackCombo2_DoD");
			CustomPrefab attack60 = new CustomPrefab(OgreA5, true);
			PrefabManager.Instance.AddPrefab(attack60);

			Debug.Log("Fantasy Creatures: Hobgoblin Attacks");
			HobgoblinA1 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack1_DoD");
			CustomPrefab attack61 = new CustomPrefab(HobgoblinA1, true);
			PrefabManager.Instance.AddPrefab(attack61);
			HobgoblinA2 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack2_DoD");
			CustomPrefab attack62 = new CustomPrefab(HobgoblinA2, true);
			PrefabManager.Instance.AddPrefab(attack62);
			HobgoblinA3 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_Attack3_DoD");
			CustomPrefab attack63 = new CustomPrefab(HobgoblinA3, true);
			PrefabManager.Instance.AddPrefab(attack63);
			HobgoblinA4 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_AttackCombo3_DoD");
			CustomPrefab attack64 = new CustomPrefab(HobgoblinA4, true);
			PrefabManager.Instance.AddPrefab(attack64);
			HobgoblinA5 = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_AttackCombo2_DoD");
			CustomPrefab attack65 = new CustomPrefab(HobgoblinA5, true);
			PrefabManager.Instance.AddPrefab(attack65);

			Debug.Log("Fantasy Creatures: Cyclops Attacks");
			CyclopsA1 = FantasyBundle.LoadAsset<GameObject>("Cyclops_Attack1_DoD");
			CustomPrefab attack66 = new CustomPrefab(CyclopsA1, true);
			PrefabManager.Instance.AddPrefab(attack66);
			CyclopsA2 = FantasyBundle.LoadAsset<GameObject>("Cyclops_Attack2_DoD");
			CustomPrefab attack67 = new CustomPrefab(CyclopsA2, true);
			PrefabManager.Instance.AddPrefab(attack67);
			CyclopsA3 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackSmash_DoD");
			CustomPrefab attack68 = new CustomPrefab(CyclopsA3, true);
			PrefabManager.Instance.AddPrefab(attack68);
			CyclopsA4 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCombo3_DoD");
			CustomPrefab attack69 = new CustomPrefab(CyclopsA4, true);
			PrefabManager.Instance.AddPrefab(attack69);
			CyclopsA5 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCombo2_DoD");
			CustomPrefab attack70 = new CustomPrefab(CyclopsA5, true);
			PrefabManager.Instance.AddPrefab(attack70);
			CyclopsA6 = FantasyBundle.LoadAsset<GameObject>("Cyclops_AttackCrush_DoD");
			CustomPrefab attack71 = new CustomPrefab(CyclopsA6, true);
			PrefabManager.Instance.AddPrefab(attack71);

			Debug.Log("Fantasy Creatures: SFX");
			GameObject SFXEntGetHit = FantasyBundle.LoadAsset<GameObject>("SFX_EntGetHit_DoD");
			PrefabManager.Instance.AddPrefab(SFXEntGetHit);
			GameObject SFXEntAlert = FantasyBundle.LoadAsset<GameObject>("SFX_EntAlert_DoD");
			PrefabManager.Instance.AddPrefab(SFXEntAlert);
			GameObject SFXEntIdle = FantasyBundle.LoadAsset<GameObject>("SFX_EntIdle_DoD");
			PrefabManager.Instance.AddPrefab(SFXEntIdle);
			GameObject SFXEntDeath = FantasyBundle.LoadAsset<GameObject>("SFX_EntDeath_DoD");
			PrefabManager.Instance.AddPrefab(SFXEntDeath);
			GameObject SFXDLGetHit = FantasyBundle.LoadAsset<GameObject>("SFX_DLGetHit_DoD");
			PrefabManager.Instance.AddPrefab(SFXDLGetHit);
			GameObject SFXDLAlert = FantasyBundle.LoadAsset<GameObject>("SFX_DLAlert_DoD");
			PrefabManager.Instance.AddPrefab(SFXDLAlert);
			GameObject SFXDLIdle = FantasyBundle.LoadAsset<GameObject>("SFX_DLIdle_DoD");
			PrefabManager.Instance.AddPrefab(SFXDLIdle);
			GameObject SFXDLDeath = FantasyBundle.LoadAsset<GameObject>("SFX_DLDeath_DoD");
			PrefabManager.Instance.AddPrefab(SFXDLDeath);
			Debug.Log("Fantasy Creatures: SFX Done");
		}
		private void AddNewCreatures()
		{
			try
			{
				Debug.Log("Fantasy Creatures: DemonLord");
				var DemonLordFab = FantasyBundle.LoadAsset<GameObject>("DemonLord_DoD");
				var DemonLordMob = new CustomCreature(DemonLordFab, false,
					new CreatureConfig
					{
						//Name = "DemonLord",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(DemonLordMob);
				Debug.Log("Fantasy Creatures: FireElemental");
				var FireElementalFab = FantasyBundle.LoadAsset<GameObject>("FireElemental_DoD");
				var FireElementalMob = new CustomCreature(FireElementalFab, false,
					new CreatureConfig
					{
						//Name = "Fire Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(FireElementalMob);
				Debug.Log("Fantasy Creatures: IceElemental");
				var IceElementalFab = FantasyBundle.LoadAsset<GameObject>("IceElemental_DoD");
				var IceElementalMob = new CustomCreature(IceElementalFab, false,
					new CreatureConfig
					{
						//Name = "Ice Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(IceElementalMob);
				Debug.Log("Fantasy Creatures: EarthElemental");
				var EarthElementalFab = FantasyBundle.LoadAsset<GameObject>("EarthElemental_DoD");
				var EarthElementalMob = new CustomCreature(EarthElementalFab, false,
					new CreatureConfig
					{
						//Name = "Earth Elemental",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						},
					});
				CreatureManager.Instance.AddCreature(EarthElementalMob);
				Debug.Log("Fantasy Creatures: Manticore");
				var ManticoreFab = FantasyBundle.LoadAsset<GameObject>("Manticore_FC");
				var ManticoreMob = new CustomCreature(ManticoreFab, false,
					new CreatureConfig
					{
						//Name = "Manticore",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(ManticoreMob);
				Debug.Log("Fantasy Creatures: Cyclops");
				var CyclopsFab = FantasyBundle.LoadAsset<GameObject>("Cyclops_DoD");
				var CyclopsMob = new CustomCreature(CyclopsFab, false,
					new CreatureConfig
					{
						//Name = "Cyclops",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(CyclopsMob);
				Debug.Log("Fantasy Creatures: Ogre");
				var OgreFab = FantasyBundle.LoadAsset<GameObject>("Ogre_DoD");
				var OgreMob = new CustomCreature(OgreFab, false,
					new CreatureConfig
					{
						//Name = "Ogre",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(OgreMob);
				Debug.Log("Fantasy Creatures: Mummy");
				var MummyFab = FantasyBundle.LoadAsset<GameObject>("Mummy_FC");
				var MummyMob = new CustomCreature(MummyFab, false,
					new CreatureConfig
					{
						//Name = "Mummy",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(MummyMob);
				Debug.Log("Fantasy Creatures: Ghoul");
				var GhoulFab = FantasyBundle.LoadAsset<GameObject>("Ghoul_FC");
				var GhoulMob = new CustomCreature(GhoulFab, false,
					new CreatureConfig
					{
						//Name = "Ghoul",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(GhoulMob);
				Debug.Log("Fantasy Creatures: TreeEnt");
				var TreeEntFab = FantasyBundle.LoadAsset<GameObject>("TreeEnt_DoD");
				var TreeEntMob = new CustomCreature(TreeEntFab, false,
					new CreatureConfig
					{
						//Name = "Tree Ent",						
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(TreeEntMob);
				Debug.Log("Fantasy Creatures: Ogre");
				var HobgoblinFab = FantasyBundle.LoadAsset<GameObject>("Hobgoblin_DoD");
				var HobgoblinMob = new CustomCreature(HobgoblinFab, false,
					new CreatureConfig
					{
						//Name = "Hobgoblin",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(HobgoblinMob);
				Debug.Log("Fantasy Creatures: Viper");
				var ViperFab = FantasyBundle.LoadAsset<GameObject>("GiantViper_FC");
				var ViperMob = new CustomCreature(ViperFab, false,
					new CreatureConfig
					{
						//Name = "Viper",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(ViperMob);
				Debug.Log("Fantasy Creatures: Kobold");
				var KoboldFab = FantasyBundle.LoadAsset<GameObject>("Kobold_DoD");
				var KoboldMob = new CustomCreature(KoboldFab, false,
					new CreatureConfig
					{
						//Name = "Kobold",
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(KoboldMob);
				Debug.Log("Fantasy Creatures: Spider");
				var SpiderFab = FantasyBundle.LoadAsset<GameObject>("DarknessSpider_FC");
				var SpiderMob = new CustomCreature(SpiderFab, false,
					new CreatureConfig
					{
						DropConfigs = new[]
						{
							new DropConfig
							{
								Item = "Coins",
								MinAmount = 3,
								MaxAmount = 10
							}
						}
					});
				CreatureManager.Instance.AddCreature(SpiderMob);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding custom creatures: {ex}");
			}
			finally
			{
				FantasyBundle.Unload(false);
			}
		}
		public static void ConfigureUnderworldSpawners(ISpawnerConfigurationCollection config)
		{
			Debug.Log("Fantasy Creatures: Configure Underworld Spawns");
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
			Debug.Log("Fantasy Creatures: Configure Spawns");
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
			Debug.Log("Fantasy Creatures: Create Underworld Spawns");
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
			Debug.Log("Fantasy Creatures: Create Spawns");
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)					
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(20_010)
					.SetPrefabName("Manticore_FC")
					.SetTemplateName("Maticore")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(3)
					.SetSpawnDuringDay(false)
					.SetConditionRequiredGlobalKey("defeated_goblinking")
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					;
				config.ConfigureWorldSpawner(20_002)
					.SetPrefabName("GiantViper_FC")
					.SetTemplateName("Giant Viper")
					.SetConditionBiomes(Heightmap.Biome.BlackForest)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(90))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(2)
					.SetSpawnDuringDay(false)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
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
					.SetSpawnAtDistanceToPlayerMin(60)
					.SetSpawnAtDistanceToPlayerMax(100)
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
					.SetSpawnAtDistanceToPlayerMin(60)
					.SetSpawnAtDistanceToPlayerMax(100)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}
	}
}
