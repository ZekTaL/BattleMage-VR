using System.Collections;
using UnityEngine;

namespace BattleMage
{
    public class FirePointReferencer : MonoBehaviour
    {
        public static FirePointReferencer Instance;
        [SerializeField] Firepoint PCLeft;
        [SerializeField] Firepoint PCRight;
        [SerializeField] Firepoint VRLeft;
        [SerializeField] Firepoint VRRight;

        RigManager rigManager;
        bool inVR;

        public Firepoint Left => inVR ? VRLeft : PCLeft;
        public Firepoint Right => inVR ? VRRight : PCRight;

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