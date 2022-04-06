using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool autoTrigger = false;
    [SerializeField] private bool useKeyPress = false;
    [SerializeField] private string doorOpen = "AnimationName";
    [SerializeField] private float openSpeed = 0.0f;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private string doorClose = "AnimationName";
    [SerializeField] private float closeSpeed = 0.0f;
    bool isOpen = false;
    private void OnTriggerEnter(Collider collider)
    {
        Player component = collider.GetComponent<Player>();
        if (!(component == null) && !(Player.m_localPlayer != component))
        {
            if (openTrigger && !isOpen)
            {
                myDoor.Play(doorOpen, 0, openSpeed);
                isOpen = true;
            }
            else if (closeTrigger && isOpen)
            {
                myDoor.Play(doorClose, 0, closeSpeed);
                isOpen = false;
            }
            else if (useKeyPress && Input.GetKeyDown(KeyCode.F) && !isOpen)
            {
                myDoor.Play(doorOpen, 0, openSpeed);
                isOpen = true;
            }
            else if (useKeyPress && Input.GetKeyDown(KeyCode.F) && isOpen)
            {
                myDoor.Play(doorClose, 0, closeSpeed);
                isOpen = false;
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if ((autoTrigger) && (isOpen))
        {
            myDoor.Play(doorClose, 0, closeSpeed);
            isOpen = false;
        }
    }
}
