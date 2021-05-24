using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleMage.Managers;

namespace BattleMage.SpellSystem
{
    public class SpellManager : MonoBehaviour
    {
        public static SpellManager Instance;

        public static int leftActive;
        public static int rightActive;

        [SerializeField] SpellHud hud;
        [SerializeField] GameObject pf_FireSpell;
        [SerializeField] GameObject pf_GroundSpell;
        [SerializeField] GameObject pf_PsychicSpell;

        RigManager rigM;
        int spellCounts;

        private void Awake()
        {
            Instance = this;
            spellCounts = Enum.GetValues(typeof(Spells)).Length;
            hud.UpdateLeft();
            hud.UpdateRight();
        }

        private void Start()
        {
            rigM = RigManager.Instance;
        }

        private void Update()
        {
            //quick test
            if (Input.GetKeyDown(KeyCode.A))
                CycleLeft();
            if (Input.GetKeyDown(KeyCode.S))
                CycleRight();
        }

        public void HoldDownLeftTrigger ()
        {
            //Activate laser visualizer
            Debug.Log("press left");
        }

        public void HoldDownRightTrigger()
        {
            //Activate laser visualizer
            Debug.Log("press right");
        }

        public void ShootLeft ()
        {
            GameObject pf = GetSpellPf(leftActive);
            pf = Instantiate(pf, GetLeftShootPoint().position, GetLeftShootPoint().rotation);
        }
        public void ShootRight()
        {
            GameObject pf = GetSpellPf(rightActive);
            pf = Instantiate(pf, GetRightShootPoint().position, GetRightShootPoint().rotation);
        }

        public void CycleLeft ()
        {
            leftActive = IncrementIndex(leftActive);
            hud.UpdateLeft();
        }
        public void CycleRight()
        {
            rightActive = IncrementIndex(rightActive);
            hud.UpdateRight();
        }

        #region helper
        int IncrementIndex(int i)
        {
            if (++i >= spellCounts)
                i = 0;
            return i;
        }

        GameObject GetSpellPf(int index) => GetSpellPf((Spells)index);
        GameObject GetSpellPf (Spells spells)
        {
            return spells switch
            {
                Spells.Fireball => pf_FireSpell,
                Spells.GroundCrack => pf_GroundSpell,
                _ => pf_PsychicSpell,
            };
        }

        Transform GetLeftShootPoint() => rigM.shootPointLeft;
        Transform GetRightShootPoint() => rigM.shootPointRight;
        #endregion
    }
}