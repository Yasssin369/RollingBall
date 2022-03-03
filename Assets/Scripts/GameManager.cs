using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endScreen;
    public TMP_Text score;
    public static GameManager gameManager;
    string currentSceneName; 
    private void Awake()
    {
        
        gameManager = this;
    }
    void Start()
    {
         currentSceneName = SceneManager.GetActiveScene().name;
    }

    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void EndGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        endScreen.SetActive(true);
        score.SetText(ScoreSystem._points.ToString());
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}