using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [Header("Object to use. Must have Animator Component")]
    [SerializeField] private Animator myObject = null;
    //[Header("Use Keypress E")]
    //[SerializeField] private bool useKeyE = false;
    [Header("Is Open Trigger")]
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private float openSpeed = 0.0f;
    [Header("Is Close Trigger")]
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private float closeSpeed = 0.0f;
    [Header("Auto Closes")]
    [SerializeField] private bool autoClose = false;
    [SerializeField] private float closeDelay = 1.5f;
    [SerializeField] private float isInUseDelay = 1.5f;
    [Header("Animations to use in Animator")]
    [SerializeField] private string doorOpen = "AnimationName";
    [SerializeField] private string doorClose = "AnimationName";
    //[SerializeField] private ItemDrop m_keyItem;
    private bool isOpen;
    private bool inUse;
    private void OnTriggerEnter(Collider collider)
    {
        Player component = collider.GetComponent<Player>();
        if (!(component == null) && !(Player.m_localPlayer != component) && !inUse)
        {
            if (openTrigger && !isOpen)
            {
                inUse = true;
                myObject.Play(doorOpen, 0, openSpeed);
                Invoke("IsOpen", isInUseDelay);
            }
            else if (closeTrigger && isOpen)
            {
                inUse = true;
                myObject.Play(doorClose, 0, closeSpeed);
                Invoke("IsClosed", isInUseDelay);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (autoClose && isOpen && !inUse)
        {
            inUse = true;
            Invoke("AutoClose", closeDelay);
        }
    }
    void IsOpen()
    {
        isOpen = true;
        inUse = false;
    }
    void IsClosed()
    {
        isOpen = false;
        inUse = false;
    }
    void AutoClose()
    {
        myObject.Play(doorClose, 0, closeSpeed);
        Invoke("IsClosed", isInUseDelay);
    }
}
