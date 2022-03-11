using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace ThemedBuildPieces
{
	[HarmonyPatch]
	public class TBPLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Stone House" },

								{ "piece_stonepillar_tp", "Stone Pillar" },
								{ "piece_roundwalls_tp", "Small Stone Tower Wall" },
								{ "piece_roundwall_tp", "Stone Tower Wall" },
								{ "piece_roundfloorle4_tp", "Stone Tower Floor Q4" },
								{ "piece_roundfloorle3_tp", "Stone Tower Floor Q3" },
								{ "piece_roundfloorle2_tp", "Stone Tower Floor Q2" },
								{ "piece_roundfloorle1_tp", "Stone Tower Floor Q1" },
								{ "piece_roundfloors4_tp", "Small Stone Tower Floor Q4" },
								{ "piece_roundfloors3_tp", "Small Stone Tower Floor Q3" },
								{ "piece_roundfloors2_tp", "Small Stone Tower Floor Q2" },
								{ "piece_roundfloors1_tp", "Small Stone Tower Floor Q1" },
								{ "piece_roundstairs_tp", "Stone Tower Stairs" },
								{ "piece_roundbase_tp", "Stone Tower Base" },
								{ "piece_roundwindowws_tp", "Stone Tower Glass Window" },
								{ "piece_roundwindowwm_tp", "Stone Tower Window" },

								{ "item_tbphammer_tp", "Themed Hammer" },
								{ "item_tbphammer_description_tp", "Required for the construction of themed building pieces" },

								{ "piece_thickstonewall8_tp", "Thick Stone Wall 2x8m" },
								{ "piece_thinstonewall4_tp", "Thin Stone Wall 4x4m" },
								{ "piece_thatchwall2_tp", "Thatch Wall 4m" },
								{ "piece_thatchwall1_tp", "Thatch Wall 2m" },
								{ "piece_thatchwindow2x1_tp", "Thatch Window 2m" },
								{ "piece_cobblewall2_tp", "Cobble Wall 4m" },
								{ "piece_cobblewall1_tp", "Cobble Wall 2m" },
								{ "piece_cobblewindow2x1_tp", "Cobble Window 2m" },
								{ "piece_oakwall2_tp", "Oak Wall 4m" },
								{ "piece_oakwall1_tp", "Oak Wall 2m" },
								{ "piece_oakwindow2x1_tp", "Oak Window 2m" },
								{ "piece_greywall2_tp", "Aged Wall 4m" },
								{ "piece_greywall1_tp", "Aged Wall 2m" },
								{ "piece_greywindow2x1_tp", "Aged Window 2m" },
								{ "piece_pinewall2_tp", "Pine Wall 4m" },
								{ "piece_pinewall1_tp", "Pine Wall 2m" },
								{ "piece_pinewindow2x1_tp", "Pine Window 2m" },
								{ "piece_tudorwall2_tp", "Tudor Wall 4m" },
								{ "piece_tudorwall1_tp", "Tudor Wall 2m" },
								{ "piece_tudorwindow2x1_tp", "Tudor Window 2m" },
								{ "piece_hardwoodpost_tp", "Hardwood Post 2m" },
								{ "piece_hardwoodpostl_tp", "Hardwood Post 4m" },
								{ "piece_clayroof26_tp", "Clay Roof 26" },
								{ "piece_clayroof45_tp", "Clay Roof 45" },
								{ "piece_worntudorwall2_tp", "Worn Tudor Wall 4m" },
								{ "piece_worntudorwall1_tp", "Worn Tudor Wall 2m" },
								{ "piece_worntudorwindow2x1_tp", "Worn Tudor Window 2m" },
								{ "piece_hardwoodbeam_tp", "Hardwood Beam 2m" },
								{ "piece_hardwoodbeaml_tp", "Hardwood Beam 4m" },
								{ "piece_hardwoodbeamh_tp", "Hardwood Beam Half" },
								{ "piece_hardwoodbeam45_tp", "Hardwood Beam 45" },
								{ "piece_worntudorfloor_tp", "Worn Tudor Floor 2m" },
								{ "piece_clayrooftop45_tp", "Clay Roof Top 45" },
								{ "piece_clayrooftop45E_tp", "Clay Roof Top End 45" },
								{ "piece_hardwooddoor_tp", "Hardwood Door" },
								{ "piece_wallside45_tp", "Side Wall 45" }
		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Stone House" },

								{ "piece_stonepillar_tp", "Stone Pillar" },
								{ "piece_roundwalls_tp", "Small Stone Tower Wall" },
								{ "piece_roundwall_tp", "Stone Tower Wall" },
								{ "piece_roundfloorle4_tp", "Stone Tower Floor Q4" },
								{ "piece_roundfloorle3_tp", "Stone Tower Floor Q3" },
								{ "piece_roundfloorle2_tp", "Stone Tower Floor Q2" },
								{ "piece_roundfloorle1_tp", "Stone Tower Floor Q1" },
								{ "piece_roundfloors4_tp", "Small Stone Tower Floor Q4" },
								{ "piece_roundfloors3_tp", "Small Stone Tower Floor Q3" },
								{ "piece_roundfloors2_tp", "Small Stone Tower Floor Q2" },
								{ "piece_roundfloors1_tp", "Small Stone Tower Floor Q1" },
								{ "piece_roundstairs_tp", "Stone Tower Stairs" },
								{ "piece_roundbase_tp", "Stone Tower Base" },
								{ "piece_roundwindowws_tp", "Stone Tower Glass Window" },
								{ "piece_roundwindowwm_tp", "Stone Tower Window" },

								{ "item_tbphammer_tp", "Themed Hammer" },
								{ "item_tbphammer_description_tp", "Required for the construction of themed building pieces" },

								{ "piece_slateroof45_tp", "Slate Roof 45" },
								{ "piece_slaterooftop45E_tp", "Slate Roof Top End 45" },
								{ "piece_thatchwall2_tp", "Thatch Wall 4m" },
								{ "piece_thatchwall1_tp", "Thatch Wall 2m" },
								{ "piece_thatchwindow2x1_tp", "Thatch Window 2m" },
								{ "piece_cobblewall2_tp", "Cobble Wall 4m" },
								{ "piece_cobblewall1_tp", "Cobble Wall 2m" },
								{ "piece_cobblewindow2x1_tp", "Cobble Window 2m" },
								{ "piece_oakwall2_tp", "Oak Wall 4m" },
								{ "piece_oakwall1_tp", "Oak Wall 2m" },
								{ "piece_oakwindow2x1_tp", "Oak Window 2m" },
								{ "piece_greywall2_tp", "Aged Wall 4m" },
								{ "piece_greywall1_tp", "Aged Wall 2m" },
								{ "piece_greywindow2x1_tp", "Aged Window 2m" },
								{ "piece_pinewall2_tp", "Pine Wall 4m" },
								{ "piece_pinewall1_tp", "Pine Wall 2m" },
								{ "piece_pinewindow2x1_tp", "Pine Window 2m" },
								{ "piece_tudorwall2_tp", "Tudor Wall 4m" },
								{ "piece_tudorwall1_tp", "Tudor Wall 2m" },
								{ "piece_tudorwindow2x1_tp", "Tudor Window 2m" },
								{ "piece_hardwoodpost_tp", "Hardwood Post 2m" },
								{ "piece_hardwoodpostl_tp", "Hardwood Post 4m" },
								{ "piece_clayroof26_tp", "Clay Roof 26" },
								{ "piece_clayroof45_tp", "Clay Roof 45" },
								{ "piece_clayrooftop45E_tp", "Clay Roof Top End 45" },
								{ "piece_clayrooftop45_tp", "Clay Roof Top 45" },
								{ "piece_worntudorwall2_tp", "Tudor Wall 4x2" },
								{ "piece_worntudorwall1_tp", "Tudor Wall 2x2" },
								{ "piece_hardwooddoor_tp", "Hardwood Door" },
								{ "piece_worntudorfloor_tp", "Worn Tudor Floor" },
								{ "piece_worntudorwindow2x1_tp", "Worn Tudor Window 2x1" },
								{ "piece_hardwoodbeaml_tp", "Hard Wood Beam Long" },
								{ "piece_hardwoodbeamh_tp", "Hard Wood Beam Half" },
								{ "piece_hardwoodbeam_tp", "Hardwood Beam" },
								{ "piece_hardwoodbeam45_tp", "Hardwood Beam 45" },
								{ "piece_wallside45_tp", "Side Wall 45" }
		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

								{ "piece_stonehouse_tp", "Taş Ev" },

								{ "piece_stonepillar_tp", "Taş Sütun" },
								{ "piece_roundwalls_tp", "Small Stone Tower Wall" },
								{ "piece_roundwall_tp", "Küçük Taş Kule Duvarı" },
								{ "piece_roundfloorle4_tp", "Küçük Taş Kule Duvarı Q4" },
								{ "piece_roundfloorle3_tp", "Taş Kule Duvarı Q3" },
								{ "piece_roundfloorle2_tp", "Taş Kule Duvarı Q2" },
								{ "piece_roundfloorle1_tp", "Taş Kule Duvarı Q1" },
								{ "piece_roundfloors4_tp", "Küçük Taş Kule Zemini Q4" },
								{ "piece_roundfloors3_tp", "Küçük Taş Kule Zemini Q3" },
								{ "piece_roundfloors2_tp", "Küçük Taş Kule Zemini Q2" },
								{ "piece_roundfloors1_tp", "Küçük Taş Kule Zemini Q1" },
								{ "piece_roundstairs_tp", "Taş Kule Merdivenleri" },
								{ "piece_roundbase_tp", "Taş Kule Zemini" },
								{ "piece_roundwindowws_tp", "Taş Kule Cam Pencere" },
								{ "piece_roundwindowwm_tp", "Taş Kule Penceresi" },

								{ "item_tbphammer_tp", "Temalı Çekiç" },
								{ "item_tbphammer_description_tp", "Temalı yapı parçalarının inşası için gereklidir." },

								{ "piece_thickstonewall8_tp", "Kalın Taş Duvar 2x8m" },
								{ "piece_thinstonewall4_tp", "İnce Taş Duvar 4x4m" },
								{ "piece_thatchwall2_tp", "Saman Duvar 4m" },
								{ "piece_thatchwall1_tp", "Saman Duvar 2m" },
								{ "piece_thatchwindow2x1_tp", "Saman Pencere 2m" },
								{ "piece_cobblewall2_tp", "Kırıktaş Duvar 4m" },
								{ "piece_cobblewall1_tp", "Kırıktaş Duvar 2m" },
								{ "piece_cobblewindow2x1_tp", "Kırıktaş Pencere 2m" },
								{ "piece_oakwall2_tp", "Meşe Duvar 4m" },
								{ "piece_oakwall1_tp", "Meşe Duvar 2m" },
								{ "piece_oakwindow2x1_tp", "Meşe Pencere 2m" },
								{ "piece_greywall2_tp", "Gri Duvar 4m" },
								{ "piece_greywall1_tp", "Gri Duvar 2m" },
								{ "piece_greywindow2x1_tp", "Gri Pencere 2m" },
								{ "piece_pinewall2_tp", "Çam Duvar 4m" },
								{ "piece_pinewall1_tp", "Çam Duvar 2m" },
								{ "piece_pinewindow2x1_tp", "Çam Pencere 2m" },
								{ "piece_tudorwall2_tp", "Tudor Duvar 4m" },
								{ "piece_tudorwall1_tp", "Tudor Duvar 2m" },
								{ "piece_tudorwindow2x1_tp", "Tudor Pencere 2m" },
								{ "piece_hardwoodpost_tp", "Sertağaç Kolon 2m" },
								{ "piece_hardwoodpostl_tp", "Sertağaç Kolon 4m" },
								{ "piece_clayroof26_tp", "Kil Çatı 26" },
								{ "piece_clayroof45_tp", "Kil Çatı 45" },
								{ "piece_worntudorwall2_tp", "Yıpranmış Tudor Duvarı 4m" },
								{ "piece_worntudorwall1_tp", "Yıpranmış Tudor Duvarı 2m" },
								{ "piece_worntudorwindow2x1_tp", "Yıpranmış Tudor Penceresi 2m" },
								{ "piece_hardwoodbeam_tp", "Sertağaç Kiriş 2m" },
								{ "piece_hardwoodbeaml_tp", "Sertağaç Kiriş 4m" },
								{ "piece_hardwoodbeamh_tp", "Sertağaç Kiriş Yarım" },
								{ "piece_hardwoodbeam45_tp", "Sertağaç Kiriş 45" },
								{ "piece_worntudorfloor_tp", "Yıpranmış Tudor Zemin 2m" },
								{ "piece_clayrooftop45_tp", "Kil Çatı Üst 45" },
								{ "piece_clayrooftop45E_tp", "Kil Çatı Üst Köşe 45" },
								{ "piece_hardwooddoor_tp", "Sertağaç Kapı" },
								{ "piece_wallside45_tp", "Yan Duvar 45" }
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
		public static class TBPLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}

