using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowGenerator : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    public bool arrowEnabled = false;
    [SerializeField] AudioClip launchSound;
    Vector2 rightPos;
    Vector2 leftPos;
    ChargeManager chargeManager;
    paddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        paddle = FindObjectOfType<paddle>();
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        paddle = FindObjectOfType<paddle>();
    }

    private void Update()
    {
        rightPos = new Vector2(paddle.transform.position.x + 1,
                               paddle.transform.position.y + 1);
        leftPos  = new Vector2(paddle.transform.position.x - 1,
                               paddle.transform.position.y + 1);
    }

    public void CreateArrow()
    {
        StartCoroutine(ArrowManager());
    }

    IEnumerator ArrowManager()
    {
        chargeManager = FindObjectOfType<ChargeManager>();
        if (chargeManager.chargeLevel_1 == true)
        {
            while (chargeManager.chargeLevel_1 == true)
            {
                yield return new WaitForSeconds(4.0f);
                Launch();
            }

        }
        else if (chargeManager.chargeLevel_1 == false)
        {

            yield break;
        }

        if (chargeManager.chargeLevel_2 == false)
        {
            yield break;
        }
        else
            while (chargeManager.chargeLevel_2 == true)
        {
            yield return new WaitForSeconds(3.0f);
            Launch();
        }
    }


    private void Launch()
    {

        Ball ball = FindObjectOfType<Ball>();
        if (ball.hasStarted)
        {
            AudioSource.PlayClipAtPoint(launchSound, Camera.main.transform.position);
            Instantiate(Arrow, leftPos, transform.rotation);
            Instantiate(Arrow, rightPos, transform.rotation);
        }
    }
}
