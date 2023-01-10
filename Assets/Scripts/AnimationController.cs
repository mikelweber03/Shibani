using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
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

}
