using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace SlimesRUs
{
	[HarmonyPatch]
	public class SlimeLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_trophy_slimeblue_hs", "Slime"},
			{"item_trophy_slimeblue_desc_hs", "Animated trophy of an Slime"},
			{"enemy_slimeblue_hs", "Slime"},
			{"enemy_slimered_hs", "Slime"},
			{"enemy_slimegreen_hs", "Slime"},
			{"enemy_slimepink_hs", "Slime"},
			{"enemy_slimepurple_hs", "Slime"},
			{"enemy_slimeyellow_hs", "Slime"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_trophy_slimeblue_hs", "Cлизь"},
			{"item_trophy_slimeblue_desc_hs", "Анимированный трофей Cлизи"},
			{"enemy_slimeblue_hs", "Cлизь"},
			{"enemy_slimered_hs", "Cлизь"},
			{"enemy_slimegreen_hs", "слизь"},
			{"enemy_slimepink_hs", "Cлизь"},
			{"enemy_slimepurple_hs", "Cлизь"},
			{"enemy_slimeyellow_hs", "Cлизь"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_trophy_slimeblue_hs", "Slime"},
			{"item_trophy_slimeblue_desc_hs", "Animierte Trophäe eines Schleims"},
			{"enemy_slimeblue_hs", "Schleim"},
			{"enemy_slimered_hs", "Schleim"},
			{"enemy_slimegreen_hs", "Schleim"},
			{"enemy_slimepink_hs", "Schleim"},
			{"enemy_slimepurple_hs", "Schleim"},
			{"enemy_slimeyellow_hs", "Schleim"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_trophy_slimeblue_hs", "Balçık"},
			{"item_trophy_slimeblue_desc_hs", "Bir Slime animasyonlu kupa"},
			{"enemy_slimeblue_hs", "Balçık"},
			{"enemy_slimered_hs", "Balçık"},
			{"enemy_slimegreen_hs", "Balçık"},
			{"enemy_slimepink_hs", "Balçık"},
			{"enemy_slimepurple_hs", "Balçık"},
			{"enemy_slimeyellow_hs", "Balçık"}

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
		public static class SlimeLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
