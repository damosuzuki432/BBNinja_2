using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suzume : MonoBehaviour
{

    Animator animator;
    [SerializeField] GameObject poopObject;
    bool poopOn = false;
    public bool generator = false;
    public float falltime = 10.0f;

    Ball ball;
    GameSession gameSession;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Poop());
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null) { ball = FindObjectOfType<Ball>(); }
        else if (ball.hasStarted == true && poopOn == false)
        {
            generator = true;
            StartCoroutine(Poop());
            poopOn = true;
        }

    }

    IEnumerator Poop()
    {
          
        if (generator == true)
        {
            while (generator == true)
            {

                yield return new WaitForSeconds(falltime);
                animator.SetBool("poop", true);

                //exit if title state
                if (FindObjectOfType<GameSession>().state == GameSession.State.title)
                {
                    generator = false;
                    yield break;
                }
                else // if not title state, continue 
                {

                    GameObject poop_obj = Instantiate(poopObject, transform.position, transform.rotation);
                    poop_obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);

                    yield return new WaitForSeconds(1.5f);
                    animator.SetBool("poop", false);

                    Destroy(poop_obj, 10.0f);
                }
            }
        }
        else if (generator == false)
        {
            yield break;
        }

    }

}
