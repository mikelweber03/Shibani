using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController player;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NullCheck()
    {
        if (_animator == null)
            Debug.LogError("'_animator' is NULL! Try assigning the Animation to the correct Component");
    }

    private void EnemyDestroySequence()
    {
        _animator.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 2.5f);
    }

    private void CheckDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        Debug.Log(distanceToPlayer);
    }


    //////////////////////////////////////////////////////////////////////////
    ///
    //* Copy and put into the Player Movement, when colliding with Enemy

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Turret"))
    //    {
    //        Destroy(other.gameObject);
    //        EnemyDestroySequence();
    //    }
    //}

}
