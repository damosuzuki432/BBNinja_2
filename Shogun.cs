using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shogun : MonoBehaviour
{
    int hitPoint = 33;
    [Range(0.1f, 0.5f)] [SerializeField] float blinkSpeed = 0.2f;
    Vector3 centerPos = new Vector3(7, 9, 0);
    bool act2 = false;
    bool act3 = false;
    public bool horizMove = false;
    public bool sinW = false;
    public bool randomM = false;
    public float moveSpeed = 5.0f;
    SpriteRenderer spriterenderer;
    GameObject[] blocks;
    GameObject[] ojamas;
    [SerializeField] AudioClip shoutNoise;
    [SerializeField] AudioClip damageNoise;
    [SerializeField] AudioClip blockCreateNoise;
    [SerializeField] AudioClip defeatNoise;
    [SerializeField] AudioClip dieNoise;
    [SerializeField] AudioClip moveNoise;
    [SerializeField] GameObject act1Blocks;
    [SerializeField] GameObject act2Blocks;
    [SerializeField] GameObject act3Blocks;
    [SerializeField] GameObject defeatParticle;
    [SerializeField] GameObject dieParticle;
    [SerializeField] Sprite shogunFaceAct2;
    [SerializeField] Sprite shogunFaceAct3;
    bool multiClear = false;

    Ball ball;
    LevelManager levelManager;
    GameSession gameSession;
    BGMmanager bGMmanager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShogunAct1());
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //watch hit point to proceed next Act
        if (hitPoint <= 30 && act2 ==false)
        {
            DestroyAllBlocks();
            StartCoroutine(ShogunAct2());
            act2 = true;
        }
        if (hitPoint <= 10 && act3 == false)
        {
            DestroyAllBlocks();
            StartCoroutine(ShogunAct3());
            act3 = true;
        }
        if (hitPoint <= 0)
        {
            randomM = false;
            StopCoroutine(RandomMove());
            if(multiClear == false)
            {
                multiClear = true;
                StartCoroutine(StartClearSequence());
            }
        }


    }


    IEnumerator ShogunAct1()
    {
        yield return new WaitForSeconds(4.0f);
        AudioSource.PlayClipAtPoint(shoutNoise, Camera.main.transform.position);
        yield return new WaitForSeconds(2.0f);
        AudioSource.PlayClipAtPoint(blockCreateNoise, Camera.main.transform.position);
        act1Blocks.SetActive(true);
        yield break;
    }


    IEnumerator ShogunAct2()
    {
        AudioSource.PlayClipAtPoint(defeatNoise, Camera.main.transform.position);
        Instantiate(defeatParticle, transform.position, transform.rotation);
        spriterenderer.sprite = shogunFaceAct2;
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = 1.25f;
        horizMove = true;
        yield return new WaitForSeconds(4.0f);
        AudioSource.PlayClipAtPoint(blockCreateNoise, Camera.main.transform.position);
        act2Blocks.SetActive(true);
        StartCoroutine(HorizontalMove());
        yield break;
    }

    IEnumerator ShogunAct3()
    {
        AudioSource.PlayClipAtPoint(defeatNoise, Camera.main.transform.position);
        Instantiate(defeatParticle, transform.position, transform.rotation);
        spriterenderer.sprite = shogunFaceAct3;
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = 0.9f;
        circleCollider2D.offset = new Vector2(0, -0.45f);
        horizMove = false;
        StopCoroutine(HorizontalMove());

        Vector3 currentPos = gameObject.transform.position;
        currentPos = centerPos;
        
        //sinW = true;
        AudioSource.PlayClipAtPoint(shoutNoise, Camera.main.transform.position);
        yield return new WaitForSeconds(2.0f);
        AudioSource.PlayClipAtPoint(blockCreateNoise, Camera.main.transform.position);
        act3Blocks.SetActive(true);

        randomM = true;
        StartCoroutine(RandomMove());
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitPoint--;
        StartBlinking();
        AudioSource.PlayClipAtPoint(damageNoise, Camera.main.transform.position);
       }


    IEnumerator Blink()
    {
        for (int i =0; i < 6; i++)
        {
            switch (spriterenderer.color.a.ToString())
            {
                case "0":
                    spriterenderer.color = new Color(spriterenderer.color.r, spriterenderer.color.g, spriterenderer.color.b, 1);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;
                 
                case "1":
                    spriterenderer.color = new Color(spriterenderer.color.r, spriterenderer.color.g, spriterenderer.color.b, 0);
                    yield return new WaitForSeconds(blinkSpeed);
                    break;

            }
        }
        spriterenderer.color = new Color(spriterenderer.color.r, spriterenderer.color.g, spriterenderer.color.b, 1);
    }

    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }

    IEnumerator HorizontalMove()
    {
        //TODO move it seemlessly
        while (horizMove == true)
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(1, 0, 0); // right
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(1, 0, 0); //right
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(-1, 0, 0); //left
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(-1, 0, 0); //left(back to center)
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(-1, 0, 0); //left
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(-1, 0, 0); //left
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(1, 0, 0); //right
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);
            gameObject.transform.Translate(1, 0, 0); // right(back to center)
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);

        }
    }

    IEnumerator RandomMove()
    {
        Vector3 currentPos = gameObject.transform.position;
        while (randomM == true) //
        {
            float randomX = Random.Range(4.5f, 9.2f);
            float randomY = Random.Range(7.3f, 10.0f);
            Vector3 randomPos = new Vector3(randomX, randomY, 0);
            currentPos = randomPos;
            gameObject.transform.position = currentPos;
            
            AudioSource.PlayClipAtPoint(moveNoise, Camera.main.transform.position);
            yield return new WaitForSeconds(0.5f);

        }
        yield break;
    }



    IEnumerator StartClearSequence()
    {
        SpriteRenderer sp = gameObject.GetComponent<SpriteRenderer>();
        sp.enabled = false;

        Instantiate(dieParticle, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(dieNoise, Camera.main.transform.position);

        DestroyAllBlocks();

        gameSession = FindObjectOfType<GameSession>();
        gameSession.gameObject.SetActive(false);
        ball = FindObjectOfType<Ball>();
        ball.gameObject.SetActive(false);

        FindObjectOfType<BGMmanager>().BGM_Stage4.Stop();
        yield return new WaitForSeconds(7f);
        FindObjectOfType<BGMmanager>().BGM_Stage5.Play();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<SceneLoader>().LoadNextScene();

        yield break;
    }

    private void DestroyAllBlocks()
    {
        blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }

        ojamas = GameObject.FindGameObjectsWithTag("OjamaShuriken");
        foreach (GameObject ojama in ojamas)
        {
            Destroy(ojama);
        }
    }
}
