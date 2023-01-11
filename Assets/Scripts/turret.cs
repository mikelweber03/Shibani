using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    //public GameObject bulletLeft;

    private CharacterController player;
    AnimationController anim;
    Animator animator;

    SphereCollider Awareness;

    private float distanceToPlayer;

    public bool bgotHit;
    public bool binRange;
    public bool bisDead;
    public bool bisFiring;
    public bool balertState;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("TurredFire", 1, 3);
        anim = turret.FindObjectOfType<AnimationController>();
        animator = gameObject.GetComponent<Animator>();
        Awareness = gameObject.GetComponentInChildren<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        GotHit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            //Destroy(this.gameObject);
            bgotHit = true;
        }
        if (other.gameObject.tag == "NinjaStern")
        {
            //Destroy(this.gameObject);
            Destroy(other.gameObject);
            bgotHit = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Sword")
    //    {
    //        //Destroy(this.gameObject);
    //        bgotHit = false;
    //    }
    //    if (other.gameObject.tag == "NinjaStern")
    //    {
    //        //Destroy(this.gameObject);
    //        bgotHit = false;
    //    }
    //}

    void SwitchToAlert()
        {
            // CheckPlayerDistance
            binRange = true;
        }

        void GotHit()
        {
        if(bgotHit)
        {
            animator.SetTrigger("OnEnemyHitted");
            bgotHit = false;
        //animator.SetBool("GotHit", true);
        }
        //if(!bgotHit)
        //{
        //animator.SetBool("GotHit", false);
        //}
    }

        void CheckPlayerDistance()
        {
            if (binRange)
            {
            animator.SetBool("SawPlayer?", true);
            }
            if (!binRange)
            {
            animator.SetBool("SawPlayer?", false);
            }
        }

        void TurretDeath()
        {
            bisDead = true;
        }


    //void TurretFire()
    //{
    //    bisFiring = true;
    //    Instantiate(bulletLeft, transform.position, transform.rotation);
    //    bisFiring = false;
    //}


}
