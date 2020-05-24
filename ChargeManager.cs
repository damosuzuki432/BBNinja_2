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
    public bool maxCharge = false;
    public int penetCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (chargeLevel_5 == true)
        {
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
        if(chargeCounter >= 10 && 20 >= chargeCounter &&
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
        if (chargeCounter >= 30 && 40 >= chargeCounter &&
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
        if (50 <= chargeCounter && 60 >= chargeCounter)
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
        if (70 <= chargeCounter && 80 >= chargeCounter)
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
        if (90 <= chargeCounter)
        {
            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[5];
            if (chargeLevel_5 == false)
            {
                ChargeSFX();
                maxCharge = true;
                PenetrateBall();
                chargeLevel_5 = true;
            }
        }
        if(chargeLevel_5 == true)
        {
            StopPenetration();
        }

        StageName = SceneManager.GetActiveScene().name;
        string chkStageName = StageName.Substring(5, 1); //see the 6th alphabet of the stagename
        if(chkStageName == "4")
        {
            ResetCharge();
        }

    }

    private void PenetrateBall()
    {       
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            if (block.GetComponent<BoxCollider2D>() == null) { return; }
            block.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void ChargeSFX()
    {
        AudioSource.PlayClipAtPoint(chargeSound, Camera.main.transform.position);
    }


    public void ChargeCountAccumulator()
    {
        //Activated from stage 2
        StageName = SceneManager.GetActiveScene().name;
        string chkStageName = StageName.Substring(5, 1); //see the 6th alphabet of the stagename
        if (chkStageName != "1" && chkStageName != "4")
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if(gameSession.state == GameSession.State.Playable)
            {
                chargeCounter++; //accumulate only when state is playable. without this, charge occurs After losing ball when shuriken breaks blocks.
            }
        }
        

    }

    private void StopPenetration()
    {
        if(penetCount > 30)
        {
            chargeLevel_5 = false;
            maxCharge = false;

            chargeImage = GetComponent<Image>();
            chargeImage.sprite = chargeSprites[4];

            chargeCounter = 71;
            penetCount = 0;

            blocks = GameObject.FindGameObjectsWithTag("Block");
            foreach (GameObject block in blocks)
            {
                if(block == null)
                {
                    break;
                }

                string gameObjectName = block.name;
                if (block.name.Contains("GR"))
                {
                    continue; //debug for glass block
                }

                block.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }

    public void ResetCharge()
    {
        chargeCounter = 0;
        penetCount = 0;
        maxCharge = false;
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
