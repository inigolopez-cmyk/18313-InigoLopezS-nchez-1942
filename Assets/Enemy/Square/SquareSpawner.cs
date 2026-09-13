using UnityEngine;
using System.Collections.Generic;

public class SquareSpawner : MonoBehaviour
{
    public GameObject Square;

    public List<GameObject> SquareList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Square, transform.position, Quaternion.identity);
            temp.SetActive(false);
            SquareList.Add(temp);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = GetSquare();
            e.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            e.SetActive(true);
            currentTime = 0;
        }
    }

    GameObject GetSquare()
    {
        foreach (GameObject temp in SquareList)
        {
            if (temp.activeInHierarchy == false)
            {
                return temp;
            }
        }
        GameObject newSquare = Instantiate(Square, transform.position, Quaternion.identity);
        newSquare.SetActive(false);
        SquareList.Add(newSquare);
        return newSquare;
    }
}
