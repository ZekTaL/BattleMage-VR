using System.Collections;
using UnityEngine;

namespace BattleMage
{
    [DefaultExecutionOrder(-10000)]
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        
        [SerializeField] Color colorInvalid = Color.red;
        [SerializeField] Color colorValid = Color.green;
        public Color ColorValid => colorValid;
        public Color ColorInvalid => colorInvalid;

        private void Awake()
        {
            Instance = this;
        }
    }
}