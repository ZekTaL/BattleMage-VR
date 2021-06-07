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