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
    paddle paddle;
    [SerializeField] GameObject showoneUp;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
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
            gameSession.state = GameSession.State.title;
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
            showOneUpImage();
            lifeIcons[lifeIcons.Length - counter + 1].SetActive(true);
            counter--;
        }
        AudioSource.PlayClipAtPoint(increaseLifeSound, Camera.main.transform.position);
    }

    private void showOneUpImage()
    {
        paddle = FindObjectOfType<paddle>();
        Vector2 paddlePos = paddle.transform.position;
        GameObject showScoretext = Instantiate(showoneUp, paddlePos, transform.rotation);
        showScoretext.AddComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
        Destroy(showScoretext, 2.0f);
    }

}
