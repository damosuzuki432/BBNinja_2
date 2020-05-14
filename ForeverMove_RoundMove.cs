using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeverMove_RoundMove : MonoBehaviour
{
    public float speed = 1;
    public int count = 0;
    Vector2 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        if (count < 100)
        {
            gameObject.transform.Translate(speed / 50, 0, 0);
        }
        else if (count >= 100 && count < 200)
        {
            gameObject.transform.Translate(0, speed / 50, 0);
        }
        else if (count >= 200 && count < 300)
        {
            gameObject.transform.Translate(-speed / 50, 0, 0);
        }

        else if (count >= 300 && count < 400)
        {
            gameObject.transform.Translate(0, -speed / 50, 0);
        }
        else if (count == 400)
        {
            count = 0;
            gameObject.transform.position = originalPos;
        }
    }
}
