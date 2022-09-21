using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uper : MonoBehaviour
{
    [SerializeField]
    private float _increaseX, _increaseY;
    private bool _isRight;    
    
    private void FixedUpdate()
    {
        if (transform.position.x >= 2.0f)
        {           
            _isRight = false;
        }
        else if (transform.position.x <= -2.0f)
        {           
            _isRight = true;
        }
        if (_isRight)
        {            
            transform.position = new Vector2(transform.position.x + _increaseX, transform.position.y + _increaseY);
        }
        else
        {            
            transform.position = new Vector2(transform.position.x - _increaseX, transform.position.y + _increaseY);
        }

        
        //Debug.Log(transform.position.x);
    }
}
