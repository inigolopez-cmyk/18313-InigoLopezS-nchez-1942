using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public IsoscelesSpawner isoscelesSpawner;
    public IrregularSpawner irregularSpawner;
    public WomanSpawner womanSpawner;

    public float waveInterval = 10f;

    float waveTimer;
    int currentWave = 0;
    int waveCount = 0;
    bool allActivated = false;

    void Start()
    {
        waveTimer = waveInterval;
        ActivateSingle(0);
    }

    void Update()
    {
        if (allActivated) return;

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0)
        {
            currentWave = (currentWave + 1) % 3;
            waveCount++;
            waveTimer = waveInterval;

            if (waveCount >= 3)
            {
                ActivateAll();
                allActivated = true;
                return;
            }

            ActivateSingle(currentWave);
        }
    }

    void ActivateSingle(int waveIndex)
    {
        isoscelesSpawner.enabled = (waveIndex == 0);
        irregularSpawner.enabled = (waveIndex == 1);
        womanSpawner.enabled = (waveIndex == 2);
    }

    void ActivateAll()
    {
        isoscelesSpawner.enabled = true;
        irregularSpawner.enabled = true;
        womanSpawner.enabled = true;
    }
}