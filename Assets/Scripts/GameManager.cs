using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject winPanel;

    int score = 0;
    int lives = 3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetWave(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;

        string heartsDisplay = "";
        for (int i = 0; i < lives; i++)
        {
            heartsDisplay += "♥ ";
        }
        livesText.text = heartsDisplay;
    }
}