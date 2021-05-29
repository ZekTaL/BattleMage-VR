using System.Collections;
using UnityEngine;
using BattleMage;
using BattleMage.SpellSystem;

namespace BattleMage.PC
{
    public class PCPlayerController : MonoBehaviour
    {
        SpellManager spellM;
        RigManager rigM;
        [SerializeField] LaserVisualizer leftLaser;
        [SerializeField] LaserVisualizer rightLaser;

        Vector3 leftHitPoint;
        Vector3 rightHitPoint;

        void Start()
        {
            spellM = SpellManager.Instance;
            rigM = RigManager.Instance;
        }

        void Update()
        {
            InputUpdate();
        }

        void InputUpdate ()
        {
            //Left
            if (Input.GetMouseButtonDown(0)) //Down
            {
                leftLaser.EnableLaser();
            }
            if (Input.GetMouseButton(0)) //Hold
            {
                leftHitPoint = leftLaser.HitPosition;
            }
            if (Input.GetMouseButtonUp(0)) //Release
            {
                spellM.ShootLeft(leftLaser.transform, leftHitPoint);
                leftLaser.DisableLaser();
            }

            //Right
            if (Input.GetMouseButtonDown(1)) //Down
            {
                rightLaser.EnableLaser();
            }
            if (Input.GetMouseButton(0)) //Hold
            {
                rightHitPoint = rightLaser.HitPosition;
            }
            if (Input.GetMouseButtonUp(1)) //Release
            {
                spellM.ShootRight(rightLaser.transform, rightHitPoint);
                rightLaser.DisableLaser();
            }
        }
    }
}