using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource gameMusic;

    void Start()
    {
        //gameMusic.ignoreListenerPause = true;
        gameMusic.Play();
    }

}
