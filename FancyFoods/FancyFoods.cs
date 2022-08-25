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

namespace FancyFoods
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.rockerkitten.boneappetit", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.SupplyCrates", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.FarmyardAnimals", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("horemvore.FruitTrees", BepInDependency.DependencyFlags.HardDependency)]
	internal class FancyFoods : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.FancyFoods";

		public const string PluginName = "FancyFoods";

		public const string PluginVersion = "0.0.4";

		public static bool isModded = true;

		public static GameObject Jam1;
		public static GameObject Jam2;
		public static GameObject Jam3;
		public static GameObject Jam4;

		public static GameObject Flour1;
		public static GameObject WhippedCream1;

		public static GameObject Base1;
		public static GameObject Base2;
		public static GameObject Base3;
		public static GameObject Base4;
		public static GameObject Base5;

		public static GameObject Food1;
		public static GameObject Food2;
		public static GameObject Food3;
		public static GameObject Food4;
		public static GameObject Food5;

		public static GameObject Cupcake1;
		public static GameObject Cupcake2;
		public static GameObject Cupcake3;
		public static GameObject Cupcake4;
		public static GameObject Cupcake5;

		public static GameObject Dessert1;
		public static GameObject Dessert2;
		public static GameObject Dessert3;
		public static GameObject Dessert4;
		public static GameObject Dessert5;

		public static GameObject Biscuit1;
		public static GameObject Biscuit2;
		public static GameObject Biscuit3;
		public static GameObject Biscuit4;
		public static GameObject Biscuit5;

		public static GameObject Cake1;

		public static GameObject Broth1;

		public static GameObject Soup1;
		public static GameObject Soup2;
		public static GameObject Soup3;
		public static GameObject Soup4;
		public static GameObject Soup5;
		public static GameObject Soup6;

		public static GameObject Stew1;
		public static GameObject Stew2;
		public static GameObject Stew3;
		public static GameObject Stew4;
		public static GameObject Stew5;
		public static GameObject Stew6;

		public AssetBundle FoodBundle;
		private Harmony _harmony;

		private void Awake() 
		{
			_harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FancyFoods");
			LoadBundle();
			LoadAssets();
			AddFlours();
			AddBroths();
			AddMaterials();
			AddVatrushka();
			AddCupcakes();
			AddDesserts();
			AddBiscuits();
			AddCakes();
			AddSoups();
			AddStews();
		}
		public void LoadBundle()
		{
			FoodBundle = AssetUtils.LoadAssetBundleFromResources("fancyfoods", Assembly.GetExecutingAssembly());
		}
		private void LoadAssets()
        {
            try
			{
				// Broths
				Broth1 = FoodBundle.LoadAsset<GameObject>("VegetableBroth_FF");
				// Jams
				Jam1 = FoodBundle.LoadAsset<GameObject>("AppleJam_FF");
				Jam2 = FoodBundle.LoadAsset<GameObject>("LimeJam_FF");
				Jam3 = FoodBundle.LoadAsset<GameObject>("OrangeJam_FF");
				Jam4 = FoodBundle.LoadAsset<GameObject>("PlumJam_FF");
				// Flours
				Flour1 = FoodBundle.LoadAsset<GameObject>("CornFlour_FF");
				// Cake Bases
				Base1 = FoodBundle.LoadAsset<GameObject>("CakeBase_FF");
				Base2 = FoodBundle.LoadAsset<GameObject>("CupCakeBase_FF");
				Base3 = FoodBundle.LoadAsset<GameObject>("DessertBase_FF");
				Base4 = FoodBundle.LoadAsset<GameObject>("VatrushkaBase_FF");
				Base5 = FoodBundle.LoadAsset<GameObject>("BiscuitBase_FF");
				// Dairy
				WhippedCream1 = FoodBundle.LoadAsset<GameObject>("WhippedCream_FF");
				// Foods
				// Stews
				Stew1 = FoodBundle.LoadAsset<GameObject>("CabbageBeefStew_FF");
				Stew2 = FoodBundle.LoadAsset<GameObject>("GameStew_FF");
				Stew3 = FoodBundle.LoadAsset<GameObject>("LambStew_FF");
				Stew4 = FoodBundle.LoadAsset<GameObject>("PorkPearStew_FF");
				Stew5 = FoodBundle.LoadAsset<GameObject>("PorkPepperStew_FF");
				Stew6 = FoodBundle.LoadAsset<GameObject>("BeefStew_FF");
				// Cakes
				Cake1 = FoodBundle.LoadAsset<GameObject>("ChocolateCherryCake_FF");
				// Soups
				Soup1 = FoodBundle.LoadAsset<GameObject>("BroccoliSoup_FF");
				Soup2 = FoodBundle.LoadAsset<GameObject>("PumkinSoup_FF");
				Soup3 = FoodBundle.LoadAsset<GameObject>("PotatoSoup_FF");
				Soup4 = FoodBundle.LoadAsset<GameObject>("SweetPotatoSoup_FF");
				Soup5 = FoodBundle.LoadAsset<GameObject>("MushroomSoup_FF");
				Soup6 = FoodBundle.LoadAsset<GameObject>("BellPepperSoup_FF");
				// Vatrushka
				Food1 = FoodBundle.LoadAsset<GameObject>("VatrushkaPlain_FF");
				Food2 = FoodBundle.LoadAsset<GameObject>("VatrushkaRaspberry_FF");
				Food3 = FoodBundle.LoadAsset<GameObject>("VatrushkaLime_FF");
				Food4 = FoodBundle.LoadAsset<GameObject>("VatrushkaOrange_FF");
				Food5 = FoodBundle.LoadAsset<GameObject>("VatrushkaPlum_FF");
				// Cupcakes
				Cupcake1 = FoodBundle.LoadAsset<GameObject>("CupcakePlain_FF");
				Cupcake2 = FoodBundle.LoadAsset<GameObject>("CupcakeCream_FF");
				Cupcake3 = FoodBundle.LoadAsset<GameObject>("CupcakeChocolate_FF");
				Cupcake4 = FoodBundle.LoadAsset<GameObject>("CupcakeOrange_FF");
				Cupcake5 = FoodBundle.LoadAsset<GameObject>("CupcakeOrangeCream_FF");
				// Dessert
				Dessert1 = FoodBundle.LoadAsset<GameObject>("DessertChocolate_FF");
				Dessert2 = FoodBundle.LoadAsset<GameObject>("DessertChocolatePear_FF");
				Dessert3 = FoodBundle.LoadAsset<GameObject>("DessertCreamPear_FF");
				Dessert4 = FoodBundle.LoadAsset<GameObject>("DessertRaspberry_FF");
				Dessert5 = FoodBundle.LoadAsset<GameObject>("DessertRaspberryCream_FF");
				// Biscuits
				Biscuit1 = FoodBundle.LoadAsset<GameObject>("Biscuit_FF");
				Biscuit2 = FoodBundle.LoadAsset<GameObject>("ChocolateBiscuit_FF");
				Biscuit3 = FoodBundle.LoadAsset<GameObject>("BiscuitChocolateCream_FF");
				Biscuit4 = FoodBundle.LoadAsset<GameObject>("BiscuitOrange_FF");
				Biscuit5 = FoodBundle.LoadAsset<GameObject>("BiscuitRaspberry_FF");
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Assets for Fancy Foods: {ex}");
			}
			finally
			{
				FoodBundle?.Unload(unloadAllLoadedObjects: false);
			}
		}
		private void AddFlours()
        {
            try
			{
				// Corn Flour
				GameObject item1 = Flour1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[1]
					{
						new RequirementConfig
						{
							Item = "Corn_SC",
							Amount = 8
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Flours for Fancy Foods: {ex}");
			}
		}
		private void AddBroths()
		{
			try
			{
				// Vegetable Broth
				GameObject item1 = Broth1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Thistle",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Turnip",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Carrot",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Broths for Fancy Foods: {ex}");
			}
		}
		private void AddMaterials()
		{
            try
			{
				// Apple and Raspberry Jam
				GameObject item1 = Jam1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Apple_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Raspberry",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Lemon_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Lime Jam
				GameObject item2 = Jam2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Lime_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Lemon_SC",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Orange and Mango Jam
				GameObject item3 = Jam3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Orange_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Mango_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Lemon_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Plum and Grape Jam
				GameObject item4 = Jam4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Plum_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Grapes_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Lemon_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Plain Cake
				GameObject item5 = Base1;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "rk_egg",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
				// Cup Cake Base
				GameObject item6 = Base2;
				CustomItem customItem6 = new CustomItem(item6, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem6);
				// Dessert Base
				GameObject item7 = Base3;
				CustomItem customItem7 = new CustomItem(item7, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem7);
				// Vatrushka Base
				GameObject item8 = Base4;
				CustomItem customItem8 = new CustomItem(item8, false, new ItemConfig
				{
					Amount = 3,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "rk_egg",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem8);
				// Biscuit Base
				GameObject item9 = Base5;
				CustomItem customItem9 = new CustomItem(item9, false, new ItemConfig
				{
					Amount = 8,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem9);
				// Whipped Cream
				GameObject item10 = WhippedCream1;
				CustomItem customItem10 = new CustomItem(item10, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem10);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding items for Fancy Foods: {ex}");
			}
		}
		private void AddVatrushka()
        {
            try
			{
				// Plain Vatrushka
				GameObject item1 = Food1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VatrushkaBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "EdamCheese_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Raspberry Vatrushka
				GameObject item2 = Food2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "VatrushkaPlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "AppleJam_FF",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Lime Vatrushka
				GameObject item3 = Food3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "VatrushkaPlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "LimeJam_FF",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Orange Vatrushka
				GameObject item4 = Food4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "VatrushkaPlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "LimeJam_FF",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Plum Vatrushka
				GameObject item5 = Food5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "VatrushkaPlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "PlumJam_FF",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Vatrushka for Fancy Foods: {ex}");
			}
		}
		private void AddCupcakes()
        {
            try
			{
				// Plain Cupcake
				GameObject item1 = Cupcake1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "CupCakeBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "CornFlour_FF",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "rk_egg",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Cream Cupcake
				GameObject item2 = Cupcake2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "CupcakePlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Chocolate Cupcake
				GameObject item3 = Cupcake3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "CupcakePlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "CocoPowder_SC",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Orange Cupcake
				GameObject item4 = Cupcake4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[2]
					{
						new RequirementConfig
						{
							Item = "CupcakePlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Orange_SC",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Orange Cream Cupcake
				GameObject item5 = Cupcake5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "CupcakePlain_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Orange_SC",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Cupcakes for Fancy Foods: {ex}");
			}
		}
		private void AddDesserts()
        {
            try
			{
				// Chocolate Dessert
				GameObject item1 = Dessert1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "DessertBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "CocoPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Chocolate Pear Dessert
				GameObject item2 = Dessert2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "DessertChocolate_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Pear_SC",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Pear Dessert
				GameObject item3 = Dessert3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "DessertBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Pear_SC",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Raspberry Dessert
				GameObject item4 = Dessert4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "DessertBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Raspberry",
							Amount = 8
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Raspberry Cream Dessert
				GameObject item5 = Dessert5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 6,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "DessertBase_FF",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 5
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Raspberry",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Desserts for Fancy Foods: {ex}");
			}
		}
		private void AddBiscuits()
        {
            try
			{
				// Biscuit
				GameObject item1 = Biscuit1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 4,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "BiscuitBase_FF",
							Amount = 8
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Chocolate Biscuit
				GameObject item2 = Biscuit2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 4,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "BiscuitBase_FF",
							Amount = 8
						},
						new RequirementConfig
						{
							Item = "CocoPowder_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Chocolate Cream Biscuit
				GameObject item3 = Biscuit3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 4,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "BiscuitBase_FF",
							Amount = 8
						},
						new RequirementConfig
						{
							Item = "CocoPowder_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Orange Biscuit
				GameObject item4 = Biscuit4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 4,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "BiscuitBase_FF",
							Amount = 8
						},
						new RequirementConfig
						{
							Item = "Orange_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Orange Biscuit
				GameObject item5 = Biscuit5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 4,
					CraftingStation = "rk_prep",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "BiscuitBase_FF",
							Amount = 8
						},
						new RequirementConfig
						{
							Item = "Raspberry",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "rk_butter",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SugarPowder_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Biscuits for Fancy Foods: {ex}");
			}
		}
		private void AddCakes()
        {
            try
			{
				// Chocolate Cherry Cake
				GameObject item1 = Cake1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "rk_prep",
					MinStationLevel = 2,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "CakeBase_FF",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "WhippedCream_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "CocoPowder_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Cherry_Food_FT",
							Amount = 25
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Cakes for Fancy Foods: {ex}");
			}
		}
		private void AddSoups()
        {
            try
			{
				// Broccoli Soup
				GameObject item1 = Soup1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Broccoli_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Pumpkin and Squash Soup
				GameObject item2 = Soup2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Squash_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Pumpkin_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Potato Soup
				GameObject item3 = Soup3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[3]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Sweet Potato Soup
				GameObject item4 = Soup4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "SweetPotato_SC",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "SpringOnion_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Mushroom Soup
				GameObject item5 = Soup5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Mushroom",
							Amount = 6
						},
						new RequirementConfig
						{
							Item = "BrownMushroom_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
				// Bell Pepper Soup
				GameObject item6 = Soup6;
				CustomItem customItem6 = new CustomItem(item6, false, new ItemConfig
				{
					Amount = 1,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "Milk_FYA",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "BellPepper_SC",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Onion",
							Amount = 1
						}
					}
				});
				ItemManager.Instance.AddItem(customItem6);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Soups for Fancy Foods: {ex}");
			}
		}
		private void AddStews()
		{
			try
			{
				// Cabbage and Beef Stew
				GameObject item1 = Stew1;
				CustomItem customItem1 = new CustomItem(item1, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "DicedMeat_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Cabbage_SC",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "Tomato_SC",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem1);
				// Game Stew
				GameObject item2 = Stew2;
				CustomItem customItem2 = new CustomItem(item2, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "DeerMeat",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "WolfMeat",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "Mushroom",
							Amount = 4
						}
					}
				});
				ItemManager.Instance.AddItem(customItem2);
				// Lamb Stew
				GameObject item3 = Stew3;
				CustomItem customItem3 = new CustomItem(item3, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "PrimeCut_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Carrot",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem3);
				// Pork and Pear Stew
				GameObject item4 = Stew4;
				CustomItem customItem4 = new CustomItem(item4, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_pork",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Mushroom",
							Amount = 4
						},
						new RequirementConfig
						{
							Item = "Pear_SC",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem4);
				// Pork and Pepper Stew
				GameObject item5 = Stew5;
				CustomItem customItem5 = new CustomItem(item5, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "rk_pork",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "BellPepper_SC",
							Amount = 3
						}
					}
				});
				ItemManager.Instance.AddItem(customItem5);
				// Beef Stew
				GameObject item6 = Stew6;
				CustomItem customItem6 = new CustomItem(item6, false, new ItemConfig
				{
					Amount = 2,
					CraftingStation = "piece_cauldron",
					MinStationLevel = 1,
					Requirements = new RequirementConfig[4]
					{
						new RequirementConfig
						{
							Item = "VegetableBroth_FF",
							Amount = 1
						},
						new RequirementConfig
						{
							Item = "DicedMeat_FYA",
							Amount = 3
						},
						new RequirementConfig
						{
							Item = "Potato_SC",
							Amount = 2
						},
						new RequirementConfig
						{
							Item = "SpringOnion_SC",
							Amount = 2
						}
					}
				});
				ItemManager.Instance.AddItem(customItem6);
			}
			catch (Exception ex)
			{
				Logger.LogWarning($"Exception caught while adding Stews for Fancy Foods: {ex}");
			}
		}
	}
}
