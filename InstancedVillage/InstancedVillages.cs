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

        public const string PluginVersion = "0.0.1";

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
            LoadBundle();
            LoadDoDAssets();
            ZoneManager.OnVanillaLocationsAvailable += AddLocations;
        }
        public void LoadBundle()
        {
            IVAssets = AssetUtils.LoadAssetBundleFromResources("concept", Assembly.GetExecutingAssembly());
        }
        private void LoadDoDAssets()
        {
            GameObject loc1 = IVAssets.LoadAsset<GameObject>("Loc_Instanced_Village");
            CustomPrefab village1 = new CustomPrefab(loc1, false);
            PrefabManager.Instance.AddPrefab(village1);
            IVAssets.Unload(false);
        }
        private void AddLocations()
        {
            IVAssets = AssetUtils.LoadAssetBundleFromResources("concept", Assembly.GetExecutingAssembly());
            try
            {
                var AnyLoc4 = ZoneManager.Instance.CreateLocationContainer(IVAssets.LoadAsset<GameObject>("Loc_Instanced_Village"), false);
                ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc4, new LocationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Quantity = 1,
                    Priotized = true,
                    ExteriorRadius = 33f,
                    ClearArea = true,
                    MinDistance = 100,
                    MaxDistance = 150,
                    MinAltitude = 4f,
                    //MaxAltitude = 660f,
                }));
            }
            finally
            {
                ZoneManager.OnVanillaLocationsAvailable -= AddLocations;
                IVAssets.Unload(false);
            }
        }
    }
}
