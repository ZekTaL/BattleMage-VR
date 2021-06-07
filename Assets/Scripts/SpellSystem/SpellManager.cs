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
        //[SerializeField] SpellBase pf_GroundSpell;
        [SerializeField] SpellBase pf_PsychicSpell;

        RigManager rigM;
        FirePointReferencer firePoints;

        //Status
        SpellBase holdingSpellLeft;
        SpellBase holdingSpellRight;

        //Cache
        int spellCounts;

        void Awake()
        {
            Instance = this;
            spellCounts = Enum.GetValues(typeof(Spells)).Length;
            hud.UpdateLeft();
            hud.UpdateRight();
        }

        void Start()
        {
            rigM = RigManager.Instance;
            firePoints = FirePointReferencer.Instance;
        }

        public void PressedTrigger(bool isLeft)
        {
            SpellBase pf = GetSpellPf(isLeft ? leftActive : rightActive);
            pf = Instantiate(pf, FirepointOrigin(isLeft), FirepointRotation(isLeft));
            pf.Initialize(GetFirepoint(isLeft));
            if (isLeft)
                holdingSpellLeft = pf;
            else
                holdingSpellRight = pf;
        }

        public void ReleasedTrigger(bool isLeft)
        {
            if (isLeft)
                holdingSpellLeft?.ReleaseSpell();
            else
                holdingSpellRight?.ReleaseSpell();
        }

        public void PressedToggleSpell(bool isLeft)
        {
            if (isLeft)
            {
                leftActive = IncrementIndex(leftActive);
                hud.UpdateLeft();
            }
            else
            {
                rightActive = IncrementIndex(rightActive);
                hud.UpdateRight();
            }
        }

        #region helper
        int IncrementIndex(int i)
        {
            if (++i >= spellCounts)
                i = 0;
            return i;
        }

        LaserCaster GetFirepoint(bool isLeft) => isLeft ? firePoints.Left : firePoints.Right;

        Vector3 FirepointOrigin(bool isLeft) =>
            isLeft
            ? firePoints.Left.transform.position
            : firePoints.Right.transform.position;

        Vector3 FirepointHitPoint(bool isLeft) =>
            isLeft
            ? firePoints.Left.HitPosition
            : firePoints.Right.HitPosition;

        Quaternion FirepointRotation(bool isLeft) =>
            isLeft
            ? firePoints.Left.transform.rotation
            : firePoints.Right.transform.rotation;

        SpellBase GetSpellPf(int index) => GetSpellPf((Spells)index);
        SpellBase GetSpellPf(Spells spells)
        {
            return spells switch
            {
                Spells.Fireball => pf_FireSpell,
                //Spells.GroundCrack => pf_GroundSpell,
                _ => pf_PsychicSpell,
            };
        }
        #endregion
    }
}