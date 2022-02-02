using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FantasyArmoury
{
	[HarmonyPatch]
	public class FALocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_sword2h_01_fa", "Placeholder"},
			{"item_sword2h_01_fa_description", "Placeholder"},
			{"item_sword2h_02_fa", "Placeholder"},
			{"item_sword2h_02_fa_description", "Placeholder"},
			{"item_sword2h_03_fa", "Placeholder"},
			{"item_sword2h_03_fa_description", "Placeholder"},
			{"item_sword2h_04_fa", "Placeholder"},
			{"item_sword2h_04_fa_description", "Placeholder"},
			{"item_sword2h_05_fa", "Placeholder"},
			{"item_sword2h_05_fa_description", "Placeholder"},
			{"item_sword2h_06_fa", "Placeholder"},
			{"item_sword2h_06_fa_description", "Placeholder"},
			{"item_sword1h_01_fa", "Placeholder"},
			{"item_sword1h_01_fa_description", "Placeholder"},
			{"item_sword1h_02_fa", "Placeholder"},
			{"item_sword1h_02_fa_description", "Placeholder"},
			{"item_sword1h_03_fa", "Placeholder"},
			{"item_sword1h_03_fa_description", "Placeholder"},
			{"item_sword1h_04_fa", "Placeholder"},
			{"item_sword1h_04_fa_description", "Placeholder"},
			{"item_sword1h_05_fa", "Placeholder"},
			{"item_sword1h_05_fa_description", "Placeholder"},
			{"item_staff2h_01_fa", "Placeholder"},
			{"item_staff2h_01_fa_description", "Placeholder"},
			{"item_staff2h_02_fa", "Placeholder"},
			{"item_staff2h_02_fa_description", "Placeholder"},
			{"item_staff2h_03_fa", "Placeholder"},
			{"item_staff2h_03_fa_description", "Placeholder"},
			{"item_staff2h_04_fa", "Placeholder"},
			{"item_staff2h_04_fa_description", "Placeholder"},
			{"item_staff2h_05_fa", "Placeholder"},
			{"item_staff2h_05_fa_description", "Placeholder"},
			{"item_scythe2h_01_fa", "Placeholder"},
			{"item_scythe2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_01_fa", "Placeholder"},
			{"item_hammer2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_02_fa", "Placeholder"},
			{"item_hammer2h_02_fa_description", "Placeholder"},
			{"item_hammer2h_03_fa", "Placeholder"},
			{"item_hammer2h_03_fa_description", "Placeholder"},
			{"item_axe1h_01_fa", "Placeholder"},
			{"item_axe1h_01_fa_description", "Placeholder"},
			{"item_axe1h_02_fa", "Placeholder"},
			{"item_axe1h_02_fa_description", "Placeholder"},
			{"item_axe1h_03_fa", "Placeholder"},
			{"item_axe1h_03_fa_description", "Placeholder"},
			{"item_axe1h_04_fa", "Placeholder"},
			{"item_axe1h_04_fa_description", "Placeholder"},
			{"item_axe1h_05_fa", "Placeholder"},
			{"item_axe1h_05_fa_description", "Placeholder"},
			{"item_axe1h_06_fa", "Placeholder"},
			{"item_axe1h_06_fa_description", "Placeholder"},
			{"item_axe1h_07_fa", "Placeholder"},
			{"item_axe1h_07_fa_description", "Placeholder"},
			{"item_axe2h_06_fa", "Placeholder"},
			{"item_axe2h_06_fa_description", "Placeholder"},
			{"item_axe2h_05_fa", "Placeholder"},
			{"item_axe2h_05_fa_description", "Placeholder"},
			{"item_axe2h_04_fa", "Placeholder"},
			{"item_axe2h_04_fa_description", "Placeholder"},
			{"item_axe2h_03_fa", "Placeholder"},
			{"item_axe2h_03_fa_description", "Placeholder"},
			{"item_axe2h_02_fa", "Placeholder"},
			{"item_axe2h_02_fa_description", "Placeholder"},
			{"item_axe2h_01_fa", "Placeholder"},
			{"item_axe2h_01_fa_description", "Placeholder"},
			{"item_shield_09_tower_fa", "Peacekeeper Warden"},
			{"item_shield_09_tower_fa_description", "Placeholder"},
			{"item_shield_08_tower_fa", "Venom Greatshield"},
			{"item_shield_08_tower_fa_description", "Placeholder"},
			{"item_shield_07_tower_fa", "Thirsty Skeletal Blockade"},
			{"item_shield_07_tower_fa_description", "Placeholder"},
			{"item_shield_06_tower_fa", "Legionnaire's Carapace"},
			{"item_shield_06_tower_fa_description", "Placeholder"},
			{"item_shield_05_tower_fa", "Desire's Willow Keeper"},
			{"item_shield_05_tower_fa_description", "Placeholder"},
			{"item_shield_04_tower_fa", "Lusting Warden"},
			{"item_shield_04_tower_fa_description", "Placeholder"},
			{"item_shield_03_tower_fa", "Blood Infused Greatshield"},
			{"item_shield_03_tower_fa_description", "Placeholder"},
			{"item_shield_02_tower_fa", "Defiled Shield Wall"},
			{"item_shield_02_tower_fa_description", "Placeholder"},
			{"item_shield_01_tower_fa", "Sorrow's Guardian"},
			{"item_shield_01_tower_fa_description", "Placeholder"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_shield_09_tower_fa", "Peacekeeper Warden"},
			{"item_shield_09_tower_fa_description", "Placeholder"},
			{"item_shield_08_tower_fa", "Venom Greatshield"},
			{"item_shield_08_tower_fa_description", "Placeholder"},
			{"item_shield_07_tower_fa", "Thirsty Skeletal Blockade"},
			{"item_shield_07_tower_fa_description", "Placeholder"},
			{"item_shield_06_tower_fa", "Legionnaire's Carapace"},
			{"item_shield_06_tower_fa_description", "Placeholder"},
			{"item_shield_05_tower_fa", "Desire's Willow Keeper"},
			{"item_shield_05_tower_fa_description", "Placeholder"},
			{"item_shield_04_tower_fa", "Lusting Warden"},
			{"item_shield_04_tower_fa_description", "Placeholder"},
			{"item_shield_03_tower_fa", "Blood Infused Greatshield"},
			{"item_shield_03_tower_fa_description", "Placeholder"},
			{"item_shield_02_tower_fa", "Defiled Shield Wall"},
			{"item_shield_02_tower_fa_description", "Placeholder"},
			{"item_shield_01_tower_fa", "Sorrow's Guardian"},
			{"item_shield_01_tower_fa_description", "Placeholder"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_shield_09_tower_fa", "Peacekeeper Warden"},
			{"item_shield_09_tower_fa_description", "Placeholder"},
			{"item_shield_08_tower_fa", "Venom Greatshield"},
			{"item_shield_08_tower_fa_description", "Placeholder"},
			{"item_shield_07_tower_fa", "Thirsty Skeletal Blockade"},
			{"item_shield_07_tower_fa_description", "Placeholder"},
			{"item_shield_06_tower_fa", "Legionnaire's Carapace"},
			{"item_shield_06_tower_fa_description", "Placeholder"},
			{"item_shield_05_tower_fa", "Desire's Willow Keeper"},
			{"item_shield_05_tower_fa_description", "Placeholder"},
			{"item_shield_04_tower_fa", "Lusting Warden"},
			{"item_shield_04_tower_fa_description", "Placeholder"},
			{"item_shield_03_tower_fa", "Blood Infused Greatshield"},
			{"item_shield_03_tower_fa_description", "Placeholder"},
			{"item_shield_02_tower_fa", "Defiled Shield Wall"},
			{"item_shield_02_tower_fa_description", "Placeholder"},
			{"item_shield_01_tower_fa", "Sorrow's Guardian"},
			{"item_shield_01_tower_fa_description", "Placeholder"}

			};

		public static void init(string lang, Localization l)
		{
			lcl = l;
			if (lang == "Russian")
			{
				t = russian;
			}
			else if (lang == "English")
			{
				t = english;
			}
			else
			{
				t = german;
			}
		}
		public static void AddWord(object[] element)
		{
			MethodInfo meth = AccessTools.Method(typeof(Localization), "AddWord", null, null);
			meth.Invoke(lcl, element);
		}
		public static void UpdateDictinary()
		{
			string missing = "Missing Words:";
			foreach (var el in english)
			{
				if (t.ContainsKey(el.Key))
				{
					AddWord(new object[] { el.Key, t[el.Key] });
					continue;
				}
				AddWord(new object[] { el.Key, el.Value });
				missing += el.Key;
			}
		}

		[HarmonyPatch(typeof(Localization), "SetupLanguage")]
		public static class FALocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
