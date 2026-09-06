using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputAction inputmovement;
    public InputAction shoot;

    public Rigidbody2D rb2D;
    public GameObject bullet;

    public int lifes;
    bool isDamage = false;

    public float currentTime;
    public float maxTime;

    //public Camera cam;
    //private float playerHalfWidth; 

    UpdateUI uiScript;

    public List<GameObject> bulletPool = new List<GameObject>();

    private void OnEnable()
    {
        inputmovement.Enable();
        shoot.Enable();
    }

    private void OnDisable()
    {
        inputmovement.Disable();
        shoot.Disable();
    }

    private void FixedUpdate()
    {
        //private float newX;

        //Movimiento
        Vector2 movement = inputmovement.ReadValue<Vector2>();
        rb2D.linearVelocity = movement * 5;
        rb2D.linearVelocity = Vector2.ClampMagnitude(rb2D.linearVelocity, 10);


        //Limites
        //Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        //newX = Mathf.Clamp(newX, leftEdge.x + playerHalfWidth, rightEdge.x - playerHalfWidth);

        //rb2D.MovePosition(new Vector2(newX, transform.position.y));
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (cam == null)
        //{
        //    cam = Camera.main;
        //}

        //playerHalfWidth = transform.localScale.x / 2f;


        lifes = 3;

        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
        uiScript.AddLifes(lifes);

        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes <= 0)
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            uiScript.OpenGameOver();
        }

        if (isDamage)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            currentTime += Time.deltaTime;
            if (currentTime > maxTime)
            {
                currentTime = 0;
                isDamage = false;
                GetComponent<PolygonCollider2D>().enabled = true;
            }
        }

        if (shoot.triggered)
        {
            GameObject temp = GetBullet();
            temp.SetActive(true);
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
            //GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rbtemp = temp.GetComponent<Rigidbody2D>();
            rbtemp.AddForce(transform.up * 5, ForceMode2D.Impulse);
        }
    }

    GameObject GetBullet()
    {
        foreach (GameObject b in bulletPool)
        {
            if (b.activeInHierarchy == false)
            {
                return b;
            }
        }
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.SetActive(false);
        bulletPool.Add(temp);
        return temp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            lifes--;
            uiScript.AddLifes(lifes);
            isDamage = true;
        }
    }
}
