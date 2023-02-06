using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileDamage = 10f;
    [SerializeField] private float projectileLifeTime = 5f;


    void Update()
    {
        transform.Translate(Vector3.left * projectileSpeed * Time.deltaTime);
        Destroy(gameObject, projectileLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerManager.Instance.TakeDamageFromEnemy(projectileDamage);
            Destroy(gameObject);
            Debug.Log("PlayerGotHit");
        }
        else
        {
            Destroy(gameObject, projectileLifeTime);
        }
    }
}
