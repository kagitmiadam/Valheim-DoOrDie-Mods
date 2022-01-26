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

        public const string PluginVersion = "0.0.1";

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
            LoadBundle();
            LoadFAAssets();
        }
        public void LoadBundle()
        {
            FAAssets = AssetUtils.LoadAssetBundleFromResources("fabundle", Assembly.GetExecutingAssembly());
        }
        private void LoadFAAssets()
        {
            GameObject weapon0 = FAAssets.LoadAsset<GameObject>("Axe_1H_07_FA");
            CustomItem axe0 = new CustomItem(weapon0, true, new ItemConfig
            {
                Name = "Hope's Ravager",
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
                Name = "Dire Axe",
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
                Name = "Broken Promise",
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
                Name = "Scourgeborne",
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
                Name = "Narcoleptic",
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
                Name = "Spinefall",
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
                Name = "Soul Breaker",
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
                Name = "Blackout",
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
                Name = "Willbreaker",
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
                Name = "Reckoning",
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
                Name = "Faithkeeper",
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
                Name = "Purifier",
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
                Name = "Malignant Defender",
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
                Name = "Vindictive Spellblade",
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
                Name = "Reckoning",
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
                Name = "Betrayer",
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
                Name = "Extinction",
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
                Name = "Scar",
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
                Name = "Fate",
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
                Name = "Seism",
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
                Name = "Comet",
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
                Name = "Sleepwalker",
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
                Name = "Remorse",
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
                Name = "Celeste",
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
                Name = "Nirvana",
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
                Name = "Justifier",
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
                Name = "Ashes",
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
                Name = "Harvester",
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
                Name = "Fortune's Razor",
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
                Name = "Ritual Reaver",
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
                Name = "Sharpened Spellblade",
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
                Name = "Divine Light",
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
                Name = "Lusting Silver Slicer",
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
    }
}
