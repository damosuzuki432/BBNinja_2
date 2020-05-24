using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMFadeout : MonoBehaviour
{
    public AudioSource BGM_1;
    public AudioSource BGM_2;
    float fadeVelocity = 0.01f;
    public bool fadeOut = false;
    public bool fadeOut2 = false;
    float oldVolume = 1.0f;
    float newVolume;
    int volume = 0;


    // Start is called before the first frame update
    
    IEnumerator Fadeout()
    {
        BGM_1.volume = oldVolume;
        newVolume = oldVolume - Time.deltaTime* 1.0f;
        BGM_1.volume = newVolume;
        yield return new WaitForSeconds(Time.deltaTime * 0.5f);
        oldVolume = newVolume;
    }

    IEnumerator Fadeout2()
    {
        BGM_2.volume = oldVolume;
        newVolume = oldVolume - Time.deltaTime * 1.0f;
        BGM_2.volume = newVolume;
        yield return new WaitForSeconds(Time.deltaTime * 0.5f);
        oldVolume = newVolume;
    }

    public void ResetParams()
    {
        oldVolume = 1.0f;
    }

    public void playDangerousBGM()
    {
        BGM_2.Play();
    }

    public void stopDangerousBGM()
    {
        BGM_2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut == true)
        {
            StartCoroutine(Fadeout());
        }
        if (fadeOut2 == true)
        {
            StartCoroutine(Fadeout2());
        }

    }
}
