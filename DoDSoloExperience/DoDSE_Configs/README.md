
## Authors Notes

**04/03/22**

Removed RRR Mods, due to this there will be some mobs and bosses missing from the biomes untill I get time to re do all the RRR attacks via Unity and Clone the mobs via JvL.

DELETE the Do or Die SE config folder before UPDATING to 1.3.0.


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

**Manual** 

	Download the Zip. Open it up and Drag the contents into your profiles(Mod Managers) or main installs (Manual), BepInEx folder and overwrite when asked. 
	
	Thunderstore Mod Manager Profile Location:- C:\Users\USER_NAME\AppData\Roaming\Thunderstore Mod Manager\DataFolder\Valheim\profiles\PROFILE_NAME\BepInEx. 
	
	R2MM Profile Location:- C:/Users/USER_NAME/AppData/Roaming/r2modmanPlus-local/Valheim/profiles/PROFILE_NAME/BepInEx. 


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

**Loot**

Random Legendaries showing up instead of Set Items. Under investigation.

**Questing**

Random Quests can be odd. Issue with custom mobs not registering for kill quests. Been looked at.

**Epic Loot**

Mildly effects performance. (10-15 fps loss)
Can cause High Latency on servers.


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
  

## Support

If you like what I do and want to support me.

<a href="https://www.buymeacoffee.com/horemvore"><img src="https://img.buymeacoffee.com/button-api/?text=Buy me a coffee&emoji=&slug=horemvore&button_colour=FFDD00&font_colour=000000&font_family=Cookie&outline_colour=000000&coffee_colour=ffffff" /></a>


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

**1.3.4**

	Removed a bunch of config files that made their way into the zip from my testing.
	Added Set drops for Mistlands, Deep North and Ash Lands monsters included in Giants, Minotaurs and Fantasy Creatures.
	Tweaked MO Classes.
	
**1.3.3**

- Fix for Priest.

**1.3.2**

- Included correct EpicMMOSystem config.

**1.3.1**

- Removed the Build mods, down to user to choose which they want to use, now there are so many.
- Updated for Magic Overhaul 1.5.5
- Added Epic MMO System.
- Added Fantasy Creatures.
- Added Farmyard Animals.
- Added Giants.
- Added Supply Crates.
	
**1.3**

- Removed RRR Mods.
- Updated for new DoD Dll's.
- Moved NPC spawns to the NPC Mod.
- Moved Reskined mobs spawns to the Monster Mod.
- Moved Mini Boss scaling to the NPC mod.
