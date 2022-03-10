using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace Stronghold
{
	[HarmonyPatch]
	public class SHLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

								{ "piece_towerdoor_sh", "Door" },
								{ "piece_outerwallcovcap_sh", "Outer Wall - Covered, Capped" },
								{ "piece_outerwallcov_sh", "Outer Wall - Covered" },
								{ "piece_outerwallcap_sh", "Outer Wall - Capped" },
								{ "piece_outerwall_sh", "Outer Wall" },
								{ "piece_outertowercenter_sh", "Outer Wall Tower - Center" },
								{ "piece_transitiontower_sh", "Transition Tower" },
								{ "piece_outerroundtower_sh", "Outer Wall - Tower Round" },
								{ "piece_outerroundtowerend_sh", "Outer Wall Tower - Round End" },
								{ "piece_outergate_sh", "Outer Wall Gate" },
								{ "piece_watchtower_sh", "Watchtower" },
								{ "piece_well_sh", "Well" },
								{ "piece_well_sh_desc", "Levels the ground for 50m in each direction and paves it." },
								{ "name_portcullis_sh", "Portcullis" },
								{ "piece_wall2f_sh", "Stronghold Wall" },
								{ "piece_enclosedtower_sh", "Enclosed Tower" },
								{ "piece_bunkhouse_sh", "Bunkhouse" },
								{ "piece_gatehouse_sh", "Gatehouse" },
								{ "piece_musteringhall_sh", "Mustering Hall" },
								{ "piece_towercorner_sh", "Stronghold Tower - Corner" },
								{ "piece_towercenter_sh", "Stronghold Tower - Center" },
								{ "piece_towerjunction_sh", "Stronghold Tower - T Junction" },
								{ "piece_wall2f_nest_sh", "Stronghold Wall - Nest" },
								{ "piece_wall2f_nestcapped_sh", "Stronghold Wall - Nest, Capped" },
								{ "piece_wall2f_ladder_sh", "Stronghold Wall - Ladder" }
		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

								{ "piece_towerdoor_sh", "Door" },
								{ "piece_outerwallcovcap_sh", "Outer Wall - Covered, Capped" },
								{ "piece_outerwallcov_sh", "Outer Wall - Covered" },
								{ "piece_outerwallcap_sh", "Outer Wall - Capped" },
								{ "piece_outerwall_sh", "Outer Wall" },
								{ "piece_outertowercenter_sh", "Outer Wall Tower - Center" },
								{ "piece_transitiontower_sh", "Transition Tower" },
								{ "piece_outerroundtower_sh", "Outer Wall - Tower Round" },
								{ "piece_outerroundtowerend_sh", "Outer Wall Tower - Round End" },
								{ "piece_outergate_sh", "Outer Wall Gate" },
								{ "piece_watchtower_sh", "Watchtower" },
								{ "piece_well_sh", "Well" },
								{ "piece_well_sh_desc", "Levels the ground for 50m in each direction and paves it." },
								{ "name_portcullis_sh", "Portcullis" },
								{ "piece_wall2f_sh", "Stronghold Wall" },
								{ "piece_enclosedtower_sh", "Enclosed Tower" },
								{ "piece_bunkhouse_sh", "Bunkhouse" },
								{ "piece_gatehouse_sh", "Gatehouse" },
								{ "piece_musteringhall_sh", "Mustering Hall" },
								{ "piece_towercorner_sh", "Stronghold Tower - Corner" },
								{ "piece_towercenter_sh", "Stronghold Tower - Center" },
								{ "piece_towerjunction_sh", "Stronghold Tower - T Junction" },
								{ "piece_wall2f_nest_sh", "Stronghold Wall - Nest" },
								{ "piece_wall2f_nestcapped_sh", "Stronghold Wall - Nest, Capped" },
								{ "piece_wall2f_ladder_sh", "Stronghold Wall - Ladder" }

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

								{ "piece_towerdoor_sh", "Kapı" },
								{ "piece_outerwallcovcap_sh", "Dış Duvar - Kapalı, Kapaklı" },
								{ "piece_outerwallcov_sh", "Dış Duvar - Kapalı" },
								{ "piece_outerwallcap_sh", "Dış Duvar - Kapaklı" },
								{ "piece_outerwall_sh", "Dış Duvar" },
								{ "piece_outertowercenter_sh", "Dış Duvar Kule - Orta" },
								{ "piece_transitiontower_sh", "Geçiş Kulesi" },
								{ "piece_outerroundtower_sh", "Dış Duvar - Yuvarlak Kule" },
								{ "piece_outerroundtowerend_sh", "Dış Duvar Kule - Yuvarlak Köşe" },
								{ "piece_outergate_sh", "Dış Duvar Kapısı" },
								{ "piece_watchtower_sh", "Gözetleme Kulesi" },
								{ "piece_well_sh", "Kuyu" },
								{ "piece_well_sh_desc", "Her yönde 50m zemini düzler ve asfaltlar." },
								{ "name_portcullis_sh", "Kale Kapısı" },
								{ "piece_wall2f_sh", "Kale Duvarı" },
								{ "piece_enclosedtower_sh", "Kapalı Kule" },
								{ "piece_bunkhouse_sh", "Ranza" },
								{ "piece_gatehouse_sh", "Ev Kapısı" },
								{ "piece_musteringhall_sh", "Toplantı Salonu" },
								{ "piece_towercorner_sh", "Kale Kulesi - Köşe" },
								{ "piece_towercenter_sh", "Kale Kulesi - Orta" },
								{ "piece_towerjunction_sh", "Kale Kulesi - T Junction" },
								{ "piece_wall2f_nest_sh", "Kale Duvarı - Yuva" },
								{ "piece_wall2f_nestcapped_sh", "Kale Duvarı - Yuva, Kapaklı" },
								{ "piece_wall2f_ladder_sh", "Kale Duvarı - Merdiven" }

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
		public static class SHLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}

