using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChargeManager : MonoBehaviour
{
    string StageName; //to test if the stage is more than 2-
    [SerializeField] public Image chargeImage;
    [SerializeField] Sprite[] chargeSprites;
    int chargeCounter = 0;
    ArrowGenerator arrowGenerator;
    Barrier barrier;
    Barrier2 barrier2;
    BarrierItself barrierItself;
    Barrier2Itself barrier2Itself;
    [SerializeField]AudioClip chargeSound;
    GameObject[] blocks;
    public bool chargeLevel_1 = false;
    public bool chargeLevel_2 = false;
    public bool chargeLevel_3 = false;
    public bool chargeLevel_4 = false;
    public bool chargeLevel_5 = false;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (chargeLevel_5 == true)
        {
            FindObjectOfType<Ball>().maxCharge = true;
            PenetrateBall();
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChargeController();
    }

    private void ChargeController()
    {
        if(chargeCounter >= 1 && 2 >= chargeCounter &&
            chargeImage.sprite != chargeSprites[1])
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[1];
            arrowGenerator = FindObjectOfType<ArrowGenerator>();
            if (chargeLevel_1 == false)
            {
                ChargeSFX();
                chargeLevel_1 = true;
                arrowGenerator.CreateArrow();
            }
        }
        if (chargeCounter >= 3 && 4 >= chargeCounter &&
            chargeImage.sprite != chargeSprites[2])
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[2];
            arrowGenerator = FindObjectOfType<ArrowGenerator>();
            if (chargeLevel_2 == false)
            {
                ChargeSFX();
                chargeLevel_1 = false;
                chargeLevel_2 = true;
                arrowGenerator.CreateArrow();
            }
        }
        if (5 <= chargeCounter && 6 >= chargeCounter)
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[3];
            if (chargeLevel_3 == false)
            {
                ChargeSFX();
                barrier = FindObjectOfType<Barrier>();
                barrier.CreateBarrier();
                chargeLevel_3 = true;
            }

        }
        if (7 <= chargeCounter && 8 >= chargeCounter)
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[4];
            if (chargeLevel_4 == false)
            {
                ChargeSFX();
                barrier2 = FindObjectOfType<Barrier2>();
                barrier2.CreateBarrier();
                chargeLevel_4 = true;
            }

        }
        if (9 <= chargeCounter)
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[5];
            if (chargeLevel_5 == false)
            {
                ChargeSFX();
                FindObjectOfType<Ball>().maxCharge = true;
                PenetrateBall();
                StartCoroutine(StopPenetration());
                chargeLevel_5 = true;
            }
        }
    }

    private void PenetrateBall()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            if (block.GetComponent<BoxCollider2D>() == null) { return; } // debug for Shogun(circle collider)
            block.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void ChargeSFX()
    {
        AudioSource.PlayClipAtPoint(chargeSound, Camera.main.transform.position);
    }


    public void ChargeCountAccumulator()
    {
        //Activated only from stage 2
        StageName = SceneManager.GetActiveScene().name;
        string chkStageName = StageName.Substring(5, 1); //see the 6th alphabet of the stagename
        if (chkStageName != "1")
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if(gameSession.state == GameSession.State.Playable)
            {
                chargeCounter++; //accumulate only when state is playable. without this, charge occurs After losing ball when shuriken breaks blocks.
            }
        }
    }

    IEnumerator StopPenetration()
    {
        yield return new WaitForSeconds(5.0f);
        if (FindObjectOfType<Ball>() == null)// for the case when the ball is missing since scene transition!
           {
            //TODO if state == dying, yield break here.
            //TODO if state == title, meaning scene transition, let's wait till(loop) scene == playable.
            //TODO if in 7sec Ball is still null, try again after 1 sec. to go on.
             yield return new WaitForSeconds(7.0f);
           }

           FindObjectOfType<Ball>().maxCharge = false;
           blocks = GameObject.FindGameObjectsWithTag("Block");
           chargeLevel_5 = false;
           chargeImage = GetComponent<Image>();
           chargeImage.sprite = chargeSprites[4];
           chargeCounter = 7;
           foreach (GameObject block in blocks)
            {
                string gameObjectName = block.name;
                if (block.name.Contains("GR"))
                {
                    continue; //debug for glass block
                }
                if (block.GetComponent<BoxCollider2D>() == null)
                {
                    continue; // debug for Shogun(circle collider)
                }
              block.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        yield break;
    }

    public void ResetCharge()
    {
        chargeCounter = 0;
        chargeLevel_1 = false;
        chargeLevel_2 = false;
        chargeLevel_3 = false;
        chargeLevel_4 = false;
        chargeLevel_5 = false;

        chargeImage = GetComponent<Image>();
        chargeImage.sprite = chargeSprites[0];

        barrierItself = FindObjectOfType<BarrierItself>();
        barrier2Itself = FindObjectOfType<Barrier2Itself>();
        int barrierCount = FindObjectsOfType<BarrierItself>().Length;
        int barrier2Count = FindObjectsOfType<Barrier2Itself>().Length;
        if (barrierCount > 0) { barrierItself.SelfDestruct(); }
        if (barrier2Count > 0) { barrier2Itself.SelfDestruct(); }


    }
}
