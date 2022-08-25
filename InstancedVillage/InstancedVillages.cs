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

        public const string PluginVersion = "0.0.4";

		public static bool isModded = true;

		private Harmony _harmony;
		public AssetBundle IVAssets;
		public ConfigEntry<bool> CosmeticEnable;
		public ConfigEntry<bool> UsableEnable;
		public ConfigEntry<bool> LocationsEnable;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
            using Stream stream = executingAssembly.GetManifestResourceStream(text);
            return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			CosmeticEnable = base.Config.Bind("Cosmetic", "Enable", defaultValue: false, new ConfigDescription("Enables placable cosmetic house's.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			UsableEnable = base.Config.Bind("Usable", "Enable", defaultValue: false, new ConfigDescription("Enables placable usable house's.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			LocationsEnable = base.Config.Bind("Locations", "Enable", defaultValue: true, new ConfigDescription("Enables the Village Locations.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake()
        {
            //Debug.Log("Instanced Villages: Loading and Creating Assets");
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.InstancedVillages");
			CreateConfigurationValues();
			LoadBundle();
			// Cosmetic
			//CreateStonePremades();
			if (CosmeticEnable.Value == true)
			{
				//Debug.Log("Instanced Villages: Do Cosmetic");
				CreateThatchPremades();
				CreateOakPremades();
				CreatePinePremades();
				CreateGreyPremades();
				CreateWornPremades();
            }
			// Useable
			if (UsableEnable.Value == true)
			{
				//Debug.Log("Instanced Villages: Do Usable");
				CreateCobblePlaceables();
				CreateGreyPlaceables();
				CreateOakPlaceables();
				CreatePinePlaceables();
				CreateThatchPlaceables();
				CreateTudorPlaceables();
				CreateWornTudorPlaceables();
			}
			if (LocationsEnable.Value == true)
			{
				//Debug.Log("Instanced Villages: Do Locations");
				ZoneManager.OnVanillaLocationsAvailable += AddLocations;
            }
			LoadIVAssets();
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
        private void CreateCobblePlaceables()
		{
			//Debug.Log("Cobble: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseCobble_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Cobble: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseCobble_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Cobble: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseCobble_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateGreyPlaceables()
		{
			//Debug.Log("Grey: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseGrey_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Grey: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseGrey_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Grey: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseGrey_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateOakPlaceables()
		{
			//Debug.Log("Oak: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseOak_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Oak: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseOak_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Oak: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseOak_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreatePinePlaceables()
		{
			//Debug.Log("Pine: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHousePine_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Pine: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHousePine_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Pine: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHousePine_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateThatchPlaceables()
		{
			//Debug.Log("Thatch: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseThatch_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Thatch: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseThatch_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Thatch: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseThatch_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateTudorPlaceables()
		{
			//Debug.Log("Tudor: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseTudor_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("Tudor: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseTudor_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("Tudor: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseTudor_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateWornTudorPlaceables()
		{
			//Debug.Log("WornTudor: Small House");
			var pieceHouse1 = IVAssets.LoadAsset<GameObject>("SmallHouseWornTudor_IV");
			var smallHouse = new CustomPiece(pieceHouse1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
				AllowedInDungeons = true,
				Requirements = new RequirementConfig[2]
				{
				new RequirementConfig
				{
					Item = "Stone",
					Amount = 65,
					Recover = true
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 30,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(smallHouse);
			//Debug.Log("WornTudor: Medium House");
			var pieceHouse2 = IVAssets.LoadAsset<GameObject>("MediumHouseWornTudor_IV");
			var medHouse = new CustomPiece(pieceHouse2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 65,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(medHouse);
			//Debug.Log("WornTudor: Large House");
			var pieceHouse3 = IVAssets.LoadAsset<GameObject>("BigHouseWornTudor_IV");
			var largeHouse = new CustomPiece(pieceHouse3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Placeable",
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
					Amount = 100,
					Recover = true
				}
				}
			});
			PieceManager.Instance.AddPiece(largeHouse);
		}
		private void CreateStonePremades()
		{
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_StoneHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_ThatchHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_OakHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_PineHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_GreyHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 1");
			var pieceHouseCobbleA = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_A");
			var customHouse1 = new CustomPiece(pieceHouseCobbleA, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 2");
			var pieceHouseCobbleB = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_B");
			var customHouse2 = new CustomPiece(pieceHouseCobbleB, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 3");
			var pieceHouseCobbleC = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_C");
			var customHouse3 = new CustomPiece(pieceHouseCobbleC, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 4");
			var pieceHouseCobbleD = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_D");
			var customHouse4 = new CustomPiece(pieceHouseCobbleD, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
			//Debug.Log("TBP House: 5");
			var pieceHouseCobbleE = IVAssets.LoadAsset<GameObject>("Cosmetic_WornHouse_E");
			var customHouse5 = new CustomPiece(pieceHouseCobbleE, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "IV Cosmetic",
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
