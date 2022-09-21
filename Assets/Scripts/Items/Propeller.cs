using DG.Tweening;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    private Animator propellerAnimator;
    private Rigidbody2D propellerRigitBody;
    private GameObject _player;
    private bool isFly;

    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.Find("Player");
        propellerAnimator = GetComponent<Animator>();
        propellerRigitBody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                _audioSource.Play();
                _player.GetComponent<Rigidbody2D>().isKinematic = true;
                //_player.GetComponent<CapsuleCollider2D>().enabled = false;
                propellerAnimator.SetBool("Propeller", true);
                transform.GetComponent<BoxCollider2D>().enabled = false;
                
                // Смещаем кепку ближе к камере, чтобы она покрывала голову игрока (позиция Z игрока = -1f)
                transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
                
                float currentY = transform.position.y;
                
                Sequence sq = DOTween.Sequence();
                sq.Append(transform.DOMoveY(currentY + 30, 4f));
                sq.Join(_player.transform.DOMoveY(currentY + 29.5f, 4f).OnComplete(() => FlyEnd()));
                
                isFly = true;

                Player.PlayerScript.currentItem = Player.CurrentItem.Cap;
                Player.PlayerScript.itemCapPropellerCounter++;
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }

    private void FlyEnd()
    {
        _audioSource.Stop();
        isFly = false;
        _player.GetComponent<CapsuleCollider2D>().enabled = true;
        propellerAnimator.SetBool("Propeller", false);
        propellerRigitBody.bodyType = RigidbodyType2D.Dynamic;
        _player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        propellerRigitBody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        propellerRigitBody.AddForce(Vector2.right * 2f, ForceMode2D.Impulse);
        Player.PlayerScript.currentItem = Player.CurrentItem.None;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (isFly)
        {
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.5f, transform.position.z);
        }
    }
}
