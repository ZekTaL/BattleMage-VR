using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage
{
    public class PlayerManager : MonoBehaviour
    {
        UIManager uiM;
        int currentHealth = 100;

        public void TakeDamage(int amount = 1)
        {
            Debug.Log("take damage " + amount);

            currentHealth -= amount;
            UpdateHealthDisplay();
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            uiM = UIManager.Instance;
            UpdateHealthDisplay();
        }
            
        void UpdateHealthDisplay () => uiM.UpdateHealth(currentHealth);
    }
}
