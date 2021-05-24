using System.Collections;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage.SpellSystem
{
    public abstract class SpellBase : MonoBehaviour
    {
        [SerializeField] int damage = 10;
        [SerializeField] int moveSpeed = 50;
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * moveSpeed;
        }

        void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<EnemyBase>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}