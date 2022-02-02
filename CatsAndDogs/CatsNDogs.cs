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

namespace CatsAndDogs
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class CatsNDogs : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.CatsAndDogs";

		public const string PluginName = "CatsAndDogs";

		public const string PluginVersion = "0.0.1";

		public static GameObject Cat1;
		public static GameObject Cat2;
		public static GameObject Cat3;
		public static GameObject Kitten1;
		public static GameObject Kitten2;
		public static GameObject Kitten3;
		public static GameObject CatBite;
		public static GameObject SFXCatHit;
		public static GameObject SFXCatGetHit;
		public static GameObject SFXCatAlert;
		public static GameObject SFXCatIdle;
		public static GameObject SFXCatDeath;
		public static GameObject SFXCatPet;
		public static GameObject SFXCatSoothe;
		public static GameObject SFXCatTame;
		public static GameObject SFXCatBirth;

		public AssetBundle PetsBundle;
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
			//FixSFX();
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.CatsAndDogs");
		}
		public void LoadBundle()
		{
			PetsBundle = AssetUtils.LoadAssetBundleFromResources("pets", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			Cat1 = PetsBundle.LoadAsset<GameObject>("Animal_Cat1_CD");
			Cat2 = PetsBundle.LoadAsset<GameObject>("Animal_Cat2_CD");
			Cat3 = PetsBundle.LoadAsset<GameObject>("Animal_Cat3_CD");
			Kitten1 = PetsBundle.LoadAsset<GameObject>("Animal_Cat1_Kit_CD");
			Kitten2 = PetsBundle.LoadAsset<GameObject>("Animal_Cat2_Kit_CD");
			Kitten3 = PetsBundle.LoadAsset<GameObject>("Animal_Cat3_Kit_CD");
			CatBite = PetsBundle.LoadAsset<GameObject>("Cat_Bite_CD");

			SFXCatHit = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Hit_CD");
			SFXCatGetHit = PetsBundle.LoadAsset<GameObject>("SFX_Cat_GetHit_CD");
			SFXCatAlert = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Alert_CD");
			SFXCatIdle = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Idle_CD");
			SFXCatDeath = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Die_CD");
			SFXCatPet = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Pet_CD");
			SFXCatSoothe = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Soothe_CD");
			SFXCatTame = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Tame_CD");
			SFXCatBirth = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Birth_CD");
			CustomPrefab sfx1 = new CustomPrefab(SFXCatHit, false);
			PrefabManager.Instance.AddPrefab(sfx1);
			CustomPrefab sfx2 = new CustomPrefab(SFXCatGetHit, false);
			PrefabManager.Instance.AddPrefab(sfx2);
			CustomPrefab sfx3 = new CustomPrefab(SFXCatAlert, false);
			PrefabManager.Instance.AddPrefab(sfx3);
			CustomPrefab sfx4 = new CustomPrefab(SFXCatIdle, false);
			PrefabManager.Instance.AddPrefab(sfx4);
			CustomPrefab sfx5 = new CustomPrefab(SFXCatDeath, false);
			PrefabManager.Instance.AddPrefab(sfx5);
			CustomPrefab sfx6 = new CustomPrefab(SFXCatPet, false);
			PrefabManager.Instance.AddPrefab(sfx6);
			CustomPrefab sfx7 = new CustomPrefab(SFXCatSoothe, false);
			PrefabManager.Instance.AddPrefab(sfx7);
			CustomPrefab sfx8 = new CustomPrefab(SFXCatTame, false);
			PrefabManager.Instance.AddPrefab(sfx8);
			CustomPrefab sfx9 = new CustomPrefab(SFXCatBirth, false);
			PrefabManager.Instance.AddPrefab(sfx9);

			GameObject VFXGetHit = PetsBundle.LoadAsset<GameObject>("VFX_Blood_Hit_CD");
			GameObject VFXDeath = PetsBundle.LoadAsset<GameObject>("VFX_Animal_Death_CD");
			GameObject VFXPoof = PetsBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_CD");
			PrefabManager.Instance.AddPrefab(VFXGetHit);
			PrefabManager.Instance.AddPrefab(VFXDeath);
			PrefabManager.Instance.AddPrefab(VFXPoof);

			GameObject attack1 = CatBite;
			CustomPrefab catbite1 = new CustomPrefab(attack1, true);
			PrefabManager.Instance.AddPrefab(catbite1);
		}
		private void AddNewAnimals()
		{
			GameObject animal21 = Cat1;
			CustomPrefab pet1 = new CustomPrefab(animal21, true);
			PrefabManager.Instance.AddPrefab(pet1);
			GameObject animal22 = Cat2;
			CustomPrefab pet2 = new CustomPrefab(animal22, true);
			PrefabManager.Instance.AddPrefab(pet2);
			GameObject animal23 = Cat3;
			CustomPrefab pet3 = new CustomPrefab(animal23, true);
			PrefabManager.Instance.AddPrefab(pet3);
			GameObject animal24 = Kitten1;
			CustomPrefab pet4 = new CustomPrefab(animal24, true);
			PrefabManager.Instance.AddPrefab(pet4);
			GameObject animal25 = Kitten2;
			CustomPrefab pet5 = new CustomPrefab(animal25, true);
			PrefabManager.Instance.AddPrefab(pet5);
			GameObject animal26 = Kitten3;
			CustomPrefab pet6 = new CustomPrefab(animal26, true);
			PrefabManager.Instance.AddPrefab(pet6);
		}
		private void FixSFX()
		{
			Debug.Log("CnD SFX Fix: Start");
			AudioSource prefab1 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Hit_CD");
			AudioSource prefab2 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_GetHit_CD");
			AudioSource prefab3 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Alert_CD");
			AudioSource prefab4 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Idle_CD");
			AudioSource prefab5 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Die_CD");
			AudioSource prefab6 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Pet_CD");
			AudioSource prefab7 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Soothe_CD");
			AudioSource prefab8 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Tame_CD");
			AudioSource prefab9 = PrefabManager.Cache.GetPrefab<AudioSource>("SFX_Cat_Birth_CD");
			Debug.Log("CnD SFX Fix: Find Mixer");
			var ambientGroup = AudioMan.instance.m_ambientMixer;
			Debug.Log("CnD SFX Fix: Set Mixer");
			prefab1.outputAudioMixerGroup = ambientGroup;
			prefab2.outputAudioMixerGroup = ambientGroup;
			prefab3.outputAudioMixerGroup = ambientGroup;
			prefab4.outputAudioMixerGroup = ambientGroup;
			prefab5.outputAudioMixerGroup = ambientGroup;
			prefab6.outputAudioMixerGroup = ambientGroup;
			prefab7.outputAudioMixerGroup = ambientGroup;
			prefab8.outputAudioMixerGroup = ambientGroup;
			prefab9.outputAudioMixerGroup = ambientGroup;
		}
		private void UnloadBundle()
		{
			PetsBundle?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
