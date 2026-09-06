using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject Enemy;

    public List<GameObject> EnemyList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Enemy, transform.position, transform.rotation);
            temp.SetActive(false);
            EnemyList.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = getEnemy();
            e.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            e.SetActive(true);
            currentTime = 0;
        }
    }

    GameObject getEnemy()
    {
        foreach (GameObject temp in EnemyList)
        {
            if (temp.activeInHierarchy == false)
            {
                return temp;
            }
        }
        GameObject newEnemy = Instantiate(Enemy, transform.position, Quaternion.identity);
        newEnemy.SetActive(false);
        EnemyList.Add(newEnemy);
        return newEnemy;
    }
}

