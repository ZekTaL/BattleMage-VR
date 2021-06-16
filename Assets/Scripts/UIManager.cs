using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the game UI
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        const float FadeSpeed = 2f;

        public static UIManager Instance;

        [SerializeField] Text healthText;
        [SerializeField] Text killText;
        [SerializeField] Image damageRed;
        [SerializeField] GameObject GameOverGroup_PC;
        [SerializeField] GameObject GameOverGroup_VR;

        //Status
        float damageAlpha;

        void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            // if the player got damaged i flash the screen red
            if (damageAlpha > 0f)
            {
                damageAlpha -= Time.deltaTime * FadeSpeed;
                Color c = damageRed.color;
                c.a = damageAlpha;
                damageRed.color = c;
            }
        }

        #region Combat
        /// <summary>
        /// Update the kill text
        /// </summary>
        /// <param name="kills"></param>
        public void SetKillScore (int kills)
        {
            killText.text = kills.ToString();
        }

        /// <summary>
        /// Damage the player
        /// </summary>
        public void PlayerDamaged ()
        {
            damageAlpha = 1f;
        }

        #endregion

        #region Gameover

        /// <summary>
        /// Enable the GameOver screen
        /// </summary>
        public void RevealGameOverScreen()
        {
            PCCursorManager.RevealCursor();
            if(RigManager.Instance.inVR)
            {
                GameOverGroup_VR.SetActive(true);
            }
            else
            {
                GameOverGroup_PC.SetActive(true);
            }
        }

        /// <summary>
        /// Restart the game
        /// </summary>
        public void Clicked_RestartGame ()
        {
            GameManager.Instance.RestartGame();
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void Clicked_QuitGame ()
        {
            GameManager.Instance.LoadMenuScene();
        }
        #endregion

        #region HUD

        /// <summary>
        /// Update the Health text in the UI
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateHealth (int amount)
        {
            healthText.text = amount.ToString();
        }

        #endregion
    }
}