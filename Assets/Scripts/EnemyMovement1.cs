using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 target;
    bool goal = true;
    private int count = 0;
    private Vector3 start;
    private VisualEffect enemyHit;
    // public BoxCollider playerBoxCollider;
    void Start()
    {
        start.x = transform.position.x;
        enemyHit = this.GetComponent<VisualEffect>();
        // playerBoxCollider = GameObject.FindWithTag("Player").GetComponent<BoxCollider>();
    }

    
    void Update()
    {   //If the enemy is at the target vector, set goal to false
        if (transform.position.x < target.x)
        {
            goal = false;
        }
        //If the nemy isn't at target vector, make him go left towards it
        if (goal == true)
        {
            
            
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            
                
            
        }
        else 
        {
            if (transform.position.x <= start.x)
            {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }
            else
                goal = true;
            
        }

        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            enemyHit.Play();
            if (count >= 1)
            {
                Destroy(this.gameObject);
                count = 0;
            }
            else
                count++;

        }

        if (other.gameObject.tag == "NinjaStern")
        {
            enemyHit.Play();
            if (count >= 1)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                count = 0;
            }
            else
                count++;

        }


    }

}

