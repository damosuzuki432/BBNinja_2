using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager2 : MonoBehaviour
{
    [SerializeField] GameObject love;
    [SerializeField] GameObject ninja;
    [SerializeField] GameObject princess;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Opening()
    {
        FindObjectOfType<ChangeCameraColor>().lightning();
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<ChangeCameraColor>().lightning();
        yield return new WaitForSeconds(0.1f);

        Destroy(love);
        Destroy(princess);

        yield return new WaitForSeconds(0.5f);

        ninja.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        ninja.GetComponent<SpriteRenderer>().flipX = false;
        yield return new WaitForSeconds(0.5f);

        ninja.GetComponent<SpriteRenderer>().flipX = true;
        yield return new WaitForSeconds(0.5f);
        ninja.GetComponent<SpriteRenderer>().flipX = false;

        yield return new WaitForSeconds(0.5f);
        ninja.GetComponent<SpriteRenderer>().flipX = true;
        FindObjectOfType<SceneLoader>().LoadNextScene();
        yield break;
    }

}
