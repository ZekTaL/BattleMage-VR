using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BattleMage.VR;

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

        public Transform shootPointLeft { get; private set; }
        public Transform shootPointRight { get; private set; }

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
            else
            {
                vrRig.gameObject.SetActive(false);
                pcRig.gameObject.SetActive(true);
            }

            shootPointLeft = inVR ? vr_ShootPoint_Left : pc_ShootPoint_Left;
            shootPointRight = inVR ? vr_ShootPoint_Right : pc_ShootPoint_Right;
            Debug.Log("=== Is in VR? " + inVR + " === shootPointLeft; " + shootPointLeft);
            Debug.Log("vr_ShootPoint_Left " + vr_ShootPoint_Left + " vr_ShootPoint_Right " + vr_ShootPoint_Right);
        }

        void Start ()
        {
            shootPointLeft = inVR ? vr_ShootPoint_Left : pc_ShootPoint_Left;
            shootPointRight = inVR ? vr_ShootPoint_Right : pc_ShootPoint_Right;
            Debug.Log("=== Is in VR? " + inVR + " === shootPointLeft; " + shootPointLeft);
            Debug.Log("vr_ShootPoint_Left " + vr_ShootPoint_Left + " vr_ShootPoint_Right " + vr_ShootPoint_Right);
        }
    }
}