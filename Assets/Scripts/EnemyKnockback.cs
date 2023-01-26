using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public float falltime;
    private Rigidbody enemyRB;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
    }


    public void Knockback()
    {

        StartCoroutine(knockb());
    }

    IEnumerator knockb()
    {
        enemyRB.velocity = Vector3.zero;
        enemyRB.angularVelocity = Vector3.zero;
        GameObject.FindWithTag("Enemy").GetComponent<PlayerMovement2>().enabled = false;
        enemyRB.AddRelativeForce(-8, 8, 0, ForceMode.Impulse);
        yield return new WaitForSeconds(falltime);
        GameObject.FindWithTag("Enemy").GetComponent<PlayerMovement2>().enabled = true;
    }
}
