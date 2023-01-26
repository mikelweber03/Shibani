using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class turret : MonoBehaviour
{
    public GameObject bulletLeft;

    private CharacterController player;
    AnimationController anim;
    public Animator animator;

    SphereCollider Awareness;

    private float distanceToPlayer;

    public bool bgotHit;
    public bool binRange;
    public bool bisDead;
    public bool bisFiring;
    public bool balertState;
    public bool bwhoHittedMe;

    public int maxHealth;

    public Transform target; //where we want to shoot(player? mouse?)
    public Transform weaponMuzzle; //The empty game object which will be our weapon muzzle to shoot from
    public GameObject bullet; //Your set-up prefab
    public float fireRate = 3000f; //Fire every 3 seconds
    public float shootingPower = 20f; //force of projection


    private float shootingTime; //local to store last time we shot so we can make sure its done every 3s

    public int health = 100;
    //public GameObject deathEffect;



    //private float position;
    //private Transform positionPlayer;

    private int count = 0;
    private Vector3 start;
    public VisualEffect enemyHit;
    public VisualEffect enemyDeath;
    private AnimationEvent TurretHitEvent;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TurretFire", 1, 3);
        anim = turret.FindObjectOfType<AnimationController>();
        Awareness = gameObject.GetComponentInChildren<SphereCollider>();
        
        //position = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        GotHit();
        //ShootBullet();
        if (bisFiring)
        {
            Fire();
        }
    }


    private void Fire()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + fireRate / 1000; //set the local var. to current time of shooting
            Vector2 myPos = new Vector2(weaponMuzzle.position.x, weaponMuzzle.position.y); //our curr position is where our muzzle points
            GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity); //create our bullet
            Vector2 direction = myPos - (Vector2)target.position; //get the direction to the target
            projectile.GetComponent<Rigidbody2D>().velocity = direction * shootingPower; //shoot the bullet
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            //Destroy(this.gameObject);
            bgotHit = true;
            TakeDamage();
        }
        if (other.gameObject.CompareTag("NinjaStern"))
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
            StartCoroutine(Waiter());           
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
        VisualEffect.Instantiate(enemyHit);
    }

    void playVFX()
    {
        enemyHit.Play();
    }

    void TurretFire()
    {
        //animator.SetTrigger("OnEnemyFiring");
        Instantiate(bulletLeft, transform.position, transform.rotation);
        Debug.Log("hilfe!");
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

    IEnumerator Waiter()
    {
        VisualEffect.Instantiate(enemyDeath);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject, 15f);
    }


}
