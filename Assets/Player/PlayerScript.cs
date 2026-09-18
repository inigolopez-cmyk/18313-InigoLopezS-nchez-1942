using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputAction inputmovement;
    public InputAction shoot;
    public InputAction dash; 

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

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    bool isDashing = false;
    bool canDash = true;
    float dashTimer;
    float dashCooldownTimer;
    Vector2 dashDirection;

    private void OnEnable()
    {
        inputmovement.Enable();
        shoot.Enable();
        dash.Enable(); 
    }

    private void OnDisable()
    {
        inputmovement.Disable();
        shoot.Disable();
        dash.Disable(); 
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb2D.linearVelocity = dashDirection * dashSpeed;
        }
        else
        {
            Vector2 movement = inputmovement.ReadValue<Vector2>();
            rb2D.linearVelocity = movement * 5;
            rb2D.linearVelocity = Vector2.ClampMagnitude(rb2D.linearVelocity, 10);
        }
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

        // --- Dash ---
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }

        if (dash.triggered && canDash && !isDashing)
        {
            Vector2 moveInput = inputmovement.ReadValue<Vector2>();
            dashDirection = moveInput.normalized;

            if (dashDirection != Vector2.zero)
            {
                isDashing = true;
                canDash = false;
                dashTimer = dashDuration;
                dashCooldownTimer = dashCooldown;
            }
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
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
        lifes = Mathf.Min(lifes, 6);
        uiScript.AddLifes(lifes);
    }
}