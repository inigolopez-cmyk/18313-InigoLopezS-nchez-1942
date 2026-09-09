using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    //public TMP_Text highScoreText;
    public Slider volumeSlider;

    void Start()
    {
        // Mostrar el highscore guardado
        //int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        //highScoreText.text = "High Score: " + savedHighScore;

        // Cargar el volumen guardado (si existe) y aplicarlo al slider
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.001f); // 1f = volumen al 100% por defecto
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1); // reemplaza "1" por el índice real de tu escena de juego en Build Settings
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value); // guarda la preferencia para la próxima vez
    }
}
