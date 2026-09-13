using NUnit.Framework;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    float currentTime;
    public float maxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            currentTime += Time.deltaTime;
            if (currentTime > maxTime)
            {
                gameObject.SetActive(false);
                currentTime = 0;
            }
        }
    }
}
