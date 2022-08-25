using Jotunn.Managers;
using UnityEngine;
using DoDMonsters;
internal static class UpdateAoE
{
	internal static void Init()
	{
		ItemManager.OnItemsRegistered += ModAoESE;
	}
	private static void ModAoESE()
	{
		/*
		Aoe prefab1 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_HoT50_DoD");
		Aoe prefab2 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_HoT100_DoD");
		Aoe prefab3 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_HoT200_DoD");
		Aoe prefab4 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Regen50_DoD");
		Aoe prefab5 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Regen100_DoD");
		Aoe prefab6 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Regen200_DoD");
		Aoe prefab7 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Protection250_DoD");
		Aoe prefab8 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Protection500_DoD");
		Aoe prefab9 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Protection1000_DoD");
		Aoe prefab10 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Bleed25_DoD");
		Aoe prefab11 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Bleed50_DoD");
		Aoe prefab12 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Bleed100_DoD");
		Aoe prefab13 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Infected_DoD");
		Aoe prefab14 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Diseased_DoD");
		Aoe prefab15 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Weak_DoD");
		Aoe prefab16 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Bitterstump_Heal_DoD");
		Aoe prefab17 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_Skir_Nova_DoD");
		Aoe prefab18 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_RogueSword_DoD");
		Aoe prefab19 = PrefabManager.Cache.GetPrefab<Aoe>("AoE_AuraHealing_DoD");

		prefab1.m_statusEffect = string.Format("SE_LesserHoT_DoD");
		Debug.Log("Updated Status Effect: AoE Lesser HoT");
		prefab2.m_statusEffect = string.Format("SE_HoT_DoD");
		Debug.Log("Updated Status Effect: AoE HoT");
		prefab3.m_statusEffect = string.Format("SE_GreaterHoT_DoD");
		Debug.Log("Updated Status Effect: AoE Greater HoT");
		prefab4.m_statusEffect = string.Format("SE_LesserRegen_DoD");
		Debug.Log("Updated Status Effect: AoE Lesser Regen");
		prefab5.m_statusEffect = string.Format("SE_Regen_DoD");
		Debug.Log("Updated Status Effect: AoE Regen");
		prefab6.m_statusEffect = string.Format("SE_GreaterRegen_DoD");
		Debug.Log("Updated Status Effect: AoE Greater Regen");
		prefab7.m_statusEffect = string.Format("SE_LesserShield_DoD");
		Debug.Log("Updated Status Effect: AoE Lesser Shield");
		prefab8.m_statusEffect = string.Format("SE_Shield_DoD");
		Debug.Log("Updated Status Effect: AoE Shield");
		prefab9.m_statusEffect = string.Format("SE_GreaterShield_DoD");
		Debug.Log("Updated Status Effect: AoE Greater Shield");
		prefab10.m_statusEffect = string.Format("SE_LesserBleeding_DoD");
		Debug.Log("Updated Status Effect: AoE Lesser Bleeding");
		prefab11.m_statusEffect = string.Format("SE_Bleeding_DoD");
		Debug.Log("Updated Status Effect: AoE Bleeding");
		prefab12.m_statusEffect = string.Format("SE_GreaterBleeding_DoD");
		Debug.Log("Updated Status Effect: AoE Greater Bleeding");
		prefab13.m_statusEffect = string.Format("SE_Infected_DoD");
		Debug.Log("Updated Status Effect: AoE Infected");
		prefab14.m_statusEffect = string.Format("SE_Diseased_DoD");
		Debug.Log("Updated Status Effect: AoE Diseased");
		prefab15.m_statusEffect = string.Format("SE_Weak_DoD");
		Debug.Log("Updated Status Effect: AoE Weak");
		prefab16.m_statusEffect = string.Format("SE_HoT_DoD");
		Debug.Log("Updated Status Effect: AoE Bitterstump Heal");
		prefab17.m_statusEffect = string.Format("SE_Blistered_DoD");
		Debug.Log("Updated Status Effect: AoE Skir Sandburst Nova");
		prefab18.m_statusEffect = string.Format("SE_Slow_DoD");
		Debug.Log("Updated Status Effect: AoE Rogue Sword");
		prefab19.m_statusEffect = string.Format("SE_SpearHealing_DoD");
		Debug.Log("Updated Status Effect: AoE Druid Spear");
		*/
		ItemManager.OnItemsRegistered -= ModAoESE;
	}
}
