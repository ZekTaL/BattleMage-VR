using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage.SpellSystem
{
    public class SpellManager : MonoBehaviour
    {
        public static SpellManager Instance;

        public static int leftActive;
        public static int rightActive;

        [SerializeField] SpellHud hud;
        [SerializeField] SpellBase pf_FireSpell;
        [SerializeField] SpellBase pf_GroundSpell;
        [SerializeField] SpellBase pf_PsychicSpell;

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
            if (Input.GetKeyDown(KeyCode.Q))
                CycleLeft();
            if (Input.GetKeyDown(KeyCode.E))
                CycleRight();
        }

        public void ShootLeft (Transform shootTransform, Vector3 hitPoint)
        {
            SpellBase pf = GetSpellPf(leftActive);
            pf = Instantiate(pf, shootTransform.position, shootTransform.rotation);
            pf.Initialize(shootTransform, hitPoint);
        }
        public void ShootRight(Transform shootTransform, Vector3 hitPoint)
        {
            SpellBase pf = GetSpellPf(rightActive);
            pf = Instantiate(pf, shootTransform.position, shootTransform.rotation);
            pf.Initialize(shootTransform, hitPoint);
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

        SpellBase GetSpellPf(int index) => GetSpellPf((Spells)index);
        SpellBase GetSpellPf (Spells spells)
        {
            return spells switch
            {
                Spells.Fireball => pf_FireSpell,
                Spells.GroundCrack => pf_GroundSpell,
                _ => pf_PsychicSpell,
            };
        }

        #endregion
    }
}