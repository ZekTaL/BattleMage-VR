using System.Collections;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage.SpellSystem
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask enemyLayer;

        protected LayerMask combinedLayerMasks;
        protected Firepoint firepoint;
        protected Transform firepointTrans;

        public virtual void Initialize(Firepoint firepoint)
        {
            this.firepoint = firepoint;
            firepointTrans = firepoint.transform;
            combinedLayerMasks = groundLayer | enemyLayer;
        }

        public virtual void ReleaseSpell ()
        {

        }

        protected virtual void DestroyBullet()
        {
            Destroy(gameObject);
        }

        protected virtual void HitsGround()
        {
            //DestroyBullet();
        }

        protected virtual void HitsEnemy(Collider other)
        {
            //DestroyBullet();
            //other.GetComponent<EnemyBase>()?.TakeDamage(damage);
        }
    
        protected void MatchFirepointTransform ()
        {
            transform.position = firepointTrans.position;
            transform.rotation = firepointTrans.rotation;
        }

        protected bool IsTargetGround(Collider collider) => groundLayer == (groundLayer | 1 << collider.gameObject.layer);
        protected bool IsTargetEnemy(Collider collider) => enemyLayer == (enemyLayer | 1 << collider.gameObject.layer);

        //void OnTriggerEnter(Collider other)
        //{
        //    if (IsTargetGround(other))
        //    {
        //        HitsGround();
        //    }
        //    if (IsTargetEnemy(other))
        //    {
        //        HitsEnemy(other);
        //    }
        //}
    }
}