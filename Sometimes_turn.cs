using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sometimes_turn : MonoBehaviour
{
    public int angle = 90;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        count++;
        if (count == 100)
        {
            gameObject.transform.Rotate(0, 0, angle);
            count = 0;
        }
    }
}
