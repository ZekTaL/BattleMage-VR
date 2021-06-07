using System;
using System.Collections;
using System.Collections.Generic;
using BattleMage.SpellSystem;
using UnityEngine;

namespace BattleMage
{
    public class PlayerManager : MonoBehaviour
    {
        #region Field and mono
        public static PlayerManager Instance;
        public static bool PlayerDead;

        [SerializeField] Collider col;

        RigManager rigManager;
        UIManager uiM;
        SpellManager spellM;
        int currentHealth = 1000000;
        
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
