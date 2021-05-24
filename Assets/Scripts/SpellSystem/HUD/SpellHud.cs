using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage.SpellSystem
{
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
                {Spells.GroundCrack, icon_GroundCrack},
                {Spells.Psychic, icon_Psychic},
            };
        }

        public void UpdateLeft ()
        {
            iconSlot_LeftActive.sprite = GetSprite((Spells)SpellManager.leftActive);
        }

        public void UpdateRight()
        {
            iconSlot_RightActive.sprite = GetSprite((Spells)SpellManager.rightActive);
        }

        #region helpers
        Sprite GetSprite (Spells spell)
        {
            return spell switch
            {
                Spells.Fireball => icon_FireBall,
                Spells.GroundCrack => icon_GroundCrack,
                _ => icon_Psychic,
            };
        }
        #endregion
    }
}