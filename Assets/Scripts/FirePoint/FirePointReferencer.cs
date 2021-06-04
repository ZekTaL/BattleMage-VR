using System.Collections;
using UnityEngine;

namespace BattleMage
{
    public class FirePointReferencer : MonoBehaviour
    {
        public static FirePointReferencer Instance;
        [SerializeField] LaserCaster PCLeft;
        [SerializeField] LaserCaster PCRight;
        [SerializeField] LaserCaster VRLeft;
        [SerializeField] LaserCaster VRRight;

        RigManager rigManager;
        bool inVR;

        public LaserCaster Left => inVR ? VRLeft : PCLeft;
        public LaserCaster Right => inVR ? VRRight : PCRight;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            rigManager = RigManager.Instance;
            inVR = rigManager.inVR;
        }
    }
}