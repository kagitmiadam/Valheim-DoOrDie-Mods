using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace CatsAndDogs
{
	[HarmonyPatch]
	public class WLLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"animal_pug_cd", "Pug"},
			{"animal_hound_cd", "Hound"},
			{"animal_celtic_cd", "Wolfhound"},
			{"animal_corso_cd", "Cane Corso"},
			{"animal_beagle_cd", "Beagle"},
			{"animal_cat_cd", "Cat"},
			{"animal_cat_kit_cd", "Kitten"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"animal_cat_cd", "Cat"},
			{"animal_cat_kit_cd", "Kitten"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"animal_cat_cd", "Cat"},
			{"animal_cat_kit_cd", "Kitten"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"animal_pug_cd", "Pug"},
			{"animal_hound_cd", "Tazı"},
			{"animal_celtic_cd", "Kurt Tazısı"},
			{"animal_corso_cd", "Cane Corso"},
			{"animal_beagle_cd", "Av Köpeği"},
			{"animal_cat_cd", "Kedi"},
			{"animal_cat_kit_cd", "Yavru Kedi"}

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
		public static class WLLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
