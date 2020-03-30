using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemPanel : MonoBehaviour
{
    public GameObject MagicShuriken;
    public GameObject Scroll;
    public GameObject UFO;

    //use to check if the item has already been obtained
    public bool MagicShurikenON = false;
    public bool ScrollON = false;
    public bool UFOON = false;


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

}
