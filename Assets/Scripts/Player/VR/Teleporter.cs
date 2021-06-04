using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.VR;

namespace BattleMage.VR
{
    [RequireComponent(typeof(LaserCaster))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private LaserCaster pointer;
        [SerializeField] private VRController controller;

        private void OnValidate()
        {
            pointer = gameObject.GetComponent<LaserCaster>();
        }

        void Start()
        {
            if (pointer == null)
                pointer = gameObject.GetComponent<LaserCaster>();

            controller.Input.OnTeleportPressed.AddListener(args =>
            {
                if (pointer.HitPosition != Vector3.zero)
                {
                    VRRig.instance.PlayerArea.position = pointer.HitPosition;
                }
            });
        }
    }
}