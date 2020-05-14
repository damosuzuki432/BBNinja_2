using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowGenerator : MonoBehaviour
{
    /// <summary>
    /// manage luanch of arrows. attached to Singlton canvas - ChargePanel - ArrowPod.
    /// </summary>

    [SerializeField] GameObject Arrow; //reference of arrow
    [SerializeField] AudioClip launchSound;

    Vector2 rightPos;
    Vector2 leftPos;

    public bool arrowEnabled = false;

    ChargeManager chargeManager;
    paddle paddle;


    // Start is called before the first frame update
    void Start()
    {
        paddle = FindObjectOfType<paddle>();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        paddle = FindObjectOfType<paddle>();
    }

    private void Update()
    {
        //always calclate the position of paddle.
        rightPos = new Vector2(paddle.transform.position.x + 1,
                               paddle.transform.position.y + 1);
        leftPos  = new Vector2(paddle.transform.position.x - 1,
                               paddle.transform.position.y + 1);
    }

    public void CreateArrow() //called by charge manager.
    {
        StartCoroutine(ArrowManager());
    }

    IEnumerator ArrowManager()
    {
        //change the frequency of arrow launch by charge level.
        chargeManager = FindObjectOfType<ChargeManager>();

        if (chargeManager.chargeLevel_1 == true)
        {
            while (chargeManager.chargeLevel_1 == true)
            {
                yield return new WaitForSeconds(4.0f);
                Launch();
            }

        }
        else if (chargeManager.chargeLevel_1 == false)
        {
            //stop while loop 
            yield break;
        }

        if (chargeManager.chargeLevel_2 == true)
        {
            while (chargeManager.chargeLevel_2 == true)
            {
                yield return new WaitForSeconds(3.0f);
                Launch();
                yield return new WaitForSeconds(0.5f);
                Launch();
            }

        }
        else if (chargeManager.chargeLevel_2 == false)
        {
            //stop while loop 
            yield break;
        }


        //while (chargeManager.chargeLevel_2 == true) //TODO delete
        //{
        //    yield return new WaitForSeconds(3.0f);
        //    Launch();
        //    yield return new WaitForSeconds(0.5f);
        //    Launch();
        //}
        //if (chargeManager.chargeLevel_2 == false)
        //{
        //    //stop while loop
        //    yield break;
        //}

    }

    private void Launch()
    {

        Ball ball = FindObjectOfType<Ball>();
        if (ball == null) { return; } //avoid launching when ball is dead.
        else if (ball.hasStarted)
        {
            AudioSource.PlayClipAtPoint(launchSound, Camera.main.transform.position);
            Instantiate(Arrow, leftPos, transform.rotation);
            Instantiate(Arrow, rightPos, transform.rotation);
        }
    }

}
