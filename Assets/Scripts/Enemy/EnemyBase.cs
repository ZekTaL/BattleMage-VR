using System.Collections;
using UnityEngine;

namespace BattleMage.Enemies
{
    public abstract class EnemyBase : MonoBehaviour
    {
        [SerializeField] int maxHealth = 10;
        int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}