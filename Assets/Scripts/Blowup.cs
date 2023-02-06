using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Blowup : MonoBehaviour
{
    VisualEffect blow;
    // Start is called before the first frame update
    void Start()
    {
        blow = GetComponent<VisualEffect>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            blow.Play();
        }
    }
}
