using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BallController : MonoBehaviour
{
    public GameObject Ball;
    private Rigidbody2D _rbBall;

    // ������ �� Collider  ������
    private EdgeCollider2D _spriteShapeController;

    public bool isFlipped = false;
    private bool isUpsideDown = false; // ��������� ���������� ������
    private float gravityScale = 1f;
    private Vector2 previousPosition;
    private Vector2 direction;

    void Start()
    {
        this._spriteShapeController = this.GetComponent<EdgeCollider2D>();

        _rbBall = Ball.GetComponent<Rigidbody2D>();
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
        // ����������� ���������
        isUpsideDown = !isUpsideDown;

        // ������ ������� ������ ������������ ������
        Vector2 position = Ball.transform.position;
        float offset = isUpsideDown ? -1.5f : 1.5f;
        Ball.transform.position = new Vector2(position.x, position.y + offset);

        // ������ ����������� ����������
        _rbBall.gravityScale = isUpsideDown ? -gravityScale : gravityScale;

        // ����� ����� ����������� ����� �� ��� Z ���� ��� ����������
        Ball.transform.Rotate(0, 0, 180);
    }
}
