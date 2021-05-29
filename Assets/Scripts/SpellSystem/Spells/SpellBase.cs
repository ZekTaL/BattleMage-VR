using System.Collections;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage.SpellSystem
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask enemyLayer;

        private void Awake()
        {
           
        }

        public virtual void Initialize (Transform shootTransform, Vector3 raycastHitPoint)
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (HitsGround(other))
            {
                Destroy(gameObject);
            }
            if (HitsEnemy(other))
            {
                other.GetComponent<EnemyBase>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        bool HitsGround (Collider collider) => groundLayer == (groundLayer | 1 << collider.gameObject.layer);
        bool HitsEnemy (Collider collider) => enemyLayer == (enemyLayer | 1 << collider.gameObject.layer);
    }
}