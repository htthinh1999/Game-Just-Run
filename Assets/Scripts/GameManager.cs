using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool GameOver { get; private set; } = false;
    public float Speed = 10f;

    bool paused = false;
    int highScore = 0, score = 0;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] Animator plus1000Ani;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;

    void Awake()
    {
        Instance = this; 
    }
    
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "UIScene")
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            highScoreText.text = highScore.ToString();
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "UIScene")
        {
            if (!GameOver && !paused)
            {
                Speed += 0.001f;
                SetScore(score++);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused)
                {
                    resumeButton.onClick.Invoke();
                }
                else
                {
                    pauseButton.onClick.Invoke();
                }
            }
        }
    }

    void SetScore(int score)
    {
        for(int i=0; i<scoreTexts.Length; i++)
        {
            scoreTexts[i].text = score.ToString();
        }
        highScoreText.text = Mathf.Max(score, highScore).ToString();
    }

    public void AddScore(int score)
    {
        this.score += score;
        SetScore(this.score);
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
        PlayerPrefs.SetInt("HighScore", Mathf.Max(score, highScore));
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

    public void Plus1000(Vector3 playerPos)
    {
        AddScore(1000);
        plus1000Ani.SetTrigger("Bonus");
        plus1000Ani.transform.position = new Vector3(playerPos.x, playerPos.y + 2, 0);
    }

}
