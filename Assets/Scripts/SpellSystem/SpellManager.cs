using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleMage.SpellSystem
{
    /// <summary>
    /// Class that manages the spells
    /// </summary>
    public class SpellManager : MonoBehaviour
    {
        public static SpellManager Instance;

        public static int leftActive;
        public static int rightActive;

        [SerializeField] SpellHud hud;
        [SerializeField] SpellBase pf_FireSpell;
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

        /// <summary>
        /// Fire the spell when you press the trigger
        /// </summary>
        /// <param name="isLeft">Check on which hand you press the trigger</param>
        public void PressedTrigger(bool isLeft)
        {
            SpellBase pf = GetSpellPf(isLeft ? leftActive : rightActive);
            pf = Instantiate(pf, FirepointOrigin(isLeft), FirepointRotation(isLeft));
            Debug.DrawLine(Vector3.zero, FirepointOrigin(isLeft), Color.red, 0.2f);
            pf.Initialize(GetFirepoint(isLeft));
            if (isLeft)
                holdingSpellLeft = pf;
            else
                holdingSpellRight = pf;
        }

        /// <summary>
        /// Release the spell when you release the trigger
        /// </summary>
        /// <param name="isLeft">Check on which hand you press the trigger</param>
        public void ReleasedTrigger(bool isLeft)
        {
            if (isLeft)
                holdingSpellLeft?.ReleaseSpell();
            else
                holdingSpellRight?.ReleaseSpell();
        }

        /// <summary>
        /// Change the spell type when you press the touchpad
        /// </summary>
        /// <param name="isLeft">Check on which hand you press the trigger</param>
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

        /// <summary>
        /// Swap the spell type in a round robin way
        /// </summary>
        /// <param name="i">Index of the current spell type</param>
        /// <returns>Returns the index of the next spell to use</returns>
        int IncrementIndex(int i)
        {
            if (++i >= spellCounts)
                i = 0;
            return i;
        }

        /// <summary>
        /// Get the firepoint location
        /// </summary>
        /// <param name="isLeft">Check on which hand you press the trigger</param>
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

        /// <summary>
        /// Get the Spell Prefab
        /// </summary>
        /// <param name="index">Index of the spell to retrieve</param>
        SpellBase GetSpellPf(int index) => GetSpellPf((Spells)index);

        /// <summary>
        /// Get the Spell Prefab
        /// </summary>
        /// <param name="spells">Type of the spell to retrieve</param>
        /// <returns></returns>
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