using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCounter : MonoBehaviour
{
    public ObjectPoolManager poolManager; // Ссылка на менеджер пулов
    public string starPoolName = "Stars"; // Имя пула звезд

    private int counter;
    public Text _textCounter;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Star"))
        {
            PlayerPrefs.SetInt("Star",0);
        }
    }
    void Start()
    {
        counter = PlayerPrefs.GetInt("Star");
        _textCounter.text = counter.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Star"))
        {
            StarAnimation starAnimation = collision.gameObject.GetComponent<StarAnimation>();

            if (starAnimation != null)
            {
                starAnimation.PlayAnimation();
                StartCoroutine(ReturnStar(collision.gameObject, starAnimation.animationDuration));
            }

            counter++;
            _textCounter.text = counter.ToString();
            PlayerPrefs.SetInt("Star", counter);
        }
    }

    private IEnumerator ReturnStar(GameObject star, float delay)
    {
        yield return new WaitForSeconds(delay);
        poolManager.ReturnObject(starPoolName, star);
    }
}

