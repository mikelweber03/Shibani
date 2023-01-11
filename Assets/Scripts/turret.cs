using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class turret : MonoBehaviour
{
    public GameObject bulletLeft;

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
    public bool bwhoHittedMe;

    public int maxHealth;

    private int count = 0;
    private Vector3 start;
    private VisualEffect enemyHit;
    private VisualEffect enemyDeath;
    private AnimationEvent TurretHitEvent;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CatBulletLeft", 1, 3);
        anim = turret.FindObjectOfType<AnimationController>();
        animator = gameObject.GetComponent<Animator>();
        Awareness = gameObject.GetComponentInChildren<SphereCollider>();
        enemyHit = GetComponentInChildren<VisualEffect>();
        enemyDeath = GetComponentInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        GotHit();
        ShootBullet();
        if (bisFiring)
        {
            TurretFire();
        }
     }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword")
        {
            //Destroy(this.gameObject);
            bgotHit = true;
            TakeDamage();
        }
        if (other.gameObject.tag == "NinjaStern")
        {
            Destroy(other.gameObject);
            bgotHit = true;
            TakeDamage();
        }
    }

    void SwitchToAlert()
    {
        // CheckPlayerDistance
        binRange = true;
    }

    void GotHit()
    {
        if (bgotHit)
        {
            animator.SetTrigger("OnEnemyHitted");
            bgotHit = false;
        }
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
        if (bisDead)
        {
            animator.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 15f);
        }
    }

    void TakeDamage()
    {
        if (count >= maxHealth)
        {
            Destroy(this.gameObject);
            count = 0;
            bisDead = true;
            TurretDeath();
        }
        else
            count++;
    }

    void playVFX()
    {
        enemyHit.Play();
    }

    void TurretFire()
    {
        animator.SetTrigger("OnEnemyFiring");
        Instantiate(bulletLeft);
    }

    void ShootBullet()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            bisFiring = true;
            TurretFire();
            bisFiring = false;
        }
    }


}
