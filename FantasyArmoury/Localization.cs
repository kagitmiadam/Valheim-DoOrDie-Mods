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

			{"item_sword2h_01_fa", "Faithkeeper, Jaws of Illuminated Dreams"},
			{"item_sword2h_01_fa_description", "Placeholder"},
			{"item_sword2h_02_fa", "Purifier, Spellblade of Lost Voices"},
			{"item_sword2h_02_fa_description", "Placeholder"},
			{"item_sword2h_03_fa", "Malignant Defender"},
			{"item_sword2h_03_fa_description", "Placeholder"},
			{"item_sword2h_04_fa", "Vindictive Spellblade"},
			{"item_sword2h_04_fa_description", "Placeholder"},
			{"item_sword2h_05_fa", "Reckoning, Gift of the Sunwalker"},
			{"item_sword2h_05_fa_description", "Placeholder"},
			{"item_sword2h_06_fa", "Betrayer"},
			{"item_sword2h_06_fa_description", "Placeholder"},
			{"item_sword1h_01_fa", "Fortune's Razor"},
			{"item_sword1h_01_fa_description", "Placeholder"},
			{"item_sword1h_02_fa", "Ritual Reaver"},
			{"item_sword1h_02_fa_description", "Placeholder"},
			{"item_sword1h_03_fa", "Sharpened Spellblade"},
			{"item_sword1h_03_fa_description", "Placeholder"},
			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "Placeholder"},
			{"item_sword1h_05_fa", "Lusting Silver Slicer"},
			{"item_sword1h_05_fa_description", "Placeholder"},
			{"item_staff2h_01_fa", "Scar"},
			{"item_staff2h_01_fa_description", "Placeholder"},
			{"item_staff2h_02_fa", "Fate"},
			{"item_staff2h_02_fa_description", "Placeholder"},
			{"item_staff2h_03_fa", "Seism"},
			{"item_staff2h_03_fa_description", "Placeholder"},
			{"item_staff2h_04_fa", "Comet"},
			{"item_staff2h_04_fa_description", "Placeholder"},
			{"item_staff2h_05_fa", "Sleepwalker"},
			{"item_staff2h_05_fa_description", "Placeholder"},
			{"item_scythe2h_01_fa", "Extinction"},
			{"item_scythe2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_01_fa", "Blackout"},
			{"item_hammer2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_02_fa", "Willbreaker"},
			{"item_hammer2h_02_fa_description", "Placeholder"},
			{"item_hammer2h_03_fa", "Reckoning"},
			{"item_hammer2h_03_fa_description", "Placeholder"},
			{"item_axe1h_01_fa", "Remorse"},
			{"item_axe1h_01_fa_description", "Placeholder"},
			{"item_axe1h_02_fa", "Celeste"},
			{"item_axe1h_02_fa_description", "Placeholder"},
			{"item_axe1h_03_fa", "Nirvana"},
			{"item_axe1h_03_fa_description", "Placeholder"},
			{"item_axe1h_04_fa", "Justifier"},
			{"item_axe1h_04_fa_description", "Placeholder"},
			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Placeholder"},
			{"item_axe1h_06_fa", "Harvester"},
			{"item_axe1h_06_fa_description", "Placeholder"},
			{"item_axe1h_07_fa", "Hope's Ravager"},
			{"item_axe1h_07_fa_description", "Placeholder"},
			{"item_axe2h_06_fa", "Soul Breaker"},
			{"item_axe2h_06_fa_description", "Placeholder"},
			{"item_axe2h_05_fa", "Spinefall"},
			{"item_axe2h_05_fa_description", "Placeholder"},
			{"item_axe2h_04_fa", "Narcoleptic"},
			{"item_axe2h_04_fa_description", "Placeholder"},
			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "Placeholder"},
			{"item_axe2h_02_fa", "Broken Promise"},
			{"item_axe2h_02_fa_description", "Placeholder"},
			{"item_axe2h_01_fa", "Dire Axe"},
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

			{"item_sword2h_01_fa", "Faithkeeper, Jaws of Illuminated Dreams"},
			{"item_sword2h_01_fa_description", "Placeholder"},
			{"item_sword2h_02_fa", "Purifier, Spellblade of Lost Voices"},
			{"item_sword2h_02_fa_description", "Placeholder"},
			{"item_sword2h_03_fa", "Malignant Defender"},
			{"item_sword2h_03_fa_description", "Placeholder"},
			{"item_sword2h_04_fa", "Vindictive Spellblade"},
			{"item_sword2h_04_fa_description", "Placeholder"},
			{"item_sword2h_05_fa", "Reckoning, Gift of the Sunwalker"},
			{"item_sword2h_05_fa_description", "Placeholder"},
			{"item_sword2h_06_fa", "Betrayer"},
			{"item_sword2h_06_fa_description", "Placeholder"},
			{"item_sword1h_01_fa", "Fortune's Razor"},
			{"item_sword1h_01_fa_description", "Placeholder"},
			{"item_sword1h_02_fa", "Ritual Reaver"},
			{"item_sword1h_02_fa_description", "Placeholder"},
			{"item_sword1h_03_fa", "Sharpened Spellblade"},
			{"item_sword1h_03_fa_description", "Placeholder"},
			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "Placeholder"},
			{"item_sword1h_05_fa", "Lusting Silver Slicer"},
			{"item_sword1h_05_fa_description", "Placeholder"},
			{"item_staff2h_01_fa", "Scar"},
			{"item_staff2h_01_fa_description", "Placeholder"},
			{"item_staff2h_02_fa", "Fate"},
			{"item_staff2h_02_fa_description", "Placeholder"},
			{"item_staff2h_03_fa", "Seism"},
			{"item_staff2h_03_fa_description", "Placeholder"},
			{"item_staff2h_04_fa", "Comet"},
			{"item_staff2h_04_fa_description", "Placeholder"},
			{"item_staff2h_05_fa", "Sleepwalker"},
			{"item_staff2h_05_fa_description", "Placeholder"},
			{"item_scythe2h_01_fa", "Extinction"},
			{"item_scythe2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_01_fa", "Blackout"},
			{"item_hammer2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_02_fa", "Willbreaker"},
			{"item_hammer2h_02_fa_description", "Placeholder"},
			{"item_hammer2h_03_fa", "Reckoning"},
			{"item_hammer2h_03_fa_description", "Placeholder"},
			{"item_axe1h_01_fa", "Remorse"},
			{"item_axe1h_01_fa_description", "Placeholder"},
			{"item_axe1h_02_fa", "Celeste"},
			{"item_axe1h_02_fa_description", "Placeholder"},
			{"item_axe1h_03_fa", "Nirvana"},
			{"item_axe1h_03_fa_description", "Placeholder"},
			{"item_axe1h_04_fa", "Justifier"},
			{"item_axe1h_04_fa_description", "Placeholder"},
			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Placeholder"},
			{"item_axe1h_06_fa", "Harvester"},
			{"item_axe1h_06_fa_description", "Placeholder"},
			{"item_axe1h_07_fa", "Hope's Ravager"},
			{"item_axe1h_07_fa_description", "Placeholder"},
			{"item_axe2h_06_fa", "Soul Breaker"},
			{"item_axe2h_06_fa_description", "Placeholder"},
			{"item_axe2h_05_fa", "Spinefall"},
			{"item_axe2h_05_fa_description", "Placeholder"},
			{"item_axe2h_04_fa", "Narcoleptic"},
			{"item_axe2h_04_fa_description", "Placeholder"},
			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "Placeholder"},
			{"item_axe2h_02_fa", "Broken Promise"},
			{"item_axe2h_02_fa_description", "Placeholder"},
			{"item_axe2h_01_fa", "Dire Axe"},
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
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_sword2h_01_fa", "Faithkeeper, Jaws of Illuminated Dreams"},
			{"item_sword2h_01_fa_description", "Placeholder"},
			{"item_sword2h_02_fa", "Purifier, Spellblade of Lost Voices"},
			{"item_sword2h_02_fa_description", "Placeholder"},
			{"item_sword2h_03_fa", "Malignant Defender"},
			{"item_sword2h_03_fa_description", "Placeholder"},
			{"item_sword2h_04_fa", "Vindictive Spellblade"},
			{"item_sword2h_04_fa_description", "Placeholder"},
			{"item_sword2h_05_fa", "Reckoning, Gift of the Sunwalker"},
			{"item_sword2h_05_fa_description", "Placeholder"},
			{"item_sword2h_06_fa", "Betrayer"},
			{"item_sword2h_06_fa_description", "Placeholder"},
			{"item_sword1h_01_fa", "Fortune's Razor"},
			{"item_sword1h_01_fa_description", "Placeholder"},
			{"item_sword1h_02_fa", "Ritual Reaver"},
			{"item_sword1h_02_fa_description", "Placeholder"},
			{"item_sword1h_03_fa", "Sharpened Spellblade"},
			{"item_sword1h_03_fa_description", "Placeholder"},
			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "Placeholder"},
			{"item_sword1h_05_fa", "Lusting Silver Slicer"},
			{"item_sword1h_05_fa_description", "Placeholder"},
			{"item_staff2h_01_fa", "Scar"},
			{"item_staff2h_01_fa_description", "Placeholder"},
			{"item_staff2h_02_fa", "Fate"},
			{"item_staff2h_02_fa_description", "Placeholder"},
			{"item_staff2h_03_fa", "Seism"},
			{"item_staff2h_03_fa_description", "Placeholder"},
			{"item_staff2h_04_fa", "Comet"},
			{"item_staff2h_04_fa_description", "Placeholder"},
			{"item_staff2h_05_fa", "Sleepwalker"},
			{"item_staff2h_05_fa_description", "Placeholder"},
			{"item_scythe2h_01_fa", "Extinction"},
			{"item_scythe2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_01_fa", "Blackout"},
			{"item_hammer2h_01_fa_description", "Placeholder"},
			{"item_hammer2h_02_fa", "Willbreaker"},
			{"item_hammer2h_02_fa_description", "Placeholder"},
			{"item_hammer2h_03_fa", "Reckoning"},
			{"item_hammer2h_03_fa_description", "Placeholder"},
			{"item_axe1h_01_fa", "Remorse"},
			{"item_axe1h_01_fa_description", "Placeholder"},
			{"item_axe1h_02_fa", "Celeste"},
			{"item_axe1h_02_fa_description", "Placeholder"},
			{"item_axe1h_03_fa", "Nirvana"},
			{"item_axe1h_03_fa_description", "Placeholder"},
			{"item_axe1h_04_fa", "Justifier"},
			{"item_axe1h_04_fa_description", "Placeholder"},
			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Placeholder"},
			{"item_axe1h_06_fa", "Harvester"},
			{"item_axe1h_06_fa_description", "Placeholder"},
			{"item_axe1h_07_fa", "Hope's Ravager"},
			{"item_axe1h_07_fa_description", "Placeholder"},
			{"item_axe2h_06_fa", "Soul Breaker"},
			{"item_axe2h_06_fa_description", "Placeholder"},
			{"item_axe2h_05_fa", "Spinefall"},
			{"item_axe2h_05_fa_description", "Placeholder"},
			{"item_axe2h_04_fa", "Narcoleptic"},
			{"item_axe2h_04_fa_description", "Placeholder"},
			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "Placeholder"},
			{"item_axe2h_02_fa", "Broken Promise"},
			{"item_axe2h_02_fa_description", "Placeholder"},
			{"item_axe2h_01_fa", "Dire Axe"},
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
