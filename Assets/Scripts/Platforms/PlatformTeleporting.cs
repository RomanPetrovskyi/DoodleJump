using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformTeleporting : MonoBehaviour
{
    private Sequence blinkSq;
    private SpriteRenderer platformSpriteRenderer;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        platformSpriteRenderer = GetComponent<SpriteRenderer>();
        var blinkSq = DOTween.Sequence();
        blinkSq.Append(platformSpriteRenderer.DOColor(new Color(1, 1, 1, 0.5f), 0.1f));
        blinkSq.Append(platformSpriteRenderer.DOColor(new Color(1, 1, 1, 1f), 0.1f));
        blinkSq.SetLoops(-1);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                _audioSource.Play();
                var sq = DOTween.Sequence();
                sq.Append(transform.DOScale(0.8f, 0.1f));
                sq.Append(transform.DOScale(1f, 0.1f));
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);

                transform.position = new Vector3(Random.Range(-1.7f, 1.7f), transform.position.y + Random.Range(-0.5f, -1f), transform.position.z);
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
