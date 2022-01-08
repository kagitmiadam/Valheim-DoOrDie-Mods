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
            GameObject weapon1 = FAAssets.LoadAsset<GameObject>("Axe2H_01_FA");
            CustomItem axe1 = new CustomItem(weapon1, false);
            ItemManager.Instance.AddItem(axe1);
            GameObject weapon2 = FAAssets.LoadAsset<GameObject>("Axe2H_02_FA");
            CustomItem axe2 = new CustomItem(weapon2, false);
            ItemManager.Instance.AddItem(axe2);
            GameObject weapon3 = FAAssets.LoadAsset<GameObject>("Axe2H_03_FA");
            CustomItem axe3 = new CustomItem(weapon3, false);
            ItemManager.Instance.AddItem(axe3);
            GameObject weapon4 = FAAssets.LoadAsset<GameObject>("Axe2H_04_FA");
            CustomItem axe4 = new CustomItem(weapon4, false);
            ItemManager.Instance.AddItem(axe4);
            GameObject weapon5 = FAAssets.LoadAsset<GameObject>("Axe2H_05_FA");
            CustomItem axe5 = new CustomItem(weapon5, false);
            ItemManager.Instance.AddItem(axe5);
            GameObject weapon6 = FAAssets.LoadAsset<GameObject>("Axe2H_06_FA");
            CustomItem axe6 = new CustomItem(weapon6, false);
            ItemManager.Instance.AddItem(axe6);
            FAAssets.Unload(false);
        }
    }
}
