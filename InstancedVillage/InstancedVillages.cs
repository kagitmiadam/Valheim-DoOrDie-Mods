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

namespace InstancedVillages
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    internal class InstancedVillages : BaseUnityPlugin
    {
        public const string PluginGUID = "horemvore.InstancedVillages";

        public const string PluginName = "InstancedVillages";

        public const string PluginVersion = "0.0.3";

		private Harmony _harmony;
		public AssetBundle IVAssets;
        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
            using Stream stream = executingAssembly.GetManifestResourceStream(text);
            return AssetBundle.LoadFromStream(stream);
        }
        private void Awake()
        {
            Debug.Log("Instanced Villages: Loading and Creating Assets");
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.ThemedBuildPieces");
			LoadBundle();
			CreateStonePremades();
			CreateThatchPremades();
			CreateOakPremades();
			CreatePinePremades();
			CreateGreyPremades();
			CreateWornPremades();
            LoadIVAssets();
			ZoneManager.OnVanillaLocationsAvailable += AddLocations;
        }
        public void LoadBundle()
        {
            IVAssets = AssetUtils.LoadAssetBundleFromResources("concept", Assembly.GetExecutingAssembly());
        }
        private void LoadIVAssets()
        {
            GameObject spawn1 = IVAssets.LoadAsset<GameObject>("Spawner_Generic");
            CustomPrefab spawner1 = new CustomPrefab(spawn1, true);
            PrefabManager.Instance.AddPrefab(spawner1);
            GameObject spawn2 = IVAssets.LoadAsset<GameObject>("Spawner_GenericNorm");
            CustomPrefab spawner2 = new CustomPrefab(spawn2, true);
            PrefabManager.Instance.AddPrefab(spawner2);
            GameObject spawn3 = IVAssets.LoadAsset<GameObject>("Spawner_GenericMed");
            CustomPrefab spawner3 = new CustomPrefab(spawn3, true);
            PrefabManager.Instance.AddPrefab(spawner3);
            IVAssets.Unload(false);
        }
        private void AddLocations()
        {
            IVAssets = AssetUtils.LoadAssetBundleFromResources("concept", Assembly.GetExecutingAssembly());
            try
            {
                var AnyLoc1 = ZoneManager.Instance.CreateLocationContainer(IVAssets.LoadAsset<GameObject>("Loc_Instanced_Village"));
                ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc1, true, new LocationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Quantity = 5,
                    Priotized = true,
                    ExteriorRadius = 32f,
                    ClearArea = true,
                    MinDistance = 1000,
                    MaxDistance = 2500,
                    MinAltitude = 4f,
                    MaxAltitude = 100f,
                    MinDistanceFromSimilar = 500f,
                }));
                var AnyLoc2 = ZoneManager.Instance.CreateLocationContainer(IVAssets.LoadAsset<GameObject>("Loc_Instanced_VillageMed"));
                ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc2, true, new LocationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Quantity = 5,
                    Priotized = true,
                    ExteriorRadius = 32f,
                    ClearArea = true,
                    MinDistance = 3000,
                    MaxDistance = 5000,
                    MinAltitude = 4f,
                    MaxAltitude = 100f,
                    MinDistanceFromSimilar = 500f,
                }));
                var AnyLoc3 = ZoneManager.Instance.CreateLocationContainer(IVAssets.LoadAsset<GameObject>("Loc_Instanced_Hut"));
                ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc3, true, new LocationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Quantity = 25,
                    Priotized = true,
                    ExteriorRadius = 10f,
                    ClearArea = true,
                    MinAltitude = 4f,
                    MaxAltitude = 100f,
                    MinDistanceFromSimilar = 100f,
                }));
            }
            finally
            {
                ZoneManager.OnVanillaLocationsAvailable -= AddLocations;
                IVAssets.Unload(false);
            }
		}
		private void CreateStonePremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
		private void CreateThatchPremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
		private void CreateOakPremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
		private void CreatePinePremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
		private void CreateGreyPremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
		private void CreateWornPremades()
		{
			Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse1);
			Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse2);
			Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse3);
			Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse4);
			Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "Hammer",
				Category = "Cosmetic Buildings",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 1,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 1,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(customHouse5);
		}
	}
}
