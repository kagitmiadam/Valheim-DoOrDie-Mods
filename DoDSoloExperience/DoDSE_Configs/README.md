
## Authors Notes

**Updating to version 1.2.0+ from an earlier version will require an uninstall of the Do or Die SE config folder prior to updating. It is just to remove the dod_vanilla_50 folder from Do or Die SE, its been moved to CLLC yaml, you can delete just that folder if you want.**
	
	You will see two DropThat warnings about overlapping World Spawner's, index numbers 15 and 24 this is intentional and can be ignored. 
	You will noticed a marked increase in difficulty with 1.1.12+ since I moved the CLLC Yaml's to the config folder and included them in a subfolder they have not been working correctly. 
	I have moved them to the Config folder so they load correctly. Deaths Inc... 
	
**Use version 1.1.0 or 1.15 of Valheim Leveling System, config included for balance reasons.**

**I have included a config for the Dual Wield mod for balance reasons, the use of this mod is down to each user and not included with this pack. If you use this mod NPC's will take full advantage.**


## Brief Overview

	Allows upto 13 Star Mobs and Bosses to spawn depending on world and sector level. 
	5 New Bosses. (See Below)
	3 New Boss Invasions. (see below) 
	5 Star and over start spawning at world level 1. (Adjust as you see fit in the CLLC config) 
	Populates Mistlands with four different NPCs, Corrupted Elderling, Vilefang, Void Spider, Jumping Spider and Black Spiders. 
	Poulates Deep North with Gray Wolf (NPC), Dire Wolf, Ice Drake, Ice Troll, Ice Ogre, Ice Spider, Awakened and Greater Fenring. 
	Populates Ashlands with Fire Golem, Obsidian Golem, Greater Surtling, Flame Spider and Flame Drakes. 
	Adds Frost Spider, Ghost Warrior and Skeleton Warrior to Mountains. 
	Adds Elderling and Forest Spider to Black Forest. (Elder death trigger) 
	Adds Forest Wolf to Meadows and Black Forest. (Moder death trigger) 
	Each Biome has 2 NPC Mini Bosses, one in the day and one in the night. (see below) 
	
	
## Installation

**Auto**

	Use a mod manager of your choice.


**Manual** 

	Download the Zip. Open it up and Drag the contents into your profiles(Mod Managers) or main installs (Manual), BepInEx folder and overwrite when asked. 
	
	Thunderstore Mod Manager Profile Location:- C:\Users\USER_NAME\AppData\Roaming\Thunderstore Mod Manager\DataFolder\Valheim\profiles\PROFILE_NAME\BepInEx. 
	
	R2MM Profile Location:- C:/Users/USER_NAME/AppData/Roaming/r2modmanPlus-local/Valheim/profiles/PROFILE_NAME/BepInEx. 


For further assistance you can find me on my offical Discord:- https://discord.gg/7BcEZXhRbV


## Questing

Uses Aedenthorn's Questing system as a soft requirement. (Includes cfg's and custom quest files, use down to user)
For questing to work you will require these 2 mods from nexus mods. 

- [Questing Framework] (https://www.nexusmods.com/valheim/mods/1583)
- [Hugin Quests] (https://www.nexusmods.com/valheim/mods/1588)

Install them in to your profiles, BepInEx\Plugins folder, or, your main games BepInEx\Plugins folder if you manually installed all your mods.

Integrated Bone Appetit and Potions Plus items into the DoD Quest's as rewards. These mods will be required. I know they are in the mod list, I also know a few of you play without them, take this as a pre warning.

Hugin will offer a quest every 20 mins. There is a mix of Killing, Gathering, Cooking, Mining, Collecting and Gardening quests, well more like Tasks tbh, 161 in total.


## Known Issues

**Better Raids**

In com.alexanderstrada.rrrbetterraids.cfg, I have the option: UpdateEventMobMinimapPins option set to false. This is to prevent an error showing in the log when you leave the raid event zone with mobs still alive. You can turn this option on as it does not look to effect game play in anyway. Upto User.

**Loot**

Random Legendaries showing up instead of Set Items. Under investigation.
Free loot at 0,0,0. Under investigation. (DoD Kit drops disabled as a result)

**Questing**

Random Quests can be odd. Issue with custom mobs not registering for kill quests. Been looked at.

**Epic Loot**

Mildly effects performance. (10-15 fps loss)

**RRR Mods**

Greatly effects performance. (25-45fps loss, these mods are been phased out gradually)


## Incompatible Mods

- ValhiemLib and by extension any mod that still uses this defunct library.
- JotunnLib and by extension any mod that still uses this defunct library.
- ValheimPlus (Conflicts with some of the dependencies)
- Valhiem Level System (Conflicts with Epic Loot and Dual Wield)
- Serverside Simulations (Breaks Spawners)

## Mods that can cause Issues

- Simply Recycling can break Epic Loot/Tombstone depending on mods used.


## Crossover Mods

- Digital Roots Bounties, its a case of his or mine. Up to user. (Until I find the time to look over this mod and mine and see if its possible to have both)


## Difficulty

Mobs have 40% more health and 35% more damage and an additional 20% health and 17.5% damager per player.
Scaling difficulty for all Monsters, 5% per World Level.
Vanilla bosses get a Buff per world level after killing them the first time. (The buff increases their Tier by 1 per world level)
Monsters gain 25% Health and Damage after you kill the Vanilla boss in that Biome.


## Servers

Required on both server and client due to dependencies.
May require balancing for more than 2-3 players at a time. Can be done by tweaking the DoD CLLC yaml's located in the Configs folder.


## Acknowledgements

	A special thanx to: DefendYourBase, RandyKnapp, Neurod0me, Tykea, HugotheDwarf, GoldenJude and ASharpPen, creators of the mods used. 
	Without them it would have been impossible to create DoD, thank you very much!

	Cepera for help with fixes and updates.
  
  If you like and use this ModPack, show you support by leaving a like for each mod included. Cheers and happy hunting!
	If you wish to donate please donate to any of the modpacks dependancies, as I do not accept donations myself.
  
  
## Epic Loot Magic Effect Changes

	Resistance Percents range from 1-40% and are limited to the following items:

	Helm: Blunt & Fire
	Chest: Pierce & Frost
	Legs: Slashing & Lightning
	Shield: Elemental & Physical
	Shoulder: Poison

	Resistances (Like the games Wolf Armour) are Cloak and Utility only. Very Low Probability. 
	Luck is Helmet only. 
	Block and Parry range from 15-75% Depending on Rarity. 
	Damage Percent range from 5-45% depending on Rarity. 
	Damage Flat range from 3-30 depending on Rarity. 
	Carry Weight is limited to Shoulder and Utility. Range from 25-225 depending on Rarity. 
	Skills range from 1-20 depending on Rarity. 
	Health and Stamina Regen percent ranges from 5-45% depending on Rarity. 
	Health and Stamina increases range from 15-75 depending on Rarity. 
	Attack Speed, Stagger Duration, Quick Learner, Reflect Damage and all Low Health Effects range from 5-27% depending on Rarity. 
	All Magical Effect scale up with no crossover. This means a higher Rarity will always be better than its previous Rarity. 
	Probably some I have missed. 


## Creature Level and Loot Control Changes

	Health Increase Per Player - 20% 
	Damage Increase Per Player - 17.5% 
	Additonal Player Count - 2 (Plus number of Players) 
	Difficulty - Custom by World Level 
	Creature and Boss Special Effects - On (As per Default CLLC) 
	Sectors - On (World Level 1) 
	Sector Level Up Kill Count - 10, 25, 50 
	World Level Up - Ingame Days: 15, 25, 50, 100, 200 
	Scaling Difficulty By World Level - 5% Health, 10% Damage, 5% Attack Speed, 5% Resistance per world Level. 
	
World Level Star Chances (0-10 Creatures and Bosses) 

- Level 0: 50, 44, 4, 1, 1
- Level 1: 40, 50, 6, 3, 1, 1
- Level 2: 27, 52, 11, 5, 2, 2, 1, 1
- Level 3: 17, 56, 13, 6, 2, 2, 2, 1, 1
- Level 4: 7, 57, 16, 8, 4, 2, 2, 2, 1, 1
- Level 5: 0, 55, 17, 10, 7, 3, 2, 2, 2, 1, 1

﻿Sector Level Ups can increase this by a further 3 Stars. 

**Changing CLLC settings**

- Do not change it off Custom or World Level. This will mess thing up and you may find yourself with enemies you can not handle if you did not adjust most of the other settings to balance it out.
- If you require to tweak the settings, you can do the difficulty via the DoD CLLC yamls. World and Sector days will also provide a buffer if increased.

## Judes Equipment Changes

TBD - Waiting on up coming update.


## Chaos Armor Changes 

**Tier One**

	Armor: 34
	Armor per level: 2
	Materials: SteelBar, Felmetal Bar, Spider Chitin, Voidling Core
	In Game Tier: 6.5

**Tier Two**

	Armor: 40
	Armor per level: 2
	Materials: Frometal Bar, Felmetal Bar, Voidling Core, Tier One Armor
	In Game Tier: 7.5

**Tier Three**

	Armor: 46
	Armor per level: 2
	Materials: Flametal, Felmetal Bar, Voidling Core, Tier Two Armor
	In Game Tier: 8.5


## Magic Overhaul Class Changes

**Death Knight**
	
	Type: Starter
	Item: None
	Helmet: 32AC 
	Weapon: 75D
	Mana Regen: 1
	Skill 2 Mana: 50
	Skill 1 Damage: 30
	Skill 2 Lifesteal: 0.2%
	Skill 3 Heal: 5%
	Skill 4 Damage: 25%
	Max Souls: 75

**Archer**

	Type: Ranged
	Item: Essence of the Archer
	Helmet: 34AC
	Weapon: 95D
	Mana Regen: 2
	Skill 1 Damage: 51

**Berserker**

	Type: Melee
	Item: Essence of the Berserker
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2
	Skill 1 Damage: 47
	Skill 3 Stamina Use: -75%
	Skill 4 Attack Speed: 100%

**Druid**

	Type: Magic
	Item: Essence of the Druid
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3
	Skill 1 Mana Cost: 75
	Skill 2 Damage: 42
	Skill 3 Heal: 55

**Mage**

	Type: Magic
	Item: Essence of the Mage
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3
	Skill 2 Damage: 50

**Monk**

	Type: Defense
	Item: Essence of the Monk
	Helmet: 40AC
	Weapon: 65D
	Mana Regen: 2
	Skill 1 Defense: 75%
	Skill 2 Damage: 46

**Ninja**

	Type: Melee
	Item: Essence of the Ninja
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2
	Skill 1 Heal: 27%
	Skill 3 Damage: 42

**Paladin**

	Type: Defense
	Item: Essence of the Paladin
	Helmet: 40AC
	Shield: 175 Block Power
	Mana Regen: 2
	Skill 3 Defense: 125%
	Skill 4 Defense: 75%

**Rogue**

	Type: Melee
	Item: Essence of the Rogue
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2
	Skill 3 Damage: 50%
	Skill 4 Damage: 75%

**Shaman**

	Type: Magic
	Item: Essence of the Shaman
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3
	Skill 2 Damage: 48
	Skill 4 Damage: 30

**Warlock**

	Type: Magic
	Item: Essence of the Warlock
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3
	Skill 1 Damage: 30
	Skill 2 Damage: 50
	Skill 4 Damage: 35


## Loot

	Item drops are intentionally hard to come by via killing mobs, 90% of the time normal worldly mobs will drop enchanting materials only. 
	This is to make Crafting still a viable option, only option early game, encourage early enchanting and finding items via other means more rewarding.
	Promotes exploration, heavily. Hodor hunting a must, Haldor too....
	Chests are your friend. World, Dungeon and Treasure Hunts!

	Explore, Explore, Explore!

	You'll be the next Indiana Jones in no time!


## Epic Loot Legendary Item Sets

6 Piece

    Unlucky Serf's Rags (Meadows)

5 Piece

    Beastmaster (Ash Lands)
    Warlord's Battlegear (Yagluth)

4 Piece

    Bloodsoaked Armor (Mistlands)
    Cinbri Attire (Deep North)
    Battlemage Vestments (Plains)
    Glitterdelve Heirlooms (Mountain)
    Plagueheart Raiment (Bonemass)
    Moder's Demise (Moder)

3 Piece

    Ravager's Outfit (Swamp)
    Relentless  (Blackforest)
    Spirit of Eikythr (Eikthyr)
    Giantstalker's Armor (The Elder)

2 Piece

    Teasha's Armament (Mistlands\DeepNorth)
    Traveller's Trinkets (Mountin\Plains)


## Drop Rates

Non Boss Drop Rates:

        Enchanting Mats: 5-15% per material type
        Magic Item: 10%
        Item Sets: 0.5%
    
Vanilla Boss Drop Rates

        Runestone: 1-2 (50%)
        Magic Item: 1-3 (100%, 50%, 25%)
        Chaos Armor: 10%
        Unique Item Set: 10%

Mini Boss Drop Rate

        Magic Item: 1-2
        Hugos 2H: 10%
        Chaos Armor: 5%

Note: Boss and Mini Boss Item Sets currently in planning.


## Bosses


**Zena (Tier 8) [Elemental]**

	Abilities: Groundslam, Kick, Fireball, Frostfire Fireball, Frostfire Meteors, Frostfire Nova, AoE Shield, Frostfire Beam, Summon Obsidian Golem, Summon Ice Golem, Cone of Cold.
	Type: Custom Raid - lasts 45 minutes, 15% chance every 10 mins.
	Trigger: Need to kill a Ice Golem, Obsidian Golem, Fire Golem, Lincoln Hunt, Draven Nox, Ash Vexx and Cinder Mortem, a minimum of 105 Days and be in an Ash Lands base.

**Neige (Tier 7) [Elemental]**

	Abilities: Groundslam, Kick, Frost Nova, Coldbreath, Ice Shards, Ice Bolt, Frost Tornado and Summon Ice Golem
	Type: Custom Raid - lasts 35 minutes, 15% chance every 10 mins.
	Trigger: Need to kill Moder, Ice Golem, Firion Winter, Lincoln Hunt, Lux Frost, Draven Nox, a minimum of 90 Days and be in a Deep North base.

**Artan (Tier 7) [Yeti]** (Under Construction)

	Abilities: Wounding Strike, Festering Strike, Impending Doom, Frost Strike, Frost Nova and Coldbreath.
	Type: Custom Raid - lasts 35 minutes, 15% chance every 10 mins.
	Trigger: Need to kill a Yeti, Farkas, and White Werewolf, a minimum of 90 Days and be in Deep North.

**Risith (Tier 6) [Spider]**

	TBA/ToDo


**Laughing Rylan (Tier 6) [Humanoid]**

	Abilities: Fire Arrow, Exploding Arrow, Poision Arrow, Stomp, Groundslam, Shockwave
	Type: Custom Raid - lasts 25 minutes, 15% chance every 10 mins.
	Trigger: Need to kill a Thief, Brigand, Shadow, Stalker, Scoundrel and Assassin, a minimum of 75 Days.


**Skir Sandburst (Tier 5) [Goblin Shaman]**

	See DoDMonsters.


**Farkas (Tier 4) [Wolf]**

	See DoDMonsters.


**Bhyg'shan (Tier 3) [Skeleton]**

	See DoDMonsters.


**Bitterstump (Tier 2) [Greydwarf Shaman]**

	See DoDMonsters.

**Ram-bone (Tier 1) [Boar]**

	See DoDMonsters.


**Anubisath (Tier 1) [Molluscan]**

	Abilities: Water Strike, Water Jet, Cold Rain
	Roaming: Coast, Rivers and Streams requires Thunderstorms or Mist.
	Trigger: Need to kill Eikthyr and have his Trophy on you.
	

## Mini Bosses

**UpirGrim (Tier 1)**

	Abilities: Jump Attack and Low Blow
	Roaming: Meadows, 2% Chance.

**ZaineEvilian (Tier 1)**

	Abilities: Front Backstab and Kick
	Roaming: Meadows, 2% Chance.

**LazarusAutumn (Tier 2)**

	Abilities: Jump Attack and Whirlwind
	Roaming: Black Forest, 2% Chance.

**GrailThornheart (Tier 2)**

	Abilities: Poison Cloud and Shockwave
	Roaming: Black Forest, 2% Chance.

**CrisenthShadowsoul (Tier 3)**

	Abilities: Whirlwind, Poison Cloud, Sonicboom
	Roaming: Swamp, 2% Chance.

**JaydenShadowmend (Tier 3)**
	
	Abilities: Poison Cloud, Poision Arrow and Whirlwind
	Roaming: Swamp, 2% Chance.

**LuxFrost (Tier 4)**
	
	Abilities: Cone of Cold, Ice Barage and Kill Shot
	Roaming: Mountains, 2% Chance.

**FirionWinter (Tier 4)**
	
	Abilities: Cone of Cold, Ice Barage and Whirlwind
	Roaming: Mountains, 2% Chance.

**EchoBlack (Tier 5)**

	Abilities: Fire Nova, Firebolt and Fireball
	Roaming: Plains, 2% Chance.

**MathianSerphent (Tier 5)**

	Abilities: Fire Beam, Fire Nova, Firebolt
	Roaming: Plains, 2% Chance.

**SceledrusShadowend (Tier 6)**

	Abilities: Shock Jump, Shockwave and Whirlwind
	Roaming: Mistlands, 2% Chance.

**LazarusDeamonne (Tier 6)**

	Abilities: Shock Jump, Shockwave and Whirlwind
	Roaming: Mistlands, 2% Chance.

**LincolnHunt (Tier 7)**

	Abilities: Sonicboom, Ice Storm and Kill Shot
	Roaming: Deep North, 2% Chance.

**DravenNox (Tier 7)**

	Abilities: Ice Storm, Whirlwind and Cone of Cold
	Roaming: Deep North, 2% Chance.

**AshVexx (Tier 8)**

	Abilities: Fire Nova, Fireball and Groundslam
	Roaming: Ashlands, 2% Chance.

**CinderMortem (Tier 8)**

	Abilities: Fire Beam, Meteors and Firebolt
	Roaming: Ashlands, 2% Chance.

- Recommened a new Character and World although it is not required. Use on a existing world at your own risk. 
- You can Limit the risk by seeing what day your world is on and comparing that with the world level listed above and adjusting the CLLC config for World Level Days.


## Recommended Mods

- Passive Powers By Smoothbrain(Config Included)
- Backpack Redux by Aedenthorn (Config Included)
- Discard or Recylce Inventory Items by Aedenthorn (Config Included)
- Durability by Aedenthorn (Config Included)
- Supplemental Raids
- TripleBronzeJVL
- Equipment and Quickslots
- Instant Monster Loot Drop


## Indexes

	Uses SpawnThat Indexes 950-1100.
	Uses DropThat Indexes 1000-1500 for Epic Loot additions.
	Uses DropThat Indexes 50-60 for additions to chests.
	
	
## Patch Notes

**1.2.10**

	Moved all NPC's over to DoD Monsters.
	Added Magic Overhaul Location. (with in 100m of Trophy Ring)
	Added Do Or Die Compendium Location. (with in 100m of Trophy Ring)
	Fixed Knarr.
	
**1.2.9**

	Moved Minibosses to Locations.
	Updated Location spawners added by DoD Biomes.
	Re-added Hugos Armory. (You can buy his items from Knarr)
	Added Instanced Villages and Wildlife mods as Requirements.
	Re-added missng configs for Judes Equipment.
	Updated to Latest DoD Biomes.
	
**1.2.8**

	Removed 2 broken or conflicting mods.
	Added DoD Biomes as a Requirement.
	
**1.2.7**

Removed some broken drops. (sorry for the double update)

**1.2.6**

	Removed Hugos Armory.
	Reduced Refect Damage from item sets.
	Added Digitalroots Slope Combat Fix as a requirement. (To prevent users from using the broken SCF mod)
	
**1.2.5**

	Stopped Ram'Bore Altar spawning on its own :)
	Fixed Typo in Forgotten Biomes cfg.
	Added This Goes Here as a requirement.
	Fixed Bugged Chests.

**1.2.4**

	Updated for DoD Monster 0.4.3.
	Laughing Rylan's minions are now called Herfiligr, Skugga and Einherjar.
	Updated all new locations spawns.
	Removed Bears.
	Fixed Black Werewolf been hostile to his brethren.
	Stopped Deep North Werewolves from having the CLLC effect, Splitting.
	Updated Forgotten Biomes cfgand veg files.
	Included placeholder (just wood) Knarr the Trader config, changed his currency to Skull Token. (Feel free to omit these changes if your using Knarr)
	
**1.2.3**

	Fixed issue's preventing the mod loading correctly.


**1.2.2**

	Adjusted Hugins rewards.
	Adjusted Discard Inventory Items cfg.
	Added new shield drops to Vanilla Bosses.
	Fixed Bhygshan not dropping Ravager Axe.
	Reduced kit drops from chests slightly.
	Added some of the DoD Trophies too Epic Loot's deconstruction.
	Fixed Vanilla Bosses not dropping Class Items.
	Vanilla bosses get a Buff per world level after killing them the first time. (The buff increases their Tier by 1 per world level)
	Fixed Shadows in Mistlands not dropping class weapons, also fixed them not using all of the class weapons.
	Fixed Rylan class drops not having a Rarity.
	Moved Shadow people in Mistlands to the 3 New Locations.
	Fixed Greater Surtling Trophy not working for Epic Loot.
	Voidlings and Stormlings can spawn in Mistlands.
	Removed VLS Config and added to incompatible list, messes with Epic Loot. (Cant have that, this pack is based around EL)
	Updated to latest version of Hugo's Armory.
	
**1.2.1**

  Tweaked Haldor's random quest rewards so they are not so majestic.
  Removed Skeleton Trophy from Tormented.
  Removed Duplicated Gray Wolf and added Tormented to Bounties.
  
**1.2**

  Removed VLS Req. Config still included for ya all that use it.
  Tweaked VLS Config.
  Enabled Zena and Neige Boss raids. (Ash Lands and Deep North)
  Reduced the distance DoD Ores spawn from each other.
  Fixed Flame Spider and Lava Golem not spawning in Ash Lands.
  Tweaked Haldor config for random quests.
  Fixed Obsidian Golem not waking up.
  Added config for Potions Plus. (Rebalanced for DoD)
  Tweaked Item Sets.
  Tweaked Dual Wield settings.
  Moved V50 over to yaml and deleted the defunct folder from the zip.
  Enabled 'raids end when mobs die', no more hiding.
  Fixed Flametal Armor (Mistlands armor from Jude) crafting costs.
  Fixed a couple of Judes items that were set to self destroy once durability reached 0.
  General bug Fixes. (Duplicate Drops, Typo's in Drop/Spawns etc)	  
  Quests added to Hugin:	
  86 Kill Quests.
  3 Gardening Quests.
  9 Mining Quests.
  2 Collection Quests.
  5 Wood Cutting Quests.
  1 Fishing Quest.
  8 Gather Quests.
  47 Cooking Quests.	
  Rewards: Coins, Foods, Class Items, Meads, Potions, Infused Gemstone & Grey Pearl.
	
**1.1.19**

	Fixed up the CLLC Yaml's.
	Oak trees drop Hardwood.
	Ancient trees have a chance of dropping Hardwood.
	Stopped Rylan event happening in the Ocean.
	Updated Kraken json and added it to the World Scaling.	
	Tweaked Forest Wendigo and Swamp Wendigo.
	Green Drakes can be found in the Swamp.
	Black Drakes can be found in the Plains.
	Dark Drakes spawn in the Meadows, Black Forest, Swamp, Mountains and Plains, after killing Moder.
	Added Vegetation to Forgotten Biomes, mistlands.veg.
	Added Oak to Plains via forgotten Biomes, plains.veg.
	Added Armor and Weapon kits to Chests, Black Forest to Plains.
	Added more rocks and Fir Trees to Deep North.
	Disabled, Slow, Reflect and all LowHealth effects from Epic Loots enchanting. They will be used for Item Set's only.
	
	Contributors - Cepera.
	
**1.1.18**

	Tweaked Nomad spawns.
	Fixed Flamerake and Greater Surtling not dropping Flametal Ore.
	Laughing Rylan's minions spawn less often. (Thief, Brigand etc)
	Added Armor and Weapon Kits to the Trader for Coins or Forest Tokens.
	Included a config for Discard Inventory Items. User preference on the use of this mod.
	
**1.1.17**

	TS Location Change.
	
**1.1.16**
	
	Fixed broken Chaos armor JSON.
	Fixed wrong material for Magic Overhaul Helmets.
	Fixed 2 broken Thief drops.
	Updated recipies.json.
	Updated adventuredata.json.
	Changed Thief to spawn during the day, compatibility with Epic Loot.
	Fixed broken Plains Spawners.
	Fixed some DoD bosses not droping Runestones.
	Disabled some Forgotten Biome options that might have been causing issues.
	Reduced Damage from Ash Rain and Freezing Winds by 50%.
	
	Thanks Cepera for this update :)
	
**1.1.15**

	Fixed Broken ItemConfig file for Judes Equipment.
	
**1.1.14**

	Fixed Mistlands Wendigo spawn.
	Fixed Nomad Armor having wrong Armor values.
	Updated Dragon Armor to fit DoD. It's the new Meta.
	
**1.1.13**

	Added Valheim Leveling as a requirement.
	Updated leveling config for DoD.
	Stopped Magic Overhaul Altar spawning in the water.
	Re-enabled Wendigo's.
	Pushed most hard spawns that spawn in eary biomes out to 5000m or greater from center.
	Fixed alot of broken Loot drops.
	Added Ice and Flame Drakes from DoD Monsters.

**1.1.12**

	Rebalanced Nomads.
	Moved CLLC Yaml's directly into plugins, no subfolders this was causing them to not load.
	Included a config for the Dual Wield mod. User choice on the use of this mod.
	
**1.1.11**

	Fixed Kraken.
	Fixed Nomads.
	Remove one off restriction on Anubisath.
	Fixed Grail Thornheart, part 2.
	Tweaked Mistlands to Ash Lands rewards from bounties.
	Added Class items to Epic Loot Trader's, Sales.

**1.1.10**

	Fixed Cinbri set not dropping.
	Fixed broken bounties. 
	Fixed Broken Thief drops.
	Added Kraken.
	Temp fix for Grail Thornheart.
	
	Thanks Cepera for this update :)
	
**1.1.9**

	Added DoD Trophie's to Epic Loots disenchanting.
	Moved Mistlands Armor up to Tier 8, renamed to Flametal Armor.
	Nomad Armor moved up to Tier 7.
	Babarian Armor moved down to Tier 5.5.
	Updated AdventureData for Epic Loots Trader.
	Tweaked Laughing Rylan.
	Stopped Forest Wolf from spawning withing 5000 meters of the center.
	
**1.1.8**

	Fixed oopsy with DK Skill 2.
	Removed Unused JSON's from the zip.
	Added hunting Nomads, they come in pairs.
	Reduced Barbarian skill 4 use to 33 and reduced speed bonus to 80%.
	Decreased Barbarian skill 3 stamina reduction to 65%.
	Decreased Monk skill 3 bonus to 50%.

**1.1.7**

	Removed Glutton.
	Added Less Food Degradation.
	Added Infused Gemstone drops to all bosses.
	Changed one M.O Helmet material. Water Globe to Infused Gemstone.
	Tweaked Berserker Skill 4.
	Tweaked DK Skill 2.
	Quick fix for bugged status effects.
	
**1.1.6**

	Tweaked Magic Overhaul's, Helm's and Weapon's. (See Readme/Description)
	Tweaked Epic Loot magic effects. (Block/Parry)
	Fixed borked legendaries.json
	Fixed oopsy in cllc config.
	Magic Essence things got stolen, beware.
	Tweaked class item drop rates in dungeons.
	Fixed Skir Sandburst altar not spawning.
	Fixed broken Ash Lands scaling yaml.
	
**1.1.5**

	Disabled CLLC Loot System.
	Fixed Eikthyr set drops and name.
	Fixed Giantstalker Set.
	Fixed Plagueheart Set.
	Tweaked MM Monsters.
	
**1.1.4**

	Increased drop rate on summoning items.
	Reduced DK Max Souls to 75 and Skill 4 Multiplier to 25%.
	Fixed some typos in legendaries.json.
	Added Boss summoning items to custom mobs, like Wild Boar.
	Updated for Judes Equipments: Wanderer Armor.
	Moved Treasure Chest drops over to EL's loottables.json.
	
**1.1.3**

	Fixed broken MonsterMash mobs.
	Fixed wakeup call on Elderlings.
	Tweaked Rylan and Zena Raids.
	Increased Item Set drop chance on Bosses.
	Tweaked Magic Overhaul ability damages.
	Tweaked Mini Boss abilities.
	Increased Max Immunity from enchantments to 50%
	Increased Max Stamina and Health from enchantments to 75.
	Updated for Magic Overhaul 1.5.0.
	
**1.1.2**

	Added unique items for DoD Boss summoning to various mobs around the world.
	Added unigue items to choose Magic Overhaul classes to all chests.
	Reduced mana regeneration for all classes included in Magic Overhaul to 3.	
	Reconfigured recipies for all Magic Overhaul items.
	Reduced armor from Magic Overhaul helmets to 38.
	Reduced damage from all Magic Overhaul weapons to 90.

**1.1.1**

	Updated for H&H patch.
	Removed left over Terrahiem changes in Adventuredata.json.
	Removed iteminfo.json, no longer need the additions.
	
**1.1.0**

	Added Treasure Chests to DropThat lists.
	Added V50 drops to DropThat Lists.
	Added Treasure Chest spawns to Mistlands, Deep North and Ash Lands.
	
**1.0.9**

	Fixed Laughing Rylan event.
	Removed base only restrictions on Zena and Laughing Rylan events.
	Fixed DropThat files having wrong naming and preventing drops.
	Fixed Forest Wolf and Molluscan having a cross over IndexNumber.
	Fixed Lich JSON.
	Adjusted Vanilla loot drops.
	
**1.0.8**

	Removed Biome Restrictions from Zena and Laughing Rylan events.
	Zena and Laughing Rylan events can be repeated.
	Redone Drops, moved to DropThat Lists and Drops. (This change requires a complete uninstall of DoD SE before upgrading)
	Reduced Boss drop rates down to 1-2 Random and 1-2 Set Items since they are now repeatable.
	Replaced Skeletons in Swamp with Skeleton (Green Eyes)
	Replaced Skeletons that spawn after killing Bonemass with Skeleton (Red Eyes)
	Updated Spawns to use new reskins from DoDMonsters.
	Included Bears and Glutton configs.
	Removed Monsternomicon compatability yamls. (Will re add if author returns after 16th Sept Patch)
	
**1.0.7**

	Fixed broken Eikythr item set.
	Fixed DropThat 1080 duplication ID.
	Added Skir Sandburst Altar and removed event. (Need to kill Goblin King, requires 12 Fuling Shaman Trophies, spawns in Plains in Stone Villages)
	Added Tier 1 Boss and Altar to Meadows, Ram-bore. (Need to kill Eikythr, requires 20 Raw Meat, spawns in Farms)
	Added Tier 4 Boss and Altar to Mountains, Farkas. (Need to kill Moder, requires 30 Wolf Fang, spawns near Graves and Wells)
	Thief, Bandit, Scoundrel, Shadow, Assassin and Stalker have a chance of dropping Grey Pearls.
	
**1.0.6**

	Fixed Skri Sandburst event.

**1.0.5**

	Replaced Greater Fenring for Gray Wolf (in werewolf form) in Deep North.
	Added White, Grey and Black Werewolves to Deep North.
	Added Forest Wendigo to Mistlands.
	Added Slimy Wendigo to Swamp.
	Added Brown Werewolf to Plains.
	Added Yeti to Mountains.
	Removed Forgotten Biomes location and vegitation configurations. (Most of the changes were not working anyways)
	Added Bitterstump Altar spawn. (Need to kill Eikthyr, requires 25 Dwarf Eyes, spawns in Black Forest near Greydwarf Nests (50% chance))
	Added Bhygshan Altar spawn. (Need to kill The Elder, requires 50 Bone Fragments, spawns in Swamp near Bonepiles (50% chance))
	Removed Bitterstump and Bhygshan Raid.
	Renamed V50 Monsters.
	Fixed scaling world level difficulty in Ash Lands, Deep North and Mistlands.
	Included Magic Overhaul config. (Feedback welcome, under testing)

**1.0.4**

	Updated Description with manual install instructions.
	Changed folder names to match those given by Thunderstore.
	Added Charred Bones to Ash Lands.
	Added two Skeleton variants to Swamp.

**1.0.3**

	Removed Fire Ring and added Firestike to Skeleton Mage.
	Updated to Drop That 2.0+.
	Fixed Giantstalker Chest not dropping.
	Fixed wrong reference to Plant Everything.

**1.0.2**

	Removed Zethpack Requirement and Loot drops.
	Removed Terrahiem Requirement and Loot drops.
	Added Barbarian Armor and Plate Armor as requirements.
	Hostile NPC's in Mistlands now drop Felmetal Ore.
	Mobs in Deep North now drop Frometal and Felmetal Ore.
	Added Black Chitin as a drop on all Mistlands, Deep North and Ash Lands spiders.
	Added Felmetal, Steel, Forest Wolf Pelt and Black Chitin as requirements for Plate Armor.
	Added Felmetal, Frometal and Dire Wolf Pelt as requirements for Barbarian Armor.
	Plate Armor Stats increased to tier 6. (32AC + 2 per level)
	Barbarian Armor stats increased to tier 7. (38AC + 2 per level)
	Bosses and Mini Bosses can now use Status Effects.
	Chaos Armor recipe Rework:
		Mistlands Armor - Felmetal, Steel, Forest Wolf Pelt, Voidling Core - (34AC + 2 per level, 5 levels)
		Deep North Armor - Felmetal, Frometal, Dire Wolf Pelt, Voidling Core + Mistland Armor - (40AC + 2 per level, 5 levels)
		Ashlands Armor - Felmetal, Flametal, Dire Wolf Pelt, Voidling Core + Deep North Armor - (46AC + 2 per level, 5 levels)
	Added Bear to Mountains, can cause injuries.
	Added Mountain bear to Deep North, can cause injuries.
	Vilefang can spread Infected.
	Dire Wolf can cause Injuries.
	Lich can spread Frostbite.
	Added missing Biomes: Mistlands, Deep North and Ash Lands and all RRR Mobs to World Level Scaling Difficulty.
	Added Missing mobs to Creature YAML.
	Added Custom Weapons to Mini Bosses that did not already have one.
	Added Felmetal deposits to Mistlands.
	Added Frometal deposits to Deep North.

**1.0.1**

	Included DoD Monsters as a requirement.
	Fixed some broken Item Set drops.
	Added Bone-Appe-tit material drops to Boar, Neck and Drake.
	Updated Obsidian Golem, Vilefang, Dire Wolf and Forest Wolf to Custom Models, with cubs.
	Added Voidling (Misty Nights in Plains), Frostling (Snowstorms in Mountains) and Stormling (Thundertorms in Black Forest).
	Added Living Water (Bad Weather, Near Water: from Meadows upto Mistalnds)
	Added Living Lava to Ashlands.
	Renamed Fire Golem and Ice Golem to Fire Elemental and Ice Elemental.

**1.0.0**

	Initial Thunderstore Release.
	Updated to DoD Assets 0.4.
	Updated Mini Bosses, Skeleton Mage and Lich to use the new Abilities included in DoD Assets.