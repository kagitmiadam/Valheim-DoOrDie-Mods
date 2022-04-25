using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [Header("Object to use. Must have Animator Component")]
    [SerializeField] private Animator myObject = null;
    [Header("Use Keypress E")]
    [SerializeField] private bool useKeyE = false;
    [Header("Is Open Trigger")]
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private float openSpeed = 0.0f;
    [Header("Is Close Trigger")]
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private float closeSpeed = 0.0f;
    [Header("Auto Closes")]
    [SerializeField] private bool autoClose = false;
    [SerializeField] private float closeDelay = 0.0f;
    [Header("Animations to use in Animator")]
    [SerializeField] private string doorOpen = "AnimationName";
    [SerializeField] private string doorClose = "AnimationName";
    [SerializeField] private ItemDrop m_keyItem;
    private bool isOpen;
    private bool inReach;
    private void OnTriggerEnter(Collider collider)
    {
        Player component = collider.GetComponent<Player>();
        if (!(component == null) && !(Player.m_localPlayer != component))
        {
            inReach = true;
            if (openTrigger && !isOpen && !useKeyE)
            {
                myObject.Play(doorOpen, 0, openSpeed);
                isOpen = true;
            }
            else if (closeTrigger && isOpen && !useKeyE)
            {
                myObject.Play(doorClose, 0, closeSpeed);
                isOpen = false;
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        inReach = false;
        if (autoClose && isOpen)
        {
            Invoke(doorClose, closeDelay);
        }
    }
    /*void Update()
    {
        if (inReach && useKeyE && Input.GetKeyDown(KeyCode.E))
        {
            if (!openTrigger)
            {
                myObject.Play(doorOpen, 0, openSpeed);
                isOpen = true;
            }
        }
        else
        {
            if (!closeTrigger)
            {
                myObject.Play(doorClose, 0, closeSpeed);
                isOpen = false;
            }
        }
    }*/
}
