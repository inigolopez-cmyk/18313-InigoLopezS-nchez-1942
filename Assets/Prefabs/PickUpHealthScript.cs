using UnityEngine;

public class PickUpHealthScript : MonoBehaviour
{
    [SerializeField] private int amountHealth = 1;

    [SerializeField] private AudioSource pickUpAudio;


    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 45, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerScript>().AddHealth(amountHealth);
            pickUpAudio.Play();

            GetComponent<SpriteRenderer>().enabled = false; // desaparece visualmente al toque
            GetComponent<Collider2D>().enabled = false; // evita que se pueda volver a tocar

            Destroy(this.gameObject, pickUpAudio.clip.length); // se destruye recién cuando termina el sonido
        }
    }
}
