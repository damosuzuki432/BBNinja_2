using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addlife : MonoBehaviour
{
    LifePanel lifePanel;
    paddle paddle;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            paddle = FindObjectOfType<paddle>();
            lifePanel = FindObjectOfType<LifePanel>();
            lifePanel.IncreaseLife();
            Destroy(gameObject);
        }
    }

}
