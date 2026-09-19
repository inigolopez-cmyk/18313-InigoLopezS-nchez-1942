using UnityEngine;

public class WomanBehaviour : MonoBehaviour
{
    enum State { Rushing, Aiming, Charging }
    State currentState = State.Rushing;

    GameObject player;
    UpdateUI uiScript;

    [SerializeField]
    private AudioSource screamAudio;

    public float rushSpeed = 4f;
    public float centerArrivalThreshold = 0.3f;
    Vector3 screenCenterWorld;

    public float aimDuration = 0.6f;
    Vector2 chargeDirection;
    float aimTimer;

    public float chargeSpeed = 12f;

    [SerializeField]
    private GameObject pickupPrefab;
    [Range(0f, 1f)]
    [SerializeField]
    private float dropChance = 0.1f;
    public int scoreValue = 50;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
    }

    void OnEnable()
    {
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        currentState = State.Rushing;

        if (Camera.main != null)
        {
            screenCenterWorld = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Mathf.Abs(Camera.main.transform.position.z)));
            screenCenterWorld.z = transform.position.z; // ajusta el Z al de la mujer para que quede en el mismo plano 2D
        }

        if (screamAudio != null)
        {
            screamAudio.Play();
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
            case State.Charging:
                DoCharging();
                break;
        }
    }

    void DoRushing()
    {
        transform.position = Vector3.MoveTowards(transform.position, screenCenterWorld, rushSpeed * Time.deltaTime); // avanza desde su posición actual hacia el centro de la pantalla, un poco en cada frame
        FaceDirection(screenCenterWorld - transform.position); // gira el sprite para que "mire" hacia el centro mientras avanza

        if (Vector3.Distance(transform.position, screenCenterWorld) <= centerArrivalThreshold)
        {
            currentState = State.Aiming; // ya está cerca del centro, pasa a la fase de apuntar
            aimTimer = aimDuration;
        }
    }

    void DoAiming()
    {
        aimTimer -= Time.deltaTime;

        if (aimTimer <= 0)
        {
            chargeDirection = (player.transform.position - transform.position).normalized; // congela la dirección hacia donde está el jugador justo en este instante
            FaceDirection(chargeDirection);
            currentState = State.Charging;
        }
    }

    void DoCharging()
    {
        transform.position += (Vector3)chargeDirection * chargeSpeed * Time.deltaTime; // avanza en línea recta cada frame en la dirección que quedó congelada en DoAiming
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
            screamAudio.Stop();
        }
    }
}
