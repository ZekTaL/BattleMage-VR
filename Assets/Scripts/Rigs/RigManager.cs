using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BattleMage.VR;
using BattleMage.PC;

namespace BattleMage.Managers
{
    [DefaultExecutionOrder(-10000)]
    public class RigManager : MonoBehaviour
    {
        public static RigManager Instance;

        public GameObject vrRig;
        public GameObject pcRig;
        [SerializeField] Transform pc_ShootPoint_Left;
        [SerializeField] Transform pc_ShootPoint_Right;
        [SerializeField] Transform vr_ShootPoint_Left;
        [SerializeField] Transform vr_ShootPoint_Right;

        bool inVR;

        public Transform ShootPointLeft => inVR ? vr_ShootPoint_Left : pc_ShootPoint_Left;
        public Transform ShootPointRight => inVR? vr_ShootPoint_Right : pc_ShootPoint_Right;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("pc_ShootPoint_Left " + pc_ShootPoint_Left);
            }

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