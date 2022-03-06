using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace DoDMonsters
{
    [HarmonyPatch]
    public class DoDLocal
    {
        private static Localization lcl;
        public static Dictionary<string, string> t;
        private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"Bhygshan", "Bhygshan"},
			{"Bitterstump", "Bitterstump"},
			{"Farkas", "Farkas"},
			{"RamBore", "Ram'bore"},
			{"SkirSandburst", "Skir Sandburst"},

			{"PurpleDrake", "Purple Drake"},
			{"BlackDeer", "Black Deer"},
			{"BlackStag", "Black Stag"},
			{"CharredRemains", "Charred Remains"},
			{"BlackDrake", "Black Drake"},
			{"DarkDrake", "Dark Drake"},
			{"DireWolfCub", "Dire Wolf Cub"},
			{"DireWolf", "Dire Wolf"},
			{"FlameDrake", "Flame Drake"},
			{"ForestWolfCub", "Forest Wolf Cub"},
			{"ForestWolf", "Forest Wolf"},
			{"Frostling", "Frostling"},
			{"FrozenBones", "Frozen Bones"},
			{"GoldDrake", "Gold Drake"},
			{"GreaterSurtling", "Greater Surtling"},
			{"IceDrake", "Ice Drake"},
			{"IceGolem", "Ice Golem"},
			{"LavaGolem", "Lava Golem"},
			{"LivingLava", "Living Lava"},
			{"LivingWater", "Living Water"},
			{"ObsidianGolem", "Obsidian Golem"},
			{"GreenDrake", "Green Drake"},
			{"Stormling", "Stormling"},
			{"VilefangCub", "Vilefang Cub"},
			{"Vilefang", "Vilefang"},
			{"Voidling", "Voidling"},

			{"BhygshanAltar", "Bhygshan Altar"},
			{"BitterstumpAltar", "Bitterstump Altar"},
			{"FarkasAltar", "Farkas Altar"},
			{"RamboneAltar", "Ram'Bone Altar"},
			{"SkirSandburstAltar", "Skir Sandburst Altar"},

			{"BlackDeerHide", "Black Deer Hide"},
			{"BlackDeerHide_desc", "Hide from a Black Deer"},
			{"BoarTusk", "Boar Tusk"},
			{"BoarTusk_desc", "Boar Tusk's are required to summon Ram'Bore"},
			{"CharredBoneFragments", "Charred Bone Fragments"},
			{"CharredBoneFragments_desc", "Fragments of charred bone"},
			{"DireWolfHide", "Dire Wolf Hide"},
			{"DireWolfHide_desc", "Hide of a Dire Wolf"},
			{"ForestWolfHide", "Forest Wolf Hide"},
			{"ForestWolfHide_desc", "Hide of a Forest Wolf"},
			{"FrostlingCore", "Frostling Core"},
			{"FrostlingCore_desc", "Core from a Frostling Demon"},
			{"FrozenBoneFragment", "Frozen Bone Fragment"},
			{"FrozenBoneFragment_desc", "Fragments of frozen bone"},
			{"GreydwarfHeart", "Greydwarf Heart"},
			{"GreydwarfHeart_desc", "Greydwarf Heart's are required to summon Bitterstump"},
			{"GreyPearl", "Grey Pearl"},
			{"GreyPearl_desc", "Not so shinny pearl"},
			{"InfusedGemstone", "Infused Gemstone"},
			{"InfusedGemstone_desc", "You feel an energy within"},
			{"LargeFang", "Large Fang"},
			{"LargeFang_desc", "Large Fang's are required to summon Farkas"},
			{"RubyFoci", "Ruby Foci"},
			{"RubyFoci_desc", "Ruby Foci are required to summon Skir Sandburst"},
			{"SkeletonBones", "Skeleton Bones"},
			{"SkeletonBones_desc", "Skeleton Bones are required for summoning Bhygshan"},
			{"item_token_skull_dod", "Skull Token"},
			{"item_token_skull_description_dod", "Skull like object"},
			{"BlackChitin", "Black Chitin"},
			{"BlackChitin_desc", "Chitin obtained from a spider"},
			{"StormlingCore", "Stormling Core"},
			{"StormlingCore_desc", "Core from a Stormling Demon"},
			{"VoidlingCore", "Voidling Core"},
			{"VoidlingCore_desc", "Core from a Voidling Demon"},
			{"GlobeofWater", "Globe of Water"},
			{"GlobeofWater_desc", "A magical Globe of Water"},

			{"item_trophy_icedrake", "Ice Drake Head"},
            {"item_trophy_icedrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_flamedrake", "Flame Drake Head"},
            {"item_trophy_flamedrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_arcanedrake", "Arcane Drake Head"},
            {"item_trophy_arcanedrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_poisondrake", "Poison Drake Head"},
            {"item_trophy_poisondrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_darkdrake", "Dark Drake Head"},
            {"item_trophy_darkdrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_darknessdrake", "Darkness Drake Head"},
            {"item_trophy_darknessdrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_golddrake", "Gold Drake Head"},
            {"item_trophy_golddrake_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_skeletonchar_dod", "Charred Skull"},
            {"item_trophy_skeletonchar_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_skeletonr_dod", "Brittle Skull"},
            {"item_trophy_skeletonr_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_skeletong_dod", "Pale Skull"},
            {"item_trophy_skeletong_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_frozenbones_dod", "Frozen Skull"},
            {"item_trophy_frozenbones_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_forestwolf_dod", "Forest Wolf Head"},
            {"item_trophy_forestwolf_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_direwolf_dod", "Dire Wolf Head"},
            {"item_trophy_direwolf_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_vilefang_dod", "Vilefang Head"},
            {"item_trophy_vilefang_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_frostling_dod", "Frostling Head"},
            {"item_trophy_frostlingdescription", "A Trophy, hang it up on your wall"},
            {"item_trophy_voidling_dod", "Voidling Head"},
            {"item_trophy_voidling_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_stormling_dod", "Stormling Head"},
            {"item_trophy_stormling_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_livinglava_dod", "Living Lava"},
            {"item_trophy_livinglava_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_blackdeer_dod", "Black Deer Head"},
            {"item_trophy_blackdeer_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_obgolem_dod", "Obsidian Golem Head"},
            {"item_trophy_obgolem__description", "A Trophy, hang it up on your wall"},
            {"item_trophy_lavagolem_dod", "Lava Golem Head"},
            {"item_trophy_lavagolem_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_icegolem_dod", "Ice Golem Head"},
            {"item_trophy_icegolem_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_livingwater_dod", "Living Water"},
            {"item_trophy_livingwater_description", "A Trophy, hang it up on your wall"},
            {"item_trophy_gsurtling_dod", "Greater Surtling Head"},
            {"item_trophy_gsurtling_description", "A Trophy, hang it up on your wall"}
        };
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

			{"item_trophy_icedrake", "Голова Ледяного Дрейка"},
			{"item_trophy_icedrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_flamedrake", "Голова Огненного Дрейка"},
			{"item_trophy_flamedrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_arcanedrake", "Голова Загадочного Дрейка"},
			{"item_trophy_arcanedrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_poisondrake", "Голова Ядовитого Дрейка"},
			{"item_trophy_poisondrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_darkdrake", "Голова Черного Дрейка"},
			{"item_trophy_darkdrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_darknessdrake", "Голова Темного Дрейка"},
			{"item_trophy_darknessdrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_golddrake", "Голова Золотого Дрейка"},
			{"item_trophy_golddrake_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_skeletonchar_dod", "Обугленный Череп"},
			{"item_trophy_skeletonchar_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_skeletonr_dod", "Хрупкий Череп"},
			{"item_trophy_skeletonr_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_skeletong_dod", "Бледный Череп"},
			{"item_trophy_skeletong_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_frozenbones_dod", "Замороженный Череп"},
			{"item_trophy_frozenbones_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_forestwolf_dod", "Голова Лесного Волка"},
			{"item_trophy_forestwolf_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_direwolf_dod", "Голова Страшного Волка"},
			{"item_trophy_direwolf_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_vilefang_dod", "Голова Мерзкого Клыка"},
			{"item_trophy_vilefang_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_frostling_dod", "Голова Обмороженного"},
			{"item_trophy_frostlingdescription", "Трофей, повесь его у себя на стене."},
			{"item_trophy_voidling_dod", "Голова Пустого"},
			{"item_trophy_voidling_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_stormling_dod", "Голова Штормового"},
			{"item_trophy_stormling_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_livinglava_dod", "Живая Лава"},
			{"item_trophy_livinglava_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_blackdeer_dod", "Голова Черного Оленя"},
			{"item_trophy_blackdeer_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_obgolem_dod", "Голова Обсидианового Голема"},
			{"item_trophy_obgolem__description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_lavagolem_dod", "Голова Лавового Голема"},
			{"item_trophy_lavagolem_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_icegolem_dod", "Голова Ледяного Голема"},
			{"item_trophy_icegolem_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_livingwater_dod", "Живая Вода"},
			{"item_trophy_livingwater_description", "Трофей, повесь его у себя на стене."},
			{"item_trophy_gsurtling_dod", "Голова Великого Суртлинга"},
			{"item_trophy_gsurtling_description", "Трофей, повесь его у себя на стене."}
		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {

			{"item_trophy_icedrake", "Eisdrachenkopf"},
			{"item_trophy_icedrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_flamedrake", "Flammendrachenkopf"},
			{"item_trophy_flamedrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_arcanedrake", "Arkandrachenkopf"},
			{"item_trophy_arcanedrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_poisondrake", "Giftdrachenkopf"},
			{"item_trophy_poisondrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_darkdrake", "Dunkeldrachenkopf"},
			{"item_trophy_darkdrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_darknessdrake", "Finsterdrachenkopf"},
			{"item_trophy_darknessdrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_golddrake", "Golddrachenkopf"},
			{"item_trophy_golddrake_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_skeletonchar_dod", "Verkohlter Schädel"},
			{"item_trophy_skeletonchar_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_skeletonr_dod", "Spröder Schädel"},
			{"item_trophy_skeletonr_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_skeletong_dod", "Blasser Schädel"},
			{"item_trophy_skeletong_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_frozenbones_dod", "Gefrorener Schädel"},
			{"item_trophy_frozenbones_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_forestwolf_dod", "Waldwolfskopf"},
			{"item_trophy_forestwolf_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_direwolf_dod", "Düsterwolfkopf"},
			{"item_trophy_direwolf_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_vilefang_dod", "Reißzahnkopf"},
			{"item_trophy_vilefang_description", "Eine Trophäe, häng sie an deine Wand"},
			{"item_trophy_frostling_dod", "Frostling-Kopf"},
			{"item_trophy_frostlingdescription", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_voidling_dod", "Leerling-Kopf"},
			{"item_trophy_voidling_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_stormling_dod", "Sturmling-Kopf"},
			{"item_trophy_stormling_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_livinglava_dod", "Lebende Lava"},
			{"item_trophy_livinglava_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_blackdeer_dod", "Schwarzhirschkopf"},
			{"item_trophy_blackdeer_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_obgolem_dod", "Obsidiangolemkopf"},
			{"item_trophy_obgolem__description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_lavagolem_dod", "Lavagolemkopf"},
			{"item_trophy_lavagolem_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_icegolem_dod", "Eisgolemkopf"},
			{"item_trophy_icegolem_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_livingwater_dod", "Lebendiges Wasser"},
			{"item_trophy_livingwater_description", "Eine Trophäe, hänge sie an deine Wand"},
			{"item_trophy_gsurtling_dod", "Großer Surtlingkopf"},
			{"item_trophy_gsurtling_description", "Eine Trophäe, häng sie an deine Wand"}
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
        public static class DoDLocalizationPatch
        {
            public static void Postfix(Localization __instance, string language)
            {
                init(language, __instance);
                UpdateDictinary();
            }
        }
    }
}
