using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.VR;

namespace BattleMage.VR
{
    [RequireComponent(typeof(LaserVisualizer))]
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private LaserVisualizer pointer;
        [SerializeField] private VRController controller;

        private void OnValidate()
        {
            pointer = gameObject.GetComponent<LaserVisualizer>();
        }

        void Start()
        {
            if (pointer == null)
                pointer = gameObject.GetComponent<LaserVisualizer>();

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