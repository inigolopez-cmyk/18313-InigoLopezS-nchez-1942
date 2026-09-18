using UnityEngine;
using UnityEngine.U2D;

public class IrregularBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [Range(0f, 1f)][SerializeField] private float dropChance = 0.1f;
    public int scoreValue = 150;

    GameObject player;
    UpdateUI uiScript;

    public float downSpeed = 1.5f;
    public float amplitude = 1f;   
    public float frequency = 2f; 

    float startX;
    float timeAlive;

    SpriteShapeRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteShapeRenderer>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
    }


    void OnEnable()
    {
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        startX = transform.position.x;
        timeAlive = 0f;
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        float newY = transform.position.y - downSpeed * Time.deltaTime;
        float newX = startX + Mathf.Cos(timeAlive * frequency) * amplitude;

        transform.position = new Vector3(newX, newY, transform.position.z);
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
