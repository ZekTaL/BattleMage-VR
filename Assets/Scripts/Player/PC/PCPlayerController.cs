using System.Collections;
using UnityEngine;
using BattleMage;

namespace BattleMage.PC
{
    public class PCPlayerController : MonoBehaviour
    {
        PlayerManager playerM;

        void Start()
        {
            playerM = PlayerManager.Instance;
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
                playerM.PressedTrigger(true);
            }
            if (Input.GetMouseButtonUp(0)) //Release
            {
                playerM.ReleasedTrigger(true);
            }

            //Right
            if (Input.GetMouseButtonDown(1)) //Down
            {
                playerM.PressedTrigger(false);
            }
            if (Input.GetMouseButtonUp(1)) //Release
            {
                playerM.ReleasedTrigger(false);
            }

            //Cycle spells
            if (Input.GetKeyDown(KeyCode.Q))
            {
                playerM.PressedToggleSpell(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerM.PressedToggleSpell(false);
            }
        }
    }
}