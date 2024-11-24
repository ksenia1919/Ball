 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationDistance : MonoBehaviour
{
    private EdgeCollider2D _edgeCollider;

    void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        if (_edgeCollider == null)
        {
            Debug.LogError("EdgeCollider2D not found on this GameObject.");
            enabled = false;
        }
    }

    public Vector2 GetFlipPosition(Vector2 ballPosition)
    {
        // ������� ��������� ����� �� ������ � ������
        float minDistance = float.MaxValue;
        Vector2 closestPoint = Vector2.zero;

        for (int i = 0; i < _edgeCollider.points.Length; i++)
        {
            Vector2 point = _edgeCollider.points[i];
            float distance = Vector2.Distance(ballPosition, point);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        // ��������� ������� � ������ � ����� ���������� ��������
        Vector2 normal = Vector2.zero;

        // ...  ��������� ��� ��� ���������� �������

        // ���������� ���������� ����� ����������. 
        // ���������� ������� � ��������� �����������, ����� ����� �� ���������
        //Vector2 flipPosition = closestPoint + normal(ball.flipOffset(ball.isUpsideDown ? -1 : 1));
        // return flipPosition;
        return normal;
    }
}
