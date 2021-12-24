using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using System;

namespace DoDMonsters
{
    [HarmonyPatch]
    public class Local
    {
        private static Localization lcl;
        public static Dictionary<string, string> t;
        private static Dictionary<string, string> english = new Dictionary<string, string>() {
								{ "spawner_firedraketower_dod", "Drake Tower" },
								{ "spawner_arena_dod", "Arena Spawner" },
								{ "piece_bitterstumpaltar_dod", "Bitterstump Altar" },
								{ "object_glowmetal_dod", "Glowing Metal" },
								{ "object_rock_dod", "Rock" },
								{ "prop_worldlevel_dod", "World Level" },
								{ "prop_worldlevel_description_dod", "Level 1: Day 15 - Level 2: Day 25 - Level 3: Day 50 - Level 4: Day 100 - Level 5: Day 200" },
								{ "prop_cavemushroom_dod", "Cave Mushroom" },
								{ "item_cavemushroom_dod", "Cave Mushroom" },
								{ "item_cavemushroom_description_dod", "These come from the underworld." },
								{ "lore_underworld_dod", "The Underworld is a vast subterranean realm inhabited strange and sinister creatures. It is a place where few humans go and from where even fewer return...." },
								{ "enemy_pin_underworld_dod", "Underworld" },
								{ "location_mysterycaveb_dod", "Icy Cavern" },
								{ "location_mysterycavet_dod", "Chilly Cavern" },
								{ "location_mysterycavem_dod", "Fiery Cavern" },
								{ "location_mysterycave_dod", "Entrance to the Underworld" },
								{ "piece_startstone_dod", "Welcome to Do or Die" },
								{ "lore_start_label_dod", "Do or Die Difficulty" },
								{ "lore_start_dod", "Scaling difficulty is always active. The longer you are in the world and the more bosses you kill, the harder the world will become. Bosses will level up one tier per world level, after you kill them the first time. Biome creatures level up once, after you kill the Vanilla boss in that biome. All creatures gain 5% Damage and Health every world level. Maximum Stars is increased to 10 plus 3 from sector level ups." },

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
								{"piece_vegvisir_dod", "Runestone"},
								{"prop_altar_bitterstump_dod", "Bitterstump Altar"},
								{"item_token_skull_dod", "Skull Token"},
								{"item_token_skull_description_dod", "Knarr the Trader likes these."},
								{"location_bitterstumpcave_dod", "Bitterstump Altar"},
								{"location_bitterstumpcave_text_dod", "Requires 5 Greydwarf Hearts"},
								{"enemy_pin_bitterstump_dod", "Bitterstump's Cave"},
								{"location_bitterstump_dod", "Bitterstump's Cave"},
								{"lore_bitterstump_dod", "Bitterstump - Unknown"},

								{"npc_skugga_dod", "Skugga"},
                                {"npc_einherjar_dod", "Einherjar"},

                                {"prop_bluemushroom_dod", "Blewit"},
                                {"prop_bluemushroom_description_dod", "Found in Mistlands"},
                                {"prop_purpmushroom_dod", "Blushing Bracket"},
                                {"prop_purpmushroom_description_dod", "Found in Mistlands"},
                                {"prop_walnuttree_dod", "Walnut"},
                                {"prop_appletree_dod", "Apple"},
                                {"prop_redcherries_dod", "Cherry"},
                                {"prop_banana_dod", "Banana"},
                                {"prop_bananatree_dod", "Banana Tree"},

                                {"prop_mistlandsoak_dod", "Mistlands Oak"},
                                {"prop_mistlandsbush_dod", "Bush"},
                                {"prop_oldoak_dod", "Old Oak"},
                                {"prop_poplar_dod", "Poplar"},
                                {"prop_willow_dod", "Willow"},
                                {"prop_northernpine_dod", "Northern Pine"},

                                {"location_mistlandscave_dod", "Mistlands Cave"},
                                {"location_mistlandstower_dod", "Shadow Tower"},
                                {"location_secretentrance_mistland_dod","Under Construction" },
                                {"location_castlearenain_dod", "Arena"},
                                {"location_castlearenaout_dod", "You left the Arena"},
                                {"location_castlearena_dod", "Castle Arena"},

								{"location_ramborecave_dod", "Ram'Bore Altar"},
								{"location_ramborecave_text_dod", "Requires 5 Boar Tusks"},
								{"enemy_pin_rambore_dod", "Ram'Bore's Cave"},
								{"location_rambore_dod", "Ram'Bore's Cave"},
								{"lore_rambore_dod", "Ram'Bore - Unknown"},

								{"piece_lorestone_dod", "Lorestone"},

								{"item_walnut_dod", "Walnuts"},
                                {"item_walnuts_description_dod", "Gatherd from a Walnut Tree in the Mistlands."},
                                {"item_apple_dod", "Apples"},
                                {"item_apple_description_dod", "Gatherd from an Apple Tree in the Mistlands."},
                                {"item_redberries_dod", "Cherries"},
                                {"item_redberries_description_dod", "Gatherd from a Cherry Tree in the Mistlands."},

                                {"item_shield_bgskull_dod", "Bhygshan's Broken Shield"},
                                {"item_shield_bgskull_description_dod", "Used to be Bhygshan's shield before you broke it while fighting him. Wonder if you can fix it."},
                                {"item_shield_byagluth_dod", "Broken Shield"},
                                {"item_shield_byagluth_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_bskir_dod", "Broken Shield"},
                                {"item_shield_bskir_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_bmoder_dod", "Broken Shield"},
                                {"item_shield_bmoder_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_bfarkas_dod", "Broken Shield"},
                                {"item_shield_bfarkas_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_bbonemass_dod", "Broken Shield"},
                                {"item_shield_bbonemass_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_belder_dod", "Broken Shield"},
                                {"item_shield_belder_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_bbitterstump_dod", "Broken Shield"},
                                {"item_shield_bbitterstump_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_beikthyr_dod", "Broken Shield"},
                                {"item_shield_beikthyr_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},
                                {"item_shield_brambone_dod", "Broken Shield"},
                                {"item_shield_brambone_description_dod", "Wonder if you can fix it. Feel free to offer suggestions for this item."},

                                {"item_shield_yagluth_dod", "Deathgate"},
                                {"item_shield_yagluth_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_skir_dod", "Curator Ward"},
                                {"item_shield_skir_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_moder_dod", "Perfect Storm"},
                                {"item_shield_moder_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_farkas_dod", "Frozen Light"},
                                {"item_shield_farkas_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_bonemass_dod", "Ghostly Wall"},
                                {"item_shield_bonemass_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_bhygshan_dod", "Vortex, Conservator of the Dead"},
                                {"item_shield_bhygshan_description_dod", "You repaired Bhygshans Shield, congratulations! Feel free to offer suggestions for this item."},
                                {"item_shield_elder_dod", "Ironbark"},
                                {"item_shield_elder_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_bitterstump_dod", "Darkheart"},
                                {"item_shield_bitterstump_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_eikthyr_dod", "Thundercloud"},
                                {"item_shield_eikthyr_description_dod", "Feel free to offer suggestions for this item."},
                                {"item_shield_rambone_dod", "Enforcer"},
                                {"item_shield_rambone_description_dod", "Feel free to offer suggestions for this item."},

                                {"item_mace_bhygshan_dod", "Defiled Warmace"},
                                {"item_mace_bhygshan_description_dod", "Defiled Warmace. Feel free to offer suggestions for this description."},
                                {"item_bow_blackforest_dod", "Hornet"},
                                {"item_bow_blackforest_description_dod", "Hornet. Feel free to offer suggestions for this description."},
                                {"item_bow_swamp_dod", "Sting, Serpent of Regret"},
                                {"item_bow_swamp_description_dod", "Sting, Serpent of Regret. Feel free to offer suggestions for this description."},
                                {"item_bow_mountain_dod", "Razorwind, Ferocity of Redemption"},
                                {"item_bow_mountain_description_dod", "Razorwind, Ferocity of Redemption. Feel free to offer suggestions for this description."},
                                {"item_bow_plains_dod", "Stryker, Guardian of the Sun"},
                                {"item_bow_plains_description_dod", "Stryker, Guardian of the Sun. Feel free to offer suggestions for this description."},
                                {"item_bow_mistlands_dod", "Soulstring, Memory of the Damned"},
                                {"item_bow_mistlands_description_dod", "Soulstring, Memory of the Damned. Feel free to offer suggestions for this description."},
                                {"item_bow_deepnorth_dod", "Siren's Song, Vengeance of the Incoming Storm"},
                                {"item_bow_deepnorth_description_dod", "Siren's Song, Vengeance of the Incoming Storm. Feel free to offer suggestions for this description."},
                                {"item_sword_meadows_dod", "Betrayer, Ferocity of Hate"},
                                {"item_sword_meadows_description_dod", "Betrayer, Ferocity of Hate. Feel free to offer suggestions for this description."},
                                {"item_sword_swamp_dod", "Ghost Reaver, Crusader of the Plague"},
                                {"item_sword_swamp_description_dod", "Ghost Reaver, Crusader of the Plague. Feel free to offer suggestions for this description."},
                                {"item_sword_plains_dod", "Light's Bane, Blade of Justice"},
                                {"item_sword_plains_description_dod", "Light's Bane, Blade of Justice. Feel free to offer suggestions for this description."},
                                {"item_sword_mistlands_dod", "Misery, Call of Denial"},
                                {"item_sword_mistlands_description_dod", "Misery, Call of Denial. Feel free to offer suggestions for this description."},
                                {"item_sword_deepnorth_dod", "Fate, Frostblade of Frozen Hells"},
                                {"item_sword_deepnorth_description_dod", "Fate, Frostblade of Frozen Hells. Feel free to offer suggestions for this description."},
                                {"item_sword_ashlands_dod", "Hellfire, Spine of Twisted Visions"},
                                {"item_sword_ashlands_description_dod", "Hellfire, Spine of Twisted Visions. Feel free to offer suggestions for this description."},
                                {"item_mountainwand_dod", "Snowfall, Instrument of the Champion"},
                                {"item_mountainwand_description_dod", "Snowfall, Instrument of the Champion. Feel free to offer suggestions for this description."},
                                {"item_mace_deepnorth_dod", "Silence, Edge of the World"},
                                {"item_mace_deepnorth_description_dod", "Silence, Edge of the World. Feel free to offer suggestions for this description."},
                                {"item_mace_mistlands_dod", "Abomination, Memory of Giants"},
                                {"item_mace_mistlands_description_dod", "Abomination, Memory of Giants. Feel free to offer suggestions for this description."},

                                {"piece_forge_ext1_dod", "Felmetal Anvils"},
                                {"piece_forge_ext2_dod", "Frometal Anvils"},
                                {"piece_forge_ext3_dod", "Flametal Anvils"},

                                {"item_crudearmorkit_dod", "Crude Armor Kit"},
                                {"item_crudearmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_basicarmorkit_dod", "Basic Armor Kit"},
                                {"item_basicarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_goodarmorkit_dod", "Good Armor Kit"},
                                {"item_goodarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_greatarmorkit_dod", "Great Armor Kit"},
                                {"item_greatarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_superiorarmorkit_dod", "Superior Armor Kit"},
                                {"item_superiorarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_excellentarmorkit_dod", "Excellent Armor Kit"},
                                {"item_excellentarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_exceptionalarmorkit_dod", "Exceptional Armor Kit"},
                                {"item_exceptionalarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_extraordinaryarmorkit_dod", "Extraordinary Armor Kit"},
                                {"item_extraordinaryarmorkit_description_dod", "Used for crafting Armor or can be sold to the Trader. Can be bought from the Trader or Crafted."},

                                {"item_crudeweaponkit_dod", "Crude Weapon Kit"},
                                {"item_crudeweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_basicweaponkit_dod", "Basic Weapon Kit"},
                                {"item_basicweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_goodweaponkit_dod", "Good Weapon Kit"},
                                {"item_goodweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_greatweaponkit_dod", "Great Weapon Kit"},
                                {"item_greatweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_superiorweaponkit_dod", "Superior Weapon Kit"},
                                {"item_superiorweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_excellentweaponkit_dod", "Excellent Weapon Kit"},
                                {"item_excellentweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_exceptionalweaponkit_dod", "Exceptional Weapon Kit"},
                                {"item_exceptionalweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},
                                {"item_extraordinaryweaponkit_dod", "Extraordinary Weapon Kit"},
                                {"item_extraordinaryweaponkit_description_dod", "Used for crafting Weapons or can be sold to the Trader. Can be bought from the Trader or Crafted."},

                                {"item_oakwood_dod", "Hardwood"},
                                {"item_oakwood_description_dod", "Hardwood is used in crafting"},

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

								{"location_ramborecave_dod", "Ram'Bore Altar"},
								{"location_ramborecave_text_dod", "Requires 5 Boar Tusks"},
								{"enemy_pin_rambore_dod", "Ram'Bore's Cave"},
								{"location_rambore_dod", "Ram'Bore's Cave"},
								{"lore_rambore_dod", "Ram'Bore - Unknown"},

								{"piece_lorestone_dod", "Lorestone"},

								{"npc_skugga_dod", "Скугга"	},
								{"npc_einherjar_dod","Эйнхерьяр" },							
								{"prop_bluemushroom_dod","Блюит" },
								{"prop_bluemushroom_description_dod", "Найдено в Туманных Землях" },
								{"prop_purpmushroom_dod", "Кронштейн для покраски" },
								{"prop_purpmushroom_description_dod", "Найдено в Туманных Землях" },
								{"prop_walnuttree_dod",	"Грецкий орех" },
								{"prop_appletree_dod",	"Яблоко" },
								{"prop_redcherries_dod", "Вишня" },
								{"prop_banana_dod", "Банан" },
								{"prop_bananatree_dod","Банановое дерево"},
								{"prop_mistlandsoak_dod","Туманные земли Дуб"},
								{"prop_mistlandsbush_dod","Куст"},
								{"prop_oldoak_dod","Старый дуб"},
								{"prop_poplar_dod","Тополь"},
								{"prop_willow_dod","Ива"},
								{
									"location_mistlandscave_dod",
									"Туманная пещера"
								},
								{
									"location_mistlandstower_dod",
									"Башня теней"
								},
								{
									"location_secretentrance_mistland_dod",
									"Строится"
								},
								{
									"$location_castlearenain_dod",
									"Арена"
								},
								{
									"$location_castlearenaout_dod",
									"Вы покинули Арену"
								},
								{
									"location_castlearena_dod",
									"Замковая арена"
								},
								{
									"item_walnut_dod",
									"Грецкие орехи"
								},
								{
									"item_walnuts_description_dod",
									"Собранный с орехового дерева в Туманных Землях."
								},
								{
									"item_apple_dod",
									"Яблоки"
								},
								{
									"item_apple_description_dod",
									"Собранный с яблони в Туманных землях."
								},
								{
									"item_redberries_dod",
									"Вишни"
								},
								{
									"item_redberries_description_dod",
									"Собран с вишневого дерева в Туманных землях."
								},
								{
									"item_shield_bgskull_dod",
									"Сломанный щит Бхигшана"
								},
								{
									"item_shield_bgskull_description_dod",
									"Раньше это был щит Бхыгшана, пока вы не сломали его во время боя с ним. Интересно, сможешь ли ты его починить."
								},
								{
									"item_shield_byagluth_dod",
									"Сломанный щит"
								},
								{
									"item_shield_byagluth_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bskir_dod",
									"Сломанный щит"
								},
								{
									"item_shield_bskir_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bmoder_dod",
									"Сломанный щит"
								},
								{
									"item_shield_bmoder_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bfarkas_dod",
									"Сломанный щит"
								},
								{
									"item_shield_bfarkas_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bbonemass_dod",
									"Сломанный щит"
								},
								{
									"item_shield_bbonemass_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_belder_dod",
									"Сломанный щит"
								},
								{
									"item_shield_belder_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bbitterstump_dod",
									"Сломанный щит"
								},
								{
									"item_shield_bbitterstump_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_beikthyr_dod",
									"Сломанный щит"
								},
								{
									"item_shield_beikthyr_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_brambone_dod",
									"Сломанный щит"
								},
								{
									"item_shield_brambone_description_dod",
									"Интересно, сможете ли вы это исправить? Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_yagluth_dod",
									"Врата смерти"
								},
								{
									"item_shield_yagluth_description_dod",
									"Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_skir_dod",
									"Куратор Уорд"
								},
								{
									"item_shield_skir_description_dod",
									"Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_moder_dod",
									"Идеальный шторм"
								},
								{
									"item_shield_moder_description_dod",
									"Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_farkas_dod",
									"Застывший свет"
								},
								{
									"item_shield_farkas_description_dod",
									"Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bonemass_dod",
									"Призрачная стена"
								},
								{
									"item_shield_bonemass_description_dod",
									"Не стесняйтесь предлагать предложения по этому пункту."
								},
								{
									"item_shield_bhygshan_dod",
									"Вихрь, хранитель мертвых"
								},
								{
									"item_shield_bhygshan_description_dod",
									"Вы отремонтировали Щит Бхыгшана, поздравляем! Не стесняйтесь предлагать предложения по этому предмету."
								},
								{
									"item_shield_elder_dod",
									"Железная кора"
								},
								{
									"item_shield_elder_description_dod",
									"Не стесняйтесь предлагать свои предложения по этому пункту."
								},
								{
									"item_shield_bitterstump_dod",
									"Темное сердце"
								},
								{
									"item_shield_bitterstump_description_dod",
									"Не стесняйтесь предлагать свои предложения по этому пункту."
								},
								{
									"item_shield_eikthyr_dod",
									"Грозовое облако"
								},
								{
									"item_shield_eikthyr_description_dod",
									"Не стесняйтесь предлагать свои предложения по этому пункту."
								},
								{
									"item_shield_rambone_dod",
									"Правоохранитель"
								},
								{
									"item_shield_rambone_description_dod",
									"Не стесняйтесь предлагать свои предложения по этому пункту."
								},
								{
									"item_mace_bhygshan_dod",
									"Оскверненная грелка"
								},
								{
									"item_mace_bhygshan_description_dod",
									"Defiled Warmace. Не стесняйтесь предлагать свои предложения для этого описания."
								},
								{
									"item_bow_blackforest_dod",
									"Шершень"
								},
								{
									"item_bow_blackforest_description_dod",
									"Шершень. Не стесняйтесь предлагать свои предложения для этого описания."
								},
								{
									"item_bow_swamp_dod",
									"Стинг, Змея сожаления"
								},
								{
									"item_bow_swamp_description_dod",
									"Стинг, Змей сожаления. Не стесняйтесь предлагать свои предложения для этого описания."
								},
								{
									"item_bow_mountain_dod",
									"Ветер бритвы, Свирепость искупления"
								},
								{
									"item_bow_mountain_description_dod",
									"Ветер бритвы, свирепость искупления. Не стесняйтесь предлагать свои предложения для этого описания."
								},
								{
									"item_bow_plains_dod",
									"Страйкер, Хранитель Солнца"
								},
								{
									"item_bow_plains_description_dod",
									"Страйкер, Хранитель Солнца"
								},
								{
									"item_bow_mistlands_dod",
									"Струна души, Память проклятых"
								},
								{
									"item_bow_mistlands_description_dod",
									"Струна души, Память проклятых"
								},
								{
									"item_bow_deepnorth_dod",
									"Песнь сирены, Месть надвигающегося шторма"
								},
								{
									"item_bow_deepnorth_description_dod",
									"Песнь сирены, Месть надвигающегося шторма"
								},
								{
									"item_sword_meadows_dod",
									"Предатель, Свирепая ненависть"
								},
								{
									"item_sword_meadows_description_dod",
									"Предатель, Свирепая ненависть"
								},
								{
									"item_sword_swamp_dod",
									"Призрачный пожиратель, крестоносец чумы"
								},
								{
									"item_sword_swamp_description_dod",
									"Призрачный пожиратель, крестоносец чумы"
								},
								{
									"item_sword_plains_dod",
									"Бич Света, Клинок Правосудия"
								},
								{
									"item_sword_plains_description_dod",
									"Бич Света, Клинок Правосудия"
								},
								{
									"item_sword_mistlands_dod",
									"Страдание, вызов отрицания"
								},
								{
									"item_sword_mistlands_description_dod",
									"Страдание, вызов отрицания"
								},
								{
									"item_sword_deepnorth_dod",
									"Судьба, Ледяной Клинок Замерзшего Ада"
								},
								{
									"item_sword_deepnorth_description_dod",
									"Судьба, Ледяной Клинок Замерзшего Ада"
								},
								{
									"item_sword_ashlands_dod",
									"Адский огонь, Хребет Искаженных Видений"
								},
								{
									"item_sword_ashlands_description_dod",
									"Адский огонь, Хребет Искаженных Видений"
								},
								{
									"item_mountainwand_dod",
									"Снегопад, орудие чемпиона"
								},
								{
									"item_mountainwand_description_dod",
									"Снегопад, орудие чемпиона"
								},
								{
									"item_mace_deepnorth_dod",
									"Тишина, край света"
								},
								{
									"item_mace_deepnorth_description_dod",
									"Тишина, край света"
								},
								{
									"item_mace_mistlands_dod",
									"Мерзость, память о гигантах"
								},
								{
									"item_mace_mistlands_description_dod",
									"Мерзость, память о гигантах"
								},
								{
									"piece_forge_ext1_dod",
									"Наковальни из фелметалла"
								},
								{
									"piece_forge_ext2_dod",
									"Наковальни из фрометалла"
								},
								{
									"piece_forge_ext3_dod",
									"Наковальни из фламетал"
								},
								{
									"item_crudearmorkit_dod",
									"Комплект Грубой Брони"
								},
								{
									"item_crudearmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_basicarmorkit_dod",
									"Basic Armor Kit"
								},
								{
									"item_basicarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_goodarmorkit_dod",
									"Хороший Комплект Брони"
								},
								{
									"item_goodarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_greatarmorkit_dod",
									"Отличный Комплект Доспехов"
								},
								{
									"item_greatarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_superiorarmorkit_dod",
									"Улучшенный Комплект Брони"
								},
								{
									"item_superiorarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_excellentarmorkit_dod",
									"Отличный Комплект Брони"
								},
								{
									"item_excellentarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_exceptionalarmorkit_dod",
									"Исключительный Комплект Брони"
								},
								{
									"item_exceptionalarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_extraordinaryarmorkit_dod",
									"Необычный Комплект Брони"
								},
								{
									"item_extraordinaryarmorkit_description_dod",
									"Используется для изготовления Доспехов или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_crudeweaponkit_dod",
									"Грубый Набор Оружия"
								},
								{
									"item_crudeweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_basicweaponkit_dod",
									"Базовый Набор Оружия"
								},
								{
									"item_basicweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_goodweaponkit_dod",
									"Хороший Набор Оружия"
								},
								{
									"item_goodweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_greatweaponkit_dod",
									"Улучшенный Набор Оружия"
								},
								{
									"item_greatweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_superiorweaponkit_dod",
									"Превосходный Набор Оружия"
								},
								{
									"item_superiorweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_excellentweaponkit_dod",
									"Отличный Набор Оружия"
								},
								{
									"item_excellentweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_exceptionalweaponkit_dod",
									"Исключительный Набор Оружия"
								},
								{
									"item_exceptionalweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_extraordinaryweaponkit_dod",
									"Необычный Набор Оружия"
								},
								{
									"item_extraordinaryweaponkit_description_dod",
									"Используется для изготовления оружия или может быть продан Торговцу. Может быть куплен у Торговца или изготовлен вручную."
								},
								{
									"item_oakwood_dod",
									"Твердая древесина"
								},
								{
									"item_oakwood_description_dod",
									"Твердая древесина используется в ремесле"
								},
								{
									"item_trophy_icedrake",
									"Голова Ледяного Дрейка"
								},
								{
									"item_trophy_icedrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_flamedrake",
									"Голова Огненного Дрейка"
								},
								{
									"item_trophy_flamedrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_arcanedrake",
									"Голова Загадочного Дрейка"
								},
								{
									"item_trophy_arcanedrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_poisondrake",
									"Голова Ядовитого Дрейка"
								},
								{
									"item_trophy_poisondrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_darkdrake",
									"Голова Черного Дрейка"
								},
								{
									"item_trophy_darkdrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_darknessdrake",
									"Голова Темного Дрейка"
								},
								{
									"item_trophy_darknessdrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_golddrake",
									"Голова Золотого Дрейка"
								},
								{
									"item_trophy_golddrake_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_skeletonchar_dod",
									"Обугленный Череп"
								},
								{
									"item_trophy_skeletonchar_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_skeletonr_dod",
									"Хрупкий Череп"
								},
								{
									"item_trophy_skeletonr_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_skeletong_dod",
									"Бледный Череп"
								},
								{
									"item_trophy_skeletong_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_frozenbones_dod",
									"Замороженный Череп"
								},
								{
									"item_trophy_frozenbones_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_forestwolf_dod",
									"Голова Лесного Волка"
								},
								{
									"item_trophy_forestwolf_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_direwolf_dod",
									"Голова Страшного Волка"
								},
								{
									"item_trophy_direwolf_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_vilefang_dod",
									"Голова Мерзкого Клыка"
								},
								{
									"item_trophy_vilefang_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_frostling_dod",
									"Голова Обмороженного"
								},
								{
									"item_trophy_frostlingdescription",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_voidling_dod",
									"Голова Пустого"
								},
								{
									"item_trophy_voidling_description",
									"Трофей, повесь его у себя на стене."
								},
								{
									"item_trophy_stormling_dod",
									"Голова Штормового"
								},
								{"item_trophy_stormling_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_livinglava_dod","Живая Лава"},
								{"item_trophy_livinglava_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_blackdeer_dod","Голова Черного Оленя"},
								{"item_trophy_blackdeer_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_obgolem_dod","Голова Обсидианового Голема"},
								{"item_trophy_obgolem__description","Трофей, повесь его у себя на стене."},
								{"item_trophy_lavagolem_dod","Голова Лавового Голема"},
								{"item_trophy_lavagolem_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_icegolem_dod","Голова Ледяного Голема"},
								{"item_trophy_icegolem_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_livingwater_dod","Живая Вода"},
								{"item_trophy_livingwater_description","Трофей, повесь его у себя на стене."},
								{"item_trophy_gsurtling_dod","Голова Великого Суртлинга"},
								{"item_trophy_gsurtling_description","Трофей, повесь его у себя на стене."}
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
        public static class MyLocalizationPatch
        {
            public static void Postfix(Localization __instance, string language)
            {
                init(language, __instance);
                UpdateDictinary();
            }
        }
    }
}
