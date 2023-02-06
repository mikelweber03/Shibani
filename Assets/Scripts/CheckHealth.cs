using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
//using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class CheckHealth : MonoBehaviour
{
    public float _heartSaturation = 0f;
    public float _heart1Saturation = -25f;
    public float _heart2Saturation = -50f;
    public float _heart3Saturation = -100f;
    public Volume vol;
    public ColorAdjustments CA;
    

    Image _heart;
    Image _heart1;
    Image _heart2;
    Image _heart3;
    
    void Start()
    {
        //_heart = GetComponent<Image>();
       // CA.saturation.value = _heartSaturation;
        _heart = GameObject.Find("Heart").GetComponent<Image>();
        _heart1 = GameObject.Find("Heart1").GetComponent<Image>();
        _heart2 = GameObject.Find("Heart2").GetComponent<Image>();
        _heart3 = GameObject.Find("Heart3").GetComponent<Image>();
        //vol.profile.TryGet<ColorAdjustments>(out CA);

    }
    public void ChangeHealth(int currentHealth)
    {
        //Checks at what health the player should be and displays it
        if(currentHealth == 3)
        {
            _heart.enabled = true;
            _heart1.enabled = false;
            _heart2.enabled = false;
            _heart3.enabled = false;
            // CA.saturation.value = _heartSaturation;

        }

        if (currentHealth == 2)
        {
            _heart.enabled = false;
            _heart1.enabled = true;
            _heart2.enabled = false;
            _heart3.enabled = false;
            // CA.saturation.value = _heart1Saturation;

        }
        if (currentHealth == 1)
        {
            _heart.enabled = false;
            _heart1.enabled = false;
            _heart2.enabled = true;
            _heart3.enabled = false;
            // CA.saturation.value = _heart2Saturation;

        }
        if (currentHealth == 0)
        {
            _heart.enabled = false;
            _heart1.enabled = false;
            _heart2.enabled = false;
            _heart3.enabled = true;
            // CA.saturation.value = _heart3Saturation;

        }
    }
}
