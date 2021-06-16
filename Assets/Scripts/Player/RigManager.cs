using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BattleMage.VR;
using BattleMage.PC;
using UnityEngine.EventSystems;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the rigs (PC and VR)
    /// </summary>
    [DefaultExecutionOrder(-100000)]
    public class RigManager : MonoBehaviour
    {

        public static RigManager Instance;
        public static Camera MainCamera;

        [SerializeField] GameObject vrRig;
        [SerializeField] GameObject pcRig;
        [SerializeField] Camera vrCamera;
        [SerializeField] Camera pcCamera;
        [SerializeField] GameObject pcCanvas;
        [SerializeField] GameObject vrCanvas;
        [SerializeField] StandaloneInputModule inputSystemModule;
        [SerializeField] VRInputModule vrModule;

        public bool inVR { get; private set; }
        bool paused;

        void Start()
        {
            //Init
            Instance = this;

            // check if VR is enable
            inVR = VRUtil.IsVREnabled();
            // Activate the appropriate rig
            ActivateRig();
        }

        void Update()
        {
            ////Pause
            //if (!inVR && Input.GetKeyDown(KeyCode.Escape))
            //{
            //    if (paused = !paused)
            //        PCCursorManager.RevealCursor();
            //    else
            //        PCCursorManager.HideCursor();
            //}

            // Enable the correct input system that is different from PC to VR
            if(inVR && inputSystemModule.enabled)
            {
                inputSystemModule.enabled = false;
                vrModule.enabled = true;
                UnityEngine.EventSystems.EventSystem.current.UpdateModules();
            }
            else if (!inVR && vrModule.enabled)
            {
                vrModule.enabled = false;
                inputSystemModule.enabled = true;
                UnityEngine.EventSystems.EventSystem.current.UpdateModules();
            }
        }

        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(20, 20, 200, 20), "InVR: " + inVR);
        //}

        public void ToggleRig()
        {
            inVR = !inVR;
            ActivateRig();
            VR.VRUtil.SetVREnabled(inVR);
        }

        /// <summary>
        /// Activate the appropriate rig between PC and VR
        /// </summary>
        void ActivateRig ()
        {
            if (inVR)
            {
                Debug.Log("in vr rig");
                vrRig.gameObject.SetActive(true);
                pcRig.gameObject.SetActive(false);
                vrCanvas.SetActive(true);
                pcCanvas.SetActive(false);
                MainCamera = vrCamera;
                //pcCamera.tag = "Untagged";
            }
            else //In PC rig
            {
                Debug.Log("in pc rig");
                //PCCursorManager.HideCursor();
                vrRig.gameObject.SetActive(false);
                pcRig.gameObject.SetActive(true);
                vrCanvas.SetActive(false);
                pcCanvas.SetActive(true);
                MainCamera = pcCamera;
                //vrCamera.tag = "Untagged";
            }
        }
    }
}