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

namespace FantasyArmoury
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    internal class fantasyArmoury : BaseUnityPlugin
    {
        public const string PluginGUID = "horemvore.FantasyArmoury";

        public const string PluginName = "FantasyArmoury";

        public const string PluginVersion = "0.0.5";

        private Harmony _harmony;
        public AssetBundle FAAssets;
        public static AssetBundle GetAssetBundleFromResources(string fileName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
            using Stream stream = executingAssembly.GetManifestResourceStream(text);
            return AssetBundle.LoadFromStream(stream);
        }
        private void Awake()
        {
            Debug.Log("FantasyArmoury: Loading and Creating Assets");
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "horemvore.FantasyArmoury");
            LoadBundle();
            LoadFAAssets();
        }
        public void LoadBundle()
        {
            FAAssets = AssetUtils.LoadAssetBundleFromResources("fabundle", Assembly.GetExecutingAssembly());
        }
        private void LoadFAAssets()
        {
            GameObject shield8 = FAAssets.LoadAsset<GameObject>("Shield_09_FA");
            CustomItem tower8 = new CustomItem(shield8, true, new ItemConfig
            {
                //Name = "$item_shield_09_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower8);
            GameObject shield7 = FAAssets.LoadAsset<GameObject>("Shield_08_FA");
            CustomItem tower7 = new CustomItem(shield7, true, new ItemConfig
            {
                //Name = "$item_shield_08_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower7);
            GameObject shield6 = FAAssets.LoadAsset<GameObject>("Shield_07_FA");
            CustomItem tower6 = new CustomItem(shield6, true, new ItemConfig
            {
                //Name = "$item_shield_07_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower6);
            GameObject shield5 = FAAssets.LoadAsset<GameObject>("Shield_06_FA");
            CustomItem tower5 = new CustomItem(shield5, true, new ItemConfig
            {
                //Name = "$item_shield_06_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower5);
            GameObject shield4 = FAAssets.LoadAsset<GameObject>("Shield_05_FA");
            CustomItem tower4 = new CustomItem(shield4, true, new ItemConfig
            {
                //Name = "$item_shield_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower4);
            GameObject shield3 = FAAssets.LoadAsset<GameObject>("Shield_04_FA");
            CustomItem tower3 = new CustomItem(shield3, true, new ItemConfig
            {
                //Name = "$item_shield_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower3);
            GameObject shield2 = FAAssets.LoadAsset<GameObject>("Shield_03_FA");
            CustomItem tower2 = new CustomItem(shield2, true, new ItemConfig
            {
                //Name = "$item_shield_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower2);
            GameObject shield1 = FAAssets.LoadAsset<GameObject>("Shield_02_FA");
            CustomItem tower1 = new CustomItem(shield1, true, new ItemConfig
            {
                //Name = "$item_shield_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower1);
            GameObject shield0 = FAAssets.LoadAsset<GameObject>("Shield_01_FA");
            CustomItem tower0 = new CustomItem(shield0, true, new ItemConfig
            {
                //Name = "$item_shield_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(tower0);
            GameObject weapon0 = FAAssets.LoadAsset<GameObject>("Axe_1H_07_FA");
            CustomItem axe0 = new CustomItem(weapon0, true, new ItemConfig
            {
                //Name = "$item_axe_1H_07_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe0);
            Debug.Log("FantasyArmoury: 2H Axe"); 
            GameObject weapon1 = FAAssets.LoadAsset<GameObject>("Axe2H_01_FA");
            CustomItem axe1 = new CustomItem(weapon1, true, new ItemConfig
            {
                //Name = "$item_axe2H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe1);
            GameObject weapon2 = FAAssets.LoadAsset<GameObject>("Axe2H_02_FA");
            CustomItem axe2 = new CustomItem(weapon2, true, new ItemConfig
            {
                //Name = "$item_axe2H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe2);
            GameObject weapon3 = FAAssets.LoadAsset<GameObject>("Axe2H_03_FA");
            CustomItem axe3 = new CustomItem(weapon3, true, new ItemConfig
            {
                //Name = "$item_axe2H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe3);
            GameObject weapon4 = FAAssets.LoadAsset<GameObject>("Axe2H_04_FA");
            CustomItem axe4 = new CustomItem(weapon4, true, new ItemConfig
            {
                //Name = "$item_axe2H_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe4);
            GameObject weapon5 = FAAssets.LoadAsset<GameObject>("Axe2H_05_FA");
            CustomItem axe5 = new CustomItem(weapon5, true, new ItemConfig
            {
                //Name = "$item_axe2H_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe5);
            GameObject weapon6 = FAAssets.LoadAsset<GameObject>("Axe2H_06_FA");
            CustomItem axe6 = new CustomItem(weapon6, true, new ItemConfig
            {
                //Name = "$item_axe2H_06_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe6);
            Debug.Log("FantasyArmoury: 2H Hammer");
            GameObject weapon7 = FAAssets.LoadAsset<GameObject>("Hammer_2H_01_FA");
            CustomItem hammer1 = new CustomItem(weapon7, true, new ItemConfig
            {
                //Name = "$item_hammer_2H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(hammer1);
            GameObject weapon8 = FAAssets.LoadAsset<GameObject>("Hammer_2H_02_FA");
            CustomItem hammer2 = new CustomItem(weapon8, true, new ItemConfig
            {
                //Name = "$item_hammer_2H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(hammer2);
            GameObject weapon9 = FAAssets.LoadAsset<GameObject>("Hammer_2H_03_FA");
            CustomItem hammer3 = new CustomItem(weapon9, true, new ItemConfig
            {
                //Name = "$item_hammer_2H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(hammer3);
            Debug.Log("FantasyArmoury: 2H Sword");
            GameObject weapon10 = FAAssets.LoadAsset<GameObject>("Sword_2H_01_FA");
            CustomItem sword1 = new CustomItem(weapon10, true, new ItemConfig
            {
                //Name = "$item_sword_2H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword1);
            GameObject weapon11 = FAAssets.LoadAsset<GameObject>("Sword_2H_02_FA");
            CustomItem sword2 = new CustomItem(weapon11, true, new ItemConfig
            {
                //Name = "$item_sword_2H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword2);
            GameObject weapon12 = FAAssets.LoadAsset<GameObject>("Sword_2H_03_FA");
            CustomItem sword3 = new CustomItem(weapon12, true, new ItemConfig
            {
                //Name = "$item_sword_2H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword3);
            GameObject weapon13 = FAAssets.LoadAsset<GameObject>("Sword_2H_04_FA");
            CustomItem sword4 = new CustomItem(weapon13, true, new ItemConfig
            {
                //Name = "$item_sword_2H_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword4);
            GameObject weapon14 = FAAssets.LoadAsset<GameObject>("Sword_2H_05_FA");
            CustomItem sword5 = new CustomItem(weapon14, true, new ItemConfig
            {
                //Name = "$item_sword_2H_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword5);
            GameObject weapon15 = FAAssets.LoadAsset<GameObject>("Sword_2H_06_FA");
            CustomItem sword6 = new CustomItem(weapon15, true, new ItemConfig
            {
                //Name = "$item_sword_2H_06_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword6);
            Debug.Log("FantasyArmoury: 2H Scythe");
            GameObject weapon16 = FAAssets.LoadAsset<GameObject>("Scythe2H_01_FA");
            CustomItem scythe1 = new CustomItem(weapon16, true, new ItemConfig
            {
                //Name = "$item_scythe2H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(scythe1);
            Debug.Log("FantasyArmoury: 2H Staff");
            GameObject weapon17 = FAAssets.LoadAsset<GameObject>("Staff_2H_01_FA");
            CustomItem staff1 = new CustomItem(weapon17, true, new ItemConfig
            {
                //Name = "$item_staff_2H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(staff1);
            GameObject weapon18 = FAAssets.LoadAsset<GameObject>("Staff_2H_02_FA");
            CustomItem staff2 = new CustomItem(weapon18, true, new ItemConfig
            {
                //Name = "$item_staff_2H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(staff2);
            GameObject weapon19 = FAAssets.LoadAsset<GameObject>("Staff_2H_03_FA");
            CustomItem staff3 = new CustomItem(weapon19, true, new ItemConfig
            {
                //Name = "$item_staff_2H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(staff3);
            GameObject weapon20 = FAAssets.LoadAsset<GameObject>("Staff_2H_04_FA");
            CustomItem staff4 = new CustomItem(weapon20, true, new ItemConfig
            {
                //Name = "$item_staff_2H_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(staff4);
            GameObject weapon21 = FAAssets.LoadAsset<GameObject>("Staff_2H_05_FA");
            CustomItem staff5 = new CustomItem(weapon21, true, new ItemConfig
            {
                //Name = "$item_staff_2H_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(staff5);
            Debug.Log("FantasyArmoury: 1H Axe");
            GameObject weapon22 = FAAssets.LoadAsset<GameObject>("Axe_1H_01_FA");
            CustomItem axe7 = new CustomItem(weapon22, true, new ItemConfig
            {
                //Name = "$item_axe_1H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe7);
            GameObject weapon23 = FAAssets.LoadAsset<GameObject>("Axe_1H_02_FA");
            CustomItem axe8 = new CustomItem(weapon23, true, new ItemConfig
            {
                //Name = "$item_axe_1H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe8);
            GameObject weapon24 = FAAssets.LoadAsset<GameObject>("Axe_1H_03_FA");
            CustomItem axe9 = new CustomItem(weapon24, true, new ItemConfig
            {
                //Name = "$item_axe_1H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe9);
            GameObject weapon25 = FAAssets.LoadAsset<GameObject>("Axe_1H_04_FA");
            CustomItem axe10 = new CustomItem(weapon25, true, new ItemConfig
            {
                //Name = "$item_axe_1H_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe10);
            GameObject weapon26 = FAAssets.LoadAsset<GameObject>("Axe_1H_05_FA");
            CustomItem axe11 = new CustomItem(weapon26, true, new ItemConfig
            {
                //Name = "$item_axe_1H_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe11);
            GameObject weapon27 = FAAssets.LoadAsset<GameObject>("Axe_1H_06_FA");
            CustomItem axe12 = new CustomItem(weapon27, true, new ItemConfig
            {
                //Name = "$item_axe_1H_06_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(axe12);
            Debug.Log("FantasyArmoury: 1H Sword");
            GameObject weapon28 = FAAssets.LoadAsset<GameObject>("Sword_1H_01_FA");
            CustomItem sword7 = new CustomItem(weapon28, true, new ItemConfig
            {
                //Name = "$item_sword_1H_01_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword7);
            GameObject weapon29 = FAAssets.LoadAsset<GameObject>("Sword_1H_02_FA");
            CustomItem sword8 = new CustomItem(weapon29, true, new ItemConfig
            {
                //Name = "$item_sword_1H_02_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword8);
            GameObject weapon30 = FAAssets.LoadAsset<GameObject>("Sword_1H_03_FA");
            CustomItem sword9 = new CustomItem(weapon30, true, new ItemConfig
            {
                //Name = "$item_sword_1H_03_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword9);
            GameObject weapon31 = FAAssets.LoadAsset<GameObject>("Sword_1H_04_FA");
            CustomItem sword10 = new CustomItem(weapon31, true, new ItemConfig
            {
                //Name = "$item_sword_1H_04_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword10);
            GameObject weapon32 = FAAssets.LoadAsset<GameObject>("Sword_1H_05_FA");
            CustomItem sword11 = new CustomItem(weapon32, true, new ItemConfig
            {
                //Name = "$item_sword_1H_05_fa",
                Amount = 1,
                CraftingStation = "forge",
                MinStationLevel = 1,
                Requirements = new RequirementConfig[4]
                {
                new RequirementConfig
                {
                    Item = "Bronze",
                    Amount = 10,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 2,
                    AmountPerLevel = 1
                },
                new RequirementConfig
                {
                    Item = "Iron",
                    Amount = 0,
                    AmountPerLevel = 3
                },
                new RequirementConfig
                {
                    Item = "DeerHide",
                    Amount = 0,
                    AmountPerLevel = 3
                }
                }
            });
            ItemManager.Instance.AddItem(sword11);
            FAAssets.Unload(false);
        }
        private void AddRecipesFromJSON()
        {
            // Load recipes from JSON file
            ItemManager.Instance.AddRecipesFromJson("Horem-FantasyArmoury/Assets/fa_recipes.json");
        }
    }
}
