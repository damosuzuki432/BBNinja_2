using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] GameObject loveImage;
    [SerializeField] GameObject kemuriImage;
    [SerializeField]AudioClip lightning;
    [SerializeField] AudioClip kemuri;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Act1());
    }



    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Act1()
    {
   
        yield return new WaitForSeconds(7.0f);
        FindObjectOfType<BGMFadeout>().fadeOut = true;
        StartCoroutine(Act2());
        yield break;
        
    }

    IEnumerator Act2()
    {
        FindObjectOfType<ChangeCameraColor>().dark();
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<BGMFadeout>().fadeOut = false;
        AudioSource.PlayClipAtPoint(lightning, Camera.main.transform.position);
        FindObjectOfType<ChangeCameraColor>().lightning();
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(lightning, Camera.main.transform.position);
        FindObjectOfType<ChangeCameraColor>().lightning();
        yield return new WaitForSeconds(0.2f);
        AudioSource.PlayClipAtPoint(lightning, Camera.main.transform.position);
        FindObjectOfType<ChangeCameraColor>().lightning();
        FindObjectOfType<ObjectController>().DisapperPrincess();

        StartCoroutine(Act3());
        yield break;
    }

    IEnumerator Act3()
    {
        FindObjectOfType<BGMFadeout>().playDangerousBGM();
        FindObjectOfType<BGMFadeout>().BGM_1.Stop();
        Destroy(loveImage);
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<ObjectController>().AngryNinja();
        FindObjectOfType<ObjectController>().NinjaMoveLeft();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<ObjectController>().NinjaFlipHolizontal();
        FindObjectOfType<ObjectController>().NinjaMoveRight();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<ObjectController>().NinjaMoveRight();
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<ObjectController>().NinjaUnFlipHolizontal();
        FindObjectOfType<ObjectController>().NinjaMoveLeft();
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<ObjectController>().NinjaFlipHolizontal();
        FindObjectOfType<ObjectController>().CalmNinja();
        StartCoroutine(Act4());
        yield break;
    }

    IEnumerator Act4()
    {
        FindObjectOfType<ChangeCameraColor>().gray();
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<ObjectController>().ApperPrincess();
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<ObjectController>().ApperShogun();
        //TODO fafafa sound
        FindObjectOfType<ObjectController>().AngryNinja();
        FindObjectOfType<ObjectController>().AppearHelp();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ObjectController>().DisappearHelp();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ObjectController>().AppearHelp();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ObjectController>().DisappearHelp();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Act5());
        yield break;
    }

    IEnumerator Act5()
    {
        kemuriImage.SetActive(true);
        AudioSource.PlayClipAtPoint(kemuri, Camera.main.transform.position);
        FindObjectOfType<ObjectController>().DisapperPrincess2();
        FindObjectOfType<ObjectController>().DisapperShogun();
        yield return new WaitForSeconds(1.0f);
        kemuriImage.SetActive(false);
        FindObjectOfType<ObjectController>().WalkNinja();
        FindObjectOfType<ObjectController>().NinjaUnFlipHolizontal();
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<ObjectController>().walkLeft = true;
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<ObjectController>().walkLeft = false;
        FindObjectOfType<ObjectController>().NinjaFlipHolizontal();
        FindObjectOfType<ObjectController>().PaddleNinja();
        FindObjectOfType<ObjectController>().walkRight = true;
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<ObjectController>().walkRight = false;
        FindObjectOfType<ObjectController>().PaddleIdleNinja();
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<ObjectController>().PaddleUnIdleNinja();
        FindObjectOfType<ObjectController>().walkRight = true;
        yield return new WaitForSeconds(3.0f);
        FindObjectOfType<BGMFadeout>().ResetParams();
        FindObjectOfType<BGMFadeout>().fadeOut2 = true;
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<BGMFadeout>().stopDangerousBGM();
        StartCoroutine(Act6());
        yield break;
    }

    IEnumerator Act6()
    {
        FindObjectOfType<SceneLoader>().LoadNextScene();
        yield break;
    }

}
