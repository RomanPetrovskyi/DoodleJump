using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlatformDrag : MonoBehaviour,  IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Player.PlayerScript.isDragPlatform = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Player.PlayerScript.isDragPlatform = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Player.PlayerScript.isDragPlatform = true;
        this.transform.position = GetMousePosition();
    }

    Vector3 GetMousePosition()
    {
        if (Camera.main != null)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
        return Vector3.zero;
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                _audioSource.Play();
                var sq = DOTween.Sequence();
                sq.Append(transform.DOScale(0.8f, 0.2f));
                sq.Append(transform.DOScale(1f, 0.2f).OnComplete(() => Destroy(gameObject)));
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
