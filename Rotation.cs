using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float angle = 90.0f;
    public float slowly = 5.0f;
    // 回転の中心になるオブジェクト
    public Transform target;
    // 回転速度
    public float speed = 10.0f;
    int hitPoint = 0;
    [SerializeField]AudioClip breakSound;


    void Update()
    {
        Vector3 axis = transform.TransformDirection(Vector3.forward);
        transform.RotateAround(target.position,axis,speed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, angle / slowly);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitPoint++;
        if(hitPoint>1)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
