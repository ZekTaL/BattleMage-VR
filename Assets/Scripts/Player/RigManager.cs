using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BattleMage.VR;
using BattleMage.PC;

namespace BattleMage
{
    [DefaultExecutionOrder(-10000)]
    public class RigManager : MonoBehaviour
    {
        public static RigManager Instance;
        public static Camera mainCamera;

        [SerializeField] GameObject vrRig;
        [SerializeField] GameObject pcRig;
        [SerializeField] Camera vrCamera;
        [SerializeField] Camera pcCamera;

        public bool inVR { get; private set; }
        bool paused;

        void Awake()
        {
            //Init
            Instance = this;
            
            inVR = VRUtil.IsVREnabled();
            if (inVR)
            {
                vrRig.gameObject.SetActive(true);
                pcRig.gameObject.SetActive(false);
                mainCamera = vrCamera;
                //pcCamera.tag = "Untagged";
            }
            else //In PC rig
            {
                PCCursorManager.HideCursor();
                vrRig.gameObject.SetActive(false);
                pcRig.gameObject.SetActive(true);
                mainCamera = pcCamera;
                //vrCamera.tag = "Untagged";
            }
        }

       void Update ()
        {
            //Pause
            if (!inVR && Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused = !paused)
                    PCCursorManager.RevealCursor();
                else
                    PCCursorManager.HideCursor();
            }
        }
    }
}