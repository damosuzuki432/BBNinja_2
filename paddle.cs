﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour
{

/*
This is attached to "Paddle", which hit the ball.
Main functions are:
 1.limit vertical movement by clamping x axis value.
 2. fix y axis value so that it cannot move vertically.
 In 1., screenwidth and Unity Units is used to cope with different screen environment.
*/

    //paddle move constraints
    [SerializeField]float maxX = 15.3f;
    [SerializeField]float minX = 0.5f;
    [SerializeField]float fixedYpos = 0.45f;

    //screen and mouse info
    float ScreenWidthInUnits; //reference of camera.main.size 
    float ClampedMousePosInUnits;

    //declare vec2 for padddle 
    Vector2 paddlePos;

    //cache out reference
    Ball ball;
    GameSession gamesession;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        gamesession = FindObjectOfType<GameSession>();
        animator = gameObject.GetComponent<Animator>(); 

    }


    // Update is called once per frame
    void Update()
    {
        if(gamesession.state == GameSession.State.title) //to avoid missing reference. IDK the reason but its necessary!!
        {
            gamesession = FindObjectOfType<GameSession>();
        }
        if (gamesession.state == GameSession.State.Playable)
        {
            if (animator.GetBool("panic") == true || Time.timeScale <= Mathf.Epsilon) { return; } //panic is set by trap such as onibi. while panic, paddle cannnot be moved.
            //or , when timescale is 0, (done so by pause script), paddle cannot be moved.
            else
            {
                //the paddle moved along with the mouse Xpos.
                //the mouse pos is dependent on screen with and witdth in units.
                //here is the formula to convert.
                ScreenWidthInUnits = Camera.main.orthographicSize * 2;
                ClampedMousePosInUnits = Mathf.Clamp(GetXpos(), minX, maxX);
                paddlePos = new Vector2(ClampedMousePosInUnits, fixedYpos);
                transform.position = paddlePos;
            }

        }
       
    }

    private float GetXpos()
    {
        if (gamesession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * ScreenWidthInUnits;
        }
    }



}
