using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [Range(0f, 1f)][SerializeField] private float dropChance = 0.1f;

    GameObject player;
    UpdateUI uiScript;

    void Start()
    {
        player = GameObject.Find("Player");
        uiScript = GameObject.Find("Canvas").GetComponent<UpdateUI>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime);
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            uiScript.AddScore(2);
            collision.gameObject.SetActive(false);

            if (Random.value <= dropChance)
            {
                Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            }

            gameObject.SetActive(false);
        }
    }
}
