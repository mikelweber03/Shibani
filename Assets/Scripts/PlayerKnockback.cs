using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    public float falltime;

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Knockback(){
        
        StartCoroutine(knockb());
    }

    IEnumerator knockb(){
       playerRb.velocity = Vector3.zero;
       playerRb.angularVelocity = Vector3.zero;
       GameObject.Find("Nagato").GetComponent<PlayerMovement>().enabled = false;
       playerRb.AddRelativeForce(-10, 10, 0, ForceMode.Impulse);
       yield return new WaitForSeconds(falltime);
       GameObject.Find("Nagato").GetComponent<PlayerMovement>().enabled = true;
    }

    
}
