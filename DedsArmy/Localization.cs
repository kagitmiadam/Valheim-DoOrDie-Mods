using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace DedsArmy
{
	[HarmonyPatch]
	public class DALocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_trophy_undeadboss_da", "Zarathos"},
			{"item_trophy_undeadboss_desc_da", "Animated trophy of Zarathos"},
			{"item_trophy_vampire_da", "Vampire"},
			{"item_trophy_vampire_desc_da", "Animated trophy of a Vampire"},
			{"item_trophy_skeleton1h_da", "Skeleton"},
			{"item_trophy_skeleton1h_desc_da", "Animated trophy of a Skeleton with a Sword and Shield"},
			{"item_trophy_skeleton2h_da", "Skeleton"},
			{"item_trophy_skeleton2h_desc_da", "Animated trophy of a Skeleton with a Two-Handed Sword"},
			{"item_trophy_undead_da", "Undead"},
			{"item_trophy_undead_desc_da", "Animated trophy of an Undead"},
			{"item_trophy_undeadcarver_da", "Carver"},
			{"item_trophy_undeadcarver_desc_da", "Animated trophy of an Undead Carver"},
			{"item_trophy_undeaddesecrator_da", "Desecrator"},
			{"item_trophy_undeaddesecrator_desc_da", "Animated trophy of an Undead Desecrator"},
			{"item_trophy_undeadreaver_da", "Reaver"},
			{"item_trophy_undeadreaver_desc_da", "Animated trophy of an Undead Reaver"},
			{"item_trophy_undeadripper_da", "Ripper"},
			{"item_trophy_undeadripper_desc_da", "Animated trophy of an Undead Ripper"},
			{"enemy_undeadboss_da", "Zarathos"},
			{"enemy_vampire_da", "Vampire"},
			{"enemy_skeleton_da", "Cadaver"},
			{"enemy_undead_da", "Undead"},
			{"enemy_undeadcarver_da", "Carver"},
			{"enemy_undeaddesecrator_da", "Desecrator"},
			{"enemy_undeadreaver_da", "Reaver"},
			{"enemy_undeadripper_da", "Ripper"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_trophy_undeadboss_da", "Zarathos"},
			{"item_trophy_undeadboss_desc_da", "Animated trophy of Zarathos"},
			{"item_trophy_vampire_da", "Vampire"},
			{"item_trophy_vampire_desc_da", "Animated trophy of a Vampire"},
			{"item_trophy_skeleton1h_da", "Skeleton"},
			{"item_trophy_skeleton1h_desc_da", "Animated trophy of a Skeleton with a Sword and Shield"},
			{"item_trophy_skeleton2h_da", "Skeleton"},
			{"item_trophy_skeleton2h_desc_da", "Animated trophy of a Skeleton with a Two-Handed Sword"},
			{"item_trophy_undead_da", "Undead"},
			{"item_trophy_undead_desc_da", "Animated trophy of an Undead"},
			{"item_trophy_undeadcarver_da", "Carver"},
			{"item_trophy_undeadcarver_desc_da", "Animated trophy of an Undead Carver"},
			{"item_trophy_undeaddesecrator_da", "Desecrator"},
			{"item_trophy_undeaddesecrator_desc_da", "Animated trophy of an Undead Desecrator"},
			{"item_trophy_undeadreaver_da", "Reaver"},
			{"item_trophy_undeadreaver_desc_da", "Animated trophy of an Undead Reaver"},
			{"item_trophy_undeadripper_da", "Ripper"},
			{"item_trophy_undeadripper_desc_da", "Animated trophy of an Undead Ripper"},
			{"enemy_undeadboss_da", "Zarathos"},
			{"enemy_vampire_da", "Vampire"},
			{"enemy_skeleton_da", "Cadaver"},
			{"enemy_undead_da", "Undead"},
			{"enemy_undeadcarver_da", "Carver"},
			{"enemy_undeaddesecrator_da", "Desecrator"},
			{"enemy_undeadreaver_da", "Reaver"},
			{"enemy_undeadripper_da", "Ripper"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_trophy_undeadboss_da", "Zarathos"},
			{"item_trophy_undeadboss_desc_da", "Animated trophy of Zarathos"},
			{"item_trophy_vampire_da", "Vampire"},
			{"item_trophy_vampire_desc_da", "Animated trophy of a Vampire"},
			{"item_trophy_skeleton1h_da", "Skeleton"},
			{"item_trophy_skeleton1h_desc_da", "Animated trophy of a Skeleton with a Sword and Shield"},
			{"item_trophy_skeleton2h_da", "Skeleton"},
			{"item_trophy_skeleton2h_desc_da", "Animated trophy of a Skeleton with a Two-Handed Sword"},
			{"item_trophy_undead_da", "Undead"},
			{"item_trophy_undead_desc_da", "Animated trophy of an Undead"},
			{"item_trophy_undeadcarver_da", "Carver"},
			{"item_trophy_undeadcarver_desc_da", "Animated trophy of an Undead Carver"},
			{"item_trophy_undeaddesecrator_da", "Desecrator"},
			{"item_trophy_undeaddesecrator_desc_da", "Animated trophy of an Undead Desecrator"},
			{"item_trophy_undeadreaver_da", "Reaver"},
			{"item_trophy_undeadreaver_desc_da", "Animated trophy of an Undead Reaver"},
			{"item_trophy_undeadripper_da", "Ripper"},
			{"item_trophy_undeadripper_desc_da", "Animated trophy of an Undead Ripper"},
			{"enemy_undeadboss_da", "Zarathos"},
			{"enemy_vampire_da", "Vampire"},
			{"enemy_skeleton_da", "Cadaver"},
			{"enemy_undead_da", "Undead"},
			{"enemy_undeadcarver_da", "Carver"},
			{"enemy_undeaddesecrator_da", "Desecrator"},
			{"enemy_undeadreaver_da", "Reaver"},
			{"enemy_undeadripper_da", "Ripper"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_trophy_undeadboss_da", "Zarathos"},
			{"item_trophy_undeadboss_desc_da", "Animated trophy of Zarathos"},
			{"item_trophy_vampire_da", "Vampire"},
			{"item_trophy_vampire_desc_da", "Animated trophy of a Vampire"},
			{"item_trophy_skeleton1h_da", "Skeleton"},
			{"item_trophy_skeleton1h_desc_da", "Animated trophy of a Skeleton with a Sword and Shield"},
			{"item_trophy_skeleton2h_da", "Skeleton"},
			{"item_trophy_skeleton2h_desc_da", "Animated trophy of a Skeleton with a Two-Handed Sword"},
			{"item_trophy_undead_da", "Undead"},
			{"item_trophy_undead_desc_da", "Animated trophy of an Undead"},
			{"item_trophy_undeadcarver_da", "Carver"},
			{"item_trophy_undeadcarver_desc_da", "Animated trophy of an Undead Carver"},
			{"item_trophy_undeaddesecrator_da", "Desecrator"},
			{"item_trophy_undeaddesecrator_desc_da", "Animated trophy of an Undead Desecrator"},
			{"item_trophy_undeadreaver_da", "Reaver"},
			{"item_trophy_undeadreaver_desc_da", "Animated trophy of an Undead Reaver"},
			{"item_trophy_undeadripper_da", "Ripper"},
			{"item_trophy_undeadripper_desc_da", "Animated trophy of an Undead Ripper"},
			{"enemy_undeadboss_da", "Zarathos"},
			{"enemy_vampire_da", "Vampire"},
			{"enemy_skeleton_da", "Skeleton"},
			{"enemy_undead_da", "Undead"},
			{"enemy_undeadcarver_da", "Carver"},
			{"enemy_undeaddesecrator_da", "Desecrator"},
			{"enemy_undeadreaver_da", "Reaver"},
			{"enemy_undeadripper_da", "Ripper"}

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
		public static class DALocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
