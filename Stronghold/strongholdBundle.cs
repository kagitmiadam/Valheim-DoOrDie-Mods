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

namespace Stronghold
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class strongholdBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.Stronghold";

		public const string PluginName = "Stronghold";

		public const string PluginVersion = "0.0.1";

		public AssetBundle StrongholdAssets;
		private Harmony _harmony;

		public static GameObject piece1;
		public static GameObject piece2;
		public static GameObject piece3;
		public static GameObject piece4;
		public static GameObject piece5;
		public static GameObject piece6;
		public static GameObject piece7;
		public static GameObject piece8;

		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		private void Awake()
		{
			Debug.Log("Stronghold: Loading and Creating Assets");
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.Stronghold");
			LoadBundle();
			LoadAssets();
			CreatePieces();
			UnloadBundle();
		}
		public void LoadAssets()
		{
			piece1 = StrongholdAssets.LoadAsset<GameObject>("SHGateHouse");
			piece2 = StrongholdAssets.LoadAsset<GameObject>("SHWallMusteringHall");
			piece3 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorCenter");
			piece4 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorCorner");
			piece5 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorJunction");
			piece6 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorWithLadder");
			piece7 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorWithNest");
			piece8 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorWithNestCapped");
		}
		public void LoadBundle()
		{
			StrongholdAssets = AssetUtils.LoadAssetBundleFromResources("stronghold", Assembly.GetExecutingAssembly());
		}
		private void CreatePieces()
		{
			Debug.Log("Stronghold: SHWallOpenTwoFloorWithNestCapped");
			var customPiece8 = new CustomPiece(piece8, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece8);
			Debug.Log("Stronghold: SHWallOpenTwoFloorWithNest");
			var customPiece7 = new CustomPiece(piece7, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece7);
			Debug.Log("Stronghold: SHWallOpenTwoFloorWithLadder");
			var customPiece6 = new CustomPiece(piece6, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece6);
			Debug.Log("Stronghold: SHTowerSquareTwoFloorJunction");
			var customPiece5 = new CustomPiece(piece5, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece5);
			Debug.Log("Stronghold: SHTowerSquareTwoFloorCorner");
			var customPiece4 = new CustomPiece(piece4, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece4);
			Debug.Log("Stronghold: SHTowerSquareTwoFloorCenter");
			var customPiece3 = new CustomPiece(piece3, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece3);
			Debug.Log("Stronghold: SHWallMusteringHall");
			var customPiece2 = new CustomPiece(piece2, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			Debug.Log("Stronghold: SHGateHouse");
			var customPiece1 = new CustomPiece(piece1, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
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
			PieceManager.Instance.AddPiece(customPiece1);
		}
		private void UnloadBundle()
		{
			StrongholdAssets?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
