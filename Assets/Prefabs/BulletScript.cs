using NUnit.Framework;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float currentTime;
    public float maxTime;

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

