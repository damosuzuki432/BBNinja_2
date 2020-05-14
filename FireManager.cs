using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<ChargeManager>().chargeLevel_5 == true)
        {
            //to soleve the problem when chargelevel 5 = true, all the blocks are isTrigger = on
            //and the Fire cannot collide to block.
       
            //FireIsTriggerOn();
            //FireColliderOff();
        }

    }

    private void FireColliderOff()
    {
        ParticleSystem.CollisionModule particleC = GetComponent<ParticleSystem>().collision;
        particleC.enabled = false;

    }

    private void FireIsTriggerOn()
    {
        ParticleSystem.TriggerModule particleT = GetComponent<ParticleSystem>().trigger;
        particleT.enabled = true;

    }
}
