using System;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PlatformMobile : MonoBehaviour
{
    private int platformType; // 0-move horizontal, 1-move vertical
    private int direction;
    private Vector3 startPositionY;
    private Vector3 endPositionY;
    
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        platformType = Random.Range(0, 2);
        direction = Random.Range(0, 2);
        platformType = 0;///////////////////////////////
        startPositionY = transform.position;
        endPositionY = new Vector3(transform.position.x, transform.position.y + Random.Range(1f, 4f),
            transform.position.z);
    }

    private void Update()
    {
        if (platformType == 0)
        {
            if (direction == 1) transform.Translate(Vector3.right * Time.deltaTime);
            else transform.Translate(Vector3.left * Time.deltaTime);

            if (transform.position.x > 2f) direction = 0;
            if (transform.position.x < -2f) direction = 1;
        }
        else if (platformType == 1)
        {
            if(direction == 1) transform.Translate(Vector3.up * Time.deltaTime);
            else transform.Translate(Vector3.down * Time.deltaTime);
            
            if (transform.position.y >= endPositionY.y) direction = 0;
            if (transform.position.y <= startPositionY.y) direction = 1;
        }
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
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                _audioSource.Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
