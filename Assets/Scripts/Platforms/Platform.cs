using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                // Эффект отталкивания от платформы
                var sq = DOTween.Sequence();
                sq.Append(transform.DOScale(0.8f, 0.1f));
                sq.Append(transform.DOScale(1f, 0.1f));
                
                // После прыжка на платформе перестают действовать "итемы"
                Player.PlayerScript.currentItem = Player.CurrentItem.None;
                Player.PlayerScript.playerRigidbody.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
                
                // Проигрываем звук прыжка
                GetComponent<AudioSource>().Play();
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
