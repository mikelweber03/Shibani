using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwarenessEnemy : MonoBehaviour
{
    public turret enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponentInParent<turret>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyScript.binRange = true;
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyScript.binRange = false;
            return;
        }
    }
}
