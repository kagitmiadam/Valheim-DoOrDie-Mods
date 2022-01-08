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
		//Projectile prefab28 = PrefabManager.Cache.GetPrefab<Projectile>("Skir_Voidbolt_Projectile_DoD");

		//var SE_Hexed_DoD = ObjectDB.instance.GetStatusEffect("SE_Hexed_DoD");

		//prefab28.m_statusEffect = string.Format("SE_Hexed_DoD");	
		
		ItemManager.OnItemsRegistered -= ModAbilityAttackSE;
	}
}
