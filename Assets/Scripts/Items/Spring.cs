using DG.Tweening;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator springAnimator;
    private AudioSource _audioSource;

    private void Start()
    {
        springAnimator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                var sq = DOTween.Sequence();
                sq.Append(transform.DOScale(0.8f, 0.1f));
                sq.Append(transform.DOScale(1f, 0.1f));
                springAnimator.SetBool("Spring", true);
                Player.PlayerScript.currentItem = Player.CurrentItem.Spring;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                Player.PlayerScript.itemSpringCounter++;
                _audioSource.Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
