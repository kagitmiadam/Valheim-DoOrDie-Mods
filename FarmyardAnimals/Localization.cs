using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FarmyardAnimals
{
	[HarmonyPatch]
	public class FYALocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {
			// Pieces
			{"piece_chickencoop_baa", "Chicken Coop"},
			{"piece_chickencoop_desc_baa", "Produces 5 Egg's every 12hrs (Real Time)"},
			{"piece_marl_fya", "Marl"},
			{"piece_thon_fya", "Thon"},
			{"piece_milkgoat_fya", "Milk Goat"},
			{"piece_goatstall_fya", "Goat Stall"},
			{"piece_goatstall_fya_desc", "Produces 8 Milk every 12 hours (real time)"},
			{"piece_milkcow_fya", "Milk Cow"},
			{"piece_cowstall_fya", "Cow Stall"},
			{"piece_cowstall_fya_desc", "Produces 8 Milk every 6 hours (real time)"},
			{"piece_butcherbench_fya", "Butcher Bench"},
			// Carcass
			{"object_carcass_fya", "Carcass"},
			{"item_legsm_fya", "Piece of a Carcass"},
			{"item_legsm_fya_desc", "Chop it up at the butcher station"},
			{"item_piecesm_fya", "Piece of a Carcass"},
			{"item_piecesm_fya_desc", "Chop it up at the butcher station"},
			{"item_quatersm_fya", "Piece of a Carcass"},
			{"item_quatersm_fya_desc", "Chop it up at the butcher station"},
			// Materials
			{"item_chicken_baa", "A Chicken"},
			{"item_chicken_desc_baa", "Required to build the Chicken Coop"},
			{"item_goat_fya", "A Goat"},
			{"item_goat_fya_desc", "Required to build the Goat Stall"},
			{"item_cow_fya", "A Cow"},
			{"item_cow_fya_desc", "Required to build the Cow Stall"},
			{"item_steak_fya", "Steak"},
			{"item_steak_fya_desc", "Can be cooked in the Oven"},
			{"item_steaksmall_fya", "Small Steak"},
			{"item_steaksmall_fya_desc", "Can be cooked in the Oven"},
			{"item_chop_fya", "Chop"},
			{"item_chop_fya_desc", "Can be cooked in the Oven"},
			{"item_poultry_fya", "Poultry Carcass"},
			{"item_poultry_fya_desc", "Can be butchered it at the Butcher's Bench"},
			{"item_rawpoultry_fya", "Whole Poultry"},
			{"item_rawpoultry_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultryleg_fya", "Poultry Leg"},
			{"item_rawpoultryleg_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultrybreast_fya", "Poultry Breast"},
			{"item_rawpoultrybreast_fya_desc", "Can be cooked in the Oven"},
			{"item_meatroll_fya", "Meat Roll"},
			{"item_meatroll_fya_desc", "Can be cooked in the Oven"},
			{"item_meatchunks_fya", "Meat Chunks x4"},
			{"item_meatchunks3_fya", "Meat Chunks x3"},
			{"item_meatchunks16_fya", "Meat Chunks x16"},
			{"item_meatchunks_fya_desc", "Can be cooked in the Oven"},
			{"item_burgermeat_fya", "Burger Meat"},
			{"item_burgermeat_fya_desc", "Can be cooked in the Oven"},
			// Food
			{"item_joint_fya", "Roasted Joint"},
			{"item_joint_fya_desc", ""},
			{"item_milk_fya", "Milk"},
			{"item_milk_fya_desc", ""},
			{"item_steakfried_fya", "Fired Steak"},
			{"item_steakfried_fya_desc", ""},
			{"item_friedmeat_fya", "Fired Meat"},
			{"$item_friedmeat_fya_desc", ""},
			{"item_steakcooked_fya", "Cooked Steak"},
			{"item_steakcooked_fya_desc", ""},
			{"item_cookedpoultry_fya", "Roasted Poultry"},
			{"item_cookedpoultry_fya_desc", ""},
			{"item_cookedpoultryleg_fya", "Drumstick"},
			{"item_cookedpoultryleg_fya_desc", ""},
			{"item_cookedpoultrybreast_fya", "Cooked Breast"},
			{"item_cookedpoultrybreast_fya_desc", ""},
			{"item_cookedchop_fya", "Cooked Chop"},
			{"item_cookedchop_fya_desc", ""},
			{"item_burgerround_fya", "Cooked Burger"},
			{"item_burgerround_fya_desc", ""},
			// Animals
			{"animal_turkey_fya", "Turkey"},
			{"animal_turkeychick_fya", "Poult"},
			{"animal_pigglet_fya", "Pigglet"},
			{"animal_oldspot_fya", "Old Spots"},
			{"animal_mulefoot_fya", "Mulesfoot"},
			{"animal_okford_fya", "Oxford"},
			{"animal_chester_fya", "Chester"},
			{"animal_highland_fya", "Highland"},
			{"animal_longhorn_fya", "Longhorn"},
			{"animal_chicken_fya", "Chicken"},
			{"animal_chick_fya", "Chick"},
			{"animal_egg_fya", "Egg"},
			{"animal_cow_fya", "Cow"},
			{"animal_sheep_fya", "Sheep"},
			{"animal_lamb_fya", "Lamb"},
			{"animal_goat_fya", "Goat"},
			{"animal_gosling_fya", "Gosling"},
			{"animal_goose_fya", "Goose"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {
			// Pieces
			{"piece_milkgoat_fya", "Milk Goat"},
			{"piece_goatstall_fya", "Goat Stall"},
			{"piece_goatstall_fya_desc", "Produces 8 Milk every 12 hours (real time)"},
			{"piece_milkcow_fya", "Milk Cow"},
			{"piece_cowstall_fya", "Cow Stall"},
			{"piece_cowstall_fya_desc", "Produces 8 Milk every 6 hours (real time)"},
			{"piece_butcherbench_fya", "Butcher Bench"},
			// Carcass
			{"object_carcass_fya", "Carcass"},
			{"item_legsm_fya", "Piece of a Carcass"},
			{"item_legsm_fya_desc", "Chop it up at the butcher station"},
			{"item_piecesm_fya", "Piece of a Carcass"},
			{"item_piecesm_fya_desc", "Chop it up at the butcher station"},
			{"item_quatersm_fya", "Piece of a Carcass"},
			{"item_quatersm_fya_desc", "Chop it up at the butcher station"},
			// Materials
			{"item_goat_fya", "A Goat"},
			{"item_goat_fya_desc", "Required to build the Goat Stall."},
			{"item_cow_fya", "A Cow"},
			{"item_cow_fya_desc", "Required to build the Cow Stall."},
			{"item_steak_fya", "Steak"},
			{"item_steak_fya_desc", "Can be cooked in the Oven"},
			{"item_steaksmall_fya", "Small Steak"},
			{"item_steaksmall_fya_desc", "Can be cooked in the Oven"},
			{"item_chop_fya", "Chop"},
			{"item_chop_fya_desc", "Can be cooked in the Oven"},
			{"item_poultry_fya", "Poultry Carcass"},
			{"item_poultry_fya_desc", "Can be butchered it at the Butcher's Bench"},
			{"item_rawpoultry_fya", "Whole Poultry"},
			{"item_rawpoultry_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultryleg_fya", "Poultry Leg"},
			{"item_rawpoultryleg_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultrybreast_fya", "Poultry Breast"},
			{"item_rawpoultrybreast_fya_desc", "Can be cooked in the Oven"},
			{"item_meatroll_fya", "Meat Roll"},
			{"item_meatroll_fya_desc", "Can be cooked in the Oven"},
			{"item_meatchunks_fya", "Meat Chunks x4"},
			{"item_meatchunks3_fya", "Meat Chunks x3"},
			{"item_meatchunks16_fya", "Meat Chunks x16"},
			{"item_meatchunks_fya_desc", "Can be cooked in the Oven"},
			{"item_burgermeat_fya", "Burger Meat"},
			{"item_burgermeat_fya_desc", "Can be cooked in the Oven"},
			// Food
			{"item_joint_fya", "Roasted Joint"},
			{"item_joint_fya_desc", ""},
			{"item_milk_fya", "Milk"},
			{"item_milk_fya_desc", ""},
			{"item_steakfried_fya", "Fired Steak"},
			{"item_steakfried_fya_desc", ""},
			{"item_friedmeat_fya", "Fired Meat"},
			{"$item_friedmeat_fya_desc", ""},
			{"item_steakcooked_fya", "Cooked Steak"},
			{"item_steakcooked_fya_desc", ""},
			{"item_cookedpoultry_fya", "Roasted Poultry"},
			{"item_cookedpoultry_fya_desc", ""},
			{"item_cookedpoultryleg_fya", "Drumstick"},
			{"item_cookedpoultryleg_fya_desc", ""},
			{"item_cookedpoultrybreast_fya", "Cooked Breast"},
			{"item_cookedpoultrybreast_fya_desc", ""},
			{"item_cookedchop_fya", "Cooked Chop"},
			{"item_cookedchop_fya_desc", ""},
			{"item_burgerround_fya", "Cooked Burger"},
			{"item_burgerround_fya_desc", ""},
			// Animals
			{"animal_turkey_fya", "Turkey"},
			{"animal_turkeychick_fya", "Poult"},
			{"animal_pigglet_fya", "Pigglet"},
			{"animal_oldspots_fya", "Old Spots"},
			{"animal_mulefoot_fya", "Mulesfoot"},
			{"animal_okford_fya", "Oxford"},
			{"animal_chester_fya", "Chester"},
			{"animal_highland_fya", "Highland"},
			{"animal_longhorn_fya", "Longhorn"},
			{"animal_chicken_fya", "Chicken"},
			{"animal_chick_fya", "Chick"},
			{"animal_egg_fya", "Egg"},
			{"animal_cow_fya", "Cow"},
			{"animal_sheep_fya", "Sheep"},
			{"animal_lamb_fya", "Lamb"},
			{"animal_goat_fya", "Goat"},
			{"animal_gosling_fya", "Gosling"},
			{"animal_goose_fya", "Goose"}

			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {
			
			// Pieces
			{"piece_milkgoat_fya", "Milk Goat"},
			{"piece_goatstall_fya", "Goat Stall"},
			{"piece_goatstall_fya_desc", "Produces 8 Milk every 12 hours (real time)"},
			{"piece_milkcow_fya", "Milk Cow"},
			{"piece_cowstall_fya", "Cow Stall"},
			{"piece_cowstall_fya_desc", "Produces 8 Milk every 6 hours (real time)"},
			{"piece_butcherbench_fya", "Butcher Bench"},
			// Carcass
			{"object_carcass_fya", "Carcass"},
			{"item_legsm_fya", "Piece of a Carcass"},
			{"item_legsm_fya_desc", "Chop it up at the butcher station"},
			{"item_piecesm_fya", "Piece of a Carcass"},
			{"item_piecesm_fya_desc", "Chop it up at the butcher station"},
			{"item_quatersm_fya", "Piece of a Carcass"},
			{"item_quatersm_fya_desc", "Chop it up at the butcher station"},
			// Materials
			{"item_goat_fya", "A Goat"},
			{"item_goat_fya_desc", "Required to build the Goat Stall."},
			{"item_cow_fya", "A Cow"},
			{"item_cow_fya_desc", "Required to build the Cow Stall."},
			{"item_steak_fya", "Steak"},
			{"item_steak_fya_desc", "Can be cooked in the Oven"},
			{"item_steaksmall_fya", "Small Steak"},
			{"item_steaksmall_fya_desc", "Can be cooked in the Oven"},
			{"item_chop_fya", "Chop"},
			{"item_chop_fya_desc", "Can be cooked in the Oven"},
			{"item_poultry_fya", "Poultry Carcass"},
			{"item_poultry_fya_desc", "Can be butchered it at the Butcher's Bench"},
			{"item_rawpoultry_fya", "Whole Poultry"},
			{"item_rawpoultry_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultryleg_fya", "Poultry Leg"},
			{"item_rawpoultryleg_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultrybreast_fya", "Poultry Breast"},
			{"item_rawpoultrybreast_fya_desc", "Can be cooked in the Oven"},
			{"item_meatroll_fya", "Meat Roll"},
			{"item_meatroll_fya_desc", "Can be cooked in the Oven"},
			{"item_meatchunks_fya", "Meat Chunks x4"},
			{"item_meatchunks3_fya", "Meat Chunks x3"},
			{"item_meatchunks16_fya", "Meat Chunks x16"},
			{"item_meatchunks_fya_desc", "Can be cooked in the Oven"},
			{"item_burgermeat_fya", "Burger Meat"},
			{"item_burgermeat_fya_desc", "Can be cooked in the Oven"},
			// Food
			{"item_joint_fya", "Roasted Joint"},
			{"item_joint_fya_desc", ""},
			{"item_milk_fya", "Milk"},
			{"item_milk_fya_desc", ""},
			{"item_steakfried_fya", "Fired Steak"},
			{"item_steakfried_fya_desc", ""},
			{"item_friedmeat_fya", "Fired Meat"},
			{"$item_friedmeat_fya_desc", ""},
			{"item_steakcooked_fya", "Cooked Steak"},
			{"item_steakcooked_fya_desc", ""},
			{"item_cookedpoultry_fya", "Roasted Poultry"},
			{"item_cookedpoultry_fya_desc", ""},
			{"item_cookedpoultryleg_fya", "Drumstick"},
			{"item_cookedpoultryleg_fya_desc", ""},
			{"item_cookedpoultrybreast_fya", "Cooked Breast"},
			{"item_cookedpoultrybreast_fya_desc", ""},
			{"item_cookedchop_fya", "Cooked Chop"},
			{"item_cookedchop_fya_desc", ""},
			{"item_burgerround_fya", "Cooked Burger"},
			{"item_burgerround_fya_desc", ""},
			// Animals
			{"animal_turkey_fya", "Turkey"},
			{"animal_turkeychick_fya", "Poult"},
			{"animal_pigglet_fya", "Pigglet"},
			{"animal_oldspots_fya", "Old Spots"},
			{"animal_mulefoot_fya", "Mulesfoot"},
			{"animal_okford_fya", "Oxford"},
			{"animal_chester_fya", "Chester"},
			{"animal_highland_fya", "Highland"},
			{"animal_longhorn_fya", "Longhorn"},
			{"animal_chicken_fya", "Chicken"},
			{"animal_chick_fya", "Chick"},
			{"animal_egg_fya", "Egg"},
			{"animal_cow_fya", "Cow"},
			{"animal_sheep_fya", "Sheep"},
			{"animal_lamb_fya", "Lamb"},
			{"animal_goat_fya", "Goat"},
			{"animal_gosling_fya", "Gosling"},
			{"animal_goose_fya", "Goose"}
		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {
			
			// Pieces
			{"piece_milkgoat_fya", "Milk Goat"},
			{"piece_goatstall_fya", "Goat Stall"},
			{"piece_goatstall_fya_desc", "Produces 8 Milk every 12 hours (real time)"},
			{"piece_milkcow_fya", "Milk Cow"},
			{"piece_cowstall_fya", "Cow Stall"},
			{"piece_cowstall_fya_desc", "Produces 8 Milk every 6 hours (real time)"},
			{"piece_butcherbench_fya", "Butcher Bench"},
			// Carcass
			{"object_carcass_fya", "Carcass"},
			{"item_legsm_fya", "Piece of a Carcass"},
			{"item_legsm_fya_desc", "Chop it up at the butcher station"},
			{"item_piecesm_fya", "Piece of a Carcass"},
			{"item_piecesm_fya_desc", "Chop it up at the butcher station"},
			{"item_quatersm_fya", "Piece of a Carcass"},
			{"item_quatersm_fya_desc", "Chop it up at the butcher station"},
			// Materials
			{"item_goat_fya", "A Goat"},
			{"item_goat_fya_desc", "Required to build the Goat Stall."},
			{"item_cow_fya", "A Cow"},
			{"item_cow_fya_desc", "Required to build the Cow Stall."},
			{"item_steak_fya", "Steak"},
			{"item_steak_fya_desc", "Can be cooked in the Oven"},
			{"item_steaksmall_fya", "Small Steak"},
			{"item_steaksmall_fya_desc", "Can be cooked in the Oven"},
			{"item_chop_fya", "Chop"},
			{"item_chop_fya_desc", "Can be cooked in the Oven"},
			{"item_poultry_fya", "Poultry Carcass"},
			{"item_poultry_fya_desc", "Can be butchered it at the Butcher's Bench"},
			{"item_rawpoultry_fya", "Whole Poultry"},
			{"item_rawpoultry_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultryleg_fya", "Poultry Leg"},
			{"item_rawpoultryleg_fya_desc", "Can be cooked in the Oven"},
			{"item_rawpoultrybreast_fya", "Poultry Breast"},
			{"item_rawpoultrybreast_fya_desc", "Can be cooked in the Oven"},
			{"item_meatroll_fya", "Meat Roll"},
			{"item_meatroll_fya_desc", "Can be cooked in the Oven"},
			{"item_meatchunks_fya", "Meat Chunks x4"},
			{"item_meatchunks3_fya", "Meat Chunks x3"},
			{"item_meatchunks16_fya", "Meat Chunks x16"},
			{"item_meatchunks_fya_desc", "Can be cooked in the Oven"},
			{"item_burgermeat_fya", "Burger Meat"},
			{"item_burgermeat_fya_desc", "Can be cooked in the Oven"},
			// Food
			{"item_joint_fya", "Roasted Joint"},
			{"item_joint_fya_desc", ""},
			{"item_milk_fya", "Milk"},
			{"item_milk_fya_desc", ""},
			{"item_steakfried_fya", "Fired Steak"},
			{"item_steakfried_fya_desc", ""},
			{"item_friedmeat_fya", "Fired Meat"},
			{"$item_friedmeat_fya_desc", ""},
			{"item_steakcooked_fya", "Cooked Steak"},
			{"item_steakcooked_fya_desc", ""},
			{"item_cookedpoultry_fya", "Roasted Poultry"},
			{"item_cookedpoultry_fya_desc", ""},
			{"item_cookedpoultryleg_fya", "Drumstick"},
			{"item_cookedpoultryleg_fya_desc", ""},
			{"item_cookedpoultrybreast_fya", "Cooked Breast"},
			{"item_cookedpoultrybreast_fya_desc", ""},
			{"item_cookedchop_fya", "Cooked Chop"},
			{"item_cookedchop_fya_desc", ""},
			{"item_burgerround_fya", "Cooked Burger"},
			{"item_burgerround_fya_desc", ""},
			// Animals
			{"animal_turkey_fya", "Turkey"},
			{"animal_turkeychick_fya", "Poult"},
			{"animal_pigglet_fya", "Pigglet"},
			{"animal_oldspots_fya", "Old Spots"},
			{"animal_mulefoot_fya", "Mulesfoot"},
			{"animal_okford_fya", "Oxford"},
			{"animal_chester_fya", "Chester"},
			{"animal_highland_fya", "Highland"},
			{"animal_longhorn_fya", "Longhorn"},
			{"animal_chicken_fya", "Chicken"},
			{"animal_chick_fya", "Chick"},
			{"animal_egg_fya", "Egg"},
			{"animal_cow_fya", "Cow"},
			{"animal_sheep_fya", "Sheep"},
			{"animal_lamb_fya", "Lamb"},
			{"animal_goat_fya", "Goat"},
			{"animal_gosling_fya", "Gosling"},
			{"animal_goose_fya", "Goose"}

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
		public static class FYALocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
