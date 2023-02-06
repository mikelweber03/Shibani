using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;



public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 3;
    private int currentHealth;
    public bool canbedamaged = true;
    public SceneTransition transition;

    public DeathMenu deathmenu;
    public CharacterBlink blink;
    public CheckHealth _healthBar;
    public GameObject player;
    private PlayerKnockback knock;
    private VisualEffect playerHit;
    [SerializeField] private Animator anim;
    void Start()
    {
        knock = GetComponent<PlayerKnockback>();
        currentHealth = maxHealth;
        blink = this.GetComponent<CharacterBlink>();
        playerHit = this.GetComponent<VisualEffect>();
    }

    private void Update()
    {
        CheckPlayerDead();
    }

    void CheckPlayerDead()
    {
        if (currentHealth == 0)
        {
            _healthBar.ChangeHealth(currentHealth);
            //
            //transition.shouldSwitch = true;
            deathmenu.ToggleEndMenu();
            //player.GetComponent("PlayerMovement2").gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CatBullet"))
        {
            if (currentHealth > 1 && canbedamaged == true)
            {
                Destroy(other.gameObject);
                //playerHit.Play();
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                //Debug.Log("Why");
                StartCoroutine("OnInvulnerable");
            }
            else if (currentHealth == 1)
            {
                Destroy(other.gameObject);
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                ///
                //transition.shouldSwitch = true;
                deathmenu.ToggleEndMenu();
                //player.GetComponent("PlayerMovement").gameObject.SetActive(false);
            }
            if (other.CompareTag("DeathPlane"))
            {
                Debug.Log("Skurt");
                currentHealth = 0;
                _healthBar.ChangeHealth(currentHealth);
                //
                //transition.shouldSwitch = true;
                deathmenu.ToggleEndMenu();
            }
            
        }
    }
    //Check if player can loose health
    //if he can then make them loose one and update healthbar
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("CatBullet"))
        {
            if (currentHealth > 1 && canbedamaged == true)
            {
                currentHealth--;
                knock.Knockback();
                //Animation play
                anim.SetTrigger("gotHit");
                //playerHit.Play();
                //blink.tookDamage();
                //Debug.Log("why");
                _healthBar.ChangeHealth(currentHealth);
                Debug.Log("Lop");
                StartCoroutine("OnInvulnerable");

            }
            else if (currentHealth == 1)
            {
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                //deathmenu.ToggleEndMenu();
                //player.GetComponent("PlayerMovement2").gameObject.SetActive(false);
            }
            
            
        }
        //If Healthpickup then regen health and destroy pickup
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth++;
                _healthBar.ChangeHealth(currentHealth);
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.CompareTag("DeathPlane"))
        {
            currentHealth = 0;
            _healthBar.ChangeHealth(currentHealth);
            //transition.shouldSwitch = true;
            deathmenu.ToggleEndMenu();
            //player.GetComponent("PlayerMovement2").gameObject.SetActive(false);
        }
        
    }

    IEnumerator OnInvulnerable()
    {
        canbedamaged = false;

        //blink.tookDamage();
        yield return new WaitForSeconds(1.5f);


        canbedamaged = true;
    }



}
