using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement2 : MonoBehaviour
{

    public bool gameOver = false;
    [Header("Jumping")]
    public bool isOnGround = true;
    public float lastGroundedTime;
    public float lastJumpTime;
    public float dustTime;
    public bool  isJumping;
    public bool hasFlipped;
    public bool  jumpInputReleased;
    public float jumpcutMultiplier;
    public float fallinggravity = 10;
    Vector3 gravityNormal;
    Vector3 gravityFalling;

    public Vector3 playerPosition;

    private Rigidbody playerRb;

    private Vector3 inputVector;

    private bool wallRight;
    private bool wallLeft;
    private bool correctWallSide;


    [SerializeField]
    [Header("Horizontal Movement")]
    private float movementSpeed = 20f;

    public float acceleration;

    public float decceleration;

    public float velPower;

    private float jumpForce = 26f;

    private float horizontalInput;

    private float verticalInput;

    private float gravityModifier = 5;

    private bool facingRight = true;
    private float directionValue;
    private bool canFlip;



    [Header("swordAtack")]

    //[SerializeField]
    public VisualEffect slash;

    public MeshRenderer swortMeshRenderer;

    public BoxCollider swortBoxCollider;

    bool canAtack = true;

    private float atackTime = 0.4f;

    private float atackCoolDown = 0.2f;



    [Header("NinjaStar")]



    public int maxstarAmont = 3;

    private int starAmont;

    public NinjaStarUI starBar;

    public GameObject ninjaStar;

    public bool gotStar;

    [SerializeField]
    private bool canThrow = true;

    public float throwTime;



    [Header("Dashing")]

    public ParticleSystem smoke;

    public bool dashJump;

    private float dashJumpForce = 24f;

    public SpriteRenderer haed;

    public SpriteRenderer rightArm;

    public SpriteRenderer body;

    public SpriteRenderer leftArm;

    public SpriteRenderer rightLeg;

    public SpriteRenderer leftLeg;

    public SpriteRenderer cloud;

    public bool dashBlock = false;

    public bool canDash = true;

    private float timeBtweDashes = 1.75f;

    private float dashForce = 60f;

    private float dashingTime = 0.2f;

    //private float dashDelay = 0.2f;�

    //public float dashJumpTime;�

    public bool floatTime = true;



    [Header("Wall Jump")]

    public bool isOnWall = false;

    //private float wallJump;�

    //public bool grounded = true;

    private float climping = 5f;

    [Header("Animation")]

    Animator anim;
    public ParticleSystem DustWalk;
    public ParticleSystem DustJump;

    public bool dusting = false;

    private float targetSpeed;

    void Start()

    {
        haed = GetComponent<SpriteRenderer>();

        starAmont = 0;

        slash = this.GetComponent<VisualEffect>();
        playerRb = GetComponent<Rigidbody>();
        gravityNormal = new Vector3(0f, -9.8f, 0f) * gravityModifier;
        gravityFalling = new Vector3(0f, -9.8f, 0f) * fallinggravity;

        Physics.gravity = gravityNormal;

       // Debug.Log(Physics.gravity);

        starBar = FindObjectOfType<NinjaStarUI>();

        //Connect Animator to Script
        anim = GetComponentInChildren<Animator>();
        dusting = false;

    }



    private void FixedUpdate()
    {

        
        if (!isOnWall && !dashBlock) 
        {
            
            //if (Mathf.Abs(horizontalInput) <= deadZonePos || Mathf.Abs(horizontalInput) >= deadZoneNeg)
            //{
            //    horizontalInput = 0;
            //}
            targetSpeed = horizontalInput * movementSpeed;

            float speedDif = targetSpeed - playerRb.velocity.x;

            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

            playerRb.AddForce(movement * Vector3.right);

        }

        

        if (horizontalInput < 0 && !isOnWall && facingRight ) // && grounded)

        {
            canFlip = true;
            directionValue = -1;
            FlipPlayer(directionValue);
        }
        else if (horizontalInput > 0 && !isOnWall && !facingRight ) // && grounded)

        {
            canFlip = true;
            directionValue = 1;
            FlipPlayer(directionValue);
        }
        else if (isOnWall && wallRight && !facingRight)
        {
            canFlip = true;
            directionValue = 1;
            FlipPlayer(directionValue);

        } 
        else if (isOnWall && wallLeft && facingRight)
        {
            canFlip = true;
            directionValue = -1;
            FlipPlayer(directionValue);
        }

        if (wallLeft && !facingRight && isOnWall)
        {
            canFlip = false;
            correctWallSide = true;
        }

        if (wallRight && facingRight && isOnWall)
        {
            canFlip = false;
            correctWallSide = true;
        }

    }

    void CreateDust()
    {
        DustWalk.Play();
    }

    void FlipPlayer(float directionValue)
    {
        if (canFlip)
        {
            Quaternion currentRotation = this.transform.rotation;
            currentRotation.y = Mathf.Acos(directionValue) * Mathf.Rad2Deg;
            this.transform.rotation = currentRotation;
            facingRight = !facingRight;
            if (isOnGround)
            {
                CreateDust();
            }
            
        }
    }

    void Update()

    {
        //Debug.Log(horizontalInput);
        // eingabe f�r die bewegung in wertikalerweise �

        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");



        // let the Player shoot a Ninja Star�
        //&& !isOnWall

        if (Input.GetKeyDown(KeyCode.Q)  && gotStar || Input.GetKeyDown(KeyCode.Joystick1Button1) && gotStar)

        {

            NinjaStarAbility();
            //Debug.Log("sternwurf");
        }

        // Anim stats for Walking
        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetFloat("MovementSpeed", 1);
            dusting = true;
        }
        else anim.SetFloat("MovementSpeed", 0); dusting = false;
        







        // macht die m�gliochkeiten um an der wand zu kleben sowie einen walljump�

        if (isOnWall )

        {
            Physics.gravity = gravityNormal;
            playerRb.velocity = Vector3.zero;

            playerRb.angularVelocity = Vector3.zero;

            playerRb.useGravity = false;

            //grounded = false;

            dashJump = false;
            
            anim.SetBool("CanDashJump", false);

            //canThrow = false;



            transform.Translate(Vector3.up * verticalInput * Time.deltaTime * climping);

            if (isOnWall && correctWallSide && Input.GetKeyDown(KeyCode.Space) || isOnWall && correctWallSide && Input.GetKeyDown(KeyCode.Joystick1Button0))

            {

                //transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 180, 0);
                playerRb.AddRelativeForce(-15, 25, 0, ForceMode.Impulse);
                isOnWall = false;
                canFlip = false;
                anim.SetBool("IsGrounded_Wall", false);
                anim.SetTrigger("OnWallJump");

                playerRb.useGravity = true;
                canThrow = true;

            }





            // release from wall�

            else if (isOnWall && Input.GetKeyDown(KeyCode.Q) || isOnWall && Input.GetKeyDown(KeyCode.Joystick1Button1) || isOnWall && isOnGround) 

            {

                playerRb.useGravity = true;


                playerRb.AddRelativeForce(-1, 3, 0, ForceMode.Impulse);

                isOnWall = false;
                canThrow = true;
                anim.SetBool("IsGrounded_Wall", false);
                anim.SetBool("IsGrounded_Ground", true);

                //grounded = true;



            }



        }
        playerPosition = transform.position;





        // flipp the player sprite�

        


        // sprung nach dem dash�

        if (Input.GetKeyDown(KeyCode.Space) && dashJump && !isOnWall && !isOnGround || Input.GetKeyDown(KeyCode.Joystick1Button0) && dashJump && !isOnWall && !isOnGround)

        {
            
            Physics.gravity = gravityNormal;

            playerRb.useGravity = true;

            dashBlock = false;

            //grounded = true;

            isOnGround = false;
            anim.SetBool("IsGrounded_Ground", false);

            playerRb.velocity = Vector3.zero;

            playerRb.angularVelocity = Vector3.zero;

            playerRb.AddForce(Vector3.up * dashJumpForce, ForceMode.Impulse);

            dashJump = false;
            anim.SetBool("CanDashJump", false);



        }



        // let the Player Jump �

        else if (Input.GetKeyDown(KeyCode.Space) && isOnGround  && !isOnWall || Input.GetButtonDown("Jump") && isOnGround && !isOnWall)

        {
            //AnimationTrigger
            anim.SetTrigger("OnBaseJump");

            playerRb.velocity = Vector3.zero;

            playerRb.angularVelocity = Vector3.zero;

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastGroundedTime = 0;
            lastJumpTime = 0;
            isJumping = true;
            jumpInputReleased = false;

            isOnGround = false;
            anim.SetBool("IsGrounded_Ground", false);
            dusting = false;
            DustJump.Play();
            
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isOnGround && !isOnWall || Input.GetButtonUp("Jump") && !isOnGround && !isOnWall)
        {
            Physics.gravity = gravityFalling;
        }


        //let the Player Dash�

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Joystick1Button4))

        {

            DashAbility();

        }



        // Sword atack�

        if (Input.GetKeyDown(KeyCode.E) && !isOnWall || Input.GetKeyDown(KeyCode.Joystick1Button2))

        {
            SwordAbility();
        }

    }

    

    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.CompareTag("StarPickUp") && other != null && starAmont < 3)

        {

            Destroy(other.gameObject);

            starAmont++;

            gotStar = true;

            starBar.ChangeStar(starAmont);

             Debug.Log("pickup");

        }

        if (other.gameObject.CompareTag("Latern"))
        {
            this.transform.parent = other.transform;

            isOnGround = true;

            dashJump = false;
        }

    }
    private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.CompareTag("Latern"))
        {
            transform.SetParent(null);
            //Debug.Log("geht das ?");
        }

    }


    private void OnCollisionEnter(Collision collision)

    {

        // Check if the player is on hart surves and gives him the ability to jump again�



        if (collision.gameObject.CompareTag("Ground"))

        {

            isOnGround = true;
            Physics.gravity = gravityNormal;
            anim.SetBool("IsGrounded_Ground", true);

            dashJump = false;
            anim.SetBool("CanDashJump", false);

            //grounded = true;



        }

        else if (collision.gameObject.CompareTag("Wall") && !isOnGround)

        {
            if(collision.transform.position.x > playerPosition.x)
            {
                wallRight = true;
                wallLeft = false;
                 
            }
            else if(collision.transform.position.x < playerPosition.x)
            {
                wallLeft = true;
                wallRight = false;
                
            }
            isOnWall = true;
            Physics.gravity = gravityNormal;
            anim.SetBool("IsGrounded_Wall", true);

            dashJump = false;
            anim.SetBool("CanDashJump", false);


        }

        else if (collision.gameObject.CompareTag("AnimBox"))
        {
            Debug.Log("I'm touching");
            anim.SetBool("isPushing", true);
        }
    }


    // check ob spieler an der wand ist�

    private void OnCollisionExit(Collision collision)

    {
        if (collision.gameObject.CompareTag("Wall"))

        {

            isOnWall = false;
            anim.SetBool("IsGrounded_Wall", false);

            playerRb.useGravity = true;

        }
        
        else if (collision.gameObject.CompareTag("AnimBox"))
        {
            Debug.Log("I'm touching");
            anim.SetBool("isPushing", false);
        }

    }





    // void for the Dash Ability�

    void DashAbility()

    {

        if (canDash)

        {
            anim.SetTrigger("OnDash");
            Physics.gravity = gravityNormal;
            StartCoroutine(Dash());

        }

    }

    // Coroutine for the dash�

    IEnumerator Dash()

    {

        dashBlock = true;

        canDash = false;
        anim.SetBool("CanDash", false);

        //haed.enabled = false;

        //rightArm.enabled = false;

        //body.enabled = false;

        //leftArm.enabled = false;

        //rightLeg.enabled = false;

        //leftLeg.enabled = false;

        //smoke.Play();�

        yield return new WaitForSeconds(0.05f);

        cloud.enabled = true;

        playerRb.velocity = Vector3.zero;

        playerRb.angularVelocity = Vector3.zero;

        playerRb.useGravity = false;

        playerRb.AddRelativeForce(dashForce, 0, 0, ForceMode.Impulse);

        yield return new WaitForSeconds(dashingTime);

        cloud.enabled = false;

        playerRb.velocity = Vector3.zero;

        playerRb.angularVelocity = Vector3.zero;

        yield return new WaitForSeconds(0.05f);

        //haed.enabled = true;

        //rightArm.enabled = true;

        //body.enabled = true;

        //leftArm.enabled = true;

        //rightLeg.enabled = true;

        //leftLeg.enabled = true;

        dashJump = true;
        anim.SetBool("CanDashJump", true);

        yield return new WaitForSeconds(0.1f);

        dashBlock = false;

        playerRb.useGravity = true;
        Physics.gravity = gravityFalling;

        yield return new WaitForSeconds(timeBtweDashes);

        canDash = true;
        anim.SetBool("CanDash", true);

    }



    void SwordAbility()

    {
        
        if (canAtack)

        {
            //Animation Trigger for SwordAttack
            anim.SetTrigger("OnCombat_Sword");
            StartCoroutine(SwordAttack());



        }



    }



    IEnumerator SwordAttack()
    {
        
        canAtack = false;
        anim.SetBool("CanAttack", false);

        slash.Play();

        swortBoxCollider.enabled = true;

        yield return new WaitForSeconds(atackTime);

        swortMeshRenderer.enabled = false;

        swortBoxCollider.enabled = false;

        yield return new WaitForSeconds(atackCoolDown);

        canAtack = true;
        anim.SetBool("CanAttack", true);

    }





    void NinjaStarAbility()

    {

        if (canThrow && gotStar)

        {
            //Animation Trigger for SwordAttack
            //anim.SetTrigger("OnCombat_Shuriken");
            StartCoroutine(NinjaStardAttack());
            Debug.Log("start ninjastar");


        }



    }



    IEnumerator NinjaStardAttack()

    {

        starAmont--;

        starBar.ChangeStar(starAmont);

        if (starAmont > 0)

        {

            gotStar = true;

            //Debug.Log("gotstar");

        }

        else if (starAmont == 0)

        {

            gotStar = false;

            //Debug.Log("nostar");

        }

        canThrow = false;

        Instantiate(ninjaStar, transform.position, playerRb.transform.rotation);

        yield return new WaitForSeconds(throwTime);

        canThrow = true;

    }




}