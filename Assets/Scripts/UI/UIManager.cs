using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] GameObject GameOverGroup;
        [SerializeField] Text healthText;
        [SerializeField] Image damageRed;

        //Damage red
        const float FadeSpeed = 2f;

        void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (damageAlpha > 0f)
            {
                damageAlpha -= Time.deltaTime * FadeSpeed;
                Color c = damageRed.color;
                c.a = damageAlpha;
                damageRed.color = c;
            }
        }

        #region Damage
        float damageAlpha;
        public void GetDamaged ()
        {
            damageAlpha = 1f;
        }
        #endregion

        #region Gameover
        public void RevealGameOverScreen()
        {
            PCCursorManager.RevealCursor();

            Instance.GameOverGroup.SetActive(true);
        }

        public void Clicked_RestartGame ()
        {
            GameManager.Instance.RestartGame();
        }

        public void Clicked_QuitGame ()
        {
            GameManager.Instance.LoadMenuScene();
        }
        #endregion

        #region HUD
        public void UpdateHealth (int amount)
        {
            healthText.text = amount.ToString();
        }
        #endregion
    }
}