using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class EnemyMovement1 : MonoBehaviour
{
    [SerializeField] private float offsetRight = 0, offsetLeft = 0, speed = 1;
    
    [SerializeField] private bool hasReachedRight = false, hasReachedLeft = false;
    
    private int count = 0;
    
    private Vector3 start;
    
    private VisualEffect enemyHit;

    [SerializeField] private Animator anim;

    [Header("Animations")] private bool isMoving;
    
    
    // public BoxCollider playerBoxCollider;
    
    
    
    
    
    void Awake()
    {
        start.x = transform.position.x;
        enemyHit = this.GetComponent<VisualEffect>();
        anim.SetBool("isMoving", true);

    }

    
    void Update()
    {
        MoveHorizontal();
        if (Input.GetButtonDown("Fire1"))
        {
            attack();
        }

    }

    void MoveHorizontal()
    {
        if (!hasReachedRight)
        {
            if (transform.position.x < start.x + offsetRight)
            {
                MovementHorizontal(offsetRight);
            }
            else if (transform.position.x >= start.x + offsetRight)
            {
                hasReachedRight = true;
                hasReachedLeft = false;
            }
        }
        else if (!hasReachedLeft)
        {
            if (transform.position.x > start.x + offsetLeft)
            {
                MovementHorizontal(offsetLeft);
            }
            else if (transform.position.x <= start.x + offsetLeft)
            {
                hasReachedRight = false;
                hasReachedLeft = true;
            }
        }
    }
    void MovementHorizontal(float offset)
    {
        if (!hasReachedRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(start.x + offset, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }

        if (!hasReachedLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(start.x + offset, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            enemyHit.Play();
            anim.SetTrigger("gotHit");  
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
            anim.SetTrigger("gotHit");
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

    private void attack()
    {
        anim.SetTrigger("Attack");
    }
    
    //Add coroutine to delay death so that animation gotHit is played beforehand
    
    
}

