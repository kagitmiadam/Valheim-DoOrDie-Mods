using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace ChickenCoopBA
{
	[HarmonyPatch]
	public class CCBALocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"piece_chickencoop_baa", "Chicken Coop"},
			{"piece_chickencoop_desc_baa", "Produces 5 Egg's every 12hrs (Real Time)"},
			{"item_chicken_baa", "A Chicken"},
			{"item_chicken_desc_baa", "Required to build the Chicken Coop"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"piece_chickencoop_baa", "Chicken Coop"},
			{"piece_chickencoop_desc_baa", "Produces 5 Egg's every 12hrs (Real Time)"},
			{"item_chicken_baa", "A Chicken"},
			{"item_chicken_desc_baa", "Required to build the Chicken Coop"}

		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"piece_chickencoop_baa", "Chicken Coop"},
			{"piece_chickencoop_desc_baa", "Produces 5 Egg's every 12hrs (Real Time)"},
			{"item_chicken_baa", "A Chicken"},
			{"item_chicken_desc_baa", "Required to build the Chicken Coop"}

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"piece_chickencoop_baa", "Chicken Coop"},
			{"piece_chickencoop_desc_baa", "Produces 5 Egg's every 12hrs (Real Time)"},
			{"item_chicken_baa", "A Chicken"},
			{"item_chicken_desc_baa", "Required to build the Chicken Coop"}

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
		public static class CCBALocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
