using UnityEngine;
using Jotunn.Utils;

internal class SE_Bleeding_DoD : StatusEffect
{
	public float m_damageInterval = 1f;

	private float m_timer;

	private float m_damageAmount = 1f;

	public static float m_effectLength = 60f;

	public SE_Bleeding_DoD()
	{
		base.name = "SE_Bleeding_DoD";
		m_name = "$effect_bleeding_dod";
		// Not sure this is the right way to load a sprite from a asset bundle?
		m_icon = AssetUtils.LoadSprite("dodmonsters/Assets/DoDMonsters/Icons/Injured_Icon_DoD");
		m_tooltip = "$effect_bleedingtooltip_dod";
		m_ttl = m_effectLength;
	}
	public override bool CanAdd(Character character)
	{
		if (character.GetSEMan().HaveStatusEffect("SE_Bleeding_DoD"))
		{
			return false;
		}
		return base.CanAdd(character);
	}
	// No clue what the Timer and dt stuff is or if its actually needed?
	public override void UpdateStatusEffect(float dt)
	{
		base.UpdateStatusEffect(dt);
		m_timer -= dt;
		if (m_timer <= 0f)
		{
			m_timer = m_damageInterval;
			HitData hitData = new HitData();
			hitData.m_point = m_character.GetCenterPoint();
			hitData.m_damage.m_damage = m_damageAmount;
			m_character.ApplyDamage(hitData, true, false);
		}
	}
}
