using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Serializable = System.SerializableAttribute;

namespace BattleMage.VR
{
    [Serializable]
    public class VRInputEvent : UnityEvent<InputEventArgs> { } //Custom Unity event

    [Serializable]
    public class InputEventArgs //Handles all input actions - button, touch pad
    {
        public VRController controller;
        public SteamVR_Input_Sources sources; //Input sources
        public Vector2 touchpadAxis;  //The position the player is touching the touchpad on.

        public InputEventArgs(VRController controller, SteamVR_Input_Sources sources, Vector2 touchpadAxis)
        {
            this.controller = controller;
            this.sources = sources;
            this.touchpadAxis = touchpadAxis;
        }
    }
}