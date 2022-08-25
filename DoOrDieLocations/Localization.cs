using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace DoOrDieBiomes
{
	[HarmonyPatch]
	public class DoDBLocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{ "spawner_firedraketower_dod", "Drake Tower" },
			{ "spawner_arena_dod", "Arena Spawner" },
			{ "object_rock_dod", "Rock" },
			{ "prop_cavemushroom_dod", "Cave Mushroom" },
			{ "item_cavemushroom_dod", "Cave Mushroom" },
			{ "item_cavemushroom_description_dod", "These come from the underworld." },
			{ "lore_underworld_dod", "The Underworld is a vast subterranean realm inhabited strange and sinister creatures. It is a place where few humans go and from where even fewer return...." },
			{ "enemy_pin_underworld_dod", "Underworld" },
			{ "location_mysterycaveb_dod", "Icy Cavern" },
			{ "location_mysterycavet_dod", "Chilly Cavern" },
			{ "location_mysterycavem_dod", "Fiery Cavern" },
			{ "location_mysterycave_dod", "Entrance to the Underworld" },

			{"prop_bluemushroom_dod", "Blewit"},
			{"prop_bluemushroom_description_dod", "Found in Mistlands"},
			{"prop_purpmushroom_dod", "Blushing Bracket"},
			{"prop_purpmushroom_description_dod", "Found in Mistlands"},

			{"item_bluemushroom_dod", "Blewit"},
			{"item_bluemushroom_description_dod", "Found in Mistlands"},
			{"item_purpmushroom_dod", "Blushing Bracket"},
			{"item_purpmushroom_description_dod", "Found in Mistlands"},

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

			{"item_oakwood_dod", "Hardwood"},
			{"item_oakwood_description_dod", "Hardwood is used in crafting"}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {

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

			{"lore_underworld_dod", "Подземный мир - это огромное подземное царство, населенное странными и зловещими существами. Это место, куда немногие люди уходят и откуда еще меньше возвращается ...."},
			{"enemy_pin_underworld_dod", "Подземный мир"},
			{"location_mysterycaveb_dod", "Ледяная пещера"},
			{"location_mysterycavet_dod", "Холодная пещера"},
			{"location_mysterycavem_dod", "Огненная пещера"},
			{"location_mysterycave_dod", "Вход в подземный мир"},
			{"piece_startstone_dod", "Добро пожаловать в Do or Die"},
			{"lore_start_label_dod", "Сложность в Do or Die"},
			{"lore_start_dod", "Сложность масштабирования всегда активна. Чем дольше вы находитесь в мире и чем больше боссов вы убиваете, тем сложнее станет мир. Боссы будут повышаться на один уровень за каждый уровень мира после того, как вы убьете их в первый раз. Существа биома повышаются на один уровень после того, как вы убиваете босса Ванили в этом биоме. Все существа получают 5% урона и здоровья на каждом из пяти мировых уровней. Максимальное количество звезд увеличивается до 10 плюс 3 при повышении уровня сектора »." },
			{"piece_vegvisir_dod", "Рунический камень"},
			{"prop_altar_bitterstump_dod", "Алтарь Горького Пня"},
			{"item_token_skull_dod", "Знак черепа"},
			{"item_token_skull_description_dod", "Они нравятся Торговцу Knarr."},
			{"location_bitterstumpcave_dod", "Алтарь Горького Пня"},
			{"location_bitterstumpcave_text_dod", "Требуется 5 серых сердец"},
			{"enemy_pin_bitterstump_dod", "Пещера Горького Пня"},
			{"location_bitterstump_dod", "Пещера Горького Пня"},
			{"lore_bitterstump_dod", "Горький Пень - Неизвестно"},
			{"prop_bluemushroom_dod","Блюит" },
			{"prop_bluemushroom_description_dod", "Найдено в Туманных Землях" },
			{"prop_purpmushroom_dod", "Кронштейн для покраски" },
			{"prop_purpmushroom_description_dod", "Найдено в Туманных Землях" },
			{"prop_walnuttree_dod", "Грецкий орех" },
			{"prop_appletree_dod",  "Яблоко" },
			{"prop_redcherries_dod", "Вишня" },
			{"prop_banana_dod", "Банан" },
			{"prop_bananatree_dod","Банановое дерево"},
			{"prop_mistlandsoak_dod","Туманные земли Дуб"},
			{"prop_mistlandsbush_dod","Куст"},
			{"prop_oldoak_dod","Старый дуб"},
			{"prop_poplar_dod","Тополь"},
			{"prop_willow_dod","Ива"},
			{"location_mistlandscave_dod","Туманная пещера"},
			{"location_mistlandstower_dod","Башня теней"},
			{"location_secretentrance_mistland_dod","Строится"},
			{"location_castlearenain_dod","Арена"},
			{"location_castlearenaout_dod","Вы покинули Арену"},
			{"location_castlearena_dod","Замковая арена"},
			{"location_ramborecave_dod", "Ram'Bore Altar"},
			{"location_ramborecave_text_dod", "Requires 5 Boar Tusks"},
			{"enemy_pin_rambore_dod", "Ram'Bore's Cave"},
			{"location_rambore_dod", "Ram'Bore's Cave"},
			{"lore_rambore_dod", "Ram'Bore - Unknown"},
			{"item_walnut_dod","Грецкие орехи"},
			{"item_walnuts_description_dod","Собранный с орехового дерева в Туманных Землях."},
			{"item_apple_dod","Яблоки"},
			{"item_apple_description_dod","Собранный с яблони в Туманных землях."},
			{"item_redberries_dod","Вишни"},
			{"item_redberries_description_dod","Собран с вишневого дерева в Туманных землях."},
		};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {
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

			{ "lore_underworld_dod", "Die Unterwelt ist ein riesiges unterirdisches Reich, das von seltsamen und unheimlichen Kreaturen bewohnt wird. Es ist ein Ort, an den nur wenige Menschen gehen und von dem noch weniger zurückkehren...." },
			{ "enemy_pin_underworld_dod", "Unterwelt" },
			{ "location_mysterycaveb_dod", "Eishöhle" },
			{ "location_mysterycavet_dod", "Kühle Höhle" },
			{ "location_mysterycavem_dod", "Feurige Höhle" },
			{ "location_mysterycave_dod", "Eingang zur Unterwelt" },
			{ "piece_startstone_dod", "Willkommen bei Do or Die" },
			{ "lore_start_label_dod", "Do or Die Schwierigkeit" },
			{ "lore_start_dod", "Skalierung der Schwierigkeit ist immer aktiv. Je länger du in der Welt bist und je mehr Bosse du tötest, desto schwieriger wird die Welt. Bosse steigen eine Stufe pro Weltlevel auf, nachdem du sie das erste Mal getötet hast. Biom-Kreaturen steigen einmal auf, nachdem du den Vanilla-Boss in diesem Biom getötet hast. Alle Kreaturen erhalten 5% Schaden und Gesundheit auf jeder der 5 Weltstufen. Die maximale Anzahl an Sternen wird auf 10 plus 3 von Sektorstufen-Aufstiegen erhöht." },

			{"piece_vegvisir_dod", "Runenstein"},
			{"prop_altar_bitterstump_dod", "Bitterstumpf-Altar"},
			{"item_token_skull_dod", "Schädel-Token"},
			{"item_token_skull_description_dod", "Knarr der Händler mag diese."},
			{"location_bitterstumpcave_dod", "Bitterstumpf-Altar"},
			{"location_bitterstumpcave_text_dod", "Erfordert 5 Grauzwergherzen"},
			{"enemy_pin_bitterstump_dod", "Bitterstumpfs Höhle"},
			{"location_bitterstump_dod", "Bitterstumpfs Höhle"},
			{"lore_bitterstump_dod", "Bitterstumpf - Unbekannt"},

			{"prop_bluemushroom_dod", "Platzer"},
			{"prop_bluemushroom_description_dod", "In Nebelland gefunden"},
			{"prop_purpmushroom_dod", "Schüchterner Armleuchter"},
			{"prop_purpmushroom_description_dod", "In Nebelland gefunden"},
			{"prop_walnuttree_dod", "Walnuss"},
			{"prop_appletree_dod", "Apfel"},
			{"prop_redcherries_dod", "Kische"},
			{"prop_banana_dod", "Banane"},
			{"prop_bananatree_dod", "Bananenbaum"},

			{"prop_mistlandsoak_dod", "Nebellandeiche"},
			{"prop_mistlandsbush_dod", "Busch"},
			{"prop_oldoak_dod", "Alte Eiche"},
			{"prop_poplar_dod", "Pappel"},
			{"prop_willow_dod", "Weide"},
			{"prop_northernpine_dod", "Nordkiefer"},

			{"location_mistlandscave_dod", "Mistlands-Höhle"},
			{"location_mistlandstower_dod", "Schattenturm"},
			{"location_secretentrance_mistland_dod", "Im Bau"},
			{"location_castlearenain_dod", "Arena"},
			{"location_castlearenaout_dod", "Du hast die Arena verlassen"},
			{"location_castlearena_dod", "Schlossarena"},

			{"location_ramborecave_dod", "Ram'Bore Altar"},
			{"location_ramborecave_text_dod", "Erfordert 5 Wildschweinhauer"},
			{"enemy_pin_rambore_dod", "Ram'Bores Höhle"},
			{"location_rambore_dod", "Ram'Bores Höhle"},
			{"lore_rambore_dod", "Ram'Bore - Unbekannt"},
		};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"enemy_pin_skirsandburst_dod", "Skir Sandburst"},
			{"enemy_pin_bhygshan_dod", "Bhygshan"},
			{"piece_skirsandburstaltar_dod", "Skir Sandburst Sunağı"},
			{"piece_skirsandburstaltar_text_dod", "5 Yakut Odağı gerekiyor."},

			{"piece_bhygshanaltar_dod", "Bhygshan Sunağı"},
			{"piece_bhygshanaltar_text_dod", "5 İskelet Kemiği gerekiyor."},
			{"piece_farkascave_dod", "Farkas Sunağı"},
			{"piece_farkascave_text_dod", "5 Büyük Diş gerekiyor."},

			{ "item_pickaxe_steel_dod", "Çelik Kazma" },
			{ "item_pickaxe_steel_description_dod", "Alevmetal Cevheri ve Ayazmetal Cevherini çıkarmak için gereklidir." },

			{ "spawner_firedraketower_dod", "Ejder Kulesi" },
			{ "spawner_arena_dod", "Arena Yumurtlayıcı" },
			{ "piece_bitterstumpaltar_dod", "Bitterstump Sunağı" },
			{ "object_glowmetal_dod", "Parlayan Metal" },
			{ "object_rock_dod", "Kaya" },
			{ "prop_worldlevel_dod", "Dünya Seviyesi" },
			{ "prop_worldlevel_description_dod", "Seviye 1: Gün 15 - Seviye 2: Gün 25 - Seviye 3: Gün 50 - Seviye 4: Gün 100 - Seviye 5: Gün 200" },
			{ "prop_cavemushroom_dod", "Mağara Mantarı" },
			{ "item_cavemushroom_dod", "Mağara Mantarı" },
			{ "item_cavemushroom_description_dod", "Bun mantarlar yeraltı dünyasından geliyorlar." },
			{ "lore_underworld_dod", "Yeraltı, garip ve uğursuz yaratıkların yaşadığı uçsuz bucaksız bir yeraltı krallığıdır. Çok az insanın gittiği ve daha da azının geri döndüğü bir yerdir..." },
			{ "enemy_pin_underworld_dod", "Yeraltı Dünyası" },
			{ "location_mysterycaveb_dod", "Buzul Mağara" },
			{ "location_mysterycavet_dod", "Soğuk Mağara" },
			{ "location_mysterycavem_dod", "Alevli Mağara" },
			{ "location_mysterycave_dod", "Yeraltı Dünyası Girişi" },
			{ "piece_startstone_dod", "Do or Die'ya Hoşgeldiniz" },
			{ "lore_start_label_dod", "Do or Die Zorluğu" },
			{ "lore_start_dod", "Ölçeklendirme zorluğu her zaman etkindir. Dünyada ne kadar uzun süre kalırsanız ve ne kadar çok BOSS öldürürseniz, dünya o kadar zorlaşır. BOSSlar, onları ilk kez öldürdükten sonra, dünya seviyesi başına bir seviye atlarlar. Biyom yaratıkları, o biyomdaki Vanilla BOSSu öldürdükten sonra bir kez seviye atlar. Tüm yaratıklar her dünya seviyesinde %5 Hasar ve Sağlık kazanırlar. Maksimum Yıldız, sektör seviyelerinden 10 artı 3'e çıkarılır." },

			{"piece_vegvisir_dod", "Rüntaşı"},
			{"prop_altar_bitterstump_dod", "Bitterstump Sunağı"},
			{"item_token_skull_dod", "Kafatası Simgesi"},
			{"item_token_skull_description_dod", "Tüccar Knarr bunları seviyor görünüyor."},
			{"location_bitterstumpcave_dod", "Bitterstump Sunağı"},
			{"location_bitterstumpcave_text_dod", "5 Bozcüce Kalbi gerekiyor."},
			{"enemy_pin_bitterstump_dod", "Bitterstump Mağarası"},
			{"location_bitterstump_dod", "Bitterstump' Mağarası"},
			{"lore_bitterstump_dod", "Bitterstump - Bilinmiyor"},

			{"prop_bluemushroom_dod", "Mavi Mantar"},
			{"prop_bluemushroom_description_dod", "Sisli Topraklar bölgesinde bulunanan bir çeşit mantar."},
			{"prop_purpmushroom_dod", "Mor Mantar"},
			{"prop_purpmushroom_description_dod", "Sisli Topraklar bölgesinde bulunanan bir çeşit mantar."},
			{"prop_walnuttree_dod", "Ceviz"},
			{"prop_appletree_dod", "Elma"},
			{"prop_redcherries_dod", "Kiraz"},
			{"item_banana_dod", "Muz"},
			{"item_banana_description_dod", "Ova bölgesinde bulunanan bir çeşit yiyecek."},
			{"prop_bananatree_dod", "Muz Ağacı"},

			{"prop_mistlandsoak_dod", "Sisli Topraklar Meşesi"},
			{"prop_mistlandsbush_dod", "Çalı"},
			{"prop_oldoak_dod", "Eski Meşe"},
			{"prop_poplar_dod", "Kavak"},
			{"prop_willow_dod", "Söğüt"},
			{"prop_northernpine_dod", "Kuzey Çamı"},

			{"location_mistlandscave_dod", "Sisli Topraklar Mağarası"},
			{"location_mistlandstower_dod", "Gölge Kulesi"},
			{"location_secretentrance_mistland_dod","Yapım Aşamasında" },
			{"location_castlearenain_dod", "Arena"},
			{"location_castlearenaout_dod", "Arenadan ayrıldınız"},
			{"location_castlearena_dod", "Kale Arenası"},

			{"piece_ramborecave_dod", "Ram'Bore Sunağı"},
			{"piece_ramborecave_text_dod", "5 adet Domuz Dişi gerekiyor."},
			{"enemy_pin_rambore_dod", "Ram'Bore'Mağarası"},
			{"location_rambore_dod", "Ram'Bore Mağarası"},
			{"lore_rambore_dod", "Ram'Bore - Bilinmiyor"},

			{"piece_lorestone_dod", "Lore Taşı"},

			{"item_walnut_dod", "Ceviz"},
			{"item_walnuts_description_dod", "Sisli Topraklar bölgesinden toplanan yiyecek."},
			{"item_apple_dod", "Elma"},
			{"item_apple_description_dod", "Sisli Topraklar bölgesinden toplanan yiyecek."},
			{"item_redberries_dod", "Kiraz"},
			{"item_redberries_description_dod", "Sisli Topraklar bölgesinden toplanan yiyecek."},
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
		public static class DoDBLocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
