using UnityEngine;

public class Shot : MonoBehaviour
{
    private float speed = 15f;
    private float destroyTime = 2f;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
