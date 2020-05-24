using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager3 : MonoBehaviour
{
    [SerializeField] GameObject princess;
    [SerializeField] GameObject shogun;
    [SerializeField] GameObject kemuri_princess;
    [SerializeField] GameObject kemuri_shogun;
    [SerializeField] GameObject help;
    SpriteRenderer helpSp;
    [Range(0.1f, 0.5f)] [SerializeField] float blinkSpeed = 0.2f;

    [SerializeField] AudioClip kemuriSFX;
    // Start is called before the first frame update
    void Start()
    {
        helpSp = help.GetComponent<SpriteRenderer>();
        StartCoroutine(Opening());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Opening()
    {
        //appear shogun
        yield return new WaitForSeconds(1.0f);
        AudioSource.PlayClipAtPoint(kemuriSFX, Camera.main.transform.position);
        kemuri_shogun.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        kemuri_shogun.SetActive(false);
        shogun.SetActive(true);


        //appear princess
        yield return new WaitForSeconds(1.0f);
        AudioSource.PlayClipAtPoint(kemuriSFX, Camera.main.transform.position);
        kemuri_princess.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        kemuri_princess.SetActive(false);
        princess.SetActive(true);

        //help
        yield return new WaitForSeconds(1.0f);
        help.SetActive(true);
        StartBlinking();
        //todo laughing shogun, animation too



        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<SceneLoader>().LoadNextScene();

        yield break;
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < 8; i++)
        {
            switch (helpSp.color.a.ToString())
            {
                case "0":
                    helpSp.color = new Color(helpSp.color.r, helpSp.color.g, helpSp.color.b, 1);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;

                case "1":
                    helpSp.color = new Color(helpSp.color.r, helpSp.color.g, helpSp.color.b, 0);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;

            }
        }
        helpSp.color = new Color(helpSp.color.r, helpSp.color.g, helpSp.color.b, 1);
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }
}
