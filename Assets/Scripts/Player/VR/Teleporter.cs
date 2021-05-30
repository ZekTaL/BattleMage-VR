using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.VR;

namespace BattleMage.VR
{
    [RequireComponent(typeof(Firepoint))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Firepoint pointer;
        [SerializeField] private VRController controller;

        private void OnValidate()
        {
            pointer = gameObject.GetComponent<Firepoint>();
        }

        void Start()
        {
            if (pointer == null)
                pointer = gameObject.GetComponent<Firepoint>();

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