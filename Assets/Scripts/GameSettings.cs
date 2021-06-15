using System.Collections;
using UnityEngine;

namespace BattleMage
{
    [DefaultExecutionOrder(-10000)]
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        // Color of the valid laser caster
        [SerializeField] private Color colorInvalid = new Color32(125, 125, 125, 185);
        // Color of the invalid laser caster
        [SerializeField] private Color colorValid = new Color32(236, 255, 0, 170);
        public Color ColorValid => colorValid;
        public Color ColorInvalid => colorInvalid;

        private void Awake()
        {
            Instance = this;
        }
    }
}