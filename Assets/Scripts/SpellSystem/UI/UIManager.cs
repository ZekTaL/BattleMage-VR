using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage
{
    public class UIManager : MonoBehaviour
    {
        const float FadeSpeed = 2f;

        public static UIManager Instance;

        [SerializeField] GameObject GameOverGroup;
        [SerializeField] Text healthText;
        [SerializeField] Text killText;
        [SerializeField] Image damageRed;

        //Status
        float damageAlpha;

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

        #region Combat
        public void SetKillScore (int kills)
        {
            killText.text = kills.ToString();
        }

        public void PlayerDamaged ()
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