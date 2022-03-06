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
		public static GameObject SFXDogHit;
		public static GameObject SFXDogGetHit;
		public static GameObject SFXDogAlert;
		public static GameObject SFXDogIdle;
		public static GameObject SFXDogDeath;
		public static GameObject SFXDogPet;
		public static GameObject SFXDogSoothe;
		public static GameObject SFXDogTame;
		public static GameObject SFXDogBirth;

		public static GameObject Dog1;
		public static GameObject Dog2;
		public static GameObject Dog3;
		public static GameObject Dog4;
		public static GameObject DogBite;

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
			Dog1 = PetsBundle.LoadAsset<GameObject>("Animal_Dog1_CD");
			Dog2 = PetsBundle.LoadAsset<GameObject>("Animal_Dog2_CD");
			Dog3 = PetsBundle.LoadAsset<GameObject>("Animal_Dog3_CD");
			Dog4 = PetsBundle.LoadAsset<GameObject>("Animal_Dog4_CD");
			Cat1 = PetsBundle.LoadAsset<GameObject>("Animal_Cat1_CD");
			Cat2 = PetsBundle.LoadAsset<GameObject>("Animal_Cat2_CD");
			Cat3 = PetsBundle.LoadAsset<GameObject>("Animal_Cat3_CD");
			Kitten1 = PetsBundle.LoadAsset<GameObject>("Animal_Cat1_Kit_CD");
			Kitten2 = PetsBundle.LoadAsset<GameObject>("Animal_Cat2_Kit_CD");
			Kitten3 = PetsBundle.LoadAsset<GameObject>("Animal_Cat3_Kit_CD");
			CatBite = PetsBundle.LoadAsset<GameObject>("Cat_Bite_CD");
			DogBite = PetsBundle.LoadAsset<GameObject>("Dog_Bite_CD");

			SFXCatHit = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Hit_CD");
			SFXCatGetHit = PetsBundle.LoadAsset<GameObject>("SFX_Cat_GetHit_CD");
			SFXCatAlert = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Alert_CD");
			SFXCatIdle = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Idle_CD");
			SFXCatDeath = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Die_CD");
			SFXCatPet = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Pet_CD");
			SFXCatSoothe = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Soothe_CD");
			SFXCatTame = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Tame_CD");
			SFXCatBirth = PetsBundle.LoadAsset<GameObject>("SFX_Cat_Birth_CD");

			SFXDogHit = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Hit_CD");
			SFXDogGetHit = PetsBundle.LoadAsset<GameObject>("SFX_Dog_GetHit_CD");
			SFXDogAlert = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Alert_CD");
			SFXDogIdle = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Idle_CD");
			SFXDogDeath = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Die_CD");
			SFXDogPet = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Pet_CD");
			SFXDogSoothe = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Soothe_CD");
			SFXDogTame = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Tame_CD");
			SFXDogBirth = PetsBundle.LoadAsset<GameObject>("SFX_Dog_Birth_CD");

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

			CustomPrefab sfx10 = new CustomPrefab(SFXDogHit, false);
			PrefabManager.Instance.AddPrefab(sfx10);
			CustomPrefab sfx11 = new CustomPrefab(SFXDogGetHit, false);
			PrefabManager.Instance.AddPrefab(sfx11);
			CustomPrefab sfx12 = new CustomPrefab(SFXDogAlert, false);
			PrefabManager.Instance.AddPrefab(sfx12);
			CustomPrefab sfx13 = new CustomPrefab(SFXDogIdle, false);
			PrefabManager.Instance.AddPrefab(sfx13);
			CustomPrefab sfx14 = new CustomPrefab(SFXDogDeath, false);
			PrefabManager.Instance.AddPrefab(sfx14);
			CustomPrefab sfx15 = new CustomPrefab(SFXDogPet, false);
			PrefabManager.Instance.AddPrefab(sfx15);
			CustomPrefab sfx16 = new CustomPrefab(SFXDogSoothe, false);
			PrefabManager.Instance.AddPrefab(sfx16);
			CustomPrefab sfx17 = new CustomPrefab(SFXDogTame, false);
			PrefabManager.Instance.AddPrefab(sfx17);
			CustomPrefab sfx18 = new CustomPrefab(SFXDogBirth, false);
			PrefabManager.Instance.AddPrefab(sfx18);

			GameObject VFXGetHit = PetsBundle.LoadAsset<GameObject>("VFX_Blood_Hit_CD");
			GameObject VFXDeath = PetsBundle.LoadAsset<GameObject>("VFX_Animal_Death_CD");
			GameObject VFXPoof = PetsBundle.LoadAsset<GameObject>("VFX_Corpse_Destruction_CD");
			PrefabManager.Instance.AddPrefab(VFXGetHit);
			PrefabManager.Instance.AddPrefab(VFXDeath);
			PrefabManager.Instance.AddPrefab(VFXPoof);

			GameObject attack1 = CatBite;
			CustomPrefab catbite1 = new CustomPrefab(attack1, true);
			PrefabManager.Instance.AddPrefab(catbite1);
			GameObject attack2 = DogBite;
			CustomPrefab dogbite1 = new CustomPrefab(attack2, true);
			PrefabManager.Instance.AddPrefab(dogbite1);
		}
		private void AddNewAnimals()
		{
			GameObject animal1 = Cat1;
			CustomPrefab pet1 = new CustomPrefab(animal1, true);
			PrefabManager.Instance.AddPrefab(pet1);
			GameObject animal2 = Cat2;
			CustomPrefab pet2 = new CustomPrefab(animal2, true);
			PrefabManager.Instance.AddPrefab(pet2);
			GameObject animal3 = Cat3;
			CustomPrefab pet3 = new CustomPrefab(animal3, true);
			PrefabManager.Instance.AddPrefab(pet3);
			GameObject animal4 = Kitten1;
			CustomPrefab pet4 = new CustomPrefab(animal4, true);
			PrefabManager.Instance.AddPrefab(pet4);
			GameObject animal5 = Kitten2;
			CustomPrefab pet5 = new CustomPrefab(animal5, true);
			PrefabManager.Instance.AddPrefab(pet5);
			GameObject animal6 = Kitten3;
			CustomPrefab pet6 = new CustomPrefab(animal6, true);
			PrefabManager.Instance.AddPrefab(pet6);
			GameObject animal7 = Dog1;
			CustomPrefab pet7 = new CustomPrefab(animal7, true);
			PrefabManager.Instance.AddPrefab(pet7);
			GameObject animal8 = Dog2;
			CustomPrefab pet8 = new CustomPrefab(animal8, true);
			PrefabManager.Instance.AddPrefab(pet8);
			GameObject animal9 = Dog3;
			CustomPrefab pet9 = new CustomPrefab(animal9, true);
			PrefabManager.Instance.AddPrefab(pet9);
			GameObject animal10 = Dog4;
			CustomPrefab pet10 = new CustomPrefab(animal10, true);
			PrefabManager.Instance.AddPrefab(pet10);
		}
		private void FixSFX()
		{
			GameObject sfxfab1 = ZNetScene.instance.GetPrefab("SFX_Cat_Hit_CD");
			try
			{
				sfxfab1.GetComponent<AudioSource>().outputAudioMixerGroup =
					AudioMan.instance.m_ambientMixer;
			}
			catch
			{
				Debug.LogWarning("CatsAndDogs: SFX Fix Failed");
			}
		}
		private void UnloadBundle()
		{
			PetsBundle?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
