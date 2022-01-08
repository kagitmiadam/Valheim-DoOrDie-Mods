
## Description

	Includes monsters from MonsterMash:- 3 Variants of Werebear, Yeti, Wizard, Crawler, Forest and Swamp Wendigo, 5 Variants of Werewolf and The Reaper. (Requires a mod like SpawnThat to make use of them)
	Adds 6 Drake, 4 Skeleton, 3 Surtling, 3 Golem, 3 Wolf, 3 Wolf Cub, 2 Blob and 2 Ghost reskins. (Requires a mod like SpawnThat to make use of them)
	Adds 10 Trees, 6 Rocks and 4 Pickables and some non interactable Flora to Mistlands.
	Adds 13 Rocks to Ashlands.
	Adds 10 Rocks, 3 Bushes and 7 Trees to Deep North.
	Adds Banana Trees to Plains and Swamp.
	Adds Apple Trees to Black Forest.
	
For further assistance you can find me on my Discord:- https://discord.gg/7BcEZXhRbV


## Special Thanx

	Zarboz for letting me include his monsters from Monster Mash along with his code.
	DefendYourBase for sharing C# code to get all these monsters/prefabs into Valheim.
	Jules for his helpful insight and help with JvL.
  Hugo the Dwarf, Balrond, Shadow84, GoldenJude and anyone else I have forgotten for their help with issues I posted in the Valhiem Modding Discord.

## Credits

	VitByr for the Russian Translation.
	
## Servers

Required on both server and client, uses JvL Network Compatibility.


## Incompatible Mods

- ValhiemLib and by extension any mod that still uses this defunct library.
- JotunnLib and by extension any mod that still uses this defunct library.
- Slop Hit FIX breaks custom Monsters attacks.


## Known Issues

- Status Effects are currently broken/disabled.
- Some class weapon secondary attacks are unbalanced due to this (Not on the OP side).

**Forgotten Biomes**

Requires vegetation to be off in Mistalnds or it will get Crowded. (Or disable Mistlands Vegetation in the DoDMonsters.cfg, user preference)


## Boss Monsters

**Ram-bore**

	Faction: Boss
	Biome: Meadows
	Abilities: Gore. (20 Damage)
	725 base Health
	Afflictions: Lesser Bleeding
	Model: Dark Boar resking with Red Eyes and Shimmering Tusks.
	Summon Type: Lair, 5 Boar Tusk.

**Bitterstump**

	Faction: Boss
	Biome: Black Forest
	Abilities: Aoe Heal, Poison Spray, Cold Spray and Roots. (75-100 Damage, Heals for 100)
	2250 base Health
	Afflictions: Frostbite, Poison
	Model: Pink/Purple Greydwarf Shaman Reskin with a shimmering Tree Stump.
	Summon Type: Altar, 5 Greydwarf Hearts.

**Bhygshan**

	Faction: Boss
	Biome: Swamp
	Abilities: Summon Skeletons, Fire Bolt, Fireball, Cold Spray and Posion AoE. (125 - 170 Damage)
	4250 base Health
	Afflictions: Frostbite, Poison, Blistered
	Model: Shimmering Rancid Remains reskin.
	Summon Type: Altar, 5 Skeleton Bones.

**Farkas**

	Faction: Boss
	Biome: Mountains
	Abilities: Wounding Strike, Frost Bite, Hounding Strike and all 3 normal attacks can chain. (170 - 200 Damage)
	5775 base Health
	Afflictions: Bleeding, Slow, Frostbitten
	Model: Dark Grey Wolf reskin with red eyes and white/grey smoke.
	Summon Type: Lair, 5 Large Fang.


**Skir Sandburst**

	Faction: Boss
	Biome: Plains
	Abilities: AoE Heal, AoE Shield, Summon Voidling, Summon Forest Wolf, Firebolt, Fireball, Voidstorm and Nova. (210 - 275 Damage, Heals for 200)
	7225 base Health
	Afflictions: Hexed, Blistered
	Model: Pink/Orange Goblin Shaman reskin.
	Summon Type: Altar, 5 Goblin Foci.

	
## Magic Overhaul Themed Additions

New class item for each class included for use with Magic Overhaul: Essence of *Class* 

**Wands (Shaman, Mage, Warlock)**

	Durability: 150 + 50 per level
	Durability Use: 2 STA
	Max Level: 10
	Knockback: 15/30
	Block Power: 15 + 5 per level
	Deflection Force: 30 + 5 per level
	Primary Attack:
		Type: Projectile
		Lifetime: 1s
		Velocity: 15
		Damage:	10 + 10 per level
		Cost: 12 STA
	Secondary Attack:
		Type: Projectile
		Lifetime: 3s
		Velocity: 30
		Cost: 33 STA
		Damage: 3x Primary
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	Sounds and VFX: Mostly Placeholders.
	
**Divine Mace (Paladin)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 25
	Block Power: 15 + 5 per level
	Deflection Force: 30 + 5 per level
	Primary Attack:
		Type: Default Mace
		Damage:	13 + 8 per level
		Bonus Damage: 3 + 4 per level vs Undead
		Cost: 10 STA
	Secondary Attack:
		Type: Default Mace Secondary
		Special: Static AoE
		Range: 6m
		Duration: 5s
		Interval: 1s
		Cost: 100 STA
		Damage: 3 + 5 per level
		Bonus Damage: 4 + 4 per level vs Undead
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Assassin's Blade (Rogue)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 25
	Block Power: 15 + 5 per level
	Deflection Force: 30 + 5 per level
	Primary Attack:
		Type: Short Sword:
		Range: 2m
		Speed: 0.266 (Sword is  0.3)
		Damage:	13 + 8 per level
		Backstab: x4
		Cost: 10 STA
	Secondary Attack:
		Type: Jump
		Special: Caster Aura
		Range: 6m
		Duration: 5s
		Interval: 1s
		Cost: 100 STA
		Damage: 2 + 2 per level
		Special: Slows Enemies by 50% for 10s
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Void Sword (Ninja)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 50
	Block Power: 12 + 12 per level
	Deflection Force: 30 + 5 per level
	Timed Block Bonus: 75%
	Primary Attack:
		Type: 2H Katana:
		Range: 2.7m
		Speed: 0.25 (Sword is  0.3)
		Damage:	18 + 11 per level
		Bonus Damage: 4 + 4 per level vs Undead
		Backstab: x3
		Cost: 15 STA
	Secondary Attack:
		Type: Jump Attack
		Damage: Landing Area
		Damage: 2x Primary
		Special: Static AoE
		Range: 6m
		Duration: 5s
		Interval: 1s
		Cost: 100 STA
		Damage: 8 + 5 per level
		Bonus Damage: 3 + 2 per level vs Undead
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Striking Mace (Monk)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 60
	Block Power: 15 + 15 per level
	Deflection Force: 30 + 5 per level
	Timed Block Bonus: 25%
	Primary Attack:
		Type: 2H Mace:
		Range: 3m
		Speed: 0.2 (Sword is  0.3)
		Damage:	17 + 11 per level
		Backstab: x2.25
		Cost: 15 STA
	Secondary Attack:
		Type: Frontal Cone (120 degree)
		Damage: 4x Primary
		Knockback: 5x Primary
		Stagger: 3x Primary
		Range: 3m
		Duration: Instant
		Cost: 100 STA
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Acid Spear (Druid)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 60
	Block Power: 12 + 8 per level
	Deflection Force: 30 + 5 per level
	Timed Block Bonus: 50%
	Primary Attack:
		Type: 2H Spear, single stab forward.
		Range: 3m
		Speed: 0.225 (Sword is  0.3)
		Damage:	17 + 11 per level
		Backstab: x2.25
		Cost: 17 STA
	Secondary Attack:
		Type: Spin
		Damage: 1.75x Primary
		Knockback: 6x Primary
		Stagger: 4x Primary
		Range: 3m
		Duration: Instant
		Cost: 100 STA
		Special: Healing AoE on completion of spin.
			Type: Static
			Healing: 5 + 1 per level
			Interval: 5s
			Duration: 60s
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Raging Battleaxe (Berserker)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 70
	Block Power: 15 + 11 per level
	Deflection Force: 30 + 5 per level
	Timed Block Bonus: 75%
	Last Chain Multiplier: 125%
	Primary Attack:
		Type: 2H Spear, single stab forward.
		Range: 2.6m
		Speed: 0.15 (Sword is  0.3)
		Damage:	18 + 12 per level
		Backstab: x2
		Cost: 18 STA
	Secondary Attack:
		Type: Spin
		Speed: 0.2 (Sword is  0.3)
		Damage: 5x Primary
		Knockback: 3x Primary
		Stagger: 6x Primary
		Range: 3m
		Duration: Instant
		Cost: 150 STA
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
	
**Chilling Axe (Deathknight)**

	Durability: 200 + 50 per level
	Durability Use: 1 STA
	Max Level: 10
	Knockback: 35
	Block Power: 15 + 5 per level
	Deflection Force: 30 + 5 per level
	Last Chain Multiplier: 150%
	Primary Attack:
		Type: 1H Battleaxe
		Range: 2.3m
		Speed: 0.2 (Sword is  0.3)
		Damage:	13 + 9 per level
		Backstab: x2.75
		Cost: 14 STA
	Secondary Attack:
		Type: Spin
		Speed: 0.3 (Sword is  0.3)
		Damage: 3.5x Primary
		Knockback: 4x Primary
		Stagger: 3x Primary
		Range: 3m
		Duration: Instant
		Cost: 75 STA
	Crafting Special Requirement: Class Item
	Additional Crafting Mats: Fine Wood, Bronze Bar, Infused Gemstone
				
**Sounds and VFX: Mostly Placeholders.**
	
	
## Monster Reskins and Changes

**Gold Drake**

	Faction: Players/Friendly
	Biome: Any
	Weapons: Firespit (200 Fire damage)
	Health: 800
	New Drops: Trophy
	Model: Yellow/Orange/White with Green Eyes.

**Purple Drake**

	Faction: Demon
	Biome: Ash Lands
	Weapons: Arcanespit (30 Fire and Lightning damage)
	Health: 1200
	New Drops: Trophy
	Model: Red/White with Orange Eyes.

**Flame Drake**

	Faction: Demon
	Biome: Ash Lands
	Weapons: Firespit (200 Fire damage)
	Health: 1500
	New Drops: Trophy
	Model: Red/White with Orange Eyes.

**Ice Drake**

	Faction: MountainMonsters
	Biome: Deep North
	Weapons: Frostspit (150 Frost damage)
	Health: 1000
	New Drops: Trophy
	Model: Dark Blue/White with Red Eyes.

**Green Drake**

	Faction: Undead
	Biome: Swamp
	Weapons: Poisonspit (25 Poison damage)
	Health: 200
	New Drops: Trophy
	Model: Green/White with Blue Eyes.

**Black Drake**

	Faction: PlainsMonsters
	Biome: Mistlands
	Weapons: Poisonspit (60 Poison damage)
	Health: 750
	New Drops: Trophy
	Model: Deep Black/Dark Grey with Green Eyes and Horns.
	
**Dark Drake**

	Faction: PlainsMonsters
	Biome: Mistlands
	Weapons: Voidspit (30 Frost, Lightning and Poison damage)
	Health: 500
	New Drops: Trophy
	Model: Black/Grey with Purple Eyes and Horns.

**Skeleton (Red Eyes)**

	Faction: Undead
	Biome: Swamp/Crypt
	Weapons: Sword or Bow (20 Physical and 15 Fire damage)
	50 base Health
	New Drops: Trophy
	Model: Deeper Colour with Red Eyes and Weapons.
	
**Skeleton (Green Eyes)**

	Faction: Undead
	Biome: Swamp/Crypt
	Weapons: Sword or Bow (25 Physical and 25 Poison damage)
	100 base Health
	New Drops: Trophy
	Model: Lighter Colour with Green Eyes and Weapons.
	
**Frozen Bones**

	Faction: MountainMonsters
	Biome: Deep North
	Weapons: Sword or Bow (75 Physical and 125 Frost damage)
	460 base Health
	New Drops: Trophy and Bones
	Model: Blue with Blue Eyes/VFX and Weapons.
		
**Charred Remains**

	Faction: Demon
	Biome: Ash Lands
	Weapons: Sword or Bow (85 Physical and 150 Fire damage)
	640 base Health
	New Drops: Trophy and Bones
	Model: Black with Orange Eyes/VFX and Weapons.

**Greater Surtling**

	Faction: Demon
	Biome: Ash Lands
	Firebolt Ability. (75 Blunt and 125 Fire damage)
	500 base Health
	New Drops: Trophy
	Model: Flowing, Orange/Red/Black with Custom Fire VFX/SFX

**Stormling**

	Faction: ForestMonsters
	Biome: Black Forest
	Stormbolt Ability. (10 Blunt and 25 Lightning damage)
	100 base Health
	New Drops: Stormling Core and Trophy
	Model: Flowing, Blue with Custom Lightning VFX and SFX

**Frostling**

	Faction: MountainMonsters
	Biome: Mountains
	Icebolt Ability. (40 Blunt and 50 Frost damage)
	200 base Health
	New Drops: Frostling Core and Trophy
	Model: Flowing, Teal with Custom Snow/Ice VFX and SFX

**Voidling**

	Faction: PlainsMonsters
	Biome: Plains
	Voidbolt Ability. (40 Frost, Lightning and Poison damage)
	300 base Health
	New Drops: Voidling Core and Trophy
	Model: Flowing, Very Dark Burgandy with Custom Arcane/Darkness VFX and SFX

**Obsidian Golem**

	Faction: Demon
	Biome: Ash Lands
	1800 base Health
	New Drops: Obsidian Golem Trophy
	Model: Black/Grey/White with Custom Texture

**Lava Golem**

	Faction: Demon
	Biome: Ash Lands
	1800 base Health
	New Drops: Lava Golem Trophy
	Model: Red/Orange/Black with Custom Flowing Texture and Red/Orange body smoke

**Ice Golem**

	Faction: MountainMonsters
	Biome: Deep North
	1300 base Health
	New Drops: Ice Golem Trophy
	Model: Teal/Blue/Black with Custom Flowing Texture and Teal/Blue body smoke

**White Ghost**

	Biome: Burial Chamber
	Same as Vanilla Ghost with Grey Smoke

**Teal Ghost**

	Immune to Frost
	Biome: Swamp
	180 base Health
	Model: Same as Vanilla Ghost with Teal Smoke

**Dire Wolf**

	Faction: MountainMonsters
	Biome: Deep North
	Attacks: 210 Damage (Physical/Frost/Poison)
	500 base Health
	New Drops: Dire Wolf Trophy and Pelt
	Model: Black/Grey with Red Eyes
	Procration: Dire Wolf Cub (20% chance, Duration: 120)

**Forest Wolf**

	Faction: ForestMonsters
	Biome: Mountain/Plains
	Attacks: 90 Damage (Physical)
	300 base Health
	New Drops: Forest Wolf Trophy and Pelt
	Model: Tan/Grey
	Procration: Forest Wolf Cub (30% chance, Duration: 75)

**Vilefang**

	Faction: PlainsMonsters
	Biome: Mistlands
	Attacks: 165 Damage (Physical/Poison)
	410 base Health
	New Drops: Vilefang Trophy
	Model: Black/Green with green eyes and green body smoke
	Procration: Vilefang Cub (15% chance, Duration: 90)

**Living Lava**

	Faction: Demon
	Biome: Ashlands
	Fire Nova Ability. (180 Fire Damage)
	500 base Health
	New Drops: Living Lava Trophy
	Model: Flowing Lava with Custom Hit/Death/Jump VFX and SFX.

**Living Water**

	Faction: Demon
	Biome: Swamp/Any
	Water Nova Ability. (80 Physical Damage and gives Wet status effect)
	150 base Health
	New Drops: Living Water Trophy and Globe of Water
	Model: Flowing Water with Custom Hit/Death/Jump VFX.

**Black Deer**

	Faction: Boss (So other factions dont attack them)
	Biome: Mistlands/Any
	200 base Health
	New Drops: Black Deer Trophy and Hide.
	Model: Grey/Black with Blue Smoke.

**Black Stag**

	Faction: Boss (So other factions dont attack them)
	Biome: Mistlands/Any
	200 base Health
	New Drops: Black Deer Trophy and Hide.
	Model: Grey/Black with Blue/Teal body smoke.

**All reskins use the same animation names the reskin is based off.**


## Status Effects

**Horrors**

	Duration: 30s
	Stamina: -2 per second (Can not be resisted)
	Jump Stamina Use: -75%
	Run Stamina Drain: -75%
	Cooldown: 120s
	AoE: No

**Hexed**

	Duration: 60s
	Health: -1 per second (Can not be resisted)
	Stamina: -1 per second (Can not be resisted)
	Cooldown: 120s
	AoE: No

**Infected**

	Duration: 60s
	Health: -1 per second (Can not be resisted)
	Stamina Regen: -25%
	Health Regen: -25%
	Cooldown: 120s
	AoE: Yes with matching AoE Projectile

**Diseased**

	Duration: 60s
	Health: -2 per second (Can not be resisted)
	Stamina Regen: -50%
	Health Regen: -50%
	Cooldown: 120s
	AoE: Yes with matching AoE Prefab

**Weak**

	Duration: 60s
	Stamina Regen: -35%
	Carry Weight: -100
	Cooldown: 120s
	AoE: Yes with matching AoE Prefab

**Flash Burn**

	Duration: 60s
	Stamina Regen: -15%
	Health Regen: -15%
	Cooldown: 120s
	AoE: No

**Blistered**

	Duration: 60s
	Stamina Regen: -30%
	Health Regen: -30%
	Cooldown: 120s
	AoE: No

**Haste**

	Duration: 12s
	Movement Speed: +50%
	Cooldown: 30s
	AoE: No

**Paralysis**

	Duration: 5s
	Movement Speed: 0
	Cooldown: 20s
	AoE: No

**Slow**

	Duration: 10s
	Movement Speed: -50%
	Cooldown: 20s
	AoE: No

**Frostbite**

	Duration: 25s
	Damage: 6 (Can be resisted)
	Interval: 1s
	Cooldown: 50s
	AoE: No

**Frostbitten**

	Duration: 25s
	Damage: 12 (Can be resisted)
	Interval: 1s
	Cooldown: 50s
	AoE: No

**Major Neuralgia**

	Duration: 25s
	Damage: 18 (Can be resisted)
	Interval: 1s
	Cooldown: 50s
	AoE: No

**Minor Injury**

	Duration: 25s
	Damage: 1 (Can not be resisted)
	Interval: 1s
	Cooldown: 50s
	AoE: Yes with matching AoE Prefab

**Injured**

	Duration: 25s
	Damage: 2 (Can not be resisted)
	Interval: 1s
	Cooldown: 50s
	AoE: Yes with matching AoE Prefab

**Major Injury**

	Duration: 25s
	Damage: 4 (Can not be resisted)
	Interval: 1
	Cooldown: 50s
	AoE: Yes with matching AoE Prefab

**Lesser HoT**

	Duration: 4s
	Health: 52
	Interval: 1s
	Cooldown: 12s
	AoE: Yes with matching AoE Prefab

**Heal Over Time**

	Duration: 4s
	Health: 104
	Interval: 1s
	Cooldown: 12s
	AoE: Yes with matching AoE Prefab

**Greater HoT**

	Duration: 4s
	Health: 208
	Interval: 1s
	Cooldown: 12s
	AoE: Yes with matching AoE Prefab

**Lesser Regeneration**

	Duration: 16s
	Health: 56
	Interval: 1s
	Cooldown: 32s
	AoE: Yes with matching AoE Prefab

**Regeneration**

	Duration: 16s
	Health: 112
	Interval: 1s
	Cooldown: 32s
	AoE: Yes with matching AoE Prefab

**Greater Regeneration**

	Duration: 16s
	Health: 224
	Interval: 1s
	Cooldown: 32s
	AoE: Yes with matching AoE Prefab

**Lesser Protection**

	Duration: 40s
	AbsorbAmount: 250
	Cooldown: 40s
	AoE: Yes with matching AoE Prefab

**Protection**

	Duration: 40s
	AbsorbAmount: 500
	Cooldown: 40s
	AoE: Yes with matching AoE Prefab

**Greater Protection**

	Duration: 40s
	AbsorbAmount: 1000
	Cooldown: 40s
	AoE: Yes with matching AoE Prefab

**Poisoned**

	Duration: 45s
	Damage: 1s
	Interval: 2s
	Cooldown: 90s
	AoE: No

## Resource Crates

**Armor Kits**

	Number of Tiers: 8 Starting with Bronze and ending with Flametal.
	Material 1: 30 Metal of relevant Tier
	Material 2: 5 Hide/Pelt
	Material 3: 5 Lower Tier Hide/Pelt
	Material 4: 10 Resin/Thread
	Weight: Weight of contents - 10%

- Used in Dod Buildables (New Anvils etc) and Items (Shields).
- Can be sold to the Trader for Coins.
- Can be Recycled for 75% of the materials back.
- Can be crafted, bought at the trader or found in chests.

**Weapon Kits**

	Number of Tiers: 8 Starting with Bronze and ending with Flametal.
	Material 1: 15 Metal of relevant Tier
	Material 2: 5 Hide/Pelt
	Material 3: 15 Wood
	Material 4: 10 Resin/Thread
	Weight: Weight of contents - 10%

- Used in Dod Buildables and Items. (Class Weapons)
- Can be sold to the Trader for Coins.
- Can be Recycled for 75% of the materials back.
- Can be crafted, bought at the trader or found in chests.

**Misc Kits**

	TBD

## Swords

	Max Level: 5 (4 Upgrades)
	Knockback: 30
	Damage: 120 (40 Slash + 3 per level, 80 Elemental + 8 per level)
	Block Power: 20 + 5 per level
	Deflection Force: 30 + 5 per level


**NOTE: Some of the Prefab and Item ID's are missing from this list, VFX/SFX mostly**
	
## Monster Mash ID's

- CrazyTroll
- EarthTroll
- Yeti
- CapeYeti
- YetiPelt
- Wizard
- WereWolfBlackArmored
- WereWolfDarkGrey
- WereWolfBlack
- WereWolfBrown
- WereWolfWhite
- WerewolfCape
- WereBearBlack
- WereBearGray
- WereBearRed
- WendigoForest
- WendigoSwamp
- EvilReaper
- Golem2
- TheNasty
- Nasty_Spawner


## Prefab ID's

**Locations**

- Loc_OreMine_DoD
- Event_StoneRing_Mistlands_DoD
- Loc_MistlandsTower_DoD
- Loc_MistlandsCave_DoD
- Loc_CastleArena_DoD
- Loc_Camp_DoD
- Loc_Boss_Rambore_DoD

**Bosses**

- SkirSandburst_DoD
- Farkas_DoD
- Bhygshan_DoD
- Bitterstump_DoD
- Rambore_DoD

**Summon Items** 

- ShamansVessel_DoD
- LargeFang_DoD
- SkeletonBones_DoD
- GreydwarfHeart_DoD
- BoarTusk_DoD

**Boss Altars**

- AltarRambone_DoD
- AltarBitterstump_DoD
- AltarBhygshan_DoD
- AltarFarkas_DoD, AltarFarkasAlt_DoD
- AltarSkirSandburst_DoD

**Monsters**

- GreaterSurtling_DoD
- CharredRemains_DoD
- SkeletonG_DoD
- SkeletonR_DoD
- FrozenBones_DoD
- BlackDeer_DoD
- BlackStag_DoD
- IceGolem_DoD
- LavaGolem_DoD
- ObsidianGolem_DoD
- Ghost_White_DoD
- Ghost_Ice_DoD
- Frostling_DoD
- Stormling_DoD
- Voidling_DoD
- ForestWolf_DoD
- ForestWolf_Cub_DoD
- DireWolf_DoD
- DireWolf_Cub_DoD
- Vilefang_DoD
- Vilefang_Cub_DoD
- LivingLava_DoD
- LivingWater_DoD
- IceDrake_DoD
- FlameDrake_DoD
- ArcaneDrake_DoD
- GoldDrake_DoD
- DarknessDrake_DoD
- PoisonDrake_DoD
- DarkDrake_DoD

**Buildables**

- FelmetalAnvils_DoD
- FrometalAnvils_DoD
- FlametalAnvils_DoD
- Rug_BlackDeer_DoD
- Rug_ForestWolf_DoD
- Rug_DireWolf_DoD

**Magic Overhaul Themed Weapons**

- ShamanWand_DoD
- MageWand_DoD
- WarlockWand_DoD
- PaladinMace_DoD
- RogueSword_DoD
- DeathknightAxe_DoD
- NinjaSword_DoD
- DruidSpear_DoD
- BerserkerAxe_DoD
- MonkMace_DoD
- DruidSpear_DoD

**Magic Overhaul Class Unlock Items**

- DeathKnightItem_DoD
- ArcherItem_DoD
- BerserkerItem_DoD
- DruidItem_DoD
- MageItem_DoD
- MonkItem_DoD
- NinjaItem_DoD
- PaladinItem_DoD
- RogueItem_DoD
- ShamanItem_DoD
- WarlockItem_DoD

**Trophies**

- TrophyIceDrake_DoD
- TrophyFlameDrake_DoD
- TrophyArcaneDrake_DoD
- TrophyDarknessDrake_DoD
- TrophyGoldDrake_DoD
- TrophyPoisonDrake_DoD
- TrophyFrostling_DoD
- TrophyStormling_DoD
- TrophyVoidling_DoD
- TrophyOGolem_DoD
- TrophyLGolem_DoD
- TrophyIceGolem_DoD
- TrophyVilefang_DoD
- TrophyDireWolf_DoD
- TrophyForestWolf_DoD
- TrophyLivingLava_DoD
- TrophyLivingWater_DoD
- TrophyBlackDeer_DoD
- TrophyFrozenBones_DoD

**Materials**

- FelmetalBar_DoD
- FelmetalOre_DoD
- FrometalBar_DoD
- FrometalOre_DoD
- SteelBar_DoD
- DireWolfPelt_DoD
- ForestWolfPelt_DoD
- FrostlingCore_DoD
- StormlingCore_DoD
- VoidlingCore_DoD
- WaterGlobe_DoD
- SpiderChitin_DoD
- BlackDeerHide_DoD

**Resource Crates**

- CrudeArmorKit_DoD
- BasicArmorKit_DoD
- GoodArmorKit_DoD
- GreatArmorKit_DoD
- SuperiorArmorKit_DoD
- ExcellentArmorKit_DoD
- ExceptionalArmorKit_DoD
- ExtraordinaryArmorKit_DoD
- CrudeWeaponKit_DoD
- BasicWeaponKit_DoD
- GoodWeaponKit_DoD
- GreatWeaponKit_DoD
- SuperiorWeaponKit_DoD
- ExcellentWeaponKit_DoD
- ExceptionalWeaponKit_DoD
- ExtraordinaryWeaponKit_DoD

**World Objects** 

- MineRock_FelOre_DoD
- MineRock_FroOre_DoD

**Shields**

- ShieldEikthyr_DoD
- ShieldRambore_DoD
- ShieldElder_DoD
- ShieldBitterstump_DoD
- ShieldBonemass_DoD
- ShieldSkullGreen_DoD
- ShieldModer_DoD
- ShieldFarkas_DoD
- ShieldSkir_DoD
- ShieldYagluth_DoD

**Shield Unlocks**

- BrokenShieldEikthyr_DoD
- BrokenShieldRambore_DoD
- BrokenShieldElder_DoD
- BrokenShieldBitterstump_DoD
- BrokenShieldBonemass_DoD
- BrokenShieldBhygshan_DoD
- BrokenShieldModer_DoD
- BrokenShieldFarkas_DoD
- BrokenShieldSkir_DoD
- BrokenShieldYagluth_DoD

**Magic Swords**

- SwordFlametal1_DoD
- SwordFlametal2_DoD
- SwordSpirit_DoD
- SwordFrometal1_DoD
- SwordFrometal2_DoD
- SwordFrometal3_DoD
- Stormblade_DoD
- SwordFelmetal1_DoD
- SwordFelmetal2_DoD

**AoE Prefab ID's**

- AoE_HoT50_DoD
- AoE_HoT100_DoD
- AoE_HoT200_DoD
- AoE_Regen50_DoD
- AoE_Regen100_DoD
- AoE_Regen200_DoD
- AoE_Protection250_DoD
- AoE_Protection500_DoD
- AoE_Protection1000_DoD
- AoE_Infected_DoD
- AoE_Diseased_DoD
- AoE_Weak_DoD

**Monster Abilities and Items**

- Bow_SkelG_DoD
- Sword_SkelG_DoD
- Bow_SkelR_DoD
- Sword_SkelR_DoD
- Bow_FrozenBones_DoD
- Sword_FrozenBones_DoD
- BlackDeer_Ragdoll_DoD
- IceGolem_Head_DoD
- IceGolem_Ragdoll_DoD
- IceGolem_Spikes_DoD
- IceGolem_Clubs_DoD
- LavaGolem_Head_DoD
- LavaGolem_Ragdoll_DoD
- LavaGolem_Spikes_DoD
- LavaGolem_Clubs_DoD
- ObsidianGolem_Head_DoD
- ObsidianGolem_Ragdoll_DoD
- ObsidianGolem_Spikes_DoD
- ObsidianGolem_Clubs_DoD
- ForestWolf_Ragdoll_DoD
- ForestWolf_Attack1_DoD
- ForestWolf_Attack2_DoD
- ForestWolf_Attack3_DoD
- DireWolf_Ragdoll_DoD
- DireWolf_Attack1_DoD
- DireWolf_Attack2_DoD
- DireWolf_Attack3_DoD
- Vilefang_Ragdoll_DoD
- Vilefang_Attack1_DoD
- Vilefang_Attack2_DoD
- Vilefang_Attack3_DoD
- livinglava_nova_attack_dod
- livingwater_nova_attack_dod
- imp_icebolt_attack_dod
- imp_stormbolt_attack_dod
- imp_voidbolt_attack_dod
- Imp_Icebolt_projectile_dod
- Imp_stormbolt_projectile_dod
- Imp_Voidbolt_projectile_dod
- NPC_NomadAoE_Attack_DoD
- Rambore_Attack_DoD
- Rambore_Gore_DoD
- SkirSandburst_FB_Attack_DoD
- SkirSandburst_FWSum_DoD
- SkirSandburst_Heal_DoD
- SkirSandburst_Nova_DoD
- SkirSandburst_Shield_DoD
- SkirSandBurst_VoidAttack_DoD
- SkirSandburst_VoidSum_DoD
- Farkas_Attack1_DoD
- Farkas_Attack2_DoD
- Farkas_Attack3_DoD
- Farkas_Bleed_DoD
- Farkas_FrostBite_DoD
- Farkas_Hamper_Attack_DoD
- drake_arcanespit_attack_dod
- drake_firespit_attack_dod
- drake_frostspit_attack_dod
- drake_poison_attack_dod
- drake_poisonspit_attack_dod
- drake_voidspit_attack_dod
- Bhygshan_AoE_DoD
- Bhygshan_Fireball_DoD
- Bhygshan_FireBolt_DoD
- Bhygshan_SprayFrost_DoD
- Bhygshan_Throw_DoD
- Bitterstump_Heal_DoD
- Bitterstump_Melee_DoD
- Bitterstump_Roots_DoD
- Bitterstump_SprayFrost_DoD
- Bitterstump_SprayPoison_DoD

**VFX**

- VFX_LivingLava_Attack_DoD
- VFX_LivingLava_Death_DoD
- VFX_LivingLava_Hit_DoD
- VFX_LivingWater_Attack_DoD
- VFX_LivingWater_Death_DoD
- VFX_LivingWater_Hit_DoD
- VFX_IceImpDeath_DoD
- VFX_IceImpHit_DoD
- VFX_StormImpDeath_DoD
- VFX_StormImpHit_DoD
- VFX_VoidImpHit_DoD
- VFX_ArcaneImpDeath_DoD
- VFX_Wolf_Death_DoD
- VFX_Wolf_Hit_DoD
- FX_Backstab_DoD
- FX_Crit_DoD
- VFX_Blocked_DoD
- VFX_Frostbite_DoD
- VFX_HitSparks_DoD
- VFX_Injured_DoD


## StatusEffect ID's - Currently Disabled

- SE_Horrors_DoD
- SE_Hexed_DoD
- SE_Infected_DoD
- SE_Diseased_DoD
- SE_Weak_DoD
- SE_FlashBurn_DoD
- SE_Blistered_DoD
- SE_Haste_DoD
- SE_Paralyze_DoD
- SE_Slow_DoD
- SE_MajorNeuralgia_DoD
- SE_Frostbitten_DoD
- SE_Frostbite_DoD
- SE_GreaterBleeding_DoD
- SE_Bleeding_DoD
- SE_LesserBleeding_DoD
- SE_GreaterShield_DoD
- SE_Shield_DoD
- SE_LesserShield_DoD
- SE_GreaterRegen_DoD
- SE_Regen_DoD
- SE_LesserRegen_DoD
- SE_GreaterHoT_DoD
- SE_HoT_DoD
- SE_LesserHoT_DoD
- SE_Poisoned_DoD


## Patch Notes

**0.4.3**

- Added new Rocks (10), Bushes (3) and Winter Trees(7) to Deep North.
- Added new Rocks (13) to Ash Lands.
- Added Ram'Bore Location (4 in the world, Meadows).
- Fixed camp spawning oddly.
- Fixed Arena groundleveling.
- Fixed Animation error in Mistlands and Deep North.
- Fixed Free loot spawning at the center ring.
- Added Russian Localization by VitByr
- Included DropThat configs for all new Trees, Bushes, Rocks, Chests and Pickables.
- Included example SpawnThat World Spawner configs for monsters added by DoD in: Mistlands, Deep North and Ash Lands.

**0.4.2**

- Valheim's 205.5 Patch.
- Added Enable/Disable config values for Weapons, Kits, Bosses and Monsters.
- Added Castle Arena location. (Red and Green Eye Skeletons and White\Teal Ghosts are default mobs (5). 1 Vilefang, outside.)
- Moved Event Ring to Multiple Biomes. (Voidling are default mobs(6))
- Re-enabled camp. (Red Eye Skeleton are default mobs (2))
- Moved Felmetal Ore deposits into Custom Resources config not Mistlands Vegetaion.
- Fixed some dropables not having an attach point.
- Fixed Mistlands Vegetation and Trees.
- Added 2 maces, Mistlands and Deep North.
- Added 1 wand, Mountains.

**0.4.1**

- Added config values for Custom Locations and Vegetation.

**0.4.0**

- 10 New Shields, 1 for each boss, Vanilla and DoD, first 5 biomes.
- 6 New craftable Bows. If they shoot backwards, April Fools!? :)
- 6 New craftable Swords. 
- Fixed some Icons having wrong sizes.
- Coverted rest of assets over to JvL. (Monster abilities and Items)
- Added custom Mistlands Vegetation and Locations via JvL's Zone Manager. (No longer need Forgotten Biomes for Mistlands)
- 4 New Locations, Mistlands Cave, Stone Ring Event Zone, Ore Deposit and Camp. (Spawn in Mistlands by default)
- 6 New Pickables, Banana Tree (Swamp, Plains), Apple Tree (Black Forest), Cherry Tree and Walnut Tree and 2 Mushrooms. (Mistlands)
- 4 New Fruits to go along with the new trees.
- 1 New Bush and 4 New Trees (Poplar, Willow, Old Oak and Mistland Oak) in 2 variants spawn in Mistlands and all drop hardwood.
- Updated Class Weapon icons.
- Added Carnivor Bait can be made at the Artisan Station.
- DoD Wolves require Carnivor Bait to be tamed.


**0.3.13**

- Fixed New drakes having wrong trophies.
- Fixed drake trophies not having Localization.
- Moved Fire Wand back to been a Club (Skillwise, seems the vanilla Fire Magic Skill breaks stuff)

**0.3.12**

- Added used vanilla Shaders and Materials to the JvL mock list. (Should fix the snow issue)

**0.3.11**

- Moved all droppable items over to JvL.
- Made Oak tree's require Blackmetal Axe to chop.
- DoD Anvils and later tier Weapon Kits now require Hardwood.
- Fixed explosion from the Fire Wand.
- Converted all vanilla used Predfabs over to JVLMock's.
- Fixed Collider on Armor and Weapon Kits. (Mesh Collider is no good, had to use Box)
- Tweaked Darkness Aura.
- Class Weapons now require Weapon Kits to craft.
- Fixed Void Wand explosion.
- Fixed Class Katana equip position.

**0.3.10**

- Fixed typo stopping some recipies from loading.
- Fixed broken fx with Darkness Aura.
- Added Weapon Kits, Consolidates mundane mats like Metals, Woods, Hides,Pelts, and Threads into 1 material crate to open up the other 3 slots for more interesting materials in armor recipes.
- Updated Prefab IDs in the readme.

**0.3.9**

- Oops. No new dll in upload.

**0.3.8**

- Added Armor Kits, Consolidates mundane mats like Metals, Hides, Pelts, Resins and Threads into 1 material crate to open up the other 3 slots for more interesting materials in armor recipes.

**0.3.7**

- Moved alot more of the mod over to JvL.
- Added 6 Hatchling reskins, Arcane Drake, Gold Drake, Black Drake, Poison Drake, Ice Drake and Flame Drake.

**0.3.6**

- Non Entity/Thunderstore glitch.

**0.3.5**

- Added 3 Anvils for the Forge.
- Enabled 3 Rugs.
- Moved Class Weapons to Forge.
- Changed Paladin Item to White.

**0.3.4**

- Added NPC Storm, Shadow, Fire, Frost Aura's.
- Added a spot light to all new boss altars and made them a bit bigger.
- Tweaked all class weapons, see description (readme.md).
- Fixed value's been set on wrong items.

**0.3.3**

- Removed Clss items from upgrade cost but increased craft cost.
- Tweaked 2H Class weapons block value.
- Fixed Class weapon upgrade levels.

See Changelog.txt for older changes.