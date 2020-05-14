using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItem : MonoBehaviour
{
    //SFX reference
    [SerializeField] AudioClip audioClip;
    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();

    }

    private void Update()
    {
        if (gameSession.state == GameSession.State.title
            || gameSession.state == GameSession.State.Special)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            TriggerSFX();
            Debug.Log(gameObject.name);
            ActivateIcon();

            Destroy(gameObject);

        }
    }

    private void ActivateIcon()
    {
        //TODO remove (Clone) because this could be troublesome
        if (gameObject.name == "MagicShuriken(Clone)")
        {
            FindObjectOfType<RareItemPanel>().ActivateMagicShuriken();
        }
        if (gameObject.name == "Scroll(Clone)")
        {
            FindObjectOfType<RareItemPanel>().ActivateScrollIcon();
        }
        if (gameObject.name == "UFO(Clone)")
        {
            FindObjectOfType<RareItemPanel>().ActivateUFOIcon();
        }
    }

    private void TriggerSFX()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
    }

}
