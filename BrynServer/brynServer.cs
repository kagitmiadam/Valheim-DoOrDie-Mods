using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;

namespace BrynServer
{
	[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
	internal class brynServer : BaseUnityPlugin
	{
		public const string PluginGUID = "BrynServer";

		public const string PluginName = "BrynServer";

		public const string PluginVersion = "0.0.1";
		private void Awake()
		{
			ItemManager.OnItemsRegistered += EditBAFood;
		}
		private void EditBAFood()
		{
			ItemDrop food1 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_icecream");
			food1.m_itemData.m_shared.m_food = 50f;
			food1.m_itemData.m_shared.m_foodStamina = 40f;
			ItemDrop food2 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_nut_ella");
			food2.m_itemData.m_shared.m_food = 50f;
			food2.m_itemData.m_shared.m_foodStamina = 40f;
			ItemDrop food3 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_carrotsticks");
			food3.m_itemData.m_shared.m_food = 50f;
			food3.m_itemData.m_shared.m_foodStamina = 30f;
			ItemDrop food4 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_boiledegg");
			food4.m_itemData.m_shared.m_food = 40f;
			food4.m_itemData.m_shared.m_foodStamina = 40f;
			ItemDrop food5 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_butter");
			food5.m_itemData.m_shared.m_food = 40f;
			food5.m_itemData.m_shared.m_foodStamina = 40f;
			ItemDrop food6 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_broth");
			food6.m_itemData.m_shared.m_food = 35f;
			food6.m_itemData.m_shared.m_foodStamina = 35f;
			ItemDrop food7 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_fishstew");
			food7.m_itemData.m_shared.m_food = 45f;
			food7.m_itemData.m_shared.m_foodStamina = 45f;
			ItemDrop food8 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_burger");
			food8.m_itemData.m_shared.m_food = 110f;
			food8.m_itemData.m_shared.m_foodStamina = 110f;
			ItemDrop food9 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_bloodsausage");
			food9.m_itemData.m_shared.m_food = 80f;
			food9.m_itemData.m_shared.m_foodStamina = 65f;
			ItemDrop food10 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_omlette");
			food10.m_itemData.m_shared.m_food = 65f;
			food10.m_itemData.m_shared.m_foodStamina = 55f;
			ItemDrop food11 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_porkrind");
			food11.m_itemData.m_shared.m_food = 55f;
			food11.m_itemData.m_shared.m_foodStamina = 35f;
			ItemDrop food12 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_haggis");
			food12.m_itemData.m_shared.m_food = 65f;
			food12.m_itemData.m_shared.m_foodStamina = 45f;
			ItemDrop food13 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_candiedturnip");
			food13.m_itemData.m_shared.m_food = 45f;
			food13.m_itemData.m_shared.m_foodStamina = 65f;
			ItemDrop food14 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_moochi");
			food14.m_itemData.m_shared.m_food = 75f;
			food14.m_itemData.m_shared.m_foodStamina = 60f;
			ItemDrop food15 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_kabob");
			food15.m_itemData.m_shared.m_food = 75f;
			food15.m_itemData.m_shared.m_foodStamina = 65f;
			ItemDrop food16 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_friedloxmeat");
			food16.m_itemData.m_shared.m_food = 110f;
			food16.m_itemData.m_shared.m_foodStamina = 110f;
			ItemDrop food17 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_glazedcarrots");
			food17.m_itemData.m_shared.m_food = 50f;
			food17.m_itemData.m_shared.m_foodStamina = 70f;
			ItemDrop food18 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_bacon");
			food18.m_itemData.m_shared.m_food = 50f;
			food18.m_itemData.m_shared.m_foodStamina = 50f;
			ItemDrop food19 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_smokedfish");
			food19.m_itemData.m_shared.m_food = 55f;
			food19.m_itemData.m_shared.m_foodStamina = 55f;
			ItemDrop food20 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_pancake");
			food20.m_itemData.m_shared.m_food = 110f;
			food20.m_itemData.m_shared.m_foodStamina = 110f;
			ItemDrop food21 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_pizza");
			food21.m_itemData.m_shared.m_food = 110f;
			food21.m_itemData.m_shared.m_foodStamina = 120f;
			ItemDrop food22 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_coffee");
			food22.m_itemData.m_shared.m_food = 80f;
			food22.m_itemData.m_shared.m_foodStamina = 100f;
			ItemDrop food23 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_latte");
			food23.m_itemData.m_shared.m_food = 70f;
			food23.m_itemData.m_shared.m_foodStamina = 100f;
			ItemDrop food24 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_firecream");
			food24.m_itemData.m_shared.m_food = 75f;
			food24.m_itemData.m_shared.m_foodStamina = 55f;
			ItemDrop food25 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_electriccream");
			food25.m_itemData.m_shared.m_food = 75f;
			food25.m_itemData.m_shared.m_foodStamina = 55f;
			ItemDrop food26 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_acidcream");
			food26.m_itemData.m_shared.m_food = 75f;
			food26.m_itemData.m_shared.m_foodStamina = 55f;
			ItemDrop food27 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_porridge");
			food27.m_itemData.m_shared.m_food = 130f;
			food27.m_itemData.m_shared.m_foodStamina = 130f;
			ItemDrop food28 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_pbj");
			food28.m_itemData.m_shared.m_food = 130f;
			food28.m_itemData.m_shared.m_foodStamina = 130f;
			ItemDrop food29 = PrefabManager.Cache.GetPrefab<ItemDrop>("rk_birthday");
			food29.m_itemData.m_shared.m_food = 130f;
			food29.m_itemData.m_shared.m_foodStamina = 30f;
			ItemManager.OnItemsRegistered -= EditBAFood;
		}
	}
}
