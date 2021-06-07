using System;
using System.Collections;
using System.Collections.Generic;
using BattleMage.SpellSystem;
using UnityEngine;
using BattleMage.Enemies;

namespace BattleMage
{
    public class PlayerManager : MonoBehaviour
    {
        #region Field and mono
        public static PlayerManager Instance;
        public static bool PlayerDead => Instance.playerDead;
        bool playerDead = false;


        [SerializeField] Collider col;

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
        #endregion

        public void TakeDamage(int amount = 1)
        {
            currentHealth -= amount;
            uiM.PlayerDamaged();
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
        public void PressedTrigger (bool isLeft)
        {
            spellM.PressedTrigger(isLeft);
        }
        public void ReleasedTrigger(bool isLeft)
        {
            spellM.ReleasedTrigger(isLeft);
        }
        public void PressedToggleSpell (bool isLeft)
        {
            spellM.PressedToggleSpell(isLeft);
        }
        #endregion

        #region Abilities

        #endregion

        #region Minor
        void UpdateHealthDisplay () => uiM.UpdateHealth(currentHealth);
        #endregion
    }
}
