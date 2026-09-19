using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public IsoscelesSpawner isoscelesSpawner;
    public IrregularSpawner irregularSpawner;
    public WomanSpawner womanSpawner;

    public float waveInterval = 10f; // cada cuántos segundos rota la oleada activa

    float waveTimer; // cuenta regresiva hasta la próxima rotación de oleada
    int currentWave = 0; // índice del enemigo activo: 0 = triángulo, 1 = blob, 2 = mujer
    int waveCount = 0; // cuántas veces ha rotado la oleada hasta ahora
    bool allActivated = false; // true cuando ya se activaron los 3 spawners para siempre

    void Start()
    {
        waveTimer = waveInterval;
        ActivateSingle(0); // arranca el juego con solo el triángulo activo
    }

    void Update()
    {
        if (allActivated) return; // si ya se activaron los 3, no hay nada más que hacer aquí

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0)
        {
            currentWave = (currentWave + 1) % 3; // avanza al siguiente enemigo (0 -> 1 -> 2 -> 0 ...)
            waveCount++;
            waveTimer = waveInterval; // reinicia el timer para la siguiente rotación

            if (waveCount >= 3) // después de la 3ra rotación, se activan los 3 a la vez
            {
                ActivateAll();
                allActivated = true;
                return;
            }

            ActivateSingle(currentWave); // sigue mostrando un solo tipo a la vez
        }
    }

    void ActivateSingle(int waveIndex)
    {
        isoscelesSpawner.enabled = (waveIndex == 0); // prende solo si le toca a este
        irregularSpawner.enabled = (waveIndex == 1); // prende solo si le toca a este
        womanSpawner.enabled = (waveIndex == 2); // prende solo si le toca a este
    }

    void ActivateAll()
    {
        isoscelesSpawner.enabled = true; // fase final: los 3 quedan activos
        irregularSpawner.enabled = true;
        womanSpawner.enabled = true;
    }
}