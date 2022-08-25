using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace MedievalVillage
{
	[HarmonyPatch]
	public class MVLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

								{ "item_hammer_mv", "Stone Hammer" },
								{ "item_hammer_desc_mv", "Required for the Medieval Village build pieces." },
								{ "piece_roofbeam45_mv", "Roof Beam 45" },
								{ "piece_foundationbeam_4m_mv", "Foundation Beam" },
								{ "piece_stonewallwindow_mv", "Stone Wall Window" },
								{ "piece_stonewall_mv", "Stone Wall" },
								{ "piece_stonewallcurved_mv", "Stone Wall Curved" },
								{ "piece_stonewallhalf_mv", "Stone Wall Half" },
								{ "piece_stonewallquarter_mv", "Stone Wall Quarter" },
								{ "piece_stonesteps_mv", "Stone Steps" },
								{ "piece_stonestepsshort_mv", "Short Stone Steps" },
								{ "piece_woodsteps_mv", "Wood Steps" },
								{ "piece_floorbeamh_3m_mv", "Floor Beam" },
								{ "piece_stonefoundationa_mv", "Foundation Stone" },
								{ "piece_stonefoundationb_mv", "Foundation Stone" },
								{ "piece_basebeam_3m_mv", "Ground Beam" },
								{ "piece_beamv_3m_mv", "Vertical Beam" },
								{ "piece_roof45_3m_mv", "Tiled Roof 45" },
								{ "piece_roofbeamhend_3m_mv", "Roof Beam" },
								{ "piece_roofwallcenter_3m_mv", "Apex Wall Center" },
								{ "piece_roofwallleft_3m_mv", "Apex Wall Left" },
								{ "piece_roofwallright_3m_mv", "Apex Wall Right" },
								{ "piece_walkway3x1_mv", "Walkway 3x1.5" },
								{ "piece_walkway3x2_mv", "Walkway 3x2" },
								{ "piece_wallcorner3x3_mv", "Wall Corner" },
								{ "piece_wallcross3x3_mv", "Wall Cross" },
								{ "piece_walldoor3x3_mv", "Doorway" },
								{ "piece_wallplain3x3_mv", "Wall Plain" },
								{ "piece_wallstone3x3_mv", "Wall Stone" },
								{ "piece_wallwindow3x3_mv", "Wall Window" },
								{ "piece_wallwood3x3_mv", "Wall Wood" },
								{ "piece_woodwallhalf_mv", "Wall Wood Half" },
								{ "piece_wallwoodt3x3_mv", "Wall Wood Thick" },
								{ "piece_wallwoodwindow3x3_mv", "Wall Wood Window" },
								{ "piece_wallzbar3x3_mv", "Wall ZBar" },
								{ "piece_woodfloor3x2_mv", "Floor 3x2" },
								{ "piece_woodfloor3x3_mv", "Floor 3x3" },
								{ "piece_wallpanelwindow3x3_mv", "Inner Window Panel" },
								{ "piece_wallpanelstone3x3_mv", "Inner Stone Panel" },
								{ "piece_wallpanel3x3_mv", "Inner Wall Panel" },
								{ "piece_stairssplit_mv", "Split Stairs" },
								{ "piece_stairs_mv", "Stairs" },
								{ "piece_beamvthick_3m_mv", "Thick Beam 3M" },
								{ "piece_beamvthick_6m_mv", "Thick Beam 6M" },
								{ "piece_beamhthick_3m_mv", "Thick Beam Horz 3M" }

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

								{ "item_hammer_mv", "Stone Hammer" },
								{ "item_hammer_desc_mv", "Required for the Medieval Village build pieces." },
								{ "piece_stonewallwindow_mv", "Stone Wall Window" },
								{ "piece_stonewall_mv", "Stone Wall" },
								{ "piece_stonewallcurved_mv", "Stone Wall Curved" },
								{ "piece_stonewallhalf_mv", "Stone Wall Half" },
								{ "piece_stonewallquarter_mv", "Stone Wall Quarter" },
								{ "piece_stonesteps_mv", "Stone Steps" },
								{ "piece_stonestepsshort_mv", "Short Stone Steps" },
								{ "piece_woodsteps_mv", "Wood Steps" },
								{ "piece_floorbeamh_3m_mv", "Floor Beam" },
								{ "piece_stonefoundationa_mv", "Foundation Stone" },
								{ "piece_stonefoundationb_mv", "Foundation Stone" },
								{ "piece_basebeam_3m_mv", "Ground Beam" },
								{ "piece_beamv_3m_mv", "Vertical Beam" },
								{ "piece_roof45_3m_mv", "Tiled Roof 45" },
								{ "piece_roofbeamhend_3m_mv", "Roof Beam" },
								{ "piece_roofwallcenter_3m_mv", "Apex Wall Center" },
								{ "piece_roofwallleft_3m_mv", "Apex Wall Left" },
								{ "piece_roofwallright_3m_mv", "Apex Wall Right" },
								{ "piece_walkway3x1_mv", "Walkway 3x1.5" },
								{ "piece_walkway3x2_mv", "Walkway 3x2" },
								{ "piece_wallcorner3x3_mv", "Wall Corner" },
								{ "piece_wallcross3x3_mv", "Wall Cross" },
								{ "piece_walldoor3x3_mv", "Doorway" },
								{ "piece_wallplain3x3_mv", "Wall Plain" },
								{ "piece_wallstone3x3_mv", "Wall Stone" },
								{ "piece_wallwindow3x3_mv", "Wall Window" },
								{ "piece_wallwood3x3_mv", "Wall Wood" },
								{ "piece_woodwallhalf_mv", "Wall Wood Half" },
								{ "piece_wallwoodt3x3_mv", "Wall Wood Thick" },
								{ "piece_wallwoodwindow3x3_mv", "Wall Wood Window" },
								{ "piece_wallzbar3x3_mv", "Wall ZBar" },
								{ "piece_woodfloor3x2_mv", "Floor 3x2" },
								{ "piece_woodfloor3x3_mv", "Floor 3x3" },
								{ "piece_wallpanelwindow3x3_mv", "Inner Window Panel" },
								{ "piece_wallpanelstone3x3_mv", "Inner Stone Panel" },
								{ "piece_wallpanel3x3_mv", "Inner Wall Panel" },
								{ "piece_stairssplit_mv", "Split Stairs" },
								{ "piece_stairs_mv", "Stairs" },
								{ "piece_beamvthick_3m_mv", "Thick Beam 3M" },
								{ "piece_beamvthick_6m_mv", "Thick Beam 6M" },
								{ "piece_beamhthick_3m_mv", "Thick Beam Horz 3M" }

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {


								{ "item_hammer_mv", "Stone Hammer" },
								{ "item_hammer_desc_mv", "Required for the Medieval Village build pieces." },
								{ "piece_stonewallwindow_mv", "Stone Wall Window" },
								{ "piece_stonewall_mv", "Stone Wall" },
								{ "piece_stonewallcurved_mv", "Stone Wall Curved" },
								{ "piece_stonewallhalf_mv", "Stone Wall Half" },
								{ "piece_stonewallquarter_mv", "Stone Wall Quarter" },
								{ "piece_stonesteps_mv", "Stone Steps" },
								{ "piece_stonestepsshort_mv", "Short Stone Steps" },
								{ "piece_woodsteps_mv", "Wood Steps" },
								{ "piece_floorbeamh_3m_mv", "Floor Beam" },
								{ "piece_stonefoundationa_mv", "Foundation Stone" },
								{ "piece_stonefoundationb_mv", "Foundation Stone" },
								{ "piece_basebeam_3m_mv", "Ground Beam" },
								{ "piece_beamv_3m_mv", "Vertical Beam" },
								{ "piece_roof45_3m_mv", "Tiled Roof 45" },
								{ "piece_roofbeamhend_3m_mv", "Roof Beam" },
								{ "piece_roofwallcenter_3m_mv", "Apex Wall Center" },
								{ "piece_roofwallleft_3m_mv", "Apex Wall Left" },
								{ "piece_roofwallright_3m_mv", "Apex Wall Right" },
								{ "piece_walkway3x1_mv", "Walkway 3x1.5" },
								{ "piece_walkway3x2_mv", "Walkway 3x2" },
								{ "piece_wallcorner3x3_mv", "Wall Corner" },
								{ "piece_wallcross3x3_mv", "Wall Cross" },
								{ "piece_walldoor3x3_mv", "Doorway" },
								{ "piece_wallplain3x3_mv", "Wall Plain" },
								{ "piece_wallstone3x3_mv", "Wall Stone" },
								{ "piece_wallwindow3x3_mv", "Wall Window" },
								{ "piece_wallwood3x3_mv", "Wall Wood" },
								{ "piece_woodwallhalf_mv", "Wall Wood Half" },
								{ "piece_wallwoodt3x3_mv", "Wall Wood Thick" },
								{ "piece_wallwoodwindow3x3_mv", "Wall Wood Window" },
								{ "piece_wallzbar3x3_mv", "Wall ZBar" },
								{ "piece_woodfloor3x2_mv", "Floor 3x2" },
								{ "piece_woodfloor3x3_mv", "Floor 3x3" },
								{ "piece_wallpanelwindow3x3_mv", "Inner Window Panel" },
								{ "piece_wallpanelstone3x3_mv", "Inner Stone Panel" },
								{ "piece_wallpanel3x3_mv", "Inner Wall Panel" },
								{ "piece_stairssplit_mv", "Split Stairs" },
								{ "piece_stairs_mv", "Stairs" },
								{ "piece_beamvthick_3m_mv", "Thick Beam 3M" },
								{ "piece_beamvthick_6m_mv", "Thick Beam 6M" },
								{ "piece_beamhthick_3m_mv", "Thick Beam Horz 3M" }

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
		public static class MVLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}

