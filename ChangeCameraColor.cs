using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraColor : MonoBehaviour
{

    [SerializeField] GameObject sakuraBackGround;

    //chreate changing pattern below and call from other css.

    private void Start()
    {
    }

    public void dark()
    {
        StartCoroutine(ChangeCameraColors_dark());
    }

    public void lightning()
    {
        StartCoroutine(ChangeCameraColors_lightning());
    }

    public void gray()
    {
        StartCoroutine(ChangeCameraColors_gray());
    }


    IEnumerator ChangeCameraColors_dark()
    {
        yield return StartCoroutine(ChangeColor(Color.black, 2f));
    }

    IEnumerator ChangeCameraColors_lightning()
    {
        yield return StartCoroutine(ChangeColor(Color.yellow, 0.01f));
        yield return StartCoroutine(ChangeColor(Color.black, 0.01f));
    }


    IEnumerator ChangeCameraColors_gray()
    {
        yield return StartCoroutine(ChangeColor(Color.gray, 2f));
    }


    //below is the engine of the change system
    IEnumerator ChangeColor(Color toColor, float duration)
    {
        Color fromColor = sakuraBackGround.GetComponent<SpriteRenderer>().color;
        //Color fromColor = Camera.main.backgroundColor;
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float marginR = toColor.r - fromColor.r;
        float marginG = toColor.g - fromColor.g;
        float marginB = toColor.b - fromColor.b;


        while (Time.time < endTime)
        {
            fromColor.r = fromColor.r + (Time.deltaTime / duration) * marginR;
            fromColor.g = fromColor.g + (Time.deltaTime / duration) * marginG;
            fromColor.b = fromColor.b + (Time.deltaTime / duration) * marginB;
            sakuraBackGround.GetComponent<SpriteRenderer>().color = fromColor;
            //Camera.main.backgroundColor = fromColor;
            yield return 0;
        }
        sakuraBackGround.GetComponent<SpriteRenderer>().color = toColor;
        //Camera.main.backgroundColor = toColor;
        yield break;
    }
}
