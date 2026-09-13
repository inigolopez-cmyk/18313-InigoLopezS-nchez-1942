using UnityEngine;
using System.Collections.Generic;


public class IsoscelesSpawner : MonoBehaviour
{
    public GameObject Isosceles;

    public List<GameObject> IsoscelesList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Isosceles, transform.position, transform.rotation);
            temp.SetActive(false);
            IsoscelesList.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = getIsosceles();
            e.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            e.SetActive(true);
            currentTime = 0;
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
