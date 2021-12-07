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

namespace DoDMonsters
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDMonsters";

		public const string PluginName = "DoOrDieMonsters";

		public const string PluginVersion = "0.4.4";

		private Harmony _harmony;

		private static Sprite HoT;
		private static Sprite Regen;
		private static Sprite MagicShield;
		private static Sprite Horror;
		private static Sprite Haunted;
		private static Sprite Hexed;
		private static Sprite Infected;
		private static Sprite Diseased;
		private static Sprite MajorInjury;
		private static Sprite Injury;
		private static Sprite MinorInjury;
		private static Sprite Frostbite;
		private static Sprite Slow;
		private static Sprite Paralyze;
		private static Sprite Weak;
		private static Sprite FlashBurn;
		private static Sprite Blistered;
		private static Sprite Poisoned;

		public static GameObject PoisonedFX;
		public static GameObject InfectedFX;
		public static GameObject SlowFX;
		public static GameObject ParalyzeFX;
		public static GameObject InjuredFX;
		public static GameObject FrostbittenFX;

		public static GameObject Firesoul;
		public static GameObject Solarflare;
		public static GameObject Reaper;
		public static GameObject Desolation;
		public static GameObject Soulstorm;
		public static GameObject Silverlight;
		public static GameObject Coldflame;
		public static GameObject Frostflame;
		public static GameObject Nethersbane;

		public static GameObject WandLightning;
		public static GameObject WandFire;
		public static GameObject WandShadow;
		public static GameObject MaceDivine;
		public static GameObject SwordAssassin;
		public static GameObject AxeKnight;
		public static GameObject SwordVoid;
		public static GameObject SpearAcid;
		public static GameObject AxeBattle;
		public static GameObject MaceShock;
		public static GameObject SpearAoE;

		public static GameObject RugDeer;
		public static GameObject RugDire;
		public static GameObject RugForest;

		public static GameObject AnvilsFel;
		public static GameObject AnvilsFro;
		public static GameObject AnvilsFlam;

		public static GameObject T1ArmorKit;
		public static GameObject T2ArmorKit;
		public static GameObject T3ArmorKit;
		public static GameObject T4ArmorKit;
		public static GameObject T5ArmorKit;
		public static GameObject T6ArmorKit;
		public static GameObject T7ArmorKit;
		public static GameObject T8ArmorKit;

		public static GameObject T1WeaponKit;
		public static GameObject T2WeaponKit;
		public static GameObject T3WeaponKit;
		public static GameObject T4WeaponKit;
		public static GameObject T5WeaponKit;
		public static GameObject T6WeaponKit;
		public static GameObject T7WeaponKit;
		public static GameObject T8WeaponKit;

		//public static GameObject Rylan;
		public static GameObject SkirSandburst;
		public static GameObject Farkas;
		public static GameObject Bhygshan;
		public static GameObject Bitterstump;
		public static GameObject Rambore;

		public static GameObject GreaterSurtling;
		public static GameObject CharredRemains;
		public static GameObject SkeletonG;
		public static GameObject SkeletonR;
		public static GameObject FrozenBones;
		public static GameObject BlackDeer;
		public static GameObject BlackStag;
		public static GameObject IceGolem;
		public static GameObject LavaGolem;
		public static GameObject ObsidianGolem;
		public static GameObject GhostWhite;
		public static GameObject GhostIce;
		public static GameObject Frostling;
		public static GameObject Stormling;
		public static GameObject Voidling;
		public static GameObject ForestWolf;
		public static GameObject ForestWolfCub;
		public static GameObject DireWolf;
		public static GameObject DireWolfCub;
		public static GameObject Vilefang;
		public static GameObject VilefangCub;
		public static GameObject LivingLava;
		public static GameObject LivingWater;
		public static GameObject IceDrake;
		public static GameObject FlameDrake;
		public static GameObject ArcaneDrake;
		public static GameObject DarkDrake;
		public static GameObject GoldDrake;
		public static GameObject PoisonDrake;
		public static GameObject BlackDrake;

		public static GameObject TrophyCharredRemains;
		public static GameObject TrophyFrozenBones;
		public static GameObject TrophySurtling;
		public static GameObject TrophySkeletonG;
		public static GameObject TrophySkeletonR;
		public static GameObject TrophyFrostling;
		public static GameObject TrophyStormling;
		public static GameObject TrophyVoidling;
		public static GameObject TrophyOGolem;
		public static GameObject TrophyLGolem;
		public static GameObject TrophyIceGolem;
		public static GameObject TrophyVilefang;
		public static GameObject TrophyDireWolf;
		public static GameObject TrophyForestWolf;
		public static GameObject TrophyLivingLava;
		public static GameObject TrophyLivingWater;
		public static GameObject TrophyBlackDeer;
		public static GameObject TrophyIceDrake;
		public static GameObject TrophyFlameDrake;
		public static GameObject TrophyArcaneDrake;
		public static GameObject TrophyDarknessDrake;
		public static GameObject TrophyGoldDrake;
		public static GameObject TrophyPoisonDrake;
		public static GameObject TrophyDarkDrake;

		public static GameObject InfusedGemstone;
		public static GameObject DeathKnightItem;
		public static GameObject ArcherItem;
		public static GameObject BerserkerItem;
		public static GameObject DruidItem;
		public static GameObject MageItem;
		public static GameObject MonkItem;
		public static GameObject NinjaItem;
		public static GameObject PaladinItem;
		public static GameObject RogueItem;
		public static GameObject ShamanItem;
		public static GameObject WarlockItem;

		public static GameObject BoarTusk;
		public static GameObject LargeFang;
		public static GameObject ShamansVessel;
		public static GameObject GreydwarfHeart;
		public static GameObject SkeletonBones;

		public static GameObject GreyPearl;
		public static GameObject FrozenBone;
		public static GameObject CharredBone;
		public static GameObject FrostlingCore;
		public static GameObject StormlingCore;
		public static GameObject VoidlingCore;
		public static GameObject ForestWolfPelt;
		public static GameObject DireWolfPelt;
		public static GameObject WaterGlobe;
		public static GameObject SpiderChitin;
		public static GameObject BlackDeerHide;
		public static GameObject OakWood;

		public static GameObject ShieldGSkull;
		public static GameObject ShieldBGSkull;
		public static GameObject BhygshanMace;
		public static GameObject ShieldEikthyr;
		public static GameObject ShieldRambore;
		public static GameObject ShieldElder;
		public static GameObject ShieldBitter;
		public static GameObject ShieldBonemass;
		public static GameObject ShieldModer;
		public static GameObject ShieldFarkas;
		public static GameObject ShieldSkir;
		public static GameObject ShieldYagluth;
		public static GameObject ShieldBEikthyr;
		public static GameObject ShieldBRambore;
		public static GameObject ShieldBElder;
		public static GameObject ShieldBBitter;
		public static GameObject ShieldBBonemass;
		public static GameObject ShieldBModer;
		public static GameObject ShieldBFarkas;
		public static GameObject ShieldBSkir;
		public static GameObject ShieldBYagluth;

		public static GameObject BowBlackForest;
		public static GameObject BowSwamp;
		public static GameObject BowMountain;
		public static GameObject BowPlains;
		public static GameObject BowMistlands;
		public static GameObject BowDeepNorth;

		public static GameObject SwordMeadows;
		public static GameObject SwordSwamp;
		public static GameObject SwordPlains;
		public static GameObject SwordMistlands;
		public static GameObject SwordDeepNorth;
		public static GameObject SwordAshLands;

		public static GameObject BowCharred;
		public static GameObject SwordCharred;
		public static GameObject ShieldCharred;
		public static GameObject BowSkelG;
		public static GameObject SwordSkelG;
		public static GameObject BowSkelR;
		public static GameObject SwordSkelR;
		public static GameObject BowFrozenBones;
		public static GameObject SwordFrozenBones;
		public static GameObject SkirSandburstHeaddress;
		public static GameObject SkirSandburstStaff;
		public static GameObject IceGolemHead;
		public static GameObject IceGolemSpikes;
		public static GameObject IceGolemClubs;
		public static GameObject LavaGolemHead;
		public static GameObject LavaGolemSpikes;
		public static GameObject LavaGolemClubs;
		public static GameObject ObsidianGolemHead;
		public static GameObject ObsidianGolemSpikes;
		public static GameObject HelmetBhygshan;
		public static GameObject ObsidianGolemClubs;

		public static GameObject imp_voidbolt_attack;
		public static GameObject imp_stormbolt_attack;
		public static GameObject imp_icebolt_attack;
		public static GameObject livingwater_nova_attack;
		public static GameObject livinglava_nova_attack;
		public static GameObject Vilefang_Attack3;
		public static GameObject Vilefang_Attack2;
		public static GameObject Vilefang_Attack1;
		public static GameObject DireWolf_Attack3;
		public static GameObject DireWolf_Attack2;
		public static GameObject DireWolf_Attack1;
		public static GameObject ForestWolf_Attack3;
		public static GameObject ForestWolf_Attack2;
		public static GameObject ForestWolf_Attack1;
		public static GameObject Mage_FireStrike_Attack;
		public static GameObject Bitterstump_SprayPoison;
		public static GameObject Bitterstump_SprayFrost;
		public static GameObject Bitterstump_Roots;
		public static GameObject Bitterstump_Melee;
		public static GameObject Bitterstump_Heal;
		public static GameObject Bhygshan_Throw;
		public static GameObject Bhygshan_SprayFrost;
		public static GameObject Bhygshan_FireBolt;
		public static GameObject Bhygshan_Fireball;
		public static GameObject Bhygshan_AoE;
		public static GameObject imp_firebolt_attack;
		public static GameObject SkirSandburst_FB_Attack;
		public static GameObject SkirSandburst_Nova;
		public static GameObject SkirSandburst_Heal;
		public static GameObject SkirSandburst_Shield;
		public static GameObject SkirSandburst_FWSum;
		public static GameObject SkirSandburst_VoidSum;
		public static GameObject SkirSandBurst_VoidAttack;
		public static GameObject Rambore_Gore;
		public static GameObject Rambore_Attack;
		public static GameObject Farkas_Attack1;
		public static GameObject Farkas_Attack2;
		public static GameObject Farkas_Attack3;
		public static GameObject Farkas_Bleed;
		public static GameObject Farkas_Hamper_Attack;
		public static GameObject Farkas_FrostBite;
		public static GameObject NPC_NomadAoE_Attack;

		public static Sprite TexFlaAnvil;
		public static Sprite TexFroAnvil;
		public static Sprite TexFelAnvil;
		public static Sprite RugBDeer;
		public static Sprite RugDWolf;
		public static Sprite RugFWolf;

		public static GameObject Walnuts;
		public static GameObject Cherry;
		public static GameObject Apple;
		public static GameObject Banana;
		public static GameObject CBait;

		public static GameObject Skugga;
		public static GameObject Einherjar;
		public static GameObject Testing;
		public static GameObject TestingA1;
		public static GameObject TestingA2;
		public static GameObject TestingA3;

		// mistlands veg
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

		// deep north veg
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

		// ashlands veg
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


		public ConfigEntry<bool> MistlandsLocEnable;
		public ConfigEntry<bool> MistlandsVegEnable;
		public ConfigEntry<bool> FruitEnable;
		public ConfigEntry<bool> ArmorCrateEnable;
		public ConfigEntry<bool> WeaponCrateEnable;
		public ConfigEntry<bool> ClassWeaponEnable;
		public ConfigEntry<bool> WeaponsEnable;
		public ConfigEntry<bool> BuildablesEnable;
		public ConfigEntry<bool> BossesEnable;
		public ConfigEntry<bool> MonstersEnable;
		public ConfigEntry<bool> CastleArenaEnable;
		public ConfigEntry<bool> DeepNorthVegEnable;
		public ConfigEntry<bool> AshLandsVegEnable;
		//public ConfigEntry<bool> RamboreEnable;

		public static GameObject HardLog;
		public static GameObject HardLogHalf;

		public static GameObject WandMountains;
		public static GameObject MaceMistlands;
		public static GameObject MaceDeepNorth;
		public static GameObject DrakespitFire;
		public static GameObject DrakespitArcane;
		public static GameObject DrakespitFrost;
		public static GameObject DrakespitPoison1;
		public static GameObject DrakespitPoison2;
		public static GameObject DrakespitVoid;

		public static GameObject GreenFrog;
		public static GameObject BlackFrog;
		public static GameObject SpottedFrog;
		public static GameObject GreyRabbit;
		public static GameObject BrownRabbit;
		public static GameObject GiantSnail;
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

		public AssetBundle DoDAssets;
		public AssetBundle DoDFixer;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		private void Awake()
		{
			CreateConfigurationValues();
			Debug.Log("DoDMonsters: Loading and Creating Assets");
			LoadBundle();
			LoadDoDAssets();
			CreateDropables(); 
			UpdateBlastFurnace(); 
			if (MonstersEnable.Value == true) {
				CreateMonsterAbilities();
				CreateMonsterItems();
				AddMonsterReskins();  }
			if (ArmorCrateEnable.Value == true) {
				CreateArmorCrates(); }
			if (WeaponCrateEnable.Value == true) {
				CreateWeaponCrates(); }
			if (ClassWeaponEnable.Value == true) {
				CreateClassWeapons(); }
			if (WeaponsEnable.Value == true) {
				CreateMaces();
				CreateWands();
				CreateMagicSwords();
				CreateBows();
				CreateSwords();	}
			if (BuildablesEnable.Value == true) {
				CreateAnvils();
				CreateRugs(); }
			if (BossesEnable.Value == true)	{
				CreateTierItems();
				CreateShields();
				AddBosses(); }
			if (MistlandsVegEnable.Value == true) {
				AddMistlandVegetation(); }
			if (FruitEnable.Value == true)	{
				AddCustomFruitTrees(); }
			if (DeepNorthVegEnable.Value == true) {
				AddDeepNorthVegetation(); }
			if (AshLandsVegEnable.Value == true) {
				AddAshLandsVegetation(); }
			AddNewAnimals();
			ZoneManager.OnVanillaLocationsAvailable += AddLocations;
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDMonsters");
		}
		public void LoadBundle()
        {
			DoDAssets = AssetUtils.LoadAssetBundleFromResources("doordieassets", Assembly.GetExecutingAssembly());
		}
		private void LoadDoDAssets()
		{
			Sheep = DoDAssets.LoadAsset<GameObject>("Sheep_DoD");
			Goat = DoDAssets.LoadAsset<GameObject>("Goat_DoD");
			Goose = DoDAssets.LoadAsset<GameObject>("Goose_DoD");
			Turtle = DoDAssets.LoadAsset<GameObject>("Turtle_DoD");
			Salamander = DoDAssets.LoadAsset<GameObject>("Salamander_DoD");
			Penguin = DoDAssets.LoadAsset<GameObject>("Penguin_DoD");
			Rat = DoDAssets.LoadAsset<GameObject>("Rat_DoD");
			BrownLizard = DoDAssets.LoadAsset<GameObject>("BrownLizard_DoD");
			GreenLizard = DoDAssets.LoadAsset<GameObject>("GreenLizard_DoD");
			SpottedLizard = DoDAssets.LoadAsset<GameObject>("SpottedLizard_DoD");

			DrakespitFire = DoDAssets.LoadAsset<GameObject>("drake_firespit_attack_dod");
			DrakespitArcane = DoDAssets.LoadAsset<GameObject>("drake_arcanespit_attack_dod");
			DrakespitFrost = DoDAssets.LoadAsset<GameObject>("drake_frostspit_attack_dod");
			DrakespitPoison1 = DoDAssets.LoadAsset<GameObject>("drake_poison_attack_dod");
			DrakespitPoison2 = DoDAssets.LoadAsset<GameObject>("drake_poisonspit_attack_dod");
			DrakespitVoid = DoDAssets.LoadAsset<GameObject>("drake_voidspit_attack_dod");
			WandMountains = DoDAssets.LoadAsset<GameObject>("Wand_Mountain_DoD");
			MaceMistlands = DoDAssets.LoadAsset<GameObject>("Mace_Mistlands_DoD");
			MaceDeepNorth = DoDAssets.LoadAsset<GameObject>("Mace_DeepNorth_DoD");
			TestingA1 = DoDAssets.LoadAsset<GameObject>("Test_Dragon_Attack_DoD");
			TestingA2 = DoDAssets.LoadAsset<GameObject>("Test_Dragon_ClawAttack_DoD");
			TestingA3 = DoDAssets.LoadAsset<GameObject>("Test_HornDragon_Attack_DoD");
			Testing = DoDAssets.LoadAsset<GameObject>("TestingMob_DoD");

			Skugga = DoDAssets.LoadAsset<GameObject>("Skugga_DoD");
			Einherjar = DoDAssets.LoadAsset<GameObject>("Einherjar_DoD");

			TexFlaAnvil = DoDAssets.LoadAsset<Sprite>("FlaAnvil_Icon_DoD");
			TexFroAnvil = DoDAssets.LoadAsset<Sprite>("FroAnvil_Icon_DoD");
			TexFelAnvil = DoDAssets.LoadAsset<Sprite>("FelAnvil_Icon_DoD");
			RugBDeer = DoDAssets.LoadAsset<Sprite>("RugBD_Icon_DoD");
			RugDWolf = DoDAssets.LoadAsset<Sprite>("RugDW_Icon_DoD");
			RugFWolf = DoDAssets.LoadAsset<Sprite>("RugFW_Icon_DoD");

			CBait = DoDAssets.LoadAsset<GameObject>("CarnivorBait_DoD");
			HardLog = DoDAssets.LoadAsset<GameObject>("Hardwood_Log_DoD");
			HardLogHalf = DoDAssets.LoadAsset<GameObject>("Hardwood_LogHalf_DoD");
			PrefabManager.Instance.AddPrefab(HardLog);
			PrefabManager.Instance.AddPrefab(HardLogHalf);

			NPC_NomadAoE_Attack = DoDAssets.LoadAsset<GameObject>("NPC_NomadAoE_Attack_DoD");
			Farkas_FrostBite = DoDAssets.LoadAsset<GameObject>("Farkas_FrostBite_DoD");
			Farkas_Hamper_Attack = DoDAssets.LoadAsset<GameObject>("Farkas_Hamper_Attack_DoD");
			Farkas_Bleed = DoDAssets.LoadAsset<GameObject>("Farkas_Bleed_DoD");
			Farkas_Attack3 = DoDAssets.LoadAsset<GameObject>("Farkas_Attack3_DoD");
			Farkas_Attack2 = DoDAssets.LoadAsset<GameObject>("Farkas_Attack2_DoD");
			Farkas_Attack1 = DoDAssets.LoadAsset<GameObject>("Farkas_Attack1_DoD");
			Rambore_Attack = DoDAssets.LoadAsset<GameObject>("Rambore_Attack_DoD");
			Rambore_Gore = DoDAssets.LoadAsset<GameObject>("Rambore_Gore_DoD");
			SkirSandBurst_VoidAttack = DoDAssets.LoadAsset<GameObject>("SkirSandBurst_VoidAttack_DoD");
			SkirSandburst_VoidSum = DoDAssets.LoadAsset<GameObject>("SkirSandburst_VoidSum_DoD");
			SkirSandburst_FWSum = DoDAssets.LoadAsset<GameObject>("SkirSandburst_FWSum_DoD");
			SkirSandburst_Shield = DoDAssets.LoadAsset<GameObject>("SkirSandburst_Shield_DoD");
			SkirSandburst_Heal = DoDAssets.LoadAsset<GameObject>("SkirSandburst_Heal_DoD");
			SkirSandburst_Nova = DoDAssets.LoadAsset<GameObject>("SkirSandburst_Nova_DoD");
			SkirSandburst_FB_Attack = DoDAssets.LoadAsset<GameObject>("SkirSandburst_FB_Attack_DoD");
			imp_firebolt_attack = DoDAssets.LoadAsset<GameObject>("imp_firebolt_attack_dod");
			Bhygshan_AoE = DoDAssets.LoadAsset<GameObject>("Bhygshan_AoE_DoD");
			Bhygshan_Fireball = DoDAssets.LoadAsset<GameObject>("Bhygshan_Fireball_DoD");
			Bhygshan_FireBolt = DoDAssets.LoadAsset<GameObject>("Bhygshan_FireBolt_DoD");
			Bhygshan_SprayFrost = DoDAssets.LoadAsset<GameObject>("Bhygshan_SprayFrost_DoD");
			Bhygshan_Throw = DoDAssets.LoadAsset<GameObject>("Bhygshan_Throw_DoD");
			Bitterstump_Heal = DoDAssets.LoadAsset<GameObject>("Bitterstump_Heal_DoD");
			Bitterstump_Melee = DoDAssets.LoadAsset<GameObject>("Bitterstump_Melee_DoD");
			Bitterstump_Roots = DoDAssets.LoadAsset<GameObject>("Bitterstump_Roots_DoD");
			Bitterstump_SprayFrost = DoDAssets.LoadAsset<GameObject>("Bitterstump_SprayFrost_DoD");
			Bitterstump_SprayPoison = DoDAssets.LoadAsset<GameObject>("Bitterstump_SprayPoison_DoD");
			Mage_FireStrike_Attack = DoDAssets.LoadAsset<GameObject>("Mage_FireStrike_Attack_DoD");
			ForestWolf_Attack1 = DoDAssets.LoadAsset<GameObject>("ForestWolf_Attack1_DoD");
			ForestWolf_Attack2 = DoDAssets.LoadAsset<GameObject>("ForestWolf_Attack2_DoD");
			ForestWolf_Attack3 = DoDAssets.LoadAsset<GameObject>("ForestWolf_Attack3_DoD");
			DireWolf_Attack1 = DoDAssets.LoadAsset<GameObject>("DireWolf_Attack1_DoD");
			DireWolf_Attack2 = DoDAssets.LoadAsset<GameObject>("DireWolf_Attack2_DoD");
			DireWolf_Attack3 = DoDAssets.LoadAsset<GameObject>("DireWolf_Attack3_DoD");
			Vilefang_Attack1 = DoDAssets.LoadAsset<GameObject>("Vilefang_Attack1_DoD");
			Vilefang_Attack2 = DoDAssets.LoadAsset<GameObject>("Vilefang_Attack2_DoD");
			Vilefang_Attack3 = DoDAssets.LoadAsset<GameObject>("Vilefang_Attack3_DoD");
			livinglava_nova_attack = DoDAssets.LoadAsset<GameObject>("livinglava_nova_attack_dod");
			livingwater_nova_attack = DoDAssets.LoadAsset<GameObject>("livingwater_nova_attack_dod");
			imp_icebolt_attack = DoDAssets.LoadAsset<GameObject>("imp_icebolt_attack_dod");
			imp_stormbolt_attack = DoDAssets.LoadAsset<GameObject>("imp_stormbolt_attack_dod");
			imp_voidbolt_attack = DoDAssets.LoadAsset<GameObject>("imp_voidbolt_attack_dod");
			// Monster Items
			ObsidianGolemClubs = DoDAssets.LoadAsset<GameObject>("ObsidianGolem_Clubs_DoD");
			HelmetBhygshan = DoDAssets.LoadAsset<GameObject>("HelmetBhygshan_DoD");
			ObsidianGolemSpikes = DoDAssets.LoadAsset<GameObject>("ObsidianGolem_Spikes_DoD");
			ObsidianGolemHead = DoDAssets.LoadAsset<GameObject>("ObsidianGolem_Head_DoD");
			LavaGolemClubs = DoDAssets.LoadAsset<GameObject>("LavaGolem_Clubs_DoD");
			LavaGolemSpikes = DoDAssets.LoadAsset<GameObject>("LavaGolem_Spikes_DoD");
			LavaGolemHead = DoDAssets.LoadAsset<GameObject>("LavaGolem_Head_DoD");
			IceGolemClubs = DoDAssets.LoadAsset<GameObject>("IceGolem_Clubs_DoD");
			IceGolemSpikes = DoDAssets.LoadAsset<GameObject>("IceGolem_Spikes_DoD");
			IceGolemHead = DoDAssets.LoadAsset<GameObject>("IceGolem_Head_DoD");
			SkirSandburstStaff = DoDAssets.LoadAsset<GameObject>("SkirSandburst_Staff_DoD");
			SkirSandburstHeaddress = DoDAssets.LoadAsset<GameObject>("SkirSandburst_Headdress_DoD");
			SwordFrozenBones = DoDAssets.LoadAsset<GameObject>("Sword_FrozenBones_DoD");
			BowFrozenBones = DoDAssets.LoadAsset<GameObject>("Bow_FrozenBones_DoD");
			SwordSkelR = DoDAssets.LoadAsset<GameObject>("Sword_SkelR_DoD");
			BowSkelR = DoDAssets.LoadAsset<GameObject>("Bow_SkelR_DoD");
			SwordSkelG = DoDAssets.LoadAsset<GameObject>("Sword_SkelG_DoD");
			BowSkelG = DoDAssets.LoadAsset<GameObject>("Bow_SkelG_DoD");
			ShieldCharred = DoDAssets.LoadAsset<GameObject>("Shield_Charred_DoD");
			SwordCharred = DoDAssets.LoadAsset<GameObject>("Sword_Charred_DoD");
			BowCharred = DoDAssets.LoadAsset<GameObject>("Bow_Charred_DoD");
			// Broken Shields
			ShieldBGSkull = DoDAssets.LoadAsset<GameObject>("BrokenShieldBhygshan_DoD");
			ShieldBEikthyr = DoDAssets.LoadAsset<GameObject>("BrokenShieldEikthyr_DoD");
			ShieldBRambore = DoDAssets.LoadAsset<GameObject>("BrokenShieldRambore_DoD");
			ShieldBElder = DoDAssets.LoadAsset<GameObject>("BrokenShieldElder_DoD");
			ShieldBBitter = DoDAssets.LoadAsset<GameObject>("BrokenShieldBitterstump_DoD");
			ShieldBBonemass = DoDAssets.LoadAsset<GameObject>("BrokenShieldBonemass_DoD");
			ShieldBModer = DoDAssets.LoadAsset<GameObject>("BrokenShieldModer_DoD");
			ShieldBFarkas = DoDAssets.LoadAsset<GameObject>("BrokenShieldFarkas_DoD");
			ShieldBSkir = DoDAssets.LoadAsset<GameObject>("BrokenShieldSkir_DoD");
			ShieldBYagluth = DoDAssets.LoadAsset<GameObject>("BrokenShieldYagluth_DoD");
			// Shields
			ShieldGSkull = DoDAssets.LoadAsset<GameObject>("ShieldSkullGreen_DoD");
			ShieldEikthyr = DoDAssets.LoadAsset<GameObject>("ShieldEikthyr_DoD");
			ShieldRambore = DoDAssets.LoadAsset<GameObject>("ShieldRambore_DoD");
			ShieldElder = DoDAssets.LoadAsset<GameObject>("ShieldElder_DoD");
			ShieldBitter = DoDAssets.LoadAsset<GameObject>("ShieldBitterstump_DoD");
			ShieldBonemass = DoDAssets.LoadAsset<GameObject>("ShieldBonemass_DoD");
			ShieldModer = DoDAssets.LoadAsset<GameObject>("ShieldModer_DoD");
			ShieldFarkas = DoDAssets.LoadAsset<GameObject>("ShieldFarkas_DoD");
			ShieldSkir = DoDAssets.LoadAsset<GameObject>("ShieldSkir_DoD");
			ShieldYagluth = DoDAssets.LoadAsset<GameObject>("ShieldYagluth_DoD");
			// Weapons
			BhygshanMace = DoDAssets.LoadAsset<GameObject>("BhygshanMace_DoD");
			BowBlackForest = DoDAssets.LoadAsset<GameObject>("Bow_BlackForest_DoD");
			BowSwamp = DoDAssets.LoadAsset<GameObject>("Bow_Swamp_DoD");
			BowMountain = DoDAssets.LoadAsset<GameObject>("Bow_Mountain_DoD");
			BowPlains = DoDAssets.LoadAsset<GameObject>("Bow_Plains_DoD");
			BowMistlands = DoDAssets.LoadAsset<GameObject>("Bow_Mistlands_DoD");
			BowDeepNorth = DoDAssets.LoadAsset<GameObject>("Bow_DeepNorth_DoD");
			SwordMeadows = DoDAssets.LoadAsset<GameObject>("Sword_Meadows_DoD");
			SwordSwamp = DoDAssets.LoadAsset<GameObject>("Sword_Swamp_DoD");
			SwordPlains = DoDAssets.LoadAsset<GameObject>("Sword_Plains_DoD");
			SwordMistlands = DoDAssets.LoadAsset<GameObject>("Sword_Mistlands_DoD");
			SwordDeepNorth = DoDAssets.LoadAsset<GameObject>("Sword_DeepNorth_DoD");
			SwordAshLands = DoDAssets.LoadAsset<GameObject>("Sword_AshLands_DoD");
			// Status Effect Assets
			HoT = DoDAssets.LoadAsset<Sprite>("HoT_Icon_DoD.png");
			Regen = DoDAssets.LoadAsset<Sprite>("Regeneration_Icon_DoD.png");
			MagicShield = DoDAssets.LoadAsset<Sprite>("MagicShield_Icon_DoD.png");
			Horror = DoDAssets.LoadAsset<Sprite>("Horrors_Icon_DoD.png");
			Haunted = DoDAssets.LoadAsset<Sprite>("Haunted_Icon_DoD.png");
			Hexed = DoDAssets.LoadAsset<Sprite>("Hexed_Icon_DoD.png");
			Infected = DoDAssets.LoadAsset<Sprite>("Infected_Icon_DoD.png");
			Diseased = DoDAssets.LoadAsset<Sprite>("Diseased_Icon_DoD.png");
			Weak = DoDAssets.LoadAsset<Sprite>("Weak_Icon_DoD.png");
			FlashBurn = DoDAssets.LoadAsset<Sprite>("FlashBurn_Icon_DoD.png");
			Blistered = DoDAssets.LoadAsset<Sprite>("Blistered_Icon_DoD.png");
			Paralyze = DoDAssets.LoadAsset<Sprite>("Paralyze_Icon_DoD.png");
			Slow = DoDAssets.LoadAsset<Sprite>("Slow_Icon_DoD.png");
			MinorInjury = DoDAssets.LoadAsset<Sprite>("MinorInjury_Icon_DoD.png");
			Injury = DoDAssets.LoadAsset<Sprite>("Injured_Icon_DoD.png");
			MajorInjury = DoDAssets.LoadAsset<Sprite>("MajorInjury_Icon_DoD.png");
			Frostbite = DoDAssets.LoadAsset<Sprite>("MinorInjury_Icon_DoD.png");
			Poisoned = DoDAssets.LoadAsset<Sprite>("Poisoned_Icon_DoD.png");
			InfectedFX = DoDAssets.LoadAsset<GameObject>("VFX_Infected_DoD");
			SlowFX = DoDAssets.LoadAsset<GameObject>("VFX_Slow_DoD");
			ParalyzeFX = DoDAssets.LoadAsset<GameObject>("VFX_Paralyze_DoD");
			InjuredFX = DoDAssets.LoadAsset<GameObject>("VFX_Injured_DoD");
			FrostbittenFX = DoDAssets.LoadAsset<GameObject>("VFX_Frostbite_DoD");
			PoisonedFX = DoDAssets.LoadAsset<GameObject>("VFX_Poisoned_DoD");
			// Sword Assets
			Firesoul = DoDAssets.LoadAsset<GameObject>("SwordFlametal1_DoD");
			Solarflare = DoDAssets.LoadAsset<GameObject>("SwordFlametal2_DoD");
			Reaper = DoDAssets.LoadAsset<GameObject>("SwordFelmetal1_DoD");
			Desolation = DoDAssets.LoadAsset<GameObject>("SwordFelmetal2_DoD");
			Soulstorm = DoDAssets.LoadAsset<GameObject>("Stormblade_DoD");
			Silverlight = DoDAssets.LoadAsset<GameObject>("SwordFrometal2_DoD");
			Coldflame = DoDAssets.LoadAsset<GameObject>("SwordFrometal1_DoD");
			Frostflame = DoDAssets.LoadAsset<GameObject>("SwordFrometal3_DoD");
			Nethersbane = DoDAssets.LoadAsset<GameObject>("SwordSpirit_DoD");
			// Class Weapon Assets
			WandLightning = DoDAssets.LoadAsset<GameObject>("ShamanWand_DoD");
			WandFire = DoDAssets.LoadAsset<GameObject>("MageWand_DoD");
			WandShadow = DoDAssets.LoadAsset<GameObject>("WarlockWand_DoD");
			MaceDivine = DoDAssets.LoadAsset<GameObject>("PaladinMace_DoD");
			SwordAssassin = DoDAssets.LoadAsset<GameObject>("RogueSword_DoD");
			AxeKnight = DoDAssets.LoadAsset<GameObject>("DeathknightAxe_DoD");
			SwordVoid = DoDAssets.LoadAsset<GameObject>("NinjaSword_DoD");
			SpearAcid = DoDAssets.LoadAsset<GameObject>("DruidSpear_DoD");
			AxeBattle = DoDAssets.LoadAsset<GameObject>("BerserkerAxe_DoD");
			MaceShock = DoDAssets.LoadAsset<GameObject>("MonkMace_DoD");
			SpearAcid = DoDAssets.LoadAsset<GameObject>("DruidSpear_DoD");
			SpearAoE = DoDAssets.LoadAsset<GameObject>("AoE_AuraHealing_DoD");
			// Buildable Assets
			RugDeer = DoDAssets.LoadAsset<GameObject>("Rug_BlackDeer_DoD");
			RugDire = DoDAssets.LoadAsset<GameObject>("Rug_DireWolf_DoD");
			RugForest = DoDAssets.LoadAsset<GameObject>("Rug_ForestWolf_DoD");
			AnvilsFel = DoDAssets.LoadAsset<GameObject>("FelmetalAnvils_DoD");
			AnvilsFro = DoDAssets.LoadAsset<GameObject>("FrometalAnvils_DoD");
			AnvilsFlam = DoDAssets.LoadAsset<GameObject>("FlametalAnvils_DoD");
			// Armor Kit Assets
			T1ArmorKit = DoDAssets.LoadAsset<GameObject>("CrudeArmorKit_DoD");
			T2ArmorKit = DoDAssets.LoadAsset<GameObject>("BasicArmorKit_DoD");
			T3ArmorKit = DoDAssets.LoadAsset<GameObject>("GoodArmorKit_DoD");
			T4ArmorKit = DoDAssets.LoadAsset<GameObject>("GreatArmorKit_DoD");
			T5ArmorKit = DoDAssets.LoadAsset<GameObject>("SuperiorArmorKit_DoD");
			T6ArmorKit = DoDAssets.LoadAsset<GameObject>("ExcellentArmorKit_DoD");
			T7ArmorKit = DoDAssets.LoadAsset<GameObject>("ExceptionalArmorKit_DoD");
			T8ArmorKit = DoDAssets.LoadAsset<GameObject>("ExtraordinaryArmorKit_DoD");
			// Weapon Kit Assets
			T1WeaponKit = DoDAssets.LoadAsset<GameObject>("CrudeWeaponKit_DoD");
			T2WeaponKit = DoDAssets.LoadAsset<GameObject>("BasicWeaponKit_DoD");
			T3WeaponKit = DoDAssets.LoadAsset<GameObject>("GoodWeaponKit_DoD");
			T4WeaponKit = DoDAssets.LoadAsset<GameObject>("GreatWeaponKit_DoD");
			T5WeaponKit = DoDAssets.LoadAsset<GameObject>("SuperiorWeaponKit_DoD");
			T6WeaponKit = DoDAssets.LoadAsset<GameObject>("ExcellentWeaponKit_DoD");
			T7WeaponKit = DoDAssets.LoadAsset<GameObject>("ExceptionalWeaponKit_DoD");
			T8WeaponKit = DoDAssets.LoadAsset<GameObject>("ExtraordinaryWeaponKit_DoD");
			// Trophy Assets
			TrophyCharredRemains = DoDAssets.LoadAsset<GameObject>("TrophyCharredRemains_DoD");
			TrophyFrozenBones = DoDAssets.LoadAsset<GameObject>("TrophyFrozenBones_DoD");
			TrophySurtling = DoDAssets.LoadAsset<GameObject>("TrophyGreatSurtling_DoD");
			TrophySkeletonG = DoDAssets.LoadAsset<GameObject>("TrophySkeletonG_DoD");
			TrophySkeletonR = DoDAssets.LoadAsset<GameObject>("TrophySkeletonR_DoD");
			TrophyFrostling = DoDAssets.LoadAsset<GameObject>("TrophyFrostling_DoD");
			TrophyStormling = DoDAssets.LoadAsset<GameObject>("TrophyStormling_DoD");
			TrophyVoidling = DoDAssets.LoadAsset<GameObject>("TrophyVoidling_DoD");
			TrophyOGolem = DoDAssets.LoadAsset<GameObject>("TrophyOGolem_DoD");
			TrophyLGolem = DoDAssets.LoadAsset<GameObject>("TrophyLGolem_DoD");
			TrophyIceGolem = DoDAssets.LoadAsset<GameObject>("TrophyIceGolem_DoD");
			TrophyVilefang = DoDAssets.LoadAsset<GameObject>("TrophyVilefang_DoD");
			TrophyDireWolf = DoDAssets.LoadAsset<GameObject>("TrophyDireWolf_DoD");
			TrophyForestWolf = DoDAssets.LoadAsset<GameObject>("TrophyForestWolf_DoD");
			TrophyLivingLava = DoDAssets.LoadAsset<GameObject>("TrophyLivingLava_DoD");
			TrophyLivingWater = DoDAssets.LoadAsset<GameObject>("TrophyLivingWater_DoD");
			TrophyBlackDeer = DoDAssets.LoadAsset<GameObject>("TrophyBlackDeer_DoD");
			TrophyIceDrake = DoDAssets.LoadAsset<GameObject>("TrophyIceDrake_DoD");
			TrophyFlameDrake = DoDAssets.LoadAsset<GameObject>("TrophyFlameDrake_DoD");
			TrophyArcaneDrake = DoDAssets.LoadAsset<GameObject>("TrophyArcaneDrake_DoD");
			TrophyDarknessDrake = DoDAssets.LoadAsset<GameObject>("TrophyDarknessDrake_DoD");
			TrophyGoldDrake = DoDAssets.LoadAsset<GameObject>("TrophyGoldDrake_DoD");
			TrophyPoisonDrake = DoDAssets.LoadAsset<GameObject>("TrophyPoisonDrake_DoD");
			TrophyDarkDrake = DoDAssets.LoadAsset<GameObject>("TrophyDarkDrake_DoD");
			// Material Assets
			Walnuts = DoDAssets.LoadAsset<GameObject>("Walnuts_DoD");
			Apple = DoDAssets.LoadAsset<GameObject>("Apple_DoD");
			Cherry = DoDAssets.LoadAsset<GameObject>("Cherries_DoD");
			Banana = DoDAssets.LoadAsset<GameObject>("Banana_DoD");
			InfusedGemstone = DoDAssets.LoadAsset<GameObject>("InfusedGemstone_DoD");
			DeathKnightItem = DoDAssets.LoadAsset<GameObject>("DeathKnightItem_DoD");
			ArcherItem = DoDAssets.LoadAsset<GameObject>("ArcherItem_DoD");
			BerserkerItem = DoDAssets.LoadAsset<GameObject>("BerserkerItem_DoD");
			DruidItem = DoDAssets.LoadAsset<GameObject>("DruidItem_DoD");
			MageItem = DoDAssets.LoadAsset<GameObject>("MageItem_DoD");
			MonkItem = DoDAssets.LoadAsset<GameObject>("MonkItem_DoD");
			NinjaItem = DoDAssets.LoadAsset<GameObject>("NinjaItem_DoD");
			PaladinItem = DoDAssets.LoadAsset<GameObject>("PaladinItem_DoD");
			RogueItem = DoDAssets.LoadAsset<GameObject>("RogueItem_DoD");
			ShamanItem = DoDAssets.LoadAsset<GameObject>("ShamanItem_DoD");
			WarlockItem = DoDAssets.LoadAsset<GameObject>("WarlockItem_DoD");
			BoarTusk = DoDAssets.LoadAsset<GameObject>("BoarTusk_DoD");
			LargeFang = DoDAssets.LoadAsset<GameObject>("LargeFang_DoD");
			ShamansVessel = DoDAssets.LoadAsset<GameObject>("ShamansVessel_DoD");
			GreydwarfHeart = DoDAssets.LoadAsset<GameObject>("GreydwarfHeart_DoD");
			SkeletonBones = DoDAssets.LoadAsset<GameObject>("SkeletonBones_DoD");
			GreyPearl = DoDAssets.LoadAsset<GameObject>("GreyPearl_DoD");
			FrozenBone = DoDAssets.LoadAsset<GameObject>("FrozenBone_DoD");
			CharredBone = DoDAssets.LoadAsset<GameObject>("CharredBone_DoD");
			FrostlingCore = DoDAssets.LoadAsset<GameObject>("FrostlingCore_DoD");
			StormlingCore = DoDAssets.LoadAsset<GameObject>("StormlingCore_DoD");
			VoidlingCore = DoDAssets.LoadAsset<GameObject>("VoidlingCore_DoD");
			ForestWolfPelt = DoDAssets.LoadAsset<GameObject>("ForestWolfPelt_DoD");
			DireWolfPelt = DoDAssets.LoadAsset<GameObject>("DireWolfPelt_DoD");
			WaterGlobe = DoDAssets.LoadAsset<GameObject>("WaterGlobe_DoD");
			SpiderChitin = DoDAssets.LoadAsset<GameObject>("SpiderChitin_DoD");
			BlackDeerHide = DoDAssets.LoadAsset<GameObject>("BlackDeerHide_DoD");
			OakWood = DoDAssets.LoadAsset<GameObject>("OakWood_DoD");
			// Monster Assets
			//Bosses
			//Rylan = DoDAssets.LoadAsset<GameObject>("LaughingRylan_DoD");
			SkirSandburst = DoDAssets.LoadAsset<GameObject>("SkirSandburst_DoD");
			Farkas = DoDAssets.LoadAsset<GameObject>("Farkas_DoD");
			Bhygshan = DoDAssets.LoadAsset<GameObject>("Bhygshan_DoD");
			Bitterstump = DoDAssets.LoadAsset<GameObject>("Bitterstump_DoD");
			Rambore = DoDAssets.LoadAsset<GameObject>("Rambore_DoD");
			// Monsters
			GreaterSurtling = DoDAssets.LoadAsset<GameObject>("GreaterSurtling_DoD");
			CharredRemains = DoDAssets.LoadAsset<GameObject>("CharredRemains_DoD");
			SkeletonG = DoDAssets.LoadAsset<GameObject>("SkeletonG_DoD");
			SkeletonR = DoDAssets.LoadAsset<GameObject>("SkeletonR_DoD");
			FrozenBones = DoDAssets.LoadAsset<GameObject>("FrozenBones_DoD");
			BlackDeer = DoDAssets.LoadAsset<GameObject>("BlackDeer_DoD");
			BlackStag = DoDAssets.LoadAsset<GameObject>("BlackStag_DoD");
			IceGolem = DoDAssets.LoadAsset<GameObject>("IceGolem_DoD");
			LavaGolem = DoDAssets.LoadAsset<GameObject>("LavaGolem_DoD");
			ObsidianGolem = DoDAssets.LoadAsset<GameObject>("ObsidianGolem_DoD");
			GhostWhite = DoDAssets.LoadAsset<GameObject>("Ghost_White_DoD");
			GhostIce = DoDAssets.LoadAsset<GameObject>("Ghost_Ice_DoD");
			Frostling = DoDAssets.LoadAsset<GameObject>("Frostling_DoD");
			Stormling = DoDAssets.LoadAsset<GameObject>("Stormling_DoD");
			Voidling = DoDAssets.LoadAsset<GameObject>("Voidling_DoD");
			ForestWolf = DoDAssets.LoadAsset<GameObject>("ForestWolf_DoD");
			ForestWolfCub = DoDAssets.LoadAsset<GameObject>("ForestWolf_Cub_DoD");
			DireWolf = DoDAssets.LoadAsset<GameObject>("DireWolf_DoD");
			DireWolfCub = DoDAssets.LoadAsset<GameObject>("DireWolf_Cub_DoD");
			Vilefang = DoDAssets.LoadAsset<GameObject>("Vilefang_DoD");
			VilefangCub = DoDAssets.LoadAsset<GameObject>("Vilefang_Cub_DoD");
			LivingLava = DoDAssets.LoadAsset<GameObject>("LivingLava_DoD");
			LivingWater = DoDAssets.LoadAsset<GameObject>("LivingWater_DoD");
			IceDrake = DoDAssets.LoadAsset<GameObject>("IceDrake_DoD");
			FlameDrake = DoDAssets.LoadAsset<GameObject>("FlameDrake_DoD");
			ArcaneDrake = DoDAssets.LoadAsset<GameObject>("ArcaneDrake_DoD");
			GoldDrake = DoDAssets.LoadAsset<GameObject>("GoldDrake_DoD");
			DarkDrake = DoDAssets.LoadAsset<GameObject>("DarknessDrake_DoD");
			PoisonDrake = DoDAssets.LoadAsset<GameObject>("PoisonDrake_DoD");
			BlackDrake = DoDAssets.LoadAsset<GameObject>("DarkDrake_DoD");
			GreenFrog = DoDAssets.LoadAsset<GameObject>("GreenFrog_DoD");
			BlackFrog = DoDAssets.LoadAsset<GameObject>("BlackFrog_DoD");
			SpottedFrog = DoDAssets.LoadAsset<GameObject>("SpottedFrog_DoD");
			GreyRabbit = DoDAssets.LoadAsset<GameObject>("GreyRabbit_DoD");
			BrownRabbit = DoDAssets.LoadAsset<GameObject>("BrownRabbit_DoD");
			GiantSnail = DoDAssets.LoadAsset<GameObject>("GiantSnail_DoD");

			GameObject AoESpray = DoDAssets.LoadAsset<GameObject>("AoE_Spray_DoD");
			GameObject BitterRoots = DoDAssets.LoadAsset<GameObject>("Bitter_RootSpawn_DoD");
			GameObject AoESkirNova = DoDAssets.LoadAsset<GameObject>("AoE_Skir_Nova_DoD");
			GameObject AoEBitterstumpHeal = DoDAssets.LoadAsset<GameObject>("AoE_Bitterstump_Heal_DoD");
			GameObject AoEInfected = DoDAssets.LoadAsset<GameObject>("AoE_Infected_DoD");
			GameObject AoEDiseased = DoDAssets.LoadAsset<GameObject>("AoE_Diseased_DoD");
			GameObject AoEWeak = DoDAssets.LoadAsset<GameObject>("AoE_Weak_DoD");
			GameObject AoEBleed25 = DoDAssets.LoadAsset<GameObject>("AoE_Bleed25_DoD");
			GameObject AoEBleed50 = DoDAssets.LoadAsset<GameObject>("AoE_Bleed50_DoD");
			GameObject AoEBleed100 = DoDAssets.LoadAsset<GameObject>("AoE_Bleed100_DoD");
			GameObject AoEHoT50 = DoDAssets.LoadAsset<GameObject>("AoE_HoT50_DoD");
			GameObject AoEHoT100 = DoDAssets.LoadAsset<GameObject>("AoE_HoT100_DoD");
			GameObject AoEHoT200 = DoDAssets.LoadAsset<GameObject>("AoE_HoT200_DoD");
			GameObject AoERegen50 = DoDAssets.LoadAsset<GameObject>("AoE_Regen50_DoD");
			GameObject AoERegen100 = DoDAssets.LoadAsset<GameObject>("AoE_Regen100_DoD");
			GameObject AoERegen200 = DoDAssets.LoadAsset<GameObject>("AoE_Regen200_DoD");
			GameObject AoEProtection250 = DoDAssets.LoadAsset<GameObject>("AoE_Protection250_DoD");
			GameObject AoEProtection500 = DoDAssets.LoadAsset<GameObject>("AoE_Protection500_DoD");
			GameObject AoEProtection1000 = DoDAssets.LoadAsset<GameObject>("AoE_Protection1000_DoD");
			GameObject AoEFireStrike = DoDAssets.LoadAsset<GameObject>("Mage_FireStrike_AoE");
			GameObject AoEPaladinMace = DoDAssets.LoadAsset<GameObject>("AoE_DivineMace_DoD");
			GameObject AoERogueSword = DoDAssets.LoadAsset<GameObject>("AoE_RogueSword_DoD");
			GameObject AoENinjaSword = DoDAssets.LoadAsset<GameObject>("AoE_NinjaSword_DoD");
			GameObject AoENomad = DoDAssets.LoadAsset<GameObject>("AoE_Nomad_DoD");
			GameObject AoEAuraFrost = DoDAssets.LoadAsset<GameObject>("AoE_AuraFrost_DoD");
			GameObject AoEAuraShadow = DoDAssets.LoadAsset<GameObject>("AoE_AuraShadow_DoD");
			GameObject AoEAuraFire = DoDAssets.LoadAsset<GameObject>("AoE_AuraFire_DoD");
			GameObject AoEAuraStorm = DoDAssets.LoadAsset<GameObject>("AoE_AuraStorm_DoD");
			PrefabManager.Instance.AddPrefab(AoESpray);
			PrefabManager.Instance.AddPrefab(BitterRoots);
			PrefabManager.Instance.AddPrefab(AoESkirNova); 
			PrefabManager.Instance.AddPrefab(AoEBitterstumpHeal);
			PrefabManager.Instance.AddPrefab(AoEInfected);
			PrefabManager.Instance.AddPrefab(AoEDiseased);
			PrefabManager.Instance.AddPrefab(AoEWeak);
			PrefabManager.Instance.AddPrefab(AoEBleed25);
			PrefabManager.Instance.AddPrefab(AoEBleed50);
			PrefabManager.Instance.AddPrefab(AoEBleed100);
			PrefabManager.Instance.AddPrefab(AoEHoT50);
			PrefabManager.Instance.AddPrefab(AoEHoT100);
			PrefabManager.Instance.AddPrefab(AoEHoT200);
			PrefabManager.Instance.AddPrefab(AoERegen50);
			PrefabManager.Instance.AddPrefab(AoERegen100);
			PrefabManager.Instance.AddPrefab(AoERegen200);
			PrefabManager.Instance.AddPrefab(AoEProtection250);
			PrefabManager.Instance.AddPrefab(AoEProtection500);
			PrefabManager.Instance.AddPrefab(AoEProtection1000);
			PrefabManager.Instance.AddPrefab(AoEFireStrike);
			PrefabManager.Instance.AddPrefab(AoEPaladinMace);
			PrefabManager.Instance.AddPrefab(AoERogueSword);
			PrefabManager.Instance.AddPrefab(AoENinjaSword);
			PrefabManager.Instance.AddPrefab(AoENomad);
			PrefabManager.Instance.AddPrefab(AoEAuraFrost);
			PrefabManager.Instance.AddPrefab(AoEAuraShadow);
			PrefabManager.Instance.AddPrefab(AoEAuraFire);
			PrefabManager.Instance.AddPrefab(AoEAuraStorm);

			GameObject VilefangRD = DoDAssets.LoadAsset<GameObject>("Vilefang_Ragdoll_DoD");
			GameObject BlackDeerRD = DoDAssets.LoadAsset<GameObject>("BlackDeer_Ragdoll_DoD");
			GameObject ForestWolfRD = DoDAssets.LoadAsset<GameObject>("ForestWolf_Ragdoll_DoD");
			GameObject DireWolfRD = DoDAssets.LoadAsset<GameObject>("DireWolf_Ragdoll_DoD");
			GameObject ObsidianGolemRD = DoDAssets.LoadAsset<GameObject>("ObsidianGolem_Ragdoll_DoD");
			GameObject LavaGolemRD = DoDAssets.LoadAsset<GameObject>("LavaGolem_Ragdoll_DoD");
			GameObject IceGolemRD = DoDAssets.LoadAsset<GameObject>("IceGolem_Ragdoll_DoD");
			GameObject IceDrakeRD = DoDAssets.LoadAsset<GameObject>("IceDrake_Ragdoll_DoD");
			GameObject FlameDrakeRD = DoDAssets.LoadAsset<GameObject>("FlameDrake_Ragdoll_DoD");
			GameObject ArcaneDrakeRD = DoDAssets.LoadAsset<GameObject>("ArcaneDrake_Ragdoll_DoD");
			GameObject DarkDrakeRD = DoDAssets.LoadAsset<GameObject>("DarknessDrake_Ragdoll_DoD");
			GameObject GoldDrakeRD = DoDAssets.LoadAsset<GameObject>("GoldDrake_Ragdoll_DoD");
			GameObject GreenDrakeRD = DoDAssets.LoadAsset<GameObject>("PoisonDrake_Ragdoll_DoD");
			GameObject GreenFrogRD = DoDAssets.LoadAsset<GameObject>("GreenFrog_RD_DoD");
			GameObject BlackFrogRD = DoDAssets.LoadAsset<GameObject>("BlackFrog_RD_DoD");
			GameObject SpottedFrogRD = DoDAssets.LoadAsset<GameObject>("SpottedFrog_RD_DoD");
			GameObject GreyRabbitRD = DoDAssets.LoadAsset<GameObject>("GreyRabbit_RD_DoD");
			GameObject BrownRabbitRD = DoDAssets.LoadAsset<GameObject>("BrownRabbit_RD_DoD");
			GameObject GiantSnailRD = DoDAssets.LoadAsset<GameObject>("Snail_RD_DoD");
			GameObject SheepRD = DoDAssets.LoadAsset<GameObject>("Sheep_RD_DoD");
			GameObject GoatRD = DoDAssets.LoadAsset<GameObject>("Goat_RD_DoD");
			GameObject GooseRD = DoDAssets.LoadAsset<GameObject>("Goose_RD_DoD");
			GameObject PenguinRD = DoDAssets.LoadAsset<GameObject>("Penguin_RD_DoD");
			GameObject RatRD = DoDAssets.LoadAsset<GameObject>("Rat_RD_DoD");
			GameObject SalamanderRD = DoDAssets.LoadAsset<GameObject>("FireSalamander_RD_DoD");
			GameObject TurtleRD = DoDAssets.LoadAsset<GameObject>("BoxTurtle_RD_DoD");
			GameObject BrownLizardRD = DoDAssets.LoadAsset<GameObject>("BrownLizard_RD_DoD");
			GameObject GreenLizardRD = DoDAssets.LoadAsset<GameObject>("GreenLizard_RD_DoD");
			GameObject SpottedLizardRD = DoDAssets.LoadAsset<GameObject>("SpottedLizard_RD_DoD");
			GameObject FarkasRD = DoDAssets.LoadAsset<GameObject>("Farkas_RD_DoD");
			PrefabManager.Instance.AddPrefab(FarkasRD);
			PrefabManager.Instance.AddPrefab(VilefangRD);
			PrefabManager.Instance.AddPrefab(BlackDeerRD);
			PrefabManager.Instance.AddPrefab(ForestWolfRD);
			PrefabManager.Instance.AddPrefab(DireWolfRD);
			PrefabManager.Instance.AddPrefab(ObsidianGolemRD);
			PrefabManager.Instance.AddPrefab(LavaGolemRD);
			PrefabManager.Instance.AddPrefab(IceGolemRD);
			PrefabManager.Instance.AddPrefab(IceDrakeRD);
			PrefabManager.Instance.AddPrefab(FlameDrakeRD);
			PrefabManager.Instance.AddPrefab(ArcaneDrakeRD);
			PrefabManager.Instance.AddPrefab(DarkDrakeRD);
			PrefabManager.Instance.AddPrefab(GoldDrakeRD);
			PrefabManager.Instance.AddPrefab(GreenDrakeRD);
			PrefabManager.Instance.AddPrefab(GreenFrogRD);
			PrefabManager.Instance.AddPrefab(BlackFrogRD);
			PrefabManager.Instance.AddPrefab(SpottedFrogRD);
			PrefabManager.Instance.AddPrefab(GreyRabbitRD);
			PrefabManager.Instance.AddPrefab(BrownRabbitRD);
			PrefabManager.Instance.AddPrefab(GiantSnailRD);
			PrefabManager.Instance.AddPrefab(SheepRD);
			PrefabManager.Instance.AddPrefab(GoatRD);
			PrefabManager.Instance.AddPrefab(GooseRD);
			PrefabManager.Instance.AddPrefab(PenguinRD);
			PrefabManager.Instance.AddPrefab(RatRD);
			PrefabManager.Instance.AddPrefab(SalamanderRD);
			PrefabManager.Instance.AddPrefab(TurtleRD);
			PrefabManager.Instance.AddPrefab(BrownLizardRD);
			PrefabManager.Instance.AddPrefab(GreenLizardRD);
			PrefabManager.Instance.AddPrefab(SpottedLizardRD);

			GameObject BhygshanFireballProjectile = DoDAssets.LoadAsset<GameObject>("Bhygshan_Fireball_Projectile_DoD");
			GameObject SkirVoidboltProjectile = DoDAssets.LoadAsset<GameObject>("Skir_Voidbolt_Projectile_DoD");
			GameObject SkirSandburstVoidThrowProjectile = DoDAssets.LoadAsset<GameObject>("SkirSandburst_VoidThrow_Projectile_DoD");
			GameObject SkirSandburstFWThrowProjectile = DoDAssets.LoadAsset<GameObject>("SkirSandburst_FWThrow_Projectile_DoD");
			GameObject BhygshanThrowProjectile = DoDAssets.LoadAsset<GameObject>("Bhygshan_Throw_Projectile_DoD");
			GameObject BhygshanFBProjectile = DoDAssets.LoadAsset<GameObject>("Bhygshan_FB_Projectile_DoD");
			GameObject ImpFireboltProjectile = DoDAssets.LoadAsset<GameObject>("Imp_Firebolt_Projectile_DoD");
			GameObject ImpIceboltProjectile = DoDAssets.LoadAsset<GameObject>("Imp_Icebolt_projectile_dod");
			GameObject ImpstormboltProjectile = DoDAssets.LoadAsset<GameObject>("Imp_stormbolt_projectile_dod");
			GameObject ImpVoidboltProjectile = DoDAssets.LoadAsset<GameObject>("Imp_Voidbolt_projectile_dod");
			GameObject StormProjectileS = DoDAssets.LoadAsset<GameObject>("Wand_Storm_ProjectileS_DoD");
			GameObject StormProjectileL = DoDAssets.LoadAsset<GameObject>("Wand_Storm_ProjectileL_DoD");
			GameObject FireProjectileS = DoDAssets.LoadAsset<GameObject>("Wand_Fire_ProjectileS_DoD");
			GameObject FireProjectileL = DoDAssets.LoadAsset<GameObject>("Wand_Fire_ProjectileL_DoD");
			GameObject ShadowProjectileS = DoDAssets.LoadAsset<GameObject>("Wand_Shadow_ProjectileS_DoD");
			GameObject ShadowProjectileL = DoDAssets.LoadAsset<GameObject>("Wand_Shadow_ProjectileL_DoD");
			PrefabManager.Instance.AddPrefab(BhygshanFireballProjectile);
			PrefabManager.Instance.AddPrefab(SkirVoidboltProjectile);
			PrefabManager.Instance.AddPrefab(SkirSandburstVoidThrowProjectile);
			PrefabManager.Instance.AddPrefab(SkirSandburstFWThrowProjectile);
			PrefabManager.Instance.AddPrefab(BhygshanThrowProjectile);
			PrefabManager.Instance.AddPrefab(BhygshanFBProjectile);
			PrefabManager.Instance.AddPrefab(ImpFireboltProjectile);
			PrefabManager.Instance.AddPrefab(ImpIceboltProjectile);
			PrefabManager.Instance.AddPrefab(ImpstormboltProjectile);
			PrefabManager.Instance.AddPrefab(ImpVoidboltProjectile);
			PrefabManager.Instance.AddPrefab(StormProjectileS);
			PrefabManager.Instance.AddPrefab(StormProjectileL);
			PrefabManager.Instance.AddPrefab(FireProjectileS);
			PrefabManager.Instance.AddPrefab(FireProjectileL);
			PrefabManager.Instance.AddPrefab(ShadowProjectileS);
			PrefabManager.Instance.AddPrefab(ShadowProjectileL);

			GameObject TCMistlands = DoDAssets.LoadAsset<GameObject>("TreasureChest_Mistlands_DoD");
			GameObject TCDeepNorth = DoDAssets.LoadAsset<GameObject>("TreasureChest_DeepNorth_DoD");
			GameObject TCAshLands = DoDAssets.LoadAsset<GameObject>("TreasureChest_AshLands_DoD");
			GameObject AltarFarkas = DoDAssets.LoadAsset<GameObject>("AltarFarkas_DoD");
			GameObject AltarSkirSandburst = DoDAssets.LoadAsset<GameObject>("AltarSkirSandburst_DoD");
			GameObject AltarRambore = DoDAssets.LoadAsset<GameObject>("AltarRambone_DoD");
			GameObject VoidlingSummon = DoDAssets.LoadAsset<GameObject>("Voidling_Spawn_DoD");
			GameObject ForestWolfSummon = DoDAssets.LoadAsset<GameObject>("ForestWolf_Spawn_DoD");
			GameObject AltarBitterstump = DoDAssets.LoadAsset<GameObject>("AltarBitterstump_DoD");
			GameObject AltarBhygshan = DoDAssets.LoadAsset<GameObject>("AltarBhygshan_DoD");
			GameObject BhygshanSummon = DoDAssets.LoadAsset<GameObject>("Bhygshan_Spawn_DoD");
			GameObject AltarFarkasAlt = DoDAssets.LoadAsset<GameObject>("AltarFarkasAlt_DoD");
			PrefabManager.Instance.AddPrefab(TCMistlands);
			PrefabManager.Instance.AddPrefab(TCDeepNorth);
			PrefabManager.Instance.AddPrefab(TCAshLands);
			PrefabManager.Instance.AddPrefab(AltarFarkas);
			PrefabManager.Instance.AddPrefab(AltarSkirSandburst);
			PrefabManager.Instance.AddPrefab(AltarRambore);
			PrefabManager.Instance.AddPrefab(VoidlingSummon);
			PrefabManager.Instance.AddPrefab(ForestWolfSummon);
			PrefabManager.Instance.AddPrefab(AltarBitterstump);
			PrefabManager.Instance.AddPrefab(AltarBhygshan);
			PrefabManager.Instance.AddPrefab(BhygshanSummon);
			PrefabManager.Instance.AddPrefab(AltarFarkasAlt);

			GameObject CastleGate = DoDAssets.LoadAsset<GameObject>("CastleGate_DoD");
			GameObject CastleWall = DoDAssets.LoadAsset<GameObject>("CastleWall_DoD");
			GameObject CastleStairs = DoDAssets.LoadAsset<GameObject>("CastleWallStairs_DoD");
			GameObject CastleDetailed = DoDAssets.LoadAsset<GameObject>("CastleWall_Detailed_DoD");
			GameObject ArenaFog = DoDAssets.LoadAsset<GameObject>("VFX_ArenaFog_DoD");
			GameObject ArenaSpawner = DoDAssets.LoadAsset<GameObject>("Arena_Spawner_DoD");
			GameObject Leveler = DoDAssets.LoadAsset<GameObject>("LevelGround_DoD");
			GameObject Camp = DoDAssets.LoadAsset<GameObject>("Camp_DoD");
			GameObject CampSpawner = DoDAssets.LoadAsset<GameObject>("Camp_Spawner_DoD");
			GameObject EventSpawner = DoDAssets.LoadAsset<GameObject>("Event_Spawner_DoD");
			GameObject LocationSpawner = DoDAssets.LoadAsset<GameObject>("Location_Spawner_DoD");
			GameObject EventSpawner1 = DoDAssets.LoadAsset<GameObject>("Event_AreaSpawner_DN_DoD");
			GameObject EventSpawner2 = DoDAssets.LoadAsset<GameObject>("Event_AreaSpawner_Mist_DoD");
			GameObject EventSpawner3 = DoDAssets.LoadAsset<GameObject>("Event_AreaSpawner_Plains_DoD");
			GameObject LootSpawner1 = DoDAssets.LoadAsset<GameObject>("LootSpawner_Award_DN_DoD");
			GameObject LootSpawner2 = DoDAssets.LoadAsset<GameObject>("LootSpawner_Award_Mist_DoD");
			GameObject LootSpawner3 = DoDAssets.LoadAsset<GameObject>("LootSpawner_Award_Plains_DoD");
			GameObject SmallCave = DoDAssets.LoadAsset<GameObject>("MiniCave_DoD");
			GameObject Runestone = DoDAssets.LoadAsset<GameObject>("Runestone_DoD");
			GameObject VisirBitter = DoDAssets.LoadAsset<GameObject>("Vegvisir_Bitterstump_DoD");
			GameObject VisirRambone = DoDAssets.LoadAsset<GameObject>("Vegvisir_Rambore_DoD");
			GameObject CaveDeep = DoDAssets.LoadAsset<GameObject>("CaveDeep_DoD");
			GameObject CaveEnter = DoDAssets.LoadAsset<GameObject>("CaveEntrance_DoD");
			GameObject BeechGround = DoDAssets.LoadAsset<GameObject>("BeechGroundCover_DoD");
			PrefabManager.Instance.AddPrefab(CastleGate);
			PrefabManager.Instance.AddPrefab(CastleWall);
			PrefabManager.Instance.AddPrefab(CastleStairs);
			PrefabManager.Instance.AddPrefab(CastleDetailed);
			PrefabManager.Instance.AddPrefab(ArenaFog);
			PrefabManager.Instance.AddPrefab(ArenaSpawner);
			PrefabManager.Instance.AddPrefab(Leveler);
			PrefabManager.Instance.AddPrefab(Camp);
			PrefabManager.Instance.AddPrefab(CampSpawner);
			PrefabManager.Instance.AddPrefab(EventSpawner);
			PrefabManager.Instance.AddPrefab(LocationSpawner);
			PrefabManager.Instance.AddPrefab(EventSpawner1);
			PrefabManager.Instance.AddPrefab(EventSpawner2);
			PrefabManager.Instance.AddPrefab(EventSpawner3);
			PrefabManager.Instance.AddPrefab(LootSpawner1);
			PrefabManager.Instance.AddPrefab(LootSpawner2);
			PrefabManager.Instance.AddPrefab(LootSpawner3);
			PrefabManager.Instance.AddPrefab(SmallCave);
			PrefabManager.Instance.AddPrefab(Runestone);
			PrefabManager.Instance.AddPrefab(VisirBitter);
			PrefabManager.Instance.AddPrefab(VisirRambone);
			PrefabManager.Instance.AddPrefab(CaveDeep);
			PrefabManager.Instance.AddPrefab(CaveEnter);
			PrefabManager.Instance.AddPrefab(BeechGround);

			GameObject FXSkirProtect = DoDAssets.LoadAsset<GameObject>("FX_Skir_Protect_DoD");
			GameObject FXSkirNova = DoDAssets.LoadAsset<GameObject>("FX_Skir_Nova_DoD");
			GameObject FXBitterRoot = DoDAssets.LoadAsset<GameObject>("FX_Bitter_RootSpawn_DoD");
			GameObject FXBackstab = DoDAssets.LoadAsset<GameObject>("FX_Backstab_DoD");
			GameObject FXCrit = DoDAssets.LoadAsset<GameObject>("FX_Crit_DoD");
			GameObject FXMageCast = DoDAssets.LoadAsset<GameObject>("FX_Mage_Cast_DoD");
			GameObject FXBhygshanFireballExpl = DoDAssets.LoadAsset<GameObject>("FX_Bhygshan_Fireball_Expl_DoD");
			PrefabManager.Instance.AddPrefab(FXSkirProtect);
			PrefabManager.Instance.AddPrefab(FXSkirNova);
			PrefabManager.Instance.AddPrefab(FXBitterRoot);
			PrefabManager.Instance.AddPrefab(FXBackstab);
			PrefabManager.Instance.AddPrefab(FXCrit);
			PrefabManager.Instance.AddPrefab(FXMageCast);
			PrefabManager.Instance.AddPrefab(FXBhygshanFireballExpl);

			GameObject SFXLivingLavaDeath = DoDAssets.LoadAsset<GameObject>("SFX_LivingLava_Death_DoD");
			GameObject SFXLivingLavaHit = DoDAssets.LoadAsset<GameObject>("SFX_LivingLava_Hit_DoD");
			GameObject SFXLivingLavaJump = DoDAssets.LoadAsset<GameObject>("SFX_LivingLava_Jump_DoD");
			GameObject SFXFrostlingHit = DoDAssets.LoadAsset<GameObject>("SFX_Frostling_Hit_DoD");
			GameObject SFXFrostlingDeath = DoDAssets.LoadAsset<GameObject>("SFX_Frostling_Death_DoD");
			GameObject SFXFrostlingAttack = DoDAssets.LoadAsset<GameObject>("SFX_Frostling_Attack_DoD");
			GameObject SFXStormlingHit = DoDAssets.LoadAsset<GameObject>("SFX_Stormling_Hit_DoD");
			GameObject SFXStormlingDeath = DoDAssets.LoadAsset<GameObject>("SFX_Stormling_Death_DoD");
			GameObject SFXStormlingAttack = DoDAssets.LoadAsset<GameObject>("SFX_Stormling_Attack_DoD");
			GameObject SFXVoidlingHit = DoDAssets.LoadAsset<GameObject>("SFX_Voidling_Hit_DoD");
			GameObject SFXVoidlingDeath = DoDAssets.LoadAsset<GameObject>("SFX_Voidling_Death_DoD");
			GameObject SFXVoidlingAttack = DoDAssets.LoadAsset<GameObject>("SFX_Voidling_Attack_DoD");
			GameObject SFXGenericHit = DoDAssets.LoadAsset<GameObject>("SFX_GenericHit_DoD");
			GameObject SFXWandHit = DoDAssets.LoadAsset<GameObject>("SFX_DaggerHit_DoD");
			GameObject SFXWoodChop = DoDAssets.LoadAsset<GameObject>("SFX_Wood_Chop_DoD");
			GameObject SFXTreeFall = DoDAssets.LoadAsset<GameObject>("SFX_Tree_Falling_DoD");
			GameObject SFXTreeFallS = DoDAssets.LoadAsset<GameObject>("SFX_SmallTree_Falling_DoD");
			GameObject SFXBushChop = DoDAssets.LoadAsset<GameObject>("SFX_Bush_Chop_DoD");
			GameObject SFXRockHit = DoDAssets.LoadAsset<GameObject>("loc_sfx_rock_hit_dod");
			GameObject SFXRockDest = DoDAssets.LoadAsset<GameObject>("loc_sfx_rock_destroyed_dod");
			GameObject SFXPickable = DoDAssets.LoadAsset<GameObject>("SFX_Pickable_Pick_DoD");
			GameObject SFXOfferingBones = DoDAssets.LoadAsset<GameObject>("SFX_OfferingBones_DoD");
			GameObject SFXBossSpawn = DoDAssets.LoadAsset<GameObject>("SFX_BossSpawn_DoD");
			GameObject SFXSummoning = DoDAssets.LoadAsset<GameObject>("SFX_BossSummon_DoD");
			GameObject SFXFrog1 = DoDAssets.LoadAsset<GameObject>("SFX_Frog1_Idle_DoD");
			GameObject SFXFrog2 = DoDAssets.LoadAsset<GameObject>("SFX_Frog2_Idle_DoD");
			GameObject SFXFrogD = DoDAssets.LoadAsset<GameObject>("SFX_Frog_Death_DoD");
			GameObject SFXRabbitD = DoDAssets.LoadAsset<GameObject>("SFX_Rabbit_Death_DoD");
			GameObject SFXSnailD = DoDAssets.LoadAsset<GameObject>("SFX_Snail_Death_DoD");
			GameObject SFXGooseD = DoDAssets.LoadAsset<GameObject>("SFX_Goose_Death_DoD");
			GameObject SFXGooseI = DoDAssets.LoadAsset<GameObject>("SFX_Goose_Idle_DoD");
			GameObject SFXSheepD = DoDAssets.LoadAsset<GameObject>("SFX_Sheep_Death_DoD");
			GameObject SFXSheepI = DoDAssets.LoadAsset<GameObject>("SFX_Sheep_Idle_DoD");
			GameObject SFXSheepF = DoDAssets.LoadAsset<GameObject>("SFX_Sheep_Footstep_DoD");
			GameObject SFXGoatD = DoDAssets.LoadAsset<GameObject>("SFX_Goat_Death_DoD");
			GameObject SFXGoatI = DoDAssets.LoadAsset<GameObject>("SFX_Goat_Idle_DoD");
			GameObject SFXPenguinD = DoDAssets.LoadAsset<GameObject>("SFX_Penguin_Death_DoD");
			GameObject SFXSalamanderD = DoDAssets.LoadAsset<GameObject>("SFX_Salamander_Death_DoD");
			GameObject SFXRatD = DoDAssets.LoadAsset<GameObject>("SFX_Rat_Death_DoD");
			GameObject SFXLizardD = DoDAssets.LoadAsset<GameObject>("SFX_Lizard_Death_DoD");
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
			PrefabManager.Instance.AddPrefab(SFXLivingLavaDeath);
			PrefabManager.Instance.AddPrefab(SFXLivingLavaHit);
			PrefabManager.Instance.AddPrefab(SFXLivingLavaJump);
			PrefabManager.Instance.AddPrefab(SFXFrostlingHit);
			PrefabManager.Instance.AddPrefab(SFXFrostlingDeath);
			PrefabManager.Instance.AddPrefab(SFXFrostlingAttack);
			PrefabManager.Instance.AddPrefab(SFXStormlingHit);
			PrefabManager.Instance.AddPrefab(SFXStormlingDeath);
			PrefabManager.Instance.AddPrefab(SFXStormlingAttack);
			PrefabManager.Instance.AddPrefab(SFXVoidlingHit);
			PrefabManager.Instance.AddPrefab(SFXVoidlingDeath);
			PrefabManager.Instance.AddPrefab(SFXVoidlingAttack);
			PrefabManager.Instance.AddPrefab(SFXGenericHit);
			PrefabManager.Instance.AddPrefab(SFXWandHit);
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
			PrefabManager.Instance.AddPrefab(SFXFrog1);
			PrefabManager.Instance.AddPrefab(SFXFrog2);
			PrefabManager.Instance.AddPrefab(SFXFrogD);
			PrefabManager.Instance.AddPrefab(SFXRabbitD);
			PrefabManager.Instance.AddPrefab(SFXSnailD);

			GameObject VFXBhygshanSpray = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Spray_DoD");
			GameObject VFXBhygshanBreath = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Breath_DoD");
			GameObject VFXSkirThrow = DoDAssets.LoadAsset<GameObject>("VFX_Skir_Throw_DoD");
			GameObject VFXBhygshanAttack = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Attack_DoD");
			GameObject VFXSummonSpawn = DoDAssets.LoadAsset<GameObject>("VFX_SummonSpawn_DoD");
			GameObject VFXSkeletonHit = DoDAssets.LoadAsset<GameObject>("VFX_Skeleton_Hit_DoD");
			GameObject VFXOfferingBowl = DoDAssets.LoadAsset<GameObject>("VFX_OfferingBowl_DoD");
			GameObject VFXHit = DoDAssets.LoadAsset<GameObject>("VFX_Hit_DoD");
			GameObject VFXBloodHit = DoDAssets.LoadAsset<GameObject>("VFX_Blood_Hit_DoD");
			GameObject VFXDustPiece = DoDAssets.LoadAsset<GameObject>("VFX_Dust_Piece_DoD");
			GameObject VFXChestPlace = DoDAssets.LoadAsset<GameObject>("VFX_Chest_Place_DoD");
			GameObject VFXSummon = DoDAssets.LoadAsset<GameObject>("VFX_Summon_DoD");
			GameObject VFXFireBoltHit = DoDAssets.LoadAsset<GameObject>("VFX_FireBolt_SurtlingHit_DoD");
			GameObject VFXBiiterSpawn = DoDAssets.LoadAsset<GameObject>("VFX_BiiterSpawn_DoD");
			GameObject VFXBitterSpawnIn = DoDAssets.LoadAsset<GameObject>("VFX_BitterSpawnIn_DoD");
			GameObject VFXMageFS = DoDAssets.LoadAsset<GameObject>("VFX_Mage_FireStrike_DoD");
			GameObject VFXBlocked = DoDAssets.LoadAsset<GameObject>("VFX_Blocked_DoD");
			GameObject VFXHitSparks = DoDAssets.LoadAsset<GameObject>("VFX_HitSparks_DoD");
			GameObject VFXWolfDeath = DoDAssets.LoadAsset<GameObject>("VFX_Wolf_Death_DoD");
			GameObject VFXWolfHit = DoDAssets.LoadAsset<GameObject>("VFX_Wolf_Hit_DoD");
			GameObject VFXLivingLavaDeath = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Death_DoD");
			GameObject VFXLivingLavaAttack = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Attack_DoD");
			GameObject VFXLivingLavaHit = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Hit_DoD");
			GameObject VFXLivingWaterDeath = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Death_DoD");
			GameObject VFXLivingWaterAttack = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Attack_DoD");
			GameObject VFXLivingWaterHit = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Hit_DoD");
			GameObject VFXArcaneImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_ArcaneImpDeath_DoD");
			GameObject VFXIceImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_IceImpDeath_DoD");
			GameObject VFXStormImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_StormImpDeath_DoD");
			GameObject VFXIceImpHit = DoDAssets.LoadAsset<GameObject>("VFX_IceImpHit_DoD");
			GameObject VFXStormImpHit = DoDAssets.LoadAsset<GameObject>("VFX_StormImpHit_DoD");
			GameObject VFXVoidImpHit = DoDAssets.LoadAsset<GameObject>("VFX_VoidImpHit_DoD");
			GameObject VFXFelOreDestroy = DoDAssets.LoadAsset<GameObject>("VFX_Felore_Destroy_DoD");
			GameObject VFXMineHit = DoDAssets.LoadAsset<GameObject>("VFX_Mine_Hit_DoD");
			GameObject VFXPickable = DoDAssets.LoadAsset<GameObject>("VFX_Pickable_Pick_DoD");
			PrefabManager.Instance.AddPrefab(VFXBhygshanSpray);
			PrefabManager.Instance.AddPrefab(VFXBhygshanBreath);
			PrefabManager.Instance.AddPrefab(VFXSkirThrow);
			PrefabManager.Instance.AddPrefab(VFXBhygshanAttack);
			PrefabManager.Instance.AddPrefab(VFXSummonSpawn);
			PrefabManager.Instance.AddPrefab(VFXSkeletonHit);
			PrefabManager.Instance.AddPrefab(VFXOfferingBowl);
			PrefabManager.Instance.AddPrefab(VFXHit);
			PrefabManager.Instance.AddPrefab(VFXBloodHit);
			PrefabManager.Instance.AddPrefab(VFXDustPiece);
			PrefabManager.Instance.AddPrefab(VFXChestPlace);
			PrefabManager.Instance.AddPrefab(VFXSummon);
			PrefabManager.Instance.AddPrefab(VFXFireBoltHit);
			PrefabManager.Instance.AddPrefab(VFXBiiterSpawn);
			PrefabManager.Instance.AddPrefab(VFXBitterSpawnIn);
			PrefabManager.Instance.AddPrefab(VFXMageFS);
			PrefabManager.Instance.AddPrefab(VFXBlocked);
			PrefabManager.Instance.AddPrefab(VFXHitSparks);
			PrefabManager.Instance.AddPrefab(VFXWolfDeath);
			PrefabManager.Instance.AddPrefab(VFXWolfHit);
			PrefabManager.Instance.AddPrefab(VFXLivingLavaDeath);
			PrefabManager.Instance.AddPrefab(VFXLivingLavaAttack);
			PrefabManager.Instance.AddPrefab(VFXLivingLavaHit);
			PrefabManager.Instance.AddPrefab(VFXLivingWaterDeath);
			PrefabManager.Instance.AddPrefab(VFXLivingWaterAttack);
			PrefabManager.Instance.AddPrefab(VFXLivingWaterHit);
			PrefabManager.Instance.AddPrefab(VFXArcaneImpDeath);
			PrefabManager.Instance.AddPrefab(VFXIceImpDeath);
			PrefabManager.Instance.AddPrefab(VFXStormImpDeath);
			PrefabManager.Instance.AddPrefab(VFXIceImpHit);
			PrefabManager.Instance.AddPrefab(VFXStormImpHit);
			PrefabManager.Instance.AddPrefab(VFXVoidImpHit);
			PrefabManager.Instance.AddPrefab(VFXFelOreDestroy);
			PrefabManager.Instance.AddPrefab(VFXMineHit);
			PrefabManager.Instance.AddPrefab(VFXPickable);


			// mistlands veg
			MineRock_FelOre_DoD = DoDAssets.LoadAsset<GameObject>("MineRock_FelOre_DoD");
			PrefabManager.Instance.AddPrefab(MineRock_FelOre_DoD);
			BlueMushroom_DoD = DoDAssets.LoadAsset<GameObject>("BlueMushroom_DoD");
			PrefabManager.Instance.AddPrefab(BlueMushroom_DoD);
			PurpleMushroom_DoD = DoDAssets.LoadAsset<GameObject>("PurpleMushroom_DoD");
			PrefabManager.Instance.AddPrefab(PurpleMushroom_DoD);
			Tree_Willow02_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Willow02_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Willow02_DoD);
			Tree_Willow01_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Willow01_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Willow01_DoD);
			Tree_Poplar02_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Poplar02_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Poplar02_DoD);
			Tree_Poplar01_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Poplar01_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Poplar01_DoD);
			Bush_RedBerries_Pickable_DoD = DoDAssets.LoadAsset<GameObject>("Bush_RedBerries_Pickable_DoD");
			PrefabManager.Instance.AddPrefab(Bush_RedBerries_Pickable_DoD);
			Tree_OldOak02_DoD = DoDAssets.LoadAsset<GameObject>("Tree_OldOak02_DoD");
			PrefabManager.Instance.AddPrefab(Tree_OldOak02_DoD);
			Mineable_RockMS_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockMS_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockMS_DoD);
			Mineable_RockMM_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockMM_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockMM_DoD);
			Mineable_RockML_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockML_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockML_DoD);
			Mineable_RockMH_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockMH_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockMH_DoD);
			Tree_OldOak01_DoD = DoDAssets.LoadAsset<GameObject>("Tree_OldOak01_DoD");
			PrefabManager.Instance.AddPrefab(Tree_OldOak01_DoD);
			Tree_Oak02_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Oak02_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Oak02_DoD);
			Tree_Oak01_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Oak01_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Oak01_DoD);
			Bush_02_DoD = DoDAssets.LoadAsset<GameObject>("Bush_02_DoD");
			PrefabManager.Instance.AddPrefab(Bush_02_DoD);
			Bush_01_DoD = DoDAssets.LoadAsset<GameObject>("Bush_01_DoD");
			PrefabManager.Instance.AddPrefab(Bush_01_DoD);
			Mineable_RockMRFL_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockMRFL_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockMRFL_DoD);
			Mineable_RockMRFM_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockMRFM_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockMRFM_DoD);
			Flora_LargeBroad_DoD = DoDAssets.LoadAsset<GameObject>("Flora_LargeBroad_DoD");
			PrefabManager.Instance.AddPrefab(Flora_LargeBroad_DoD);
			Flora_SmallMulti_B_DoD = DoDAssets.LoadAsset<GameObject>("Flora_SmallMulti_B_DoD");
			PrefabManager.Instance.AddPrefab(Flora_SmallMulti_B_DoD);
			Flora_LargeSingle_DoD = DoDAssets.LoadAsset<GameObject>("Flora_LargeSingle_DoD");
			PrefabManager.Instance.AddPrefab(Flora_LargeSingle_DoD);
			Flora_MediumSingle_DoD = DoDAssets.LoadAsset<GameObject>("Flora_MediumSingle_DoD");
			PrefabManager.Instance.AddPrefab(Flora_MediumSingle_DoD);
			Flora_Large_DoD = DoDAssets.LoadAsset<GameObject>("Flora_Large_DoD");
			PrefabManager.Instance.AddPrefab(Flora_Large_DoD);
			Flora_LargeTrio_DoD = DoDAssets.LoadAsset<GameObject>("Flora_LargeTrio_DoD");
			PrefabManager.Instance.AddPrefab(Flora_LargeTrio_DoD);
			Flora_LargeDuo_DoD = DoDAssets.LoadAsset<GameObject>("Flora_LargeDuo_DoD");
			PrefabManager.Instance.AddPrefab(Flora_LargeDuo_DoD);
			Tree_Walnut_Pickable_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Walnut_Pickable_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Walnut_Pickable_DoD);


			// deep north
			MineRock_FroOre_DoD = DoDAssets.LoadAsset<GameObject>("MineRock_FroOre_DoD");
			PrefabManager.Instance.AddPrefab(MineRock_FroOre_DoD);
			Bush3_DeepNorth_DoD = DoDAssets.LoadAsset<GameObject>("Bush3_DeepNorth_DoD");
			PrefabManager.Instance.AddPrefab(Bush3_DeepNorth_DoD);
			Bush2_DeepNorth_DoD = DoDAssets.LoadAsset<GameObject>("Bush2_DeepNorth_DoD");
			PrefabManager.Instance.AddPrefab(Bush2_DeepNorth_DoD);
			Bush1_DeepNorth_DoD = DoDAssets.LoadAsset<GameObject>("Bush1_DeepNorth_DoD");
			PrefabManager.Instance.AddPrefab(Bush1_DeepNorth_DoD);
			Mineable_RockDN10_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN10_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN10_DoD);
			Mineable_RockDN9_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN9_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN9_DoD);
			Mineable_RockDN8_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN8_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN8_DoD);
			Mineable_RockDN7_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN7_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN7_DoD);
			Mineable_RockDN6_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN6_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN6_DoD);
			Mineable_RockDN5_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN5_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN5_DoD);
			Mineable_RockDN4_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN4_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN4_DoD);
			Mineable_RockDN3_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN3_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN3_DoD);
			Mineable_RockDN2_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN2_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN2_DoD);
			Mineable_RockDN1_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_RockDN1_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_RockDN1_DoD);
			WinterPine7_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine7_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine7_DoD); 
			WinterPine6_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine6_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine6_DoD);
			WinterPine5_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine5_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine5_DoD);
			WinterPine4_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine4_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine4_DoD);
			WinterPine3_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine3_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine3_DoD);
			WinterPine2_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine2_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine2_DoD);
			WinterPine1_DoD = DoDAssets.LoadAsset<GameObject>("WinterPine1_DoD");
			PrefabManager.Instance.AddPrefab(WinterPine1_DoD);

			// ashlands
			Mineable_SandRock16_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock16_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock16_DoD);
			Mineable_SandRock15_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock15_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock15_DoD);
			Mineable_SandRock14_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock14_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock14_DoD);
			Mineable_SandRock13_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock13_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock13_DoD);
			Mineable_SandRock12_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock12_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock12_DoD);
			Mineable_SandRock11_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock11_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock11_DoD);
			Mineable_SandRock10_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock10_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock10_DoD);
			Mineable_SandRock9_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock9_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock9_DoD);
			Mineable_SandRock8_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock8_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock8_DoD);
			Mineable_SandRock5_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock5_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock5_DoD);
			Mineable_SandRock4_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock4_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock4_DoD);
			Mineable_SandRock3_DoD = DoDAssets.LoadAsset<GameObject>("Mineable_SandRock3_DoD");
			PrefabManager.Instance.AddPrefab(Mineable_SandRock3_DoD);

			// fruit trees
			Tree_Banana_Pickable_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Banana_Pickable_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Banana_Pickable_DoD);
			Tree_Apple_Pickable_DoD = DoDAssets.LoadAsset<GameObject>("Tree_Apple_Pickable_DoD");
			PrefabManager.Instance.AddPrefab(Tree_Apple_Pickable_DoD);

		}

		public void CreateConfigurationValues()
		{
			MistlandsLocEnable = base.Config.Bind("Mistlands Locations", "Enable", defaultValue: true, new ConfigDescription("Enables Locations in Mistlands", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MistlandsVegEnable = base.Config.Bind("Mistlands Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Trees, Bushes, Pickables and Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			FruitEnable = base.Config.Bind("Custom Resources", "Enable", defaultValue: true, new ConfigDescription("Enables Felmetal Ore, Banana Tree and Apple Tree", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			ArmorCrateEnable = base.Config.Bind("Armor Kits", "Enable", defaultValue: true, new ConfigDescription("Enables Armor Crates, if you disable these you will have to disable bosses below", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			WeaponCrateEnable = base.Config.Bind("Weapon Kits", "Enable", defaultValue: true, new ConfigDescription("Enables Weapon Kits, if you disable these you will have to disable all the Weapons below", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			ClassWeaponEnable = base.Config.Bind("Weapons - Magic Overhaul Themed", "Enable", defaultValue: true, new ConfigDescription("Enables Magic Overhaul Themed Class Weapons, requires Weapon Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			WeaponsEnable = base.Config.Bind("Weapons", "Enable", defaultValue: true, new ConfigDescription("Enables Weapons, requires Weapon Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BuildablesEnable = base.Config.Bind("Buildables", "Enable", defaultValue: true, new ConfigDescription("Enables Anvils and Rugs", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BossesEnable = base.Config.Bind("Bosses", "Enable", defaultValue: true, new ConfigDescription("Enables Bosses and Shields, requires Armor Kits", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MonstersEnable = base.Config.Bind("Monster Reskins", "Enable", defaultValue: true, new ConfigDescription("Enables Monster Reskins", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			CastleArenaEnable = base.Config.Bind("Custom Locations", "Enable", defaultValue: true, new ConfigDescription("Enables Castle Arena, Event Ring and Camp Locations.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			DeepNorthVegEnable = base.Config.Bind("Deep North Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Deep North Trees, Bushes and Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			AshLandsVegEnable = base.Config.Bind("Ash Lands Vegetation", "Enable", defaultValue: true, new ConfigDescription("Enables Rocks.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			/*RamboreEnable = base.Config.Bind("RamBores Cave", "Enable", defaultValue: true, new ConfigDescription("Enables RamBore Location.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));*/
		}
		private void AddLocations()
		{
			DoDFixer = AssetUtils.LoadAssetBundleFromResources("fixer", Assembly.GetExecutingAssembly());
			DoDAssets = AssetUtils.LoadAssetBundleFromResources("doordieassets", Assembly.GetExecutingAssembly());
			try
			{
				if (BossesEnable.Value == true)
				{
					var Rambore = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_Boss_Rambore_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Rambore, new LocationConfig
					{
						Biome = Heightmap.Biome.Meadows,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
					var Bitterstump = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_Boss_Bitterstump_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(Bitterstump, new LocationConfig
					{
						Biome = Heightmap.Biome.BlackForest,
						Quantity = 4,
						Priotized = true,
						ExteriorRadius = 8f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
				}
				if (MistlandsVegEnable.Value == true)
                {				
					var MistLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_OreMine_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistLoc2, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 12f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
					var MistCave1 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_MistlandsCave_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistCave1, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 100,
						Priotized = true,
						ExteriorRadius = 16f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
					var MistLoc3 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_MistlandsTower_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(MistLoc3, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 200,
						Priotized = true,
						ExteriorRadius = 18f,
						MinAltitude = 5f,
						ClearArea = true,
					}));
                }
				if (CastleArenaEnable.Value == true)
				{
					var AnyLoc1 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_CastleArena_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc1, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 10,
						Priotized = true,
						ExteriorRadius = 20f,
						MinAltitude = 10f,
						ClearArea = true,
					}));
					var AnyLoc2 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Loc_Camp_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc2, new LocationConfig
					{
						Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Mountain, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands),
						Quantity = 300,
						Priotized = true,
						ExteriorRadius = 6f,
						MinAltitude = 2f,
						ClearArea = true,
					}));
					var AnyLoc3 = ZoneManager.Instance.CreateLocationContainer(DoDAssets.LoadAsset<GameObject>("Event_StoneRing_Mistlands_DoD"), false);
					ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc3, new LocationConfig
					{
						Biome = Heightmap.Biome.Mistlands,
						Quantity = 50,
						Priotized = true,
						ExteriorRadius = 20f,
						MinAltitude = 10f,
						ClearArea = true,						
					}));
				}
			}
			finally
			{
				ZoneManager.OnVanillaLocationsAvailable -= AddLocations;
			}
			DoDAssets.Unload(false);
		}
		private void AddCustomFruitTrees()
        {
			
			CustomVegetation customBananaTree1 = new CustomVegetation(Tree_Banana_Pickable_DoD, new VegetationConfig
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
			ZoneManager.Instance.AddCustomVegetation(customBananaTree1);

			CustomVegetation customVegetation26 = new CustomVegetation(Tree_Apple_Pickable_DoD, new VegetationConfig
			{
				Max = 1f,
				GroupSizeMin = 3,
				GroupSizeMax = 3,
				GroupRadius = 10f,
				BlockCheck = true,
				Biome = Heightmap.Biome.BlackForest,
				MinAltitude = 50f,
				MaxAltitude = 500f,
				MaxTilt = 20f
			});
			ZoneManager.Instance.AddCustomVegetation(customVegetation26);
						
		}
		private void AddMistlandVegetation()
		{
			
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
		private void CreateMonsterItems()
		{
			GameObject monsteritem1 = ObsidianGolemClubs;
			CustomItem customItem1 = new CustomItem(monsteritem1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject monsteritem2 = HelmetBhygshan;
			CustomItem customItem2 = new CustomItem(monsteritem2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject monsteritem3 = ObsidianGolemSpikes;
			CustomItem customItem3 = new CustomItem(monsteritem3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject monsteritem4 = ObsidianGolemHead;
			CustomItem customItem4 = new CustomItem(monsteritem4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject monsteritem5 = LavaGolemClubs;
			CustomItem customItem5 = new CustomItem(monsteritem5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);
			GameObject monsteritem6 = LavaGolemSpikes;
			CustomItem customItem6 = new CustomItem(monsteritem6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);
			GameObject monsteritem7 = LavaGolemHead;
			CustomItem customItem7 = new CustomItem(monsteritem7, fixReference: true);
			ItemManager.Instance.AddItem(customItem7);
			GameObject monsteritem8 = IceGolemClubs;
			CustomItem customItem8 = new CustomItem(monsteritem8, fixReference: true);
			ItemManager.Instance.AddItem(customItem8);
			GameObject monsteritem9 = IceGolemSpikes;
			CustomItem customItem9 = new CustomItem(monsteritem9, fixReference: true);
			ItemManager.Instance.AddItem(customItem9);
			GameObject monsteritem10 = IceGolemHead;
			CustomItem customItem10 = new CustomItem(monsteritem10, fixReference: true);
			ItemManager.Instance.AddItem(customItem10);
			GameObject monsteritem11 = SkirSandburstStaff;
			CustomItem customItem11 = new CustomItem(monsteritem11, fixReference: true);
			ItemManager.Instance.AddItem(customItem11);
			GameObject monsteritem12 = SkirSandburstHeaddress;
			CustomItem customItem12 = new CustomItem(monsteritem12, fixReference: true);
			ItemManager.Instance.AddItem(customItem12);
			GameObject monsteritem13 = SwordFrozenBones;
			CustomItem customItem13 = new CustomItem(monsteritem13, fixReference: true);
			ItemManager.Instance.AddItem(customItem13);
			GameObject monsteritem14 = BowFrozenBones;
			CustomItem customItem14 = new CustomItem(monsteritem14, fixReference: true);
			ItemManager.Instance.AddItem(customItem14);
			GameObject monsteritem15 = SwordSkelR;
			CustomItem customItem15 = new CustomItem(monsteritem15, fixReference: true);
			ItemManager.Instance.AddItem(customItem15);
			GameObject monsteritem16 = BowSkelR;
			CustomItem customItem16 = new CustomItem(monsteritem16, fixReference: true);
			ItemManager.Instance.AddItem(customItem16);
			GameObject monsteritem17 = SwordSkelG;
			CustomItem customItem17 = new CustomItem(monsteritem17, fixReference: true);
			ItemManager.Instance.AddItem(customItem17);
			GameObject monsteritem18 = BowSkelG;
			CustomItem customItem18 = new CustomItem(monsteritem18, fixReference: true);
			ItemManager.Instance.AddItem(customItem18);
			GameObject monsteritem19 = ShieldCharred;
			CustomItem customItem19 = new CustomItem(monsteritem19, fixReference: true);
			ItemManager.Instance.AddItem(customItem19);
			GameObject monsteritem20 = SwordCharred;
			CustomItem customItem20 = new CustomItem(monsteritem20, fixReference: true);
			ItemManager.Instance.AddItem(customItem20);
			GameObject monsteritem21 = BowCharred;
			CustomItem customItem21 = new CustomItem(monsteritem21, fixReference: true);
			ItemManager.Instance.AddItem(customItem21);
		}
		private void CreateMonsterAbilities()
		{
			GameObject testability1 = TestingA1;
			CustomItem customAbility1 = new CustomItem(testability1, fixReference: true);
			ItemManager.Instance.AddItem(customAbility1);
			GameObject testability2 = TestingA2;
			CustomItem customAbility2 = new CustomItem(testability2, fixReference: true);
			ItemManager.Instance.AddItem(customAbility2);
			GameObject testability3 = TestingA3;
			CustomItem customAbility3 = new CustomItem(testability3, fixReference: true);
			ItemManager.Instance.AddItem(customAbility3);

			GameObject monsterability1 = NPC_NomadAoE_Attack;
			CustomItem customItem1 = new CustomItem(monsterability1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject monsterability2 = Farkas_FrostBite;
			CustomItem customItem2 = new CustomItem(monsterability2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject monsterability3 = Farkas_Hamper_Attack;
			CustomItem customItem3 = new CustomItem(monsterability3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject monsterability4 = Farkas_Bleed;
			CustomItem customItem4 = new CustomItem(monsterability4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject monsterability5 = Farkas_Attack3;
			CustomItem customItem5 = new CustomItem(monsterability5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);
			GameObject monsterability6 = Farkas_Attack2;
			CustomItem customItem6 = new CustomItem(monsterability6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);
			GameObject monsterability7 = Farkas_Attack1;
			CustomItem customItem7 = new CustomItem(monsterability7, fixReference: true);
			ItemManager.Instance.AddItem(customItem7);
			GameObject monsterability8 = Rambore_Attack;
			CustomItem customItem8 = new CustomItem(monsterability8, fixReference: true);
			ItemManager.Instance.AddItem(customItem8);
			GameObject monsterability9 = Rambore_Gore;
			CustomItem customItem9 = new CustomItem(monsterability9, fixReference: true);
			ItemManager.Instance.AddItem(customItem9);
			GameObject monsterability10 = SkirSandBurst_VoidAttack;
			CustomItem customItem10 = new CustomItem(monsterability10, fixReference: true);
			ItemManager.Instance.AddItem(customItem10);
			GameObject monsterability11 = SkirSandburst_VoidSum;
			CustomItem customItem11 = new CustomItem(monsterability11, fixReference: true);
			ItemManager.Instance.AddItem(customItem11);
			GameObject monsterability12 = SkirSandburst_FWSum;
			CustomItem customItem12 = new CustomItem(monsterability12, fixReference: true);
			ItemManager.Instance.AddItem(customItem12);
			GameObject monsterability13 = SkirSandburst_Shield;
			CustomItem customItem13 = new CustomItem(monsterability13, fixReference: true);
			ItemManager.Instance.AddItem(customItem13);
			GameObject monsterability14 = SkirSandburst_Heal;
			CustomItem customItem14 = new CustomItem(monsterability14, fixReference: true);
			ItemManager.Instance.AddItem(customItem14);
			GameObject monsterability15 = SkirSandburst_Nova;
			CustomItem customItem15 = new CustomItem(monsterability15, fixReference: true);
			ItemManager.Instance.AddItem(customItem15);
			GameObject monsterability16 = SkirSandburst_FB_Attack;
			CustomItem customItem16 = new CustomItem(monsterability16, fixReference: true);
			ItemManager.Instance.AddItem(customItem16);
			GameObject monsterability17 = imp_firebolt_attack;
			CustomItem customItem17 = new CustomItem(monsterability17, fixReference: true);
			ItemManager.Instance.AddItem(customItem17);
			GameObject monsterability18 = Bhygshan_AoE;
			CustomItem customItem18 = new CustomItem(monsterability18, fixReference: true);
			ItemManager.Instance.AddItem(customItem18);
			GameObject monsterability19 = Bhygshan_Fireball;
			CustomItem customItem19 = new CustomItem(monsterability19, fixReference: true);
			ItemManager.Instance.AddItem(customItem19);
			GameObject monsterability20 = Bhygshan_FireBolt;
			CustomItem customItem20 = new CustomItem(monsterability20, fixReference: true);
			ItemManager.Instance.AddItem(customItem20);
			GameObject monsterability21 = Bhygshan_SprayFrost;
			CustomItem customItem21 = new CustomItem(monsterability21, fixReference: true);
			ItemManager.Instance.AddItem(customItem21);
			GameObject monsterability22 = Bhygshan_Throw;
			CustomItem customItem22 = new CustomItem(monsterability22, fixReference: true);
			ItemManager.Instance.AddItem(customItem22);
			GameObject monsterability23 = Bitterstump_Heal;
			CustomItem customItem23 = new CustomItem(monsterability23, fixReference: true);
			ItemManager.Instance.AddItem(customItem23);
			GameObject monsterability24 = Bitterstump_Melee;
			CustomItem customItem24 = new CustomItem(monsterability24, fixReference: true);
			ItemManager.Instance.AddItem(customItem24);
			GameObject monsterability25 = Bitterstump_Roots;
			CustomItem customItem25 = new CustomItem(monsterability25, fixReference: true);
			ItemManager.Instance.AddItem(customItem25);
			GameObject monsterability26 = Bitterstump_SprayFrost;
			CustomItem customItem26 = new CustomItem(monsterability26, fixReference: true);
			ItemManager.Instance.AddItem(customItem26);
			GameObject monsterability27 = Bitterstump_SprayPoison;
			CustomItem customItem27 = new CustomItem(monsterability27, fixReference: true);
			ItemManager.Instance.AddItem(customItem27);
			GameObject monsterability28 = Mage_FireStrike_Attack;
			CustomItem customItem28 = new CustomItem(monsterability28, fixReference: true);
			ItemManager.Instance.AddItem(customItem28);
			GameObject monsterability29 = ForestWolf_Attack1;
			CustomItem customItem29 = new CustomItem(monsterability29, fixReference: true);
			ItemManager.Instance.AddItem(customItem29);
			GameObject monsterability30 = ForestWolf_Attack2;
			CustomItem customItem30 = new CustomItem(monsterability30, fixReference: true);
			ItemManager.Instance.AddItem(customItem30);
			GameObject monsterability31 = ForestWolf_Attack3;
			CustomItem customItem31 = new CustomItem(monsterability31, fixReference: true);
			ItemManager.Instance.AddItem(customItem31);
			GameObject monsterability32 = DireWolf_Attack1;
			CustomItem customItem32 = new CustomItem(monsterability32, fixReference: true);
			ItemManager.Instance.AddItem(customItem32);
			GameObject monsterability33 = DireWolf_Attack2;
			CustomItem customItem33 = new CustomItem(monsterability33, fixReference: true);
			ItemManager.Instance.AddItem(customItem33);
			GameObject monsterability34 = DireWolf_Attack3;
			CustomItem customItem34 = new CustomItem(monsterability34, fixReference: true);
			ItemManager.Instance.AddItem(customItem34);
			GameObject monsterability35 = Vilefang_Attack1;
			CustomItem customItem35 = new CustomItem(monsterability35, fixReference: true);
			ItemManager.Instance.AddItem(customItem35);
			GameObject monsterability36 = Vilefang_Attack2;
			CustomItem customItem36 = new CustomItem(monsterability36, fixReference: true);
			ItemManager.Instance.AddItem(customItem36);
			GameObject monsterability37 = Vilefang_Attack3;
			CustomItem customItem37 = new CustomItem(monsterability37, fixReference: true);
			ItemManager.Instance.AddItem(customItem37);
			GameObject monsterability38 = livinglava_nova_attack;
			CustomItem customItem38 = new CustomItem(monsterability38, fixReference: true);
			ItemManager.Instance.AddItem(customItem38);
			GameObject monsterability39 = livingwater_nova_attack;
			CustomItem customItem39 = new CustomItem(monsterability39, fixReference: true);
			ItemManager.Instance.AddItem(customItem39);
			GameObject monsterability40 = imp_icebolt_attack;
			CustomItem customItem40 = new CustomItem(monsterability40, fixReference: true);
			ItemManager.Instance.AddItem(customItem40);
			GameObject monsterability41 = imp_stormbolt_attack;
			CustomItem customItem41 = new CustomItem(monsterability41, fixReference: true);
			ItemManager.Instance.AddItem(customItem41);
			GameObject monsterability42 = imp_voidbolt_attack;
			CustomItem customItem42 = new CustomItem(monsterability42, fixReference: true);
			ItemManager.Instance.AddItem(customItem42);

			GameObject monsterability43 = DrakespitFire;
			CustomItem customItem43 = new CustomItem(monsterability43, fixReference: true);
			ItemManager.Instance.AddItem(customItem43);
			GameObject monsterability44 = DrakespitArcane;
			CustomItem customItem44 = new CustomItem(monsterability44, fixReference: true);
			ItemManager.Instance.AddItem(customItem44);
			GameObject monsterability45 = DrakespitFrost;
			CustomItem customItem45 = new CustomItem(monsterability45, fixReference: true);
			ItemManager.Instance.AddItem(customItem45);
			GameObject monsterability46 = DrakespitPoison1;
			CustomItem customItem46 = new CustomItem(monsterability46, fixReference: true);
			ItemManager.Instance.AddItem(customItem46);
			GameObject monsterability47 = DrakespitPoison2;
			CustomItem customItem47 = new CustomItem(monsterability47, fixReference: true);
			ItemManager.Instance.AddItem(customItem47);
			GameObject monsterability48 = DrakespitVoid;
			CustomItem customItem48 = new CustomItem(monsterability48, fixReference: true);
			ItemManager.Instance.AddItem(customItem48);
		}
		private void AddBosses()
		{
			//GameObject gameObject6 = Rylan;
			//CustomPrefab customPrefab6 = new CustomPrefab(gameObject6, true);
			//PrefabManager.Instance.AddPrefab(customPrefab6);

			GameObject gameObject5 = SkirSandburst;
			CustomPrefab customPrefab5 = new CustomPrefab(gameObject5, true);
			PrefabManager.Instance.AddPrefab(customPrefab5);

			GameObject gameObject4 = Farkas;
			CustomPrefab customPrefab4 = new CustomPrefab(gameObject4, true);
			PrefabManager.Instance.AddPrefab(customPrefab4);

			GameObject gameObject3 = Bhygshan;
			CustomPrefab customPrefab3 = new CustomPrefab(gameObject3, true);
			PrefabManager.Instance.AddPrefab(customPrefab3);

			GameObject gameObject2 = Bitterstump;
			CustomPrefab customPrefab2 = new CustomPrefab(gameObject2, true);
			PrefabManager.Instance.AddPrefab(customPrefab2);

			GameObject gameObject1 = Rambore;
			CustomPrefab customPrefab1 = new CustomPrefab(gameObject1, true);
			PrefabManager.Instance.AddPrefab(customPrefab1);
		}
		private void AddMonsterReskins()
		{
			GameObject testing = Testing;
			CustomPrefab testingmob = new CustomPrefab(testing, true);
			PrefabManager.Instance.AddPrefab(testingmob);

			GameObject npc2 = Einherjar;
			CustomPrefab customNPC2 = new CustomPrefab(npc2, true);
			PrefabManager.Instance.AddPrefab(customNPC2);

			GameObject npc1 = Skugga;
			CustomPrefab customNPC1 = new CustomPrefab(npc1, true);
			PrefabManager.Instance.AddPrefab(customNPC1);

			GameObject gameObject30 = BlackDrake;
			CustomPrefab customPrefab30 = new CustomPrefab(gameObject30, true);
			PrefabManager.Instance.AddPrefab(customPrefab30);

			GameObject gameObject29 = PoisonDrake;
			CustomPrefab customPrefab29 = new CustomPrefab(gameObject29, true);
			PrefabManager.Instance.AddPrefab(customPrefab29);

			GameObject gameObject28 = GoldDrake;
			CustomPrefab customPrefab28 = new CustomPrefab(gameObject28, true);
			PrefabManager.Instance.AddPrefab(customPrefab28);

			GameObject gameObject27 = DarkDrake;
			CustomPrefab customPrefab27 = new CustomPrefab(gameObject27, true);
			PrefabManager.Instance.AddPrefab(customPrefab27);

			GameObject gameObject26 = ArcaneDrake;
			CustomPrefab customPrefab26 = new CustomPrefab(gameObject26, true);
			PrefabManager.Instance.AddPrefab(customPrefab26);

			GameObject gameObject25 = IceDrake;
			CustomPrefab customPrefab25 = new CustomPrefab(gameObject25, true);
			PrefabManager.Instance.AddPrefab(customPrefab25);

			GameObject gameObject24 = FlameDrake;
			CustomPrefab customPrefab24 = new CustomPrefab(gameObject24, true);
			PrefabManager.Instance.AddPrefab(customPrefab24);

			GameObject gameObject23 = LivingWater;
			CustomPrefab customPrefab23 = new CustomPrefab(gameObject23, true);
			PrefabManager.Instance.AddPrefab(customPrefab23);

			GameObject gameObject22 = LivingLava;
			CustomPrefab customPrefab22 = new CustomPrefab(gameObject22, true);
			PrefabManager.Instance.AddPrefab(customPrefab22);

			GameObject gameObject21 = VilefangCub;
			CustomPrefab customPrefab21 = new CustomPrefab(gameObject21, true);
			PrefabManager.Instance.AddPrefab(customPrefab21);

			GameObject gameObject20 = Vilefang;
			CustomPrefab customPrefab20 = new CustomPrefab(gameObject20, true);
			PrefabManager.Instance.AddPrefab(customPrefab20);

			GameObject gameObject19 = DireWolfCub;
			CustomPrefab customPrefab19 = new CustomPrefab(gameObject19, true);
			PrefabManager.Instance.AddPrefab(customPrefab19);

			GameObject gameObject18 = DireWolf;
			CustomPrefab customPrefab18 = new CustomPrefab(gameObject18, true);
			PrefabManager.Instance.AddPrefab(customPrefab18);

			GameObject gameObject17 = ForestWolfCub;
			CustomPrefab customPrefab17 = new CustomPrefab(gameObject17, true);
			PrefabManager.Instance.AddPrefab(customPrefab17);

			GameObject gameObject16 = ForestWolf;
			CustomPrefab customPrefab16 = new CustomPrefab(gameObject16, true);
			PrefabManager.Instance.AddPrefab(customPrefab16);

			GameObject gameObject15 = Voidling;
			CustomPrefab customPrefab15 = new CustomPrefab(gameObject15, true);
			PrefabManager.Instance.AddPrefab(customPrefab15);

			GameObject gameObject14 = Stormling;
			CustomPrefab customPrefab14 = new CustomPrefab(gameObject14, true);
			PrefabManager.Instance.AddPrefab(customPrefab14);

			GameObject gameObject13 = Frostling;
			CustomPrefab customPrefab13 = new CustomPrefab(gameObject13, true);
			PrefabManager.Instance.AddPrefab(customPrefab13);

			GameObject gameObject12 = GhostIce;
			CustomPrefab customPrefab12 = new CustomPrefab(gameObject12, true);
			PrefabManager.Instance.AddPrefab(customPrefab12);

			GameObject gameObject11 = GhostWhite;
			CustomPrefab customPrefab11 = new CustomPrefab(gameObject11, true);
			PrefabManager.Instance.AddPrefab(customPrefab11);

			GameObject gameObject10 = ObsidianGolem;
			CustomPrefab customPrefab10 = new CustomPrefab(gameObject10, true);
			PrefabManager.Instance.AddPrefab(customPrefab10);

			GameObject gameObject9 = LavaGolem;
			CustomPrefab customPrefab9 = new CustomPrefab(gameObject9, true);
			PrefabManager.Instance.AddPrefab(customPrefab9);

			GameObject gameObject8 = IceGolem;
			CustomPrefab customPrefab8 = new CustomPrefab(gameObject8, true);
			PrefabManager.Instance.AddPrefab(customPrefab8);

			GameObject gameObject7 = BlackStag;
			CustomPrefab customPrefab7 = new CustomPrefab(gameObject7, true);
			PrefabManager.Instance.AddPrefab(customPrefab7);

			GameObject gameObject6 = BlackDeer;
			CustomPrefab customPrefab6 = new CustomPrefab(gameObject6, true);
			PrefabManager.Instance.AddPrefab(customPrefab6);

			GameObject gameObject5 = FrozenBones;
			CustomPrefab customPrefab5 = new CustomPrefab(gameObject5, true);
			PrefabManager.Instance.AddPrefab(customPrefab5);

			GameObject gameObject4 = SkeletonR;
			CustomPrefab customPrefab4 = new CustomPrefab(gameObject4, true);
			PrefabManager.Instance.AddPrefab(customPrefab4);

			GameObject gameObject3 = SkeletonG;
			CustomPrefab customPrefab3 = new CustomPrefab(gameObject3, true);
			PrefabManager.Instance.AddPrefab(customPrefab3);

			GameObject gameObject2 = CharredRemains;
			CustomPrefab customPrefab2 = new CustomPrefab(gameObject2, true);
			PrefabManager.Instance.AddPrefab(customPrefab2);

			GameObject gameObject1 = GreaterSurtling;
			CustomPrefab customPrefab1 = new CustomPrefab(gameObject1, true);
			PrefabManager.Instance.AddPrefab(customPrefab1);
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
			GameObject animal6 = GiantSnail;
			CustomPrefab critter6 = new CustomPrefab(animal6, true);
			PrefabManager.Instance.AddPrefab(critter6);
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
		private void UpdateBlastFurnace()
		{
			GameObject itemPrefab = DoDAssets.LoadAsset<GameObject>("SteelBar_DoD");
			CustomItem customItem = new CustomItem(itemPrefab, fixReference: false);
			ItemManager.Instance.AddItem(customItem);

			CustomItemConversion itemConversion = new CustomItemConversion(new SmelterConversionConfig
			{
				Station = "blastfurnace",
				FromItem = "Iron",
				ToItem = "SteelBar_DoD"
			});
			ItemManager.Instance.AddItemConversion(itemConversion);
			GameObject itemPrefab2 = DoDAssets.LoadAsset<GameObject>("FrometalOre_DoD");
			GameObject itemPrefab3 = DoDAssets.LoadAsset<GameObject>("FrometalBar_DoD");
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
			GameObject itemPrefab4 = DoDAssets.LoadAsset<GameObject>("FelmetalOre_DoD");
			GameObject itemPrefab5 = DoDAssets.LoadAsset<GameObject>("FelmetalBar_DoD");
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
		private void CreateRugs()
		{
			GameObject gameObject1 = RugDeer;
			CustomPiece customPiece1 = new CustomPiece(gameObject1, true, new PieceConfig
			{
				Description = "Increases Comfort level by one",
				Icon = RugBDeer,
				PieceTable = "Hammer",
				Category = "Furniture",
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "LinenThread",
					Amount = 20,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "BlackDeerHide_DoD",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);

			GameObject gameObject2 = RugDire;
			CustomPiece customPiece2 = new CustomPiece(gameObject2, true, new PieceConfig
			{
				Description = "Increases Comfort level by one",
				Icon = RugDWolf,
				PieceTable = "Hammer",
				Category = "Furniture",
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "LinenThread",
					Amount = 20,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);

			GameObject gameObject3 = RugForest;
			CustomPiece customPiece3 = new CustomPiece(gameObject3, true, new PieceConfig
			{
				Description = "Increases Comfort level by one",
				Icon = RugFWolf,
				PieceTable = "Hammer",
				Category = "Furniture",
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "LinenThread",
					Amount = 20,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "ForestWolfPelt_DoD",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
		}
		private void CreateWeaponCrates()
		{
			GameObject T1 = T1WeaponKit;
			CustomItem customItem1 = new CustomItem(T1, fixReference: true, new ItemConfig
			{
				Name = "Crude Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T2 = T2WeaponKit;
			CustomItem customItem2 = new CustomItem(T2, fixReference: true, new ItemConfig
			{
				Name = "Basic Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T3 = T3WeaponKit;
			CustomItem customItem3 = new CustomItem(T3, fixReference: true, new ItemConfig
			{
				Name = "Good Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Silver",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "FineWood",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T4 = T4WeaponKit;
			CustomItem customItem4 = new CustomItem(T4, fixReference: true, new ItemConfig
			{
				Name = "Great Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "BlackMetal",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T5 = T5WeaponKit;
			CustomItem customItem = new CustomItem(T5, fixReference: true, new ItemConfig
			{
				Name = "Superior Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "SteelBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T6 = T6WeaponKit;
			CustomItem customItem6 = new CustomItem(T6, fixReference: true, new ItemConfig
			{
				Name = "Excellent Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FelmetalBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T7 = T7WeaponKit;
			CustomItem customItem7 = new CustomItem(T7, fixReference: true, new ItemConfig
			{
				Name = "Exceptional Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FrometalBar_DoD",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T8 = T8WeaponKit;
			CustomItem customItem8 = new CustomItem(T8, fixReference: true, new ItemConfig
			{
				Name = "Extraordinary Weapon kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Flametal",
						Amount = 15
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ElderBark",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "OakWood_DoD",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
		}
		private void CreateArmorCrates()
		{
			GameObject T1 = T1ArmorKit;
			CustomItem customItem1 = new CustomItem(T1, fixReference: true, new ItemConfig
			{
				Name = "Crude Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Bronze",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "DeerHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T2 = T2ArmorKit;
			CustomItem customItem2 = new CustomItem(T2, fixReference: true, new ItemConfig
			{
				Name = "Basic Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "DeerHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T3 = T3ArmorKit;
			CustomItem customItem3 = new CustomItem(T3, fixReference: true, new ItemConfig
			{
				Name = "Good Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Silver",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "TrollHide",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "Resin",
						Amount = 5
					}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T4 = T4ArmorKit;
			CustomItem customItem4 = new CustomItem(T4, fixReference: true, new ItemConfig
			{
				Name = "Great Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "BlackMetal",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "WolfPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T5 = T5ArmorKit;
			CustomItem customItem = new CustomItem(T5, fixReference: true, new ItemConfig
			{
				Name = "Superior Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "SteelBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "LoxPelt",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");

			GameObject T6 = T6ArmorKit;
			CustomItem customItem6 = new CustomItem(T6, fixReference: true, new ItemConfig
			{
				Name = "Excellent Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FelmetalBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "ForestWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T7 = T7ArmorKit;
			CustomItem customItem7 = new CustomItem(T7, fixReference: true, new ItemConfig
			{
				Name = "Exceptional Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "FrometalBar_DoD",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
			GameObject T8 = T8ArmorKit;
			CustomItem customItem8 = new CustomItem(T8, fixReference: true, new ItemConfig
			{
				Name = "Extraordinary Armor kit",
				Amount = 1,
				CraftingStation = "piece_workbench",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
					new RequirementConfig
					{
						Item = "Flametal",
						Amount = 30
					},
					new RequirementConfig
					{
						Item = "DireWolfPelt_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "BlackDeerHide_DoD",
						Amount = 5
					},
					new RequirementConfig
					{
						Item = "LinenThread",
						Amount = 10
					}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Oops");
		}
		private void CreateClassWeapons()
		{
			GameObject monkmace = MaceShock;
			CustomItem customItem1 = new CustomItem(monkmace, fixReference: true, new ItemConfig
			{
				Name = "Striking Mace",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "MonkItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: MaceShock");
			GameObject berserkeraxe = AxeBattle;
			CustomItem customItem2 = new CustomItem(berserkeraxe, fixReference: true, new ItemConfig
			{
				Name = "Raging Battleaxe",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "BerserkerItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: AxeBattle");
			GameObject druidspear = SpearAcid;
			CustomItem customItem3 = new CustomItem(druidspear, fixReference: true, new ItemConfig
			{
				Name = "Acid Spear",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DruidItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			//Jotunn.Logger.LogMessage("Created Recipie: SpearAcid");
			GameObject shamanwand = WandLightning;
			CustomItem customItem4 = new CustomItem(shamanwand, fixReference: true, new ItemConfig
			{
				Name = "Lightning Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamanItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: WandLightning");
			GameObject wandmage = WandFire;
			CustomItem customItem5 = new CustomItem(wandmage, fixReference: true, new ItemConfig
			{
				Name = "Fire Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "MageItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			//Jotunn.Logger.LogMessage("Created Recipie: WandFire");
			GameObject wandwarlock = WandShadow;
			CustomItem customItem6 = new CustomItem(wandwarlock, fixReference: true, new ItemConfig
			{
				Name = "Shadow Wand",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "WarlockItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: WandShadow");
			GameObject paladinmace = MaceDivine;
			CustomItem customItem7 = new CustomItem(paladinmace, fixReference: true, new ItemConfig
			{
				Name = "Divine Mace",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 3,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "PaladinItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: MaceDivine");
			GameObject roguesword = SwordAssassin;
			CustomItem customItem8 = new CustomItem(roguesword, fixReference: true, new ItemConfig
			{
				Name = "Assassin's Blade",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "RogueItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: SwordAssassin");
			GameObject knightaxe = AxeKnight;
			CustomItem customItem9 = new CustomItem(knightaxe, fixReference: true, new ItemConfig
			{
				Name = "Brutal Axe",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DeathKnightItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			//Jotunn.Logger.LogMessage("Created Recipie: AxeKnight");
			GameObject ninjasword = SwordVoid;
			CustomItem customItem10 = new CustomItem(ninjasword, fixReference: true, new ItemConfig
			{
				Name = "Void Sword",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 1,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 4,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "NinjaItem_DoD",
					Amount = 3,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BasicWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem10);
			//Jotunn.Logger.LogMessage("Created Recipie: SwordNinja");
		}
		private void CreateMagicSwords()
		{
			GameObject firesoul = Firesoul;
			CustomItem customItem1 = new CustomItem(firesoul, fixReference: false, new ItemConfig
			{
				Name = "Firesoul",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "Flametal",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
			//Jotunn.Logger.LogMessage("Created Recipie: Firesoul");
			GameObject solarflare = Solarflare;
			CustomItem customItem2 = new CustomItem(solarflare, fixReference: false, new ItemConfig
			{
				Name = "Solarflare",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "Flametal",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			//Jotunn.Logger.LogMessage("Created Recipie: Solarflare");
			GameObject reaper = Reaper;
			CustomItem customItem3 = new CustomItem(reaper, fixReference: false, new ItemConfig
			{
				Name = "Reaper",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "VoidlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			//Jotunn.Logger.LogMessage("Created Recipie: Reaper");
			GameObject desolation = Desolation;
			CustomItem customItem4 = new CustomItem(desolation, fixReference: false, new ItemConfig
			{
				Name = "Desolation",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			//Jotunn.Logger.LogMessage("Created Recipie: Desolation");
			GameObject soulstorm = Soulstorm;
			CustomItem customItem5 = new CustomItem(soulstorm, fixReference: false, new ItemConfig
			{
				Name = "Soulstorm",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			//Jotunn.Logger.LogMessage("Created Recipie: Soulstorm");
			GameObject silverlight = Silverlight;
			CustomItem customItem6 = new CustomItem(silverlight, fixReference: false, new ItemConfig
			{
				Name = "Silverlight",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			//Jotunn.Logger.LogMessage("Created Recipie: Silverlight");
			GameObject coldflame = Coldflame;
			CustomItem customItem7 = new CustomItem(coldflame, fixReference: false, new ItemConfig
			{
				Name = "Coldflame",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FrometalBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "SteelBar_DoD",
					Amount = 20,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			//Jotunn.Logger.LogMessage("Created Recipie: Coldflame");
			GameObject frostflame = Frostflame;
			CustomItem customItem8 = new CustomItem(frostflame, fixReference: false, new ItemConfig
			{
				Name = "Frostflame",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FrometalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "FrostlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			//Jotunn.Logger.LogMessage("Created Recipie: Frostflame");
			GameObject nethersbane = Nethersbane;
			CustomItem customItem9 = new CustomItem(nethersbane, fixReference: false, new ItemConfig
			{
				Name = "Nethersbane",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 6,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FelmetalBar_DoD",
					Amount = 40,
					AmountPerLevel = 10
				},
				new RequirementConfig
				{
					Item = "VoidlingCore_DoD",
					Amount = 10,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "StormlingCore_DoD",
					Amount = 5,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "DireWolfPelt_DoD",
					Amount = 5,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			//Jotunn.Logger.LogMessage("Created Recipie: Nethersbane");
		}
		private void CreateDropables()
		{
			GameObject npcitem4 = DoDAssets.LoadAsset<GameObject>("SkullToken_DoD");
			CustomItem customNpcItem4 = new CustomItem(npcitem4, fixReference: true);
			ItemManager.Instance.AddItem(customNpcItem4);

			GameObject npcitem3 = DoDAssets.LoadAsset<GameObject>("Shield_NPC_DoD");
			CustomItem customNpcItem3 = new CustomItem(npcitem3, fixReference: true);
			ItemManager.Instance.AddItem(customNpcItem3);

			GameObject npcitem2 = DoDAssets.LoadAsset<GameObject>("Bow_NPC_DoD");
			CustomItem customNpcItem2 = new CustomItem(npcitem2, fixReference: true);
			ItemManager.Instance.AddItem(customNpcItem2);

			GameObject npcitem1 = DoDAssets.LoadAsset<GameObject>("Sword_NPC_DoD");
			CustomItem customNpcItem1 = new CustomItem(npcitem1, fixReference: true);
			ItemManager.Instance.AddItem(customNpcItem1);

			GameObject food4 = CBait;
			CustomItem customFood4 = new CustomItem(food4, fixReference: true, new ItemConfig
			{
				Name = "Carnivor Bait",
				Amount = 5,
				CraftingStation = "piece_artisanstation",
				MinStationLevel = 5,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "LoxMeat",
						Amount = 20
					} 
				}
			});
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

			GameObject dropable1 = TrophyCharredRemains;
			CustomItem customItem1 = new CustomItem(dropable1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);

			GameObject dropable2 = TrophyFrozenBones;
			CustomItem customItem2 = new CustomItem(dropable2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);

			GameObject dropable3 = TrophySurtling;
			CustomItem customItem3 = new CustomItem(dropable3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);

			GameObject dropable4 = TrophySkeletonG;
			CustomItem customItem4 = new CustomItem(dropable4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);

			GameObject dropable5 = TrophySkeletonR;
			CustomItem customItem5 = new CustomItem(dropable5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);

			GameObject dropable6 = TrophyFrostling;
			CustomItem customItem6 = new CustomItem(dropable6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);

			GameObject dropable7 = TrophyStormling;
			CustomItem customItem7 = new CustomItem(dropable7, fixReference: true);
			ItemManager.Instance.AddItem(customItem7);

			GameObject dropable8 = TrophyVoidling;
			CustomItem customItem8 = new CustomItem(dropable8, fixReference: true);
			ItemManager.Instance.AddItem(customItem8);

			GameObject dropable9 = TrophyOGolem;
			CustomItem customItem9 = new CustomItem(dropable9, fixReference: true);
			ItemManager.Instance.AddItem(customItem9);

			GameObject dropable10 = TrophyLGolem;
			CustomItem customItem10 = new CustomItem(dropable10, fixReference: true);
			ItemManager.Instance.AddItem(customItem10);

			GameObject dropable11 = TrophyIceGolem;
			CustomItem customItem11 = new CustomItem(dropable11, fixReference: true);
			ItemManager.Instance.AddItem(customItem11);

			GameObject dropable12 = TrophyVilefang;
			CustomItem customItem12 = new CustomItem(dropable12, fixReference: true);
			ItemManager.Instance.AddItem(customItem12);

			GameObject dropable13 = TrophyDireWolf;
			CustomItem customItem13 = new CustomItem(dropable13, fixReference: true);
			ItemManager.Instance.AddItem(customItem13);

			GameObject dropable14 = TrophyForestWolf;
			CustomItem customItem14 = new CustomItem(dropable14, fixReference: true);
			ItemManager.Instance.AddItem(customItem14);

			GameObject dropable15 = TrophyLivingLava;
			CustomItem customItem15 = new CustomItem(dropable15, fixReference: true);
			ItemManager.Instance.AddItem(customItem15);

			GameObject dropable16 = TrophyLivingWater;
			CustomItem customItem16 = new CustomItem(dropable16, fixReference: true);
			ItemManager.Instance.AddItem(customItem16);

			GameObject dropable17 = TrophyBlackDeer;
			CustomItem customItem17 = new CustomItem(dropable17, fixReference: true);
			ItemManager.Instance.AddItem(customItem17);

			GameObject dropable18 = TrophyIceDrake;
			CustomItem customItem18 = new CustomItem(dropable18, fixReference: true);
			ItemManager.Instance.AddItem(customItem18);

			GameObject dropable19 = TrophyFlameDrake;
			CustomItem customItem19 = new CustomItem(dropable19, fixReference: true);
			ItemManager.Instance.AddItem(customItem19);

			GameObject dropable20 = TrophyArcaneDrake;
			CustomItem customItem20 = new CustomItem(dropable20, fixReference: true);
			ItemManager.Instance.AddItem(customItem20);

			GameObject dropable21 = TrophyDarknessDrake;
			CustomItem customItem21 = new CustomItem(dropable21, fixReference: true);
			ItemManager.Instance.AddItem(customItem21);

			GameObject dropable22 = TrophyGoldDrake;
			CustomItem customItem22 = new CustomItem(dropable22, fixReference: true);
			ItemManager.Instance.AddItem(customItem22);

			GameObject dropable23 = TrophyPoisonDrake;
			CustomItem customItem23 = new CustomItem(dropable23, fixReference: true);
			ItemManager.Instance.AddItem(customItem23);

			GameObject dropable24 = TrophyDarkDrake;
			CustomItem customItem24 = new CustomItem(dropable24, fixReference: true);
			ItemManager.Instance.AddItem(customItem24);

			GameObject dropable25 = InfusedGemstone;
			CustomItem customItem25 = new CustomItem(dropable25, fixReference: true);
			ItemManager.Instance.AddItem(customItem25);

			GameObject dropable26 = DeathKnightItem;
			CustomItem customItem26 = new CustomItem(dropable26, fixReference: true);
			ItemManager.Instance.AddItem(customItem26);

			GameObject dropable27 = ArcherItem;
			CustomItem customItem27 = new CustomItem(dropable27, fixReference: true);
			ItemManager.Instance.AddItem(customItem27);

			GameObject dropable28 = BerserkerItem;
			CustomItem customItem28 = new CustomItem(dropable28, fixReference: true);
			ItemManager.Instance.AddItem(customItem28);

			GameObject dropable29 = DruidItem;
			CustomItem customItem29 = new CustomItem(dropable29, fixReference: true);
			ItemManager.Instance.AddItem(customItem29);

			GameObject dropable30 = MageItem;
			CustomItem customItem30 = new CustomItem(dropable30, fixReference: true);
			ItemManager.Instance.AddItem(customItem30);

			GameObject dropable31 = MonkItem;
			CustomItem customItem31 = new CustomItem(dropable31, fixReference: true);
			ItemManager.Instance.AddItem(customItem31);

			GameObject dropable32 = NinjaItem;
			CustomItem customItem32 = new CustomItem(dropable32, fixReference: true);
			ItemManager.Instance.AddItem(customItem32);

			GameObject dropable33 = PaladinItem;
			CustomItem customItem33 = new CustomItem(dropable33, fixReference: true);
			ItemManager.Instance.AddItem(customItem33);

			GameObject dropable34 = RogueItem;
			CustomItem customItem34 = new CustomItem(dropable34, fixReference: true);
			ItemManager.Instance.AddItem(customItem34);

			GameObject dropable35 = ShamanItem;
			CustomItem customItem35 = new CustomItem(dropable35, fixReference: true);
			ItemManager.Instance.AddItem(customItem35);

			GameObject dropable36 = WarlockItem;
			CustomItem customItem36 = new CustomItem(dropable36, fixReference: true);
			ItemManager.Instance.AddItem(customItem36);

			GameObject dropable37 = BoarTusk;
			CustomItem customItem37 = new CustomItem(dropable37, fixReference: true);
			ItemManager.Instance.AddItem(customItem37);

			GameObject dropable38 = LargeFang;
			CustomItem customItem38 = new CustomItem(dropable38, fixReference: true);
			ItemManager.Instance.AddItem(customItem38);

			GameObject dropable39 = ShamansVessel;
			CustomItem customItem39 = new CustomItem(dropable39, fixReference: true);
			ItemManager.Instance.AddItem(customItem39);

			GameObject dropable40 = GreydwarfHeart;
			CustomItem customItem40 = new CustomItem(dropable40, fixReference: true);
			ItemManager.Instance.AddItem(customItem40);

			GameObject dropable41 = SkeletonBones;
			CustomItem customItem41 = new CustomItem(dropable41, fixReference: true);
			ItemManager.Instance.AddItem(customItem41);

			GameObject dropable42 = GreyPearl;
			CustomItem customItem42 = new CustomItem(dropable42, fixReference: true);
			ItemManager.Instance.AddItem(customItem42);

			GameObject dropable43 = FrozenBone;
			CustomItem customItem43 = new CustomItem(dropable43, fixReference: true);
			ItemManager.Instance.AddItem(customItem43);

			GameObject dropable44 = CharredBone;
			CustomItem customItem44 = new CustomItem(dropable44, fixReference: true);
			ItemManager.Instance.AddItem(customItem44);

			GameObject dropable45 = FrostlingCore;
			CustomItem customItem45 = new CustomItem(dropable45, fixReference: true);
			ItemManager.Instance.AddItem(customItem45);

			GameObject dropable46 = StormlingCore;
			CustomItem customItem46 = new CustomItem(dropable46, fixReference: true);
			ItemManager.Instance.AddItem(customItem46);

			GameObject dropable47 = VoidlingCore;
			CustomItem customItem47 = new CustomItem(dropable47, fixReference: true);
			ItemManager.Instance.AddItem(customItem47);

			GameObject dropable48 = ForestWolfPelt;
			CustomItem customItem48 = new CustomItem(dropable48, fixReference: true);
			ItemManager.Instance.AddItem(customItem48);

			GameObject dropable49 = DireWolfPelt;
			CustomItem customItem49 = new CustomItem(dropable49, fixReference: true);
			ItemManager.Instance.AddItem(customItem49);

			GameObject dropable50 = WaterGlobe;
			CustomItem customItem50 = new CustomItem(dropable50, fixReference: true);
			ItemManager.Instance.AddItem(customItem50);

			GameObject dropable51 = SpiderChitin;
			CustomItem customItem51 = new CustomItem(dropable51, fixReference: true);
			ItemManager.Instance.AddItem(customItem51);

			GameObject dropable52 = BlackDeerHide;
			CustomItem customItem52 = new CustomItem(dropable52, fixReference: true);
			ItemManager.Instance.AddItem(customItem52);

			GameObject dropable53 = OakWood;
			CustomItem customItem53 = new CustomItem(dropable53, fixReference: true);
			ItemManager.Instance.AddItem(customItem53);

			GameObject dropable55 = BhygshanMace;
			CustomItem customItem55 = new CustomItem(dropable55, fixReference: true);
			ItemManager.Instance.AddItem(customItem55);

			GameObject dropable56 = Walnuts;
			CustomItem customItem56 = new CustomItem(dropable56, fixReference: true);
			ItemManager.Instance.AddItem(customItem56);
		}
		private void CreateTierItems()
        {
			GameObject tieritem1 = ShieldBGSkull;
			CustomItem customItem1 = new CustomItem(tieritem1, fixReference: true);
			ItemManager.Instance.AddItem(customItem1);
			GameObject tieritem2 = ShieldBEikthyr;
			CustomItem customItem2 = new CustomItem(tieritem2, fixReference: true);
			ItemManager.Instance.AddItem(customItem2);
			GameObject tieritem3 = ShieldBRambore;
			CustomItem customItem3 = new CustomItem(tieritem3, fixReference: true);
			ItemManager.Instance.AddItem(customItem3);
			GameObject tieritem4 = ShieldBElder;
			CustomItem customItem4 = new CustomItem(tieritem4, fixReference: true);
			ItemManager.Instance.AddItem(customItem4);
			GameObject tieritem5 = ShieldBBitter;
			CustomItem customItem5 = new CustomItem(tieritem5, fixReference: true);
			ItemManager.Instance.AddItem(customItem5);
			GameObject tieritem6 = ShieldBBonemass;
			CustomItem customItem6 = new CustomItem(tieritem6, fixReference: true);
			ItemManager.Instance.AddItem(customItem6);
			GameObject tieritem7 = ShieldBModer;
			CustomItem customItem7 = new CustomItem(tieritem7, fixReference: true);
			ItemManager.Instance.AddItem(customItem7);
			GameObject tieritem8 = ShieldBFarkas;
			CustomItem customItem8 = new CustomItem(tieritem8, fixReference: true);
			ItemManager.Instance.AddItem(customItem8);
			GameObject tieritem9 = ShieldBSkir;
			CustomItem customItem9 = new CustomItem(tieritem9, fixReference: true);
			ItemManager.Instance.AddItem(customItem9);
			GameObject tieritem10 = ShieldBYagluth;
			CustomItem customItem10 = new CustomItem(tieritem10, fixReference: true);
			ItemManager.Instance.AddItem(customItem10);
		}
		private void CreateShields()
		{
			GameObject shield10 = ShieldYagluth;
			CustomItem customItem10 = new CustomItem(shield10, fixReference: true, new ItemConfig
			{
				Name = "Deathgate",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldYagluth_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem10);
			GameObject shield9 = ShieldSkir;
			CustomItem customItem9 = new CustomItem(shield9, fixReference: true, new ItemConfig
			{
				Name = "Curator Ward",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldSkir_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			GameObject shield8 = ShieldFarkas;
			CustomItem customItem8 = new CustomItem(shield8, fixReference: true, new ItemConfig
			{
				Name = "Frozen Light",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldFarkas_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			GameObject shield7 = ShieldModer;
			CustomItem customItem7 = new CustomItem(shield7, fixReference: true, new ItemConfig
			{
				Name = "Perfect Storm",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldModer_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "DragonEgg",
					Amount = 1,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			GameObject shield6 = ShieldBonemass;
			CustomItem customItem6 = new CustomItem(shield6, fixReference: true, new ItemConfig
			{
				Name = "Ghostly Wall",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBonemass_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			GameObject shield5 = ShieldBitter;
			CustomItem customItem5 = new CustomItem(shield5, fixReference: true, new ItemConfig
			{
				Name = "Darkheart",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBitterstump_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			GameObject shield4 = ShieldElder;
			CustomItem customItem4 = new CustomItem(shield4, fixReference: true, new ItemConfig
			{
				Name = "Ironbark",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldElder_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			GameObject shield3 = ShieldRambore;
			CustomItem customItem3 = new CustomItem(shield3, fixReference: true, new ItemConfig
			{
				Name = "Enforcer",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldRambore_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "CrudeArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			GameObject shield2 = ShieldEikthyr;
			CustomItem customItem2 = new CustomItem(shield2, fixReference: true, new ItemConfig
			{
				Name = "Thundercloud",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldEikthyr_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "CrudeArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			GameObject shield1 = ShieldGSkull;
			CustomItem customItem1 = new CustomItem(shield1, fixReference: true, new ItemConfig
			{
				Name = "Vortex, Conservator of the Dead",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBhygshan_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Wishbone",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateBows()
		{
			GameObject bow6 = BowDeepNorth;
			CustomItem customItem6 = new CustomItem(bow6, fixReference: true, new ItemConfig
			{
				Name = "Siren's Song",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExceptionalArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);

			GameObject bow5 = BowMistlands;
			CustomItem customItem5 = new CustomItem(bow5, fixReference: true, new ItemConfig
			{
				Name = "Soulstring",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExcellentArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);

			GameObject bow4 = BowPlains;
			CustomItem customItem4 = new CustomItem(bow4, fixReference: true, new ItemConfig
			{
				Name = "Stryker",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);

			GameObject bow3 = BowMountain;
			CustomItem customItem3 = new CustomItem(bow3, fixReference: true, new ItemConfig
			{
				Name = "Razorwind",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			GameObject bow2 = BowSwamp;
			CustomItem customItem2 = new CustomItem(bow2, fixReference: true, new ItemConfig
			{
				Name = "Sting",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SkeletonBones_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject bow1 = BowBlackForest;
			CustomItem customItem1 = new CustomItem(bow1, fixReference: true, new ItemConfig
			{
				Name = "Hornet",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "GreydwarfHeart_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateSwords()
		{
			GameObject sword6 = SwordAshLands;
			CustomItem customItem6 = new CustomItem(sword6, fixReference: true, new ItemConfig
			{
				Name = "Hellfire",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SurtlingCore",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "ExtraordinaryArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);

			GameObject sword5 = SwordDeepNorth;
			CustomItem customItem5 = new CustomItem(sword5, fixReference: true, new ItemConfig
			{
				Name = "Fate",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 8,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ExceptionalArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);

			GameObject sword4 = SwordMistlands;
			CustomItem customItem4 = new CustomItem(sword4, fixReference: true, new ItemConfig
			{
				Name = "Misery",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SpiderChitin_DoD",
					Amount = 6,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ExcellentArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);

			GameObject sword3 = SwordPlains;
			CustomItem customItem3 = new CustomItem(sword3, fixReference: true, new ItemConfig
			{
				Name = "Light's Bane",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GreatArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "ShamansVessel_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "SuperiorArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);

			GameObject sword2 = SwordSwamp;
			CustomItem customItem2 = new CustomItem(sword2, fixReference: true, new ItemConfig
			{
				Name = "Ghost Reaver",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "SkeletonBones_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject sword1 = SwordMeadows;
			CustomItem customItem1 = new CustomItem(sword1, fixReference: true, new ItemConfig
			{
				Name = "Betrayer",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "CrudeWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "BoarTusk_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BasicArmorKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreateMaces()
		{
			GameObject mace2 = MaceDeepNorth;
			CustomItem customItem2 = new CustomItem(mace2, fixReference: true, new ItemConfig
			{
				Name = "Silence",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "FrozenBone_DoD",
					Amount = 20,
					AmountPerLevel = 20
				},
				new RequirementConfig
				{
					Item = "ExceptionalWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);

			GameObject mace1 = MaceMistlands;
			CustomItem customItem1 = new CustomItem(mace1, fixReference: true, new ItemConfig
			{
				Name = "Abomination",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "SuperiorWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 3
				},
				new RequirementConfig
				{
					Item = "SpiderChitin_DoD",
					Amount = 10,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "ExcellentWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);

		}
		private void CreateWands()
		{
			GameObject wand1 = WandMountains;
			CustomItem customItem1 = new CustomItem(wand1, fixReference: true, new ItemConfig
			{
				Name = "Snowfall",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 2,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "InfusedGemstone_DoD",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "LargeFang_DoD",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "GoodWeaponKit_DoD",
					Amount = 0,
					AmountPerLevel = 1
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void UnloadBundle()
		{
			DoDAssets?.Unload(unloadAllLoadedObjects: false);
		}
		private void OnDestroy()
		{
			Harmony harmony = _harmony;
			if (harmony != null)
			{
				harmony.UnpatchSelf();
			}
		}
	}
}