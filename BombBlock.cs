using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : MonoBehaviour
{
    [SerializeField] GameObject invisibleCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        InstantiateInvCol();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InstantiateInvCol();
    }

    private void InstantiateInvCol()
    {
        GameObject upinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        upinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);
        Destroy(upinvCol, 0.3f);

        GameObject rightinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        rightinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
        Destroy(rightinvCol, 0.3f);

        GameObject leftinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        leftinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        Destroy(leftinvCol, 0.3f);

        GameObject downinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        downinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2);
        Destroy(downinvCol, 0.3f);

        GameObject uprightinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        uprightinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 2);
        Destroy(uprightinvCol, 0.3f);

        GameObject downrightinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        downrightinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(2, -2);
        Destroy(downrightinvCol, 0.3f);

        GameObject downleftinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        downleftinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, -2);
        Destroy(downleftinvCol, 0.3f);

        GameObject upleftinvCol = Instantiate(invisibleCollider, transform.position, transform.rotation);
        upleftinvCol.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 2);
        Destroy(upleftinvCol, 0.3f);


    }
}
