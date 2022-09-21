using UnityEngine;

public class Star : MonoBehaviour
{
    private MediatorGame _mediator;
    private AudioSource _audioSource;

    [SerializeField] private ParticleSystem _particleSystem;
    private void Start()
    {
        _particleSystem.Stop();
        _mediator = GameObject.Find("MediatorGame").GetComponent<MediatorGame>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            _audioSource.Play();
            _particleSystem.Play();
            _mediator.stars++;

            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            
            Destroy(gameObject, 2f);

            Player.PlayerScript.itemStarCounter++;
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
