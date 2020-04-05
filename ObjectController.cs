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
        

    public bool ShogunFall = true;


    private void Awake()
    {
        animator = Ninja.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    

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
    }


    public void NinjaMoveLeft()
    {
        Ninja.transform.Translate(-1, 0, 0);
    }

    public void NinjaFlipHolizontal()
    {
        Ninja.GetComponent<SpriteRenderer>().flipX = true;
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
