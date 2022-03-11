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

namespace DoDMonsters
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("asharppen.valheim.spawn_that", BepInDependency.DependencyFlags.HardDependency)]
	internal class dodMonsters : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDMonsters";

		public const string PluginName = "DoOrDieMonsters";

		public const string PluginVersion = "0.5.1";

		private Harmony _harmony;
		public static readonly ManualLogSource DoDLogger = BepInEx.Logging.Logger.CreateLogSource(PluginName);

		//public static GameObject Rylan;
		public static GameObject SkirSandburst;
		public static GameObject Farkas;
		public static GameObject FarkasAlt;
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
		public static GameObject InfusedGemstone;

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

		public static GameObject CBait;
		public static GameObject SkullToken;

		public static GameObject DrakespitFire;
		public static GameObject DrakespitArcane;
		public static GameObject DrakespitFrost;
		public static GameObject DrakespitPoison1;
		public static GameObject DrakespitPoison2;
		public static GameObject DrakespitVoid;

		public static GameObject BhygshanAlt;
		public static Sprite RugBDeer;
		public static Sprite RugDWolf;
		public static Sprite RugFWolf;
		public static GameObject RugDeer;
		public static GameObject RugDire;
		public static GameObject RugForest;

		public ConfigEntry<bool> BossesEnable;
		public ConfigEntry<bool> MonstersEnable;
		public ConfigEntry<bool> BuildablesEnable;

		public AssetBundle DoDAssets;
		internal static ManualLogSource Log;

		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			BossesEnable = base.Config.Bind("Bosses", "Enable", defaultValue: true, new ConfigDescription("Adds Bosses and Altars", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MonstersEnable = base.Config.Bind("Monster Reskins", "Enable", defaultValue: true, new ConfigDescription("Enables Monster Reskins", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BuildablesEnable = base.Config.Bind("Buildables", "Enable", defaultValue: true, new ConfigDescription("Enables Rugs", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
		{
			Log = Logger;
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.DoDMonsters");
			CreateConfigurationValues();
			Debug.Log("DoDMonsters: Loading and Creating Assets");
			LoadBundle();
			LoadDoDAssets();
			if (MonstersEnable.Value == true) {
				CreateDropables();
				CreateMonsterAbilities();
				CreateMonsterItems();
				AddMonsterReskins();
				CreateRugs();
				SpawnerConfigurationManager.OnConfigure += ConfigureBiomeSpawners;
			}
			if (BossesEnable.Value == true) {
				AddBosses();
				AddNewMonsters();
			}
			//ItemManager.OnItemsRegistered += ModMonsterAttackSE;
			UnloadBundle();
		}
		public void LoadBundle()
		{
			DoDAssets = AssetUtils.LoadAssetBundleFromResources("dodmonsters", Assembly.GetExecutingAssembly());
		}
		private void LoadDoDAssets()
		{
			SkullToken = DoDAssets.LoadAsset<GameObject>("SkullToken_DoD");
			RugBDeer = DoDAssets.LoadAsset<Sprite>("BlackDeerRug_Icon_DoD");
			RugDWolf = DoDAssets.LoadAsset<Sprite>("DireWolfRug_Icon_DoD");
			RugFWolf = DoDAssets.LoadAsset<Sprite>("ForestWolfRug_Icon_DoD");
			RugDeer = DoDAssets.LoadAsset<GameObject>("Rug_BlackDeer_DoD");
			RugDire = DoDAssets.LoadAsset<GameObject>("Rug_DireWolf_DoD");
			RugForest = DoDAssets.LoadAsset<GameObject>("Rug_ForestWolf_DoD");
			Debug.Log("DoDMonsters: Materials");
			CBait = DoDAssets.LoadAsset<GameObject>("CarnivorBait_DoD");
			InfusedGemstone = DoDAssets.LoadAsset<GameObject>("InfusedGemstone_DoD");
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

			Debug.Log("DoDMonsters: Bosses");
			BhygshanAlt = DoDAssets.LoadAsset<GameObject>("BhygshanAlt_DoD");
			SkirSandburst = DoDAssets.LoadAsset<GameObject>("SkirSandburst_DoD");
			Farkas = DoDAssets.LoadAsset<GameObject>("Farkas_DoD");
			FarkasAlt = DoDAssets.LoadAsset<GameObject>("Farkas_Alt_DoD");
			Bhygshan = DoDAssets.LoadAsset<GameObject>("Bhygshan_DoD");
			Bitterstump = DoDAssets.LoadAsset<GameObject>("Bitterstump_DoD");
			Rambore = DoDAssets.LoadAsset<GameObject>("Rambore_DoD");

			Debug.Log("DoDMonsters: Monsters");
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

			Debug.Log("DoDMonsters: Monster Attacks");
			DrakespitFire = DoDAssets.LoadAsset<GameObject>("drake_firespit_attack_dod");
			DrakespitArcane = DoDAssets.LoadAsset<GameObject>("drake_arcanespit_attack_dod");
			DrakespitFrost = DoDAssets.LoadAsset<GameObject>("drake_frostspit_attack_dod");
			DrakespitPoison1 = DoDAssets.LoadAsset<GameObject>("drake_poison_attack_dod");
			DrakespitPoison2 = DoDAssets.LoadAsset<GameObject>("drake_poisonspit_attack_dod");
			DrakespitVoid = DoDAssets.LoadAsset<GameObject>("drake_voidspit_attack_dod");
			NPC_NomadAoE_Attack = DoDAssets.LoadAsset<GameObject>("NPC_NomadAoE_Attack_DoD");
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

			Debug.Log("DoDMonsters: Boss Attacks");
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

			Debug.Log("DoDMonsters: Monster Items");
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
			BowCharred = DoDAssets.LoadAsset<GameObject>("Bow_Charred_DoD");

			Debug.Log("DoDMonsters: Trophies");
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

			Debug.Log("DoDMonsters: AoE");
			GameObject AoEHealinng = DoDAssets.LoadAsset<GameObject>("AoE_AuraHealing_DoD");
			GameObject AoEBhygshanMace = DoDAssets.LoadAsset<GameObject>("AoE_BhygshanMace_DoD");
			GameObject AoEBitterHeal = DoDAssets.LoadAsset<GameObject>("AoE_Bitterstump_Heal_DoD");
			GameObject AoEHoT200 = DoDAssets.LoadAsset<GameObject>("AoE_HoT200_DoD");
			GameObject AoEProtect500 = DoDAssets.LoadAsset<GameObject>("AoE_Protection500_DoD");
			GameObject AoESkirHealing = DoDAssets.LoadAsset<GameObject>("AoE_Skir_Nova_DoD");
			GameObject AoESpray = DoDAssets.LoadAsset<GameObject>("AoE_Spray_DoD");
			GameObject AoERootSpawn = DoDAssets.LoadAsset<GameObject>("Bitter_RootSpawn_DoD");
			PrefabManager.Instance.AddPrefab(AoEHealinng);
			PrefabManager.Instance.AddPrefab(AoEBhygshanMace);
			PrefabManager.Instance.AddPrefab(AoEBitterHeal);
			PrefabManager.Instance.AddPrefab(AoEHoT200);
			PrefabManager.Instance.AddPrefab(AoEProtect500);
			PrefabManager.Instance.AddPrefab(AoESkirHealing);
			PrefabManager.Instance.AddPrefab(AoESpray);
			PrefabManager.Instance.AddPrefab(AoERootSpawn);

			Debug.Log("DoDMonsters: Ragdolls");
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
			GameObject FarkasRD = DoDAssets.LoadAsset<GameObject>("Farkas_RD_DoD");
			GameObject FarkasAltRD = DoDAssets.LoadAsset<GameObject>("Farkas_Alt_RD_DoD");
			PrefabManager.Instance.AddPrefab(FarkasAltRD);
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

			Debug.Log("DoDMonsters: Projectiles");
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

			Debug.Log("DoDMonsters: Altars");
			GameObject AltarFarkas = DoDAssets.LoadAsset<GameObject>("AltarFarkas_DoD");
			GameObject AltarSkirSandburst = DoDAssets.LoadAsset<GameObject>("AltarSkirSandburst_DoD");
			GameObject AltarRambore = DoDAssets.LoadAsset<GameObject>("AltarRambone_DoD");
			GameObject AltarBitterstump = DoDAssets.LoadAsset<GameObject>("AltarBitterstump_DoD");
			GameObject AltarBhygshan = DoDAssets.LoadAsset<GameObject>("AltarBhygshan_DoD");
			GameObject AltarFarkasAlt = DoDAssets.LoadAsset<GameObject>("AltarFarkasAlt_DoD");
			PrefabManager.Instance.AddPrefab(AltarFarkas);
			PrefabManager.Instance.AddPrefab(AltarSkirSandburst);
			PrefabManager.Instance.AddPrefab(AltarRambore);
			PrefabManager.Instance.AddPrefab(AltarBitterstump);
			PrefabManager.Instance.AddPrefab(AltarBhygshan);
			PrefabManager.Instance.AddPrefab(AltarFarkasAlt);

			GameObject BhygshanSummon = DoDAssets.LoadAsset<GameObject>("Bhygshan_Spawn_DoD");
			GameObject VoidlingSummon = DoDAssets.LoadAsset<GameObject>("Voidling_Spawn_DoD");
			GameObject ForestWolfSummon = DoDAssets.LoadAsset<GameObject>("ForestWolf_Spawn_DoD");
			PrefabManager.Instance.AddPrefab(VoidlingSummon);
			PrefabManager.Instance.AddPrefab(ForestWolfSummon);
			PrefabManager.Instance.AddPrefab(BhygshanSummon);

			Debug.Log("DoDMonsters: FX");
			GameObject FXSkirProtect = DoDAssets.LoadAsset<GameObject>("FX_Skir_Protect_DoD");
			GameObject FXSkirNova = DoDAssets.LoadAsset<GameObject>("FX_Skir_Nova_DoD");
			GameObject FXBitterRoot = DoDAssets.LoadAsset<GameObject>("FX_Bitter_RootSpawn_DoD");
			GameObject FXBackstab = DoDAssets.LoadAsset<GameObject>("FX_Backstab_DoD");
			GameObject FXCrit = DoDAssets.LoadAsset<GameObject>("FX_Crit_DoD");
			GameObject FXBhygshanFireballExpl = DoDAssets.LoadAsset<GameObject>("FX_Bhygshan_Fireball_Expl_DoD");
			PrefabManager.Instance.AddPrefab(FXSkirProtect);
			PrefabManager.Instance.AddPrefab(FXSkirNova);
			PrefabManager.Instance.AddPrefab(FXBitterRoot);
			PrefabManager.Instance.AddPrefab(FXBackstab);
			PrefabManager.Instance.AddPrefab(FXCrit);
			PrefabManager.Instance.AddPrefab(FXBhygshanFireballExpl);

			Debug.Log("DoDMonsters: SFX");
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
			GameObject SFXWolfIdle = DoDAssets.LoadAsset<GameObject>("SFX_Wolf_Idle_DoD");
			GameObject SFXWolfGetHit = DoDAssets.LoadAsset<GameObject>("SFX_Wolf_GetHit_DoD");
			PrefabManager.Instance.AddPrefab(SFXWolfGetHit);
			PrefabManager.Instance.AddPrefab(SFXWolfIdle);
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

			Debug.Log("DoDMonsters: VFX");
			GameObject VFXArcaneImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_ArcaneImpDeath_DoD");
			GameObject VFXBhygshanSpray = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Spray_DoD");
			GameObject VFXBhygshanBreath = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Breath_DoD");
			GameObject VFXBhygshanAttack = DoDAssets.LoadAsset<GameObject>("VFX_Bhygshan_Attack_DoD");
			GameObject VFXBlocked = DoDAssets.LoadAsset<GameObject>("VFX_Blocked_DoD");
			GameObject VFXFireBoltHit = DoDAssets.LoadAsset<GameObject>("VFX_FireBolt_SurtlingHit_DoD");
			GameObject VFXHitSparks = DoDAssets.LoadAsset<GameObject>("VFX_HitSparks_DoD");
			GameObject VFXIceImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_IceImpDeath_DoD");
			GameObject VFXIceImpHit = DoDAssets.LoadAsset<GameObject>("VFX_IceImpHit_DoD");
			GameObject VFXLivingLavaDeath = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Death_DoD");
			GameObject VFXLivingLavaAttack = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Attack_DoD");
			GameObject VFXLivingLavaHit = DoDAssets.LoadAsset<GameObject>("VFX_LivingLava_Hit_DoD");
			GameObject VFXLivingWaterDeath = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Death_DoD");
			GameObject VFXLivingWaterAttack = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Attack_DoD");
			GameObject VFXLivingWaterHit = DoDAssets.LoadAsset<GameObject>("VFX_LivingWater_Hit_DoD");
			GameObject VFXSkirThrow = DoDAssets.LoadAsset<GameObject>("VFX_Skir_Throw_DoD");
			GameObject VFXSkeletonHit = DoDAssets.LoadAsset<GameObject>("VFX_Skeleton_Hit_DoD");
			GameObject VFXStormImpDeath = DoDAssets.LoadAsset<GameObject>("VFX_StormImpDeath_DoD");
			GameObject VFXStormImpHit = DoDAssets.LoadAsset<GameObject>("VFX_StormImpHit_DoD");
			GameObject VFXVoidImpHit = DoDAssets.LoadAsset<GameObject>("VFX_VoidImpHit_DoD");
			GameObject VFXWolfDeath = DoDAssets.LoadAsset<GameObject>("VFX_Wolf_Death_DoD");
			GameObject VFXWolfHit = DoDAssets.LoadAsset<GameObject>("VFX_Wolf_Hit_DoD");
			PrefabManager.Instance.AddPrefab(VFXBhygshanSpray);
			PrefabManager.Instance.AddPrefab(VFXBhygshanBreath);
			PrefabManager.Instance.AddPrefab(VFXSkirThrow);
			PrefabManager.Instance.AddPrefab(VFXBhygshanAttack);
			PrefabManager.Instance.AddPrefab(VFXSkeletonHit);
			PrefabManager.Instance.AddPrefab(VFXFireBoltHit);
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
		}
		private void CreateDropables()
		{
			Debug.Log("DoDMonsters: CreateDropables");
			GameObject food4 = CBait;
			CustomItem customFood4 = new CustomItem(food4, fixReference: true, new ItemConfig
			{
				Name = "Carnivor Bait",
				Amount = 5,
				CraftingStation = "piece_artisanstation",
				MinStationLevel = 1,
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

			GameObject dropable53 = SkullToken;
			CustomItem customItem53 = new CustomItem(dropable53, fixReference: true);
			ItemManager.Instance.AddItem(customItem53);
		}
		private void CreateMonsterItems()
		{
			Debug.Log("DoDMonsters: CreateMonsterItems");
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
			Debug.Log("DoDMonsters: CreateMonsterAbilities");
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
			Debug.Log("DoDMonsters: AddBosses");
			GameObject gameObject6 = BhygshanAlt;
			CustomPrefab customPrefab6 = new CustomPrefab(gameObject6, true);
			PrefabManager.Instance.AddPrefab(customPrefab6);

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
			Debug.Log("DoDMonsters: AddMonsterReskins");
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
		private void AddNewMonsters()
		{
			Debug.Log("DoDMonsters: AddNewMonsters");
			GameObject monster4 = FarkasAlt;
			CustomPrefab creature4 = new CustomPrefab(monster4, true);
			PrefabManager.Instance.AddPrefab(creature4);
		}
		private void CreateRugs()
		{
			Debug.Log("DoDMonsters: RugDeer");
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

			Debug.Log("DoDMonsters: RugDire");
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

			Debug.Log("DoDMonsters: RugForest");
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
		private void UnloadBundle()
		{
			DoDAssets?.Unload(unloadAllLoadedObjects: false);
		}
		private static void ModMonsterAttackSE()
		{
			Aoe prefab30 = PrefabManager.Cache.GetPrefab<Aoe>("bonemass_aoe");
			Aoe prefab34 = PrefabManager.Cache.GetPrefab<Aoe>("shaman_attack_aoe");
			Aoe prefab35 = PrefabManager.Cache.GetPrefab<Aoe>("blob_aoe");
			Aoe prefab36 = PrefabManager.Cache.GetPrefab<Aoe>("bee_aoe");
			Projectile prefab38 = PrefabManager.Cache.GetPrefab<Projectile>("GoblinShaman_projectile_fireball");
			Projectile prefab29 = PrefabManager.Cache.GetPrefab<Projectile>("gdking_root_projectile");
			Projectile prefab33 = PrefabManager.Cache.GetPrefab<Projectile>("troll_throw_projectile");
			prefab29.m_statusEffect = "SE_Weak_DoD";
			prefab30.m_statusEffect = "SE_Infected_DoD";
			prefab33.m_statusEffect = "SE_Slow_DoD";
			prefab34.m_statusEffect = "SE_Infected_DoD";
			prefab35.m_statusEffect = "SE_Diseased_DoD";
			prefab36.m_statusEffect = "SE_Infected_DoD";
			prefab38.m_statusEffect = "SE_Blistered_DoD";

			/*ItemDrop prefab28 = PrefabManager.Cache.GetPrefab<ItemDrop>("Eikthyr_stomp");
			ItemDrop prefab31 = PrefabManager.Cache.GetPrefab<ItemDrop>("dragon_coldbreath");
			ItemDrop prefab32 = PrefabManager.Cache.GetPrefab<ItemDrop>("GoblinKing_Nova");
			ItemDrop prefab37 = PrefabManager.Cache.GetPrefab<ItemDrop>("stonegolem_attack_doublesmash");
			var SE_Blistered_DoD = ObjectDB.instance.GetStatusEffect("SE_Blistered_DoD");
			var SE_Frostbitten_DoD = ObjectDB.instance.GetStatusEffect("SE_Frostbitten_DoD");
			var SE_Frostbite_DoD = ObjectDB.instance.GetStatusEffect("SE_Frostbite_DoD");
			var SE_Weak_DoD = ObjectDB.instance.GetStatusEffect("SE_Weak_DoD");
			prefab28.m_itemData.m_shared.m_attackStatusEffect = SE_Blistered_DoD;
			prefab31.m_itemData.m_shared.m_attackStatusEffect = SE_Frostbitten_DoD;
			prefab32.m_itemData.m_shared.m_attackStatusEffect = SE_Blistered_DoD;
			prefab37.m_itemData.m_shared.m_attackStatusEffect = SE_Weak_DoD;*/

			ItemManager.OnItemsRegistered -= ModMonsterAttackSE;
		}
		public static void ConfigureBiomeSpawners(ISpawnerConfigurationCollection config)
		{
			try
			{
				ConfigureWorldSpawners(config);
			}
			catch (Exception e)
			{
				System.Console.WriteLine($"Cepera made something go horribly wrong: {e.Message}\nStackTrace:\n{e.StackTrace}");
			}
		}
		private static void ConfigureWorldSpawners(ISpawnerConfigurationCollection config)
		{
			try
			{
				config.ConfigureWorldSpawner(21_023)
					.SetPrefabName("Ghost_Ice_DoD")
					.SetTemplateName("Ghost Ice")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp)
					.SetSpawnChance(75)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Misty", "DeepForest Mist")
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					.SetConditionDistanceToCenter(1500)
					.SetMinDistanceToOther(75)
					;
				config.ConfigureWorldSpawner(21_022)
					.SetPrefabName("Ghost_White_DoD")
					.SetTemplateName("Ghost White")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp)
					.SetSpawnChance(75)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Misty", "DeepForest Mist")
					.SetConditionRequiredGlobalKey("defeated_gdking")
					.SetConditionDistanceToCenter(1500)
					.SetMinDistanceToOther(75)
					;
				config.ConfigureWorldSpawner(21_021)
					.SetPrefabName("LavaGolem_DoD")
					.SetTemplateName("Lava Golem")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_020)
					.SetPrefabName("CharredRemains_DoD")
					.SetTemplateName("Charred Remains")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(2)
					.SetPackSizeMax(4)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_019)
					.SetPrefabName("LivingLava_DoD")
					.SetTemplateName("Living Lava")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_018)
					.SetPrefabName("ObsidianGolem_DoD")
					.SetTemplateName("Obsidian Golem")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(360))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_017)
					.SetPrefabName("FlameDrake_DoD")
					.SetTemplateName("Flame Drake")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_016)
					.SetPrefabName("GreaterSurtling_DoD")
					.SetTemplateName("Greater Surtling")
					.SetConditionBiomes(Heightmap.Biome.AshLands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(3)
					.SetPackSizeMax(5)
					.SetMaxSpawned(4)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(75)
					;
				config.ConfigureWorldSpawner(21_015)
					.SetPrefabName("IceGolem_DoD")
					.SetTemplateName("Ice Golem")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_014)
					.SetPrefabName("FrozenBones_DoD")
					.SetTemplateName("Awakened")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(2)
					.SetPackSizeMax(6)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_013)
					.SetPrefabName("DireWolf_DoD")
					.SetTemplateName("Dire Wolf")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(200))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_012)
					.SetPrefabName("IceDrake_DoD")
					.SetTemplateName("Ice Drake")
					.SetConditionBiomes(Heightmap.Biome.DeepNorth)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(200))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(5)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_011)
					.SetPrefabName("BlackStag_DoD")
					.SetTemplateName("Black Stag")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(1)
					.SetMaxSpawned(1)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(150)
					;
				config.ConfigureWorldSpawner(21_010)
					.SetPrefabName("BlackDeer_DoD")
					.SetTemplateName("Black Deer")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(3)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(75)
					;
				config.ConfigureWorldSpawner(21_009)
					.SetPrefabName("Vilefang_DoD")
					.SetTemplateName("Vilefang")
					.SetConditionBiomes(Heightmap.Biome.Mistlands)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(300))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_008)
					.SetPrefabName("DarknessDrake_DoD")
					.SetTemplateName("Dark Drake")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Mountain, Heightmap.Biome.Plains)
					.SetSpawnChance(8)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(1)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_moder")
					.SetMinDistanceToOther(100)
					.SetConditionDistanceToCenter(3000)
					.SetModifierFaction(Character.Faction.Boss)
					;
				config.ConfigureWorldSpawner(21_007)
					.SetPrefabName("LivingWater_DoD")
					.SetTemplateName("Living Water")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest, Heightmap.Biome.Swamp, Heightmap.Biome.Plains, Heightmap.Biome.Mistlands)
					.SetSpawnChance(50)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Misty", "ThunderStorm", "Rain", "LightRain")
					.SetConditionRequiredGlobalKey("defeated_bonemass")
					.SetConditionDistanceToCenter(1500)
					.SetMinDistanceToOther(50)
					.SetModifierFaction(Character.Faction.Boss)
					;
				config.ConfigureWorldSpawner(21_006)
					.SetPrefabName("Voidling_DoD")
					.SetTemplateName("Voidling")
					.SetConditionBiomes(Heightmap.Biome.Plains)
					.SetSpawnChance(75)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(2)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Misty", "ThunderStorm")
					.SetConditionDistanceToCenter(1500)
					;
				config.ConfigureWorldSpawner(21_005)
					.SetPrefabName("Frostling_DoD")
					.SetTemplateName("Frostling")
					.SetConditionBiomes(Heightmap.Biome.Mountain)
					.SetSpawnChance(75)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(2)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Twilight_SnowStorm", "SnowStorm")
					.SetConditionDistanceToCenter(1500)
					;
				config.ConfigureWorldSpawner(21_004)
					.SetPrefabName("FrozenBones_DoD")
					.SetTemplateName("Awakened")
					.SetConditionBiomes(Heightmap.Biome.Mountain)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_dravennox")
					.SetMinDistanceToOther(100)
					.SetConditionDistanceToCenter(3000)
					;
				config.ConfigureWorldSpawner(21_003)
					.SetPrefabName("SkeletonR_DoD")
					.SetTemplateName("SkeletonRed")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetSpawnDuringDay(false)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_002)
					.SetPrefabName("SkeletonG_DoD")
					.SetTemplateName("SkeletonGreen")
					.SetConditionBiomes(Heightmap.Biome.Swamp)
					.SetSpawnChance(15)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetSpawnDuringNight(false)
					.SetMinDistanceToOther(100)
					;
				config.ConfigureWorldSpawner(21_001)
					.SetPrefabName("Stormling_DoD")
					.SetTemplateName("Stormling")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest)
					.SetSpawnChance(75)
					.SetSpawnInterval(TimeSpan.FromSeconds(180))
					.SetPackSizeMin(2)
					.SetPackSizeMax(3)
					.SetMaxSpawned(3)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionEnvironments("Thunderstorm")
					.SetConditionDistanceToCenter(1500)
					;
				config.ConfigureWorldSpawner(21_000)
					.SetPrefabName("ForestWolf_DoD")
					.SetTemplateName("ForestWolf")
					.SetConditionBiomes(Heightmap.Biome.Meadows, Heightmap.Biome.BlackForest)
					.SetSpawnChance(10)
					.SetSpawnInterval(TimeSpan.FromSeconds(600))
					.SetPackSizeMin(1)
					.SetPackSizeMax(2)
					.SetMaxSpawned(2)
					.SetSpawnAtDistanceToPlayerMin(75)
					.SetSpawnAtDistanceToPlayerMax(125)
					.SetConditionRequiredGlobalKey("defeated_dragon")
					.SetMinDistanceToOther(100)
					.SetConditionDistanceToCenter(3000)
					;
			}
			catch (Exception e)
			{
				Log.LogError(e);
			}
		}

		/*private void ModPlayer()
		{
			var prefab2 = ObjectDB.instance.GetItemPrefab("MyItem");
			Player prefab1 = PrefabManager.Cache.GetPrefab<Player>("Player");
			prefab1.m_defaultItems.AddItem<GameObject>(prefab2);
			ItemManager.OnItemsRegistered -= ModPlayer;
		}*/
		/*[HarmonyPatch(typeof(OfferingBowl), "Awake")]
		public static class AlterOfferBowlAwake
		{
			public static void Prefix(OfferingBowl __instance)
			{
				if (__instance == null) return;
				if (ZNetScene.instance.GetPrefab("AltarLaughingRylan_DoD"))
				{
					GameObject prefab = PrefabManager.Cache.GetPrefab<GameObject>("AltarLaughingRylan_DoD");
					OfferingBowl componentInChildren = prefab.GetComponentInChildren<OfferingBowl>();
					componentInChildren.m_bossPrefab = ZNetScene.instance.GetPrefab("RRRN_LaughingRylan_DoD").gameObject;

					Jotunn.Logger.LogInfo("Updated Altars");
				}
				else
				{
					Debug.LogError("Error offering bowl didn't contain offer");
				}
			}
		}*/
	}
}