using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    string currentSceneName;
    AudioSource audioSource;
    [SerializeField] AudioClip startSFX;
    bool avoidMultipleSound = true;
  

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "NanoLogo")
        {
            StartCoroutine(Fadeout());
        }

    }

    IEnumerator Fadeout()
    {
        yield return new WaitForSeconds(3.0f);
        Color color = Color.black;
        Initiate.Fade("StartMenu", color, 0.5f);
        yield break;

    }

    private void Update()
    {
        if (currentSceneName == "StartMenu")
        {
            GetStarted();
        }
    }

    private void GetStarted()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (avoidMultipleSound == true)
            {
                AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
                StartCoroutine(FindObjectOfType<OpeningManager2>().Opening());
                avoidMultipleSound = false;
            }
        }
    }

 


    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadInfoScene()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
        //TODO NOTE that this is hard coded
    }

    public void Continue()
    {
        if (avoidMultipleSound == true)
        {
            AudioSource.PlayClipAtPoint(startSFX, Camera.main.transform.position);
            Invoke("LoadPreviousScene", 2.0f);
            avoidMultipleSound = false;
        }
        FindObjectOfType<LifePanel>().ResetLife();
        FindObjectOfType<GameSession>().ResetScore();

    }

    public void LoadPreviousScene()
    {
        string chkStageName = FindObjectOfType<LifePanel>().prevSceneName.Substring(5, 1);
        if (chkStageName == "1")
        {
            SceneManager.LoadScene("Stage1-1");
        }
        else if (chkStageName == "2")
        {
            SceneManager.LoadScene("Stage2-1");
        }
        else if (chkStageName == "3")
        {
            SceneManager.LoadScene("Stage3-1");
        }
        else if (chkStageName == "4")
        {
            SceneManager.LoadScene("Stage4-1");
        }
    }
}
