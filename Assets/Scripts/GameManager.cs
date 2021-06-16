using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleMage
{
    /// <summary>
    /// Class that manages the game and the scenes
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        const int SceneIndex_Menu = 0;

        UIManager uiM;

        int kills;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            uiM = UIManager.Instance;
        }

        /// <summary>
        /// Load the Menu scene
        /// </summary>
        public void LoadMenuScene()
        {
            SceneManager.LoadScene(SceneIndex_Menu);
        }

        /// <summary>
        /// Restart the game
        /// </summary>
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }

        /// <summary>
        /// Increase the number of kills
        /// </summary>
        public void IncreaseKill()
        {
            uiM.SetKillScore(++kills);
        }
    }
}