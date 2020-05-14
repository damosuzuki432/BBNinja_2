using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscShogun : MonoBehaviour
{
    public bool horizMove = false;
    public bool sinW = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (horizMove == true)
        {
            float cycle = 4f;
            float f = 1.0f / cycle;
            float sin = Mathf.Sin(2 * Mathf.PI * f * Time.time);
            float cos = Mathf.Cos(2 * Mathf.PI * f * Time.time);
            gameObject.transform.Translate(sin / 8, 0, 0);
        }
        if (sinW == true)
        {
            float cycle = 4f;
            float f = 1.0f / cycle;
            float sin = Mathf.Sin(2 * Mathf.PI * f * Time.time);
            float cos = Mathf.Cos(2 * Mathf.PI * f * Time.time);
            gameObject.transform.Translate(sin / 8, cos / 8, 0);
        }

    }

}
