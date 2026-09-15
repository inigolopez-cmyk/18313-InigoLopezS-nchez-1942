using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of the GameManager class

    public bool isPlaying;

    [SerializeField]
    private float gameTime;


    [SerializeField]
    private TMP_Text gameTimeText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        isPlaying = true;

        gameTime = 300;
        UpdateGameTimeText();


    }

    void Update()
    {
        if (isPlaying)
        {
            gameTime -= Time.deltaTime;
            if (gameTime <= 0)
            {
                isPlaying = false;

            }


            if (isPlaying == false)
            {
                gameTime = 0;
            }

            UpdateGameTimeText();
        }


    }

    void UpdateGameTimeText()
    {
        int min = (int)gameTime / 60;
        int sec = (int)gameTime % 60;
        gameTimeText.text = min.ToString("00") + ":" + sec.ToString("00"); 
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public void PlayerDied()
    {
        isPlaying = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }
}
