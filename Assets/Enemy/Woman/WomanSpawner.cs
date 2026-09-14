using UnityEngine;
using System.Collections.Generic;

public class WomanSpawner : MonoBehaviour
{
    public GameObject Woman;

    public List<GameObject> WomanList = new List<GameObject>();

    float currentTime;
    public float maxTime;

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
            e.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            e.SetActive(true);
            currentTime = 0;
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
