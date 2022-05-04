using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace SupplyCrates
{
	[HarmonyPatch]
	public class SCLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			// Fruit
			{"item_apple_sc", "Apple"},
			{"item_apple_desc_sc", "One of your Five a Day!"},
			{"item_banana_sc", "Banana"},
			{"item_banana_desc_sc", "One of your Five a Day!"},
			{"item_grapes_sc", "Grapes"},
			{"item_grapes_desc_sc", "One of your Five a Day!"},
			{"item_lemon_sc", "Lemon"},
			{"item_lemon_desc_sc", "One of your Five a Day!"},
			{"item_mango_sc", "Mango"},
			{"item_mango_desc_sc", "One of your Five a Day!"},
			{"item_orange_sc", "Orange"},
			{"item_orange_desc_sc", "One of your Five a Day!"},
			{"item_peach_sc", "Peach"},
			{"item_peach_desc_sc", "One of your Five a Day!"},
			{"item_pear_sc", "Pear"},
			{"item_pear_desc_sc", "One of your Five a Day!"},
			{"item_plum_sc", "Plum"},
			{"item_plum_desc_sc", "One of your Five a Day!"},
			{"item_watermelon_sc", "Watermelon"},
			{"item_watermelon_desc_sc", "One of your Five a Day!"},
			// Veg
			{"item_bellpepper_sc", "Bell Pepper"},
			{"item_bellpepper_desc_sc", "One of your Five a Day!"},
			{"item_broccoli_sc", "Broccoli"},
			{"item_broccoli_desc_sc", "One of your Five a Day!"},
			{"item_cabbage_sc", "Cabbage"},
			{"item_cabbage_desc_sc", "One of your Five a Day!"},
			{"item_corn_sc", "Corn"},
			{"item_corn_desc_sc", "One of your Five a Day!"},
			{"item_cucumber_sc", "Cucumber"},
			{"item_cucumber_desc_sc", "One of your Five a Day!"},
			{"item_lettuce_sc", "Lettuce"},
			{"item_lettuce_desc_sc", "One of your Five a Day!"},
			{"item_potato_sc", "Potato"},
			{"item_potato_desc_sc", "One of your Five a Day!"},
			{"item_pumpkin_sc", "Pumpkin"},
			{"item_pumpkin_desc_sc", "One of your Five a Day!"},
			{"item_springonion_sc", "Spring Onion"},
			{"item_springonion_desc_sc", "One of your Five a Day!"},
			{"item_squash_sc", "Squash"},
			{"item_squash_desc_sc", "One of your Five a Day!"},
			{"item_sweetpotato_sc", "Sweet Potato"},
			{"item_sweetpotato_desc_sc", "One of your Five a Day!"},
			{"item_tomato_sc", "Tomato"},
			{"item_tomatoc_desc_sc", "One of your Five a Day!"},
			// Dairy
			{"item_bluecheese_sc", "Blue Cheese"},
			{"item_bluecheese_desc_sc", "This smells like old socks!"},
			{"item_edamcheese_sc", "Edam Cheese"},
			{"item_edamcheese_desc_sc", "Not so smelly cheese."},
			// Breads
			{"item_bagel_sc", "Bagel"},
			{"item_bagel_desc_sc", "A sweet bread."},
			{"item_bagette_sc", "Bagette"},
			{"item_bagette_desc_sc", "Crusty bread."},
			{"item_pretzel_sc", "Pretzel"},
			{"item_pretzel_desc_sc", "Salty bread."},
			// Crates
			{"pickable_boxoffruits_sc", "Provisions Crate"},
			{"pickable_boxofvegetables_sc", "Provisions Crate"},
			{"pickable_boxofdairy_sc", "Provisions Crate"},

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			// Fruit
			{"item_apple_sc", "Apple"},
			{"item_apple_desc_sc", "One of your Five a Day!"},
			{"item_banana_sc", "Banana"},
			{"item_banana_desc_sc", "One of your Five a Day!"},
			{"item_grapes_sc", "Grapes"},
			{"item_grapes_desc_sc", "One of your Five a Day!"},
			{"item_lemon_sc", "Lemon"},
			{"item_lemon_desc_sc", "One of your Five a Day!"},
			{"item_mango_sc", "Mango"},
			{"item_mango_desc_sc", "One of your Five a Day!"},
			{"item_orange_sc", "Orange"},
			{"item_orange_desc_sc", "One of your Five a Day!"},
			{"item_peach_sc", "Peach"},
			{"item_peach_desc_sc", "One of your Five a Day!"},
			{"item_pear_sc", "Pear"},
			{"item_pear_desc_sc", "One of your Five a Day!"},
			{"item_plum_sc", "Plum"},
			{"item_plum_desc_sc", "One of your Five a Day!"},
			{"item_watermelon_sc", "Watermelon"},
			{"item_watermelon_desc_sc", "One of your Five a Day!"},
			// Veg
			{"item_bellpepper_sc", "Bell Pepper"},
			{"item_bellpepper_desc_sc", "One of your Five a Day!"},
			{"item_broccoli_sc", "Broccoli"},
			{"item_broccoli_desc_sc", "One of your Five a Day!"},
			{"item_cabbage_sc", "Cabbage"},
			{"item_cabbage_desc_sc", "One of your Five a Day!"},
			{"item_corn_sc", "Corn"},
			{"item_corn_desc_sc", "One of your Five a Day!"},
			{"item_cucumber_sc", "Cucumber"},
			{"item_cucumber_desc_sc", "One of your Five a Day!"},
			{"item_lettuce_sc", "Lettuce"},
			{"item_lettuce_desc_sc", "One of your Five a Day!"},
			{"item_potato_sc", "Potato"},
			{"item_potato_desc_sc", "One of your Five a Day!"},
			{"item_pumpkin_sc", "Pumpkin"},
			{"item_pumpkin_desc_sc", "One of your Five a Day!"},
			{"item_springonion_sc", "Spring Onion"},
			{"item_springonion_desc_sc", "One of your Five a Day!"},
			{"item_squash_sc", "Squash"},
			{"item_squash_desc_sc", "One of your Five a Day!"},
			{"item_sweetpotato_sc", "Sweet Potato"},
			{"item_sweetpotato_desc_sc", "One of your Five a Day!"},
			{"item_tomato_sc", "Tomato"},
			{"item_tomatoc_desc_sc", "One of your Five a Day!"},
			// Dairy
			{"item_bluecheese_sc", "Blue Cheese"},
			{"item_bluecheese_desc_sc", "This smells like old socks!"},
			{"item_edamcheese_sc", "Edam Cheese"},
			{"item_edamcheese_desc_sc", "Not so smelly cheese."},
			// Breads
			{"item_bagel_sc", "Bagel"},
			{"item_bagel_desc_sc", "A sweet bread."},
			{"item_bagette_sc", "Bagette"},
			{"item_bagette_desc_sc", "Crusty bread."},
			{"item_pretzel_sc", "Pretzel"},
			{"item_pretzel_desc_sc", "Salty bread."},
			// Crates
			{"pickable_boxoffruits_sc", "Provisions Crate"},
			{"pickable_boxofvegetables_sc", "Provisions Crate"},
			{"pickable_boxofdairy_sc", "Provisions Crate"},

		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			// Fruit
			{"item_apple_sc", "Apple"},
			{"item_apple_desc_sc", "One of your Five a Day!"},
			{"item_banana_sc", "Banana"},
			{"item_banana_desc_sc", "One of your Five a Day!"},
			{"item_grapes_sc", "Grapes"},
			{"item_grapes_desc_sc", "One of your Five a Day!"},
			{"item_lemon_sc", "Lemon"},
			{"item_lemon_desc_sc", "One of your Five a Day!"},
			{"item_mango_sc", "Mango"},
			{"item_mango_desc_sc", "One of your Five a Day!"},
			{"item_orange_sc", "Orange"},
			{"item_orange_desc_sc", "One of your Five a Day!"},
			{"item_peach_sc", "Peach"},
			{"item_peach_desc_sc", "One of your Five a Day!"},
			{"item_pear_sc", "Pear"},
			{"item_pear_desc_sc", "One of your Five a Day!"},
			{"item_plum_sc", "Plum"},
			{"item_plum_desc_sc", "One of your Five a Day!"},
			{"item_watermelon_sc", "Watermelon"},
			{"item_watermelon_desc_sc", "One of your Five a Day!"},
			// Veg
			{"item_bellpepper_sc", "Bell Pepper"},
			{"item_bellpepper_desc_sc", "One of your Five a Day!"},
			{"item_broccoli_sc", "Broccoli"},
			{"item_broccoli_desc_sc", "One of your Five a Day!"},
			{"item_cabbage_sc", "Cabbage"},
			{"item_cabbage_desc_sc", "One of your Five a Day!"},
			{"item_corn_sc", "Corn"},
			{"item_corn_desc_sc", "One of your Five a Day!"},
			{"item_cucumber_sc", "Cucumber"},
			{"item_cucumber_desc_sc", "One of your Five a Day!"},
			{"item_lettuce_sc", "Lettuce"},
			{"item_lettuce_desc_sc", "One of your Five a Day!"},
			{"item_potato_sc", "Potato"},
			{"item_potato_desc_sc", "One of your Five a Day!"},
			{"item_pumpkin_sc", "Pumpkin"},
			{"item_pumpkin_desc_sc", "One of your Five a Day!"},
			{"item_springonion_sc", "Spring Onion"},
			{"item_springonion_desc_sc", "One of your Five a Day!"},
			{"item_squash_sc", "Squash"},
			{"item_squash_desc_sc", "One of your Five a Day!"},
			{"item_sweetpotato_sc", "Sweet Potato"},
			{"item_sweetpotato_desc_sc", "One of your Five a Day!"},
			{"item_tomato_sc", "Tomato"},
			{"item_tomatoc_desc_sc", "One of your Five a Day!"},
			// Dairy
			{"item_bluecheese_sc", "Blue Cheese"},
			{"item_bluecheese_desc_sc", "This smells like old socks!"},
			{"item_edamcheese_sc", "Edam Cheese"},
			{"item_edamcheese_desc_sc", "Not so smelly cheese."},
			// Breads
			{"item_bagel_sc", "Bagel"},
			{"item_bagel_desc_sc", "A sweet bread."},
			{"item_bagette_sc", "Bagette"},
			{"item_bagette_desc_sc", "Crusty bread."},
			{"item_pretzel_sc", "Pretzel"},
			{"item_pretzel_desc_sc", "Salty bread."},
			// Crates
			{"pickable_boxoffruits_sc", "Provisions Crate"},
			{"pickable_boxofvegetables_sc", "Provisions Crate"},
			{"pickable_boxofdairy_sc", "Provisions Crate"},

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			// Fruit
			{"item_apple_sc", "Apple"},
			{"item_apple_desc_sc", "One of your Five a Day!"},
			{"item_banana_sc", "Banana"},
			{"item_banana_desc_sc", "One of your Five a Day!"},
			{"item_grapes_sc", "Grapes"},
			{"item_grapes_desc_sc", "One of your Five a Day!"},
			{"item_lemon_sc", "Lemon"},
			{"item_lemon_desc_sc", "One of your Five a Day!"},
			{"item_mango_sc", "Mango"},
			{"item_mango_desc_sc", "One of your Five a Day!"},
			{"item_orange_sc", "Orange"},
			{"item_orange_desc_sc", "One of your Five a Day!"},
			{"item_peach_sc", "Peach"},
			{"item_peach_desc_sc", "One of your Five a Day!"},
			{"item_pear_sc", "Pear"},
			{"item_pear_desc_sc", "One of your Five a Day!"},
			{"item_plum_sc", "Plum"},
			{"item_plum_desc_sc", "One of your Five a Day!"},
			{"item_watermelon_sc", "Watermelon"},
			{"item_watermelon_desc_sc", "One of your Five a Day!"},
			// Veg
			{"item_bellpepper_sc", "Bell Pepper"},
			{"item_bellpepper_desc_sc", "One of your Five a Day!"},
			{"item_broccoli_sc", "Broccoli"},
			{"item_broccoli_desc_sc", "One of your Five a Day!"},
			{"item_cabbage_sc", "Cabbage"},
			{"item_cabbage_desc_sc", "One of your Five a Day!"},
			{"item_corn_sc", "Corn"},
			{"item_corn_desc_sc", "One of your Five a Day!"},
			{"item_cucumber_sc", "Cucumber"},
			{"item_cucumber_desc_sc", "One of your Five a Day!"},
			{"item_lettuce_sc", "Lettuce"},
			{"item_lettuce_desc_sc", "One of your Five a Day!"},
			{"item_potato_sc", "Potato"},
			{"item_potato_desc_sc", "One of your Five a Day!"},
			{"item_pumpkin_sc", "Pumpkin"},
			{"item_pumpkin_desc_sc", "One of your Five a Day!"},
			{"item_springonion_sc", "Spring Onion"},
			{"item_springonion_desc_sc", "One of your Five a Day!"},
			{"item_squash_sc", "Squash"},
			{"item_squash_desc_sc", "One of your Five a Day!"},
			{"item_sweetpotato_sc", "Sweet Potato"},
			{"item_sweetpotato_desc_sc", "One of your Five a Day!"},
			{"item_tomato_sc", "Tomato"},
			{"item_tomatoc_desc_sc", "One of your Five a Day!"},
			// Dairy
			{"item_bluecheese_sc", "Blue Cheese"},
			{"item_bluecheese_desc_sc", "This smells like old socks!"},
			{"item_edamcheese_sc", "Edam Cheese"},
			{"item_edamcheese_desc_sc", "Not so smelly cheese."},
			// Breads
			{"item_bagel_sc", "Bagel"},
			{"item_bagel_desc_sc", "A sweet bread."},
			{"item_bagette_sc", "Bagette"},
			{"item_bagette_desc_sc", "Crusty bread."},
			{"item_pretzel_sc", "Pretzel"},
			{"item_pretzel_desc_sc", "Salty bread."},
			// Crates
			{"pickable_boxoffruits_sc", "Provisions Crate"},
			{"pickable_boxofvegetables_sc", "Provisions Crate"},
			{"pickable_boxofdairy_sc", "Provisions Crate"},

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
		public static class SCLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
