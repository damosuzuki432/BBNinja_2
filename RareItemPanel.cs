using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemPanel : MonoBehaviour
{
    public GameObject MagicShuriken;
    public GameObject Scroll;
    public GameObject UFO;
    [SerializeField] GameObject Rare_Explositon;
    [SerializeField] GameObject Sparkles;
    [SerializeField] AudioClip ExplosionSFX;

    //use to check if the item has already been obtained
    public bool MagicShurikenON = false;
    public bool ScrollON = false;
    public bool UFOON = false;
    GameObject[] blocks;


    private void Update()
    {
        if(MagicShurikenON == true &&
           ScrollON == true &&
           UFOON == true)
        {

            Vector2 exploPos = new Vector2(7.2f, 2.4f);
            AudioSource.PlayClipAtPoint(ExplosionSFX, Camera.main.transform.position);
            GameObject explosion = Instantiate(Rare_Explositon, exploPos, transform.rotation);
            DestroyAllBlocks();
            Destroy(explosion, 3.0f);
        }

    }

  
    public void ActivateMagicShuriken()
    {
        MagicShuriken.SetActive(true);
        MagicShurikenON = true;
    }

    public void ActivateScrollIcon()
    {
        Scroll.SetActive(true);
        ScrollON = true;
    }

    public void ActivateUFOIcon()
    {
        UFO.SetActive(true);
        UFOON = true;
    }

    private void DestroyAllBlocks()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.state = GameSession.State.Special; //make it special state to have enough time to preview VFX. See Level manager script.

        MagicShurikenON = false;
        MagicShuriken.SetActive(false);
        ScrollON = false;
        Scroll.SetActive(false);
        UFOON = false;
        UFO.SetActive(false);

        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            GameObject sparkles = Instantiate(Sparkles, block.transform.position, block.transform.rotation);
            Destroy(block);
        }
    }

}
