using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FruitTrees
{
	[HarmonyPatch]
	public class FTLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"prop_walnuttree_dod", "Walnut Tree"},
			{"prop_appletree_dod", "Apple Tree"},
			{"prop_redcherries_dod", "Cherry Tree"},
			{"prop_bananatree_dod", "Banana Tree"},
			{"prop_grapevine_dod", "Grape Vine"},
			{"item_grapes_ft", "Green Grapes"},
			{"item_grapes_description_ft", "Gatherd from Grape Vine's in the Meadows and Black Forest"},
			{"item_banana_dod", "Banana"},
			{"item_banana_description_dod", "Gatherd from Banana Tree's in the Plains and Swamp"},
			{"item_walnut_dod", "Walnuts"},
			{"item_walnuts_description_dod", "Gatherd from Walnut Tree's in the Mistlands."},
			{"item_apple_dod", "Pink Lady"},
			{"item_apple_description_dod", "Gatherd from Apple Tree's in the Meadows."},
			{"item_redberries_dod", "Cherries"},
			{"item_redberries_description_dod", "Gatherd from Cherry Tree's in the Black Forest and Swamp."}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"prop_walnuttree_dod", "Walnut Tree"},
			{"prop_appletree_dod", "Apple Tree"},
			{"prop_redcherries_dod", "Cherry Tree"},
			{"prop_bananatree_dod", "Banana Tree"},
			{"prop_grapevine_dod", "Grape Vine"},
			{"item_grapes_ft", "Grapes"},
			{"item_grapes_description_ft", "Gatherd from Grape Vine's in the Meadows and Black Forest"},
			{"item_banana_dod", "Banana"},
			{"item_banana_description_dod", "Gatherd from Banana Tree's in the Plains and Swamp"},
			{"item_walnut_dod", "Walnuts"},
			{"item_walnuts_description_dod", "Gatherd from Walnut Tree's in the Mistlands."},
			{"item_apple_dod", "Pink Lady"},
			{"item_apple_description_dod", "Gatherd from Apple Tree's in the Black Forest."},
			{"item_redberries_dod", "Cherries"},
			{"item_redberries_description_dod", "Gatherd from Cherry Tree's in the Black Forest and Swamp."}

		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"prop_walnuttree_dod", "Walnut Tree"},
			{"prop_appletree_dod", "Apple Tree"},
			{"prop_redcherries_dod", "Cherry Tree"},
			{"prop_bananatree_dod", "Banana Tree"},
			{"prop_grapevine_dod", "Grape Vine"},
			{"item_grapes_ft", "Grapes"},
			{"item_grapes_description_ft", "Gatherd from Grape Vine's in the Meadows and Black Forest"},
			{"item_banana_dod", "Banana"},
			{"item_banana_description_dod", "Gatherd from Banana Tree's in the Plains and Swamp"},
			{"item_walnut_dod", "Walnuts"},
			{"item_walnuts_description_dod", "Gatherd from Walnut Tree's in the Mistlands."},
			{"item_apple_dod", "Pink Lady"},
			{"item_apple_description_dod", "Gatherd from Apple Tree's in the Black Forest."},
			{"item_redberries_dod", "Cherries"},
			{"item_redberries_description_dod", "Gatherd from Cherry Tree's in the Black Forest and Swamp."}

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"prop_walnuttree_dod", "Walnut Tree"},
			{"prop_appletree_dod", "Apple Tree"},
			{"prop_redcherries_dod", "Cherry Tree"},
			{"prop_bananatree_dod", "Banana Tree"},
			{"prop_grapevine_dod", "Grape Vine"},
			{"item_grapes_ft", "Grapes"},
			{"item_grapes_description_ft", "Gatherd from Grape Vine's in the Meadows and Black Forest"},
			{"item_banana_dod", "Banana"},
			{"item_banana_description_dod", "Gatherd from Banana Tree's in the Plains and Swamp"},
			{"item_walnut_dod", "Walnuts"},
			{"item_walnuts_description_dod", "Gatherd from Walnut Tree's in the Mistlands."},
			{"item_apple_dod", "Pink Lady"},
			{"item_apple_description_dod", "Gatherd from Apple Tree's in the Black Forest."},
			{"item_redberries_dod", "Cherries"},
			{"item_redberries_description_dod", "Gatherd from Cherry Tree's in the Black Forest and Swamp."}

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
		public static class FTLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
