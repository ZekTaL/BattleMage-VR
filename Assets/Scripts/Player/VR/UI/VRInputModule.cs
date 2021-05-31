using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.VR;
using UnityEngine.EventSystems;

namespace BattleMage
{
    public class VRInputModule : BaseInputModule
    {
        public Vector3 ControllerPosition { get; set; }
        public bool ControllerButtonDown { get; set; }
        public bool ControllerButtonUp { get; set; }

        GameObject currentObject;
        PointerEventData data; //Passed by the input module
        [SerializeField] Camera vrCamera;
        protected override void Awake()
        {
            base.Awake();

            data = new PointerEventData(eventSystem);
        }

        protected override void Start()
        {
            base.Start();
            //camera = RigManager.Instance.Headset;
        }

        public override void Process() //Update loop for input modules
        {
            data.Reset();
            data.position = (Vector2)ControllerPosition;

            //Raycast
            eventSystem.RaycastAll(data, m_RaycastResultCache);
            data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            currentObject = data.pointerCurrentRaycast.gameObject; //The obj gound in th==h

            //Clear the raycast data
            m_RaycastResultCache.Clear();

            //Handle hovering for selectable UI elements
            HandlePointerExitAndEnter(data, currentObject);

            if (ControllerButtonDown)
                ProcessPress();
            if (ControllerButtonUp)
                ProcessRelease();

            //Reset the button flag to prevent 
            ControllerButtonDown = false;
            ControllerButtonUp = false;
        }

        private void ProcessPress()
        {
            //Set the press raycast to the current raycast
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            //Check for the hit object, get the down handler and call itrrrrrrrrrrrr
            GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

            //If no down handler found, try and get the click handler
            if (newPointerPress == null)
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            //Copy the lk
            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = currentObject;
        }

        private void ProcessRelease ()
        {
            //Execute the pointer up
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            //Check for a click handler
            GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

            //Check if the clicked object amtches the one htat was set in the press fuctnion
            if (data.pointerPress == pointerUpHandler)
            {
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
            }

            //Clear the selected go and reset pointer ata
            eventSystem.SetSelectedGameObject(null);
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;


        }
    }
}
