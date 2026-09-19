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

    public float dashSpeed = 20f; // velocidad a la que se mueve el jugador mientras dura el dash
    public float dashDuration = 0.2f; // cuánto dura el dash activo (en segundos)
    public float dashCooldown = 1f; // cuánto hay que esperar después de un dash antes de poder hacer otro

    bool isDashing = false; // true mientras el jugador está en medio de un dash
    bool canDash = true; // true cuando ya pasó el cooldown y se puede volver a dashear
    float dashTimer; // cuenta regresiva de cuánto le falta al dash actual para terminar
    float dashCooldownTimer; // cuenta regresiva del cooldown antes de permitir otro dash
    Vector2 dashDirection; // dirección congelada en la que se mueve el dash

    private void OnEnable()
    {
        inputmovement.Enable();
        shoot.Enable();
        dash.Enable(); // habilita el input de dash junto con los demás
    }

    private void OnDisable()
    {
        inputmovement.Disable();
        shoot.Disable();
        dash.Disable(); // deshabilita el input de dash junto con los demás
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb2D.linearVelocity = dashDirection * dashSpeed; // ignora el input normal y fuerza la velocidad del dash
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
            dashCooldownTimer -= Time.deltaTime; // va bajando el tiempo de cooldown
            if (dashCooldownTimer <= 0)
            {
                canDash = true; // ya se puede volver a dashear
            }
        }

        if (dash.triggered && canDash && !isDashing) // solo si se presionó el botón, no hay cooldown, y no está dasheando ya
        {
            Vector2 moveInput = inputmovement.ReadValue<Vector2>();
            dashDirection = moveInput.normalized; // dirección del dash = dirección actual de movimiento

            if (dashDirection != Vector2.zero) // no se permite dashear si está parado
            {
                isDashing = true;
                canDash = false;
                dashTimer = dashDuration; // arranca la duración del dash
                dashCooldownTimer = dashCooldown; // arranca el cooldown al mismo tiempo
            }
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime; // cuenta regresiva de lo que le falta al dash
            if (dashTimer <= 0)
            {
                isDashing = false; // termina el dash, vuelve al movimiento normal
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