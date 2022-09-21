using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformExploding : MonoBehaviour
{
    private float timeToExploding;
    private Animator platformAnimator;
    private SpriteRenderer platformSpriteRenderer;
    private AudioSource _audioSource;
    
    private void Start()
    {
        platformAnimator = GetComponent<Animator>();
        platformSpriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        
        timeToExploding = Random.Range(1f, 4f);
        Sequence sq = DOTween.Sequence();
        sq.Append(transform.DOScale(1f, timeToExploding));
        sq.OnComplete(() => RunExploding());
    }

    private void RunExploding()
    {
        _audioSource.Play();
        platformAnimator.SetBool("Exploding", true);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                _audioSource.Play();
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                platformAnimator.SetBool("Exploding", true);
                Player.PlayerScript.platformExplodingCounter++;
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }

    public void KillPlatform()
    {
        platformSpriteRenderer.DOColor(new Color(1, 1, 1, 1f), 0.1f).OnComplete(() => Destroy(gameObject));
    }
}
