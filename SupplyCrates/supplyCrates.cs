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

namespace SupplyCrates
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class supplyCrates : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.SupplyCrates";

		public const string PluginName = "SupplyCrates";

		public const string PluginVersion = "0.0.1";

		internal static ManualLogSource Log;

		public static GameObject Food1;
		public static GameObject Food2;
		public static GameObject Food3;
		public static GameObject Food4;
		public static GameObject Food5;
		public static GameObject Food6;
		public static GameObject Food7;
		public static GameObject Food8;
		public static GameObject Food9;
		public static GameObject Food10;
		public static GameObject Food11;
		public static GameObject Food12;
		public static GameObject Food13;
		public static GameObject Food14;
		public static GameObject Food15;
		public static GameObject Food16;
		public static GameObject Food17;
		public static GameObject Food18;
		public static GameObject Food19;
		public static GameObject Food20;
		public static GameObject Food21;
		public static GameObject Food22;
		public static GameObject Food23;
		public static GameObject Food24;
		public static GameObject Food25;
		public static GameObject Food26;
		public static GameObject Food27;
		public static GameObject Food28;
		public static GameObject Food29;
		public static GameObject Food30;

		public static GameObject Pickable1;
		public static GameObject Pickable2;
		public static GameObject Pickable3;

		public static GameObject Bowl1;
		public static GameObject Bowl2;
		public static GameObject Bowl3;
		public static GameObject Bowl4;
		public static GameObject Bowl5;
		public static GameObject Bowl6;
		public static GameObject Bowl7;
		public static GameObject Bowl8;
		public static GameObject Bowl9;
		public static GameObject Bowl10;
		public static GameObject Bowl11;
		public static GameObject Bowl12;
		public static GameObject Bowl13;
		public static GameObject Bowl14;
		public static GameObject Bowl15;
		public static GameObject Bowl16;
		public static GameObject Bowl17;
		public static GameObject Bowl18;
		public static GameObject Bowl19;
		public static GameObject Bowl20;
		public static GameObject Bowl21;
		public static GameObject Bowl22;
		public static GameObject Bowl23;
		public static GameObject Bowl24;
		public static GameObject Bowl25;
		public static GameObject Bowl26;
		public static GameObject Bowl27;
		public static GameObject Bowl28;
		public static GameObject Bowl29;

		public static GameObject Box1;
		public static GameObject Box2;
		public static GameObject Box3;
		public static GameObject Box4;
		public static GameObject Box5;
		public static GameObject Box6;
		public static GameObject Box7;
		public static GameObject Box8;
		public static GameObject Box9;
		public static GameObject Box10;
		public static GameObject Box11;
		public static GameObject Box12;
		public static GameObject Box13;
		public static GameObject Box14;
		public static GameObject Box15;
		public static GameObject Box16;
		public static GameObject Box17;
		public static GameObject Box18;
		public static GameObject Box19;
		public static GameObject Box20;
		public static GameObject Box21;
		public static GameObject Box22;
		public static GameObject Box23;
		public static GameObject Box24;
		public static GameObject Box25;
		public static GameObject Box26;
		public static GameObject Box27;
		public static GameObject Box28;
		public static GameObject Box29;

		public static GameObject Bucket1;
		//public static GameObject Bucket2;
		public static GameObject Bucket3;
		public static GameObject Bucket4;
		//public static GameObject Bucket5;
		public static GameObject Bucket6;
		public static GameObject Bucket7;
		public static GameObject Bucket8;
		public static GameObject Bucket9;
		public static GameObject Bucket10;
		public static GameObject Bucket11;
		public static GameObject Bucket12;
		public static GameObject Bucket13;
		public static GameObject Bucket14;
		public static GameObject Bucket15;
		public static GameObject Bucket16;
		public static GameObject Bucket17;
		public static GameObject Bucket18;
		public static GameObject Bucket19;
		public static GameObject Bucket20;
		public static GameObject Bucket21;
		public static GameObject Bucket22;
		public static GameObject Bucket23;
		public static GameObject Bucket24;
		//public static GameObject Bucket25;
		public static GameObject Bucket26;
		public static GameObject Bucket27;
		public static GameObject Bucket28;
		public static GameObject Bucket29;

		public ConfigEntry<bool> MeadowsEnable;
		public ConfigEntry<bool> BlackForestEnable;
		public ConfigEntry<bool> SwampEnable;
		public ConfigEntry<bool> MountainEnable;
		//public ConfigEntry<bool> PlainsEnable;
		public ConfigEntry<bool> valharvestEnabled;
		public ConfigEntry<bool> FYAEnabled;
		public ConfigEntry<bool> BAEnabled;

		public AssetBundle SupplyBundle;
		private Harmony _harmony;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void CreateConfigurationValues()
		{
			MeadowsEnable = base.Config.Bind("Meadows", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Meadows.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BlackForestEnable = base.Config.Bind("Black Forest", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Black Forest.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			SwampEnable = base.Config.Bind("Swamp", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Swamp.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			MountainEnable = base.Config.Bind("Mountain", "Enable", defaultValue: true, new ConfigDescription("Enables Supply Crates in the Mountain's.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			valharvestEnabled = base.Config.Bind("Valharvest", "Enable", defaultValue: false, new ConfigDescription("Adds Valharvest's Apple, Salt and Vegetables to Provision Crate's.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			FYAEnabled = base.Config.Bind("Farmyard Animals", "Enable", defaultValue: false, new ConfigDescription("Adds additional food to the consume list for taming.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
			BAEnabled = base.Config.Bind("Bone Appetite", "Enable", defaultValue: false, new ConfigDescription("Adds BA Egg's to the dairy Provision Crate.", null, new ConfigurationManagerAttributes
			{
				IsAdminOnly = true
			}));
		}
		private void Awake() 
		{
			CreateConfigurationValues();
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.SupplyCrates");
			Log = Logger;
			LoadBundle();
			LoadAssets();
			AddItems();
			AddBowlPieces();
			AddBoxPieces();
			AddBucketPieces();
			if (valharvestEnabled.Value) PrefabManager.OnVanillaPrefabsAvailable += ValHavestAdditions;
			if (FYAEnabled.Value) CreatureManager.OnVanillaCreaturesAvailable += FYAAdditions;
			if (BAEnabled.Value) CreatureManager.OnVanillaCreaturesAvailable += BAAdditions;
			ZoneManager.OnVanillaLocationsAvailable += UpdateLocations;
		}
		public void LoadBundle()
		{
			SupplyBundle = AssetUtils.LoadAssetBundleFromResources("supplycrates", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
		{
			// Buckets
			Bucket1 = SupplyBundle.LoadAsset<GameObject>("BucketApples_SC");
			//Bucket2 = SupplyBundle.LoadAsset<GameObject>("BucketBagettes_SC");
			Bucket3 = SupplyBundle.LoadAsset<GameObject>("BucketBananas_SC");
			Bucket4 = SupplyBundle.LoadAsset<GameObject>("BucketBellPeppers_SC");
			//Bucket5 = SupplyBundle.LoadAsset<GameObject>("BucketBlueCheese_SC");
			Bucket6 = SupplyBundle.LoadAsset<GameObject>("BucketBroccoli_SC");
			Bucket7 = SupplyBundle.LoadAsset<GameObject>("BucketCabbages_SC");
			Bucket8 = SupplyBundle.LoadAsset<GameObject>("BucketCarrots_SC");
			Bucket9 = SupplyBundle.LoadAsset<GameObject>("BucketCoconuts_SC");
			Bucket10 = SupplyBundle.LoadAsset<GameObject>("BucketCorn_SC");
			Bucket11 = SupplyBundle.LoadAsset<GameObject>("BucketCucumber_SC");
			Bucket12 = SupplyBundle.LoadAsset<GameObject>("BucketGrapes_SC");
			Bucket13 = SupplyBundle.LoadAsset<GameObject>("BucketBagettes_SC");
			Bucket14 = SupplyBundle.LoadAsset<GameObject>("BucketLemons_SC");
			Bucket15 = SupplyBundle.LoadAsset<GameObject>("BucketLettuces_SC");
			Bucket16 = SupplyBundle.LoadAsset<GameObject>("BucketLimes_SC");
			Bucket17 = SupplyBundle.LoadAsset<GameObject>("BucketMangoes_SC");
			Bucket18 = SupplyBundle.LoadAsset<GameObject>("BucketMushrooms_SC");
			Bucket19 = SupplyBundle.LoadAsset<GameObject>("BucketOranges_SC");
			Bucket20 = SupplyBundle.LoadAsset<GameObject>("BucketPeaches_SC");
			Bucket21 = SupplyBundle.LoadAsset<GameObject>("BucketPears_SC");
			Bucket22 = SupplyBundle.LoadAsset<GameObject>("BucketPlums_SC");
			Bucket23 = SupplyBundle.LoadAsset<GameObject>("BucketPotatoes_SC");
			Bucket24 = SupplyBundle.LoadAsset<GameObject>("BucketPumpkins_SC");
			//Bucket25 = SupplyBundle.LoadAsset<GameObject>("BucketSpringOnions_SC");
			Bucket26 = SupplyBundle.LoadAsset<GameObject>("BucketSqaush_SC");
			Bucket27 = SupplyBundle.LoadAsset<GameObject>("BucketSweetPotatoes_SC");
			Bucket28 = SupplyBundle.LoadAsset<GameObject>("BucketTomatoes_SC");
			Bucket29 = SupplyBundle.LoadAsset<GameObject>("BucketWatermelons_SC");
			// Boxes
			Box1 = SupplyBundle.LoadAsset<GameObject>("BoxApples_SC");
			Box2 = SupplyBundle.LoadAsset<GameObject>("BoxBagels_SC");
			Box3 = SupplyBundle.LoadAsset<GameObject>("BoxBananas_SC");
			Box4 = SupplyBundle.LoadAsset<GameObject>("BoxBellPeppers_SC");
			Box5 = SupplyBundle.LoadAsset<GameObject>("BoxBlueCheese_SC");
			Box6 = SupplyBundle.LoadAsset<GameObject>("BoxBroccolis_SC");
			Box7 = SupplyBundle.LoadAsset<GameObject>("BoxCabbages_SC");
			Box8 = SupplyBundle.LoadAsset<GameObject>("BoxCarrots_SC");
			Box9 = SupplyBundle.LoadAsset<GameObject>("BoxCoconuts_SC");
			Box10 = SupplyBundle.LoadAsset<GameObject>("BoxCorn_SC");
			Box11 = SupplyBundle.LoadAsset<GameObject>("BoxCucumber_SC");
			Box12 = SupplyBundle.LoadAsset<GameObject>("BoxEdamCheese_SC");
			Box13 = SupplyBundle.LoadAsset<GameObject>("BoxBagettes_SC");
			Box14 = SupplyBundle.LoadAsset<GameObject>("BoxLemons_SC");
			Box15 = SupplyBundle.LoadAsset<GameObject>("BoxLettuce_SC");
			Box16 = SupplyBundle.LoadAsset<GameObject>("BoxLimes_SC");
			Box17 = SupplyBundle.LoadAsset<GameObject>("BoxMangoes_SC");
			Box18 = SupplyBundle.LoadAsset<GameObject>("BoxMilk_SC");
			Box19 = SupplyBundle.LoadAsset<GameObject>("BoxOranges_SC");
			Box20 = SupplyBundle.LoadAsset<GameObject>("BoxPeaches_SC");
			Box21 = SupplyBundle.LoadAsset<GameObject>("BoxPears_SC");
			Box22 = SupplyBundle.LoadAsset<GameObject>("BoxPlums_SC");
			Box23 = SupplyBundle.LoadAsset<GameObject>("BoxPotatoes_SC");
			Box24 = SupplyBundle.LoadAsset<GameObject>("BoxPumpkins_SC");
			Box25 = SupplyBundle.LoadAsset<GameObject>("BoxSpringOnions_SC");
			Box26 = SupplyBundle.LoadAsset<GameObject>("BoxSquash_SC");
			Box27 = SupplyBundle.LoadAsset<GameObject>("BoxSweetPotatoes_SC");
			Box28 = SupplyBundle.LoadAsset<GameObject>("BoxTomatoes_SC");
			Box29 = SupplyBundle.LoadAsset<GameObject>("BoxWatermelons_SC");
			// Bowls
			Bowl1 = SupplyBundle.LoadAsset<GameObject>("BowlApple_SC");
			Bowl2 = SupplyBundle.LoadAsset<GameObject>("BowlBagel_SC");
			Bowl3 = SupplyBundle.LoadAsset<GameObject>("BowlBanana_SC");
			Bowl4 = SupplyBundle.LoadAsset<GameObject>("BowlBellPepper_SC");
			Bowl5 = SupplyBundle.LoadAsset<GameObject>("BowlBlueCheese_SC");
			Bowl6 = SupplyBundle.LoadAsset<GameObject>("BowlBroccoli_SC");
			Bowl7 = SupplyBundle.LoadAsset<GameObject>("BowlCabbage_SC");
			Bowl8 = SupplyBundle.LoadAsset<GameObject>("BowlCarrot_SC");
			Bowl9 = SupplyBundle.LoadAsset<GameObject>("BowlCoconut_SC");
			Bowl10 = SupplyBundle.LoadAsset<GameObject>("BowlCorn_SC");
			Bowl11 = SupplyBundle.LoadAsset<GameObject>("BowlCucumber_SC");
			Bowl12 = SupplyBundle.LoadAsset<GameObject>("BowlEgg_SC");
			Bowl13 = SupplyBundle.LoadAsset<GameObject>("BowlGrape_SC");
			Bowl14 = SupplyBundle.LoadAsset<GameObject>("BowlLemon_SC");
			Bowl15 = SupplyBundle.LoadAsset<GameObject>("BowlLettuce_SC");
			Bowl16 = SupplyBundle.LoadAsset<GameObject>("BowlLime_SC");
			Bowl17 = SupplyBundle.LoadAsset<GameObject>("BowlMangoe_SC");
			Bowl18 = SupplyBundle.LoadAsset<GameObject>("BowlMushroom_SC");
			Bowl19 = SupplyBundle.LoadAsset<GameObject>("BowlOrange_SC");
			Bowl20 = SupplyBundle.LoadAsset<GameObject>("BowlPeach_SC");
			Bowl21 = SupplyBundle.LoadAsset<GameObject>("BowlPear_SC");
			Bowl22 = SupplyBundle.LoadAsset<GameObject>("BowlPlum_SC");
			Bowl23 = SupplyBundle.LoadAsset<GameObject>("BowlPotato_SC");
			Bowl24 = SupplyBundle.LoadAsset<GameObject>("BowlPumpkin_SC");
			Bowl25 = SupplyBundle.LoadAsset<GameObject>("BowlSpringOnion_SC");
			Bowl26 = SupplyBundle.LoadAsset<GameObject>("BowlSquash_SC");
			Bowl27 = SupplyBundle.LoadAsset<GameObject>("BowlSweetPotato_SC");
			Bowl28 = SupplyBundle.LoadAsset<GameObject>("BowlTomato_SC");
			Bowl29 = SupplyBundle.LoadAsset<GameObject>("BowlWatermelon_SC");
			// Fruit
			Food1 = SupplyBundle.LoadAsset<GameObject>("Apple_SC");
			Food2 = SupplyBundle.LoadAsset<GameObject>("Banana_SC");
			Food3 = SupplyBundle.LoadAsset<GameObject>("Coconut_SC");
			Food4 = SupplyBundle.LoadAsset<GameObject>("Lemon_SC");
			Food5 = SupplyBundle.LoadAsset<GameObject>("Mango_SC");
			Food6 = SupplyBundle.LoadAsset<GameObject>("Orange_SC");
			Food7 = SupplyBundle.LoadAsset<GameObject>("Peach_SC");
			Food8 = SupplyBundle.LoadAsset<GameObject>("Pear_SC");
			Food9 = SupplyBundle.LoadAsset<GameObject>("Plum_SC");
			Food10 = SupplyBundle.LoadAsset<GameObject>("Watermelon_SC");
			Food11 = SupplyBundle.LoadAsset<GameObject>("Grapes_SC");
			Food29 = SupplyBundle.LoadAsset<GameObject>("Lime_SC");
			// Veg
			Food12 = SupplyBundle.LoadAsset<GameObject>("BellPepper_SC");
			Food13 = SupplyBundle.LoadAsset<GameObject>("Broccoli_SC");
			Food14 = SupplyBundle.LoadAsset<GameObject>("Cabbage_SC");
			Food15 = SupplyBundle.LoadAsset<GameObject>("Corn_SC");
			Food16 = SupplyBundle.LoadAsset<GameObject>("Cucumber_SC");
			Food17 = SupplyBundle.LoadAsset<GameObject>("Lettuce_SC");
			Food18 = SupplyBundle.LoadAsset<GameObject>("Potato_SC");
			Food19 = SupplyBundle.LoadAsset<GameObject>("Pumpkin_SC");
			Food20 = SupplyBundle.LoadAsset<GameObject>("SpringOnion_SC");
			Food21 = SupplyBundle.LoadAsset<GameObject>("Squash_SC");
			Food22 = SupplyBundle.LoadAsset<GameObject>("SweetPotato_SC");
			Food23 = SupplyBundle.LoadAsset<GameObject>("Tomato_SC");
			Food30 = SupplyBundle.LoadAsset<GameObject>("BrownMushroom_SC");
			// Dairy
			Food24 = SupplyBundle.LoadAsset<GameObject>("BlueCheese_SC");
			Food25 = SupplyBundle.LoadAsset<GameObject>("EdamCheese_SC");
			// Breads
			Food26 = SupplyBundle.LoadAsset<GameObject>("Bagel_SC");
			Food27 = SupplyBundle.LoadAsset<GameObject>("Bagette_SC");
			Food28 = SupplyBundle.LoadAsset<GameObject>("Pretzel_SC");
			// Boxes
			Pickable1 = SupplyBundle.LoadAsset<GameObject>("BoxOfFruits_SC");
			Pickable2 = SupplyBundle.LoadAsset<GameObject>("BoxOfVegetables_SC");
			Pickable3 = SupplyBundle.LoadAsset<GameObject>("BoxOfDairy_SC");
			// Add Prefabs
			CustomPrefab pick1 = new CustomPrefab(Pickable1, true);
			PrefabManager.Instance.AddPrefab(pick1);
			CustomPrefab pick2 = new CustomPrefab(Pickable2, true);
			PrefabManager.Instance.AddPrefab(pick2);
			CustomPrefab pick3 = new CustomPrefab(Pickable3, true);
			PrefabManager.Instance.AddPrefab(pick3);
		}
		private void AddBowlPieces()
		{
			var customPiece1 = new CustomPiece(Bowl1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Apple_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);
			var customPiece2 = new CustomPiece(Bowl2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bagel_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			var customPiece3 = new CustomPiece(Bowl3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Banana_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
			var customPiece4 = new CustomPiece(Bowl4, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BellPepper_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece4);
			var customPiece5 = new CustomPiece(Bowl5, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BlueCheese_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece5);
			var customPiece6 = new CustomPiece(Bowl6, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Broccoli_SC",
						Amount = 4,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece6);
			var customPiece7 = new CustomPiece(Bowl7, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cabbage_SC",
						Amount = 3,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece7);
			var customPiece8 = new CustomPiece(Bowl8, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Carrot",
						Amount = 12,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece8);
			var customPiece9 = new CustomPiece(Bowl9, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Coconut_SC",
						Amount = 4,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece9);
			var customPiece10 = new CustomPiece(Bowl10, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Corn_SC",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece10);
			var customPiece11 = new CustomPiece(Bowl11, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cucumber_SC",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece11);
			/*var customPiece12 = new CustomPiece(Bowl12, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Egg_SC",
						Amount = 2,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece12);*/
			var customPiece13 = new CustomPiece(Bowl13, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Grapes_SC",
						Amount = 2,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece13);
			var customPiece14 = new CustomPiece(Bowl14, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lemon_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece14);
			var customPiece15 = new CustomPiece(Bowl15, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lettuce_SC",
						Amount = 3,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece15);
			var customPiece16 = new CustomPiece(Bowl16, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lime_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece16);
			var customPiece17 = new CustomPiece(Bowl17, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Mango_SC",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece17);
			var customPiece18 = new CustomPiece(Bowl18, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BrownMushroom_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece18);
			var customPiece19 = new CustomPiece(Bowl19, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Orange_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece19);
			var customPiece20 = new CustomPiece(Bowl20, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Peach_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece20);
			var customPiece21 = new CustomPiece(Bowl21, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pear_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece21);
			var customPiece22 = new CustomPiece(Bowl22, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Plum_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece22);
			var customPiece23 = new CustomPiece(Bowl23, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Potato_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece23);
			var customPiece24 = new CustomPiece(Bowl24, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pumpkin_SC",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece24);
			var customPiece25 = new CustomPiece(Bowl25, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SpringOnion_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece25);
			var customPiece26 = new CustomPiece(Bowl26, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Squash_SC",
						Amount = 4,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece26);
			var customPiece27 = new CustomPiece(Bowl27, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SweetPotato_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece27);
			var customPiece28 = new CustomPiece(Bowl28, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Tomato_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece28);
			var customPiece29 = new CustomPiece(Bowl29, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 1,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Watermelon_SC",
						Amount = 3,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece29);

		}
		private void AddBoxPieces()
		{
			var customPiece1 = new CustomPiece(Box1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Apple_SC",
						Amount = 24,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);
			var customPiece2 = new CustomPiece(Box2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bagel_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			var customPiece3 = new CustomPiece(Box3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Banana_SC",
						Amount = 24,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
			var customPiece4 = new CustomPiece(Box4, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BellPepper_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece4);
			var customPiece5 = new CustomPiece(Box5, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BlueCheese_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece5);
			var customPiece6 = new CustomPiece(Box6, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Broccoli_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece6);
			var customPiece7 = new CustomPiece(Box7, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cabbage_SC",
						Amount = 9,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece7);
			var customPiece8 = new CustomPiece(Box8, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Carrot",
						Amount = 17,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece8);
			var customPiece9 = new CustomPiece(Box9, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Coconut_SC",
						Amount = 12,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece9);
			var customPiece10 = new CustomPiece(Box10, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Corn_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece10);
			var customPiece11 = new CustomPiece(Box11, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cucumber_SC",
						Amount = 17,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece11);
			var customPiece12 = new CustomPiece(Box12, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "EdamCheese_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece12);
			var customPiece13 = new CustomPiece(Box13, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bagette_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece13);
			var customPiece14 = new CustomPiece(Box14, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lemon_SC",
						Amount = 15,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece14);
			var customPiece15 = new CustomPiece(Box15, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lettuce_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece15);
			var customPiece16 = new CustomPiece(Box16, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lime_SC",
						Amount = 23,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece16);
			var customPiece17 = new CustomPiece(Box17, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Mango_SC",
						Amount = 19,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece17);
			/*var customPiece18 = new CustomPiece(Box18, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Milk_FYA",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece18);*/
			var customPiece19 = new CustomPiece(Box19, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Orange_SC",
						Amount = 12,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece19);
			var customPiece20 = new CustomPiece(Box20, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Peach_SC",
						Amount = 19,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece20);
			var customPiece21 = new CustomPiece(Box21, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pear_SC",
						Amount = 15,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece21);
			var customPiece22 = new CustomPiece(Box22, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Plum_SC",
						Amount = 18,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece22);
			var customPiece23 = new CustomPiece(Box23, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Potato_SC",
						Amount = 18,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece23);
			var customPiece24 = new CustomPiece(Box24, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pumpkin_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece24);
			var customPiece25 = new CustomPiece(Box25, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SpringOnion_SC",
						Amount = 12,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece25);
			var customPiece26 = new CustomPiece(Box26, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Squash_SC",
						Amount = 15,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece26);
			var customPiece27 = new CustomPiece(Box27, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SweetPotato_SC",
						Amount = 14,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece27);
			var customPiece28 = new CustomPiece(Box28, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Tomato_SC",
						Amount = 22,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece28);
			var customPiece29 = new CustomPiece(Box29, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Watermelon_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece29);

		}
		private void AddBucketPieces()
		{
			var customPiece1 = new CustomPiece(Bucket1, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Apple_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece1);
			Debug.Log("Supply Crates: Bucket1");
			/*var customPiece2 = new CustomPiece(Bucket2, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bagette_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece2);
			Debug.Log("Supply Crates: Bucket2");*/
			var customPiece3 = new CustomPiece(Bucket3, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Banana_SC",
						Amount = 37,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece3);
			Debug.Log("Supply Crates: Bucket3");
			var customPiece4 = new CustomPiece(Bucket4, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BellPepper_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece4);
			Debug.Log("Supply Crates: Bucket4");
			/*var customPiece5 = new CustomPiece(Bucket5, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BlueCheese_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece5);
			Debug.Log("Supply Crates: Bucket5");*/
			var customPiece6 = new CustomPiece(Bucket6, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Broccoli_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece6);
			Debug.Log("Supply Crates: Bucket6");
			var customPiece7 = new CustomPiece(Bucket7, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cabbage_SC",
						Amount = 5,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece7);
			Debug.Log("Supply Crates: Bucket7");
			var customPiece8 = new CustomPiece(Bucket8, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Carrot",
						Amount = 17,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece8);
			Debug.Log("Supply Crates: Bucket8");
			var customPiece9 = new CustomPiece(Bucket9, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Coconut_SC",
						Amount = 9,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece9);
			Debug.Log("Supply Crates: Bucket9");
			var customPiece10 = new CustomPiece(Bucket10, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Corn_SC",
						Amount = 9,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece10);
			Debug.Log("Supply Crates: Bucket10");
			var customPiece11 = new CustomPiece(Bucket11, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Cucumber_SC",
						Amount = 16,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece11);
			Debug.Log("Supply Crates: Bucket11");
			var customPiece12 = new CustomPiece(Bucket12, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Grapes_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece12);
			Debug.Log("Supply Crates: Bucket12");
			var customPiece13 = new CustomPiece(Bucket13, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Bagette_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece13);
			Debug.Log("Supply Crates: Bucket13");
			var customPiece14 = new CustomPiece(Bucket14, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lemon_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece14);
			Debug.Log("Supply Crates: Bucket14");
			var customPiece15 = new CustomPiece(Bucket15, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lettuce_SC",
						Amount = 7,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece15);
			Debug.Log("Supply Crates: Bucket15");
			var customPiece16 = new CustomPiece(Bucket16, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Lime_SC",
						Amount = 32,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece16);
			Debug.Log("Supply Crates: Bucket16");
			var customPiece17 = new CustomPiece(Bucket17, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Mango_SC",
						Amount = 10,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece17);
			Debug.Log("Supply Crates: Bucket17");
			var customPiece18 = new CustomPiece(Bucket18, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "BrownMushroom_SC",
						Amount = 14,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece18);
			Debug.Log("Supply Crates: Bucket18");
			var customPiece19 = new CustomPiece(Bucket19, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Orange_SC",
						Amount = 13,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece19);
			Debug.Log("Supply Crates: Bucket19");
			var customPiece20 = new CustomPiece(Bucket20, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Peach_SC",
						Amount = 30,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece20);
			Debug.Log("Supply Crates: Bucket20");
			var customPiece21 = new CustomPiece(Bucket21, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pear_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece21);
			Debug.Log("Supply Crates: Bucket21");
			var customPiece22 = new CustomPiece(Bucket22, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Plum_SC",
						Amount = 41,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece22);
			Debug.Log("Supply Crates: Bucket22");
			var customPiece23 = new CustomPiece(Bucket23, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Potato_SC",
						Amount = 11,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece23);
			Debug.Log("Supply Crates: Bucket23");
			var customPiece24 = new CustomPiece(Bucket24, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Pumpkin_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece24);
			Debug.Log("Supply Crates: Bucket24");
			/*var customPiece25 = new CustomPiece(Bucket25, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SpringOnion_SC",
						Amount = 12,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece25);
			Debug.Log("Supply Crates: Bucket25");*/
			var customPiece26 = new CustomPiece(Bucket26, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Squash_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece26);
			Debug.Log("Supply Crates: Bucket26");
			var customPiece27 = new CustomPiece(Bucket27, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "SweetPotato_SC",
						Amount = 8,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece27);
			Debug.Log("Supply Crates: Bucket27");
			var customPiece28 = new CustomPiece(Bucket28, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Tomato_SC",
						Amount = 21,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece28);
			Debug.Log("Supply Crates: Bucket28");
			var customPiece29 = new CustomPiece(Bucket29, true, new PieceConfig
			{
				PieceTable = "_HammerPieceTable",
				Category = "Farm",
				Requirements = new RequirementConfig[2]
				{
					new RequirementConfig
					{
						Item = "Wood",
						Amount = 2,
						Recover = true
					},
					new RequirementConfig
					{
						Item = "Watermelon_SC",
						Amount = 6,
						Recover = true
					}
				}
			});
			PieceManager.Instance.AddPiece(customPiece29);
			Debug.Log("Supply Crates: Bucket29");
		}
		private void AddItems()
		{
            try
			{
				// Fruit
				GameObject dropable1 = Food1;
				CustomItem customItem1 = new CustomItem(dropable1, false);
				ItemManager.Instance.AddItem(customItem1);
				GameObject dropable2 = Food2;
				CustomItem customItem2 = new CustomItem(dropable2, false);
				ItemManager.Instance.AddItem(customItem2);
				GameObject dropable3 = Food3;
				CustomItem customItem3 = new CustomItem(dropable3, false);
				ItemManager.Instance.AddItem(customItem3);
				GameObject dropable4 = Food4;
				CustomItem customItem4 = new CustomItem(dropable4, false);
				ItemManager.Instance.AddItem(customItem4);
				GameObject dropable5 = Food5;
				CustomItem customItem5 = new CustomItem(dropable5, false);
				ItemManager.Instance.AddItem(customItem5);
				GameObject dropable6 = Food6;
				CustomItem customItem6 = new CustomItem(dropable6, false);
				ItemManager.Instance.AddItem(customItem6);
				GameObject dropable7 = Food7;
				CustomItem customItem7 = new CustomItem(dropable7, false);
				ItemManager.Instance.AddItem(customItem7);
				GameObject dropable8 = Food8;
				CustomItem customItem8 = new CustomItem(dropable8, false);
				ItemManager.Instance.AddItem(customItem8);
				GameObject dropable9 = Food9;
				CustomItem customItem9 = new CustomItem(dropable9, false);
				ItemManager.Instance.AddItem(customItem9);
				GameObject dropable10 = Food10;
				CustomItem customItem10 = new CustomItem(dropable10, false);
				ItemManager.Instance.AddItem(customItem10);
				GameObject dropable29 = Food29;
				CustomItem customItem29 = new CustomItem(dropable29, false);
				ItemManager.Instance.AddItem(customItem29);
				// Veg
				GameObject dropable11 = Food11;
				CustomItem customItem11 = new CustomItem(dropable11, false);
				ItemManager.Instance.AddItem(customItem11);
				GameObject dropable12 = Food12;
				CustomItem customItem12 = new CustomItem(dropable12, false);
				ItemManager.Instance.AddItem(customItem12);
				GameObject dropable13 = Food13;
				CustomItem customItem13 = new CustomItem(dropable13, false);
				ItemManager.Instance.AddItem(customItem13);
				GameObject dropable14 = Food14;
				CustomItem customItem14 = new CustomItem(dropable14, false);
				ItemManager.Instance.AddItem(customItem14);
				GameObject dropable15 = Food15;
				CustomItem customItem15 = new CustomItem(dropable15, false);
				ItemManager.Instance.AddItem(customItem15);
				GameObject dropable16 = Food16;
				CustomItem customItem16 = new CustomItem(dropable16, false);
				ItemManager.Instance.AddItem(customItem16);
				GameObject dropable17 = Food17;
				CustomItem customItem17 = new CustomItem(dropable17, false);
				ItemManager.Instance.AddItem(customItem17);
				GameObject dropable18 = Food18;
				CustomItem customItem18 = new CustomItem(dropable18, false);
				ItemManager.Instance.AddItem(customItem18);
				GameObject dropable19 = Food19;
				CustomItem customItem19 = new CustomItem(dropable19, false);
				ItemManager.Instance.AddItem(customItem19);
				GameObject dropable20 = Food20;
				CustomItem customItem20 = new CustomItem(dropable20, false);
				ItemManager.Instance.AddItem(customItem20);
				GameObject dropable21 = Food21;
				CustomItem customItem21 = new CustomItem(dropable21, false);
				ItemManager.Instance.AddItem(customItem21);
				GameObject dropable22 = Food22;
				CustomItem customItem22 = new CustomItem(dropable22, false);
				ItemManager.Instance.AddItem(customItem22);
				GameObject dropable23 = Food23;
				CustomItem customItem23 = new CustomItem(dropable23, false);
				ItemManager.Instance.AddItem(customItem23);
				GameObject dropable30 = Food30;
				CustomItem customItem30 = new CustomItem(dropable30, false);
				ItemManager.Instance.AddItem(customItem30);
				// Dairy
				GameObject dropable24 = Food24;
				CustomItem customItem24 = new CustomItem(dropable24, false);
				ItemManager.Instance.AddItem(customItem24);
				GameObject dropable25 = Food25;
				CustomItem customItem25 = new CustomItem(dropable25, false);
				ItemManager.Instance.AddItem(customItem25);
				// Breads
				GameObject dropable26 = Food26;
				CustomItem customItem26 = new CustomItem(dropable26, false);
				ItemManager.Instance.AddItem(customItem26);
				GameObject dropable27 = Food27;
				CustomItem customItem27 = new CustomItem(dropable27, false);
				ItemManager.Instance.AddItem(customItem27);
				GameObject dropable28 = Food28;
				CustomItem customItem28 = new CustomItem(dropable28, false);
				ItemManager.Instance.AddItem(customItem28);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding fruit: {ex}");
			}
            /*finally
			{
				SupplyBundle?.Unload(unloadAllLoadedObjects: false);
			}*/
		}
		private void UpdateLocations()
        {
            try
			{
				// Load objects to add to locations
				var fruitPrefab = PrefabManager.Instance.GetPrefab("BoxOfFruits_SC");
				var vegPrefab = PrefabManager.Instance.GetPrefab("BoxOfVegetables_SC");
				var dairyPrefab = PrefabManager.Instance.GetPrefab("BoxOfDairy_SC");
				Debug.Log("Supply Crates: Boxes");
				// Biomes
				if (MeadowsEnable.Value == true)
                {
					// Load Locations to Edit
					var house1Location = ZoneManager.Instance.GetZoneLocation("WoodHouse8");
					Debug.Log("Supply Crates: House1");
					var house2Location = ZoneManager.Instance.GetZoneLocation("WoodHouse6");
					Debug.Log("Supply Crates: House2");
					var house3Location = ZoneManager.Instance.GetZoneLocation("WoodHouse3");
					Debug.Log("Supply Crates: House3");
					var house4Location = ZoneManager.Instance.GetZoneLocation("WoodHouse11");
					Debug.Log("Supply Crates: House4");
					var house5Location = ZoneManager.Instance.GetZoneLocation("WoodHouse2");
					Debug.Log("Supply Crates: House5");
					var house6Location = ZoneManager.Instance.GetZoneLocation("WoodHouse7");
					Debug.Log("Supply Crates: House6");
					var house7Location = ZoneManager.Instance.GetZoneLocation("WoodHouse13");
					Debug.Log("Supply Crates: House7");
					var house8Location = ZoneManager.Instance.GetZoneLocation("Dolmen03");
					Debug.Log("Supply Crates: House8");
					// Add objects to locations
					// WoodHouse8
					var house1Fruit = Instantiate(fruitPrefab, house1Location.m_prefab.transform);
					house1Fruit.name = fruitPrefab.name;
					house1Fruit.transform.localPosition = new Vector3(3.379997f, 0f, 3.62999f); 
					ZoneSystem.PrepareNetViews(house1Location.m_prefab, house1Location.m_netViews);
					Debug.Log("Supply Crates: Loc1");
					// WoodHouse6
					var house2Fruit = Instantiate(fruitPrefab, house2Location.m_prefab.transform);
					house2Fruit.name = fruitPrefab.name;
					house2Fruit.transform.localPosition = new Vector3(-0.4f, 0f, 2.8f);
					ZoneSystem.PrepareNetViews(house2Location.m_prefab, house2Location.m_netViews);
					Debug.Log("Supply Crates: Loc2");
					// WoodHouse3
					var house3Fruit = Instantiate(fruitPrefab, house3Location.m_prefab.transform);
					house3Fruit.name = fruitPrefab.name;
					house3Fruit.transform.localPosition = new Vector3(-4.48f, 0f, 3f);
					ZoneSystem.PrepareNetViews(house3Location.m_prefab, house3Location.m_netViews);
					Debug.Log("Supply Crates: Loc3");
					// WoodHouse11
					var house4Fruit = Instantiate(fruitPrefab, house4Location.m_prefab.transform);
					house4Fruit.name = fruitPrefab.name;
					house4Fruit.transform.localPosition = new Vector3(1.55f, 0f, 3.34f);
					ZoneSystem.PrepareNetViews(house4Location.m_prefab, house4Location.m_netViews);
					Debug.Log("Supply Crates: Loc4");
					// WoodHouse2
					var house1Veg = Instantiate(vegPrefab, house5Location.m_prefab.transform);
					house1Veg.name = vegPrefab.name;
					house1Veg.transform.localPosition = new Vector3(1.7f, 0f, 2.8f);
					ZoneSystem.PrepareNetViews(house5Location.m_prefab, house5Location.m_netViews);
					Debug.Log("Supply Crates: Loc5");
					// WoodHouse7
					var house2Veg = Instantiate(vegPrefab, house6Location.m_prefab.transform);
					house2Veg.name = vegPrefab.name;
					house2Veg.transform.localPosition = new Vector3(1.9f, 0f, 1f);
					ZoneSystem.PrepareNetViews(house6Location.m_prefab, house6Location.m_netViews);
					Debug.Log("Supply Crates: Loc6");
					// WoodHouse13
					var house3Veg = Instantiate(vegPrefab, house7Location.m_prefab.transform);
					house3Veg.name = vegPrefab.name;
					house3Veg.transform.localPosition = new Vector3(3.1f, 0f, -2.55f);
					ZoneSystem.PrepareNetViews(house7Location.m_prefab, house7Location.m_netViews);
					Debug.Log("Supply Crates: Loc7");
					// WoodHouse13
					var house4Veg = Instantiate(vegPrefab, house8Location.m_prefab.transform);
					house4Veg.name = vegPrefab.name;
					house4Veg.transform.localPosition = new Vector3(1.53f, 0f, 3.54f);
					ZoneSystem.PrepareNetViews(house8Location.m_prefab, house8Location.m_netViews);
					Debug.Log("Supply Crates: Loc8");
				}
				if (BlackForestEnable.Value == true)
				{
					// Load Locations to Edit
					var trollLocation = ZoneManager.Instance.GetZoneLocation("TrollCave02");
					var runeLocation = ZoneManager.Instance.GetZoneLocation("Runestone_Greydwarfs");
					var crypt2Location = ZoneManager.Instance.GetZoneLocation("Crypt2");
					var ruin1Location = ZoneManager.Instance.GetZoneLocation("Ruin1");
					var crypt3Location = ZoneManager.Instance.GetZoneLocation("Crypt3");
					var tower1Location = ZoneManager.Instance.GetZoneLocation("StoneTowerRuins03");
					// Add objects to locations
					// TrollCave02
					var trollFruit = Instantiate(fruitPrefab, trollLocation.m_prefab.transform);
					trollFruit.name = fruitPrefab.name;
					trollFruit.transform.localPosition = new Vector3(4.8f, 0f, 6f);
					ZoneSystem.PrepareNetViews(trollLocation.m_prefab, trollLocation.m_netViews);
					Debug.Log("Supply Crates: Loc9");
					// Runestone_Greydwarfs
					var runeFruit = Instantiate(fruitPrefab, runeLocation.m_prefab.transform);
					runeFruit.name = fruitPrefab.name;
					runeFruit.transform.localPosition = new Vector3(0f, 0f, 1.7f);
					ZoneSystem.PrepareNetViews(runeLocation.m_prefab, runeLocation.m_netViews);
					Debug.Log("Supply Crates: Loc10");
					// Crypt2
					var cryptFruit = Instantiate(fruitPrefab, crypt2Location.m_prefab.transform);
					cryptFruit.name = fruitPrefab.name;
					cryptFruit.transform.localPosition = new Vector3(-3.23f, 0f, -2.65f);
					ZoneSystem.PrepareNetViews(crypt2Location.m_prefab, crypt2Location.m_netViews);
					Debug.Log("Supply Crates: Loc11");
					// Ruin1
					var ruinFruit = Instantiate(fruitPrefab, ruin1Location.m_prefab.transform);
					ruinFruit.name = fruitPrefab.name;
					ruinFruit.transform.localPosition = new Vector3(0f, 0f, 6.3f);
					ZoneSystem.PrepareNetViews(ruin1Location.m_prefab, ruin1Location.m_netViews);
					Debug.Log("Supply Crates: Loc12");
					// Crypt3
					var cryptVeg = Instantiate(vegPrefab, crypt3Location.m_prefab.transform);
					cryptVeg.name = vegPrefab.name;
					cryptVeg.transform.localPosition = new Vector3(1.88f, -2.75f, 1.94f);
					ZoneSystem.PrepareNetViews(crypt3Location.m_prefab, crypt3Location.m_netViews);
					Debug.Log("Supply Crates: Loc13");
					// StoneTowerRuins03
					var tower1Veg = Instantiate(vegPrefab, tower1Location.m_prefab.transform);
					tower1Veg.name = vegPrefab.name;
					tower1Veg.transform.localPosition = new Vector3(2.98f, 0f, -5.24f);
					ZoneSystem.PrepareNetViews(tower1Location.m_prefab, tower1Location.m_netViews);
					Debug.Log("Supply Crates: Loc14");
				}
				if (SwampEnable.Value == true)
                {
					var rune1Location = ZoneManager.Instance.GetZoneLocation("Runestone_Draugr");
					var crypt4Location = ZoneManager.Instance.GetZoneLocation("SunkenCrypt1");
					var crypt5Location = ZoneManager.Instance.GetZoneLocation("SunkenCrypt4");
					var graveLocation = ZoneManager.Instance.GetZoneLocation("Grave1");
					var crypt6Location = ZoneManager.Instance.GetZoneLocation("SunkenCrypt3");
					var hut1Location = ZoneManager.Instance.GetZoneLocation("SwampHut2");
					var hut2Location = ZoneManager.Instance.GetZoneLocation("SwampHut4");
					// Runestone_Draugr
					var runeVeg = Instantiate(vegPrefab, rune1Location.m_prefab.transform);
					runeVeg.name = vegPrefab.name;
					runeVeg.transform.localPosition = new Vector3(2.98f, 0f, -5.24f);
					ZoneSystem.PrepareNetViews(rune1Location.m_prefab, rune1Location.m_netViews);
					Debug.Log("Supply Crates: Loc15");
					// SunkenCrypt1
					var suncrypt1Veg = Instantiate(vegPrefab, crypt4Location.m_prefab.transform);
					suncrypt1Veg.name = vegPrefab.name;
					suncrypt1Veg.transform.localPosition = new Vector3(-4.2f, 0f, 1f);
					ZoneSystem.PrepareNetViews(crypt4Location.m_prefab, crypt4Location.m_netViews);
					Debug.Log("Supply Crates: Loc16");
					// SunkenCrypt4
					var suncrypt2Veg = Instantiate(vegPrefab, crypt5Location.m_prefab.transform);
					suncrypt2Veg.name = vegPrefab.name;
					suncrypt2Veg.transform.localPosition = new Vector3(-3.64f, 0f, -2.77f);
					ZoneSystem.PrepareNetViews(crypt5Location.m_prefab, crypt5Location.m_netViews);
					Debug.Log("Supply Crates: Loc17");
					// Grave1
					var graveDairy = Instantiate(dairyPrefab, graveLocation.m_prefab.transform);
					graveDairy.name = dairyPrefab.name;
					graveDairy.transform.localPosition = new Vector3(4.12f, 0f, -1.65f);
					ZoneSystem.PrepareNetViews(graveLocation.m_prefab, graveLocation.m_netViews);
					Debug.Log("Supply Crates: Loc18");
					// SunkenCrypt3
					var crypt1Dairy = Instantiate(dairyPrefab, crypt6Location.m_prefab.transform);
					crypt1Dairy.name = dairyPrefab.name;
					crypt1Dairy.transform.localPosition = new Vector3(-3.48f, 0f, 1f);
					ZoneSystem.PrepareNetViews(crypt6Location.m_prefab, crypt6Location.m_netViews);
					Debug.Log("Supply Crates: Loc19");
					// SwampHut2
					var hut1Dairy = Instantiate(dairyPrefab, hut1Location.m_prefab.transform);
					hut1Dairy.name = dairyPrefab.name;
					hut1Dairy.transform.localPosition = new Vector3(-3.52f, 0f, 0.116f);
					ZoneSystem.PrepareNetViews(hut1Location.m_prefab, hut1Location.m_netViews);
					Debug.Log("Supply Crates: Loc20");
					// SwampHut4
					var hut2Dairy = Instantiate(dairyPrefab, hut2Location.m_prefab.transform);
					hut2Dairy.name = dairyPrefab.name;
					hut2Dairy.transform.localPosition = new Vector3(6f, 1.05f, 0f);
					ZoneSystem.PrepareNetViews(hut2Location.m_prefab, hut2Location.m_netViews);
					Debug.Log("Supply Crates: Loc21");
				}
				if (MountainEnable.Value == true)
				{
					var lore1Location = ZoneManager.Instance.GetZoneLocation("DrakeLorestone");
					var rune2Location = ZoneManager.Instance.GetZoneLocation("Runestone_Mountains");
					var cabin1Location = ZoneManager.Instance.GetZoneLocation("AbandonedLogCabin01");
					var cabin2Location = ZoneManager.Instance.GetZoneLocation("AbandonedLogCabin03");
					var tower1Location = ZoneManager.Instance.GetZoneLocation("StoneTowerRuins04");
					// DrakeLorestone
					var lore1Dairy = Instantiate(dairyPrefab, lore1Location.m_prefab.transform);
					lore1Dairy.name = dairyPrefab.name;
					lore1Dairy.transform.localPosition = new Vector3(-0.27f, 0f, 1.82f);
					ZoneSystem.PrepareNetViews(lore1Location.m_prefab, lore1Location.m_netViews);
					Debug.Log("Supply Crates: Loc22");
					// Runestone_Mountains
					var rune2Dairy = Instantiate(dairyPrefab, rune2Location.m_prefab.transform);
					rune2Dairy.name = dairyPrefab.name;
					rune2Dairy.transform.localPosition = new Vector3(0f, 0f, 2f);
					ZoneSystem.PrepareNetViews(rune2Location.m_prefab, rune2Location.m_netViews);
					Debug.Log("Supply Crates: Loc23");
					// AbandonedLogCabin01
					/*var cabin1Dairy = Instantiate(dairyPrefab, cabin1Location.m_prefab.transform);
					cabin1Dairy.name = dairyPrefab.name;
					cabin1Dairy.transform.localPosition = new Vector3(-2.4f, 0f, -3.9f);
					ZoneSystem.PrepareNetViews(cabin1Location.m_prefab, cabin1Location.m_netViews);
					Debug.Log("Supply Crates: Loc24");*/
					// AbandonedLogCabin03
					var cabin2Dairy = Instantiate(dairyPrefab, cabin2Location.m_prefab.transform);
					cabin2Dairy.name = dairyPrefab.name;
					cabin2Dairy.transform.localPosition = new Vector3(2.363f, 0f, 3.836f);
					ZoneSystem.PrepareNetViews(cabin2Location.m_prefab, cabin2Location.m_netViews);
					Debug.Log("Supply Crates: Loc25");
					// StoneTowerRuins04
					var tower1Dairy = Instantiate(dairyPrefab, tower1Location.m_prefab.transform);
					tower1Dairy.name = dairyPrefab.name;
					tower1Dairy.transform.localPosition = new Vector3(-2.69f, 0f, -0.86f);
					ZoneSystem.PrepareNetViews(tower1Location.m_prefab, tower1Location.m_netViews);
					Debug.Log("Supply Crates: Loc26");

				}
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating Locations: {ex}");
			}
            finally
			{
				SupplyBundle?.Unload(unloadAllLoadedObjects: false);
			}
		}
		private void ValHavestAdditions()
        {
            try
			{
				var pick1 = PrefabManager.Instance.GetPrefab("BoxOfFruits_SC");
				var pick2 = PrefabManager.Instance.GetPrefab("BoxOfVegetables_SC");
				var pick3 = PrefabManager.Instance.GetPrefab("BoxOfDairy_SC");
				// Fruit
				var vharv1 = PrefabManager.Instance.GetPrefab("apple");
				// Misc
				var vharv2 = PrefabManager.Instance.GetPrefab("salt");
				// Veg
				var vharv3 = PrefabManager.Instance.GetPrefab("garlic");
				var vharv4 = PrefabManager.Instance.GetPrefab("pepper");
				var vharv5 = PrefabManager.Instance.GetPrefab("pumpkin");
				var vharv6 = PrefabManager.Instance.GetPrefab("tomato");
				var vharv7 = PrefabManager.Instance.GetPrefab("potato");
				var vharv8 = PrefabManager.Instance.GetPrefab("rice");
				var vharv9 = PrefabManager.Instance.GetPrefab("bonemeal");
				pick1.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData 
				{ 
					m_item = vharv1,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick3.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv2,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv3,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv4,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv5,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv6,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv7,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv8,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
				pick2.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = vharv9,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});

			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating for ValHarvest: {ex}");
			}
            finally
            {
				PrefabManager.OnVanillaPrefabsAvailable -= ValHavestAdditions;
            }
		}
		private void FYAAdditions()
        {
            try
			{   
				// Items
				var apple = PrefabManager.Cache.GetPrefab<ItemDrop>("Apple_SC");
				var pear = PrefabManager.Cache.GetPrefab<ItemDrop>("Pear_SC");
				var cabbage = PrefabManager.Cache.GetPrefab<ItemDrop>("Cabbage_SC");
				var lettuce = PrefabManager.Cache.GetPrefab<ItemDrop>("Lettuce_SC");
				var corn = PrefabManager.Cache.GetPrefab<ItemDrop>("Corn_SC");
				var bagel = PrefabManager.Cache.GetPrefab<ItemDrop>("Bagel_SC");
				var bagette = PrefabManager.Cache.GetPrefab<ItemDrop>("Bagette_SC");
				var mushroom = PrefabManager.Cache.GetPrefab<ItemDrop>("BrownMushroom_SC");
				// Goat
				var goat = CreatureManager.Instance.GetCreaturePrefab("Goat_FYA");
				var animal1 = goat.GetComponent<MonsterAI>();
					animal1.m_consumeItems.Add(apple);
					animal1.m_consumeItems.Add(pear);
				// Sheep
				var sheep = CreatureManager.Instance.GetCreaturePrefab("Sheep_FYA");
				var animal2 = goat.GetComponent<MonsterAI>();
					animal2.m_consumeItems.Add(cabbage);
					animal2.m_consumeItems.Add(lettuce);
				// ChickenB
				var chickenB = CreatureManager.Instance.GetCreaturePrefab("ChickenB_FYA");
				var animal3 = goat.GetComponent<MonsterAI>();
					animal3.m_consumeItems.Add(corn);
					animal3.m_consumeItems.Add(bagel);
					animal3.m_consumeItems.Add(bagette);
				// ChickenBW
				var chickenBW = CreatureManager.Instance.GetCreaturePrefab("ChickenBW_FYA");
				var animal4 = goat.GetComponent<MonsterAI>();
					animal4.m_consumeItems.Add(corn);
					animal4.m_consumeItems.Add(bagel);
					animal4.m_consumeItems.Add(bagette);
				// ChickenBW
				var chickenW = CreatureManager.Instance.GetCreaturePrefab("ChickenW_FYA");
				var animal5 = goat.GetComponent<MonsterAI>();
					animal5.m_consumeItems.Add(corn);
					animal5.m_consumeItems.Add(bagel);
					animal5.m_consumeItems.Add(bagette);
				// TurkeyB
				var turkeyB = CreatureManager.Instance.GetCreaturePrefab("TurkeyB_FYA");
				var animal6 = goat.GetComponent<MonsterAI>();
					animal6.m_consumeItems.Add(corn);
					animal6.m_consumeItems.Add(bagel);
					animal6.m_consumeItems.Add(bagette);
				// TurkeyR
				var turkeyR = CreatureManager.Instance.GetCreaturePrefab("TurkeyR_FYA");
				var animal7 = goat.GetComponent<MonsterAI>();
					animal7.m_consumeItems.Add(corn);
					animal7.m_consumeItems.Add(bagel);
					animal7.m_consumeItems.Add(bagette);
				// TurkeyW
				var turkeyW = CreatureManager.Instance.GetCreaturePrefab("TurkeyW_FYA");
				var animal8 = goat.GetComponent<MonsterAI>();
					animal8.m_consumeItems.Add(corn);
					animal8.m_consumeItems.Add(bagel);
					animal8.m_consumeItems.Add(bagette);
				// CowB
				var cowB = CreatureManager.Instance.GetCreaturePrefab("CowB_FYA");
				var animal9 = goat.GetComponent<MonsterAI>();
					animal9.m_consumeItems.Add(cabbage);
					animal9.m_consumeItems.Add(lettuce);
				// CowBW
				var cowBW = CreatureManager.Instance.GetCreaturePrefab("CowBW_FYA");
				var anima20 = goat.GetComponent<MonsterAI>();
					anima20.m_consumeItems.Add(cabbage);
					anima20.m_consumeItems.Add(lettuce);
				// Goose
				var goose = CreatureManager.Instance.GetCreaturePrefab("Goose_FYA");
				var anima21 = goat.GetComponent<MonsterAI>();
					anima21.m_consumeItems.Add(corn);
					anima21.m_consumeItems.Add(bagel);
					anima21.m_consumeItems.Add(bagette);
				// Highland
				var highland = CreatureManager.Instance.GetCreaturePrefab("Highland_FYA");
				var anima22 = goat.GetComponent<MonsterAI>();
					anima22.m_consumeItems.Add(cabbage);
					anima22.m_consumeItems.Add(lettuce);
				// LonghornB
				var longhornB = CreatureManager.Instance.GetCreaturePrefab("LonghornB_FYA");
				var anima23 = goat.GetComponent<MonsterAI>();
					anima23.m_consumeItems.Add(cabbage);
					anima23.m_consumeItems.Add(lettuce);
				// LonghornW
				var longhornW = CreatureManager.Instance.GetCreaturePrefab("LonghornW_FYA");
				var anima24 = goat.GetComponent<MonsterAI>();
					anima24.m_consumeItems.Add(cabbage);
					anima24.m_consumeItems.Add(lettuce);
				// Mulefoot
				var mulefoot = CreatureManager.Instance.GetCreaturePrefab("Mulefoot_FYA");
				var anima25 = goat.GetComponent<MonsterAI>();
					anima25.m_consumeItems.Add(cabbage);
					anima25.m_consumeItems.Add(lettuce);
					anima25.m_consumeItems.Add(corn);
					anima25.m_consumeItems.Add(bagel);
					anima25.m_consumeItems.Add(bagette);
					anima25.m_consumeItems.Add(apple);
					anima25.m_consumeItems.Add(pear);
					anima25.m_consumeItems.Add(mushroom);
				// OldSpots
				var oldspots = CreatureManager.Instance.GetCreaturePrefab("OldSpots_FYA");
				var anima26 = goat.GetComponent<MonsterAI>();
					anima26.m_consumeItems.Add(cabbage);
					anima26.m_consumeItems.Add(lettuce);
					anima26.m_consumeItems.Add(corn);
					anima26.m_consumeItems.Add(bagel);
					anima26.m_consumeItems.Add(bagette);
					anima26.m_consumeItems.Add(apple);
					anima26.m_consumeItems.Add(pear);
					anima26.m_consumeItems.Add(mushroom);
				// OldSpots
				var oxford = CreatureManager.Instance.GetCreaturePrefab("Oxford_FYA");
				var anima27 = goat.GetComponent<MonsterAI>();
					anima27.m_consumeItems.Add(cabbage);
					anima27.m_consumeItems.Add(lettuce);
					anima27.m_consumeItems.Add(corn);
					anima27.m_consumeItems.Add(bagel);
					anima27.m_consumeItems.Add(bagette);
					anima27.m_consumeItems.Add(apple);
					anima27.m_consumeItems.Add(pear);
					anima27.m_consumeItems.Add(mushroom);
				// Chester
				var chester = CreatureManager.Instance.GetCreaturePrefab("Chester_FYA");
				var anima28 = goat.GetComponent<MonsterAI>();
					anima28.m_consumeItems.Add(cabbage);
					anima28.m_consumeItems.Add(lettuce);
					anima28.m_consumeItems.Add(corn);
					anima28.m_consumeItems.Add(bagel);
					anima28.m_consumeItems.Add(bagette);
					anima28.m_consumeItems.Add(apple);
					anima28.m_consumeItems.Add(pear);
					anima28.m_consumeItems.Add(mushroom);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating for FYA: {ex}");
			}
            finally
            {
				CreatureManager.OnVanillaCreaturesAvailable -= FYAAdditions;
			}
        }
		private void BAAdditions()
        {
            try
			{
				var pick1 = PrefabManager.Instance.GetPrefab("BoxOfDairy_SC");
				var ba1 = PrefabManager.Instance.GetPrefab("rk_egg");
				pick1.GetComponent<Pickable>().m_extraDrops.m_drops.Add(new DropTable.DropData
				{
					m_item = ba1,
					m_stackMin = 1,
					m_stackMax = 3,
					m_weight = 1
				});
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while updating for Bone Appetite: {ex}");
			}
			finally
			{
				PrefabManager.OnVanillaPrefabsAvailable -= BAAdditions;
			}
		}
	}
}
