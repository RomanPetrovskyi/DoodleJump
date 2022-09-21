using UnityEngine;

public class PlatformFinish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            if (col.relativeVelocity.y <= 0)
            {
                GetComponent<AudioSource>().Play();
                if(PlayerPrefs.GetInt("Vibro") == 1) Handheld.Vibrate();
                Player.PlayerScript.isFinish = true;
                Player.PlayerScript.isCanShot = false; // Запрещаем игроку стрелять на участке финиша
            }
        }
        else if (col.gameObject.name == "NullZone") Destroy(gameObject);
    }
}
