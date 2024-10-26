using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    private float gap = 0.8f; // Минимальный промежуток между звездами
    private float offsetX = 0.641929f; // Смещение по X
    private float verticalOffset = 1f; // Вертикальное расстояние от линии до звезд
    public int pointsPerSegment = 5; // Количество точек для определения сегмента
    private float starSpawnChance = 0.7f; // Вероятность спавна звезд на сегменте
    private string previousSide = null; // Отслеживаем предыдущую сторону
    public int startSegmentIndex = 1; // Индекс сегмента, с которого начинается спавн звезд

    private EdgeCollider2D _edgeCollider;

    void Start()
    {
        _edgeCollider = GetComponent<EdgeCollider2D>();
        SpawnStarsAlongEdgeCollider();
    }

    private void SpawnStarsAlongEdgeCollider()
    {
        int totalPoints = _edgeCollider.pointCount;

        int numSegments = Mathf.CeilToInt((float)totalPoints / pointsPerSegment);

        // Проходим по каждому сегменту
        for (int segmentIndex = startSegmentIndex; segmentIndex < numSegments; segmentIndex++)
        {
            int startPointIndex = segmentIndex * pointsPerSegment;
            if (startPointIndex >= totalPoints) break;

            int endPointIndex = Mathf.Min(startPointIndex + pointsPerSegment, totalPoints) - 1;

            // Получаем точки для Bézier
            List<Vector2> segmentPoints = new List<Vector2>();
            for (int i = startPointIndex; i <= endPointIndex; i++)
            {
                segmentPoints.Add(_edgeCollider.points[i]);
            }

            // Проверяем, нужно ли спавнить звезды на этом сегменте
            if (Random.value < starSpawnChance)
            {
                float segmentLength = Vector2.Distance(segmentPoints[0], segmentPoints[segmentPoints.Count - 1]);

                int numStars = Mathf.FloorToInt(segmentLength / gap);

                // Спавним звезды с равным интервалом
                for (int j = 0; j < numStars; j++)
                {
                    // Вычисляем прогресс вдоль сегмента
                    float progress = (float)j / (numStars - 1);

                    // Вычисляем позицию звезды на Bézier
                    Vector2 starPosition = BezierCurve.GetPoint(segmentPoints, progress);

                    // Вычисляем позицию звезды с отступом
                    Vector2 starPositionTop = starPosition + GetPerpendicularDirection(segmentPoints[0], segmentPoints[segmentPoints.Count - 1]) * verticalOffset;
                    Vector2 starPositionBottom = starPosition - GetPerpendicularDirection(segmentPoints[0], segmentPoints[segmentPoints.Count - 1]) * verticalOffset;

                    // Случайно выбираем сторону для спавна (только если это не первый сегмент)
                    string side = previousSide == null ? Random.value > 0.5f ? "top" : "bottom" : previousSide == "top" ? "bottom" : null;

                    // Спавним звезду на выбранной стороне, если side не null
                    if (side != null)
                    {
                        if (side == "top")
                        {
                            Instantiate(starPrefab, new Vector2(starPositionTop.x - offsetX, starPositionTop.y), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(starPrefab, new Vector2(starPositionBottom.x - offsetX, starPositionBottom.y), Quaternion.identity);
                        }
                    }
                    previousSide = side;
                }
            }
        }
    }
    // Получение перпендикулярного вектора
    private Vector2 GetPerpendicularDirection(Vector2 start, Vector2 end)
    {
        return new Vector2(end.y - start.y, -(end.x - start.x)).normalized;
    }
}

// Класс для работы с Bézier-кривыми
public static class BezierCurve
{
    public static Vector2 GetPoint(List<Vector2> points, float t)
    {
        int n = points.Count - 1;
        Vector2 result = Vector2.zero;
        for (int i = 0; i <= n; i++)
        {
            result += points[i] * BernsteinPolynomial(n, i, t);
        }
        return result;
    }
 
    private static float BernsteinPolynomial(int n, int i, float t)
    {
        return Factorial(n) / (Factorial(i) * Factorial(n - i)) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }

    private static int Factorial(int n)
    {
        if (n == 0) return 1;
        return n * Factorial(n - 1);
    }
}