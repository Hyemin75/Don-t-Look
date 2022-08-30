using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class FadeEffect : MonoBehaviour
{

    public float animTime = 2f;         
    private float start = 1f;          
    private float end = 0f;           
    private float time = 0f;           

    private bool stopIn = true; 
    private bool stopOut = false;

    void Awake()
    {
        
    }

    public void PlayerFadeIn(Image fadeImage)
    {
        if (stopIn == false && time <= 2)
        {
            time += Time.deltaTime / animTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(end, start, time);

            fadeImage.color = color;
        }

        if (time >= 2 && stopIn == false)
        {
            stopIn = true;
            time = 0;
        }

    }

    public void PlayerFadeOut(Image fadeImage)
    {
        if (stopOut == false && time <= 2)
        {
            time += Time.deltaTime / animTime;

            Color color = fadeImage.color;

            color.a = Mathf.Lerp(end, start, time);

            fadeImage.color = color;
        }

        if (time >= 2 && stopOut == false)
        {
            stopIn = false;
            stopOut = true;
            time = 0;
        }
    }
}
