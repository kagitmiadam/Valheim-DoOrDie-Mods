using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace DoDNPCs
{
	[HarmonyPatch]
	public class DoDNPCLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"piece_rylanaltar_dod", "Rylan's Altar"},
			{"piece_rylanaltar_text_dod", "Requires 10 Grey Pearls"},
			{"npc_rylan_dod", "Laughing Rylan"},
			{"boss_meadows_ashvexx", "Ash Vexx"},
			{"boss_meadows_cindermortem", "Cinder Mortem"},
			{"boss_meadows_lincolnhunt", "Lincoln Hunt"},
			{"boss_meadows_dravennox", "Draven Nox"},
			{"boss_meadows_lazarusdeamonne", "Lazarus Deamonne"},
			{"boss_meadows_sceledrusshadowend", "Sceledrus Shadowend"},
			{"boss_meadows_mathianserphent", "Mathian Serphent"},
			{"boss_meadows_echoblack", "Echo Black"},
			{"boss_meadows_firionwinter", "Firion Winter"},
			{"boss_meadows_luxfrost", "Lux Frost"},
			{"boss_meadows_jaydenshadowmend", "Jayden Shadowmend"},
			{"boss_meadows_crisenthshadowsoul", "Crisenth Shadowsoul"},
			{"boss_meadows_lazarusautumn", "Lazarus Autumn"},
			{"boss_meadows_grailthornheart", "Grail Thornheart"},
			{"boss_meadows_zaineevilian", "Zaine Evilian"},
			{"boss_meadows_upirgrim", "Upir Grim"},
			{"npc_einherjar_dod", "Einherjar"},
			{"npc_graywolf_dod", "Gray Wolf"},
			{"npc_njord_dod", "Njord"},
			{"npc_nomad_dod", "Nomad"},
			{"npc_vidar_dod", "Vidar"},
			{"npc_skugga_young_dod", "Youngling"},
			{"npc_skugga_dod", "Skugga"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"piece_rylanaltar_dod", "Rylan's Altar"},
			{"piece_rylanaltar_text_dod", "Requires 10 Grey Pearls"},
			{"npc_rylan_dod", "Laughing Rylan"},
			{"boss_meadows_ashvexx", "Ash Vexx"},
			{"boss_meadows_cindermortem", "Cinder Mortem"},
			{"boss_meadows_lincolnhunt", "Lincoln Hunt"},
			{"boss_meadows_dravennox", "Draven Nox"},
			{"boss_meadows_lazarusdeamonne", "Lazarus Deamonne"},
			{"boss_meadows_sceledrusshadowend", "Sceledrus Shadowend"},
			{"boss_meadows_mathianserphent", "Mathian Serphent"},
			{"boss_meadows_echoblack", "Echo Black"},
			{"boss_meadows_firionwinter", "Firion Winter"},
			{"boss_meadows_luxfrost", "Lux Frost"},
			{"boss_meadows_jaydenshadowmend", "Jayden Shadowmend"},
			{"boss_meadows_crisenthshadowsoul", "Crisenth Shadowsoul"},
			{"boss_meadows_lazarusautumn", "Lazarus Autumn"},
			{"boss_meadows_grailthornheart", "Grail Thornheart"},
			{"boss_meadows_zaineevilian", "Zaine Evilian"},
			{"boss_meadows_upirgrim", "Upir Grim"},
			{"npc_einherjar_dod", "Einherjar"},
			{"npc_graywolf_dod", "Gray Wolf"},
			{"npc_njord_dod", "Njord"},
			{"npc_nomad_dod", "Nomad"},
			{"npc_vidar_dod", "Vidar"},
			{"npc_skugga_young_dod", "Youngling"},
			{"npc_skugga_dod", "Skugga"}

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"piece_rylanaltar_dod", "Rylan's Altar"},
			{"piece_rylanaltar_text_dod", "Requires 10 Grey Pearls"},
			{"npc_rylan_dod", "Laughing Rylan"},
			{"boss_meadows_ashvexx", "Ash Vexx"},
			{"boss_meadows_cindermortem", "Cinder Mortem"},
			{"boss_meadows_lincolnhunt", "Lincoln Hunt"},
			{"boss_meadows_dravennox", "Draven Nox"},
			{"boss_meadows_lazarusdeamonne", "Lazarus Deamonne"},
			{"boss_meadows_sceledrusshadowend", "Sceledrus Shadowend"},
			{"boss_meadows_mathianserphent", "Mathian Serphent"},
			{"boss_meadows_echoblack", "Echo Black"},
			{"boss_meadows_firionwinter", "Firion Winter"},
			{"boss_meadows_luxfrost", "Lux Frost"},
			{"boss_meadows_jaydenshadowmend", "Jayden Shadowmend"},
			{"boss_meadows_crisenthshadowsoul", "Crisenth Shadowsoul"},
			{"boss_meadows_lazarusautumn", "Lazarus Autumn"},
			{"boss_meadows_grailthornheart", "Grail Thornheart"},
			{"boss_meadows_zaineevilian", "Zaine Evilian"},
			{"boss_meadows_upirgrim", "Upir Grim"},
			{"npc_einherjar_dod", "Einherjar"},
			{"npc_graywolf_dod", "Gray Wolf"},
			{"npc_njord_dod", "Njord"},
			{"npc_nomad_dod", "Nomad"},
			{"npc_vidar_dod", "Vidar"},
			{"npc_skugga_young_dod", "Youngling"},
			{"npc_skugga_dod", "Skugga"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"piece_rylanaltar_dod", "Rylan's Altar"},
			{"piece_rylanaltar_text_dod", "Requires 10 Grey Pearls"},
			{"npc_rylan_dod", "Laughing Rylan"},
			{"boss_meadows_ashvexx", "Ash Vexx"},
			{"boss_meadows_cindermortem", "Kül Ölüm"},
			{"boss_meadows_lincolnhunt", "Avcı Lincoln"},
			{"boss_meadows_dravennox", "Draven Nox"},
			{"boss_meadows_lazarusdeamonne", "Lazarus Deamonne"},
			{"boss_meadows_sceledrusshadowend", "Sceledrus Songölge"},
			{"boss_meadows_mathianserphent", "Mathian Serphent"},
			{"boss_meadows_echoblack", "Karanlık Yankı"},
			{"boss_meadows_firionwinter", "Ateş Kışı"},
			{"boss_meadows_luxfrost", "Ayaz Lux"},
			{"boss_meadows_jaydenshadowmend", "Jayden Gölgetamir"},
			{"boss_meadows_crisenthshadowsoul", "Crisenth Gölgeruh"},
			{"boss_meadows_lazarusautumn", "Bahar Lazarus"},
			{"boss_meadows_grailthornheart", "Grail Dikenlikalp"},
			{"boss_meadows_zaineevilian", "Zaine Evilian"},
			{"boss_meadows_upirgrim", "Upir Grim"},
			{"npc_einherjar_dod", "Einherjar"},
			{"npc_graywolf_dod", "Gri Kurt"},
			{"npc_njord_dod", "Njord"},
			{"npc_nomad_dod", "Nomad"},
			{"npc_vidar_dod", "Vidar"},
			{"npc_skugga_young_dod", "Youngling"},
			{"npc_skugga_dod", "Skugga"}

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
		public static class DoDNPCLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
