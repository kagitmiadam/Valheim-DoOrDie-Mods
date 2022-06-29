
## Authors Notes

**12/06/22**

**MAJOR CHANGE - Changed from Epic Loot to Jewelcrafting**

**New Character and World is REQUIRED for the upgrade to 2.0.0**

**DELETE the Do or Die SE config folder before UPDATING to 2.0.0**


## Brief Overview

	Allows upto 13 Star Mobs and Bosses to spawn depending on world and sector level. 
	5 New Bosses. (See Below)
	3 New Boss Invasions. (see below) 
	5 Star and over start spawning at world level 1. (Adjust as you see fit in the CLLC config) 
	Populates Mistlands with Vilefang, Brown Spider and Forest Spiders. 
	Poulates Deep North with Gray Wolf (NPC), Dire Wolf, Ice Drake, Ice Spider, Awakened Werewolves. 
	Populates Ashlands with Lava Golen, Obsidian Golem, Greater Surtling, Flame Spider and Flame Drakes. 
	Adds Frost Spider, Ghost Warrior and Skeleton Warrior to Mountains. 
	Adds Forest Spider to Black Forest. (Elder death trigger) 
	Adds Forest Wolf to Meadows and Black Forest. (Moder death trigger) 
	Each Biome has 2 NPC Mini Bosses, one in the day and one in the night. (see below) 
	
	
## Installation

**Auto**

	Use either TSMM or R2MM.
	
	Thunderstore Mod Manager Profile Location:- C:\Users\USER_NAME\AppData\Roaming\Thunderstore Mod Manager\DataFolder\Valheim\profiles\PROFILE_NAME\BepInEx. 
	
	R2MM Profile Location:- C:/Users/USER_NAME/AppData/Roaming/r2modmanPlus-local/Valheim/profiles/PROFILE_NAME/BepInEx. 

**Manual** 

	Download the Zip. Open it up and Drag the contents into main installs, BepInEx folder and overwrite when asked. 	
	
	....steampapps/common/Valheim/BepinEx 

For further assistance you can find me on my offical Discord:- https://discord.gg/7BcEZXhRbV


## Questing

Uses Aedenthorn's Questing system as a soft requirement. (Includes cfg's and custom quest files, use down to user)
For questing to work you will require these 2 mods from nexus mods. 

- [Questing Framework](https://www.nexusmods.com/valheim/mods/1583)
- [Hugin Quests](https://www.nexusmods.com/valheim/mods/1588)

Install them in to your profiles, BepInEx\Plugins folder, or, your main games BepInEx\Plugins folder if you manually installed all your mods.

Integrated Bone Appetit and Potions Plus items into the DoD Quest's as rewards. These mods will be required. I know they are in the mod list, I also know a few of you play without them, take this as a pre warning.

Hugin will offer a quest every 20 mins. There is a mix of Killing, Gathering, Cooking, Mining, Collecting and Gardening quests, well more like Tasks tbh, 161 in total.


## Known Issues

**Better Raids**

In com.alexanderstrada.rrrbetterraids.cfg, I have the option: UpdateEventMobMinimapPins option set to false. This is to prevent an error showing in the log when you leave the raid event zone with mobs still alive. You can turn this option on as it does not look to effect game play in anyway. Upto User.

**Questing**

Random Quests can be odd. Issue with custom mobs not registering for kill quests. Been looked at.


## Incompatible Mods

- ValhiemLib and by extension any mod that still uses this defunct library.
- JotunnLib and by extension any mod that still uses this defunct library.
- ValheimPlus (Conflicts with some of the dependencies)
- Valhiem Level System (Conflicts with Epic Loot and Dual Wield)
- Serverside Simulations (Breaks Spawners)


## Mods that can cause Issues

- Simply Recycling can break Epic Loot/Tombstone depending on mods used.
- Terrahiem. (Can cause NRE Spam due to clashing)
- Forgotten Biomes. (Can  cause NRE Spam due to clashing)


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
  
	Thumbs up to Twenty-One for the Epic MMO xp json.

## Support

- If you like and use this ModPack, show you support by leaving a like for each mod included. Cheers and happy hunting!
	
- If you wish to donate please donate to any of the modpacks dependancies before myself.
  

<a href="https://www.buymeacoffee.com/horemvore"><img src="https://img.buymeacoffee.com/button-api/?text=Buy me a coffee&emoji=&slug=horemvore&button_colour=FFDD00&font_colour=000000&font_family=Cookie&outline_colour=000000&coffee_colour=ffffff" /></a>


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

To Do


## Magic Overhaul Class Changes

**Spartan**
	
	Type: Starter
	Item: None
	Helmet: 32AC 
	Weapon: 75D
	Mana Regen: 1

**Death Knight**
	
	Type: Starter
	Item: None
	Helmet: 32AC 
	Weapon: 75D
	Mana Regen: 1

**Archer**

	Type: Ranged
	Item: Essence of the Archer
	Helmet: 34AC
	Weapon: 95D
	Mana Regen: 2

**Berserker**

	Type: Melee
	Item: Essence of the Berserker
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2

**Druid**

	Type: Magic
	Item: Essence of the Druid
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3

**Mage**

	Type: Magic
	Item: Essence of the Mage
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3

**Monk**

	Type: Defense
	Item: Essence of the Monk
	Helmet: 40AC
	Weapon: 65D
	Mana Regen: 2

**Ninja**

	Type: Melee
	Item: Essence of the Ninja
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2

**Paladin**

	Type: Defense
	Item: Essence of the Paladin
	Helmet: 40AC
	Shield: 175 Block Power
	Mana Regen: 2

**Rogue**

	Type: Melee
	Item: Essence of the Rogue
	Helmet: 38AC
	Weapon: 90D
	Mana Regen: 2

**Shaman**

	Type: Magic
	Item: Essence of the Shaman
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3

**Warlock**

	Type: Magic
	Item: Essence of the Warlock
	Helmet: 35AC
	Weapon: 80D
	Mana Regen: 3


## Bosses

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
	
	
- Recommened a new Character and World although it is not required. Use on a existing world at your own risk. 
- You can Limit the risk by seeing what day your world is on and comparing that with the world level listed above and adjusting the CLLC config for World Level Days.


## Recommended Mods

- Passive Powers By Smoothbrain(Config Included)
- Backpack Redux by Aedenthorn (Config Included)
- Supplemental Raids
- TripleBronzeJVL
- Odins QOL
- Instant Monster Loot Drop


## Indexes

	Uses SpawnThat Indexes 950-1100.
	Uses DropThat Indexes 1000-1500 for Epic Loot additions.
	Uses DropThat Indexes 50-60 for additions to chests.
	
	
## Patch Notes

**2.0.4**

	Added missing Drops for Fantasy Creatures.
	Added more items to Knarr.
	
**2.0.3**

- Added more drops to the Wendigo, Yet and Werewolves
- Removed the double DLL for DoD SE in the Quest Framework folder (You will need to manual delete this folder)
- Slight adjustments to some monsters difficulty

**2.0.2**

- Added Fancy Food dependancy.
- Updated Jewelcrafting config.
	
**2.0.1**

- Fixed Giant's scaling with CLLC.

**2.0.0**

- Added Dual Wield, Starvation, Minotaurs, Deds Army, Jewelcrafting, Evasion, Tenacity, Mining, Vitality, Blacksmithing and Cooking as dependancies.
- Swamp monsters changed to Deds Army mobs once Bonemass is killed. (World Spawns)
- Zarathos spawns once Bhygshan is killed.
- Added all mobs to Epic MMO's XP System.	
- Removed Epic Loot Dependancy.

