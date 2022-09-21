using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformCrazy : MonoBehaviour
{
    private Sequence shakePlatformSq;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        shakePlatformSq = DOTween.Sequence();
        shakePlatformSq.Append(transform.DOMoveX(transform.position.x + 0.1f, 0.1f));
        shakePlatformSq.Append(transform.DOMoveX(transform.position.x - 0.1f, 0.1f));
        shakePlatformSq.SetLoops(-1);
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
                
                shakePlatformSq.Kill();
                transform.position = new Vector3(Random.Range(-1.7f, 1.7f), transform.position.y, transform.position.z);
                
                shakePlatformSq = DOTween.Sequence();
                shakePlatformSq.Append(transform.DOMoveX(transform.position.x + 0.1f, 0.1f));
                shakePlatformSq.Append(transform.DOMoveX(transform.position.x - 0.1f, 0.1f));
                shakePlatformSq.SetLoops(-1);
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
