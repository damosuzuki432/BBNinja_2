using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector2 movementVector = new Vector2(0, -10f);
    [SerializeField] float period = 2;
    [Range(0, 1)] [SerializeField] float movementFactor; //0 for not moved, 1 for fully moved

    Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period; //grows continually from zero 
        const float tau = Mathf.PI * 2; //about 6.28
        float rawSinWave = Mathf.Sin(cycle * tau); //move between -1 to 1
        movementFactor = rawSinWave / 2 + 0.5f;
        Vector2 offset = movementVector * movementFactor;
        transform.localPosition = startingPos + offset;


    }
}
