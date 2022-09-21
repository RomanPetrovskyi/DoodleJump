using UnityEngine;
using DG.Tweening;

public class Trampoline : MonoBehaviour
{
    private Animator trampolineAnimator;
    private GameObject _player;
    private AudioSource _audioSource;
    
    private void Start()
    {
        trampolineAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player");
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                trampolineAnimator.SetBool("Trampoline", true);
                Player.PlayerScript.currentItem = Player.CurrentItem.Trampoline;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 17f, ForceMode2D.Impulse);
                _player.transform.DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360);
                Player.PlayerScript.itemTrampolineCounter++;
                _audioSource.Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
