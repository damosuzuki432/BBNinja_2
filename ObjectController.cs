using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] GameObject Ninja;
    [SerializeField] GameObject Princess;
    [SerializeField] GameObject Princess2;
    [SerializeField] GameObject Shogun;
    [SerializeField] GameObject help;
    Animator animator;

    public float speed = 10.0f;
    public bool walkLeft;
    public bool walkRight;

    private void Awake()
    {
        animator = Ninja.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (walkLeft == true)
        {
            Ninja.transform.Translate(-speed / 50, 0, 0);
        }
        if (walkRight == true)
        {
            Ninja.transform.Translate(speed / 50, 0, 0);
        }

    }




    public void DisapperPrincess()
    {
        Princess.SetActive(false);
    }

    public void DisapperPrincess2()
    {
        Princess2.SetActive(false);
    }

    public void ApperPrincess()
    {
        Princess2.SetActive(true);
    }

    public void AngryNinja()
    {
        animator.SetBool("GetAngry", true);
    }

    public void CalmNinja()
    {
        animator.SetBool("GetAngry", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Paddle", false);
    }

    public void WalkNinja()
    {
        animator.SetBool("Walk", true);
    }

    public void PaddleNinja()
    {
        animator.SetBool("Paddle", true);
    }

    public void PaddleIdleNinja()
    {
        animator.SetBool("PaddleIdle", true);
    }

    public void PaddleUnIdleNinja()
    {
        animator.SetBool("PaddleIdle", false);
    }


    public void NinjaMoveLeft()
    {
        Ninja.transform.Translate(-1, 0, 0);
    }

    public void NinjaFlipHolizontal()
    {
        Ninja.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void NinjaUnFlipHolizontal()
    {
        Ninja.GetComponent<SpriteRenderer>().flipX = false;
    }


    public void NinjaMoveRight()
    {
        Ninja.transform.Translate(1, 0, 0);
    }

    public void ApperShogun()
    {
        Shogun.SetActive(true);
    }
    public void DisapperShogun()
    {
        Shogun.SetActive(false);
    }
    public void AppearHelp()
    {
        help.SetActive(true);
    }
    public void DisappearHelp()
    {
        help.SetActive(false);
    }


}
