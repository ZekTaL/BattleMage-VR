using System.Collections;
using BattleMage.Enemies;
using UnityEngine;

namespace BattleMage.SpellSystem
{
    /// <summary>
    /// Class that manages the firespell based on the SpellBase class
    /// </summary>
    public class FireballSpell : SpellBase
    {
        //Honest idk why the raycast collision detection is not very reliable, 
        //but we increase the raycast distance just in case.
        const float BonusDist = 1f; 

        [SerializeField] int damage = 10;
        [SerializeField] float lifeDuration = 8f;
        [SerializeField] float moveSpeed = 8f;
        [SerializeField] Collider col;

        Rigidbody rb;
        bool isReleased;

        void Update()
        {
            // when release the spell, shoot the raycast
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

        /// <summary>
        /// Initialize the spell
        /// </summary>
        /// <param name="firepoint">Firepoint where you need to initialize the spell</param>
        public override void Initialize(LaserCaster firepoint)
        {
            base.Initialize(firepoint);
            rb = GetComponent<Rigidbody>();
            firepoint.ToggleLaser(true);
        }

        /// <summary>
        /// Releaase the spell after you hold it
        /// </summary>
        public override void ReleaseSpell()
        {
            //firepoint.ToggleLaser(false);
            MatchFirepointTransform();
            col.enabled = true;
            rb.velocity = transform.forward * moveSpeed;
            isReleased = true;

            Destroy(gameObject, lifeDuration);
        }

        /// <summary>
        /// Destroy the spell
        /// </summary>
        protected override void DestroyBullet()
        {
            base.DestroyBullet();
            FeedbackManager.Instance.SpawnFireballExplosion(transform.position);
        }

        /// <summary>
        /// When the spell hits the ground
        /// </summary>
        protected override void HitsGround()
        {
            DestroyBullet();
        }

        /// <summary>
        /// When the spell hits an enemy
        /// </summary>
        /// <param name="other">The other collider that got hit</param>
        protected override void HitsEnemy(Collider other)
        {
            DestroyBullet();
            EnemyBase e = other.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.TakeDamage(damage);
                e.ExitLiftMode();
            }
        }
    }
}