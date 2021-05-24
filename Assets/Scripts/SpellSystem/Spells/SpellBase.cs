using System.Collections;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage.SpellSystem
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        [SerializeField] int moveSpeed = 20;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask enemyLayer;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * moveSpeed;
            Destroy(gameObject, 8f);
        }

        void OnTriggerEnter(Collider other)
        {
            if (HitsGround(other))
            {
                Debug.Log("hits ground");
                Destroy(gameObject);
            }
            if (HitsEnemy(other))
            {
                Debug.Log("hits enemy");
                other.GetComponent<EnemyBase>()?.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        bool HitsGround (Collider collider) => groundLayer == (groundLayer | 1 << collider.gameObject.layer);
        bool HitsEnemy (Collider collider) => enemyLayer == (enemyLayer | 1 << collider.gameObject.layer);
    }
}