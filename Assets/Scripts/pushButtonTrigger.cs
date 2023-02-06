using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushButtonTrigger : MonoBehaviour
{
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //Lucianos code in if statement: other.gameObject.tag == "LootBox" 
        if ( other.gameObject.tag == "Player")
        {
            Destroy(Door);
            StartCoroutine(Pushdown());
        }
    }

    IEnumerator Pushdown()
    {
        Vector3 pos;
        for (int i = 0; i < 25; i++)
        {
            pos = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
            transform.position = pos;
            yield return new WaitForSeconds(0.02f);
        }
    }
}
