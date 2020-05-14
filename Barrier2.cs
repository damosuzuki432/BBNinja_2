using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Barrier2 : MonoBehaviour
{
    [SerializeField] GameObject barrier2;
    Vector3 barrier2Pos;
    public bool barrier2On = false;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        barrier2Pos = new Vector3(6.2f, 1.0f, 1.0f);
    }

    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        if (barrier2On == true)
        {
            CreateBarrier();
        }
    }


    public void CreateBarrier()
    {
        Instantiate(barrier2, barrier2Pos, transform.rotation);
        barrier2On = true;
    }

}
