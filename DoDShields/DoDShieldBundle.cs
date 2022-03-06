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

namespace DoDShields
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class DoDShieldBundle : BaseUnityPlugin
	{
		public const string PluginGUID = "horemvore.DoDShields";
		public const string PluginName = "DoOrDieShields";
		public const string PluginVersion = "0.0.4";

		public static GameObject ShieldGSkull;
		public static GameObject ShieldBGSkull;
		public static GameObject ShieldEikthyr;
		public static GameObject ShieldRambore;
		public static GameObject ShieldElder;
		public static GameObject ShieldBitter;
		public static GameObject ShieldBonemass;
		public static GameObject ShieldModer;
		public static GameObject ShieldFarkas;
		public static GameObject ShieldSkir;
		public static GameObject ShieldYagluth;
		public static GameObject ShieldBEikthyr;
		public static GameObject ShieldBRambore;
		public static GameObject ShieldBElder;
		public static GameObject ShieldBBitter;
		public static GameObject ShieldBBonemass;
		public static GameObject ShieldBModer;
		public static GameObject ShieldBFarkas;
		public static GameObject ShieldBSkir;
		public static GameObject ShieldBYagluth;

		public AssetBundle DoDShields;
		public static AssetBundle GetAssetBundleFromResources(string fileName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = executingAssembly.GetManifestResourceNames().Single((string str) => str.EndsWith(fileName));
			using Stream stream = executingAssembly.GetManifestResourceStream(text);
			return AssetBundle.LoadFromStream(stream);
		}
		public void LoadBundle()
		{
			DoDShields = AssetUtils.LoadAssetBundleFromResources("dodshields", Assembly.GetExecutingAssembly());
		}
		private void Awake()
		{
			LoadBundle();
			LoadDoDAssets();
			CreateShields();
			DoDShields?.Unload(unloadAllLoadedObjects: false);
		}
		private void LoadDoDAssets()
        {
			//Debug.Log("DoDShields: 1");
			// Broken Shields
			ShieldBGSkull = DoDShields.LoadAsset<GameObject>("BrokenShieldBhygshan_DoD");
			ShieldBEikthyr = DoDShields.LoadAsset<GameObject>("BrokenShieldEikthyr_DoD");
			ShieldBRambore = DoDShields.LoadAsset<GameObject>("BrokenShieldRambore_DoD");
			ShieldBElder = DoDShields.LoadAsset<GameObject>("BrokenShieldElder_DoD");
			ShieldBBitter = DoDShields.LoadAsset<GameObject>("BrokenShieldBitterstump_DoD");
			ShieldBBonemass = DoDShields.LoadAsset<GameObject>("BrokenShieldBonemass_DoD");
			ShieldBModer = DoDShields.LoadAsset<GameObject>("BrokenShieldModer_DoD");
			ShieldBFarkas = DoDShields.LoadAsset<GameObject>("BrokenShieldFarkas_DoD");
			ShieldBSkir = DoDShields.LoadAsset<GameObject>("BrokenShieldSkir_DoD");
			ShieldBYagluth = DoDShields.LoadAsset<GameObject>("BrokenShieldYagluth_DoD");
			PrefabManager.Instance.AddPrefab(ShieldBGSkull);
			PrefabManager.Instance.AddPrefab(ShieldBEikthyr);
			PrefabManager.Instance.AddPrefab(ShieldBRambore);
			PrefabManager.Instance.AddPrefab(ShieldBElder);
			PrefabManager.Instance.AddPrefab(ShieldBBitter);
			PrefabManager.Instance.AddPrefab(ShieldBBonemass);
			PrefabManager.Instance.AddPrefab(ShieldBModer);
			PrefabManager.Instance.AddPrefab(ShieldBFarkas);
			PrefabManager.Instance.AddPrefab(ShieldBSkir);
			PrefabManager.Instance.AddPrefab(ShieldBYagluth);
			//Debug.Log("DoDShields: 2");
			// Shields
			ShieldGSkull = DoDShields.LoadAsset<GameObject>("ShieldSkullGreen_DoD");
			ShieldEikthyr = DoDShields.LoadAsset<GameObject>("ShieldEikthyr_DoD");
			ShieldRambore = DoDShields.LoadAsset<GameObject>("ShieldRambore_DoD");
			ShieldElder = DoDShields.LoadAsset<GameObject>("ShieldElder_DoD");
			ShieldBitter = DoDShields.LoadAsset<GameObject>("ShieldBitterstump_DoD");
			ShieldBonemass = DoDShields.LoadAsset<GameObject>("ShieldBonemass_DoD");
			ShieldModer = DoDShields.LoadAsset<GameObject>("ShieldModer_DoD");
			ShieldFarkas = DoDShields.LoadAsset<GameObject>("ShieldFarkas_DoD");
			ShieldSkir = DoDShields.LoadAsset<GameObject>("ShieldSkir_DoD");
			ShieldYagluth = DoDShields.LoadAsset<GameObject>("ShieldYagluth_DoD");
		}
		private void CreateShields()
		{
			GameObject shield10 = ShieldYagluth;
			CustomItem customItem10 = new CustomItem(shield10, fixReference: true, new ItemConfig
			{
				Name = "Deathgate",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldYagluth_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "YagluthDrop",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Tar",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "BlackMetal",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem10);
			GameObject shield9 = ShieldSkir;
			CustomItem customItem9 = new CustomItem(shield9, fixReference: true, new ItemConfig
			{
				Name = "Curator Ward",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldSkir_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "Obsidian",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "YagluthDrop",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "BlackMetal",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem9);
			GameObject shield8 = ShieldFarkas;
			CustomItem customItem8 = new CustomItem(shield8, fixReference: true, new ItemConfig
			{
				Name = "Frozen Light",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldFarkas_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "DragonTear",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "WolfFang",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Silver",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem8);
			GameObject shield7 = ShieldModer;
			CustomItem customItem7 = new CustomItem(shield7, fixReference: true, new ItemConfig
			{
				Name = "Perfect Storm",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldModer_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "WolfFang",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "DragonEgg",
					Amount = 1,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Silver",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem7);
			GameObject shield6 = ShieldBonemass;
			CustomItem customItem6 = new CustomItem(shield6, fixReference: true, new ItemConfig
			{
				Name = "Ghostly Wall",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBonemass_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "BoneFragments",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "Guck",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Iron",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem6);
			GameObject shield5 = ShieldBitter;
			CustomItem customItem5 = new CustomItem(shield5, fixReference: true, new ItemConfig
			{
				Name = "Darkheart",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBitterstump_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "GreydwarfEye",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Bronze",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem5);
			GameObject shield4 = ShieldElder;
			CustomItem customItem4 = new CustomItem(shield4, fixReference: true, new ItemConfig
			{
				Name = "Ironbark",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldElder_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "Bronze",
					Amount = 3,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "CryptKey",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Wood",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem4);
			GameObject shield3 = ShieldRambore;
			CustomItem customItem3 = new CustomItem(shield3, fixReference: true, new ItemConfig
			{
				Name = "Enforcer",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldRambore_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "LeatherScraps",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Bronze",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem3);
			GameObject shield2 = ShieldEikthyr;
			CustomItem customItem2 = new CustomItem(shield2, fixReference: true, new ItemConfig
			{
				Name = "Thundercloud",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldEikthyr_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "GreydwarfEye",
					Amount = 5,
					AmountPerLevel = 5
				},
				new RequirementConfig
				{
					Item = "HardAntler",
					Amount = 4,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Bronze",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem2);
			GameObject shield1 = ShieldGSkull;
			CustomItem customItem1 = new CustomItem(shield1, fixReference: true, new ItemConfig
			{
				Name = "Vortex, Conservator of the Dead",
				Amount = 1,
				CraftingStation = "forge",
				MinStationLevel = 3,
				Requirements = new RequirementConfig[4]
				{
				new RequirementConfig
				{
					Item = "BrokenShieldBhygshan_DoD",
					Amount = 1,
					AmountPerLevel = 0
				},
				new RequirementConfig
				{
					Item = "Guck",
					Amount = 5,
					AmountPerLevel = 2
				},
				new RequirementConfig
				{
					Item = "Wishbone",
					Amount = 2,
					AmountPerLevel = 1
				},
				new RequirementConfig
				{
					Item = "Iron",
					Amount = 5,
					AmountPerLevel = 5
				}
				}
			});
			ItemManager.Instance.AddItem(customItem1);
		}
	}
}
