using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BallController : MonoBehaviour
{
    public static BallController instance;

    private Rigidbody2D _rbBall;

    public bool isFlipped = false;
    public float gravityScale = 1f;

    public GameObject track;
    public float raycastOffset = 0.1f; // Добавлен offset для луча
    public float maxRaycastDistance = 70f; // Максимальное расстояние луча
    public LayerMask trackLayer;
    public EdgeCollider2D _edgeCollider;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _rbBall = GetComponent<Rigidbody2D>();
        _rbBall.gravityScale = gravityScale;
        _edgeCollider = track.GetComponent<EdgeCollider2D>();
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
        isFlipped = !isFlipped;
        _rbBall.gravityScale = isFlipped ? -gravityScale : gravityScale;

        float trackWidth = _edgeCollider.edgeRadius * 2f;

        Vector2 ballPosition = transform.position;
        Vector2 targetPosition = CalculateTargetPosition(ballPosition, trackWidth);

        if (targetPosition != Vector2.zero)
        {
            transform.position = targetPosition;
        }
        else
        {
            Debug.LogWarning("Не удалось определить целевую позицию");
        }
    }

    private Vector2 CalculateTargetPosition(Vector2 ballPosition, float trackWidth)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(ballPosition, Vector2.up, maxRaycastDistance, trackLayer);
        RaycastHit2D hitDown = Physics2D.Raycast(ballPosition, Vector2.down, maxRaycastDistance, trackLayer);

        Debug.DrawRay(ballPosition, Vector2.up * maxRaycastDistance, Color.red, 0.5f);
        Debug.DrawRay(ballPosition, Vector2.down * maxRaycastDistance, Color.blue, 0.5f);

        RaycastHit2D hit = hitUp.collider != null ? hitUp : hitDown; // Выбираем луч, который столкнулся с трассой

        if (hit.collider != null)
        {
            Vector2 trackNormal = hit.normal; // Направление смещения (перпендикулярно трассе)
            Vector2 targetPoint = hit.point; // Начальная точка расчета 

            Debug.DrawRay(hit.point, trackNormal, Color.green, 0.5f); // Отображаем нормаль

            targetPoint -= trackNormal * trackWidth; // Смещение всегда направлено в противоположную сторону от нормали

            Debug.DrawRay(targetPoint, Vector2.up, Color.yellow, 0.5f); // Отображаем целевую позицию
            return targetPoint;
        }
        else
        {
            return Vector2.zero;
        }
    }
}

