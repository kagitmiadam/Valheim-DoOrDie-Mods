using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace DoDShields
{
	[HarmonyPatch]
	public class DoDSLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_shield_bgskull_dod", "Bhygshan's Broken Shield"},
			{"item_shield_bgskull_description_dod", "Wonder if you can fix it."},
			{"item_shield_byagluth_dod", "Yagluth's Broken Shield"},
			{"item_shield_byagluth_description_dod", "Wonder if you can fix it."},
			{"item_shield_bskir_dod", "Skir's Broken Shield"},
			{"item_shield_bskir_description_dod", "Wonder if you can fix it."},
			{"item_shield_bmoder_dod", "Moder's Broken Shield"},
			{"item_shield_bmoder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bfarkas_dod", "Farkas's Broken Shield"},
			{"item_shield_bfarkas_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbonemass_dod", "Bonemass's Broken Shield"},
			{"item_shield_bbonemass_description_dod", "Wonder if you can fix it."},
			{"item_shield_belder_dod", "Elder's Broken Shield"},
			{"item_shield_belder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbitterstump_dod", "Bitterstump's Broken Shield"},
			{"item_shield_bbitterstump_description_dod", "Wonder if you can fix it."},
			{"item_shield_beikthyr_dod", "Eikthyr's Broken Shield"},
			{"item_shield_beikthyr_description_dod", "Wonder if you can fix it."},
			{"item_shield_brambone_dod", "Ram'bone's Broken Shield"},
			{"item_shield_brambone_description_dod", "Wonder if you can fix it."},

			{"item_shield_yagluth_dod", "Deathgate"},
			{"item_shield_yagluth_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_skir_dod", "Curator Ward"},
			{"item_shield_skir_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_moder_dod", "Perfect Storm"},
			{"item_shield_moder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_farkas_dod", "Frozen Light"},
			{"item_shield_farkas_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bonemass_dod", "Ghostly Wall"},
			{"item_shield_bonemass_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bhygshan_dod", "Vortex, Conservator of the Dead"},
			{"item_shield_bhygshan_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_elder_dod", "Ironbark"},
			{"item_shield_elder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bitterstump_dod", "Darkheart"},
			{"item_shield_bitterstump_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_eikthyr_dod", "Thundercloud"},
			{"item_shield_eikthyr_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_rambone_dod", "Enforcer"},
			{"item_shield_rambone_description_dod", "Feel free to offer suggestions for this item."},

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_shield_bgskull_dod", "Bhygshan's Broken Shield"},
			{"item_shield_bgskull_description_dod", "Wonder if you can fix it."},
			{"item_shield_byagluth_dod", "Yagluth's Broken Shield"},
			{"item_shield_byagluth_description_dod", "Wonder if you can fix it."},
			{"item_shield_bskir_dod", "Skir's Broken Shield"},
			{"item_shield_bskir_description_dod", "Wonder if you can fix it."},
			{"item_shield_bmoder_dod", "Moder's Broken Shield"},
			{"item_shield_bmoder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bfarkas_dod", "Farkas's Broken Shield"},
			{"item_shield_bfarkas_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbonemass_dod", "Bonemass's Broken Shield"},
			{"item_shield_bbonemass_description_dod", "Wonder if you can fix it."},
			{"item_shield_belder_dod", "Elder's Broken Shield"},
			{"item_shield_belder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbitterstump_dod", "Bitterstump's Broken Shield"},
			{"item_shield_bbitterstump_description_dod", "Wonder if you can fix it."},
			{"item_shield_beikthyr_dod", "Eikthyr's Broken Shield"},
			{"item_shield_beikthyr_description_dod", "Wonder if you can fix it."},
			{"item_shield_brambone_dod", "Ram'bone's Broken Shield"},
			{"item_shield_brambone_description_dod", "Wonder if you can fix it."},

			{"item_shield_yagluth_dod", "Deathgate"},
			{"item_shield_yagluth_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_skir_dod", "Curator Ward"},
			{"item_shield_skir_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_moder_dod", "Perfect Storm"},
			{"item_shield_moder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_farkas_dod", "Frozen Light"},
			{"item_shield_farkas_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bonemass_dod", "Ghostly Wall"},
			{"item_shield_bonemass_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bhygshan_dod", "Vortex, Conservator of the Dead"},
			{"item_shield_bhygshan_description_dod", "You repaired Bhygshans Shield, congratulations! Feel free to offer suggestions for this item."},
			{"item_shield_elder_dod", "Ironbark"},
			{"item_shield_elder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bitterstump_dod", "Darkheart"},
			{"item_shield_bitterstump_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_eikthyr_dod", "Thundercloud"},
			{"item_shield_eikthyr_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_rambone_dod", "Enforcer"},
			{"item_shield_rambone_description_dod", "Feel free to offer suggestions for this item."},

		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_shield_bgskull_dod", "Bhygshan's Broken Shield"},
			{"item_shield_bgskull_description_dod", "Wonder if you can fix it."},
			{"item_shield_byagluth_dod", "Yagluth's Broken Shield"},
			{"item_shield_byagluth_description_dod", "Wonder if you can fix it."},
			{"item_shield_bskir_dod", "Skir's Broken Shield"},
			{"item_shield_bskir_description_dod", "Wonder if you can fix it."},
			{"item_shield_bmoder_dod", "Moder's Broken Shield"},
			{"item_shield_bmoder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bfarkas_dod", "Farkas's Broken Shield"},
			{"item_shield_bfarkas_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbonemass_dod", "Bonemass's Broken Shield"},
			{"item_shield_bbonemass_description_dod", "Wonder if you can fix it."},
			{"item_shield_belder_dod", "Elder's Broken Shield"},
			{"item_shield_belder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbitterstump_dod", "Bitterstump's Broken Shield"},
			{"item_shield_bbitterstump_description_dod", "Wonder if you can fix it."},
			{"item_shield_beikthyr_dod", "Eikthyr's Broken Shield"},
			{"item_shield_beikthyr_description_dod", "Wonder if you can fix it."},
			{"item_shield_brambone_dod", "Ram'bone's Broken Shield"},
			{"item_shield_brambone_description_dod", "Wonder if you can fix it."},

			{"item_shield_yagluth_dod", "Deathgate"},
			{"item_shield_yagluth_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_skir_dod", "Curator Ward"},
			{"item_shield_skir_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_moder_dod", "Perfect Storm"},
			{"item_shield_moder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_farkas_dod", "Frozen Light"},
			{"item_shield_farkas_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bonemass_dod", "Ghostly Wall"},
			{"item_shield_bonemass_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bhygshan_dod", "Vortex, Conservator of the Dead"},
			{"item_shield_bhygshan_description_dod", "You repaired Bhygshans Shield, congratulations! Feel free to offer suggestions for this item."},
			{"item_shield_elder_dod", "Ironbark"},
			{"item_shield_elder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bitterstump_dod", "Darkheart"},
			{"item_shield_bitterstump_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_eikthyr_dod", "Thundercloud"},
			{"item_shield_eikthyr_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_rambone_dod", "Enforcer"},
			{"item_shield_rambone_description_dod", "Feel free to offer suggestions for this item."},

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_shield_bgskull_dod", "Bhygshan's Broken Shield"},
			{"item_shield_bgskull_description_dod", "Wonder if you can fix it."},
			{"item_shield_byagluth_dod", "Yagluth's Broken Shield"},
			{"item_shield_byagluth_description_dod", "Wonder if you can fix it."},
			{"item_shield_bskir_dod", "Skir's Broken Shield"},
			{"item_shield_bskir_description_dod", "Wonder if you can fix it."},
			{"item_shield_bmoder_dod", "Moder's Broken Shield"},
			{"item_shield_bmoder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bfarkas_dod", "Farkas's Broken Shield"},
			{"item_shield_bfarkas_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbonemass_dod", "Bonemass's Broken Shield"},
			{"item_shield_bbonemass_description_dod", "Wonder if you can fix it."},
			{"item_shield_belder_dod", "Elder's Broken Shield"},
			{"item_shield_belder_description_dod", "Wonder if you can fix it."},
			{"item_shield_bbitterstump_dod", "Bitterstump's Broken Shield"},
			{"item_shield_bbitterstump_description_dod", "Wonder if you can fix it."},
			{"item_shield_beikthyr_dod", "Eikthyr's Broken Shield"},
			{"item_shield_beikthyr_description_dod", "Wonder if you can fix it."},
			{"item_shield_brambone_dod", "Ram'bone's Broken Shield"},
			{"item_shield_brambone_description_dod", "Wonder if you can fix it."},

			{"item_shield_yagluth_dod", "Deathgate"},
			{"item_shield_yagluth_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_skir_dod", "Curator Ward"},
			{"item_shield_skir_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_moder_dod", "Perfect Storm"},
			{"item_shield_moder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_farkas_dod", "Frozen Light"},
			{"item_shield_farkas_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bonemass_dod", "Ghostly Wall"},
			{"item_shield_bonemass_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bhygshan_dod", "Vortex, Conservator of the Dead"},
			{"item_shield_bhygshan_description_dod", "You repaired Bhygshans Shield, congratulations! Feel free to offer suggestions for this item."},
			{"item_shield_elder_dod", "Ironbark"},
			{"item_shield_elder_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_bitterstump_dod", "Darkheart"},
			{"item_shield_bitterstump_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_eikthyr_dod", "Thundercloud"},
			{"item_shield_eikthyr_description_dod", "Feel free to offer suggestions for this item."},
			{"item_shield_rambone_dod", "Enforcer"},
			{"item_shield_rambone_description_dod", "Feel free to offer suggestions for this item."},

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
		public static class DoDSLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
