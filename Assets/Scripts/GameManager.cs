using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    const int SceneIndex_Menu = 0;

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(SceneIndex_Menu);
    }

    public void RestartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Awake()
    {
        Instance = this;
    }
}
