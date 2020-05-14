using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningItem : MonoBehaviour
{
    [SerializeField] GameObject Lightning;
    paddle paddle;
    [SerializeField] AudioClip powerUpSound;


    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
            Destroy(gameObject);
            StartCoroutine(CreateThunder());
        
        }
    }

    IEnumerator　CreateThunder()
    {
        int randomX = Random.Range(1, 11);
        Vector2 lightningPos = new Vector2(randomX, 9.0f);
        Quaternion rote = Quaternion.Euler(90.0f, 0.0f, 180.0f);
        GameObject Thunder = Instantiate(Lightning, lightningPos, rote);
        Destroy(Thunder,2.5f);
        yield break;
    }

  
}
