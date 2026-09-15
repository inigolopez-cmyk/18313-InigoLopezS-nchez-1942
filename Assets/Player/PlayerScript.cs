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

    [SerializeField] 
    private AudioSource shootAudio;

    public int lifes;
    bool isDamage = false;

    public float currentTime;
    public float maxTime;

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
        //Movimiento
        Vector2 movement = inputmovement.ReadValue<Vector2>();
        rb2D.linearVelocity = movement * 5;
        rb2D.linearVelocity = Vector2.ClampMagnitude(rb2D.linearVelocity, 10);

    }

    void Start()
    {
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

    void Update()
    {
        if (lifes <= 0)
        {
            gameObject.SetActive(false);
            uiScript.OpenGameOver();      
            GameManager.Instance.PlayerDied(); 
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
            shootAudio.Play();
            GameObject temp = GetBullet();
            temp.SetActive(true);
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
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

    public void AddHealth(int value)
    {
        lifes += value;
        lifes = Mathf.Min(lifes, 6); // no deja que vidas pase de 6
        uiScript.AddLifes(lifes);

    }
}
