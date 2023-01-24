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
    public DeathMenu deathmenu;
    public CharacterBlink blink;
    public CheckHealth _healthBar;
    public bool canbedamaged = true;
    public GameObject player;
    private PlayerKnockback knock;
    private VisualEffect playerHit;
    void Start()
    {
        knock = GetComponent<PlayerKnockback>();
        currentHealth = maxHealth;
        blink = this.GetComponent<CharacterBlink>();
        playerHit = this.GetComponent<VisualEffect>();
    }

    public bool CanBeDamaged()
    {
        if (canbedamaged == true)
        {
            //Debug.Log("True");
            return true;
        }

        else
        {
            
            return false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CatBullet"))
        {
            if (currentHealth > 1 && CanBeDamaged() == true)
            {
                Destroy(other.gameObject);
                canbedamaged = false;
                //playerHit.Play();
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                Debug.Log("Why");

            }
            else if (currentHealth == 1)
            {
                Destroy(other.gameObject);
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                deathmenu.ToggleEndMenu();
                player.GetComponent("PlayerMovement").gameObject.SetActive(false);
            }
            canbedamaged = true;
        }
    }
    //Check if player can loose health
    //if he can then make them loose one and update healthbar
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (currentHealth > 1 && CanBeDamaged() == true) 
            {
                currentHealth--;
                knock.Knockback();
                //playerHit.Play();
                canbedamaged = false;
                //blink.tookDamage();
                Debug.Log("why");
                _healthBar.ChangeHealth(currentHealth);
                
                
            }
            else if (currentHealth == 1)
            {
                currentHealth--;
                _healthBar.ChangeHealth(currentHealth);
                deathmenu.ToggleEndMenu();
                player.GetComponent("PlayerMovement2").gameObject.SetActive(false);
            }
            canbedamaged = true;
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
        if (collision.gameObject.CompareTag("Death"))
        {
            currentHealth = 0;
            _healthBar.ChangeHealth(currentHealth);
            deathmenu.ToggleEndMenu();
            player.GetComponent("PlayerMovement").gameObject.SetActive(false);
        }
    }

 

}
