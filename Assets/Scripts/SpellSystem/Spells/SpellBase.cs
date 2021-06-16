using System.Collections;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage.SpellSystem
{
    /// <summary>
    /// Base abstract class for the spell
    /// </summary>
    public abstract class SpellBase : MonoBehaviour
    {
        // Define a ground layer in the raycast
        [SerializeField] LayerMask groundLayer;
        // Define an enemy layer in the raycast
        [SerializeField] LayerMask enemyLayer;

        protected LayerMask combinedLayerMasks;
        protected LaserCaster firepoint;
        protected Transform firepointTrans;

        /// <summary>
        /// Initialize the spell
        /// </summary>
        /// <param name="firepoint">LaserCaster of the spell</param>
        public virtual void Initialize(LaserCaster firepoint)
        {
            this.firepoint = firepoint;
            firepointTrans = firepoint.transform;
            combinedLayerMasks = groundLayer | enemyLayer;
        }

        /// <summary>
        /// Releaase the spell after you hold it
        /// </summary>
        public virtual void ReleaseSpell ()
        {

        }

        /// <summary>
        /// Destroy the spell
        /// </summary>
        protected virtual void DestroyBullet()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// When the spell hits the ground
        /// </summary>
        protected virtual void HitsGround()
        {
            //DestroyBullet();
        }

        /// <summary>
        /// When the spell hits an enemy
        /// </summary>
        /// <param name="other">The other collider that got hit</param>
        protected virtual void HitsEnemy(Collider other)
        {
            //DestroyBullet();
            //other.GetComponent<EnemyBase>()?.TakeDamage(damage);
        }
    
        /// <summary>
        /// Align the firepoint transforms
        /// </summary>
        protected void MatchFirepointTransform ()
        {
            transform.position = firepointTrans.position;
            transform.rotation = firepointTrans.rotation;
        }

        /// <summary>
        /// Check if the collider hit is the ground
        /// </summary>
        /// <param name="collider">The collider that got hit</param>
        protected bool IsTargetGround(Collider collider) => groundLayer == (groundLayer | 1 << collider.gameObject.layer);
        /// <summary>
        /// Check if the collider hit is an enemy
        /// </summary>
        /// <param name="collider">The collider that got hit</param>
        protected bool IsTargetEnemy(Collider collider) => enemyLayer == (enemyLayer | 1 << collider.gameObject.layer);
    }
}