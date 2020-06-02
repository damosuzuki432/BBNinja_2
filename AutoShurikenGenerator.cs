using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShurikenGenerator : MonoBehaviour
{
    [SerializeField]GameObject ShurikenItem;
    public float fallTime = 10.0f;
    public bool generator = false;
    bool shurikenOn = false;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (ball == null) { ball = FindObjectOfType<Ball>(); }
        else if (ball.hasStarted == true && shurikenOn == false)
        {
            generator = true;
            StartCoroutine(ShurikenGenerator());
            shurikenOn = true;
        }
    }

    IEnumerator ShurikenGenerator()
    {     
            if (generator == true)
            {
                while (generator == true)
                {
                    yield return new WaitForSeconds(fallTime);
                    GameObject shurikenItem_obj = Instantiate(ShurikenItem, transform.position, transform.rotation);
                    shurikenItem_obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
                    Destroy(shurikenItem_obj, 10.0f);   
                }
            }
            else if (generator == false)
            {
                yield break;
            }
        
    }
}
