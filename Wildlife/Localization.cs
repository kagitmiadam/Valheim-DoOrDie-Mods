using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace WildlifeMobs
{
    [HarmonyPatch]
    public class WildlifeLocal
    {
        private static Localization lcl;
        public static Dictionary<string, string> t;
        private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"animal_sheep_dod", "Sheep"},
			{"animal_goat_dod", "Goat"},
			{"animal_goose_dod", "Goose"},
			{"animal_penguin_dod", "Penguin"},
			{"animal_salamander_dod", "Fire Salamander"},
			{"animal_turtle_dod", "Box Turtle"},
			{"animal_rat_dod", "Rat"},
			{"animal_greenlizard_dod", "Green Lizard"},
			{"animal_brownlizard_dod", "Brown Lizard"},
			{"animal_spottedlizard_dod", "Spotted Lizard"},
			{"animal_greenfrog_dod", "Green Frog"},
			{"animal_blackfrog_dod", "Black Frog"},
			{"animal_spottedfrog_dod", "Spotted Frog"},
			{"animal_greyrabbit_dod", "Grey Rabbit"},
			{"animal_brownrabbit_dod", "Brown Rabbit"},
			{"animal_giantsnail_dod", "Giant Snail"},
        };
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"animal_sheep_dod", "Овца"},
			{"animal_goat_dod", "Козел"},
			{"animal_goose_dod", "Гусь"},
			{"animal_penguin_dod", "Пингвин"},
			{"animal_salamander_dod", "Огненная саламандра"},
			{"animal_turtle_dod", "Коробчатая черепаха"},
			{"animal_rat_dod", "Крыса"},
			{"animal_greenlizard_dod", "Зеленая ящерица"},
			{"animal_brownlizard_dod", "Коричневая ящерица"},
			{"animal_spottedlizard_dod", "Пятнистая ящерица"},
			{"animal_greenfrog_dod", "Зеленая лягушка"},
			{"animal_blackfrog_dod", "Черная лягушка"},
			{"animal_spottedfrog_dod", "Пятнистая лягушка"},
			{"animal_greyrabbit_dod", "Серый кролик"},
			{"animal_brownrabbit_dod", "Коричневый кролик"},
			{"animal_giantsnail_dod", "Гигантская улитка"},

			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"animal_sheep_dod", "Schaf"},
			{"animal_goat_dod", "Ziege"},
			{"animal_goose_dod", "Gans"},
			{"animal_penguin_dod", "Pinguin"},
			{"animal_salamander_dod", "Feuersalamander"},
			{"animal_turtle_dod", "Dosenschildkröte"},
			{"animal_rat_dod", "Ratte"},
			{"animal_greenlizard_dod", "Grüne Eidechse"},
			{"animal_brownlizard_dod", "Braune Eidechse"},
			{"animal_spottedlizard_dod", "Gefleckte Eidechse"},
			{"animal_greenfrog_dod", "Grüner Frosch"},
			{"animal_blackfrog_dod", "Schwarzer Frosch"},
			{"animal_spottedfrog_dod", "Gefleckter Frosch"},
			{"animal_greyrabbit_dod", "Graues Kaninchen"},
			{"animal_brownrabbit_dod", "Braunes Kaninchen"},
			{"animal_giantsnail_dod", "Riesenschnecke"},

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
        public static class WildLifeLocalizationPatch
        {
            public static void Postfix(Localization __instance, string language)
            {
                init(language, __instance);
                UpdateDictinary();
            }
        }
    }
}
