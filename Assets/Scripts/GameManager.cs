using UnityEngine;
using TMPro;

//  manager for score, lives, waves and ui
// other scripts talk to this via GameManager.Instance
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
        Instance = this; // reference so other scripts can reach this
    }

    void Start()
    {
        UpdateUI(); // make sure the ui shows the correct values from the start
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
        // game over is handled by PlayerHealth, not here
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
        Time.timeScale = 0f; // pauses the game when game over panel
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f; // pause the game when win panel shows
    }

    public void Restart()
    {
        Time.timeScale = 1f; // unpause before reloading
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // reload startscreen (scene index 0) > so the startsScreenScene
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;

        // build the hearts string based on how many lives are left
        string heartsDisplay = "";
        for (int i = 0; i < lives; i++)
        {
            heartsDisplay += "♥ ";
        }
        livesText.text = heartsDisplay;
    }
}