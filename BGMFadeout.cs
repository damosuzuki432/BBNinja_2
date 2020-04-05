using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMFadeout : MonoBehaviour
{
    AudioSource audioSource;
    float fadeVelocity = 0.01f;
    public bool fadeOut = false;
    float oldVolume = 1.0f;
    float newVolume;
    int volume = 0;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
     }

    IEnumerator Fadeout()
    {
        audioSource.volume = oldVolume;
        newVolume = oldVolume - Time.deltaTime* 1.0f;
        audioSource.volume = newVolume;
        yield return new WaitForSeconds(Time.deltaTime * 0.5f);
        oldVolume = newVolume;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (fadeOut == true)
        {
            StartCoroutine(Fadeout());
        }

    }
}
