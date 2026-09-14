using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource gameMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameMusic.ignoreListenerPause = true;
        gameMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
