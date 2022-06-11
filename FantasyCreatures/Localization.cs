using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FantasyCreatures
{
	[HarmonyPatch]
	public class FCLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_trophy_cyclops_fc", "Cyclops"},
			{"item_trophy_cyclops_desc_fc", "Animated trophy of a Cyclops"},
			{"item_trophy_darknessspider_fc", "Giant Spider"},
			{"item_trophy_darknessspider_desc_fc", "Animated trophy of a Darkness Spider"},
			{"item_trophy_demonlord_fc", "Balrog"},
			{"item_trophy_demonlord_desc_fc", "Animated trophy of a Balrog"},
			{"item_trophy_earthelemental_fc", "Earth Elemental"},
			{"item_trophy_earthelemental_desc_fc", "Animated trophy of a Earth Elemental"},
			{"item_trophy_ent_fc", "Ent"},
			{"item_trophy_ent_desc_fc", "Animated trophy of a Ent"},
			{"item_trophy_fireelemental_fc", "Fire Elemental"},
			{"item_trophy_fireelemental_desc_fc", "Animated trophy of a Fire Elemental"},
			{"item_trophy_ghoul_fc", "Ghoul"},
			{"item_trophy_ghoul_desc_fc", "Animated trophy of a Ghoul"},
			{"item_trophy_giantviper_fc", "Giant Viper"},
			{"item_trophy_giantviper_desc_fc", "Animated trophy of a Giant Viper"},
			{"item_trophy_hobgoblin_fc", "Hobgoblin"},
			{"item_trophy_hobgoblin_desc_fc", "Animated trophy of a Hobgoblin"},
			{"item_trophy_iceelemental_fc", "Ice Elemental"},
			{"item_trophy_iceelemental_desc_fc", "Animated trophy of a Ice Elemental"},
			{"item_trophy_kobold_fc", "Kobold"},
			{"item_trophy_kobold_desc_fc", "Animated trophy of a Kobold"},
			{"item_trophy_manticore_fc", "Manticore"},
			{"item_trophy_manticore_desc_fc", "Animated trophy of a Manticore"},
			{"item_trophy_mummy_fc", "Mummy"},
			{"item_trophy_mummy_desc_fc", "Animated trophy of a Mummy"},
			{"item_trophy_ogre_fc", "Ogre"},
			{"item_trophy_ogre_desc_fc", "Animated trophy of a Ogre"},
			{"enemy_demonlord_dod", "Balrog"},
			{"enemy_fireelemental_dod", "Fire Elemental"},
			{"enemy_iceelemental_dod", "Ice Elemental"},
			{"enemy_darknessspider_fc", "Giant Spider"},
			{"enemy_elemental_dod", "Elemental"},
			{"enemy_manticore_fc", "Manticore"},
			{"enemy_cyclops_dod", "Cyclops"},
			{"enemy_ogre_dod", "Ogre"},
			{"enemy_ghoul_fc", "Ghoul"},
			{"enemy_mummy_fc", "Mummy"},
			{"enemy_treeent_dod", "Ent"},
			{"enemy_hobgoblin_dod", "Hobgoblin"},
			{"enemy_giantviper_fc", "Giant Viper"},
			{"enemy_kobold_dod", "Kobold"},
			// Unfinished
			{"enemy_harpy_fc", "Harpy"},
			{"enemy_griffin_fc", "Griffin"},
			{"enemy_beholder_dod", "Beholder"},
			{"enemy_hydra_dod", "Hydra"},
			{"enemy_dragon_dod", "Dragon"},
			{"enemy_lizard_dod", "Lizardman"},

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_trophy_cyclops_fc", "Cyclops"},
			{"item_trophy_cyclops_desc_fc", "Animated trophy of a Cyclops"},
			{"item_trophy_darknessspider_fc", "Giant Spider"},
			{"item_trophy_darknessspider_desc_fc", "Animated trophy of a Darkness Spider"},
			{"item_trophy_demonlord_fc", "Balrog"},
			{"item_trophy_demonlord_desc_fc", "Animated trophy of a Balrog"},
			{"item_trophy_earthelemental_fc", "Earth Elemental"},
			{"item_trophy_earthelemental_desc_fc", "Animated trophy of a Earth Elemental"},
			{"item_trophy_ent_fc", "Ent"},
			{"item_trophy_ent_desc_fc", "Animated trophy of a Ent"},
			{"item_trophy_fireelemental_fc", "Fire Elemental"},
			{"item_trophy_fireelemental_desc_fc", "Animated trophy of a Fire Elemental"},
			{"item_trophy_ghoul_fc", "Ghoul"},
			{"item_trophy_ghoul_desc_fc", "Animated trophy of a Ghoul"},
			{"item_trophy_giantviper_fc", "Giant Viper"},
			{"item_trophy_giantviper_desc_fc", "Animated trophy of a Giant Viper"},
			{"item_trophy_hobgoblin_fc", "Hobgoblin"},
			{"item_trophy_hobgoblin_desc_fc", "Animated trophy of a Hobgoblin"},
			{"item_trophy_iceelemental_fc", "Ice Elemental"},
			{"item_trophy_iceelemental_desc_fc", "Animated trophy of a Ice Elemental"},
			{"item_trophy_kobold_fc", "Kobold"},
			{"item_trophy_kobold_desc_fc", "Animated trophy of a Kobold"},
			{"item_trophy_manticore_fc", "Manticore"},
			{"item_trophy_manticore_desc_fc", "Animated trophy of a Manticore"},
			{"item_trophy_mummy_fc", "Mummy"},
			{"item_trophy_mummy_desc_fc", "Animated trophy of a Mummy"},
			{"item_trophy_ogre_fc", "Ogre"},
			{"item_trophy_ogre_desc_fc", "Animated trophy of a Ogre"},
			{"enemy_demonlord_dod", "Balrog"},
			{"enemy_fireelemental_dod", "Fire Elemental"},
			{"enemy_iceelemental_dod", "Ice Elemental"},
			{"enemy_darknessspider_fc", "Giant Spider"},
			{"enemy_elemental_dod", "Elemental"},
			{"enemy_manticore_fc", "Manticore"},
			{"enemy_cyclops_dod", "Cyclops"},
			{"enemy_ogre_dod", "Ogre"},
			{"enemy_ghoul_fc", "Ghoul"},
			{"enemy_mummy_fc", "Mummy"},
			{"enemy_treeent_dod", "Ent"},
			{"enemy_hobgoblin_dod", "Hobgoblin"},
			{"enemy_giantviper_fc", "Giant Viper"},
			{"enemy_kobold_dod", "Kobold"},

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_trophy_cyclops_fc", "Cyclops"},
			{"item_trophy_cyclops_desc_fc", "Animated trophy of a Cyclops"},
			{"item_trophy_darknessspider_fc", "Giant Spider"},
			{"item_trophy_darknessspider_desc_fc", "Animated trophy of a Darkness Spider"},
			{"item_trophy_demonlord_fc", "Balrog"},
			{"item_trophy_demonlord_desc_fc", "Animated trophy of a Balrog"},
			{"item_trophy_earthelemental_fc", "Earth Elemental"},
			{"item_trophy_earthelemental_desc_fc", "Animated trophy of a Earth Elemental"},
			{"item_trophy_ent_fc", "Ent"},
			{"item_trophy_ent_desc_fc", "Animated trophy of a Ent"},
			{"item_trophy_fireelemental_fc", "Fire Elemental"},
			{"item_trophy_fireelemental_desc_fc", "Animated trophy of a Fire Elemental"},
			{"item_trophy_ghoul_fc", "Ghoul"},
			{"item_trophy_ghoul_desc_fc", "Animated trophy of a Ghoul"},
			{"item_trophy_giantviper_fc", "Giant Viper"},
			{"item_trophy_giantviper_desc_fc", "Animated trophy of a Giant Viper"},
			{"item_trophy_hobgoblin_fc", "Hobgoblin"},
			{"item_trophy_hobgoblin_desc_fc", "Animated trophy of a Hobgoblin"},
			{"item_trophy_iceelemental_fc", "Ice Elemental"},
			{"item_trophy_iceelemental_desc_fc", "Animated trophy of a Ice Elemental"},
			{"item_trophy_kobold_fc", "Kobold"},
			{"item_trophy_kobold_desc_fc", "Animated trophy of a Kobold"},
			{"item_trophy_manticore_fc", "Manticore"},
			{"item_trophy_manticore_desc_fc", "Animated trophy of a Manticore"},
			{"item_trophy_mummy_fc", "Mummy"},
			{"item_trophy_mummy_desc_fc", "Animated trophy of a Mummy"},
			{"item_trophy_ogre_fc", "Ogre"},
			{"item_trophy_ogre_desc_fc", "Animated trophy of a Ogre"},
			{"enemy_demonlord_dod", "Balrog"},
			{"enemy_fireelemental_dod", "Fire Elemental"},
			{"enemy_iceelemental_dod", "Ice Elemental"},
			{"enemy_darknessspider_fc", "Giant Spider"},
			{"enemy_elemental_dod", "Elemental"},
			{"enemy_manticore_fc", "Manticore"},
			{"enemy_cyclops_dod", "Cyclops"},
			{"enemy_ogre_dod", "Ogre"},
			{"enemy_ghoul_fc", "Ghoul"},
			{"enemy_mummy_fc", "Mummy"},
			{"enemy_treeent_dod", "Ent"},
			{"enemy_hobgoblin_dod", "Hobgoblin"},
			{"enemy_giantviper_fc", "Giant Viper"},
			{"enemy_kobold_dod", "Kobold"},

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_trophy_cyclops_fc", "Cyclops"},
			{"item_trophy_cyclops_desc_fc", "Animated trophy of a Cyclops"},
			{"item_trophy_darknessspider_fc", "Giant Spider"},
			{"item_trophy_darknessspider_desc_fc", "Animated trophy of a Darkness Spider"},
			{"item_trophy_demonlord_fc", "Balrog"},
			{"item_trophy_demonlord_desc_fc", "Animated trophy of a Balrog"},
			{"item_trophy_earthelemental_fc", "Earth Elemental"},
			{"item_trophy_earthelemental_desc_fc", "Animated trophy of a Earth Elemental"},
			{"item_trophy_ent_fc", "Ent"},
			{"item_trophy_ent_desc_fc", "Animated trophy of a Ent"},
			{"item_trophy_fireelemental_fc", "Fire Elemental"},
			{"item_trophy_fireelemental_desc_fc", "Animated trophy of a Fire Elemental"},
			{"item_trophy_ghoul_fc", "Ghoul"},
			{"item_trophy_ghoul_desc_fc", "Animated trophy of a Ghoul"},
			{"item_trophy_giantviper_fc", "Giant Viper"},
			{"item_trophy_giantviper_desc_fc", "Animated trophy of a Giant Viper"},
			{"item_trophy_hobgoblin_fc", "Hobgoblin"},
			{"item_trophy_hobgoblin_desc_fc", "Animated trophy of a Hobgoblin"},
			{"item_trophy_iceelemental_fc", "Ice Elemental"},
			{"item_trophy_iceelemental_desc_fc", "Animated trophy of a Ice Elemental"},
			{"item_trophy_kobold_fc", "Kobold"},
			{"item_trophy_kobold_desc_fc", "Animated trophy of a Kobold"},
			{"item_trophy_manticore_fc", "Manticore"},
			{"item_trophy_manticore_desc_fc", "Animated trophy of a Manticore"},
			{"item_trophy_mummy_fc", "Mummy"},
			{"item_trophy_mummy_desc_fc", "Animated trophy of a Mummy"},
			{"item_trophy_ogre_fc", "Ogre"},
			{"item_trophy_ogre_desc_fc", "Animated trophy of a Ogre"},
			{"enemy_demonlord_dod", "Balrog"},
			{"enemy_fireelemental_dod", "Ateş Elementali"},
			{"enemy_iceelemental_dod", "Ayaz Elemanteli"},
			{"enemy_darknessspider_fc", "Dev Örümcek"},
			{"enemy_elemental_dod", "Elemental"},
			{"enemy_manticore_fc", "Mantikor"},
			{"enemy_cyclops_dod", "Tepegöz"},
			{"enemy_ogre_dod", "Ogre"},
			{"enemy_ghoul_fc", "Hortlak"},
			{"enemy_mummy_fc", "Mumya"},
			{"enemy_treeent_dod", "Ent"},
			{"enemy_hobgoblin_dod", "İfrit"},
			{"enemy_giantviper_fc", "Dev Engerek"},
			{"enemy_kobold_dod", "Kobold"},
			// Unfinished
			{"enemy_harpy_fc", "Harpi"},
			{"enemy_griffin_fc", "Griffin"},
			{"enemy_beholder_dod", "Beholder"},
			{"enemy_hydra_dod", "Hidra"},
			{"enemy_dragon_dod", "Ejderha"},
			{"enemy_lizard_dod", "Kertenkele Adam"},

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
		public static class FCLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
