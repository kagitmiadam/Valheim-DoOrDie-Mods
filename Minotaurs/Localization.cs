using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace Minotaurs
{
	[HarmonyPatch]
	public class MinoLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_trophy_fireminotaurarmoured_hm", "Armoured Fire Minotaur"},
			{"item_trophy_fireminotaurarmoured_desc_hm", "Animated trophy of an Armoured Fire Minotaur"},
			{"item_trophy_fireminotaur_hm", "Fire Minotaur"},
			{"item_trophy_fireminotaur_desc_hm", "Animated trophy of a Fire Minotaur"},
			{"item_trophy_frostminotaurarmoured_hm", "Armoured Frost Minotaur"},
			{"item_trophy_frostminotaurarmoured_desc_hm", "Animated trophy of an Armoured Frost Minotaur"},
			{"item_trophy_frostminotaur_hm", "Frost Minotaur"},
			{"item_trophy_frostminotaur_desc_hm", "Animated trophy of a Frost Minotaur"},
			{"item_trophy_minotaurarmoured_hm", "Armoured Minotaur"},
			{"item_trophy_minotaurarmoured_desc_hm", "Animated trophy of an Armoured Minotaur"},
			{"item_trophy_minotaur_hm", "Minotaur"},
			{"item_trophy_minotaur_desc_hm", "Animated trophy of a Minotaur"},
			{"enemy_fireminotaurarmored_hm", "Armoured Fire Minotaur"},
			{"enemy_fireminotaur_hm", "Fire Minotaur"},
			{"enemy_frostminotaurarmored_hm", "Armoured Frost Minotaur"},
			{"enemy_frostminotaur_hm", "Frost Minotaur"},
			{"enemy_minotaurarmored_hm", "Armoured Minotaur"},
			{"enemy_minotaur_hm", "Minotaur"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_trophy_fireminotaurarmoured_hm", "Armoured Fire Minotaur"},
			{"item_trophy_fireminotaurarmoured_desc_hm", "Animated trophy of an Armoured Fire Minotaur"},
			{"item_trophy_fireminotaur_hm", "Fire Minotaur"},
			{"item_trophy_fireminotaur_desc_hm", "Animated trophy of a Fire Minotaur"},
			{"item_trophy_frostminotaurarmoured_hm", "Armoured Frost Minotaur"},
			{"item_trophy_frostminotaurarmoured_desc_hm", "Animated trophy of an Armoured Frost Minotaur"},
			{"item_trophy_frostminotaur_hm", "Frost Minotaur"},
			{"item_trophy_frostminotaur_desc_hm", "Animated trophy of a Frost Minotaur"},
			{"item_trophy_minotaurarmoured_hm", "Armoured Minotaur"},
			{"item_trophy_minotaurarmoured_desc_hm", "Animated trophy of an Armoured Minotaur"},
			{"item_trophy_minotaur_hm", "Minotaur"},
			{"item_trophy_minotaur_desc_hm", "Animated trophy of a Minotaur"},
			{"enemy_fireminotaurarmored_hm", "Armoured Fire Minotaur"},
			{"enemy_fireminotaur_hm", "Fire Minotaur"},
			{"enemy_frostminotaurarmored_hm", "Armoured Frost Minotaur"},
			{"enemy_frostminotaur_hm", "Frost Minotaur"},
			{"enemy_minotaurarmored_hm", "Armoured Minotaur"},
			{"enemy_minotaur_hm", "Minotaur"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_trophy_fireminotaurarmoured_hm", "Armoured Fire Minotaur"},
			{"item_trophy_fireminotaurarmoured_desc_hm", "Animated trophy of an Armoured Fire Minotaur"},
			{"item_trophy_fireminotaur_hm", "Fire Minotaur"},
			{"item_trophy_fireminotaur_desc_hm", "Animated trophy of a Fire Minotaur"},
			{"item_trophy_frostminotaurarmoured_hm", "Armoured Frost Minotaur"},
			{"item_trophy_frostminotaurarmoured_desc_hm", "Animated trophy of an Armoured Frost Minotaur"},
			{"item_trophy_frostminotaur_hm", "Frost Minotaur"},
			{"item_trophy_frostminotaur_desc_hm", "Animated trophy of a Frost Minotaur"},
			{"item_trophy_minotaurarmoured_hm", "Armoured Minotaur"},
			{"item_trophy_minotaurarmoured_desc_hm", "Animated trophy of an Armoured Minotaur"},
			{"item_trophy_minotaur_hm", "Minotaur"},
			{"item_trophy_minotaur_desc_hm", "Animated trophy of a Minotaur"},
			{"enemy_fireminotaurarmored_hm", "Armoured Fire Minotaur"},
			{"enemy_fireminotaur_hm", "Fire Minotaur"},
			{"enemy_frostminotaurarmored_hm", "Armoured Frost Minotaur"},
			{"enemy_frostminotaur_hm", "Frost Minotaur"},
			{"enemy_minotaurarmored_hm", "Armoured Minotaur"},
			{"enemy_minotaur_hm", "Minotaur"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_trophy_fireminotaurarmoured_hm", "Armoured Fire Minotaur"},
			{"item_trophy_fireminotaurarmoured_desc_hm", "Animated trophy of an Armoured Fire Minotaur"},
			{"item_trophy_fireminotaur_hm", "Fire Minotaur"},
			{"item_trophy_fireminotaur_desc_hm", "Animated trophy of a Fire Minotaur"},
			{"item_trophy_frostminotaurarmoured_hm", "Armoured Frost Minotaur"},
			{"item_trophy_frostminotaurarmoured_desc_hm", "Animated trophy of an Armoured Frost Minotaur"},
			{"item_trophy_frostminotaur_hm", "Frost Minotaur"},
			{"item_trophy_frostminotaur_desc_hm", "Animated trophy of a Frost Minotaur"},
			{"item_trophy_minotaurarmoured_hm", "Armoured Minotaur"},
			{"item_trophy_minotaurarmoured_desc_hm", "Animated trophy of an Armoured Minotaur"},
			{"item_trophy_minotaur_hm", "Minotaur"},
			{"item_trophy_minotaur_desc_hm", "Animated trophy of a Minotaur"},
			{"enemy_fireminotaurarmored_hm", "Armoured Fire Minotaur"},
			{"enemy_fireminotaur_hm", "Fire Minotaur"},
			{"enemy_frostminotaurarmored_hm", "Armoured Frost Minotaur"},
			{"enemy_frostminotaur_hm", "Frost Minotaur"},
			{"enemy_minotaurarmored_hm", "Armoured Minotaur"},
			{"enemy_minotaur_hm", "Minotaur"}

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
			else if (lang == "Turkish")
			{
				t = turkish;
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
		public static class MinoLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
