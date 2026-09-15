using UnityEngine;
using System.Collections.Generic;

public class WomanSpawner : MonoBehaviour
{
    public GameObject Woman;

    public List<GameObject> WomanList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    public float spawnMarginY = 1f;
    public int maxEnemiesThisWave = 5;
    int enemiesSpawnedCount = 0;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Woman, transform.position, Quaternion.identity);
            temp.SetActive(false);
            WomanList.Add(temp);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = GetWoman();

            float randomX = Random.Range(-7f, 7f);
            float spawnY = 5f + spawnMarginY;
            e.transform.position = new Vector3(randomX, spawnY, 0);

            e.SetActive(true);
            currentTime = 0;

            enemiesSpawnedCount++;
            if (enemiesSpawnedCount >= maxEnemiesThisWave)
            {
                enabled = false;
            }
        }

        GameObject GetWoman()
        {
            foreach (GameObject temp in WomanList)
            {
                if (temp.activeInHierarchy == false)
                {
                    return temp;
                }
            }
            GameObject newWoman = Instantiate(Woman, transform.position, Quaternion.identity);
            newWoman.SetActive(false);
            WomanList.Add(newWoman);
            return newWoman;
        }
    }
}
