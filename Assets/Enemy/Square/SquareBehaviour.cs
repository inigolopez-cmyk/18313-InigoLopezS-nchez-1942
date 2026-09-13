using UnityEngine;
using System.Collections.Generic;

public class SquareBehaviour : MonoBehaviour
{
    enum State { Rushing, Aiming, Shooting }
    State currentState = State.Rushing;

    GameObject player;
    UpdateUI uiScript;

    public float rushSpeed = 1f;
    public float centerArrivalThreshold = 0.3f;
    Vector3 screenCenterWorld;

    public float aimDuration = 0.6f;
    public float fireCooldown = 1.5f;
    Vector2 shootDirection;
    float aimTimer;

    public GameObject enemyBullet;
    public float bulletForce = 6f;
    public List<GameObject> bulletPool = new List<GameObject>();

    [SerializeField] private GameObject pickupPrefab;
    [Range(0f, 1f)][SerializeField] private float dropChance = 0.1f;
    public int scoreValue = 30;

    void Start()
    {
        player = GameObject.Find("Player");
        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();

        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            temp.SetActive(false);
            bulletPool.Add(temp);
        }
    }

    void OnEnable()
    {
        currentState = State.Rushing;

        if (Camera.main != null)
        {
            screenCenterWorld = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Mathf.Abs(Camera.main.transform.position.z)));
            screenCenterWorld.z = transform.position.z;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Rushing:
                DoRushing();
                break;
            case State.Aiming:
                DoAiming();
                break;
            case State.Shooting:
                DoShooting();
                break;
        }
    }

    void DoRushing()
    {
        transform.position = Vector3.MoveTowards(transform.position, screenCenterWorld, rushSpeed * Time.deltaTime);
        FaceDirection(screenCenterWorld - transform.position);

        if (Vector3.Distance(transform.position, screenCenterWorld) <= centerArrivalThreshold)
        {
            currentState = State.Aiming;
            aimTimer = aimDuration;
        }
    }

    void DoAiming()
    {
        aimTimer -= Time.deltaTime;
        FaceDirection(player.transform.position - transform.position);

        if (aimTimer <= 0)
        {
            shootDirection = (player.transform.position - transform.position).normalized;
            currentState = State.Shooting;
        }
    }

    void DoShooting()
    {
        Shoot();
        currentState = State.Aiming;
        aimTimer = fireCooldown;
    }

    void Shoot()
    {
        GameObject b = GetEnemyBullet();
        b.transform.position = transform.position;
        b.transform.rotation = Quaternion.identity;
        b.SetActive(true);

        Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(shootDirection * bulletForce, ForceMode2D.Impulse);
    }

    GameObject GetEnemyBullet()
    {
        foreach (GameObject b in bulletPool)
        {
            if (b.activeInHierarchy == false)
            {
                return b;
            }
        }
        GameObject newBullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }

    void FaceDirection(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            uiScript.AddScore(scoreValue);
            collision.gameObject.SetActive(false);

            if (Random.value <= dropChance)
            {
                Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            }

            gameObject.SetActive(false);
        }
    }
}
