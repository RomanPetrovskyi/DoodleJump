using System;
using UnityEngine;
using DG.Tweening;
using Sequence = Unity.VisualScripting.Sequence;

public class Monster : MonoBehaviour
{
    private bool isLife = true;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isLife)
        {
            if (col.gameObject.name == "NullZone") Destroy(gameObject);
            if (col.gameObject.name == "Player")
            {
                // Если игрок летит сверху вниз на монстра...
                if (col.relativeVelocity.y <= 0)
                {
                    _audioSource.Play();
                    Player.PlayerScript.monsterAllCounter++;
                    Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                    isLife = false;
                    var sq = DOTween.Sequence();
                    sq.Append(transform.DOScale(1.3f, 0.1f));
                    sq.Append(transform.DOScale(0f, 0.5f).OnComplete(() => Destroy(gameObject)));   
                }
                // Если игрок летит снизу вверх и при этом он не использует никакой итем...
                else
                {
                    if (Player.PlayerScript.currentItem == Player.CurrentItem.None)
                    {
                        Player.PlayerScript._audioSource.Play();
                        if(PlayerPrefs.GetInt("Vibro") == 1) Handheld.Vibrate();
                        _audioSource.Play();
                        Player.PlayerScript.gameOverCameraPosition = Camera.main.transform.position;
                        Player.PlayerScript.gameObject.SetActive(false);
                        Player.PlayerScript.isGameOver = false;
                        Player.PlayerScript.isShowGameOverMenu = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "LikProjectile(Clone)" && isLife)
        {
            Player.PlayerScript.monsterAllCounter++;
            Destroy(col.gameObject);
            _audioSource.Play();
            isLife = false;
            var sq = DOTween.Sequence();
            sq.Append(transform.DOScale(1.3f, 0.1f));
            sq.Append(transform.DOScale(0f, 0.5f).OnComplete(() => Destroy(gameObject)));
        }
    }
}
