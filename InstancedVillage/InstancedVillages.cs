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
                    MinDistance = 150,
                    MaxDistance = 1000,
                    MinAltitude = 4f,
                    MaxAltitude = 400f,
                    MinDistanceFromSimilar = 100f,
                }));
                var AnyLoc2 = ZoneManager.Instance.CreateLocationContainer(IVAssets.LoadAsset<GameObject>("Loc_Instanced_VillageMed"));
                ZoneManager.Instance.AddCustomLocation(new CustomLocation(AnyLoc2, true, new LocationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Quantity = 5,
                    Priotized = true,
                    ExteriorRadius = 32f,
                    ClearArea = true,
                    MinDistance = 1000,
                    MaxDistance = 2000,
                    MinAltitude = 4f,
                    MaxAltitude = 400f,
                    MinDistanceFromSimilar = 100f,
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
                    MaxAltitude = 400f,
                    MinDistanceFromSimilar = 100f,
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
