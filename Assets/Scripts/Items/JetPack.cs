using DG.Tweening;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    private Animator _jetPackAnimator;
    private Rigidbody2D _jetPackRigidBody;
    private GameObject _player;
    private SpriteRenderer _playerSpriteRenderer;
    private bool _isFly;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _jetPackAnimator = GetComponent<Animator>();
        _jetPackRigidBody = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                _audioSource.Play();
                //_player.GetComponent<CapsuleCollider2D>().enabled = false;
                _player.GetComponent<Rigidbody2D>().isKinematic = true;
                transform.GetComponent<BoxCollider2D>().enabled = false;
                
                _jetPackAnimator.SetBool("JetPack", true);
                
                float currentY = transform.position.y;
                
                Sequence sq = DOTween.Sequence();
                sq.Append(transform.DOMoveY(currentY + 40, 4f));
                sq.Join(_player.transform.DOMoveY(currentY + 40.5f, 4f).OnComplete(() => FlyEnd()));
                
                _isFly = true;

                Player.PlayerScript.currentItem = Player.CurrentItem.JetPack;
                Player.PlayerScript.itemJetPackCounter++;
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }

    public void FlyEnd()
    {
        _audioSource.Stop();
        _isFly = false;
        _player.GetComponent<CapsuleCollider2D>().enabled = true;
        _jetPackAnimator.SetBool("JetPack", false);
        _jetPackRigidBody.bodyType = RigidbodyType2D.Dynamic;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        _jetPackRigidBody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        _jetPackRigidBody.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
        Player.PlayerScript.currentItem = Player.CurrentItem.None;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (_isFly)
        {
            if (!_playerSpriteRenderer.flipX)
                transform.position = new Vector3(_player.transform.position.x + 0.4f, _player.transform.position.y - 0.5f, transform.position.z);
            else
                transform.position = new Vector3(_player.transform.position.x - 0.4f, _player.transform.position.y - 0.5f, transform.position.z);
        }
    }
}
