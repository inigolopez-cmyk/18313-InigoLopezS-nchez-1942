using UnityEngine;
using System.Collections.Generic;

public class IrregularSpawner : MonoBehaviour
{
    public GameObject Irregular;

    public List<GameObject> IrregularList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(Irregular, transform.position, Quaternion.identity);
            temp.SetActive(false);
            IrregularList.Add(temp);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            GameObject e = GetIrregular();
            e.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            e.SetActive(true);
            currentTime = 0;
        }
    }

    GameObject GetIrregular()
    {
        foreach (GameObject temp in IrregularList)
        {
            if (temp.activeInHierarchy == false)
            {
                return temp;
            }
        }
        GameObject newIrregular = Instantiate(Irregular, transform.position, Quaternion.identity);
        newIrregular.SetActive(false);
        IrregularList.Add(newIrregular);
        return newIrregular;
    }
}
