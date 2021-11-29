using Jotunn.Managers;
using UnityEngine;

internal static class UpdateAbilities
{
	internal static void Init()
	{
		ItemManager.OnItemsRegistered += ModAbilityAttackSE;
	}
	private static void ModAbilityAttackSE()
	{
		/*
		ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("SkirSandburst_Shield_DoD");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("SkirSandburst_Heal_DoD");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("Bitterstump_Heal_DoD");
		ItemDrop prefab4 = PrefabManager.Cache.GetPrefab<ItemDrop>("Rambore_Gore_DoD");
		ItemDrop prefab5 = PrefabManager.Cache.GetPrefab<ItemDrop>("Farkas_FrostBite_DoD");
		ItemDrop prefab6 = PrefabManager.Cache.GetPrefab<ItemDrop>("Farkas_Hamper_Attack_DoD");
		ItemDrop prefab7 = PrefabManager.Cache.GetPrefab<ItemDrop>("Farkas_Bleed_DoD");
		Projectile prefab8 = PrefabManager.Cache.GetPrefab<Projectile>("Bhygshan_Fireball_Projectile_DoD");
		Projectile prefab9 = PrefabManager.Cache.GetPrefab<Projectile>("Skir_Voidbolt_Projectile_DoD");
		ItemDrop prefab10 = PrefabManager.Cache.GetPrefab<ItemDrop>("Bhygshan_SprayFrost_DoD");
		ItemDrop prefab11 = PrefabManager.Cache.GetPrefab<ItemDrop>("Bitterstump_SprayFrost_DoD");

		var SE_LesserShield_DoD = ObjectDB.instance.GetStatusEffect("SE_LesserShield_DoD");
		var SE_LesserHoT_DoD = ObjectDB.instance.GetStatusEffect("SE_LesserHoT_DoD");
		var SE_HoT_DoD = ObjectDB.instance.GetStatusEffect("SE_HoT_DoD");
		var SE_LesserBleeding_DoD = ObjectDB.instance.GetStatusEffect("SE_LesserBleeding_DoD");
		var SE_Bleeding_DoD = ObjectDB.instance.GetStatusEffect("SE_Bleeding_DoD");
		var SE_Slow_DoD = ObjectDB.instance.GetStatusEffect("SE_Slow_DoD");
		var SE_Frostbitten_DoD = ObjectDB.instance.GetStatusEffect("SE_Frostbitten_DoD");
		var SE_Frostbite_DoD = ObjectDB.instance.GetStatusEffect("SE_Frostbite_DoD");
		var SE_Hexed_DoD = ObjectDB.instance.GetStatusEffect("SE_Hexed_DoD");
		var SE_Blistered_DoD = ObjectDB.instance.GetStatusEffect("SE_Blistered_DoD");

		prefab1.m_itemData.m_shared.m_attackStatusEffect = SE_LesserShield_DoD;
		Debug.Log("Updated AttackSE: SkirSandburst_Shield_DoD");
		prefab2.m_itemData.m_shared.m_attackStatusEffect = SE_HoT_DoD;
		Debug.Log("Updated AttackSE: SkirSandburst_Heal_DoD");
		prefab3.m_itemData.m_shared.m_attackStatusEffect = SE_HoT_DoD;
		Debug.Log("Updated AttackSE: Bitterstump_Heal_DoD");
		prefab4.m_itemData.m_shared.m_attackStatusEffect = SE_LesserBleeding_DoD;
		Debug.Log("Updated AttackSE: Rambore_Gore_DoD");
		prefab5.m_itemData.m_shared.m_attackStatusEffect = SE_Frostbitten_DoD;
		Debug.Log("Updated AttackSE: Farkas_FrostBite_DoD");
		prefab6.m_itemData.m_shared.m_attackStatusEffect = SE_Slow_DoD;
		Debug.Log("Updated AttackSE: Farkas_Hamper_Attack_DoD");
		prefab7.m_itemData.m_shared.m_attackStatusEffect = SE_Bleeding_DoD;
		Debug.Log("Updated AttackSE: Farkas_Bleed_DoD");
		prefab10.m_itemData.m_shared.m_attackStatusEffect = SE_Frostbite_DoD;
		Debug.Log("Updated AttackSE: Bhygshan_SprayFrost_DoD");
		prefab11.m_itemData.m_shared.m_attackStatusEffect = SE_Frostbitten_DoD;
		Debug.Log("Updated AttackSE: Bitterstump_SprayFrost_DoD");
		prefab8.m_statusEffect = string.Format("SE_Blistered_DoD");
		Debug.Log("Updated AttackSE: Bhygshan_Fireball_DoD");
		prefab9.m_statusEffect = string.Format("SE_Hexed_DoD");
		Debug.Log("Updated AttackSE: Skir_Voidbolt_Projectile_DoD");
		*/
		ItemManager.OnItemsRegistered -= ModAbilityAttackSE;
	}
}
