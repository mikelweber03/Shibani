using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class turret : MonoBehaviour
{

    [SerializeField] GameObject bullet;

    AnimationController anim;

    public VisualEffect enemyHit;
    public VisualEffect enemyDeath;
    private AnimationEvent TurretHitEvent;

    SphereCollider Awareness;

    private float distanceToPlayer;

    public bool bgotHit;
    public bool binRange;
    public bool bisDead;
    public bool bisFiring;
    public bool balertState;

    public int maxHealth;
    public int health = 100;

    float fireRate;
    float nextFire;

    //New variables
    public float Range;
    public Transform Target;
    bool aware = false;
    Vector2 Direction;
    public GameObject Gun;
    //Already stated above - Bullet
    float nextTimeToFire = 0;
    public Transform Shootpoint;
    public float Force;

    private int count = 0;

    //Flos Vars
    [SerializeField] Transform target;
    [SerializeField] GameObject player;
    [SerializeField] private float enemyRange = 20f;
    [SerializeField] private float maxFiringRange = 20f;
    private float distanceBetweenTarget;
    [SerializeField] private Transform[] projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    private float countdownBetweenFire;
    [SerializeField] private float FireRate = 2f;
    [SerializeField] private Animator animator;

    void Start()
    {
        nextFire = Time.time;
        anim = turret.FindObjectOfType<AnimationController>();
        Awareness = gameObject.GetComponentInChildren<SphereCollider>();
        player = GameObject.FindWithTag("Player");
        animator.GetComponent<Animator>();
    }

    void Update()
    {
        //CheckPlayerDistance();
        //GotHit();
        //ShootBullet();
        //CheckIfTimeToFire();
        //ShootPlayer();

        target = player.transform;
        distanceBetweenTarget = Vector3.Distance(target.position, transform.position);

        if (distanceBetweenTarget <= maxFiringRange)
        {
            animator.SetTrigger("OnEnemyFiring");

            if (countdownBetweenFire <= 0f)
            {
                foreach (Transform SpawnPoints in projectileSpawnPoint)
                {
                    Instantiate(projectilePrefab, SpawnPoints.position, transform.rotation);
                }

                countdownBetweenFire = 1f / FireRate;
            }

            countdownBetweenFire -= Time.deltaTime;

        }
    }

        void CheckIfTimeToFire()
        {
            if (Time.time > nextFire)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }


        void OnTriggerEnter(Collider other)
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
            //Instantiate(bulletLeft, transform.position, transform.rotation);
            // Debug.Log("hilfe!");
        }

        IEnumerator Waiter()
        {
            VisualEffect.Instantiate(enemyDeath);
            yield return new WaitForSeconds(3);
            Destroy(this.gameObject, 15f);
        }

        void ShootPlayer()
        {
            Vector3 targetPos = Target.position;
            Direction = targetPos - (Vector3)transform.position;
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);
            if (rayInfo)
            {
                if (rayInfo.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Found Player");
                    if (binRange == false)
                    {
                        binRange = true;
                    }
                }
                else
                {
                    if (binRange == true)
                    {
                        binRange = false;
                    }
                }
            }
            if (binRange)
            {
                //Gun.transform.up = Direction;
                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / FireRate;
                    shoot();
                }
            }
        }

        void shoot()
        {

            Instantiate(bullet, Shootpoint.position, Shootpoint.rotation);

            //Get angle above the horizonatl where target is
            float angle = Vector3.Angle(Vector3.right, Direction);

            //Create a rotation that will point towards the target
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //In case of negative position on x, flip the angle
            if (Target.transform.position.y < transform.position.y) angle *= -1;
            //Spawn Bullet
            Instantiate(bullet, Shootpoint.position, bulletRotation);
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, Range);
        }
}

