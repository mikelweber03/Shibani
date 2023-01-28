using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float moveSpeed = 10f;

    Rigidbody rb;

    public GameObject target;
    Vector2 moveDirection;

    // New Variables


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //moveDirection = -(target.transform.position - transform.position).normalized * moveSpeed;
        //rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name.Equals("Player"))
    //    {
    //        Debug.Log("Hit!");
    //        Destroy(gameObject);
    //    }
    //}
}
