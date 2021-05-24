using System.Collections;
using UnityEngine;
using BattleMage.SpellSystem;

namespace BattleMage.PC
{
    public class PCPlayerController : MonoBehaviour
    {
        SpellManager spellM;



        void Start()
        {
            spellM = SpellManager.Instance;
        }

        void Update()
        {
            InputUpdate();
        }

        void InputUpdate ()
        {
            if (Input.GetMouseButtonDown(0))
            {
                spellM.HoldDownLeftTrigger();
            }
            if (Input.GetMouseButtonDown(1))
            {
                spellM.HoldDownRightTrigger();
            }
            if (Input.GetMouseButtonUp(0))
            {
                spellM.ShootLeft();
            }
            if (Input.GetMouseButtonUp(1))
            {
                spellM.ShootRight();
            }
        }
    }
}