using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace DoDSoloExperience
{
    [HarmonyPatch]
    public class DoDSELocal
    {
        private static Localization lcl;
        public static Dictionary<string, string> t;
        private static Dictionary<string, string> english = new Dictionary<string, string>() {
								{ "prop_worldlevel_dod", "World Level" },
								{ "prop_worldlevel_description_dod", "Level 1: Day 15 - Level 2: Day 25 - Level 3: Day 50 - Level 4: Day 100 - Level 5: Day 200" },
								{ "piece_startstone_dod", "Welcome to Do or Die" },
								{ "lore_start_label_dod", "Do or Die Difficulty" },
								{ "lore_start_dod", "Scaling difficulty is always active. The longer you are in the world and the more bosses you kill, the harder the world will become. Bosses will level up one tier per world level, after you kill them the first time. Biome creatures level up once, after you kill the Vanilla boss in that biome. All creatures gain 5% Damage and Health every world level. Maximum Stars is increased to 10 plus 3 from sector level ups." },

        };
        private static Dictionary<string, string> russian = new Dictionary<string, string>() {

		};

        public static void init(string lang, Localization l)
        {
            lcl = l;
            if (lang == "Russian")
            {
                t = russian;
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
        public static class DoDSELocalizationPatch
        {
            public static void Postfix(Localization __instance, string language)
            {
                init(language, __instance);
                UpdateDictinary();
            }
        }
    }
}
