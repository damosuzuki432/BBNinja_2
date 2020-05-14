using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    /*
    This is attached to "Block", which is the main target of this game.
    Main functions are:
     1. refer to audio clip and play it when destroyed.
     2. refert to VFX, usually a particle system, to play when destroyed.
     3. define score per hit, and call scoreboard to update score.
     4. manage the times of hit and change sprite image accordingly.
     5. destroy itself when timesHit reaches maxhitpoints.
    */


    //FX reference
    [SerializeField] AudioClip breakSound;　//SFX reference 
    [SerializeField] GameObject blockSparklesVFX; //VFX reference

    //sprite reference
    [SerializeField] Sprite[] dmgSprites; //multiple sprite reference
    [Tooltip("RareItem Has Specific Order!")][SerializeField] GameObject[] items; //item reference

    //config params
    [SerializeField] public int scorePerBreak = 100; //set scorepoints per break

    //cached reference
    Rigidbody2D rigidbody;
    RareItemPanel rareItemPanel;
    BoxCollider2D boxCollider2D;

    public bool IsPenetrate =false;

    //hitpoints manager
    int timesHit = 0;

   
    private void Start()
    { 
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerSFX();
        DestroyBlock();
    }


    private void OnParticleCollision(GameObject other) //for fireball (when block trigger is off)
    {
        TriggerSFX();
        DestroyBlock();
    }

    private void OnParticleTrigger() //for fireball (when block trigger is on)
    {
        TriggerSFX();
        DestroyBlock();
    }

    private void TriggerSFX()
    {
        //PlayClipAtPoint is USEFUL becase it can play the sound even if the object is destroyed.
        //Without this, the sound is not played because it'll be immediately broken
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void DestroyBlock()
    {
        //On every hit, add score
        AddScore();
        DestroyManager();

    }

    private void AddScore()
    {
        FindObjectOfType<GameSession>().Score(scorePerBreak);
    }

    private void DestroyManager()
    {
        //count up timesHit
        timesHit++;

        //maxHits is the HP of the block.
        //the HP should be one more than the number of dmg sprites.
        //e.g. if you 2 have dmgSprites, you have one(1) original sprites, and two damaged image.
        //that means you it requires 3 hits to be broken.
        //assumes you change the image on every hit. 
        int maxHits = dmgSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            TriggerSparklesVFX();
            Destroy(gameObject);
            ItemFallManager();
            FindObjectOfType<LevelManager>().DecreaseBlock();
            FindObjectOfType<ChargeManager>().ChargeCountAccumulator();
        }
        else
        {
            showNextDmgSprites();
        }
    }

    
    public void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1.0f);
    }
    
    private void ItemFallManager() //TODO Clean Up! RareItemHAS specific order!!!
    {
        int itemChecker;
        GameObject getItem;
        itemChecker = UnityEngine.Random.Range(1, 257);
        if (items[0] == null)
        {
            if (1 <= itemChecker && itemChecker <= 32) //normal item 1/8
            {
                if (items[1] == null) { return; }
                getItem = Instantiate(items[1], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
            else if (33 <= itemChecker && itemChecker <= 40) //rare item 1/32
            {
                if (items[2] == null) { return; }
                getItem = Instantiate(items[2], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
            else if (41 <= itemChecker && itemChecker <= 44) // super rare item 1/64 no.1
            {
                rareItemPanel = FindObjectOfType<RareItemPanel>();
                if (items[3] == null || rareItemPanel.MagicShurikenON == true) { return; }
                getItem = Instantiate(items[3], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
            else if (45 <= itemChecker && itemChecker <= 48) //super rare item 1/64 no.2
            {
                rareItemPanel = FindObjectOfType<RareItemPanel>();
                if (items[4] == null || rareItemPanel.ScrollON == true) { return; }
                getItem = Instantiate(items[4], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
            else if (49 <= itemChecker && itemChecker <= 52) //super rare item 1/64 no.3
            {
                rareItemPanel = FindObjectOfType<RareItemPanel>();
                if (items[5] == null || rareItemPanel.UFOON == true) { return; }
                getItem = Instantiate(items[5], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
            else if (itemChecker == 53) //god rare item 1/256
            {
                if (items[6] == null) { return; }
                getItem = Instantiate(items[6], transform.position, transform.rotation);
                ItemFallDown(getItem);
            }
        } //use when you absolutely want to show an item
        else
        {
            getItem = Instantiate(items[0], transform.position, transform.rotation);
            ItemFallDown(getItem);
        }
     
    }

    private static void ItemFallDown(GameObject getItem)
    {
        getItem.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
    }

    private void showNextDmgSprites()
    {
        //on first hit, timesHit becomes 1.
        //so, you want to show timesHit - 1 = 0 index of the sprite array.
        int spriteIndex = timesHit - 1;

        //Just in case, lets avoid when dmgSprites is null!
        //we tend to forget to set them in the inspector.

        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }
        if (dmgSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = dmgSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) //for penetration ball and glass block
    {

        TriggerSFX();
        DestroyBlock();

        
    }
}
