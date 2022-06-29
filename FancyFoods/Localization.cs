using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FancyFoods
{
	[HarmonyPatch]
	public class FFLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"item_cabbagebeefstew_ff", "Cabbage and Beef Stew"},
			{"item_cabbagebeefstew_desc_ff", "Tasty Stew."},
			{"item_gamestew_ff", "Game Stew"},
			{"item_gamestew_desc_ff", "Tasty Stew."},
			{"item_lambstew_ff", "Lamb Stew"},
			{"item_lambstew_desc_ff", "Tasty Stew."},
			{"item_porkpearstew_ff", "Pork and Pear Stew"},
			{"item_porkpearstew_desc_ff", "Tasty Stew."},
			{"item_porkpepperstew_ff", "Pork and Pepper Stew"},
			{"item_porkpepperstew_desc_ff", "Tasty Stew."},
			{"item_beefstew_ff", "Beef Stew"},
			{"item_beefstew_desc_ff", "Tasty Stew."},

			{"item_vegetablebroth_ff", "Vegetable Broth"},
			{"item_vegetablebroth_desc_ff", "Used in cooking"},
			{"item_whippedcream_ff", "Whipped Cream"},
			{"item_whippedcream_desc_ff", "Used in cooking"},
			{"item_cornflour_ff", "Cornflour"},
			{"item_cornflour_desc_ff", "Used in cooking"},

			{"item_pumpkinsoup_ff", "Pumpkin and Squash Soup"},
			{"item_pumpkinsoup_desc_ff", "Tasty soup."},
			{"item_broccolisoup_ff", "Broccoli Soup"},
			{"item_broccolisoup_desc_ff", "Tasty soup."},
			{"item_mushroomsoup_ff", "Mushroom Soup"},
			{"item_mushroomsoup_desc_ff", "Tasty soup."},
			{"item_potatosoup_ff", "Potato Soup"},
			{"item_potatosoup_desc_ff", "Tasty soup."},
			{"item_bellpeppersoup_ff", "Bell Pepper Soup"},
			{"item_bellpeppersoup_desc_ff", "Tasty soup."},
			{"item_sweetpotatosoup_ff", "Sweet Potato Soup"},
			{"item_sweetpotatosoup_desc_ff", "Tasty soup."},

			{"item_cakechocolatecherry_ff", "Chocolate Cherry Cake"},
			{"item_cakechocolatecherry_desc_ff", "A large cake."},

			{"item_biscuit_ff", "Biscuit"},
			{"item_biscuit_desc_ff", "A small sweet biscuit."},
			{"item_chocolatebiscuit_ff", "Chocolate Biscuit"},
			{"item_chocolatebiscuit_desc_ff", "A small sweet biscuit."},
			{"item_biscuitchocolatecream_ff", "Chocolate Cream Biscuit"},
			{"item_biscuitchocolatecream_desc_ff", "A small sweet biscuit."},
			{"item_biscuitorange_ff", "Orange Biscuit"},
			{"item_biscuitorange_desc_ff", "A small sweet biscuit."},
			{"item_biscuitraspberry_ff", "Raspberry Biscuit"},
			{"item_biscuitraspberry_desc_ff", "A small sweet biscuit."},

			{"item_dessertchocolate_ff", "Chocolate Dessert"},
			{"item_dessertchocolate_desc_ff", "A small sweet dessert."},
			{"item_dessertchocolatepear_ff", "Chocolate Pear Dessert"},
			{"item_dessertchocolatepear_desc_ff", "A small sweet dessert."},
			{"item_dessertcreampear_ff", "Pear Dessert"},
			{"item_dessertcreampear_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberry_ff", "Raspberry Dessert"},
			{"item_dessertraspberry_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberrycream_ff", "Raspberry Cream Dessert"},
			{"item_dessertraspberrycream_desc_ff", "A small sweet dessert."},

			{"item_cupcakechocolate_ff", "Chocolate Cupcake"},
			{"item_cupcakechocolate_desc_ff", "A small sweet cake."},
			{"item_cupcakecream_ff", "Cream Cupcake"},
			{"item_cupcakecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeorange_ff", "Orange Cupcake"},
			{"item_cupcakeorange_desc_ff", "A small sweet cake."},
			{"item_cupcakeorangecream_ff", "Orange Cream Cupcake"},
			{"item_cupcakeorangecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeplain_ff", "Plain Cupcake"},
			{"item_cupcakeplain_desc_ff", "A small sweet cake."},

			{"item_vatrushkaplain_ff", "Vatrushka"},
			{"item_vatrushkaplain_desc_ff", "A small sweet cake."},
			{"item_vatrushkalime_ff", "Lime Vatrushka"},
			{"item_vatrushkalime_desc_ff", "A small sweet cake."},
			{"item_vatrushkaorange_ff", "Orange Vatrushka"},
			{"item_vatrushkaorange_desc_ff", "A small sweet cake."},
			{"item_vatrushkaplum_ff", "Plum Vatrushka"},
			{"item_vatrushkaplum_desc_ff", "A small sweet cake."},
			{"item_vatrushkaraspberry_ff", "Raspberry Vatrushka"},
			{"item_vatrushkaraspberry_desc_ff", "A small sweet cake."},

			{"item_cakebase_ff", "Plain Cake"},
			{"item_cakebase_desc_ff", "Used in baking recipies"},
			{"item_cupcakebase_ff", "Cup Cake Base"},
			{"item_cupcakebase_desc_ff", "Used in baking recipies"},
			{"item_dessertbase_ff", "Dessert Base"},
			{"item_dessertbase_desc_ff", "Used in baking recipies"},
			{"item_vatrushkabase_ff", "Vatrushka Base"},
			{"item_vatrushkabase_desc_ff", "Used in baking recipies"},
			{"item_biscuitbase_ff", "Biscuit Base"},
			{"item_biscuitbase_desc_ff", "Used in baking recipies"},

			{"item_applejam_ff", "Apple and Raspberry Compote"},
			{"item_applejam_desc_ff", "Used in baking recipies"},
			{"item_limejam_ff", "Lemon and Lime Compote"},
			{"item_limejam_desc_ff", "Used in baking recipies"},
			{"item_orangejam_ff", "Orange and Mango Compote"},
			{"item_orangejam_desc_ff", "Used in baking recipies"},
			{"item_plumjam_ff", "Plum and Grape Compote"},
			{"item_plumjam_desc_ff", "Used in baking recipies"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_cabbagebeefstew_ff", "Cabbage and Beef Stew"},
			{"item_cabbagebeefstew_desc_ff", "Tasty Stew."},
			{"item_gamestew_ff", "Game Stew"},
			{"item_gamestew_desc_ff", "Tasty Stew."},
			{"item_lambstew_ff", "Lamb Stew"},
			{"item_lambstew_desc_ff", "Tasty Stew."},
			{"item_porkpearstew_ff", "Pork and Pear Stew"},
			{"item_porkpearstew_desc_ff", "Tasty Stew."},
			{"item_porkpepperstew_ff", "Pork and Pepper Stew"},
			{"item_porkpepperstew_desc_ff", "Tasty Stew."},
			{"item_beefstew_ff", "Beef Stew"},
			{"item_beefstew_desc_ff", "Tasty Stew."},

			{"item_vegetablebroth_ff", "Vegetable Broth"},
			{"item_vegetablebroth_desc_ff", "Used in cooking"},

			{"item_pumpkinsoup_ff", "Pumpkin and Squash Soup"},
			{"item_pumpkinsoup_desc_ff", "Tasty soup."},
			{"item_broccolisoup_ff", "Broccoli Soup"},
			{"item_broccolisoup_desc_ff", "Tasty soup."},
			{"item_mushroomsoup_ff", "Mushroom Soup"},
			{"item_mushroomsoup_desc_ff", "Tasty soup."},
			{"item_potatosoup_ff", "Potato Soup"},
			{"item_potatosoup_desc_ff", "Tasty soup."},
			{"item_bellpeppersoup_ff", "Bell Pepper Soup"},
			{"item_bellpeppersoup_desc_ff", "Tasty soup."},
			{"item_sweetpotatosoup_ff", "Sweet Potato Soup"},
			{"item_sweetpotatosoup_desc_ff", "Tasty soup."},

			{"item_cakechocolatecherry_ff", "Chocolate Cherry Cake"},
			{"item_cakechocolatecherry_desc_ff", "A large cake."},

			{"item_cornflour_ff", "Cornflour"},
			{"item_cornflour_desc_ff", "Used in Baking"},

			{"item_biscuit_ff", "Biscuit"},
			{"item_biscuit_desc_ff", "A small sweet biscuit."},
			{"item_chocolatebiscuit_ff", "Chocolate Biscuit"},
			{"item_chocolatebiscuit_desc_ff", "A small sweet biscuit."},
			{"item_biscuitchocolatecream_ff", "Chocolate Cream Biscuit"},
			{"item_biscuitchocolatecream_desc_ff", "A small sweet biscuit."},
			{"item_biscuitorange_ff", "Orange Biscuit"},
			{"item_biscuitorange_desc_ff", "A small sweet biscuit."},
			{"item_biscuitraspberry_ff", "Raspberry Biscuit"},
			{"item_biscuitraspberry_desc_ff", "A small sweet biscuit."},

			{"item_dessertchocolate_ff", "Chocolate Dessert"},
			{"item_dessertchocolate_desc_ff", "A small sweet dessert."},
			{"item_dessertchocolatepear_ff", "Chocolate Pear Dessert"},
			{"item_dessertchocolatepear_desc_ff", "A small sweet dessert."},
			{"item_dessertcreampear_ff", "Pear Dessert"},
			{"item_dessertcreampear_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberry_ff", "Raspberry Dessert"},
			{"item_dessertraspberry_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberrycream_ff", "Raspberry Cream Dessert"},
			{"item_dessertraspberrycream_desc_ff", "A small sweet dessert."},

			{"item_cupcakechocolate_ff", "Chocolate Cupcake"},
			{"item_cupcakechocolate_desc_ff", "A small sweet cake."},
			{"item_cupcakecream_ff", "Cream Cupcake"},
			{"item_cupcakecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeorange_ff", "Orange Cupcake"},
			{"item_cupcakeorange_desc_ff", "A small sweet cake."},
			{"item_cupcakeorangecream_ff", "Orange Cream Cupcake"},
			{"item_cupcakeorangecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeplain_ff", "Plain Cupcake"},
			{"item_cupcakeplain_desc_ff", "A small sweet cake."},

			{"item_vatrushkaplain_ff", "Vatrushka"},
			{"item_vatrushkaplain_desc_ff", "A small sweet cake."},
			{"item_vatrushkalime_ff", "Lime Vatrushka"},
			{"item_vatrushkalime_desc_ff", "A small sweet cake."},
			{"item_vatrushkaorange_ff", "Orange Vatrushka"},
			{"item_vatrushkaorange_desc_ff", "A small sweet cake."},
			{"item_vatrushkaplum_ff", "Plum Vatrushka"},
			{"item_vatrushkaplum_desc_ff", "A small sweet cake."},
			{"item_vatrushkaraspberry_ff", "Raspberry Vatrushka"},
			{"item_vatrushkaraspberry_desc_ff", "A small sweet cake."},

			{"item_cakebase_ff", "Plain Cake"},
			{"item_cakebase_desc_ff", "Used in baking recipies"},
			{"item_cupcakebase_ff", "Cup Cake Base"},
			{"item_cupcakebase_desc_ff", "Used in baking recipies"},
			{"item_dessertbase_ff", "Dessert Base"},
			{"item_dessertbase_desc_ff", "Used in baking recipies"},
			{"item_vatrushkabase_ff", "Vatrushka Base"},
			{"item_vatrushkabase_desc_ff", "Used in baking recipies"},
			{"item_biscuitbase_ff", "Biscuit Base"},
			{"item_biscuitbase_desc_ff", "Used in baking recipies"},

			{"item_applejam_ff", "Apple and Raspberry Compote"},
			{"item_applejam_desc_ff", "Used in baking recipies"},
			{"item_limejam_ff", "Lemon and Lime Compote"},
			{"item_limejam_desc_ff", "Used in baking recipies"},
			{"item_orangejam_ff", "Orange and Mango Compote"},
			{"item_orangejam_desc_ff", "Used in baking recipies"},
			{"item_plumjam_ff", "Plum and Grape Compote"},
			{"item_plumjam_desc_ff", "Used in baking recipies"}

		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_cabbagebeefstew_ff", "Cabbage and Beef Stew"},
			{"item_cabbagebeefstew_desc_ff", "Tasty Stew."},
			{"item_gamestew_ff", "Game Stew"},
			{"item_gamestew_desc_ff", "Tasty Stew."},
			{"item_lambstew_ff", "Lamb Stew"},
			{"item_lambstew_desc_ff", "Tasty Stew."},
			{"item_porkpearstew_ff", "Pork and Pear Stew"},
			{"item_porkpearstew_desc_ff", "Tasty Stew."},
			{"item_porkpepperstew_ff", "Pork and Pepper Stew"},
			{"item_porkpepperstew_desc_ff", "Tasty Stew."},
			{"item_beefstew_ff", "Beef Stew"},
			{"item_beefstew_desc_ff", "Tasty Stew."},

			{"item_vegetablebroth_ff", "Vegetable Broth"},
			{"item_vegetablebroth_desc_ff", "Used in cooking"},

			{"item_pumpkinsoup_ff", "Pumpkin and Squash Soup"},
			{"item_pumpkinsoup_desc_ff", "Tasty soup."},
			{"item_broccolisoup_ff", "Broccoli Soup"},
			{"item_broccolisoup_desc_ff", "Tasty soup."},
			{"item_mushroomsoup_ff", "Mushroom Soup"},
			{"item_mushroomsoup_desc_ff", "Tasty soup."},
			{"item_potatosoup_ff", "Potato Soup"},
			{"item_potatosoup_desc_ff", "Tasty soup."},
			{"item_bellpeppersoup_ff", "Bell Pepper Soup"},
			{"item_bellpeppersoup_desc_ff", "Tasty soup."},
			{"item_sweetpotatosoup_ff", "Sweet Potato Soup"},
			{"item_sweetpotatosoup_desc_ff", "Tasty soup."},

			{"item_cakechocolatecherry_ff", "Chocolate Cherry Cake"},
			{"item_cakechocolatecherry_desc_ff", "A large cake."},

			{"item_cornflour_ff", "Cornflour"},
			{"item_cornflour_desc_ff", "Used in Baking"},

			{"item_biscuit_ff", "Biscuit"},
			{"item_biscuit_desc_ff", "A small sweet biscuit."},
			{"item_chocolatebiscuit_ff", "Chocolate Biscuit"},
			{"item_chocolatebiscuit_desc_ff", "A small sweet biscuit."},
			{"item_biscuitchocolatecream_ff", "Chocolate Cream Biscuit"},
			{"item_biscuitchocolatecream_desc_ff", "A small sweet biscuit."},
			{"item_biscuitorange_ff", "Orange Biscuit"},
			{"item_biscuitorange_desc_ff", "A small sweet biscuit."},
			{"item_biscuitraspberry_ff", "Raspberry Biscuit"},
			{"item_biscuitraspberry_desc_ff", "A small sweet biscuit."},

			{"item_dessertchocolate_ff", "Chocolate Dessert"},
			{"item_dessertchocolate_desc_ff", "A small sweet dessert."},
			{"item_dessertchocolatepear_ff", "Chocolate Pear Dessert"},
			{"item_dessertchocolatepear_desc_ff", "A small sweet dessert."},
			{"item_dessertcreampear_ff", "Pear Dessert"},
			{"item_dessertcreampear_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberry_ff", "Raspberry Dessert"},
			{"item_dessertraspberry_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberrycream_ff", "Raspberry Cream Dessert"},
			{"item_dessertraspberrycream_desc_ff", "A small sweet dessert."},

			{"item_cupcakechocolate_ff", "Chocolate Cupcake"},
			{"item_cupcakechocolate_desc_ff", "A small sweet cake."},
			{"item_cupcakecream_ff", "Cream Cupcake"},
			{"item_cupcakecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeorange_ff", "Orange Cupcake"},
			{"item_cupcakeorange_desc_ff", "A small sweet cake."},
			{"item_cupcakeorangecream_ff", "Orange Cream Cupcake"},
			{"item_cupcakeorangecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeplain_ff", "Plain Cupcake"},
			{"item_cupcakeplain_desc_ff", "A small sweet cake."},

			{"item_vatrushkaplain_ff", "Vatrushka"},
			{"item_vatrushkaplain_desc_ff", "A small sweet cake."},
			{"item_vatrushkalime_ff", "Lime Vatrushka"},
			{"item_vatrushkalime_desc_ff", "A small sweet cake."},
			{"item_vatrushkaorange_ff", "Orange Vatrushka"},
			{"item_vatrushkaorange_desc_ff", "A small sweet cake."},
			{"item_vatrushkaplum_ff", "Plum Vatrushka"},
			{"item_vatrushkaplum_desc_ff", "A small sweet cake."},
			{"item_vatrushkaraspberry_ff", "Raspberry Vatrushka"},
			{"item_vatrushkaraspberry_desc_ff", "A small sweet cake."},

			{"item_cakebase_ff", "Plain Cake"},
			{"item_cakebase_desc_ff", "Used in baking recipies"},
			{"item_cupcakebase_ff", "Cup Cake Base"},
			{"item_cupcakebase_desc_ff", "Used in baking recipies"},
			{"item_dessertbase_ff", "Dessert Base"},
			{"item_dessertbase_desc_ff", "Used in baking recipies"},
			{"item_vatrushkabase_ff", "Vatrushka Base"},
			{"item_vatrushkabase_desc_ff", "Used in baking recipies"},
			{"item_biscuitbase_ff", "Biscuit Base"},
			{"item_biscuitbase_desc_ff", "Used in baking recipies"},

			{"item_applejam_ff", "Apple and Raspberry Compote"},
			{"item_applejam_desc_ff", "Used in baking recipies"},
			{"item_limejam_ff", "Lemon and Lime Compote"},
			{"item_limejam_desc_ff", "Used in baking recipies"},
			{"item_orangejam_ff", "Orange and Mango Compote"},
			{"item_orangejam_desc_ff", "Used in baking recipies"},
			{"item_plumjam_ff", "Plum and Grape Compote"},
			{"item_plumjam_desc_ff", "Used in baking recipies"}

		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_cabbagebeefstew_ff", "Cabbage and Beef Stew"},
			{"item_cabbagebeefstew_desc_ff", "Tasty Stew."},
			{"item_gamestew_ff", "Game Stew"},
			{"item_gamestew_desc_ff", "Tasty Stew."},
			{"item_lambstew_ff", "Lamb Stew"},
			{"item_lambstew_desc_ff", "Tasty Stew."},
			{"item_porkpearstew_ff", "Pork and Pear Stew"},
			{"item_porkpearstew_desc_ff", "Tasty Stew."},
			{"item_porkpepperstew_ff", "Pork and Pepper Stew"},
			{"item_porkpepperstew_desc_ff", "Tasty Stew."},
			{"item_beefstew_ff", "Beef Stew"},
			{"item_beefstew_desc_ff", "Tasty Stew."},

			{"item_vegetablebroth_ff", "Vegetable Broth"},
			{"item_vegetablebroth_desc_ff", "Used in cooking"},

			{"item_pumpkinsoup_ff", "Pumpkin and Squash Soup"},
			{"item_pumpkinsoup_desc_ff", "Tasty soup."},
			{"item_broccolisoup_ff", "Broccoli Soup"},
			{"item_broccolisoup_desc_ff", "Tasty soup."},
			{"item_mushroomsoup_ff", "Mushroom Soup"},
			{"item_mushroomsoup_desc_ff", "Tasty soup."},
			{"item_potatosoup_ff", "Potato Soup"},
			{"item_potatosoup_desc_ff", "Tasty soup."},
			{"item_bellpeppersoup_ff", "Bell Pepper Soup"},
			{"item_bellpeppersoup_desc_ff", "Tasty soup."},
			{"item_sweetpotatosoup_ff", "Sweet Potato Soup"},
			{"item_sweetpotatosoup_desc_ff", "Tasty soup."},

			{"item_cakechocolatecherry_ff", "Chocolate Cherry Cake"},
			{"item_cakechocolatecherry_desc_ff", "A large cake."},

			{"item_cornflour_ff", "Cornflour"},
			{"item_cornflour_desc_ff", "Used in Baking"},

			{"item_biscuit_ff", "Biscuit"},
			{"item_biscuit_desc_ff", "A small sweet biscuit."},
			{"item_chocolatebiscuit_ff", "Chocolate Biscuit"},
			{"item_chocolatebiscuit_desc_ff", "A small sweet biscuit."},
			{"item_biscuitchocolatecream_ff", "Chocolate Cream Biscuit"},
			{"item_biscuitchocolatecream_desc_ff", "A small sweet biscuit."},
			{"item_biscuitorange_ff", "Orange Biscuit"},
			{"item_biscuitorange_desc_ff", "A small sweet biscuit."},
			{"item_biscuitraspberry_ff", "Raspberry Biscuit"},
			{"item_biscuitraspberry_desc_ff", "A small sweet biscuit."},

			{"item_dessertchocolate_ff", "Chocolate Dessert"},
			{"item_dessertchocolate_desc_ff", "A small sweet dessert."},
			{"item_dessertchocolatepear_ff", "Chocolate Pear Dessert"},
			{"item_dessertchocolatepear_desc_ff", "A small sweet dessert."},
			{"item_dessertcreampear_ff", "Pear Dessert"},
			{"item_dessertcreampear_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberry_ff", "Raspberry Dessert"},
			{"item_dessertraspberry_desc_ff", "A small sweet dessert."},
			{"item_dessertraspberrycream_ff", "Raspberry Cream Dessert"},
			{"item_dessertraspberrycream_desc_ff", "A small sweet dessert."},

			{"item_cupcakechocolate_ff", "Chocolate Cupcake"},
			{"item_cupcakechocolate_desc_ff", "A small sweet cake."},
			{"item_cupcakecream_ff", "Cream Cupcake"},
			{"item_cupcakecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeorange_ff", "Orange Cupcake"},
			{"item_cupcakeorange_desc_ff", "A small sweet cake."},
			{"item_cupcakeorangecream_ff", "Orange Cream Cupcake"},
			{"item_cupcakeorangecream_desc_ff", "A small sweet cake."},
			{"item_cupcakeplain_ff", "Plain Cupcake"},
			{"item_cupcakeplain_desc_ff", "A small sweet cake."},

			{"item_vatrushkaplain_ff", "Vatrushka"},
			{"item_vatrushkaplain_desc_ff", "A small sweet cake."},
			{"item_vatrushkalime_ff", "Lime Vatrushka"},
			{"item_vatrushkalime_desc_ff", "A small sweet cake."},
			{"item_vatrushkaorange_ff", "Orange Vatrushka"},
			{"item_vatrushkaorange_desc_ff", "A small sweet cake."},
			{"item_vatrushkaplum_ff", "Plum Vatrushka"},
			{"item_vatrushkaplum_desc_ff", "A small sweet cake."},
			{"item_vatrushkaraspberry_ff", "Raspberry Vatrushka"},
			{"item_vatrushkaraspberry_desc_ff", "A small sweet cake."},

			{"item_cakebase_ff", "Plain Cake"},
			{"item_cakebase_desc_ff", "Used in baking recipies"},
			{"item_cupcakebase_ff", "Cup Cake Base"},
			{"item_cupcakebase_desc_ff", "Used in baking recipies"},
			{"item_dessertbase_ff", "Dessert Base"},
			{"item_dessertbase_desc_ff", "Used in baking recipies"},
			{"item_vatrushkabase_ff", "Vatrushka Base"},
			{"item_vatrushkabase_desc_ff", "Used in baking recipies"},
			{"item_biscuitbase_ff", "Biscuit Base"},
			{"item_biscuitbase_desc_ff", "Used in baking recipies"},

			{"item_applejam_ff", "Apple and Raspberry Compote"},
			{"item_applejam_desc_ff", "Used in baking recipies"},
			{"item_limejam_ff", "Lemon and Lime Compote"},
			{"item_limejam_desc_ff", "Used in baking recipies"},
			{"item_orangejam_ff", "Orange and Mango Compote"},
			{"item_orangejam_desc_ff", "Used in baking recipies"},
			{"item_plumjam_ff", "Plum and Grape Compote"},
			{"item_plumjam_desc_ff", "Used in baking recipies"}
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
		public static class FFLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
