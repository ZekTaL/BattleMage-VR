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
    public class PlatformRigManager : MonoBehaviour
    {
        public GameObject vrRig;
        public GameObject pcRig;

        void Awake()
        {
            if (VRUtil.IsVREnabled())
            {
                vrRig.gameObject.SetActive(true);
                pcRig.gameObject.SetActive(false);
            }
            else
            {
                vrRig.gameObject.SetActive(false);
                pcRig.gameObject.SetActive(true);
            }
        }
    }
}