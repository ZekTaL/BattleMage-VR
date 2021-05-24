using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleMage.PC
{
    public class PCCursorManager : MonoBehaviour
    {
        public static void TogglePCCursor(bool isActive)
        {
            if (isActive)
                RevealCursor();
            else
                HideCursor();
        }

        public static void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static void RevealCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}