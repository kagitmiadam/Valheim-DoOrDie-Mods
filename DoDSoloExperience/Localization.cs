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

                                { "lore_creatures_dod", "Do or Die: Creatures" },
                                { "lore_creatures_description_dod", "Most of the new Monsters are in the 3 empty biomes, Skugga are everywhere. You will find some random new monsters in the populated biomes depending on what bosses you have killed." },
                                { "lore_materials_dod", "Do or Die: Items" },
                                { "lore_materials_description_dod", "You can find Class Essences and Infused Gemstone's in Chests, on Bosses and with the Trader. You will require Armor or Weapon Kit's to make most of the items added by Do or Die." },
                                { "lore_worldlevel_dod", "Do or Die: World Level" },
                                { "lore_worldlevel_description_dod", "Level 1: Day 15 - Level 2: Day 25 - Level 3: Day 50 - Level 4: Day 100 - Level 5: Day 200" },
                                { "piece_startstone_dod", "Do or Die Compendium" },
                                { "lore_start_label_dod", "Do or Die: Scaling Difficulty" },
                                { "lore_start_dod", "Scaling difficulty is always active. The longer you are in the world and the more bosses you kill, the harder the world will become. Bosses will level up one tier per world level, after you kill them the first time. Biome creatures level up once, after you kill the Vanilla boss in that biome. All creatures gain 5% Damage and Health every world level. Maximum Stars is increased to 10 plus 3 from sector level ups." },

        };
        private static Dictionary<string, string> russian = new Dictionary<string, string>() {

                                { "lore_creatures_dod", "Do or Die: Creatures" },
                                { "lore_creatures_description_dod", "Most of the new Monsters are in the 3 empty biomes, Skugga are everywhere. You will find some random new monsters in the populated biomes depending on what bosses you have killed." },
                                { "lore_materials_dod", "Do or Die: Items" },
                                { "lore_materials_description_dod", "You can find Essences in Chests, on Bosses and with the Trader." },
                                { "lore_worldlevel_dod", "Do or Die: World Level" },
                                { "lore_worldlevel_description_dod", "Level 1: Day 15 - Level 2: Day 25 - Level 3: Day 50 - Level 4: Day 100 - Level 5: Day 200" },
                                { "piece_startstone_dod", "Do or Die Compendium" },
                                { "lore_start_label_dod", "Do or Die: Scaling Difficulty" },
                                { "lore_start_dod", "Scaling difficulty is always active. The longer you are in the world and the more bosses you kill, the harder the world will become. Bosses will level up one tier per world level, after you kill them the first time. Biome creatures level up once, after you kill the Vanilla boss in that biome. All creatures gain 5% Damage and Health every world level. Maximum Stars is increased to 10 plus 3 from sector level ups." },

        };
        private static Dictionary<string, string> german = new Dictionary<string, string>() {

                                { "lore_creatures_dod", "Do or Die: Creatures" },
                                { "lore_creatures_description_dod", "Most of the new Monsters are in the 3 empty biomes, Skugga are everywhere. You will find some random new monsters in the populated biomes depending on what bosses you have killed." },
                                { "lore_materials_dod", "Do or Die: Items" },
                                { "lore_materials_description_dod", "You can find Essences in Chests, on Bosses and with the Trader." },
                                { "lore_worldlevel_dod", "Do or Die: World Level" },
                                { "lore_worldlevel_description_dod", "Level 1: Day 15 - Level 2: Day 25 - Level 3: Day 50 - Level 4: Day 100 - Level 5: Day 200" },
                                { "piece_startstone_dod", "Do or Die Compendium" },
                                { "lore_start_label_dod", "Do or Die: Scaling Difficulty" },
                                { "lore_start_dod", "Scaling difficulty is always active. The longer you are in the world and the more bosses you kill, the harder the world will become. Bosses will level up one tier per world level, after you kill them the first time. Biome creatures level up once, after you kill the Vanilla boss in that biome. All creatures gain 5% Damage and Health every world level. Maximum Stars is increased to 10 plus 3 from sector level ups." },

        };
        private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

                                { "lore_creatures_dod", "Do or Die: Yaratıklar" },
                                { "lore_creatures_description_dod", "Yeni eklenen canavarların çoğunu 3 boş biyomda, Skugga ise her biyomda bulabilirsiniz. Öldürdüğünüz BOSSlara bağlı olarak, nüfuslu biyomlarda rastgele doğan yeni canavarlar bulacaksınız." },
                                { "lore_materials_dod", "Do or Die: Öğeler" },
                                { "lore_materials_description_dod", "Özleri Sandıklarda, BOSSlarda ve Tüccarda bulabilirsiniz." },
                                { "lore_worldlevel_dod", "Do or Die: Dünya Seviyesi" },
                                { "lore_worldlevel_description_dod", "Seviye 1: Gün 15 - Seviye 2: Gün 25 - Seviye 3: Gün 50 - Seviye 4: Gün 100 - Seviye 5: Gün 200" },
                                { "piece_startstone_dod", "Do or Die Külliyatı" },
                                { "lore_start_label_dod", "Do or Die: Ölçekleme Zorluğu" },
                                { "lore_start_dod", "Ölçeklendirme zorluğu her zaman etkindir. Dünyada ne kadar uzun süre kalırsanız ve ne kadar çok BOSS öldürürseniz, dünya o kadar zorlaşır. BOSSlar, onları ilk kez öldürdükten sonra, dünya seviyesi başına bir seviye atlarlar. Biyom yaratıkları, o biyomdaki Vanilla BOSSu öldürdükten sonra bir kez seviye atlar. Tüm yaratıklar her dünya seviyesinde %5 Hasar ve Sağlık kazanırlar. Maksimum Yıldız, sektör seviyelerinden 10 artı 3'e çıkarılır." },

        };

        public static void init(string lang, Localization l)
        {
            lcl = l;
            if (lang == "Russian")
            {
                t = russian;
            }
            if (lang == "German")
            {
                t = german;
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
