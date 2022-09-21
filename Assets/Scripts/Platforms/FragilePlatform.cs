using UnityEngine;

public class FragilePlatform : MonoBehaviour
{
    private Animator platformAnimator;
    private Rigidbody2D platformRigidbody2D;
    private AudioSource _audioSource;
    
    private void Start()
    {
        platformAnimator = GetComponent<Animator>();
        platformRigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                platformAnimator.SetBool("Break", true);
                platformRigidbody2D.isKinematic = false;
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.platformFragileCounter++;
                _audioSource.Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
