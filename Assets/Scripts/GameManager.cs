using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameOver { get; private set; } = false;
    public float Speed = 4f;

    bool paused = false;
    int score = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] GameObject gameOverScreen;

    void Awake()
    {
        Instance = this; 
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!GameOver && !paused && SceneManager.GetActiveScene().name != "UIScene")
        {
            Speed += 0.001f;
            SetScore(score++);
        }
    }

    void SetScore(int score)
    {
        for(int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = score.ToString();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Speed = 0;
        GameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenuGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
