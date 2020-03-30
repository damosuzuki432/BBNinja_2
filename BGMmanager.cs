using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    string StageName; //to see the stage num
    public AudioSource BGM_Stage1;
    public AudioSource BGM_Stage2;
    public AudioSource BGM_Stage3;
    private string previousScene = "Stage1-9"; //the name of the previous scene

    private void Awake()
    {
        int bgmManagerCount = FindObjectsOfType<BGMmanager>().Length;
        if (bgmManagerCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        StageName = SceneManager.GetActiveScene().name;
        string chkStageName = StageName.Substring(5, 1); //see the 6th alphabet of the stagename
        if (chkStageName == "2")
        {
            BGM_Stage1.Stop();
            BGM_Stage2.Play();
        }
        else if (chkStageName == "3")
        {
            BGM_Stage1.Stop();
            BGM_Stage3.Play();
        }
    }


    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (nextScene.name == "Stage2-1")
        {
            if (BGM_Stage2.isPlaying) { return; }
            BGM_Stage1.Stop();
            BGM_Stage2.Play();
        }
        else if (nextScene.name == "Stage3-1")
        {
            if (BGM_Stage3.isPlaying) { return; }
            BGM_Stage2.Stop();
            BGM_Stage3.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
