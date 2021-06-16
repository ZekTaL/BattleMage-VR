using System;
using System.Collections;
using System.Collections.Generic;
using BattleMage.SpellSystem;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the player
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] Collider col;

        public static PlayerManager Instance;
        public static bool PlayerDead => Instance.playerDead;
        bool playerDead = false;

        RigManager rigManager;
        UIManager uiM;
        SpellManager spellM;
        int currentHealth = 100;
        
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            uiM = UIManager.Instance;
            rigManager = RigManager.Instance;
            spellM = SpellManager.Instance;

            UpdateHealthDisplay();
        }

        /// <summary>
        /// When the player takes damage
        /// </summary>
        /// <param name="amount">amount of damage taken</param>
        public void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            // damage the player
            uiM.PlayerDamaged();
            // Update the health UI
            UpdateHealthDisplay();

            if (currentHealth <= 0)
            {
                col.enabled = false;
                //model.SetActive(false);

                uiM.RevealGameOverScreen();

                playerDead = true;
                

                var enemies = FindObjectsOfType<EnemyBase>();
                foreach (var e in enemies)
                {
                    Destroy(e.gameObject);
                }

                //Time.timeScale = 0f;
            }
        }

        #region Interactions

        /// <summary>
        /// Call this function when the player press the trigger
        /// </summary>
        /// <param name="isLeft">Which hand pressed the trigger</param>
        public void PressedTrigger (bool isLeft)
        {
            spellM.PressedTrigger(isLeft);
        }

        /// <summary>
        /// Call this function when the player released the trigger
        /// </summary>
        /// <param name="isLeft">Which hand released the trigger</param>
        public void ReleasedTrigger(bool isLeft)
        {
            spellM.ReleasedTrigger(isLeft);
        }

        /// <summary>
        /// Call this function when the player press the touchpad
        /// </summary>
        /// <param name="isLeft">Which hand pressed the touchpad</param>
        public void PressedToggleSpell (bool isLeft)
        {
            spellM.PressedToggleSpell(isLeft);
        }

        #endregion

        /// <summary>
        /// Update the Health in the HUD
        /// </summary>
        void UpdateHealthDisplay () => uiM.UpdateHealth(currentHealth);
    }
}
