using System.Collections;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f; // Длительность анимации
    public float heightOffset = 1f; // Высота подъема
    public float startAlpha = 1f; // Начальная прозрачность
    public float endAlpha = 0f; // Конечная прозрачность

    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void PlayAnimation()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float elapsedTime = 0f;
        
        GetComponent<AudioSource>().Play();

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / animationDuration;

            // Перемещение
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * heightOffset, progress);

            // Прозрачность
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(startAlpha, endAlpha, progress));

            yield return null;
        }
    }
}