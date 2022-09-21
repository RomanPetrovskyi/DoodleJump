using System;
using UnityEngine;
using DG.Tweening;

public class PlatformDisposable : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                var sq = DOTween.Sequence();
                sq.Append(transform.DOScale(1.2f, 0.2f));
                sq.Append(transform.DOScale(0f, 0.2f).OnComplete(() => Destroy(gameObject)));

                Player.PlayerScript.platformDisposableCounter++;
                _audioSource.Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
