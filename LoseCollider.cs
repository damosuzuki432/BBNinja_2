using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// on collision to the lose collider, which is part of PlaySpace,
/// you decrease life by one(1). it also load current scene to start again.
/// Also, charge manager goes back to zero.(Reset Charge) 
/// Also, Restart is called by Charge Manager Script.
/// </summary>

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ball")
        {
            LifePanel lifepanel = FindObjectOfType<LifePanel>();
            lifepanel.DecraeseLife(); //decrease life and restart.

            ChargeManager chargeManager = FindObjectOfType<ChargeManager>();
            chargeManager.ResetCharge(); //set charge level to zero.

            Destroy(collision.gameObject);
         
           
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
  }
