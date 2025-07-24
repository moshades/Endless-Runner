using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{
    [SerializeField] private int gold = 0;
    [SerializeField] private long score = 0;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI goldText, scoreText;
    [SerializeField] private TextMeshProUGUI finalScore, highScore;

    public bool isGameOver = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) == 0)
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    public void AddGold()
    {
        gold++;
        goldText.text = gold.ToString();
    }

    public void UpdateScore(float offset, float scoreToAdd)
    {
        score = (long)(scoreToAdd - offset);
        scoreText.text = score.ToString("D6");
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        int finalScore = int.Parse(scoreText.text);
        this.finalScore.text = finalScore.ToString();
        if (finalScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
        }
        highScore.text = "High Score\n" + PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void MenuReturn()
    {
        SceneManager.LoadSceneAsync("Title Scene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
