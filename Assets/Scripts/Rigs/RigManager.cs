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

        [SerializeField] LayerMask raycastLayers;
        [SerializeField] Color colorInvalid = Color.red;
        [SerializeField] Color colorValid = Color.green;

        public GameObject vrRig;
        public GameObject pcRig;
        bool inVR;

        public LayerMask RaycastLayers => raycastLayers;
        public Color ColorValid => colorValid;
        public Color ColorInvalid => colorInvalid;


        void Awake()
        {
            //Init
            Instance = this;
            
            inVR = VRUtil.IsVREnabled();
            if (inVR)
            {
                vrRig.gameObject.SetActive(true);
                pcRig.gameObject.SetActive(false);
            }
            else //In PC rig
            {
                PCCursorManager.HideCursor();
                vrRig.gameObject.SetActive(false);
                pcRig.gameObject.SetActive(true);
            }
        }

        bool paused;
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