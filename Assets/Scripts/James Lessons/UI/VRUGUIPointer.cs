using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.VR;
using Valve.VR;

/*
 VRUGUIPointer
sets up the connection between the input and VRInputModule

VRInputModule does the processing of the VRInputs

---
More than 1 input module: EventSystem will remove the inactive one
 */

namespace BattleMage
{
    //[RequireComponent(typeof(VRControllerInput))]
    public class VRUGUIPointer : MonoBehaviour
    {
        [SerializeField] SteamVR_Action_Boolean clickAction;
        [SerializeField] LayerMask uiMask;
        //[SerializeField] LayerMask uiMask = (LayerMask)LayerMask.NameToLayer("UI");
        [SerializeField] LaserCaster laser;

        VRInputModule inputModule;

        void Start()
        {
            inputModule = FindObjectOfType<VRInputModule>();
        }

        void LateUpdate()
        {
            inputModule.ControllerButtonDown = clickAction.stateDown;
            inputModule.ControllerButtonUp = clickAction.stateUp;

            Vector3 position = Vector3.zero;
            bool hitUI = false;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, uiMask))
            {
                position = hit.point;
                hitUI = true;
            }

            //When we put mouse over UI, it'll make the laser appear
            //The canvas has to have a box collider
            inputModule.ControllerPosition = position;
            if (laser != null)
            {
                //laser.ToggleLaser(hitUI);
                laser.ToggleLaser(true);
            }
        }
    }

}