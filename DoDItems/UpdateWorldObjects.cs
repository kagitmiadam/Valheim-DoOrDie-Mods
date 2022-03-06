using Jotunn.Managers;
using UnityEngine;

internal static class UpdateWorldObjects
{
	internal static void Init()
	{
		ItemManager.OnItemsRegistered += ModWorldObjects;
	}
	private static void ModWorldObjects()
	{
		TreeBase prefab1 = PrefabManager.Cache.GetPrefab<TreeBase>("Oak1");
		prefab1.m_minToolTier = 3;
		ItemManager.OnItemsRegistered -= ModWorldObjects;
	}
}