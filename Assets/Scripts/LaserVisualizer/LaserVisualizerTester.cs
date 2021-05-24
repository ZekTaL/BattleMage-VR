using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class LaserVisualizerTester : MonoBehaviour
    {
        [SerializeField] LaserVisualizer laser;


        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                laser.UpdateLaser();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                laser.TurnOffLaser();
            }
        }
    }
}
