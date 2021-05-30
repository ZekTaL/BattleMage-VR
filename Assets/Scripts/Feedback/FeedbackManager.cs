using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class FeedbackManager : MonoBehaviour
    {
        public static FeedbackManager Instance;

        [SerializeField] GameObject pfx_fireballExplosion;

        void Awake()
        {
            Instance = this;
        }

        public void SpawnFireballExplosion (Vector3 pos)
        {
            GameObject go = Instantiate(pfx_fireballExplosion, pos, Quaternion.identity) as GameObject;
            Destroy(go, 2f);
        }
    }
}