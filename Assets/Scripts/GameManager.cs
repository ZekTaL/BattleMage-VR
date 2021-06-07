using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleMage
{
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

        //---------------------
        public void LoadMenuScene()
        {
            SceneManager.LoadScene(SceneIndex_Menu);
        }

        public void RestartGame()
        {
            Debug.Log("RESTART!!!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }



        public void IncreaseKill()
        {
            uiM.SetKillScore(++kills);
        }
    }
}