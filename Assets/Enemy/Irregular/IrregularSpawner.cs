using UnityEngine;
using System.Collections.Generic;

public class IrregularSpawner : MonoBehaviour
{
    public GameObject Irregular;

    public List<GameObject> IrregularList = new List<GameObject>();

    float currentTime;
    public float maxTime;

    public float spawnMarginY = 1f;

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

            float randomX = Random.Range(-7f, 7f);
            float spawnY = 5f + spawnMarginY;
            e.transform.position = new Vector3(randomX, spawnY, 0);

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
