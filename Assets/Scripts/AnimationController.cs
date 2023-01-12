using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;


    [SerializeField] ParticleSystem OnHitParticles;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //OnHitParticles = GetComponent<HitEnemy_VFX>();
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

    public void OnEnemyHittedSequence()
    {
        _animator.SetTrigger("OnEnemyHitted");
    }

    private void EnemyDestroySequence()
    {
        _animator.SetTrigger("OnEnemyDeath");
        Destroy(this.gameObject, 2.5f);
    }

    private void AwareOfPlayer()
    {
        _animator.SetBool("SawPlayer?", true);
    }

    private void EnemyFiringSequence()
    {
        _animator.SetTrigger("OnEnemyFiring");
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
