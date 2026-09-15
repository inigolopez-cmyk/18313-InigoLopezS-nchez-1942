using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public IsoscelesSpawner isoscelesSpawner;
    public IrregularSpawner irregularSpawner; 
    public WomanSpawner womanSpawner;    

    bool wave1Done = false;
    bool wave2Done = false;
    bool wave3Done = false;

    void Start()
    {
        isoscelesSpawner.enabled = false;
        irregularSpawner.enabled = false;
        womanSpawner.enabled = false;
    }

    void Update()
    {
        float elapsed = 300 - GameManager.Instance.GetGameTime();

        if (elapsed >= 5 && !wave1Done)
        {
            isoscelesSpawner.enabled = true;
            wave1Done = true;
        }

        if (elapsed >= 20 && !wave2Done)
        {
            irregularSpawner.enabled = true;
            wave2Done = true;
        }

        if (elapsed >= 40 && !wave3Done)
        {
            womanSpawner.enabled = true;
            wave3Done = true;
        }
    }
}