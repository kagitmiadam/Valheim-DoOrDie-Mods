using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDoorController : MonoBehaviour
{
    [Header("Doors to Animate")]
    [SerializeField] private Animator doorLeft = null;
    [SerializeField] private Animator doorRight = null;
    [SerializeField] private Animator doorCenter = null;
    [Header("Animations to use from Animators")]
    [SerializeField] private string doorLeftOpen = "AnimationName";
    [SerializeField] private string doorLeftClose = "AnimationName";
    [SerializeField] private string doorRightOpen = "AnimationName";
    [SerializeField] private string doorRightClose = "AnimationName";
    [SerializeField] private string doorCenterOpen = "AnimationName";
    [SerializeField] private string doorCenterClose = "AnimationName";
    [Header("Double Door Trigger")]
    [SerializeField] private bool doubleDoorTrigger = false;
    [SerializeField] private float openSpeedLarge = 0.0f;
    [Header("Single Door Trigger")]
    [SerializeField] private bool singleDoorTrigger = false;
    [SerializeField] private float openSpeedSmall = 0.0f;
    [Header("Auto Close")]
    [SerializeField] private bool autoClose = false;
    [SerializeField] private float closeDelay = 1.5f;
    [SerializeField] private float isInUseDelay = 1.5f;
    //[SerializeField] private ItemDrop m_keyItem;
    private bool isOpen;
    private bool isSmallOpen;
    private bool inUse;
    private void OnTriggerEnter(Collider collider)
    {
        Player component = collider.GetComponent<Player>();
        if (!(component == null) && !(Player.m_localPlayer != component) && !inUse)
        {
            if (doubleDoorTrigger && !isOpen)
            {
                inUse = true;
                doorLeft.Play(doorLeftOpen, 0, openSpeedLarge);
                doorRight.Play(doorRightOpen, 0, openSpeedLarge);
                Invoke("IsLargeOpen", isInUseDelay);
                if (isSmallOpen)
                {
                    doorCenter.Play(doorCenterClose, 0, openSpeedSmall);
                }
            }
            else if (singleDoorTrigger && !isSmallOpen)
            {
                inUse = true;
                doorCenter.Play(doorCenterOpen, 0, openSpeedSmall);
                Invoke("IsSmallOpen", isInUseDelay);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (autoClose && isOpen && !inUse)
        {
            inUse = true;
            Invoke("AutoCloseLarge", closeDelay);
        }
        else if (autoClose && isSmallOpen && !inUse)
        {
            inUse = true;
            Invoke("AutoCloseSmall", closeDelay);
        }
    }
    void IsLargeOpen()
    {
        isOpen = true;
        inUse = false;
    }
    void IsSmallOpen()
    {
        isSmallOpen = true;
        inUse = false;
    }
    void IsLargeClosed()
    {
        isOpen = false;
        inUse = false;
    }
    void IsSmallClosed()
    {
        isSmallOpen = false;
        inUse = false;
    }
    void AutoCloseLarge()
    {
        doorLeft.Play(doorLeftClose, 0, closeDelay);
        doorRight.Play(doorRightClose, 0, closeDelay);
        Invoke("IsLargeClosed", isInUseDelay);
    }
    void AutoCloseSmall()
    {
        doorCenter.Play(doorCenterClose, 0, closeDelay);
        Invoke("IsSmallClosed", isInUseDelay);
    }
}

