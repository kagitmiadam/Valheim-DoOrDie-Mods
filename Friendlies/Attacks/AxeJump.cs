﻿using BepInEx;
using System;
using RRRCore;
using UnityEngine;

namespace Friendlies.Attacks
{
    public static class AxeJump
    {
        public static GameObject Get(
            GameObject owner,
            string weaponName
            )
        {   
            //Fix showing up as Knife in NPC's hand

            bool alreadyExisted = false;
            GameObject clone = RRRLateLoadPrefabs.CloneRepeatable(ref alreadyExisted, weaponName, "AxeJump", regOdb: true);
            if (alreadyExisted)
                return clone;
            
            ItemDrop component = clone.GetComponent<ItemDrop>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                throw new NullReferenceException("No ItemDrop component in prefab: " + weaponName);
            /*
            for (int index = 0; index < clone.transform.childCount; ++index)
            {
                UnityEngine.Object.Destroy(clone.transform.GetChild(index).gameObject);
            }
            GameObject prefab = ZNetScene.instance.GetPrefab("AxeBlackMetal");
            for (int index = 0; index < prefab.transform.childCount; ++index)
            {
                UnityEngine.Object gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab.transform.GetChild(index).gameObject, clone.transform);
                gameObject.name = gameObject.name.TrimCloneTag();
            }
            */
            /*
            Transform transform = clone.transform;
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

            ZSyncTransform zSync = clone.GetComponent<ZSyncTransform>();
            zSync = prefab.GetComponent<ZSyncTransform>();
            ZNetView zNet = clone.GetComponent<ZNetView>();
            zNet = prefab.GetComponent<ZNetView>();
            Rigidbody rigidbody = clone.GetComponent<Rigidbody>();
            rigidbody = prefab.GetComponent<Rigidbody>();
            */

            ItemDrop.ItemData.SharedData shared = component.m_itemData.m_shared;

            if (!shared.m_attack.m_attackOriginJoint.IsNullOrWhiteSpace() && Utils.FindChild(owner.transform, shared.m_attack.m_attackOriginJoint) == null)
                shared.m_attack.m_attackOriginJoint = "";

            shared.m_name = "Axe Jump";
            shared.m_description = "jump with an axe";
            shared.m_useDurability = false;
            shared.m_skillType = Skills.SkillType.Axes;
            shared.m_animationState = ItemDrop.ItemData.AnimationState.OneHanded;
        
            shared.m_attackForce = 50;
            shared.m_backstabBonus = 3;

            shared.m_damages.m_pierce = 0;
            shared.m_damages.m_slash = 30;
            shared.m_damages.m_chop = 40;

            shared.m_aiAttackRange = 5f;
            shared.m_aiAttackRangeMin = 0f;
            shared.m_aiAttackInterval = 4f;

            shared.m_attack.m_attackAnimation = "knife_secondary";

            return clone;
        }
    }
}
