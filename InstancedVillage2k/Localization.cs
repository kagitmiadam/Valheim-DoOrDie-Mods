using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace InstancedVillages
{
	[HarmonyPatch]
	public class IVLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Stone House" },
								{ "piece_greyhouse_tp", "Grey Wood House" },
								{ "piece_oakhouse_tp", "Oak Wood House" },
								{ "piece_thatchhouse_tp", "Dark Wood House" },
								{ "piece_wornhouse_tp", "Worn Tudor House" },
								{ "piece_pinehouse_tp", "Pine Wood House" }

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Stone House" },
								{ "piece_greyhouse_tp", "Grey Wood House" },
								{ "piece_oakhouse_tp", "Oak Wood House" },
								{ "piece_thatchhouse_tp", "Dark Wood House" },
								{ "piece_wornhouse_tp", "Worn Tudor House" },
								{ "piece_pinehouse_tp", "Pine Wood House" }

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Taş Ev" },
								{ "piece_greyhouse_tp", "Gri Ahşap Ev" },
								{ "piece_oakhouse_tp", "Meşe Ahşap Ev" },
								{ "piece_thatchhouse_tp", "Kara Ahşap Ev" },
								{ "piece_wornhouse_tp", "Yıpranmış Tudor Evi" },
								{ "piece_pinehouse_tp", "Çam Ahşap Ev" }

		};

		public static void init(string lang, Localization l)
		{
			lcl = l;
			if (lang == "Russian")
			{
				t = russian;
			}
			if (lang == "Turkish")
			{
				t = turkish;
			}
			else
			{
				t = english;
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
		public static class IVLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}

