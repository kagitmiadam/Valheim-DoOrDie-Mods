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

namespace ChickenCoopBA
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.rockerkitten.boneappetit", BepInDependency.DependencyFlags.HardDependency)]
	internal class ChickenCoopBA : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.ChickenCoopBA";

		public const string PluginName = "ChickenCoopBA";

		public const string PluginVersion = "0.0.1";

		public static bool isModded = true;

		public static GameObject chickenCoop;
		public static GameObject chickenItem;

		public AssetBundle CoopBundle;
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
			AddItem();
			AddCoop();
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.ChickenCoopBA");
		}
		public void LoadBundle()
		{
			CoopBundle = AssetUtils.LoadAssetBundleFromResources("coopba", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			chickenCoop = CoopBundle.LoadAsset<GameObject>("ChickenCoop_BAA");
			chickenItem = CoopBundle.LoadAsset<GameObject>("ChickenItem_BAA");
		}
		private void AddItem()
		{
			GameObject dropable1 = chickenItem;
			CustomItem customItem1 = new CustomItem(dropable1, true);
			ItemManager.Instance.AddItem(customItem1);
		}
		private void AddCoop()
		{
			var customPiece1 = new CustomPiece(chickenCoop, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Misc",
				Requirements = new RequirementConfig[3]
				{
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
		private void UnloadBundle()
		{
			CoopBundle?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
