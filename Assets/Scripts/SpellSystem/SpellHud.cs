using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage.SpellSystem
{
    /// <summary>
    /// Class that manages to update the spells UI
    /// </summary>
    public class SpellHud : MonoBehaviour
    {
        [SerializeField] Sprite icon_FireBall;
        [SerializeField] Sprite icon_GroundCrack;
        [SerializeField] Sprite icon_Psychic;
        [SerializeField] Image iconSlot_LeftActive;
        [SerializeField] Image iconSlot_RightActive;


        Dictionary<Spells, Sprite> spriteLookup;


        private void Awake()
        {
            //Init
            spriteLookup = new Dictionary<Spells, Sprite>()
            {
                {Spells.Fireball, icon_FireBall },
                //{Spells.GroundCrack, icon_GroundCrack},
                {Spells.Psychic, icon_Psychic},
            };
        }

        /// <summary>
        /// Update the UI of the left hand spell
        /// </summary>
        public void UpdateLeft ()
        {
            iconSlot_LeftActive.sprite = GetSprite((Spells)SpellManager.leftActive);
        }

        /// <summary>
        /// Update the UI of the right hand spell
        /// </summary>
        public void UpdateRight()
        {
            iconSlot_RightActive.sprite = GetSprite((Spells)SpellManager.rightActive);
        }

        #region helpers

        /// <summary>
        /// Get the sprite of the spell to display
        /// </summary>
        /// <param name="spell">type of the spell</param>
        Sprite GetSprite (Spells spell)
        {
            return spell switch
            {
                Spells.Fireball => icon_FireBall,
                //Spells.GroundCrack => icon_GroundCrack,
                _ => icon_Psychic,
            };
        }

        #endregion
    }
}