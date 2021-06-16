using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the feedbacks of the spells
    /// </summary>
    public class FeedbackManager : MonoBehaviour
    {
        public static FeedbackManager Instance;

        [SerializeField] GameObject pfx_fireballExplosion;
        [SerializeField] GameObject pfx_gravityExplosion;

        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Create a little explosion when the fireball spell hit something
        /// </summary>
        /// <param name="pos">position of the explosion</param>
        public void SpawnFireballExplosion (Vector3 pos)
        {
            GameObject go = Instantiate(pfx_fireballExplosion, pos, Quaternion.identity) as GameObject;
            Destroy(go, 2f);
        }

        /// <summary>
        /// Create a little explosion when the gravityspell hit something
        /// </summary>
        /// <param name="pos">position of the explosion</param>
        public void SpawnGravityExplosion(Vector3 pos)
        {
            GameObject go = Instantiate(pfx_gravityExplosion, pos, Quaternion.identity) as GameObject;
            Destroy(go, 2f);
        }
    }
}