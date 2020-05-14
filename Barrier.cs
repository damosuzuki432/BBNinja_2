using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject barrier;
    Vector3 barrierPos;
    public bool barrierOn = false;


    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        barrierPos = new Vector3(6.2f, 1.15f, 1.0f);
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (barrierOn == true)
        {
            CreateBarrier();
        }
    }

    public void CreateBarrier()
    {
        Instantiate(barrier, barrierPos, transform.rotation);
        barrierOn = true;
    }
        
}
