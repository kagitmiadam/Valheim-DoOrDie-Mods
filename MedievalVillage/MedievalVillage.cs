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

namespace MedievalVillage
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    internal class MedievalVillage : BaseUnityPlugin
    {
        public const string PluginGUID = "horemvore.MedievalVillage";

        public const string PluginName = "MedievalVillage";

        public const string PluginVersion = "0.0.3";

        public static bool isModded = true;

        internal static ManualLogSource Log;
		private Harmony _harmony;
        public AssetBundle MVAssets;

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
        public static GameObject piece36;
        public static GameObject piece37;
        public static GameObject piece38;
        public static GameObject piece39;
        public static GameObject piece40;
        public static GameObject piece41;
        public static GameObject piece42;
        public static GameObject piece43;
        public static GameObject hammerMV;
        public static GameObject pieceTableMV;
        public ConfigEntry<bool> ModEnable;
        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
            using Stream stream = executingAssembly.GetManifestResourceStream(text);
            return AssetBundle.LoadFromStream(stream);
        }
        public void CreateConfigurationValues()
        {
            ModEnable = base.Config.Bind("Medieval Villages", "Enable", defaultValue: true, new ConfigDescription("Enables or disables the Mod.", null, new ConfigurationManagerAttributes
            {
                IsAdminOnly = true
            }));
        }
        private void Awake()
        {
            CreateConfigurationValues();
            if (ModEnable.Value == true)
            {
                Debug.Log("Medieval Village: Loading and Creating Assets");
                Log = Logger;
			    _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.MedievalVillage");
                LoadBundle();
                LoadIVAssets();
                AddMVHammer();
                CreatePieces();
            }
            else
            {
                Debug.Log("Medieval Village is Disabled in the config.");
            }
        }
        public void LoadBundle()
        {
            MVAssets = AssetUtils.LoadAssetBundleFromResources("medvillage", Assembly.GetExecutingAssembly());
        }
        private void LoadIVAssets()
        {
            piece1 = MVAssets.LoadAsset<GameObject>("WallWoodHalf_MV");
            piece2 = MVAssets.LoadAsset<GameObject>("WallWoodT3x3_MV");
            piece3 = MVAssets.LoadAsset<GameObject>("WallCorner3x3_MV");
            piece4 = MVAssets.LoadAsset<GameObject>("WallStone3x3_MV");
            piece5 = MVAssets.LoadAsset<GameObject>("WallWoodWindow3x3_MV");
            piece6 = MVAssets.LoadAsset<GameObject>("WallWood3x3_MV");
            piece7 = MVAssets.LoadAsset<GameObject>("WallWindow3x3_MV");
            piece8 = MVAssets.LoadAsset<GameObject>("WallZbar3x3_MV");
            piece9 = MVAssets.LoadAsset<GameObject>("WallCross3x3_MV");
            piece10 = MVAssets.LoadAsset<GameObject>("WallPlain3x3_MV");
            piece11 = MVAssets.LoadAsset<GameObject>("Roof45_3M_MV");
            piece12 = MVAssets.LoadAsset<GameObject>("RoofWallRight_3M_MV");
            piece13 = MVAssets.LoadAsset<GameObject>("RoofWallLeft_3M_MV");
            piece14 = MVAssets.LoadAsset<GameObject>("RoofWallCenter_3M_MV");
            piece15 = MVAssets.LoadAsset<GameObject>("RoofBeamHEnd_3M_MV");
            piece16 = MVAssets.LoadAsset<GameObject>("WalkWay3x1.5_MV");
            piece17 = MVAssets.LoadAsset<GameObject>("WalkWay3x2_MV");
            piece18 = MVAssets.LoadAsset<GameObject>("WoodFloor3x3_MV");
            piece19 = MVAssets.LoadAsset<GameObject>("WoodFloor3x2_MV");
            piece20 = MVAssets.LoadAsset<GameObject>("BeamV_3M_MV");
            piece21 = MVAssets.LoadAsset<GameObject>("BaseBeam_3M_MV");
            piece22 = MVAssets.LoadAsset<GameObject>("WallDoor3x3_MV");
            piece23 = MVAssets.LoadAsset<GameObject>("WallPanelWindow3x3_MV");
            piece24 = MVAssets.LoadAsset<GameObject>("WallPanelStone3x3_MV");
            piece25 = MVAssets.LoadAsset<GameObject>("WallPanel3x3_MV");
            piece26 = MVAssets.LoadAsset<GameObject>("StairsSplit_MV");
            piece27 = MVAssets.LoadAsset<GameObject>("Stairs_MV");
            piece28 = MVAssets.LoadAsset<GameObject>("BeamVThick_3M_MV");
            piece29 = MVAssets.LoadAsset<GameObject>("BeamVThick_6M_MV");
            piece30 = MVAssets.LoadAsset<GameObject>("BeamHThick_3M_MV");
            piece31 = MVAssets.LoadAsset<GameObject>("StoneWallWindow3x3_MV");
            piece32 = MVAssets.LoadAsset<GameObject>("StoneWall3x3_MV");
            piece33 = MVAssets.LoadAsset<GameObject>("StoneWallCurved3x3_MV");
            piece34 = MVAssets.LoadAsset<GameObject>("StoneWallHalf3x3_MV");
            piece35 = MVAssets.LoadAsset<GameObject>("StoneWallQuarter3x3_MV");
            piece36 = MVAssets.LoadAsset<GameObject>("StoneSteps_MV");
            piece37 = MVAssets.LoadAsset<GameObject>("StoneStepsShort_MV");
            piece38 = MVAssets.LoadAsset<GameObject>("WoodSteps_MV");
            piece39 = MVAssets.LoadAsset<GameObject>("FloorBeamH_3M_MV");
            piece40 = MVAssets.LoadAsset<GameObject>("StoneFoundationA_MV");
            piece41 = MVAssets.LoadAsset<GameObject>("StoneFoundationB_MV");
            piece42 = MVAssets.LoadAsset<GameObject>("FoundationBeam_4M_MV");
            piece43 = MVAssets.LoadAsset<GameObject>("RoofBeam45_MV");
            //piece44 = MVAssets.LoadAsset<GameObject>("BeamHThick_6M_MV");
            hammerMV = MVAssets.LoadAsset<GameObject>("Hammer_MV");
            pieceTableMV = MVAssets.LoadAsset<GameObject>("PieceTable_MV");
        }
        private void AddMVHammer()
        {
            Debug.Log("MV: Piece Table");
            var pieceTableTBP = pieceTableMV;
            CustomPieceTable tbp_table = new CustomPieceTable(pieceTableTBP,
                new PieceTableConfig
                {
                    CanRemovePieces = true,
                    UseCategories = false,
                    UseCustomCategories = true,
                    CustomCategories = new string[]
                    {
                        "MedVillage"
                    }
                }
            );
            PieceManager.Instance.AddPieceTable(tbp_table);
            Debug.Log("MV: Hammer");
            var hammer = hammerMV;
            CustomItem customHammer = new CustomItem(hammer, fixReference: true, new ItemConfig
            {
                Amount = 1,
                Requirements = new RequirementConfig[2]
                {
                    new RequirementConfig
                    {
                        Item = "Wood",
                        Amount = 2
                    },
                    new RequirementConfig
                    {
                        Item = "Stone",
                        Amount = 10
                    }
                }
            });
            ItemManager.Instance.AddItem(customHammer);
        }
        private void CreatePieces()
        {
            try
            {
                Debug.Log("Medieval Village: RoofBeam45_MV");
                var customPiece43 = new CustomPiece(piece43, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 10,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece43);
                Debug.Log("Medieval Village: FoundationBeam_4M_MV");
                var customPiece42 = new CustomPiece(piece42, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
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
                            Item = "FineWood",
                            Amount = 12,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece42);
                Debug.Log("Medieval Village: StoneFoundationB_MV");
                var customPiece41 = new CustomPiece(piece41, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[1]
                    {
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece41);
                Debug.Log("Medieval Village: StoneFoundationA_MV");
                var customPiece40 = new CustomPiece(piece40, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[1]
                    {
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece40);
                Debug.Log("Medieval Village: FloorBeamH_3M_MV");
                var customPiece39 = new CustomPiece(piece39, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece39);
                Debug.Log("Medieval Village: WoodSteps_MV");
                var customPiece38 = new CustomPiece(piece38, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[1]
                    {
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece38);
                Debug.Log("Medieval Village: StoneStepsShort_MV");
                var customPiece37 = new CustomPiece(piece37, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[1]
                    {
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece37);
                Debug.Log("Medieval Village: StoneSteps_MV");
                var customPiece36 = new CustomPiece(piece36, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[1]
                    {
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece36);
                Debug.Log("Medieval Village: StoneWallQuarter3x3_MV");
                var customPiece35 = new CustomPiece(piece35, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece35);
                Debug.Log("Medieval Village: StoneWallHalf3x3_MV");
                var customPiece34 = new CustomPiece(piece34, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece34);
                Debug.Log("Medieval Village: StoneWallCurved3x3_MV");
                var customPiece33 = new CustomPiece(piece33, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece33);
                Debug.Log("Medieval Village: StoneWall3x3_MV");
                var customPiece32 = new CustomPiece(piece32, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece32);
                Debug.Log("Medieval Village: StoneWallWindow3x3_MV");
                var customPiece31 = new CustomPiece(piece31, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece31);
                Debug.Log("Medieval Village: BeamHThick_3M_MV");
                var customPiece30 = new CustomPiece(piece30, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece30);
                Debug.Log("Medieval Village: BeamVThick_6M_MV");
                var customPiece29 = new CustomPiece(piece29, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece29);
                Debug.Log("Medieval Village: BeamVThick_3M_MV");
                var customPiece28 = new CustomPiece(piece28, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece28);
                Debug.Log("Medieval Village: Stairs_MV");
                var customPiece27 = new CustomPiece(piece27, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece27);
                Debug.Log("Medieval Village: StairsSplit_MV");
                var customPiece26 = new CustomPiece(piece26, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 24,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece26);
                Debug.Log("Medieval Village: WallPanel3x3_MV");
                var customPiece25 = new CustomPiece(piece25, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 1,
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
                            Item = "FineWood",
                            Amount = 4,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece25);
                Debug.Log("Medieval Village: WallPanelStone3x3_MV");
                var customPiece24 = new CustomPiece(piece24, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 1,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 6,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 2,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece24);
                Debug.Log("Medieval Village: WallPanelWindow3x3_MV");
                var customPiece23 = new CustomPiece(piece23, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 1,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 3,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 3,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece23);
                Debug.Log("Medieval Village: WallDoor3x3_MV");
                var customPiece22 = new CustomPiece(piece22, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 6,
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
                            Item = "FineWood",
                            Amount = 20,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece22);
                Debug.Log("Medieval Village: BaseBeam_3M_MV");
                var customPiece21 = new CustomPiece(piece21, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 4,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece21);
                Debug.Log("Medieval Village: BeamV_3M_MV");
                var customPiece20 = new CustomPiece(piece20, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 4,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece20);
                Debug.Log("Medieval Village: WoodFloor3x2_MV");
                var customPiece19 = new CustomPiece(piece19, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 12,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece19);
                Debug.Log("Medieval Village: WoodFloor3x3_MV");
                var customPiece18 = new CustomPiece(piece18, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece18);
                Debug.Log("Medieval Village: WoodFloor3x2_MV");
                var customPiece17 = new CustomPiece(piece17, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 12,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece17);
                Debug.Log("Medieval Village: WalkWay3x1.5_MV");
                var customPiece16 = new CustomPiece(piece16, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece16);
                Debug.Log("Medieval Village: RoofBeamHEnd_3M_MV");
                var customPiece15 = new CustomPiece(piece15, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[2]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece15);
                Debug.Log("Medieval Village: RoofWallCenter_3M_MV");
                var customPiece14 = new CustomPiece(piece14, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece14);
                Debug.Log("Medieval Village: RoofWallLeft_3M_MV");
                var customPiece13 = new CustomPiece(piece13, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece13);
                Debug.Log("Medieval Village: RoofWallRight_3M_MV");
                var customPiece12 = new CustomPiece(piece12, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece12);
                Debug.Log("Medieval Village: Roof45_3M_MV");
                var customPiece11 = new CustomPiece(piece11, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 20,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 10,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece11);
                Debug.Log("Medieval Village: WallPlain3x3_MV");
                var customPiece10 = new CustomPiece(piece10, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 20,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 10,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece10);
                Debug.Log("Medieval Village: WallCross3x3_MV");
                var customPiece9 = new CustomPiece(piece9, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 14,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 18,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece9);
                Debug.Log("Medieval Village: WallZbar3x3_MV");
                var customPiece8 = new CustomPiece(piece8, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 12,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 20,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece8);
                Debug.Log("Medieval Village: WallWindow3x3_MV");
                var customPiece7 = new CustomPiece(piece7, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 12,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece7);
                Debug.Log("Medieval Village: WallWood3x3_MV");
                var customPiece6 = new CustomPiece(piece6, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece6);
                Debug.Log("Medieval Village: WallWoodWindow3x3_MV");
                var customPiece5 = new CustomPiece(piece5, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece5);
                Debug.Log("Medieval Village: WallStone3x3_MV");
                var customPiece4 = new CustomPiece(piece4, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 24,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece4);
                Debug.Log("Medieval Village: WallCorner3x3_MV");
                var customPiece3 = new CustomPiece(piece3, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece3);
                Debug.Log("Medieval Village: WallWoodT3x3_MV");
                var customPiece2 = new CustomPiece(piece2, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 4,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 16,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 16,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece2);
                Debug.Log("Medieval Village: WallWoodHalf_MV");
                var customPiece1 = new CustomPiece(piece1, true, new PieceConfig
                {
                    PieceTable = "PieceTable_MV",
                    Category = "MedVillage",
                    AllowedInDungeons = true,
                    Requirements = new RequirementConfig[3]
                    {
                        new RequirementConfig
                        {
                            Item = "Iron",
                            Amount = 2,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "Stone",
                            Amount = 8,
                            Recover = true
                        },
                        new RequirementConfig
                        {
                            Item = "FineWood",
                            Amount = 8,
                            Recover = true
                        }
                    }
                });
                PieceManager.Instance.AddPiece(customPiece1);
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding Medieval Village piece: {ex}");
            }
            finally
            {
                MVAssets.Unload(false);
            }
        }
    }
}
