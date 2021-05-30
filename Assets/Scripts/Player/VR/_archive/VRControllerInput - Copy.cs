
//using UnityEngine;
//using Valve.VR;

//namespace BattleMage.VR
//{
//    //Putting a wrapper ontop of a wrapper.
//    public class VRControllerInput2 : MonoBehaviour
//    {
//        public VRController Controller => controller;

//        public VRInputEvent OnTouchpadAxisChanged => onTouchpadAxisChanged;
//        #region Steam Actions (the input actions)
//        [Header("Steam actions")]
//        [SerializeField] private SteamVR_Action_Boolean pointer; //Clicking
//        [SerializeField] private SteamVR_Action_Boolean teleport;
//        [SerializeField] private SteamVR_Action_Boolean interact;
//        [SerializeField] private SteamVR_Action_Boolean grab;
//        [SerializeField] private SteamVR_Action_Vector2 touchpadAxis;
//        #endregion

//        #region Unity Input Events
//        [Header("Unity events")]
//        public VRInputEvent onPointerPressed = new VRInputEvent();
//        public VRInputEvent onPointerReleased = new VRInputEvent();
//        public VRInputEvent onTeleportPressed = new VRInputEvent();
//        public VRInputEvent onTeleportReleased = new VRInputEvent();
//        public VRInputEvent onInteractPressed = new VRInputEvent();
//        public VRInputEvent onInteractReleased = new VRInputEvent();
//        public VRInputEvent onGrabPressed = new VRInputEvent();
//        public VRInputEvent onGrabReleased = new VRInputEvent();
//        public VRInputEvent onTouchpadAxisChanged = new VRInputEvent();
//        #endregion

//        #region Steam VR Input Callbacks
//        private void OnPointerDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onPointerPressed.Invoke(GenerateArgs());
//        private void OnPointerUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onPointerReleased.Invoke(GenerateArgs());
//        private void OnTeleportDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onTeleportPressed.Invoke(GenerateArgs());
//        private void OnTeleportUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onTeleportReleased.Invoke(GenerateArgs());
//        private void OnInteractDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onInteractPressed.Invoke(GenerateArgs());
//        private void OnInteractUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onInteractReleased.Invoke(GenerateArgs());
//        private void OnGrabDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onGrabPressed.Invoke(GenerateArgs());
//        private void OnGrabUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onGrabReleased.Invoke(GenerateArgs());
//        private void OnTouchpadChanged(SteamVR_Action_Vector2 action, SteamVR_Input_Sources source, Vector2 axis, Vector2 delta) => onTouchpadAxisChanged.Invoke(GenerateArgs());
//        #endregion

//        VRController controller;
//        public void Initialise(VRController _controller)
//        {
//            controller = _controller;

//            //Link callback to the steam events
//            pointer.AddOnStateDownListener(OnPointerDown, controller.InputSource);
//            pointer.AddOnStateUpListener(OnPointerUp, controller.InputSource);
//            teleport.AddOnStateDownListener(OnTeleportDown, controller.InputSource);
//            teleport.AddOnStateUpListener(OnTeleportUp, controller.InputSource);
//            interact.AddOnStateDownListener(OnInteractDown, controller.InputSource);
//            interact.AddOnStateUpListener(OnInteractUp, controller.InputSource);
//            grab.AddOnStateDownListener(OnGrabDown, controller.InputSource);
//            grab.AddOnStateUpListener(OnGrabUp, controller.InputSource);
//            touchpadAxis.AddOnChangeListener(OnTouchpadChanged, controller.InputSource);

//            OnGrabPressed.AddListener((args) => Dummy());

//            //OnGrabPressed += Dummy;
//            //pointer.AddOnStateDownListener(
//            //   (SteamVR_Action_Boolean action, SteamVR_Input_Sources source) => onGrabPressed.Invoke(GenerateArgs()),
//            //   controller.InputSource);
//        }

//        void Dummy() { }

//        /// <summary>
//        /// Sets up an instance of InputEventArgs based on the controller and touchpad values.
//        /// </summary>
//        private InputEventArgs GenerateArgs() => new InputEventArgs(controller, controller.InputSource, touchpadAxis.axis);
//    }
//}