using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LifePanel : MonoBehaviour
{
    public GameObject[] lifeIcons;
    LoseCollider loseCollider;
    int counter = 1;
    int maxLife = 5; 
    Ball ball;
    [SerializeField]AudioClip decreaseLifeSound;
    [SerializeField]AudioClip increaseLifeSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecraeseLife() //called by lose collider
    {
        if (counter < maxLife + 1)
        {
            lifeIcons[lifeIcons.Length - counter].SetActive(false);
            counter++;
            AudioSource.PlayClipAtPoint(decreaseLifeSound, Camera.main.transform.position);
            FindObjectOfType<SceneLoader>().Invoke("LoadCurrentScene", 2);
            
        }
        else if (counter >= maxLife + 1)
        {
            FindObjectOfType<SceneLoader>().Invoke("LoadGameOverScene", 2);
        }

    }

    public void IncreaseLife() //called by Addlife
    {
        if (counter == 1) { return; }
        else
        {
            lifeIcons[lifeIcons.Length - counter + 1].SetActive(true);
            counter--;
        }
        AudioSource.PlayClipAtPoint(increaseLifeSound, Camera.main.transform.position);
    }
    

}
