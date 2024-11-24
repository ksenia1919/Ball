using System.Collections;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f; 
    public float heightOffset = 1f; 
    public float startAlpha = 1f; 
    public float endAlpha = 0f; 

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

            // Relocation
            transform.position = Vector3.Lerp(startPosition, startPosition + Vector3.up * heightOffset, progress);

            // Transparency
            spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(startAlpha, endAlpha, progress));

            yield return null;
        }
    }
}
