using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text lifesText;
    //public TMP_Text highScoreText; 
    public int score;
    public GameObject gameOverPanel;

    [SerializeField] 
    private AudioSource gameOverAudio;


    void Start()
    {
        Time.timeScale = 1;

        // Muestra el highscore guardado apenas empieza la partida
        //int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        //highScoreText.text = "High Score: " + savedHighScore;
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Points: " + score.ToString();
    }

    //public void SaveHighScore()
    //{
    //    int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

    //    if (score > savedHighScore)
    //    {
    //        PlayerPrefs.SetInt("HighScore", score);
    //    }
    //}

    public void AddLifes(int value)
    {
        lifesText.text = "Lifes: " + value;
    }


    public void OpenGameOver()
    {
        gameOverAudio.ignoreListenerPause = true;
        gameOverAudio.Play();
        gameOverPanel.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
