using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using System;

namespace FantasyArmoury
{
	[HarmonyPatch]
	public class FALocal
	{
		private static Localization lcl;
		public static Dictionary<string, string> t;
		private static Dictionary<string, string> english = new Dictionary<string, string>() {

			{"firepit_fa", "Firepit"},
			{"firepit_desc_fa", "Adds 1 level to the Forge"},
			{"furnace_fa", "Furnace"},
			{"furnace_desc_fa", "Adds 1 level to the Forge"},
			{"press_fa", "Press"},
			{"press_desc_fa", "Adds 1 level to the Forge"},
			{"trough_fa", "Trough"},
			{"trough_desc_fa", "Adds 1 level to the Forge"},

			{"item_sword2h_01_fa", "Frostsoul"},
			{"item_sword2h_01_fa_description", "A weapon of ice and death."},

			{"item_sword2h_02_fa", "The Unclean"},
			{"item_sword2h_02_fa_description", "Blighted. They are all blighted."},

			{"item_sword2h_03_fa", "Torment"},
			{"item_sword2h_03_fa_description", "A weapon designed to cause anguishing pain."},

			{"item_sword2h_04_fa", "Crystal Edge"},
			{"item_sword2h_04_fa_description", "A sword with a rough blade of crystal, designed to both bludgeon and slice."},

			{"item_sword2h_05_fa", "Silverwing"},
			{"item_sword2h_05_fa_description", "Silver and ruby singing in harmony."},

			{"item_sword2h_06_fa", "The Betrayer"},
			{"item_sword2h_06_fa_description", "Cold-forged steel and a shaft of withered bone."},

			{"item_sword1h_01_fa", "Fortune's Edge"},
			{"item_sword1h_01_fa_description", "Gold runes bestow great luck upon the bearer of this weapon... for a price."},

			{"item_sword1h_02_fa", "Soul Piercer"},
			{"item_sword1h_02_fa_description", "Flesh can be mended, the soul cannot."},

			{"item_sword1h_03_fa", "The Spellblade"},
			{"item_sword1h_03_fa_description", "Infused with the elements by an unknown force."},

			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "A gift from Asgard, engraved with fiery runes of power."},

			{"item_sword1h_05_fa", "Ravenblade"},
			{"item_sword1h_05_fa_description", "A cold blade empowered, they say, in the cover of night."},

			{"item_staff2h_01_fa", "Arm of the Ent"},
			{"item_staff2h_01_fa_description", "Ripped from an Ent and fashioned into a conduit for the very spirit of nature itself."},

			{"item_staff2h_02_fa", "Greydwarf's Bane"},
			{"item_staff2h_02_fa_description", "It sees the things they see. Eyes, the magic is in their eyes."},

			{"item_staff2h_03_fa", "Talon"},
			{"item_staff2h_03_fa_description", "Wicked sharp and viciously fast."},

			{"item_staff2h_04_fa", "Dirty Pike"},
			{"item_staff2h_04_fa_description", "The tip of this pike is laced with disease. Woe befalls those into whom's flesh it pierces."},

			{"item_staff2h_05_fa", "The Martyr"},
			{"item_staff2h_05_fa_description", "Blessed by seers from Midgard, this weapon found its way into the tenth world. Use it wisely."},

			{"item_scythe2h_01_fa", "Pestilence"},
			{"item_scythe2h_01_fa_description", "Decay, suffering, disease..."},

			{"item_hammer2h_01_fa", "Rocksmasher"},
			{"item_hammer2h_01_fa_description", "Anything can be smashed with the right tool. This is that tool."},

			{"item_hammer2h_02_fa", "The Willbreaker"},
			{"item_hammer2h_02_fa_description", "This hammer reigns down fiery judgement upon any and all who are deemed unclean."},

			{"item_hammer2h_03_fa", "Shockpiercer"},
			{"item_hammer2h_03_fa_description", "A gift, they say, left to the tenth world by Thor himself."},

			{"item_axe1h_01_fa", "The Blood Spiller"},
			{"item_axe1h_01_fa_description", "This axe is layered with spikes that both rend and puncture its target. It is a weapon of bloody death."},

			{"item_axe1h_02_fa", "Moonsong"},
			{"item_axe1h_02_fa_description", "With every swing, a song of violence."},

			{"item_axe1h_03_fa", "Roughcut"},
			{"item_axe1h_03_fa_description", "Built using scraps of metal and stone, this axe cuts deep and dirty."},

			{"item_axe1h_04_fa", "Frozen Justice"},
			{"item_axe1h_04_fa_description", "Justice, but colder!"},

			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Everything burns, all will be ashen in the end."},

			{"item_axe1h_06_fa", "The Harvester"},
			{"item_axe1h_06_fa_description", "It thirst for souls; It is never sated."},

			{"item_axe1h_07_fa", "Dryad Bane"},
			{"item_axe1h_07_fa_description", "A blade grafted from the bark of the oldest of trees."},

			{"item_axe2h_06_fa", "Ghoulblade, The Defiler"},
			{"item_axe2h_06_fa_description", "Each swing cries with the voices of the long dead."},

			{"item_axe2h_05_fa", "Spirit Sapling"},
			{"item_axe2h_05_fa_description", "An axe they say is forged from a splinter of Yggdrasill itself."},

			{"item_axe2h_04_fa", "Demonbite"},
			{"item_axe2h_04_fa_description", "Laced with the living ooze of the swamplands, this blade inflicts wounds beyond flesh."},

			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "This weapon was cast into the depths of the tenth world for being an abomination of nature. It is here you discovered the recipe. It is here you will wield its will."},

			{"item_axe2h_02_fa", "Vengeance"},
			{"item_axe2h_02_fa_description", "Vengeance persists, even through death. Of course, this axe can help with the death part."},

			{"item_axe2h_01_fa", "Dire Axe"},
			{"item_axe2h_01_fa_description", "A dirty weapon, designed to inflict unsealable wounds that both pierce and rend flesh."},

			{"item_shield_09_tower_fa", "Pride"},
			{"item_shield_09_tower_fa_description", "Courage and light fill the bearer of this shield."},

			{"item_shield_08_tower_fa", "Rambore's Likeness"},
			{"item_shield_08_tower_fa_description", "A shield carved to mimic the visage of Rambore."},

			{"item_shield_07_tower_fa", "Cryptkeeper"},
			{"item_shield_07_tower_fa_description", "The dead stay dead at the sight of The Cryptkeeper"},

			{"item_shield_06_tower_fa", "Righteous Indignation"},
			{"item_shield_06_tower_fa_description", "A holy glow emanates from the gem welded into this shield."},

			{"item_shield_05_tower_fa", "Entwood Shield"},
			{"item_shield_05_tower_fa_description", "This shield is crafted with the wood of the Ents themselves."},

			{"item_shield_04_tower_fa", "The Watchful Eye"},
			{"item_shield_04_tower_fa_description", "Heed its whispers for it sees all before it."},

			{"item_shield_03_tower_fa", "Devil's Screech"},
			{"item_shield_03_tower_fa_description", "Carved in the likeness of the devil himself from reagents of fire and fury."},

			{"item_shield_02_tower_fa", "Skull, The Unbreaking"},
			{"item_shield_02_tower_fa_description", "They say the skull is the hardest bone in the body, this shield puts that to the test."},

			{"item_shield_01_tower_fa", "Talgane's Tear"},
			{"item_shield_01_tower_fa_description", "Carved from the scales of dragons, this shield has unworldly durability to the elements."}

		};
		private static Dictionary<string, string> russian = new Dictionary<string, string>() {


			{"firepit_fa", "Firepit"},
			{"firepit_desc_fa", "Adds 1 level to the Forge"},
			{"furnace_fa", "Furnace"},
			{"furnace_desc_fa", "Adds 1 level to the Forge"},
			{"press_fa", "Press"},
			{"press_desc_fa", "Adds 1 level to the Forge"},
			{"trough_fa", "Trough"},
			{"trough_desc_fa", "Adds 1 level to the Forge"},

			{"item_sword2h_01_fa", "Frostsoul"},
			{"item_sword2h_01_fa_description", "A weapon of ice and death."},

			{"item_sword2h_02_fa", "The Unclean"},
			{"item_sword2h_02_fa_description", "Blighted. They are all blighted."},

			{"item_sword2h_03_fa", "Torment"},
			{"item_sword2h_03_fa_description", "A weapon designed to cause anguishing pain."},

			{"item_sword2h_04_fa", "Crystal Edge"},
			{"item_sword2h_04_fa_description", "A sword with a rough blade of crystal, designed to both bludgeon and slice."},

			{"item_sword2h_05_fa", "Silverwing"},
			{"item_sword2h_05_fa_description", "Silver and ruby singing in harmony."},

			{"item_sword2h_06_fa", "The Betrayer"},
			{"item_sword2h_06_fa_description", "Cold-forged steel and a shaft of withered bone."},

			{"item_sword1h_01_fa", "Fortune's Edge"},
			{"item_sword1h_01_fa_description", "Gold runes bestow great luck upon the bearer of this weapon... for a price."},

			{"item_sword1h_02_fa", "Soul Piercer"},
			{"item_sword1h_02_fa_description", "Flesh can be mended, the soul cannot."},

			{"item_sword1h_03_fa", "The Spellblade"},
			{"item_sword1h_03_fa_description", "Infused with the elements by an unknown force."},

			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "A gift from Asgard, engraved with fiery runes of power."},

			{"item_sword1h_05_fa", "Ravenblade"},
			{"item_sword1h_05_fa_description", "A cold blade empowered, they say, in the cover of night."},

			{"item_staff2h_01_fa", "Arm of the Ent"},
			{"item_staff2h_01_fa_description", "Ripped from an Ent and fashioned into a conduit for the very spirit of nature itself."},

			{"item_staff2h_02_fa", "Greydwarf's Bane"},
			{"item_staff2h_02_fa_description", "It sees the things they see. Eyes, the magic is in their eyes."},

			{"item_staff2h_03_fa", "Talon"},
			{"item_staff2h_03_fa_description", "Wicked sharp and viciously fast."},

			{"item_staff2h_04_fa", "Dirty Pike"},
			{"item_staff2h_04_fa_description", "The tip of this pike is laced with disease. Woe befalls those into whom's flesh it pierces."},

			{"item_staff2h_05_fa", "The Martyr"},
			{"item_staff2h_05_fa_description", "Blessed by seers from Midgard, this weapon found its way into the tenth world. Use it wisely."},

			{"item_scythe2h_01_fa", "Pestilence"},
			{"item_scythe2h_01_fa_description", "Decay, suffering, disease..."},

			{"item_hammer2h_01_fa", "Rocksmasher"},
			{"item_hammer2h_01_fa_description", "Anything can be smashed with the right tool. This is that tool."},

			{"item_hammer2h_02_fa", "The Willbreaker"},
			{"item_hammer2h_02_fa_description", "This hammer reigns down fiery judgement upon any and all who are deemed unclean."},

			{"item_hammer2h_03_fa", "Shockpiercer"},
			{"item_hammer2h_03_fa_description", "A gift, they say, left to the tenth world by Thor himself."},

			{"item_axe1h_01_fa", "The Blood Spiller"},
			{"item_axe1h_01_fa_description", "This axe is layered with spikes that both rend and puncture its target. It is a weapon of bloody death."},

			{"item_axe1h_02_fa", "Moonsong"},
			{"item_axe1h_02_fa_description", "With every swing, a song of violence."},

			{"item_axe1h_03_fa", "Roughcut"},
			{"item_axe1h_03_fa_description", "Built using scraps of metal and stone, this axe cuts deep and dirty."},

			{"item_axe1h_04_fa", "Frozen Justice"},
			{"item_axe1h_04_fa_description", "Justice, but colder!"},

			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Everything burns, all will be ashen in the end."},

			{"item_axe1h_06_fa", "The Harvester"},
			{"item_axe1h_06_fa_description", "It thirst for souls; It is never sated."},

			{"item_axe1h_07_fa", "Dryad Bane"},
			{"item_axe1h_07_fa_description", "A blade grafted from the bark of the oldest of trees."},

			{"item_axe2h_06_fa", "Ghoulblade, The Defiler"},
			{"item_axe2h_06_fa_description", "Each swing cries with the voices of the long dead."},

			{"item_axe2h_05_fa", "Spirit Sapling"},
			{"item_axe2h_05_fa_description", "An axe they say is forged from a splinter of Yggdrasill itself."},

			{"item_axe2h_04_fa", "Demonbite"},
			{"item_axe2h_04_fa_description", "Laced with the living ooze of the swamplands, this blade inflicts wounds beyond flesh."},

			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "This weapon was cast into the depths of the tenth world for being an abomination of nature. It is here you discovered the recipe. It is here you will wield its will."},

			{"item_axe2h_02_fa", "Vengeance"},
			{"item_axe2h_02_fa_description", "Vengeance persists, even through death. Of course, this axe can help with the death part."},

			{"item_axe2h_01_fa", "Dire Axe"},
			{"item_axe2h_01_fa_description", "A dirty weapon, designed to inflict unsealable wounds that both pierce and rend flesh."},

			{"item_shield_09_tower_fa", "Pride"},
			{"item_shield_09_tower_fa_description", "Courage and light fill the bearer of this shield."},

			{"item_shield_08_tower_fa", "Rambore's Likeness"},
			{"item_shield_08_tower_fa_description", "A shield carved to mimic the visage of Rambore."},

			{"item_shield_07_tower_fa", "Cryptkeeper"},
			{"item_shield_07_tower_fa_description", "The dead stay dead at the sight of The Cryptkeeper"},

			{"item_shield_06_tower_fa", "Righteous Indignation"},
			{"item_shield_06_tower_fa_description", "A holy glow emanates from the gem welded into this shield."},

			{"item_shield_05_tower_fa", "Entwood Shield"},
			{"item_shield_05_tower_fa_description", "This shield is crafted with the wood of the Ents themselves."},

			{"item_shield_04_tower_fa", "The Watchful Eye"},
			{"item_shield_04_tower_fa_description", "Heed its whispers for it sees all before it."},

			{"item_shield_03_tower_fa", "Devil's Screech"},
			{"item_shield_03_tower_fa_description", "Carved in the likeness of the devil himself from reagents of fire and fury."},

			{"item_shield_02_tower_fa", "Skull, The Unbreaking"},
			{"item_shield_02_tower_fa_description", "They say the skull is the hardest bone in the body, this shield puts that to the test."},

			{"item_shield_01_tower_fa", "Talgane's Tear"},
			{"item_shield_01_tower_fa_description", "Carved from the scales of dragons, this shield has unworldly durability to the elements."}


			};
		private static Dictionary<string, string> german = new Dictionary<string, string>() {


			{"firepit_fa", "Firepit"},
			{"firepit_desc_fa", "Adds 1 level to the Forge"},
			{"furnace_fa", "Furnace"},
			{"furnace_desc_fa", "Adds 1 level to the Forge"},
			{"press_fa", "Press"},
			{"press_desc_fa", "Adds 1 level to the Forge"},
			{"trough_fa", "Trough"},
			{"trough_desc_fa", "Adds 1 level to the Forge"},

			{"item_sword2h_01_fa", "Frostsoul"},
			{"item_sword2h_01_fa_description", "A weapon of ice and death."},

			{"item_sword2h_02_fa", "The Unclean"},
			{"item_sword2h_02_fa_description", "Blighted. They are all blighted."},

			{"item_sword2h_03_fa", "Torment"},
			{"item_sword2h_03_fa_description", "A weapon designed to cause anguishing pain."},

			{"item_sword2h_04_fa", "Crystal Edge"},
			{"item_sword2h_04_fa_description", "A sword with a rough blade of crystal, designed to both bludgeon and slice."},

			{"item_sword2h_05_fa", "Silverwing"},
			{"item_sword2h_05_fa_description", "Silver and ruby singing in harmony."},

			{"item_sword2h_06_fa", "The Betrayer"},
			{"item_sword2h_06_fa_description", "Cold-forged steel and a shaft of withered bone."},

			{"item_sword1h_01_fa", "Fortune's Edge"},
			{"item_sword1h_01_fa_description", "Gold runes bestow great luck upon the bearer of this weapon... for a price."},

			{"item_sword1h_02_fa", "Soul Piercer"},
			{"item_sword1h_02_fa_description", "Flesh can be mended, the soul cannot."},

			{"item_sword1h_03_fa", "The Spellblade"},
			{"item_sword1h_03_fa_description", "Infused with the elements by an unknown force."},

			{"item_sword1h_04_fa", "Divine Light"},
			{"item_sword1h_04_fa_description", "A gift from Asgard, engraved with fiery runes of power."},

			{"item_sword1h_05_fa", "Ravenblade"},
			{"item_sword1h_05_fa_description", "A cold blade empowered, they say, in the cover of night."},

			{"item_staff2h_01_fa", "Arm of the Ent"},
			{"item_staff2h_01_fa_description", "Ripped from an Ent and fashioned into a conduit for the very spirit of nature itself."},

			{"item_staff2h_02_fa", "Greydwarf's Bane"},
			{"item_staff2h_02_fa_description", "It sees the things they see. Eyes, the magic is in their eyes."},

			{"item_staff2h_03_fa", "Talon"},
			{"item_staff2h_03_fa_description", "Wicked sharp and viciously fast."},

			{"item_staff2h_04_fa", "Dirty Pike"},
			{"item_staff2h_04_fa_description", "The tip of this pike is laced with disease. Woe befalls those into whom's flesh it pierces."},

			{"item_staff2h_05_fa", "The Martyr"},
			{"item_staff2h_05_fa_description", "Blessed by seers from Midgard, this weapon found its way into the tenth world. Use it wisely."},

			{"item_scythe2h_01_fa", "Pestilence"},
			{"item_scythe2h_01_fa_description", "Decay, suffering, disease..."},

			{"item_hammer2h_01_fa", "Rocksmasher"},
			{"item_hammer2h_01_fa_description", "Anything can be smashed with the right tool. This is that tool."},

			{"item_hammer2h_02_fa", "The Willbreaker"},
			{"item_hammer2h_02_fa_description", "This hammer reigns down fiery judgement upon any and all who are deemed unclean."},

			{"item_hammer2h_03_fa", "Shockpiercer"},
			{"item_hammer2h_03_fa_description", "A gift, they say, left to the tenth world by Thor himself."},

			{"item_axe1h_01_fa", "The Blood Spiller"},
			{"item_axe1h_01_fa_description", "This axe is layered with spikes that both rend and puncture its target. It is a weapon of bloody death."},

			{"item_axe1h_02_fa", "Moonsong"},
			{"item_axe1h_02_fa_description", "With every swing, a song of violence."},

			{"item_axe1h_03_fa", "Roughcut"},
			{"item_axe1h_03_fa_description", "Built using scraps of metal and stone, this axe cuts deep and dirty."},

			{"item_axe1h_04_fa", "Frozen Justice"},
			{"item_axe1h_04_fa_description", "Justice, but colder!"},

			{"item_axe1h_05_fa", "Ashes"},
			{"item_axe1h_05_fa_description", "Everything burns, all will be ashen in the end."},

			{"item_axe1h_06_fa", "The Harvester"},
			{"item_axe1h_06_fa_description", "It thirst for souls; It is never sated."},

			{"item_axe1h_07_fa", "Dryad Bane"},
			{"item_axe1h_07_fa_description", "A blade grafted from the bark of the oldest of trees."},

			{"item_axe2h_06_fa", "Ghoulblade, The Defiler"},
			{"item_axe2h_06_fa_description", "Each swing cries with the voices of the long dead."},

			{"item_axe2h_05_fa", "Spirit Sapling"},
			{"item_axe2h_05_fa_description", "An axe they say is forged from a splinter of Yggdrasill itself."},

			{"item_axe2h_04_fa", "Demonbite"},
			{"item_axe2h_04_fa_description", "Laced with the living ooze of the swamplands, this blade inflicts wounds beyond flesh."},

			{"item_axe2h_03_fa", "Scourgeborne"},
			{"item_axe2h_03_fa_description", "This weapon was cast into the depths of the tenth world for being an abomination of nature. It is here you discovered the recipe. It is here you will wield its will."},

			{"item_axe2h_02_fa", "Vengeance"},
			{"item_axe2h_02_fa_description", "Vengeance persists, even through death. Of course, this axe can help with the death part."},

			{"item_axe2h_01_fa", "Dire Axe"},
			{"item_axe2h_01_fa_description", "A dirty weapon, designed to inflict unsealable wounds that both pierce and rend flesh."},

			{"item_shield_09_tower_fa", "Pride"},
			{"item_shield_09_tower_fa_description", "Courage and light fill the bearer of this shield."},

			{"item_shield_08_tower_fa", "Rambore's Likeness"},
			{"item_shield_08_tower_fa_description", "A shield carved to mimic the visage of Rambore."},

			{"item_shield_07_tower_fa", "Cryptkeeper"},
			{"item_shield_07_tower_fa_description", "The dead stay dead at the sight of The Cryptkeeper"},

			{"item_shield_06_tower_fa", "Righteous Indignation"},
			{"item_shield_06_tower_fa_description", "A holy glow emanates from the gem welded into this shield."},

			{"item_shield_05_tower_fa", "Entwood Shield"},
			{"item_shield_05_tower_fa_description", "This shield is crafted with the wood of the Ents themselves."},

			{"item_shield_04_tower_fa", "The Watchful Eye"},
			{"item_shield_04_tower_fa_description", "Heed its whispers for it sees all before it."},

			{"item_shield_03_tower_fa", "Devil's Screech"},
			{"item_shield_03_tower_fa_description", "Carved in the likeness of the devil himself from reagents of fire and fury."},

			{"item_shield_02_tower_fa", "Skull, The Unbreaking"},
			{"item_shield_02_tower_fa_description", "They say the skull is the hardest bone in the body, this shield puts that to the test."},

			{"item_shield_01_tower_fa", "Talgane's Tear"},
			{"item_shield_01_tower_fa_description", "Carved from the scales of dragons, this shield has unworldly durability to the elements."}


			};
		private static Dictionary<string, string> turkish = new Dictionary<string, string>() {

			{"item_sword2h_01_fa", "İnançbekleyen, Aydınlatılmış Rüyaların Ağıtı"},
			{"item_sword2h_01_fa_description", "İnançbekleyen, çift elli bir kılıçtır."},
			{"item_sword2h_02_fa", "Arındırançizik, Kayıp Seslerin Kılıcı"},
			{"item_sword2h_02_fa_description", "Arındırançizik, çift elli bir kılıçtır."},
			{"item_sword2h_03_fa", "Habiskovan"},
			{"item_sword2h_03_fa_description", "Habiskovan, çift elli bir kılıçtır."},
			{"item_sword2h_04_fa", "Kindarbüyü Kılıcı"},
			{"item_sword2h_04_fa_description", "Kindarbüyü Kılıcı, çift elli bir kılıçtır."},
			{"item_sword2h_05_fa", "Hesapkesen, Güneşgezen'in Hediyesi"},
			{"item_sword2h_05_fa_description", "Hesapkesen, çift elli bir kılıçtır."},
			{"item_sword2h_06_fa", "İhanetgezen"},
			{"item_sword2h_06_fa_description", "İhanetgezen, çift elli bir kılıçtır."},
			{"item_sword1h_01_fa", "Talih Kılıcı"},
			{"item_sword1h_01_fa_description", "Talih Kılıcı, tek elli bir kılıçtır."},
			{"item_sword1h_02_fa", "Ayinbozan"},
			{"item_sword1h_02_fa_description", "Ayinbozan, tek elli bir kılıçtır."},
			{"item_sword1h_03_fa", "Bilenmiş Büyükılıcı"},
			{"item_sword1h_03_fa_description", "Bilenmiş Büyükılıcı, tek elli bir kılıçtır."},
			{"item_sword1h_04_fa", "İlahiışık"},
			{"item_sword1h_04_fa_description", "İlahiışık, tek elli bir kılıçtır."},
			{"item_sword1h_05_fa", "Şehvetli Gümüş Dilimleyen"},
			{"item_sword1h_05_fa_description", "Şehvetli Gümüş Dilimleyen, tek elli bir kılıçtır."},
			{"item_staff2h_01_fa", "Yaraizi"},
			{"item_staff2h_01_fa_description", "Yaraizi, çift elli bir asadır."},
			{"item_staff2h_02_fa", "Kader"},
			{"item_staff2h_02_fa_description", "Kader, çift elli bir asadır."},
			{"item_staff2h_03_fa", "Sismoloji"},
			{"item_staff2h_03_fa_description", "Sismoloji, çift elli bir asadır."},
			{"item_staff2h_04_fa", "Kuyruklu Yıldız"},
			{"item_staff2h_04_fa_description", "Kuyruklu Yıldız, çift elli bir asadır."},
			{"item_staff2h_05_fa", "Uyurgezer"},
			{"item_staff2h_05_fa_description", "Uyurgezer, çift elli bir asadır."},
			{"item_scythe2h_01_fa", "Nesiltüketen"},
			{"item_scythe2h_01_fa_description", "Nesiltüketen, çift elli bir tırpandır."},
			{"item_hammer2h_01_fa", "Gözkarartan"},
			{"item_hammer2h_01_fa_description", "Gözkarartan, çift elli bir balyozdur."},
			{"item_hammer2h_02_fa", "İradekıran"},
			{"item_hammer2h_02_fa_description", "İradekıran, çift elli bir balyozdur."},
			{"item_hammer2h_03_fa", "Hesapezen"},
			{"item_hammer2h_03_fa_description", "Hesapezen, çift elli bir balyozdur."},
			{"item_axe1h_01_fa", "Azapçeken"},
			{"item_axe1h_01_fa_description", "Azapçeken, tek elli bir baltadır."},
			{"item_axe1h_02_fa", "Celeste"},
			{"item_axe1h_02_fa_description", "Celeste, tek elli bir baltadır."},
			{"item_axe1h_03_fa", "Nirvana"},
			{"item_axe1h_03_fa_description", "Nirvana, tek elli bir baltadır."},
			{"item_axe1h_04_fa", "İspatyırtan"},
			{"item_axe1h_04_fa_description", "İspatyırtan, tek elli bir baltadır."},
			{"item_axe1h_05_fa", "Küldiyar"},
			{"item_axe1h_05_fa_description", "Küldiyar, tek elli bir baltadır."},
			{"item_axe1h_06_fa", "Biçerdöver"},
			{"item_axe1h_06_fa_description", "Biçerdöver, tek elli bir baltadır."},
			{"item_axe1h_07_fa", "Umut Yağmalayan"},
			{"item_axe1h_07_fa_description", "Umut Yağmalayan, tek elli bir baltadır."},
			{"item_axe2h_06_fa", "Ruhkesen"},
			{"item_axe2h_06_fa_description", "Ruhkesen, çift elli bir baltadır."},
			{"item_axe2h_05_fa", "Omurgabiçen"},
			{"item_axe2h_05_fa_description", "Omurgabiçen, çift elli bir baltadır."},
			{"item_axe2h_04_fa", "Narkoleptik"},
			{"item_axe2h_04_fa_description", "Narkoleptik, çift elli bir baltadır."},
			{"item_axe2h_03_fa", "Belakesik"},
			{"item_axe2h_03_fa_description", "Belakesik, çift elli bir baltadır."},
			{"item_axe2h_02_fa", "Sözkesen"},
			{"item_axe2h_02_fa_description", "Sözkesen, çift elli bir baltadır."},
			{"item_axe2h_01_fa", "Korkunç Balta"},
			{"item_axe2h_01_fa_description", "Korkunç Balta, çift elli bir baltadır."},
			{"item_shield_09_tower_fa", "Barış Muhafızı"},
			{"item_shield_09_tower_fa_description", "Barış Muhafızı, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_08_tower_fa", "Zehir Kesen Kalkan"},
			{"item_shield_08_tower_fa_description", "Zehir Kesen Kalkan, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_07_tower_fa", "Susamış İskelet Kalkanı"},
			{"item_shield_07_tower_fa_description", "Susamış İskelet Kalkanı, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_06_tower_fa", "Lejyoner Kalkanı"},
			{"item_shield_06_tower_fa_description", "Lejyoner Kalkanı, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_05_tower_fa", "Arzunun Söğüt Kalkanı"},
			{"item_shield_05_tower_fa_description", "Arzunun Söğüt Kalkanı, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_04_tower_fa", "Şehvetli Muhafız"},
			{"item_shield_04_tower_fa_description", "Şehvetli Muhafız, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_03_tower_fa", "Kanlı Büyük Kalkan"},
			{"item_shield_03_tower_fa_description", "Kanlı Büyük Kalkan, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_02_tower_fa", "Kirlenmiş Kalkan Duvar"},
			{"item_shield_02_tower_fa_description", "Kirlenmiş Kalkan Duvar, sizi saldırılardan koruyan bir kalkandır."},
			{"item_shield_01_tower_fa", "Hüzün Koruyucusu"},
			{"item_shield_01_tower_fa_description", "Hüzün Koruyucusu, sizi saldırılardan koruyan bir kalkandır."}

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
		public static class FALocalizationPatch
		{
			public static void Postfix(Localization __instance, string language)
			{
				init(language, __instance);
				UpdateDictinary();
			}
		}
	}
}
