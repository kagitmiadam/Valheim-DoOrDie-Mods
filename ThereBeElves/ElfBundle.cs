using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace ThereBeElves
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    internal class ElfBundle : BaseUnityPlugin
    {
        public const string PluginGUID = "horemvore.ThereBeElves";

        public const string PluginName = "ThereBeElves";

        public const string PluginVersion = "0.0.1";
    
    }
}
