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
        public GameObject vrControl;
        public GameObject desktopControl;

        void Start()
        {
            if (VRUtil.IsVREnabled())
            {
                vrControl.gameObject.SetActive(true);
                desktopControl.gameObject.SetActive(false);
            }
            else
            {
                vrControl.gameObject.SetActive(false);
                desktopControl.gameObject.SetActive(true);
            }
        }
    }
}