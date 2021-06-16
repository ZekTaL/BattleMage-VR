using System.Collections;
using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Class that references the firepoint in both PC and VR
    /// </summary>
    public class FirePointReferencer : MonoBehaviour
    {
        public static FirePointReferencer Instance;

        [SerializeField] LaserCaster PCLeft;
        [SerializeField] LaserCaster PCRight;
        [SerializeField] LaserCaster VRLeft;
        [SerializeField] LaserCaster VRRight;

        RigManager rigManager;

        /// <summary>
        /// Laser of the Left hand
        /// </summary>
        public LaserCaster Left
        {
            get
            {
                Debug.Log("rigManager.inVR: " + rigManager.inVR);
                Debug.DrawLine(new Vector3(0, 10f, 0f), VRLeft.transform.position, Color.yellow, 0.2f);
                Debug.DrawLine(Vector3.zero, PCLeft.transform.position, Color.blue, 0.2f);

                return rigManager.inVR? VRLeft : PCLeft;
            }
        }

        /// <summary>
        /// Laser of the Right hand
        /// </summary>
        public LaserCaster Right => rigManager.inVR ? VRRight : PCRight;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            rigManager = RigManager.Instance;
        }
    }
}