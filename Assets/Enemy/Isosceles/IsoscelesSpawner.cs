using UnityEngine;
using System.Collections.Generic;

public class IsoscelesSpawner : MonoBehaviour
{
    public GameObject Isosceles;

    public List<GameObject> IsoscelesList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    public float spawnMarginY = 1f;       
    public int maxEnemiesThisWave = 5;     
    int enemiesSpawnedCount = 0;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Isosceles, transform.position, transform.rotation);
            temp.SetActive(false);
            IsoscelesList.Add(temp);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = getIsosceles();

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
    }

    GameObject getIsosceles()
    {
        foreach (GameObject temp in IsoscelesList)
        {
            if (temp.activeInHierarchy == false)
            {
                return temp;
            }
        }
        GameObject newIsosceles = Instantiate(Isosceles, transform.position, Quaternion.identity);
        newIsosceles.SetActive(false);
        IsoscelesList.Add(newIsosceles);
        return newIsosceles;
    }
}