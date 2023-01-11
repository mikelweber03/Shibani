using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;



public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    public int _maxHealth = 3;
    private int _currentHealth;
    public DeathMenu deathmenu;
    public CharacterBlink blink;
    public CheckHealth _healthBar;
    public bool canbedamaged = true;
    public GameObject player;
    private VisualEffect playerHit;
    void Start()
    {
        _currentHealth = _maxHealth;
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
            if (_currentHealth > 1 && CanBeDamaged() == true)
            {
                Destroy(other.gameObject);
                canbedamaged = false;
                playerHit.Play();
                _currentHealth--;
                _healthBar.ChangeHealth(_currentHealth);

            }
            else if (_currentHealth == 1)
            {
                Destroy(other.gameObject);
                _currentHealth--;
                _healthBar.ChangeHealth(_currentHealth);
                deathmenu.ToggleEndMenu();
                player.GetComponent("PlayerMovement").gameObject.SetActive(false);
            }
        }
    }
    //Check if player can loose health
    //if he can then make them loose one and update healthbar
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_currentHealth > 1 && CanBeDamaged() == true) 
            {
                _currentHealth--;
                playerHit.Play();
                canbedamaged = false;
                blink.tookDamage();
                _healthBar.ChangeHealth(_currentHealth);
                Debug.Log("Succ");
                return;
                
            }
            else if (_currentHealth == 1)
            {
                _currentHealth--;
                _healthBar.ChangeHealth(_currentHealth);
                deathmenu.ToggleEndMenu();
                player.GetComponent("PlayerMovement").gameObject.SetActive(false);
            }
            canbedamaged = true;
        }
        //If Healthpickup then regen health and destroy pickup
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            if (_currentHealth < _maxHealth)
            {
                _currentHealth++;
                _healthBar.ChangeHealth(_currentHealth);
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.CompareTag("Death"))
        {
            _currentHealth = 0;
            _healthBar.ChangeHealth(_currentHealth);
            deathmenu.ToggleEndMenu();
            player.GetComponent("PlayerMovement").gameObject.SetActive(false);
        }
    }

 

}
