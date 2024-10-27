using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BallController : MonoBehaviour
{
    public static BallController instance;

    private Rigidbody2D _rbBall;

    public bool isFlipped = false;
    public bool isUpsideDown = false; 
    public float gravityScale = 1f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _rbBall = GetComponent<Rigidbody2D>();
        _rbBall.gravityScale = gravityScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flip();
        }

    }

    private void Flip()
    {
        isUpsideDown = !isUpsideDown;

        Vector2 position = this.transform.position;
        float offset = isUpsideDown ? -1.5f : 1.5f;
        this.transform.position = new Vector2(position.x, position.y + offset);

        _rbBall.gravityScale = isUpsideDown ? -gravityScale : gravityScale;

    }
}
