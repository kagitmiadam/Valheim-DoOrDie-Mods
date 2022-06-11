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

namespace ThemedBuildPieces
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class ThemedBuildPieces : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.ThemedBuildPieces";

		public const string PluginName = "ThemedBuildPieces";

		public const string PluginVersion = "0.0.3";

		public static bool isModded = true;

		public AssetBundle TBPAssets;
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
			Debug.Log("Themed Build Pieces: Loading and Creating Assets");
			LoadBundle();
			AddTBPHammer();
			CreatePieces();
			//CreatePremades();
			CreateTowerPieces();
			UnloadBundle();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.ThemedBuildPieces");
		}
		public void LoadBundle()
		{
			TBPAssets = AssetUtils.LoadAssetBundleFromResources("tbpassets", Assembly.GetExecutingAssembly());
		}
		private void AddTBPHammer()
        {
			var TBPHammer = TBPAssets.LoadAsset<GameObject>("TBPHammer_TP");
			CustomItem customItem1 = new CustomItem(TBPHammer, fixReference: true, new ItemConfig
			{
				Amount = 1,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 10
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
		private void CreatePieces()
		{
			Debug.Log("TBP: 0");
			var pieceTableTBP = TBPAssets.LoadAsset<GameObject>("TBPPieceTable_TP");
			CustomPieceTable tbp_table = new CustomPieceTable(pieceTableTBP,
				new PieceTableConfig
				{
					CanRemovePieces = true,
					UseCategories = false,
					UseCustomCategories = true,
					CustomCategories = new string[]
					{
						"Stone", "Wood"
					}
				}
			);
			PieceManager.Instance.AddPieceTable(tbp_table);
			Debug.Log("TBP: 1");
			var pieceSlate2 = TBPAssets.LoadAsset<GameObject>("SlateRoof45_TP");
			var customPiece36 = new CustomPiece(pieceSlate2, true, new PieceConfig
			{
				Name = "$piece_slateroof45_tp",
				//Description = "$piece_clayrooftop45_tp",
				//Icon = pieceClay3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece36);
			Debug.Log("TBP: 2");
			var pieceSlate1 = TBPAssets.LoadAsset<GameObject>("SlateRoofTop45E_TP");
			var customPiece35 = new CustomPiece(pieceSlate1, true, new PieceConfig
			{
				Name = "$piece_slaterooftop45E_tp",
				//Description = "$piece_clayrooftop45E_tp",
				//Icon = pieceClay4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece35);
			Debug.Log("TBP: 3");
			var pieceHardwood8 = TBPAssets.LoadAsset<GameObject>("HardwoodPost_TP");
			var customPiece34 = new CustomPiece(pieceHardwood8, true, new PieceConfig
			{
				Name = "$piece_hardwoodpost_tp",
				//Description = "$piece_hardwoodpost_tp",
				//Icon = pieceHardwood4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece34);
			Debug.Log("TBP: 4");
			var pieceHardwood7 = TBPAssets.LoadAsset<GameObject>("HardwoodPostL_TP");
			var customPiece33 = new CustomPiece(pieceHardwood7, true, new PieceConfig
			{
				Name = "$piece_hardwoodpostl_tp",
				//Description = "$piece_hardwoodpostl_tp",
				//Icon = pieceHardwood4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece33);
			Debug.Log("TBP: 5");
			var pieceThatch3 = TBPAssets.LoadAsset<GameObject>("ThatchWall2x2_TP");
			var customPiece32 = new CustomPiece(pieceThatch3, true, new PieceConfig
			{
				Name = "$piece_thatchwall1_tp",
				//Description = "$piece_thatchwall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece32);
			Debug.Log("TBP: 6");
			var pieceThatch2 = TBPAssets.LoadAsset<GameObject>("ThatchWall4x2_TP");
			var customPiece31 = new CustomPiece(pieceThatch2, true, new PieceConfig
			{
				Name = "$piece_thatchwall2_tp",
				//Description = "$piece_thatchwall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece31);
			Debug.Log("TBP: 7");
			var pieceThatch1 = TBPAssets.LoadAsset<GameObject>("ThatchWindow2x1_TP");
			var customPiece30 = new CustomPiece(pieceThatch1, true, new PieceConfig
			{
				Name = "$piece_thatchwindow2x1_tp",
				//Description = "$piece_thatchwindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece30);
			Debug.Log("TBP: 8");
			var pieceCobble3 = TBPAssets.LoadAsset<GameObject>("CobbleWall2x2_TP");
			var customPiece29 = new CustomPiece(pieceCobble3, true, new PieceConfig
			{
				Name = "$piece_cobblewall1_tp",
				//Description = "$piece_cobblewall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece29);
			Debug.Log("TBP: 9");
			var pieceCobble2 = TBPAssets.LoadAsset<GameObject>("CobbleWall4x2_TP");
			var customPiece28 = new CustomPiece(pieceCobble2, true, new PieceConfig
			{
				Name = "$piece_cobblewall2_tp",
				//Description = "$piece_cobblewall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece28);
			Debug.Log("TBP: 10");
			var pieceCobble1 = TBPAssets.LoadAsset<GameObject>("CobbleWindow2x1_TP");
			var customPiece27 = new CustomPiece(pieceCobble1, true, new PieceConfig
			{
				Name = "$piece_cobblewindow2x1_tp",
				//Description = "$piece_cobblewindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece27);
			Debug.Log("TBP: 11");
			var pieceGrey3 = TBPAssets.LoadAsset<GameObject>("GreyWall2x2_TP");
			var customPiece26 = new CustomPiece(pieceGrey3, true, new PieceConfig
			{
				Name = "$piece_greywall1_tp",
				//Description = "$piece_greywall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece26);
			Debug.Log("TBP: 12");
			var pieceGrey2 = TBPAssets.LoadAsset<GameObject>("GreyWall4x2_TP");
			var customPiece25 = new CustomPiece(pieceGrey2, true, new PieceConfig
			{
				Name = "$piece_greywall2_tp",
				//Description = "$piece_greywall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece25);
			Debug.Log("TBP: 13");
			var pieceGrey1 = TBPAssets.LoadAsset<GameObject>("GreyWindow2x1_TP");
			var customPiece24 = new CustomPiece(pieceGrey1, true, new PieceConfig
			{
				Name = "$piece_greywindow2x1_tp",
				//Description = "$piece_greywindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece24);
			Debug.Log("TBP: 14");
			var pieceOak3 = TBPAssets.LoadAsset<GameObject>("OakWall2x2_TP");
			var customPiece23 = new CustomPiece(pieceOak3, true, new PieceConfig
			{
				Name = "$piece_oakwall1_tp",
				//Description = "$piece_oakwall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece23);
			Debug.Log("TBP: 15");
			var pieceOak2 = TBPAssets.LoadAsset<GameObject>("OakWall4x2_TP");
			var customPiece22 = new CustomPiece(pieceOak2, true, new PieceConfig
			{
				Name = "$piece_oakwall2_tp",
				//Description = "$piece_oakwall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece22);
			Debug.Log("TBP: 16");
			var pieceOak1 = TBPAssets.LoadAsset<GameObject>("OakWindow2x1_TP");
			var customPiece21 = new CustomPiece(pieceOak1, true, new PieceConfig
			{
				Name = "$piece_oakwindow2x1_tp",
				//Description = "$piece_oakwindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece21);
			Debug.Log("TBP: 17");
			var piecePine3 = TBPAssets.LoadAsset<GameObject>("PineWall2x2_TP");
			var customPiece20 = new CustomPiece(piecePine3, true, new PieceConfig
			{
				Name = "$piece_pinewall1_tp",
				//Description = "$piece_pinewall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece20);
			Debug.Log("TBP: 18");
			var piecePine2 = TBPAssets.LoadAsset<GameObject>("PineWall4x2_TP");
			var customPiece19 = new CustomPiece(piecePine2, true, new PieceConfig
			{
				Name = "$piece_pinewall2_tp",
				//Description = "$piece_pinewall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece19);
			Debug.Log("TBP: 19");
			var piecePine1 = TBPAssets.LoadAsset<GameObject>("PineWindow2x1_TP");
			var customPiece18 = new CustomPiece(piecePine1, true, new PieceConfig
			{
				Name = "$piece_pinewindow2x1_tp",
				//Description = "$piece_pinewindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece18);
			Debug.Log("TBP: 20");
			var pieceTudor5 = TBPAssets.LoadAsset<GameObject>("TudorWall2x2_TP");
			var customPiece17 = new CustomPiece(pieceTudor5, true, new PieceConfig
			{
				Name = "$piece_tudorwall1_tp",
				//Description = "$piece_tudorwall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece17);
			Debug.Log("TBP: 21");
			var pieceTudor6 = TBPAssets.LoadAsset<GameObject>("TudorWall4x2_TP");
			var customPiece16 = new CustomPiece(pieceTudor6, true, new PieceConfig
			{
				Name = "$piece_tudorwall2_tp",
				//Description = "$piece_tudorwall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece16);
			Debug.Log("TBP: 22");
			var pieceTudor7 = TBPAssets.LoadAsset<GameObject>("TudorWindow2x1_TP");
			var customPiece15 = new CustomPiece(pieceTudor7, true, new PieceConfig
			{
				Name = "$piece_tudorwindow2x1_tp",
				//Description = "$piece_tudorwindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece15);
			Debug.Log("TBP: 23");
			//var pieceTudor1Icon = TBPAssets.LoadAsset<Sprite>("WornTudorFloor_Icon_TP");
			var pieceTudor1 = TBPAssets.LoadAsset<GameObject>("WornTudorFloor_TP");
			var customPiece1 = new CustomPiece(pieceTudor1, true, new PieceConfig
			{
				Name = "$piece_worntudorfloor_tp",
				//Description = "$piece_worntudorfloor_tp",
				//Icon = pieceTudor1Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);
			Debug.Log("TBP: 24");
			//var pieceTudor2Icon = TBPAssets.LoadAsset<Sprite>("WornTudor2_Icon_TP");
			var pieceTudor2 = TBPAssets.LoadAsset<GameObject>("WornTudorWall2x2_TP");
			var customPiece2 = new CustomPiece(pieceTudor2, true, new PieceConfig
			{
				Name = "$piece_worntudorwall1_tp",
				//Description = "$piece_worntudorwall1_tp",
				//Icon = pieceTudor2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			Debug.Log("TBP: 25");
			//var pieceTudor3Icon = TBPAssets.LoadAsset<Sprite>("WornTudor4_Icon_TP");
			var pieceTudor3 = TBPAssets.LoadAsset<GameObject>("WornTudorWall4x2_TP");
			var customPiece3 = new CustomPiece(pieceTudor3, true, new PieceConfig
			{
				Name = "$piece_worntudorwall2_tp",
				//Description = "$piece_worntudorwall2_tp",
				//Icon = pieceTudor3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
			Debug.Log("TBP: 26");
			//var pieceTudor4Icon = TBPAssets.LoadAsset<Sprite>("WornTudorWindow2_Icon_TP");
			var pieceTudor4 = TBPAssets.LoadAsset<GameObject>("WornTudorWindow2x1_TP");
			var customPiece4 = new CustomPiece(pieceTudor4, true, new PieceConfig
			{
				Name = "$piece_worntudorwindow2x1_tp",
				//Description = "$piece_worntudorwindow2x1_tp",
				//Icon = pieceTudor4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece4);
			Debug.Log("TBP: 27");
			//var pieceClay1Icon = TBPAssets.LoadAsset<Sprite>("ClayRoof26_Icon_TP");
			var pieceClay1 = TBPAssets.LoadAsset<GameObject>("ClayRoof26_TP");
			var customPiece5 = new CustomPiece(pieceClay1, true, new PieceConfig
			{
				Name = "$piece_clayroof26_tp",
				//Description = "$piece_clayroof26_tp",
				//Icon = pieceClay1Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece5);
			Debug.Log("TBP: 28");
			//var pieceClay2Icon = TBPAssets.LoadAsset<Sprite>("ClayRoof45_Icon_TP");
			var pieceClay2 = TBPAssets.LoadAsset<GameObject>("ClayRoof45_TP");
			var customPiece6 = new CustomPiece(pieceClay2, true, new PieceConfig
			{
				Name = "$piece_clayroof45_tp",
				//Description = "$piece_clayroof45_tp",
				//Icon = pieceClay2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece6);
			Debug.Log("TBP: 29");
			//var pieceClay3Icon = TBPAssets.LoadAsset<Sprite>("ClayRoofTop45_Icon_TP");
			var pieceClay3 = TBPAssets.LoadAsset<GameObject>("ClayRoofTop45_TP");
			var customPiece7 = new CustomPiece(pieceClay3, true, new PieceConfig
			{
				Name = "$piece_clayrooftop45_tp",
				//Description = "$piece_clayrooftop45_tp",
				//Icon = pieceClay3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece7);
			Debug.Log("TBP: 30");
			//var pieceClay4Icon = TBPAssets.LoadAsset<Sprite>("ClayRoofEnd45_Icon_TP");
			var pieceClay4 = TBPAssets.LoadAsset<GameObject>("ClayRoofTop45E_TP");
			var customPiece8 = new CustomPiece(pieceClay4, true, new PieceConfig
			{
				Name = "$piece_clayrooftop45E_tp",
				//Description = "$piece_clayrooftop45E_tp",
				//Icon = pieceClay4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece8);
			Debug.Log("TBP: 31");
			//var pieceHardwood1Icon = TBPAssets.LoadAsset<Sprite>("HardwoodBeam45_Icon_TP");
			var pieceHardwood1 = TBPAssets.LoadAsset<GameObject>("HardwoodBeam45_TP");
			var customPiece9 = new CustomPiece(pieceHardwood1, true, new PieceConfig
			{
				Name = "$piece_hardwoodbeam45_tp",
				//Description = "$piece_hardwoodbeam45_tp",
				//Icon = pieceHardwood1Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece9);
			Debug.Log("TBP: 32");
			//var pieceHardwood2Icon = TBPAssets.LoadAsset<Sprite>("HardwoodBeam2_Icon_TP");
			var pieceHardwood2 = TBPAssets.LoadAsset<GameObject>("HardwoodBeam_TP");
			var customPiece10 = new CustomPiece(pieceHardwood2, true, new PieceConfig
			{
				Name = "$piece_hardwoodbeam_tp",
				//Description = "$piece_hardwoodbeam_tp",
				//Icon = pieceHardwood2Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece10);
			Debug.Log("TBP: 33");
			//var pieceHardwood3Icon = TBPAssets.LoadAsset<Sprite>("HardwoodBeam1_Icon_TP");
			var pieceHardwood3 = TBPAssets.LoadAsset<GameObject>("HardwoodBeamH_TP");
			var customPiece11 = new CustomPiece(pieceHardwood3, true, new PieceConfig
			{
				Name = "$piece_hardwoodbeamh_tp",
				//Description = "$piece_hardwoodbeamh_tp",
				//Icon = pieceHardwood3Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece11);
			Debug.Log("TBP: 34");
			//var pieceHardwood4Icon = TBPAssets.LoadAsset<Sprite>("HardwoodBeam4_Icon_TP");
			var pieceHardwood4 = TBPAssets.LoadAsset<GameObject>("HardwoodBeamL_TP");
			var customPiece12 = new CustomPiece(pieceHardwood4, true, new PieceConfig
			{
				Name = "$piece_hardwoodbeaml_tp",
				//Description = "$piece_hardwoodbeaml_tp",
				//Icon = pieceHardwood4Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece12);
			Debug.Log("TBP: 35");
			//var pieceHardwood5Icon = TBPAssets.LoadAsset<Sprite>("HardwoodDoor_Icon_TP");
			var pieceHardwood5 = TBPAssets.LoadAsset<GameObject>("HardwoodDoor_TP");
			var customPiece13 = new CustomPiece(pieceHardwood5, true, new PieceConfig
			{
				Name = "$piece_hardwooddoor_tp",
				//Description = "$piece_hardwooddoor_tp",
				//Icon = pieceHardwood5Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 4,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece13);
			Debug.Log("TBP: 36");
			//var pieceHardwood6Icon = TBPAssets.LoadAsset<Sprite>("HardwoodSide45_Icon_TP");
			var pieceHardwood6 = TBPAssets.LoadAsset<GameObject>("HardwoodSide45_TP");
			var customPiece14 = new CustomPiece(pieceHardwood6, true, new PieceConfig
			{
				Name = "$piece_wallside45_tp",
				//Description = "$piece_wallside45_tp",
				//Icon = pieceHardwood6Icon,
				PieceTable = "TBPPieceTable_TP",
				Category = "Wood",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "FineWood",
					Amount = 2,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Resin",
					Amount = 4,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 2,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customPiece14);
		}
		private void CreateTowerPieces()
		{
			Debug.Log("TBP Tower: 0");
			var pieceTower0 = TBPAssets.LoadAsset<GameObject>("Tower_Round_DoorwayFloor_TBP");
			var customTower0 = new CustomPiece(pieceTower0, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower0);
			Debug.Log("TBP Tower: 1");
			var pieceTower1 = TBPAssets.LoadAsset<GameObject>("Tower_Round_WindowWM_TBP");
			var customTower1 = new CustomPiece(pieceTower1, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[3]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 20,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Crystal",
					Amount = 15,
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
			PieceManager.Instance.AddPiece(customTower1);
			Debug.Log("TBP Tower: 2");
			var pieceTower2 = TBPAssets.LoadAsset<GameObject>("Tower_Round_WindowWS_TBP");
			var customTower2 = new CustomPiece(pieceTower2, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 25,
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
			PieceManager.Instance.AddPiece(customTower2);
			Debug.Log("TBP Tower: 3");
			var pieceTower3 = TBPAssets.LoadAsset<GameObject>("Tower_Round_Doorway_TBP");
			var customTower3 = new CustomPiece(pieceTower3, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower3);
			Debug.Log("TBP Tower: 4");
			var pieceTower4 = TBPAssets.LoadAsset<GameObject>("Tower_Round_Stair_TBP");
			var customTower4 = new CustomPiece(pieceTower4, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 15,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower4);
			Debug.Log("TBP Tower: 5");
			var pieceTower5 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorS1_TBP");
			var customTower5 = new CustomPiece(pieceTower5, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower5);
			Debug.Log("TBP Tower: 6");
			var pieceTower6 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorS2_TBP");
			var customTower6 = new CustomPiece(pieceTower6, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower6);
			Debug.Log("TBP Tower: 7");
			var pieceTower7 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorS3_TBP");
			var customTower7 = new CustomPiece(pieceTower7, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower7);
			Debug.Log("TBP Tower: 8");
			var pieceTower8 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorS4_TBP");
			var customTower8 = new CustomPiece(pieceTower8, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower8);
			Debug.Log("TBP Tower: 9");
			var pieceTower9 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorLE1_TBP");
			var customTower9 = new CustomPiece(pieceTower9, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
			PieceManager.Instance.AddPiece(customTower9);
			Debug.Log("TBP Tower: 10");
			var pieceTower10 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorLE2_TBP");
			var customTower10 = new CustomPiece(pieceTower10, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
			PieceManager.Instance.AddPiece(customTower10);
			Debug.Log("TBP Tower: 11");
			var pieceTower11 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorLE3_TBP");
			var customTower11 = new CustomPiece(pieceTower11, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
			PieceManager.Instance.AddPiece(customTower11);
			Debug.Log("TBP Tower: 12");
			var pieceTower12 = TBPAssets.LoadAsset<GameObject>("Tower_Round_FloorLE4_TBP");
			var customTower12 = new CustomPiece(pieceTower12, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
			PieceManager.Instance.AddPiece(customTower12);
			Debug.Log("TBP Tower: 13");
			var pieceTower13 = TBPAssets.LoadAsset<GameObject>("Tower_Round_Wall_TBP");
			var customTower13 = new CustomPiece(pieceTower13, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower13);
			Debug.Log("TBP Tower: 14");
			var pieceTower14 = TBPAssets.LoadAsset<GameObject>("Tower_Round_WallS_TBP");
			var customTower14 = new CustomPiece(pieceTower14, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
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
			PieceManager.Instance.AddPiece(customTower14);
			Debug.Log("TBP Tower: 15");
			var pieceTower15 = TBPAssets.LoadAsset<GameObject>("StonePillar_TBP");
			var customTower15 = new CustomPiece(pieceTower15, true, new PieceConfig
			{
				PieceTable = "TBPPieceTable_TP",
				Category = "Stone",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[1]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 10,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customTower15);
		}
		private void UnloadBundle()
		{
			TBPAssets?.Unload(unloadAllLoadedObjects: false);
		}
	}
}
