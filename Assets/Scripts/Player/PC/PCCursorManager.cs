using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the Cursor in the PC version
    /// </summary>
    public static class PCCursorManager
    {
        /// <summary>
        /// Lock or Reveal the cursor in game
        /// </summary>
        public static void TogglePCCursor(bool isActive)
        {
            if (isActive)
                RevealCursor();
            else
                HideCursor();
        }

        /// <summary>
        /// Lock the cursor 
        /// </summary>
        public static void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        /// Reset the cursor
        /// </summary>
        public static void RevealCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}