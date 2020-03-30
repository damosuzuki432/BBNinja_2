using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] AudioClip barrierSFX;
    [SerializeField] GameObject barrier;
    Vector2 barrierPos;
    paddle paddle;


    //todo barrier disappers when scene transition. why?

    // Start is called before the first frame update
    void Start()
    {
        //TODO it must be a fixed pos 
        paddle = FindObjectOfType<paddle>();
        barrierPos = new Vector2(paddle.transform.position.x,
                                 paddle.transform.position.y - 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CreateBarrier()
    {
        Instantiate(barrier, barrierPos, transform.rotation);
    }
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            AudioSource.PlayClipAtPoint(barrierSFX, Camera.main.transform.position);
            gameObject.SetActive(false);
        }
    }

}
