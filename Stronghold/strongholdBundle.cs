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

		public const string PluginVersion = "0.0.8";

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
		public static GameObject piece9;
		public static GameObject piece10;
		public static GameObject piece11;
		public static GameObject piece12;
		public static GameObject piece13;
		public static GameObject piece14;
		public static GameObject piece15;
		public static GameObject piece16;
		public static GameObject piece17;
		public static GameObject piece18;
		public static GameObject piece19;
		public static GameObject piece20;
		public static GameObject piece21;
		public static GameObject piece22;
		public static GameObject piece23;
		public static GameObject piece24;
		public static GameObject piece25;
		public static GameObject piece26;
		public static GameObject piece27;
		public static GameObject piece28;
		public static GameObject piece29;
		public static GameObject piece30;
		public static GameObject piece31;
		public static GameObject piece32;
		public static GameObject piece33;
		public static GameObject piece34;
		public static GameObject piece35;
		public void Awake()
		{
			//Debug.Log("Stronghold: Loading and Creating Assets");
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.Stronghold");
			LoadBundle();
			LoadAssets();
			CreatePieces();
			UnloadBundle();
		}
		public void LoadBundle()
		{
			StrongholdAssets = AssetUtils.LoadAssetBundleFromResources("stronghold", Assembly.GetExecutingAssembly());
		}
		public void LoadAssets()
		{
			piece1 = StrongholdAssets.LoadAsset<GameObject>("SHGateHouse");
			piece2 = StrongholdAssets.LoadAsset<GameObject>("SHWallMusteringHall");
			piece3 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorCenter");
			piece4 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorCorner");
			piece5 = StrongholdAssets.LoadAsset<GameObject>("SHTowerSquareTwoFloorJunction");
			piece6 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorCapped");
			piece7 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorWithNest");
			piece8 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloorWithNestCapped");
			piece9 = StrongholdAssets.LoadAsset<GameObject>("SHWallOpenTwoFloor");
			piece10 = StrongholdAssets.LoadAsset<GameObject>("SHEnclosedTower");
			piece11 = StrongholdAssets.LoadAsset<GameObject>("SHBunkhouse");
			piece12 = StrongholdAssets.LoadAsset<GameObject>("SHWell");
			piece13 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallCovered");
			piece14 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallOpenCapped");
			piece15 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallOpen");
			piece16 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallTowerSquareCenter");
			piece17 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallTowerTransition");
			piece18 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallTowerRound");
			piece19 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallGate");
			piece20 = StrongholdAssets.LoadAsset<GameObject>("SHWatchtower");
			piece21 = StrongholdAssets.LoadAsset<GameObject>("SHTowerRoundWallEnd");
			piece22 = StrongholdAssets.LoadAsset<GameObject>("SHOuterWallCoverdCapped");
			piece23 = StrongholdAssets.LoadAsset<GameObject>("SHWallInnerArch");
			piece24 = StrongholdAssets.LoadAsset<GameObject>("SHWallInnerPillar");
			piece25 = StrongholdAssets.LoadAsset<GameObject>("SHWallInnerPlain");
			piece26 = StrongholdAssets.LoadAsset<GameObject>("SHWallInnerPosh");
			piece27 = StrongholdAssets.LoadAsset<GameObject>("SHHouseSmall");
			piece28 = StrongholdAssets.LoadAsset<GameObject>("SHHouseMedium");
			piece29 = StrongholdAssets.LoadAsset<GameObject>("SHHouseLarge");
			piece30 = StrongholdAssets.LoadAsset<GameObject>("SHHayBarn");
			piece31 = StrongholdAssets.LoadAsset<GameObject>("SHOldBarn");
			piece32 = StrongholdAssets.LoadAsset<GameObject>("SHStorageBarn");
			piece33 = StrongholdAssets.LoadAsset<GameObject>("SHMainHall");
			piece34 = StrongholdAssets.LoadAsset<GameObject>("SHWellGround");
			piece35 = StrongholdAssets.LoadAsset<GameObject>("SHWellGroundRound");

			/*door1 = StrongholdAssets.LoadAsset<GameObject>("SHArchDoor");
			door2 = StrongholdAssets.LoadAsset<GameObject>("SHArchDoorMH");
			door3 = StrongholdAssets.LoadAsset<GameObject>("SHBarnDoor");
			door4 = StrongholdAssets.LoadAsset<GameObject>("SHBarnDoorsArch");
			door5 = StrongholdAssets.LoadAsset<GameObject>("SHBarnDoorTall");
			door6 = StrongholdAssets.LoadAsset<GameObject>("SHBunkhouseDoor");
			door7 = StrongholdAssets.LoadAsset<GameObject>("SHHouseDoor");
			door8 = StrongholdAssets.LoadAsset<GameObject>("SHLattice");
			door9 = StrongholdAssets.LoadAsset<GameObject>("SHMainHallDoor");
			door10 = StrongholdAssets.LoadAsset<GameObject>("SHMusterHallDoor");
			door11 = StrongholdAssets.LoadAsset<GameObject>("SHPortcullis");
			door12 = StrongholdAssets.LoadAsset<GameObject>("SHSmallDoor");
			door13 = StrongholdAssets.LoadAsset<GameObject>("SHSTowerDoor");
			door14 = StrongholdAssets.LoadAsset<GameObject>("SHSTowerDoorWide");
			door15 = StrongholdAssets.LoadAsset<GameObject>("SHTowerDoorTall");
			door16 = StrongholdAssets.LoadAsset<GameObject>("SHTrapDoor");*/
		}
		private void CreatePieces()
		{
			//Debug.Log("Stronghold: SHWellGround");
			var customPiece35 = new CustomPiece(piece35, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 25,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece35);
			//Debug.Log("Stronghold: SHWellGroundRound");
			var customPiece34 = new CustomPiece(piece34, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 25,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece34);
			//Debug.Log("Stronghold: SHMainHall");
			var customPiece33 = new CustomPiece(piece33, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 400,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 200,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece33);
			//Debug.Log("Stronghold: SHStorageBarn");
			var customPiece32 = new CustomPiece(piece32, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 250,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 50,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 75,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece32);
			//Debug.Log("Stronghold: SHOldBarn");
			var customPiece31 = new CustomPiece(piece31, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 40,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 225,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece31);
			//Debug.Log("Stronghold: SHHayBarn");
			var customPiece30 = new CustomPiece(piece30, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 50,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 175,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece30);
			//Debug.Log("Stronghold: SHHouseLarge");
			var customPiece29 = new CustomPiece(piece29, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 375,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 200,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece29);
			//Debug.Log("Stronghold: SHHouseMedium");
			var customPiece28 = new CustomPiece(piece28, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 300,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 75,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 150,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece28);
			//Debug.Log("Stronghold: SHHouseSmall");
			var customPiece27 = new CustomPiece(piece27, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 200,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 50,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 100,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece27);
			//Debug.Log("Stronghold: SHWallInnerPosh");
			var customPiece26 = new CustomPiece(piece26, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 15,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 10,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece26);
			//Debug.Log("Stronghold: SHWallInnerPlain");
			var customPiece25 = new CustomPiece(piece25, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 20,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece25);
			//Debug.Log("Stronghold: SHWallInnerPillar");
			var customPiece24 = new CustomPiece(piece24, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 20,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece24);
			//Debug.Log("Stronghold: SHWallInnerArch");
			var customPiece23 = new CustomPiece(piece23, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 50,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece23);
			//Debug.Log("Stronghold: SHOuterWallCoverdCapped");
			var customPiece22 = new CustomPiece(piece22, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 200,
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
			PieceManager.Instance.AddPiece(customPiece22);
			//Debug.Log("Stronghold: SHTowerRoundWallEnd");
			var customPiece21 = new CustomPiece(piece21, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 110,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 80,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 50,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece21);
			//Debug.Log("Stronghold: SHWatchtower");
			var customPiece20 = new CustomPiece(piece20, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 110,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 80,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece20);
			//Debug.Log("Stronghold: SHOuterWallGate");
			var customPiece19 = new CustomPiece(piece19, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 75,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece19);
			//Debug.Log("Stronghold: SHOuterWallTowerRound");
			var customPiece18 = new CustomPiece(piece18, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 110,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 80,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 50,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece18);
			//Debug.Log("Stronghold: SHOuterWallTowerTransition");
			var customPiece17 = new CustomPiece(piece17, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 100,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece17);
			//Debug.Log("Stronghold: SHOuterWallTowerSquareCenter");
			var customPiece16 = new CustomPiece(piece16, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Iron",
						Amount = 100,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece16);
			//Debug.Log("Stronghold: SHOuterWallOpen");
			var customPiece15 = new CustomPiece(piece15, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 150,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece15);
			//Debug.Log("Stronghold: SHOuterWallOpenCapped");
			var customPiece14 = new CustomPiece(piece14, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 200,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece14);
			//Debug.Log("Stronghold: SHOuterWallCovered");
			var customPiece13 = new CustomPiece(piece13, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 150,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 40,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece13);
			//Debug.Log("Stronghold: SHWell");
			var customPiece12 = new CustomPiece(piece12, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 25,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece12);
			//Debug.Log("Stronghold: SHBunkhouse");
			var customPiece11 = new CustomPiece(piece11, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 300,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 200,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece11);
			//Debug.Log("Stronghold: SHEnclosedTower");
			var customPiece10 = new CustomPiece(piece10, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 150,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 150,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece10);
			//Debug.Log("Stronghold: SHWallOpenTwoFloor");
			var customPiece9 = new CustomPiece(piece9, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Stronghold",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Stone",
						Amount = 75,
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
			PieceManager.Instance.AddPiece(customPiece9);
			//Debug.Log("Stronghold: SHWallOpenTwoFloorWithNestCapped");
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
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 75,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece8);
			//Debug.Log("Stronghold: SHWallOpenTwoFloorWithNest");
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
						Amount = 100,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 75,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece7);
			//Debug.Log("Stronghold: SHWallOpenTwoFloorCapped");
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
						Amount = 75,
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
			//Debug.Log("Stronghold: SHTowerSquareTwoFloorJunction");
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
						Amount = 75,
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
			//Debug.Log("Stronghold: SHTowerSquareTwoFloorCorner");
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
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 100,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece4);
			//Debug.Log("Stronghold: SHTowerSquareTwoFloorCenter");
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
						Amount = 125,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 100,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
			//Debug.Log("Stronghold: SHWallMusteringHall");
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
						Amount = 200,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 125,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			//Debug.Log("Stronghold: SHGateHouse");
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
						Amount = 150,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 150,
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
