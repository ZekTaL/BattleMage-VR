using System.Collections;
using BattleMage.Enemies;
using UnityEngine;

namespace BattleMage.SpellSystem
{
    public class GravitySpell : SpellBase
    {
        //Honest idk why the raycast collision detection is not very reliable, 
        //but we increase the raycast distance just in case.
        const float BonusDist = 1f; 

        [SerializeField] float lifeDuration = 8f;
        [SerializeField] float moveSpeed = 4f;
        [SerializeField] Collider col;

        Rigidbody rb;
        bool isReleased;

        public override void Initialize(LaserCaster firepoint)
        {
            base.Initialize(firepoint);
            rb = GetComponent<Rigidbody>();
            firepoint.ToggleLaser(true);
        }

        public override void ReleaseSpell()
        {
            firepoint.ToggleLaser(false);
            MatchFirepointTransform();
            col.enabled = true;
            rb.velocity = transform.forward * moveSpeed;
            isReleased = true;

            Destroy(gameObject, lifeDuration);
        }

        protected override void DestroyBullet()
        {
            base.DestroyBullet();
            FeedbackManager.Instance.SpawnFireballExplosion(transform.position);
        }

        void Update()
        {
            if (isReleased)
            {
                //Debug.DrawRay(transform.position, transform.forward * (rb.velocity.magnitude * Time.deltaTime + BonusDist), Color.red);
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rb.velocity.magnitude * Time.deltaTime + BonusDist, combinedLayerMasks))
                {
                    if (IsTargetEnemy(hit.collider))
                    {
                        HitsEnemy(hit.collider);
                    }
                    if (IsTargetGround(hit.collider))
                    {
                        HitsGround();
                    }
                }
            }
            else
            {
                MatchFirepointTransform();
            }
        }

        protected override void HitsGround()
        {
            DestroyBullet();
        }

        protected override void HitsEnemy(Collider other)
        {
            DestroyBullet();
            other.GetComponent<EnemyBase>()?.EnterLiftMode();
        }
    }
}