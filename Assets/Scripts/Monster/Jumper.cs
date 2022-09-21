using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody2D _monsterRigidbody;
    [SerializeField] private float _powerJump;
    private void Start()
    {
        _monsterRigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Platform" && _monsterRigidbody.velocity.y <= 0)
        {
            _monsterRigidbody.AddForce(Vector2.up * _powerJump, ForceMode2D.Impulse);
        }
    }
}
